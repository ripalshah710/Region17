<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="complete.aspx.cs" Inherits="catalog_register_complete" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mainBody" runat="Server">
    <a name="MainBody"></a>
    <span style="padding-right: 10px;">
        <button type="button" onclick="javascript:history.back()" class="formInput btn btn-R17Blue btn-lg" role="button" style="width: 130px; font-size: small" tooltip="Click here to go to previous page.">Previous</button></span>
    <br />
    <br />
    <div class="container-fluid">
        <div class="row">
            <div class="col-12 col-sm-12">
                <span style="font-size: 16px;"><b>Thank you for your registration</b></span>
            </div>
        </div>

        <br />

        <div class="row">
            <div class="col-12 col-sm-12">
                <span style="font-size: 14px;">You have been registered for:</span>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-xs-12 col-sm-12">
                <table runat="server" id="registrationTable" />
            </div>
        </div>
        <br />

        <div style="padding-left: 12px;">
            <div class="row">
                <div class="col-12 col-sm-12" style="padding-left: 0">
                    <span style="font-size: 14px;">You may visit your </span><a href="~/shoebox/registration/default.aspx" runat="server" class="link smallerFont">Registration History</a><span style="font-size: 14px;"> to print a confirmation page for each <%# Resources.Names.Session %> for which you are registered.</span>
                </div>
            </div>
        </div>
        <br />
        <asp:Panel runat="server" ID="panelPaymentVoucher">
            If you are paying by check or purchase order please include the payment voucher with your payment.  You can download the payment voucher by clicking <a runat="server" id="aVoucher">here</a>
        </asp:Panel>
    </div>
</asp:Content>

