<%@ Register TagPrefix="rada" Namespace="Telerik.WebControls" Assembly="RadAjax.Net2" %>
<%@ Register TagPrefix="chart" Namespace="ChartDirector" Assembly="netchartdir" %>
<%@ Register TagPrefix="cc1" Namespace="FrameWork.WebControl" Assembly="FrameWork.WebControl" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="RelatorioPaginas.aspx.vb" Inherits="FrameWork.UI.RelatorioPaginas"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>%%SITE%%</title>
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="%%PATH%%/estilos/FrameWork.css" type="text/css" rel="stylesheet">
		<LINK href="/gridStyle.css" type="text/css" rel="stylesheet">
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
									<!-- cccccccccccc START: Panel AJAX ccccccccccc--><rada:radajaxpanel id="RadAjaxPanel1" runat="server" ClientEvents-OnRequestStart="OnRequestStart" LoadingPanelID="lpaCadastro"><!--cccccccccccc START: Panel Listagem ccccccccccc-->
										<asp:Panel id="pnlErro" Runat="server"></asp:Panel>
										<table class="MarginTopBarra" cellspacing="0" cellpadding="0" width="760" align="center" border="0">
											<tr>
												<td class="CantoEsq"></td>
												<td class="cadBarraTitulo" width="100%" height="15">
													<img src="%%PATH%%/Imagens/comum/bulletbarra.gif" align="middle" />
													<asp:Label id="lblTitulo" runat="server">Relatório de Páginas</asp:Label></td>
												<td class="CantoDir"></td>
											</tr>
											<tr> <!-- cccccccccccc START: Conteudo  ccccccccccc -->
												<td valign="top" align="center" colspan="3">
													<table class="ContainerBordaLRB" cellspacing="0" cellpadding="5" width="100%" align="center"
														border="0">
														<tr>
															<td class="cadMsg" align="left">
																<asp:label id="lblMensagem" runat="server" ForeColor="Red"></asp:label>
																<asp:validationsummary id="Validationsummary1" runat="server" DisplayMode="List"></asp:validationsummary></td>
														</tr>
														<tr>
															<td>
																<table id="dtgItemStyle" cellspacing="2" cellpadding="2" width="100%" align="center" border="0">
																	<tr>
																		<td class="cadlbl" colspan="2">
																			<asp:label id="lblGrupos" runat="server"><u>G</u>rupo:</asp:label><BR>
																			<asp:dropdownlist id="ddlGrupo" accessKey="G" runat="server" CssClass="cadddl"></asp:dropdownlist></td>
																	</tr>
																	<tr>
																		<td class="cadlbl" colspan="2">
																			<asp:label id="lblUsuario" runat="server"><u>U</u>suário:</asp:label><BR>
																			<asp:dropdownlist id="ddlUsuario" accessKey="U" runat="server" CssClass="cadddl"></asp:dropdownlist></td>
																	</tr>
																	<tr>
																		<td class="cadlbl">
																			<asp:label id="lblDataInicial" runat="server">Data <u>I</u>nicial:</asp:label><BR>
																			<cc1:fwmascara id="txtDataInicial" accessKey="I" runat="server" CssClass="cadtxt" Mascara="DATA"
																				width="100px" MaxLength="10"></cc1:fwmascara><A hideFocus onclick="if(self.gfPopComp)gfPopComp.fStartPop(document.forms[0].all('<% = me.txtDataInicial.ClientID.ToString() %>'),document.forms[0].all('<% = me.txtDataFinal.ClientID.ToString() %>'));return false;" href="javascript:void(0)">
																				<IMG height="19" alt="" src="%%PATH%%/agenda/calbtn.gif" width="20" align="absMiddle" border="0" name="popcal"></A>
																			<asp:rangevalidator id="rgvtxtInicioPeriodo" runat="server" CssClass="nomecampos" ControlToValidate="txtDataInicial"
																				ErrorMessage="- Data inválida: A data deve estar entre 01/01/1900 e 31/12/2099: Data Cadastro" Display="Dynamic">*</asp:rangevalidator>
																			<asp:comparevalidator id="cpvtxtInicioXFinal" runat="server" ControlToValidate="txtDataInicial" ErrorMessage="A data inicial deve ser menor que a data final."
																				Operator="LessThanEqual" Display="Dynamic" ControlToCompare="txtDataFinal">*</asp:comparevalidator>
																			<asp:requiredfieldvalidator id="rfvDataInicial" runat="server" ControlToValidate="txtDataInicial" ErrorMessage="- Campo Obrigatório: Data Inicial"
																				Display="Dynamic">*</asp:requiredfieldvalidator></td>
																		<td class="cadlbl">
																			<asp:label id="lblDataFinal" runat="server">Data <u>F</u>inal:</asp:label><BR>
																			<cc1:fwmascara id="txtDataFinal" accessKey="F" runat="server" CssClass="cadtxt" Mascara="DATA"
																				width="100px" MaxLength="10"></cc1:fwmascara><A hideFocus onclick="if(self.gfPopComp)gfPopComp.fEndPop(document.forms[0].all('<% = me.txtDataInicial.ClientID.ToString() %>'),document.forms[0].all('<% = me.txtDataFinal.ClientID.ToString() %>'));return false;" href="javascript:void(0)">
																				<IMG height="19" alt="" src="%%PATH%%/agenda/calbtn.gif" width="20" align="absMiddle" border="0" name="popcal"></A>
																			<asp:rangevalidator id="rgvtxtFimPeriodo" Display="Dynamic" runat="server" CssClass="nomecampos" ControlToValidate="txtDataFinal"
																				ErrorMessage="- Data inválida: A data deve estar entre 01/01/1900 e 31/12/2099: Data Cadastro" >*</asp:rangevalidator>
																			<asp:requiredfieldvalidator id="rfvDataFinal" Display="Dynamic" runat="server" ControlToValidate="txtDataFinal" ErrorMessage="- Campo Obrigatório: Data Final">*</asp:requiredfieldvalidator></td>
																	</tr>
																	<tr>
																		<td class="cadlbl" colspan="2">
																			<asp:label id="lblGrafico" runat="server">G<u>r</u>áfico:</asp:label><BR>
																			<asp:dropdownlist id="ddlTipoGrafico" accessKey="R" runat="server" CssClass="cadddl">
																				<asp:ListItem Value="1">3D Bar Chart</asp:ListItem>
																				<asp:ListItem Value="0" Selected="True">Cylinder Bar Shape</asp:ListItem>
																				<asp:ListItem Value="2">3D Line Chart</asp:ListItem>
																				<asp:ListItem Value="3">Pie with Small Sectors</asp:ListItem>
																			</asp:dropdownlist></td>
																	</tr>
																</table>
																<table id="tblBotoes" height="50" cellspacing="0" cellpadding="0" width="100%" align="center"
																	border="0">
																	<tr>
																		<td valign="middle" align="center">
																			<asp:button id="btnGerar" runat="server" CssClass="cadbutton" Width="106px" Text="Gerar Relatório"></asp:button></td>
																	</tr>
																</table>
																<table cellspacing="0" cellpadding="0" width="100%" align="center" border="0">
																	<tr>
																		<td align="center">
																			<chart:webchartviewer id="WebChartViewer1" runat="server"></chart:webchartviewer></td>
																	</tr>
																</table>
															</td>
														</tr>
													</table>
													<table cellspacing="0" cellpadding="0" width="100%" align="center" border="0">
														<tr>
															<td class="backBarraBotoes"><INPUT class="cadbutton100" id="btnVoltar" onclick="javascript:window.navigate('%%PATH%%/Admin/Admin.aspx')"
																	type="button" value="Voltar" name="btnVoltar" runat="server">
															</td>
														</tr>
													</table>
												</td>
											</tr>
										</table> <!-- cccccccccccc END: Panel Cadastro ccccccccccc--></rada:radajaxpanel><rada:ajaxloadingpanel id="lpaCadastro" Runat="server" width="75px" HorizontalAlign="Center" Transparency="30"
										height="75px">
										<asp:Image id="Image1" style="MARGIN-TOP: 100px" runat="server" AlternateText="Aguarde ..."
											ImageUrl="%%PATH%%/RadControls/Ajax/Skins/Default/Loading.gif"></asp:Image>
									</rada:ajaxloadingpanel>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td>
						<cc1:FWServerControl id="RodapeAdmin" runat="server" Controle="RodapeAdmin"></cc1:FWServerControl></td>
				</tr>
			</table>
			<asp:HtmlIframe id="IframeAgenda" runat="server" style="Z-INDEX: 101; LEFT: -500px; VISIBILITY: visible; POSITION: absolute; TOP: 0px"
				name="gToday:contrastcomp:agenda.js" src="%%PATH%%/agenda/calendariocomp.htm" frameBorder="0"
				width="132" scrolling="no" height="142"/>
		</form>
	</body>
</HTML>
