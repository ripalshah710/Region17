using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Text;


public partial class Barcode_PaymentID : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        string s = txtpaymentid.Text;
        string[] split = s.Split(new char[] { '\n' });

        string payment_id = string.Empty;
        for (int lcv = 0; lcv < split.Length; lcv++)
        {
            if (split[lcv].ToString().Trim() != "")
            {
                try
                {
                    SqlConnection conn = region4.Common.DataConnection.DbConnection;
                    payment_id = split[lcv].ToString().Replace("\r", "");
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "update [event.session.attendee.payment] set status = 'Invoiced' where [attendee.payment_pk]=" + payment_id;
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

        }

        Psignin.Visible = false;
        pSuccess.Visible = true;


    }
}
