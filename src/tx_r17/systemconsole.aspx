<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="systemconsole.aspx.cs" Inherits="systemconsole" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mainBody" Runat="Server"><a name="MainBody"></a>
    <div style="float: left; border: solid 1px gray; width: 200px; height: 50px;">
<b>Impersonate User</b>
<br />
Email Address: <asp:TextBox CssClass="formInput" runat="server" ID="txtRunAsEmail" />
<br />
<asp:Label runat="server" ID="lblRunAsError" CssClass="error" />
<br />
<asp:Button runat="server" ID="btnImpersonate" CssClass="formInput" Text="Go" />
</div>
<div style="float: left; border: solid 1px gray; width: 200px; height: 50px; margin-left: 50px;">
<b>escWeb Base Library Version</b>
<br /><em><asp:Label runat="server" ID="lblVersion" /></em>
</div>
<br />
<div style="float:left; width: 200px; border: solid 1px gray; height: 50px;">
Get Database Menu Xml<br /><asp:DropDownList runat="server" ID="ddlMenuId" />
<br /><asp:Button runat="server" ID="btnGetMenu" Text="Get Menu Xml" />
</div>
</asp:Content>

