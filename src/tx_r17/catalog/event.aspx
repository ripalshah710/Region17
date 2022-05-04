<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="event.aspx.cs" Inherits="catalog_event" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mainBody" runat="Server">
    <a name="MainBody"></a>
    <span style="padding-right: 10px;">
        <button type="button" onclick="javascript:history.back()" class="formInput btn btn-R17Blue btn-lg" role="button" style="width: 130px; font-size: small" tooltip="Click here to go to previous page.">Previous</button></span>
    <br />
    <br />
    <asp:Label runat="server" ID="lblErrorMessage" CssClass="error" />
    <br />
    <table style="width: 100%; border-collapse: collapse;" runat="server" id="contentsTable" class="mainBody">
        <tr valign="top">
            <td>

                <asp:ImageButton runat="server" ID="btnNewSearch" AlternateText="New Search" />&nbsp;&nbsp;&nbsp;<%# base.SharePageButton %></td>
            <td rowspan="2">
                <br />
                <br />

            </td>
        </tr>
        <tr>
            <td>
                <asp:Label runat="server" ID="lblTitle" CssClass="mainBodyBold" /><br />
                <asp:Label runat="server" ID="lblDescription" CssClass="mainBodySmall" /><br />
                <br />
                <br />
            </td>
        </tr>
        <tr>
            <td colspan="2" style="border-bottom: solid 1px black"><%# region4.escWeb.SiteVariables.ObjectProvider.SessionPluralNameCapitalized %> scheduled for this <%# region4.escWeb.SiteVariables.ObjectProvider.EventName %>:<br />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Panel runat="server" ID="pSessionControls" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <escWorks:SessionSummary runat="server" ID="s" TypeOfButton="ImageButton" />
            </td>
        </tr>
    </table>

</asp:Content>
