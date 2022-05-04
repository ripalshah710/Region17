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

public partial class instructor_default : region4.escWeb.BasePages.Instructor.default_aspx
{

    protected override void AssignControlsToBase()
    {
        this._radTabStrip = radTabStrip;
        this._pPlaceHolder = pPlaceHolder;
        this._hiddenFieldTabValue = hiddenFieldTabValue;
    }
   
}
