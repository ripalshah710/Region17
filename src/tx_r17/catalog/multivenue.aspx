<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="multivenue.aspx.cs" Inherits="catalog_multivenue"  Title="Multi-Venue Events"%>

<asp:Content ID="Content1" ContentPlaceHolderID="mainBody" runat="Server"><a name="MainBody"></a>
    <asp:Label runat="server" ID="txtTitle" CssClass="heading" />
    <br />
    <asp:Label runat="server" ID="txtDescription" Font-Italic="true" />
    <br />
    <escWorks:MultiVenueSessions runat="server" ID="sessionDisplay" />
</asp:Content>
