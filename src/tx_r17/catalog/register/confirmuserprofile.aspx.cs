using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using region4;
using System.Text.RegularExpressions;

public partial class catalog_register_confirmuserprofile : region4.escWeb.BasePage
{
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        if (ddlPosition != null) ddlPosition.Load += new EventHandler(_position_Load);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(lblErrorMessage.Text.Trim()))
        {
            if (!Page.IsPostBack)
            {
                CascadingDropDown1.SelectedValue = CurrentUser.Location.Site.Organization.LocationID.ToString();
                CascadingDropDown2.SelectedValue = CurrentUser.Location.Site.LocationID.ToString();
                CascadingDropDown3.SelectedValue = CurrentUser.Location.LocationID.ToString();
                //txtSpecialNeeds.Text = ((escWeb.tx_r17.ObjectModel.User)CurrentUser).SpecialNeeds;
            }

        }
    }

    protected void OnCheckedChanged(Object sender, EventArgs e)
    {
        this.btnSaveUserProfile.Visible = this.checkboxCertify.Checked;
    }

    protected virtual void _position_Load(object sender, EventArgs e)
    {
        DropDownList list = sender as DropDownList;
        if (list == null)
            throw new Exception("Expected a dropdownlist but didn't get one");

        if (list.Items.Count > 0)
            return;
        list.Items.Add(new ListItem("Please select a position...", ""));
        ItemCollection items = ItemCollection.ReturnItemsByGroup(region4.escWeb.SiteVariables.ItemGroupIDs.Position);
        foreach (Item item in items)
            if (item.Enabled)
                list.Items.Add(new ListItem(item.Display, item.ItemId.ToString()));

        ListItem tmp = list.Items.FindByValue(CurrentUser.PrimaryPosition.ItemId.ToString());
        if (tmp != null)
            list.SelectedIndex = list.Items.IndexOf(tmp);
    }

    protected void btnSaveUserProfileClick(object sender, EventArgs e)
    {
        string validationMessage = string.Empty;
        bool err = false;

        int location_id, site_id, organization_id, position_id = 0;

        if (!Int32.TryParse(ddlRegion.SelectedValue, out organization_id))
        {
            validationMessage += "<div class='error'>* Please select a " + region4.escWeb.SiteVariables.ObjectProvider.OrganizationNameCapitalized + "</div>";
            err = true;
        }
        if (!Int32.TryParse(ddlDistrict.SelectedValue, out site_id))
        {
            validationMessage += "<div class='error'>* Please select a "
            + region4.escWeb.SiteVariables.ObjectProvider.SiteNameCapitalized + "</div>";
            err = true;
        }
        if (!Int32.TryParse(ddlSchool.SelectedValue, out location_id))
        {
            validationMessage += "<div class='error'>* Please select a "
                + region4.escWeb.SiteVariables.ObjectProvider.SchoolNameCapitalized + "</div>";
            err = true;
        }
        if (ddlPosition != null && !Int32.TryParse(ddlPosition.SelectedValue, out position_id))
        {
            validationMessage += "<div class='error'>* Please select a position</div>";
            err = true;
        }

        if (err)
        {
            lblErrorMessage.Text = validationMessage;
            return;
        }

        #region Save Profile

        CurrentUser.LocationID = location_id;
        CurrentUser.Locationsite = site_id;
        CurrentUser.PrimaryPosition = region4.Item.ReturnItem(position_id);
        //((escWeb.tx_r17.ObjectModel.User)CurrentUser).SpecialNeeds = txtSpecialNeeds.Text;
        CurrentUser.Save();

        Response.Redirect("~/catalog/register/checkout.aspx");

        #endregion
    }
}