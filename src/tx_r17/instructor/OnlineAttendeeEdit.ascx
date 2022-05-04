<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OnlineAttendeeEdit.ascx.cs"
    Inherits="instructor_OnlineAttendeeEdit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<table id="Table2" cellspacing="2" cellpadding="1" width="100%" border="1" rules="none"
    style="border-collapse: collapse">
    <tr>
        <td>
            <table id="Table3" cellspacing="1" cellpadding="1" width="100%" border="0">
                <tr>
                    <td>
                        Expiration Date:
                    </td>
                    <td>
                        <telerik:RadDatePicker ID="ExpirationDatePicker" runat="server" MinDate="1/1/1900"
                            DbSelectedDate='<%# DataBinder.Eval( Container, "DataItem.OnlineExpirationDateDisplay") %>'
                            TabIndex="1">
                        </telerik:RadDatePicker>
                    </td>
                </tr>
                <tr>
                    <td>
                        Attended Date:
                    </td>
                    <td>
                        <telerik:RadDatePicker ID="AttendedDatePicker" runat="server" MinDate="1/1/1900"
                            DbSelectedDate='<%# DataBinder.Eval( Container, "DataItem.AttendedTimeStampDisplay") %>'
                            TabIndex="2">
                        </telerik:RadDatePicker>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td align="right" colspan="2">
            <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update"></asp:Button>
            &nbsp;
            <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                CommandName="Cancel"></asp:Button>
        </td>
    </tr>
</table>
