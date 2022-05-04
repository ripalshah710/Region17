using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public partial class _Default : region4.escWeb.BasePage//region4.escWeb.BasePages.default_aspx
{

    public struct FileNameURL
    {
        public string FileName;
        public string URL;
    }

    static Regex MobileCheck = new Regex(@"android|(android|bb\d+|meego).+mobile|avantgo|bada\/|blackberry|blazer|compal|elaine|fennec|hiptop|iemobile|ip(hone|od)|iris|kindle|lge |maemo|midp|mmp|mobile.+firefox|netfront|opera m(ob|in)i|palm( os)?|phone|p(ixi|re)\/|plucker|pocket|psp|series(4|6)0|symbian|treo|up\.(browser|link)|vodafone|wap|windows (ce|phone)|xda|xiino", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled);
    static Regex MobileVersionCheck = new Regex(@"1207|6310|6590|3gso|4thp|50[1-6]i|770s|802s|a wa|abac|ac(er|oo|s\-)|ai(ko|rn)|al(av|ca|co)|amoi|an(ex|ny|yw)|aptu|ar(ch|go)|as(te|us)|attw|au(di|\-m|r |s )|avan|be(ck|ll|nq)|bi(lb|rd)|bl(ac|az)|br(e|v)w|bumb|bw\-(n|u)|c55\/|capi|ccwa|cdm\-|cell|chtm|cldc|cmd\-|co(mp|nd)|craw|da(it|ll|ng)|dbte|dc\-s|devi|dica|dmob|do(c|p)o|ds(12|\-d)|el(49|ai)|em(l2|ul)|er(ic|k0)|esl8|ez([4-7]0|os|wa|ze)|fetc|fly(\-|_)|g1 u|g560|gene|gf\-5|g\-mo|go(\.w|od)|gr(ad|un)|haie|hcit|hd\-(m|p|t)|hei\-|hi(pt|ta)|hp( i|ip)|hs\-c|ht(c(\-| |_|a|g|p|s|t)|tp)|hu(aw|tc)|i\-(20|go|ma)|i230|iac( |\-|\/)|ibro|idea|ig01|ikom|im1k|inno|ipaq|iris|ja(t|v)a|jbro|jemu|jigs|kddi|keji|kgt( |\/)|klon|kpt |kwc\-|kyo(c|k)|le(no|xi)|lg( g|\/(k|l|u)|50|54|\-[a-w])|libw|lynx|m1\-w|m3ga|m50\/|ma(te|ui|xo)|mc(01|21|ca)|m\-cr|me(rc|ri)|mi(o8|oa|ts)|mmef|mo(01|02|bi|de|do|t(\-| |o|v)|zz)|mt(50|p1|v )|mwbp|mywa|n10[0-2]|n20[2-3]|n30(0|2)|n50(0|2|5)|n7(0(0|1)|10)|ne((c|m)\-|on|tf|wf|wg|wt)|nok(6|i)|nzph|o2im|op(ti|wv)|oran|owg1|p800|pan(a|d|t)|pdxg|pg(13|\-([1-8]|c))|phil|pire|pl(ay|uc)|pn\-2|po(ck|rt|se)|prox|psio|pt\-g|qa\-a|qc(07|12|21|32|60|\-[2-7]|i\-)|qtek|r380|r600|raks|rim9|ro(ve|zo)|s55\/|sa(ge|ma|mm|ms|ny|va)|sc(01|h\-|oo|p\-)|sdk\/|se(c(\-|0|1)|47|mc|nd|ri)|sgh\-|shar|sie(\-|m)|sk\-0|sl(45|id)|sm(al|ar|b3|it|t5)|so(ft|ny)|sp(01|h\-|v\-|v )|sy(01|mb)|t2(18|50)|t6(00|10|18)|ta(gt|lk)|tcl\-|tdg\-|tel(i|m)|tim\-|t\-mo|to(pl|sh)|ts(70|m\-|m3|m5)|tx\-9|up(\.b|g1|si)|utst|v400|v750|veri|vi(rg|te)|vk(40|5[0-3]|\-v)|vm40|voda|vulc|vx(52|53|60|61|70|80|81|83|85|98)|w3c(\-| )|webc|whit|wi(g |nc|nw)|wmlb|wonu|x700|yas\-|your|zeto|zte\-", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled);

    public static bool IsMobileBrowser()
    {
        if (HttpContext.Current.Request != null && HttpContext.Current.Request.ServerVariables["HTTP_USER_AGENT"] != null)
        {
            var u = HttpContext.Current.Request.ServerVariables["HTTP_USER_AGENT"].ToString();

            if (u.Length < 4)
                return false;

            if (MobileCheck.IsMatch(u) || MobileVersionCheck.IsMatch(u.Substring(0, 4)))
                return true;
        }

        return false;
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);




        // System.Web.HttpBrowserCapabilities bc = System.Web.HttpContext.Current.Request.Browser;
        string UserAgent = System.Web.HttpContext.Current.Request.UserAgent;
        string Browser = System.Web.HttpContext.Current.Request.Browser.Browser;
        string Browserversion = System.Web.HttpContext.Current.Request.Browser.Version;
        int majorversion = System.Web.HttpContext.Current.Request.Browser.MajorVersion;

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

        // _browserID.Text = "Agent:" + UserAgent + "<br>" + "browser:" + Browser + "<br>" + "browser version:" + Browserversion + "<br>" + "Major version:" + majorversion.ToString();

        //System.Web.HttpContext.Current.Response.Write("httpagent:"+Request.ServerVariables["HTTP_USER_AGENT"]);
        //System.Web.HttpContext.Current.Response.Write("<br>");

        if (Session["BS"] != "yes" &&
            ((Browser == "InternetExplorer" && System.Web.HttpContext.Current.Request.Browser.MajorVersion < Convert.ToInt32(ConfigurationManager.AppSettings["ieversion"].ToString()))
            || (Browser == "IE" && System.Web.HttpContext.Current.Request.Browser.MajorVersion < 7 && UserAgent.IndexOf(".NET4.0E") < 0)  // On few machine browser as IE
            || (Browser == "Chrome" && System.Web.HttpContext.Current.Request.Browser.MajorVersion < Convert.ToInt32(ConfigurationManager.AppSettings["chromeversion"].ToString()))
            || (UserAgent.IndexOf("Windows NT 5.1") > 0 && Browser == "IE")  //windows xp
            || (UserAgent.IndexOf("Windows NT 5.1") > 0 && Browser == "Chrome" && System.Web.HttpContext.Current.Request.Browser.MajorVersion < Convert.ToInt32(ConfigurationManager.AppSettings["chromeversion"].ToString())) //windows xp
            || (UserAgent.IndexOf("Windows NT 5.1") > 0 && Browser == "InternetExplorer") //windows xp
            || Browser == "Safari"
            || Browser == "Firefox"
            || (Browser == "IE" && System.Web.HttpContext.Current.Request.Browser.MajorVersion < Convert.ToInt32(ConfigurationManager.AppSettings["ieversion"].ToString()) && UserAgent.IndexOf(".NET4.0E") < 0)
            )
           )
        {
            // Response.Redirect("browsercheck.aspx");
            string url = "browsercheck.aspx";
            string ws = "window.open('" + url + "', 'popup_window', 'width=700,height=800,left=100,top=100,resizable=yes,scrollbars=yes');";
            ClientScript.RegisterStartupScript(this.GetType(), "script", ws, true);
            Session.Add("BS", "yes");

        }



        List<FileNameURL> listFiles = new List<FileNameURL>();

        using (SqlConnection conn = region4.Common.DataConnection.DbConnection)
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = region4.baseStoredProcedures.escWeb.HomePageLoadAdvertisementFile;
            cmd.Parameters.AddWithValue("@CustomerCode", region4.escWeb.SiteVariables.customer_id);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Connection.Open();

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    int fileSize = Convert.ToInt32(reader["fileSize"].ToString());
                    string fileName = reader["filename"].ToString();
                    string URL = reader["URL"].ToString();
                    byte[] byteArray = new byte[fileSize];
                    long DataToRead = reader.GetBytes(reader.GetOrdinal("binary"), 0, byteArray, 0, fileSize);
                    DateTime timestampDB = reader.GetDateTime(reader.GetOrdinal("timestamp"));

                    FileNameURL fileNameURL = new FileNameURL();
                    fileNameURL.FileName = fileName;
                    fileNameURL.URL = URL;
                    listFiles.Add(fileNameURL);

                    try
                    {
                        string completeFilePath = HttpContext.Current.Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath)
                                + "/swf_files/" + fileName;

                        bool bWriteFile = true;

                        if (File.Exists(completeFilePath))
                        {
                            DateTime timestampFile = File.GetLastWriteTime(completeFilePath);
                            //If timestamp from Database is newer than file timestamp, then overwrite file from database
                            if (timestampDB <= timestampFile)
                            {
                                bWriteFile = false;
                            }
                        }

                        if (bWriteFile)
                        {
                            // Open file for reading  
                            System.IO.FileStream fileStream = new System.IO.FileStream(completeFilePath, System.IO.FileMode.Create, System.IO.FileAccess.Write);
                            // Writes a block of bytes to this stream using data from a byte array.
                            fileStream.Write(byteArray, 0, byteArray.Length);
                            // close file stream
                            fileStream.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
        }

        //Loopthrough files
        foreach (FileNameURL fileNameURL in listFiles)
        {
            HyperLink hyperLink = new HyperLink();
            hyperLink.NavigateUrl = fileNameURL.URL;
            hyperLink.ImageUrl = "swf_files/" + fileNameURL.FileName;
            //hyperLink.ImageWidth = new Unit(550);
            //hyperLink.ImageHeight = new Unit(362);
            hyperLink.Text = "Advertisement File";

            if (IsMobileBrowser())
            {
                hyperLink.ImageWidth = new Unit(345);
                hyperLink.ImageHeight = new Unit(217);
            }
            else
            {
                hyperLink.ImageWidth = new Unit(450);
                hyperLink.ImageHeight = new Unit(280);
            }

            if (fileNameURL.URL.Contains(region4.escWeb.SiteVariables.CustomerHostUrl))
            {
                hyperLink.Target = "_self";
            }
            else
            {
                hyperLink.Target = "_blank";
            }
            divAdItems.Controls.Add(hyperLink);


            //string completeFilePath = HttpContext.Current.Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath) + "/swf_files/advertisement.html";
            //DateTime timestamp = region4.ObjectModel.modelConstants.DefaultDateTime;

            //if (File.Exists(completeFilePath))
            //{
            //    timestamp = File.GetLastWriteTime(completeFilePath);
            //}

            //using (SqlConnection conn = region4.Common.DataConnection.DbConnection)
            //{
            //    SqlCommand cmd = conn.CreateCommand();
            //    cmd.CommandText = region4.baseStoredProcedures.escWeb.HomePageLoadAdvertisementFile;
            //    cmd.Parameters.AddWithValue("@timestamp", timestamp);
            //    cmd.Parameters.AddWithValue("@CustomerCode", region4.escWeb.SiteVariables.customer_id);
            //    cmd.CommandType = System.Data.CommandType.StoredProcedure;
            //    cmd.Connection.Open();

            //    using (SqlDataReader reader = cmd.ExecuteReader())
            //    {
            //        while (reader.Read())
            //        {
            //            int fileSize = Convert.ToInt32(reader["fileSize"].ToString());
            //            byte[] byteArray = new byte[fileSize];
            //            long DataToRead = reader.GetBytes(reader.GetOrdinal("binary"), 0, byteArray, 0, fileSize);
            //            try
            //            {
            //                // Open file for reading  
            //                System.IO.FileStream fileStream = new System.IO.FileStream(completeFilePath, System.IO.FileMode.Create, System.IO.FileAccess.Write);
            //                // Writes a block of bytes to this stream using data from a byte array.
            //                fileStream.Write(byteArray, 0, byteArray.Length);
            //                // close file stream
            //                fileStream.Close();
            //            }
            //            catch (Exception ex)
            //            {
            //            }
            //        }
            //    }
            //}

        }
        escWeb.tx_r17.WebControls.ClassUpcomingEvents upcoming = new escWeb.tx_r17.WebControls.ClassUpcomingEvents();
        upcomingevents.Controls.Add(upcoming);

    }


}