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

public partial class evalmanager_default : region4.escWeb.BasePages.evalManager.default_aspx
{

    protected override void AssignControlsToBase()
    {
        base._btnFind = btnFind;
        base._pResults = pResults;
        base._txtSessionID = txtSessionID;

    }
}
