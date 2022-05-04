<%@ Page Language="C#" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="instructor_default"
    MasterPageFile="~/masterpage.master" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="mainBody"><a name="MainBody"></a>
    <span style="padding-right: 10px;"><button type="button" onclick="javascript:history.back()" class="formInput btn btn-R17Blue btn-lg" role="button" style="width: 130px; font-size:small" ToolTip="Click here to go to previous page.">Previous</button></span>
    <br /><br />
    <style>
        .rtsSelected, .rtsSelected span {
            background: url(../Images/btnUpdate.jpg) no-repeat 0 100% !important;
            background-color: dimgray !important;
            text-align: center;
            color: white;
        }

        .RadTabStrip_Telerik li a.slected {
            background: url(../Images/btnUpdate.jpg) no-repeat 0 100% !important;
            background-color: dimgray !important;
            text-align: center;
            color: white;
        }
    </style>
    <script type="text/javascript" language="javascript">
        function OnTabSelected(sender, args) {
            var hidField = document.getElementById("<%=hiddenFieldTabValue.ClientID%>");
            hidField.value = args.get_tab().get_value();
        }
    </script>
    <font color="white">Hold down Shift and M key to bring into focus anytime even in the event focus is lost and then navigate through arrow keys and hit enter.</font>
    <br /><br />
    <telerik:RadTabStrip ID="radTabStrip" AutoPostBack="true" runat="server" OnClientTabSelected="OnTabSelected" RenderMode="Lightweight">
        <KeyboardNavigationSettings CommandKey="Shift" FocusKey="M" />
        <Tabs>
            <telerik:RadTab runat="server" Text="Upcoming Events" Value="FutureEvents" Selected="true">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Past Events" Value="PastEvents">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Online Event" Value="OnlineEvents">
            </telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <asp:PlaceHolder runat="server" ID="pPlaceHolder" />
    <asp:HiddenField ID="hiddenFieldTabValue" runat="server" Value="FutureEvents" />
    <script language="javascript" type="text/javascript">
        function getAttendee(param) {
            window.open('instructor.aspx?session_id=' + param, '_blank', 'width=900, height=650, resizable=1');
        }
    </script>
</asp:Content>
