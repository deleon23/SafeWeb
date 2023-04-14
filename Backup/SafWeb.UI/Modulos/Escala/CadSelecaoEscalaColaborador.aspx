<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CadSelecaoEscalaColaborador.aspx.cs" Inherits="SafWeb.UI.Modulos.Escala.CadSelecaoEscalaColaborador" %>

<%@ Register TagPrefix="rad" Namespace="Telerik.WebControls" Assembly="RadGrid.Net2" %>
<%@ Register TagPrefix="rada" Namespace="Telerik.WebControls" Assembly="RadAjax.Net2" %>
<%@ Register TagPrefix="cc1" Namespace="FrameWork.WebControl" Assembly="FrameWork.WebControl" %>
<html>
<head>
    <title>%%SITE%%</title>
    <link href="../../Estilos/FrameWork.css" type="text/css" rel="stylesheet" />
    <link href="../../Estilos/Brinks.css" rel="stylesheet" type="text/css" />
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

                    vrRetorno = true;
                }
            }
            else {
                vrRetorno = true;
            }

            return vrRetorno;
        }

        function PreencherHiddenColaboradores(colaboradores) {
            document.getElementById("txtHiddenColaboradores").value = colaboradores;

            AjaxNS.AR('Colaboradores', '', 'RadAjaxPanelCadastro');
        }
    </script>
</head>
<body>
    <form id="form" method="post" runat="server">
    <table cellspacing="0" cellpadding="0" width="778" align="center" border="0">
        <tr>
            <td style="width: 1010px">
                <!-- ********************* START: CABEÇALHO ****************** -->
                <cc1:FWServerControl ID="FWServerControlCabecalho" runat="server" Controle="Pagebanner"
                    Arquivo="/Modulos/Framework/CabecalhoBrinks.ascx"></cc1:FWServerControl>
                <!-- ********************* END: CABEÇALHO ****************** -->
            </td>
        </tr>
        <tr>
            <!-- ********************* START: CONTEÚDO ****************** -->
            <td class="backbox" valign="top" height="250" style="width: 1010px">
                <rada:RadAjaxPanel ID="RadAjaxPanel1" runat="server" ClientEvents-OnRequestStart="OnRequestStart"
                    LoadingPanelID="lpaCadastro">
                    <!-- ********************* PAINEL DE ERRO ****************** -->
                    <asp:Panel ID="pnlErro" runat="server">
                    </asp:Panel>
                    <!-- ****************** START: PAINEL LISTAGEM *********************** -->
                    <asp:Panel ID="pnlListagem" runat="server">
                        <!-- ***************** START: BARRA DE TITUTO ******************* -->
                        <table class="MarginTopBarra" cellspacing="0" cellpadding="0" width="760" align="center"
                            border="0">
                            <tbody>
                                <tr>
                                    <td style="width: 750px; height: 25px" class="cadBarraTitulo" align="left">
                                        <img src="../../Imagens/comum/bulletbarra.gif" align="absMiddle" />
                                        <asp:Label ID="Label1" runat="server" Text="Listagem de Escalas Periódicas de Colaboradores"></asp:Label>
                                    </td>
                                    <td style="width: 9px" class="cadBarraTitulo" align="right" height="25">
                                        <asp:ImageButton ID="ImageButton1" runat="server" Visible="False" ImageAlign="AbsMiddle"
                                            ImageUrl="%%PATH%%/imagens/icones/ico_help.jpg" AlternateText="Ajuda"></asp:ImageButton>
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
                                                    <td>
                                                        <!-- ****************** START: FILTROS *********************** -->
                                                        <table cellspacing="0" cellpadding="0" width="740" align="center" border="0">
                                                            <tbody>
                                                                <tr>
                                                                    <td class="cadlbl" width="180">
                                                                        <asp:Label ID="lblNumeroEscalaList" runat="server" Text="Número da Escala:"></asp:Label>
                                                                    </td>
                                                                    <td class="cadlbl" width="180">
                                                                        <asp:Label ID="lblEscalaDepartamentalList" runat="server" Text="Escala Departamental:"></asp:Label>
                                                                    </td>
                                                                    <td class="cadlbl" width="215">
                                                                        <asp:Label ID="lblRegionalList" runat="server" Text="Regional:"></asp:Label>
                                                                    </td>
                                                                    <td class="cadlbl" width="180">
                                                                        <asp:Label ID="lblFilialList" runat="server" Text="Filial:"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:TextBox ID="txtNumeroEscalaList" runat="server" CssClass="cadtxt" MaxLength="50"
                                                                            Width="150px"></asp:TextBox>
                                                                    </td>
                                                                    <td style="height: 19px">
                                                                        <asp:DropDownList ID="ddlEscalaDepartamentalList" runat="server" CssClass="cadddl"
                                                                            Width="150px" OnSelectedIndexChanged="ddlEscalaDepartamentalList_SelectedIndexChanged"
                                                                            AutoPostBack="True">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td style="height: 19px">
                                                                        <asp:DropDownList ID="ddlRegionalList" runat="server" CssClass="cadddl" Width="150px"
                                                                            Enabled="False">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td style="height: 19px">
                                                                        <asp:DropDownList ID="ddlFilialList" runat="server" CssClass="cadddl" Width="150px"
                                                                            Enabled="False">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="cadlbl">
                                                                        <asp:Label ID="lblStatusList" runat="server" Text="Status:"></asp:Label>
                                                                    </td>
                                                                    <td class="cadlbl">
                                                                        <asp:Label ID="lblTipoSolicitacaoList" runat="server" Text="Tipo de Solicitação:"></asp:Label>
                                                                    </td>
                                                                    <td class="cadlbl" width="215">
                                                                        <asp:Label ID="lblDataInicioList" runat="server" Text="Data Início:"></asp:Label>
                                                                    </td>
                                                                    <td class="cadlbl">
                                                                        <asp:Label ID="lbldataFimList" runat="server" Text="Data Fim:"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="height: 19px" width="180">
                                                                        <asp:DropDownList ID="ddlStatusList" runat="server" CssClass="cadddl" Width="150px">
                                                                        </asp:DropDownList>
                                                                    </td>

                                                                    <td style="height: 19px" width="180">
                                                                        <asp:DropDownList ID="ddlTipoSolicitacaoList" runat="server" CssClass="cadddl" Width="150px">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td width="180">
                                                                        <asp:TextBox ID="txtDataInicioList" runat="server" CssClass="cadtxt" MaxLength="10"
                                                                            Width="127px"></asp:TextBox>
                                                                        <a hidefocus onclick="if(self.gfPop)gfPop.fPopCalendar(document.forms[0].all('<% = this.txtDataInicioList.ClientID.ToString() %>'));return false;"
                                                                            href="javascript:void(0)">
                                                                            <img height="19" alt="" src="../../Agenda/calbtn.gif" width="20" align="absMiddle"
                                                                                border="0" name="popcal" />
                                                                        </a>
                                                                    </td>
                                                                    <td width="180">
                                                                        <asp:TextBox ID="txtDataFimList" runat="server" CssClass="cadtxt" MaxLength="10"
                                                                            Width="127px"></asp:TextBox>
                                                                        <a hidefocus onclick="if(self.gfPop)gfPop.fPopCalendar(document.forms[0].all('<% = this.txtDataFimList.ClientID.ToString() %>'));return false;"
                                                                            href="javascript:void(0)">
                                                                            <img height="19" alt="" src="../../Agenda/calbtn.gif" width="20" align="absMiddle"
                                                                                border="0" name="popcal" />
                                                                        </a>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="cadlbl" colspan="2">
                                                                        <asp:Label ID="lblSolicitanteList" runat="server" Text="Solicitante:"></asp:Label>
                                                                    </td>
                                                                    <td class="cadlbl" colspan="2">
                                                                        <asp:Label ID="lblColaboradorList" runat="server" Text="Colaborador:"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="height: 19px" colspan="2">
                                                                        <asp:TextBox ID="txtSolicitanteList" runat="server" CssClass="cadtxt" MaxLength="50"
                                                                            Width="327px"></asp:TextBox>
                                                                    </td>
                                                                    <td style="height: 19px" colspan="2">
                                                                        <asp:TextBox ID="txtColaboradorList" runat="server" CssClass="cadtxt" MaxLength="50"
                                                                            Width="359px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="cadlbl" colspan="2">
                                                                        <asp:Label ID="lblAprovadorList" runat="server" Text="Aprovador:"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="height: 19px" colspan="2">
                                                                        <asp:TextBox ID="txtAprovadorList" runat="server" CssClass="cadtxt" MaxLength="50"
                                                                            Width="327px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="padding-left: 50px; height: 19px" align="center" colspan="4">
                                                                        <br />
                                                                        <asp:Button ID="btnBuscarList" OnClick="btnBuscarList_Click" runat="server" Text="Buscar"
                                                                            CssClass="cadbuttonfiltro" Width="60px" CausesValidation="False"></asp:Button>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                        <!-- ****************** END: FILTROS *********************** -->
                                                        <table class="MarginTopConteudo" cellspacing="0" cellpadding="0" width="740" align="center"
                                                            border="0">
                                                            <tbody>
                                                                <tr>
                                                                    <td style="padding-top: 20px" align="center" colspan="3">
                                                                        <!-- ********************* STAR: RADGRID ************************** -->
                                                                        <rad:RadGrid ID="radGridEscala" runat="server" CssClass="dtg" Width="100%" OnNeedDataSource="radGridEscala_NeedDataSource"
                                                                            OnItemDataBound="radGridEscala_ItemDataBound" OnItemCommand="radGridEscala_ItemCommand"
                                                                            Skin="None" GridLines="None" AutoGenerateColumns="False" AllowSorting="True"
                                                                            AllowPaging="True">
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
                                                                                    <rad:GridBoundColumn DataField="Id_Escalacao" HeaderText="Nº Escala" SortExpression="Id_Escalacao"
                                                                                        UniqueName="Id_Escalacao">
                                                                                        <ItemStyle Width="5%"></ItemStyle>
                                                                                    </rad:GridBoundColumn>
                                                                                    <rad:GridBoundColumn DataField="Des_EscalaDpto" HeaderText="Escala Departamental"
                                                                                        SortExpression="Des_EscalaDpto" UniqueName="Des_EscalaDpto">
                                                                                        <ItemStyle Width="25%"></ItemStyle>
                                                                                    </rad:GridBoundColumn>
                                                                                    <rad:GridBoundColumn DataField="Dt_InicioPeriodo" HeaderText="Data/Hora Início" SortExpression="Dt_InicioPeriodo"
                                                                                        UniqueName="Dt_InicioPeriodo" DataFormatString="{0:dd/MM/yyyy HH:mm}">
                                                                                        <ItemStyle Width="5%"></ItemStyle>
                                                                                    </rad:GridBoundColumn>
                                                                                    <rad:GridBoundColumn DataField="Dt_FinalPeriodo" HeaderText="Data/Hora Fim" SortExpression="Dt_FinalPeriodo"
                                                                                        UniqueName="Dt_FinalPeriodo" DataFormatString="{0:dd/MM/yyyy HH:mm}">
                                                                                        <ItemStyle Width="5%"></ItemStyle>
                                                                                    </rad:GridBoundColumn>
                                                                                    <rad:GridBoundColumn DataField="Alias_Filial" HeaderText="Filial" SortExpression="Alias_Filial"
                                                                                        UniqueName="Alias_Filial">
                                                                                        <ItemStyle Width="15%" HorizontalAlign="Center"></ItemStyle>
                                                                                    </rad:GridBoundColumn>


                                                                                    <rad:GridBoundColumn DataField="DES_TipoSolicitacao" HeaderText="Tipo da Solicitação" SortExpression="DES_TipoSolicitacao"
                                                                                        UniqueName="DES_TipoSolicitacao">
                                                                                        <ItemStyle Width="15%" HorizontalAlign="Center"></ItemStyle>
                                                                                    </rad:GridBoundColumn>


                                                                                    <rad:GridBoundColumn DataField="Des_StatusAprovacao" HeaderText="Status" SortExpression="Des_StatusAprovacao"
                                                                                        UniqueName="Des_StatusAprovacao">
                                                                                        <ItemStyle Width="15%"></ItemStyle>
                                                                                    </rad:GridBoundColumn>



                                                                                    <rad:GridBoundColumn DataField="USU_N_CODIGO" SortExpression="USU_N_CODIGO" UniqueName="USU_N_CODIGO"
                                                                                        Visible="false" />
                                                                                    <rad:GridTemplateColumn UniqueName="TemplateColumn" Visible="False">
                                                                                        <ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
                                                                                        <ItemTemplate>
                                                                                            <asp:ImageButton ID="Visualizar" runat="server" AlternateText="Visualizar" CausesValidation="False"
                                                                                                CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id_Escalacao") %>'
                                                                                                CommandName="Visualizar" ImageUrl="%%PATH%%/imagens/icones/ico_editar.gif" ToolTip="Visualizar">
                                                                                            </asp:ImageButton>
                                                                                        </ItemTemplate>
                                                                                    </rad:GridTemplateColumn>
                                                                                    <rad:GridButtonColumn CommandName="Ativar" UniqueName="Ativar" ButtonType="ImageButton"
                                                                                        ImageUrl="%%PATH%%/imagens/icones/ico_Ativo.gif" Text="Ativar">
                                                                                        <ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
                                                                                    </rad:GridButtonColumn>
                                                                                    <rad:GridButtonColumn CommandName="Editar" UniqueName="Editar" ButtonType="ImageButton"
                                                                                        ImageUrl="%%PATH%%/imagens/icones/ico_editar.gif" Text="Editar">
                                                                                        <ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
                                                                                    </rad:GridButtonColumn>
                                                                                    <rad:GridBoundColumn DataField="Flg_Editando" SortExpression="Flg_Editando" UniqueName="Flg_Editando"
                                                                                        Visible="false">
                                                                                        <ItemStyle Width="5%"></ItemStyle>
                                                                                    </rad:GridBoundColumn>
                                                                                    <rad:GridBoundColumn DataField="ID_STATUSSOLICITACAO" SortExpression="ID_STATUSSOLICITACAO" UniqueName="ID_STATUSSOLICITACAO"
                                                                                        Visible="false">
                                                                                        <ItemStyle Width="5%"></ItemStyle>
                                                                                    </rad:GridBoundColumn>

                                                                                    <rad:GridBoundColumn DataField="Id_TipoSolicitacao" SortExpression="Id_TipoSolicitacao" UniqueName="Id_TipoSolicitacao"
                                                                                        Visible="false">
                                                                                        <ItemStyle Width="5%"></ItemStyle>
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
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <!-- ********************* START: BARRA DE BOTÕES INFERIOR ************************** -->
                                        <table cellspacing="0" cellpadding="0" width="760" align="center" border="0">
                                            <tbody>
                                                <tr>
                                                    <td class="backBarraBotoes" align="right">
                                                        <asp:Button ID="btnMontarNovaEscala" OnClick="btnMontarNovaEscala_Click" runat="server"
                                                            Text="Montar Nova Escala" CssClass="cadbutton120" Width="120px"></asp:Button>
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
                    <!-- ********************* END: PAINEL LISTAGEM ************************** -->
                    <!-- ****************** START: PAINEL CADASTRO *********************** -->
                    <asp:Panel ID="pnlCadastro" runat="server">
                        <!-- ***************** START: BARRA DE TITUTO ******************* -->
                        <table class="MarginTopBarra" cellspacing="0" cellpadding="0" width="760" align="center"
                            border="0">
                            <tbody>
                                <tr>
                                    <td style="width: 750px; height: 25px" class="cadBarraTitulo" align="left">
                                        <img src="../../Imagens/comum/bulletbarra.gif" align="absMiddle" />
                                        <asp:Label ID="lblTituloCad" runat="server" Text="Lançamento de Escalas - Seleção de Escala e Colaboradores"></asp:Label>
                                    </td>
                                    <td style="width: 9px" class="cadBarraTitulo" align="right" height="25">
                                        <asp:ImageButton ID="btnHelpCad" runat="server" Visible="False" ImageAlign="AbsMiddle"
                                            ImageUrl="%%PATH%%/imagens/icones/ico_help.jpg" AlternateText="Ajuda"></asp:ImageButton>
                                    </td>
                                </tr>
                                <!-- ***************** END: BARRA DE TITUTO ******************* -->
                                <tr>
                                    <td style="height: 250px" class="backboxconteudo" valign="top" colspan="2">
                                        <rada:RadAjaxPanel ID="RadAjaxPanelCadastro" runat="server" LoadingPanelID="lpaCadastro"
                                            ClientEvents-OnRequestStart="OnRequestStart">
                                            <!-- ****************** START: FORMULARIO *********************** -->
                                            <table class="MarginTopConteudo" cellspacing="0" cellpadding="0" width="740" align="center"
                                                border="0">
                                                <tr>
                                                    <td class="cadmsg">
                                                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List" />
                                                        <asp:Label ID="lblMensagem" runat="server" Text="Label" Visible="false" CssClass="cadlbl"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Panel ID="pnlCadEscala" runat="server" />
                                                        <asp:HiddenField ID="txtHiddenColaboradores" runat="server" />
                                                        <table width="740" cellspacing="0" cellpadding="0" align="center" border="0">
                                                            <tr>
                                                                <td class="cadlbl" width="240">
                                                                    <asp:Label ID="lblEscalaDepartamental" runat="server" Text="Escala Departamental:"></asp:Label>
                                                                </td>
                                                                <td class="cadlbl" width="240">
                                                                    <asp:Label ID="lblRegional" runat="server" Text="Regional:"></asp:Label>
                                                                </td>
                                                                <td class="cadlbl" width="240">
                                                                    <asp:Label ID="lblFilial" runat="server" Text="Filial:"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td width="240" style="height: 19px">
                                                                    <asp:DropDownList ID="ddlEscalaDepartamental" runat="server" CssClass="cadddl" AutoPostBack="True"
                                                                        Width="150px" OnSelectedIndexChanged="ddlEscalaDepartamental_SelectedIndexChanged">
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="rfvEscalaDepartamental" runat="server" ControlToValidate="ddlEscalaDepartamental"
                                                                        ErrorMessage="Campo Obrigatório: Escala Departamental." InitialValue="0" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                                                </td>
                                                                <td width="240" style="height: 19px">
                                                                    <asp:DropDownList ID="ddlRegional" runat="server" CssClass="cadddl" AutoPostBack="false"
                                                                        Width="150px" Enabled="False">
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="rfvRegional" runat="server" ControlToValidate="ddlRegional"
                                                                        ErrorMessage="Campo Obrigatório: Regional." InitialValue="0" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                                                </td>
                                                                <td width="240" style="height: 19px">
                                                                    <asp:DropDownList ID="ddlFilial" runat="server" CssClass="cadddl" Enabled="False"
                                                                        Width="150px">
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="rfvFilial" runat="server" ControlToValidate="ddlFilial"
                                                                        ErrorMessage="Campo Obrigatório: Filial." InitialValue="0" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="cadlbl" colspan="3">
                                                                    <asp:Label ID="lblPeríodo" runat="server" Text="Período:"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="3" style="height: 19px">
                                                                    <asp:DropDownList ID="ddlPeriodo" runat="server" CssClass="cadddl" Width="245px">
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="rfvPeriodo" runat="server" ControlToValidate="ddlPeriodo"
                                                                        ErrorMessage="Campo Obrigatório: Período." InitialValue="0" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="cadlbl" colspan="3" style="padding-top: 10px;">
                                                                    <asp:Label ID="lblListaColaboradores" runat="server" Text="Lista de Colaboradores:"></asp:Label>
                                                                    <asp:ImageButton ID="btnAdicionar" CssClass="BotaoAdicionar" runat="server" ImageUrl="~/Imagens/icones/ico_btn_adiconar.png"
                                                                        OnClick="btnAdicionar_Click" CausesValidation="False" ToolTip="Adicionar Colaboradores" />
                                                                    <asp:ImageButton ID="btnRemover" CssClass="BotaoAdicionar" runat="server" ImageUrl="~/Imagens/icones/ico_btn_remover.png"
                                                                        CausesValidation="False" ToolTip="Remover Colaborador(es)" OnClick="btnRemover_Click" />
                                                                    <asp:CustomValidator ID="cvrListaColaboradores" runat="server" ErrorMessage="Campo Obrigatório: Lista de Colaboradores"
                                                                        OnServerValidate="cvrListaColaboradores_ServerValidate">*</asp:CustomValidator>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="3" style="padding-bottom: 5px;">
                                                                    <asp:ListBox ID="lstColaboradores" runat="server" Height="300px" Width="643px" SelectionMode="Multiple"
                                                                        CssClass="cadlstBox"></asp:ListBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </rada:RadAjaxPanel>
                                        <!-- ********************* START: BARRA DE BOTÕES INFERIOR ************************** -->
                                        <table cellspacing="0" cellpadding="0" width="760" align="center" border="0">
                                            <caption>
                                            </caption>
                                            <tbody>
                                                <tr>
                                                    <td style="height: 28px" class="backBarraBotoes" align="right">
                                                        &nbsp;<asp:Button ID="btnVoltar" OnClick="btnVoltar_Click" runat="server" Text="Voltar"
                                                            CssClass="cadbutton100" Width="50px" CausesValidation="False"></asp:Button>
                                                        &nbsp;
                                                        <asp:Button ID="btnAvancar" OnClick="btnAvancar_Click" runat="server" Text="Avançar"
                                                            CssClass="cadbutton100"></asp:Button>
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
                    <!-- ********************* END: PAINEL CADASTRO ************************** -->
                </rada:RadAjaxPanel>
                <rada:AjaxLoadingPanel ID="lpaCadastro" runat="server" Width="75px" Height="75px"
                    Transparency="30" HorizontalAlign="Center">
                    <asp:Image ID="Image1" Style="margin-top: 100px" runat="server" ImageUrl="%%PATH%%/RadControls/Ajax/Skins/Default/Loading.gif"
                        AlternateText="Aguarde ..."></asp:Image>
                </rada:AjaxLoadingPanel>
            </td>
        </tr>
    </table>
    <iframe id="IframeAgenda" runat="server" style="z-index: 101; left: -500px; visibility: visible;
        position: absolute; top: 0px" name="gToday:contrast:agenda.js" src="../../Agenda/calendario.htm"
        frameborder="0" width="132" scrolling="no" height="142"></iframe>
    </form>
</body>
</html>
