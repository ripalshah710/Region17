<%@ Page Language="C#" AutoEventWireup="true" EnableViewState="false" MasterPageFile="MasterPage.master" CodeFile="Search.aspx.cs" Inherits="tx_esc_r4.Catalog.Search" %>

<%@ Register src="lib/Controls/Search.ascx" tagname="Search" tagprefix="uc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="ContentContainer" ContentPlaceHolderID="mainBody" Runat="Server"><a name="MainBody"></a>
    <table border="0" cellpadding="4" cellspacing="0" width="100%"> 
        <tr>
            <td>
                        <uc1:Search ID="Search1" runat="server"/>   
            </td>
        </tr>
    </table>
</asp:Content>
