<%@ Page Language="C#" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="evalmanager_default"  MasterPageFile="~/MasterPage.master"%>
<asp:Content runat="server"  ContentPlaceHolderID="mainBody"><a name="MainBody"></a>

    


    <asp:Label ID="SessionIDBoxLabel"
        text=<%# region4.escWeb.SiteVariables.ObjectProvider.SessionNameCapitalized %>
        AssociatedControlID="txtSessionID"
runat="server"></asp:Label>
         ID: <asp:TextBox runat="server" ID="txtSessionID" />&nbsp;&nbsp;<asp:Button runat="server" ID="btnFind" Text="Search" />
<br /><br />
<asp:PlaceHolder runat="server" ID="pResults" />

</asp:Content>

