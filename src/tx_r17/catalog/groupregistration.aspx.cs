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

public partial class catalog_groupregistration : region4.escWeb.BasePages.catalog.groupregistration_aspx
{
    protected override void AssignControlsToBase()
    {
        this._listBoxAvailableUsers = listBoxAvailableUsers;
        this._hiddenFieldSelectedUsers = hiddenFieldSelectedUsers;
        this._hiddenFieldSessionID = hiddenFieldSessionID;
        this._btnCheckout = btnCheckout;
    }

    protected override void RenderSession(region4.ObjectModel.Session session)
    {
        this.lblTitle.Text = session.Title;
        this.lblSessionID.Text = session.ID.ToString();
        this.lblDescription.Text = session.Description;
        this.hiddenFieldAvailableSeats.Value = this.ShoppingCart.NumberOfSeatsAvailable(session.ID).ToString();
        this.hiddenFieldSessionID.Value = session.ID.ToString();
    }
}