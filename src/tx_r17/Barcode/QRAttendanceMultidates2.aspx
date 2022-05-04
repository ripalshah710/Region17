<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QRAttendanceMultidates2.aspx.cs" Inherits="Barcode_QRAttendanceMultidates2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body >
    <form id="form1" runat="server">
    <div align =center>
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
      <asp:Label ID="namedisplay" runat="server" Style="font-size: 20pt"  > </asp:Label>
     </div>
        <asp:PlaceHolder ID=plMultiSessions runat=server Visible=false>
    
        <asp:Table ID=tblSessions runat=server>
        <asp:TableHeaderRow>
            <asp:TableHeaderCell ID="empty" Text=""></asp:TableHeaderCell>
            <asp:TableHeaderCell ID="SessionID" Text="SessionID"></asp:TableHeaderCell>
            <asp:TableHeaderCell ID="Title" Text="Title"></asp:TableHeaderCell>
            <asp:TableHeaderCell ID="StartDate" Text="Start Date"></asp:TableHeaderCell>
            <asp:TableHeaderCell ID="EndDate" Text="End Date"></asp:TableHeaderCell>
            
        </asp:TableHeaderRow>
        </asp:Table>
    
    </asp:PlaceHolder>
       
    
   
     <br>
      <br>
    <asp:Button ID=btnSubmit runat=server OnClick=MarkAttenedance Text=Submit  Width=100PX />
    </form>
</body>
</html>
