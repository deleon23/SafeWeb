<%@ Page Language="vb" AutoEventWireup="false" Codebehind="Versoes.aspx.vb" Inherits="FrameWork.UI.Versoes" %>
<%@ Register TagPrefix="cc1" Namespace="FrameWork.WebControl" Assembly="FrameWork.WebControl" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>%%SITE%%</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="%%PATH%%/estilos/FrameWork.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body >
		<form id="Form1" method="post" runat="server">
			<table cellspacing="0" cellpadding="0" width="778" align="center" border="0">
				<tr>
					<td>
						<cc1:FWServerControl id="Fwservercontrol3" runat="server" Controle="PagebannerAdmin"></cc1:FWServerControl></td>
				</tr>
				<tr>
					<td valign="top">
						<table class="backPadrao" cellspacing="0" cellpadding="0" width="100%" align="center" height="424">
							<tr>
								<td class="BordaDefaultLRB" align="center" valign="top">
									<!-- cccccccccccc START: Titulo  ccccccccccc -->
									<table width="740" border="0" cellspacing="0" cellpadding="0" align="center">
										<tr>
											<td class="CantoEsq"></td>
											<td class="cadBarraTitulo" height="15" width="720">
												<asp:Label id="lblTitulo" runat="server">Versões</asp:Label>
											</td>
											<td class="CantoDir"></td>
										</tr>
										<tr>
											<!-- cccccccccccc START: Conteudo  ccccccccccc -->
											<td align="center" valign="top" colspan="3">
												<table class="ContainerBordaLRB" cellspacing="0" cellpadding="5" width="100%" border="0"
													align="center">
													<tr>
														<td>&nbsp;&nbsp;</td>
														<td class='cadlbl'><b><asp:Label id="lblDescricao" runat="server">Descrição</asp:Label></b></td>
														<td class='cadlbl'><b><asp:Label id="lblArquivo" runat="server">Arquivo</asp:Label></b></td>
														<td class='cadlbl'><b><asp:Label id="lblDesenvolvedor" runat="server">Desenvolvedor</asp:Label></b></td>
														<td class='cadlbl'><b><asp:Label id="lblVersao" runat="server">Versão</asp:Label></b></td>
													</tr>
													<tr>
														<td colspan="5"></td>
													</tr>
													<asp:PlaceHolder id="plhConteudo" runat="server"></asp:PlaceHolder>
												</table>
												<table cellspacing="0" cellpadding="0" width="100%" align="center" border="0">
													<tr>
														<td class="backBarraBotoes"><input type="button" id="btnVoltar" runat="server" Class="cadbutton100" Value="Voltar"
																OnClick="javascript:window.navigate('%%PATH%%/Admin/Admin.aspx')" NAME="btnVoltar"></td>
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
						<cc1:FWServerControl id="Fwservercontrol2" runat="server" Controle="RodapeAdmin"></cc1:FWServerControl>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
