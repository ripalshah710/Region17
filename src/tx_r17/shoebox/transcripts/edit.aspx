<%@ Page Language="C#" AutoEventWireup="true" CodeFile="edit.aspx.cs" Inherits="shoebox_transcripts_edit" MasterPageFile="~/MasterPage.master" %>

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
    </script>
    <table style="border-collapse: collapse; border: 1px; border-color: Gray; vertical-align: top;">
        <tr>
            <td colspan="2" valign="top">

                <asp:Label ID="TitleLabel"
                    Text="Title:"
                    AssociatedControlID="txtTitle"
                    runat="server"></asp:Label>

            </td>
            <td>

                <asp:Label ID="DateLabel"
                    Text="Date:"
                    AssociatedControlID="txtDate"
                    runat="server"></asp:Label>

            </td>
        </tr>
        <tr>
            <td colspan="2" valign="bottom">
                <asp:TextBox runat="server" ID="txtTitle" CssClass="forminput" Width="290px" />
            </td>
            <td>
                <asp:TextBox runat="server" ID="txtDate" CssClass="forminput" />&nbsp;<asp:ImageButton
                    runat="server" ID="btnCalendar1" alt="Calendar Button" ImageUrl="~/lib/img/buttons/calendar.png" />
                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDate"
                    PopupButtonID="btnCalendar1" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="CreditTypeLabel"
                    Text="Credit Type:"
                    AssociatedControlID="ddlCreditType"
                    runat="server"></asp:Label>


            </td>
            <td>
                <asp:Label ID="CreditNameLabel"
                    Text="***Credit Name:"
                    AssociatedControlID="txtCreditName"
                    runat="server"></asp:Label>
            </td>
            <td>

                <asp:Label ID="CreditEarnedLabel"
                    Text="Credit Earned:"
                    AssociatedControlID="txtCreditEarned"
                    runat="server"></asp:Label>

            </td>
        </tr>
        <tr>
            <td>
                <asp:DropDownList runat="server" ID="ddlCreditType" CssClass="formInput" OnChange="CheckForOther(this);" /></td>
            <td>
                <asp:TextBox runat="server" ID="txtCreditName" CssClass="formInput" /></td>
            <td>
                <asp:TextBox runat="server" ID="txtCreditEarned" CssClass="forminput" /></td>
        </tr>
        <tr>
            <td colspan="3">
                <br />
                <asp:Label runat="server" ID="lblError" CssClass="error" /><br />
                <asp:Button runat="server" ID="btnAddCredit" Text="Save Credit" CssClass="formInput" />
                <input type="button" class="formInput" onclick="javascript:window.location.href='personal.aspx';" value="Cancel" /></td>
        </tr>
    </table>
</asp:Content>
