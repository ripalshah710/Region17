<%-- Copyright (c) Microsoft Corporation. All rights reserved. --%>

<%@ Page Language="C#" AutoEventWireup="true" Inherits="Microsoft.LearningComponents.Frameset.Frameset_NavClosed" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<!-- MICROSOFT PROVIDES SAMPLE CODE "AS IS" AND WITH ALL FAULTS, AND WITHOUT ANY WARRANTY WHATSOEVER.  
     MICROSOFT EXPRESSLY DISCLAIMS ALL WARRANTIES WITH RESPECT TO THE SOURCE CODE, INCLUDING BUT NOT 
     LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE.  THERE IS 
     NO WARRANTY OF TITLE OR NONINFRINGEMENT FOR THE SOURCE CODE. -->
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
    <link rel="stylesheet" type="text/css" href="Theme/Styles.css" />
    <script language="javascript" type="text/javascript" src="./Include/FramesetMgr.js"></script>
    <script language="javascript" type="text/javascript" src="./Include/Nav.js"></script>
</head>
<body tabindex="1" onload="OnLoad( NAVCLOSED_FRAME );">
    <div id="TOCFrameVisibleDiv" style="display: none">
        <table height="12" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tbody>
                <tr valign="top">
                    <td valign="top" align="left" width="100%">
                        <img id="HeadShadow1" height="12" src="Theme/HeadShadow.gif" width="100%" border="0"
                            tabindex="1">
                    </td>
                    <td valign="top" align="right" width="28">
                        <img id="HeadCornerRight1" height="21" src="Theme/HeadCornerRt.gif" width="28" border="0"
                            tabindex="1">
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    <div id="TOCFrameHiddenDiv" style="display: inline">
        <table height="21" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tbody>
                <tr valign="top">
                    <td valign="top" align="left" width="179">
                        <img id="TocClosedTab" height="20" src="Theme/TocClosedTab.gif" width="179" border="0">
                    </td>
                    <td valign="top" align="left" width="100%">
                        <img id="HeadShadow2" height="12" src="Theme/HeadShadow.gif" width="100%" border="0">
                    </td>
                    <td valign="top" align="right" width="28">
                        <img id="HeadCornerRight2" height="21" src="Theme/HeadCornerRt.gif" width="28" border="0">
                    </td>
                </tr>
            </tbody>
        </table>
        <div class="NavClosedPreviousBtnGrphic" id="divPrevious">
            <label class="ShellSaveText">
                Navigation Bar</label>
            <%--		<IMG id=imgPrevious title="<%=PreviousTitleHtml%>" height=15 src="Theme/Prev.gif" width=15 border=0 tabindex=1>--%>
        </div>
        <div class="NavClosedNextBtnGrphic" id="divNext">
            <%--		<IMG id=imgNext title="<%=NextTitleHtml%>" height=15 src="Theme/Next.gif" width=15 border=0 tabindex=1>--%>
        </div>
        <div class="NavClosedSaveBtnGrphic" id="divSave">
            <%--<IMG id=imgSave title="<%=SaveTitleHtml%>" height=15 src="Theme/Save.gif" width=15 border=0 tabindex=1>--%>
        </div>
        <div class="NavClosedShowTOCGrphic">
            <img id="imgOpenToc" title="<%=MaximizeTitleHtml%>" height="14" src="Theme/TocOpen.gif"
                width="14" border="0" tabindex="1">
        </div>
    </div>
</body>
<%
    // <body>
    //     <form id="form1" runat="server">
    //     <div>
    //     
    //     </div>
    //    </form>
    // </body>
%>
</html>
