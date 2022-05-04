<%@ Page language="c#" Inherits="tx_r17.Shoebox.Evaluation.Pages.Eval" CodeFile="Eval.aspx.cs" %>
<!DOCTYPE html PUBLIC "-//W3C//Dtd html 4.0 Transitional//EN" >
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
			function LoadEvaluation(id)
			{
				window.open("../evaluation/?registrationId=" + id, "Evaluation", "width=575, height=600, resizable=no, scrollbars=no, status=no, menubar=no");
			}
					
			function LoadCertificate(id)
			{
			 //top.location.href = "../../lib/report.aspx?reportName=certificate&instruction=download:pdf&attendee_id=" + id; 
			}
			</script>
	</head>
	<body onload="Page_OnInit();" style="margin:0px;">
		<div align="center">
			<br>
			<form id="aspnetForm" method="post" runat="server">
				<table cellspacing="0" cellpadding="0" align="center" border="0" style="WIDTH:550px;BORDER-COLLAPSE:collapse;HEIGHT:250px">
					<tr>
						<td class="LayoutRule" colspan="3" width="1"><img src="../../lib/standard/img/trans.gif" border="0" width="1" height="1" alt="Transparent Gif"></td>
					</tr>
					<tr>
						<td class="LayoutRule" width="1"><img src="../../lib/standard/img/trans.gif" border="0" width="1" height="1" alt="Transparent Gif"></td>
						<td class="LayoutContent">
							<table cellpadding="0" cellspacing="5" width="100%" height="100%">
								<tbody>
									<tr>
										<td bgcolor="#ffffff">&nbsp;<img src="../../lib/standard/img/Evaluation.gif" width="32" height="32" alt="" border="0"
												align="absMiddle"></td>
										<td bgcolor="#ffffff" colspan="2"><span style="FONT-WEIGHT: bold; FONT-SIZE: 12pt; COLOR: #191970">Evaluation Form Verification</span><br>
										</td>
									</tr>
									<tr>
										<td class="LayoutRule" height="1" colspan="3"><img src="../../lib/standard/img/trans.gif" border="0" width="1" height="1" alt="Transparent Gif"></td>
									</tr>
									<asp:PlaceHolder ID="pSignIn" Runat="server">
										<tr>
											<td align="center" width="100%" colSpan="3" height="25">
												<asp:Label id="lblErrorMessage" Runat="server" ForeColor="#FF0000"></asp:Label></td>
										</tr>
										<tr>
											<td class="FormInput" noWrap align="right" width="150">Session ID:</td>
											<td class="FormInput" align="left">
												<asp:TextBox id="tbSessionId" Runat="Server"></asp:TextBox></td>
											<td noWrap align="left" width="100%">
												<asp:RequiredFieldValidator id="Requiredfieldvalidator1" Runat="Server" Font-Size="9pt" Display="Dynamic" ErrorMessage="Session ID field is required!"
													ControlToValidate="tbSessionId"></asp:RequiredFieldValidator><asp:RegularExpressionValidator ControlToValidate="tbSessionId" ValidationExpression="^\d+$" Font-Size="9pt" Display="Dynamic"
													ErrorMessage="Session ID contains numeric values only." Runat="Server" ID="Regularexpressionvalidator1"></asp:RegularExpressionValidator></td>
										</tr>
										<tr>
											<td class="FormInput" noWrap align="right" width="150">Registration ID:</td>
											<td class="FormInput" align="left">
												<asp:TextBox id="tbUserId" Runat="Server"></asp:TextBox></td>
											<td noWrap align="left" width="100%">
												<asp:RequiredFieldValidator id="Requiredfieldvalidator2" Runat="Server" Font-Size="9pt" Display="Dynamic" ErrorMessage="Registration ID field is required!"
													ControlToValidate="tbUserId"></asp:RequiredFieldValidator><asp:RegularExpressionValidator ControlToValidate="tbUserId" ValidationExpression="^\d+$" Font-Size="9pt" Display="Dynamic"
													ErrorMessage="User ID contains numeric values only." Runat="Server" ID="Regularexpressionvalidator2"></asp:RegularExpressionValidator></td>
						</td>
					</tr>
					<tr>
						<td class="FormInput" noWrap align="right" width="150">Last Name:</td>
						<td class="FormInput" align="left">
							<asp:TextBox id="tbLastName" Runat="Server"></asp:TextBox></td>
						<td noWrap align="left" width="100%">
							<asp:RequiredFieldValidator id="Requiredfieldvalidator3" Runat="Server" Font-Size="9pt" Display="Dynamic" ErrorMessage="Last Name field is required!"
								ControlToValidate="tbLastName"></asp:RequiredFieldValidator></td>
					</tr>
					<tr>
						<td align="right" colSpan="2">
							<asp:button id="btnValidate" onclick="Click_btnValidate" Runat="Server" Text="Submit"></asp:button></td>
					</tr>
					<tr>
						<td width="100%" colSpan="3" height="100%"></td>
					</tr>
					</asp:PlaceHolder>
					<asp:PlaceHolder ID="pEvaluation" Visible="False" Runat="Server">
						<tr vAlign="middle">
							<td class="FormInput" width="100%" colSpan="3"><I><B>We value your feedback.</B><br>
									<br>
									Please click on the <B>Evaluation</B> button in order to answer a very short 
									evaluation.<br>
									After submitting your feedback, you will be able to return to your Registration 
									History and print a copy of your certificate.</I></td>
						</tr>
						<tr vAlign="middle">
							<td class="FormInput" align="center" width="100%" colSpan="3">
								<asp:PlaceHolder id="btnEvaluation" Runat="Server"></asp:PlaceHolder></td>
						</tr>
					</asp:PlaceHolder>
					<asp:PlaceHolder ID="pCertificate" Visible="False" Runat="Server">
						<tr vAlign="middle">
							<td class="FormInput" align="center" width="100%" colSpan="3"><I><B>Thank you for 
										completing the evaluation form.</B><br>
									<br>
									Please click on the <B>Certificate</B> button in order to download your 
									Certificate of Completion.</I></td>
						</tr>
						<tr vAlign="middle">
							<td class="FormInput" align="center" width="100%" colSpan="3">
								<asp:PlaceHolder id="btnCertificate" Runat="Server"></asp:PlaceHolder></td>
					</asp:PlaceHolder>
					<asp:PlaceHolder ID="pNoEvaluation" Visible="False" Runat="Server">
						<tr vAlign="middle">
							<td class="FormInput" align="center" width="100%" colSpan="3">
							<I><B>There are no evaluation at this time.</B><br>
							<br>Please check back at a later time.</I></td>
						</tr>
					</asp:PlaceHolder>
					<tr height="1">
						<td class="LayoutRule" colspan="3"><img src="../../lib/standard/img/trans.gif" border="0" width="1" height="1" alt="Transparent Gif"></td>
					</tr>
					<tr>
						<td bgcolor="#ffffff" colspan="3">&nbsp;<img src="../../lib/standard/img/secure.gif" width="32" height="32" alt="" border="0" align="absMiddle">&nbsp;
						<span style="FONT-WEIGHT: bold; FONT-SIZE: 8pt; COLOR: #191970">escWorks<small><sup>&reg;</sup></small> .NET Evaluation Form © 2005 </span><br>
						</td>
					</tr>
				</table>
				</td>
				<td class="LayoutRule" width="1"><img src="../../lib/standard/img/trans.gif" border="0" width="1" height="1" alt="Transparent Gif"></td>
				</tr>
				<tr>
					<td class="LayoutRule" colspan="3" width="1"><img src="../../lib/standard/img/trans.gif" border="0" width="1" height="1" alt="Transparent Gif"></td>
				</tr>
				</tbody></table>
			</form>
		</div>
	</body>
</html>
