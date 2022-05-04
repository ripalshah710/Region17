using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web;
using System.Web.UI.HtmlControls;
using escWorks;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

public partial class shoebox_registration_confirmation : Page
{
    // protected override  
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
    }
    protected override void OnLoad(EventArgs e)
    {
        int attendee_id;
        Guid _registrationGrouping;
        Guid _sid;
        if (!Int32.TryParse(Tools.Strings.CleanUp("attendee_id", true, false), out attendee_id))
        {
            if (!Guid.TryParse(Tools.Strings.CleanUp("attendee_id", true, false), out _registrationGrouping))
            {
                //writer.Write(Microsoft.Security.Application.Encoder.HtmlEncode("Unable to locate attendee record"));
                return;
            }
            else
            {
                if (!Guid.TryParse(Tools.Strings.CleanUp("sid", true, false), out _sid))
                {
                    // writer.Write(Microsoft.Security.Application.Encoder.HtmlEncode("Unable to locate attendee record"));
                    return;
                }

                List<region4.ObjectModel.Attendee> attendees = region4.ObjectModel.Attendee.ReturnAttendeesByRegistrationID(_registrationGrouping);
                SortedList<region4.Item, region4.ObjectModel.Attendee> lists = new SortedList<region4.Item, region4.ObjectModel.Attendee>();
                foreach (region4.ObjectModel.Attendee a in attendees)
                {
                    if (a.User.Sid == _sid) //Group Registration might register people 
                    {
                        lists.Add(a.Session.BreakoutSession, a);
                    }
                }
                //   writer.AddAttribute(HtmlTextWriterAttribute.Title, "Print confirmation");
                // writer.AddAttribute(HtmlTextWriterAttribute.Headers)
                HtmlTable table = region4.escWeb.SiteVariables.ObjectProvider.EmailProvider.ReturnConfirmationReport(lists, false);
                printReceipt.Controls.Add(table);

                //table.RenderControl(writer);
            }
        }
        else //Single Attendee
        {
            region4.ObjectModel.Attendee attendee = region4.escWeb.SiteVariables.ObjectProvider.ReturnAttendee(attendee_id);
            if (attendee.ErrorOccurred)
            {
                // writer.Write(Microsoft.Security.Application.Encoder.HtmlEncode("Unable to locate attendee record"));
                return;
            }

            HtmlTable table = region4.escWeb.SiteVariables.ObjectProvider.EmailProvider.ReturnConfirmationReport(attendee, false);
            printReceipt.Controls.Add(table);

        }

    }
}
