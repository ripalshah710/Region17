<%@ Page Language="C#" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="shoebox_subscriptions_default"
    EnableEventValidation="false" MasterPageFile="~/MasterPage.master" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="mainBody">
    <a name="MainBody"></a>
    <span style="padding-right: 10px;">
        <button type="button" onclick="javascript:history.back()" class="formInput btn btn-R17Blue btn-lg" role="button" style="width: 130px; font-size: small" tooltip="Click here to go to previous page.">Previous</button></span>
    <br />
    <br />
    <style type="text/css">
        .RadListBox .rlbSelected {
            background-color: BLACK !important;
        }
    </style>
    <script language="javascript" type="text/javascript">
        // Global variables
        // -------------------------
        // Written by: Andrew Lieng
        // -------------------------

        $(document).ready(function () {
            if ($(window).width() > 415) {
                $('.horizontal').css("display", "visible");
                $('.vertical').css("display", "none");
                //$('.vertical').css("display", "visible");
                $('#HorizontalSave').css("display", "visible");
                $('#VerticalSave').css("display", "none");
                //$('#VerticalSave').css("display", "visible");
            }
            else {
                $('.vertical').css("display", "visible");
                $('.horizontal').css("display", "none");
                $('.hideList').css("display", "none");
                $('#HorizontalSave').css("display", "none");
                $('#VerticalSave').css("display", "visible");
            }
        });

        var http = createRequestObject();
        objElement = new Object();
        var responseUrl = "default.aspx";

        // Create an XMLHttpRequest for IE, Mozilla, and Safari browswers
        // -------------------------
        // Written by: Andrew Lieng
        // -------------------------
        function createRequestObject() {
            var ro;
            var browser = navigator.appName;
            if (browser == "Microsoft Internet Explorer") {
                ro = new ActiveXObject("Microsoft.XMLHTTP");
            } else {
                ro = new XMLHttpRequest();
            }
            return ro;
        }

        function SaveSubscription() {
            var dd1 = document.getElementById('ctl00_mainBody_ddSubscribed');
            var dd2 = document.getElementById('ctl00_mainBody_ddRecommended');

            var xmlString = '<?xml version="1.0" encoding="utf-8"?>';
            xmlString += '<xmlData>';

            xmlString += '<Item><Subscribed>' + dd1.options[dd1.selectedIndex].value + '</Subscribed></Item>';
            xmlString += '<Item><Recommended>' + dd2.options[dd2.selectedIndex].value + '</Recommended></Item>';

            var listBox2 = $find("<%= lsbSubscription2.ClientID %>");
            var items = listBox2.get_items();

            for (var i = 0; i < items.get_count(); i++) {
                xmlString += '<Item><Key>' + listBox2.getItem(i).get_value() + '</Key></Item>';
            }

            xmlString += '</xmlData>';

            http.open('post', responseUrl + '?action=SaveSubscription', true);
            //	http.setRequestHeader('Content-Type','text/xml');
            http.onreadystatechange = handleSaveSubscription;
            http.send(xmlString);
        }

        function SaveSubscriptionWithoutRecomanded() {
            var listBox2 = document.getElementById('ctl00_mainBody_lsbSubscription2');
            var dd1 = document.getElementById('ctl00_mainBody_ddSubscribed');
            var dd2 = document.getElementById('ctl00_mainBody_ddRecommended');

            var xmlString = '<?xml version="1.0" encoding="utf-8"?>';
            xmlString += '<xmlData>';

            xmlString += '<Item><Subscribed>' + dd1.options[dd1.selectedIndex].value + '</Subscribed></Item>';
            //    xmlString += '<Item><Recommended>' + dd2.options[dd2.selectedIndex].value + '</Recommended></Item>';

            //for loop
            for (var i = listBox2.options.length - 1; i > -1; i--) {
                xmlString += '<Item><Key>' + listBox2.options[i].value + '</Key></Item>'
            }

            xmlString += '</xmlData>';

            http.open('post', responseUrl + '?action=SaveSubscription', true);
            //	http.setRequestHeader('Content-Type','text/xml');
            http.onreadystatechange = handleSaveSubscription;
            http.send(xmlString);
        }


        function SaveSubscriptionMobile() {
            var dd1 = document.getElementById('ctl00_mainBody_ddSubscribed');
            //                var dd2 = document.getElementById('ctl00_mainBody_ddRecommended');

            var xmlString = '<?xml version="1.0" encoding="utf-8"?>';
            xmlString += '<xmlData>';

            xmlString += '<Item><Subscribed>' + dd1.options[dd1.selectedIndex].value + '</Subscribed></Item>';
            xmlString += '<Item><Recommended>1</Recommended></Item>';

            var listBox2 = $find("<%= lsbSubscription4.ClientID %>");
            var items = listBox2.get_items();

            for (var i = 0; i < items.get_count(); i++) {
                xmlString += '<Item><Key>' + listBox2.getItem(i).get_value() + '</Key></Item>';
            }

            xmlString += '</xmlData>';

            http.open('post', responseUrl + '?action=SaveSubscription', true);
            //	http.setRequestHeader('Content-Type','text/xml');
            http.onreadystatechange = handleSaveSubscription;
            http.send(xmlString);
        }

        function handleSaveSubscription() {

            // Loaded
            if (http.readyState == 4) {
                // OK
                if (http.status == 200) {
                    var response = http.responseXML;
                    document.getElementById('SaveStatus').innerText = response.getElementsByTagName('Response')[0].firstChild.data;

                    var response = http.responseText;
                    //	document.getElementById('SaveStatus').innerText = response;
                    setTimeout(ClearStatus, 3000);
                }
                else {
                    alert("Problem retrieving XML data from server.");
                }
            }
            else {
                document.getElementById('SaveStatus').innerText = 'Saving...';
                setTimeout(ClearStatus, 3000);
            }
        }

        function ClearStatus() {
            document.getElementById('SaveStatus').innerText = '';
        }

        function Cancel() {
            top.location.href = "../Default.aspx";
        }

        var listbox;
        function setListBoxDefaultTabIndex(sender) {
            listbox = sender;
            var $groupElement = $(listbox.get_element()).find('.rlbGroup');
            if ($groupElement.attr('tabindex') === undefined) {
                $groupElement.attr("tabindex", "0");
            }

        }

        function OnClientLoad(sender, args) {

            var listbox = $find("<%= lsbSubscription1.ClientID %>");
            if (listbox.get_items().get_count() == 0) {
                listbox.get_element().style.backgroundColor = "Red";
            }
        }

        function RestSubscriptionMobile() {
            location.reload();
        }
    </script>
    <span id="mobileHiddenText"><font color="white"> Hold down Shift and M key to bring into focus even when focus is lost and then navigate through arrow keys. Hit down or up arrow key to select the item. To move selected item to right side hold down Ctrl button and hit right arrow key and to move to left the selected item hold down Ctrl button and hit left arrow key. If arrow keys do not respond, please click Refresh button at the bottom of the page and try again. </font></span>
    <br />
    <br />
    <table border="0" cellpadding="4" cellspacing="0" width="100%">
        <tr>
            <td class="mainBody">Use the subscriptions area to request email notifications when new
                <%# region4.escWeb.SiteVariables.ObjectProvider.SessionPluralName %>
                of interest are made available.
            </td>
        </tr>
    </table>
    <div align="center">
        <table>
            <tr>
                <td class="mainBody smallestFont">Subjects
                </td>
                <td class="mainBody smallestFont hideList">Subscription List
                </td>
            </tr>

            <tr id="horizontal">
                <td class="mainBody smallestFont horizontal">
                    <telerik:RadListBox ID="lsbSubscription1" Width="330" Height="600" runat="Server"
                        SelectionMode="Multiple" CssClass="mainBody smallFont optionHeight" AllowTransfer="true" AllowTransferOnDoubleClick="true"
                        EnableDragAndDrop="true" TransferToID="lsbSubscription2" OnClientLoad="setListBoxDefaultTabIndex" RenderMode="Lightweight">
                        <KeyboardNavigationSettings CommandKey="Shift" FocusKey="M" />
                        <ButtonSettings AreaWidth="100" Position="Right" RenderButtonText="true" ShowTransferAll="false" />
                        <Localization ToLeft="Remove" ToRight="Add" AllToLeft="Remove All" AllToRight="Add All" />
                    </telerik:RadListBox>

                </td>
                <td class="mainBody smallestFont horizontal">
                    <telerik:RadListBox ID="lsbSubscription2" Width="330" Height="600" runat="Server"
                        SelectionMode="Multiple" CssClass="mainBody smallFont" AllowTransferOnDoubleClick="true"
                        EnableDragAndDrop="true" RenderMode="Lightweight" OnClientLoad="setListBoxDefaultTabIndex">
                        <KeyboardNavigationSettings CommandKey="Shift" FocusKey="M" />
                    </telerik:RadListBox>

                </td>
            </tr>

            <tr id="vertical">
                <td class="mainBody smallestFont vertical">
                    <telerik:RadListBox ID="lsbSubscription3" Width="330" Height="600" runat="Server"
                        SelectionMode="Multiple" CssClass="mainBody smallFont optionHeight" AllowTransfer="true" AllowTransferOnDoubleClick="true"
                        EnableDragAndDrop="true" TransferToID="lsbSubscription4" RenderMode="Lightweight" OnClientLoad="setListBoxDefaultTabIndex">
                        <KeyboardNavigationSettings CommandKey="Shift" FocusKey="M" />
                        <ButtonSettings AreaWidth="100" Position="Bottom" RenderButtonText="true" ShowTransferAll="false" />
                        <Localization ToTop="Remove" ToBottom="Add" AllToTop="Remove All" AllToBottom="Add All" />
                    </telerik:RadListBox>
                </td>
            </tr>
            <tr>
                <td class="mainBody smallestFont vertical">Subscription List
                    <telerik:RadListBox ID="lsbSubscription4" Width="330" Height="600" runat="Server"
                        SelectionMode="Multiple" CssClass="mainBody smallFont" AllowTransferOnDoubleClick="true"
                        EnableDragAndDrop="true" RenderMode="Lightweight" OnClientLoad="setListBoxDefaultTabIndex">
                        <KeyboardNavigationSettings CommandKey="Shift" FocusKey="M" />
                    </telerik:RadListBox>
                </td>
            </tr>

            <tr>
                <td height="10"></td>
            </tr>
            <tr>
                <td class="mainBody smallestFont" colspan="2">
                    <font color="red"><span id="SaveStatus"></span></font>
                </td>
            </tr>
            <tr>
                <td height="10"></td>
            </tr>
            <tr>
                <td class="mainBody" align="left" colspan="2">
                    <asp:Label ID="SubscriptionStatusLabel"
                        Text="Subscription status:&nbsp;&nbsp;"
                        AssociatedControlID="ddSubscribed"
                        runat="server"></asp:Label>
                    <asp:DropDownList ID="ddSubscribed" runat="Server"
                        CssClass="DropDownList">
                        <asp:ListItem Value="1">Subscribed</asp:ListItem>
                        <asp:ListItem Value="0">Unsubscribed</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td height="10"></td>
            </tr>
            <tr>
                <td class="mainBody" align="left" colspan="2">
                    <asp:Label ID="EventsByEmailLabel"
                        Text="Would you like to receive Recommended Events by email?&nbsp;&nbsp;"
                        AssociatedControlID="ddRecommended"
                        runat="server"></asp:Label>
                    <asp:DropDownList
                        ID="ddRecommended" runat="Server" CssClass="DropDownList smallestFont">
                        <asp:ListItem Value="1">Yes</asp:ListItem>
                        <asp:ListItem Value="0">No</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="mainBody" align="left" colspan="3">
                    <font size="1" color="dimgray"><i>(Recommended Events are based upon your prior participation.)</i></font>
                </td>
            </tr>
            <tr>
                <td height="10"></td>
            </tr>
            <tr>
                <td class="mainBody" align="left" colspan="2">
                    <input type="button" name="btnCancel" value="Cancel" class="formInput btn btn-R17Blue btn-lg" style="width: 140px; font-size:small" onclick="top.location.href = '../../Default.aspx';" tabindex="0" />
                    <input type="button" id="HorizontalSave" name="btnSave" value="Save Subscriptions" class="formInput btn btn-R17Blue btn-lg" style="width: 140px; font-size:small" onclick="SaveSubscription()" tabindex="0" />
                    <input type="button" id="VerticalSave" name="btnSaveMobile" value="Save Subscriptions" class="formInput btn btn-R17Blue btn-lg" style="width: 140px; font-size:small" onclick="SaveSubscriptionMobile()" tabindex="0" />
                    <input type="button" id="btnRefresh" name="btnRefresh" value="Refresh" class="formInput btn btn-R17Blue btn-lg" style="width: 140px; font-size: small" onclick="RestSubscriptionMobile()" tabindex="0" />
                    <%--<input type="button" name="btnCancel" value="Cancel" class="formInput btn btn-secondary btn-lg" style="width: 140px; font-size: small" onclick="top.location.href = '../../Default.aspx';" />
                    <input type="button" id="HorizontalSave" name="btnSave" value="Save Subscriptions" class="formInput btn btn-R17Blue btn-lg" style="width: 140px; font-size: small" onclick="SaveSubscription()" />
                    <input type="button" id="VerticalSave" name="btnSaveMobile" value="Save Subscriptions" class="formInput btn btn-R17Blue btn-lg" style="width: 140px; font-size: small" onclick="SaveSubscriptionMobile()" />--%>
                </td>
            </tr>
            <tr>
                <td class="mainBody" colspan="3"></td>
            </tr>
        </table>
    </div>
</asp:Content>
