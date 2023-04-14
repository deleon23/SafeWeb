<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ErroAdm.aspx.vb" Inherits="FrameWork.UI.ErroAdm" %>
<%@ Register TagPrefix="cc1" Namespace="FrameWork.WebControl" Assembly="FrameWork.WebControl" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>%%SITE%%</title>
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="%%PATH%%/estilos/FrameWork.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body >
		<form id="Form2" method="post" runat="server">
			<table cellspacing="0" cellpadding="0" width="778" align="center" border="0" >
				<tr>
					<td>
						<cc1:FWServerControl id="Fwservercontrol3" runat="server" Controle="PagebannerAdmin"></cc1:FWServerControl></td>
				</tr>
				<tr>
					<td valign="top" >
						<table class="backPadrao" cellspacing="0" cellpadding="0" width="100%" align="center" height="424">
							<tr>
								<td class="BordaDefaultLRB" align="center" valign="top">
									<!-- cccccccccccc START: Titulo  ccccccccccc -->
									
									<table class="MarginTopBarra" width="740" border="0" cellspacing="0" cellpadding="0" align="center">
										<tr>
											<td class="CantoEsq"></td>
											<td class="cadBarraTitulo" height="15" width="720">
												<img src="%%PATH%%/imagens/comum/bulletbarra.gif" align="absMiddle">
												<asp:Label id="lblTitulo" runat="server">Erro de Execução</asp:Label>
											</td>
											<td class="CantoDir"></td>
										</tr>
										<tr>
											<!-- cccccccccccc START: Conteudo  ccccccccccc -->
											<td align="center" valign="top" colspan="3">
												<table class="ContainerBordaLRB" cellspacing="0" cellpadding="5" width="100%" border="0" align="center">
													<tr>
														<td class="cadlblForm" align="center">
															<asp:PlaceHolder id="plhErro" runat="server"></asp:PlaceHolder>
														</td>
													</tr>
													<tr>
														<td height="100">&nbsp;</td>
													</tr>
												</table>
												<table width="740" border="0" cellspacing="0" cellpadding="0" align="center">
													<tr>
														<td class="backBarraBotoes">&nbsp;
															<asp:Button id="btnVoltar" runat="server" Text="Voltar" CssClass="cadbutton100"></asp:Button>
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
					<td>
						<cc1:FWServerControl id="Fwservercontrol4" runat="server" Controle="RodapeAdmin"></cc1:FWServerControl></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
