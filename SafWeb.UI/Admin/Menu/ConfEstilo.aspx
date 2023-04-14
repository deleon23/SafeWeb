<%@ Register TagPrefix="radG" Namespace="Telerik.WebControls" Assembly="RadGrid.Net2" %>
<%@ Register TagPrefix="rada" Namespace="Telerik.WebControls" Assembly="RadAjax.Net2" %>
<%@ Register TagPrefix="cc1" Namespace="FrameWork.WebControl" Assembly="FrameWork.WebControl" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ConfEstilo.aspx.vb" Inherits="FrameWork.UI.ConfEstilo" %>
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
									<!-- cccccccccccc START: Panel AJAX ccccccccccc--><rada:radajaxpanel id="RadAjaxPanel1" runat="server" LoadingPanelID="lpaCadastro" ClientEvents-OnRequestStart="OnRequestStart"><!-- cccccccccccc START: Panel Listagem ccccccccccc-->
										<asp:Panel id="pnlErro" Runat="server"></asp:Panel>
										<table class="MarginTopBarra"  cellspacing="0" cellpadding="0" width="760" align="center" border="0">
											<tr>
												<td class="CantoEsq"></td>
												<td class="cadBarraTitulo" width="100%" height="15">
													<img src="%%PATH%%/Imagens/comum/bulletbarra.gif" align="middle" />
													<asp:Label id="lblTitulo" runat="server">Cadastro de Estilos</asp:Label></td>
												<td class="CantoDir"></td>
											</tr>
											<tr> <!-- cccccccccccc START: Conteudo  ccccccccccc -->
												<td valign="top" align="center" colspan="3"><!-- configuração Estilo -->
													<asp:Panel id="pnlListagem" runat="server">
														<table class="ContainerBordaLRB" cellspacing="0" cellpadding="5" width="100%" align="center"
															border="0">
															<tr>
																<td class="cadMsg" align="left">
																	<asp:label id="lblMensagemListagem" runat="server">Pesquisa</asp:label></td>
															</tr>
															<tr>
																<td>
																	<table height="22" cellspacing="0" cellpadding="0" width="100%" border="0">
																		<tr>
																			<td id="ColunaExportacao" align="right" runat="server">
																				<asp:ImageButton id="btnExportWord" runat="server" ImageUrl="%%PATH%%/imagens/icones/ico_doc.gif"></asp:ImageButton>&nbsp;
																				<asp:ImageButton id="btnExportExcel" runat="server" ImageUrl="%%PATH%%/imagens/icones/ico_xls.gif"></asp:ImageButton></td>
																		</tr>
																		<tr>
																			<td>
																				<radg:radgrid id="radGridDados" runat="server" CssClass="dtg" Width="100%" AutoGenerateColumns="False"
																					AllowPaging="True" AllowSorting="True" Gridlines="None" Skin="None">
																					<GroupHeaderItemStyle CssClass="dtgGroupHeader"></GroupHeaderItemStyle>
																					<GroupPanel Text="">
																						<PanelStyle CssClass="dtgGroupPanel"></PanelStyle>
																						<PanelItemsStyle CssClass="dtgGroupPanelItem"></PanelItemsStyle>
																					</GroupPanel>
																					<AlternatingItemStyle CssClass="dtgItemStyle"></AlternatingItemStyle>
																					<ItemStyle CssClass="dtgItemStyleAlternate"></ItemStyle>
																					<HeaderStyle CssClass="dtgHeaderStyle"></HeaderStyle>
																					<MasterTableView>
																						<NoRecordsTemplate>
																							<div>
																								<table width="100%" height="300" cellpadding="0" cellspacing="0">
																									<tr>
																										<td><% = FrameWork.BusinessLayer.Idioma.BLIdiomas.TraduzirMensagens(FrameWork.Model.Idioma.Mensagens.CONSULTA_SEM_RETORNO) %>
																										</td>
																									</tr>
																								</table>
																							</div>
																						</NoRecordsTemplate>
																						<PagerTemplate>
																							<table height="35" cellspacing="0" class="pag" cellpadding="5" width="100%" align="center"
																								border="0">
																								<tr>
																									<td valign="center" align="center">
																									    <asp:Panel runat="server" ID="pnlPaginaAtual" DefaultButton="btnIr">
																									        <asp:ImageButton runat="server" ID="imgPrimeira" CssClass="pagImg" CausesValidation="false" CommandArgument="First" CommandName="Page" ImageUrl="~/imagens/icones/ico_setaesq.gif" />
																										    <asp:linkbutton id="btnPrimeira" runat="server" CssClass="pagLink" CausesValidation="False" CommandArgument="First" Enabled="True" CommandName="Page">Primeira</asp:linkbutton>
																											<asp:ImageButton runat="server" ID="imgAnterior" CssClass="pagImg" CausesValidation="false" CommandArgument="Prev" CommandName="Page" ImageUrl="~/imagens/icones/ico_setaesq2.gif" />
																										    <asp:linkbutton id="btnAnterior" CommandName="Page" runat="server" CssClass="pagLink" CausesValidation="False" CommandArgument="Prev" Enabled="True">Anterior</asp:linkbutton>&nbsp;&nbsp;
																										    <asp:Label cssclass="paglbl" id="lblPaginaDescricao" runat="server">| Página:</asp:Label>
																										    <cc1:FWMascara id="txtPagina" Text='<%# cint(DataBinder.Eval(Container, "OwnerTableView.CurrentPageIndex")) + 1 %>' runat="server" CssClass="pagtxtbox" Width="50px" MaxLength="4" AutoPostBack="False" Mascara="NUMERO"></cc1:FWMascara>
																										    <asp:Label cssclass="paglbl" id="lblPaginaDe" runat="server">de</asp:Label>
																										    <asp:label cssclass="paglbl" id="lblPagina" runat="server" Enabled="True"><%#Right("00000" + DataBinder.Eval(Container, "Paging.PageCount").ToString(), 4)%></asp:label>
																										    <asp:linkbutton id="btnIr" runat="server" CssClass="pagLink" CommandName="IrPagina">Ir</asp:linkbutton><span class="paglbl">&nbsp;|&nbsp;</span>
																										    <asp:linkbutton id="btnProxima" runat="server" CssClass="pagLink" CausesValidation="False" CommandName="Page" CommandArgument="Next" Enabled="True">Próxima</asp:linkbutton>
																											<asp:ImageButton runat="server" ID="imgProxima" CssClass="pagImg" CausesValidation="false" CommandArgument="Next" CommandName="Page" ImageUrl="~/imagens/icones/ico_setadir2.gif" />
																										    <asp:linkbutton id="btnUltima" runat="server" CssClass="pagLink" CausesValidation="False" CommandName="Page" CommandArgument="Last" Enabled="True">Última</asp:linkbutton>
																										    <asp:ImageButton runat="server" ID="imgUltima" CssClass="pagImg" CausesValidation="false" CommandArgument="Last" CommandName="Page" ImageUrl="~/imagens/icones/ico_setadir.gif" />
																										</asp:Panel>
																									</td>
																								</tr>
																							</table>
																						</PagerTemplate>
																					
																					<Columns>
																						<radg:GridBoundColumn HeaderText="Código" SortExpression="MST_N_CODIGO" DataField="MST_N_CODIGO" UniqueName="MST_N_CODIGO">
																							<ItemStyle Width="10%" HorizontalAlign="center"></ItemStyle>
																						</radg:GridBoundColumn>
																						<radg:GridBoundColumn HeaderText="Estilos" SortExpression="MST_C_TITULO" DataField="MST_C_TITULO" UniqueName="MST_C_TITULO">
																							<ItemStyle HorizontalAlign="Left"></ItemStyle>
																						</radg:GridBoundColumn>
																						<radg:GridTemplateColumn UniqueName="TemplateColumn" visible="false">
																							<ItemStyle Width="5%" HorizontalAlign="Center"></ItemStyle>
																							<ItemTemplate>
																								<asp:ImageButton id=btnEditar AlternateText="Editar" runat="server" CausesValidation="False" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "MST_N_CODIGO") %>' ToolTip="Editar" ImageUrl="%%PATH%%/imagens/icones/ico_editar.gif" CommandName="Editar">
																								</asp:ImageButton>
																							</ItemTemplate>
																						</radg:GridTemplateColumn>
																						<radg:GridButtonColumn CommandName="Excluir" ConfirmText="%%REGISTRO_CONFIRMA_EXCLUSAO%%" UniqueName="DeleteColumn"
																							ButtonType="ImageButton" ImageUrl="%%PATH%%/imagens/icones/ico_deletar.gif">
																							<ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
																						</radg:GridButtonColumn>
																					</Columns>
																					</MasterTableView>
																				</radg:radgrid></td>
																		</tr>
																	</table>
																</td>
															</tr>
														</table>
														<table cellspacing="0" cellpadding="0" width="100%" align="center" border="0">
															<tr>
																<td class="backBarraBotoes">
																	<asp:Button id="btnNovoListagem" runat="server" CssClass="cadbutton100" CausesValidation="False"
																		Text="Novo"></asp:Button>&nbsp;<INPUT class="cadbutton100" id="btnVoltarListagem" onclick="javascript:window.navigate('%%PATH%%/Admin/Admin.aspx')"
																		type="button" value="Voltar" name="btnVoltarListagem" runat="server"></td>
															</tr>
														</table>
													</asp:Panel>
													<asp:Panel id="pnlCadastro" runat="server" Visible="false">
														<table class="ContainerBordaLRB" cellspacing="0" cellpadding="5" width="100%" align="center"
															border="0">
															<tr>
																<td class="cadMsg">
																	<asp:label id="lblMensagemCadastro" runat="server">Formulário de Cadastro</asp:label>
																	<asp:validationsummary id="Validationsummary1" runat="server" DisplayMode="List"></asp:validationsummary></td>
															</tr>
															<tr>
																<td class="cadlbl">
																	<asp:Label id="lblDescricao" runat="server" cssclass="cadlbl"><u>T</u>ítulo:</asp:Label><BR>
																	<asp:textbox id="txtTitEstilo" accessKey="T" runat="server" CssClass="cadtxt" Width="250px" MaxLength="15"></asp:textbox>
																	<asp:requiredfieldvalidator id="rfvTitulo" runat="server" CssClass="cadrad" Display="Dynamic" Font-Bold="False"
																		ControlToValidate="txttitestilo" ErrorMessage="- Campo Obrigatório: Título">*</asp:requiredfieldvalidator></td>
															</tr>
															<tr>
																<td class="cadlbl">
																	<asp:Label id="lblPadraoRaiz" runat="server" cssclass="cadlbl">Padrão de ítens <u>r</u>aiz:</asp:Label><BR>
																	<asp:DropDownList id="ddlPadraoRaiz" accessKey="R" runat="server" CssClass="cadddl" DataTextField="MPD_C_TITULO"
																		DataValueField="MPD_N_CODIGO"></asp:DropDownList>
																	<asp:requiredfieldvalidator id="rfvPadraoRaiz" runat="server" CssClass="cadrad" Display="Dynamic" Font-Bold="False"
																		ControlToValidate="ddlPadraoRaiz" ErrorMessage="- Seleção Obrigatória: Padrão de ítens raiz" InitialValue="0">*</asp:requiredfieldvalidator></td>
															</tr>
															<tr>
																<td class="cadlbl">
																	<asp:Label id="lblRadraoSub" runat="server" cssclass="cadlbl">Padrão de <u>s</u>ubmenus:</asp:Label><BR>
																	<asp:DropDownList id="ddlPadraoSub" accessKey="S" runat="server" CssClass="cadddl" DataTextField="MPD_C_TITULO"
																		DataValueField="MPD_N_CODIGO"></asp:DropDownList>
																	<asp:requiredfieldvalidator id="rfvPadraoSub" runat="server" CssClass="cadrad" Display="Dynamic" Font-Bold="False"
																		ControlToValidate="ddlPadraoSub" ErrorMessage="- Seleção Obrigatória: Padrão de submenus" InitialValue="0">*</asp:requiredfieldvalidator></td>
															</tr>
															<tr>
																<td class="cadlbl">
																	<asp:Label id="lblPadraosel" runat="server" cssclass="cadlbl">Padrão do ítem s<u>e</u>lecionado:</asp:Label><BR>
																	<asp:DropDownList id="ddlPadraoSel" accessKey="E" runat="server" CssClass="cadddl" DataTextField="MPD_C_TITULO"
																		DataValueField="MPD_N_CODIGO"></asp:DropDownList>
																	<asp:requiredfieldvalidator id="rfvPadraoSel" runat="server" CssClass="cadrad" Display="Dynamic" Font-Bold="False"
																		ControlToValidate="ddlPadraoSel" ErrorMessage="- Seleção Obrigatória: Padrão do ítem selecionado" InitialValue="0">*</asp:requiredfieldvalidator></td>
															</tr>
															<tr>
																<td class="cadlbl">
																	<asp:Label id="lblModoSub" runat="server" cssclass="cadlbl">Modo de <u>a</u>bertura do submenu:</asp:Label><BR>
																	<asp:RadioButtonList id="chkClick" accessKey="A" runat="server" CssClass="cadrad" Width="192px" RepeatDirection="Horizontal">
																		<asp:ListItem Value="0" Selected="True">MouseOver</asp:ListItem>
																		<asp:ListItem Value="1">Click</asp:ListItem>
																	</asp:RadioButtonList></td>
															</tr>
														</table>
														<table cellspacing="0" cellpadding="0" width="100%" align="center" border="0">
															<tr>
																<td class="backBarraBotoes">
																	<asp:Button id="btnSalvarCadastro" runat="server" CssClass="cadbutton100" Text="Salvar"></asp:Button>&nbsp;
																	<asp:Button id="btnNovoCadastro" runat="server" CssClass="cadbutton100" CausesValidation="False"
																		Text="Novo"></asp:Button>&nbsp;
																	<asp:button id="btnVoltarCadastro" runat="server" CssClass="cadbutton100" CausesValidation="False"
																		Text="Voltar"></asp:button></td>
															</tr>
														</table> <!-- cccccccccccc END: Conteudo  ccccccccccc --></asp:Panel></td>
											</tr>
										</table> <!-- cccccccccccc END: Panel Cadastro ccccccccccc-->
									</rada:radajaxpanel>
									<rada:AjaxLoadingPanel id="lpaCadastro" width="75px" height="75px" Runat="server" Transparency="30" HorizontalAlign="Center">
										<asp:Image id="Image1" style="MARGIN-TOP: 100px" runat="server" ImageUrl="~/RadControls/Ajax/Skins/Default/Loading.gif"
											AlternateText="Aguarde ..."></asp:Image>
									</rada:AjaxLoadingPanel>
									<!-- cccccccccccc END: Panel AJAX ccccccccccc-->
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<!-- cccccccccccc START: Rodape  ccccccccccc -->
				<tr>
					<td><cc1:fwservercontrol id="Fwservercontrol2" runat="server" Controle="RodapeAdmin"></cc1:fwservercontrol></td>
				</tr>
				<!-- cccccccccccc END: Rodape  ccccccccccc -->
			</table>
		</form>
	</body>
</HTML>
