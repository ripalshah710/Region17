using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace escWeb.tx_r17.ObjectModel
{
    /// <summary>
    /// Summary description for AttendeeInfo
    /// </summary>
    [Serializable]
    public class AttendeeInfo : region4.ObjectModel.AttendeeInfo
    {
        private bool _ondemandAllowReview = false; //Added by VM 6-28-2018

        public bool OndemandAllowReview { get { return _ondemandAllowReview; } } //Added by VM 6-28-2018

        public AttendeeInfo(DataRow reader) : base(reader)
        {
            //
            // TODO: Add constructor logic here
            //
        }

        //TTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTT Added by VM 6-28-2018
        protected override void LoadCustomerData(DataRow reader)
        {
            _ondemandAllowReview = (bool)reader["ondemandAllowReview"];
            this._PrevSessionID = (int)(reader["PrerequisiteSessionID"] == DBNull.Value ? -1 : reader["PrerequisiteSessionID"]);
            this._PrevSessionIDAttended = (bool)(reader["PrevSessionIDAttended"] == DBNull.Value ? false : reader["PrevSessionIDAttended"]);
        }
        //LLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLL


        public override bool ShowChangeRegistrationLink
        {
            get
            {
                if (IsInstructorLedOnline && IsMultiVenue)
                    return ((DateTime.Now < RegistrationEndDate)
                    && (DateTime.Now < CancellationEndDate));
                else
                    return ((DateTime.Now < RegistrationEndDate)
                    && (DateTime.Now < CancellationEndDate)
                    && !(IsOnline)
                    && (CheckAvailableSessionsCount()));
            }
        }

        protected virtual bool CheckAvailableSessionsCount()
        {
            region4.ObjectModel.Attendee _attendee = region4.escWeb.SiteVariables.ObjectProvider.ReturnAttendee(ID);
            // escWeb.demo.ObjectModel.Attendee _attendee = attendee as escWeb.demo.ObjectModel.Attendee;
            List<region4.ObjectModel.Session> sessions = _attendee.Session.Event.UpcomingSessions;
            bool retVal = false;
            foreach (ObjectModel.Session session in sessions)
            {
                if (session == _attendee.Session)
                    continue;

                bool breakOutTheSame = true;
                if (session.Event is ObjectModel.Conference || session.Event is ObjectModel.MultiVenue)
                    breakOutTheSame &= session.BreakoutSession == _attendee.Session.BreakoutSession;
                region4.ObjectModel.SessionRegistration registration = region4.escWeb.SiteVariables.ObjectProvider.ReturnSessionRegistration(session, _attendee.User);

                if (registration.ReturnUserFee() == _attendee.Fee && session.DisplayOnWeb && DateTime.Now < session.RegistrationEndDate && !session.SessionFull && !session.IsUserRegistered(_attendee.User.Sid) && breakOutTheSame)
                    //_rblSessions.Items.Add(new ListItem(ReturnSessionSummary(session), session.ID.ToString()));
                    retVal = true;
                break;
            }
            return retVal;
        }

        //TTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTT //Added by VM 6-28-2018

        public override void AddOnlineCompleteTile(HtmlTableRowCollection htmlTableRowCollection)
        {
            //Session ID
            HtmlTableRow row = new HtmlTableRow();
            row.Cells.Add(new HtmlTableCell());
            row.Cells.Add(new HtmlTableCell());

            row.Cells[0].Attributes.Add("class", "onlineHeading");
            row.Cells[0].Controls.Add(new LiteralControl(string.Format("<b>{0}</b>", "Session ID:")));

            row.Cells[1].Controls.Add(new LiteralControl(string.Format("{0}", SessionID)));
            row.Cells[1].Attributes.Add("class", "onlineContent");

            htmlTableRowCollection.Add(row);
            //space
            row = new HtmlTableRow();
            row.Cells.Add(new HtmlTableCell());
            htmlTableRowCollection.Add(row);

            //Title
            row = new HtmlTableRow();
            row.Cells.Add(new HtmlTableCell());
            row.Cells.Add(new HtmlTableCell());

            row.Cells[0].Attributes.Add("class", "onlineHeading");
            row.Cells[0].Controls.Add(new LiteralControl(string.Format("<b>{0}</b>", "Session Title:")));

            row.Cells[1].Controls.Add(new LiteralControl(string.Format("{0} - {1}", Title, Subtitle)));
            row.Cells[1].Attributes.Add("class", "onlineContent");

            htmlTableRowCollection.Add(row);
            //space
            row = new HtmlTableRow();
            row.Cells.Add(new HtmlTableCell());
            htmlTableRowCollection.Add(row);

            //Credit
            row = new HtmlTableRow();
            row.Cells.Add(new HtmlTableCell());
            row.Cells.Add(new HtmlTableCell());

            row.Cells[0].Attributes.Add("class", "onlineHeading");
            row.Cells[0].Controls.Add(new LiteralControl(string.Format("<b>{0}</b>", "Credit:")));
            row.Cells[1].Controls.Add(new LiteralControl(string.Format("{0} {1}", _CreditDisplay, _CreditAmount.ToString())));
            row.Cells[1].Attributes.Add("class", "onlineContent");

            htmlTableRowCollection.Add(row);
            //space
            row = new HtmlTableRow();
            row.Cells.Add(new HtmlTableCell());
            htmlTableRowCollection.Add(row);

            if (IsInstructorLedOnline)
            {
                //start date
                row = new HtmlTableRow();
                row.Cells.Add(new HtmlTableCell());
                row.Cells.Add(new HtmlTableCell());

                row.Cells[0].Attributes.Add("class", "onlineHeading");
                row.Cells[0].Controls.Add(new LiteralControl(string.Format("<b>{0}</b>", "Start Date:")));

                row.Cells[1].Attributes.Add("class", "onlineContent");
                row.Cells[1].Controls.Add(new LiteralControl(string.Format("{0}", StartDate.ToLongDateString())));

                htmlTableRowCollection.Add(row);
                //space
                row = new HtmlTableRow();
                row.Cells.Add(new HtmlTableCell());
                htmlTableRowCollection.Add(row);

                //end date
                row = new HtmlTableRow();
                row.Cells.Add(new HtmlTableCell());
                row.Cells.Add(new HtmlTableCell());

                row.Cells[0].Attributes.Add("class", "onlineHeading");
                row.Cells[0].Controls.Add(new LiteralControl(string.Format("<b>{0}</b>", "End Date:")));

                row.Cells[1].Attributes.Add("class", "onlineContent");
                row.Cells[1].Controls.Add(new LiteralControl(string.Format("{0}", EndDate.ToLongDateString())));

                htmlTableRowCollection.Add(row);
                //space
                row = new HtmlTableRow();
                row.Cells.Add(new HtmlTableCell());
                htmlTableRowCollection.Add(row);
            }
            else if (IsOnDemandOnline)
            {
                //score
                row = new HtmlTableRow();
                row.Cells.Add(new HtmlTableCell());
                row.Cells.Add(new HtmlTableCell());

                row.Cells[0].Attributes.Add("class", "onlineHeading");
                row.Cells[0].Controls.Add(new LiteralControl(string.Format("<b>{0}</b>", "Score:")));

                row.Cells[1].Attributes.Add("class", "onlineContent");
                row.Cells[1].Controls.Add(new LiteralControl(string.Format("{0}", _onDemandTotalPoints.ToString())));

                htmlTableRowCollection.Add(row);
                //space
                row = new HtmlTableRow();
                row.Cells.Add(new HtmlTableCell());
                htmlTableRowCollection.Add(row);

                //complete date
                row = new HtmlTableRow();
                row.Cells.Add(new HtmlTableCell());
                row.Cells.Add(new HtmlTableCell());

                row.Cells[0].Attributes.Add("class", "onlineHeading");
                row.Cells[0].Controls.Add(new LiteralControl(string.Format("<b>{0}</b>", "Completed Date:")));

                row.Cells[1].Attributes.Add("class", "onlineContent");
                row.Cells[1].Controls.Add(new LiteralControl(string.Format("{0}", _onDemandcompletedDate.ToString())));

                htmlTableRowCollection.Add(row);


                //OndemandAllowReview
                if (OndemandAllowReview)
                {
                    row.Cells.Add(new HtmlTableCell());

                    Button link = new Button();
                    link.Text = "Review";
                    link.ID = "OpenTraining" + ID.ToString();
                    link.CommandArgument = ID.ToString();
                    link.OnClientClick = String.Format("javascript:OpenTraining('{0}', this); return false;", OnDemandOrgOrAtt);
                    row.Cells[2].Controls.Add(link);
                }

                //space
                row = new HtmlTableRow();
                row.Cells.Add(new HtmlTableCell());
                htmlTableRowCollection.Add(row);
            }

            else
            {
                //expiration date
                row = new HtmlTableRow();
                row.Cells.Add(new HtmlTableCell());
                row.Cells.Add(new HtmlTableCell());

                row.Cells[0].Attributes.Add("class", "onlineHeading");
                row.Cells[0].Controls.Add(new LiteralControl(string.Format("<b>{0}</b>", "Completed Date:")));

                row.Cells[1].Attributes.Add("class", "onlineContent");
                row.Cells[1].Controls.Add(new LiteralControl(string.Format("{0}", AttendedTimeStamp.ToString())));

                htmlTableRowCollection.Add(row);
                //space
                row = new HtmlTableRow();
                row.Cells.Add(new HtmlTableCell());
                htmlTableRowCollection.Add(row);
            }


            //BING Comment out Rating
            //this.AddRating(htmlTableRowCollection);

            //Add link
            if (ShowEvaluationLink)
            {
                row = new HtmlTableRow();
                row.Cells.Add(new HtmlTableCell());
                row.Cells.Add(new HtmlTableCell());

                row.Cells[0].Attributes.Add("class", "onlineHeading");
                row.Cells[0].Controls.Add(new LiteralControl(string.Format("<b>{0}</b>", "Link:")));

                PlaceHolder pLinks = new PlaceHolder();
                row.Cells[1].Attributes.Add("class", "onlineContent");
                LinkButton link = new LinkButton();
                link.Text = "Evaluation";
                link.CssClass = "link";
                link.ID = "OnlineEvaluation" + ID.ToString();
                link.CommandArgument = ID.ToString();
                //                link.OnClientClick = String.Format(@"javascript:window.open('../evaluation/?registrationId={0}&sessionId={1}', 'Evaluation', 'width=575, height=550, resizable=no, scrollbars=no, status=no, menubar=no'); return false;", ID, SessionID);
                link.OnClientClick = String.Format(@"javascript:window.open('../evaluation/?registrationId={0}&sessionId={1}&certificateId={2}', 'Evaluation', 'width=700, height=650, resizable=no, scrollbars=no, status=no, menubar=no');return false;", ID, SessionID, this.CertificateID);
                AddLink(pLinks, link);
                AddOnlineCustomerLinks(pLinks);
                row.Cells[1].Controls.Add(pLinks);
                htmlTableRowCollection.Add(row);
                //space
                row = new HtmlTableRow();
                row.Cells.Add(new HtmlTableCell());
                htmlTableRowCollection.Add(row);
            }
            else if (ShowCertificateLink)
            {
                row = new HtmlTableRow();
                row.Cells.Add(new HtmlTableCell());
                row.Cells.Add(new HtmlTableCell());

                row.Cells[0].Attributes.Add("class", "onlineHeading");
                row.Cells[0].Controls.Add(new LiteralControl(string.Format("<b>{0}</b>", "Link:")));

                PlaceHolder pLinks = new PlaceHolder();
                row.Cells[1].Attributes.Add("class", "onlineContent");
                LinkButton link = new LinkButton();
                link = new LinkButton();
                link.Text = "Certificate";
                link.CssClass = "link";
                link.ID = "OnlineCertificate" + ID.ToString();
                //                link.CommandArgument = ID.ToString();
                link.CommandArgument = ID.ToString() + "|" + this.CertificateID.ToString();
                link.Click += new EventHandler(certificateLink_Click);
                AddLink(pLinks, link);
                AddOnlineCustomerLinks(pLinks);
                row.Cells[1].Controls.Add(pLinks);

                htmlTableRowCollection.Add(row);
                //space
                row = new HtmlTableRow();
                row.Cells.Add(new HtmlTableCell());
                htmlTableRowCollection.Add(row);
            }

            //space
            row = new HtmlTableRow();
            row.Cells.Add(new HtmlTableCell());
            row.BgColor = "#909090";
            row.Cells[0].ColSpan = 2;
            htmlTableRowCollection.Add(row);

        }

        public override void AddOnlineInProgressTile(HtmlTableRowCollection htmlTableRowCollection)
        {
            HtmlTableRow row = new HtmlTableRow();
            row.Cells.Add(new HtmlTableCell());
            row.Cells.Add(new HtmlTableCell());
            row.Cells.Add(new HtmlTableCell());

            row.Cells[0].Attributes.Add("class", "onlineHeading");
            //  row.Cells[0].Attributes.Add("bgcolor","black");
            row.Cells[0].Controls.Add(new LiteralControl(string.Format("<b>{0}</b>", "Session ID:")));

            row.Cells[1].Controls.Add(new LiteralControl(string.Format("{0}", SessionID)));
            row.Cells[1].Attributes.Add("class", "onlineContent");
            row.Cells[2].Attributes.Add("class", "onlineContent");
            row.Cells[2].Align = "right";

            if (IsOnline)
            {

                if (IsInstructorLedOnline || IsSelfPacedOnline)
                {
                    Button link = new Button();
                    link.Text = "Open";
                    link.ID = "CourseLink" + ID.ToString();
                    link.CommandArgument = ID.ToString();

                    if (OnlineCourseUrl.StartsWith("http://") || OnlineCourseUrl.StartsWith("https://"))
                    {
                        link.OnClientClick = String.Format("javascript:window.open('{0}'); return false;", OnlineCourseUrl);
                    }
                    else
                    {
                        link.OnClientClick = String.Format("javascript:alert('{0}'); return false;", OnlineCourseUrl);
                    }
                    row.Cells[2].Controls.Add(link);
                }
                else
                {
                    if (_PrevSessionID > 0 && !_PrevSessionIDAttended)
                    {
                        Button link = new Button();
                        link.Text = "Locked";
                        link.ID = "OpenMessageForPrerequisiteSession" + ID.ToString();
                        link.CommandArgument = ID.ToString();
                        link.OnClientClick = String.Format("javascript: alert('Please complete Session {0} first.'); return false;", _PrevSessionID);
                        row.Cells[2].Controls.Add(link);
                    }
                    else
                    {
                        Button link = new Button();
                        link.Text = "Play";
                        link.ID = "OpenTraining" + ID.ToString();
                        link.CommandArgument = ID.ToString();
                        link.OnClientClick = String.Format("javascript:OpenTraining('{0}', this); return false;", OnDemandOrgOrAtt);
                        row.Cells[2].Controls.Add(link);
                    }
                }

            }
            else
            {
                Button link = new Button();
                link.Text = "Play";
                link.ID = "OpenTraining" + ID.ToString();
                link.CommandArgument = ID.ToString();
                link.OnClientClick = String.Format("javascript:OpenTraining('{0}', this); return false;", OnDemandOrgOrAtt);
                row.Cells[2].Controls.Add(link);
            }

            htmlTableRowCollection.Add(row);
            //space
            row = new HtmlTableRow();
            row.Cells.Add(new HtmlTableCell());
            htmlTableRowCollection.Add(row);

            row = new HtmlTableRow();
            row.Cells.Add(new HtmlTableCell());
            row.Cells.Add(new HtmlTableCell());

            row.Cells[0].Attributes.Add("class", "onlineHeading");

            row.Cells[0].Controls.Add(new LiteralControl(string.Format("<b>{0}</b>", "Session Title:")));

            row.Cells[1].Controls.Add(new LiteralControl(string.Format("{0} - {1}", Title, Subtitle)));
            row.Cells[1].ColSpan = 2;
            row.Cells[1].Attributes.Add("class", "onlineContent");

            htmlTableRowCollection.Add(row);
            //space
            row = new HtmlTableRow();
            row.Cells.Add(new HtmlTableCell());
            htmlTableRowCollection.Add(row);

            if (IsInstructorLedOnline)
            {
                row = new HtmlTableRow();
                row.Cells.Add(new HtmlTableCell());
                row.Cells.Add(new HtmlTableCell());
                row.Cells[0].Attributes.Add("class", "onlineHeading");
                row.Cells[0].Controls.Add(new LiteralControl(string.Format("<b>{0}</b>", "Start Date:")));
                row.Cells[1].ColSpan = 2;
                row.Cells[1].Attributes.Add("class", "onlineContent");

                if (StartDate.Hour == 0) //Midnight
                    row.Cells[1].Controls.Add(new LiteralControl(string.Format("{0}", StartDate.ToLongDateString())));
                else
                    row.Cells[1].Controls.Add(new LiteralControl(String.Format("{0:d} {1:t}", StartDate.ToLongDateString(), StartDate)));
                htmlTableRowCollection.Add(row);
                //space
                row = new HtmlTableRow();
                row.Cells.Add(new HtmlTableCell());
                htmlTableRowCollection.Add(row);

                row = new HtmlTableRow();
                row.Cells.Add(new HtmlTableCell());
                row.Cells.Add(new HtmlTableCell());
                row.Cells[0].Attributes.Add("class", "onlineHeading");
                row.Cells[0].Controls.Add(new LiteralControl(string.Format("<b>{0}</b>", "End Date:")));
                row.Cells[1].ColSpan = 2;
                row.Cells[1].Attributes.Add("class", "onlineContent");

                if (StartDate.Hour == 0) //Midnight
                    row.Cells[1].Controls.Add(new LiteralControl(string.Format("{0}", EndDate.ToLongDateString())));
                else
                    row.Cells[1].Controls.Add(new LiteralControl(String.Format("{0:d} {1:t}", EndDate.ToLongDateString(), EndDate)));
                htmlTableRowCollection.Add(row);

                if (ShowChangeRegistrationLink) //For instructor led online event, if event type is multi velue, we can switch registration
                {
                    row = new HtmlTableRow();
                    row.Cells.Add(new HtmlTableCell());

                    PlaceHolder pLinks = new PlaceHolder();
                    LinkButton link = new LinkButton();
                    link.Text = "Change Registration";
                    link.CommandArgument = ID.ToString();
                    link.CssClass = "link";
                    link.ID = "changeregistration_" + ID.ToString();
                    link.Click += new EventHandler(changeRegistrationLink_Click);
                    AddLink(pLinks, link);
                    row.Cells[0].Controls.Add(pLinks);
                    htmlTableRowCollection.Add(row);
                }
                //space
                row = new HtmlTableRow();
                row.Cells.Add(new HtmlTableCell());
                htmlTableRowCollection.Add(row);
            }
            else
            {
                row = new HtmlTableRow();
                row.Cells.Add(new HtmlTableCell());
                row.Cells.Add(new HtmlTableCell());
                row.Cells[0].Attributes.Add("class", "onlineHeading");
                row.Cells[0].Controls.Add(new LiteralControl(string.Format("<b>{0}</b>", "Expiration Date:")));
                row.Cells[1].Controls.Add(new LiteralControl(string.Format("{0}", OnlineExpirationDate.ToString())));
                row.Cells[1].ColSpan = 2;
                row.Cells[1].Attributes.Add("class", "onlineContent");
                htmlTableRowCollection.Add(row);
                //space
                row = new HtmlTableRow();
                row.Cells.Add(new HtmlTableCell());
                htmlTableRowCollection.Add(row);
            }

            row = new HtmlTableRow();
            row.Cells.Add(new HtmlTableCell());
            row.BgColor = "#909090";
            row.Cells[0].ColSpan = 3;
            htmlTableRowCollection.Add(row);

        }

        public override string SiteRoomDisplay
        {
            get { return String.Format("{0} {1}", SiteName, RoomName == string.Empty ? "" : "," + RoomName); }
        }

        //LLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLL //Added by VM 6-28-2018
        
    }
}