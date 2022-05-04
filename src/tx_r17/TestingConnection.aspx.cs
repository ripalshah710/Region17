using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class TestingConnection : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        using (SqlConnection conn = region4.Common.DataConnection.DbConnection)
        {
            SqlCommand cmd = conn.CreateCommand();
            try
            {
                cmd.CommandText = "SELECT TOP 1 [Id] ,[Name] FROM [SuccessStatus] WITH(NOLOCK) ORDER BY ID DESC";
                cmd.CommandType = System.Data.CommandType.Text;

                if (cmd.Connection.State != System.Data.ConnectionState.Open)
                    cmd.Connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    reader.Read();
                    Response.Clear();
                    Response.Cache.SetCacheability(HttpCacheability.Private);
                    Response.Expires = -1;
                    Response.Buffer = false;
                    //int fSize = Convert.ToInt32(reader["FileSize"]);
                    //byte[] buffer = new byte[fSize];
                    //buffer = (byte[])reader["FileContent"];
                    //Response.BinaryWrite(buffer);
                    Response.Write(reader["Name"]);
                    Response.End();
                }
            }
            catch (Exception ex)
            {
                //region4.ErrorReporter.ReportError(ex, HttpContext.Current, region4.ErrorReporter.Severity.notgiven, region4.ErrorReporter.Occurance.webControls);
            }
            finally
            {
                cmd.Connection.Close();
            }
        }
    }
}