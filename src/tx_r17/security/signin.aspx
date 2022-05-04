<%@ Page Language="C#" AutoEventWireup="true" CodeFile="signin.aspx.cs" Inherits="security_signin" MasterPageFile="~/MasterPage.master" %>

<asp:Content runat="server" ID="content1" ContentPlaceHolderID="mainBody">
    <a name="MainBody"></a>
    <span style="padding-right: 10px;">
        <button type="button" onclick="javascript:history.back()" class="formInput btn btn-R17Blue btn-lg" role="button" style="width: 130px; font-size: small" tooltip="Click here to go to previous page.">Previous</button></span>
    <br />
    <br />
    <div>

        <div class="row">
            <div class="col-xs-12 col-sm-6" style="background-color: #F0F0F0; padding: 20px 5px 20px 5px;">
                <h2 style="padding-left: 13px;">Account Sign-In</h2>
                <br />
                <span style="padding-left: 13px;">Sign in to your account</span><br />
                <br />

                <div class="form-group col-xs-12 col-sm-11">
                    <div class="input-group">
                        <div class="input-group-prepend">
                            <div class="input-group-text">
                                <div class="btn btn-secondary glyphicon glyphicon-user">
                                </div>
                            </div>
                        </div>
                        <label for="ctl00_mainBody_txtUserName" style="color:white; display:none;">Username</label><br />
                        <asp:TextBox ID="txtUserName" CssClass="smallFont" Style="height: 32px; width: 80%; font-family: Arial; color: black; margin: 0px;" class="form-control" placeholder="E-mail Address" runat="server" />
                    </div>
                </div>
                <div class="form-group col-xs-12 col-sm-11">
                    <div class="input-group">
                        <div class="input-group-prepend">
                            <div class="input-group-text">
                                <div class="btn btn-secondary glyphicon glyphicon-lock">
                                </div>
                            </div>
                        </div>
                        <label for="ctl00_mainBody_txtPassword" style="color:white; display:none;">Username</label><br />
                        <asp:TextBox ID="txtPassword" CssClass="smallFont" Style="height: 32px; width: 80%; font-family: Arial; color: black; margin: 0px;" class="form-control" placeholder="Password" TextMode="Password" runat="server" />
                    </div>

                </div>
                <div class="form-group col-xs-12 col-sm-11">
                    <br />
                    <div class="form-group col-xs-12 col-sm-6">
                        <asp:Button CssClass="btn btn-R17Blue btn-sm" runat="server" ID="btnSubmit" Text="Login" OnClick="btnSubmit_Click" Style="font-size: medium; width: 140px;" /></div>
                    <br />
                    <br />
                    <div class="form-group col-xs-12 col-sm-6"><span><a href="../shoebox/account/password.aspx?mode=forgot">Forgot Password?</a></span></div>
                </div>
            </div>

            <div class="col-xs-12 col-sm-6" style="background-color: #1b4065; padding: 20px 5px 20px 5px;">
                <h2 style="padding-left: 13px; color: white;">Create an Account</h2>
                <br />
                <br />
                <div style="padding-left: 13px; color: white;">To create a new <b>Professional Development</b> account please click the "<b>Create Account</b>" button.</div>
                <br />
                <br />
                <br />
                <br />

                <div style="padding-left: 18px;">
                    <div class="form-group col-xs-12 col-sm-6">
                        <asp:Button CssClass="formInput btn btn-secondary btn-sm" runat="server" ID="btnNewAccount" Text="Create Account" Style="font-size: medium; color: white; width: 140px;" PostBackUrl="~/shoebox/account/signup.aspx" />
                    </div>
                </div>
            </div>

        </div>
        <br />
        <br />
        <asp:Label runat="server" ID="lblMessage" CssClass="error" />
    </div>
</asp:Content>
