<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="cart.aspx.cs" Inherits="catalog_register_cart" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mainBody" runat="Server">
    <a name="MainBody"></a>
    <span style="padding-right: 10px;">
        <button type="button" onclick="javascript:history.back()" class="formInput btn btn-R17Blue btn-lg" role="button" style="width: 130px; font-size: small" tooltip="Click here to go to previous page.">Previous</button></span>
    <br />
    <br />
    <escWorks:MobileCartDisplay runat="server" ID="cartDisplay1" DisplayRemoveButton="true" />
    <br />
    <br />
    <label for="ctl00_mainBody_txtPromoCode" style="display: none;">PromonCode</label>

    <asp:Panel runat="server" ID="pPromoCode">
        Promotional Code:
    <asp:TextBox runat="server" ID="txtPromoCode" />

        <asp:Button runat="server" ID="btnApplyCode" Text="Apply Code" CssClass="formInput btn btn-R17Blue btn-lg" Style="width: 170px; font-size: small"
            ToolTip="Click here to enter promotion code." />
    </asp:Panel>
    <asp:Button runat="server" ID="btnCheckOut" Text="Checkout" CssClass="formInput btn btn-R17Blue btn-lg" Style="width: 170px; font-size: small"
        ToolTip="Click here to checkout." />
</asp:Content>
