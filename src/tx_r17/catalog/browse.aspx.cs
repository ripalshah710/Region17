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

public partial class catalog_browse : region4.escWeb.BasePages.Catalog.browse_aspx
{
    
    protected override void LoadControlsToBase()
    {
        base._ddlType = ddlType;
        base._ddlSortOrder = ddlSort;
        base._chkDisplayWithSessions = displayWithSessions;
        base._pFirst = this.pFirst;
        base._pSummary = pSummary;
        base._btnSearch = btnSubmit;
        base._resultsTable = tblResults;
        base._btnNewSearch = btnNewSearch;

    }

    
}
