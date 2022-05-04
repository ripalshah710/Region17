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
using System.Data.SqlClient;
using region4;

public partial class security_signin : region4.escWeb.BasePages.Security.signin_aspx
{

    protected override void AssignControlsToBase()
    {
        base._errorMessage = this.lblMessage;
        base._txtPassword = this.txtPassword;
        base._txtUserName = this.txtUserName;
    }

    protected override string NoEmailGiven { get { return Resources.security.noUserName; } }

    protected override string NoPasswordGiven { get { return Resources.security.noPassword; } }

    protected override string InvalidCredentials { get { return Resources.security.invalidCredentials; } }

    protected override string AccountDisabled { get { return Resources.security.accountDisabled; } }

    protected override string DatabaseError { get { return Resources.security.databaseError; } }

    protected override AuthenticationResult AuthenticateUser(string userName, string password, out Guid sid)
    {
        sid = Guid.Empty;
        password = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(password.ToLower().Trim(), "sha1");

        //var ue = new UnicodeEncoding();
        //var byteSourceText = ue.GetBytes(password.ToLower().Trim());
        //var byteHash = new System.Security.Cryptography.SHA256Managed().ComputeHash(byteSourceText);
        // password= Convert.ToBase64String(byteHash);

        using (SqlConnection conn = region4.Common.DataConnection.DbConnection)
        {
            string s = System.Web.HttpUtility.HtmlEncode(userName);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = baseStoredProcedures.escWeb.AuthenticateUser;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@uid", System.Web.HttpUtility.HtmlEncode(userName));
            cmd.Parameters.AddWithValue("@pwd", password);
            cmd.Parameters.Add("@guid", System.Data.SqlDbType.UniqueIdentifier);
            cmd.Parameters.Add("@result", System.Data.SqlDbType.Int);

            cmd.Parameters["@result"].Direction = System.Data.ParameterDirection.ReturnValue;
            cmd.Parameters["@guid"].Direction = System.Data.ParameterDirection.Output;

            try
            {
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                try
                {
                    sid = (Guid)cmd.Parameters["@guid"].Value;
                }
                catch (Exception ex)
                {
                    sid = Guid.Empty;
                }

                return (AuthenticationResult)Enum.Parse(typeof(AuthenticationResult), cmd.Parameters["@result"].Value.ToString());
            }
            catch (Exception e)
            {
                ErrorReporter.ReportError(e, Context, ErrorReporter.Severity.notgiven, ErrorReporter.Occurance.basePages);
                return AuthenticationResult.DbError;
            }
            finally
            {
                cmd.Connection.Close();
            }

        }
    }
}
