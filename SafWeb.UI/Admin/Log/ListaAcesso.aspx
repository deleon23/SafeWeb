<%@ Register TagPrefix="rada" Namespace="Telerik.WebControls" Assembly="RadAjax.Net2" %>
<%@ Register TagPrefix="radG" Namespace="Telerik.WebControls" Assembly="RadGrid.Net2" %>

<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ListaAcesso.aspx.vb" Inherits="FrameWork.UI.ListaAcesso" %>

<%@ Register TagPrefix="cc1" Namespace="FrameWork.WebControl" Assembly="FrameWork.WebControl" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>%%SITE%%</title>
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="%%PATH%%/estilos/FrameWork.css" type="text/css" rel="stylesheet">
    <link href="/gridStyle.css" type="text/css" rel="stylesheet">
</head>
<body>
    <form id="Form1" method="post" runat="server">
        <table cellspacing="0" cellpadding="0" width="778" align="center" border="0">
            <tr>
                <td>
                    <cc1:FWServerControl ID="Fwservercontrol3" runat="server" Controle="PagebannerAdmin">
                    </cc1:FWServerControl>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <table class="backPadrao" cellspacing="0" cellpadding="0" width="100%" align="center"
                        height="424">
                        <tr>
                            <td class="BordaDefaultLRB" align="center" valign="top">
                                <!-- cccccccccccc START: Panel AJAX ccccccccccc-->
                                <rada:RadAjaxPanel ID="RadAjaxPanel1" runat="server" ClientEvents-OnRequestStart="OnRequestStart"
                                    LoadingPanelID="lpaCadastro">
                                    <!--cccccccccccc START: Panel Listagem ccccccccccc-->
                                    <asp:Panel ID="pnlErro" runat="server">
                                    </asp:Panel>
                                    <!-- cccccccccccc END: Panel Cadastro ccccccccccc-->
                                    <table class="MarginTopBarra" align="center" border="0" cellpadding="0" cellspacing="0"
                                        width="760">
                                        <tr>
                                            <td class="CantoEsq">
                                            </td>
                                            <td class="cadBarraTitulo" height="15" width="100%">
                                                <img src="%%PATH%%/Imagens/comum/bulletbarra.gif" align="middle" />
                                                <asp:Label ID="lblTituloListagem" runat="server">Listagem de Acesso</asp:Label>
                                            </td>
                                            <td class="CantoDir">
                                            </td>
                                        </tr>
                                        <tr>
                                            &nbsp;<!-- cccccccccccc START: Conteudo  ccccccccccc -->
                                            <td align="center" colspan="3" valign="top">
                                                <table align="center" border="0" cellpadding="5" cellspacing="0" class="ContainerBordaLRB"
                                                    width="100%">
                                                    <tr>
                                                        <td align="left" class="cadMsg">
                                                            <asp:Label ID="lblMensagem" runat="server">Formulário de Cadastro</asp:Label>
                                                            <asp:ValidationSummary ID="Validationsummary1" runat="server" DisplayMode="List"></asp:ValidationSummary>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table id="dtgItemStyle" align="center" border="0" cellpadding="2" cellspacing="2"
                                                                width="100%">
                                                                <tr>
                                                                    <td class="cadlbl">
                                                                        <asp:Label ID="lblDataInicial" runat="server">Data 
                                                                            <u>I</u>nicial:</asp:Label>
                                                                        <br>
                                                                            <cc1:FWMascara ID="txtDataInicial" runat="server" AccessKey="I" CssClass="cadtxt"
                                                                                Mascara="DATA" MaxLength="10" Width="100px"></cc1:FWMascara><a hidefocus="" href="javascript:void(0)"
                                                                                    onclick="if(self.gfPopComp)gfPopComp.fStartPop(document.forms[0].all('<% = me.txtDataInicial.ClientID.ToString() %>'),document.forms[0].all('<% = me.txtDataFinal.ClientID.ToString() %>'));return false;">
                                                                                    <img align="absMiddle" alt="" border="0" height="19" name="popcal" src="%%PATH%%/agenda/calbtn.gif"
                                                                                        width="20" />
                                                                                </a>
                                                                            <asp:RangeValidator ID="rgvtxtInicioPeriodo" runat="server" ControlToValidate="txtDataInicial"
                                                                                CssClass="nomecampos" Display="Dynamic" ErrorMessage="- Data inválida: A data deve estar entre 01/01/1900 e 31/12/2099: Data Cadastro">*</asp:RangeValidator>
                                                                            <asp:CompareValidator ID="cpvtxtInicioPeriodo" runat="server" ControlToValidate="txtDataInicial"
                                                                                CssClass="nomecampos" Display="Dynamic" ErrorMessage="- Data Inválida: Data Cadastro"
                                                                                Operator="DataTypeCheck" Type="Date">*</asp:CompareValidator>
                                                                            <asp:CompareValidator ID="cpvDataInicialXFinal" runat="server" ControlToCompare="txtDataFinal"
                                                                                ControlToValidate="txtDataInicial" ErrorMessage="- A data inicial deve ser menor que a data final."
                                                                                Operator="LessThanEqual">*</asp:CompareValidator>
                                                                        </br>
                                                                    </td>
                                                                    <td class="cadlbl">
                                                                        <asp:Label ID="lblDataFinal" runat="server">Data 
                                                                            <u>F</u>inal:</asp:Label>
                                                                        <br />
                                                                        <cc1:FWMascara ID="txtDataFinal" runat="server" AccessKey="F" CssClass="cadtxt" Mascara="DATA"
                                                                            MaxLength="10" Width="100px"></cc1:FWMascara><a hidefocus="" href="javascript:void(0)"
                                                                                onclick="if(self.gfPopComp)gfPopComp.fEndPop(document.forms[0].all('<% = me.txtDataInicial.ClientID.ToString() %>'),document.forms[0].all('<% = me.txtDataFinal.ClientID.ToString() %>'));return false;">
                                                                                <img align="absMiddle" alt="" border="0" height="19" name="popcal" src="%%PATH%%/agenda/calbtn.gif"
                                                                                    width="20" />
                                                                            </a>
                                                                        <asp:RangeValidator ID="rgvtxtFimPeriodo" runat="server" ControlToValidate="txtDataFinal"
                                                                            CssClass="nomecampos" Display="Dynamic" ErrorMessage="- Data inválida: A data deve estar entre 01/01/1900 e 31/12/2099: Data Cadastro">*</asp:RangeValidator>
                                                                        <asp:CompareValidator ID="cpvtxtFimPeriodo" runat="server" ControlToValidate="txtDataFinal"
                                                                            CssClass="nomecampos" Display="Dynamic" ErrorMessage="- Data Inválida: Data Cadastro"
                                                                            Operator="DataTypeCheck" Type="Date">*</asp:CompareValidator>
                                                                        </br>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <table id="tblBotoes" align="center" border="0" cellpadding="0" cellspacing="0" height="50"
                                                                width="100%">
                                                                <tr>
                                                                    <td align="center" valign="middle">
                                                                        <asp:Button ID="btnBuscar" runat="server" CssClass="cadbutton" Text="Buscar" Width="90px">
                                                                        </asp:Button>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <table border="0" cellpadding="0" cellspacing="0" height="22" width="100%">
                                                                <tr>
                                                                    <td id="ColunaExportacao" runat="server" align="right">
                                                                        <asp:ImageButton ID="btnExportWord" runat="server" ImageUrl="%%PATH%%/imagens/icones/ico_doc.gif">
                                                                        </asp:ImageButton>
                                                                        &nbsp;
                                                                        <asp:ImageButton ID="btnExportExcel" runat="server" ImageUrl="%%PATH%%/imagens/icones/ico_xls.gif">
                                                                        </asp:ImageButton>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <radG:RadGrid ID="radGridDados" runat="server" AllowPaging="True" AllowSorting="True"
                                                                            AutoGenerateColumns="False" CssClass="dtg" GridLines="None" Skin="None" Width="100%">
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
                                                                                        <table cellpadding="0" cellspacing="0" height="300" width="100%">
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <% = FrameWork.BusinessLayer.Idioma.BLIdiomas.TraduzirMensagens(FrameWork.Model.Idioma.Mensagens.CONSULTA_SEM_RETORNO) %>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </div>
                                                                                </NoRecordsTemplate>
                                                                                <PagerTemplate>
                                                                                    <table align="center" border="0" cellpadding="5" cellspacing="0" class="pag" height="35"
                                                                                        width="100%">
                                                                                        <tr>
                                                                                            <td valign="center" align="center">
                                                                                                <asp:Panel runat="server" ID="pnlPaginaAtual" DefaultButton="btnIr">
                                                                                                    <asp:ImageButton runat="server" ID="imgPrimeira" CssClass="pagImg" CausesValidation="false"
                                                                                                        CommandArgument="First" CommandName="Page" ImageUrl="~/imagens/icones/ico_setaesq.gif" />
                                                                                                    <asp:LinkButton ID="btnPrimeira" runat="server" CssClass="pagLink" CausesValidation="False"
                                                                                                        CommandArgument="First" Enabled="True" CommandName="Page">Primeira</asp:LinkButton>
                                                                                                    <asp:ImageButton runat="server" ID="imgAnterior" CssClass="pagImg" CausesValidation="false"
                                                                                                        CommandArgument="Prev" CommandName="Page" ImageUrl="~/imagens/icones/ico_setaesq2.gif" />
                                                                                                    <asp:LinkButton ID="btnAnterior" CommandName="Page" runat="server" CssClass="pagLink"
                                                                                                        CausesValidation="False" CommandArgument="Prev" Enabled="True">Anterior</asp:LinkButton>&nbsp;&nbsp;
                                                                                                    <asp:Label CssClass="paglbl" ID="lblPaginaDescricao" runat="server">| Página:</asp:Label>
                                                                                                    <cc1:FWMascara ID="txtPagina" Text='<%# cint(DataBinder.Eval(Container, "OwnerTableView.CurrentPageIndex")) + 1 %>'
                                                                                                        runat="server" CssClass="pagtxtbox" Width="50px" MaxLength="4" AutoPostBack="False"
                                                                                                        Mascara="NUMERO"></cc1:FWMascara>
                                                                                                    <asp:Label CssClass="paglbl" ID="lblPaginaDe" runat="server">de</asp:Label>
                                                                                                    <asp:Label CssClass="paglbl" ID="lblPagina" runat="server" Enabled="True"><%#Right("00000" + DataBinder.Eval(Container, "Paging.PageCount").ToString(), 4)%></asp:Label>
                                                                                                    <asp:LinkButton ID="btnIr" runat="server" CssClass="pagLink" CommandName="IrPagina">Ir</asp:LinkButton><span
                                                                                                        class="paglbl">&nbsp;|&nbsp;</span>
                                                                                                    <asp:LinkButton ID="btnProxima" runat="server" CssClass="pagLink" CausesValidation="False"
                                                                                                        CommandName="Page" CommandArgument="Next" Enabled="True">Próxima</asp:LinkButton>
                                                                                                    <asp:ImageButton runat="server" ID="imgProxima" CssClass="pagImg" CausesValidation="false"
                                                                                                        CommandArgument="Next" CommandName="Page" ImageUrl="~/imagens/icones/ico_setadir2.gif" />
                                                                                                    <asp:LinkButton ID="btnUltima" runat="server" CssClass="pagLink" CausesValidation="False"
                                                                                                        CommandName="Page" CommandArgument="Last" Enabled="True">Última</asp:LinkButton>
                                                                                                    <asp:ImageButton runat="server" ID="imgUltima" CssClass="pagImg" CausesValidation="false"
                                                                                                        CommandArgument="Last" CommandName="Page" ImageUrl="~/imagens/icones/ico_setadir.gif" />
                                                                                                </asp:Panel>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </PagerTemplate>
                                                                                <Columns>
                                                                                    <radG:GridBoundColumn DataField="ACC_C_PAG_C_TITULO" HeaderText="Nome Página" SortExpression="ACC_C_PAG_C_TITULO"
                                                                                        UniqueName="ACC_C_PAG_C_TITULO">
                                                                                        <ItemStyle Width="60%"></ItemStyle>
                                                                                    </radG:GridBoundColumn>
                                                                                    <radG:GridBoundColumn DataField="ACC_D_DATAREF" HeaderText="Data" SortExpression="ACC_D_DATAREF"
                                                                                        UniqueName="ACC_D_DATAREF">
                                                                                        <ItemStyle HorizontalAlign="Center" Width="20%"></ItemStyle>
                                                                                    </radG:GridBoundColumn>
                                                                                    <radG:GridBoundColumn DataField="ACC_N_COUNTER" HeaderText="Quantidade de acessos"
                                                                                        SortExpression="ACC_N_COUNTER" UniqueName="ACC_N_COUNTER">
                                                                                        <ItemStyle HorizontalAlign="Center" Width="20%"></ItemStyle>
                                                                                    </radG:GridBoundColumn>
                                                                                </Columns>
                                                                            </MasterTableView>
                                                                        </radG:RadGrid>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <table align="center" border="0" cellpadding="0" cellspacing="0" width="100%">
                                                    <tr>
                                                        <td class="backBarraBotoes">
                                                            <input id="btnVoltar" runat="server" class="cadbutton100" name="btnVoltar" onclick="javascript:window.navigate('%%PATH%%/Admin/Admin.aspx')"
                                                                type="button" value="Voltar" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                    <!-- cccccccccccc END: Panel Cadastro ccccccccccc-->
                                </rada:RadAjaxPanel>
                                <rada:AjaxLoadingPanel ID="lpaCadastro" runat="server" Width="75px" HorizontalAlign="Center"
                                    Transparency="30" Height="75px">
                                    <asp:Image ID="Image1" Style="margin-top: 100px" runat="server" ImageUrl="%%PATH%%/RadControls/Ajax/Skins/Default/Loading.gif"
                                        AlternateText="Aguarde ..."></asp:Image>
                                </rada:AjaxLoadingPanel>
                                <!-- cccccccccccc END: Panel AJAX ccccccccccc-->
                            </td>
                        </tr>
                        <!-- cccccccccccc START: Rodape  ccccccccccc -->
                        <tr>
                            <td>
                                <cc1:FWServerControl ID="Fwservercontrol2" runat="server" Controle="RodapeAdmin"></cc1:FWServerControl>
                            </td>
                        </tr>
                        <!-- cccccccccccc END: Rodape  ccccccccccc -->
                    </table>
                </td>
            </tr>
        </table>
        <asp:HtmlIframe id="IframeAgenda" runat="server" style="z-index: 101; left: -500px; visibility: visible;
            position: absolute; top: 0px" name="gToday:contrastcomp:agenda.js" src="%%PATH%%/agenda/calendariocomp.htm"
            frameborder="0" width="132" scrolling="no" height="142"/>
    </form>
</body>
</html>
