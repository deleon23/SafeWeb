<%@ Page Language="C#" AutoEventWireup="true" Codebehind="cadSaida.aspx.cs" 
    Inherits="SafWeb.UI.Modulos.Portaria.cadSaida" %>

<%@ Register TagPrefix="rad" Namespace="Telerik.WebControls" Assembly="RadGrid.Net2" %>
<%@ Register TagPrefix="cc1" Namespace="FrameWork.WebControl" Assembly="FrameWork.WebControl" %>
<%@ Register TagPrefix="rada" Namespace="Telerik.WebControls" Assembly="RadAjax.Net2" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>%%SITE%%</title>
    <script src="../../Scripts/jquery-1.7.2.js" type="text/javascript"></script>
    <link href="../../Estilos/FrameWork.css" type="text/css" rel="stylesheet"/>
    <link href="../../Estilos/Brinks.css" type="text/css" rel="stylesheet"/>

    <script language="javascript" type="text/javascript">
        function GetRadWindow() {
            var oWindow = null;
            if (window.radWindow) oWindow = window.radWindow;
            else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;

            return oWindow;

        }

        //Fecha a RadWindow
        function CloseWin() {
            //Get the RadWindow   
            var oWindow = GetRadWindow();
            //oWindow.BrowserWindow.location.reload(); 
            //Call its Close() method   
            oWindow.Close();
        }

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

        function FormataPlaca(e, obj) {
            //usar no evento keypress
            //bloqueia caracteres alfa e coloca as barras nas posições        


            var key = window.event ? e.keyCode : e.which;
            var keychar = String.fromCharCode(key);
            var vrRetorno = false;

            //Backspace e Tab
            if (key != 8 && key != 0) {
                if ((obj.value.length >= 0 && obj.value.length <= 2) && key != 8) {
                    goodChars = "ABCDEFGHIJKLMNOPQRSTUVWYXZabcdefghijklmnopqrstuvwyxz";
                }
                else {
                    goodChars = "0123456789";
                }

                if (goodChars.indexOf(keychar) != -1) {
                    vrRetorno = true;
                }
            }
            else {
                vrRetorno = true;
            }

            return vrRetorno;
        }

        function AbrirHelpCad() {
            window.open("HelpSaidaCadastro.aspx", "JANELA", "height = 400, width = 530");
        }

        function AbrirHelpList() {
            window.open("HelpSaidaListagem.aspx", "JANELA", "height = 400, width = 530");
        }

        function PreencherHiddenColaboradores(colaboradores) {
            document.getElementById("txtHiddenColaboradores").value = colaboradores;

            AjaxNS.AR('Colaboradores', '', 'RadAjaxPanel1');
        }

        //////////////////////////////////////////////////////////////////////
        // Segunda Aba

        var allCheckBoxSelector = '#<%=radGridColaborador.ClientID%> input[id*="headerChkbox"]:checkbox';
        var checkBoxSelector = '#<%=radGridColaborador.ClientID%> input[id*="chkActive"]:checkbox';

        function ToggleCheckUncheckAllOptionAsNeeded() {
            var totalCheckboxes = $(checkBoxSelector),
             checkedCheckboxes = totalCheckboxes.filter(":checked"),
             noCheckboxesAreChecked = (checkedCheckboxes.length === 0),
             allCheckboxesAreChecked = (totalCheckboxes.length === checkedCheckboxes.length);

            $(allCheckBoxSelector).attr('checked', allCheckboxesAreChecked);
        }

        $(document).ready(function () {

            $(allCheckBoxSelector).live('click', function () {
                $(checkBoxSelector).attr('checked', $(this).is(':checked'));

                ToggleCheckUncheckAllOptionAsNeeded();
            });

            $(checkBoxSelector).live('click', ToggleCheckUncheckAllOptionAsNeeded);

            ToggleCheckUncheckAllOptionAsNeeded();
            
            $("#<%=ddlEscalaDep.ClientID%>").live('change', function () {
                    $("#hdEscalaDep").val($("#<%=ddlEscalaDep.ClientID%>").val());
            });
        });

        
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
                        <asp:HiddenField ID="txtHiddenColaboradores" runat="server" />
                        <asp:Panel ID="pnlErro" runat="server">
                        </asp:Panel>
                        <asp:Panel ID="pnlColaborador" runat="server">
                        </asp:Panel>
                        <!-- ****************** START: PAINEL DE LISTAGEM ************************** -->
                        <asp:Panel runat="server" ID="pnlListagem">
                            <!-- ***************** START: BARRA DE TITUTO ******************* -->
                            <table class="MarginTopBarra" cellspacing="0" cellpadding="0" width="760" align="center"
                                border="0">
                                <tr>
                                    <td class="cadBarraTitulo" align="left" height="25" style="width: 750px">
                                        <img src="../../Imagens/comum/bulletbarra.gif" align="middle" />
                                        <asp:Label ID="lblListagem" runat="server" Text="Listagem de Visitantes Agendados - Saída"></asp:Label>
                                    </td>
                                    <td class="cadBarraTitulo" align="right" height="25" style="width: 9px">
                                        <asp:ImageButton ID="btnHelp" runat="server" AlternateText="Ajuda" ImageUrl="%%PATH%%/imagens/icones/ico_help.jpg"
                                            ImageAlign="AbsMiddle" Visible="False" />
                                    </td>
                                </tr>
                                <!-- ***************** END: BARRA DE TITUTO ******************* -->
                                <tr>
                                    <td class="backboxconteudo" valign="top" height="250" style="width: 759px">
                                        <!-- ****************** START: FILTROS ************************** -->
                                        <table class="MarginTopConteudo" cellspacing="0" cellpadding="0" width="740" align="center"
                                            border="0">
                                            <tr>
                                                <td>
                                                    <table cellspacing="0" cellpadding="0" width="740" align="center" border="0">
                                                        <tr>
                                                            <td>
                                                                <table cellspacing="0" cellpadding="0" align="left" border="0">
                                                                    <tr>
                                                                        <td>
                                                                                <br />
                                                                            </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td width="50%">
                                                                            <asp:Button ID="btnAbaVisitantes" runat="server" CssClass="cadbuttonAbaAtiva" Width="100%"
                                                                                CausesValidation="False" Text="Visitantes Agendados" OnClick="btnAbaVisitantes_Click">
                                                                            </asp:Button></td>
                                                                        <td>
                                                                            <asp:Image ID="imgAbaVisitantes" runat="server" ImageUrl="%%PATH%%/Imagens/right-abaAtiva.gif">
                                                                            </asp:Image></td>
                                                                        <td width="50%">
                                                                            <asp:Button ID="btnAbaColaboradores" Text="Escala de Colaboradores" runat="server"
                                                                                CssClass="cadbuttonAbaInativa" Width="100%" CausesValidation="False" OnClick="btnAbaColaboradores_Click">
                                                                            </asp:Button></td>
                                                                        <td>
                                                                            <asp:Image ID="imgAbaColaboradores" runat="server" ImageUrl="%%PATH%%/Imagens/right-abaInativa.gif">
                                                                            </asp:Image></td>

                                                                        <td width="50%">
                                                                        </td>
                                                                        <td>
                                                                        </td>

                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <!-- ****************** START: PRIMEIRA ABA ************************** -->
                                                                <div id="divAbaVisitantes" style="border-right: #d6d6d6 1px solid; border-left: #d6d6d6 1px solid;
                                                                    width: 760px; border-bottom: #d6d6d6 1px solid; border-right: #d6d6d6 1px solid;
                                                                    background-color: #ecf1f7" align="center" runat="server">
                                                                    <asp:Panel runat="server" ID="pnlListagemVis">
                                                                        <asp:ImageButton ID="btnHelpList" runat="server" AlternateText="Ajuda" ImageUrl="%%PATH%%/imagens/icones/ico_help.jpg"
                                                                            ImageAlign="AbsMiddle" Visible="False" />
                                                                        <!-- ****************** START: FILTROS ************************** -->
                                                                        <table class="MarginTopConteudo" cellspacing="0" cellpadding="0" width="740px" align="center"
                                                                            border="0">
                                                                            <tr>
                                                                                <td align="center">
                                                                                    <table style="border-right: #cccccc 1px solid; border-top: #cccccc 1px solid; padding: 5px;
                                                                                        border-left: #cccccc 1px solid; padding-top: 5px; border-bottom: #cccccc 1px solid"
                                                                                        cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <br />
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="cadlbl" width="245" colspan="3">
                                                                                                    <asp:Label ID="lblNomeVisitante" runat="server" Text="Nome Visitante:"></asp:Label>
                                                                                                </td>
                                                                                                <td width="5">
                                                                                                </td>
                                                                                                <td class="cadlbl" width="150">
                                                                                                    &nbsp;<asp:Label ID="Label1" runat="server" Text="Crachá:"></asp:Label></td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td colspan="3">
                                                                                                    <asp:TextBox ID="txtNomeVisitante" runat="server" CssClass="cadtxt" MaxLength="100"
                                                                                                        Width="385px"></asp:TextBox>
                                                                                                </td>
                                                                                                <td>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:TextBox ID="txtCracha" runat="server" CssClass="cadtxt" MaxLength="12" EnableTheming="False"></asp:TextBox>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="cadlbl" width="245">
                                                                                                    &nbsp;<asp:Label ID="lblPlaca" runat="server" Text="Placa:"></asp:Label></td>
                                                                                                <td width="5">
                                                                                                </td>
                                                                                                <td class="cadlbl" width="150">
                                                                                                    <asp:Label ID="lblTipoDocumento" runat="server" Text="Tipo Documento:"></asp:Label></td>
                                                                                                <td width="5">
                                                                                                </td>
                                                                                                <td class="cadlbl" width="150">
                                                                                                    &nbsp;<asp:Label ID="lblDocumento" runat="server" Text="Documento:"></asp:Label></td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td style="height: 21px">
                                                                                                    <asp:TextBox ID="txtPlaca" runat="server" CssClass="cadtxt" MaxLength="7"></asp:TextBox></td>
                                                                                                <td style="height: 21px">
                                                                                                </td>
                                                                                                <td style="height: 21px">
                                                                                                    <asp:DropDownList ID="ddlTipoDocumento" CssClass="cadddl" runat="server" AutoPostBack="True"
                                                                                                        OnSelectedIndexChanged="ddlTipoDocumento_SelectedIndexChanged">
                                                                                                    </asp:DropDownList></td>
                                                                                                <td style="height: 21px">
                                                                                                </td>
                                                                                                <td style="height: 21px">
                                                                                                    <cc1:FWMascara ID="txtDocumento" runat="server" AutoPostBack="True" CssClass="cadtxt"
                                                                                                        Enabled="False" Mascara="NENHUMA" MaxLength="10" Width="150px"></cc1:FWMascara>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td colspan="5" align="center">
                                                                                                    <br />
                                                                                                    <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="cadbuttonfiltro"
                                                                                                        OnClick="btnBuscar_Click" />
                                                                                                    <br />
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                        <!-- ********************** END: FILTROS ************************** -->
                                                                                    <table class="MarginTopConteudo" cellspacing="0" cellpadding="0" width="740" align="center"
                                                                            border="0">
                                                                            <tr>
                                                                                <td align="center" colspan="3">
                                                                                    <!-- ********************* STAR: RADGRID ************************** -->
                                                                                    <rad:RadGrid ID="radSaida" runat="server" AllowPaging="True" AllowSorting="True"
                                                                                        AutoGenerateColumns="False" CssClass="dtg" GridLines="None" Skin="None" Width="740px"
                                                                                        OnItemCommand="radSaida_ItemCommand" OnItemDataBound="radSaida_ItemDataBound"
                                                                                        OnNeedDataSource="radSaida_NeedDataSource">
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
                                                                                                <rad:GridBoundColumn DataField="NumCracha" SortExpression="NumCracha" HeaderText="Crachá">
                                                                                                    <HeaderStyle Width="15%" HorizontalAlign="Center"></HeaderStyle>
                                                                                                </rad:GridBoundColumn>
                                                                                                <rad:GridBoundColumn DataField="Placa" SortExpression="Placa" HeaderText="Veículo">
                                                                                                    <HeaderStyle Width="10%" HorizontalAlign="Center"></HeaderStyle>
                                                                                                </rad:GridBoundColumn>
                                                                                                <rad:GridBoundColumn DataField="CodAcessoVisitante" SortExpression="CodAcessoVisitante"
                                                                                                    HeaderText="CodAcessoVisitante" Visible="false">
                                                                                                </rad:GridBoundColumn>
                                                                                                <rad:GridBoundColumn DataField="NomVisitante" SortExpression="NomVisitante" HeaderText="Nome Visitante">
                                                                                                    <HeaderStyle Width="40%" HorizontalAlign="Center"></HeaderStyle>
                                                                                                </rad:GridBoundColumn>
                                                                                                <rad:GridBoundColumn DataField="DesTipoDocumento" SortExpression="DesTipoDocumento"
                                                                                                    HeaderText="Tipo Documento">
                                                                                                    <HeaderStyle Width="10%" HorizontalAlign="Center"></HeaderStyle>
                                                                                                </rad:GridBoundColumn>
                                                                                                <rad:GridBoundColumn DataField="NumDocumento" SortExpression="NumDocumento" HeaderText="Documento">
                                                                                                    <HeaderStyle Width="10%" HorizontalAlign="Center"></HeaderStyle>
                                                                                                </rad:GridBoundColumn>
                                                                                                <rad:GridBoundColumn DataField="DataEntrada" HeaderText="Data/Hora Entrada" SortExpression="DataEntrada"
                                                                                                    UniqueName="DataEntrada" DataFormatString="{0:dd/MM/yyyy HH:mm}">
                                                                                                    <ItemStyle Width="20%" HorizontalAlign="Center"></ItemStyle>
                                                                                                </rad:GridBoundColumn>
                                                                                                <rad:GridTemplateColumn Visible="false">
                                                                                                    <HeaderStyle Width="0%"></HeaderStyle>
                                                                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                                                    <ItemTemplate>
                                                                                                        <asp:ImageButton ID="imgEditar" runat="server" ImageUrl="~/imagens/icones/ico_editar.gif"
                                                                                                            CommandName="Editar" Visible="False"></asp:ImageButton>
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
                                                                        <table align="center" border="0" cellpadding="0" cellspacing="0" width="760">
                                                                            <tr>
                                                                                <td align="right" class="backBarraBotoes">
                                                                                    <asp:Button ID="btnVoltar" runat="server" CausesValidation="False" CssClass="cadbutton100"
                                                                                        Text="Voltar" Width="50px" OnClick="btnVoltar_Click" />
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </asp:Panel>

                                                                    <asp:Panel runat="server" ID="pnlCadastro">
                                                                        <asp:ImageButton ID="btnHelpCad" runat="server" AlternateText="Ajuda" ImageUrl="%%PATH%%/imagens/icones/ico_help.jpg"
                                                                                ImageAlign="AbsMiddle" Visible="False" />
                                                                        <!-- ****************** START: FORMULARIO *********************** -->
                                                                        <br />
                                                                        <table class="MarginTopConteudo" cellspacing="0" cellpadding="0" width="740" align="center"
                                                                            border="0">
                                                                            <tr>
                                                                                <td align="center">
                                                                                    <table style="border-right: #cccccc 1px solid; border-top: #cccccc 1px solid; padding: 5px;
                                                                                    border-left: #cccccc 1px solid; padding-top: 5px; border-bottom: #cccccc 1px solid"
                                                                                    cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                                    <tr>
                                                                                        <td colspan="3" class="cadmsg">
                                                                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" />
                                                                                            <asp:Label ID="lblMensagem" runat="server" Text="Label" Visible="false" CssClass="cadlbl"></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="cadmsg" style="width: 726px; height: 28px; text-align: center;" align="center"
                                                                                            colspan="9">
                                                                                                <br />
                                                                                            </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="cadlbl" width="150">
                                                                                            <asp:RadioButton ID="rdbCracha" Text="Crachá" CssClass="cadchk" runat="server" Checked="True"
                                                                                                GroupName="Saida" OnCheckedChanged="rdbCracha_CheckedChanged" AutoPostBack="True" />
                                                                                            <asp:RadioButton ID="rdbPlaca" Text="Placa" CssClass="cadchk" runat="server" GroupName="Saida"
                                                                                                OnCheckedChanged="rdbPlaca_CheckedChanged" AutoPostBack="True" />
                                                                                        </td>
                                                                                        <td width="5">
                                                                                        </td>
                                                                                        <td class="cadlbl" width="350">
                                                                                            <asp:Label ID="lblDataSaida" runat="server" Text="Data/Hora Saída"></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="cadlbl">
                                                                                            &nbsp;<cc1:FWMascara ID="txtCrachaPlaca" runat="server" CssClass="cadtxt" Mascara="NUMERO"
                                                                                                MaxLength="12" Width="99px"></cc1:FWMascara>
                                                                                            <asp:ImageButton ID="btnListar" runat="server" AlternateText="Buscar" ImageUrl="~/Imagens/icones/ico_Visualizar.gif"
                                                                                                OnClick="btnListar_Click" />
                                                                                        </td>
                                                                                        <td>
                                                                                        </td>
                                                                                        <td class="cadlbl">
                                                                                            <asp:TextBox ID="txtDataSaida" runat="server" CssClass="cadtxt" Width="115px" MaxLength="16"></asp:TextBox>
                                                                                            <a hidefocus onclick="if(self.gfPop)gfPop.fPopCalendar(document.forms[0].all('<% = this.txtDataSaida.ClientID.ToString() %>'));return false;"
                                                                                                href="javascript:void(0)">
                                                                                                <img height="19" alt="" src="../../Agenda/calbtn.gif" width="20" align="absMiddle"
                                                                                                    border="0" name="popcal"></a>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td><br /></td>
                                                                                    </tr>
                                                                                </table>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                        <br /><br /><br /><br /><br /><br />
                                                                        <table align="center" border="0" cellpadding="0" cellspacing="0" width="760">
                                                                            <tr>
                                                                                <td align="right" class="backBarraBotoes" style="height: 30px; width: 759px;">
                                                                                    <asp:Button ID="btnBaixar" runat="server" CausesValidation="False" CssClass="cadbutton100"
                                                                                        Text="Baixar" Width="50px" OnClick="btnBaixar_Click" />
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </asp:Panel>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <!-- ****************** START: SEGUNDA ABA ************************** -->
                                                                <div id="divAbaColaboradores" style="border-right: #d6d6d6 1px solid; border-left: #d6d6d6 1px solid;
                                                                    width: 760px; border-bottom: #d6d6d6 1px solid; border-right: #d6d6d6 1px solid;
                                                                    background-color: #ecf1f7" align="center" runat="server">
                                                                    
                                                                    <table class="MarginTopConteudo" cellspacing="0" cellpadding="0" width="720" align="center"
                                                                        border="0">
                                                                        <tr>
                                                                            <td>
                                                                                <table style="border-right: #cccccc 1px solid; border-top: #cccccc 1px solid; padding: 5px;
                                                                                    border-left: #cccccc 1px solid; padding-top: 5px; border-bottom: #cccccc 1px solid"
                                                                                    cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                                    <tr>
                                                                                        <td class="cadmsg" style="width: 726px; height: 28px; text-align: center;" align="center"
                                                                                            colspan="9">
                                                                                            <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List" />
                                                                                            <asp:Label ID="lblMensagemEscalacao" runat="server" CssClass="cadalert"></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="cadlbl" width="240" colspan="3">
                                                                                            <asp:Label ID="lblNumeroDocumento" runat="server" Text="Número Documento:"></asp:Label></td>
                                                                                        <td style="width: 5px;">
                                                                                        </td>
                                                                                        <td class="cadlbl" width="240">
                                                                                            <asp:Label ID="lblFilialColaborador" runat="server" Text="Filial:"></asp:Label></td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="height: 20px" colspan="3">
                                                                                            <cc1:FWMascara ID="txtNumeroDocumento" runat="server" Width="99px" CssClass="cadtxt"
                                                                                                MaxLength="30" Mascara="NUMERO"></cc1:FWMascara>
                                                                                        </td>
                                                                                        <td style="height: 20px; width: 5px;">
                                                                                        </td>
                                                                                        <td style="height: 20px">
                                                                                            <cc1:FWMascara ID="txtFilialColaborador" runat="server" Width="99px" CssClass="cadtxt"
                                                                                                MaxLength="10" Mascara="NUMERO" Enabled="False"></cc1:FWMascara>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="cadlbl" colspan="3">
                                                                                            <asp:Label ID="lblNomeColaborador" runat="server" Text="Nome Colaborador:"></asp:Label></td>
                                                                                        <td style="height: 20px; width: 5px;">
                                                                                        </td>
                                                                                        <td class="cadlbl">
                                                                                            <asp:Label ID="lblEscalaColaborador" runat="server" Text="Número de Escala:"></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td colspan="3">
                                                                                            <asp:TextBox ID="txtNomeColaborador" runat="server" Width="370px" CssClass="cadtxt"
                                                                                                MaxLength="50"></asp:TextBox></td>
                                                                                        <td style="width: 5px; height: 20px;">
                                                                                        </td>
                                                                                        <td>
                                                                                            <cc1:FWMascara ID="txtEscalaColaborador" runat="server" Width="99px" CssClass="cadtxt"
                                                                                                MaxLength="10" Mascara="NUMERO"></cc1:FWMascara></td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="width: 15%;" class="cadlbl">
                                                                                            <asp:Label ID="lblHoraInicio" runat="server" Text="Hora Início:"></asp:Label></td>
                                                                                        <td class="cadlbl" colspan="2" style="width: 15%;">
                                                                                            <asp:Label ID="lblHoraFim" runat="server" Text="Hora Fim:"></asp:Label>
                                                                                        </td>
                                                                                        <td>
                                                                                        </td>
                                                                                        <td class="cadlbl">
                                                                                            <asp:Label ID="lblEscalaDep" runat="server" Text="Escala Departamental:"></asp:Label></td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="height: 20px;">
                                                                                            <cc1:FWMascara ID="txtHoraInicio" runat="server" Width="99px" CssClass="cadtxt" MaxLength="10"
                                                                                                Mascara="HORA"></cc1:FWMascara><asp:RegularExpressionValidator ID="RegularExpressionValidator1"
                                                                                                    runat="server" ErrorMessage="- Campo Hora Início: Formato Inválido" ControlToValidate="txtHoraInicio"
                                                                                                    ValidationExpression="^([0-1][0-9]|[2][0-3]):[0-5][0-9]$">*</asp:RegularExpressionValidator>
                                                                                        </td>
                                                                                        <td colspan="2" style="height: 20px;">
                                                                                            <cc1:FWMascara ID="txtHoraFim" runat="server" Width="99px" CssClass="cadtxt" MaxLength="10"
                                                                                                Mascara="HORA"></cc1:FWMascara>
                                                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtHoraFim"
                                                                                                ErrorMessage="- Campo Hora Fim: Formato Inválido" ValidationExpression="^([0-1][0-9]|[2][0-3]):[0-5][0-9]$">*</asp:RegularExpressionValidator></td>
                                                                                        <td style="width: 5px">
                                                                                        </td>
                                                                                        <td style="height: 20px">
                                                                                        <asp:TextBox ID="hdEscalaDep" runat="server" CssClass="hiddencol" /><!-- Mantem o Selected Value da ddlEscalaDep quando abre a janela para confirmar os funcionários -->
                                                                                        <asp:DropDownList ID="ddlEscalaDep" AutoPostBack="False" CssClass="cadddl" runat="server"
                                                                                                Width="120px" EnableViewState="true">
                                                                                            </asp:DropDownList>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td align="center" colspan="5" style="height: 43px">
                                                                                            <br />
                                                                                            <asp:Button ID="btnBuscarCol" OnClick="btnBuscarCol_Click" runat="server" Text="Buscar"
                                                                                                CssClass="cadbuttonfiltro"></asp:Button>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                                <table class="MarginTopConteudo" cellspacing="0" cellpadding="0" width="740" align="center"
                                                                                    border="0">
                                                                                    <tr>
                                                                                        <td align="center" colspan="3">
                                                                                            <br />
                                                                                            <!-- ********************* STAR: RADGRID ************************** -->
                                                                                            <rad:RadGrid ID="radGridColaborador" runat="server" Width="100%" CssClass="dtg" Skin="None"
                                                                                                PageSize="50" GridLines="None" AutoGenerateColumns="False" AllowSorting="True" AllowPaging="True" 
                                                                                                SortingSettings-SortToolTip="Clique para filtrar" DataSourceID="ObjectDataSource1" 
                                                                                                OnItemCommand="radGridColaborador_ItemCommand" OnItemDataBound="radGridColaborador_ItemDataBound">
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
                                                                                                            <table cellpadding="0" cellspacing="0" width="100%">
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
        
                                                                                                        <rad:GridTemplateColumn UniqueName="CheckAcesso">
                                                                                                            <HeaderTemplate>
                                                                                                                <asp:CheckBox ID="headerChkbox" runat="server"></asp:CheckBox>
                                                                                                            </HeaderTemplate>
                                                                                                            <ItemTemplate>
                                                                                                                <asp:CheckBox ID="chkActive" runat="server"></asp:CheckBox>
                                                                                                            </ItemTemplate>
                                                                                                            <HeaderStyle HorizontalAlign="Center" Width="5px" />
                                                                                                            <ItemStyle HorizontalAlign="Center" Width="5px" />
                                                                                                        </rad:GridTemplateColumn>

                                                                                                        <rad:GridButtonColumn CommandName="RegistrarAcesso" UniqueName="RegistrarAcesso" ItemStyle-Height="35px"
                                                                                                            ButtonType="ImageButton" HeaderText="Registrar Saída" ImageUrl="%%PATH%%/imagens/icones/ico_Ativo.gif">
                                                                                                            <ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
                                                                                                        </rad:GridButtonColumn>
                                                                                                        <rad:GridBoundColumn DataField="HoraEscalacao" HeaderText="Hora Entrada" SortExpression="HORA_ESCALACAO"
                                                                                                            UniqueName="HoraEscalacao">
                                                                                                            <ItemStyle Width="10%"></ItemStyle>
                                                                                                        </rad:GridBoundColumn>
                                                                                                        <rad:GridTemplateColumn UniqueName="TemplateColumn" HeaderText="Último Acesso" SortExpression="DT_ESCALACAO">     
                                                                                                            <HeaderStyle Width="30%"></HeaderStyle>
                                                                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                                                            <ItemTemplate>
                                                                                                                <asp:Label ID="lblUltimoAcesso" runat="server"></asp:Label>
                                                                                                            </ItemTemplate>
                                                                                                        </rad:GridTemplateColumn> 
                                                                                                        <rad:GridBoundColumn DataField="NomeColaborador" HeaderText="Nome do Colaborador" HeaderStyle-CssClass="dtgHeaderStyle"
                                                                                                            SortExpression="NOM_COLABORADOR" UniqueName="NomeColaborador">
                                                                                                            <ItemStyle  Width="40%"></ItemStyle>
                                                                                                        </rad:GridBoundColumn>
                                                                                                        <rad:GridBoundColumn DataField="CodigoColaborador" HeaderText="RE" SortExpression="COD_COLABORADOR"
                                                                                                            UniqueName="CodigoColaborador">
                                                                                                            <ItemStyle Width="30%" HorizontalAlign="Center"></ItemStyle>
                                                                                                        </rad:GridBoundColumn>
                                                                                                        <rad:GridBoundColumn DataField="CodigoEscalacao" HeaderText="Nº da Escala" SortExpression="ID_ESCALACAO"
                                                                                                            UniqueName="CodigoEscalacao">
                                                                                                            <ItemStyle Width="30%" HorizontalAlign="Center"></ItemStyle>
                                                                                                        </rad:GridBoundColumn>
                                                                                                        <rad:GridBoundColumn DataField="DescricaoEscalaDepto" HeaderText="Escala Departamental"
                                                                                                            SortExpression="DES_ESCALADPTO" UniqueName="DescricaoEscalaDepto">
                                                                                                            <ItemStyle Width="30%" HorizontalAlign="Center"></ItemStyle>
                                                                                                        </rad:GridBoundColumn>
                                                                                                        <rad:GridBoundColumn DataField="FlgStatusAcesso" HeaderText="" SortExpression="FLG_STATUSACESSO"
                                                                                                            UniqueName="FlgStatusAcesso" Visible="false">
                                                                                                        </rad:GridBoundColumn>

                                                                                                        <rad:GridBoundColumn DataField="DataEntrada" HeaderText="" SortExpression="DT_ENTRADA"
                                                                                                            UniqueName="DataEntrada" Visible="false">
                                                                                                        </rad:GridBoundColumn>
                                                                                                        <rad:GridBoundColumn DataField="DataSaida" HeaderText="" SortExpression="DT_SAIDA"
                                                                                                            UniqueName="DataSaida" Visible="false">
                                                                                                        </rad:GridBoundColumn>

                                                                                                        <rad:GridTemplateColumn UniqueName="TemplateColumn" Visible="False">
                                                                                                            <ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
                                                                                                            <ItemTemplate>
                                                                                                                <asp:ImageButton ID="btnEditar" runat="server" AlternateText="Editar" CausesValidation="False"
                                                                                                                    CommandArgument='<%# DataBinder.Eval(Container.DataItem, "CodigoFilial") %>'
                                                                                                                    CommandName="Editar" ImageUrl="%%PATH%%/imagens/icones/ico_editar.gif" ToolTip="Editar">
                                                                                                                </asp:ImageButton>
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
                                                                                            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
                                                                                                SelectMethod="GetColaboradoresDentro" 
                                                                                                EnablePaging="true"
                                                                                                TypeName="SafWeb.UI.Modulos.Portaria.ColaboradoresData" 
                                                                                                StartRowIndexParameterName="startIndex" 
                                                                                                MaximumRowsParameterName="pageSize" 
                                                                                                SortParameterName="sortBy" 
                                                                                                SelectCountMethod="GetColaboradoresDentroCount">
                                                                                                <SelectParameters>
                                                                                                    <asp:FormParameter name="pNumeroDocumento" formfield="txtNumeroDocumento" />
                                                                                                    <asp:FormParameter name="pNomeColaborador" formfield="txtNomeColaborador" />
                                                                                                    <asp:FormParameter name="pHoraInicio" formfield="txtHoraInicio" />
                                                                                                    <asp:FormParameter name="pHoraFim" formfield="txtHoraFim" />
                                                                                                    <asp:FormParameter name="pEscalaColaborador" formfield="txtEscalaColaborador" />
                                                                                                    <asp:FormParameter name="pEscalaDep" formfield="hdEscalaDep" />
                                                                                                </SelectParameters>
                                                                                            </asp:ObjectDataSource>
                                                                                            <!-- ********************* END: RADGRID ************************** -->
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
																	
																	 <br />
                                                                 <span style="text-align:left;margin-left:8px">                                                                    
                                                                    <asp:Button ID="btnInserirSaida" runat="server" Text="Registrar Saída" CssClass="buttonInserirSaida"
                                                                        OnClick="btnInserirSaida_Click" />
                                                                    </span>
                                                                    <br />
                                                                    <br />
                                                                 

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
                            </asp:Panel>
                           <!-- ****************** END: PAINEL DE LISTAGEM ************************** -->
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
        <iframe id="IframeAgenda" runat="server" style="z-index: 101; left: -500px; visibility: visible;
            position: absolute; top: 0px" name="gToday:contrast:agenda.js" src="../../Agenda/calendario.htm"
            frameborder="0" width="132" scrolling="no" height="142"></iframe>
    </form>
</body>
</html>
