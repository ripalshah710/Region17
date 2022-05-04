<%@ Page Language="C#" AutoEventWireup="true" CodeFile="session.aspx.cs" Inherits="evalmanager_session" MasterPageFile="~/MasterPage.master" %>
<asp:Content runat="server" ContentPlaceHolderID="mainBody"><a name="MainBody"></a>
<h1 class="heading"><asp:Label runat="server" ID="lblTitle" /></h1>
    <br />
    <div style="width:100%; text-align:right"><a href="default.aspx" class="link">Return to Search</a></div>
    <asp:PlaceHolder runat="server" ID="pResultsTable" />
</asp:Content>