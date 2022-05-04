<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="contact.aspx.cs" Inherits="about_contact" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mainBody" runat="Server"><a name="MainBody"></a>

    <asp:Panel runat="server" ID="pStep1">

<div class="container">

  <div class="row">
        <div class="form-group col-xs-12 col-sm-6" >  
        <asp:Label ID="categorylabel"
                  Text ="Category: <br/>"
                  AssociatedControlID="ddlCategory"
                  CssClass="smallFont"
                  runat="server">
       </asp:Label>
       <asp:DropDownList 
           ID="ddlCategory" CssClass="form-control smallFont" style="height: 35px;" runat="server" />
           
    </div>
  </div>

    <div class="row">
       <div class="form-group col-xs-12 col-sm-12">  
        <asp:Label ID="CommentsLabel" class="textwidth"
                  Text ="Enter your comments in the space provided below: <br/> "
                  AssociatedControlID="txtComments"
            CssClass="smallFont"
                  runat="server">
       </asp:Label>
       
       <asp:TextBox 
           ID="txtComments" CssClass="form-control smallFont" TextMode="MultiLine"
            Height="99px" runat="server"/>
           
        </div>
     </div>

        <div class="textwidth smallFont"><b>Please provide some information about yourself:</b></div>
        <br />

    <div class="row" style="padding-left: 15px;">
      <div class="form-group col-xs-12 col-sm-6">      
            <asp:Label ID="NameLabel"
                  Text ="Name:"
                  AssociatedControlID="txtName"
                  CssClass="smallFont"
                  runat="server">
            </asp:Label>
            <asp:TextBox  
           ID="txtName" CssClass="form-control smallFont" runat="server" />
           
        </div>
    </div>

    <div class="row" style="padding-left: 15px;">
        <div class="form-group col-xs-12 col-sm-6">  
        <asp:Label ID="EmailLabel"
                  Text ="E-mail:"
                  AssociatedControlID="txtEmail"
                  CssClass="smallFont"
                  runat="server">
       </asp:Label>
       <asp:TextBox  
           ID="txtEmail" CssClass="form-control smallFont" runat="server"/>      
      </div>
    </div>

    <div class="row" style="padding-left: 15px;">
        <div class="form-group col-xs-12 col-sm-6"> 
        <asp:Label ID="PhoneLabel"
                  Text ="Telephone:"
                  AssociatedControlID="txtPhone"
                  CssClass="smallFont"
                  runat="server">
       </asp:Label>
       <asp:TextBox  
           ID="txtPhone" CssClass="form-control smallFont" runat="server" />
           
      </div>
    </div>
  </div>
        <br />
        <asp:CheckBox runat="server" ID="chkASAP" CssClass="textwidth smallFont" Text="Please contact me as soon as possible regarding this matter." />
        <br />
        <br />
        <label for="g-recaptcha-response" style="color:white; display:none;">recaptcha-response</label><br />
        <div class="g-recaptcha" data-sitekey="6LeRZBETAAAAAKN3XmAIQW4yM6xh0icj4W6SOvsv" id="g-recaptcha" title="g-recaptcha" style="width:100%">
        </div>
        <br />
        <br />
        <asp:Label runat="server" ID="lblError" CssClass="error smallFont" /><br />
        <br />
        <asp:Button runat="server" ID="btnSubmit" Text="Submit Comments" CssClass="formInput btn btn-R17Blue btn-lg" Style="width: 150px; font-size:small" />
        &nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button runat="server" ID="btnCancel" Text="Cancel" CssClass="formInput btn btn-secondary btn-lg" Style="width: 150px; font-size:small" />
    </asp:Panel>
    <asp:Panel runat="server" ID="pStep2">
        <span class="smallFont">Thank you for your comments</span><br />
        <br />
        <a href="~/default.aspx" runat="server" class="link smallFont">Click here to continue</a>
    </asp:Panel>

<%--    <asp:Panel runat="server" ID="pStep1">
        <asp:Label ID="CategoryLabel"
            text="<b>Category</b>"
            AssociatedControlID="ddlCategory"
            runat="server"></asp:Label>
            <br />
        <asp:DropDownList runat="server" ID="ddlCategory" CssClass="formInput" />
        <br />
        <br />
        <asp:Label ID="CommentsLabel"
            text="<b>Enter your comments in the space provided below:</b>"
            AssociatedControlID="txtComments"
            runat="server"></asp:Label>
        <br />
        <asp:TextBox runat="server" ID="txtComments" CssClass="formInput" TextMode="MultiLine"
            Height="99px" Width="282px" />
        <br />
        <br />
        <b>Please provide some information about yourself:</b><br />
        <br />
        <asp:Label ID="NameLabel"
            text="<b>Name: </b>"
            AssociatedControlID="txtName"
            runat="server"></asp:Label>
        <asp:TextBox runat="server" ID="txtName" CssClass="formInput" />
        <br />
        <br />
        <asp:Label ID="EmailLabel"
            text="<b>E-mail: </b>"
            AssociatedControlID="txtEmail"
            runat="server"></asp:Label>
        <asp:TextBox runat="server" ID="txtEmail" CssClass="formInput" />
        <br />
        <br />
        <asp:Label ID="TelephoneLabel"
            text="<b>Telephone: </b>"
            AssociatedControlID="txtPhone"
            runat="server"></asp:Label>
        <asp:TextBox runat="server" ID="txtPhone" CssClass="formInput" />
        <br />
        <asp:CheckBox runat="server" ID="chkASAP" Text="Please contact me as soon as possible regarding this matter." />
        <br />
        <br />
        <div class="g-recaptcha" data-sitekey="6LeRZBETAAAAAKN3XmAIQW4yM6xh0icj4W6SOvsv">
        </div>
        <br />
        <br />
        <asp:Label runat="server" ID="lblError" CssClass="error" /><br />
        <br />
        <asp:Button runat="server" ID="btnSubmit" Text="Submit Comments" />
        <asp:Button runat="server" ID="btnCancel" Text="Cancel" /></asp:Panel>
    <asp:Panel runat="server" ID="pStep2">
        Thank you for your comments<br />
        <br />
        <a href="~/default.aspx" runat="server" class="link">Click here to continue</a>
    </asp:Panel>--%>
</asp:Content>
