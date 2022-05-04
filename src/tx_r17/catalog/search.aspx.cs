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

public partial class catalog_search : region4.escWeb.BasePages.Catalog.search_aspx
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    

    protected override void AssignControls()
    {
        base._ddlAudience = ddlAudience;
        base._ddlSubject = ddlSubject;
        base._txtStartdate = txtStartDate;
        base._txtEndDate = txtEndDate;
        base._txtSessionID = txtSessionID;
        base._txtKeywords = txtKeywords;
        base._lblErrorMessage = lblError;
        base._btnFindSession = btnSearchSession;
        base._btnSearch = btnSearch;

        base._pResults = pResults;

        //base._tableQuery = tableQuery;
        base._pQuery = pQuery;
        base._tableResults = tableResults;

        base._lblResultsDescription = lblResultsDescription;
        base._btnNewSearch = btnNewSearch;

    }

}
