using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

public partial class catalog_conference : region4.escWeb.BasePages.Catalog.conference_aspx
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected override void AssignControlsToBase()
    {
        base._txtDescription = txtDescription;
        base._txtTitle = txtTitle;
        base._conferenceSelector = sessionDisplay;
    }

    protected override void RenderDetails(region4.ObjectModel.Conference _conference)
    {
        _txtTitle.Text = _conference.Title;
        _txtDescription.Text = _conference.Description;

        if (_conference.Dimensions != string.Empty && _conference.Standards != string.Empty)
        {
            plttess.Visible = true;
            libT_PRESS.Text = _conference.Dimensions;
            if (_conference.Standards != string.Empty)
                libT_PRESS.Text += "; " + _conference.Standards;
        }

    }
}
