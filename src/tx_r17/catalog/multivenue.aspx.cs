using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class catalog_multivenue : region4.escWeb.BasePages.Catalog.multivenue_aspx
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected override void AssignControlsToBase()
    {
        base._txtDescription = txtDescription;
        base._txtTitle = txtTitle;
        base._sessionSelector = sessionDisplay;
    }
}