<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="results.aspx.cs" Inherits="catalog_results" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mainBody" runat="Server">
    <a name="MainBody"></a>
    <span style="padding-right: 10px;">
        <button type="button" onclick="javascript:history.back()" class="formInput btn btn-R17Blue btn-lg" role="button" style="width: 130px; font-size: small" tooltip="Click here to go to previous page.">Previous</button></span>
    <br />
    <br />
    <table runat="server" id="resultsTable" class="mainBody" />
</asp:Content>

