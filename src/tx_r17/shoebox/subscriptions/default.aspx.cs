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

public partial class shoebox_subscriptions_default : region4.escWeb.BasePages.ShoeBox.subscriptions.default_aspx
{


    protected override void AssignControlsToBase()
    {
        //base.lblSubscriptionNumber = lblSubscriptionNumber;
        base.lsbSubscription1 = lsbSubscription1;
        base.lsbSubscription2 = lsbSubscription2;
        base.lsbSubscription3 = lsbSubscription3;
        base.lsbSubscription4 = lsbSubscription4;
        base.ddRecommended = ddRecommended;
        base.ddSubscribed = ddSubscribed;
    }
}
