<%@ WebHandler Language="C#" Class="session" %>

using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Collections.Generic;
using System.Data.SqlClient;

public class session : IHttpHandler {

    protected string cacheKey
    {
        get { return "SessionRss_cachekey"; }
    }    
    
    public void ProcessRequest (HttpContext context) {
        string rssStream;
        if (context.Cache[cacheKey] == null)
        {
            rssStream = ReturnResponse();
            context.Cache.Add(cacheKey, rssStream, null, DateTime.Now.AddMinutes(5),
                System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.Normal, null);
        }
        else
            rssStream = context.Cache[cacheKey].ToString();

        rssStream = rssStream.Replace("encoding=\"utf-16\"", "");

        context.Response.ContentType = "text/xml";
        context.Response.ContentEncoding = System.Text.Encoding.UTF8;
        context.Response.Write(rssStream);
        context.Response.Flush();
        context.Response.End();
    }

    protected string ReturnResponse()
    {
        using (SqlConnection conn = region4.Common.DataConnection.DbConnection)
        {
            SqlDataAdapter adapter = new SqlDataAdapter("[p.catalog.screendisplay.weekly]", conn);
            DataSet ds = new DataSet("list");
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.Fill(ds, "event");
            string strSessions = ds.GetXml();
            return strSessions;
        }
    }    
 
    public bool IsReusable {
        get {
            return true;
        }
    }

}