<%@ Page Language="C#" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="shoebox_account_default" EnableEventValidation="false" MasterPageFile="~/MasterPage.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content runat="server" ContentPlaceHolderID="mainBody">
    <a name="MainBody"></a>
    <span style="padding-right: 10px;">
        <button type="button" onclick="javascript:history.back()" class="formInput btn btn-R17Blue btn-lg" role="button" style="width: 130px; font-size: small" tooltip="Click here to go to previous page.">Previous</button></span>
    <br />
    <br />
    <asp:Panel runat="server" ID="pFirst">

        <div class="row">
            <div class="col-xs-12 col-sm-8">
                <asp:Label ID="PrimaryEmailLabel"
                    Text="<strong>Primary Email:</strong>"
                    AssociatedControlID="txtPrimaryEmail"
                    CssClass="smallFont"
                    runat="server"></asp:Label>
                <asp:TextBox ID="txtPrimaryEmail" runat="server" CssClass="form-control smallFont" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtPrimaryEmail"
                    CssClass="error smallFont" Display="Dynamic" ForeColor="DarkRed" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">Primary Email is required</asp:RequiredFieldValidator>

            </div>
        </div>

        <button type="button" class="btn btn-R17Blue btn-lg" onclick="location.href='../account/email.aspx'">Change Primary Email</button>
        <asp:LinkButton ID="btnChangePassword" runat="server" OnClick="OnChangePassword"><button type="button" class="btn btn-secondary btn-lg">Change Password</button></asp:LinkButton>
        <br />
        <br />
        <div class="row">
            <div class="col-xs-12 col-sm-8">
                <asp:Label ID="SecondaryEmailLabel"
                    Text="<strong>Secondary Email:</strong>"
                    AssociatedControlID="txtSecondaryEmail"
                    CssClass="smallFont"
                    runat="server"></asp:Label>
                <asp:TextBox ID="txtSecondaryEmail" CssClass="form-control smallFont" runat="server" /><br />
            </div>
        </div>
        <div class="row">
            <div class="col-xs-12 col-sm-4">
                <asp:Label ID="SalutationLabel"
                    Text="<strong>* Salutation</strong>"
                    AssociatedControlID="ddlSalutation"
                    CssClass="smallFont"
                    runat="server"></asp:Label>
                <asp:DropDownList ID="ddlSalutation" runat="server" Height="33px" CssClass="form-control smallFont">
                </asp:DropDownList><br />
            </div>
        </div>

        <div class="row">
            <div class="col-xs-12 col-sm-4">
                <asp:Label ID="LastNameLabel"
                    Text="<strong>* Last Name:</strong>"
                    AssociatedControlID="txtLastName"
                    CssClass="smallFont"
                    runat="server"></asp:Label>
                <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control smallFont" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtLastName"
                    CssClass="error smallFont" ForeColor="DarkRed" ErrorMessage="Last Name is required"></asp:RequiredFieldValidator>
            </div>

            <div class="col-xs-12 col-sm-4">
                <asp:Label ID="FirstNameLabel"
                    Text="<strong>* First Name: </strong>"
                    AssociatedControlID="txtFirstName"
                    CssClass="smallFont"
                    runat="server"></asp:Label>
                <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control smallFont" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtFirstName"
                    CssClass="error smallFont" ForeColor="DarkRed" ErrorMessage="First Name is required"></asp:RequiredFieldValidator>
            </div>

            <div class="col-xs-12 col-sm-4">
                <asp:Label ID="MiddleNameLabel"
                    Text="<strong>Middle Name:</strong>"
                    AssociatedControlID="txtMiddleName"
                    CssClass="smallFont"
                    runat="server"></asp:Label>
                <asp:TextBox ID="txtMiddleName" runat="server" CssClass="form-control smallFont" /><br />
            </div>
        </div>
        <div class="row">
            <div class="col-xs-12 col-sm-12">
                <asp:Label ID="HomeAddressLabel"
                    Text="<strong>* Home Address:</strong>"
                    AssociatedControlID="txtHomeAddress"
                    CssClass="smallFont"
                    runat="server"></asp:Label>
                <asp:TextBox ID="txtHomeAddress" runat="server" CssClass="form-control smallFont" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtHomeAddress"
                    CssClass="error smallFont" ForeColor="DarkRed" ErrorMessage="Home Address is required"></asp:RequiredFieldValidator>
            </div>
        </div>

        <div class="row">
            <div class="col-xs-12 col-sm-4">
                <asp:Label ID="CityLabel"
                    Text="<strong>* City:</strong>"
                    AssociatedControlID="txtCity"
                    CssClass="smallFont"
                    runat="server"></asp:Label>
                <asp:TextBox ID="txtCity" runat="server" CssClass="form-control smallFont" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtCity"
                    CssClass="error smallFont" ForeColor="DarkRed" ErrorMessage="City is required"></asp:RequiredFieldValidator>
            </div>
            <div class="col-xs-12 col-sm-4">
                <asp:Label ID="StateLabel"
                    Text="<strong>* State: (2 letter Abbrev)</strong>"
                    AssociatedControlID="ddState"
                    CssClass="smallFont"
                    runat="server"></asp:Label>
                <asp:DropDownList ID="ddState" runat="server" CssClass="form-control smallFont" Style="height: 30px"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddState"
                    CssClass="error smallFont" ForeColor="DarkRed" ErrorMessage="State is required"></asp:RequiredFieldValidator>
            </div>
            <div class="col-xs-12 col-sm-4">
                <asp:Label ID="ZipLabel"
                    Text="<strong>* Zip:</strong>"
                    AssociatedControlID="txtZip"
                    CssClass="smallFont"
                    runat="server"></asp:Label>
                <asp:TextBox ID="txtZip" runat="server" CssClass="form-control smallFont" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtZip"
                    CssClass="error smallFont" ForeColor="DarkRed" ErrorMessage="Zip is required"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="row">
            <div class="col-xs-12 col-sm-4">
                <asp:Label ID="HomePhoneLabel"
                    Text="<strong>* Home Phone:</strong>"
                    AssociatedControlID="txtHomePhone"
                    CssClass="smallFont"
                    runat="server"></asp:Label>
                <asp:TextBox ID="txtHomePhone" Text="Home/Cell Phone" runat="server" CssClass="form-control smallFont" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtHomePhone"
                    CssClass="error smallFont" ForeColor="DarkRed" ErrorMessage="Home Phone is required"></asp:RequiredFieldValidator>
            </div>
            <div class="col-xs-12 col-sm-4">
                <asp:Label ID="WorkPhoneLabel"
                    Text="<strong>Work Phone:</strong>"
                    AssociatedControlID="txtWorkPhone"
                    CssClass="smallFont"
                    runat="server"></asp:Label>
                <asp:TextBox ID="txtWorkPhone" Text="Work Phone" runat="server" CssClass="form-control smallFont" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtWorkPhone"
                    CssClass="error smallFont" ForeColor="DarkRed" ErrorMessage="Work Phone is required"></asp:RequiredFieldValidator>
            </div>
        </div>

        <div class="row">
            <div class="col-xs-12 col-sm-10">
                <strong>
                    <asp:Label ID="OrganizationLabel"
                        Text="* Region/Organization (If you do not know your Region, Please select 'Other') :"
                        AssociatedControlID="ddlRegion"
                        CssClass="smallFont"
                        runat="server"></asp:Label>
                </strong>
                <asp:DropDownList ID="ddlRegion" runat="server" CssClass="form-control smallFont" Height="33px" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlRegion"
                    CssClass="error smallFont" ForeColor="DarkRed" ErrorMessage="Organization is a required field"></asp:RequiredFieldValidator>
                <cc1:CascadingDropDown ID="CascadingDropDown1" runat="server" TargetControlID="ddlRegion" Category="Org" PromptText="Please select a region..." ServicePath="~/services/locationservice.asmx" ServiceMethod="GetRegions" />
            </div>
        </div>

        <div class="row">
            <div class="col-xs-12 col-sm-10">
                <strong>
                    <asp:Label ID="DistrictLabel"
                        Text="* District/Site (If you do not know your District, Please select 'Other') :"
                        AssociatedControlID="ddlDistrict"
                        CssClass="smallFont"
                        runat="server"></asp:Label>
                </strong>
                <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="form-control smallFont" Height="33px" /><br />
                <cc1:CascadingDropDown ID="CascadingDropDown2" runat="server" TargetControlID="ddlDistrict" ParentControlID="ddlRegion" PromptText="Please select a district..." ServicePath="~/services/locationservice.asmx" ServiceMethod="GetDistrictsForRegion" Category="Site" />
            </div>
        </div>

        <div class="row">
            <div class="col-xs-12 col-sm-10">
                <strong>
                    <asp:Label ID="SchoolLabel"
                        Text="* Campus (If you do not know your Campus, Please select 'Other') :"
                        AssociatedControlID="ddlSchool"
                        CssClass="smallFont"
                        runat="server"></asp:Label>
                </strong>
                <asp:DropDownList ID="ddlSchool" runat="server" CssClass="form-control smallFont" Height="33px" /><br />
                <cc1:CascadingDropDown ID="CascadingDropDown3" runat="server" TargetControlID="ddlSchool" ParentControlID="ddlDistrict" PromptText="Please select a school..." ServicePath="~/services/locationservice.asmx" ServiceMethod="GetSchoolsFromDistricts" Category="Room" />
            </div>
        </div>

        <div class="row">
            <div class="col-xs-12 col-sm-10">
                <asp:Label ID="PositionLabel"
                    Text="* Position:(Parents, please select PARENT from the below dropdown menu) :"
                    AssociatedControlID="ddlPosition"
                    CssClass="smallFont"
                    runat="server"></asp:Label>
                <asp:DropDownList ID="ddlPosition" runat="server" CssClass="form-control smallFont" Height="33px" /><br />
            </div>
        </div>
        <div class="row">
            <div class="col-xs-12 col-sm-12">
                <strong>
                    For Available Accommodations, please call (806) 281-5701 or (806) 281-5715</strong>
            </div>
        </div>
        <asp:Label ID="lbErrorMessage" runat="server"></asp:Label>
        <br />
        <asp:Button ID="btnSubmit" runat="Server" Text="Save Record" CssClass="formInput btn btn-R17Blue btn-lg"
            Style="width: 170px; font-size: small" ToolTip="Click here to save record."></asp:Button>
    </asp:Panel>
    <asp:Panel runat="server" ID="pSuccess" Visible="false">
        <b>You have successfully saved the changes to your account!<br />
        </b><a href="~/default.aspx" runat="server" class="link">Please click here to continue</a>
    </asp:Panel>
</asp:Content>
