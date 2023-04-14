<%@ Register TagPrefix="rada" Namespace="Telerik.WebControls" Assembly="RadAjax.Net2" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ConfMenu.aspx.vb" Inherits="FrameWork.UI.ConfMenu" %>
<%@ Register TagPrefix="cc1" Namespace="FrameWork.WebControl" Assembly="FrameWork.WebControl" %>
<%@ Register TagPrefix="radt" Namespace="Telerik.WebControls" Assembly="RadTreeView.Net2" %>
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
									<!-- cccccccccccc START: Panel AJAX ccccccccccc--><rada:radajaxpanel id="RadAjaxPanel1" runat="server" LoadingPanelID="lpaCadastro" ><!-- cccccccccccc START: Panel Listagem ccccccccccc-->
										<asp:Panel id="pnlErro" Runat="server"></asp:Panel>
										<table class="MarginTopBarra"  cellspacing="0" cellpadding="0" width="760" align="center" border="0">
											<tr>
												<td class="CantoEsq"></td>
												<td class="cadBarraTitulo" width="100%" height="15">
													<img src="%%PATH%%/Imagens/comum/bulletbarra.gif" align="middle" />
													<asp:Label id="lblTitulo" runat="server">Cadastro de Itens de Menu</asp:Label></td>
												<td class="CantoDir"></td>
											</tr>
											<tr> <!-- cccccccccccc START: Conteudo  ccccccccccc -->
												<td valign="top" align="center" colspan="3">
													<table class="ContainerBordaLRB" cellspacing="0" cellpadding="5" width="100%" align="center"
														border="0">
														<tr>
															<td>
																<table cellspacing="4" cellpadding="0" width="100%" border="0">
																	<tr>
																		<td valign="top" width="40%">
																			<table width="100%">
																				<tr>
																					<td valign="top" align="left">
																						<radt:radtreeview id="rtvMenu" runat="server" Skin="Makesys"></radt:radtreeview></td>
																				</tr>
																			</table>
																		</td>
																		<td id="linhadivisoria" valign="top" width="1%" background="%%PATH%%/imagens/admin/linha-vertical.gif"><IMG src="%%PATH%%/imagens/transparente.gif" width="10"></td>
																		<td valign="top" width="59%">
																			<asp:panel id="pnledit" runat="server" Visible="False">
																				<table cellspacing="2" cellpadding="0" width="100%" border="0">
																					<tr>
																						<td class="cadlbl" width="120">
																							<asp:Label id="lblTituloCadastro" runat="server"><u>T</u>ítulo:</asp:Label><BR>
																							<asp:TextBox id="txttitulo" accessKey="T" runat="server" ReadOnly="True" CssClass="cadtxt" Width="270px"></asp:TextBox></td>
																					</tr>
																					<tr>
																						<td class="cadlbl" style="HEIGHT: 13px" width="120">
																							<asp:Label id="lblImagem" runat="server"><u>I</u>magem:</asp:Label><BR>
																							<asp:DropDownList id="ddlImagem" accessKey="I" runat="server" CssClass="cadddl" DataTextField="ARQUIVO"></asp:DropDownList></td>
																					</tr>
																					<tr>
																						<td class="cadlbl" width="120">
																							<asp:Label id="lblDescricao" runat="server"><u>D</u>escrição</asp:Label><BR>
																							<asp:TextBox id="txtDescricao" accessKey="D" runat="server" Visible="False" CssClass="cadtxt"
																								Width="270px" MaxLength="150"></asp:TextBox></td>
																					</tr>
																					<tr>
																						<td class="cadlbl" style="HEIGHT: 36px" width="120">
																							<asp:Label id="lblFormaAbertura" runat="server"><u>F</u>orma de Abertura:</asp:Label><BR>
																							<asp:DropDownList id="ddlFormaAbertura" accessKey="F" runat="server" CssClass="cadddl">
																								<asp:ListItem Value="2">Janela Pai (parent)</asp:ListItem>
																								<asp:ListItem Value="1" Selected="True">Mesma Janela (self)</asp:ListItem>
																								<asp:ListItem Value="3">Nova Janela (blank)</asp:ListItem>
																								<asp:ListItem Value="4">Janela Raiz (top)</asp:ListItem>
																							</asp:DropDownList><!--<asp:ListItem Value="5">Frame</asp:ListItem>
																								<asp:ListItem Value="6">Frame em PopUp</asp:ListItem>--></td>
																					</tr>
																					<tr>
																						<td class="cadlbl" width="120">
																							<table id="Table11" cellspacing="2" cellpadding="2" border="0">
																								<tr>
																									<td class="cadlbl" colspan="3">
																										<asp:label id="lblGrupos" runat="server">Grupo que acessam:</asp:label></td>
																								</tr>
																								<tr>
																									<td>
																										<asp:listbox id="lstGruposRemovidos" runat="server" CssClass="cadlstBox" Width="150px" Rows="8"
																											SelectionMode="Multiple"></asp:listbox></td>
																									<td align="center">&nbsp;&nbsp;
																										<asp:button id="btnGruAdiciona" runat="server" CssClass="cadbutton" Width="30px" Text=">>" CausesValidation="False"></asp:button>&nbsp;&nbsp;<BR>
																										<BR>
																										&nbsp;&nbsp;
																										<asp:button id="btnGruRemove" runat="server" CssClass="cadbutton" Width="30px" Text="<<" CausesValidation="False"></asp:button>&nbsp;&nbsp;</td>
																									<td>
																										<asp:listbox id="lstGruposAdicionados" runat="server" CssClass="cadlstBox" Width="150px" Rows="8"
																											SelectionMode="Multiple"></asp:listbox></td>
																								</tr>
																							</table>
																						</td>
																					</tr>
																					<tr>
																						<td class="cadlbl" style="HEIGHT: 33px" width="120">
																							<asp:Label id="lblLink" runat="server">Tipo de <u>L</u>ink:</asp:Label><BR>
																							<asp:DropDownList id="ddlLink" accessKey="L" runat="server" CssClass="cadddl" AutoPostBack="True">
																								<asp:ListItem Value="0">Informar URL</asp:ListItem>
																								<asp:ListItem Value="1">Modulos</asp:ListItem>
																								<asp:ListItem Value="3" Selected="True">Sem Link</asp:ListItem>
																							</asp:DropDownList></td>
																					</tr>
																					<tr id="trURL" runat="server" visible="false">
																						<td class="cadlbl" width="120">
																							<asp:Label id="lblURL" runat="server"><u>U</u>RL:</asp:Label><BR>
																							<asp:TextBox id="txtURL" accessKey="U" runat="server" CssClass="cadtxt" Width="270px" MaxLength="100"></asp:TextBox></td>
																					</tr>
																					<tr id="trMODULOS" runat="server" visible="false">
																						<td class="cadlbl" style="HEIGHT: 33px" width="120">
																							<asp:Label id="lblModulos" runat="server"><u>M</u>odulos:</asp:Label><BR>
																							<asp:DropDownList id="ddlModulos" accessKey="M" runat="server" CssClass="cadddl" AutoPostBack="True"></asp:DropDownList></td>
																					</tr>
																					<tr id="trPARAMETRO" runat="server" visible="false">
																						<td class="cadlbl" width="120">
																							<asp:Label id="lblParametro" runat="server"><u>P</u>arâmetros:</asp:Label><BR>
																							<asp:TextBox id="txtParametro" accessKey="P" runat="server" CssClass="cadtxt" Width="270px"></asp:TextBox></td>
																					</tr>
																					<tr>
																						<td class="cadlbl" width="120">
																							<asp:Label id="lblErro" runat="server" Visible="False" Cssclass="cadMsg" EnableViewState="False"></asp:Label></td>
																					</tr>
																					<tr id="trTRANSLATE" runat="server" visible="false">
																						<td class="cadlbl" width="120"><asp:button id="btnTraduzirMenu" runat="server" CssClass="cadbutton100" Text="Traduzir"></asp:button></td>
																					</tr>
																					<tr>
																						<td class="cadlbl" width="120"></td>
																					</tr>
																				</table>
																			</asp:panel></td>
																	</tr>
																</table>
															</td>
														</tr>
													</table>
													<table cellspacing="0" cellpadding="0" width="100%" align="center" border="0">
														<tr>
															<td class="backBarraBotoes">
																<asp:button id="btnOK" runat="server" CssClass="cadbutton100" Text="OK"></asp:button>&nbsp;
																<asp:button id="btnCancela" runat="server" CssClass="cadbutton100" Text="Cancela" CausesValidation="False"></asp:button>&nbsp;<INPUT class="cadbutton100" id="btnVoltar" onclick="javascript:window.navigate('%%PATH%%/Admin/Menu/CadMenu.aspx')"
																	type="button" value="Voltar" name="btnVoltar" runat="server"></td>
														</tr>
													</table>
												</td>
											</tr>
										</table>
									</rada:radajaxpanel>
									<rada:AjaxLoadingPanel id="lpaCadastro" width="75px" height="75px" Runat="server" Transparency="30" HorizontalAlign="Center">
										<asp:Image id="Image1" style="MARGIN-TOP: 100px" runat="server" ImageUrl="%%PATH%%/RadControls/Ajax/Skins/Default/Loading.gif"
											AlternateText="Aguarde ..."></asp:Image>
									</rada:AjaxLoadingPanel>
									<!-- cccccccccccc END: Panel AJAX ccccccccccc-->
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
