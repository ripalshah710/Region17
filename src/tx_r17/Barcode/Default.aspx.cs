using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Text;

public partial class Barcode_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        string s = txtbarcode.Text;
        string[] split = s.Split(new char[] { '\n' });

        string attendee_id_dot = string.Empty;
        string attendee_id = string.Empty;
        string attendee = string.Empty;
        for (int lcv = 0; lcv < split.Length; lcv++)
        {

            try
            {
                SqlConnection conn = region4.Common.DataConnection.DbConnection;
                attendee_id_dot = split[lcv].ToString().Replace("\r", "");
                attendee = attendee_id_dot.Replace(",", "");
                attendee_id = attendee.Substring(0, attendee.IndexOf("."));
                int attendeeID = Convert.ToInt32(attendee_id);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "update [event.session.attendee] set attended = 1 where attendee_Pk = @ID";
                cmd.Parameters.AddWithValue("@ID", attendeeID);
                cmd.Connection = conn;
                conn.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }

            catch (Exception Ex)
            {
                System.Web.HttpContext.Current.Response.Write(Ex.Message);

            }

        }

        Psignin.Visible = false;
        pSuccess.Visible = true;


    }


}
