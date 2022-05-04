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
using System.IO;
using System.Data.SqlClient;

public partial class MasterPage : region4.escWeb.MasterPage
{



    public override void AssignControlsToBase()
    {
      base._pageTitle = this.pageTitle;
        base._stagingIndicator = lblStagingServer;
    }
    protected override void OnLoad(EventArgs e)
    {

        base.OnLoad(e);
    }
    protected override void OnInit(EventArgs e)
    {
        //loadFiles();
        base.OnInit(e);
    }
    /*private void loadFiles()
    {
        using (SqlConnection conn = region4.Common.DataConnection.DbConnection)
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "Select ID, Filename from FileAttachments";
            Table tbl = (Table)this.FindControl("lftlinks");

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
                    fileDelCell = new TableCell();
                    fileDelCell.Text = "<a href='Default.aspx?ID=" + sqlReader[0].ToString() + "'><img border='0' width='10' height='10' src='http://localhost:2317/tx_gccisd/lib/img/delete.bmp'></a>";
                    fileRow.Cells.Add(fileDelCell);
                    fileCell.HorizontalAlign = HorizontalAlign.Left;
                    fileCell.Text = "<b><a href='FileDisplay.aspx?ID=" + sqlReader[0].ToString() + "' target='_new'>" + sqlReader[1].ToString() + "</a></b>";
                    fileRow.Cells.Add(fileCell);
                    tbl.Rows.Add(fileRow);
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
    }*/
}
