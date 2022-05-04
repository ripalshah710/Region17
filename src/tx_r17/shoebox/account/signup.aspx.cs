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
using region4;
using region4.escWeb;

public partial class shoebox_account_signup : region4.escWeb.BasePages.ShoeBox.Account.signup_aspx
{

    public event UserCreatedHandler UserCreated;

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
        base._password = txtPassword;
        base._passwordConfirmation = txtConfirmPassword;
        base._errorMessage = lbErrorMessage;
        
        base._btnSubmit = btnSubmit;

        //base._gradeLevel = ddlGradeLevel;

        base._pSuccess = pSuccess;
        base._pFirst = pFirst;
    }

    protected override void SetCustomerParameters(region4.ObjectModel.User user)
    {
       
    }


    protected override void _btnSubmit_Click(object sender, EventArgs e)
    {
        #region Validation
        string validationMessage;
        if (!ValidateData(out validationMessage))
        {
            _errorMessage.Text = validationMessage;
            return;
        }

        if (!ValidateCustomerData(out validationMessage))
        {
            _errorMessage.Text = validationMessage;
            return;
        }

        bool err = false;

        int location_id, site_id, organization_id, position_id = 0, salutation_id = 0, gradelevel_id = 0;

        if (!Int32.TryParse(_school.SelectedValue, out location_id))
        {
            validationMessage += String.Format("<div class='error'>* Please select a {0}</div>", region4.escWeb.SiteVariables.ObjectProvider.SchoolName);
            err = true;
        }

        if (!Int32.TryParse(_district.SelectedValue, out site_id))
        {
            //TODO: parameritze this
            validationMessage += String.Format("<div class='error'>* Please select a {0}</div>", region4.escWeb.SiteVariables.ObjectProvider.SiteName);
            err = true;
        }

        if (!Int32.TryParse(_region.SelectedValue, out organization_id))
        {
            validationMessage += String.Format("<div class='error'>* Please select a {0}</div>", region4.escWeb.SiteVariables.ObjectProvider.OrganizationName);
            err = true;
        }

        if (_position != null && !Int32.TryParse(_position.SelectedValue, out position_id))
        {
            validationMessage += "<div class='error'>* Please select a position</div>";
            err = true;
        }

        if (_saluation != null && !Int32.TryParse(_saluation.SelectedValue, out salutation_id))
        {
            validationMessage += "<div class='error'>* Please select a salutation</div>";
            err = true;
        }

        if (_gradeLevel != null && !Int32.TryParse(_gradeLevel.SelectedValue, out gradelevel_id))
        {
            gradelevel_id = 0;
        }

        if (err)
        {
            _errorMessage.Text = validationMessage;
            return;
        }

        #endregion

        string password = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(_password.Text.ToLower().Trim(), "sha1");

        region4.ObjectModel.User user = region4.ObjectModel.User.CreateUser(_lastName.Text, _firstName.Text, _middleName.Text, _homeAddress.Text, _city.Text, UserState,
            _zip.Text, _homePhone.Text, _workPhone.Text, location_id, site_id, organization_id, _primaryEmail.Text, _secondaryEmail.Text, position_id, salutation_id, gradelevel_id, password);

        if (!user.ExceptionOccurred)
        {
            SetCustomerParameters(user);
            user.Save();

            //If the a user created event has been registered then  fire the event
            if (UserCreated != null)
            {
                UserCreated(user);
                //Response.Write("section 0");
            }


            if (this.ShoppingCart != null)
            {
                //Response.Write("section 01");
                this.ShoppingCart.ChangeUser(user);//In order to display the fee discount for the Conferences/Sessions, update user information when cart is not null Added by VM 4-2-2012
                //Response.Write("section 02");
            }
            if (!String.IsNullOrEmpty(Tools.Strings.CleanUp("conf_ref", true, false)))
            {
                string url = String.Format("../../catalog/conference.aspx?conf_ref={0}&email={1}&conference_id={2}", Tools.Strings.CleanUp("conf_ref", true, false), user.PrimaryEmail, Tools.Strings.CleanUp("conference_id", true, false));
                //Response.Write("section 1");
                Response.Redirect(Microsoft.Security.Application.Encoder.UrlEncode(url));
            }
            if (!String.IsNullOrEmpty(Tools.Strings.CleanUp("session_id", true, false)))
            {
                string url = String.Format("../../walkin/default.aspx?session_id={0}&emailID={1}", Tools.Strings.CleanUp("session_id", true, false).ToString(), Tools.Strings.CleanUp("emailID", true, false).ToString());
                //Response.Write("section 2");
                Response.Redirect(Microsoft.Security.Application.Encoder.UrlEncode(url));
            }
            else if (Tools.Strings.CleanUp("ReturnUrl", true, false) != "")
            {
                string url = System.Web.HttpUtility.UrlDecode(Tools.Strings.CleanUp("ReturnUrl", true, false));

                this.CurrentUser = user;
                Session.Remove("profile");
                Session["profile"] = user;
                if (url.Contains("/tx_r17/default.aspx"))
                {
                    System.Web.Security.FormsAuthentication.SetAuthCookie(user.Sid.ToString(), false);
                    //Response.Write("section 3");
                    Response.Redirect("../subscriptions/default.aspx");               
                }
                else
                    //Response.Write("section 4");
                    System.Web.Security.FormsAuthentication.RedirectFromLoginPage(user.Sid.ToString(), false);
            }
            else
            {
                //Response.Write("section 5");
                this.CurrentUser = user;
                Session.Remove("profile");
                Session["profile"] = user;
                System.Web.Security.FormsAuthentication.SetAuthCookie(user.Sid.ToString(), false);
                _pFirst.Visible = false;
                _pSuccess.Visible = true;
            }

        }
    }

    protected override bool ValidateData(out string validationMessage)
    {
        validationMessage = "";

        if (_primaryEmail.Text == "")
        {
            validationMessage = "Please enter your email address";
            return false;
        }
        if (region4.ObjectModel.User.EmailInUse(_primaryEmail.Text))
        {
            validationMessage = "The email address you have entered is already in use";
            return false;
        }


        string patternStrict = @"^(([^<>()[\]\\.,;:\s@\""]+"

                     + @"(\.[^<>()[\]\\.,;:\s@\""]+)*)|(\"".+\""))@"

                     + @"((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}"

                     + @"\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+"

                     + @"[a-zA-Z]{2,}))$";

        Regex reStrict = new Regex(patternStrict);


        bool isStrictMatch = reStrict.IsMatch(_primaryEmail.Text);

        if (!isStrictMatch)
        {
            validationMessage = "The primary email address you have entered does not appear to be a valid email address";
            return false;
        }

        isStrictMatch = reStrict.IsMatch(_secondaryEmail.Text);

        if (!String.IsNullOrEmpty(_secondaryEmail.Text) && !isStrictMatch)
        {
            validationMessage = "The secondary email address you have entered does not appear to be a valid email address";
            return false;
        }


        if (_lastName.Text == "")
        {
            validationMessage = "Please enter your last name";
            return false;
        }
        if (_firstName.Text == "")
        {
            validationMessage = "Please enter your first name";
            return false;
        }
        if (_homeAddress.Text == "")
        {
            validationMessage = "Please enter your home address";
            return false;
        }
        if (_city.Text == "")
        {
            validationMessage = "Please enter your city";
            return false;
        }
        if (UserState.Length != 2)
        {
            validationMessage = "Please enter a valid state";
            return false;
        }
        if (_zip.Text == "")
        {
            validationMessage = "Please enter your zip code";
            return false;
        }

        return true;
    }

    protected override void _state_Load(object sender, EventArgs e)
    {
        DropDownList list = sender as DropDownList;
        if (list == null)
            throw new Exception("Expected a dropdownlist but didn't get one");

        if (list.Items.Count > 0)
            return;
        list.Items.Add(new ListItem(""));
        ItemCollection states = ItemCollection.ReturnItemsByGroup(SiteVariables.ItemGroupIDs.State);
        foreach (Item item in states)
            list.Items.Add(new ListItem(item.Display, item.Display));

        list.SelectedValue = "TX";
    }
}
