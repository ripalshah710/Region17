using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Net.Mail;
using System.Web.UI;
using System.Configuration;
using escWorks.BusinessObjects;
using System.Data.SqlClient;


/// <summary>
/// Summary description for EmailProvider
/// </summary>
public class EmailProvider : region4.Utilities.Email.baseEmailProvider
{
    protected override string CustomerLogoUrl
    {
        get
        {
            return "lib/img/reg17small.jpg";
        }
    }

    public override HtmlTable ReturnConfirmationReport(region4.ObjectModel.Attendee attendee, bool embedLogo)
    {
        //Render HtmlTable
        HtmlTable table = new HtmlTable();
        table.Width = "75%";

        HtmlTableRow row;

        if (CustomerLogo != null)
        {
            row = new HtmlTableRow();
            row.Cells.Add(new HtmlTableCell());
            row.Cells[0].Align = "left";
            row.Cells[0].Width = "50%";
            row.Cells[0].InnerHtml = String.Format("<img src=\"{0}\" alt=\"Region17 Logo\"/>", embedLogo ? "cid:customerLogo" : pathToRoot + CustomerLogoUrl);
            row.Cells.Add(new HtmlTableCell());
            row.Cells[1].Align = "right";
            row.Cells[1].Width = "50%";

            row.Cells[1].InnerHtml = String.Format("<a href=\"{0}\">Manage Your Account</a>&nbsp;|&nbsp;<a href=\"{1}\">Courses</a>",
                 pathToRoot + "shoebox/registration/default.aspx",
                 pathToRoot + "search.aspx");
            table.Rows.Add(row);
        }

        //Date
        row = new HtmlTableRow();
        row.Cells.Add(new HtmlTableCell());
        row.Cells[0].ColSpan = 2;
        System.Text.StringBuilder builder = new System.Text.StringBuilder();
        builder.AppendFormat("<br /><br />{0}, {1} {2}, {3} at {4:t}<br /><br />", System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.AbbreviatedDayNames[(int)attendee.RegistrationTime.DayOfWeek], System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.AbbreviatedMonthNames[(int)attendee.RegistrationTime.Month - 1], attendee.RegistrationTime.Day, attendee.RegistrationTime.Year, attendee.RegistrationTime);
        builder.AppendFormat("{0} {1} <br />", attendee.User.FirstName, attendee.User.LastName);

        if (!String.IsNullOrEmpty(attendee.User.Address))
        {
            builder.AppendFormat("{0}<br />", attendee.User.Address);
            builder.AppendFormat("{0}, {1} {2} <br /> <br />", attendee.User.City, attendee.User.State, attendee.User.Zip);
        }
        else
            builder.Append("<br /><br />");

        builder.AppendFormat("<div style=\"font-size: 12pt; font-weight: bold\">Confirmation Number: <font color=\"green\">{0}-{1}-{2}</font></div>", attendee.Session.Event.EventID, attendee.Session.ID, attendee.ID);
       
        if (attendee.Creator != null)
            builder.AppendFormat("<br />Thank you for your registration. This confirms your registration for the following workshop by {0}. If payment was required, your receipt is included in the Payments Received section below.<br/><br/>", attendee.Creator.FullName);
        else
            builder.AppendFormat("<br />Thank you for your registration. This confirms your registration for the following workshop. If payment was required, your receipt is included in the Payments Received section below.<br/><br/>");

        if (!String.IsNullOrEmpty(attendee.Session.Subtitle))
            builder.AppendFormat("<span style=\"font-weight: bold;\">{0}</span><br />{1}<br />{2}<br /> <br />", attendee.Session.Event.Title, attendee.Session.Subtitle, attendee.Session.Event.Description);
        else
            builder.AppendFormat("<span style=\"font-weight: bold;\">{0}</span><br />{2}<br /> <br />", attendee.Session.Event.Title, attendee.Session.Subtitle, attendee.Session.Event.Description);

        builder.AppendFormat("<strong>Session ID: </strong>{0}<br /><br />", attendee.Session.ID);

        row.Cells[0].InnerHtml = builder.ToString();

        table.Rows.Add(row);

        //For online session
        if (attendee.Session.IsOnline)
        {
            row = new HtmlTableRow(); //Location
            row.Cells.Add(new HtmlTableCell());
            row.Cells.Add(new HtmlTableCell());

            row.Cells[0].InnerHtml = "<b>Location:<b>";
            row.Cells[1].InnerHtml = "Online";
            table.Rows.Add(row);

            if (attendee.Session.IsSelfPacedOnline || attendee.Session.IsOnDemandOnline)
            {
                row = new HtmlTableRow(); //Subscription length
                row.Cells.Add(new HtmlTableCell());
                row.Cells.Add(new HtmlTableCell());

                row.Cells[0].InnerHtml = "<b>Subscription Length:<b>";
                row.Cells[1].InnerHtml = attendee.Session.SubscriptionLength;
                table.Rows.Add(row);

                row = new HtmlTableRow(); //Expiration Date
                row.Cells.Add(new HtmlTableCell());
                row.Cells.Add(new HtmlTableCell());

                row.Cells[0].InnerHtml = "<b>Expiration Date:<b>";
                row.Cells[1].InnerHtml = attendee.Session.OnlineExpirationDate.ToShortDateString();
                table.Rows.Add(row);
            }
            else
            {
                row = new HtmlTableRow(); //Dates
                row.Cells.Add(new HtmlTableCell());
                row.Cells.Add(new HtmlTableCell());

                row.Cells[0].InnerHtml = "<b>Dates:<b>";
                row.Cells[1].InnerHtml = attendee.Session.StartDate.ToShortDateString() + " - " + attendee.Session.EndDate.ToShortDateString();
                table.Rows.Add(row);
            }
        }
        else
        {
            //Date/Time Location
            row = new HtmlTableRow();
            row.Cells.Add(new HtmlTableCell());
            row.Cells.Add(new HtmlTableCell());

            DateTime currentStart, currentEnd;
            currentStart = currentEnd = new DateTime(1919, 10, 9);

            row.Cells[0].InnerHtml = "<b>Dates/Times:<b>";
            row.Cells[1].InnerHtml = "<b>Location:</b>";
            table.Rows.Add(row);

            foreach (region4.ObjectModel.ScheduleItem schedule in attendee.Session.Schedule)
            {
                row = new HtmlTableRow();
                row.Cells.Add(new HtmlTableCell());
                row.Cells.Add(new HtmlTableCell());

                if (schedule.StartDate == currentStart && schedule.EndDate == currentEnd)
                    continue;
                row.Cells[0].InnerHtml = String.Format("{0:d} {0:t} - {1:t}<br />", schedule.StartDate, schedule.EndDate);
                currentStart = schedule.StartDate;
                currentEnd = schedule.EndDate;
                region4.ObjectModel.Room room = schedule.Location;
                row.Cells[1].InnerHtml += string.Format("{0}:{1} {2} {3}, {4} {5}<br />", room.Site.Name, room.Name,
                   room.Site.IsServiceCenter ? room.Site.Address1 : room.Address1,
                    room.Site.IsServiceCenter ? room.Site.City : room.City,
                    room.Site.IsServiceCenter ? room.Site.State : room.State,
                    room.Site.IsServiceCenter ? room.Site.Zip : room.Zip);
                table.Rows.Add(row);
            }
        }

        if (!String.IsNullOrEmpty(attendee.Session.ConfirmationComments))
        {
            row = new HtmlTableRow();
            row.Cells.Add(new HtmlTableCell());
            row.Cells[0].ColSpan = 2;
            row.Cells[0].InnerHtml = String.Format("<b>Additional Information:</b><br />{0}", attendee.Session.ConfirmationComments);
            table.Rows.Add(row);
        }

        table.Rows.Add(row);

        row = new HtmlTableRow();
        row.Cells.Add(new HtmlTableCell());
        row.Cells[0].ColSpan = 2;
        row.Cells[0].InnerHtml = String.Format("<b>In times of icy or snowy weather, please check the <a href=\"http://www.esc17.net\"> ESC 17</a> website for workshop cancellations or late starts.</b><br />");
        table.Rows.Add(row);

        if (attendee.Payments.Count > 0)
        {
            row = new HtmlTableRow();
            row.Cells.Add(new HtmlTableCell());
            row.Cells[0].ColSpan = 2;

            row.Cells[0].InnerHtml = "<strong><br />Payments Received/Submitted:</strong><br />The following payments have been received for/submitted to your account:<br /><br />";

            table.Rows.Add(row);

            row = new HtmlTableRow();
            row.Cells.Add(new HtmlTableCell());
            row.Cells[0].ColSpan = 2;

            HtmlTable detailTable = new HtmlTable();
            HtmlTableRow detailRow = new HtmlTableRow();

            detailTable.Style.Add("border", "solid #c0c0c0 1px");
            detailTable.Style.Add("border-collapse", "collapse");
            detailTable.Align = "center";
            detailTable.Width = "100%";

            detailRow = new HtmlTableRow();
            detailRow.Cells.Add(new HtmlTableCell());
            detailRow.Cells.Add(new HtmlTableCell());
            detailRow.Cells.Add(new HtmlTableCell());
            detailRow.Cells.Add(new HtmlTableCell());
            detailRow.Cells.Add(new HtmlTableCell());

            detailRow.Style.Add("font-weight", "bold");

            detailRow.Cells[0].InnerText = "Date Submitted";
            detailRow.Cells[1].InnerText = "Payment Type";
            detailRow.Cells[2].InnerText = "Amount";
            detailRow.Cells[3].InnerText = "Status";
            detailRow.Cells[4].InnerText = "Reference/Receipt";

            detailTable.Rows.Add(detailRow);

            foreach (region4.ObjectModel.PaymentItem p in attendee.Payments)
            {
                detailRow = new HtmlTableRow();
                detailRow.Cells.Add(new HtmlTableCell());
                detailRow.Cells.Add(new HtmlTableCell());
                detailRow.Cells.Add(new HtmlTableCell());
                detailRow.Cells.Add(new HtmlTableCell());
                detailRow.Cells.Add(new HtmlTableCell());

                detailRow.Cells[0].Style.Add("border", "1px solid #c0c0c0");
                detailRow.Cells[1].Style.Add("border", "1px solid #c0c0c0");
                detailRow.Cells[2].Style.Add("border", "1px solid #c0c0c0");
                detailRow.Cells[3].Style.Add("border", "1px solid #c0c0c0");
                detailRow.Cells[4].Style.Add("border", "1px solid #c0c0c0");

                detailRow.Cells[0].InnerText = String.Format("{0:d}", p.timestamp.Date);
                detailRow.Cells[1].InnerText = p.type.Display;
                detailRow.Cells[2].InnerText = String.Format("{0:c}", p.amount);
                detailRow.Cells[3].InnerText = p.status;
                detailRow.Cells[4].InnerHtml = "&nbsp;" + p.reference;

                detailTable.Rows.Add(detailRow);
            }

            row.Cells[0].Controls.Add(detailTable);
            table.Rows.Add(row);
        }

        row = new HtmlTableRow();
        row.Cells.Add(new HtmlTableCell());
        row.Cells[0].ColSpan = 2;
        row.Cells[0].Align = "left";
        row.Cells[0].InnerHtml = String.Format("<br/><b>Questions?</b></br>Manage your <a href=\"{0}\">registrations online</a><br/>Contact by <a href=\"{1}\">Email</a> or Phone : {2} <br/><br/>",
             region4.escWeb.SiteVariables.CustomerHostUrl + "shoebox/registration/default.aspx",
             attendee.Session.ContactPerson.PrimaryEmail, attendee.Session.ContactPerson.WorkPhone);

        table.Rows.Add(row);

        row = new HtmlTableRow();
        row.Cells.Add(new HtmlTableCell());
        row.Cells[0].ColSpan = 2;
        row.Cells[0].Align = "center";
        row.Cells[0].InnerHtml = "<a href=\"http://www.esc17.net\"><font color=\"green\">Region 17 Education Service Center</font></a> 1111 West Loop 289 | Lubbock, TX 79416  | T 806-792-4000 ";
        table.Rows.Add(row);

        row = new HtmlTableRow();
        row.Cells.Add(new HtmlTableCell());
        row.Cells[0].ColSpan = 2;
        row.Cells[0].Align = "right";

        row.Cells[0].InnerHtml = String.Format("<br /><br /><img src=\"{0}\" alt=\"Region17 Logo\" />", embedLogo ? "cid:escWorksLogo" : pathToRoot + "lib/img/pwrdby_clear.gif");
        table.Rows.Add(row);

        return table;
    }

    public override void AddAdditionalEmailAddresses(escWorks.BusinessObjects.SendEmail sendEmail, region4.ObjectModel.Attendee attendee)
    {
        escWeb.tx_r17.ObjectModel.Location loc = new escWeb.tx_r17.ObjectModel.Location(attendee.User.LocationID);

        if (loc.ContactEmail != null)
            sendEmail.AddEmailBCC(loc.ContactEmail);
    }

    public override bool SendConfirmationEmail(region4.ObjectModel.Attendee attendee, bool appendPaymentVoucher)
    {
        HtmlTable table = this.ReturnConfirmationReport(attendee, true);

        StringBuilder result = new StringBuilder();
        System.IO.StringWriter writer = new System.IO.StringWriter(result);
        System.Web.UI.HtmlTextWriter html = new HtmlTextWriter(writer);
        table.RenderControl(html);

        if (appendPaymentVoucher)
        {
            html.Write("<br /><br />");
            table = this.ReturnPaymentVoucherReport(attendee, true);
            table.RenderControl(html);
        }

        SendEmail sendMail = new SendEmail();
        sendMail.AddEmailTo(attendee.User.PrimaryEmail);
        if (!String.IsNullOrEmpty(attendee.User.SecondaryEmail))
            sendMail.AddEmailTo(attendee.User.SecondaryEmail);

        //Get Current Login User
        if ((attendee.Creator != null) && (attendee.Creator.PrimaryEmail != attendee.User.PrimaryEmail))
        {
            sendMail.AddEmailCC(attendee.Creator.PrimaryEmail);
        }

        if (attendee.Session.IsOnline || attendee.Session.IsMultiVenueOnline)
            sendMail.AddEmailCC(attendee.Session.ContactPerson.PrimaryEmail);

        AddAdditionalEmailAddresses(sendMail, attendee);

        sendMail.Subject = EmailConfirmationSubject;// "Registration Confirmation";
        sendMail.IsBodyHtml = true;
        sendMail.From = region4.escWeb.SiteVariables.emailFrom;

        string MailSendescWorksSupport = ConfigurationManager.AppSettings["Mail.Send.escWorksSupport"];
        if ((MailSendescWorksSupport != null) && (MailSendescWorksSupport.ToLower() == "true"))
        {
            if (IsValidEmailAddress(region4.escWeb.SiteVariables.escWorksSupport))
            {
                sendMail.AddEmailBCC(region4.escWeb.SiteVariables.escWorksSupport);
            }
        }

        //Add AlternateViews
        LinkedResource logo = new LinkedResource(System.Web.HttpContext.Current.Server.MapPath("~/lib/img/pwrdby_clear.gif"));
        logo.ContentId = "escWorksLogo";
        AlternateView htmlView = AlternateView.CreateAlternateViewFromString(result.ToString(), null, "text/html");
        htmlView.LinkedResources.Add(logo);
        if (CustomerLogo != null)
            htmlView.LinkedResources.Add(CustomerLogo);
        AlternateView plainView = AlternateView.CreateAlternateViewFromString("This message requires a mail client capable of displaying html messages", null, "text/plain");
        sendMail.AddAlternateView(plainView);
        sendMail.AddAlternateView(htmlView);

        //Add Ics File
        //sendMail.AttachICalendarFilesFromSessionInfo(attendee,false);
        sendMail.AttachICalendarFilesFromSessionInfo(attendee);
        sendMail.Send();

      SendToSessionContacts(attendee);

         

        return false;
    }


    private void SendToSessionContacts(region4.ObjectModel.Attendee attendee)
    {
       
          HtmlTable table = new HtmlTable();
            table.Width = "100%";



            HtmlTableRow row;

            //escWeb.BasePage page = System.Web.HttpContext.Current.CurrentHandler as escWeb.BasePage;
            if (CustomerLogo != null)
            {
                row = new HtmlTableRow();
                row.Cells.Add(new HtmlTableCell());
                row.Cells[0].Align = CustomerLogoAlignment;
             //   row.Cells[0].InnerHtml = String.Format("<img src=\"{0}\" alt=\"customer logo\" />", embedLogo ? "cid:customerLogo" : pathToRoot + CustomerLogoUrl);
                table.Rows.Add(row);
            }



            row = new HtmlTableRow();
            row.Cells.Add(new HtmlTableCell());
            row.Cells.Add(new HtmlTableCell());

            row.Cells[0].InnerHtml = String.Format("<b>Participant Name:</b>");
            row.Cells[1].InnerText = String.Format("{0}", attendee.User.FullName);

            table.Rows.Add(row);




            row = new HtmlTableRow();
            row.Cells.Add(new HtmlTableCell());
            row.Cells.Add(new HtmlTableCell());

            row.Cells[0].InnerHtml = String.Format("<b>Participant Location:</b>");
            row.Cells[1].InnerText = String.Format("{0},{1}", attendee.User.Location.Site.Name, attendee.User.Location.Name);

            table.Rows.Add(row);





            row = new HtmlTableRow();
            row.Cells.Add(new HtmlTableCell());
            row.Cells.Add(new HtmlTableCell());

            row.Cells[0].InnerHtml = String.Format("<b>Participant Email:</b>");
            //row.Cells[1].InnerText = String.Format("<a href=\"mailto:{0}\">{0}</a>", attendee.User.PrimaryEmail);
            row.Cells[1].InnerHtml = String.Format("<a href=\"mailto:{0}\">{0}</a>", attendee.User.PrimaryEmail);

            table.Rows.Add(row);





            row = new HtmlTableRow();
            row.Cells.Add(new HtmlTableCell());
            row.Cells.Add(new HtmlTableCell());

            row.Cells[0].InnerHtml = String.Format("<b>Date & Time of Registration:</b>");
            row.Cells[1].InnerText = String.Format("{0}", attendee.RegistrationTime);

            table.Rows.Add(row);
        
        
        
        

            row = new HtmlTableRow();
            row.Cells.Add(new HtmlTableCell());
            row.Cells.Add(new HtmlTableCell());

            row.Cells[0].InnerHtml = String.Format("<b>Event Title:</b>");
            row.Cells[1].InnerText = String.Format("{0}", attendee.Session.Event.Title);

            table.Rows.Add(row);

            row = new HtmlTableRow();
            row.Cells.Add(new HtmlTableCell());
            row.Cells.Add(new HtmlTableCell());

            row.Cells[0].InnerHtml = String.Format("<b>Session SubTitle:</b>");
            row.Cells[1].InnerText = String.Format("{0}", attendee.Session.Subtitle);

            table.Rows.Add(row);

            row = new HtmlTableRow();
            row.Cells.Add(new HtmlTableCell());
            row.Cells.Add(new HtmlTableCell());

            row.Cells[0].InnerHtml = String.Format("<b>Session ID:</b>");
            row.Cells[1].InnerText = String.Format("{0}", attendee.Session.ID);

            table.Rows.Add(row);


            row = new HtmlTableRow();
            row.Cells.Add(new HtmlTableCell());
            row.Cells.Add(new HtmlTableCell());

            row.Cells[0].InnerHtml = String.Format("<b>Session Date & Time:</b>");
            row.Cells[1].InnerText = String.Format("{0}", attendee.Session.StartDate);

            table.Rows.Add(row);


            //row = new HtmlTableRow();
            //row.Cells.Add(new HtmlTableCell());
            //row.Cells.Add(new HtmlTableCell());

            //row.Cells[0].InnerHtml = String.Format("<b>Participant Name:</b>");
            //row.Cells[1].InnerText = String.Format("{0}", attendee.User.FullName);

            //table.Rows.Add(row);

            //row = new HtmlTableRow();
            //row.Cells.Add(new HtmlTableCell());
            //row.Cells.Add(new HtmlTableCell());

            //row.Cells[0].InnerHtml = String.Format("<b>Participant Email:</b>");
            //row.Cells[1].InnerText = String.Format("{0}", attendee.User.PrimaryEmail);

            //table.Rows.Add(row);


            //row = new HtmlTableRow();
            //row.Cells.Add(new HtmlTableCell());
            //row.Cells.Add(new HtmlTableCell());

            //row.Cells[0].InnerHtml = String.Format("<b>Date & Time of Registration:</b>");
            //row.Cells[1].InnerText = String.Format("{0}", attendee.RegistrationTime);

            //table.Rows.Add(row);



            //row = new HtmlTableRow();
            //row.Cells.Add(new HtmlTableCell());
            //row.Cells.Add(new HtmlTableCell());

            //row.Cells[0].InnerHtml = String.Format("<b>Participant Location:</b>");
            //row.Cells[1].InnerText = String.Format("{0},{1}", attendee.User.Location.Site.Name, attendee.User.Location.Name);

            //table.Rows.Add(row);


            StringBuilder result = new StringBuilder();
            System.IO.StringWriter writer = new System.IO.StringWriter(result);
            System.Web.UI.HtmlTextWriter html = new HtmlTextWriter(writer);
            table.RenderControl(html);


        SendEmail sendMail = new SendEmail();


        sendMail.AddEmailTo(((escWeb.tx_r17.ObjectModel.Session)attendee.Session).SessionContacts);

        sendMail.Subject = EmailConfirmationSubject;// "Registration Confirmation";
        sendMail.IsBodyHtml = true;
        sendMail.From = region4.escWeb.SiteVariables.emailFrom;


        //Add AlternateViews
        LinkedResource logo = new LinkedResource(System.Web.HttpContext.Current.Server.MapPath("~/lib/img/pwrdby_clear.gif"));
        logo.ContentId = "escWorksLogo";
        AlternateView htmlView = AlternateView.CreateAlternateViewFromString(result.ToString(), null, "text/html");
        htmlView.LinkedResources.Add(logo);
        if (CustomerLogo != null)
            htmlView.LinkedResources.Add(CustomerLogo);
        AlternateView plainView = AlternateView.CreateAlternateViewFromString("This message requires a mail client capable of displaying html messages", null, "text/plain");
        sendMail.AddAlternateView(plainView);
        sendMail.AddAlternateView(htmlView);


        sendMail.Send();

    }
    public override HtmlTable ReturnPaymentVoucherReport(List<region4.ObjectModel.Attendee> attendees, bool embedLogo)
    {
        //Render HtmlTable
        HtmlTable table = new HtmlTable();
        table.Width = "100%";
        table.Style.Add("border", "dashed 1px black");

        HtmlTableRow row;

        //escWeb.BasePage page = System.Web.HttpContext.Current.CurrentHandler as escWeb.BasePage;
        if (CustomerLogo != null)
        {
            row = new HtmlTableRow();
            row.Cells.Add(new HtmlTableCell());
            row.Cells[0].Align = CustomerLogoAlignment;
            row.Cells[0].InnerHtml = String.Format("<img src=\"{0}\" alt=\"Region17 Logo\"/>", embedLogo ? "cid:customerLogo" : region4.escWeb.SiteVariables.CustomerHostUrl + CustomerLogoUrl);
            table.Rows.Add(row);
        }

        row = new HtmlTableRow();
        row.Cells.Add(new HtmlTableCell());

        if (NeedVoucher(attendees))
            row.Cells[0].InnerHtml = "<div style=\"font-size: 18pt; color: gray; font-family:Arial; font-style: bold;\">Payment Voucher</div><br /> <br />";
        else
            row.Cells[0].InnerHtml = "<div style=\"font-size: 18pt; color: gray; font-family:Arial; font-style: bold;\">Registration Summary</div><br /> <br />";
        row.Cells[0].Align = "center";
        table.Rows.Add(row);

        if (NeedVoucher(attendees))
        {
            row = new HtmlTableRow();
            row.Cells.Add(new HtmlTableCell());
            row.Cells[0].InnerHtml = String.Format("Please submit this form with your payment to: <br /><div style=\"padding-left: 10px\">{0}<br />Attn: {1}<br />{2}<br />{3}, {4} {5}</div><br />", region4.escWeb.SiteVariables.customer_name,
                PaymentContact(attendees[0]),
                CustomerAddress(attendees[0]),
                CustomerCity(attendees[0]),
                CustomerState(attendees[0]),
                CustomerZip(attendees[0]));
            table.Rows.Add(row);
        }

        row = new HtmlTableRow();
        row.Cells.Add(new HtmlTableCell());

        HtmlTable detailTable = new HtmlTable();
        HtmlTableRow detailRow = new HtmlTableRow();

        detailRow.Cells.Add(new HtmlTableCell());
        detailRow.Cells.Add(new HtmlTableCell());
        detailRow.Cells.Add(new HtmlTableCell());
        detailRow.Cells.Add(new HtmlTableCell());
        detailRow.Cells.Add(new HtmlTableCell());
        detailRow.Cells.Add(new HtmlTableCell());
        detailRow.Cells.Add(new HtmlTableCell());
        detailRow.Cells.Add(new HtmlTableCell());

        detailRow.Cells[0].BgColor = "gray";
        detailRow.Cells[1].BgColor = "gray";
        detailRow.Cells[2].BgColor = "gray";
        detailRow.Cells[3].BgColor = "gray";
        detailRow.Cells[4].BgColor = "gray";
        detailRow.Cells[5].BgColor = "gray";
        detailRow.Cells[6].BgColor = "gray";
        detailRow.Cells[7].BgColor = "gray";

        detailRow.Cells[0].InnerText = "Attendee Name";
        detailRow.Cells[1].InnerText = region4.escWeb.SiteVariables.ObjectProvider.SiteNameCapitalized;
        detailRow.Cells[2].InnerText = region4.escWeb.SiteVariables.ObjectProvider.SchoolNameCapitalized;
        detailRow.Cells[3].InnerText = String.Format("{0} ID", region4.escWeb.SiteVariables.ObjectProvider.SessionNameCapitalized);
        detailRow.Cells[4].InnerText = "Title";
        detailRow.Cells[5].InnerText = "Contact Person";
        detailRow.Cells[6].InnerText = "Amount";
        detailRow.Cells[7].InnerText = "Start Date";

        detailTable.Rows.Add(detailRow);

        decimal totalFee = 0M;
        foreach (region4.ObjectModel.Attendee attendee in attendees)
        {
            detailRow = new HtmlTableRow();

            detailRow.Cells.Add(new HtmlTableCell());
            detailRow.Cells.Add(new HtmlTableCell());
            detailRow.Cells.Add(new HtmlTableCell());
            detailRow.Cells.Add(new HtmlTableCell());
            detailRow.Cells.Add(new HtmlTableCell());
            detailRow.Cells.Add(new HtmlTableCell());
            detailRow.Cells.Add(new HtmlTableCell());
            detailRow.Cells.Add(new HtmlTableCell());

            bool isConference = attendee.Session.Event is region4.ObjectModel.Conference;
            detailRow.Cells[0].InnerText = attendee.User.FirstName + " " + attendee.User.LastName;
            detailRow.Cells[1].InnerText = attendee.User.Location.Site.Name;
            detailRow.Cells[2].InnerText = attendee.User.Location.Name;
            detailRow.Cells[3].InnerText = attendee.Session.ID.ToString();
            detailRow.Cells[4].InnerHtml = isConference ? String.Format("{0}<br /><em>{1}</em>", attendee.Session.Event.Title, attendee.Session.Subtitle) : attendee.Session.Event.Title;
            detailRow.Cells[5].InnerText = String.Format("{0} {1}", attendee.Session.ContactPerson.FirstName, attendee.Session.ContactPerson.LastName);
            detailRow.Cells[6].InnerText = String.Format("{0:c}", attendee.Fee);
            detailRow.Cells[7].InnerText = String.Format("{0:c}", attendee.Session.StartDate.ToShortDateString());

            totalFee += attendee.Fee;

            detailTable.Rows.Add(detailRow);
        }

        detailRow = new HtmlTableRow();

        detailRow.Cells.Add(new HtmlTableCell());
        detailRow.Cells.Add(new HtmlTableCell());
        detailRow.Cells.Add(new HtmlTableCell());
        detailRow.Cells.Add(new HtmlTableCell());
        detailRow.Cells.Add(new HtmlTableCell());
        detailRow.Cells.Add(new HtmlTableCell());
        detailRow.Cells.Add(new HtmlTableCell());
        detailRow.Cells.Add(new HtmlTableCell());

        detailRow.Cells[5].InnerText = "TOTAL  ";
        detailRow.Cells[6].InnerText = String.Format("{0:c}", totalFee);
        detailTable.Rows.Add(detailRow);

        row.Cells[0].Controls.Add(detailTable);
        table.Rows.Add(row);

        return table;
    }
}
