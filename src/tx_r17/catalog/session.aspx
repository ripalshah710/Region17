<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="session.aspx.cs" Inherits="catalog_session" Title="Untitled Page" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainBody" runat="Server"><a name="MainBody"></a>
    <script language="javascript" type="text/javascript">

        function OnClientOpenView() {
            var tooltip = $find("<%=radToolTipReviews.ClientID %>");
            tooltip.show();
        }

        function CloseToolTip() {
            var tooltip = Telerik.Web.UI.RadToolTip.getCurrent();
            if (tooltip) {
                tooltip.hide();
            }
        }

        
    function printDiv() {
        var divToPrint = document.getElementById('printarea');
        newWin= window.open();
        newWin.document.write(divToPrint.innerHTML);
        newWin.location.reload();
        newWin.focus();
        newWin.print();
        newWin.close();
}

//$.fn.extend({
//	print: function() {
//		var frameName = 'printIframe';
//		var doc = window.frames[frameName];
//		if (!doc) {
//			$('<iframe>').hide().attr('name', frameName).appendTo(document.body);
//			doc = window.frames[frameName];
//		}
//		doc.document.body.innerHTML = this.html();
//		doc.window.print();
//		return this;
//	}
//});

        </script>
<div style="margin-top: -28px;"></div>
    <button id="showPrinter" style="float: right; border: none;" onmousedown="printDiv()">
<%--<button id="showPrinter" style="float: right; border: none;" onmousedown="$('#printarea').print();">--%>
<img src="..\lib\img\buttons\printer.png" alt="print icon"/>
</button> 
    <br /><br />

    <asp:Label runat="server" ID="lblErrorMessage" CssClass="error" />
<%--    <br />--%>
    <%--<div id="showContents">--%> 
    <div style="width: 100%; border-collapse: collapse;" runat="server" id="contentsTable" class="mainBody"></div>

        <div class="row">
            <div class="form-group col-xs-12 col-sm-12"> 
                <div>
                    <div>
                    <span style="padding-right: 10px;"><button type="button" onclick="javascript:history.back()" class="formInput btn btn-R17Blue btn-lg" role="button" style="width: 130px; font-size:small" ToolTip="Click here to go to previous page.">Previous</button></span>

                    <button type="button" onclick="window.location.href='<%#ResolveUrl("~/search.aspx") %>'" class="formInput btn btn-R17Blue btn-lg" role="button" style="width: 130px; font-size:small" ToolTip="Click here for new search.">New Search</button>
                    </div>
                    <div style="clear: both;"></div>
                </div>
            </div>
        </div>


<%--    <table style="width: 100%; background-color: #ffffff; border-collapse: collapse;" runat="server" id="contentsTable"
        class="mainBody">
        <tr valign="top">
        
            <td>
               <a id="A1" runat="server" href="javascript:history.back()"> 
               <img id="Img1" runat="server" src="~/lib/img/buttons/previous.png" alt="Previous" border="0"  /></a>
                <a id="A2" runat="server" href="~/search.aspx">
                    <img id="Img2" runat="server" src="~/lib/img/buttons/newsearch.png" alt="New Search" border="0" /></a>
            </td>--%>

<%--    <div id="printarea">
    <div class="row">
        <div class="form-group col-xs-12 col-sm-12">
            12345
        </div>
    </div>--%>
    <div class="row">
        <div class="form-group col-xs-12 col-sm-12"> 
            <div>
                <%--<h2><asp:Label runat="server" ID="lblTitle" style="font-size:18px;" /></h2><br />--%>

                    <%--<br />--%>
                    <div>

                    <span ><asp:Button runat="server" ID="btnRegister" Cssclass="formInput btn btn-R17Blue btn-lg" AlternateText="Register" Text="Register" Style="width: 130px; font-size:small; margin-right: 10px !important;" ToolTip="Click here to register." /></span>
                    <span ><asp:Button runat="server" ID="btnGroupRegister" Cssclass="formInput btn btn-R17Blue btn-lg" AlternateText="Group Register" Text="Group Register" Style="width: 130px; font-size:small; margin-right: 10px !important;" ToolTip="Click here to group register." /></span>
                    <span ><asp:Button runat="server" ID="btnWaitingList" Cssclass="formInput btn btn-R17Blue btn-lg" AlternateText="Waiting List" Text="Waiting List" Style="width: 130px; font-size:small; margin-right: 10px !important;" ToolTip="Click here to wait list." /></span>
                    <span ><asp:Button runat="server" ID="btnRemoveFromCart" Cssclass="formInput btn btn-R17Blue btn-lg" AlternateText="Remove" Text="Remove From Cart" Style="width: 130px; font-size:small; margin-right: 10px !important;" ToolTip="Click here to remove from cart.." OnClientClick="localStorage.setItem('itemCount', parseInt(localStorage.getItem('itemCount') - 1));"/></span>
                    <span style="float:left; display:inline-block"><button type="button" id="btnShare" class="formInput btn btn-R17Blue btn-lg" style="margin-right: 10px !important; width: 130px; font-size:small; color: white" onclick="window.open('<%#ResolveUrl("~/lib/collaboration/default.aspx") %>?url=<%# server %>catalog/session.aspx?session_id=<%=lblSessionID.Text %>&title=Session%20Detail&session_id=<%=lblSessionID.Text %>')" role="button">Share</button></span>
                    </div>
                        <div style="clear: both;"></div>
                </div>
            </div>
        </div>
            <%--<td rowspan="2" valign="middle">
                <asp:ImageButton runat="server" ID="btnRegister" AlternateText="Register" />
                <asp:ImageButton runat="server" ID="btnGroupRegister" AlternateText="Group Regsiter" />
                <asp:ImageButton runat="server" ID="btnWaitingList" AlternateText="Waiting List" />
                <asp:ImageButton runat="server" ID="btnRemoveFromCart" AlternateText="Remove" />
                <br />
                <%# base.SharePageButton %>
                <br />
                <br />
                <br />--%>
                <br />
<%--            </td>
        </tr>
        <tr>
         
            <td>--%>
<div id="printarea">
    <div class="row">
        <div class="form-group col-xs-12 col-sm-12"> 
            <div>   
                <h2><asp:Label runat="server" ID="lblTitle" style="font-size:18px;" /></h2>
                <asp:Label runat="server" ID="label2" CssClass="mainBodySmall" /><br />
                <%--<h2><asp:Label runat="server" ID="lblTitle" CssClass="h2.mainBodyBold" /></h2><br />--%>
                <asp:Panel runat="server" ID="panelRating" Visible="false">
                    <telerik:RadRating ID="radRatingSession" runat="server" ItemCount="5" SelectionMode="Continuous"
                        Precision="Exact" ReadOnly="true" Skin="Default">
                    </telerik:RadRating>
                    <asp:LinkButton ID="btnOpenReview" CssClass="link" OnClientClick="OnClientOpenView();return false;"
                        runat="server"></asp:LinkButton>
                    <telerik:RadToolTip ID="radToolTipReviews" runat="server" ShowEvent="FromCode" HideEvent="FromCode"
                        TargetControlID="btnOpenReview" RelativeTo="Element" Skin="Default" Position="MiddleLeft"
                        OffsetX="-15" Animation="Slide">
                        <asp:Panel ID="panelDetailedReviews" runat="server">
                        </asp:Panel>
                        <asp:Button ID="btnCloseReview" OnClientClick="CloseToolTip();return false;" runat="server" Text="Close" />
                    </telerik:RadToolTip>
                </asp:Panel>
                <br />
                <asp:Label runat="server" ID="lblDescription" CssClass="mainBodySmall" /><br />
<%--            </td>
        </tr>
        <tr>--%>
         
            </div>
        </div>
    </div>

<%--    
    <div class="row">
        <div class="form-group col-xs-12 col-sm-12">
            12345<h2><asp:Label runat="server" ID="lblTitle" style="font-size:18px;" /></h2><br />
        </div>
    </div>--%>


    <div style:"font-family: Arial";>
        <div class="row">
            <div class="form-group col-xs-12 col-sm-12"> 
                <div>
                <h2><span style="font-size:18px;" ><asp:PlaceHolder runat="server" ID="placeHolderImportantInfo" /><b>Important
                    <%# region4.escWeb.SiteVariables.ObjectProvider.SessionNameCapitalized %>
                    Information:</b><br /></span></h2>
                <%--</div>--%>
                <br />
                <div>
                    <asp:Label runat="server" ID="lblWebComments" CssClass="leftIndent mainBodySmall" />
                </div>

                    <%--<asp:Label runat="server" ID="lblWebComments" CssClass="mainBody" /><br />--%>
                    </div>
                </div>
</div> </div>

            <%--<td colspan="2">
                <h3><b>Important
                    <%# region4.escWeb.SiteVariables.ObjectProvider.SessionNameCapitalized %>
                    Information:</b><br /></h3>
                <br />
                <asp:Label runat="server" ID="lblWebComments" CssClass="mainBody" />
            </td>
        </tr>--%>

<div class="row">
        <div class="form-group col-xs-12 col-sm-12"> 
            <div>
                <asp:PlaceHolder ID="plttess" runat="server" Visible="false">
                                    <br /><b>T-TESS/T-PESS</b>:<br /> <div style="padding-left:26px"><asp:Label runat="server" ID="lblTPESS" CssClass="mainBody" /></div><br /><br />
                </asp:PlaceHolder>

               </div>
            <div class="sessionSummary">
                <div>
                <asp:Label Cssclass="leftIndent" runat="server" ID="lblRegistrationStatus" />
                    </div>
            </div>
            <br />
         </div>
    </div>

<%--        <tr>
            <td colspan="2">
                <asp:PlaceHolder ID="plttess" runat="server" Visible="false">
                                    <br /><b>T-TESS/T-PESS</b>:<br /> <asp:Label runat="server" ID="lblTPESS" CssClass="mainBody" /><br /><br />
                </asp:PlaceHolder>

               
            </td>
        </tr>
        <tr>
         
            <td colspan="2" class="sessionSummary">
                <asp:Label runat="server" ID="lblRegistrationStatus" />
            </td>
        </tr>
    </table>--%>

<div id="contentsTable1" runat="server">
    <div class="mainBody">
        <div class="row">
       <div class="form-group col-xs-12 col-sm-6 mainBodySmall">  
            <div class="mainBodySmall">
                <b>
                    <%# region4.escWeb.SiteVariables.ObjectProvider.SessionNameCapitalized %>
                    ID:</b><br />
                <asp:Label runat="server" class="mainBodySmall" ID="lblSessionID" />
            </div>
        </div>
        <div class="form-group col-xs-12 col-sm-6 mainBodySmall">  
            <div>
                <b>Credits Available: </b>
                <br />
                <asp:Label runat="server" ID="lblCredits" />
            </div>
        </div>
      </div>
        <div class="row">
                <escWorks:RecommendedEvents runat="server" ID="recommendedEvents" />
        </div>

<%--    <table style="width: 100%; background-color: #ffffff;" class="mainBody" id="contentsTable1" runat="server">
        <tr>
        
            <td>
                <b>
                    <%# region4.escWeb.SiteVariables.ObjectProvider.SessionNameCapitalized %>
                    ID:</b><br />
                <asp:Label runat="server" ID="lblSessionID" />
            </td>
            <td>
                <b>Credits Available: </b>
                <br />
                <asp:Label runat="server" ID="lblCredits" />
            </td>
            <td rowspan="3" align="right" valign="top">
                <escWorks:RecommendedEvents runat="server" ID="recommendedEvents" />
            </td>
           
        </tr>--%>

<div class="row">
       <div class="form-group col-xs-12 col-sm-6 mainBodySmall">  
            <div class="mainBodySmall">
                <b>
                    Seats Available:
                </b><br />
                <asp:Label ID="lblSeatsFilled" runat="Server" class="mainBodySmall" />
            </div>
        </div>
        <div class="form-group col-xs-12 col-sm-6 mainBodySmall">  
            <div>
                <b>Fee: </b>
                <br />
                <asp:Label ID="lblFee" runat="server">{0:c}</asp:Label>
            </div>
        </div>
</div> 

<%--        <tr>
         
            <td>
                <b>Seats Available:</b><br />
                <asp:Label runat="server" ID="lblSeatsFilled" />
            </td>
            <td>
                <b>Fee:</b><br />
                <asp:Label runat="server" ID="lblFee" />
            </td>
        </tr>--%>

<div class="row">
       <div class="form-group col-xs-12 col-sm-6 mainBodySmall">  
            <div class="mainBodySmall">
                <b>Contact Person:</b><br />
                <asp:Label runat="server" ID="lblContactPerson" />
            </div>
        </div>
        <div class="form-group col-xs-12 col-sm-6 mainBodySmall">  
            <div>
                <b>Instructor(s):</b><br />
                <asp:Label runat="server" ID="lblInstructors" />
            </div>
        </div>
</div>


        <%--<tr>
         
            <td>
                <b>Contact Person:</b><br />
                <asp:Label runat="server" ID="lblContactPerson" />
            </td>
            <td>
                <b>Instructor(s):</b><br />
                <asp:Label runat="server" ID="lblInstructors" />
            </td>
        </tr>--%>

<div class="row">
       <div class="form-group col-xs-12 col-sm-6 mainBodySmall">  
            <div class="mainBodySmall">
                <b>
                    Audience:
                </b><br />
                <asp:Label ID="lblAudiences" runat="Server" class="mainBodySmall" />
            </div>
        </div>
        <div class="form-group col-xs-12 col-sm-6 mainBodySmall">  
            <div>
                &nbsp;
            </div>
        </div>
</div> 
</div>
<%--           <tr>
            <td colspan="2">
                <b>Audience:</b><br>
                <asp:Label ID="lblAudiences" runat="Server"></asp:Label>
            </td>
        </tr>
    </table>--%>

<div class="row">
       <div class="form-group col-xs-12 col-sm-12 mainBodySmall">  
            <div class="mainBodySmall">
    <asp:PlaceHolder runat="server" ID="pLocationPlaceHolder" />
                            </div>
        </div>
</div> 

<%--    <asp:PlaceHolder runat="server" ID="pLocationPlaceHolder"/>--%>
    <!-- Begin Added By VM 6-28-2018 -->
<%--    <br /><br />--%>
     <asp:Panel runat="server" ID="panelOnDemand" Visible="false">
        <i>This course is part of a series. Upon registering for this class, any other courses in the series will be added to your cart, and fees will be adjusted accordingly.
        </i>
    </asp:Panel>
    </div>
        </div>
    <!-- End Added By VM 6-28-2018 -->
</asp:Content>
