using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using region4.escWeb;

public partial class Accountability_Default : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string script = "<script language='javascript' ID='Message'>window.open('default2.aspx','_blank','top=50, left=140, width=1000, height=800, resizable=yes, scrollbars=1');</script>";
        ScriptManager.RegisterStartupScript((Page)HttpContext.Current.Handler, typeof(Page), "Message", script, false);
    }
}
