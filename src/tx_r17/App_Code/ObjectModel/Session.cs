using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using region4;

namespace escWeb.tx_r17.ObjectModel
{
    [Serializable]
    public class Session : region4.ObjectModel.Session
    {
        private string _dimensions = string.Empty;
        public string Dimensions { get { return this._dimensions; } }

        private string _standards = string.Empty;
        public string Standards { get { return this._standards; } }

        private string _SessionContacts = string.Empty;
        public string SessionContacts { get { return this._SessionContacts; } }

        public Session(int session_id) : base(session_id)
        {
                    
        }

        public override HtmlTableRow DisplayOneBreakoutSessionForConference(System.Web.HttpContext context, region4.escWeb.BasePage page, bool sessionTimesSame, int conferenceId, int ItemId)
        {
            HtmlTableRow row;

            row = new HtmlTableRow();
            row.Cells.Add(new HtmlTableCell());
            row.Cells.Add(new HtmlTableCell());
            row.Cells[0].Attributes.Add("class", "breakoutAltRow");


            row.Cells[0].Width = "75%";
            row.Cells[1].Width = "25%";
            string result = string.Empty;
            region4.ObjectModel.SessionRegistration registration = new region4.ObjectModel.SessionRegistration(this, page.CurrentUser);
            decimal userFee = registration.ReturnUserFee();
            if (sessionTimesSame)
                result = String.Format("<b>Session ID: {0}</b><br/>{1}<br/>Seats Filled: {2} / {3}", ID, Subtitle, NumberOfAttendeesRegistered, Limit);
            else
                result = String.Format("<b>Session ID: {0}</b><br/>{1}<br/>Date/Time: {4:D} {4:t} - {5:t}<br />Seats Filled: {2} / {3}",
                                                                ID, Subtitle, NumberOfAttendeesRegistered, Limit, StartDate, EndDate);

            if (userFee > 0)
                result += String.Format("<br />Fee: {0:c}", userFee);

            result += "<br />Location:" + SiteRoomDisplay;

            if (!String.IsNullOrEmpty(WebComments))
                result += "<br /><br /><b>Important Breakout Information:</b><br />" + WebComments;

            result += "<hr />";

            row.Cells[0].InnerHtml = result;

            Button button = new Button();
            button.CommandArgument = this.ID.ToString();
            button.ID = String.Format("btnRegister_Session_{0}", this.ID);
           // button.Attributes.Add("aria-describedby", this.ID.ToString()); //508 complaince

            bool bDisplayRegisterButton = false;
            if (page.CurrentUser.isDistrictRegister || page.CurrentUser.isCampusRegister || page.CurrentUser.isGlobalRegister)
            {
                bDisplayRegisterButton = region4.escWeb.SiteVariables.ObjectProvider.DisplayGroupRegisterButton(this, page, context);
            }
            else
            {
                bDisplayRegisterButton = region4.escWeb.SiteVariables.ObjectProvider.DisplayRegisterButton(this, page, context);
            }
            button.Visible = bDisplayRegisterButton;
            
            row.Cells[1].Controls.Add(button);

            region4.ObjectModel.ConferenceRegistration conferenceRegistration = region4.WebControls.ConferenceSelection.GetCurrentConferenceRegistration(conferenceId, region4.escWeb.SiteVariables.ObjectProvider.ReturnUser(Guid.Empty));

            if (SessionSelected(conferenceRegistration.SessionIDList, this.ID))
            {
                button.Text = "Remove";
                button.OnClientClick = "SelectButtonClick(this, " + conferenceId + ", " + this.ID.ToString() + ");return false;";
            }
            else
            {
                button.Text = "Select";
                button.OnClientClick = "SelectButtonClick(this, " + conferenceId + ", " + this.ID.ToString() + ");return false;";
            }

            LiteralControl lblSessionFull = new LiteralControl(this.RegistrationStatusForConference(context, page, page.CurrentUser.isDistrictRegister || page.CurrentUser.isCampusRegister || page.CurrentUser.isGlobalRegister));
            lblSessionFull.Visible = !bDisplayRegisterButton;
            row.Cells[1].Controls.Add(lblSessionFull);
            //row.Cells[0].InnerHtml += String.Format("<br/>");
            row.Cells[1].Style.Add("text-align", "center");

            return row;
        }


        protected override void LoadCustomerData(SqlDataReader reader)
        {
            this._dimensions = reader["dimensions"].ToString();
            this._standards = reader["standards"].ToString();
            this._SessionContacts = reader["sessionContacts"].ToString();

            this._PrevSessionID = (int)(reader["PrerequisiteSessionID"] == DBNull.Value ? -1 : reader["PrerequisiteSessionID"]); //Added by VM 6-28-2018
            this._NextSessionID = (int)(reader["NextSessionID"] == DBNull.Value ? -1 : reader["NextSessionID"]);//Added by VM 6-28-2018
        }

        protected override void LoadCustomerData(System.Data.SqlClient.SqlCommand cmd)
        {
           cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "SELECT allowPO FROM [event.session] WHERE([obj_id] = @session_id)";
            cmd.Parameters.AddWithValue("@session_id", base.obj_id);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                reader.Read();
                this._allowPO = Convert.ToBoolean(reader["allowPO"].ToString());
            } 
    
        }

        protected override void SaveCustomerData(System.Data.SqlClient.SqlCommand cmd)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        //TTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTT //Added by VM 6-28-2018
        public override int Limit
        {
            get
            {
                if (IsSelfPacedOnline || (IsOnDemandOnline && _limit == 0))
                    return 999999;
                else
                    return _limit;
            }

        }
        //LLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLL // Added by VM 6-28-2018
    }

}