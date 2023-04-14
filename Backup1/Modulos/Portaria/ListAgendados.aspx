<%@ Page Language="C#" AutoEventWireup="true" Codebehind="ListAgendados.aspx.cs"
    Inherits="SafWeb.UI.Modulos.Portaria.ListAgendados" %>

<%@ Register TagPrefix="rad" Namespace="Telerik.WebControls" Assembly="RadGrid.Net2" %>
<%@ Register TagPrefix="cc1" Namespace="FrameWork.WebControl" Assembly="FrameWork.WebControl" %>
<%@ Register TagPrefix="rada" Namespace="Telerik.WebControls" Assembly="RadAjax.Net2" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>%%SITE%%</title>
    <script src="../../Scripts/jquery-1.7.2.js" type="text/javascript"></script>
    <link href="../../Estilos/FrameWork.css" type="text/css" rel="stylesheet"/>
    <link href="../../Estilos/Brinks.css" type="text/css" rel="stylesheet"/>

    <script language="javascript">
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

        function AbrirHelpList() {
            window.open("HelpEntradaListagem.aspx", "JANELA", "height = 400, width = 530");
        }
        
        function PreencherHiddenColaboradores(colaboradores) {
            document.getElementById("txtHiddenColaboradoresMensagem").value = colaboradores;
			
			AjaxNS.AR('Colaboradores','', 'RadAjaxPanel1');
		}


		function PreencherHiddenVisitante(colaboradores) {
		    document.getElementById("txtHiddenColaboradores").value = colaboradores;

		    AjaxNS.AR('Colaboradores', '', 'RadAjaxPanel1');
		}


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
            $('#<%=ddlEscalaDep.ClientID%>').change(function () {
                $('#<%=txtNomeColaborador.ClientID%>').text($('#<%=ddlEscalaDep.ClientID%>').val());
                alert($('#<%=txtNomeColaborador.ClientID%>').val());

            });

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
                <td style="height: 19px">
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
                        <asp:HiddenField ID="txtHiddenColaboradoresMensagem" runat="server" />
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
                                        <asp:Label ID="lblListagem" runat="server" Text="Listagem de Visitantes Agendados - Entrada"></asp:Label>
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
                                        <table class="MarginTopConteudo" cellspacing="0" cellpadding="0" width="760" align="center"
                                            border="0">
                                            <tr>
                                                <td>
                                                    <table cellspacing="0" cellpadding="0" width="740" align="center" border="0">
                                                        <tr>
                                                            <td>
                                                                <table cellspacing="0" cellpadding="0" align="left" border="0">
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
                                                                            <asp:Button ID="btnAbaContingencia" Text="Acesso Contingencial (Fora de horário escalado)" runat="server"
                                                                                CssClass="cadbuttonAbaInativa" Width="100%" CausesValidation="False"  OnClick="btnAbaContingencia_Click">
                                                                            </asp:Button></td>
                                                                        <td>
                                                                            <asp:Image ID="imgAbaContingencia" runat="server" ImageUrl="%%PATH%%/Imagens/right-abaInativa.gif">
                                                                            </asp:Image></td>

                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <div id="divAbaVisitantes" style="border-right: #d6d6d6 1px solid; border-left: #d6d6d6 1px solid;
                                                                    width: 760px; border-bottom: #d6d6d6 1px solid; border-right: #d6d6d6 1px solid;
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
                                                                                        <td class="cadlbl" style="width: 180px">
                                                                                            <asp:Label ID="lblRegional" runat="server" Text="Regional:"></asp:Label>
                                                                                        </td>
                                                                                        <td style="width: 6px">
                                                                                        </td>
                                                                                        <td class="cadlbl" style="width: 180px">
                                                                                            <asp:Label ID="lblFilial" runat="server" Text="Filial:"></asp:Label>
                                                                                        </td>
                                                                                        <td style="width: 6px">
                                                                                        </td>
                                                                                        <td class="cadlbl" style="width: 180px">
                                                                                        </td>
                                                                                        <td style="width: 18px">
                                                                                        </td>
                                                                                        <td class="cadlbl" style="width: 180px">
                                                                                            <asp:Label ID="lblTipoDocumento" runat="server" Text="Tipo Documento:"></asp:Label></td>
                                                                                        <td style="width: 6px">
                                                                                        </td>
                                                                                        <td class="cadlbl" style="width: 180px">
                                                                                            <asp:Label ID="lblDocumento" runat="server" Text="Documento:"></asp:Label></td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="height: 20px">
                                                                                            <asp:DropDownList ID="ddlRegional" AutoPostBack="True" CssClass="cadddl" runat="server"
                                                                                                Width="120px" Enabled="False">
                                                                                            </asp:DropDownList>
                                                                                        </td>
                                                                                        <td style="height: 20px">
                                                                                        </td>
                                                                                        <td style="height: 20px">
                                                                                            <asp:DropDownList ID="ddlFilial" CssClass="cadddl" runat="server" Width="120px" Enabled="False">
                                                                                            </asp:DropDownList>
                                                                                        </td>
                                                                                        <td style="height: 20px">
                                                                                        </td>
                                                                                        <td style="width: 180px; height: 20px">
                                                                                        </td>
                                                                                        <td style="width: 18px; height: 20px">
                                                                                        </td>
                                                                                        <td style="height: 20px">
                                                                                            <asp:DropDownList ID="ddlTipoDocumento" CssClass="cadddl" runat="server" Width="140px"
                                                                                                AutoPostBack="True" OnSelectedIndexChanged="ddlTipoDocumento_SelectedIndexChanged">
                                                                                            </asp:DropDownList></td>
                                                                                        <td style="height: 20px">
                                                                                        </td>
                                                                                        <td style="height: 20px">
                                                                                            <cc1:FWMascara ID="txtDocumento" runat="server" AutoPostBack="True" CssClass="cadtxt"
                                                                                                Enabled="False" Mascara="NENHUMA" MaxLength="10" Width="120px"></cc1:FWMascara></td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="cadlbl" colspan="5">
                                                                                            <asp:Label ID="lblNomeVisitante" runat="server" Text="Visitante:"></asp:Label>
                                                                                        </td>
                                                                                        <td style="width: 18px">
                                                                                        </td>
                                                                                        <td class="cadlbl" colspan="5">
                                                                                            <asp:Label ID="lblNumSolicitacao" runat="server" Text="Solicitação:"></asp:Label></td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td colspan="5">
                                                                                            <asp:TextBox ID="txtVisitante" runat="server" CssClass="cadtxt" MaxLength="100" Width="360px"></asp:TextBox>
                                                                                        </td>
                                                                                        <td style="width: 18px">
                                                                                        </td>
                                                                                        <td colspan="5">
                                                                                            <cc1:FWMascara ID="txtNumSolicitacao" runat="server" CssClass="cadtxt" Mascara="NUMERO"
                                                                                                MaxLength="5" Width="75px"></cc1:FWMascara></td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="cadlbl" colspan="5">
                                                                                            &nbsp;<asp:Label ID="lblAprovador" runat="server" Text="Aprovador:"></asp:Label></td>
                                                                                        <td style="width: 18px">
                                                                                        </td>
                                                                                        <td class="cadlbl" colspan="5">
                                                                                            <asp:Label ID="lblEmpresa" runat="server" Text="Empresa:"></asp:Label></td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td colspan="5">
                                                                                            <asp:TextBox ID="txtAprovador" runat="server" CssClass="cadtxt" MaxLength="100" Width="360px"></asp:TextBox></td>
                                                                                        <td style="width: 18px">
                                                                                        </td>
                                                                                        <td colspan="5">
                                                                                            <asp:DropDownList ID="ddlEmpresa" CssClass="cadddl" runat="server" AutoPostBack="True"
                                                                                                OnSelectedIndexChanged="ddlTipoDocumento_SelectedIndexChanged">
                                                                                            </asp:DropDownList></td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td colspan="9" align="center" style="height: 43px">
                                                                                            <br />
                                                                                            <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="cadbuttonfiltro"
                                                                                                OnClick="btnBuscar_Click" /></td>
                                                                                    </tr>
                                                                                </table>
                                                                                <!-- ********************** END: FILTROS ************************** -->
                                                                                <table class="MarginTopConteudo" cellspacing="0" cellpadding="0" width="740" align="center"
                                                                                    border="0">
                                                                                    <tr>
                                                                                        <td align="center" colspan="3">
                                                                                            <br />
                                                                                            <!-- ********************* STAR: RADGRID ************************** -->
                                                                                            <rad:RadGrid ID="radVisAgendados" runat="server" AllowPaging="True" AllowSorting="True"
                                                                                                AutoGenerateColumns="False" CssClass="dtg" GridLines="None" Skin="None" Width="740px"
                                                                                                OnItemCommand="radVisAgendados_ItemCommand"  SortingSettings-SortToolTip="Clique para filtrar" OnItemDataBound="radVisAgendados_ItemDataBound"
                                                                                                OnNeedDataSource="radVisAgendados_NeedDataSource">
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
                                                                                                        <rad:GridBoundColumn DataField="Id_Solicitacao" HeaderText="Nº Solicitação" UniqueName="Id_Solicitacao"
                                                                                                            SortExpression="Id_Solicitacao">
                                                                                                            <ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
                                                                                                        </rad:GridBoundColumn>
                                                                                                        <rad:GridBoundColumn DataField="Id_Visitante" Visible="false" HeaderText="Id_Visitante"
                                                                                                            UniqueName="Id_Visitante" SortExpression="Id_Visitante">
                                                                                                        </rad:GridBoundColumn>
                                                                                                        <rad:GridBoundColumn DataField="Nom_Visitante" HeaderText="Visitante" UniqueName="Nom_Visitante"
                                                                                                            SortExpression="Nom_Visitante">
                                                                                                            <ItemStyle HorizontalAlign="Left" Width="15%"></ItemStyle>
                                                                                                        </rad:GridBoundColumn>
                                                                                                        <rad:GridBoundColumn DataField="Emp_Visitante" HeaderText="Empresa" UniqueName="Emp_Visitante"
                                                                                                            SortExpression="Emp_Visitante">
                                                                                                            <ItemStyle HorizontalAlign="Left" Width="10%"></ItemStyle>
                                                                                                        </rad:GridBoundColumn>
                                                                                                        <rad:GridBoundColumn DataField="Nom_Visitado" HeaderText="Visitado" UniqueName="Nom_Visitado"
                                                                                                            SortExpression="Nom_Visitado">
                                                                                                            <ItemStyle HorizontalAlign="Left" Width="15%"></ItemStyle>
                                                                                                        </rad:GridBoundColumn>
                                                                                                        <rad:GridBoundColumn DataField="Dt_InicioVisita" HeaderText="Data/Hora Início" UniqueName="Dt_InicioVisita"
                                                                                                            SortExpression="Dt_InicioVisita" DataFormatString="{0:dd/MM/yyyy HH:mm}">
                                                                                                            <ItemStyle HorizontalAlign="Center" Width="15%"></ItemStyle>
                                                                                                        </rad:GridBoundColumn>
                                                                                                        <rad:GridBoundColumn DataField="Dt_FimVisita" HeaderText="Data/Hora Fim" UniqueName="Dt_FimVisita"
                                                                                                            SortExpression="Dt_FimVisita" DataFormatString="{0:dd/MM/yyyy HH:mm}">
                                                                                                            <ItemStyle HorizontalAlign="Center" Width="15%"></ItemStyle>
                                                                                                        </rad:GridBoundColumn>
                                                                                                        <rad:GridBoundColumn DataField="Des_ObsSolicitacao" HeaderText="Observação" UniqueName="Des_ObsSolicitacao"
                                                                                                            SortExpression="Des_ObsSolicitacao">
                                                                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                                        </rad:GridBoundColumn>
                                                                                                        <rad:GridButtonColumn CommandName="Ativar" UniqueName="Ativar" ButtonType="ImageButton"
                                                                                                            ImageUrl="%%PATH%%/imagens/icones/ico_Ativo.gif" Text="Ativar">
                                                                                                            <ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
                                                                                                        </rad:GridButtonColumn>
                                                                                                        <rad:GridTemplateColumn Visible="False" UniqueName="TemplateColumn4">
                                                                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                                                            <ItemTemplate>
                                                                                                                <asp:ImageButton ID="imgSelecionar" runat="server" ImageUrl="~/imagens/icones/ico_editar.gif"
                                                                                                                    CommandName="Selecionar" Visible="false"></asp:ImageButton>
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
                                                                                            </rad:RadGrid>&nbsp;
                                                                                            <!-- ********************* END: RADGRID ************************** -->
                                                                                            <br />
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
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
                                                                                                Width="120px">
                                                                                            </asp:DropDownList></td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td align="center" colspan="5" style="height: 43px">
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
                                                                                                            ButtonType="ImageButton" HeaderText="Registrar Entrada" ImageUrl="%%PATH%%/imagens/icones/ico_Ativo.gif">
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
                                                                                                SelectMethod="GetColaboradoresFora" 
                                                                                                EnablePaging="true"
                                                                                                TypeName="SafWeb.UI.Modulos.Portaria.ColaboradoresData" 
                                                                                                StartRowIndexParameterName="startIndex" 
                                                                                                MaximumRowsParameterName="pageSize" 
                                                                                                SortParameterName="sortBy" 
                                                                                                SelectCountMethod="GetColaboradoresForaCount">
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
                                                                    <asp:Button ID="btnInserirEntrada" runat="server" Text="Registrar Entrada" CssClass="buttonInserirEntrada"
                                                                        OnClick="btnInserirEntrada_Click" />
                                                                    </span>
                                                                    <br />
                                                                    <br />

                                                                </div>
                                                                    
                                                            </td>
                                                        </tr>

                                                        <tr>
                                                            <td>
                                                                <!-- ****************** START: TERCEIRA ABA ************************** -->

                                                                   <div id="divAbaContingencia" style="border-right: #d6d6d6 1px solid; border-left: #d6d6d6 1px solid;
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
                                                                                        <td class="cadmsg" style="width: 726px; height: 28px; text-align: left;" align="center"
                                                                                            colspan="3">
                                                                                            <asp:ValidationSummary ID="rfvContingencia" runat="server" DisplayMode="List" ValidationGroup="Contingencia" />
                                                                                            <asp:ValidationSummary ID="rfvContingencia2" runat="server" DisplayMode="List" ValidationGroup="Contingencia2" />
                                                                                            <asp:ValidationSummary ID="rfvContingencia3" runat="server" DisplayMode="List" ValidationGroup="Contingencia3" />
                                                                                        </td>
                                                                                    </tr>



<table style="border: 1px solid black" width="740">


                                                                                    <tr>
                                                                                        <td class="cadlbl" width="240">
                                                                                            <asp:Label ID="lblNomeVisitante_Conting" runat="server" Text="Nome Colaborador:">
                                                                                            </asp:Label>
                                                                                        </td>

                                                                                        <td style="width: 5px;">
                                                                                        </td>

                                                                                        <td class="cadlbl" width="240">
                                                                                            <asp:Label ID="lblRE_Conting" runat="server" Text="RE:"></asp:Label>
                                                                                        </td>
                                                                                    </tr>

                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:TextBox ID="txtNomeColaborador_Conting" runat="server" CssClass="cadtxt" MaxLength="50"
                                                                                                Width="390px" Enabled="False"></asp:TextBox>
                                                                                                <asp:ImageButton ID="btnListarColaborador_Conting"
                                                                                                    runat="server" ImageUrl="~/Imagens/icones/ico_Visualizar.gif" AlternateText="Buscar"
                                                                                                    OnClick="btnListarColaborador_Conting_Click"/>
                                                                                            <asp:RequiredFieldValidator ID="rfvNomeColaborador" runat="server" ControlToValidate="txtNomeColaborador_Conting"
                                                                                                ErrorMessage="Campo Obrigatório: Nome do colaborador." InitialValue="" ValidationGroup="Contingencia3">*</asp:RequiredFieldValidator>
                                                                                                
                                                                                                <asp:ImageButton ID="btnLimparColaborador_Conting"
                                                                                                    runat="server" ImageUrl="~/Imagens/icones/ico_deletar.gif" AlternateText="Remover" Visible="False"
                                                                                                    OnClick="btnLimparColaborador_Conting_Click"/>

                                                                                        </td>

                                                                            
                                                                                        <td style="height: 20px; width: 5px;">
                                                                                        </td>
                                                                                        <td style="height: 20px">
                                                                                        <asp:TextBox ID="txtREColaborador_Conting" runat="server" CssClass="cadtxt" MaxLength="50" Width="85px"
                                                                                              Height="20px" Enabled="false"></asp:TextBox>

                                                                                        </td>
                                                                                    </tr>

                                                                                    <tr>
                                                                                        <td class="cadlbl" width="240">
                                                                                            <asp:Label ID="Label4" runat="server" Text="Cargo:">
                                                                                            </asp:Label>
                                                                                        </td>

                                                                                        <td style="width: 5px;">
                                                                                        </td>

                                                                                        <td class="cadlbl" width="240">
                                                                                            <asp:Label ID="Label5" runat="server" Text="Foto:"></asp:Label>
                                                                                        </td>
                                                                                    </tr>

                                                                                    <tr>
                                                                                        <td class="cadlbl" width="240" valign="top">
                                                                                            <asp:TextBox ID="txtCargoColaborador" runat="server" CssClass="cadtxt" MaxLength="50" Width="390px"
                                                                                              Height="20px" Enabled="false"></asp:TextBox>
                                                                                        </td>

                                                                                        <td style="width: 5px;">
                                                                                            
                                                                                        </td>
                                                                                        <td class="cadlbl" width="240">
                                                                                            <asp:image ID="imgFoto" runat="server" Visible="false" Width="79" Height="105" BorderWidth="1px"/>
                                                                                        </td>
                                                                                    </tr>

                                                                                </table>

                                                                                    <tr>
                                                                                        <td class="cadlbl">
                                                                                            &nbsp;
                                                                                        </td>
                                                                                    </tr>

                                                                            <table style="border: 1px solid black" width="740">
                                                                                    <tr>
                                                                                        <td class="cadlbl">
                                                                                            <asp:Label ID="lblEscalaDpto_Conting" runat="server" 
                                                                                                Text="Escala Departamental:">
                                                                                            </asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                             <asp:DropDownList ID="ddlEscalaDpto_Conting" runat="server" 
                                                                                                CssClass="cadddl" Width="392px" Enabled="True" AutoPostBack="True" 
                                                                                                OnSelectedIndexChanged="ddlEscalaDpto_Conting_SelectedIndexChanged" 
                                                                                                Height="16px">
                                                                                             </asp:DropDownList>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="cadlbl">
                                                                                            <asp:Label ID="lblListaColaboradores_Conting" runat="server" 
                                                                                                Text="Lista de colaboradores:"></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:ListBox ID="lstColaboradores_Conting" runat="server" Height="192px" Width="391px" SelectionMode="Multiple"
                                                                                                CssClass="cadlstBox"></asp:ListBox>
                                                                                            <asp:RequiredFieldValidator ID="rfvColaborabores_Conting" runat="server" ControlToValidate="lstColaboradores_Conting"
                                                                                                ErrorMessage="Campo Obrigatório: Selecionar um ou mais colaboradores." InitialValue="" ValidationGroup="Contingencia2">*</asp:RequiredFieldValidator>

                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="cadlbl">
                                                                                            <asp:CheckBox id="chkSelTodos_Conting" runat="server" Checked="False"  AutoPostBack="true" OnCheckedChanged="chkSelTodos_Conting_CheckedChanged" Text="Selecionar todos" />
                                                                                        </td>
                                                                                    </tr>

</table>



                                                                                    <tr>
                                                                                        <td class="cadlbl">
                                                                                            &nbsp;
                                                                                        </td>
                                                                                    </tr>

<table style="border: 1px solid black;background-color: #ecf1f7" width="740">
                                                                                    <tr>
                                                                                        <td class="cadlbl">
                                                                                            <asp:Label ID="lblMotivoLiberacao_Conting" runat="server" Text="Motivo da Liberação:"></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="cadlbl">
                                                                                            <asp:TextBox ID="txtMotivoLiberacao_Conting" runat="server" CssClass="cadlstBox" MaxLength="10"
                                                                                                Width="390px" Height="80px" TextMode="MultiLine"></asp:TextBox>
                                                                                            <asp:RequiredFieldValidator ID="rfvMotivoLiberacao" runat="server" ControlToValidate="txtMotivoLiberacao_Conting"
                                                                                                ErrorMessage="Campo Obrigatório: Motivo da liberação." InitialValue="" ValidationGroup="Contingencia">*</asp:RequiredFieldValidator>
                                                                                        </td>
                                                                                    </tr>

</table>
                                                                                    <tr>
                                                                                        <td class="cadlbl">
                                                                                            <br/>
                                                                                            <table style="border: 1px solid black; background-color: #ecf1f7" width="740">
                                                                                                <tr>
                                                                                                    <td class="cadlbl" width="500">
                                                                                                        <asp:Label ID="lblAprovador_Conting" runat="server" Text="Responsável pela liberação:"></asp:Label>
                                                                                                    </td>
                                                                                                    <td style="height: 20px; width: 55px;">
                                                                                                    </td>

                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td class="cadlbl">
                                                                                                        <asp:DropDownList ID="ddlAprovador_Conting" runat="server" CssClass="cadddl" Width="392px"
                                                                                                            Enabled="False" AutoPostBack="False" OnSelectedIndexChanged="ddlAprovador_Conting_SelectedIndexChanged">
                                                                                                        </asp:DropDownList>
                                                                                                        <asp:RequiredFieldValidator ID="rfvAprovador" runat="server" ControlToValidate="ddlAprovador_Conting"
                                                                                                            ErrorMessage="Campo Obrigatório: Aprovador." InitialValue="0" ValidationGroup="Contingencia">*</asp:RequiredFieldValidator>
                                                                                                    </td>
                                                                                                    <td style="height: 20px; width: 55px;">
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </td>
                                                                                    </tr>


                                                                                    <tr>
                                                                                        <td align="center" style="height: 43px">
                                                                                            <br />
                                                                                            <!-- ********************* START: BARRA DE BOTÕES INFERIOR ************************** -->
                                                                                            <table align="center" border="0" cellpadding="0" cellspacing="0" width="760">
                                                                                                <caption>
                                                                                                </caption>
                                                                                                <tr>
                                                                                                    <td align="right" class="backBarraBotoes">
                                                                                                        &nbsp;<asp:Button ID="btnGravarContingencia" runat="server" CssClass="cadbutton100" Text="Liberar Acesso" OnClick="btnGravarContingencia_Click"
                                                                                                             ValidationGroup="Contingencia" />
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
