<%@ Page Language="C#" AutoEventWireup="true" CodeFile="password.aspx.cs" Inherits="shoebox_account_password"
    MasterPageFile="~/MasterPage.master" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content runat="server" ContentPlaceHolderID="mainBody"><a name="MainBody"></a>
    <span style="padding-right: 10px;">
        <button type="button" onclick="javascript:history.back()" class="formInput btn btn-R17Blue btn-lg" role="button" style="width: 130px; font-size: small" tooltip="Click here to go to previous page.">Previous</button></span>
    <br />
    <br />
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="PlaceHolder1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="PlaceHolder1" UpdatePanelCssClass="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <asp:PlaceHolder ID="PlaceHolder1" runat="server">
        <table border="0" cellpadding="4" cellspacing="0">
            <tr>
                <td>
                    <asp:PlaceHolder ID="pForgot" runat="server" Visible="False">
                        <div class="container-fluid">
                            <div class="row">
                                <div class="col-xs-12 col-sm-12">
                                    <p>
                                        In order to retrieve a lost password, you must supply your email address and click
                                    'Send'.
                                    </p>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-xs-12 col-sm-12">
                                    <p>You will be emailed a link that will enable you to change your password.</p>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-xs-12 col-sm-6">
                                    <asp:Label ID="ForgotEmailLabel"
                                        Text="Email Address:"
                                        AssociatedControlID="ForgotEmail"
                                        runat="server"></asp:Label><br />
                                    <asp:RequiredFieldValidator ID="Require_EmailText" runat="Server" ControlToValidate="ForgotEmail"
                                        ErrorMessage="Email is missing.<br>" CssClass="RequiredText" Display="Dynamic"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="Validate_EmailText" runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                        ErrorMessage="The email address is not in an acceptable format. (user@domain.com)<br>"
                                        Display="Dynamic" ControlToValidate="ForgotEmail" CssClass="RequiredText"></asp:RegularExpressionValidator>
                                    <asp:TextBox ID="ForgotEmail" runat="server" CssClass="mediumFont fullWidth" Style="height: 28px"></asp:TextBox>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-xs-12 col-sm-6">
                                    <p><b>Account F.A.Q.s</b></p>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-xs-12 col-sm-6">
                                    <p><b>Q:</b>What do I do now that my email address has changed?</p>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-xs-12 col-sm-6">
                                    <p>
                                        <b>A:</b>If you know your previous email address, <a href="email.aspx" class="link">click
                                                    here</a> to update your account.
                                    </p>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-xs-12 col-sm-3">
                                    <asp:Button ID="SendEmailButton" runat="server" Text="Send" CssClass="btn-dark btn btn-lg fullWidth" Style="width: 140px;" OnClick="OnSendEmail" />
                                </div>
                            </div>
                        </div>
                    </asp:PlaceHolder>
                    <asp:PlaceHolder ID="pChangeCode" runat="server" Visible="False">

                        <div class="container-fluid">
                            <div class="row">
                                <div class="col-xs-12 col-sm-12">
                                    <p>
                                        To change your password, you need to provide the information below. Once you have
                                        entered the data required, click on the 'Change Password' button located at the
                                        bottom of this page.
                                    </p>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-xs-12 col-sm-6">
                                    <asp:Label ID="ChangeEmailAddressLabel"
                                        Text="Email Address:<br />"
                                        AssociatedControlID="ChangeEmailUsingCode"
                                        runat="server"></asp:Label>
                                    <asp:TextBox ID="ChangeEmailUsingCode" runat="server" CssClass="mediumFont fullWidth" Style="height: 28px"
                                        ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>
                            <br />

                            <div class="row">
                                <div class="col-xs-12 col-sm-6">
                                    <asp:PlaceHolder ID="PasswordPlaceHolder" runat="server">
                                        <asp:Label ID="OldPasswordLabel"
                                            Text="Old Password:<br />"
                                            AssociatedControlID="OldPasswordTextBox"
                                            runat="server"></asp:Label>
                                        <asp:TextBox ID="OldPasswordTextBox" runat="server" TextMode="Password"
                                            CssClass="mediumFont fullWidth" Style="height: 28px"></asp:TextBox>
                                    </asp:PlaceHolder>
                                </div>
                            </div>
                            <br />

                            <div class="row">
                                <div class="col-xs-12 col-sm-6">
                                    <p>What should your new password be?</p>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-xs-12 col-sm-12">
                                    <p><i>Choose your new password carefully. We recommend using a password that has at least 5 characters that are alpha-numeric.</i></p>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-xs-12 col-sm-6">
                                    <asp:Label ID="NewPasswordLabel"
                                        Text="New Password:<br />"
                                        AssociatedControlID="TNewPassUsingCode"
                                        runat="server"></asp:Label>
                                    <asp:RequiredFieldValidator ID="PasswordValidatorUsingCode" runat="Server" ControlToValidate="TNewPassUsingCode"
                                        ErrorMessage="A valid password is required.<br>" Display="Dynamic" Font-Size="9pt"></asp:RequiredFieldValidator>
                                    <asp:TextBox ID="TNewPassUsingCode" runat="server" TextMode="Password"
                                        CssClass="mediumFont fullWidth" Style="height: 28px"></asp:TextBox>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-xs-12 col-sm-6">
                                    <asp:Label ID="ConfirmPasswordLabel"
                                        Text="Confirm New Password:<br />"
                                        AssociatedControlID="TConfirmPassUsingCode"
                                        runat="server"></asp:Label>
                                    <asp:CompareValidator ID="ComparePasswordUsingCode" runat="Server" ErrorMessage="The password entered must match!<br>"
                                        ControlToValidate="TNewPassUsingCode" ControlToCompare="TConfirmPassUsingCode"
                                        Display="Dynamic"></asp:CompareValidator>
                                    <asp:TextBox ID="TConfirmPassUsingCode" runat="server" TextMode="Password"
                                        CssClass="mediumFont fullWidth" Style="height: 28px"></asp:TextBox>
                                </div>
                            </div>

                            <br />
                            <div class="row">
                                <div class="col-xs-12 col-sm-3">
                                    <asp:Button ID="btnChangePassword" CssClass="btn-R17Blue btn btn-lg fullWidth" runat="server" Text="Change Password" OnClick="OnChangePassword" />
                                </div>
                            </div>
                        </div>
                    </asp:PlaceHolder>
                    <br />
                    <br />
                    <asp:PlaceHolder ID="pMessage" runat="server" Visible="False">
                        <asp:Label ID="labelMessageHeader" runat="server" Text="Label"></asp:Label>
                        <hr />
                        <asp:Label ID="labelMessageDetail" runat="server"></asp:Label>
                    </asp:PlaceHolder>
        </table>
    </asp:PlaceHolder>

</asp:Content>
