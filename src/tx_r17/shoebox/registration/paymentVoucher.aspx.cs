using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using escWorks;

public partial class shoebox_registration_paymentVoucher : Page
{
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
    }
    protected override void OnLoad(EventArgs e)
    {
        Guid grouping;
        try
        {
            grouping = new Guid(Tools.Strings.CleanUp("id", true, false));
        }
        catch
        {
            pError.Visible = true;
            //writer.Write(Microsoft.Security.Application.Encoder.HtmlEncode("Unable to locate payment voucher"));
            return;
        }
        List<region4.ObjectModel.Attendee> attendees = region4.ObjectModel.Attendee.ReturnAttendeesByRegistrationID(grouping);
        HtmlTable result = region4.escWeb.SiteVariables.ObjectProvider.EmailProvider.ReturnPaymentVoucherReport(attendees, false);
        printvoucher.Controls.Add(result);
        //result.RenderControl(writer);
    }
}