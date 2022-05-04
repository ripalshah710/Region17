<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="confirmuserprofile.aspx.cs" Inherits="catalog_register_confirmuserprofile"
    EnableEventValidation="false" Title="Confirm User Profile" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainBody" runat="Server">
    <a name="MainBody"></a>
    <span style="padding-right: 10px;">
        <button type="button" onclick="javascript:history.back()" class="formInput btn btn-R17Blue btn-lg" role="button" style="width: 130px; font-size: small" tooltip="Click here to go to previous page.">Previous</button></span>
    <br />
    <br />
    <asp:Label runat="server" ID="lblErrorMessage" CssClass="error" />
    <br />
    <asp:Label ID="labelPleaseVerify" runat="server"><strong><big>Please help us verify your information to ensure you receive proper credit for your professional development. 
        </big></strong></asp:Label><br />
    <br />
    <br />

        <div class="row">
            <div class="col-xs-12 col-sm-12">
                <strong><asp:Label ID="Organizationlable"
                    text=<%# region4.escWeb.SiteVariables.ObjectProvider.OrganizationNameCapitalized %>
                    AssociatedControlID="ddlRegion"
                    runat="server"></asp:Label>
                </strong>(Select Region 17 ESC if you do not know your region.)
            </div>
            <div class="col-xs-12 col-sm-4">
                <asp:DropDownList ID="ddlRegion" runat="server" CssClass="smallestFont fullWidth" style="height: 28px" />
                <br />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlRegion"
                        CssClass="error" forecolor="DarkRed" ErrorMessage="Region is a required field"></asp:RequiredFieldValidator>
                    <cc1:CascadingDropDown ID="CascadingDropDown1" runat="server" TargetControlID="ddlRegion"
                        Category="Org" PromptText="Please select a region..." ServicePath="~/services/locationservice.asmx"
                        ServiceMethod="GetRegions" />
            </div>     
        </div>

<%--    <table width="75%" class="mainBody">
        <tr>
            <td colspan="3" valign="top">
                <strong><asp:Label ID="Organizationlable"
                    text=<%# region4.escWeb.SiteVariables.ObjectProvider.OrganizationNameCapitalized %>
                    AssociatedControlID="ddlRegion"
                    runat="server"></asp:Label>
                </strong>(Select Region 17 ESC if you do not know your region.)
            </td>
        </tr>
        <tr>
            <td colspan="3" valign="top">
                <asp:DropDownList ID="ddlRegion" runat="server" CssClass="formInput" />
                <br />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlRegion"
                    CssClass="error" ErrorMessage="Region is a required field"></asp:RequiredFieldValidator>
                <cc1:CascadingDropDown ID="CascadingDropDown1" runat="server" TargetControlID="ddlRegion"
                    Category="Org" PromptText="Please select a region..." ServicePath="~/services/locationservice.asmx"
                    ServiceMethod="GetRegions" />
            </td>
        </tr>--%>

        <div class="row">
            <div class="col-xs-12 col-sm-12">
                <strong><asp:Label ID="SiteLabel"
                    text=<%# region4.escWeb.SiteVariables.ObjectProvider.SiteNameCapitalized %>
                    AssociatedControlID="ddlDistrict"
                    runat="server"></asp:Label>
                </strong>(Select Other Organizations if you do not know your District.)
            </div>
            <div class="col-xs-12 col-sm-4">
                <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="smallestFont fullWidth" style="height: 28px" />
                <br />
                <br />
                <cc1:CascadingDropDown ID="CascadingDropDown2" runat="server" TargetControlID="ddlDistrict"
                    ParentControlID="ddlRegion" PromptText="Please select a district..." ServicePath="~/services/locationservice.asmx"
                    ServiceMethod="GetDistrictsForRegion" Category="Site" />
            </div>     
        </div>

<%--        <tr>
            <td colspan="3" valign="top">
                <strong><asp:Label ID="SiteLabel"
                    text=<%# region4.escWeb.SiteVariables.ObjectProvider.SiteNameCapitalized %>
                    AssociatedControlID="ddlDistrict"
                    runat="server"></asp:Label>
                </strong>(Select Other Organizations if you do not know your District.)
            </td>
        </tr>
        <tr>
            <td colspan="3" valign="top">
                <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="formInput" />
                <br />
                <br />
                <cc1:CascadingDropDown ID="CascadingDropDown2" runat="server" TargetControlID="ddlDistrict"
                    ParentControlID="ddlRegion" PromptText="Please select a district..." ServicePath="~/services/locationservice.asmx"
                    ServiceMethod="GetDistrictsForRegion" Category="Site" />
            </td>
        </tr>--%>

    <div class="row">
            <div class="col-xs-12 col-sm-12">
                <strong><asp:Label ID="SchoolLabel"
                    text=<%# region4.escWeb.SiteVariables.ObjectProvider.SchoolNameCapitalized %>
                    AssociatedControlID="ddlSchool"
                    runat="server"></asp:Label>
                   
                </strong>(Select Other Organizations if you do not know your Campus.)
            </div>
            <div class="col-xs-12 col-sm-4">
                <asp:DropDownList ID="ddlSchool" runat="server" CssClass="smallestFont fullWidth" style="height: 28px" />
                <br />
                <br />
                <cc1:CascadingDropDown ID="CascadingDropDown3" runat="server" TargetControlID="ddlSchool"
                    ParentControlID="ddlDistrict" PromptText="Please select a campus..." ServicePath="~/services/locationservice.asmx"
                    ServiceMethod="GetSchoolsFromDistricts" Category="Room" />
            </div>     
        </div>

<%--        <tr>
            <td colspan="3" valign="top">
                <strong><asp:Label ID="SchoolLabel"
                    text=<%# region4.escWeb.SiteVariables.ObjectProvider.SchoolNameCapitalized %>
                    AssociatedControlID="ddlSchool"
                    runat="server"></asp:Label>
                   
                </strong>(Select Other Organizations if you do not know your Campus.)
            </td>
        </tr>
        <tr>
            <td colspan="3" valign="top">
                <asp:DropDownList ID="ddlSchool" runat="server" CssClass="formInput" />
                <br />
                <br />
                <cc1:CascadingDropDown ID="CascadingDropDown3" runat="server" TargetControlID="ddlSchool"
                    ParentControlID="ddlDistrict" PromptText="Please select a campus..." ServicePath="~/services/locationservice.asmx"
                    ServiceMethod="GetSchoolsFromDistricts" Category="Room" />
            </td>
        </tr>--%>

        <div class="row">
            <div class="col-xs-12 col-sm-12">
                <strong><asp:Label ID="PositionLabel"
                    text="Position:"
                    AssociatedControlID="ddlPosition"
                    runat="server"></asp:Label></strong>
            </div>
            <div class="col-xs-12 col-sm-4">
                <asp:DropDownList ID="ddlPosition" runat="server" CssClass="smallestFont fullWidth" style="height: 28px" >
                </asp:DropDownList>
                <br />
                <br />
            </div>     
        </div>

<%--        <tr>
            <td colspan="3" valign="top">
                <strong><asp:Label ID="PositionLabel"
                    text="Position:"
                    AssociatedControlID="ddlPosition"
                    runat="server"></asp:Label></strong>
            </td>
        </tr>
        <tr>
            <td colspan="3" valign="top">
                <asp:DropDownList ID="ddlPosition" runat="server" CssClass="formInput">
                </asp:DropDownList>
                <br />
            </td>
        </tr>--%>
             <!--<tr>
                <td  colspan="3" valign="top">
                    <strong>Required Accommodations:</strong>
                </td>
            </tr>
             <tr>
                <td style="height: 45px" valign="top" colspan="3">
                    <asp:TextBox ID="txtSpecialNeeds" runat="server" CssClass="formInput" Width="510px"/><br />
                </td>
            </tr>
        <tr> -->

     <div class="row">
        <div class="col-xs-12 col-sm-12">
            <asp:CheckBox runat="server" ID="checkboxCertify" AutoPostBack="True" Text="I certifiy the above information to be accurate"
                    OnCheckedChanged="OnCheckedChanged" />
        </div>
        <div class="col-xs-12 col-sm-4">
            &nbsp;
        </div>     
    </div>
    <br />

            <%--<td colspan="3" valign="top">
                <br />
                <br />
            </td>
        </tr>
        <tr>
            <td colspan="3" valign="top">
                <asp:CheckBox runat="server" ID="checkboxCertify" AutoPostBack="True" Text="I certify the above information to be accurate"
                    OnCheckedChanged="OnCheckedChanged" />
            </td>
        </tr>--%>
<%--        <tr>
            <td colspan="3" valign="top">
                <br />
                <br />
            </td>
        </tr>
    </table>--%>

    <asp:Button runat="server" ID="btnSaveUserProfile" Text="Continue" CssClass="formInput btn btn-R17Blue btn-lg" Style="width: 170px; font-size:small" OnClick="btnSaveUserProfileClick"
        Visible="false" />

<%--    <asp:Button runat="server" ID="btnSaveUserProfile" Text="Continue" OnClick="btnSaveUserProfileClick"
        Visible="false" />--%>
</asp:Content>
