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

public partial class catalog_register_checkout : region4.escWeb.BasePages.Catalog.Register.checkout_aspx
{

    protected override void AssignControlsToBase()
    {
        base._lblErrorMessage = lblErrorMessage;
        base._btnCheckout = btnCheckout;
        base._rblPaymentMethod = rblPaymentList;
        base._pCashDetails = pCash;
        base._pCCDetails = pCreditCard;
        base._pCheckDetails = pCheck;
        base._pDCheckDetails = PDCheck;
        base._pMODetails = pMoneyOrder;
        base._pPurchaseOrder = pPurchaseOrder;

        base._expMonth = ddlMonth;
        base._expYear = ddlYear;
        base._nameOnCard = txtNameOnCard;
        base._cardNumber = txtCardNumber;
        base._poNumber = txtPONumber;

        base._lblPaymentPrompt = lblPaymentPrompt;

        base._chkABANumber = txtABANumber;
        base._chkAccountHolder = txtChkAccountHolder;
        base._chkAccountNumber = txtCheckAcctNumber;
        base._pElectronicCheck = pElectronicCheck;
    }

    protected override void AssignCustomerPaymentMethod(string paymentMethod)
    {
        
    }

    protected override void HideCustomerPaymentPanels()
    { // As per Bing , this feature is unique to Region4 and doesnt applies to any other customer - Commented by Mohana on 24th April 2014
      

        if (!this.ShoppingCart.AllowPO)
        {
            if (_rblPaymentMethod.Items.Count > 2)
                _rblPaymentMethod.Items.RemoveAt(2); //Remove PO
        }
    }

    protected override region4.ObjectModel.IPaymentMethod SetCustomerPaymentMethodDisplay(string paymentMethod)
    {
        return null;
    }
}
