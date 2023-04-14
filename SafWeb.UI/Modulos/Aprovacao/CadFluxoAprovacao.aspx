<%@ Page Language="C#" AutoEventWireup="true" Codebehind="CadFluxoAprovacao.aspx.cs"
    Inherits="SafWeb.UI.Modulos.Aprovacao.CadFluxoAprovacao" %>

<%@ Register TagPrefix="rad" Namespace="Telerik.WebControls" Assembly="RadGrid.Net2" %>
<%@ Register TagPrefix="cc1" Namespace="FrameWork.WebControl" Assembly="FrameWork.WebControl" %>
<%@ Register TagPrefix="rada" Namespace="Telerik.WebControls" Assembly="RadAjax.Net2" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>%%SITE%%</title>
    <link href="../../Estilos/FrameWork.css" type="text/css" rel="stylesheet">
    <link href="../../Estilos/Brinks.css" type="text/css" rel="stylesheet">
    <link href="../../Scripts/Mascara.js" type="text/javascript" rel="Stylesheet">

    <script language="javascript">
        
        function AbrirHelpCad()
        {
            window.open("HelpFluxoCadastro.aspx", "JANELA", "height = 400, width = 530");
        }
        
        function AbrirHelpList()
        {
            window.open("HelpFluxoListagem.aspx", "JANELA", "height = 400, width = 530");
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
                <!-- ********************* START: CONTEÚDO ****************** -->
                <td class="backbox" valign="top" height="250">
                    <!-- ****************** START: PAINEL AJAX ************************** -->
                    <rada:RadAjaxPanel ID="RadAjaxPanel1" runat="server" ClientEvents-OnRequestStart="OnRequestStart"
                        LoadingPanelID="lpaCadastro">
                        <asp:Panel ID="pnlErro" runat="server">
                        </asp:Panel>
                        <!-- ****************** START: PAINEL DE LISTAGEM ************************** -->
                        <asp:Panel runat="server" ID="pnlListagem">
                            <!-- ***************** START: BARRA DE TITUTO ******************* -->
                            <table class="MarginTopBarra" cellspacing="0" cellpadding="0" width="760" align="center"
                                border="0">
                                <tr>
                                    <td class="cadBarraTitulo" align="left" style="width: 700px; height: 25px;">
                                        <img src="../../Imagens/comum/bulletbarra.gif" align="middle" />
                                        <asp:Label ID="lblListagem" runat="server" Text="Listagem de Fluxo de Aprovação"></asp:Label>
                                    </td>
                                    <td class="cadBarraTitulo" align="right" style="width: 59px; height: 25px;">
                                        <asp:ImageButton ID="btnHelpList" runat="server" AlternateText="Ajuda" ImageUrl="%%PATH%%/imagens/icones/ico_help.jpg"
                                            ImageAlign="AbsMiddle" Visible="False" />
                                    </td>
                                </tr>
                                <!-- ***************** END: BARRA DE TITUTO ******************* -->
                                <tr>
                                    <td class="backboxconteudo" valign="top" style="height: 250px" colspan="2">
                                        <table class="MarginTopConteudo" cellspacing="0" cellpadding="0" width="740" align="center"
                                            border="0">
                                            <tr>
                                                <td colspan="3" class="cadmsg">
                                                    <asp:Label ID="lblMensagemListagem" runat="server" Text="Label" Visible="false" CssClass="cadlbl"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" colspan="3">
                                                    <!-- ********************* STAR: RADGRID ************************** -->
                                                    <rad:RadGrid ID="radTipoSolic" runat="server" AllowPaging="True" AllowSorting="True"
                                                        AutoGenerateColumns="False" CssClass="dtg" GridLines="None" Skin="None" Width="720px"
                                                        OnItemCommand="radTipoSolic_ItemCommand" OnItemDataBound="radTipoSolic_ItemDataBound"
                                                        OnNeedDataSource="radTipoSolic_NeedDataSource">
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
                                                                <rad:GridBoundColumn DataField="Codigo" Visible="false" SortExpression="CodTipoSolicitacao"
                                                                    HeaderText="CodTipoSolicitacao" UniqueName="CodTipoSolicitacao">
                                                                </rad:GridBoundColumn>
                                                                <rad:GridBoundColumn DataField="Descricao" SortExpression="Descricao" HeaderText="Tipo Solicitação"
                                                                    UniqueName="Descricao">
                                                                </rad:GridBoundColumn>
                                                                <rad:GridTemplateColumn Visible="False" UniqueName="TemplateColumn4">
                                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="imgEditar" runat="server" ImageUrl="~/imagens/icones/ico_editar.gif"
                                                                            CommandName="Editar" Visible="false"></asp:ImageButton>
                                                                    </ItemTemplate>
                                                                </rad:GridTemplateColumn>
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
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <!-- ****************** END: PAINEL DE LISTAGEM ************************** -->
                        <!-- ****************** START: PAINEL DE CADASTRO ************************** -->
                        <asp:Panel runat="server" ID="pnlCadastro" Visible="false">
                            <!-- ***************** START: BARRA DE TITUTO ******************* -->
                            <table class="MarginTopBarra" cellspacing="0" cellpadding="0" width="760" align="center"
                                border="0">
                                <tr>
                                    <td class="cadBarraTitulo" align="left" style="width: 700px; height: 25px;">
                                        <img src="../../Imagens/comum/bulletbarra.gif" align="Middle">
                                        <asp:Label ID="lblCadastro" runat="server" Text="Edição de Fluxo de Aprovação"></asp:Label>
                                    </td>
                                    <td class="cadBarraTitulo" align="right" style="width: 59px; height: 25px;">
                                        <asp:ImageButton ID="btnHelpCad" runat="server" AlternateText="Ajuda" ImageUrl="%%PATH%%/imagens/icones/ico_help.jpg"
                                            ImageAlign="AbsMiddle" Visible="False" />
                                    </td>
                                </tr>
                                <!-- ***************** END: BARRA DE TITUTO ******************* -->
                                <tr>
                                    <td class="backboxconteudo" valign="top" style="height: 250px" colspan="2">
                                        <!-- ****************** START: FORMULARIO *********************** -->
                                        <table class="MarginTopConteudo" cellspacing="0" cellpadding="0" width="740" align="center"
                                            border="0">
                                            <tr>
                                                <td colspan="5" class="cadmsg">
                                                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List" />
                                                    <asp:Label ID="lblMensagemCadastro" runat="server" Text="Label" Visible="false" CssClass="cadlbl"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="cadlbl" colspan="5">
                                                    <asp:Label ID="lblTipoSolicitacao" runat="server" Text="Tipo da Solicitação:"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="5">
                                                    <asp:DropDownList ID="ddlTipoSolicitacao" Enabled="false" runat="server" CssClass="cadddl"
                                                        Width="205px" AutoPostBack="True" OnSelectedIndexChanged="ddlTipoSolicitacao_SelectedIndexChanged">
                                                    </asp:DropDownList></td>
                                            </tr>
                                            <tr>
                                                <td class="cadlbl" width="50">
                                                    <asp:Label ID="lblOrdem" runat="server" Text="Ordem:"></asp:Label>
                                                </td>
                                                <td class="cadlbl" style="width: 234px">
                                                    <asp:Label ID="lblStatus" runat="server" Text="Status:"></asp:Label>
                                                </td>
                                                <td class="cadlbl" width="200" colspan="2">
                                                    <asp:Label ID="lblNivelAprovador" runat="server" Text="Nível Mínimo do Aprovador:"></asp:Label>
                                                </td>
                                                <td width="50">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="cadlbl" style="width: 110px">
                                                    <asp:TextBox ID="txtOrdem" runat="server" CssClass="cadtxt" Width="40px" MaxLength="5"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvOrdem" runat="server" ControlToValidate="txtOrdem"
                                                        CssClass="cadlbl" ValidationGroup="vgAdd">*</asp:RequiredFieldValidator></td>
                                                <td class="cadlbl" style="width: 234px">
                                                    <asp:DropDownList ID="ddlStatus" runat="server" CssClass="cadddl" Width="205px">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvStatus" runat="server" ControlToValidate="ddlStatus"
                                                        CssClass="cadlbl" InitialValue="-1" ValidationGroup="vgAdd">*</asp:RequiredFieldValidator></td>
                                                <td class="cadlbl" colspan="2">
                                                    <asp:DropDownList ID="ddlNivelAprovador" runat="server" CssClass="cadddl" Width="180px">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvNivelAprov" runat="server" ControlToValidate="ddlNivelAprovador"
                                                        CssClass="cadlbl" InitialValue="-1" ValidationGroup="vgAdd">*</asp:RequiredFieldValidator></td>
                                                <td>
                                                    <asp:Button ID="btnAdicionar" runat="server" Text="Adicionar" CssClass="cadbutton100"
                                                        OnClick="btnAdicionar_Click" ValidationGroup="vgAdd" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:CheckBox ID="chkContigencia" runat="server" CssClass="cadchk" Text="Requer Aprov. de Contigência" />
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="chkSegurancao" runat="server" CssClass="cadchk" Text="Requer Aprov. de Área de Seg." />
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="chkTI" runat="server" CssClass="cadchk" 
                                                        Text="Requer Aprov. de TI" />
                                                </td>
                                                <td colspan="2">
                                                    <asp:CheckBox ID="chkAprovadorPermissaoCracha" runat="server" CssClass="cadchk" 
                                                        Text="Requer Aprov. de Perm. Crachá" />
                                                </td>
                                            </tr>
                                        </table>
                                        <table class="MarginTopConteudo" cellspacing="0" cellpadding="0" width="740" align="center"
                                            border="0">
                                            <tr>
                                                <td align="center" colspan="3">
                                                    <br />
                                                    <!-- ********************* STAR: RADGRID ************************** -->
                                                    <rad:RadGrid ID="radFluxoAprov" runat="server" AllowPaging="True" AllowSorting="True"
                                                        AutoGenerateColumns="False" CssClass="dtg" SortingSettings-SortToolTip="Clique para filtrar" GridLines="None" Skin="None" Width="720px"
                                                        OnItemCommand="radFluxoAprov_ItemCommand" OnItemDataBound="radFluxoAprov_ItemDataBound"
                                                        OnNeedDataSource="radFluxoAprov_NeedDataSource">
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

                                                                <rad:GridBoundColumn DataField="CodFluxoAprovacao" SortExpression="CodFluxoAprovacao"
                                                                    HeaderText="CodFluxoAprovacao" Visible="False" UniqueName="CodFluxoAprovacao">
                                                                </rad:GridBoundColumn>


                                                                <rad:GridBoundColumn DataField="CodTipoSolicitacao" Visible="false" SortExpression="CodTipoSolicitacao"
                                                                    HeaderText="CodTipoSolicitacao" UniqueName="CodTipoSolicitacao">
                                                                </rad:GridBoundColumn>

                                                                <rad:GridBoundColumn DataField="DesTipoSolicitacao" HeaderText="Tipo Solicitação"
                                                                    UniqueName="DesTipoSolicitacao">
                                                                    <ItemStyle Width="150px" HorizontalAlign="Center" />
                                                                </rad:GridBoundColumn>

                                                                <rad:GridBoundColumn DataField="CodOrdemAprovacao" SortExpression="CodOrdemAprovacao"
                                                                    HeaderText="Ordem Aprovação" UniqueName="CodOrdemAprovacao">
                                                                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                                                                </rad:GridBoundColumn>

                                                                <rad:GridBoundColumn DataField="CodStatusSolicitacao" Visible="False" HeaderText="CodStatusSolicitacao"
                                                                    UniqueName="CodStatusSolicitacao">
                                                                </rad:GridBoundColumn>

                                                                <rad:GridBoundColumn DataField="DesStatusAprovacao" SortExpression="DesStatusAprovacao"
                                                                    HeaderText="Status Aprovação" UniqueName="DesStatusAprovacao">
                                                                    <ItemStyle Width="150px" HorizontalAlign="Center" />
                                                                </rad:GridBoundColumn>

                                                                <rad:GridBoundColumn DataField="FlgAprovaAreaSeg" Visible="False" HeaderText="FlgAprovaAreaSeg"
                                                                    UniqueName="FlgAprovaAreaSeg">
                                                                </rad:GridBoundColumn>

                                                                <rad:GridTemplateColumn DataField="FlgAprovaAreaSeg" SortExpression="FlgAprovaAreaSeg"
                                                                    HeaderText="Aprova Área Seg." UniqueName="TFlgAprovaAreaSeg">
                                                                    <ItemStyle Width="110px" HorizontalAlign="Center" />
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkAprovaAreaSeg" runat="server" Enabled="false" />
                                                                    </ItemTemplate>
                                                                </rad:GridTemplateColumn>

                                                                <rad:GridBoundColumn DataField="FlgAprovaContingencia" Visible="False" HeaderText="FlgAprovaContingencia"
                                                                    UniqueName="FlgAprovaContingencia">
                                                                </rad:GridBoundColumn>

                                                                <rad:GridTemplateColumn DataField="FlgAprovaContingencia" SortExpression="FlgAprovaContingencia"
                                                                    HeaderText="Aprova Contingência" UniqueName="TFlgAprovaContingencia">
                                                                    <ItemStyle Width="120px" HorizontalAlign="Center" />
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkAprovaContingencia" runat="server" Enabled="false" />
                                                                    </ItemTemplate>
                                                                </rad:GridTemplateColumn>

                                                                <rad:GridBoundColumn DataField="FlgAprovaAreaTI" Visible="False" HeaderText="FlgAprovaAreaTI"
                                                                    UniqueName="FlgAprovaAreaTI">
                                                                </rad:GridBoundColumn>

                                                                <rad:GridTemplateColumn DataField="FlgAprovaAreaTI" SortExpression="FlgAprovaAreaTI"
                                                                    HeaderText="Aprova TI" UniqueName="TFlgAprovaAreaTI">
                                                                    <ItemStyle Width="120px" HorizontalAlign="Center" />
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkTI" runat="server" Enabled="false" />
                                                                    </ItemTemplate>
                                                                </rad:GridTemplateColumn>

                                                                <rad:GridBoundColumn DataField="FlgAprovaCracha" Visible="False" HeaderText="FlgAprovaCracha"
                                                                    UniqueName="FlgAprovaCracha">
                                                                </rad:GridBoundColumn>

                                                                <rad:GridTemplateColumn DataField="FlgAprovaCracha" SortExpression="FlgAprovaCracha"
                                                                    HeaderText="Aprova Perm. Crachá" UniqueName="TFlgAprovaCracha">
                                                                    <ItemStyle Width="120px" HorizontalAlign="Center" />
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkAprovaCracha" runat="server" Enabled="false" />
                                                                    </ItemTemplate>
                                                                </rad:GridTemplateColumn>


                                                                <rad:GridBoundColumn DataField="CodNivelAprovacao" SortExpression="CodNivelAprovacao"
                                                                    HeaderText="CodNivelAprovacao" Visible="False" UniqueName="CodNivelAprovacao">
                                                                </rad:GridBoundColumn>

                                                                <rad:GridBoundColumn DataField="DesNivelAprovacao" SortExpression="DesNivelAprovacao"
                                                                    HeaderText="Nível Aprovação" UniqueName="DesNivelAprovacao">
                                                                    <ItemStyle Width="130px" HorizontalAlign="Center" />
                                                                </rad:GridBoundColumn>

                                                                <rad:GridTemplateColumn Visible="False" UniqueName="TemplateColumn1">
                                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="imgEditar" runat="server" ImageUrl="~/imagens/icones/ico_editar.gif"
                                                                            CommandName="Editar" Visible="false"></asp:ImageButton>
                                                                    </ItemTemplate>
                                                                </rad:GridTemplateColumn>

                                                                <rad:GridBoundColumn DataField="FlgSituacao" SortExpression="FlgSituacao" HeaderText="FlgSituacao"
                                                                    Visible="False" UniqueName="FlgSituacao">
                                                                </rad:GridBoundColumn>

                                                                <rad:GridButtonColumn CommandName="Ativar" UniqueName="Ativar" ButtonType="ImageButton"
                                                                    ImageUrl="%%PATH%%/imagens/icones/ico_Ativo.gif" Text="Ativar">
                                                                    <ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
                                                                </rad:GridButtonColumn>


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
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            <!-- ********************* START: BARRA DE BOTÕES INFERIOR ************************** -->
                            <table align="center" border="0" cellpadding="0" cellspacing="0" width="760">
                                <tr>
                                    <td align="right" class="backBarraBotoes">
                                        <asp:Button ID="btnGravarSair" runat="server" CssClass="cadbutton100" Text="Gravar e Sair"
                                            OnClick="btnGravarSair_Click" CausesValidation="False" />
                                        &nbsp;
                                        <asp:Button ID="btnGravar" runat="server" CssClass="cadbutton100" Text="Gravar" OnClick="btnGravar_Click"
                                            CausesValidation="False" />
                                        &nbsp;
                                        <asp:Button ID="btnVoltar" runat="server" CausesValidation="False" CssClass="cadbutton100"
                                            Text="Voltar" Width="50px" OnClick="btnVoltar_Click" />
                                    </td>
                                </tr>
                            </table>
                            <!-- ********************* END: BARRA DE BOTÕES INFERIOR ************************** -->
                        </asp:Panel>
                        <!-- ****************** END: PAINEL DE CADASTRO ************************** -->
                        <br />
                    </rada:RadAjaxPanel>
                    <rada:AjaxLoadingPanel ID="lpaCadastro" Width="75px" runat="server" HorizontalAlign="Center"
                        Transparency="30" Height="75px">
                        <asp:Image ID="Image1" Style="margin-top: 100px" runat="server" ImageUrl="%%PATH%%/RadControls/Ajax/Skins/Default/Loading.gif"
                            AlternateText="Aguarde ..."></asp:Image>
                    </rada:AjaxLoadingPanel>
                    <!-- ****************** END: PAINEL AJAX ************************** -->
                </td>
                <!-- ********************* END: CONTEÚDO ****************** -->
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
    </form>
</body>
</html>
