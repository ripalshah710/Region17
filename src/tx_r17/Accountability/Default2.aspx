<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default2.aspx.cs" Inherits="Accountability_Default"  EnableViewStateMac="false"%>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<html lang="en-us" xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
  <link type="text/css" href="../lib/css/jquery-ui-1.8.20.custom.css" rel="Stylesheet" />	
 <script type="text/javascript" src="../lib/js/jquery-1.7.2.min.js"></script>
 <script type="text/javascript" src="../lib/js/jquery-ui-1.8.20.custom.min.js"></script>
 
 <script type="text/javascript" language="javascript">

     function OpenWindow(strDate) {

         var args = new Object;

         args.strDate = strDate;
         args.UpdateParent = UpdateParent;

         dialogArguments = args;
         ShowDialog("../activities/default.aspx?date=" + args.strDate, args, 800, 550, false);
     }

     function ShowDialog(strUrl, args, cx, cy, fScroll) {
         // display a dialog box with URL <strUrl>, arguments <args>, width <cx>, height <cy>,
         // scrollbars if <fScroll>; this can be done using either showModalDialog() or
         // window.open(): the former has better modal behavior; the latter allows selection
         // within the window
         var useShowModalDialog = false;
         var strScroll = fScroll ? "yes" : "no";
         if (useShowModalDialog) {
             showModalDialog(strUrl, args,
                 "dialogWidth: " + cx + "px; dialogHeight: " + cy +
                 "px; center: yes; resizable: yes; scroll: " + strScroll + ";");
         }
         else {
             dialogArguments = args; // global variable accessed by dialog
             var x = Math.max(0, (screen.width - cx) / 2);
             var y = Math.max(0, (screen.height - cy) / 2);
             window.open(strUrl, "_blank", "left=" + x + ",top=" + y +
                 ",width=" + cx + ",height=" + cy +
                 ",location=no,menubar=no,scrollbars=" + strScroll +
                 ",status=no,toolbar=no,resizable=yes");
         }
     }
     function UpdateParent(strDate, obj_id) {

         window.location = "../accountability/default2.aspx?date=" + strDate + "&obj_id=" + obj_id + "&detail=1";
     }

 </script>
 
 <script type="text/javascript">
     function pageLoad() {
         $("[id$=txtDate]").datepicker({

             showOn: 'button',

             buttonImageOnly: true,

             buttonImage: '../lib/img/calendar.gif'

         });
     }


     function disable() {

         $("[id$=txtDate]").datepicker({

             showOn: 'off'
         });

         $('input[type="button"]').prop('disabled', true);

     }

     function disableButtons() {

         $("[id$=txtDate]").datepicker({

             showOn: 'off'
         });

         $('input[type="button"]').prop('disabled', true);

     }

     function enableButtons() {

         $("[id$=txtDate]").datepicker({

             showOn: 'button',

             buttonImageOnly: true,

             buttonImage: '../lib/img/calendar.gif'
         });

         $('input[type="button"]').prop('enabled', true);

     }

 </script>
 
 <script type="text/javascript">

     function addcontrols() {



         //alert(MemberFeeList.ClientID)

     }
     function AddMultiListTextBox(dropdown, textbox, listbox, hiddenValue, msg) {
         var ddControl = document.getElementById(dropdown);
         var lbControl = document.getElementById(listbox);
         var tbControl = document.getElementById(textbox);

         var valueControl = document.getElementById(hiddenValue);

         if (ddControl.selectedIndex < 0) {
             alert(msg);
             return;
         }

         var m_Text = ddControl[ddControl.selectedIndex].text;
         var m_Value = ddControl[ddControl.selectedIndex].value;

         if (m_Value.length == 0) {
             alert(msg);
             return;
         }

         if (tbControl.value.length == 0) {
             alert('Please provide value for the amount field.');
             return;
         }
         else if (isNaN(tbControl.value)) {
             alert('Please input numeric value only.');
             return;
         }
         else if (Number(tbControl.value) < 0) {
             alert('Please input a positive number.');
             return;
         }

         var option = new Option(m_Text + ' | ' + tbControl.value, m_Value + ':' + tbControl.value);

         lbControl[lbControl.length] = option;
         ddControl[ddControl.selectedIndex] = null;
         ddControl.selectedIndex = 0;

         if (valueControl.value.length > 0)
             valueControl.value = valueControl.value + ',' + m_Value + ':' + tbControl.value;
         else
             valueControl.value = m_Value + ':' + tbControl.value;

     }

     //     function OnRemove(lbControl, dropdown, textbox) {

     //         if (lbControl.length > 0 && lbControl.selectedIndex >= 0) {
     //             var ddControl = document.getElementById(dropdown);
     //             var tbControl = document.getElementById(textbox);

     //             var m_Text = lbControl[lbControl.selectedIndex].text;
     //             var m_Value = lbControl[lbControl.selectedIndex].value;

     //             var option = new Option(m_Text.split(' | ')[0], m_Value.split(':')[0]);
     //             ddControl[ddControl.length] = option;
     //             lbControl[lbControl.selectedIndex] = null;

     //             var text = '';
     //             for (var n = 0; n < lbControl.length; n++) {
     //                 if (n == 0)
     //                     text = lbControl[n].value;
     //                 else
     //                     text = text + ',' + lbControl[n].value;
     //             }
     //             tbControl.value = text;
     //         }
     //     }

     function AddMultiSelect(dropdown, listbox, hiddenValue, msg) {

         var ddControl = document.getElementById(dropdown);
         var lbControl = document.getElementById(listbox);
         var valueControl = document.getElementById(hiddenValue);

         if (ddControl.selectedIndex < 1) {
             alert(msg);
             return;
         }

         var m_Text = ddControl[ddControl.selectedIndex].text;
         var m_Value = ddControl[ddControl.selectedIndex].value;

         if (m_Value.length == 0) {
             alert(msg);
             return;
         }

         var option = new Option(m_Text);

         if (valueControl.value.length > 0)
             valueControl.value = valueControl.value + ',' + m_Value;
         else
             valueControl.value = m_Value;

         lbControl[lbControl.length] = option;
         ddControl[ddControl.selectedIndex] = null;

         ddControl.selectedIndex = 0;

     }

     function OnRemoveMulti(lbControl, dropdown, textbox) {
         var ddControl = document.getElementById(dropdown);
         var tbControl = document.getElementById(textbox);
         var lbControl = document.getElementById(lbControl);

         if (lbControl.length > 0 && lbControl.selectedIndex >= 0) {
             var m_Text = lbControl[lbControl.selectedIndex].text;
             var m_Value = lbControl[lbControl.selectedIndex].value;

             var option = new Option(m_Text, m_Value);
             ddControl[ddControl.length] = option;
             lbControl[lbControl.selectedIndex] = null;

             var text = '';
             for (var n = 0; n < lbControl.length; n++) {
                 if (n == 0)
                     text = lbControl[n].value;
                 else
                     text = text + ',' + lbControl[n].value;
             }
             tbControl.value = text;
         }
     }

 </script>
 
 <script type="text/javascript">
     /**
     * DHTML date validation script. Courtesy of SmartWebby.com (http://www.smartwebby.com/dhtml/datevalidation.asp)
     */
     // Declaring valid date character, minimum year and maximum year
     var dtCh = "/";
     var minYear = 1900;
     var maxYear = 2100;

     function isInteger(s) {
         var i;
         for (i = 0; i < s.length; i++) {
             // Check that current character is number.
             var c = s.charAt(i);
             if (((c < "0") || (c > "9"))) return false;
         }
         // All characters are numbers.
         return true;
     }

     function stripCharsInBag(s, bag) {
         var i;
         var returnString = "";
         // Search through string's characters one by one.
         // If character is not in bag, append to returnString.
         for (i = 0; i < s.length; i++) {
             var c = s.charAt(i);
             if (bag.indexOf(c) == -1) returnString += c;
         }
         return returnString;
     }

     function daysInFebruary(year) {
         // February has 29 days in any year evenly divisible by four,
         // EXCEPT for centurial years which are not also divisible by 400.
         return (((year % 4 == 0) && ((!(year % 100 == 0)) || (year % 400 == 0))) ? 29 : 28);
     }
     function DaysArray(n) {
         for (var i = 1; i <= n; i++) {
             this[i] = 31
             if (i == 4 || i == 6 || i == 9 || i == 11) { this[i] = 30 }
             if (i == 2) { this[i] = 29 }
         }
         return this
     }

     function isDate(dtStr) {
         var daysInMonth = DaysArray(12)
         var pos1 = dtStr.indexOf(dtCh)
         var pos2 = dtStr.indexOf(dtCh, pos1 + 1)
         var strMonth = dtStr.substring(0, pos1)
         var strDay = dtStr.substring(pos1 + 1, pos2)
         var strYear = dtStr.substring(pos2 + 1)
         strYr = strYear
         if (strDay.charAt(0) == "0" && strDay.length > 1) strDay = strDay.substring(1)
         if (strMonth.charAt(0) == "0" && strMonth.length > 1) strMonth = strMonth.substring(1)
         for (var i = 1; i <= 3; i++) {
             if (strYr.charAt(0) == "0" && strYr.length > 1) strYr = strYr.substring(1)
         }
         month = parseInt(strMonth)
         day = parseInt(strDay)
         year = parseInt(strYr)
         if (pos1 == -1 || pos2 == -1) {
             alert("The date format should be : mm/dd/yyyy")
             return false
         }
         if (strMonth.length < 1 || month < 1 || month > 12) {
             alert("Please enter a valid month")
             return false
         }
         if (strDay.length < 1 || day < 1 || day > 31 || (month == 2 && day > daysInFebruary(year)) || day > daysInMonth[month]) {
             alert("Please enter a valid day")
             return false
         }
         if (strYear.length != 4 || year == 0 || year < minYear || year > maxYear) {
             alert("Please enter a valid 4 digit year between " + minYear + " and " + maxYear)
             return false
         }
         if (dtStr.indexOf(dtCh, pos2 + 1) != -1 || isInteger(stripCharsInBag(dtStr, dtCh)) == false) {
             alert("Please enter a valid date")
             return false
         }
         return true
     }
     function fnCertifyClick() {

         var retVal = confirm("I certify that the time entered for this month is accurate and correct to the best of my knowledge.” By clicking 'OK', you are electronically signing this declaration.");

         if (retVal) {
             //alert(retVal);
             this.document.getElementById("CertifyOK").value = "TRUE";
             //alert(this.document.getElementById("CertifyOK").value);
             document.forms[0].submit();
             //window.location.href("default2.aspx");


         }
     }
     function ValidateForm() {
         var dt = document.getElementById("txtDate")
         if (isDate(dt.value) == false) {
             dt.focus()
             return false
         }
         return true
     }

</script>

 <script type="text/javascript" language="javascript">

     function stopRKey(evt) {
         var evt = (evt) ? evt : ((event) ? event : null);
         var node = (evt.target) ? evt.target : ((evt.srcElement) ? evt.srcElement : null);
         if ((evt.keyCode == 13) && (node.type == "text")) { return false; }
     }




  </script>

 <link type="text/css" rel="stylesheet" href="../lib/css/masterStyle.css" />
    
<title>escWorks</title>
</head>
<body style="width: 100%; background-color:#e9e9e9; font-family:">
<form id="Form1" runat="server">
<asp:HiddenField  id="CertifyOK" value ="FALSE" runat="server"/>
 <telerik:RadScriptManager ID="RadScriptManager1"  EnableScriptCombine="false"  runat="server"></telerik:RadScriptManager>
 <div style="width:100%; text-align:center">

 <table id="Table1" style="margin-left: auto; width: 950; margin-right: auto; margin-top: 20px; border-collapse: collapse; border-right: #48789b 1px solid; border-top: #48789b 1px solid; border-left: #48789b 1px solid; border-bottom: #48789b 1px solid;background-color:#ffffff; height:500px" runat="server">
 <tr>
    <td style="background-color:#ffffff; height:60px; border-bottom:solid 1px #667a95">
        <table width="100%" border="0">
            <tr>
                <td><img src='<%= ResolveUrl("~/lib/img/escWorks_logo.gif")%>' alt="escWorks Logo" height="50px"/></td>
                <td  valign="bottom" align="right">
                    <asp:LinkButton ID="lnkHome" runat="server" OnClick="lnkHome_Click">HOME</asp:LinkButton>&nbsp;&nbsp;
                </td>
            </tr>
            
        </table>
    </td>     
 </tr>
 <tr>
 <td  valign="top">
   <div style="padding:5px">
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
    <table id="Table2"  runat="server" width="100%">
        <tr>
           <td style="width:25%">
                <asp:Calendar ID="calActivity" runat="server" Width="250px" Height="200px" DayStyle-BorderColor="#cccccc" 
                  DayStyle-BorderWidth="1px" DayHeaderStyle-BorderWidth="1px" 
                  DayHeaderStyle-BorderColor="#cccccc"  FirstDayOfWeek="Monday" 
                  TitleStyle-BorderColor="#cccccc" TitleStyle-BorderWidth="1px" 
                  TitleStyle-BackColor="#48789b" WeekendDayStyle-BackColor="#c0c0c0" 
                  TitleStyle-Font-Bold="true" TitleStyle-ForeColor="#ffffff"
                  NextPrevStyle-ForeColor="#ffffff" DayStyle-Font-Underline="false"
                  DayStyle-Font-Size="xx-small" DayHeaderStyle-Font-Size="x-Small"
                  OnDayRender="calActivity_DayRender"  
                  OnVisibleMonthChanged = "calActivity_VisibleMonthChanged"  
                  ></asp:Calendar>
            </td>
           <td style="width:25%"  align="center">
                <table width="100%">
                    <tr>
                        <td align="center" colspan="2">
                            <font size="2" color="#29507b">WELCOME  - </font><asp:Literal ID="litEmployeeName" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr><td colspan="2">&nbsp;</td></tr>
                    <tr>
                    
                      <td align="center" colspan="2">
                        <table width="250px">
                             <tr>
                             <td style="border:solid 2px #29507b; height:20px; width:75px;">
                                <asp:Literal ID="litDate" runat="server"></asp:Literal>
                             </td>
                             <td style="border:solid 2px #29507b; height:20px; width:75px;"><b>Month</b></td>
                             </tr>
                             <tr>
                                <td>
                                    <asp:Literal ID="litDayHours" runat="server"></asp:Literal>
                                </td>
                                <td>
                                    <asp:Literal ID="litThisWeekHours" runat="server"></asp:Literal>
                                </td>
                            </tr>
                             <tr><td colspan="2"><br /></td></tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Literal ID="litMessage" runat="server"></asp:Literal>
                                </td>
                            </tr>
                        </table>
                      </td>
                    </tr>
                   
                </table>
            </td>
           <td style="width:30%"  align="center" >
               <table width="100%">
               <tr>
               <td align="center">
               <asp:Panel ID="Preqhours" Visible="false" runat="server">
               Required Hours : <asp:Literal ID="litreqhours" runat="server"></asp:Literal>
               </asp:Panel>
               </td>
               </tr>
               <tr>
                   <td>
                <asp:Label ID="lblcertifymsg" runat="Server" />
                </td></tr>
               <tr><td><br /><br /></td></tr>
               <tr>
               <td align="center">
                <!--<asp:Button ID="btnCertify" runat="server" Text="CERTIFY" Width="120px" Height="20px" BackColor="#48789b" Font-Size="xx-Small" ForeColor="#ffffff" BorderWidth="1px" Enabled="false"/> !-->
              <asp:Panel id="certifyPanel" Visible="false" runat="server">
               <input type="button" id="btnCertify" name="MonthlyCertify" value ="CERTIFY" onclick="fnCertifyClick()" style="Width:120px; Height:20px; background-color:#48789b; Font-Size:xx-Small;color:#ffffff;border-width:1px;" />
               
             </asp:Panel>
                </td>
               </tr>
               <tr>
                       <asp:Panel ID="panelActionButtons" runat="server">
                        <td align="center">
                           <asp:Button ID="btnNew" runat="server" Text="NEW" Width="55px" Height="20px" BackColor="#48789b" Font-Size="xx-Small" ForeColor="#ffffff" BorderWidth="1px" OnClick="btnNew_Click"/> 
                           <asp:Button ID="btnCopy" runat="server" Text="COPY" Width="55px" Height="20px" BackColor="#48789b" Font-Size="xx-Small" ForeColor="#ffffff" BorderWidth="1px" OnClick="btnCopy_Click"/>
                           <asp:Button ID="btnSave" runat="server" Text="SAVE" Width="55px" Height="20px" BackColor="#48789b" Font-Size="xx-Small" ForeColor="#ffffff" BorderWidth="1px" OnClick="btnSave_Click" onkeypress="ValidateForm();"/>
                           <asp:Button ID="btnCancel" runat="server" Text="CANCEL" Width="55px" Height="20px" BackColor="#48789b" Font-Size="xx-Small" ForeColor="#ffffff" BorderWidth="1px" OnClick="btnCancel_Click" Visible="false"/>
                           <asp:Button ID="btnDelete" runat="server" Text="DELETE" Width="55px" Height="20px" BackColor="#48789b" Font-Size="xx-Small" ForeColor="#ffffff" 
                                        BorderWidth="1px" OnClick="btnDelete_Click" 
                                       OnClientClick="if(!confirm('Click OK to confirm or Cancel to keep record.')) return false;" />

                        </td>
                       </asp:Panel>
                    </tr>
               <tr><td><br /><br /></td></tr>
               <tr>
                         <td align="center">
                          <asp:ImageButton ID="btnPrev" runat="server" ImageUrl="../lib/img/arrow_left.png" ToolTip="Go to Previous Record" OnClick="btnPrev_Click" Visible="false"/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                          <asp:ImageButton ID="btnNext" runat="server" ImageUrl="../lib/img/arrow_right.png" ToolTip="Go to Next Record" OnClick="btnNext_Click" Visible="false"/>
                         </td>
                    </tr>
               </table>
           </td>
        </tr>
         
        <tr>
        <td colspan="3">
        <asp:Panel ID="panelSpace" runat="server"><br /><br /><br /><br /><br /><br /><br /></br><br /><br/><br /><br /><br /><br /></br><br /><br/><br /><br /><br /><br /></br></asp:Panel></td>
        </tr>
        <tr>
         <td colspan="3">
           <asp:Panel ID="pnlDetail" runat="server" Visible="false">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
              <ContentTemplate>
                <table id="Table3" width="100%" cellpadding="3" cellspacing="3" runat="server">
                <tr>
                     
                    <td align="right">
                        <asp:Literal ID="litServiceDate" runat="server"><font color="red">* </font></asp:Literal>
                        <asp:Label ID="ServiceDateLabel"
                            text="Date of Service:"
                            AssociatedControlID="txtDate"
                            runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtDate" runat="server" Width="173px"></asp:TextBox>
                    </td>
                      <td align="right">
                        <asp:Literal ID="litBudgetCode" runat="server"><font color="red">* </font></asp:Literal>
                          <asp:Label ID="BudgetCodeLabel"
                              text="Budget Code:"
                              AssociatedControlID="ddlBudgetCode"
                              runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlBudgetCode" runat="server" Width="200px">
                        </asp:DropDownList>
                    </td>
                    <td align="right">
                        <asp:Literal ID="litTEC" runat="server"><font color="red">* </font></asp:Literal>
                        <asp:Label ID="ProjectLabel"
                            text="Project:"
                            AssociatedControlID="ddlTEC"
                            runat="server"></asp:Label>
                    </td>
                    <td>
                     <asp:DropDownList ID="ddlTEC" runat="server" Width="200px">
                     </asp:DropDownList>
                    </td>
                    
                   
                                  
                </tr>
                                 
                <tr>
                  <td align="right">
                        <asp:Literal ID="litActivities" runat="server"><font color="red">* </font></asp:Literal>
                      <asp:Label ID="ActivityLabel"
                          text="Activity:"
AssociatedControlID="ddlActivities"
                          runat="server"></asp:Label>
                    </td>
                    
                    <td>
                        <asp:DropDownList ID="ddlActivities" runat="server" Width="200px" ClientID="ddlActivities" OnSelectedIndexChanged="ddlActivities_SelectedIndexChanged" AutoPostBack="true">
                        </asp:DropDownList>
                    </td>
                 <td align="right">
                        <asp:Literal ID="litContactMethod" runat="server"><font color="red">* </font></asp:Literal>
                     <asp:Label ID="ContactMethodLabel"
                         text="Contact Method:"
                         AssociatedControlID="ddlContactMethod"
                         runat="server"></asp:Label>
                    </td>
                    <td>
                     <asp:DropDownList ID="ddlContactMethod" runat="server" Width="200px">
                     </asp:DropDownList>
                    </td> 
                     <td align="right">
                        <asp:Literal ID="litServiceLength" runat="server"><font color="red">* </font></asp:Literal>
                         <asp:label ID="HoursLabel"
                             text="Hours:"
                             AssociatedControlID="ddlServiceLength"
                             runat="server"></asp:label>
                    </td>
                    <td>
                     <asp:DropDownList ID="ddlServiceLength" runat="server" Width="200px" onfocus="ValidateForm();" >
                     </asp:DropDownList>
                    </td>        
                  </tr>
                <tr>
                  
                     <td align="right"><font color="red">* </font>
                         <asp:Label ID="ContractLabel"
                             text="Contract:"
                             AssociatedControlId="ddlFocus"
                             runat="server"></asp:Label>
                             </td>
                    <td>
                     <asp:DropDownList ID="ddlFocus" runat="server" Width="200px">
                     </asp:DropDownList>
                    </td> 
                   
                    <td>&nbsp</td>  
                </tr>
                <tr>
                   <td colspan="6" style="background-color:#f5f5f5;border:dashed 1px #dcdcdc;">
                   <asp:Literal ID="litAudience" runat="server"><font color="red">* </font></asp:Literal>Location/Audience/#Served
                    
                    <table border="0" cellpadding="3" cellspacing="0">
                        <tr >
                            <td colspan="6">
                               <ASP:Checkbox runat="server" ID="CK22B" Text="Indicator 2.2b: Natural Disaster and Emergency Mass Communications"  ForeColor="black" AutoPostBack="true"  Width="500" />
                            </td>
                        </tr>
					    <tr>
                        <td align="right"></td>
                        <td>  
                        <telerik:RadComboBox ID="ddlClient" runat="server" Label="Client:" Height="200px" Width="150px" Filter="Contains" 
                                 MarkFirstMatch="true" EnableLoadOnDemand="true" ClientID="ddlClient"
                                 OnItemsRequested="ddlClient_ItemsRequested"  AllowCustomText="true" AutoPostBack="true" 
                                 HighlightTemplatedItems="true" OnSelectedIndexChanged = "ddlClient_SelectedIndexChanged"></telerik:RadComboBox>
                                
                        </td>
                        <td align="right"></td>
                        <td>
                    
                        <telerik:RadComboBox ID="ddlSite" runat="server" Label="Specific Site:" Height="200px" Width="150px" Filter="Contains" 
                                     MarkFirstMatch="true" EnableLoadOnDemand="true" ClientID="ddlSite" 
                                     AllowCustomText="true" AutoPostBack="true" HighlightTemplatedItems="true" OnSelectedIndexChanged="ddlSite_SelectedIndexChanged">
                        </telerik:RadComboBox>
                        </td>
                                                
						<td>
						    <asp:Label ID="AudienceLabel"
                                text="Audience:"
                                AssociatedControlID="AudienceList"
                                runat="server"></asp:Label>
                                <asp:DropDownList ID="AudienceList" runat="server" Width="100px"></asp:DropDownList>
						</td>
						<td>
							<asp:Label ID="NumberServedLabel"
                                text="#Served:"
                                AssociatedControlID="AudienceValue"
                                runat="server"></asp:Label>
                                <asp:TextBox ID="AudienceValue" runat="server" Width="50px" ></asp:TextBox>
						</td>
						<td>
							&nbsp;&nbsp;<asp:Button ID="btnAdd" runat="server" Text="Add" Width="50px" OnClick="btnAdd_Click" /><asp:Button ID="btnDeleteLocationList" runat="server" Text="Del." OnClick="btnDeleteLocationList_Click" />
						</td>
					    </tr>
					    <tr>
						    <td colspan="8">
							    <asp:ListBox ID="AudienceListBox" runat="server" Width="100%"></asp:ListBox>
							    <span style="font-family:Arial;font-size:7pt;color:#909090;">Double-click on the row to remove. <asp:Label ID="AudienceListBoxLabel"
                                    text="<font color=#f5f5f5>Selected Values</font>"
                                    AssociatedControlID="AudienceListBox"
                                    runat="server"></asp:Label></span>
						    </td>
					    </tr>
				    </table>       
                   </td>
                   
                </tr>
                            
                <tr>                 
                 
                   <td colspan="6" valign="top">
                        <table border="0" width="100%" cellpadding="3">
                            <tr>
                                <td valign="top"><asp:label ID="CommentsLabel"
                                    text="Comments"
                                    AssociatedControlID="txtComments"
                                    runat="server"></asp:label>
                                    </td>
                                <td>
                                 <asp:TextBox Rows="100" Height="110px" runat="server" ID="txtComments" Width="800px" TextMode="MultiLine"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                   </td>
                </tr>
                <tr>
                    <td colspan="6">
                     <div style="display:inline-block;vertical-align:top">
                          TEA Strategic Priorities
                      </div>
                      <div style="margin: 0px 10px;">
                            <table border="0">
					            <tr>
						            <td>
						                <asp:DropDownList ID="ddlStrategicPriorityList" runat="server" Width="500px"></asp:DropDownList>
						            </td>
						            <td>
							            &nbsp;&nbsp;<input type="button" id="StrategicPriorityAdd" name="StrategicPriorityAdd" width="60px" value="Add" onclick="AddMultiSelect('ddlStrategicPriorityList', 'lbStrategicPriority', 'StrategicPriority', 'No Strategic Priority.');" /><input type="button" name="btnStrategicPriority" id="btnStrategicPriority" value="Del." onclick="    OnRemoveMulti('lbStrategicPriority', 'ddlStrategicPriorityList', 'StrategicPriority');"/>
						            </td>
					            </tr>
					            <tr>
						            <td>
							            <asp:ListBox ID="lbStrategicPriority" runat="server" Width="500px" Height="50"></asp:ListBox>
                                        <input type="hidden" id="StrategicPriority" runat="server" /><br />
						            </td>
					            </tr>
				                </table>  
                          </div>
                    </td>
                </tr>
                <tr><td colspan="6"><br /></td></tr>
                <tr><td colspan="6"><h3>Texas ESC Standards and Indicators</h3></td></tr>
                <tr>
                    <td colspan="6" style="border-top:1 dashed #000000; border-bottom:1 dashed #000000; padding:5px">
                        <table cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <td><asp:Literal ID="litFunding" runat="server"><font color="red">*</font></asp:Literal>
                                <asp:Label ID="FundingLabel"
                                    text="Funding:"
                                    AssociatedControlID="ddlFunding"
                                    runat="server"></asp:Label></td>
                            <td> <asp:DropDownList ID="ddlFunding" runat="server" Width="300px"></asp:DropDownList></td>
                        </tr>
                        <tr><td colspan="2"><br /></td></tr>
                        <tr>
                        <tr>
                        <td colspan="3" valign="top" style="width:500px">
                        <table cellpadding="0" cellspacing="0" border="0">
                         <tr>
                            <td>
                                <asp:CheckBox runat="server" ID="cbCoreService" Text="4.1 Core Service"/>
                            </td>
                         </tr>
                         <tr>
                            <td>
                                <asp:CheckBox runat="server" ID="cbExtendedLearningOpportunity" Text="2.4 Extended Learning Opportunities"/>
                            </td>
                         </tr>
                        
                         <tr> 
                            <td>
                             <asp:CheckBox ID="cbFinance" runat="server" Text="2.3b School Finance Related"/>
                           </td>        
                         </tr>
                         <asp:Panel ID="panelNonFirstStandardFinancialAssistance" runat="server" Visible="false">
                         <tr>
                            <td colspan="2" width="50%" style="background-color:#f5f5f5;border:dashed 1px #dcdcdc;">
                            <asp:Literal ID="litNonFirst" runat="server"><font color="#f5f5f5">*</font></asp:Literal>2.2b Non-FIRST Standard Financial Assistance
                            <table border="0" width="410px" cellpadding="3" cellspacing="0">
					            <tr>
						            <td>
						                <asp:DropDownList ID="ddlNonFirstStandardFinancialAssistanceList" runat="server" Width="300px"></asp:DropDownList>
						            </td>
						            <td>
							            &nbsp;&nbsp;<input type="button" id="NonFirstStandardFinancialAssistanceAdd" name="NonFirstStandardFinancialAssistanceAdd" width="60px" value="Add" onclick="AddMultiSelect('ddlNonFirstStandardFinancialAssistanceList', 'lbNonFirstStandardFinancialAssistance', 'NonFirstStandardFinancialAssistance', 'No Non-FIRST Standard Financial Assistance.');" /><input type="button" name="btnDeleteNonFirst" id="btnDeleteNonFirst" value="Del." onclick="OnRemoveMulti('lbNonFirstStandardFinancialAssistance', 'ddlNonFirstStandardFinancialAssistanceList', 'NonFirstStandardFinancialAssistance');"/>
						            </td>
					            </tr>
					            <tr>
						            <td colspan="3">
							            <asp:ListBox ID="lbNonFirstStandardFinancialAssistance" runat="server" Width="300px" Height="50"></asp:ListBox>
                                        <input type="hidden" id="NonFirstStandardFinancialAssistance" runat="server" /><br />
							           <%-- <span style="font-family:Arial;font-size:7pt;color:#909090;">Double-click on the row to remove.</span>--%>
						            </td>
					            </tr>
				                </table>       
                            </td>
                         </tr>
                         </asp:Panel>
                         </table>
                    </td>
                        <td colspan="3">
                        <table cellpadding="0" cellspacing="0" border="0">
                            <tr>
                              <td width="50%" style="background-color:#f5f5f5;border:dashed 1px #dcdcdc;">
                              <asp:Panel ID="panelIncreaseStudentPerformance" Visible="false" runat="server">
                              <asp:Literal ID="litIncreasedStudentPerformance" runat="server"><font color="#f5f5f5">*</font></asp:Literal>1.1b Increase Student Performance:
                                <table border="0" width="410px" cellpadding="3" cellspacing="0">
					                <tr>
						                <td>
						                   <asp:DropDownList ID="ddlPerformanceList" runat="server" Width="300px"></asp:DropDownList>
						                </td>
						                <td>
							                &nbsp;&nbsp;<input type="button" id="PerformanceAdd" name="PerformanceAdd" width="60px" value="Add" onclick="AddMultiSelect('ddlPerformanceList', 'lbPerformance', 'performance', 'No Performance has been selected.');" /><input type="button" name="btnDeletePerformance" id="btnDeletePerformance" value="Del." onclick="OnRemoveMulti('lbPerformance', 'ddlPerformanceList', 'performance');"/>
						                </td>
					                </tr>
					                <tr>
						                <td colspan="3">
							                <asp:ListBox ID="lbPerformance" runat="server" Width="300px" Height="50"></asp:ListBox>
                                            <input type="hidden" id="performance" runat="server" name="performance"/><br />
							                <%--<span style="font-family:Arial;font-size:7pt;color:#909090;">Double-click on the row to remove.</span>--%>
				
						                </td>
					                </tr>
				                 </table> 
                                 </asp:Panel>      
                                </td>
                            </tr>
                           
                            <tr>
                              <td width="50%" style="background-color:#f5f5f5;border:dashed 1px #dcdcdc;">
                              <asp:Literal ID="libSecondaryCredit" runat="server"><font color="#f5f5f5">*</font></asp:Literal>
                                  <asp:Label ID="PostSecondaryCreditLabel"
                                      text="1.2 Post Secondary Credit:"
                                      AssociatedControlID="ddlCreditList"
                                      runat="server"></asp:Label>
                                <table border="0" cellpadding="3" cellspacing="0">
					                <tr>
						                <td>
						                   <asp:DropDownList ID="ddlCreditList" runat="server" Width="300px"></asp:DropDownList>
						                </td>
						                <td>
							                &nbsp;&nbsp;<input type="button" id="CreditAdd" name="CreditAdd" width="60px" value="Add" onclick="AddMultiSelect('ddlCreditList', 'lbCredit', 'credit', 'No Credit has been selected.');" /><input type="button" name="btnDeleteCredit" id="btnDeleteCredit" value="Del." onclick="OnRemoveMulti('lbCredit', 'ddlCreditList', 'credit');"/>
						                </td>
					                </tr>
					                <tr>
						                <td colspan="2">
							                <asp:ListBox ID="lbCredit" runat="server" Width="300px" Height="50"></asp:ListBox>
                                            <asp:Label ID="ListboxCreditLabel"
                                                text="<font color=#f5f5f5>Selected Values</font>"
                                                AssociatedControlID="lbCredit"
                                                runat="server"></asp:Label>
                                            <input type="hidden" id="credit" runat="server" /><br />
							                <%--<span style="font-family:Arial;font-size:7pt;color:#909090;">Double-click on the row to remove.</span>--%>
				
						                </td>
					                </tr>
                                    <tr>
                                      <td colspan="2" width="50%" style="background-color:#f5f5f5;border:dashed 1px #dcdcdc;">
                                      <asp:Literal ID="litFinacialIntegrity" runat="server"><font color="#f5f5f5">*</font></asp:Literal>
                                          <asp:Label ID="IntegrityLabel"
                                              text="2.3a Financial Integrity:"
                                              AssociatedControlID="ddlIntegrityList"
                                              runat="server"></asp:Label>
                                        <table border="0" width="410px" cellpadding="3" cellspacing="0">
					                        <tr>
						                        <td>
						                           <asp:DropDownList ID="ddlIntegrityList" runat="server" Width="300px"></asp:DropDownList>
						                        </td>
						                        <td>
							                        &nbsp;&nbsp;<input type="button" id="IntegrityAdd" name="IntegrityAdd" width="60px" value="Add" onclick="AddMultiSelect('ddlIntegrityList', 'lbIntegrity', 'integrity', 'No Finacial Integrity has been selected.');" /><input type="button" name="btnDeleteIntegrity" id="btnDeleteIntegrity" value="Del." onclick="OnRemoveMulti('lbIntegrity', 'ddlIntegrityList', 'integrity');"/>
						                        </td>
					                        </tr>
					                        <tr>
						                        <td colspan="3">
							                        <asp:ListBox ID="lbIntegrity" runat="server" Width="300px" Height="50"></asp:ListBox>
                                                    <asp:Label ID="listBoxIntegrityLabel"
                                                        text="<font color=#f5f5f5>Selected Values</font>"
                                                        AssociatedControlID="lbIntegrity"
                                                        runat="server"></asp:Label>
                                                    <input type="hidden" id="integrity" runat="server" /><br />
							                        <%--<span style="font-family:Arial;font-size:7pt;color:#909090;">Double-click on the row to remove.</span>--%>
				
						                        </td>
					                        </tr>
				                         </table>       
                                        </td>
                                    </tr>
				                 </table>       
                                </td>
                            </tr>
                           
                        </table>
                    </td>
                </tr>
               
                </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="6">&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="6" align="right">
                        <asp:Button ID="btnSave2" runat="server" Text="SAVE" Width="55px" Height="20px" BackColor="#48789b" Font-Size="xx-Small" ForeColor="#ffffff" BorderWidth="1px" OnClick="btnSave_Click" onkeypress="ValidateForm();"/>
                    </td>
                </tr>
                <tr>
                <td colspan="6">&nbsp;</td>
                </tr>
                             
            </table>
            </ContentTemplate>
           </asp:UpdatePanel>
          </asp:Panel>
           <asp:Panel ID="pnlReports" runat="server">
               <div style="text-align:right">
                    <table width="30%">
                        <tr><td colspan="2"><h5>Reports:</h5></td></tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td>
                                <a style="cursor:pointer; text-decoration:underline; color:#48789b" onclick='window.open("<%= url %>report.aspx?fromfrontend=1&cid=tx_r17&sid=<%= sid %>&id=<%= report_time_sheet_id %>","_blank","width=550 height=550 resizable=1")'>Current Time Sheet Report</a>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td>
                                <a style="cursor:pointer; text-decoration:underline; color:#48789b" onclick='window.open("<%= url %>report.aspx?fromfrontend=1&cid=tx_r17&sid=<%= sid %>&id=<%= report_summary_id %>","_blank","width=550 height=550 resizable=1")'>Summary Report</a>
                            </td>
                        </tr>
                        <tr><td colspan="2" ><br /><br /></td></tr>
                        <tr>
                            <td colspan="2" align="center">
                                <input type="button" value="CLOSE" name="btnClose" onclick="window.close();" style="width:60px; height:20px; background-color:#48789b; font-size:xx-small; color:#ffffff; border:1px"/>
                            </td>
                        </tr>
                    </table>
               </div>
           </asp:Panel>
         </td>
        </tr>
    </table>
   </ContentTemplate>
   </asp:UpdatePanel>
   </div>
  </td>
 </tr>
 <tr>
    <td>
        <div style="padding:5px; text-align:center">
             <a title="escWorks .NET"  href="#" onclick="window.open('http://www.escworks.com','_blank', 'width=500, height=500, resizable=yes')" style="cursor:pointer">
                <img src='<%= ResolveUrl("~/lib/standard/img/escWorks_LOGO_PoweredBy.jpg")%>' alt="escWorks Logo" border="0" />
             </a>
        </div>           
    </td>
 </tr>
 </table>
 </div>
 <asp:TextBox ID="txtObjID" runat="server" Visible="false"></asp:TextBox>
     
</form>
</body>
</html>
