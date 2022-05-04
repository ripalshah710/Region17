using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class Barcode_QRAttendanceMultidates2 : System.Web.UI.Page
{
    protected string firstname, lastname;
    protected void Page_Load(object sender, EventArgs e)
    {
        string attendee_id = Request.QueryString["user_id"].ToString();

        firstname = Request.QueryString["fname"].ToString();
        lastname = Request.QueryString["lname"].ToString();

        if (!Page.IsPostBack)
        {



            SqlConnection SQLConn = region4.Common.DataConnection.DbConnection;
            using (SqlCommand SQLCommand = new SqlCommand("[/qrcode/multidates/attendance]", SQLConn))
            {
                try
                {
                    SQLCommand.CommandType = CommandType.StoredProcedure;
                    SQLCommand.Parameters.AddWithValue("@attendeeID", Convert.ToInt32(attendee_id));

                    using (SqlDataAdapter SqlDa = new SqlDataAdapter(SQLCommand))
                    {
                        DataSet ResultSet = new DataSet("attendees");


                        SqlDa.Fill(ResultSet, "attendees");
                        if (ResultSet.Tables.Count > 0)
                        {
                            plMultiSessions.Visible = true;

                            DataView dv = ResultSet.Tables[0].DefaultView;

                            int i = 0;

                            for (int ii = 0; ii < ResultSet.Tables[0].Rows.Count; ii++)
                            {
                                DataRowView drv = dv[ii];
                                i++;

                                TableRow RecordRow = new TableRow();

                                for (int j = 0; j <= 6; j++)
                                {
                                    RecordRow.Cells.Add(new TableCell());

                                    RecordRow.Cells[j].CssClass = (i % 2 == 0) ? "evenRow_Bottom" : "oddRow_Bottom";

                                    if (j != 2)
                                        RecordRow.Cells[j].Wrap = false;
                                }

                                RecordRow.Cells[0].Controls.Add(new LiteralControl(string.Format("<input type=\"radio\"  name=\"selSession\" id=\"attendeed:{0}\" value=\"{0}\">", drv[7].ToString())));
                                RecordRow.Cells[1].Text = drv[0].ToString();
                                RecordRow.Cells[2].Text = drv[4].ToString();
                                RecordRow.Cells[3].Text = drv[2].ToString();
                                RecordRow.Cells[4].Text = drv[3].ToString();
                                RecordRow.Cells[5].Controls.Add(new LiteralControl(string.Format("<input type=\"hidden\"   name=\"attendee\" value=\"{0}\">", drv[7].ToString())));

                                tblSessions.Rows.Add(RecordRow);
                            }
                        }


                    }


                }
                catch (Exception ex)
                {

                    //  Response.Write(ex.Message);
                    Response.Write("Please run QRcode for sessions that are taking place today.");

                }

                finally
                {
                    if (SQLCommand.Connection.State != ConnectionState.Closed)
                        SQLCommand.Connection.Close();

                    SQLCommand.Connection.Dispose();
                }
            }

        }


    }

    public void MarkAttenedance(Object sender, System.EventArgs e)
    {
        SqlConnection SQLConn = region4.Common.DataConnection.DbConnection;
        string[] mAttendees = Request.Form["selSession"].Split(',');
        using (SqlCommand SQLCommand = new SqlCommand("[/qrcode/multidates/attendance2]", SQLConn))
        {
            SQLCommand.CommandType = CommandType.StoredProcedure;

            SQLCommand.Parameters.AddWithValue("@attendee_id", -1);
            // SQLCommand.Parameters.AddWithValue("@attended", false);
            SQLCommand.Parameters.AddWithValue("@returnvalue", 0).Direction = ParameterDirection.ReturnValue;
            SQLCommand.Connection.Open();

            SQLCommand.Parameters[0].Value = Convert.ToInt32(mAttendees[0].ToString());


            try
            {

                SQLCommand.ExecuteNonQuery();



                if (Convert.ToBoolean(SQLCommand.Parameters[1].Value))
                {
                    namedisplay.Text = firstname + " " + lastname + ", Successfully marked Attended!";
                    btnSubmit.Visible = false;
                    tblSessions.Visible = false;
                }
                else
                {

                    namedisplay.Text = firstname + " " + lastname + ", Not Successfully marked Attended!";
                    btnSubmit.Visible = false;
                    tblSessions.Visible = false;
                }


            }
            catch (Exception ex)
            {
                Response.Write("Please run QRcode for sessions that are taking place today.");
            }


            finally
            {
                if (SQLCommand.Connection.State != ConnectionState.Closed)
                    SQLCommand.Connection.Close();

                SQLCommand.Connection.Dispose();
            }



        }

    }

}