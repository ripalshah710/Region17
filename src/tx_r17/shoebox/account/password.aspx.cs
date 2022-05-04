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
using System.Net.Mail;
using region4.ObjectModel;

public partial class shoebox_account_password : region4.escWeb.BasePage
{
    Exception _exception;
    #region Legacy Code
    private bool EmailPasswordChangeCode(string Email)
    {
        string mChangeCode = new Random().Next(0, 9999).ToString().PadLeft(4, '0');

        int result = DatabaseHelper.SetEmailChangeCode(Email, mChangeCode);

        try
        {
            switch (result)
            {
                case 0:
                    {
                        MailMessage mailMessage = new MailMessage();
                        mailMessage.Subject = region4.escWeb.SiteVariables.customer_name + " Password Reset";
                        string url = "https://" + System.Web.HttpContext.Current.Request.ServerVariables["SERVER_NAME"] + System.Web.HttpContext.Current.Request.ApplicationPath + "/shoebox/account/password.aspx?mode=change&code=" + mChangeCode + "&email=" + Email;
                        mailMessage.Body = string.Format("Someone has requested your password from " + region4.escWeb.SiteVariables.customer_name + ".  If you did not request your password, do not be alarmed, you are the only person receiving this email and nothing has been changed with your account.\n\nFollow this link to change your password now:\n" + url);

                        mailMessage.From = new MailAddress(region4.escWeb.SiteVariables.emailFrom);
                        mailMessage.To.Add(Email);

                        SmtpClient smtpClient = new SmtpClient(region4.escWeb.SiteVariables.emailServer);
                        smtpClient.Send(mailMessage);
                        mailMessage.Dispose();

                        return true;
                    }
                case 1:
                    {
                        // not found
                        throw new ApplicationException(string.Format("The email address <i>{0}</i> is not in our system.", Email));
                    }
                case 2:
                    {
                        // Disabled
                        throw new ApplicationException(string.Format("The email address <i>{0}</i> has been disabled.", Email));
                    }
                default:
                    {
                        // unspecify Error
                        throw new ApplicationException("An unspecified error has occurred.");
                    }
            }
        }
        catch (Exception ex)
        {
            region4.ErrorReporter.ReportError(ex, Context, region4.ErrorReporter.Severity.notgiven, region4.ErrorReporter.Occurance.customerWeb);
            _exception = ex;
            return false;
        }
    }

    private bool ChangePassword(string Email, string Password, string ChangeCode, string NewPassword)
    {
        int result = DatabaseHelper.ChangePassword(Email, Password, ChangeCode, NewPassword);

        try
        {
            switch (result)
            {
                case 0:
                    {
                        // Success
                        return true;
                    }
                case 1:
                case 2:
                    {
                        // Incorrect Code/Password
                        throw new ApplicationException("The code or password provided does not match one that we have on record.<br><br>");
                    }
                case 3:
                    {
                        // Not Found
                        throw new ApplicationException(string.Format("The email address <i>{0}</i> is not in our system.", Email));
                    }
                default:
                    {
                        // unspecify Error
                        throw new ApplicationException("An unspecified error has occurred.");
                    }
            }
        }
        catch (Exception ex)
        {
            region4.ErrorReporter.ReportError(ex, Context, region4.ErrorReporter.Severity.notgiven, region4.ErrorReporter.Occurance.customerWeb);
            _exception = ex;
            return false;
        }
    }
    #endregion

    public void OnChangePassword(object sender, EventArgs e)
    {
        string code = LegacyCode.Strings.GetSafeString("code", LegacyCode.Strings.StringType.QueryString);

        string mEmail = ChangeEmailUsingCode.Text;
        string mPassword = this.OldPasswordTextBox.Text; 
        string mChangeCode = LegacyCode.Strings.GetSafeString("code", LegacyCode.Strings.StringType.QueryString);
        string mNewPassword = this.TNewPassUsingCode.Text;

        bool succeeded = ChangePassword(mEmail, mPassword, mChangeCode, mNewPassword);

        pMessage.Visible = true;

        if (succeeded)
        {
            this.labelMessageHeader.Font.Bold = true;
            this.labelMessageHeader.Text = "Your password is changed";
            this.labelMessageDetail.Text = "";
        }
        else
        {
            this.labelMessageHeader.Font.Bold = true;
            this.labelMessageHeader.Text = "An error was reported...See details below:";
            this.labelMessageDetail.Text = _exception.Message;
        }
    }

    public void OnSendEmail(object sender, EventArgs e)
    {
        bool succeeded = EmailPasswordChangeCode(this.ForgotEmail.Text);

        pMessage.Visible = true;

        if (succeeded)
        {
            this.labelMessageHeader.Font.Bold = true;
            this.labelMessageHeader.Text = "An email has been sent to " + this.ForgotEmail.Text;
            this.labelMessageDetail.Text = "Please check your email, click the link to reset password";
        }
        else
        {
            this.labelMessageHeader.Font.Bold = true;
            this.labelMessageHeader.Text = "An error was reported...See details below:";
            this.labelMessageDetail.Text = _exception.Message;
        }
    }

    override protected void OnInit(EventArgs e)
    {
        string mode = LegacyCode.Strings.GetSafeString("mode", LegacyCode.Strings.StringType.QueryString);
        string code = LegacyCode.Strings.GetSafeString("code", LegacyCode.Strings.StringType.QueryString);
        this.ChangeEmailUsingCode.Text = LegacyCode.Strings.GetSafeString("email", LegacyCode.Strings.StringType.QueryString);

        this.pForgot.Visible = false;
        this.pChangeCode.Visible = false;
        this.pMessage.Visible = false;

        this.pChangeCode.Visible = (!string.IsNullOrEmpty(mode) && string.Compare(mode, "change", true) == 0);
        this.pForgot.Visible = !this.pChangeCode.Visible;

        if (!string.IsNullOrEmpty(code) && string.Compare(code, "0") != 0)
        {
            PasswordPlaceHolder.Visible = false;
        }

        base.OnInit(e);
    }

    private void InitializeComponent()
    {

    }

    protected void Page_Load(object sender, System.EventArgs e)
    {
        // Put user code to initialize the page here
    }
}


