using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class shoebox_registration_cancel : region4.escWeb.BasePage
{
    region4.ObjectModel.Attendee myAttendee;
    private region4.Utilities.Financial.CancelRegistration CancelData = new region4.Utilities.Financial.CancelRegistration();
    private escWeb.tx_r17.ObjectModel.CancelRegistration CustomerData = new escWeb.tx_r17.ObjectModel.CancelRegistration();

    protected int attendee_id;
    protected int eventTypeID;




    private int _attendee_id;
    protected Panel _pCancelDetails, _pUnavailable, _pSuccess, _pError;
    private region4.ObjectModel.Attendee attendee;




    protected void Page_Init(object sender, EventArgs e)
    {
        // AssignControlsToBase();
        //  btnSubmit.Click += new EventHandler(btnSubmit_Click);

        //Retrieve attendee id from query string, if not available display an error and return
        if (!Int32.TryParse(Request.QueryString["attendee_id"], out _attendee_id))
        {
            HideAllPanels();
            pError.Visible = true;
            return;
        }

        //Get the attendee object
        attendee = region4.escWeb.SiteVariables.ObjectProvider.ReturnAttendee(_attendee_id);
        myAttendee = attendee;

        //If a loading error occurred or the attendee is already cancelled display an error
        if (attendee.ErrorOccurred || attendee.Cancelled)
        {
            HideAllPanels();
            pError.Visible = true;
            return;
        }

    }

    public void btnSubmit_Click(object sender, EventArgs e)
    {
        //Hide all panels
        HideAllPanels();
        //Cancel Registration
        if (Request.Form["eventtype"] == "1258")
        {
            pAdmin.Visible = true;
            return;

        }

        else if (DateTime.Now < cancellationEndDate(myAttendee))
        {

            if (attendee.CancelRegistration())
                pSuccess.Visible = true;
            else
                pError.Visible = true;
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        //If an error has occurred you don't have to do any more work
        if (pError.Visible || pSuccess.Visible)
            return;

        HideAllPanels();
        if (DateTime.Now > attendee.Session.CancellationEndDate)
            pUnavailable.Visible = true;
        else
            pCancelDetails.Visible = true;

        RenderPage(attendee);


    }

    private void HideAllPanels()
    {
        pCancelDetails.Visible = pError.Visible = pSuccess.Visible = pUnavailable.Visible = false;
    }


    protected void RenderPage(region4.ObjectModel.Attendee attendee)
    {
        lblDescription.Text = attendee.Session.Event.Description;
        lblTitle.Text = lblTitle2.Text = attendee.Session.Title;
        lblStartDate.Text = lblStartDate2.Text = String.Format("{0:d} at {0:t}", attendee.Session.StartDate);
        lblLocation.Text = lblLocation2.Text = String.Format("{0}, {1}", attendee.Session.Schedule[0].Location.Site.Name, attendee.Session.Schedule[0].Location.Name);
        lblFee.Text = String.Format("{0:c}", attendee.Fee);
        lblSessionID.Text = lblSessionId2.Text = attendee.Session.ID.ToString();
        eventTypeID = attendee.Session.Event.EventType.ItemId;

        myAttendee = attendee;
        attendee_id = Convert.ToInt32(Request.QueryString["attendee_id"]);
        // this.Form.Controls.Add(new LiteralControl("<input type=\"hidden\" name=\"eventtype\" value=\"" + eventTypeID + "\"/>"));


    }

    protected void Page_PreRenderComplete(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (DateTime.Now > attendee.Session.CancellationEndDate)
            {

                pUnavailable.Visible = false;
                pCancelDetails.Visible = true;

                btnSubmit.Attributes.Add("onclick", "javascript:CancellationMessage();");
            }

            else
            {


                //if (myAttendee.Session.Event.EventType.ItemId == Convert.ToInt32(ConfigurationManager.AppSettings["onlineCourseTypeID"]))
                if (myAttendee.Session.IsOnline)
                {
                    pUnavailable.Visible = false;
                    pCancelDetails.Visible = true;


                    btnSubmit.Attributes.Add("onclick", "javascript:CancellationMessage();");
                }
                else
                {
                    pUnavailable.Visible = false;
                    pCancelDetails.Visible = true;
                    btnSubmit.Attributes.Add("onclick", "javascript:CancellationMessage();");
                }
            }

        }
        else
        {
            if (DateTime.Now > attendee.Session.CancellationEndDate)
            {

                pUnavailable.Visible = true;
                pCancelDetails.Visible = false;


            }
            else
            {
                //if (myAttendee.Session.Event.EventType.ItemId == Convert.ToInt32(ConfigurationManager.AppSettings["onlineCourseTypeID"]))
                if (myAttendee.Session.IsOnline)
                {
                    pAdmin.Visible = true;
                    pCancelDetails.Visible = false;
                }
                else
                {
                    pCancelDetails.Visible = false;
                }

                attendee_id = Convert.ToInt32(Request.QueryString["attendee_id"]);
                string[] PayOptions = CancelData.GetPaymentType(attendee_id);//attendee id as parameter
                if (PayOptions.Length == 0)
                    AfterCancelAction();
                else
                {

                    foreach (string paytype in PayOptions)
                    {
                        string[] xx = paytype.Split(',');

                        switch (xx[0].ToString().ToLower().Trim())
                        {
                            case "ae":
                            case "mc":
                            case "vs":
                            case "dc":

                                DataSet DS = CancelData.GetPNREF(attendee_id, Convert.ToInt32(xx[1].ToString()));
                                CustomerData.ProcessCCRefund(DS, myAttendee);
                                myAttendee.CancelRegistration();
                                //AfterCancelAction();
                                DisplayCancelledSession(myAttendee);
                                break;
                            case "ach":
                                DataSet ACHDS = CancelData.GetPNREF(attendee_id, Convert.ToInt32(xx[1].ToString()));
                                CustomerData.ProcessDebit(ACHDS, myAttendee);
                                //AfterCancelAction();
                                myAttendee.CancelRegistration();
                                DisplayCancelledSession(myAttendee);

                                break;
                            case "po":
                            case "mo":
                            case "cs":
                            case "ck":
                                DataSet PODS = CancelData.GetPNREF(attendee_id, Convert.ToInt32(xx[1].ToString()));
                                CustomerData.ProcessPORefund(PODS, Convert.ToInt32(xx[1].ToString()), myAttendee);
                                myAttendee.CancelRegistration();
                                //AfterCancelAction();
                                DisplayCancelledSession(myAttendee);
                                break;

                            default:
                                break;
                        }
                    }
                }
            }

        }
    }

    protected DateTime cancellationEndDate(region4.ObjectModel.Attendee attendee)
    {

        switch (attendee.Session.StartDate.DayOfWeek)
        {
            case DayOfWeek.Monday:
            case DayOfWeek.Tuesday:
            case DayOfWeek.Wednesday:
                return attendee.Session.StartDate.AddDays(-7);
                break;
            case DayOfWeek.Thursday:
            case DayOfWeek.Friday:
            case DayOfWeek.Saturday:
                return attendee.Session.StartDate.AddDays(-7);
                break;
            default:
                return attendee.Session.StartDate.AddDays(-7);
                break;
        }
    }

    private void AfterCancelAction()
    {

        //
        // Remove from session and display result
        // pCancelDetails.Visible = true;
        myAttendee.CancelRegistration();
        DisplayCancelledSession(myAttendee);




    }

    private void DisplayCancelledSession(region4.ObjectModel.Attendee attendee)
    {
        pSuccess.Visible = true;

        lblTitle2.Text = attendee.Session.Title;
        lblStartDate2.Text = String.Format("{0:d} at {0:t}", attendee.Session.StartDate);
        lblLocation2.Text = String.Format("{0}, {1}", attendee.Session.Schedule[0].Location.Site.Name, attendee.Session.Schedule[0].Location.Name);

        lblSessionId2.Text = attendee.Session.ID.ToString();
    }

}
