<%@ Page Language="C#" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="shoebox_default" MasterPageFile="~/MasterPage.master" %>


<asp:Content runat="server" ContentPlaceHolderID="mainBody"><a name="MainBody"></a>

    <table border="0" cellpadding="4" cellspacing="0" width="100%"> 
		<tr>
			<td valign="top">
				<b><i>"My Account"</i></b> is a record tracking system that enables
				educators to track their professional growth on-line.
				The Portfolio component is free to users of the <b><i><%# region4.escWeb.SiteVariables.customer_name %></i></b> escWorks<small><sup>&reg;</sup></small> system.
				
				<table border="0" cellpadding="8" cellspacing="0" width="100%">
					<tr>
						<td valign="top">
							<img src="../lib/img/shoebox.jpg" border="0" alt="Shoebox Image">
							<span style="font-size: 8pt; font-style: Italic;">
								Let "My Account" free you from the hassles of paper-based
								record keeping.
							</span>
						</td>
						<td width="100%">
							<table border="0" cellpadding="0" cellspacing="0" width="100%">
								<tr>
									<td width="100%">
										<a href="transcripts/default.aspx" class="link"><b>The Professional Development Record</b></a>
										<br>
										<i>
											The Professional Development Record allow you to track the amount of credit you have 
											received from professional development events. All of this data is aggregated 
											into a printable format that can be used as proof of credit.
										</i>
									</td>
								</tr>
								<tr>
									<td width="100%">
										<br>
										<a href="account/default.aspx" class="link"><b>User Account</b></a>
										<br>
										<i>
											Your user account information is stored in this section of your
											portfolio.  It can only be accessed via a secure connection, and all
											of the data is protected by industry standard security features.
										</i>
									</td>
								</tr>
								<tr>
									<td width="100%">
										<br>
										<a href="registration/default.aspx" class="link"><b>Registration History</b></a>
										<br>
										<i>
											The registration history allows you to view your past and future <%# region4.escWeb.SiteVariables.ObjectProvider.SessionPluralName %>. 
										</i>
									</td>
								</tr>
								<tr>
									<td width="100%">
										<br>
										<a href="subscriptions/default.aspx" class="link"><b>Subscriptions</b></a>
										<br>
										<i>
											Subscriptions allow you to notified when relevant <%# region4.escWeb.SiteVariables.ObjectProvider.SessionPluralName %> become available.
										</i>
									</td>
								</tr>
							</table>
						</td>
					</tr>
				</table>
			</td>
		</tr>
	</table>
	

</asp:Content>