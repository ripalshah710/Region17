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

public partial class shoebox_transcripts_personal : region4.escWeb.BasePages.ShoeBox.transcripts.personal_aspx
{

    protected override void AssignControlToBase()
    {
        base._txtTitle = txtTitle;
        base._txtDate = txtDate;
        base._txtCredit = txtCreditEarned;
        base._ddlCreditType = ddlCreditType;
        base._btnAddCredit = btnAddCredit;
        base._txtCreditName = txtCreditName;
        base._lblError = lblError;

        base._txtEndDate = txtEndDate;
        base._txtStartDate = txtStartDate;
        base._btnPrint = btnPrint;
        base._chkIncludeOfficial = chkIncludeOffical;

        base._pResults = pTableResults;

        base._hiddenField = hiddenField;
    }
}
