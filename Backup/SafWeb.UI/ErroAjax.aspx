<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ErroAjax.aspx.vb" Inherits="FrameWork.UI.ErroAjax"%>
<%@ Register TagPrefix="cc1" Namespace="FrameWork.WebControl" Assembly="FrameWork.WebControl" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>%%SITE%%</title>
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="%%PATH%%/estilos/FrameWork.css" type="text/css" rel="stylesheet">
		<script language="javascript">
			// --------------------------------------------------------------
			// Obtem a RadWindow aberta nesta página 
			// --------------------------------------------------------------

			function GetRadWindow()
			{
			var oWindow = null;
			if (window.radWindow) oWindow = window.radWindow;
			else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
			return oWindow;
			} 
			  
			// --------------------------------------------------------------
			// Fecha a janela atual
			// --------------------------------------------------------------
			  
			function CloseWin()   
			{   
				var oWindow = GetRadWindow();   
				oWindow.Close();   
			}   

		</script>
	</HEAD>
	<body >
		<form id="Form2" method="post" runat="server">
			<table cellspacing="0" cellpadding="0" width="580" align="center" height="100%">
				<tr>
					<td align="center" valign="top">
						<table class="backPadrao" cellspacing="0" cellpadding="0" width="560" align="center" height="250">
							<tr>
								<td class="BordaDefaultLRB" align="center" valign="top">
									<!-- cccccccccccc START: Titulo  ccccccccccc -->
									<table width="540" border="0" cellspacing="0" cellpadding="0" align="center">
										<tr>
											<td colspan="3" height="15">&nbsp;</td>
										</tr>
										<tr>
											<td class="CantoEsq"></td>
											<td class="cadBarraTitulo" height="15" width="520">
												<asp:Label id="lblTitulo" runat="server">Erro de Execução</asp:Label>
											</td>
											<td class="CantoDir"></td>
										</tr>
										<tr>
											<!-- cccccccccccc START: Conteudo  ccccccccccc -->
											<td align="center" valign="top" colspan="3">
												<table class="ContainerBordaLRB" cellspacing="0" cellpadding="5" width="100%" border="0"
													align="center">
													<tr>
														<td class="cadlblForm" align="center"><asp:placeholder id="plhErro" runat="server"></asp:placeholder></td>
													</tr>
													<tr>
														<td height="100">&nbsp;</td>
													</tr>
												</table>
												<table cellspacing="0" cellpadding="0" width="100%" align="center" border="0">
													<tr>
														<td class="backBarraBotoes">&nbsp;
															<asp:Button id="btnVoltar" runat="server" Text="Fechar" CssClass="cadbutton100"></asp:Button>
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
		</form>
	</body>
</HTML>
