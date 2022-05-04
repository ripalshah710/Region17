using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace tx_r17.Shoebox.Evaluation.Pages
{
    /// <summary>
    /// Summary description for eval.
    /// </summary>
    public partial class Eval : System.Web.UI.Page
    {

        private Evaluation eForm = new Evaluation();

        private int mSessionId;
        private int mRegistrationId;
        private string mLastName;

        protected void Page_Load(object sender, System.EventArgs e)
        {
            try
            {
                if (!Request.IsSecureConnection && !Request.IsLocal)
                {
                    string mAddress = Request.FilePath.ToLower() + ((Request.QueryString.ToString().Length > 0) ? "?" + Request.QueryString.ToString() : "").ToLower();
                    Response.Redirect(string.Format("https://{0}{1}", Request.Url.Host, mAddress), true);
                }

                //
                // Set QueryStrings
                mSessionId = Convert.ToInt32(LegacyCode.Strings.GetSafeString("sessNum", LegacyCode.Strings.StringType.QueryString, -1));
                mRegistrationId = Convert.ToInt32(LegacyCode.Strings.GetSafeString("id", LegacyCode.Strings.StringType.QueryString, -1));
                mLastName = LegacyCode.Strings.GetSafeString("lastName", LegacyCode.Strings.StringType.QueryString, string.Empty);

                if (!Page.IsPostBack)
                {
                    //
                    // Load textbox from querystring if value exist
                    if (mSessionId > 0)
                        tbSessionId.Text = mSessionId.ToString();
                    if (mRegistrationId > 0)
                        tbUserId.Text = mRegistrationId.ToString();
                    if (mLastName != string.Empty)
                        tbLastName.Text = mLastName;
                }

                //
                // Instantiate and load Form object
                eForm.LoadForm(mRegistrationId);

                this.lblErrorMessage.Text = null;

                this.btnEvaluation.Controls.Add(new LiteralControl("<INPUT TYPE=button ID=btnEvaluation VALUE='Evaluation' onClick='JavaScript: LoadEvaluation(" + mRegistrationId + ");'>"));

                LinkButton link = new LinkButton();
                link.CssClass = "PageLink";
                link.Text = "Certificate";
                link.CommandArgument = mRegistrationId.ToString();
                link.Click += new EventHandler(link_Click);


                //this.btnCertificate.Controls.Add(new LiteralControl( "<INPUT TYPE=button ID=btnCertificate VALUE='Certificate' onClick='JavaScript: LoadCertificate("+mRegistrationId+");'>"));
                this.btnCertificate.Controls.Add(link);

                if ((mSessionId < 0) || (mRegistrationId < 0) || (mLastName == string.Empty))
                {
                    pSignIn.Visible = true;
                    this.Load += new System.EventHandler(this.Page_Load);
                    base.OnInit(e);
                }
                else
                {
                    if (Validate(mSessionId, mRegistrationId, mLastName))
                    {
                        pSignIn.Visible = false;

                        if (eForm.Title == null)
                            pNoEvaluation.Visible = true;
                        else if (eForm.Completed)
                            pCertificate.Visible = true;
                        else
                            pEvaluation.Visible = true;
                    }
                }


            }
            catch (Exception ex)
            {
                this.lblErrorMessage.Text = "Invalid Parameter Value: " + ex.Message;
            }


        }

        void link_Click(object sender, EventArgs e)
        {
            LinkButton link = sender as LinkButton;
            int attendee_id;
            if (!Int32.TryParse(link.CommandArgument, out attendee_id))
                throw new Exception("couldn't parse attendee_id");

            region4.escWeb.SiteVariables.ReportsProvider.SendCertificate(attendee_id);
        }

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {

        }
        #endregion

        private bool Validate(int session_id, int user_id, string last_name)
        {
            switch (eForm.ValidateAccount(session_id, user_id, last_name))
            {
                case 0:
                    {
                        return true;
                    }
                case 1:
                    {
                        this.lblErrorMessage.Text += "Session ID not found!<br>";
                        this.lblErrorMessage.Visible = true;
                        return false;
                    }
                case 2:
                    {
                        this.lblErrorMessage.Text += "Registration ID and Last Name does not match!";
                        this.lblErrorMessage.Visible = true;
                        return false;
                    }
                default:
                    return false;
            }
        }

        public void Click_btnValidate(object sender, System.EventArgs e)
        {
            //
            // Set QueryStrings
            int mSessionId = Convert.ToInt32(LegacyCode.Strings.GetSafeString("tbSessionId", LegacyCode.Strings.StringType.Form, -1));
            int mRegistrationId = Convert.ToInt32(LegacyCode.Strings.GetSafeString("tbUserId", LegacyCode.Strings.StringType.Form, -1));
            string mLastName = LegacyCode.Strings.GetSafeString("tbLastName", LegacyCode.Strings.StringType.Form, string.Empty);

            this.lblErrorMessage.Text = string.Empty;

            Response.Redirect("eval.aspx?sessNum=" + this.tbSessionId.Text + "&id=" + this.tbUserId.Text + "&lastName=" + this.tbLastName.Text);
        }


    }
}
