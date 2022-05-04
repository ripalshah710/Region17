<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Search.ascx.cs" Inherits="Search" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<a name="MainBody"></a>
    <span style="padding-right: 10px;">
        <button type="button" onclick="javascript:history.back()" class="formInput btn btn-R17Blue btn-lg" role="button" style="width: 130px; font-size: small" tooltip="Click here to go to previous page.">Previous</button></span>
    <br />
    <br />
<style type="text/css">
    .RadGrid A:hover
    {
        color: black;
    }
</style>

<label for="ctl00_mainBody_Search1_grdSearch_ctl00_ctl02_ctl00_PageSizeComboBox_Input" style="display: none;">formLabel</label>
<label for="ctl00_mainBody_Search1_grdSearch_ctl00_ctl03_ctl01_PageSizeComboBox_Input" style="display: none;">formLabel</label>


<script language="javascript" type="text/javascript">
    function OnClientSelectedIndexChanged(sender, eventArgs) {
        var item = eventArgs.get_item();
        var combo = $find("<%= ddItems.ClientID %>");
        var s, e, len;

        if (item.get_value() == "9000") {
            s = getIndex('9000');
            e = getIndex('9001');
            len = e - s;
            check_unchecl_all(combo, item, s, e);
        }
        else if (item.get_value() == "9001") {
            s = getIndex('9001');
            e = getIndex('9002');
            check_unchecl_all(combo, item, s, e);
        }
        else if (item.get_value() == "9002") {
            s = getIndex('9002');
            e = getIndex('9003');
            check_unchecl_all(combo, item, s, e);
        }
        else if (item.get_value() == "9003") {
            s = getIndex('9003');
            e = getIndex('9004');
            check_unchecl_all(combo, item, s, e);
        }
        else if (item.get_value() == "9004") {
            s = getIndex('9004');
            e = combo.get_items().get_count();
            check_unchecl_all(combo, item, s, e);

        }
    }

    function getIndex(i) {
        var combo = $find("<%= ddItems.ClientID %>");
        var items = combo.get_items();
      
        var itemsCount = items.get_count();
      
        for (var itemIndex = 0; itemIndex < itemsCount; itemIndex++) {
            var item = combo.get_items().getItem(itemIndex).get_value();
            if (item == i) { return itemIndex;}
        }
    }

    function check_unchecl_all(combo, item, s, e)
    {
            if (item.get_checked()) {
                for (var i = s; i < e - 1; i++) 
                    combo.get_items().getItem(i + 1).set_checked(true);
            }
            else {
                for (var i = s; i < e - 1; i++)
                    combo.get_items().getItem(i + 1).set_checked(false);
            }
    }

</script>

<div class="container">
    <div class="row" style="width: 90%">
        <div id="searchCriteriaDesktop">
            <div class="row">
                &nbsp;&nbsp;&nbsp;&nbsp;Search:
            </div>
            <div class="row">
                <div>
                    <label for="ctl00_mainBody_Search1_txtSearch" style="color:white; display:none;">Search</label><br />
                    <asp:TextBox ID="txtSearch" runat="server" Width="330px" Style="margin-left: 15px !important;" />&nbsp;&nbsp;
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-6 col-sm-6">
                    <div>
                        <asp:Button id="btnSearch" Text="Search" runat="server" OnClick="btnSearch_Click" class="formInput btn btn-R17Blue btn-lg" style="width: 140px; height:30px; font-size:small" />
                    </div>
                </div>

                <div class="col-3 col-sm-4">
                    <asp:Button id="btnReset" Text="Reset" runat="server" OnClick="btnReset_Click" class="formInput btn btn-secondary btn-lg" style="width: 140px; height:30px; font-size:small" />

                </div>
            </div>
        </div>
    </div>
</div>

<br />
<div class="container">
    <div class="row">
        <div class="col-12 col-sm-2">
            <asp:CheckBox ID="chkFF" runat="server" Text="&nbsp;&nbsp;Face-To-Face" Checked="true" Font-Size="X-Small" />
        </div>
        <div class="col-12 col-sm-2">
            <asp:CheckBox ID="chkVideo" runat="server" Text="&nbsp;&nbsp;Online" Checked="true" Font-Size="X-Small" />
        </div>
        <div class="col-12 col-sm-2">
            <asp:CheckBox ID="chkFree" runat="server" Text="&nbsp;&nbsp;Free" Font-Size="X-Small" />
        </div>
        <div class="col-12 col-sm-2">
            <asp:CheckBox ID="chkWeekend" runat="server" Text="&nbsp;&nbsp;Weekend" Font-Size="X-Small" />
        </div>
    </div>
    <div class="row">
        <div class="col-12 col-sm-3"><small><b>T-TESS/T-PESS</b></small></div>
    </div>
    <div class="row">

        <div class="col-4 col-sm-3">
            <telerik:RadComboBox CssClass="dropdownWidth" ID="ddItems" runat="server" ToolTip="Please domains/dimensions/standards" CheckBoxes="true" CheckedItemsTexts="DisplayAllInInput" Width="320px" OnClientItemChecked="OnClientSelectedIndexChanged" RenderMode="Auto">
                <CollapseAnimation Type="OutQuint" Duration="200"></CollapseAnimation>
            </telerik:RadComboBox>
        </div>
    </div>
    <br />
    <label for="formLabel" style="display: none;">formLabel</label>
    <label for="ctl00_mainBody_Search1_ddItems_Input" style="display: none;">formLabel</label>
    <div id="searchGridDeskTop">

            <telerik:RadGrid ID="grdSearch" runat="server" AllowSorting="true" AutoGenerateColumns="false"
                Skin="Outlook" CellPadding="0" Width="100%" GridLines="None" AllowPaging="True"
                PagerStyle-Position="TopAndBottom" BorderStyle="Solid" BorderColor="#0066FF">
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                <AlternatingItemStyle BackColor="#CFDDF3" />
               <PagerStyle ChangePageSizeButtonToolTip="Change Page Size" 
                    ChangePageSizeComboBoxTableSummary="The table which holds the composite controls for the ChangePageSize RadComboBox control."
                    ChangePageSizeComboBoxToolTip="Change Page Size"
                    ChangePageSizeTextBoxToolTip="Change Page Size" GoToPageButtonToolTip="Go To Page"
                    GoToPageTextBoxToolTip="Go To Page" />
                <MasterTableView Width="100%" DataKeyNames="ID" Font-Size="X-Small" AllowMultiColumnSorting="false">
                    <SortExpressions>
                        <telerik:GridSortExpression FieldName="StartDate" SortOrder="Ascending" />
                    </SortExpressions>
                    <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                    <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column">
                    </RowIndicatorColumn>
                    <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column">
                    </ExpandCollapseColumn>
                    <Columns>
                        <telerik:GridHyperLinkColumn DataNavigateUrlFields="URL" DataTextField="ID" UniqueName="ID"
                            HeaderText="ID" DataNavigateUrlFormatString="{0}">
                            <HeaderStyle Width="6%" Font-Size="Small" />
                        </telerik:GridHyperLinkColumn>
                        <telerik:GridBoundColumn DataField="StartDate" DataFormatString="{0:d}" UniqueName="StartDate"
                            HeaderText="Start Date" AllowSorting="true" ShowSortIcon="true" AllowFiltering="false"
                            Groupable="false" Reorderable="false" Visible="true">
                            <HeaderStyle Width="10%" Font-Size="Small" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Title" UniqueName="Title" HeaderText="Title"
                            AllowSorting="true" ShowSortIcon="true" AllowFiltering="false" Groupable="false"
                            Reorderable="false" Visible="true">
                            <HeaderStyle Width="40%" Font-Size="Small" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="DescriptionDisplay" UniqueName="DescriptionDisplay"
                            HeaderText="Subtitle" AllowSorting="false" AllowFiltering="false" Groupable="false"
                            Reorderable="false" Visible="true">
                            <HeaderStyle Width="30%" Font-Size="Small" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="EventTypeDisplay" UniqueName="EventTypeDisplay" HeaderText="Type"
                            AllowSorting="true" ShowSortIcon="true" AllowFiltering="false" Groupable="false"
                            Reorderable="true" Visible="true">
                            <HeaderStyle Width="14%" Font-Size="Small" />
                        </telerik:GridBoundColumn>
                    </Columns>
                    <EditFormSettings>
                        <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                        </EditColumn>
                    </EditFormSettings>
                </MasterTableView>
                <FilterMenu EnableImageSprites="False">
                </FilterMenu>
            </telerik:RadGrid>
            <asp:Label ID="lbError" runat="server" Visible="false" ForeColor="Red"></asp:Label>
        </div>

    <div id="searchGridMobile">

             <telerik:RadGrid ID="grdSearchMobile" Label="Page Size" runat="server" AllowSorting="false" AutoGenerateColumns="false"
                Skin="Outlook" CellPadding="0" Width="100%" GridLines="None" AllowPaging="false"
                PagerStyle-Position="TopAndBottom" BorderStyle="Solid" BorderColor="#0066FF" RenderMode="Auto">
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                <AlternatingItemStyle BackColor="#CFDDF3" />
                <PagerStyle ChangePageSizeButtonToolTip="Change Page Size" 
                    ChangePageSizeComboBoxTableSummary="The table which holds the composite controls for the ChangePageSize RadComboBox control."
                    ChangePageSizeComboBoxToolTip="Change Page Size"
                    ChangePageSizeTextBoxToolTip="Change Page Size" GoToPageButtonToolTip="Go To Page"
                    GoToPageTextBoxToolTip="Go To Page" />
                <MasterTableView Width="100%" DataKeyNames="ID" Font-Size="X-Small" AllowMultiColumnSorting="false">
                    <SortExpressions>
                        <telerik:GridSortExpression FieldName="StartDate" SortOrder="Ascending" />
                    </SortExpressions>
                    <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                    <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column">
                    </RowIndicatorColumn>
                    <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column">
                    </ExpandCollapseColumn>
                    <Columns>
                         <telerik:GridBoundColumn DataField="SearchResult" UniqueName="SearchResult" HeaderText="SearchResult"
                            AllowSorting="true" ShowSortIcon="true" AllowFiltering="false" Groupable="false"
                            Reorderable="false" Visible="true">
                            <HeaderStyle Width="3%" Font-Size="Small" />
                        </telerik:GridBoundColumn>
                    </Columns>
                    <EditFormSettings>
                        <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                        </EditColumn>
                    </EditFormSettings>
                </MasterTableView>
                <FilterMenu EnableImageSprites="False">
                </FilterMenu>
            </telerik:RadGrid>
        <asp:Label ID="Label1" runat="server" Visible="false" ForeColor="Red"></asp:Label>
    </div>
</div>