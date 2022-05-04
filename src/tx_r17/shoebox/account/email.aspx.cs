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

public partial class shoebox_account_email : region4.escWeb.BasePages.ShoeBox.Account.email_aspx
{

    protected override void AssignControlsToBase()
    {
        base._btnSubmit = btnSubmit;
        base._txtNewEmail = txtNewEmail;
        base._txtNewEmail2 = txtNewEmail2;
        base._txtOldEmail = txtEmailAddress;
        base._pStep1 = pStep1;
        base._pStep2 = pStep2;
        base._lblError = lblError;

    }
}
