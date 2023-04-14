<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ListLimitePrmColaborador.aspx.cs"
    Inherits="SafWeb.UI.Admin.Permissao.ListLimitePrmColaborador" %>

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
    <script src="../../Scripts/jquery.event.drag-2.2.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.meio.mask.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-popup.js" type="text/javascript"></script>
    <%--<script src="../../Scripts/jquery-1.7.2-vsdoc.js" type="text/javascript"></script>--%>
    <script language="javascript" type="text/javascript">

        $(document).ready(function () {
            $('input[type="text"]').setMask();
            //BindEvents();
        });

        $('input[type="text"]').live("setMask");

        function PopupInit(evento) {
            $.CriarPopUp({
                'titulo': evento + ' Limite',
                'width': 340,
                'height': (evento == "Adicionar" ? 188 : 158),
                'id': 'ppPrincipal',
                "closeCallback": function (obj) {
                    if (confirm("Deseja fechar a janela?")) {
                        return true;
                    } else {
                        return false;
                    }
                }
            });

            $("#ppPrincipal").keypress(function (event) {
                if (event.keyCode == 13) {
                    $("#btnSalvar", $(this)).click();

                    event.preventDefault();
                }
            });
        }

        function atualizar() {
            $('input[type="text"]').setMask();
        }

        $("input[id='btnAdicionar']").live("click", function () {
            ShowLoad("lpaCadastro");
            PopupInit("Adicionar");
            //abre a nova janela, já com a sua devida posição
            $("#ppPrincipal").popup("loadUrl", {
                url: '%%PATH%%/Admin/Permissao/CadLimitePrmColaborador.aspx',
                callback: function () {
                    HideLoad("lpaCadastro");
                    $("#ddlRegional", "#dvRegional").focus();
                }
            });
        });

        $("img[id='btnEditar']").live("click", function () {
            ShowLoad("lpaCadastro");
            PopupInit("Editar");

            idLimiteColaborador = parseInt($(this).parent().parent().children("td:first").html());
            //abre a nova janela, já com a sua devida posição
            $("#ppPrincipal").popup("loadUrl", {
                url: '%%PATH%%/Admin/Permissao/CadLimitePrmColaborador.aspx?idLimitePrmColaborador=' + idLimiteColaborador,
                callback: function () {
                    HideLoad("lpaCadastro");
                    $("#ddlLimite", ".backBarraBotoes").focus();
                }
            });
        });

        $("img[id='btnExcluir']").live("click", function () {
            if (confirmaExclusao()) {
                ExcluiColaborador($(this));
            }
        });

        function ExcluiColaborador(obj) {

            idLimiteColaborador = parseInt($(obj).parent().parent().children("td:first").html());
            ShowLoad("lpaCadastro");
            PageMethods.RemoverPrmColaborador(
                idLimiteColaborador,
                function (r) {
                    HideLoad("lpaCadastro");

                    if (r.erro == false) {
                        $(obj).parent().parent().fadeOut();
                    } else {
                        alert(r.mensagem);
                    }
                },
                function (r) {
                    HideLoad("lpaCadastro");
                    alert("Tente novamente!");
                }
            );
        }

        function confirmaExclusao() {
            if (confirm("Confirma a exclusão do Colaborador?")) {
                return true;
            } else {
                return false;
            }

        }
    </script>
    <style type="text/css">
        .dtgItemStyle img, .dtgItemStyleAlternate img
        {
            cursor: pointer;
        }
        
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
    </style>
</head>
<body>
    <form id="form" method="post" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
    </asp:ScriptManager>
    <table cellspacing="0" cellpadding="0" width="778" align="center" border="0">
        <tr>
            <td style="width: 1010px">
                <!-- ********************* START: CABEÇALHO ****************** -->
                <cc1:FWServerControl ID="Fwservercontrol3" runat="server" Controle="PagebannerAdmin">
                </cc1:FWServerControl>
                <!-- ********************* END: CABEÇALHO ****************** -->
            </td>
        </tr>
        <tr>
            <!-- ********************* START: CONTEÚDO ****************** -->
            <td class="backbox" valign="top" height="250" style="width: 1010px">
                <rada:RadAjaxPanel ID="RadAjaxPanel1" runat="server" ClientEvents-OnRequestStart="OnRequestStart"
                    LoadingPanelID="lpaCadastro" ClientEvents-OnResponseEnd="atualizar">
                    <!-- ********************* PAINEL DE ERRO ****************** -->
                    <asp:Panel ID="pnlErro" runat="server">
                    </asp:Panel>
                    <!-- ****************** START: PAINEL LISTAGEM *********************** -->
                    <asp:Panel ID="pnlListagem" runat="server">
                        <!-- ***************** START: BARRA DE TITUTO ******************* -->
                        <table width="760" cellspacing="0" cellpadding="0" border="0" align="center" class="MarginTopBarra">
                            <tbody>
                                <tr>
                                    <td class="style1" align="left">
                                        <img src="../../Imagens/comum/bulletbarra.gif" align="absMiddle" />
                                        Lista de Colaboradores com Limite de permissões
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
                                        <table width="740" cellspacing="0" cellpadding="0" border="0" align="center" class="MarginTopConteudo">
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <!-- ****************** START: FILTROS *********************** -->
                                                        <table cellspacing="0" cellpadding="0" border="0" width="740" align="center">
                                                            <tbody>
                                                                <tr>
                                                                    <td class="cadlbl" width="157px">
                                                                        <asp:Label ID="lblColaborador" runat="server" Text="Colaborador"></asp:Label>
                                                                    </td>
                                                                    <td class="cadlbl" width="50px">
                                                                        <asp:Label ID="lblLimite" runat="server" Text="Limite"></asp:Label>
                                                                    </td>
                                                                    <td class="cadlbl" width="180">
                                                                        <asp:Label ID="LblCriterio" runat="server" Text="Critério Limite"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="height: 19px">
                                                                        <asp:TextBox ID="txtColaborador" runat="server" CssClass="cadtxt" MaxLength="50"
                                                                            Width="150px">
                                                                        </asp:TextBox>
                                                                    </td>
                                                                    <td style="height: 19px">
                                                                        <asp:TextBox ID="txtLimite" runat="server" CssClass="cadtxt" MaxLength="50" Width="43px"
                                                                            mask="porcentagem">
                                                                        </asp:TextBox>
                                                                    </td>
                                                                    <td style="height: 19px">
                                                                        <asp:DropDownList ID="ddlcriterio" runat="server" CssClass="cadddl" Width="109px">
                                                                            <asp:ListItem Value="0">Igual</asp:ListItem>
                                                                            <asp:ListItem Value="1">Maior ou Igual</asp:ListItem>
                                                                            <asp:ListItem Value="2">Menor ou Igual</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="height: 19px" align="center" colspan="3">
                                                                        <br />
                                                                        <asp:Button ID="btnBuscarList" runat="server" Text="Buscar" CssClass="cadbuttonfiltro"
                                                                            Width="60px" CausesValidation="False" OnClick="btnBuscarList_Click"></asp:Button>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                        <!-- ****************** END: FILTROS *********************** -->
                                                        <table width="740" cellspacing="0" cellpadding="0" border="0" align="center" class="MarginTopConteudo">
                                                            <tbody>
                                                                <tr>
                                                                    <td style="padding-top: 20px" align="center" colspan="3">
                                                                        <!-- ********************* STAR: RADGRID ************************** -->
                                                                        <rad:RadGrid ID="radGridLimite" runat="server" CssClass="dtg" Width="100%" OnNeedDataSource="radGridLimite_NeedDataSource"
                                                                            OnItemDataBound="radGridLimite_ItemDataBound" OnItemCommand="radGridLimite_ItemCommand"
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
                                                                                                    <asp:Label ID="Label6" runat="server" CssClass="paglbl" Enabled="True">
                                                                                                        <%#DataBinder.Eval(Container, "Paging.PageCount").ToString()%></asp:Label>
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
                                                                                    <rad:GridBoundColumn DataField="idLimiteColaborador" HeaderText="Código Limite Colaborador"
                                                                                        SortExpression="idLimiteColaborador" UniqueName="idLimiteColaborador">
                                                                                        <HeaderStyle Width="30%" HorizontalAlign="Left" />
                                                                                        <ItemStyle Width="30%"></ItemStyle>
                                                                                    </rad:GridBoundColumn>
                                                                                    <rad:GridBoundColumn DataField="NomeColaborador" HeaderText="Colaborador" SortExpression="NomeColaborador"
                                                                                        UniqueName="NomeColaborador">
                                                                                        <HeaderStyle Width="30%" HorizontalAlign="Left" />
                                                                                        <ItemStyle Width="30%"></ItemStyle>
                                                                                    </rad:GridBoundColumn>
                                                                                    <rad:GridBoundColumn DataField="Limite" HeaderText="Limite" SortExpression="Limite"
                                                                                        UniqueName="Limite" DataFormatString="{0:##0.00%}">
                                                                                        <HeaderStyle HorizontalAlign="Left" Width="60px" />
                                                                                        <ItemStyle Width="60px"></ItemStyle>
                                                                                    </rad:GridBoundColumn>
                                                                                    <rad:GridTemplateColumn UniqueName="TemplateColumn" Visible="true">
                                                                                        <ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
                                                                                        <ItemTemplate>
                                                                                            <img src="%%PATH%%/imagens/icones/ico_editar.gif" alt="Editar" id="btnEditar" />
                                                                                        </ItemTemplate>
                                                                                    </rad:GridTemplateColumn>
                                                                                    <rad:GridTemplateColumn UniqueName="TemplateColumn" Visible="true">
                                                                                        <ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
                                                                                        <ItemTemplate>
                                                                                            <img src="%%PATH%%/imagens/icones/ico_deletar.gif" alt="Exluir" id="btnExcluir" />
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
                                        <table align="center" border="0" cellpadding="0" cellspacing="0" width="760">
                                            <tbody>
                                                <tr>
                                                    <td align="right" class="backBarraBotoes">
                                                        <input type="button" id="btnAdicionar" class="cadbutton80" width="80px" value="Adicionar" />
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
                </rada:RadAjaxPanel>
                <rada:AjaxLoadingPanel ID="lpaCadastro" runat="server" Width="75px" Height="75px"
                    Transparency="30" HorizontalAlign="Center">
                    <asp:Image ID="Image1" Style="margin-top: 100px" runat="server" ImageUrl="%%PATH%%/RadControls/Ajax/Skins/Default/Loading.gif"
                        AlternateText="Aguarde ..."></asp:Image>
                </rada:AjaxLoadingPanel>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
