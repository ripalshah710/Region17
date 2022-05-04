<%@ Page Language="C#" AutoEventWireup="true" CodeFile="email.aspx.cs" Inherits="instructor_email" MasterPageFile="~/MasterPage.master" %>

<asp:Content runat="server" ContentPlaceHolderID="mainBody"><a name="MainBody"></a>

    <asp:Panel runat="server" ID="panelEmail">
        <table width="100%">
            <tr>
                <td colspan="2">
                    <asp:Label runat="server" ID="lblSessionID" Font-Bold="True" /></td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label runat="server" ID="lblSessionTitle" Font-Italic="True" /></td>
            </tr>
           <asp:Panel runat="server" ID="panelEmailStuff">
            <tr>
                <td colspan="2">
                    <b>Recipient List:</b></td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:CheckBoxList runat="server" ID="chkEmailAddresses"  AutoPostBack="true"/><br />
                    <asp:LinkButton runat="server" ID="lnkCheckAll"   Text="select all" Font-Italic="true" />&nbsp;&nbsp;
                    <asp:LinkButton runat="server" ID="lnkCheckNone"   Text="select none" Font-Italic="true" />
                </td>
                <tr>
                    <td>
                    </td>
                    <td>
                        <br />
                        <br />
                    </td>
                </tr>
            </tr>
           <tr><td valign="top">To: </td><td><asp:Label runat="server" ID="lblTo" /></td></tr> 
            <tr>
                <td valign="top">
                    <asp:Label ID="SubjectLabel"
                        text="Subject:"
                        AssociatedControlID="txtSubject"
                        runat="server"></asp:Label>
                        </td>
                <td width="75%">
                    <asp:TextBox runat="server" ID="txtSubject" Width="100%" /></td>
            </tr>
            <tr>
                <td valign="top">
                    <asp:Label ID="CommentsLabel"
                        text="Comments:"
                        AssociatedControlID="txtComments"
                        runat="server"></asp:Label></td>
                <td>
                    <asp:TextBox runat="server" ID="txtComments" TextMode="MultiLine" Width="100%" Height="100px" /></td>
            </tr>
           </asp:Panel>
          <asp:Panel runat="server" ID="panelNoAttendees">
          <tr>
          <td colspan="2"><b>No Attendees are currently registered for this session</b></td>
          </tr>
          </asp:Panel>  
        </table>
        
        <asp:Button runat="server" ID="btnSend" Text="Send"  />&nbsp;&nbsp;&nbsp;<asp:Button
            runat="server" ID="btnCancel" Text="Cancel"  />
           
            </asp:Panel>
    <asp:Panel runat="server" ID="panelConfirmation" Visible="false">
        Your email has been sent.
        <br /><br /><br /><br />
        <asp:Button runat="server" ID="btnContinue" Text="Continue" />
    </asp:Panel>
</asp:Content>