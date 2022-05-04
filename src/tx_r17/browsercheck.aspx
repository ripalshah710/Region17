<%@ Page Language="C#" AutoEventWireup="true" CodeFile="browsercheck.aspx.cs" Inherits="browsercheck" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>

    
<body>
  
    <form id="form1" runat="server">
    <div>
        <table border="0" >
            <tr>
                 <td align="center" colspan="3"><img src="lib/img/warning.jpg"  height="150"/><br></td>
            </tr>

        <tr>
           <td align ="center" colspan="3"> <font size="5" >Unsupported Browser</font></td>
      
        </tr>
            <tr>
              <td colspan="3"><hr size="1"></td>
            </tr>
            <tr>
                <td colspan="3">
     <font  color="red">You are using an unsupported browser.</font><br><br>
        Unsupported browsers can put your security at risk, are slow and don't work with newer features. To get the latest this website has to offer, you will need to upgrade your browser.
        Please install the latest version of Internet Explore or Chrome that escWorks supports.<br><br></td>

                    <tr>
                        <td align="left">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img src="lib/img/IE.png" /></td>
                        <td>Install upgraded version of Internet Explorer for free</td>
                        <td><a href="http://windows.microsoft.com/en-us/internet-explorer/download-ie"><img src="lib/img/install.png" height="70"/> </a></td>


                    </tr>
    


                    <tr>
                        <td align="left"><img src="lib/img/chrome.png"  height="120" width="170"/></td>
                        <td>Install upgraded version of Google Chrome for free</td>
                        <td><a href="https://www.google.com/chrome/"><img src="lib/img/install.png" height="70" /> </a></td>


                    </tr>

   
</table>
    
    </div>

        By closing this window you acknowledge that your experience on this website may be degraded.<br><br>
        <asp:Button ID="btnContinue" Text="Close This Window" runat="server" OnClick="btnContinue_Click" /><br><br>

        
    </form>
</body>
</html>
