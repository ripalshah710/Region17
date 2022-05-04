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

public partial class catalog_event : region4.escWeb.BasePages.Catalog.event_aspx
{

   
    protected void Page_Load(object sender, EventArgs e)
    {

    }



    protected override void AssignControlsToBase()
    {
        base._lblTitle = lblTitle;
        base._lblDescription = lblDescription;
        base._sessionControls = s;
        base._btnNewSearch = btnNewSearch;
    }

    
}
