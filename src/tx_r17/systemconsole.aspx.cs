using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

public partial class systemconsole : region4.escWeb.BasePages.systemconsole_aspx
{
    protected override void AssignControlsToBase()
    {
        base._lblescWebBaseVersion = lblVersion;
        base._lblSudoError = lblRunAsError;
        base._btnImpersonate = btnImpersonate;
        base._txtSudoUsrEmail = txtRunAsEmail;

        base._ddlMenuID = ddlMenuId;
        base._btnGetMenu_Click = btnGetMenu;
    }
}
