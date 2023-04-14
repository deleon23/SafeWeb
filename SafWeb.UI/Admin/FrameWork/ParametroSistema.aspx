<%@ Register TagPrefix="rada" Namespace="Telerik.WebControls" Assembly="RadAjax.Net2" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ParametroSistema.aspx.vb" Inherits="FrameWork.UI.ParametroSistema" %>
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
									<table class="MarginTopBarra" width="740" border="0" cellspacing="0" cellpadding="0" align="center">
										<tr>
											<td class="CantoEsq"></td>
											<td class="cadBarraTitulo" height="15" width="720">
												<img src="%%PATH%%/Imagens/comum/bulletbarra.gif" align="middle" />
												<asp:Label id="lblTitulo" runat="server">Configuração Sistema</asp:Label>
											</td>
											<td class="CantoDir"></td>
										</tr>
										<tr>
											<!-- cccccccccccc START: Conteudo  ccccccccccc -->
											<td align="center" valign="top" colspan="3">
												<rada:RadAjaxPanel id="rapCadastro" runat="server" LoadingPanelID="lpaCadastro">
													<asp:Panel id="pnlErro" Runat="server"></asp:Panel>
													<table class="ContainerBordaLRB" cellspacing="0" cellpadding="5" width="100%" align="center"
														border="0">
														<tr>
															<td>
																<table cellspacing="0" cellpadding="0" width="100%" border="0">
																	<tr>
																		<td class="cadMsg" colspan="2">
																			<asp:label id="lblMensagem" runat="server">Formulário de Cadastro</asp:label>
																			<asp:validationsummary id="Validationsummary1" runat="server" DisplayMode="List"></asp:validationsummary></td>
																	</tr>
																</table>
																<table cellspacing="0" cellpadding="0" width="100%" border="0">
																	<tr>
																		<td class="cadlbl" colspan="2">
																			<asp:label id="lblSite" runat="server">Titulo <u>S</u>ite:</asp:label><BR>
																			<asp:textbox id="txtNomeSite" accessKey="S" runat="server" width="300px" MaxLength="100" CssClass="cadtxt"></asp:textbox></td>
																	</tr>
																	<tr>
																		<td class="cadlbl">
																			<asp:label id="lblLogErro" runat="server">Grava Log <u>E</u>rro:</asp:label><BR>
																			<asp:radiobuttonlist id="rblGravaLogErro" accessKey="E" runat="server" CssClass="cadrad" RepeatDirection="Horizontal">
																				<asp:ListItem Value="1" Selected="True">Sim</asp:ListItem>
																				<asp:ListItem Value="0">N&#227;o</asp:ListItem>
																			</asp:radiobuttonlist></td>
																		<td class="cadlbl">
																			<asp:label id="lblLogAcesso" runat="server">Grava Log <u>A</u>cesso:</asp:label><BR>
																			<asp:radiobuttonlist id="rblGravaLogAcesso" accessKey="A" runat="server" CssClass="cadrad" RepeatDirection="Horizontal">
																				<asp:ListItem Value="1" Selected="True">Sim</asp:ListItem>
																				<asp:ListItem Value="0">N&#227;o</asp:ListItem>
																			</asp:radiobuttonlist></td>
																	</tr>
																	<tr id="trAutenticaForm" runat="server">
																		<td class="cadlbl">
																			<asp:label id="lblAutoCadastramento" runat="server">Auto <u>C</u>adastramento:</asp:label><BR>
																			<asp:radiobuttonlist id="rblAutoCadastramento" accessKey="C" runat="server" CssClass="cadrad" RepeatDirection="Horizontal">
																				<asp:ListItem Value="1" Selected="True">Sim</asp:ListItem>
																				<asp:ListItem Value="0">N&#227;o</asp:ListItem>
																			</asp:radiobuttonlist></td>
																		<td class="cadlbl">
																			<asp:label id="lblCampoLogin" runat="server">Campo para <u>L</u>ogin:</asp:label><BR>
																			<asp:radiobuttonlist id="rblCampoLogin" accessKey="L" runat="server" CssClass="cadrad" RepeatDirection="Horizontal">
																				<asp:ListItem Value="1" Selected="True">Email</asp:ListItem>
																				<asp:ListItem Value="0">Login</asp:ListItem>
																			</asp:radiobuttonlist></td>
																	</tr>
																	<tr id="trAutenticaForm1" runat="server">
																		<td class="cadlbl">
																			<asp:label id="lblExpiraSenha" runat="server">E<u>x</u>pira Senha:</asp:label><BR>
																			<asp:radiobuttonlist id="rblExpiraSenha" accessKey="X" runat="server" CssClass="cadrad" RepeatDirection="Horizontal"
																				Width="100px">
																				<asp:ListItem Value="1" Selected="True">Sim</asp:ListItem>
																				<asp:ListItem Value="0">N&#227;o</asp:ListItem>
																			</asp:radiobuttonlist></td>
																		<td class="cadlbl">
																			<asp:label id="lblQuantDias" runat="server">Quantidade <u>d</u>ias:</asp:label><BR>
																			<cc1:fwmascara id="txtExpiraSenha" accessKey="D" runat="server" width="100px" MaxLength="3" CssClass="cadtxt"
																				Mascara="NUMERO"></cc1:fwmascara></td>
																	</tr>
																	<tr id="trAutenticaForm2" runat="server">
																		<td class="cadlbl" colspan="2">
																			<asp:label id="lblConfSenha" runat="server">Configuração Senha:</asp:label><BR>
																			&nbsp;&nbsp;&nbsp;
																			<asp:label id="lblTamanhoSenha" runat="server"><u>T</u>amanho:</asp:label>
																			<cc1:fwmascara id="txtTamanho" accessKey="T" runat="server" width="50px" MaxLength="2" CssClass="cadtxt"
																				Mascara="NUMERO"></cc1:fwmascara>&nbsp;
																			<asp:label id="lblQuantNumeros" runat="server"><u>Q</u>tde Numeros:</asp:label>
																			<cc1:fwmascara id="txtQtdeNumeros" accessKey="Q" runat="server" width="50px" MaxLength="2" CssClass="cadtxt"
																				Mascara="NUMERO"></cc1:fwmascara>&nbsp;
																			<asp:label id="lblQuantLetras" runat="server">Qtde Le<u>t</u>ras:</asp:label>
																			<cc1:fwmascara id="txtQtdeLetras" accessKey="T" runat="server" width="50px" MaxLength="2" CssClass="cadtxt"
																				Mascara="NUMERO"></cc1:fwmascara></td>
																	</tr>
																	<tr id="trAutenticaForm3" runat="server">
																		<td class="cadlbl" colspan="2">
																			<asp:label id="lblCaracterEspecial" runat="server">Ca<u>r</u>acter Especial:</asp:label>
																			&nbsp;&nbsp;&nbsp;<asp:radiobuttonlist id="rblEspecial" accessKey="R" runat="server" CssClass="cadrad" RepeatDirection="Horizontal"
																				Width="100px">
																				<asp:ListItem Value="1" Selected="True">Sim</asp:ListItem>
																				<asp:ListItem Value="0">N&#227;o</asp:ListItem>
																			</asp:radiobuttonlist></td>
																	</tr>
																	<tr>
																		<td class="cadlbl" colspan="2">
																			<asp:label id="lblOrigemEmail" runat="server"><u>O</u>rigem email:</asp:label><BR>
																			<asp:textbox id="txtOrigemEmail" accessKey="O" runat="server" width="300px" MaxLength="50" CssClass="cadtxt"></asp:textbox></td>
																	</tr>
																	<tr>
																		<td class="cadlbl" colspan="2">
																			<asp:label id="lblSMTP" runat="server">Servidor S<u>M</u>TP:</asp:label><BR>
																			<asp:textbox id="txtSMTP" accessKey="M" runat="server" width="300px" MaxLength="50" CssClass="cadtxt"></asp:textbox></td>
																	</tr>
																</table>
															</td>
														</tr>
													</table>
													<table cellspacing="0" cellpadding="0" width="100%" align="center" border="0">
														<tr>
															<td class="backBarraBotoes">
																<asp:button id="btnSalvar" runat="server" CssClass="cadbutton100" Text="Salvar"></asp:button>&nbsp;<INPUT class="cadbutton100" id="btnVoltar" onclick="javascript:window.navigate('%%PATH%%/Admin/Admin.aspx')"
																	type="button" value="Voltar" name="btnVoltar" runat="server"></td>
														</tr>
													</table>
												</rada:RadAjaxPanel>
												<rada:AjaxLoadingPanel id="lpaCadastro" width="75px" height="75px" Runat="server" Transparency="30" HorizontalAlign="Center">
													<asp:Image id="Image1" style="MARGIN-TOP: 100px" runat="server" AlternateText="Aguarde ..."
														ImageUrl="%%PATH%%/RadControls/Ajax/Skins/Default/Loading.gif"></asp:Image>
												</rada:AjaxLoadingPanel>
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
