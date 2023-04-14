<%@ Register TagPrefix="cc1" Namespace="FrameWork.WebControl" Assembly="FrameWork.WebControl" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="Login.aspx.vb" Inherits="FrameWork.UI.Login"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html >

	<head runat="server">
		<title>%%SITE%%</title>
		<link href="%%PATH%%/Estilos/FrameWork.css" type="text/css" rel="stylesheet"/>
        <script language="javascript">
            //new cf_style()
        </script>
	</head>
	<body>
		<form id="Form1" method="post" runat="server">
			<table cellspacing="0" cellpadding="0" width="778" align="center" border="0">
				<tr>
					<td><cc1:FWServerControl id="FWServerControl2" runat="server" Arquivo="/Modulos/FrameWork/CabecalhoLogin.ascx"
                        
							Controle="Pagebanner" />                       
                        </td>
				</tr>
				<tr>
					<td height="424">
						<table class="backPadrao" height="426" cellspacing="0" cellpadding="0" width="100%" align="center">
							<tr>
								<td class="BordaDefaultLRB" valign="middle" align="center">
									<!-- cccccccccccc START: Panel AJAX ccccccccccc-->
										<asp:Panel id="pnlErro" Runat="server"></asp:Panel>
										<table cellspacing="0" cellpadding="0" width="402" align="center" bgColor="#ffffff" border="0">
											<tr>
												<td class="tituloBoxLogin" width="192" background="~/imagens/Admin/login1.gif" height="25">
													<asp:Label id="lblTitulo" CssClass="tituloBoxLogin" runat="server">» Login</asp:Label></td>
												<td width="210" height="25"><IMG height="25" alt="" src="~/imagens/Admin/login2.gif" width="210" border="0"
														name="login2"></td>
											</tr>
											<tr>
												<td width="192" height="32"><IMG alt="" src="~/imagens/Admin/login3.gif" border="0" name="login3"></td>
												<td width="210" height="32"><IMG alt="" src="~/imagens/Admin/login4.gif" border="0" name="login4"></td>
											</tr>
											<tr>
												<td class="backBoxLogin" width="402" colspan="2">
													<table cellspacing="0" cellpadding="3" width="90%" align="center" border="0">
														<tr>
															<td class="lblLogin" align="right">
																<asp:Label id="lblLogin" runat="server">Login:</asp:Label></td>
															<td>
																<asp:textbox id="signonlogin" runat="server" Width="203px" CssClass="txtLogin" MaxLength="50"></asp:textbox></td>
														</tr>
														<tr>
															<td class="lblLogin" align="right">
																<asp:Label id="lblSenha" runat="server">Senha:</asp:Label></td>
															<td>
																<asp:textbox id="signonsenha" runat="server" Width="203px" CssClass="txtLogin" MaxLength="10"
																	TextMode="Password"></asp:textbox></td>
														</tr>
														<tr>
															<td class="lblLogin" align="right">
																<asp:Label id="lblNovaSenha" runat="server">Nova senha:</asp:Label></td>
															<td>
																<asp:textbox id="signonnsenha" runat="server" Width="203px" CssClass="txtLogin" MaxLength="10"
																	TextMode="Password"></asp:textbox></td>
														</tr>
														<tr>
															<td class="lblLogin" align="right">
																<asp:Label id="lblConfirmarSenha" runat="server">Confirmar nova senha:</asp:Label></td>
															<td>
																<asp:textbox id="signonconfsenha" runat="server" Width="203px" CssClass="txtLogin" MaxLength="10"
																	TextMode="Password"></asp:textbox></td>
														</tr>
														<tr>
															<td></td>
															<td>
																<asp:button id="btnLogin" runat="server" cssclass="buttonLogin100" Text="LOGIN"></asp:button>&nbsp;
																<asp:button id="btnCadastro" runat="server" cssclass="buttonLogin100" Text="CADASTRE-SE" CausesValidation="true" Visible="False"></asp:button></td>
														</tr>
													</table>
													<table class="textoBoxLogin" cellspacing="0" cellpadding="3" width="95%" align="center" border="0">
														<tr id="trMensagem" runat="server">
															<td align="center">
																<asp:label id="lblErro" runat="server"></asp:label></td>
														</tr>
														<tr>
															<td align="center">
																<asp:requiredfieldvalidator id="rfvLogin" runat="server" Display="Dynamic" ControlToValidate="signonlogin" ErrorMessage="O e-mail do usuário deve ser informado!<br>"></asp:requiredfieldvalidator>
																<asp:regularexpressionvalidator id="revEmail" runat="server" Display="Dynamic" ControlToValidate="signonlogin" ErrorMessage="O formato do e-mail está incorreto.<br>"
																	ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" EnableClientScript="True"></asp:regularexpressionvalidator>
																<asp:requiredfieldvalidator id="rfvSenha" runat="server" Display="Dynamic" ControlToValidate="signonsenha" ErrorMessage="A senha deve ser informada!"></asp:requiredfieldvalidator>
																<asp:comparevalidator id="cpvSenha" runat="server" Display="Dynamic" ControlToValidate="signonnsenha"
																	ErrorMessage="Para alterar a senha digite a nova e a confirmação !" ControlToCompare="signonconfsenha"></asp:comparevalidator>
																<asp:comparevalidator id="cpvConfSenha" runat="server" Display="Dynamic" ControlToValidate="signonconfsenha"
																	ErrorMessage="A nova senha e sua confirmação devem ser idênticas !" ControlToCompare="signonnsenha"></asp:comparevalidator></td>
														</tr>
													</table>
													<table cellspacing="0" cellpadding="3" width="95%" align="center" border="0">
														<tr>
															<td class="textoLogin">
																<asp:Label id="lblEsqueciSenha" runat="server">» Para receber sua senha por e-mail digite o e-mail e</asp:Label>
																<asp:linkbutton id="lnkEsqueci" runat="server" cssclass="LinkLogin" CausesValidation="False">clique 
															aqui</asp:linkbutton>.<BR>
															</td>
														</tr>
													</table>
												</td>
											</tr>
											<tr>
												<td width="402" colspan="2" height="9"><img height="9" alt="" src="~/imagens/Admin/login_base.gif" width="402" border="0"
														name="login_base"></td>
											</tr>
										</table> <!-- cccccccccccc END: Panel Cadastro ccccccccccc-->
									<!-- cccccccccccc END: Panel AJAX ccccccccccc--></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td><cc1:fwservercontrol id="FWServerControl1" runat="server" Controle="Rodape" Arquivo="/Modulos/FrameWork/RodapeBrinks.ascx"></cc1:fwservercontrol></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
