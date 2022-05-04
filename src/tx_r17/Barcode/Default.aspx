<%@ Page Title="" Language="C#"  AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Barcode_Default" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<link rel="stylesheet" href="<%=Request.ApplicationPath%>/lib/css/01.style.css">

<html lang="en-us" xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">

     <table align="center" border="0" >
     <tr valign="top">
     <td width="1068px">
     <table style="margin-left: auto;background-color:White;margin-right: auto; margin-top: 20px; border-collapse: collapse; height: 500px; border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid;">
        <tr>
            <td colspan="2" style="padding-right: 0px; padding-left: 0px; padding-bottom: 0px; padding-top: 0px;">
            <a  href="http://www.esc4.net/" title="Region 4 Home">
           	<img border="0" alt="link to home" src="<%= Request.ApplicationPath %>/lib/img/header_bkg.gif"/></a></td>

            </a>
            </td>
        </tr>
        <tr><td colspan="2"><br /></td></tr>
        <tr><td colspan="2" style="padding:10px;">
        <a  href="http://www.escweb.net/tx_esc_04/" style="display:block;"><font size="5px">Home</font></a>
 </td></tr>
       <tr><td colspan="2" style="padding:10px">
 
<asp:Panel runat ="server" ID="Psignin" >
    
    <h1 class="heading">Attendee Sign-in </h1><br />
   
   <table border=0>
   <tr><td>
   <asp:Label runat="server" ID="lbl1" Text="Please scan the bar code in the below given text area and click the submit button"/>
   </td></tr>
   <tr><td><br /></td></tr>
   <tr><td>   
    <asp:TextBox ID="txtbarcode" runat="server" TextMode="MultiLine" Columns ="50" Rows ="25" wrap="false" />
       <asp:Label ID="BarCodeBoxLabel"
text="<font color=#ffffff>Selected Items</font>"
AssociatedControlID="txtbarcode"
runat="server"></asp:Label>
     </td></tr>
  </table>   
    <br />
    <br />
    <asp:Button runat="server" ID="btnSubmit" Text="Submit" OnClick="btnSubmit_Click" />

</asp:Panel>    
 
    <asp:Panel runat="server" ID="pSuccess" Visible="false">
       <b> All the attendees are marked attended!<br /></b>
       <a id="A1" href="~/default.aspx" runat="server" class="link">Please click here to continue</a>
    </asp:Panel>
    </td></tr>
       
       
        <tr> <td >
        <div class="clearfloat">
</div>
<div id="footer">
<link rel="stylesheet" href="<%=Request.ApplicationPath%>/lib/css/01.style.css">
<div id="footerin1">
    <table border="0" cellpadding="0" cellspacing="0" id="ft1" width="100%">
    <tr><td width="100%" align="left"></td></tr>
 <tr><td width="100%" valign="top" align="left"><div class="footertext">7145 West Tidwell  |  Houston, TX 77092-2096 | 713.462.7708</div></td></tr>
 <tr><td width="100%" valign="top" align="left"></td></tr>
 <tr><td width="100%"><img border='0' src='images/10X10.gif' alt="" width='1' height='0'></td></tr>
</table>
</div>
<div id="footerin2">

    <div id="footerin2b>
        <link rel="stylesheet" href="<%=Request.ApplicationPath%>/lib/css/01.style.css">
<!-- UltimateSearch_IgnoreTextBegin -->
	    <table border="0" cellpadding="0" cellspacing="0" width="100%" id="f1">
         <tr><td width="100%" align="left"></td></tr>
         <tr><td width="100%" valign="top" align="left">
         <div class="nostyle"><a href="http://esc4.net/default.aspx?name=pi.feedback" class="footermenu" title="Feedback">Feedback</a>
        <a href="http://esc4.net/default.aspx?name=pi.terms" class="footermenu" title="Terms & Privacy">Terms & Privacy</a>
        <a href="http://esc4.net/default.aspx?name=pi.contact" class="footermenu" title="Contact Us">Contact Us</a></div></td></tr>
         <tr><td width="100%" valign="top" align="left"></td></tr>
         <tr><td width="100%"><img border='0' src='images/10X10.gif' width='1' alt="" height='0'></td></tr>
         <tr><td width="100%" align="left"></td></tr>
         <tr><td width="100%" valign="top" align="left"></td></tr>
         <tr><td width="100%" valign="top" align="left"></td></tr>
         <tr><td width="100%"><img border='0' src='images/10X10.gif' width='1' alt="" height='0'></td></tr>
         </table>
        <!-- UltimateSearch_IgnoreTextEnd -->
    </div>
</div>
<br />
<br />
<br />
</div>        </td>        </tr>

    </table>

    <br />

    </td></tr>
    </table>

    </form>
</body>
</html>


