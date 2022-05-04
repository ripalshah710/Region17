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

public partial class about_contact : region4.escWeb.BasePages.About.contact_aspx
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
            //Create a random code and store it in the Session object.
            this.Session["CaptchaImageText"] = GenerateRandomCode(5);
    }

    public override bool ValidateSecurityImage()
    {
        string EncodedResponse = Request.Form["g-recaptcha-response"];
        bool IsCaptchaValid = (region4.Utilities.ReCaptchaClass.Validate(EncodedResponse) == "True" ? true : false);
        return IsCaptchaValid;
    }


    protected override void AssignControlsToBase()
    {
        base._ddlCategory = ddlCategory;
        base._txtComments = txtComments;
        base._txtName = txtName;
        base._txtEmail = txtEmail;
        base._txtPhone = txtPhone;
        base._chkASAP = chkASAP;

        base._btnSubmit = btnSubmit;
        base._btnCancel = btnCancel;

        base._lblError = lblError;

        base._pStep1 = pStep1;
        base._pStep2 = pStep2;

    }
}
