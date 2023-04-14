<%@ Register TagPrefix="rada" Namespace="Telerik.WebControls" Assembly="RadAjax.Net2" %>
<%@ Register TagPrefix="radG" Namespace="Telerik.WebControls" Assembly="RadGrid.Net2" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="CadPermissoes.aspx.vb" Inherits="FrameWork.UI.CadPermissoes" %>
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
									<!-- cccccccccccc START: Panel AJAX ccccccccccc--><rada:radajaxpanel id="RadAjaxPanel1" runat="server" LoadingPanelID="lpaCadastro"><!-- cccccccccccc START: Panel Listagem ccccccccccc-->
										<asp:Panel id="pnlErro" Runat="server"></asp:Panel>
										<asp:Panel id="pnlListagem" Runat="server"> <!-- cccccccccccc START: Titulo  ccccccccccc -->
											<table class="MarginTopBarra" cellspacing="0" cellpadding="0" width="760" align="center" border="0">
												<tr>
													<td class="CantoEsq"></td>
													<td class="cadBarraTitulo" width="100%" height="15">
													    <img src="%%PATH%%/Imagens/comum/bulletbarra.gif" align="middle" />
														<asp:Label id="lblTituloListagem" runat="server">Permissões</asp:Label></td>
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
																	<table cellspacing="2" cellpadding="2" width="100%" align="center" border="0">
																		<tr>
																			<td class="cadlbl">
																				<asp:label id="lblSuperGrupo" runat="server">Super <u>G</u>rupo:</asp:label><BR>
																				<asp:dropdownlist id="ddlSuperGrupo" accessKey="G" runat="server" CssClass="cadddl"></asp:dropdownlist></td>
																		</tr>
																		<tr>
																			<td class="cadlbl">
																				<asp:label id="lblNomeListagem" runat="server"><u>N</u>ome Grupo:</asp:label><BR>
																				<asp:textbox id="txtNomeListagem" accessKey="N" runat="server" CssClass="cadtxt" width="300px"
																					MaxLength="50"></asp:textbox></td>
																		</tr>
																	</table>
																	<table id="tblBotoes" height="50" cellspacing="0" cellpadding="0" width="100%" align="center"
																		border="0">
																		<tr>
																			<td valign="middle" align="center">
																				<asp:button id="btnBuscar" runat="server" CssClass="cadbutton" Text="Buscar" Width="90px"></asp:button></td>
																		</tr>
																	</table>
																	<table height="22" cellspacing="0" cellpadding="0" width="100%" border="0">
																		<tr>
																			<td>
																				<radg:radgrid id="radGridDados" runat="server" CssClass="dtg" Width="100%" AutoGenerateColumns="False"
																					Skin="None" Gridlines="None" AllowPaging="True" AllowSorting="True">
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
																						<radg:GridBoundColumn HeaderText="Grupos" SortExpression="GRP_C_NOME" DataField="GRP_C_NOME" UniqueName="GRP_C_NOME">
																							<ItemStyle Width="95%"></ItemStyle>
																						</radg:GridBoundColumn>
																						<radg:GridBoundColumn visible="False" UniqueName="SGP_GRP_N_CODIGO_ADMIN" HeaderText="CodigoSuperGrupo"></radg:GridBoundColumn>
																						<radg:GridTemplateColumn UniqueName="TemplateColumn" visible="False">
																							<ItemStyle Width="5%" HorizontalAlign="Center"></ItemStyle>
																							<ItemTemplate>
																								<asp:ImageButton id=btnEditar AlternateText="Editar" runat="server" CausesValidation="False" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "GRP_N_CODIGO") %>' ToolTip="Editar" ImageUrl="%%PATH%%/imagens/icones/ico_editar.gif" CommandName="Editar">
																								</asp:ImageButton>
																							</ItemTemplate>
																						</radg:GridTemplateColumn>
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
																<td class="backBarraBotoes"><INPUT class="cadbutton100" id="btnVoltarListagem" onclick="javascript:window.navigate('%%PATH%%/Admin/Admin.aspx')"
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
														<asp:Label id="lblTituloCadastro" runat="server">Cadastro de Permissões</asp:Label></td>
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
																			<td class="cadMsg">
																				<asp:label id="lblMensagemCadastro" runat="server">Formulário de Cadastro</asp:label>
																				<asp:validationsummary id="Validationsummary1" runat="server" DisplayMode="List"></asp:validationsummary></td>
																		</tr>
																	</table>
																	<table id="tblDataGrid" cellspacing="0" cellpadding="0" width="100%" align="center" border="0"
																		runat="server">
																		<tr>
																			<td class="cadlbl">
																				<asp:Label id="lblGrupo" runat="server">Grupo:</asp:Label><BR>
																				<asp:textbox id="txtDescricao" runat="server" CssClass="cadtxt" MaxLength="50" Width="398px"
																					ReadOnly="True"></asp:textbox></td>
																		</tr>
																		<tr>
																			<td class="cadlbl">
																				<asp:label id="lblModulo" runat="server"><u>M</u>odulo:</asp:label><BR>
																				<asp:dropdownlist id="ddlModulo" accessKey="M" runat="server" CssClass="cadddl" AutoPostBack="True"></asp:dropdownlist></td>
																		</tr>
																		<tr>
																			<td>&nbsp;</td>
																		</tr>
																		<tr>
																			<td>
																				<asp:datagrid id="dtgDados" runat="server" CssClass="tabelaescuro" Width="100%" AutoGenerateColumns="False">
																					<AlternatingItemStyle CssClass="dtgItemStyle"></AlternatingItemStyle>
																					<ItemStyle CssClass="dtgItemStyleAlternate"></ItemStyle>
																					<HeaderStyle CssClass="dtgHeaderStyle" HorizontalAlign="Center"></HeaderStyle>
																					<Columns>
																						<asp:BoundColumn Visible="False" DataField="FUN_N_CODIGO" HeaderText="Codigo"></asp:BoundColumn>
																						<asp:BoundColumn DataField="FUN_C_DESCRICAO" HeaderText="Descrição"></asp:BoundColumn>
																						<asp:BoundColumn DataField="MOF_C_DESCRICAO" HeaderText="Modulo" ItemStyle-HorizontalAlign="Center"></asp:BoundColumn>
																						<asp:TemplateColumn ItemStyle-HorizontalAlign="Center" HeaderText="L">
																							<ItemTemplate>
																								<asp:CheckBox id="chkL" runat="server"></asp:CheckBox>
																							</ItemTemplate>
																						</asp:TemplateColumn>
																						<asp:TemplateColumn ItemStyle-HorizontalAlign="Center" HeaderText="I">
																							<ItemTemplate>
																								<asp:CheckBox id="chkI" runat="server"></asp:CheckBox>
																							</ItemTemplate>
																						</asp:TemplateColumn>
																						<asp:TemplateColumn ItemStyle-HorizontalAlign="Center" HeaderText="E">
																							<ItemTemplate>
																								<asp:CheckBox id="chkE" runat="server"></asp:CheckBox>
																							</ItemTemplate>
																						</asp:TemplateColumn>
																						<asp:TemplateColumn ItemStyle-HorizontalAlign="Center" HeaderText="A">
																							<ItemTemplate>
																								<asp:CheckBox id="chkA" runat="server"></asp:CheckBox>
																							</ItemTemplate>
																						</asp:TemplateColumn>
																					</Columns>
																				</asp:datagrid></td>
																		</tr>
																	</table>
																</td>
															</tr>
														</table>
														<table cellspacing="0" cellpadding="0" width="100%" align="center" border="0">
															<tr>
																<td class="backBarraBotoes">
																	<asp:button id="btnSalvarCadastro" runat="server" CssClass="cadbutton100" Text="Salvar"></asp:button>&nbsp;
																	<asp:button id="btnVoltarCadastro" runat="server" CssClass="cadbutton100" Text="Voltar" CausesValidation="False"></asp:button></td>
															</tr>
														</table>
													</td>
												</tr>
											</table> <!-- cccccccccccc END: Conteudo  ccccccccccc --></asp:Panel> <!-- cccccccccccc END: Panel Cadastro ccccccccccc--></rada:radajaxpanel><rada:ajaxloadingpanel id="lpaCadastro" width="75px" Runat="server" HorizontalAlign="Center" Transparency="30"
										height="75px">
										<asp:Image id="Image1" style="MARGIN-TOP: 100px" runat="server" AlternateText="Aguarde ..."
											ImageUrl="%%PATH%%/RadControls/Ajax/Skins/Default/Loading.gif"></asp:Image>
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
