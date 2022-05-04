<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="checkout.aspx.cs" Inherits="catalog_register_checkout" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mainBody" runat="Server">
    <a name="MainBody"></a>
    <span style="padding-right: 10px;">
        <button type="button" onclick="javascript:history.back()" class="formInput btn btn-R17Blue btn-lg" role="button" style="width: 130px; font-size: small" tooltip="Click here to go to previous page.">Previous</button></span>
    <br />
    <br />
    <script type="text/javascript">
        function DisableButton() {
            document.getElementById("<%=btnCheckout.ClientID %>").disabled = true;
        }
        window.onbeforeunload = DisableButton;
    </script>
    <br />
    <br />
    <asp:Label runat="server" ID="lblErrorMessage" CssClass="error" />
    <br />

    <div class="container-fluid">
        <div class="row">
            <div class="col-xs-12 col-sm-12">
                <span style="font-size: 1.25em">Click the 'Complete Checkout' button to register for the <%# region4.escWeb.SiteVariables.ObjectProvider.SessionPluralName %> displayed below.</span>
            </div>
        </div>
        <br />
        <%--<escWorks:MobileCartDisplay runat="server" ID="MobileCartDisplay1" DisplayRemoveButton="false" />--%>
            <escWorks:MobileCartDisplay runat="server" ID="cartDisplay1" DisplayRemoveButton="false" />
        <br />
    </div>
    <div class="row">
        <div class="col-xs-12 col-sm-12">
            <asp:Label runat="server" ID="lblPaymentPrompt" Text="Please select a method of payment to continue" CssClass="smallFont" />
        </div>
    </div>
    <br />
    <table class="mainBody">
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:RadioButtonList runat="server" ID="rblPaymentList" AutoPostBack="true" CssClass="mainBody" Style="margin-left: 13px;">
                            <asp:ListItem Text="Check" Value="CK" />
                            <asp:ListItem Text="Purchase Order" Value="PO" />
                            <asp:ListItem Text="Credit Card" Value="CC" />
                            <asp:ListItem Text="Electronic Check" Value="ACH" />
                        </asp:RadioButtonList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td>&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;
            </td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div style="margin-left: -700px;">
                            <asp:Panel runat="server" ID="pCheck" Visible="false">
                            </asp:Panel>
                            <asp:Panel runat="server" ID="PDCheck" Visible="false">
                            </asp:Panel>
                            <asp:Panel runat="server" ID="pMoneyOrder" Visible="false">
                            </asp:Panel>
                            <asp:Panel runat="server" ID="pPurchaseOrder" Visible="false">
                                <asp:Label ID="PONumberLabel"
                                    Text="PO Number:"
                                    AssociatedControlID="txtPONumber"
                                    runat="server"></asp:Label>

                                <asp:TextBox runat="server" ID="txtPONumber" />
                            </asp:Panel>
                            <asp:Panel runat="server" ID="pCreditCard" Visible="false">
                                <asp:Label ID="CCNumberLabel"
                                    Text="Credit Card Number:"
                                    AssociatedControlID="txtCardNumber"
                                    runat="server"></asp:Label>

                                <asp:TextBox runat="server" ID="txtCardNumber" />
                                <br />
                                <asp:Label ID="CCExpMonthLabel"
                                    Text="Exp&nbsp;"
                                    AssociatedControlID="ddlMonth"
                                    runat="server"></asp:Label>

                                <asp:Label ID="CCExpYearLabel"
                                    Text="Date:"
                                    AssociatedControlID="ddlYear"
                                    runat="server"></asp:Label>

                                <asp:DropDownList runat="server" ID="ddlMonth" />
                                <asp:DropDownList runat="server" ID="ddlYear" />
                                <br />
                                <asp:Label ID="NameOnCardLabel"
                                    Text="Name as it appears on card:"
                                    AssociatedControlID="txtNameOnCard"
                                    runat="server"></asp:Label>

                                <asp:TextBox runat="server" ID="txtNameOnCard" />
                            </asp:Panel>
                            <asp:Panel runat="server" ID="pCash" Visible="false">
                            </asp:Panel>
                            <asp:Panel runat="server" ID="pElectronicCheck" Visible="false">
                                Please provide the following information. All information is required
                    <br />
                                <asp:Label ID="AccountHolderNameLabel"
                                    Text="Account Holder’s Name:"
                                    AssociatedControlID="txtChkAccountHolder"
                                    runat="server"></asp:Label>

                                <br />
                                <asp:TextBox runat="server" ID="txtChkAccountHolder" />
                                <br />
                                <asp:Label ID="RoutingNumberLabel"
                                    Text="ABA Routing Number:"
                                    AssociatedControlID="txtABANumber"
                                    runat="server"></asp:Label>

                                <br />
                                <asp:TextBox runat="server" ID="txtABANumber" />
                                <br />
                                <asp:Label ID="CheckAcctNumberLabel"
                                    Text="Checking Account Number:"
                                    AssociatedControlID="txtCheckAcctNumber"
                                    runat="server"></asp:Label>

                                <br />
                                <asp:TextBox runat="server" ID="txtCheckAcctNumber" />
                            </asp:Panel>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>

    <asp:Button runat="server" ID="btnCheckout" Text="Complete Checkout" CssClass="formInput btn btn-R17Blue btn-lg" Style="width: 170px; font-size: small" />
    <asp:ImageButton runat="server" ID="btnCancelCheckout" Visible="false" alt="Cancel Checkout Button" />
</asp:Content>
