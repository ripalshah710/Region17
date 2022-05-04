<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UploadFile.ascx.cs" EnableViewState="False" Inherits="lib_Controls_UploadFile" %>
  <table border="0" cellpadding="0" cellspacing="0" width="100%">
   <asp:Panel ID="pnlUpload" runat="server" Visible="false">
        <div>
         <strong><label for=<%=FileUpload1.ClientID %>>File Upload:</label></strong><br />      
        <asp:FileUpload ID="FileUpload1" runat="server" /><br />
        <br />
        <asp:Button ID="btnUpload" runat="server" OnClick="btnUpload_Click" Text="Upload" /><br />
        <br />
        <asp:Label ID="lblMessage" runat="server"></asp:Label><br />
        </div>
        </asp:Panel>
   </table>
