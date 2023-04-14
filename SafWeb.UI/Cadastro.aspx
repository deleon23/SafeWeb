<%@ Register TagPrefix="cc1" Namespace="FrameWork.WebControl" Assembly="FrameWork.WebControl" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="Cadastro.aspx.vb" Inherits="FrameWork.UI.Cadastro"%>
<%@ Register TagPrefix="rada" Namespace="Telerik.WebControls" Assembly="RadAjax.Net2" %>
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
			<table cellspacing="0" cellpadding="0" width="100%" align="center" border="0">
				<tr>
					<td>
						<cc1:FWServerControl id="Fwservercontrol3" runat="server" Controle="Pagebanner"></cc1:FWServerControl></td>
				</tr>
				<tr>
					<td valign="top">
						<table class="backPadrao" cellspacing="0" cellpadding="0" width="100%" align="center" height="424">
							<tr>
								<td class="BordaDefaultLRB" align="center" valign="top">
									<!-- cccccccccccc START: Titulo  ccccccccccc -->
									<!-- cccccccccccc START: Panel AJAX ccccccccccc--><rada:radajaxpanel id="RadAjaxPanel1" runat="server" LoadingPanelID="lpaCadastro" ClientEvents-OnRequestStart="OnRequestStart"><!-- cccccccccccc START: Conteudo  ccccccccccc -->
										<asp:Panel id="pnlErro" Runat="server"></asp:Panel>
										<table cellspacing="0" cellpadding="0" width="740" align="center" border="0">
											<tr>
												<td class="CantoEsq"></td>
												<td class="cadBarraTitulo" width="720" height="15">
													<asp:Label id="lblTitulo" runat="server">Cadastro de Usuários</asp:Label></td>
												<td class="CantoDir"></td>
											</tr>
											<tr> <!-- cccccccccccc START: Conteudo  ccccccccccc -->
												<td valign="top" align="center" colspan="3">
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
																<table id="dtgItemStyle" cellspacing="2" cellpadding="2" width="100%" align="center" border="0">
																	<tr>
																		<td class="cadlbl" style="WIDTH: 150px">
																			<asp:label id="lblNome" runat="server"><u>N</u>ome:</asp:label><BR>
																			<asp:textbox id="txtNome" accessKey="N" runat="server" width="300px" MaxLength="50" CssClass="cadtxt"></asp:textbox>
																			<asp:requiredfieldvalidator id="rfvNome" runat="server" CssClass="nomecampos" Display="Dynamic" ErrorMessage="- Campo Obrigatório: Nome"
																				ControlToValidate="txtNome">*</asp:requiredfieldvalidator></td>
																		<td class="cadlbl" style="WIDTH: 150px">
																			<asp:label id="lblEmail" runat="server"><u>E</u>mail:</asp:label><BR>
																			<asp:textbox id="txtEmail" accessKey="E" runat="server" width="300px" MaxLength="50" CssClass="cadtxt"></asp:textbox>
																			<asp:regularexpressionvalidator id="revEmail" runat="server" CssClass="nomecampos" Display="Dynamic" ErrorMessage="- O formato do e-mail está incorreto."
																				ControlToValidate="txtEmail" EnableClientScript="True" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:regularexpressionvalidator>
																			<asp:requiredfieldvalidator id="rfvEmail" runat="server" CssClass="nomecampos" Display="Dynamic" ErrorMessage="- Campo Obrigatório: Email"
																				ControlToValidate="txtEmail">*</asp:requiredfieldvalidator></td>
																	</tr>
																	<tr id="trLogin" runat="server">
																		<td class="cadlbl" style="WIDTH: 150px">
																			<asp:label id="lblLogin" runat="server"><u>L</u>ogin:</asp:label><BR>
																			<asp:textbox id="txtLogin" accessKey="L" runat="server" width="300px" MaxLength="50" CssClass="cadtxt"></asp:textbox>
																			<asp:regularexpressionvalidator id="revLogin" runat="server" CssClass="nomecampos" Display="Dynamic" ErrorMessage="- O formato do e-mail do Login está incorreto."
																				ControlToValidate="txtEmail" EnableClientScript="True" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:regularexpressionvalidator>
																			<asp:requiredfieldvalidator id="rfvLogin" runat="server" CssClass="nomecampos" Display="Dynamic" ErrorMessage="- Campo Obrigatório: Login"
																				ControlToValidate="txtLogin">*</asp:requiredfieldvalidator></td>
																		<td class="cadlbl"><BR>
																		</td>
																	</tr>
																	<tr id="trIdioma" runat="server">
																		<td class="cadlbl" colspan="2">
																			<asp:label id="lblIdioma" runat="server"><u>I</u>dioma:</asp:label><BR>
																			<asp:dropdownlist id="ddlIdioma" accessKey="I" runat="server" CssClass="cadddl"></asp:dropdownlist></td>
																	</tr>
																</table>
																<table id="tblSenha" cellspacing="2" cellpadding="2" width="100%" border="0" runat="server">
																	<tr>
																		<td class="cadlbl" style="WIDTH: 150px">
																			<asp:label id="lblSenha" runat="server"><u>S</u>enha:</asp:label><BR>
																			<asp:textbox id="txtSenha" accessKey="S" runat="server" width="150px" MaxLength="50" CssClass="cadtxt"
																				TextMode="Password"></asp:textbox>
																			<asp:requiredfieldvalidator id="rfvSenha" runat="server" CssClass="nomecampos" Display="Dynamic" ErrorMessage="- Campo Obrigatório: Senha "
																				ControlToValidate="txtSenha">*</asp:requiredfieldvalidator></td>
																	</tr>
																	<tr>
																		<td class="cadlbl" style="WIDTH: 150px">
																			<asp:label id="lblSenhaConfirmacao" runat="server">Senha <u>C</u>onfirmação:</asp:label><BR>
																			<asp:textbox id="txtSenhaConfirmacao" accessKey="C" runat="server" width="150px" MaxLength="50"
																				CssClass="cadtxt" TextMode="Password"></asp:textbox>
																			<asp:requiredfieldvalidator id="rfvConfirmacaoSenha" runat="server" CssClass="nomecampos" Display="Dynamic"
																				ErrorMessage="- Campo Obrigatório: Senha Confirmação" ControlToValidate="txtSenhaConfirmacao">*</asp:requiredfieldvalidator>
																			<asp:comparevalidator id="cpvSenha" runat="server" CssClass="nomecampos" Display="Dynamic" ErrorMessage="A senha não confere!!!"
																				ControlToValidate="txtSenhaConfirmacao" ControlToCompare="txtSenha">*</asp:comparevalidator></td>
																	</tr>
																</table>
															</td>
														</tr>
													</table>
													<table cellspacing="0" cellpadding="0" width="100%" align="center" border="0">
														<tr>
															<td class="backBarraBotoes">
																<asp:button id="btnSalvar" runat="server" CssClass="cadbutton100" Text="Salvar"></asp:button>&nbsp;<INPUT class="cadbutton100" id="btnVoltar" onclick="javascript:window.navigate('Default.aspx')" type="button" value="Voltar" name="btnVoltar" runat="server"></td>
														</tr>
													</table>
												</td>
											</tr>
										</table> <!-- cccccccccccc END: Panel Cadastro ccccccccccc-->
									</rada:radajaxpanel>
									<rada:AjaxLoadingPanel id="lpaCadastro" width="75px" height="75px" Runat="server" Transparency="30" HorizontalAlign="Center">
										<asp:Image id="Image1" style="MARGIN-TOP: 100px" runat="server" AlternateText="Aguarde ..."
											ImageUrl="%%PATH%%/RadControls/Ajax/Skins/Default/Loading.gif"></asp:Image>
									</rada:AjaxLoadingPanel>
									<!-- cccccccccccc END: Panel AJAX ccccccccccc-->
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td><cc1:fwservercontrol id="Fwservercontrol2" runat="server" Controle="Rodape"></cc1:fwservercontrol></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
