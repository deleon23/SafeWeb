<%@ Register TagPrefix="radG" Namespace="Telerik.WebControls" Assembly="RadGrid.Net2" %>
<%@ Register TagPrefix="rada" Namespace="Telerik.WebControls" Assembly="RadAjax.Net2" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="CadIdioma.aspx.vb" Inherits="FrameWork.UI.CadIdioma" %>
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
				<!-- cccccccccccc START: Cabecalho  ccccccccccc -->
				<tr>
					<td><cc1:fwservercontrol id="Fwservercontrol3" runat="server" Controle="PagebannerAdmin"></cc1:fwservercontrol></td>
				</tr>
				<!-- cccccccccccc END: Cabecalho  ccccccccccc -->
				<tr>
					<td valign="top">
						<table class="backPadrao" height="424" cellspacing="0" cellpadding="0" width="100%" align="center">
							<tr>
								<td class="BordaDefaultLRB" valign="top" align="center">
									<!-- cccccccccccc START: Panel AJAX ccccccccccc--><rada:radajaxpanel id="RadAjaxPanel1" runat="server" ClientEvents-OnRequestStart="OnRequestStart" LoadingPanelID="lpaCadastro"><!--cccccccccccc START: Panel Listagem ccccccccccc-->
										<asp:Panel id="pnlErro" Runat="server"></asp:Panel>
										<asp:Panel id="pnlListagem" Runat="server"> <!-- cccccccccccc START: Titulo  ccccccccccc -->
											<table class="MarginTopBarra" cellspacing="0" cellpadding="0" width="760" align="center" border="0">
												<tr>
													<td class="CantoEsq"></td>
													<td class="cadBarraTitulo" width="100%" height="15">
														<img src="%%PATH%%/Imagens/comum/bulletbarra.gif" align="middle" />
														<asp:Label id="lblTituloListagem" runat="server">Listagem de Idiomas</asp:Label></td>
													<td class="CantoDir"></td>
												</tr> <!-- cccccccccccc END: Titulo  ccccccccccc --> <!-- cccccccccccc START: Conteudo  ccccccccccc -->
												<tr>
													<td valign="top" align="center" colspan="3">
														<table class="ContainerBordaLRB" cellspacing="0" cellpadding="5" width="100%" align="center"
															border="0">
															<tr>
																<td class="cadMsg" align="left">
																	<asp:label id="lblMensagemListagem" runat="server">Pesquisa</asp:label>
																	<asp:validationsummary id="Validationsummary3" runat="server" DisplayMode="List"></asp:validationsummary></td>
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
																				<radg:radgrid id="radGridDados" runat="server" Width="100%" AutoGenerateColumns="False" AllowSorting="True"
																					AllowPaging="True" CssClass="dtg" Gridlines="None" Skin="None">
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
																						<radg:GridBoundColumn HeaderText="Descrição" SortExpression="IDI_C_DESCRICAO" DataField="IDI_C_DESCRICAO"
																							UniqueName="IDI_C_DESCRICAO">
																							<ItemStyle Width="80%"></ItemStyle>
																						</radg:GridBoundColumn>
																						<radg:GridBoundColumn HeaderText="Sigla" SortExpression="IDI_C_SIGLA" DataField="IDI_C_SIGLA" UniqueName="IDI_C_SIGLA">
																							<ItemStyle HorizontalAlign="Center" Width="15%"></ItemStyle>
																						</radg:GridBoundColumn>
																						<radg:GridTemplateColumn UniqueName="TemplateColumn" visible="false">
																							<ItemStyle Width="5%" HorizontalAlign="Center"></ItemStyle>
																							<ItemTemplate>
																								<asp:ImageButton id=btnEditar AlternateText="Editar" runat="server" CausesValidation="False" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "IDI_N_CODIGO") %>' ToolTip="Editar" ImageUrl="%%PATH%%/imagens/icones/ico_editar.gif" CommandName="Editar">
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
																	<asp:button id="btnNovoListagem" runat="server" CssClass="cadbutton100" CausesValidation="False"
																		Text="Novo"></asp:button>&nbsp;<INPUT class="cadbutton100" id="btnVoltarListagem" onclick="javascript:window.navigate('%%PATH%%/Admin/Admin.aspx')"
																		type="button" value="Voltar" name="btnVoltarListagem" runat="server">
																</td>
															</tr>
														</table>
													</td>
												</tr>
											</table> <!-- cccccccccccc END: Conteudo  ccccccccccc --></asp:Panel> <!-- cccccccccccc END: Panel Listagem ccccccccccc--> <!-- cccccccccccc START: Panel Cadastro ccccccccccc-->
										<asp:Panel id="pnlCadastro" Runat="server"> <!-- cccccccccccc START: Titulo Cadastro ccccccccccc -->
											<table class="MarginTopBarra" cellspacing="0" cellpadding="0" width="760" align="center" border="0">
												<tr>
													<td class="CantoEsq"></td>
													<td class="cadBarraTitulo" width="100%" height="15">
														<img src="%%PATH%%/Imagens/comum/bulletbarra.gif" align="middle" />
														<asp:Label id="lblTitulo" runat="server">Cadastro de Idiomas</asp:Label></td>
													<td class="CantoDir"></td>
												</tr> <!-- cccccccccccc END: Titulo Cadastro ccccccccccc --> <!-- cccccccccccc START: Conteudo  ccccccccccc -->
												<tr>
													<td valign="top" align="center" colspan="3">
														<table class="ContainerBordaLRB" cellspacing="0" cellpadding="5" width="100%" align="center"
															border="0">
															<tr>
																<td>
																	<table cellspacing="0" cellpadding="0" width="100%" border="0">
																		<tr>
																			<td class="cadMsg" colspan="2">
																				<asp:label id="lblMensagemCadastro" runat="server">Formulário de Cadastro</asp:label>
																				<asp:validationsummary id="Validationsummary1" runat="server" DisplayMode="List"></asp:validationsummary></td>
																		</tr>
																	</table>
																	<table cellspacing="0" cellpadding="0" width="100%" border="0">
																		<tr>
																			<td class="cadlbl">
																				<asp:Label id="lblDescricao" runat="server"><u>D</u>escrição:</asp:Label><BR>
																				<asp:textbox id="txtNome" accessKey="D" runat="server" CssClass="cadtxt" MaxLength="50" width="300px"></asp:textbox>
																				<asp:RequiredFieldValidator id="rfvNome" runat="server" CssClass="nomecampos" ErrorMessage="- Campo Obrigatório: Descrição"
																					Display="Dynamic" ControlToValidate="txtNome">*</asp:RequiredFieldValidator></td>
																		</tr>
																		<tr>
																			<td class="cadlbl">
																				<asp:Label id="lblSigla" runat="server"><u>S</u>igla:</asp:Label><BR>
																				<asp:textbox id="txtSigla" accessKey="S" runat="server" CssClass="cadtxt" MaxLength="5" width="100px"></asp:textbox>
																				<asp:RequiredFieldValidator id="rfvSigla" runat="server" CssClass="nomecampos" ErrorMessage="- Campo Obrigatório: Sigla"
																					Display="Dynamic" ControlToValidate="txtSigla">*</asp:RequiredFieldValidator></td>
																		</tr>
																	</table>
																</td>
															</tr>
														</table>
														<table cellspacing="0" cellpadding="0" width="100%" align="center" border="0">
															<tr>
																<td class="backBarraBotoes">
																	<asp:button id="btnSalvarCadastro" runat="server" CssClass="cadbutton100" Text="Salvar"></asp:button>&nbsp;
																	<asp:button id="btnNovoCadastro" runat="server" CssClass="cadbutton100" CausesValidation="False"
																		Text="Novo"></asp:button>&nbsp;
																	<asp:button id="btnVoltarCadastro" runat="server" CssClass="cadbutton100" CausesValidation="False"
																		Text="Voltar"></asp:button></td>
															</tr>
														</table>
													</td>
												</tr>
											</table> <!-- cccccccccccc END: Conteudo  ccccccccccc --></asp:Panel> <!-- cccccccccccc END: Panel Cadastro ccccccccccc--></rada:radajaxpanel><rada:ajaxloadingpanel id="lpaCadastro" Runat="server" width="75px" HorizontalAlign="Center" Transparency="30"
										height="75px">
										<asp:Image id="Image1" style="MARGIN-TOP: 100px" runat="server" ImageUrl="%%PATH%%/RadControls/Ajax/Skins/Default/Loading.gif"
											AlternateText="Aguarde ..."></asp:Image>
									</rada:ajaxloadingpanel>
									<!-- cccccccccccc END: Panel AJAX ccccccccccc--></td>
							</tr>
						</table>
					</td>
				</tr>
				<!-- cccccccccccc START: Rodape  ccccccccccc -->
				<tr>
					<td><cc1:fwservercontrol id="Fwservercontrol2" runat="server" Controle="RodapeAdmin"></cc1:fwservercontrol></td>
				</tr>
				<!-- cccccccccccc END: Rodape  ccccccccccc --></table>
		</form>
	</body>
</HTML>
