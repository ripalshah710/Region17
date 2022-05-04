using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Security.Cryptography;
using System.Net;
//using System.Net.Http;
//using System.Net.Http.Headers;
using System.Text;
using System.Security.Cryptography.X509Certificates;

using Newtonsoft.Json;
using region4.Utilities.Qlik;
using System.Configuration;

public partial class Dashboard_ExecSSOSignin : region4.escWeb.BasePage
{
    protected override void OnPreInit(EventArgs e)
    {
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        ////TTTTTTTTTTTTTTTT Single Sign On for DashBoard TTTTTTTTTTTTTTTTTTTTTTTTTTTTTT
        //string SSOPortalURL = System.Web.Configuration.WebConfigurationManager.AppSettings["SSOPortal"];
        //if (!string.IsNullOrEmpty(SSOPortalURL) && CurrentUser.IsInRole("executivedashboard"))
        //{
        //    string redirectUrl = EncodeSSOPortalUrl(SSOPortalURL, "texasdashboard17@esc17.net", "ExecDashboard2.aspx");
        //    HttpContext.Current.Response.Redirect(redirectUrl);
        //}
        ////LLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLL
        ///

        TicketRequest ticket_request = null;
        TicketResponse ticket_response = null;
        try
        {
            ticket_request = TicketRequestFromConfiguration(new TicketBody
            {
                UserDirectory = "R04QLIK",
                UserId = "texasdashboard17", //Change USerID to per customer
                Attributes = new KeyValuePair<string, string>[0]
            });

            ticket_response = region4.Utilities.Qlik.Qlik.GetTicket(ticket_request, "hdfgk234n~.s39w"); // change vitual proxy name per customer

            if (String.IsNullOrWhiteSpace(ticket_response.Ticket))
                throw new Exception("Unexpected response from QlikSense");
        }
        catch (Exception EX)
        {
            var response_string = ticket_response != null
                                  && ticket_response.Raw() != null
                                  ? ticket_response.Raw().ToString()
                                  : "";

            region4.ErrorReporter.ReportError(EX, System.Web.HttpContext.Current, region4.ErrorReporter.Severity.severe, region4.ErrorReporter.Occurance.objectModel, "res: " + response_string);
            Response.ClearContent();
            Response.Redirect("/");
        }

        Response.ClearContent();
        // Response.Redirect(Qlik.GenRedirectURL(ticket_request, ticket_response));
        //Response.Redirect(String.Format("https://{0}?qlikTicket={1}", "www.escweb.net/tx_esc_04Mobile/Dashboard/ExecDashboard2.aspx", ticket_response.Ticket));
        Response.Redirect("https://qlik.escworks.app/hdfgk234n~.s39w/sense/app/bc59defc-9e0e-4bc6-9802-76560c98bd42?qlikTicket=" + ticket_response.Ticket); // change vitual proxy name per customer

    }

    private static TicketRequest TicketRequestFromConfiguration(TicketBody body)
    {
        return new TicketRequest
        {
            Host = ConfigurationManager.AppSettings["qliksense.host"],
            Port = Convert.ToInt32(ConfigurationManager.AppSettings["qliksense.port"]),
            VirtualProxy = "hdfgk234n~.s39w",//SettingStr("qliksense.virtualproxy"), // change vitual proxy name per customer
            Body = body
        };
    }

    private string EncodeSSOPortalUrl(string SSOPortalURL, string Email, string returnURL)
    {
        string SHARED_KEY = "70467023fcfcd40fe3b884b2b96f6c48";
        TimeSpan t = (DateTime.UtcNow - new DateTime(1970, 1, 1));
        int timestamp = (int)t.TotalSeconds;

        var payload = new System.Collections.Generic.Dictionary<string, object>() {
              { "userId", "texasdashboard17" },
                 { "userDirectory", "EXCELUSER" },
                // { "url", region4.escWeb.SiteVariables.CustomerHostUrl + returnURL },
            };
        string token = JWT.JsonWebToken.Encode(payload, SHARED_KEY, JWT.JwtHashAlgorithm.HS256);
        //  string redirectUrl = SSOPortalURL + "login.aspx?" + "jwt=" + token;
        string redirectUrl = returnURL;
        return redirectUrl;
    }
}