<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CadLista.aspx.cs" Inherits="SafWeb.UI.Modulos.Solicitacao.CadLista" %>

<%@ Register TagPrefix="radw" Namespace="Telerik.WebControls" Assembly="RadWindow.Net2" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.WebControls" Assembly="RadGrid.Net2" %>
<%@ Register TagPrefix="rada" Namespace="Telerik.WebControls" Assembly="RadAjax.Net2" %>
<%@ Register TagPrefix="cc1" Namespace="FrameWork.WebControl" Assembly="FrameWork.WebControl" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>%%SITE%%</title>
    <link href="../../Estilos/FrameWork.css" type="text/css" rel="stylesheet">
    <link href="../../Estilos/Brinks.css" type="text/css" rel="stylesheet">
    <script type="text/javascript">

        function PreencherHidden(codigo) {
            document.getElementById("HiddenField").value = codigo
			
			AjaxNS.AR('VerConjuge','', 'RadAjaxPanel2');
		}	
            
        function AbrirHelpCad() {
            window.open("HelpListaCadastro.aspx", "JANELA", "height = 600, width = 530");
        }
        
        function AbrirHelpList() {
            window.open("HelpListaListagem.aspx", "JANELA", "height = 400, width = 530");
        }
        
    </script>
</head>
<body>
    <form id="form" runat="server">
        <table cellspacing="0" cellpadding="0" width="778" align="center" border="0">
            <tr>
                <td style="width: 778px">
                    <!-- ********************* START: CABEÇALHO ****************** -->
                    <cc1:FWServerControl ID="FWServerControlCabecalho" runat="server" Controle="Pagebanner"
                        Arquivo="/Modulos/Framework/CabecalhoBrinks.ascx"></cc1:FWServerControl>
                    <!-- ********************* END: CABEÇALHO ****************** -->
                </td>
            </tr>
            <tr>
                <!-- ********************* START: CONTEÚDO ****************** -->
                <td class="backbox" valign="top" height="250" style="width: 778px">
                    <!-- ****************** START: PAINEL AJAX ************************** -->
                    <rada:RadAjaxPanel ID="RadAjaxPanel1" runat="server" ClientEvents-OnRequestStart="OnRequestStart"
                        LoadingPanelID="lpaCadastro">
                        <asp:Panel ID="pnlErro" runat="server">
                        </asp:Panel>
                        <!-- ****************** START: PAINEL DE LISTAGEM ************************** -->
                        <asp:Panel ID="pnlListagem" runat="server">
                            <!-- ***************** START: BARRA DE TITUTO ******************* -->
                            <table class="MarginTopBarra" cellspacing="0" cellpadding="0" width="760" align="center"
                                border="0">
                                <tbody>
                                    <tr>
                                        <td class="cadBarraTitulo" align="left" height="25" style="width: 750px">
                                            <img src="../../Imagens/comum/bulletbarra.gif" align="middle" />
                                            <asp:Label ID="lblListagem" runat="server" Text="Listagem de Listas"></asp:Label>
                                        </td>
                                        <td class="cadBarraTitulo" align="right" height="25" style="width: 9px">
                                            <asp:ImageButton ID="btnHelpList" runat="server" ImageAlign="AbsMiddle" ImageUrl="%%PATH%%/imagens/icones/ico_help.jpg"
                                                AlternateText="Ajuda" Visible="False"></asp:ImageButton>
                                        </td>
                                    </tr>
                                    <!-- ***************** END: BARRA DE TITUTO ******************* -->
                                    <tr>
                                        <td class="backboxconteudo" valign="top" colspan="2" height="250">
                                            <!-- ****************** START: FILTROS ************************** -->
                                            <table class="MarginTopConteudo" cellspacing="0" cellpadding="0" width="740" align="center"
                                                border="0">
                                                <tbody>
                                                    <tr>
                                                        <td class="cadlbl" width="240">
                                                        <asp:Label ID="lblRegional" runat="server" Text="Regional:"></asp:Label>
                                                    </td>
                                                        <td width="5">
                                                        </td>
                                                        <td class="cadlbl" width="240">
                                                        <asp:Label ID="lblFilial" runat="server" Text="Filial:"></asp:Label>
                                                    </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:DropDownList ID="ddlRegional" runat="server" OnSelectedIndexChanged="ddlRegional_SelectedIndexChanged"
                                                                AutoPostBack="True" Width="130px" CssClass="cadddl">
                                                        </asp:DropDownList>
                                                    </td>
                                                        <td>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlFilial" runat="server" Width="130px" CssClass="cadddl" Enabled="False">
                                                        </asp:DropDownList>
                                                    </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 19px" class="cadlbl">
                                                        <asp:Label ID="lblCodigoLista" runat="server" Text="Código da Lista:"></asp:Label>
                                                    </td>
                                                        <td style="height: 19px">
                                                        </td>
                                                        <td style="height: 19px" class="cadlbl">
                                                            <asp:Label ID="lblNomeLista" runat="server" Text="Nome da Lista:"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <cc1:FWMascara ID="txtCodLista" runat="server" Width="99px" CssClass="cadtxt" MaxLength="10"
                                                            Mascara="NUMERO"></cc1:FWMascara>
                                                    </td>
                                                        <td>
                                                        </td>
                                                        <td>
                                                        <asp:TextBox ID="txtNomeLista" runat="server" Width="370px" CssClass="cadtxt" MaxLength="50"></asp:TextBox>
                                                    </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" colspan="3" style="height: 57px">
                                                            <br />
                                                            <asp:Button ID="btnBuscar" OnClick="btnBuscar_Click" runat="server" Text="Buscar"
                                                                CssClass="cadbuttonfiltro"></asp:Button>
                                                            <br />
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                            <!-- ********************** END: FILTROS ************************** -->
                                            <table class="MarginTopConteudo" cellspacing="0" cellpadding="0" width="740" align="center"
                                                border="0">
                                                <tbody>
                                                    <tr>
                                                        <td align="center" colspan="3">
                                                            <!-- ********************* STAR: RADGRID ************************** -->
                                                            <rad:RadGrid ID="radGridLista" runat="server" Width="100%" CssClass="dtg" OnNeedDataSource="radGridLista_NeedDataSource"
                                                                Skin="None" OnItemDataBound="radGridLista_ItemDataBound" OnItemCommand="radGridLista_ItemCommand"
                                                                GridLines="None" AutoGenerateColumns="False" AllowSorting="True" AllowPaging="True">
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
                                                                            <table cellpadding="0" cellspacing="0" height="100" width="100%">
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
                                                                        <rad:GridBoundColumn DataField="IdLista" HeaderText="C&#243;digo" SortExpression="IdLista"
                                                                            UniqueName="IdLista">
                                                                            <ItemStyle Width="10%"></ItemStyle>
                                                                        </rad:GridBoundColumn>
                                                                        <rad:GridBoundColumn DataField="DescricaoLista" HeaderText="Nome Lista" SortExpression="DescricaoLista"
                                                                            UniqueName="DescricaoLista">
                                                                            <ItemStyle Width="40%"></ItemStyle>
                                                                        </rad:GridBoundColumn>
                                                                        <rad:GridBoundColumn DataField="DescricaoFilial" HeaderText="Filial" SortExpression="DescricaoFilial"
                                                                            UniqueName="DescricaoFilial">
                                                                            <ItemStyle Width="30%" HorizontalAlign="Center"></ItemStyle>
                                                                        </rad:GridBoundColumn>
                                                                        <rad:GridBoundColumn DataField="Situacao" SortExpression="Situacao" UniqueName="Situacao"
                                                                            Visible="False" />
                                                                        <rad:GridTemplateColumn UniqueName="TemplateColumn" Visible="False">
                                                                            <ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <asp:ImageButton ID="btnEditar" runat="server" AlternateText="Editar" CausesValidation="False"
                                                                                    CommandArgument='<%# DataBinder.Eval(Container.DataItem, "IdLista") %>' CommandName="Editar"
                                                                                    ImageUrl="%%PATH%%/imagens/icones/ico_editar.gif" ToolTip="Editar"></asp:ImageButton>
                                                                            </ItemTemplate>
                                                                        </rad:GridTemplateColumn>
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
                                                </tbody>
                                            </table>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            <!-- ********************* START: BARRA DE BOTÕES INFERIOR ************************** -->
                            <table cellspacing="0" cellpadding="0" width="760" align="center" border="0">
                                <tbody>
                                    <tr>
                                        <td style="height: 30px" class="backBarraBotoes" align="right">
                                            <asp:Button ID="btnIncluir" OnClick="btnIncluir_Click" runat="server" Text="Incluir"
                                                CssClass="cadbutton100"></asp:Button>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            <!-- ********************* END: BARRA DE BOTÕES INFERIOR ************************** -->
                        </asp:Panel>
                        <!-- ****************** END: PAINEL DE LISTAGEM ************************** -->
                        <!-- ****************** START: PAINEL DE CADASTRO ************************** -->
                        <asp:Panel ID="pnlCadastro" runat="server">
                            <!-- ***************** START: BARRA DE TITUTO ******************* -->
                            <table class="MarginTopBarra" cellspacing="0" cellpadding="0" width="760" align="center"
                                border="0">
                                <tbody>
                                    <tr>
                                        <td style="width: 750px" class="cadBarraTitulo" align="left" height="25">
                                            <img src="../../Imagens/comum/bulletbarra.gif" align="middle" />
                                            <asp:Label ID="lblCadastro" runat="server" Text="Cadastro de Lista"></asp:Label>
                                        </td>
                                        <td style="width: 9px" class="cadBarraTitulo" align="right" height="25">
                                            <asp:ImageButton ID="btnHelpCad" runat="server" ImageAlign="AbsMiddle" ImageUrl="%%PATH%%/imagens/icones/ico_help.jpg"
                                                AlternateText="Ajuda" Visible="False"></asp:ImageButton>
                                        </td>
                                    </tr>
                                    <!-- ***************** END: BARRA DE TITUTO ******************* -->
                                    <tr>
                                        <td style="height: 250px" class="backboxconteudo" valign="top" colspan="2">
                                            <!-- ****************** START: FORMULARIO *********************** -->
                                            <table class="MarginTopConteudo" cellspacing="0" cellpadding="0" width="740" align="center"
                                                border="0">
                                                <tbody>
                                                    <tr>
                                                        <td class="cadmsg" colspan="5">
                                                            &nbsp;<asp:Label ID="lblMensagem" runat="server" Text="Label" CssClass="cadlbl" Visible="False"></asp:Label>
                                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List">
                                                        </asp:ValidationSummary>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="cadlbl">
                                                            <asp:Label ID="lblDadosVisitado" runat="server" Text="Dados Lista"></asp:Label>
                                                            <table style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;
                                                                border-bottom: black 1px solid" width="740">
                                                                <tbody>
                                                                    <tr>
                                                                        <td class="cadlbl" width="20">
                                                                            <asp:Label ID="lblCodListaCad" runat="server" Text="Código:"></asp:Label>
                                                                        </td>
                                                                        <td width="5">
                                                                        </td>
                                                                        <td style="width: 358px" class="cadlbl">
                                                                            <asp:Label ID="lblNomeListaCad" runat="server" Text="Nome da Lista:"></asp:Label>
                                                                        </td>
                                                                        <td width="5">
                                                                        </td>
                                                                        <td class="cadlbl" width="200">
                                                                            <asp:Label ID="lblRegionalCad" runat="server" Text="Regional:"></asp:Label>
                                                                        </td>
                                                                        <td width="5">
                                                                        </td>
                                                                        <td class="cadlbl" width="200">
                                                                            <asp:Label ID="lblFilialCad" runat="server" Text="Filial:"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:TextBox ID="txtCodListaCad" runat="server" Width="85px" CssClass="cadtxt" Enabled="false"
                                                                                MaxLength="50"></asp:TextBox>
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                        <td style="width: 358px">
                                                                            <asp:TextBox ID="txtNomeListaCad" runat="server" Width="293px" CssClass="cadtxt"
                                                                                MaxLength="50"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator ID="rfvNomeLista" runat="server" SetFocusOnError="True"
                                                                            ValidationGroup="Gravar" ErrorMessage="Campo Obrigatório: Nome Lista." ControlToValidate="txtNomeListaCad">*</asp:RequiredFieldValidator>
                                                                    </td>
                                                                        <td>
                                                                        </td>
                                                                        <td>
                                                                            <asp:DropDownList ID="ddlRegionalCad" runat="server" OnSelectedIndexChanged="ddlRegionalCad_SelectedIndexChanged"
                                                                                AutoPostBack="True" CssClass="cadddl">
                                                                            </asp:DropDownList>
                                                                            <asp:RequiredFieldValidator ID="rfvRegional" runat="server" SetFocusOnError="True"
                                                                                ValidationGroup="Gravar" ErrorMessage="Campo Obrigatório: Regional." ControlToValidate="ddlRegionalCad"
                                                                            InitialValue="0">*</asp:RequiredFieldValidator>
                                                                    </td>
                                                                        <td>
                                                                        &nbsp;
                                                                    </td>
                                                                        <td>
                                                                            <asp:DropDownList ID="ddlFilialCad" runat="server" CssClass="cadddl" Enabled="False">
                                                                            </asp:DropDownList>
                                                                            <asp:RequiredFieldValidator ID="rfvFilial" runat="server" SetFocusOnError="True"
                                                                                ValidationGroup="Gravar" ErrorMessage="Campo Obrigatório: Filial." ControlToValidate="ddlFilialCad"
                                                                            InitialValue="-1">*</asp:RequiredFieldValidator>
                                                                    </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="cadlbl">
                                                            <br />
                                                            <asp:Label ID="lblDadosVisitante" runat="server" Text="Dados Visitante"></asp:Label>
                                                            <rada:RadAjaxPanel ID="RadAjaxPanel2" runat="server" LoadingPanelID="lpaCadastro"
                                                                ClientEvents-OnRequestStart="OnRequestStart">
                                                                <asp:Panel ID="pnlLista" runat="server">
                                                                </asp:Panel>
                                                                <table style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;
                                                                    border-bottom: black 1px solid" width="740">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td class="cadlbl" width="177">
                                                                            <asp:Label ID="lblTipoVisitante" runat="server" Text="Tipo Visitante:"></asp:Label>
                                                                        </td>
                                                                            <td width="5">
                                                                            </td>
                                                                            <td width="150">
                                                                            </td>
                                                                            <td width="5">
                                                                            </td>
                                                                            <td class="cadlbl" width="190">
                                                                            </td>
                                                                            <td width="150">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="height: 22px">
                                                                                <asp:DropDownList ID="ddlTipoVisitante" runat="server" OnSelectedIndexChanged="ddlTipoVisitante_SelectedIndexChanged"
                                                                                    AutoPostBack="True" CssClass="cadddl">
                                                                                </asp:DropDownList>
                                                                                <asp:RequiredFieldValidator ID="rfvTipoVisitante" runat="server" SetFocusOnError="True"
                                                                                    ValidationGroup="Buscar" ErrorMessage="Campo Obrigatório: Tipo Visitantel." ControlToValidate="ddlTipoVisitante"
                                                                                InitialValue="-1">*</asp:RequiredFieldValidator>
                                                                        </td>
                                                                            <td style="height: 22px">
                                                                            </td>
                                                                            <td style="height: 22px">
                                                                            </td>
                                                                            <td style="height: 22px">
                                                                            </td>
                                                                            <td style="height: 22px">
                                                                            </td>
                                                                            <td style="height: 22px">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="cadlbl">
                                                                            <asp:Label ID="lblNomeVisitante" runat="server" Text="Nome Visitante:"></asp:Label>
                                                                        </td>
                                                                            <td>
                                                                            </td>
                                                                            <td>
                                                                            </td>
                                                                            <td>
                                                                            </td>
                                                                            <td class="cadlbl">
                                                                            <asp:Label ID="lblTipoDocumento" runat="server" Text="Tipo Documento:"></asp:Label>
                                                                        </td>
                                                                            <td class="cadlbl">
                                                                                <asp:Label ID="lblDocumento" runat="server" Text="Nº Documento:"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="3">
                                                                                <asp:TextBox ID="txtNomeVisitante" onfocus="ObterColaborador()" runat="server" Width="362px"
                                                                                    CssClass="cadtxt" Enabled="False" MaxLength="100"></asp:TextBox>
                                                                                <asp:ImageButton ID="btnListarVisitante" OnClick="btnListarVisitante_Click" runat="server"
                                                                                    ImageUrl="~/Imagens/icones/ico_Visualizar.gif" AlternateText="Buscar" ValidationGroup="Buscar">
                                                                                </asp:ImageButton><asp:RequiredFieldValidator ID="rfvNomeVisitante" runat="server"
                                                                                    SetFocusOnError="True" ValidationGroup="Adicionar" ErrorMessage="Campo Obrigatório: Nome Visitante."
                                                                                    ControlToValidate="txtNomeVisitante">*</asp:RequiredFieldValidator>
                                                                                <asp:HiddenField ID="HiddenField" runat="server"></asp:HiddenField>
                                                                            </td>
                                                                            <td>
                                                                            </td>
                                                                            <td>
                                                                                <asp:DropDownList ID="ddlTipoDocumento" runat="server" OnSelectedIndexChanged="ddlTipoDocumento_SelectedIndexChanged"
                                                                                    AutoPostBack="True" Width="115px" CssClass="cadddl" Enabled="False">
                                                                                </asp:DropDownList>
                                                                                <asp:RequiredFieldValidator ID="rfvTipoDocumento" runat="server" SetFocusOnError="True"
                                                                                    ValidationGroup="Adicionar" ErrorMessage="Campo Obrigatório: Tipo Documento."
                                                                                ControlToValidate="ddlTipoDocumento" InitialValue="0">*</asp:RequiredFieldValidator>
                                                                        </td>
                                                                            <td>
                                                                                <cc1:FWMascara ID="txtDocumento" runat="server" AutoPostBack="True" Width="99px"
                                                                                    CssClass="cadtxt" Enabled="False" Mascara="NENHUMA"></cc1:FWMascara>
                                                                                <asp:RequiredFieldValidator ID="rfvNumDocumento" runat="server" SetFocusOnError="True"
                                                                                ValidationGroup="Adicionar" ErrorMessage="Campo Obrigatório: Nº Documento." ControlToValidate="txtDocumento">*</asp:RequiredFieldValidator>
                                                                        </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="cadlbl">
                                                                                <asp:Label ID="lblEmpresaCad" runat="server" Text="Empresa:"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                            </td>
                                                                            <td class="cadlbl">
                                                                                <asp:Label ID="lblOutraEmpresa" runat="server" Text="Outra Empresa:" Visible="false"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                            </td>
                                                                            <td>
                                                                            </td>
                                                                            <td>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:DropDownList ID="ddlEmpresaCad" runat="server" OnSelectedIndexChanged="ddlEmpresaCad_SelectedIndexChanged"
                                                                                    AutoPostBack="True" Width="235px" CssClass="cadddl" Enabled="False" ValidationGroup="Adicionar">
                                                                            </asp:DropDownList>
                                                                            <asp:RequiredFieldValidator ID="rfvEmpresa" runat="server" SetFocusOnError="True"
                                                                                    ValidationGroup="Adicionar" ErrorMessage="Campo Obrigatório: Empresa." ControlToValidate="ddlEmpresaCad"
                                                                                InitialValue="-1">*</asp:RequiredFieldValidator>
                                                                        </td>
                                                                            <td style="height: 25px">
                                                                            </td>
                                                                            <td colspan="3">
                                                                                <asp:TextBox ID="txtOutraEmpresa" runat="server" Width="250px" CssClass="cadtxt"
                                                                                    MaxLength="100" Visible="false"></asp:TextBox>
                                                                            </td>
                                                                            <td>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="7">
                                                                                <table cellspacing="0" cellpadding="0" width="730" align="center" border="0">
                                                                                    <tbody>
                                                                                        <tr>
                                                                                            <td class="backBarraBotoes" align="right">
                                                                                                <asp:Button ID="btnLimpar" OnClick="btnLimpar_Click" runat="server" Text="Limpar"
                                                                                                    CssClass="cadbutton100"></asp:Button>
                                                                                                <asp:Button ID="btnAdicionarCad" OnClick="btnAdicionarCad_Click" runat="server" Text="Adicionar"
                                                                                                    CssClass="cadbutton100" ValidationGroup="Adicionar"></asp:Button>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </tbody>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td valign="middle" align="center" colspan="7" visible="false">
                                                                                <br />
                                                                                <rad:RadGrid ID="radGridVisitantes" runat="server" Width="100%" CssClass="dtg" OnNeedDataSource="radGridVisitantes_NeedDataSource"
                                                                                    Skin="None" OnItemDataBound="radGridVisitantes_ItemDataBound" OnItemCommand="radGridVisitantes_ItemCommand"
                                                                                    GridLines="None" AutoGenerateColumns="False" AllowSorting="True" AllowPaging="True">
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
                                                                                                <table cellpadding="0" cellspacing="0" height="100" width="100%">
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
                                                                                                            <cc1:FWMascara ID="FWMascara1" runat="server" AutoPostBack="False" CssClass="pagtxtbox"
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
                                                                                            <rad:GridBoundColumn DataField="NomeColaborador" HeaderText="Visitante" SortExpression="NomeColaborador"
                                                                                                UniqueName="NomeColaborador">
                                                                                                <ItemStyle Width="40%"></ItemStyle>
                                                                                            </rad:GridBoundColumn>
                                                                                            <rad:GridBoundColumn DataField="DescricaoEmpresa" HeaderText="Empresa" SortExpression="DescricaoEmpresa"
                                                                                                UniqueName="DescricaoEmpresa">
                                                                                                <ItemStyle Width="20%"></ItemStyle>
                                                                                            </rad:GridBoundColumn>
                                                                                            <rad:GridBoundColumn DataField="TipoDocumento" HeaderText="Tipo Documento" SortExpression="TipoDocumento"
                                                                                                UniqueName="TipoDocumento">
                                                                                                <ItemStyle Width="20%" HorizontalAlign="Center"></ItemStyle>
                                                                                            </rad:GridBoundColumn>
                                                                                            <rad:GridBoundColumn DataField="NumeroDocumento" HeaderText="N&#186; Documento" SortExpression="NumeroDocumento"
                                                                                                UniqueName="NumeroDocumento">
                                                                                                <ItemStyle Width="20%" HorizontalAlign="Center"></ItemStyle>
                                                                                            </rad:GridBoundColumn>
                                                                                        <rad:GridButtonColumn CommandName="Ativar" UniqueName="Ativar" ButtonType="ImageButton"
                                                                                            ImageUrl="%%PATH%%/imagens/icones/ico_Ativo.gif" Text="Ativar">
                                                                                            <ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
                                                                                        </rad:GridButtonColumn>
                                                                                            <rad:GridButtonColumn CommandName="Excluir" UniqueName="Ativar" ButtonType="ImageButton"
                                                                                                ImageUrl="%%PATH%%/imagens/icones/ico_deletar.gif" Text="Excluir">
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
                                                                                <br />
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </rada:RadAjaxPanel>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                            <!-- ********************* START: BARRA DE BOTÕES INFERIOR ************************** -->
                                            <table cellspacing="0" cellpadding="0" width="760" align="center" border="0">
                                                <tbody>
                                                    <tr>
                                                        <td style="height: 31px" class="backBarraBotoes" align="right">
                                                            <asp:Button ID="btnGravarSair" OnClick="btnGravarSair_Click" runat="server" Text="Gravar e Sair"
                                                                CssClass="cadbutton100" ValidationGroup="Gravar"></asp:Button>
                                                            &nbsp; &nbsp;<asp:Button ID="btnGravar" OnClick="btnGravar_Click" runat="server"
                                                                Text="Gravar" CssClass="cadbutton100" ValidationGroup="Gravar"></asp:Button>
                                                            &nbsp;
                                                            <asp:Button ID="btnVoltar" OnClick="btnVoltar_Click" runat="server" Text="Voltar"
                                                                Width="50px" CssClass="cadbutton100" CausesValidation="False"></asp:Button>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                            <!-- ********************* END: BARRA DE BOTÕES INFERIOR ************************** -->
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
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
                <td style="width: 778px">
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
