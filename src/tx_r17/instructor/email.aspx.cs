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

public partial class instructor_email : region4.escWeb.BasePages.Instructor.email_aspx
{

    protected override void AssignControlsToBase()
    {
        base._chkEmailAddresses = chkEmailAddresses;
        base._btnSend = btnSend;
        base._btnCancel = btnCancel;
        base._lnkSelectAll = lnkCheckAll;
        base._lnkSelectNone = lnkCheckNone;
        base._pEmailStuff = panelEmailStuff;
        base._pNoAttendees = panelNoAttendees;
        base._pConfirmation = panelConfirmation;
        base._txtSubject = txtSubject;
        base._txtComments = txtComments;
        base._lblSessionID = lblSessionID;
        base. _lblSessionTitle = lblSessionTitle;
        base._lblTo = lblTo;
        base._btnContinue = btnContinue;
        base._pEmail = panelEmail;
    }
}
