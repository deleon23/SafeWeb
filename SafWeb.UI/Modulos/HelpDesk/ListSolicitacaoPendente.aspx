<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ListSolicitacaoPendente.aspx.cs"
    Inherits="SafWeb.UI.Modulos.HelpDesk.ListSolicitacaoPendente" %>

<%@ Register TagPrefix="rad" Namespace="Telerik.WebControls" Assembly="RadGrid.Net2" %>
<%@ Register TagPrefix="rada" Namespace="Telerik.WebControls" Assembly="RadAjax.Net2" %>
<%@ Register TagPrefix="cc1" Namespace="FrameWork.WebControl" Assembly="FrameWork.WebControl" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions" %>
<html>
<head>
    <title>%%SITE%%</title>
    <link href="../../Estilos/FrameWork.css" type="text/css" rel="stylesheet" />
    <link href="../../Estilos/Brinks.css" rel="stylesheet" type="text/css" />
    <link href="../../Estilos/PopUp.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery-1.7.2.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-popup.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {

            $("#ddlRegional").change(function () {
                BuscarFilial($(this));
            });

            //BindEvents();
        });

        function atualizar() {
            //BindEvents();
        }

        $("img[id='btnEditar']").live("click", function () {
            Id_SolicitacaoCrachaTitular = parseInt($(this).parent().parent().children("td:first").html());
            //abre a nova janela, já com a sua devida posição
            PageMethods.CriptografarParametro(
                Id_SolicitacaoCrachaTitular,
                function (r) {
                    if (r.erro == false) {
                        window.location = "%%PATH%%/Modulos/HelpDesk/ManutencaoPermissaoCrachaTitular.aspx?mod=" + r.lista;
                    } else {
                        alert(r.mensagem);
                    }
                },
                function (r) {
                    alert("Tente novamente!");
                }
            );
        })

        function BindEvents() {

        }

        function BuscarFilial(obj) {
            var options = "<option value='0'><-- Selecione --></option>";

            var IdRegional = parseInt($(obj).val());
            if (IdRegional == 0) {
                $("#ddlFilial").attr("disabled", "disabled");
            }
            else {
                $("#ddlFilial").attr("disabled", "");
                $("#ddlFilial").removeAttr("disabled");
                PageMethods.PopularFilial(
                    IdRegional,
                    function (r) {
                        if (r.erro == false) {
                            $.each(r.lista, function (key, value) {
                                options += "<option value='" + value.IdFilial + "'>" + value.AliasFilial + "</option>";
                            });


                        } else {
                            alert(r.mensagem);
                        }

                        $("#ddlFilial").html(options);
                    },
                    function (r) {
                        alert("Tente novamente!");
                    }
                );
            }
        }

    </script>
    <style type="text/css">
        .style1
        {
            font-style: normal;
            font-variant: normal;
            font-weight: bold;
            font-size: 11px;
            line-height: normal;
            font-family: Tahoma, arial;
            color: #ffffff;
            width: 750px;
            height: 25px;
            background-color: #526CA1;
        }
        .style2
        {
            font-style: normal;
            font-variant: normal;
            font-weight: bold;
            font-size: 11px;
            line-height: normal;
            font-family: Tahoma, arial;
            color: #ffffff;
            width: 9px;
            height: 25px;
            background-color: #526CA1;
        }
        
        #btnEditar
        {
            cursor: pointer;
        }
    </style>
</head>
<body>
    <form id="form" method="post" runat="server">
    <asp:ScriptManager ID="ScriptManager" runat="server" EnablePageMethods="true">
    </asp:ScriptManager>
    <table cellspacing="0" cellpadding="0" width="778" align="center" border="0">
        <tbody>
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
                    <rada:RadAjaxPanel ID="RadAjaxPanel" runat="server" ClientEvents-OnRequestStart="OnRequestStart"
                        LoadingPanelID="lpaCadastro" ClientEvents-OnResponseEnd="atualizar">
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
                                        <td class="style1" align="left">
                                            <img src="../../Imagens/comum/bulletbarra.gif" align="absMiddle" />
                                            Listagem de Permissão Pendentes de Crachá Titular
                                        </td>
                                        <td class="style2" align="right">
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
                                                <tr>
                                                    <td colspan="7" class="cadmsg">
                                                        <asp:Label ID="lblMensagem" runat="server" Text="Label" Visible="false" CssClass="cadlbl"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="cadlbl" width="180">
                                                        <asp:Label ID="lblNumSolicitacao" runat="server" Text="Nº Solicitação:"></asp:Label>
                                                    </td>
                                                    <td width="5">
                                                    </td>
                                                    <td class="cadlbl" width="180">
                                                        <asp:Label ID="lblRegional" runat="server" Text="Regional:"></asp:Label>
                                                    </td>
                                                    <td width="5">
                                                    </td>
                                                    <td class="cadlbl" style="width: 215px">
                                                        <asp:Label ID="lblFilial" runat="server" Text="Filial:"></asp:Label>
                                                    </td>
                                                    <td width="5">
                                                    </td>
                                                    <td class="cadlbl" width="180">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <cc1:FWMascara ID="txtNumSolicitacao" runat="server" CssClass="cadtxt" Mascara="NUMERO"
                                                            MaxLength="5" Width="75px" AutoPostBack="false"></cc1:FWMascara>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlRegional" runat="server" CssClass="cadddl" Width="180px">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td style="width: 215px">
                                                        <asp:DropDownList ID="ddlFilial" Enabled="False" CssClass="cadddl" runat="server"
                                                            Width="120px">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="cadlbl" colspan="3">
                                                        <asp:Label ID="lblColaborador" runat="server" Text="Colaborador:"></asp:Label>
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3" style="height: 27px">
                                                        <asp:TextBox ID="txtColaborador" runat="server" CssClass="cadtxt" MaxLength="50"
                                                            Width="340px"></asp:TextBox>
                                                    </td>
                                                    <td style="height: 27px">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="cadlbl" colspan="3">
                                                        <asp:Label ID="Label9" runat="server" Text="Tipo Solicitação:"></asp:Label>
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3" style="height: 27px">
                                                        <asp:DropDownList ID="ddlTipoSolicitacao" CssClass="cadddl" runat="server" Width="335px">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="height: 21px">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="7" align="center" style="height: 19px">
                                                        <br />
                                                        <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="cadbuttonfiltro"
                                                            OnClick="btnBuscar_Click" />
                                                        <br />
                                                    </td>
                                                </tr>
                                            </table>
                                            <table class="MarginTopConteudo" cellspacing="0" cellpadding="0" width="740" align="center"
                                                border="0">
                                                <tr>
                                                    <td align="center" colspan="3">
                                                        <!-- ********************* STAR: RADGRID ************************** -->
                                                        <rad:RadGrid ID="radGridSolicitacao" runat="server" AllowPaging="True" AllowSorting="True"
                                                            AutoGenerateColumns="False" SortingSettings-SortToolTip="Clique para filtrar"
                                                            CssClass="dtg" GridLines="None" Skin="None" Width="100%">
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
                                                                    <rad:GridBoundColumn DataField="idSolicitacaoCrachaTitular" HeaderText="Nº Solicitação"
                                                                        SortExpression="idSolicitacaoCrachaTitular" UniqueName="idSolicitacaoCrachaTitular">
                                                                        <ItemStyle Width="60px" HorizontalAlign="Center"></ItemStyle>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                    </rad:GridBoundColumn>
                                                                    <rad:GridBoundColumn DataField="Filial" HeaderText="Filial" SortExpression="Filial"
                                                                        UniqueName="Filial">
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle Width="30px"></ItemStyle>
                                                                    </rad:GridBoundColumn>
                                                                    <rad:GridBoundColumn DataField="NomeSolicitante" HeaderText="Solicitante" SortExpression="NomeSolicitante"
                                                                        UniqueName="NomeSolicitante" AllowFiltering="true">
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle Width="130px"></ItemStyle>
                                                                    </rad:GridBoundColumn>
                                                                    <rad:GridBoundColumn DataField="NomeColaborador" HeaderText="Colaborador" SortExpression="NomeColaborador"
                                                                        UniqueName="NomeColaborador" AllowFiltering="true">
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle Width="130px"></ItemStyle>
                                                                    </rad:GridBoundColumn>
                                                                    <rad:GridBoundColumn DataField="Areas" HeaderText="Áreas Solicitadas" SortExpression="Areas"
                                                                        UniqueName="Areas">
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle Width="10px" HorizontalAlign="Center"></ItemStyle>
                                                                    </rad:GridBoundColumn>
                                                                    <rad:GridBoundColumn DataField="TipoSolicitacao" HeaderText="Tipo Solicitação" SortExpression="TipoSolicitacao"
                                                                        UniqueName="TipoSolicitacao">
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle Width="140px" HorizontalAlign="Center"></ItemStyle>
                                                                    </rad:GridBoundColumn>
                                                                    <rad:GridBoundColumn DataField="DataSolicitacao" HeaderText="Data da Solicitação"
                                                                        SortExpression="DataSolicitacao" UniqueName="DataSolicitacao">
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle Width="10px" HorizontalAlign="Center"></ItemStyle>
                                                                    </rad:GridBoundColumn>
                                                                    <rad:GridTemplateColumn UniqueName="TemplateColumn" Visible="true">
                                                                        <ItemStyle HorizontalAlign="Center" Width="10px"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <img src="%%PATH%%/imagens/icones/ico_editar.gif" text="Editar" id="btnEditar" />
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
                                </tbody>
                            </table>
                            <!-- ********************** END: FILTROS ************************** -->
                            <!-- ********************* START: BARRA DE BOTÕES INFERIOR ************************** -->
                            <table cellspacing="0" cellpadding="0" width="760" align="center" border="0">
                                <tbody>
                                    <tr>
                                        <td class="backBarraBotoes" align="right">
                                            &nbsp;
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            <!-- ********************* END: BARRA DE BOTÕES INFERIOR ************************** -->
                        </asp:Panel>
                    </rada:RadAjaxPanel>
                </td>
            </tr>
        </tbody>
    </table>
    <!-- ********************* END: PAINEL LISTAGEM ************************** -->
    <rada:AjaxLoadingPanel ID="lpaCadastro" runat="server" Width="75px" Height="75px"
        Transparency="30" HorizontalAlign="Center">
        <asp:Image ID="Image1" Style="margin-top: 100px" runat="server" ImageUrl="%%PATH%%/RadControls/Ajax/Skins/Default/Loading.gif"
            AlternateText="Aguarde ..."></asp:Image>
    </rada:AjaxLoadingPanel>
    </form>
</body>
</html>
