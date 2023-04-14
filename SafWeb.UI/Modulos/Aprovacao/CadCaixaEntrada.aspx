<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CadCaixaEntrada.aspx.cs"
    Inherits="SafWeb.UI.Modulos.Aprovacao.CadCaixaEntrada" %>

<%@ Register TagPrefix="rad" Namespace="Telerik.WebControls" Assembly="RadGrid.Net2" %>
<%@ Register TagPrefix="rada" Namespace="Telerik.WebControls" Assembly="RadAjax.Net2" %>
<%@ Register TagPrefix="cc1" Namespace="FrameWork.WebControl" Assembly="FrameWork.WebControl" %>
<%@ Register TagPrefix="radc" Namespace="Telerik.WebControls" Assembly="RadCalendar.Net2" %>
<html>
<head>
    <title>%%SITE%%</title>
    <link href="../../Estilos/FrameWork.css" type="text/css" rel="stylesheet">
    <link href="../../Estilos/Brinks.css" type="text/css" rel="stylesheet">
    <link href="../../Scripts/Mascara.js" type="text/javascript" rel="Stylesheet">
    <script language="javascript">
        function FormataData(e, obj) {
            //usar no evento keypress
            //bloqueia caracteres alfa e coloca as barras nas posições        
            var key = window.event ? e.keyCode : e.which;
            var keychar = String.fromCharCode(key);
            var vrRetorno = false;

            //Backspace e Tab
            if (key != 8 && key != 0) {
                goodChars = "0123456789";
                if (goodChars.indexOf(keychar) != -1) {
                    if ((obj.value.length == 2 || obj.value.length == 5) && key != 8) {
                        obj.value += "/";
                    }
                    if (obj.value.length == 10 && key != 8) {
                        obj.value += " ";
                    }
                    if (obj.value.length == 13 && key != 8) {
                        obj.value += ":";
                    }
                    vrRetorno = true;
                }
            }
            else {
                vrRetorno = true;
            }

            return vrRetorno;
        }

        function AbrirHelp() {
            window.open("HelpCaixaEntrada.aspx", "JANELA", "height = 400, width = 530");
        }
    </script>
</head>
<body>
    <form id="form" method="post" runat="server">
    <table cellspacing="0" cellpadding="0" width="778" align="center" border="0">
        <tr>
            <td>
                <!-- ********************* START: CABEÇALHO ****************** -->
                <cc1:FWServerControl ID="FWServerControlCabecalho" runat="server" Controle="Pagebanner"
                    Arquivo="/Modulos/Framework/CabecalhoBrinks.ascx"></cc1:FWServerControl>
                <!-- ********************* END: CABEÇALHO ****************** -->
            </td>
        </tr>
        <tr>
            <td class="backbox" valign="top" height="250">
                <!-- ****************** START: PAINEL AJAX ************************** -->
                <rada:RadAjaxPanel ID="RadAjaxPanel1" runat="server" ClientEvents-OnRequestStart="OnRequestStart"
                    LoadingPanelID="lpaCadastro">
                    <asp:Panel ID="pnlErro" runat="server">
                    </asp:Panel>
                    <asp:Panel ID="pnlLista" runat="server">
                    </asp:Panel>
                    <table class="MarginTopBarra" cellspacing="0" cellpadding="0" width="760" align="center"
                        border="0">
                        <tr>
                            <!-- ***************** START: BARRA DE TITUTO ******************* -->
                            <td class="cadBarraTitulo" align="left" height="25" style="width: 700px">
                                <img src="%%PATH%%/imagens/comum/bulletbarra.gif" align="absMiddle" />
                                <asp:Label ID="lblArmazHistoric" runat="server" Text="Caixa de Entrada"></asp:Label>
                            </td>
                            <td class="cadBarraTitulo" align="right" height="25" style="width: 59px">
                                <asp:ImageButton ID="btnHelp" runat="server" AlternateText="Ajuda" ImageUrl="%%PATH%%/imagens/icones/ico_help.jpg"
                                    ImageAlign="AbsMiddle" Visible="False" />
                            </td>
                            <!-- ***************** END: BARRA DE TITUTO ******************* -->
                        </tr>
                        <tr>
                            <td class="backboxconteudo" valign="top" align="center" height="250" style="width: 759px"
                                colspan="2">
                                <br />
                                <table cellspacing="0" cellpadding="0" width="740" align="center" border="0">
                                    <tr>
                                        <td>
                                            <table cellspacing="0" cellpadding="0" width="740" align="center" border="0">
                                                <tr>
                                                    <td>
                                                        <table cellspacing="0" cellpadding="0" align="left" border="0">
                                                            <tr>
                                                                <td width="30%" valign="top">
                                                                    <asp:Button ID="btnAbaPendente" runat="server" CssClass="cadbuttonAbaAtiva" Width="100%"
                                                                        CausesValidation="False" Text="Solicitações Pendentes" OnClick="btnAbaPendente_Click">
                                                                    </asp:Button>
                                                                </td>
                                                                <td>
                                                                    <asp:Image ID="imgAbaPendetes" runat="server" ImageUrl="%%PATH%%/Imagens/right-abaAtiva.gif">
                                                                    </asp:Image>
                                                                </td>
                                                                <td width="30%" valign="top">
                                                                    <asp:Button ID="btnAbaHistorico" Text="Histórico Visitantes" runat="server" CssClass="cadbuttonAbaInativa"
                                                                        Width="100%" CausesValidation="False" OnClick="btnAbaHistorico_Click"></asp:Button>
                                                                </td>
                                                                <td>
                                                                    <asp:Image ID="imgAbaHistorico" runat="server" ImageUrl="%%PATH%%/Imagens/right-abaInativa.gif">
                                                                    </asp:Image>
                                                                </td>
                                                                <td width="34%" valign="top">
                                                                    <asp:Button ID="btnAbaHistoricoColab" Text="Histórico Escalas de Colaboradores" runat="server"
                                                                        CssClass="cadbuttonAbaInativa" Width="100%" CausesValidation="False" OnClick="btnAbaHistoricoColab_Click">
                                                                    </asp:Button>
                                                                </td>
                                                                <td>
                                                                    <asp:Image ID="imgAbaHistoricoColab" runat="server" ImageUrl="%%PATH%%/Imagens/right-abaInativa.gif">
                                                                    </asp:Image>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <!-- ************************ START PRIMEIRA ABA ************************* -->
                                                        <div id="divAbaPendentes" style="border-right: #d6d6d6 1px solid; border-left: #d6d6d6 1px solid;
                                                            width: 740px; border-bottom: #d6d6d6 1px solid; border-right: #d6d6d6 1px solid;
                                                            background-color: #ecf1f7" align="center" runat="server">
                                                            <table class="MarginTopConteudo" cellspacing="0" cellpadding="0" width="720" align="center"
                                                                border="0">
                                                                <tr>
                                                                    <td align="center">
                                                                        <table style="border-right: #cccccc 1px solid; border-top: #cccccc 1px solid; padding: 5px;
                                                                            border-left: #cccccc 1px solid; padding-top: 5px; border-bottom: #cccccc 1px solid"
                                                                            cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                            <tr>
                                                                                <td class="cadmsg" style="width: 726px; height: 28px; text-align: center;" align="center">
                                                                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" />
                                                                                    <asp:Label ID="lblMensagem" runat="server" Text="Label" Visible="False" CssClass="cadlbl"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="cadlbl" style="height: 19px">
                                                                                    <asp:Label ID="lblEscala" runat="server" Text="Escalas"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="center" style="height: 304px; width: 726px;">
                                                                                    <div style="width: 720px; overflow: auto; height: 290px; text-align: center;">
                                                                                        <!-- ********************* STAR: RADGRID ************************** -->
                                                                                        <rad:RadGrid ID="radEscalasPendente" runat="server" AllowPaging="True" AllowSorting="True"
                                                                                            AutoGenerateColumns="False" SortingSettings-SortToolTip="Clique para filtrar" CssClass="dtg" GridLines="None" Skin="None" Width="1135px"
                                                                                            OnItemCommand="radGridEscalas_ItemCommand" OnItemDataBound="radGridEscalas_ItemDataBound"
                                                                                            OnNeedDataSource="radGridEscalas_NeedDataSource">
                                                                                            <GroupHeaderItemStyle CssClass="dtgGroupHeader"></GroupHeaderItemStyle>
                                                                                            <GroupPanel Text="">
                                                                                                <PanelStyle CssClass="dtgGroupPanel"></PanelStyle>
                                                                                                <PanelItemsStyle CssClass="dtgGroupPanelItem"></PanelItemsStyle>
                                                                                            </GroupPanel>
                                                                                            <AlternatingItemStyle CssClass="dtgItemStyleAlternate"></AlternatingItemStyle>
                                                                                            <ItemStyle CssClass="dtgItemStyle"></ItemStyle>
                                                                                            <HeaderStyle CssClass="dtgHeaderStyle"></HeaderStyle>
                                                                                            <MasterTableView>
                                                                                                <NoRecordsTemplate>
                                                                                                    <div>
                                                                                                        <table cellpadding="2px" cellspacing="0" height="50px" width="100%">
                                                                                                            <tr>
                                                                                                                <td class="cadlbl">
                                                                                                                    <% = FrameWork.BusinessLayer.Idioma.BLIdiomas.TraduzirMensagens(FrameWork.Model.Idioma.Mensagens.CONSULTA_SEM_RETORNO) %>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                        </table>
                                                                                                    </div>
                                                                                                </NoRecordsTemplate>
                                                                                                <PagerTemplate>
                                                                                                    <table align="center" border="0" cellpadding="5" cellspacing="0" class="pag" height="15"
                                                                                                        width="100%">
                                                                                                        <tr>
                                                                                                            <td align="center" valign="center">
                                                                                                                <asp:Panel ID="pnlPaginaAtual" runat="server" DefaultButton="btnIr">
                                                                                                                    <asp:ImageButton ID="imgPrimeira" runat="server" CausesValidation="false" CommandArgument="First"
                                                                                                                        CommandName="Page" CssClass="pagImg" ImageUrl="~/imagens/icones/ico_setaesq.gif" />
                                                                                                                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandArgument="First"
                                                                                                                        CommandName="Page" CssClass="pagLink" Enabled="True">Primeira</asp:LinkButton>
                                                                                                                    <asp:ImageButton ID="imgAnterior" runat="server" CausesValidation="false" CommandArgument="Prev"
                                                                                                                        CommandName="Page" CssClass="pagImg" ImageUrl="~/imagens/icones/ico_setaesq2.gif" />
                                                                                                                    <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandArgument="Prev"
                                                                                                                        CommandName="Page" CssClass="pagLink" Enabled="True">Anterior</asp:LinkButton>&#160;&#160;
                                                                                                                    <asp:Label ID="Label3" runat="server" CssClass="paglbl">| Página:</asp:Label>
                                                                                                                    <cc1:FWMascara ID="FWMascara1" runat="server" AutoPostBack="False" CssClass="pagtxtbox"
                                                                                                                        Mascara="NUMERO" MaxLength="4" Text='<%# (int)(DataBinder.Eval(Container, "OwnerTableView.CurrentPageIndex")) + 1 %>'
                                                                                                                        Width="50px"></cc1:FWMascara>
                                                                                                                    <asp:Label ID="Label4" runat="server" CssClass="paglbl">de</asp:Label>
                                                                                                                    <asp:Label ID="Label6" runat="server" CssClass="paglbl" Enabled="True"><%#DataBinder.Eval(Container, "Paging.PageCount").ToString()%></asp:Label>
                                                                                                                    <asp:LinkButton ID="btnIr" runat="server" CommandName="IrPagina" CssClass="pagLink">Ir</asp:LinkButton>
                                                                                                                    <span class="paglbl">&#160;|&#160;</span>
                                                                                                                    <asp:LinkButton ID="LinkButton3" runat="server" CausesValidation="False" CommandArgument="Next"
                                                                                                                        CommandName="Page" CssClass="pagLink" Enabled="True">Próxima</asp:LinkButton>
                                                                                                                    <asp:ImageButton ID="imgProxima" runat="server" CausesValidation="false" CommandArgument="Next"
                                                                                                                        CommandName="Page" CssClass="pagImg" ImageUrl="~/imagens/icones/ico_setadir2.gif" />
                                                                                                                    <asp:LinkButton ID="LinkButton4" runat="server" CausesValidation="False" CommandArgument="Last"
                                                                                                                        CommandName="Page" CssClass="pagLink" Enabled="True">Última</asp:LinkButton>
                                                                                                                    <asp:ImageButton ID="imgUltima" runat="server" CausesValidation="false" CommandArgument="Last"
                                                                                                                        CommandName="Page" CssClass="pagImg" ImageUrl="~/imagens/icones/ico_setadir.gif" />
                                                                                                                </asp:Panel>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                    </table>
                                                                                                </PagerTemplate>
                                                                                                <Columns>
                                                                                                    <rad:GridTemplateColumn UniqueName="TemplateColumn">
                                                                                                        <HeaderTemplate>
                                                                                                            <asp:CheckBox ID="chkeTodosItems" runat="server" AutoPostBack="True" OnCheckedChanged="chkItemEscala_CheckedChanged"
                                                                                                                ToolTip="Selecionar todos" />
                                                                                                        </HeaderTemplate>
                                                                                                        <HeaderStyle Width="5px"></HeaderStyle>
                                                                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                                                        <ItemTemplate>
                                                                                                            <asp:CheckBox ID="chkeItem" runat="server" />
                                                                                                        </ItemTemplate>
                                                                                                    </rad:GridTemplateColumn>
                                                                                                    <rad:GridBoundColumn DataField="IdEscalacao" SortExpression="IdEscalacao" HeaderText="N&#186; Escala"
                                                                                                        UniqueName="IdEscalacao">
                                                                                                        <ItemStyle Width="30px" HorizontalAlign="Center" />
                                                                                                    </rad:GridBoundColumn>
                                                                                                    <rad:GridBoundColumn DataField="IdFilial" Visible="False" HeaderText="IdFilial" UniqueName="IdFilial">
                                                                                                    </rad:GridBoundColumn>
                                                                                                    <rad:GridBoundColumn DataField="AliasFilial" SortExpression="AliasFilial" HeaderText="Filial"
                                                                                                        UniqueName="AliasFilial">
                                                                                                        <ItemStyle Width="100px" HorizontalAlign="center" />
                                                                                                    </rad:GridBoundColumn>
                                                                                                    <rad:GridBoundColumn DataField="IdEscalaDepartamental" Visible="False" HeaderText="IdEscalaDepartamental"
                                                                                                        UniqueName="IdEscalaDepartamental">
                                                                                                    </rad:GridBoundColumn>
                                                                                                    <rad:GridBoundColumn DataField="DescricaoEscalaDepartamental" SortExpression="DescricaoEscalaDepartamental"
                                                                                                        Visible="False" UniqueName="DescricaoEscalaDepartamental">
                                                                                                    </rad:GridBoundColumn>
                                                                                                    <rad:GridTemplateColumn DataField="DescricaoEscalaDepartamental" SortExpression="DescricaoEscalaDepartamental"
                                                                                                        HeaderText="Descrição Escala" UniqueName="DescricaoEscalaDepartamental">
                                                                                                        <ItemStyle Width=" 115px" HorizontalAlign="Center" />
                                                                                                        <ItemTemplate>
                                                                                                            <asp:LinkButton ID="lnkColaborador" runat="server" CommandName="VisualizarCol" Width="75px"></asp:LinkButton>
                                                                                                        </ItemTemplate>
                                                                                                    </rad:GridTemplateColumn>
                                                                                                    <rad:GridBoundColumn DataField="IdTipoSolicitacao" Visible="False" HeaderText="IdTipoSolicitacao"
                                                                                                        UniqueName="IdTipoSolicitacao">
                                                                                                    </rad:GridBoundColumn>
                                                                                                    <rad:GridBoundColumn DataField="DescricaoTipoSolicitacao" SortExpression="DescricaoTipoSolicitacao"
                                                                                                        HeaderText="Tipo Escala" UniqueName="DescricaoTipoSolicitacao">
                                                                                                        <ItemStyle Width="175px" HorizontalAlign="Center" />
                                                                                                        <HeaderStyle HorizontalAlign="center" />
                                                                                                    </rad:GridBoundColumn>
                                                                                                    <rad:GridBoundColumn DataField="Periodo" SortExpression="Periodo" HeaderText="Período"
                                                                                                        UniqueName="Periodo">
                                                                                                        <HeaderStyle Width="170px" HorizontalAlign="center" />
                                                                                                        <ItemStyle />
                                                                                                    </rad:GridBoundColumn>
                                                                                                    <rad:GridBoundColumn DataField="IdStatusSolicitacao" Visible="False" HeaderText="IdStatus"
                                                                                                        UniqueName="IdStatus">
                                                                                                    </rad:GridBoundColumn>
                                                                                                    <rad:GridBoundColumn DataField="DescricaoStatusAprovacao" SortExpression="DescricaoStatusAprovacao"
                                                                                                        HeaderText="Status" UniqueName="DescricaoStatusAprovacao">
                                                                                                        <HeaderStyle Width="140px" />
                                                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                                                    </rad:GridBoundColumn>
                                                                                                    <rad:GridBoundColumn DataField="NomeUsuarioSolicitante" SortExpression="NomeUsuarioSolicitante"
                                                                                                        HeaderText="Solicitante" UniqueName="NomeUsuarioSolicitante">
                                                                                                        <HeaderStyle Width="100px" />
                                                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                                                    </rad:GridBoundColumn>
                                                                                                    <rad:GridTemplateColumn DataField="UsuarioAprov" SortExpression="UsuarioAprov" HeaderText="Último Aprovador"
                                                                                                        UniqueName="TUsuarioAprov">
                                                                                                        <ItemStyle Width="100px" />
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblUsuarioAprov" runat="server"></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                    </rad:GridTemplateColumn>
                                                                                                    <rad:GridBoundColumn DataField="PendenteCom" SortExpression="PendenteCom" HeaderText="Pendente Com"
                                                                                                        UniqueName="PendenteCom">
                                                                                                        <HeaderStyle Width="100px" />
                                                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                                                    </rad:GridBoundColumn>
                                                                                                    <rad:GridTemplateColumn UniqueName="TemplateColumn" Visible="False">
                                                                                                        <ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
                                                                                                        <ItemTemplate>
                                                                                                            <asp:ImageButton ID="btnVisualizar" runat="server" AlternateText="Visualizar" CausesValidation="False"
                                                                                                                CommandArgument='<%# DataBinder.Eval(Container.DataItem, "IdEscalacao") %>' CommandName="Visualizar"
                                                                                                                ImageUrl="%%PATH%%/imagens/icones/ico_editar.gif"></asp:ImageButton>
                                                                                                        </ItemTemplate>
                                                                                                    </rad:GridTemplateColumn>
                                                                                                    <rad:GridBoundColumn DataField="CorFonteHtml" SortExpression="CorFonteHtml" HeaderText="CorFonteHtml"
                                                                                                        Visible="False" UniqueName="CorFonteHtml">
                                                                                                    </rad:GridBoundColumn>
                                                                                                </Columns>
                                                                                                <RowIndicatorColumn Visible="False">
                                                                                                    <HeaderStyle Width="20px" />
                                                                                                    <HeaderStyle Width="20px" />
                                                                                                </RowIndicatorColumn>
                                                                                                <ExpandCollapseColumn Resizable="False" Visible="False">
                                                                                                    <HeaderStyle Width="20px" />
                                                                                                </ExpandCollapseColumn>
                                                                                            </MasterTableView>
                                                                                        </rad:RadGrid>
                                                                                        <!-- ********************* END: RADGRID ************************** -->
                                                                                    </div>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="cadlbl">
                                                                                    <asp:Label ID="lblSolicitacaoAcesso" runat="server" Text="Solicitação de Acesso"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="center" style="height: 304px; width: 726px;">
                                                                                    <div style="width: 720px; overflow: auto; height: 300px; text-align: center;">
                                                                                        <!-- ********************* STAR: RADGRID ************************** -->
                                                                                        <rad:RadGrid ID="radPendentes" runat="server" AllowPaging="True" AllowSorting="True"
                                                                                            AutoGenerateColumns="False" SortingSettings-SortToolTip="Clique para filtrar" CssClass="dtg" GridLines="None" Skin="None" Width="1500px"
                                                                                            OnItemDataBound="radPendentes_ItemDataBound" OnItemCommand="radPendentes_ItemCommand"
                                                                                            OnNeedDataSource="radPendentes_NeedDataSource">
                                                                                            <GroupHeaderItemStyle CssClass="dtgGroupHeader"></GroupHeaderItemStyle>
                                                                                            <GroupPanel Text="">
                                                                                                <PanelStyle CssClass="dtgGroupPanel"></PanelStyle>
                                                                                                <PanelItemsStyle CssClass="dtgGroupPanelItem"></PanelItemsStyle>
                                                                                            </GroupPanel>
                                                                                            <AlternatingItemStyle CssClass="dtgItemStyleAlternate"></AlternatingItemStyle>
                                                                                            <ItemStyle CssClass="dtgItemStyle"></ItemStyle>
                                                                                            <HeaderStyle CssClass="dtgHeaderStyle"></HeaderStyle>
                                                                                            <MasterTableView>
                                                                                                <NoRecordsTemplate>
                                                                                                    <div>
                                                                                                        <table cellpadding="2px" cellspacing="0" height="50px" width="100%">
                                                                                                            <tr>
                                                                                                                <td class="cadlbl">
                                                                                                                    <% = FrameWork.BusinessLayer.Idioma.BLIdiomas.TraduzirMensagens(FrameWork.Model.Idioma.Mensagens.CONSULTA_SEM_RETORNO) %>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                        </table>
                                                                                                    </div>
                                                                                                </NoRecordsTemplate>
                                                                                                <PagerTemplate>
                                                                                                    <table align="center" border="0" cellpadding="5" cellspacing="0" class="pag" height="15"
                                                                                                        width="100%">
                                                                                                        <tr>
                                                                                                            <td align="center" valign="center">
                                                                                                                <asp:Panel ID="pnlPaginaAtual" runat="server" DefaultButton="btnIr">
                                                                                                                    <asp:ImageButton ID="imgPrimeira" runat="server" CausesValidation="false" CommandArgument="First"
                                                                                                                        CommandName="Page" CssClass="pagImg" ImageUrl="~/imagens/icones/ico_setaesq.gif" />
                                                                                                                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandArgument="First"
                                                                                                                        CommandName="Page" CssClass="pagLink" Enabled="True">Primeira</asp:LinkButton>
                                                                                                                    <asp:ImageButton ID="imgAnterior" runat="server" CausesValidation="false" CommandArgument="Prev"
                                                                                                                        CommandName="Page" CssClass="pagImg" ImageUrl="~/imagens/icones/ico_setaesq2.gif" />
                                                                                                                    <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandArgument="Prev"
                                                                                                                        CommandName="Page" CssClass="pagLink" Enabled="True">Anterior</asp:LinkButton>&#160;&#160;
                                                                                                                    <asp:Label ID="Label1" runat="server" CssClass="paglbl">| Página:</asp:Label>
                                                                                                                    <cc1:FWMascara ID="txtPagina" runat="server" AutoPostBack="False" CssClass="pagtxtbox"
                                                                                                                        Mascara="NUMERO" MaxLength="4" Text='<%# (int)(DataBinder.Eval(Container, "OwnerTableView.CurrentPageIndex")) + 1 %>'
                                                                                                                        Width="50px"></cc1:FWMascara>
                                                                                                                    <asp:Label ID="Label2" runat="server" CssClass="paglbl">de</asp:Label>
                                                                                                                    <asp:Label ID="Label3" runat="server" CssClass="paglbl" Enabled="True"><%#DataBinder.Eval(Container, "Paging.PageCount").ToString()%></asp:Label>
                                                                                                                    <asp:LinkButton ID="btnIr" runat="server" CommandName="IrPagina" CssClass="pagLink">Ir</asp:LinkButton>
                                                                                                                    <span class="paglbl">&#160;|&#160;</span>
                                                                                                                    <asp:LinkButton ID="LinkButton3" runat="server" CausesValidation="False" CommandArgument="Next"
                                                                                                                        CommandName="Page" CssClass="pagLink" Enabled="True">Próxima</asp:LinkButton>
                                                                                                                    <asp:ImageButton ID="imgProxima" runat="server" CausesValidation="false" CommandArgument="Next"
                                                                                                                        CommandName="Page" CssClass="pagImg" ImageUrl="~/imagens/icones/ico_setadir2.gif" />
                                                                                                                    <asp:LinkButton ID="LinkButton4" runat="server" CausesValidation="False" CommandArgument="Last"
                                                                                                                        CommandName="Page" CssClass="pagLink" Enabled="True">Última</asp:LinkButton>
                                                                                                                    <asp:ImageButton ID="imgUltima" runat="server" CausesValidation="false" CommandArgument="Last"
                                                                                                                        CommandName="Page" CssClass="pagImg" ImageUrl="~/imagens/icones/ico_setadir.gif" />
                                                                                                                </asp:Panel>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                    </table>
                                                                                                </PagerTemplate>
                                                                                                <Columns>
                                                                                                    <rad:GridTemplateColumn UniqueName="TemplateColumn">
                                                                                                        <HeaderTemplate>
                                                                                                            <asp:CheckBox ID="chkTodosItems" runat="server" AutoPostBack="True" OnCheckedChanged="chkItem_CheckedChanged"
                                                                                                                ToolTip="Selecionar todos" />
                                                                                                        </HeaderTemplate>
                                                                                                        <HeaderStyle Width="5px"></HeaderStyle>
                                                                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                                                        <ItemTemplate>
                                                                                                            <asp:CheckBox ID="chkItem" runat="server" />
                                                                                                        </ItemTemplate>
                                                                                                    </rad:GridTemplateColumn>
                                                                                                    <rad:GridBoundColumn DataField="Codigo" SortExpression="Codigo" HeaderText="N&#186; Solic."
                                                                                                        UniqueName="Codigo">
                                                                                                        <ItemStyle HorizontalAlign="Center" Width="40px" />
                                                                                                    </rad:GridBoundColumn>
                                                                                                    <rad:GridBoundColumn DataField="CodFilial" Visible="False" HeaderText="CodFilial"
                                                                                                        UniqueName="CodFilial">
                                                                                                    </rad:GridBoundColumn>
                                                                                                    <rad:GridBoundColumn DataField="AliasFilial" SortExpression="AliasFilial" HeaderText="Filial"
                                                                                                        UniqueName="AliasFilial">
                                                                                                        <ItemStyle Width="45px" />
                                                                                                    </rad:GridBoundColumn>
                                                                                                    <rad:GridBoundColumn DataField="CodVisitante" Visible="False" HeaderText="CodVisitante"
                                                                                                        UniqueName="CodVisitante">
                                                                                                    </rad:GridBoundColumn>
                                                                                                    <rad:GridTemplateColumn DataField="Visitante" SortExpression="Visitante" HeaderText="Visitante"
                                                                                                        UniqueName="TVisitante">
                                                                                                        <ItemStyle Width="75px" />
                                                                                                        <ItemTemplate>
                                                                                                            <asp:LinkButton ID="lnkVisitante" runat="server" CommandName="Lista" Width="75px"></asp:LinkButton>
                                                                                                        </ItemTemplate>
                                                                                                    </rad:GridTemplateColumn>
                                                                                                    <rad:GridBoundColumn DataField="CodEmpresa" Visible="False" HeaderText="CodEmpresa"
                                                                                                        UniqueName="CodEmpresa">
                                                                                                    </rad:GridBoundColumn>
                                                                                                    <rad:GridBoundColumn DataField="Empresa" SortExpression="Empresa" HeaderText="Empresa"
                                                                                                        UniqueName="Empresa">
                                                                                                        <ItemStyle Width="60px" />
                                                                                                    </rad:GridBoundColumn>
                                                                                                    <rad:GridBoundColumn DataField="CodTipoSolicitacao" Visible="False" HeaderText="CodTipoSolicitacao"
                                                                                                        UniqueName="CodTipoSolicitacao">
                                                                                                    </rad:GridBoundColumn>
                                                                                                    <rad:GridBoundColumn DataField="TipoSolicitacao" SortExpression="TipoSolicitacao"
                                                                                                        HeaderText="Tipo Solic." UniqueName="TipoSolicitacao">
                                                                                                        <ItemStyle Width="95px" />
                                                                                                    </rad:GridBoundColumn>
                                                                                                    <rad:GridBoundColumn DataField="InicioVisita" SortExpression="InicioVisita" HeaderText="Data/Hora In&#237;cio"
                                                                                                        UniqueName="InicioVisita" DataFormatString="{0:dd/MM/yyyy HH:mm}">
                                                                                                        <ItemStyle Width="103px" />
                                                                                                    </rad:GridBoundColumn>
                                                                                                    <rad:GridBoundColumn DataField="FimVisita" SortExpression="FimVisita" HeaderText="Data/Hora Fim"
                                                                                                        UniqueName="FimVisita" DataFormatString="{0:dd/MM/yyyy HH:mm}">
                                                                                                        <ItemStyle Width="103px" />
                                                                                                    </rad:GridBoundColumn>
                                                                                                    <rad:GridBoundColumn DataField="AcSabado" SortExpression="AcSabado" HeaderText="S&#225;b."
                                                                                                        Visible="False" UniqueName="AcSabado">
                                                                                                    </rad:GridBoundColumn>
                                                                                                    <rad:GridTemplateColumn DataField="AcSabado" SortExpression="AcSabado" HeaderText="S&#225;b."
                                                                                                        UniqueName="TemplateColumn1">
                                                                                                        <ItemStyle Width="5px" />
                                                                                                        <ItemTemplate>
                                                                                                            <asp:CheckBox ID="chkAcSabado" runat="server" Enabled="false" />
                                                                                                        </ItemTemplate>
                                                                                                    </rad:GridTemplateColumn>
                                                                                                    <rad:GridBoundColumn DataField="AcDomingo" SortExpression="AcDomingo" HeaderText="Dom."
                                                                                                        Visible="False" UniqueName="AcDomingo">
                                                                                                    </rad:GridBoundColumn>
                                                                                                    <rad:GridTemplateColumn DataField="AcDomingo" SortExpression="AcDomingo" HeaderText="Dom."
                                                                                                        UniqueName="TemplateColumn2">
                                                                                                        <ItemStyle Width="5px" />
                                                                                                        <ItemTemplate>
                                                                                                            <asp:CheckBox ID="chkAcDomingo" runat="server" Enabled="false" />
                                                                                                        </ItemTemplate>
                                                                                                    </rad:GridTemplateColumn>
                                                                                                    <rad:GridBoundColumn DataField="AcFeriado" SortExpression="AcFeriado" HeaderText="Fer."
                                                                                                        Visible="False" UniqueName="AcFeriado">
                                                                                                    </rad:GridBoundColumn>
                                                                                                    <rad:GridTemplateColumn DataField="AcFeriado" SortExpression="AcFeriado" HeaderText="Fer."
                                                                                                        UniqueName="TemplateColumn3">
                                                                                                        <ItemStyle Width="5px" />
                                                                                                        <ItemTemplate>
                                                                                                            <asp:CheckBox ID="chkAcFeriado" runat="server" Enabled="false" />
                                                                                                        </ItemTemplate>
                                                                                                    </rad:GridTemplateColumn>
                                                                                                    <rad:GridBoundColumn DataField="CodStatus" Visible="False" HeaderText="CodStatus"
                                                                                                        UniqueName="CodStatus">
                                                                                                    </rad:GridBoundColumn>
                                                                                                    <rad:GridBoundColumn DataField="Status" SortExpression="Status" HeaderText="Status"
                                                                                                        UniqueName="Status">
                                                                                                        <ItemStyle Width="115px" />
                                                                                                    </rad:GridBoundColumn>
                                                                                                    <rad:GridBoundColumn DataField="CodUsuSolic" Visible="False" HeaderText="CodUsuSolic"
                                                                                                        UniqueName="CodUsuSolic">
                                                                                                    </rad:GridBoundColumn>
                                                                                                    <rad:GridBoundColumn DataField="UsuarioSolic" SortExpression="UsuarioSolic" HeaderText="Solicitante"
                                                                                                        UniqueName="UsuarioSolic">
                                                                                                        <ItemStyle Width="130px" />
                                                                                                    </rad:GridBoundColumn>
                                                                                                    <rad:GridBoundColumn DataField="CodUsuarioAprov" Visible="False" HeaderText="CodUsuarioAprov"
                                                                                                        UniqueName="CodUsuarioAprov">
                                                                                                    </rad:GridBoundColumn>
                                                                                                    <rad:GridTemplateColumn DataField="UsuarioAprov" SortExpression="UsuarioAprov" HeaderText="&#218;ltimo Aprovador"
                                                                                                        UniqueName="TUsuarioAprov">
                                                                                                        <ItemStyle Width="168px" />
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblUsuarioAprov" runat="server"></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                    </rad:GridTemplateColumn>
                                                                                                    <rad:GridBoundColumn DataField="PendenteCom" SortExpression="PendenteCom" HeaderText="Pendente com"
                                                                                                        UniqueName="PendenteCom">
                                                                                                        <ItemStyle Width="168px" />
                                                                                                    </rad:GridBoundColumn>
                                                                                                    <rad:GridBoundColumn DataField="Visitante" SortExpression="Visitante" HeaderText="Visitante"
                                                                                                        Visible="False" UniqueName="Visitante">
                                                                                                    </rad:GridBoundColumn>
                                                                                                    <rad:GridBoundColumn DataField="CodColaboradorLista" SortExpression="CodColaboradorLista"
                                                                                                        HeaderText="CodColaboradorLista" Visible="False" UniqueName="CodColaboradorLista">
                                                                                                    </rad:GridBoundColumn>
                                                                                                    <rad:GridTemplateColumn Visible="False" UniqueName="TemplateColumn4">
                                                                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                                                        <ItemTemplate>
                                                                                                            <asp:ImageButton ID="imgEditar" runat="server" ImageUrl="~/imagens/icones/ico_editar.gif"
                                                                                                                CommandName="Visualizar" Visible="false"></asp:ImageButton>
                                                                                                        </ItemTemplate>
                                                                                                    </rad:GridTemplateColumn>
                                                                                                    <rad:GridBoundColumn DataField="CorFonte" SortExpression="CorFonte" HeaderText="CorFonte"
                                                                                                        Visible="False" UniqueName="CorFonte">
                                                                                                    </rad:GridBoundColumn>
                                                                                                </Columns>
                                                                                                <RowIndicatorColumn Visible="False">
                                                                                                    <HeaderStyle Width="20px" />
                                                                                                </RowIndicatorColumn>
                                                                                                <ExpandCollapseColumn Resizable="False" Visible="False">
                                                                                                    <HeaderStyle Width="20px" />
                                                                                                </ExpandCollapseColumn>
                                                                                            </MasterTableView>
                                                                                        </rad:RadGrid>
                                                                                        <!-- ********************* END: RADGRID ************************** -->
                                                                                    </div>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="cadlbl">
                                                                                    <asp:Label ID="Label13" runat="server" Text="Permissão de Crachá Titular"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td id="tdCrachaTitular" runat="server" align="center" style="height: 304px; width: 726px;">
                                                                                    <div id="divCrachaTitular" runat="server" style="width: 720px; overflow: auto; height: 300px; text-align: center;">



                                                                                        <!-- ********************* STAR: RADGRID ************************** -->
                                                                                        <rad:RadGrid ID="radCrachaTitular" runat="server" AllowPaging="True" AllowSorting="True"
                                                                                            AutoGenerateColumns="False" SortingSettings-SortToolTip="Clique para filtrar" CssClass="dtg" GridLines="None" Skin="None" Width="1500px"
                                                                                            OnItemDataBound="radCrachaTitular_ItemDataBound" OnItemCommand="radCrachaTitular_ItemCommand"
                                                                                            OnNeedDataSource="radCrachaTitular_NeedDataSource">
                                                                                            <GroupHeaderItemStyle CssClass="dtgGroupHeader"></GroupHeaderItemStyle>
                                                                                            <GroupPanel Text="">
                                                                                                <PanelStyle CssClass="dtgGroupPanel"></PanelStyle>
                                                                                                <PanelItemsStyle CssClass="dtgGroupPanelItem"></PanelItemsStyle>
                                                                                            </GroupPanel>
                                                                                            <AlternatingItemStyle CssClass="dtgItemStyleAlternate"></AlternatingItemStyle>
                                                                                            <ItemStyle CssClass="dtgItemStyle"></ItemStyle>
                                                                                            <HeaderStyle CssClass="dtgHeaderStyle"></HeaderStyle>
                                                                                            <MasterTableView>
                                                                                                <NoRecordsTemplate>
                                                                                                    <div>
                                                                                                        <table cellpadding="2px" cellspacing="0" height="50px" width="100%">
                                                                                                            <tr>
                                                                                                                <td class="cadlbl">
                                                                                                                    <% = FrameWork.BusinessLayer.Idioma.BLIdiomas.TraduzirMensagens(FrameWork.Model.Idioma.Mensagens.CONSULTA_SEM_RETORNO) %>
                                                                                                                </td>
                                                                                                            </tr>
                                                                        </table>
                                                                                                    </div>
                                                                                                </NoRecordsTemplate>
                                                                                                <PagerTemplate>
                                                                                                    <table align="center" border="0" cellpadding="5" cellspacing="0" class="pag" height="15"
                                                                                                        width="100%">
                                                                                                        <tr>
                                                                                                            <td align="center" valign="center">
                                                                                                                <asp:Panel ID="pnlPaginaAtual" runat="server" DefaultButton="btnIr">
                                                                                                                    <asp:ImageButton ID="imgPrimeira" runat="server" CausesValidation="false" CommandArgument="First"
                                                                                                                        CommandName="Page" CssClass="pagImg" ImageUrl="~/imagens/icones/ico_setaesq.gif" />
                                                                                                                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandArgument="First"
                                                                                                                        CommandName="Page" CssClass="pagLink" Enabled="True">Primeira</asp:LinkButton>
                                                                                                                    <asp:ImageButton ID="imgAnterior" runat="server" CausesValidation="false" CommandArgument="Prev"
                                                                                                                        CommandName="Page" CssClass="pagImg" ImageUrl="~/imagens/icones/ico_setaesq2.gif" />
                                                                                                                    <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandArgument="Prev"
                                                                                                                        CommandName="Page" CssClass="pagLink" Enabled="True">Anterior</asp:LinkButton>&#160;&#160;
                                                                                                                    <asp:Label ID="Label1" runat="server" CssClass="paglbl">| Página:</asp:Label>
                                                                                                                    <cc1:FWMascara ID="txtPagina" runat="server" AutoPostBack="False" CssClass="pagtxtbox"
                                                                                                                        Mascara="NUMERO" MaxLength="4" Text='<%# (int)(DataBinder.Eval(Container, "OwnerTableView.CurrentPageIndex")) + 1 %>'
                                                                                                                        Width="50px"></cc1:FWMascara>
                                                                                                                    <asp:Label ID="Label2" runat="server" CssClass="paglbl">de</asp:Label>
                                                                                                                    <asp:Label ID="Label3" runat="server" CssClass="paglbl" Enabled="True"><%#DataBinder.Eval(Container, "Paging.PageCount").ToString()%></asp:Label>
                                                                                                                    <asp:LinkButton ID="btnIr" runat="server" CommandName="IrPagina" CssClass="pagLink">Ir</asp:LinkButton>
                                                                                                                    <span class="paglbl">&#160;|&#160;</span>
                                                                                                                    <asp:LinkButton ID="LinkButton3" runat="server" CausesValidation="False" CommandArgument="Next"
                                                                                                                        CommandName="Page" CssClass="pagLink" Enabled="True">Próxima</asp:LinkButton>
                                                                                                                    <asp:ImageButton ID="imgProxima" runat="server" CausesValidation="false" CommandArgument="Next"
                                                                                                                        CommandName="Page" CssClass="pagImg" ImageUrl="~/imagens/icones/ico_setadir2.gif" />
                                                                                                                    <asp:LinkButton ID="LinkButton4" runat="server" CausesValidation="False" CommandArgument="Last"
                                                                                                                        CommandName="Page" CssClass="pagLink" Enabled="True">Última</asp:LinkButton>
                                                                                                                    <asp:ImageButton ID="imgUltima" runat="server" CausesValidation="false" CommandArgument="Last"
                                                                                                                        CommandName="Page" CssClass="pagImg" ImageUrl="~/imagens/icones/ico_setadir.gif" />
                                                                                                                </asp:Panel>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                    </table>
                                                                                                </PagerTemplate>
                                                                                                <Columns>
                                                                                                    <rad:GridTemplateColumn UniqueName="TemplateColumn">
                                                                                                        <HeaderTemplate>
                                                                                                            <asp:CheckBox ID="chkTodosItems" runat="server" AutoPostBack="True" OnCheckedChanged="chkItemCrachaTitular_CheckedChanged"
                                                                                                                ToolTip="Selecionar todos" />
                                                                                                        </HeaderTemplate>
                                                                                                        <HeaderStyle Width="1%"></HeaderStyle>
                                                                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                                                        <ItemTemplate>
                                                                                                            <asp:CheckBox ID="chkItem" runat="server" />
                                                                                                        </ItemTemplate>
                                                                                                    </rad:GridTemplateColumn>
                                                                                                    <rad:GridBoundColumn DataField="Id_SolicitacaoCrachaTitular" SortExpression="Id_SolicitacaoCrachaTitular" HeaderText="N&#186; Solic."
                                                                                                        UniqueName="Id_SolicitacaoCrachaTitular">
                                                                                                        <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                                                                    </rad:GridBoundColumn>

                                                                                                    <rad:GridBoundColumn DataField="Id_Filial" Visible="False" HeaderText="Id_Filial"
                                                                                                        UniqueName="Id_Filial">
                                                                                                    </rad:GridBoundColumn>

                                                                                                    <rad:GridBoundColumn DataField="Alias_Filial" SortExpression="Alias_Filial" HeaderText="Filial"
                                                                                                        UniqueName="Alias_Filial">
                                                                                                        <ItemStyle Width="2%" />
                                                                                                    </rad:GridBoundColumn>

                                                                                                    <rad:GridBoundColumn DataField="Id_Colaborador" Visible="False" HeaderText="Id_Colaborador"
                                                                                                        UniqueName="Id_Colaborador">
                                                                                                    </rad:GridBoundColumn>
                                                                                                    <rad:GridBoundColumn DataField="Nom_Colaborador" SortExpression="Nom_Colaborador" HeaderText="Colaborador"
                                                                                                        UniqueName="Nom_Colaborador">
                                                                                                        <ItemStyle Width="10%" />
                                                                                                    </rad:GridBoundColumn>

                                                                                                    <rad:GridBoundColumn DataField="Des_Area" HeaderText="Áreas Solicitadas" SortExpression="Des_Area"
                                                                                                        UniqueName="Des_Area">
                                                                                                        <ItemStyle Width="10%" HorizontalAlign="Center"></ItemStyle>
                                                                                                    </rad:GridBoundColumn>


                                                                                                    <rad:GridBoundColumn DataField="Id_TipoSolicitacao" Visible="False" HeaderText="Id_TipoSolicitacao"
                                                                                                        UniqueName="Id_TipoSolicitacao">
                                                                                                    </rad:GridBoundColumn>

                                                                                                    <rad:GridBoundColumn DataField="Des_TipoSolicitacao" SortExpression="Des_TipoSolicitacao"
                                                                                                        HeaderText="Tipo Solic." UniqueName="Des_TipoSolicitacao">
                                                                                                        <ItemStyle Width="5%" />
                                                                                                    </rad:GridBoundColumn>

                                                                                                    <rad:GridBoundColumn DataField="Data_Solicitacao" SortExpression="Data_Solicitacao" HeaderText="Data/Hora Solic."
                                                                                                        UniqueName="Data_Solicitacao" DataFormatString="{0:dd/MM/yyyy HH:mm}">
                                                                                                        <ItemStyle Width="5%" />
                                                                                                    </rad:GridBoundColumn>

                                                                                                    <rad:GridBoundColumn DataField="Id_StatusSolicitacao" Visible="False" HeaderText="Id_StatusSolicitacao"
                                                                                                        UniqueName="Id_StatusSolicitacao">
                                                                                                    </rad:GridBoundColumn>

                                                                                                    <rad:GridBoundColumn DataField="Des_StatusAprovacao" SortExpression="Des_StatusAprovacao" HeaderText="Status"
                                                                                                        UniqueName="Des_StatusAprovacao">
                                                                                                        <ItemStyle Width="5%" />
                                                                                                    </rad:GridBoundColumn>

                                                                                                    <rad:GridBoundColumn DataField="Id_UsuarioSolicitante" Visible="False" HeaderText="Id_UsuarioSolicitante"
                                                                                                        UniqueName="Id_UsuarioSolicitante">
                                                                                                    </rad:GridBoundColumn>

                                                                                                    <rad:GridBoundColumn DataField="Nom_UsuarioSolicitante" SortExpression="Nom_UsuarioSolicitante" HeaderText="Solicitante"
                                                                                                        UniqueName="Nom_UsuarioSolicitante">
                                                                                                        <ItemStyle Width="5%" />
                                                                                                    </rad:GridBoundColumn>

                                                                                                    <rad:GridTemplateColumn Visible="False" UniqueName="TemplateColumn4">
                                                                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                                                        <ItemTemplate>
                                                                                                            <asp:ImageButton ID="imgEditar" runat="server" ImageUrl="~/imagens/icones/ico_editar.gif"
                                                                                                                CommandName="Visualizar" Visible="false"></asp:ImageButton>
                                                                                                        </ItemTemplate>
                                                                                                    </rad:GridTemplateColumn>
                                                                                                    <rad:GridBoundColumn DataField="CorFonte" SortExpression="CorFonte" HeaderText="CorFonte"
                                                                                                        Visible="False" UniqueName="CorFonte">
                                                                                                    </rad:GridBoundColumn>
                                                                                                </Columns>
                                                                                                <RowIndicatorColumn Visible="False">
                                                                                                    <HeaderStyle Width="20px" />
                                                                                                </RowIndicatorColumn>
                                                                                                <ExpandCollapseColumn Resizable="False" Visible="False">
                                                                                                    <HeaderStyle Width="20px" />
                                                                                                </ExpandCollapseColumn>
                                                                                            </MasterTableView>
                                                                                        </rad:RadGrid>
                                                                                        <!-- ********************* END: RADGRID ************************** -->
                                                                                    </div>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                        <!-- ********************* START: PAINEL OBSERVACAO ************************** -->
                                                                        <asp:Panel ID="pnlObservacao" Visible="false" runat="server">
                                                                            <br />
                                                                            <table style="border-right: #cccccc 1px solid; border-top: #cccccc 1px solid; padding: 5px;
                                                                                border-left: #cccccc 1px solid; padding-top: 5px; border-bottom: #cccccc 1px solid"
                                                                                cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                                <tr>
                                                                                    <td align="center">
                                                                                        <table width="720">
                                                                                            <tr>
                                                                                                <td class="cadlbl">
                                                                                                    <asp:Label ID="lblObservacao" runat="server" Text="Observação:"></asp:Label>
                                                                                                    <asp:RequiredFieldValidator ID="rfvObs" runat="server" ControlToValidate="txtObservacao"
                                                                                                        CssClass="cadlbl" ErrorMessage="Campo Obrigatório" ValidationGroup="Obs">*</asp:RequiredFieldValidator>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <asp:TextBox ID="txtObservacao" runat="server" CssClass="dtg" MaxLength="149" Width="100%"
                                                                                                        Height="50px" Font-Names="Verdana" Font-Size="XX-Small" TextMode="MultiLine"></asp:TextBox>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td colspan="7">
                                                                                                    <!-- ********************* START: BARRA DE BOTÕES INFERIOR ************************** -->
                                                                                                    <table align="center" border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                                                        <tr>
                                                                                                            <td align="center">
                                                                                                                <asp:Button ID="btnGravar" runat="server" CssClass="cadbutton100" Text="Gravar" OnClick="btnGravar_Click"
                                                                                                                    ValidationGroup="Obs" />
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                    </table>
                                                                                                    <!-- ********************* END: BARRA DE BOTÕES INFERIOR ************************** -->
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                            <br />
                                                                        </asp:Panel>
                                                                        <!-- ********************* END: PAINEL OBSERVACAO ************************** -->
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                <tr>
                                                                    <td class="backBarraBotoes" align="right" style="height: 30px">
                                                                        <asp:Button ID="btnAprovar" runat="server" Text="Aprovar" CssClass="cadbutton100Verde"
                                                                            OnClick="btnAprovar_Click" CausesValidation="False"></asp:Button>
                                                                        <asp:Button ID="btnReprovar" runat="server" Text="Reprovar" CssClass="cadbutton100Vermelho"
                                                                            OnClick="btnReprovar_Click" CausesValidation="False"></asp:Button>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                        <!-- ****************** END: PRIMEIRA ABA **************************-->
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <!-- ****************** START: SEGUNDA ABA ************************** -->
                                                        <div id="divAbaHistorico" style="border-right: #d6d6d6 1px solid; border-left: #d6d6d6 1px solid;
                                                            width: 740px; border-bottom: #d6d6d6 1px solid; border-right: #d6d6d6 1px solid;
                                                            background-color: #ecf1f7" align="center" runat="server">
                                                            <table class="MarginTopConteudo" cellspacing="0" cellpadding="0" width="720" align="center"
                                                                border="0">
                                                                <tr>
                                                                    <td>
                                                                        <table style="border-right: #cccccc 1px solid; border-top: #cccccc 1px solid; padding: 5px;
                                                                            border-left: #cccccc 1px solid; padding-top: 5px; border-bottom: #cccccc 1px solid"
                                                                            cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                            <tr>
                                                                                <td align="center">
                                                                                    <table width="720px" cellspacing="0" cellpadding="0" border="0">
                                                                                        <tr>
                                                                                            <td colspan="7">
                                                                                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List" />
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td class="cadlbl" width="170px">
                                                                                                <asp:Label ID="lblNumSolicitacao" runat="server" Text="Nº Solicitação:"></asp:Label>
                                                                                            </td>
                                                                                            <td width="5px">
                                                                                            </td>
                                                                                            <td class="cadlbl" style="width: 172px">
                                                                                                <asp:Label ID="lblEmpresa" runat="server" Text="Empresa:"></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                            </td>
                                                                                            <td class="cadlbl" width="170px">
                                                                                                <asp:Label ID="lblRegional" runat="server" Text="Regional:"></asp:Label>
                                                                                            </td>
                                                                                            <td width="5px">
                                                                                            </td>
                                                                                            <td class="cadlbl" width="170px">
                                                                                                <asp:Label ID="lblFilial" runat="server" Text="Filial:"></asp:Label>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtNumSolicitacao" runat="server" CssClass="cadtxt" MaxLength="8"
                                                                                                    Width="75px"></asp:TextBox>
                                                                                            </td>
                                                                                            <td>
                                                                                            </td>
                                                                                            <td style="width: 172px">
                                                                                                <asp:DropDownList ID="ddlEmpresa" CssClass="cadddl" runat="server" Width="165px">
                                                                                                </asp:DropDownList>
                                                                                            </td>
                                                                                            <td>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:DropDownList ID="ddlRegional" CssClass="cadddl" runat="server" Width="165px"
                                                                                                    AutoPostBack="True" OnSelectedIndexChanged="ddlRegional_SelectedIndexChanged">
                                                                                                </asp:DropDownList>
                                                                                            </td>
                                                                                            <td>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:DropDownList ID="ddlFilial" CssClass="cadddl" runat="server" Width="165px" Enabled="False">
                                                                                                </asp:DropDownList>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td class="cadlbl">
                                                                                                <asp:Label ID="lblStatus" runat="server" Text="Status:"></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                            </td>
                                                                                            <td class="cadlbl" style="width: 172px">
                                                                                                <asp:Label ID="lblTipoSolicitacao" runat="server" Text="Tipo Solicitação:"></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                            </td>
                                                                                            <td class="cadlbl">
                                                                                                <asp:Label ID="lblDataInicio" runat="server" Text="Data Início:"></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                            </td>
                                                                                            <td class="cadlbl">
                                                                                                <asp:Label ID="lblDataFim" runat="server" Text="Data Fim:"></asp:Label>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <asp:DropDownList ID="ddlStatus" CssClass="cadddl" runat="server" Width="165px">
                                                                                                </asp:DropDownList>
                                                                                            </td>
                                                                                            <td>
                                                                                            </td>
                                                                                            <td style="width: 172px">
                                                                                                <asp:DropDownList ID="ddlTipoSolicitacao" CssClass="cadddl" runat="server" Width="165px">
                                                                                                </asp:DropDownList>
                                                                                            </td>
                                                                                            <td>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtDataInicio" runat="server" CssClass="cadtxt" Width="145px" MaxLength="16"></asp:TextBox>
                                                                                                <a hidefocus onclick="if(self.gfPop)gfPop.fPopCalendar(document.forms[0].all('<% = this.txtDataInicio.ClientID.ToString() %>'));return false;"
                                                                                                    href="javascript:void(0)">
                                                                                                    <img height="19" alt="" src="../../Agenda/calbtn.gif" width="20" align="absMiddle"
                                                                                                        border="0" name="popcal"></a>
                                                                                            </td>
                                                                                            <td>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtDataFim" runat="server" CssClass="cadtxt" Width="145px" MaxLength="16"></asp:TextBox>
                                                                                                <a hidefocus onclick="if(self.gfPop)gfPop.fPopCalendar(document.forms[0].all('<% = this.txtDataFim.ClientID.ToString() %>'));return false;"
                                                                                                    href="javascript:void(0)">
                                                                                                    <img height="19" alt="" src="../../Agenda/calbtn.gif" width="20" align="absMiddle"
                                                                                                        border="0" name="popcal"></a><asp:CompareValidator ID="cpvDataFim" runat="server"
                                                                                                            ControlToCompare="txtDataInicio" ControlToValidate="txtDataFim" ErrorMessage="A Data Fim deve ser maior que a Data Início"
                                                                                                            Operator="GreaterThanEqual">*</asp:CompareValidator>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td class="cadlbl" colspan="3">
                                                                                                <asp:Label ID="lblVisitado" runat="server" Text="Visitado:"></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                            </td>
                                                                                            <td class="cadlbl" colspan="3">
                                                                                                <asp:Label ID="lblVisitante" runat="server" Text="Visitante:"></asp:Label>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td colspan="3">
                                                                                                <asp:TextBox ID="txtNomeVisitado" runat="server" CssClass="cadtxt" MaxLength="50"
                                                                                                    Width="343px"></asp:TextBox>
                                                                                            </td>
                                                                                            <td>
                                                                                            </td>
                                                                                            <td colspan="3">
                                                                                                <asp:TextBox ID="txtNomeVisitante" runat="server" CssClass="cadtxt" MaxLength="50"
                                                                                                    Width="343px"></asp:TextBox>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td class="cadlbl" colspan="3">
                                                                                                <asp:Label ID="lblSolicitante" runat="server" Text="Solicitante:"></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                            </td>
                                                                                            <td class="cadlbl" colspan="3">
                                                                                                <asp:Label ID="lblAprovador" runat="server" Text="Aprovador:"></asp:Label>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td colspan="3">
                                                                                                <asp:TextBox ID="txtNomeSolicitante" runat="server" CssClass="cadtxt" MaxLength="50"
                                                                                                    Width="343px"></asp:TextBox>
                                                                                            </td>
                                                                                            <td>
                                                                                            </td>
                                                                                            <td colspan="3">
                                                                                                <asp:TextBox ID="txtNomeAprovador" runat="server" CssClass="cadtxt" MaxLength="50"
                                                                                                    Width="343px"></asp:TextBox>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td colspan="7" align="center">
                                                                                                <br />
                                                                                                <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="cadbuttonfiltro"
                                                                                                    OnClick="btnBuscar_Click" />
                                                                                                <br />
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                    <table width="720px" cellspacing="0" cellpadding="0" border="0">
                                                                                        <tr>
                                                                                            <td align="center" colspan="3">
                                                                                                <br />
                                                                                                <div style="width: 720px; overflow: auto; height: 310px; text-align: center;">
                                                                                                    <!-- ********************* STAR: RADGRID ************************** -->
                                                                                                    <rad:RadGrid ID="radHistorico" runat="server" AllowPaging="True" AllowSorting="True"
                                                                                                        AutoGenerateColumns="False" CssClass="dtg" GridLines="None" Skin="None" Height="290px"
                                                                                                        Width="920px" OnItemDataBound="radHistorico_ItemDataBound" OnItemCommand="radHistorico_ItemCommand"
                                                                                                        OnNeedDataSource="radHistorico_NeedDataSource">
                                                                                                        <GroupHeaderItemStyle CssClass="dtgGroupHeader"></GroupHeaderItemStyle>
                                                                                                        <GroupPanel Text="">
                                                                                                            <PanelStyle CssClass="dtgGroupPanel"></PanelStyle>
                                                                                                            <PanelItemsStyle CssClass="dtgGroupPanelItem"></PanelItemsStyle>
                                                                                                        </GroupPanel>
                                                                                                        <AlternatingItemStyle CssClass="dtgItemStyleAlternate"></AlternatingItemStyle>
                                                                                                        <ItemStyle CssClass="dtgItemStyle"></ItemStyle>
                                                                                                        <HeaderStyle CssClass="dtgHeaderStyle"></HeaderStyle>
                                                                                                        <MasterTableView>
                                                                                                            <NoRecordsTemplate>
                                                                                                                <div>
                                                                                                                    <table cellpadding="2px" cellspacing="0" height="50px" width="100%">
                                                                                                                        <tr>
                                                                                                                            <td class="cadlbl">
                                                                                                                                <% = FrameWork.BusinessLayer.Idioma.BLIdiomas.TraduzirMensagens(FrameWork.Model.Idioma.Mensagens.CONSULTA_SEM_RETORNO) %>
                                                                                                                            </td>
                                                                                                                        </tr>
                                                                                                                    </table>
                                                                                                                </div>
                                                                                                            </NoRecordsTemplate>
                                                                                                            <PagerTemplate>
                                                                                                                <table align="center" border="0" cellpadding="5" cellspacing="0" class="pag" height="15"
                                                                                                                    width="100%">
                                                                                                                    <tr>
                                                                                                                        <td align="center" valign="center">
                                                                                                                            <asp:Panel ID="pnlPaginaAtual" runat="server" DefaultButton="btnIr">
                                                                                                                                <asp:ImageButton ID="imgPrimeira" runat="server" CausesValidation="false" CommandArgument="First"
                                                                                                                                    CommandName="Page" CssClass="pagImg" ImageUrl="~/imagens/icones/ico_setaesq.gif" />
                                                                                                                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandArgument="First"
                                                                                                                                    CommandName="Page" CssClass="pagLink" Enabled="True">Primeira</asp:LinkButton>
                                                                                                                                <asp:ImageButton ID="imgAnterior" runat="server" CausesValidation="false" CommandArgument="Prev"
                                                                                                                                    CommandName="Page" CssClass="pagImg" ImageUrl="~/imagens/icones/ico_setaesq2.gif" />
                                                                                                                                <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandArgument="Prev"
                                                                                                                                    CommandName="Page" CssClass="pagLink" Enabled="True">Anterior</asp:LinkButton>&#160;&#160;
                                                                                                                                <asp:Label ID="Label1" runat="server" CssClass="paglbl">| Página:</asp:Label>
                                                                                                                                <cc1:FWMascara ID="txtPagina2" runat="server" AutoPostBack="False" CssClass="pagtxtbox"
                                                                                                                                    Mascara="NUMERO" MaxLength="4" Text='<%# (int)(DataBinder.Eval(Container, "OwnerTableView.CurrentPageIndex")) + 1 %>'
                                                                                                                                    Width="50px"></cc1:FWMascara>
                                                                                                                                <asp:Label ID="Label2" runat="server" CssClass="paglbl">de</asp:Label>
                                                                                                                                <asp:Label ID="Label3" runat="server" CssClass="paglbl" Enabled="True"><%#DataBinder.Eval(Container, "Paging.PageCount").ToString()%></asp:Label>
                                                                                                                                <asp:LinkButton ID="btnIr" runat="server" CommandName="IrPagina" CssClass="pagLink">Ir</asp:LinkButton>
                                                                                                                                <span class="paglbl">&#160;|&#160;</span>
                                                                                                                                <asp:LinkButton ID="LinkButton3" runat="server" CausesValidation="False" CommandArgument="Next"
                                                                                                                                    CommandName="Page" CssClass="pagLink" Enabled="True">Próxima</asp:LinkButton>
                                                                                                                                <asp:ImageButton ID="imgProxima" runat="server" CausesValidation="false" CommandArgument="Next"
                                                                                                                                    CommandName="Page" CssClass="pagImg" ImageUrl="~/imagens/icones/ico_setadir2.gif" />
                                                                                                                                <asp:LinkButton ID="LinkButton4" runat="server" CausesValidation="False" CommandArgument="Last"
                                                                                                                                    CommandName="Page" CssClass="pagLink" Enabled="True">Última</asp:LinkButton>
                                                                                                                                <asp:ImageButton ID="imgUltima" runat="server" CausesValidation="false" CommandArgument="Last"
                                                                                                                                    CommandName="Page" CssClass="pagImg" ImageUrl="~/imagens/icones/ico_setadir.gif" />
                                                                                                                            </asp:Panel>
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                </table>
                                                                                                            </PagerTemplate>
                                                                                                            <Columns>
                                                                                                                <rad:GridBoundColumn DataField="Codigo" SortExpression="Codigo" HeaderText="N&#186; Solic."
                                                                                                                    UniqueName="Codigo">
                                                                                                                    <ItemStyle HorizontalAlign="Center" Width="40px" />
                                                                                                                </rad:GridBoundColumn>
                                                                                                                <rad:GridBoundColumn DataField="CodFilial" Visible="False" HeaderText="CodFilial"
                                                                                                                    UniqueName="CodFilial">
                                                                                                                </rad:GridBoundColumn>
                                                                                                                <rad:GridBoundColumn DataField="AliasFilial" SortExpression="AliasFilial" HeaderText="Filial"
                                                                                                                    UniqueName="AliasFilial">
                                                                                                                    <ItemStyle Width="45px" />
                                                                                                                </rad:GridBoundColumn>
                                                                                                                <rad:GridBoundColumn DataField="CodVisitante" Visible="False" HeaderText="CodVisitante"
                                                                                                                    UniqueName="CodVisitante">
                                                                                                                </rad:GridBoundColumn>
                                                                                                                <rad:GridTemplateColumn DataField="Visitante" SortExpression="Visitante" HeaderText="Visitante"
                                                                                                                    UniqueName="TVisitante">
                                                                                                                    <ItemStyle Width="95px" />
                                                                                                                    <ItemTemplate>
                                                                                                                        <asp:LinkButton ID="lnkVisitante" runat="server" CommandName="Lista" Width="75px"></asp:LinkButton>
                                                                                                                    </ItemTemplate>
                                                                                                                </rad:GridTemplateColumn>
                                                                                                                <rad:GridBoundColumn DataField="CodEmpresa" Visible="False" HeaderText="CodEmpresa"
                                                                                                                    UniqueName="CodEmpresa">
                                                                                                                </rad:GridBoundColumn>
                                                                                                                <rad:GridBoundColumn DataField="Empresa" SortExpression="Empresa" HeaderText="Empresa"
                                                                                                                    UniqueName="Empresa">
                                                                                                                    <ItemStyle Width="60px" />
                                                                                                                </rad:GridBoundColumn>
                                                                                                                <rad:GridBoundColumn DataField="CodTipoSolicitacao" Visible="False" HeaderText="CodTipoSolicitacao"
                                                                                                                    UniqueName="CodTipoSolicitacao">
                                                                                                                </rad:GridBoundColumn>
                                                                                                                <rad:GridBoundColumn DataField="TipoSolicitacao" SortExpression="TipoSolicitacao"
                                                                                                                    HeaderText="Tipo Solic." UniqueName="TipoSolicitacao">
                                                                                                                    <ItemStyle Width="185px" />
                                                                                                                </rad:GridBoundColumn>
                                                                                                                <rad:GridBoundColumn DataField="InicioVisita" SortExpression="InicioVisita" HeaderText="Data/Hora In&#237;cio"
                                                                                                                    UniqueName="InicioVisita" DataFormatString="{0:dd/MM/yyyy HH:mm}">
                                                                                                                    <ItemStyle Width="103px" />
                                                                                                                </rad:GridBoundColumn>
                                                                                                                <rad:GridBoundColumn DataField="FimVisita" SortExpression="FimVisita" HeaderText="Data/Hora Fim"
                                                                                                                    UniqueName="FimVisita" DataFormatString="{0:dd/MM/yyyy HH:mm}">
                                                                                                                    <ItemStyle Width="103px" />
                                                                                                                </rad:GridBoundColumn>
                                                                                                                <rad:GridBoundColumn DataField="AcSabado" SortExpression="AcSabado" HeaderText="S&#225;b."
                                                                                                                    Visible="False" UniqueName="AcSabado">
                                                                                                                </rad:GridBoundColumn>
                                                                                                                <rad:GridTemplateColumn DataField="AcSabado" SortExpression="AcSabado" HeaderText="S&#225;b."
                                                                                                                    UniqueName="TemplateColumn1">
                                                                                                                    <ItemStyle Width="5px" />
                                                                                                                    <ItemTemplate>
                                                                                                                        <asp:CheckBox ID="chkAcSabado" runat="server" Enabled="false" />
                                                                                                                    </ItemTemplate>
                                                                                                                </rad:GridTemplateColumn>
                                                                                                                <rad:GridBoundColumn DataField="AcDomingo" SortExpression="AcDomingo" HeaderText="Dom."
                                                                                                                    Visible="False" UniqueName="AcDomingo">
                                                                                                                </rad:GridBoundColumn>
                                                                                                                <rad:GridTemplateColumn DataField="AcDomingo" SortExpression="AcDomingo" HeaderText="Dom."
                                                                                                                    UniqueName="TemplateColumn2">
                                                                                                                    <ItemStyle Width="5px" />
                                                                                                                    <ItemTemplate>
                                                                                                                        <asp:CheckBox ID="chkAcDomingo" runat="server" Enabled="false" />
                                                                                                                    </ItemTemplate>
                                                                                                                </rad:GridTemplateColumn>
                                                                                                                <rad:GridBoundColumn DataField="AcFeriado" SortExpression="AcFeriado" HeaderText="Fer."
                                                                                                                    Visible="False" UniqueName="AcFeriado">
                                                                                                                </rad:GridBoundColumn>
                                                                                                                <rad:GridTemplateColumn DataField="AcFeriado" SortExpression="AcFeriado" HeaderText="Fer."
                                                                                                                    UniqueName="TemplateColumn3">
                                                                                                                    <ItemStyle Width="5px" />
                                                                                                                    <ItemTemplate>
                                                                                                                        <asp:CheckBox ID="chkAcFeriado" runat="server" Enabled="false" />
                                                                                                                    </ItemTemplate>
                                                                                                                </rad:GridTemplateColumn>
                                                                                                                <rad:GridBoundColumn DataField="CodStatus" Visible="False" HeaderText="CodStatus"
                                                                                                                    UniqueName="CodStatus">
                                                                                                                </rad:GridBoundColumn>
                                                                                                                <rad:GridBoundColumn DataField="Status" SortExpression="Status" HeaderText="Status"
                                                                                                                    UniqueName="Status">
                                                                                                                    <ItemStyle Width="147px" />
                                                                                                                </rad:GridBoundColumn>
                                                                                                                <rad:GridBoundColumn DataField="CodColaboradorLista" SortExpression="CodColaboradorLista"
                                                                                                                    HeaderText="CodColaboradorLista" Visible="False" UniqueName="CodColaboradorLista">
                                                                                                                </rad:GridBoundColumn>
                                                                                                                <rad:GridBoundColumn DataField="Visitante" SortExpression="Visitante" HeaderText="Visitante"
                                                                                                                    Visible="False" UniqueName="Visitante">
                                                                                                                </rad:GridBoundColumn>
                                                                                                                <rad:GridTemplateColumn Visible="False">
                                                                                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                                                                    <ItemTemplate>
                                                                                                                        <asp:ImageButton ID="imgVisualizar" runat="server" ImageUrl="~/imagens/icones/ico_editar.gif"
                                                                                                                            CommandName="Visualizar" Visible="false"></asp:ImageButton>
                                                                                                                    </ItemTemplate>
                                                                                                                </rad:GridTemplateColumn>
                                                                                                                <rad:GridBoundColumn DataField="CorFonte" SortExpression="CorFonte" HeaderText="CorFonte"
                                                                                                                    Visible="False" UniqueName="CorFonte">
                                                                                                                </rad:GridBoundColumn>
                                                                                                            </Columns>
                                                                                                            <RowIndicatorColumn Visible="False">
                                                                                                                <HeaderStyle Width="20px" />
                                                                                                            </RowIndicatorColumn>
                                                                                                            <ExpandCollapseColumn Resizable="False" Visible="False">
                                                                                                                <HeaderStyle Width="20px" />
                                                                                                            </ExpandCollapseColumn>
                                                                                                        </MasterTableView>
                                                                                                    </rad:RadGrid>
                                                                                                    <!-- ********************* END: RADGRID ************************** -->
                                                                                                </div>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                <tr>
                                                                    <td class="backBarraBotoes" align="right" style="height: 30px">
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                        <!-- ****************** END: SEGUNDA ABA ************************** -->
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <!-- ****************** START: TERCEIRA ABA ************************** -->
                                                        <div id="divAbaHistoricoColab" style="border-right: #d6d6d6 1px solid; border-left: #d6d6d6 1px solid;
                                                            width: 740px; border-bottom: #d6d6d6 1px solid; border-right: #d6d6d6 1px solid;
                                                            background-color: #ecf1f7" align="center" runat="server">
                                                            <table class="MarginTopConteudo" cellspacing="0" cellpadding="0" width="720" align="center"
                                                                border="0">
                                                                <tr>
                                                                    <td align="center">
                                                                        <table style="border-right: #cccccc 1px solid; border-top: #cccccc 1px solid; padding: 5px;
                                                                            border-left: #cccccc 1px solid; padding-top: 5px; border-bottom: #cccccc 1px solid"
                                                                            cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                            <tr>
                                                                                <td align="center">
                                                                                    <table width="720px" cellspacing="0" cellpadding="0" border="0">
                                                                                        <tr>
                                                                                            <td colspan="7">
                                                                                                <asp:ValidationSummary ID="ValidationSummary3" runat="server" DisplayMode="List" />
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td class="cadlbl" width="170px">
                                                                                                <asp:Label ID="Label4" runat="server" Text="Nº Escala:"></asp:Label>
                                                                                            </td>
                                                                                            <td width="5px">
                                                                                            </td>
                                                                                            <td class="cadlbl" style="width: 172px">
                                                                                                <asp:Label ID="Label5" runat="server" Text="Descrição Escala:"></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                            </td>
                                                                                            <td class="cadlbl" width="170px">
                                                                                                <asp:Label ID="Label6" runat="server" Text="Regional:"></asp:Label>
                                                                                            </td>
                                                                                            <td width="5px">
                                                                                            </td>
                                                                                            <td class="cadlbl" width="170px">
                                                                                                <asp:Label ID="Label7" runat="server" Text="Filial:"></asp:Label>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtNumSolicitacaoColab" runat="server" CssClass="cadtxt" MaxLength="8"
                                                                                                    Width="75px"></asp:TextBox>
                                                                                            </td>
                                                                                            <td>
                                                                                            </td>
                                                                                            <td style="width: 172px">
                                                                                                <asp:DropDownList ID="ddlEscalaHistColab" CssClass="cadddl" runat="server" Width="165px">
                                                                                                </asp:DropDownList>
                                                                                            </td>
                                                                                            <td>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:DropDownList ID="ddlRegionalHistColab" CssClass="cadddl" runat="server" Width="165px"
                                                                                                    AutoPostBack="True" OnSelectedIndexChanged="ddlRegionalHistColab_SelectedIndexChanged">
                                                                                                </asp:DropDownList>
                                                                                            </td>
                                                                                            <td>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:DropDownList ID="ddlFilialHistColab" CssClass="cadddl" runat="server" Width="165px"
                                                                                                    Enabled="False">
                                                                                                </asp:DropDownList>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td class="cadlbl">
                                                                                                <asp:Label ID="Label8" runat="server" Text="Status:"></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                            </td>
                                                                                            <td class="cadlbl" style="width: 172px">
                                                                                                <asp:Label ID="Label9" runat="server" Text="Tipo Escala:"></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                            </td>
                                                                                            <td class="cadlbl">
                                                                                                <asp:Label ID="Label10" runat="server" Text="Data Início:"></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                            </td>
                                                                                            <td class="cadlbl">
                                                                                                <asp:Label ID="Label11" runat="server" Text="Data Fim:"></asp:Label>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <asp:DropDownList ID="ddlStatusHistColab" CssClass="cadddl" runat="server" Width="165px">
                                                                                                </asp:DropDownList>
                                                                                            </td>
                                                                                            <td>
                                                                                            </td>
                                                                                            <td style="width: 172px">
                                                                                                <asp:DropDownList ID="ddlTipoSolHistColab" CssClass="cadddl" runat="server" Width="165px">
                                                                                                </asp:DropDownList>
                                                                                            </td>
                                                                                            <td>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtDataInicioHistColab" runat="server" CssClass="cadtxt" Width="145px"
                                                                                                    MaxLength="16"></asp:TextBox>
                                                                                                <a hidefocus onclick="if(self.gfPop)gfPop.fPopCalendar(document.forms[0].all('<% = this.txtDataInicio.ClientID.ToString() %>'));return false;"
                                                                                                    href="javascript:void(0)">
                                                                                                    <img height="19" alt="" src="../../Agenda/calbtn.gif" width="20" align="absMiddle"
                                                                                                        border="0" name="popcal"></a>
                                                                                            </td>
                                                                                            <td>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtDataFimHistColab" runat="server" CssClass="cadtxt" Width="145px"
                                                                                                    MaxLength="16"></asp:TextBox>
                                                                                                <a hidefocus onclick="if(self.gfPop)gfPop.fPopCalendar(document.forms[0].all('<% = this.txtDataFim.ClientID.ToString() %>'));return false;"
                                                                                                    href="javascript:void(0)">
                                                                                                    <img height="19" alt="" src="../../Agenda/calbtn.gif" width="20" align="absMiddle"
                                                                                                        border="0" name="popcal"></a><asp:CompareValidator ID="CompareValidator1" runat="server"
                                                                                                            ControlToCompare="txtDataInicio" ControlToValidate="txtDataFim" ErrorMessage="A Data Fim deve ser maior que a Data Início"
                                                                                                            Operator="GreaterThanEqual">*</asp:CompareValidator>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td class="cadlbl" colspan="3">
                                                                                                <asp:Label ID="Label12" runat="server" Text="Colaborador Escalado:"></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                            </td>
                                                                                            <td class="cadlbl" colspan="3">
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td colspan="3">
                                                                                                <asp:TextBox ID="txtColabEscalado" runat="server" CssClass="cadtxt" MaxLength="50"
                                                                                                    Width="343px"></asp:TextBox>
                                                                                            </td>
                                                                                            <td>
                                                                                            </td>
                                                                                            <td colspan="3">
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td class="cadlbl" colspan="3">
                                                                                                <asp:Label ID="Label14" runat="server" Text="Solicitante:"></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                            </td>
                                                                                            <td class="cadlbl" colspan="3">
                                                                                                <asp:Label ID="Label15" runat="server" Text="Aprovador:"></asp:Label>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td colspan="3">
                                                                                                <asp:TextBox ID="txtSolicitanteHistColab" runat="server" CssClass="cadtxt" MaxLength="50"
                                                                                                    Width="343px"></asp:TextBox>
                                                                                            </td>
                                                                                            <td>
                                                                                            </td>
                                                                                            <td colspan="3">
                                                                                                <asp:TextBox ID="txtAprovadorHistColab" runat="server" CssClass="cadtxt" MaxLength="50"
                                                                                                    Width="343px"></asp:TextBox>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td colspan="7" align="center">
                                                                                                <br />
                                                                                                <asp:Button ID="btnBuscarHistColab" runat="server" Text="Buscar" CssClass="cadbuttonfiltro"
                                                                                                    OnClick="btnBuscarHistColab_Click" />
                                                                                                <br />
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                    <table width="720px" cellspacing="0" cellpadding="0" border="0">
                                                                                        <tr>
                                                                                            <td align="center">
                                                                                                <br />
                                                                                                <div style="width: 720px; overflow: auto; height: 310px; text-align: center;">
                                                                                                    <!-- ********************* STAR: RADGRID ************************** -->
                                                                                                    <rad:RadGrid ID="radHistoricoColab" runat="server" AllowPaging="True" AllowSorting="True"
                                                                                                        AutoGenerateColumns="False" CssClass="dtg" GridLines="None" Skin="None" Height="290px"
                                                                                                        Width="920px" OnNeedDataSource="radHistoricoColab_NeedDataSource" OnItemDataBound="radHistoricoColab_ItemDataBound"
                                                                                                        OnItemCommand="radHistoricoColab_ItemCommand">
                                                                                                        <GroupHeaderItemStyle CssClass="dtgGroupHeader"></GroupHeaderItemStyle>
                                                                                                        <GroupPanel Text="">
                                                                                                            <PanelStyle CssClass="dtgGroupPanel"></PanelStyle>
                                                                                                            <PanelItemsStyle CssClass="dtgGroupPanelItem"></PanelItemsStyle>
                                                                                                        </GroupPanel>
                                                                                                        <AlternatingItemStyle CssClass="dtgItemStyleAlternate"></AlternatingItemStyle>
                                                                                                        <ItemStyle CssClass="dtgItemStyle"></ItemStyle>
                                                                                                        <HeaderStyle CssClass="dtgHeaderStyle"></HeaderStyle>
                                                                                                        <MasterTableView>
                                                                                                            <NoRecordsTemplate>
                                                                                                                <div>
                                                                                                                    <table cellpadding="2px" cellspacing="0" height="50px" width="100%">
                                                                                                                        <tr>
                                                                                                                            <td class="cadlbl">
                                                                                                                                <% = FrameWork.BusinessLayer.Idioma.BLIdiomas.TraduzirMensagens(FrameWork.Model.Idioma.Mensagens.CONSULTA_SEM_RETORNO) %>
                                                                                                                            </td>
                                                                                                                        </tr>
                                                                                                                    </table>
                                                                                                                </div>
                                                                                                            </NoRecordsTemplate>
                                                                                                            <PagerTemplate>
                                                                                                                <table align="center" border="0" cellpadding="5" cellspacing="0" class="pag" height="15"
                                                                                                                    width="100%">
                                                                                                                    <tr>
                                                                                                                        <td align="center" valign="center">
                                                                                                                            <asp:Panel ID="pnlPaginaAtual" runat="server" DefaultButton="btnIr">
                                                                                                                                <asp:ImageButton ID="imgPrimeira" runat="server" CausesValidation="false" CommandArgument="First"
                                                                                                                                    CommandName="Page" CssClass="pagImg" ImageUrl="~/imagens/icones/ico_setaesq.gif" />
                                                                                                                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandArgument="First"
                                                                                                                                    CommandName="Page" CssClass="pagLink" Enabled="True">Primeira</asp:LinkButton>
                                                                                                                                <asp:ImageButton ID="imgAnterior" runat="server" CausesValidation="false" CommandArgument="Prev"
                                                                                                                                    CommandName="Page" CssClass="pagImg" ImageUrl="~/imagens/icones/ico_setaesq2.gif" />
                                                                                                                                <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandArgument="Prev"
                                                                                                                                    CommandName="Page" CssClass="pagLink" Enabled="True">Anterior</asp:LinkButton>&#160;&#160;
                                                                                                                                <asp:Label ID="Label1" runat="server" CssClass="paglbl">| Página:</asp:Label>
                                                                                                                                <cc1:FWMascara ID="txtPagina2" runat="server" AutoPostBack="False" CssClass="pagtxtbox"
                                                                                                                                    Mascara="NUMERO" MaxLength="4" Text='<%# (int)(DataBinder.Eval(Container, "OwnerTableView.CurrentPageIndex")) + 1 %>'
                                                                                                                                    Width="50px"></cc1:FWMascara>
                                                                                                                                <asp:Label ID="Label2" runat="server" CssClass="paglbl">de</asp:Label>
                                                                                                                                <asp:Label ID="Label3" runat="server" CssClass="paglbl" Enabled="True"><%#DataBinder.Eval(Container, "Paging.PageCount").ToString()%></asp:Label>
                                                                                                                                <asp:LinkButton ID="btnIr" runat="server" CommandName="IrPagina" CssClass="pagLink">Ir</asp:LinkButton>
                                                                                                                                <span class="paglbl">&#160;|&#160;</span>
                                                                                                                                <asp:LinkButton ID="LinkButton3" runat="server" CausesValidation="False" CommandArgument="Next"
                                                                                                                                    CommandName="Page" CssClass="pagLink" Enabled="True">Próxima</asp:LinkButton>
                                                                                                                                <asp:ImageButton ID="imgProxima" runat="server" CausesValidation="false" CommandArgument="Next"
                                                                                                                                    CommandName="Page" CssClass="pagImg" ImageUrl="~/imagens/icones/ico_setadir2.gif" />
                                                                                                                                <asp:LinkButton ID="LinkButton4" runat="server" CausesValidation="False" CommandArgument="Last"
                                                                                                                                    CommandName="Page" CssClass="pagLink" Enabled="True">Última</asp:LinkButton>
                                                                                                                                <asp:ImageButton ID="imgUltima" runat="server" CausesValidation="false" CommandArgument="Last"
                                                                                                                                    CommandName="Page" CssClass="pagImg" ImageUrl="~/imagens/icones/ico_setadir.gif" />
                                                                                                                            </asp:Panel>
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                </table>
                                                                                                            </PagerTemplate>
                                                                                                            <Columns>
                                                                                                                <rad:GridBoundColumn DataField="IdEscalacao" SortExpression="IdEscalacao" HeaderText="N&#186; Escala"
                                                                                                                    UniqueName="IdEscalacao">
                                                                                                                    <ItemStyle Width="30px" HorizontalAlign="Center" />
                                                                                                                </rad:GridBoundColumn>
                                                                                                                <rad:GridBoundColumn DataField="IdFilial" Visible="False" HeaderText="IdFilial" UniqueName="IdFilial">
                                                                                                                </rad:GridBoundColumn>
                                                                                                                <rad:GridBoundColumn DataField="AliasFilial" SortExpression="AliasFilial" HeaderText="Filial"
                                                                                                                    UniqueName="AliasFilial">
                                                                                                                    <ItemStyle Width="100px" HorizontalAlign="center" />
                                                                                                                </rad:GridBoundColumn>
                                                                                                                <rad:GridBoundColumn DataField="IdEscalaDepto" Visible="False" HeaderText="IdEscalaDepto"
                                                                                                                    UniqueName="IdEscalaDepto">
                                                                                                                </rad:GridBoundColumn>
                                                                                                                <rad:GridBoundColumn DataField="DesEscalaDepto" SortExpression="DesEscalaDepto" Visible="False"
                                                                                                                    UniqueName="DesEscalaDepto">
                                                                                                                </rad:GridBoundColumn>
                                                                                                                <rad:GridTemplateColumn DataField="DesEscalaDepto" SortExpression="DesEscalaDepto"
                                                                                                                    HeaderText="Descrição Escala" UniqueName="DesEscalaDepto">
                                                                                                                    <ItemStyle Width=" 115px" HorizontalAlign="Center" />
                                                                                                                    <ItemTemplate>
                                                                                                                        <asp:LinkButton ID="lnkColaboradorEscalado" runat="server" CommandName="VisualizarColHist"
                                                                                                                            Width="75px"></asp:LinkButton>
                                                                                                                    </ItemTemplate>
                                                                                                                </rad:GridTemplateColumn>
                                                                                                                <rad:GridBoundColumn DataField="IdTipoSolicitacao" Visible="False" HeaderText="IdTipoSolicitacao"
                                                                                                                    UniqueName="IdTipoSolicitacao">
                                                                                                                </rad:GridBoundColumn>
                                                                                                                <rad:GridBoundColumn DataField="DesTipoSolicitacao" SortExpression="DesTipoSolicitacao"
                                                                                                                    HeaderText="Tipo Escala" UniqueName="DesTipoSolicitacao">
                                                                                                                    <ItemStyle Width="175px" HorizontalAlign="Center" />
                                                                                                                    <HeaderStyle HorizontalAlign="center" />
                                                                                                                </rad:GridBoundColumn>
                                                                                                                <rad:GridBoundColumn DataField="Periodo" SortExpression="Periodo" HeaderText="Período"
                                                                                                                    UniqueName="Periodo">
                                                                                                                    <HeaderStyle Width="170px" HorizontalAlign="center" />
                                                                                                                    <ItemStyle />
                                                                                                                </rad:GridBoundColumn>
                                                                                                                <rad:GridBoundColumn DataField="IdStatusSolicitacao" Visible="False" HeaderText="IdStatus"
                                                                                                                    UniqueName="IdStatusSolicitacao">
                                                                                                                </rad:GridBoundColumn>
                                                                                                                <rad:GridBoundColumn DataField="DescricaoStatusAprovacao" SortExpression="DescricaoStatusAprovacao"
                                                                                                                    HeaderText="Status" UniqueName="DescricaoStatusAprovacao">
                                                                                                                    <HeaderStyle Width="140px" />
                                                                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                                                                </rad:GridBoundColumn>
                                                                                                                <rad:GridBoundColumn DataField="NomeUsuarioSolicitante" SortExpression="NomeUsuarioSolicitante"
                                                                                                                    HeaderText="Solicitante" UniqueName="NomeUsuarioSolicitante">
                                                                                                                    <HeaderStyle Width="100px" />
                                                                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                                                                </rad:GridBoundColumn>
                                                                                                                <rad:GridTemplateColumn DataField="UsuarioAprov" SortExpression="UsuarioAprov" HeaderText="Último Aprovador"
                                                                                                                    UniqueName="TUsuarioAprov">
                                                                                                                    <ItemStyle Width="100px" />
                                                                                                                    <ItemTemplate>
                                                                                                                        <asp:Label ID="lblUsuarioAprov" runat="server"></asp:Label>
                                                                                                                    </ItemTemplate>
                                                                                                                </rad:GridTemplateColumn>
                                                                                                                <rad:GridBoundColumn DataField="PendenteCom" SortExpression="PendenteCom" HeaderText="Pendente Com"
                                                                                                                    UniqueName="PendenteCom">
                                                                                                                    <HeaderStyle Width="100px" />
                                                                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                                                                </rad:GridBoundColumn>
                                                                                                                <rad:GridTemplateColumn UniqueName="TemplateColumn" Visible="False">
                                                                                                                    <ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
                                                                                                                    <ItemTemplate>
                                                                                                                        <asp:ImageButton ID="btnVisualizar" runat="server" AlternateText="Visualizar" CausesValidation="False"
                                                                                                                            CommandArgument='<%# DataBinder.Eval(Container.DataItem, "IdEscalacao") %>' CommandName="Visualizar"
                                                                                                                            ImageUrl="%%PATH%%/imagens/icones/ico_editar.gif"></asp:ImageButton>
                                                                                                                    </ItemTemplate>
                                                                                                                </rad:GridTemplateColumn>
                                                                                                                <rad:GridBoundColumn DataField="CorFonteHtml" SortExpression="CorFonteHtml" HeaderText="CorFonteHtml"
                                                                                                                    Visible="False" UniqueName="CorFonteHtml">
                                                                                                                </rad:GridBoundColumn>
                                                                                                            </Columns>
                                                                                                            <RowIndicatorColumn Visible="False">
                                                                                                                <HeaderStyle Width="20px" />
                                                                                                            </RowIndicatorColumn>
                                                                                                            <ExpandCollapseColumn Resizable="False" Visible="False">
                                                                                                                <HeaderStyle Width="20px" />
                                                                                                            </ExpandCollapseColumn>
                                                                                                        </MasterTableView>
                                                                                                    </rad:RadGrid>
                                                                                                    <!-- ********************* END: RADGRID ************************** -->
                                                                                                </div>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                <tr>
                                                                    <td class="backBarraBotoes" align="right" style="height: 30px">
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                        <!-- ****************** END: TERCEIRA ABA ************************** -->
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </rada:RadAjaxPanel>
                <rada:AjaxLoadingPanel ID="lpaCadastro" Width="75px" runat="server" HorizontalAlign="Center"
                    Transparency="30" Height="75px">
                    <asp:Image ID="Image1" Style="margin-top: 100px" runat="server" ImageUrl="%%PATH%%/RadControls/Ajax/Skins/Default/Loading.gif"
                        AlternateText="Aguarde ..."></asp:Image>
                </rada:AjaxLoadingPanel>
                <!-- ****************** END: PAINEL AJAX ************************** -->
            </td>
        </tr>
        <tr>
            <td>
                <!-- ********************* START: RODAPÉ ************************** -->
                <cc1:FWServerControl ID="FWServerControlRodape" runat="server" Controle="Rodape"
                    Arquivo="/Modulos/Framework/RodapeBrinks.ascx"></cc1:FWServerControl>
                <!-- ********************* END: RODAPÉ ************************** -->
            </td>
        </tr>
    </table>
    <asp:HtmlIframe id="IframeAgenda" runat="server" style="z-index: 101; left: -500px; visibility: visible;
        position: absolute; top: 0px" name="gToday:contrast:agenda.js" src="../../Agenda/calendario.htm"
        frameborder="0" width="132" scrolling="no" height="142"/>
    </form>
</body>
</html>
