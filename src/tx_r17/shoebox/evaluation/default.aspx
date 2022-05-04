<%@ Page language="c#" Inherits="tx_r17.Shoebox.Evaluation.Pages.DefaultPage" EnableViewState="False" EnableViewStateMac="False" CodeFile="Default.aspx.cs" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD html 4.0 Transitional//EN" >
<html
lang="en"
xml:lang="en">
	<head>
		<title>escWorks® .NET Evaluation Form</title>
		<link href="../../lib/css/local.css" type="text/css" rel="stylesheet" />
			<style></style>
			<script language="javascript">
			function Page_OnInit()
			{
				<asp:PlaceHolder ID="pCloser" Runat="Server" Visible="False">self.close();</asp:PlaceHolder>
			}
			
			function SendPage()
			{
				if(!Page_ClientValidate())
					return;
					
				//
				// Submit the form
				document.EvaluationForm.submit();
			}
			</script>
	</head>
	<body onload="Page_OnInit();" style="margin:0px;">
		<table cellSpacing="0" cellPadding="0" width="100%" height="100%" border="0">
			<tr>
				<td colspan="2" height="1" bgcolor="#191970"><img src="../../lib/standard/img/trans.gif" border="0" height="1" alt="Transparent Gif"></td>
			</tr>
			<tr>
				<td colspan="2" height="4" bgcolor="#ffffff"><img src="../../lib/standard/img/trans.gif" border="0" height="4" alt="Transparent Gif"></td>
			</tr>
			<tr>
				<td bgcolor="#ffffff">&nbsp;<img src="../../lib/standard/img/Evaluation.gif" width="32" height="32" alt="" border="0" align="absMiddle"></td>
				<td bgcolor="#ffffff"><span style="FONT-WEIGHT: bold; FONT-SIZE: 12pt; COLOR: #191970"><asp:Label ID="ctrlFormTitle" Runat="Server" /></span><br></td>
			</tr>
			<tr>
				<td colspan="2" height="4" bgcolor="#ffffff"><img src="../../lib/standard/img/trans.gif" border="0" height="4" alt="Transparent Gif"></td>
			</tr>
			<tr>
				<td colspan="2" height="1" bgcolor="#191970"><img src="../../lib/standard/img/trans.gif" border="0" height="1" alt="Transparent Gif"></td>
			</tr>
			<tr valign="middle">
				<td colspan="2"><asp:PlaceHolder ID="pIFrameContent" Runat="server"></asp:PlaceHolder></td>
			</tr>
			<tr>
				<td colspan="2" height="1" bgcolor="#191970"><img src="../../lib/standard/img/trans.gif" border="0" height="1" alt="Transparent Gif"></td>
			</tr>
			<tr>
				<td colspan="2" height="4" bgcolor="#ffffff"><img src="../../lib/standard/img/trans.gif" border="0" height="4" alt="Transparent Gif"></td>
			</tr>
			<tr>
				<td bgcolor="#ffffff">&nbsp;<img src="../../lib/standard/img/secure.gif" width="32" height="32" alt="" border="0" align="absMiddle"></td>
				<td bgcolor="#ffffff"><span style="FONT-WEIGHT: bold; FONT-SIZE: 8pt; COLOR: #191970">escWorks<small><sup>&reg;</sup></small> .NET Evaluation Form &copy; 2005 </span><br></td>
			</tr>
			<tr>
				<td colspan="2" height="4" bgcolor="#ffffff"><img src="../../lib/standard/img/trans.gif" border="0" height="4" alt="Transparent Gif"></td>
			</tr>
			<tr>
				<td colspan="2" height="1" bgcolor="#191970"><img src="../../lib/standard/img/trans.gif" border="0" height="1" alt="Transparent Gif"></td>
			</tr>
		</table>
	</body>
</html>
