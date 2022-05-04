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

public partial class lib_collaboration_default : region4.escWeb.BasePages.Lib.collaboration.default_aspx
{

    protected override void AssignControlsToBase()
    {
        base._txtMessage = ResourceMessage;
        base._lblSource = ResourceSource;
        base._txtRptEmail = ResourceRecipient;
        base._txtSenderEmail = ResourceSenderEmail;
        base._txtSenderName = ResourceSender;
        base._btnSend = btnSend;
        base._lblErrorMessage = lblError;
    }
}
