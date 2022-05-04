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

public partial class shoebox_transcripts_edit : region4.escWeb.BasePages.ShoeBox.transcripts.edit_aspx
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
    }
}
