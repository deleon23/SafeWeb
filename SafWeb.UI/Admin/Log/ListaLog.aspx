<%@ Register TagPrefix="radG" Namespace="Telerik.WebControls" Assembly="RadGrid.Net2" %>
<%@ Register TagPrefix="rada" Namespace="Telerik.WebControls" Assembly="RadAjax.Net2" %>

<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ListaLog.aspx.vb" Inherits="FrameWork.UI.ListaLog" %>

<%@ Register TagPrefix="cc1" Namespace="FrameWork.WebControl" Assembly="FrameWork.WebControl" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>%%SITE%%</title>
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="%%PATH%%/estilos/FrameWork.css" type="text/css" rel="stylesheet">
</head>
<body>
    <form id="Form1" method="post" runat="server">
        <table cellspacing="0" cellpadding="0" width="778" align="center" border="0">
            <!-- cccccccccccc START: Cabecalho  ccccccccccc -->
            <tr>
                <td>
                    <cc1:FWServerControl ID="Fwservercontrol3" runat="server" Controle="PagebannerAdmin">
                    </cc1:FWServerControl>
                </td>
            </tr>
            <!-- cccccccccccc END: Cabecalho  ccccccccccc -->
            <tr>
                <td valign="top">
                    <table class="backPadrao" height="424" cellspacing="0" cellpadding="0" width="100%"
                        align="center">
                        <tr>
                            <td class="BordaDefaultLRB" valign="top" align="center">
                                <!-- cccccccccccc START: Panel AJAX ccccccccccc-->
                                <rada:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="lpaCadastro"
                                    ClientEvents-OnRequestStart="OnRequestStart">
                                    <!-- cccccccccccc START: Panel Listagem ccccccccccc-->
                                    <asp:Panel ID="pnlErro" runat="server">
                                    </asp:Panel>
                                    <asp:Panel ID="pnlListagem" runat="server">
                                        <!-- cccccccccccc START: Titulo  ccccccccccc -->
                                        <table class="MarginTopBarra" cellspacing="0" cellpadding="0" width="760" align="center"
                                            border="0">
                                            <tr>
                                                <td class="CantoEsq">
                                                </td>
                                                <td class="cadBarraTitulo" width="100%" height="15">
                                                    <img src="%%PATH%%/Imagens/comum/bulletbarra.gif" align="middle" />
                                                    <asp:Label ID="lblTituloListagem" runat="server">Listagem de Log de Erros</asp:Label></td>
                                                <td class="CantoDir">
                                                </td>
                                            </tr>
                                            <!-- cccccccccccc END: Titulo  ccccccccccc -->
                                            <!-- cccccccccccc START: Conteudo  ccccccccccc -->
                                            <tr>
                                                <td valign="top" align="center" colspan="3">
                                                    <table class="ContainerBordaLRB" cellspacing="0" cellpadding="5" width="100%" align="center"
                                                        border="0">
                                                        <tr>
                                                            <td class="cadMsg" align="left">
                                                                <asp:Label ID="lblMensagem" runat="server">Formulário de Cadastro</asp:Label>
                                                                <asp:ValidationSummary ID="Validationsummary1" runat="server" DisplayMode="List"></asp:ValidationSummary>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <table id="dtgItemStyle" cellspacing="2" cellpadding="2" width="100%" align="center"
                                                                    border="0">
                                                                    <tr>
                                                                        <td class="cadlbl">
                                                                            <asp:Label ID="lblUsuario" runat="server"><u>U</u>suário:</asp:Label><br>
                                                                            <asp:DropDownList ID="ddlUsuario" AccessKey="U" TabIndex="1" runat="server" CssClass="cadddl">
                                                                            </asp:DropDownList></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="cadlbl">
                                                                            <asp:Label ID="lblDataCadastro" runat="server"><u>D</u>ata Cadastro:</asp:Label><br>
                                                                            <cc1:FWMascara ID="txtData" AccessKey="D" runat="server" CssClass="cadtxt" Mascara="DATA"
                                                                                Width="100px" MaxLength="10"></cc1:FWMascara><a hidefocus onclick="if(self.gfPop)gfPop.fPopCalendar(document.forms[0].all('<% = me.txtData.ClientID.ToString() %>'));return false;"
                                                                                    href="javascript:void(0)">
                                                                                    <img height="19" alt="" src="%%PATH%%/agenda/calbtn.gif" width="20" align="absMiddle"
                                                                                        border="0" name="popcal"></a>
                                                                            <asp:RangeValidator ID="rgvtxtInicioPeriodo" runat="server" CssClass="nomecampos"
                                                                                ControlToValidate="txtData" ErrorMessage="- Data inválida: A data deve estar entre 01/01/1900 e 31/12/2099: Data Cadastro"
                                                                                Display="Dynamic">*</asp:RangeValidator>
                                                                            <asp:CompareValidator ID="cpvtxtInicioPeriodo" runat="server" CssClass="nomecampos"
                                                                                ControlToValidate="txtData" ErrorMessage="- Data Inválida: Data Cadastro" Display="Dynamic"
                                                                                Type="Date" Operator="DataTypeCheck">*</asp:CompareValidator></td>
                                                                    </tr>
                                                                </table>
                                                                <table id="tblBotoes" height="50" cellspacing="0" cellpadding="0" width="100%" align="center"
                                                                    border="0">
                                                                    <tr>
                                                                        <td valign="middle" align="center">
                                                                            <asp:Button ID="btnBuscar" runat="server" CssClass="cadbutton" Width="90px" Text="Buscar">
                                                                            </asp:Button></td>
                                                                    </tr>
                                                                </table>
                                                                <table height="22" cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                    <tr>
                                                                        <td id="ColunaExportacao" align="right" runat="server">
                                                                            <asp:ImageButton ID="btnExportWord" runat="server" ImageUrl="%%PATH%%/imagens/icones/ico_doc.gif">
                                                                            </asp:ImageButton>&nbsp;
                                                                            <asp:ImageButton ID="btnExportExcel" runat="server" ImageUrl="%%PATH%%/imagens/icones/ico_xls.gif">
                                                                            </asp:ImageButton></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <radG:RadGrid ID="radGridDados" runat="server" CssClass="dtg" Width="100%" GridLines="None"
                                                                                Skin="None" AutoGenerateColumns="False" AllowPaging="True" AllowSorting="True">
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
                                                                                                    <td>
                                                                                                        <% = FrameWork.BusinessLayer.Idioma.BLIdiomas.TraduzirMensagens(FrameWork.Model.Idioma.Mensagens.CONSULTA_SEM_RETORNO) %>
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
                                                                                        <radG:GridBoundColumn DataField="LOG_D_DATA" UniqueName="LOG_D_DATA" SortExpression="LOG_D_DATA"
                                                                                            HeaderText="Data Cadastro">
                                                                                            <ItemStyle HorizontalAlign="center" Width="100px"></ItemStyle>
                                                                                        </radG:GridBoundColumn>
                                                                                        <radG:GridBoundColumn DataField="LOG_C_ACAO" SortExpression="LOG_C_ACAO" HeaderText="Método">
                                                                                            <ItemStyle HorizontalAlign="left"></ItemStyle>
                                                                                        </radG:GridBoundColumn>
                                                                                        <radG:GridBoundColumn DataField="LOG_C_DESCRICAO" SortExpression="LOG_C_DESCRICAO"
                                                                                            HeaderText="Erro">
                                                                                            <ItemStyle HorizontalAlign="left"></ItemStyle>
                                                                                        </radG:GridBoundColumn>
                                                                                        <radG:GridBoundColumn DataField="USU_C_NOME" SortExpression="USU_C_NOME" HeaderText="Usuário">
                                                                                            <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                                                        </radG:GridBoundColumn>
                                                                                    </Columns>
                                                                                </MasterTableView>
                                                                            </radG:RadGrid></td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <table cellspacing="0" cellpadding="0" width="100%" align="center" border="0">
                                                        <tr>
                                                            <td class="backBarraBotoes">
                                                                <asp:Button ID="btnLimparLog" runat="server" CssClass="cadbutton100" Text="Limpar Log"
                                                                    CausesValidation="False"></asp:Button>&nbsp;<input class="cadbutton100" id="btnVoltar"
                                                                        onclick="javascript:window.navigate('%%PATH%%/Admin/Admin.aspx')" type="button"
                                                                        value="Voltar" name="btnVoltar" runat="server"></td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                        <!-- cccccccccccc END: Conteudo  ccccccccccc -->
                                    </asp:Panel>
                                    <!-- cccccccccccc END: Panel Cadastro ccccccccccc-->
                                </rada:RadAjaxPanel>
                                <rada:AjaxLoadingPanel ID="lpaCadastro" Width="75px" Height="75px" runat="server"
                                    Transparency="30" HorizontalAlign="Center">
                                    <asp:Image ID="Image1" Style="margin-top: 100px" runat="server" ImageUrl="%%PATH%%/RadControls/Ajax/Skins/Default/Loading.gif"
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
                <td>
                    <cc1:FWServerControl ID="Fwservercontrol2" runat="server" Controle="RodapeAdmin"></cc1:FWServerControl>
                </td>
            </tr>
            <!-- cccccccccccc END: Rodape  ccccccccccc -->
        </table>
        <asp:HtmlIframe id="IframeAgenda" runat="server" style="z-index: 101; left: -500px; visibility: visible;
            position: absolute; top: 0px" name="gToday:contrast:agenda.js" src="%%PATH%%/agenda/calendario.htm"
            frameborder="0" width="132" scrolling="no" height="142"/>
    </form>
</body>
</html>
