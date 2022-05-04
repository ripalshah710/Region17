<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Activities_Default" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" lang="en" xml:lang="en">
<head id="Head1" runat="server">
    <title>Activities</title>
    <link type="text/css" rel="stylesheet" href="../lib/css/masterStyle.css" />
</head>
<body>
    <form id="form1" runat="server">
         <Telerik:RadScriptManager ID="RadScriptManager1"  EnableScriptCombine="false"  runat="server"></Telerik:RadScriptManager>
   <div style="width:750px; padding:10px">
     <script type="text/javascript" language="javascript">

         // allow this dialog box to post to itself

         // if this is a simulated dialog box (using window.open instead of
         // showModalDialog), initialize dialogArguments from the parent window
         if (typeof (dialogArguments) == "undefined")
             dialogArguments = opener.dialogArguments;

    </script>
    <%--<asp:HiddenField ID="strDate" runat="server" />--%>
         <script type="text/javascript" language="javascript">

             // initialize the OrganizationId hidden field with information from the parent page
             
//             var strDate = document.getElementById("ctl00_mainBody_strDate");
//             alert(dialogArguments.strDate);
//             strDate.value = dialogArguments.strDate;

        </script>
       
     <table border="0" width="750px">
        <tr>
            <td style="width:50%">
                <table border="0" width="100%">
                    <td valign="top">
                        <font size="2"><asp:Literal ID="litEmployeeName" runat="server"></asp:Literal></font>
                    </td>
                    <td valign="top">
                       <table border="0" width="55%">
                            <tr>
                                <td style="border:solid 2px #29507b; height:20px;" align="center">
                                    <asp:Literal ID="libDate" runat="server"></asp:Literal>
                                </td>
                            </tr>
                             <tr>
                                <td align="center">
                                    <asp:Literal ID="litHours" runat="server"></asp:Literal>
                                </td>
                            </tr>
                       </table>
                    </td>
                </table>
            </td>
            <td style="width:50%"></td>
        </tr>
        <tr>
            <td colspan="2"><br /><br /></td>
        </tr>
        <tr>
            <td colspan="2">
                 <asp:Panel ID="panelActivity" runat="server">
                    <div>
                      <telerik:RadGrid Skin="Vista" ID="grdActivity" runat="server" AllowSorting="false"
                        AutoGenerateColumns="False"  GridLines="None" ShowFooter="True" 
                        OnItemCommand="grdActivity_ItemCommand"
                        OnItemCreated="grdActivity_ItemCreated"
                        OnItemDataBound="grdActivity_ItemDataBound"
                        AlternatingItemStyle-BackColor="#CFDDF3" Width="100%"  DataKeyField="ObjId">
                        <MasterTableView DataKeyNames="ObjId" CommandItemDisplay="top">
                        <HeaderStyle Font-Bold="true" HorizontalAlign="Center"></HeaderStyle>
                        <FooterStyle Font-Bold="true" ForeColor="Black" HorizontalAlign="Center" />
                        <CommandItemSettings AddNewRecordText="Add New"/>
                            <RowIndicatorColumn Visible="False">
                                <HeaderStyle Width="20px" />
                            </RowIndicatorColumn>
                            <ExpandCollapseColumn Resizable="False" Visible="False">
                                <HeaderStyle Width="20px" />
                            </ExpandCollapseColumn>
                            <Columns>
                                <telerik:GridBoundColumn datafield="ObjId" headertext="ID" uniquename="ObjId"></telerik:GridBoundColumn>  
                                <telerik:GridBoundColumn datafield="Activity_Name" headertext="Activity" uniquename="Activity_Name"></telerik:GridBoundColumn>
                                <telerik:GridDateTimeColumn datafield="Service_Length" headertext="Length of Service" uniquename="Service_Length"></telerik:GridDateTimeColumn>
                                <telerik:GridTemplateColumn HeaderText="Select">
                                      <ItemTemplate>
                                       <asp:LinkButton ID="lnkSelect" runat="server" CommandName="Select"><img src="../lib/img/check.png" alt="Check" border="0"/></asp:LinkButton>
                                      </ItemTemplate>
                                </telerik:GridTemplateColumn>  
                            </Columns>
                            <DetailTables>
                            <telerik:GridTableView AutoGenerateColumns="false" Caption="Locations" AllowSorting="false"
                                DataSourceID="LocationList" Width="100%" PageSize="7">
                                <ParentTableRelation>
                                    <telerik:GridRelationFields DetailKeyField="ObjId" MasterKeyField="ObjId" />
                                </ParentTableRelation>
                                <Columns>
                                    <telerik:GridBoundColumn SortExpression="SpecificSite" HeaderText="Specific Site"
                                        HeaderButtonType="TextButton" DataField="room_name" Resizable="True" Reorderable="True" />
                                    <telerik:GridBoundColumn SortExpression="Client" HeaderText="Client" 
                                        DataField="site_name" Resizable="True" Reorderable="True" />
                                    <telerik:GridBoundColumn SortExpression="Region" HeaderText="Region" 
                                        DataField="org_name" Resizable="True" Reorderable="True" />
                                </Columns>
                            </telerik:GridTableView>
                        </DetailTables>

                        </MasterTableView>
                        <AlternatingItemStyle BackColor="#CFDDF3"></AlternatingItemStyle>
                        <PagerStyle Mode="NumericPages"></PagerStyle> 
                        <FilterMenu Skin="Vista" EnableTheming="True">
                        <CollapseAnimation Type="OutQuint" Duration="200"></CollapseAnimation>
                        </FilterMenu>
                    </telerik:RadGrid>
                </div>
            </asp:Panel>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="right">
                 <asp:Button ID="btnClose" runat="server" Text="CLOSE" Width="80px" Height="20px" BackColor="#48789b" Font-Size="xx-Small" ForeColor="#ffffff" BorderWidth="1px" OnClick="btnClose_Click"/> 
                 <asp:HiddenField ID="txtDate" runat="server" />
                 <asp:HiddenField ID="txtObjId" runat="server" />
            </td>
        </tr>
        <asp:Literal ID="UpdateParentPageScript" Visible="false" runat="server">
        <script type="text/javascript"  language="javascript"defer="defer">

            var objId = document.getElementById("txtObjId");
            var date = document.getElementById("txtDate")
            dialogArguments.UpdateParent(date.value, objId.value);
           
         
        </script>
        </asp:Literal>
        <asp:Literal ID="CloseDialogScript" Visible="false" runat="server">
        <script type="text/javascript" language="javascript" defer="defer">

            window.close();
 
        </script>
        </asp:Literal>
      
     </table>
   </div>
    <asp:SqlDataSource ID="LocationList" runat="server" 
        ConnectionString="" 
        SelectCommand="ItsLocationListGet" 
        SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:QueryStringParameter Name="ObjId" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    </form>
</body>
</html>
