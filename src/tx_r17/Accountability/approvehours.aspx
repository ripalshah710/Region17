<%@ Page Language="C#" AutoEventWireup="true" CodeFile="approvehours.aspx.cs" EnableViewState="true" EnableViewStateMac="true"  MasterPageFile="~/MasterPage.master" Inherits="Accountability_approvehours" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="mainBody"><a name="MainBody"></a>

<font size="2">Please select an employee from the below value list</font><br /><br />

<asp:DropDownList ID="ddlmonth" runat="server" Width="200px" enableviewstate = "true">
</asp:DropDownList>
<asp:DropDownList ID="ddlyear" runat="server" Width="200px" enableviewstate = "true">
</asp:DropDownList>
<asp:DropDownList ID="ddlemployees" runat="server" Width="200px" enableviewstate = "true">
</asp:DropDownList>
<asp:Button ID="getEmployees" runat="server" OnClick="getemployees_Click" Text="View" />
<br />
<br />

<div>
    <telerik:RadGrid Skin="Outlook" ID="grdHours" runat="server" AllowSorting="false"
                        AutoGenerateColumns="False"  GridLines="None" ShowFooter="True" 
                         AlternatingItemStyle-BackColor="#CFDDF3" Width="100%"  DataKeyField="ObjId">
                         <MasterTableView DataKeyNames="ObjId" >
                        <SortExpressions>
                        <telerik:GridSortExpression FieldName="DateofService" SortOrder="Ascending" />
                        </SortExpressions>
                         <Columns>
                                <telerik:GridBoundColumn datafield="ObjId" headertext="ID" uniquename="ObjId"></telerik:GridBoundColumn>  
                                <telerik:GridDateTimeColumn datafield="DateofService" headertext="Date" uniquename="DateofService"></telerik:GridDateTimeColumn>
                                <telerik:GridBoundColumn datafield="Activity" headertext="Activity" uniquename="Activity"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn datafield="Hours" headertext="Hours" uniquename="Hours"></telerik:GridBoundColumn>                                 
                            </Columns>   
                        </MasterTableView>                     
    </telerik:RadGrid>

</div>

<br /><br />
<asp:Panel ID="PanelButtons" runat="server" Visible="false">
<font size="2">Please enter your comments to deny the hours and the same will be notified to the employee via email.</font><br /><br />
<asp:Label ID="lbloverride" runat="server" Text="This employee is approved by the supervisor." ForeColor="Red" Visible="false" />
<asp:TextBox ID="txtdenycomments" runat="server" Width="300px" Height="100" TextMode="MultiLine" Visible="false" /><br /><br /><br />
<asp:TextBox ID="txtcfocomments" runat="server" Width="300px" Height="100" TextMode="MultiLine" Visible="false" /><br /><br /><br />

<asp:Button ID="Btn_Approve" runat="server" OnClick="Btn_Approve_Click" Text="Approve" Visible="false" />
&nbsp;&nbsp;&nbsp;<asp:Button ID="Btn_Deny" runat="server" OnClick="Btn_Deny_Click" Text="Deny" />
&nbsp;&nbsp;&nbsp;<asp:Button ID="Btn_Override" runat="server" OnClick="BtnOverride_Click" Text="Force Approval" Visible="false" /><br /><br />

</asp:Panel>





</asp:Content>
