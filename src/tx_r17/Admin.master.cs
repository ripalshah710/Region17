using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin : region4.escWeb.MasterPage
{
    public override void AssignControlsToBase()
    {
        base._pageTitle = this.pageTitle;
        base._stagingIndicator = lblStagingServer;
    }
}
