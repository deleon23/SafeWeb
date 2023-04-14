<%@ Register TagPrefix="radG" Namespace="Telerik.WebControls" Assembly="RadGrid.Net2" %>
<%@ Register TagPrefix="rada" Namespace="Telerik.WebControls" Assembly="RadAjax.Net2" %>
<%@ Register TagPrefix="cc1" Namespace="FrameWork.WebControl" Assembly="FrameWork.WebControl" %>

<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ListaTrace.aspx.vb" Inherits="FrameWork.UI.ListaTrace" %>

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
                                    <table class="MarginTopBarra" cellspacing="0" cellpadding="0" width="760" align="center"
                                        border="0">
                                        <tr>
                                            <td class="CantoEsq">
                                            </td>
                                            <td class="cadBarraTitulo" width="100%" height="15">
                                                <img src="%%PATH%%/Imagens/comum/bulletbarra.gif" align="middle" />
                                                <asp:Label ID="lblTitulo" runat="server">Listagem de Trace</asp:Label></td>
                                            <td class="CantoDir">
                                            </td>
                                        </tr>
                                        <tr>
                                            <!-- cccccccccccc START: Conteudo  ccccccccccc -->
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
                                                                        <asp:DropDownList ID="ddlUsuario" AccessKey="U" runat="server" CssClass="cadddl">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="cadlbl">
                                                                        <asp:Label ID="lblDataInicial" runat="server"><u>D</u>ata:</asp:Label><br>
                                                                        <cc1:FWMascara ID="txtDataInicial" AccessKey="D" runat="server" CssClass="cadtxt"
                                                                            MaxLength="10" Width="100px" Mascara="DATA"></cc1:FWMascara>
                                                                        <a hidefocus="" href="javascript:void(0)" onclick="if(self.gfPop)gfPop.fPopCalendar(document.forms[0].all('<% = me.txtDataInicial.ClientID.ToString() %>'));return false;">
                                                                            <img align="absMiddle" alt="" border="0" height="19" name="popcal" src="%%PATH%%/agenda/calbtn.gif"
                                                                                width="20"></img></a>
                                                                        <asp:RangeValidator ID="rgvtxtInicioPeriodo" runat="server" CssClass="nomecampos"
                                                                            Display="Dynamic" ErrorMessage="- Data inválida: A data deve estar entre 01/01/1900 e 31/12/2099: Data Cadastro"
                                                                            ControlToValidate="txtDataInicial">*</asp:RangeValidator>
                                                                        <asp:CompareValidator ID="cpvtxtInicioPeriodo" runat="server" CssClass="nomecampos"
                                                                            Display="Dynamic" ErrorMessage="- Data Inválida: Data Cadastro" ControlToValidate="txtDataInicial"
                                                                            Operator="DataTypeCheck" Type="Date">*</asp:CompareValidator></td>
                                                                </tr>
                                                            </table>
                                                            <table id="tblBotoes" height="50" cellspacing="0" cellpadding="0" width="100%" align="center"
                                                                border="0">
                                                                <tr>
                                                                    <td valign="middle" align="center">
                                                                        <asp:Button ID="btnBuscar" runat="server" CssClass="cadbutton" Text="Buscar" Width="90px">
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
                                                                        <radG:RadGrid ID="radGridDados" runat="server" CssClass="dtg" Width="100%" AllowSorting="True"
                                                                            AllowPaging="True" AutoGenerateColumns="False" Skin="None" GridLines="None">
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
                                                                                    <radG:GridBoundColumn UniqueName="ACT_D_DATAREF" HeaderText="Data/Hora" SortExpression="ACT_D_DATAREF"
                                                                                        DataField="ACT_D_DATAREF">
                                                                                        <ItemStyle Width="20%" HorizontalAlign="Center"></ItemStyle>
                                                                                    </radG:GridBoundColumn>
                                                                                    <radG:GridBoundColumn HeaderText="Página / Funcionalidades" SortExpression="DESCRICAO"
                                                                                        DataField="DESCRICAO" UniqueName="DESCRICAO">
                                                                                        <ItemStyle Width="80%" HorizontalAlign="Center"></ItemStyle>
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
                                                            <input class="cadbutton100" id="btnVoltar" onclick="javascript:window.navigate('%%PATH%%/Admin/Admin.aspx')"
                                                                type="button" value="Voltar" name="btnVoltar" runat="server">
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
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <cc1:FWServerControl ID="RodapeAdmin" runat="server" Controle="RodapeAdmin"></cc1:FWServerControl>
                </td>
            </tr>
        </table>
        <asp:HtmlIframe id="IframeAgenda" runat="server" style="z-index: 101; left: -500px; visibility: visible;
            position: absolute; top: 0px" name="gToday:contrast:agenda.js" src="%%PATH%%/agenda/calendario.htm"
            frameborder="0" width="132" scrolling="no" height="142"/>
    </form>
</body>
</html>
