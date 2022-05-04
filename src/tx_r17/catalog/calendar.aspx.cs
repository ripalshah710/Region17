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

public partial class catalog_calendar : region4.escWeb.BasePages.Catalog.calendar_aspx
{
    
    protected override void AssignControls()
    {
        //Calendar Control
        base._calendar = cal1;
    }

}
