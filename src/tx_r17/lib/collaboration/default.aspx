<%@ Page Language="C#" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="lib_collaboration_default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="en-us" xmlns="http://www.w3.org/1999/xhtml">
<%--<html xmlns="http://www.w3.org/1999/xhtml" lang="en" xml:lang="en">--%>
<head runat="server">

    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" />
  <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
  <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>


    <title>Share a Page</title>
    <script src='https://www.google.com/recaptcha/api.js' type="text/javascript" async defer></script>
        <meta name="viewport" content="width=device-width, initial-scale=1">

      <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" />
  <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
<script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>

        <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" />
        <link href="Content/bootstrap.min.css" rel="stylesheet" />
              <link href="../../Content/screen1004.css" rel="stylesheet" type="text/css" />
    <link href="../../Content/screen768.css" rel="stylesheet" type="text/css" />
    <link href="../../Content/screen480.css" rel="stylesheet" type="text/css" />
    <link href="../../Content/screen320.css" rel="stylesheet" type="text/css" />
<%--    <title>Share a Page</title>
    <script src='https://www.google.com/recaptcha/api.js' type="text/javascript" async defer></script>
    <meta name="viewport" content="width=device-width, initial-scale=1">

      <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" />
  <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
  <script src="https://maxc
        <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" />
        <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="../../Content/screen1004.css" rel="stylesheet" type="text/css" />
    <link href="../../Content/screen768.css" rel="stylesheet" type="text/css" />
    <link href="../../Content/screen480.css" rel="stylesheet" type="text/css" />
    <link href="../../Content/screen320.css" rel="stylesheet" type="text/css" />--%>
</head>

<%--<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" 
<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
<script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>--%>

<script language="javascript" type="text/javascript">
    function Page_OnInit() {
        resizeTo(350, 650);
    }
</script>



<body onload="Page_OnInit();">
    <%--<a name="MainBody"></a>--%>
    <span style="padding-right: 10px;"><button type="button" onclick="javascript:history.back()" class="formInput btn btn-R17Blue btn-sm" role="button" style="width: 130px; font-size:small" ToolTip="Click here to go to previous page.">Previous</button></span>
    <br /><br />
    <div style="padding-left: 10px">
    <form id="form1" runat="server">

    <div class="container-fluid">      
    <div class="row">
    <div class="col-12 col-sm-3">
        <img src="../../lib/standard/img/trans.gif" border="0" height="1" alt="Transparent Gif" />     
    </div>  
        
    <div class="row"> 
        <div class="col-12 col-sm-3">
            <img src="../../lib/standard/img/trans.gif" border="0" height="4" alt="Transparent Gif" />     
        </div>   
    </div>

        <div class="row"> 
            <div class="col-12 col-sm-3">
                <div style="background-color:#ffffff">&nbsp;<img src="../../lib/standard/img/share.gif" width="32" height="32" alt="" border="0"
                        align="middle" /> 
                </div>
            <%--</div>--%>   
            <%--<div class="col-12 col-sm-12">--%>
                <div style="background-color:#ffffff">
                        <span style="color: #191970; font-weight: bold; font-size: 14pt;">Collaboration: Share
                        a page</span><br />
                        <span style="font-size: 12pt;">Share a resource with a colleague or friend.</span>
                </div> 
            </div>
        </div>
        <div class="row"> 
            <div class="col-12 col-sm-3">
                <div style="background-color:#ffffff">
                    <img src="../../lib/standard/img/trans.gif" border="0" height="4" alt="Transparent Gif" />
                </div>
            </div>   
        </div>
        <div class="row"> 
            <div class="col-12 col-sm-3">
                <div style="background-color:#191970">
                    <img src="../../lib/standard/img/trans.gif" border="0" height="1" alt="Transparent Gif" />
                </div> 
            </div>   
        </div>

        <div class="row"> 
            <div class="col-12 col-sm-3">
                <div class="FormInput">You are sharing the following resource:<br />
                </div> 
            </div>   
        </div>
        <div class="row"> 
            <div class="col-12 col-sm-3">
                <span style="color:#0066ff" onclick="window.close();"><u><i>Session Detail</i></u></span>
                <asp:Label ID="ResourceSource" runat="server" Font-Size="9pt" Font-Italic="True"></asp:Label>
            </div> 
        </div>
                                   <br />
                                    <br />

        <div class="row"> 
            <div class="col-12 col-sm-3">
                
                <asp:Label ID="RecipeintLabel"
                                        text="Friend or Colleagues Email:"
                                        AssociatedControlID="ResourceRecipient"
                                        runat="server"></asp:Label>                                  
									<asp:RequiredFieldValidator ID="Require_EmailText" runat="Server" ControlToValidate="ResourceRecipient" ForeColor="DarkRed" ErrorMessage="Email is missing.<br>" CssClass="RequiredText"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="Validate_EmailText" runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ErrorMessage="The email address is not in an acceptable format. (user@domain.com)<br>" Display="Dynamic" ControlToValidate="ResourceRecipient" CssClass="RequiredText"></asp:RegularExpressionValidator>
                                    <asp:TextBox ID="ResourceRecipient" runat="server" CssClass="TextBox" Width="100%"></asp:TextBox>
                                    <asp:PlaceHolder ID="pSenderInfo" runat="server">
                                    </div>   
        </div>
        <br />

<div class="row"> 
            <div class="col-12 col-sm-3">
                <asp:Label ID="YourNameLabel"
                                            text="Your Name:"
                                            AssociatedControlID="ResourceSender"
                                            runat="server"></asp:Label><br />
                                        
            <asp:TextBox ID="ResourceSender" runat="server" CssClass="TextBox" Style="width: 100%"></asp:TextBox>
    </div> 
</div> 
<br />
        <div class="row"> 
            <div class="col-12 col-sm-3">
                                    <asp:Label ID="YourEmailLabel"
            text="Your E-mail:"
            AssociatedControlID="ResourceSenderEmail"
            runat="server"></asp:Label>
                                    <asp:RequiredFieldValidator ID="Require_EmailText2" runat="Server" ControlToValidate="ResourceSenderEmail"
                                        ForeColor="DarkRed" ErrorMessage="Email is missing.<br>" CssClass="RequiredText"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="Validate_EmailText2" runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                        ForeColor="DarkRed" ErrorMessage="The email address is not in an acceptable format. (user@domain.com)<br>"
                                        Display="Dynamic" ControlToValidate="ResourceSenderEmail" CssClass="RequiredText"></asp:RegularExpressionValidator>
                                    <asp:TextBox ID="ResourceSenderEmail" runat="server" CssClass="TextBox" Width="100%"></asp:TextBox>
                                </asp:PlaceHolder>
    </div>
    </div>  
        <br />
    <div class="row"> 
            <div class="col-12 col-sm-3">
                <asp:Label ID="MessageLabel"
                                        text="Short Message:"
                                        AssociatedControlID="ResourceMessage"
                                        runat="server"></asp:Label><br />

                                    <asp:TextBox ID="ResourceMessage" runat="server" CssClass="TextBox" TextMode="MultiLine"
                                        Rows="10" Columns="40" Height="60px" Style="width: 100%"></asp:TextBox>
        </div> 
    </div> 
<br />
        <div>
            <label for="g-recaptcha-response" style="color:white; display:none;">recaptcha-response</label><br />
        <div class="g-recaptcha" data-sitekey="6LeRZBETAAAAAKN3XmAIQW4yM6xh0icj4W6SOvsv">
        </div>
        <br />
        <br />
    </div>
    <div class="row"> 
        <div class="col-12 col-sm-3">
            <asp:Label runat="server" CssClass="error" ID="lblError" />
        <br />
                <asp:Button runat="server" ID="btnSend" BackColor="DarkBlue" ForeColor="White" Text="Send" CssClass="formInput btn btn-R17Blue btn-sm" style="width: 140px; font-size:small" />
            &nbsp;&nbsp;
            <input type="button" value="Cancel" class="formInput btn btn-secondary btn-sm" style="width: 140px; font-size:small;" onclick="self.close();" />
        </div>
    </div>
</div>
</div>
    </form>
</div>
</body>
</html>
