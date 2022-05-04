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

public partial class shoebox_registration_default : region4.escWeb.BasePages.ShoeBox.registration.default_aspx
{

    protected override void AssignControlsToBase()
    {
        _regHistory = regHistory;
        _radTabStrip = radTabStrip;
        _hiddenFieldTabValue = hiddenFieldTabValue;
    }

}
