<%@ Page Language="C#" AutoEventWireup="true" CodeFile="personal.aspx.cs" Inherits="shoebox_transcripts_personal"
    MasterPageFile="~/MasterPage.master" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content runat="server" ContentPlaceHolderID="mainBody">
    <a name="MainBody"></a>
    <span style="padding-right: 10px;">
        <button type="button" onclick="javascript:history.back()" class="formInput btn btn-R17Blue btn-lg" role="button" style="width: 130px; font-size: small" tooltip="Click here to go to previous page.">Previous</button></span>
    <br />
    <br />
    <script type="text/javascript">
        function CheckForOther(ddlControl) {
            if (ddlControl[ddlControl.selectedIndex].value == "0") {
                //
                // They need the option to enter the credit
                // name for the report.
                document.aspnetForm['<%# txtCreditName.UniqueID %>'].disabled = false;
        }
        else {
            //
            // A valid credit name already exists
            document.aspnetForm['<%# txtCreditName.UniqueID %>'].disabled = true;
            document.aspnetForm['<%# txtCreditName.UniqueID %>'].value = "";
            }
        }

        function RemoveCredit(creditID) {
            if (confirm("Are you sure you want to remove this credit?")) {
                document.aspnetForm['<%# hiddenField.UniqueID %>'].value = creditID;
                document.forms[0].submit();
            }
        }
    </script>
    <div class="row">
        <div class="col-xs-12 col-sm-1">
            <asp:Label ID="LabelTitle"
                Text="Title:<br/> "
                AssociatedControlID="txtTitle"
                runat="server"></asp:Label>
        </div>
        <div class="col-xs-12 col-sm-5">
            <asp:TextBox ID="txtTitle" Width="260px" CssClass="forminput" runat="server" />
        </div>
        <div class="col-xs-12 col-sm-1">
            <asp:Label ID="DateLabel"
                Text="Date:<br/> "
                AssociatedControlID="txtDate"
                runat="server"></asp:Label>

        </div>
        <div class="col-xs-12 col-sm-5" style="z-index: 1;">
            <asp:TextBox runat="server" ID="txtDate" Width="188px" CssClass="forminput" />&nbsp;<asp:ImageButton
                runat="server" ID="btnCalendar1" alt="Credit Date Calendar Button" ImageUrl="~/lib/img/buttons/calendar.png" />
            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDate"
                PopupButtonID="btnCalendar1" />
        </div>
    </div>
    <div class="row">
        <div class="col-xs-12 col-sm-3">
            <asp:Label ID="CreditTypeLabel"
                Text="Credit Type: <br/>"
                AssociatedControlID="ddlCreditType"
                runat="server"></asp:Label>
        </div>
        <div class="col-xs-12 col-sm-3">
            <asp:DropDownList
                ID="ddlCreditType" runat="server" CssClass="form-control fullWidth mediumFont" Style="height: 32px" OnChange="CheckForOther(this);" />
        </div>
    </div>

    <div class="row">
        <div class="col-xs-12 col-sm-3">
            <asp:Label ID="CreditNameLabel"
                Text="***Credit Name: <br/> "
                AssociatedControlID="txtCreditName"
                runat="server"></asp:Label>
        </div>
        <div class="col-xs-12 col-sm-3">
            <asp:TextBox
                ID="txtCreditName" runat="server" CssClass="form-control fullWidth mediumFont" />
        </div>
    </div>

    <div class="row">
        <div class="col-xs-12 col-sm-3">
            <asp:Label ID="CreditEarnedLabel"
                Text="Credit Earned: <br/> "
                AssociatedControlID="txtCreditEarned"
                runat="server"></asp:Label>
        </div>
        <div class="col-xs-12 col-sm-3">
            <asp:TextBox
                ID="txtCreditEarned" runat="server" CssClass="form-control fullWidth mediumFont" />
        </div>
    </div>
    <span style="font-size: smaller; font-style: italic;">*** When the credit type 'Other' is selected, you have the option of entering a custom credit type. </span>
    <br />
    <asp:Label runat="server" ID="lblError" CssClass="error" /><br />
    <asp:Button runat="server" ID="btnAddCredit" Text="Add Credit" CssClass="formInput btn btn-R17Blue btn-lg" Style="width: 140px; height: 30px; font-size: small" />
    <br />
    <br />
    <table style="border-collapse: collapse; border: solid 1px gray; width: 100%">
        <tr>
            <td>
                <asp:Label ID="StartDateLabel"
                    Text="Start Date:"
                    AssociatedControlID="txtStartDate"
                    runat="server">

                    <asp:TextBox runat="server" ID="txtStartDate" CssClass="formInput" />&nbsp;<asp:ImageButton
                        runat="server" ID="btnCalendar2" alt="Start Date Calendar Button" ImageUrl="~/lib/img/buttons/calendar.png" />
                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtStartDate"
                        PopupButtonID="btnCalendar2" />
                </asp:Label>
                <asp:Label ID="EndDateLabel"
                    Text="End Date:&nbsp;"
                    AssociatedControlID="txtEndDate"
                    runat="server">

                    <asp:TextBox runat="server" ID="txtEndDate" CssClass="formInput" />&nbsp;<asp:ImageButton
                        runat="server" ID="btnCalendar3" alt="End Date Calendar Button" ImageUrl="~/lib/img/buttons/calendar.png" />
                    <cc1:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtEndDate"
                        PopupButtonID="btnCalendar3" />
                </asp:Label>
                &nbsp;<asp:Button runat="server" ID="btnGo" Text="Go" CssClass="formInput btn btn-R17Blue btn-lg" Style="width: 50px; height: 30px; font-size: small" />
                &nbsp;
                    <asp:Button runat="server" ID="btnPrint" Text="Print" CssClass="formInput btn btn-R17Blue btn-lg" Style="width: 50px; height: 30px; font-size: small" />&nbsp;
                    <br />
                <asp:CheckBox
                    runat="server" ID="chkIncludeOffical" Text="Include Official Credits" Visible="false" /></td>
        </tr>
        <tr>
            <td>
                <asp:PlaceHolder runat="server" ID="pTableResults" />
            </td>
        </tr>
    </table>

    <asp:HiddenField runat="server" ID="hiddenField" />
</asp:Content>
