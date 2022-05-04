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

public partial class catalog_waitinglistremove : region4.escWeb.BasePages.Catalog.waitinglistremove_aspx
{

    protected override void AssignControlsToBase()
    {
        base._lbMessage = lbMessage;
    }
}
