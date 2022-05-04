using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class browsercheck : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        string UserAgent = System.Web.HttpContext.Current.Request.UserAgent;
        string Browser = System.Web.HttpContext.Current.Request.Browser.Browser;
        string Browserversion = System.Web.HttpContext.Current.Request.Browser.Version;
        int majorversion = System.Web.HttpContext.Current.Request.Browser.MajorVersion;

        //System.Web.HttpContext.Current.Response.Write("<br>");
        //System.Web.HttpContext.Current.Response.Write("browser check...");
        //System.Web.HttpContext.Current.Response.Write("<br>");
        //System.Web.HttpContext.Current.Response.Write("Browser:" + Browser);
        //System.Web.HttpContext.Current.Response.Write("<br>");

        //System.Web.HttpContext.Current.Response.Write("majorversion:" + majorversion.ToString());

        //System.Web.HttpContext.Current.Response.Write("<br>");

        //System.Web.HttpContext.Current.Response.Write("Browserversion:" + Browserversion.ToString());
        //System.Web.HttpContext.Current.Response.Write("<br>");
        //System.Web.HttpContext.Current.Response.Write("Platform:" + System.Web.HttpContext.Current.Request.Browser.Platform.ToString());

        //System.Web.HttpContext.Current.Response.Write("<br>");
        //System.Web.HttpContext.Current.Response.Write("agent:" + UserAgent);
        //System.Web.HttpContext.Current.Response.Write("<br>");

        //System.Web.HttpContext.Current.Response.Write("httpagent:" + Request.ServerVariables["HTTP_USER_AGENT"]);
        //System.Web.HttpContext.Current.Response.Write("<br>");

    }

    
protected void btnContinue_Click(object sender, EventArgs e)
    {
    
        //Session.Remove("BS");
        string clscript="window.close()";
        ClientScript.RegisterStartupScript(this.GetType(),"script",clscript,true);


    }
}