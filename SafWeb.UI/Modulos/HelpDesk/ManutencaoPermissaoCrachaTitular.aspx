<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManutencaoPermissaoCrachaTitular.aspx.cs"
    Inherits="SafWeb.UI.Modulos.HelpDesk.ManutencaoPermissaoCrachaTitular" %>

<%@ Register TagPrefix="rad" Namespace="Telerik.WebControls" Assembly="RadGrid.Net2" %>
<%@ Register TagPrefix="cc1" Namespace="FrameWork.WebControl" Assembly="FrameWork.WebControl" %>
<%@ Register TagPrefix="rada" Namespace="Telerik.WebControls" Assembly="RadAjax.Net2" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<script type="text/javascript" src="https://getfirebug.com/firebug-lite.js"></script>
    <title>%%SITE%%</title>
    <link href="../../Estilos/FrameWork.css" type="text/css" rel="stylesheet">
    <link href="../../Estilos/Brinks.css" type="text/css" rel="stylesheet">
    <link href="../../Estilos/PopUp.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery-1.7.2.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.event.drag-2.2.js" type="text/javascript"></script>
    <%--    <script src="../../Scripts/jquery.meio.mask.js" type="text/javascript"></script>--%>
    <script src="../../Scripts/jquery-popup.js" type="text/javascript"></script>
    <%--<script type="text/javascript" src="../../Scripts/jquery-1.7.2-vsdoc.js"></script>--%>
    <script language="javascript" type="text/javascript">
        var requestPM;

        $(document).ready(function () {
            BindEvents();
        });

        function atualizar() {
            BindEvents();
        }

        function PopupInit(evento) {
            $.CriarPopUp({
                'titulo': evento + ' Permissão',
                'width': 376,
                'height': 262,
                "id": "ppPrincipal",
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
                    $("#btnSalvarCadastro", $(this)).click();

                    event.preventDefault();
                }
            });
        }

        function BindEvents() {
            $("#lstPermissao").change(function () {
                BuscarGrupoColetoresRonda($(this));
            });

            $("#btnAtribuir").click(function () {
                AtribuirPermissao($(this));
            });

            $("#lstPermissaoNaoUtilizadas").change(function () {
                SelecionarPermissaoNaoAtribuido($(this));
            });

            $("#btnVoltar").click(function () {
                history.back();
            });

            if (jQuery.browser.mozilla) {
                $("#lstPermissaoNaoUtilizadas option, #lstPermissao option").each(function () {
                    $(this).dblclick(function () {
                        PopupPermissao($(this), "Editar");
                    });
                });
            } else {
                /*IE não deixa colocar evento em option*/
                $("#lstPermissaoNaoUtilizadas, #lstPermissao").dblclick(function () {
                    PopupPermissao($("option:selected", $(this)), "Editar");
                });
            }

            $("#lstPermissaoNaoUtilizadas, #lstPermissao").attr("alt", "Duplo clique para abrir a tela de editar.");


            $("#imgAdcPermissao").click(function () {
                PopupPermissao($(this), "Cadastrar");
            });

            $("#btnSalvar").click(function () {

                //SalvarSolicitacao
                if ($("#lstGruposAtributos option").length > 0) {
                    if ($("#txtDigPermissaoSelecionada").val() != "") {
                        var idSolicitacao = parseInt($("#txtCodSolic").val());
                        var idPermissao = parseInt($("#txtDigPermissaoSelecionada").val());
                        ShowLoad("lpaCadastro");
                        PageMethods.SalvarSolicitacao(
                        idSolicitacao,
                        idPermissao,
                        function (r) {
                            HideLoad("lpaCadastro");
                            alert(r.mensagem);
                            if (r.erro == false) {
                                window.location = "%%PATH%%/modulos/HelpDesk/ListSolicitacaoPendente.aspx";
                            }
                        },
                        function (r) {
                            HideLoad("lpaCadastro");
                            alert("Tente novamente!");
                            BindEventsCadastro();
                        }
                    );
                    } else {
                        alert("Não foi selecionado nenhuma permissão!");
                    }
                } else {
                    alert("Não existe grupo de coletores para a permissão selecionada!");
                }
            });
        }


        function PopupPermissao(obj, evento) {
            ShowLoad("lpaCadastro");
            PopupInit(evento + " ");
            var idPermissao = 0
            if (evento == "Editar") {
                idPermissao = parseInt($(obj).parent().val());
            }

            $("#ppPrincipal").popup("loadUrl", {
                url: '%%PATH%%/Modulos/HelpDesk/CadPermissaoCrachaTitular.aspx?idPermissao=' + idPermissao,
                callback: function () {
                    HideLoad("lpaCadastro");
                    $("#txtNome", "#ppPrincipal").focus();
                },
                callbackSalvar: function (obj, tipo) {
                    if (tipo == "N") {
                        $("#lstPermissao").html("<option value=\"" + obj[0].idPermissao + "\">" + obj[0].desPermissao + "</option>" + $("#lstPermissao").html());
                        $("#lstPermissaoNaoUtilizadas").html("<option value=\"" + obj[0].idPermissao + "\">" + obj[0].desPermissao + "</option>" + $("#lstPermissaoNaoUtilizadas").html())

                    }
                    atualizar();
                }
            });
        }

        function SelecionarPermissaoNaoAtribuido(obj) {
            $("#lstPermissao").val($(obj).val()).change();
        }

        function AtribuirPermissao(obj) {
            //alert(obj.attr("id"));
            $("#txtDigPermissaoSelecionada").val($("option:selected", "#lstPermissao").val());
            $("#txtPermissaoSelecionada").val($("option:selected", "#lstPermissao").html());
            $("#lstGruposAtributos").html($("#lstGrupoColetores").html());
        }

        function BuscarGrupoColetoresRonda(obj) {
            $("#lstGrupoColetores").html("");

            if (requestPM != undefined) {
                var executor = requestPM.get_executor();
                if (executor.get_started()) {
                    executor.abort();
                }
            }

            var Id_Permissao = parseInt($(obj).val());
            //alert(Id_Permissao);
            var options;
            if (Id_Permissao != 0) {
                requestPM = PageMethods._staticInstance.PopularGrupoCorretoresRonda(
                    Id_Permissao,
                    function (r) {
                        //console.log(r.codigo + ' ' + parseInt($(obj).val()));
                        if (r.codigo == parseInt($(obj).val())) {
                            //console.log(r.codigo + ' == ' + parseInt($(obj).val()));
                            if (r.erro == false) {
                                $.each(r.lista, function (key, value) {
                                    options += "<option value='" + value.idGrupoColetores + "'>" + value.desGrupoColetores + "</option>";
                                });
                            } else {
                                alert(r.mensagem);
                            }

                            $("#lstGrupoColetores").html(options);
                        }
                    },
                    function (r) {
                        var executor1 = requestPM.get_executor();
                        if (executor1.get_aborted() == false) {
                            alert("Tente novamente!");
                        }
                        $("#lstGrupoColetores").html("");
                        //console.log("Abortado: " + executor1.get_aborted());
                    }
                );
            }
        }

    </script>
    <style type="text/css">
        .dtg
        {
        }
        .style3
        {
            font-size: 11px;
            font-family: Tahoma, Verdana, sans-serif;
            color: #154E7A;
            text-align: left;
            font-weight: bold;
            width: 300px;
            padding-left: 0px;
            padding-right: 2px;
            padding-top: 3px;
            padding-bottom: 3px;
        }
        .style4
        {
            width: 299px;
        }
    </style>
</head>
<body>
    <form id="form" method="post" runat="server">
    <asp:ScriptManager ID="ScriptManager" runat="server" EnablePageMethods="true">
    </asp:ScriptManager>
    <table cellspacing="0" cellpadding="0" width="778" align="center" border="0">
        <!-- ********************* START: CABEÇALHO ****************** -->
        <tr>
            <td>
                <cc1:FWServerControl ID="FWServerControlCabecalho" runat="server" Controle="Pagebanner"
                    Arquivo="/Modulos/Framework/CabecalhoBrinks.ascx"></cc1:FWServerControl>
            </td>
        </tr>
        <!-- ********************* END: CABEÇALHO ****************** -->
        <!-- ********************* START: CONTEÚDO ****************** -->
        <tr>
            <td class="backbox" valign="top" height="250">
                <!-- ***************** START: BARRA DE TITUTO ******************* -->
                <table class="MarginTopBarra" cellspacing="0" cellpadding="0" width="760" align="center"
                    border="0">
                    <tr>
                        <td class="cadBarraTitulo" align="left" height="25">
                            <img src="../../Imagens/comum/bulletbarra.gif" align="middle" />
                            Atribuir Permissão
                        </td>
                    </tr>
                    <!-- ***************** END: BARRA DE TITUTO ******************* -->
                    <tr>
                        <td class="backboxconteudo" valign="top">
                            <table class="MarginTopConteudo" cellspacing="0" cellpadding="0" align="center" border="0">
                                <tbody>
                                    <tr>
                                        <td class="cadlbl">
                                            <%--<asp:Label ID="lblDadosVisita" runat="server" Text="Dados da Permissão de Crachá Titular"></asp:Label>--%>
                                            <span id="Span1">Dados da Permissão de Crachá Titular</span>
                                            <table style="border: 1px solid black" width="747">
                                                <tbody>
                                                    <tr>
                                                        <td class="cadlbl" width="300" colspan="3">
                                                            <asp:Label ID="lblCodSolic" runat="server" Text="Código da Solicitação:"></asp:Label>
                                                        </td>
                                                        <td width="7">
                                                        </td>
                                                        <td class="style3">
                                                            <asp:Label ID="lblDataInicio" runat="server" Text="Data da Solicitação:"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3">
                                                            <asp:TextBox ID="txtCodSolic" runat="server" CssClass="cadtxt" Width="100px" Enabled="False"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td class="style4">
                                                            <asp:TextBox ID="txtDataInicio" runat="server" CssClass="cadtxt" Width="110px" Enabled="False"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="cadlbl" width="300" colspan="3">
                                                            <asp:Label ID="lblNomeVisitado" runat="server" Text="Nome do Colaborador:"></asp:Label>
                                                        </td>
                                                        <td width="7">
                                                        </td>
                                                        <td class="style3">
                                                            <asp:Label ID="lblRE" runat="server" Text="RE:"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3">
                                                            <asp:TextBox ID="txtNomeVisitado" runat="server" CssClass="cadtxt" Width="350px"
                                                                Enabled="False"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td class="style4">
                                                            <asp:TextBox ID="txtRE" runat="server" CssClass="cadtxt" MaxLength="50" Width="110px"
                                                                Enabled="False"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="cadlbl" colspan="3">
                                                            <asp:Label ID="lblFilial" runat="server" Text="Filial:"></asp:Label>
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td class="style3">
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3">
                                                            <asp:TextBox ID="txtFilial" runat="server" CssClass="cadtxt" Enabled="False" MaxLength="50"
                                                                Width="200px"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td class="style4">
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="cadlbl" colspan="3">
                                                            <asp:Label ID="lblAreaVisitada" runat="server" Text="Áreas Visitadas:"></asp:Label>
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td class="style3">
                                                            <asp:Label ID="lblObservacao" runat="server" Text="Motivo da Solicitação:"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3">
                                                            <asp:ListBox ID="lstAreaVisita" runat="server" CssClass="cadlstBox" Height="80px"
                                                                Width="200px"></asp:ListBox>
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td class="style4">
                                                            <asp:TextBox ID="txtObservacao" runat="server" CssClass="cadlstBox" MaxLength="50"
                                                                Width="200px" Enabled="False" Height="80px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="cadlbl">
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td class="cadlbl">
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td class="style3">
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="cadlbl">
                                            <br />
                                            <asp:Label ID="lblHistoricoSolicitacao" runat="server" Text="Histórico da Solicitação"></asp:Label>
                                            <table style="border: 1px solid black" width="747">
                                                <tbody>
                                                    <tr>
                                                        <td align="center">
                                                            <!-- ********************* STAR: RADGRID ************************** -->
                                                            <rad:RadGrid ID="radSolicitacoes" runat="server" AllowPaging="True" AllowSorting="True"
                                                                AutoGenerateColumns="False" SortingSettings-SortToolTip="Clique para filtrar"
                                                                CssClass="dtg" GridLines="None" Skin="None" Width="740px" OnItemCommand="radSolicitacoes_ItemCommand"
                                                                OnNeedDataSource="radSolicitacoes_NeedDataSource" Height="80px">
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
                                                                        <rad:GridBoundColumn DataField="TIPOPESSOA" SortExpression="TIPOPESSOA" HeaderText="Tipo Pessoa"
                                                                            UniqueName="TIPOPESSOA">
                                                                            <ItemStyle HorizontalAlign="Center" Width="20%" />
                                                                        </rad:GridBoundColumn>
                                                                        <rad:GridBoundColumn DataField="NOME" SortExpression="NOME" HeaderText="Nome" UniqueName="NOME">
                                                                            <ItemStyle HorizontalAlign="Center" Width="35%" />
                                                                        </rad:GridBoundColumn>
                                                                        <rad:GridBoundColumn DataField="STATUS" SortExpression="STATUS" HeaderText="Status"
                                                                            UniqueName="STATUS">
                                                                            <ItemStyle HorizontalAlign="Center" Width="20%" />
                                                                        </rad:GridBoundColumn>
                                                                        <rad:GridBoundColumn DataField="DATA" SortExpression="DATA" HeaderText="Data Aprovação/Criação"
                                                                            UniqueName="DATA" DataFormatString="{0:dd/MM/yyyy HH:mm}">
                                                                            <ItemStyle HorizontalAlign="Center" Width="25%" />
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
                                                    <tr>
                                                        <td align="left">
                                                            <br />
                                                            <asp:Label ID="lblObservacaoReprovacao" runat="server" Text="Observação:" CssClass="cadlbl"
                                                                Visible="false"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtObservacaoReprovacao" CssClass="cadlstBox" runat="server" Enabled="false"
                                                                TextMode="multiline" Width="740px" Height="50px" Visible="false"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="cadlbl" style="height: 100px" bgcolor="White">
                                            <asp:Label ID="lblManutencao" runat="server" Text="Manutenção das Permissões"></asp:Label>
                                            <table style="border: 1px solid black" width="747">
                                                <tbody>
                                                    <!-- ****************** END: PAINEL DE CADASTRO ************************** -->
                                                    <tr>
                                                        <td class="cadlbl">
                                                            <asp:Label ID="lblPermissao" runat="server" Text="Permissões "></asp:Label>
                                                            <asp:Image ID="imgAdcPermissao" runat="server" ImageUrl="~/Imagens/icones/ico_btn_adiconar.png" />
                                                        </td>
                                                        <td class="cadlbl" style="padding-left: 84px;">
                                                            <asp:Label ID="lblGrupoColetores" runat="server" Text="Grupo de Coletores"></asp:Label>
                                                        </td>
                                                        <td class="cadlbl" style="padding-left: 84px;">
                                                            <asp:Label ID="lblPermissaoNaoUtilizada" runat="server" Text="Permissões não utilizadas"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 188px; text-align: right;">
                                                            <asp:ListBox ID="lstPermissao" runat="server" CssClass="cadlstBox" Height="178px"
                                                                Width="188px"></asp:ListBox>
                                                            <br />
                                                            <input type="button" id="btnAtribuir" class="cadbutton80" style="width: 80px; margin-top: 6px;"
                                                                value="Atribuir" />
                                                        </td>
                                                        <td style="vertical-align: top; width: 188px; padding-left: 85px;">
                                                            <asp:ListBox ID="lstGrupoColetores" runat="server" CssClass="cadlstBox" Height="178px"
                                                                Width="188px"></asp:ListBox>
                                                        </td>
                                                        <td style="vertical-align: top; padding-left: 84px;">
                                                            <asp:ListBox ID="lstPermissaoNaoUtilizadas" runat="server" CssClass="cadlstBox" Height="178px"
                                                                Width="188px"></asp:ListBox>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="cadlbl" style="height: 100px" bgcolor="White">
                                            <asp:Label ID="lblPermissaoSelecionda" runat="server" Text="Permissão Selecionada "></asp:Label>
                                            <table style="border: 1px solid black" width="747">
                                                <tbody>
                                                    <tr>
                                                        <td class="cadlbl">
                                                            <span>Permissão</span>
                                                        </td>
                                                        <td class="cadlbl">
                                                            <span>Grupo de coletores</span>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 272px; vertical-align: top;">
                                                            <asp:TextBox ID="txtDigPermissaoSelecionada" runat="server" CssClass="cadtxt" Enabled="False"
                                                                MaxLength="50" Width="49px"></asp:TextBox>
                                                            <asp:TextBox ID="txtPermissaoSelecionada" runat="server" CssClass="cadtxt" Enabled="False"
                                                                MaxLength="50" Width="123px"></asp:TextBox>
                                                        </td>
                                                        <td colspan="3" bgcolor="White">
                                                            <asp:ListBox ID="lstGruposAtributos" runat="server" CssClass="cadlstBox" Height="86px"
                                                                Width="188px"></asp:ListBox>
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
                            <table align="center" border="0" cellpadding="0" cellspacing="0" width="760">
                                <tbody>
                                    <tr>
                                        <td align="right" class="backBarraBotoes">
                                            <input type="button" id="btnVoltar" class="cadbutton80" value="Voltar" />
                                            <input type="button" id="btnSalvar" class="cadbutton80" width="80px" value="Salvar" />
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </td>
                    </tr>
                </table>
                <!-- ****************** END: PAINEL AJAX ************************** -->
                <rada:AjaxLoadingPanel ID="lpaCadastro" Width="75px" runat="server" HorizontalAlign="Center"
                    Transparency="30" Height="75px">
                    <asp:Image ID="Image1" Style="margin-top: 100px" runat="server" ImageUrl="%%PATH%%/RadControls/Ajax/Skins/Default/Loading.gif"
                        AlternateText="Aguarde ..."></asp:Image>
                </rada:AjaxLoadingPanel>
            </td>
        </tr>
        <!-- ********************* END: CONTEÚDO ****************** -->
    </table>
    <!-- ********************* START: RODAPÉ ************************** -->
    <cc1:FWServerControl ID="FWServerControlRodape" runat="server" Controle="Rodape"
        Arquivo="/Modulos/Framework/RodapeBrinks.ascx"></cc1:FWServerControl>
    <!-- ********************* END: RODAPÉ ************************** -->
    </form>
</body>
</html>
