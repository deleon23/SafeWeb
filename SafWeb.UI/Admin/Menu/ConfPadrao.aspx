<%@ Register TagPrefix="uc1" TagName="ColorPicker" Src="ColorPicker.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ConfPadrao.aspx.vb" Inherits="FrameWork.UI.ConfPadrao" %>
<%@ Register TagPrefix="cc1" Namespace="FrameWork.WebControl" Assembly="FrameWork.WebControl" %>
<%@ Register TagPrefix="radG" Namespace="Telerik.WebControls" Assembly="RadGrid.Net2" %>
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
									<rada:radajaxpanel id="RadAjaxPanel1" runat="server" LoadingPanelID="lpaCadastro" ClientEvents-OnRequestStart="OnRequestStart"> <!-- cccccccccccc START: Panel Listagem ccccccccccc-->
										<asp:Panel id="pnlErro" Runat="server"></asp:Panel>
										<table class="MarginTopBarra"  width="760" border="0" cellspacing="0" cellpadding="0" align="center">
											<tr>
												<td class="CantoEsq"></td>
												<td class="cadBarraTitulo" height="15" width="100%">
													<img src="%%PATH%%/Imagens/comum/bulletbarra.gif" align="middle" />
													<asp:Label id="lblTitulo" runat="server">Cadastro de Estilos</asp:Label>
												</td>
												<td class="CantoDir"></td>
											</tr>
											<tr>
												<!-- cccccccccccc START: Conteudo  ccccccccccc -->
												<td align="center" valign="top" colspan="3">
													<!-- cccccccccccc START: Panel AJAX ccccccccccc-->
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
																				<radg:radgrid id="radGridDados" runat="server" Gridlines="None" Skin="None" CssClass="dtg" AutoGenerateColumns="False"
																					AllowPaging="True" AllowSorting="True" Width="100%">
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
																						<radg:GridBoundColumn HeaderText="Código" SortExpression="MPD_N_CODIGO" DataField="MPD_N_CODIGO" UniqueName="MPD_N_CODIGO">
																							<ItemStyle Width="100px" HorizontalAlign="center"></ItemStyle>
																						</radg:GridBoundColumn>
																						<radg:GridBoundColumn HeaderText="Padrões" SortExpression="MPD_C_TITULO" DataField="MPD_C_TITULO" UniqueName="MPD_C_TITULO">
																							<ItemStyle HorizontalAlign="Left"></ItemStyle>
																						</radg:GridBoundColumn>
																						<radg:GridTemplateColumn UniqueName="TemplateColumn" visible="false">
																							<ItemStyle Width="5%" HorizontalAlign="Center"></ItemStyle>
																							<ItemTemplate>
																								<asp:ImageButton id=btnEditar AlternateText="Editar" runat="server" CausesValidation="False" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "MPD_N_CODIGO") %>' ToolTip="Editar" ImageUrl="%%PATH%%/imagens/icones/ico_editar.gif" CommandName="Editar">
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
													<asp:Panel id="pnlCadastro" runat="server">
														<table class="ContainerBordaLRB" cellspacing="0" cellpadding="5" width="100%" align="center"
															border="0">
															<tr>
																<td class="cadMsg" colspan="3">
																	<asp:Label id="lblMensagemCadastro" runat="server" EnableViewState="False"></asp:Label>
																	<asp:validationsummary id="sumValidators" runat="server" DisplayMode="List"></asp:validationsummary></td>
															</tr>
															<tr>
															    <td colspan="4"></td><!-- esta linha está nula para corrigir TABINDEX na página - não remova -->
															    <td rowspan="16" valign="middle"><uc1:ColorPicker id="ColorPicker1" runat="server"></uc1:ColorPicker></td>
															</tr>
															<tr>
																<td class="cadlbl" width="100">
																	<asp:Label id="lblTitpad" runat="server"><u>T</u>ítulo:</asp:Label></td>
																<td class="cadlbl" valign="top" colspan="3">
																	<asp:textbox id="txttitulo" accessKey="T" runat="server" CssClass="cadtxt" MaxLength="15" TabIndex="0"></asp:textbox>
																	<asp:requiredfieldvalidator id="rfvTitulo" runat="server" ErrorMessage="- Campo Obrigatório: Título" ControlToValidate="txttitulo"
																		Display="Dynamic">*</asp:requiredfieldvalidator></td>
															</tr>
															<tr>
																<td class="cadlbl" width="100">
																	<asp:Label id="lblPosiciona" runat="server">Posicionamento:</asp:Label></td>
																<td class="cadlbl" valign="top" colspan="3">&nbsp;
																	<asp:Label id="lblTop" runat="server">T<u>o</u>p:</asp:Label>&nbsp;
																	<cc1:FWMascara id="txtTop" accessKey="O" runat="server" Width="48px" CssClass="cadtxt" MaxLength="4"
																		Mascara="NUMERO"></cc1:FWMascara>&nbsp;&nbsp;&nbsp;
																	<asp:Label id="lblLeft" runat="server">L<u>e</u>ft:</asp:Label>&nbsp;
																	<cc1:FWMascara id="txtLeft" accessKey="E" runat="server" Width="48px" CssClass="cadtxt" MaxLength="4"
																		Mascara="NUMERO"></cc1:FWMascara></td>
															</tr>
															<tr>
																<td class="cadlbl" width="100">
																	<asp:Label id="lblDimensao" runat="server">Dimensões:</asp:Label></td>
																<td class="cadlbl" valign="top" colspan="3">&nbsp;
																	<asp:Label id="lblWidth" runat="server"><u>L</u>argura:</asp:Label>
																	<cc1:FWMascara id="txtWidth" accessKey="L" runat="server" Width="48px" CssClass="cadtxt" MaxLength="4"
																		Mascara="NUMERO"></cc1:FWMascara>&nbsp;&nbsp;&nbsp;
																	<asp:Label id="lblHeight" runat="server"><u>A</u>ltura:</asp:Label>
																	<cc1:FWMascara id="txtHeight" accessKey="A" runat="server" Width="48px" CssClass="cadtxt" MaxLength="4"
																		Mascara="NUMERO"></cc1:FWMascara>&nbsp;&nbsp;&nbsp;
																	<asp:Label id="lblPadding" runat="server"><u>E</u>spaçamento:</asp:Label>
																	<cc1:FWMascara id="txtPadding" accessKey="E" runat="server" Width="48px" CssClass="cadtxt" MaxLength="4"
																		Mascara="NUMERO"></cc1:FWMascara>&nbsp;
																</td>
															</tr>
															<tr>
																<td class="cadlbl" width="100">
																	<asp:Label id="lblOrienta" runat="server">Orientação:</asp:Label></td>
																<td class="cadlbl" valign="top" colspan="3">
																	<asp:RadioButtonList id="rblOrientacao" runat="server" RepeatDirection="Horizontal" cssclass="cadrad">
																		<asp:ListItem Value="1">Horizontal</asp:ListItem>
																		<asp:ListItem Value="2" Selected="True">Vertical</asp:ListItem>
																	</asp:RadioButtonList></td>
															</tr>
															<tr>
																<td class="cadlbl" width="100">
																	<asp:Label id="lblAlinha" runat="server">Alinhamento:</asp:Label></td>
																<td class="cadlbl" valign="top" colspan="3">
																	<asp:RadioButtonList id="rblAlinhamento" runat="server" CssClass="cadrad" RepeatDirection="Horizontal">
																		<asp:ListItem Value="1">esquerda</asp:ListItem>
																		<asp:ListItem Value="2" Selected="True">centralizado</asp:ListItem>
																		<asp:ListItem Value="3">direita</asp:ListItem>
																	</asp:RadioButtonList></td>
															</tr>
															<tr>
																<td class="cadlbl" style="HEIGHT: 13px" width="100" height="13">
																	<asp:Label id="lblTransicao" runat="server">T<u>r</u>ansição:</asp:Label></td>
																<td class="cadlbl" style="HEIGHT: 13px" valign="top" colspan="2" height="13">
																	<asp:dropdownlist id="ddlTransicao" accessKey="R" runat="server" CssClass="cadddl">
																		<asp:ListItem Value="RandomBars">Barras</asp:ListItem>
																		<asp:ListItem Value="Dissolve">Dissolver</asp:ListItem>
																		<asp:ListItem Value="Pixelate">Distor&#231;&#227;o</asp:ListItem>
																		<asp:ListItem Value="Inset">Entrar</asp:ListItem>
																		<asp:ListItem Value="Wipe">Esq-&gt;Dir</asp:ListItem>
																		<asp:ListItem Value="Fade">Fade</asp:ListItem>
																		<asp:ListItem Value="Sem Efeito" Selected="True">Sem Efeito</asp:ListItem>
																	</asp:dropdownlist></td>
															</tr>
															<tr>
																<td class="cadlbl" width="100">
																	<asp:Label id="lblEfeitos" runat="server">Efeitos:</asp:Label></td>
																<td class="cadlbl" valign="top" colspan="3">&nbsp;
																	<asp:checkbox id="chkSombra" accessKey="S" runat="server" CssClass="cadchk" Text="<u>S</u>ombra"></asp:checkbox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
																	<asp:Label id="lblTransp" runat="server">Tra<u>n</u>sparência:</asp:Label>
																	<cc1:FWMascara id="txtAlpha" accessKey="N" runat="server" Width="48px" CssClass="cadtxt" MaxLength="2"
																		Mascara="NUMERO"></cc1:FWMascara>&nbsp;
																</td>
															</tr>
															<tr>
																<td class="cadlbl" style="HEIGHT: 93px" width="100" rowSpan="2">
																	<asp:Label id="lblMouse" runat="server">Mouse:</asp:Label></td>
																<td class="cadlbl" style="HEIGHT: 48px" bgColor="#ffffcc">
																	<asp:Label id="lblOn" runat="server">ON</asp:Label></td>
																<td class="cadlbl" style="HEIGHT: 48px" valign="top" bgColor="#ffffcc">
																	<asp:Label id="lblOncorfonte" runat="server">Cor font<u>e</u>:</asp:Label>&nbsp;
																	<asp:textbox id="txtONFNCOLOR" accessKey="E" runat="server" Width="85px" 
                                                                        CssClass="cadtxt" MaxLength="11"></asp:textbox>&nbsp;
																	<asp:Label id="lblOncorfundo" runat="server">Cor f<u>u</u>ndo:</asp:Label>
																	<asp:textbox id="txtONBGCOLOR" accessKey="U" runat="server" Width="85px" 
                                                                        CssClass="cadtxt" MaxLength="11"></asp:textbox>
                                                                    <br>
																	<asp:Label ID="lblOnitem" runat="server">ÍTEM:</asp:Label>:
																	<asp:Label ID="lblOncorborda" runat="server">Cor 
                                                                    <u>b</u>orda:</asp:Label>
																	<asp:TextBox ID="txtONBDCOLOR" runat="server" accessKey="B" 
                                                            CssClass="cadtxt" MaxLength="11" Width="85px"></asp:TextBox>&#160;&#160;
																	<asp:Label ID="lblOnimg" runat="server">Imagem 
                                                                    <u>f</u>undo:</asp:Label>&#160;
																	<asp:DropDownList ID="ddlImgon" runat="server" accessKey="F" 
                                                            CssClass="cadddl" DataTextField="ARQUIVO">
                                                            </asp:DropDownList>
                                                                    <br></br>
                                                                    </br></td>
															</tr>
															<tr>
																<td class="cadlbl" style="HEIGHT: 45px" bgColor="#dcdcdc">
																	<asp:Label id="lbloff" runat="server">OFF</asp:Label></td>
																<td class="cadlbl" style="HEIGHT: 45px" valign="top" bgColor="gainsboro">
																	<asp:Label id="lbloffcorfonte" accessKey="E" runat="server">Cor font<u>e</u>:</asp:Label>
																	<asp:textbox id="txtOFFNCOLOR" accessKey="E" runat="server" Width="85px" 
                                                                        CssClass="cadtxt" MaxLength="11"></asp:textbox>&nbsp;
																	<asp:Label id="lbloffcorfundo" runat="server">Cor f<u>u</u>ndo:</asp:Label>
																	<asp:textbox id="txtOFBGCOLOR" accessKey="U" runat="server" Width="85px" 
                                                                        CssClass="cadtxt" MaxLength="11"></asp:textbox>
                                                                    <br>
																	<asp:Label ID="lbloffitem" runat="server">ÍTEM:</asp:Label>:&#160;
																	<asp:Label ID="lbloffcorborda" runat="server">Cor 
                                                                    <u>b</u>orda:</asp:Label>
																	<asp:TextBox ID="txtOFBDCOLOR" runat="server" accessKey="B" 
                                                            CssClass="cadtxt" MaxLength="11" Width="85px"></asp:TextBox>&#160;&#160;
																	<asp:Label ID="lbloffimg" runat="server">Imagem fundo:</asp:Label>&#160;<asp:DropDownList ID="ddlImgoff" runat="server" accessKey="F" 
                                                            CssClass="cadddl" DataTextField="ARQUIVO">
                                                            </asp:DropDownList>
                                                                    <br></br>
                                                                    </br></td>
															</tr>
															<tr>
																<td class="cadlbl" width="100">
																	<asp:Label id="lblBorda" runat="server">Borda:</asp:Label></td>
																<td class="cadlbl" valign="top" colspan="2">
																	<asp:Label id="lblCorborda" runat="server"><u>C</u>or:</asp:Label>
																	<asp:textbox id="txtCorborda" accessKey="C" runat="server" Width="85px" 
                                                                        CssClass="cadtxt" MaxLength="11"></asp:textbox>&nbsp;
																	<asp:Label id="lblLarguraborda" runat="server"><u>L</u>argura:</asp:Label>
																	<cc1:FWMascara id="txtLarguraborda" accessKey="L" runat="server" Width="48px" CssClass="cadtxt"
																		MaxLength="4" Mascara="NUMERO"></cc1:FWMascara>&nbsp;
																</td>
															</tr>
															<tr>
																<td class="cadlbl" width="100">
																	<asp:Label id="lblFonte" runat="server">Fonte:</asp:Label></td>
																<td class="cadlbl" valign="top" colspan="2" height="19">
																	<asp:dropdownlist id="ddlFontName" runat="server" CssClass="cadddl">
																		<asp:ListItem Value="Arial">Arial</asp:ListItem>
																		<asp:ListItem Value="Courier">Courier</asp:ListItem>
																		<asp:ListItem Value="Helvetica">Helvetica</asp:ListItem>
																		<asp:ListItem Value="Tahoma">Tahoma</asp:ListItem>
																		<asp:ListItem Value="Times">Times</asp:ListItem>
																		<asp:ListItem Value="Verdana" Selected="True">Verdana</asp:ListItem>
																	</asp:dropdownlist>&nbsp;
																	<asp:Label id="lblTamanho" runat="server"><u>T</u>amanho:</asp:Label>
																	<cc1:FWMascara id="txtFontSize" accessKey="T" runat="server" 
                                                            Width="80px" CssClass="cadtxt" MaxLength="10"
																		Mascara="NENHUMA"></cc1:FWMascara>&nbsp;
																	<asp:checkbox id="chkFntStyle" runat="server" CssClass="cadchk" Text="Itálico"></asp:checkbox>
																	<asp:checkbox id="chkFntBold" runat="server" CssClass="cadchk" Text="Negrito"></asp:checkbox>
																	<asp:checkbox id="chkFntDecoration" runat="server" CssClass="cadchk" Text="Sublinhado"></asp:checkbox></td>
															</tr>
															<tr>
																<td class="cadlbl" width="100">
																	<asp:Label id="lblImg" runat="server">Imagem SUB:</asp:Label></td>
																<td class="cadlbl" valign="top" colspan="2">&nbsp;
																	<asp:Label id="lblImagem" runat="server"><u>I</u>magem:</asp:Label>&nbsp;
																	<asp:DropDownList id="ddlImgsub" accessKey="I" runat="server" CssClass="cadddl" DataTextField="ARQUIVO"></asp:DropDownList>&nbsp;
																	<asp:Label id="lblImgpadding" runat="server"><u>E</u>spaçamento:</asp:Label>
																	<cc1:FWMascara id="txtSubPadding" accessKey="E" runat="server" Width="48px" CssClass="cadtxt" MaxLength="4"
																		Mascara="NUMERO"></cc1:FWMascara>&nbsp;
																	<asp:RadioButtonList id="rblSubAlign" runat="server" CssClass="cadrad" RepeatDirection="Horizontal">
																		<asp:ListItem Value="1">topo</asp:ListItem>
																		<asp:ListItem Value="2">meio</asp:ListItem>
																		<asp:ListItem Value="3">baixo</asp:ListItem>
																	</asp:RadioButtonList></td>
															</tr>
															<tr>
																<td class="cadlbl" width="100">
																	<asp:Label id="lblSeparador" runat="server">Separador:</asp:Label></td>
																<td class="cadlbl" valign="top" colspan="2">
																	<asp:Label id="lblCorSep" runat="server"><u>C</u>or:</asp:Label>
																	<asp:textbox id="txtSepColor" accessKey="C" runat="server" Width="85px" 
                                                                        CssClass="cadtxt" MaxLength="11"></asp:textbox>&nbsp;
																	<asp:Label id="lblLarguraSep" runat="server"><u>L</u>argura:</asp:Label>
																	<cc1:FWMascara id="txtSepWidth" accessKey="L" runat="server" Width="48px" CssClass="cadtxt" MaxLength="4"
																		Mascara="NUMERO"></cc1:FWMascara>&nbsp;&nbsp;&nbsp;
																	<asp:Label id="lblPaddingSep" runat="server"><u>E</u>spaçamento:</asp:Label>
																	<cc1:FWMascara id="txtSepPadding" accessKey="E" runat="server" Width="48px" CssClass="cadtxt" MaxLength="4"
																		Mascara="NUMERO"></cc1:FWMascara>&nbsp;
																</td>
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
														</table> <!-- cccccccccccc END: Conteudo  ccccccccccc --></asp:Panel>
												</td>
											</tr>
										</table>
										<!-- cccccccccccc END: Panel Cadastro ccccccccccc--></rada:radajaxpanel>
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
