<%@ Page Language="vb" AutoEventWireup="false" Codebehind="Admin.aspx.vb" Inherits="FrameWork.UI.Admin" %>
<%@ Register TagPrefix="cc1" Namespace="FrameWork.WebControl" Assembly="FrameWork.WebControl" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>%%SITE%%</title>
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="%%PATH%%/estilos/FrameWork.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<table cellspacing="0" cellpadding="0" width="778" align="center" border="0">
				<tr>
					<td style="HEIGHT: 18px">
						<cc1:FWServerControl id="FWServerControl2" runat="server" Controle="PagebannerAdmin"></cc1:FWServerControl></td>
				</tr>
				<tr>
					<td height="424">
						<table align="center" cellpadding="0" cellspacing="0" class="backPadrao" width="100%" height="426">
							<tr>	
								<td align="center" valign="middle" class="BordaDefaultLRB">
									<img src="%%PATH%%/Imagens/menuballs/background_capaBrinks.jpg"></td>								
							</tr>							
						</table>
				  </td>
				</tr>
				<tr>
					<td valign="bottom">
						<cc1:FWServerControl id="FWServerControl1" runat="server" Controle="RodapeAdmin"></cc1:FWServerControl></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
