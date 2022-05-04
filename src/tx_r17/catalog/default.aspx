<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="catalog_default" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mainBody" runat="Server">
    <a name="MainBody"></a>
    <span style="padding-right: 10px;">
        <button type="button" onclick="javascript:history.back()" class="formInput btn btn-R17Blue btn-lg" role="button" style="width: 130px; font-size: small" tooltip="Click here to go to previous page.">Previous</button></span>
    <br />
    <br />
    <table border="0" cellpadding="4" cellspacing="0" width="100%">
        <tr>
            <td>
                <table border="0" width="100%">
                    <tr>
                        <td width="40" nowrap>
                            <img src="../../lib/standard/img/search.gif" border="0" alt="Search Offerings" width="32" height="32"></td>
                        <td width="100%">
                            <font size="2">
                                <a href="search.aspx" class="link"><b>Search <%# region4.escWeb.SiteVariables.ObjectProvider.SessionPluralNameCapitalized %></b></a>
                                <br>
                                <i>Use this feature to search through available <%# region4.escWeb.SiteVariables.ObjectProvider.SessionPluralName %> for topics of interest.
                                </i>
                            </font>
                        </td>
                    </tr>
                </table>
                <br>
                <table border="0" width="100%">
                    <tr>
                        <td width="40" nowrap>
                            <img src="../../lib/standard/img/browse.gif" border="0" alt="Browse Offerings" width="32" height="32"></td>
                        <td width="100%">
                            <font size="2">
                                <a href="browse.aspx" class="link"><b>Browse <%# region4.escWeb.SiteVariables.ObjectProvider.SessionPluralNameCapitalized %></b></a>
                                <br>
                                <i>Use this option to browse all catalog offerings based on audience or subject.
                                </i>
                            </font>
                        </td>
                    </tr>
                </table>
                <br>
                <table border="0" width="100%">
                    <tr>
                        <td width="40" nowrap>
                            <img src="../../lib/standard/img/calendar.gif" border="0" alt="Calendar of Events" width="32" height="32"></td>
                        <td width="100%">
                            <font size="2">
                                <a href="calendar.aspx" class="link"><b>Calendar of <%# region4.escWeb.SiteVariables.ObjectProvider.SessionPluralNameCapitalized %></b></a>
                                <br>
                                <i>This calendar shows all of the available <%# region4.escWeb.SiteVariables.ObjectProvider.SessionPluralName %>  in a monthly format.
                                </i>
                            </font>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>

</asp:Content>

