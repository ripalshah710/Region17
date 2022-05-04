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
using Telerik.Web.UI;
using System.Text.RegularExpressions;
using escWorks;
using Microsoft.Security.Application;

public partial class shoebox_account_default : region4.escWeb.BasePages.ShoeBox.Account.default_aspx
{
    private string SplAccomodations = string.Empty;
    protected override void AssignControlsToBase()
    {
        base._primaryEmail = txtPrimaryEmail;
        base._secondaryEmail = txtSecondaryEmail;
        base._saluation = ddlSalutation;
        base._lastName = txtLastName;
        base._firstName = txtFirstName;
        base._middleName = txtMiddleName;
        base._homeAddress = txtHomeAddress;
        base._city = txtCity;
        base._state = ddState;
        base._zip = txtZip;
        base._homePhone = txtHomePhone;
        base._workPhone = txtWorkPhone;
        base._region = ddlRegion;
        base._district = ddlDistrict;
        base._school = ddlSchool;
        base._position = ddlPosition;
        base._errorMessage = lbErrorMessage;
              
        

        base._btnSubmit = btnSubmit;

        base._pSuccess = pSuccess;
        base._pFirst = pFirst;

        //base._gradeLevel = ddlGradeLevel;

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
                
            CascadingDropDown1.SelectedValue = CurrentUser.Location.Site.Organization.LocationID.ToString();
            CascadingDropDown2.SelectedValue = CurrentUser.Location.Site.LocationID.ToString();
            CascadingDropDown3.SelectedValue = CurrentUser.Location.LocationID.ToString();        

        }
    }
  



    protected override void RedirectToCheckOut()
    {
        Response.Redirect("../subscriptions/default.aspx");
    }

    protected void OnChangePassword(object sender, EventArgs e)
    {
        string Email = this.txtPrimaryEmail.Text;

        string url = "password.aspx?mode=change&code=0" + "&email=" + Email;
        Response.Redirect(url);
    }

    protected override void LoadCustomerData()
    {
        #region Load User Info
        _primaryEmail.Enabled = false;
        if (!Page.IsPostBack)
        {
            _primaryEmail.Text = System.Web.HttpUtility.HtmlDecode(System.Web.HttpUtility.HtmlDecode(CurrentUser.PrimaryEmail));
            _secondaryEmail.Text = System.Web.HttpUtility.HtmlDecode(System.Web.HttpUtility.HtmlDecode(CurrentUser.SecondaryEmail));
            _lastName.Text = System.Web.HttpUtility.HtmlDecode(CurrentUser.LastName);
            _firstName.Text = System.Web.HttpUtility.HtmlDecode(CurrentUser.FirstName);
            _middleName.Text = CurrentUser.MiddleName;
            _homeAddress.Text = System.Web.HttpUtility.HtmlDecode(CurrentUser.Address);
            _city.Text = CurrentUser.City;
            UserState = CurrentUser.State;
            _zip.Text = CurrentUser.Zip;
            _homePhone.Text = CurrentUser.HomePhone;
            _workPhone.Text = CurrentUser.WorkPhone;

            this.LoadCustomerParameters(CurrentUser);
        }
        #endregion
    }
}


