<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="browse.aspx.cs" Inherits="catalog_browse" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mainBody" runat="Server">
    <a name="MainBody"></a>
    <span style="padding-right: 10px;">
        <button type="button" onclick="javascript:history.back()" class="formInput btn btn-R17Blue btn-lg" role="button" style="width: 130px; font-size: small" tooltip="Click here to go to previous page.">Previous</button></span>
    <br />
    <br />
    <asp:Panel runat="server" ID="pFirst">
        <em>Please choose the method you would like to use to browse our offerings.</em>
        <br />
        <br />
        I would like to browse by: 
        <br />
        <asp:DropDownList ID="ddlType" runat="server" />
        <asp:DropDownList ID="ddlSort" runat="server" /><br />
        <asp:CheckBox runat="server" ID="displayWithSessions" />
        <br />
        <br />
        <asp:ImageButton ID="btnSubmit" runat="server" AlternateText="Browse" />
    </asp:Panel>

    <asp:Panel runat="server" ID="pSummary" Visible="false">
        <asp:ImageButton runat="server" ID="btnNewSearch" AlternateText="Previous" />
        <table runat="server" id="tblResults" class="mainBody" />
    </asp:Panel>
</asp:Content>

