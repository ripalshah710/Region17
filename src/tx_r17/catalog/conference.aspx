<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="conference.aspx.cs" Inherits="catalog_conference" Title="Untitled Page" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mainBody" runat="Server">
    <a name="MainBody"></a>
    <span style="padding-right: 10px;">
        <button type="button" onclick="javascript:history.back()" class="formInput btn btn-R17Blue btn-lg" role="button" style="width: 130px; font-size: small" tooltip="Click here to go to previous page.">Previous</button></span>
    <br />
    <br />
    <div class="container-fluid">
        <div class="row">
            <div class="col-xs-12 col-sm-12">
                <div>
                    <asp:Label runat="server" ID="txtTitle" CssClass="heading" />
                    <br />
                </div>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-xs-12 col-sm-12">
                <div>
                    <asp:Label runat="server" ID="txtDescription" Font-Italic="true" />
                    <br />
                    <br />
                </div>
            </div>
        </div>
    </div>
    <br />
    <br />
    <div>
        <asp:PlaceHolder ID="plttess" runat="server" Visible="false">
            <b>T-TESS/T-PESS:</b><br />
            <asp:Label runat="server" ID="libT_PRESS" Font-Italic="true" /><br />
            <br />
        </asp:PlaceHolder>
    </div>
    <escWorks:ConferenceSelection runat="server" ID="sessionDisplay" CollapseBreakoutByDefault="false" />
</asp:Content>

