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

public partial class shoebox_registration_change : region4.escWeb.BasePages.ShoeBox.registration.change_aspx
{

    protected override void AssignControlsToBase()
    {
        base._pChangeDetail = pChangeDetail;
        base._pError = pError;
        base._pSuccess = pSuccess;
        base._pUnavailable = pUnavailable;
        base._btnSubmit = btnSubmit;
        base._rblSessions = rblSessions;
        base._lblNoSessions = lblNoSessions;

    }

    protected override void RenderPage(region4.ObjectModel.Attendee attendee)
    {
        lblDescription.Text = attendee.Session.Event.Description;
        lblFee.Text = String.Format("{0:c}", attendee.Fee);
        lblSessionID.Text = attendee.Session.ID.ToString();
        lblStartDate.Text = String.Format("{0:d}", attendee.Session.StartDate);
        lblTitle.Text = attendee.Session.Title;
        lblLocation.Text = String.Format("{0}, {1}", attendee.Session.Schedule[0].Location.Site.Name, attendee.Session.Schedule[0].Location.Name);
    }
}
