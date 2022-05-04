using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;
using region4.Common;
using escWorks;

public partial class lib_Controls_DisplayFiles : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (escWorks.Tools.Strings.CleanUp("Action", true, false) != null && escWorks.Tools.Strings.CleanUp("Action", true, false) == "DeleteFile" && escWorks.Tools.Strings.CleanUp("ID", true, false) != null)
        {
            int id = Convert.ToInt32(Request.QueryString["ID"].ToString().Trim());
            if (id > 1)
                deleteFile(id);

            Response.Redirect("default.aspx");
        }
        loadFiles();
    }

    private void loadFiles()
    {
        using (SqlCommand cmd = new SqlCommand("Select ID, Filename from FileAttachments", DataConnection.DbConnection))
        {
            FilesList.Rows.Clear();
            try
            {
                cmd.Connection.Open();
                SqlDataReader sqlReader = cmd.ExecuteReader();
                TableRow fileRow = null;
                TableCell fileCell = null;
                TableCell fileDelCell = null;
                while (sqlReader.Read())
                {
                    fileRow = new TableRow();
                    fileCell = new TableCell();

                    if ((Context.User.Identity.IsAuthenticated && Context.User.IsInRole("account")))
                    {
                        fileDelCell = new TableCell();
                        fileDelCell.Text = "<a  class='uploadLink', href='Default.aspx?Action=DeleteFile&ID=" + sqlReader[0].ToString() + "'><img border='0' width='10' height='10' alt=\"delete file\" src='lib/img/delete.bmp'></a>";
                        fileRow.Cells.Add(fileDelCell);
                    }
                    fileCell.HorizontalAlign = HorizontalAlign.Left;
                    fileCell.Text = "<b><a class='uploadLink' href=" + Request.ApplicationPath + "/FileDisplay.aspx?ID=" + sqlReader[0].ToString() + " target='_new'>" + sqlReader[1].ToString() + "</a></b>";
                    fileRow.Cells.Add(fileCell);
                    FilesList.Rows.Add(fileRow);
                }
            }
            catch
            {


            }
            finally
            {
                cmd.Connection.Close();
            }
        }
    }

    private void deleteFile(int idx)
    {
        using (SqlConnection conn = region4.Common.DataConnection.DbConnection)
        {
            SqlCommand cmd = conn.CreateCommand();
            try
            {
                cmd.CommandText = region4.baseStoredProcedures.escWeb.HomePageFileAttachementDelete;
                cmd.Parameters.AddWithValue("@ID", idx);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                if (cmd.Connection.State != System.Data.ConnectionState.Open)
                    cmd.Connection.Open();

                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                region4.ErrorReporter.ReportError(ex, HttpContext.Current, region4.ErrorReporter.Severity.notgiven, region4.ErrorReporter.Occurance.webControls);
            }
            finally
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();
            }
        }
    }
}
