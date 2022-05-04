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

public partial class catalog_register_complete : region4.escWeb.BasePages.Catalog.Register.complete_aspx
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected override void AssignControlsToBase()
    {
        base._registrationTable = registrationTable;
        base._panelPaymentVoucher = panelPaymentVoucher;
        base._lnkPaymentVoucher = aVoucher;
    }
}
