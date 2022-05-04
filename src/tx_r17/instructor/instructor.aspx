<%@ Page Language="C#" AutoEventWireup="true" CodeFile="instructor.aspx.cs" Inherits="instructor_instructor" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml"
lang="en"
xml:lang="en">
<head id="Head1" runat="server">
    <title>Attendance</title>
</head>
<body>
    <form id="formAttendee" runat="server">
    <asp:ScriptManager ID="ScriptManager1" EnablePageMethods="true" runat="server">
    </asp:ScriptManager>
    Below is a list of registrants for Session
    <asp:Label ID="lblSessionId" runat="server"></asp:Label><br />
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadGrid1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="clrFilters">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="clrFilters"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadGrid ID="RadGrid1" runat="server" AllowFilteringByColumn="True" AllowSorting="true" Height="550px"
        Width="100%" AutoGenerateColumns="false" Skin="Outlook" CellPadding="0" GridLines="Horizontal"
        AllowPaging="True" PageSize="20"  PagerStyle-Position="Top" OnPageIndexChanged="RadGrid1_PageIndexChanged"
        OnPageSizeChanged="RadGrid1_PageSizeChanged" OnSortCommand="RadGrid1_SortCommand"
        OnUpdateCommand="RadGrid1_UpdateCommand">
        <GroupingSettings CaseSensitive="false" />
        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
        <AlternatingItemStyle BackColor="#CFDDF3" />
        <PagerStyle AlwaysVisible="true" Position="TopAndBottom"></PagerStyle>
        <MasterTableView Width="100%" DataKeyNames="ID" AllowMultiColumnSorting="false">
            <SortExpressions>
                <telerik:GridSortExpression FieldName="FullName" SortOrder="Ascending" />
            </SortExpressions>
            <Columns>
                <telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditCommandColumn"
                    Visible="false">
                    <HeaderStyle Width="40px" />
                </telerik:GridEditCommandColumn>
                <telerik:GridTemplateColumn HeaderText="Attended" AllowFiltering="false">
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckBoxAttended" AutoPostBack="true" runat="server" Checked='<%# Bind("Attended") %>'
                            OnCheckedChanged="Check_Clicked" />
                    </ItemTemplate>
                    <HeaderStyle Width="70px" />
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="FullName" UniqueName="FullName" HeaderText="Name"
                    AllowSorting="true" ShowSortIcon="true" AllowFiltering="true" FilterControlWidth="200px"
                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                    Groupable="false" Reorderable="false" Visible="true">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="PrimaryEmail" UniqueName="PrimaryEmail" HeaderText="Email"
                    AllowSorting="true" ShowSortIcon="true" AllowFiltering="true" FilterControlWidth="200px"
                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                    Groupable="false" Reorderable="false" Visible="true">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="UserSiteRoomDisplay" UniqueName="UserSiteRoomDisplay"
                    HeaderText="Location" AllowSorting="false" AllowFiltering="true" FilterControlWidth="200px"
                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                    Groupable="false" Reorderable="false" Visible="true">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="OnlineExpirationDateDisplay" UniqueName="OnlineExpirationDateDisplay"
                    AllowFiltering="false" HeaderText="Expiration Date" AllowSorting="false" ShowFilterIcon="false"
                    Groupable="false" Reorderable="false" Visible="false">
                    <HeaderStyle Width="140px" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="AttendedTimeStampDisplay" UniqueName="AttendedTimeStampDisplay"
                    AllowFiltering="false" HeaderText="Attendance Date" AllowSorting="false" ShowFilterIcon="false"
                    Groupable="false" Reorderable="false" Visible="false">
                    <HeaderStyle Width="140px" />
                </telerik:GridBoundColumn>
            </Columns>
            <EditFormSettings UserControlName="OnlineAttendeeEdit.ascx" EditFormType="WebUserControl">
                <EditColumn UniqueName="EditCommandColumn1">
                </EditColumn>
            </EditFormSettings>
        </MasterTableView>
        <ClientSettings>
            <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="True" ScrollHeight="500px">
            </Scrolling>
        </ClientSettings>
    </telerik:RadGrid>
    <br />
    <hr />
    <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save" />
    </form>
</body>
</html>
