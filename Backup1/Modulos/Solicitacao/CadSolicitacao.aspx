<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CadSolicitacao.aspx.cs"
    Inherits="SafWeb.UI.Modulos.Solicitacao.CadSolicitacao" %>

<%@ Register TagPrefix="rada" Namespace="Telerik.WebControls" Assembly="RadAjax.Net2" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.WebControls" Assembly="RadGrid.Net2" %>
<%@ Register TagPrefix="cc1" Namespace="FrameWork.WebControl" Assembly="FrameWork.WebControl" %>
<%@ Register TagPrefix="radc" Namespace="Telerik.WebControls" Assembly="RadCalendar.Net2" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>%%SITE%%</title>
    <link href="../../Estilos/FrameWork.css" type="text/css" rel="stylesheet">
    <link href="../../Estilos/Brinks.css" rel="stylesheet" type="text/css" />
    <link href="../../Web20/Calendar.Web20.css" rel="stylesheet" type="text/css" />
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
        
        function PreencherHiddenVisitado(codigo) {
            document.getElementById("HiddenVisitado").value = codigo
			
			AjaxNS.AR('Visitado','', 'RadAjaxPanelVisitado');
		}
		
        function PreencherHiddenVisitante(codigo) {
            document.getElementById("HiddenVisitante").value = codigo
			
			AjaxNS.AR('Visitante','', 'RadAjaxPanelVisitante');
		}
		
        function PreencherHiddenLista(retirados) {
		
			document.getElementById("HiddenColRetirados").value = retirados
			
			AjaxNS.AR('Grid','', 'RadAjaxPanelGrid');
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
            window.open("HelpSolCadastro.aspx", "JANELA", "width = 530, scrollbars=yes");
        }
        
        function AbrirHelpList() {
            window.open("HelpSolListagem.aspx", "JANELA", "height = 400, width = 530");
        }
        
        function blocTexto(valor) {
		    quant = 199;
		    total = valor.length;
            if (total > quant) {
			    document.getElementById('txtObservacao').value = valor.substr(0,quant);
		    }
		    
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
                        <asp:Panel ID="pnlErro" runat="server">                        
                        </asp:Panel>
                        <!-- ****************** START: PAINEL DE LISTAGEM ************************** -->
                        <asp:Panel runat="server" ID="pnlListagem">
                            <!-- ***************** START: BARRA DE TITUTO ******************* -->
                            <table class="MarginTopBarra" cellspacing="0" cellpadding="0" width="760" align="center"
                                border="0">
                                <tr>
                                    <td class="cadBarraTitulo" align="left" height="25" style="width: 750px">
                                        <img src="../../Imagens/comum/bulletbarra.gif" align="middle" />
                                        <asp:Label ID="lblListagem" runat="server" Text="Listagem de Solicitação"></asp:Label>
                                    </td>
                                    <td class="cadBarraTitulo" align="right" height="25" style="width: 9px">
                                        <asp:ImageButton ID="btnHelpList" runat="server" AlternateText="Ajuda" ImageUrl="%%PATH%%/imagens/icones/ico_help.jpg"
                                            ImageAlign="AbsMiddle" Visible="False" />
                                    </td>
                                </tr>
                                <!-- ***************** END: BARRA DE TITUTO ******************* -->
                                <tr>
                                    <td class="backboxconteudo" valign="top" colspan="2">
                                        <!-- ****************** START: FILTROS ************************** -->
                                        <table class="MarginTopConteudo" cellspacing="0" cellpadding="0" width="740" align="center"
                                            border="0">
                                            <tr>
                                                <td colspan="7" class="cadmsg">
                                                    <asp:ValidationSummary ID="ValidationSummary3" runat="server" DisplayMode="List" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="cadlbl" width="180">
                                                    <asp:Label ID="lblNumSolicitacao" runat="server" Text="Nº Solicitação:"></asp:Label>
                                                </td>
                                                <td width="5">
                                                </td>
                                                <td class="cadlbl" width="180">
                                                <asp:Label ID="lblEmpresa" runat="server" Text="Empresa:"></asp:Label>
                                            </td>
                                                <td width="5">
                                                </td>
                                                <td class="cadlbl" style="width: 215px">
                                                    <asp:Label ID="lblRegional" runat="server" Text="Regional:"></asp:Label>
                                                </td>
                                                <td width="5">
                                                </td>
                                                <td class="cadlbl" width="180">
                                                    <asp:Label ID="lblFilial" runat="server" Text="Filial:"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                <cc1:FWMascara ID="txtNumSolicitacao" runat="server" CssClass="cadtxt" Mascara="NUMERO"
                                                    MaxLength="8" Width="75px"></cc1:FWMascara>
                                                </td>
                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlEmpresa" CssClass="cadddl" runat="server" Width="150px">
                                    </asp:DropDownList>
                                </td>
                                                <td>
                                                </td>
                                                <td style="width: 215px">
                                                    <asp:DropDownList ID="ddlRegional" CssClass="cadddl" runat="server" Width="150px"
                                                        OnSelectedIndexChanged="ddlRegional_SelectedIndexChanged" AutoPostBack="True">
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlFilial" CssClass="cadddl" runat="server" Width="150px" Enabled="False">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="cadlbl">
                                    <asp:Label ID="lblStatus" runat="server" Text="Status:"></asp:Label>
                                </td>
                                                <td>
                                                </td>
                                                <td class="cadlbl">
                                    <asp:Label ID="lblTipoSolicitacao" runat="server" Text="Tipo Solicitação:"></asp:Label>
                                </td>
                                                <td>
                                                </td>
                                                <td class="cadlbl" style="width: 215px">
                                    <asp:Label ID="lblDataInicio" runat="server" Text="Data Início:"></asp:Label>
                                </td>
                                                <td>
                                                </td>
                                                <td class="cadlbl">
                                    <asp:Label ID="lblDataFim" runat="server" Text="Data Fim:"></asp:Label>
                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 21px">
                                                    <asp:DropDownList ID="ddlStatus" CssClass="cadddl" runat="server" Width="150px">
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="height: 21px">
                                                </td>
                                                <td style="height: 21px">
                                                    <asp:DropDownList ID="ddlTipoSolicitacao" CssClass="cadddl" runat="server" Width="150px">
                                    </asp:DropDownList>
                                </td>
                                                <td style="height: 21px">
                                                </td>
                                                <td style="width: 215px; height: 21px;">
                                                <cc1:FWMascara ID="txtDataInicio" runat="server" AccessKey="I" CssClass="cadtxt"
                                                    Mascara="DATA" MaxLength="10" Width="100px"></cc1:FWMascara><a hidefocus="" href="javascript:void(0)"
                                                        onclick="if(self.gfPopComp)gfPopComp.fStartPop(document.forms[0].all('<% = txtDataInicio.ClientID.ToString() %>'),document.forms[0].all('<% = txtDataInicio.ClientID.ToString() %>'));return false;">
                                                        <img align="absMiddle" alt="" border="0" height="19" name="popcal" src="%%PATH%%/agenda/calbtn.gif"
                                                            width="20" />
                                                    </a>
                                                <asp:CompareValidator ID="cpvtxtInicioPeriodo" runat="server" ControlToValidate="txtDataInicio"
                                        Display="Dynamic" ErrorMessage="- Data Inválida: Data Início" Operator="DataTypeCheck"
                                        Type="Date">*</asp:CompareValidator>
                                                <asp:CompareValidator ID="cpvDataInicialXFinal" runat="server" ControlToCompare="txtDataFim"
                                                    ControlToValidate="txtDataInicio" ErrorMessage="- A data início deve ser menor que a data fim."
                                                    Operator="LessThanEqual">*</asp:CompareValidator>
                                                </td>
                                                <td style="height: 21px">
                                                </td>
                                                <td style="height: 21px">
                                                                        <cc1:FWMascara ID="txtDataFim" runat="server" AccessKey="F" CssClass="cadtxt" Mascara="DATA"
                                                                            MaxLength="10" Width="100px"></cc1:FWMascara><a hidefocus="" href="javascript:void(0)"
                                                                                onclick="if(self.gfPopComp)gfPopComp.fEndPop(document.forms[0].all('<% = txtDataInicio.ClientID.ToString() %>'),document.forms[0].all('<% = txtDataFim.ClientID.ToString() %>'));return false;">
                                                                                <img align="absMiddle" alt="" border="0" height="19" name="popcal" src="%%PATH%%/agenda/calbtn.gif"
                                                                                    width="20" />
                                                                            </a>
                                                                        <asp:CompareValidator ID="cpvtxtFimPeriodo" runat="server" ControlToValidate="txtDataFim"
                                        Display="Dynamic" ErrorMessage="- Data Inválida: Data fim" Operator="DataTypeCheck"
                                        Type="Date">*</asp:CompareValidator>
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
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3" style="height: 27px">
                                                    <asp:TextBox ID="txtNomeVisitado" runat="server" CssClass="cadtxt" MaxLength="50"
                                        Width="340px"></asp:TextBox>
                                </td>
                                                <td style="height: 27px">
                                                </td>
                                                <td colspan="3" style="height: 27px">
                                                    <asp:TextBox ID="txtNomeVisitante" runat="server" CssClass="cadtxt" MaxLength="50"
                                        Width="335px"></asp:TextBox>
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
                                        Width="340px"></asp:TextBox>
                                </td>
                                                <td>
                                                </td>
                                                <td colspan="3">
                                                    <asp:TextBox ID="txtNomeAprovador" runat="server" CssClass="cadtxt" MaxLength="50"
                                        Width="335px"></asp:TextBox>
                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="8" align="center" style="height: 19px">
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
                                                    <rad:RadGrid ID="radGridSolicitacao" runat="server" AllowPaging="True" AllowSorting="True"
                                        AutoGenerateColumns="False" SortingSettings-SortToolTip="Clique para filtrar"
                                        CssClass="dtg" GridLines="None" Skin="None" Width="100%" OnItemCommand="radGridSolicitacao_ItemCommand"
                                        OnItemDataBound="radGridSolicitacao_ItemDataBound" OnNeedDataSource="radGridSolicitacao_NeedDataSource">
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
                                                                <rad:GridBoundColumn DataField="Codigo" HeaderText="Nº Solicitação" SortExpression="Codigo"
                                                                    UniqueName="Codigo">
                                                                    <ItemStyle Width="5%"></ItemStyle>
                                                                </rad:GridBoundColumn>
                                                                <rad:GridBoundColumn DataField="Visitante" HeaderText="Visitante" SortExpression="Visitante"
                                                                    UniqueName="Visitante" AllowFiltering="true">
                                                                    <ItemStyle Width="25%"></ItemStyle>
                                                                </rad:GridBoundColumn>
                                                                <rad:GridBoundColumn DataField="InicioVisita" HeaderText="Data/Hora Início" SortExpression="InicioVisita"
                                                                    UniqueName="InicioVisita" DataFormatString="{0:dd/MM/yyyy HH:mm}">
                                                                    <ItemStyle Width="5%"></ItemStyle>
                                                                </rad:GridBoundColumn>
                                                                <rad:GridBoundColumn DataField="FimVisita" HeaderText="Data/Hora Fim" SortExpression="FimVisita"
                                                                    UniqueName="FimVisita" DataFormatString="{0:dd/MM/yyyy HH:mm}">
                                                                    <ItemStyle Width="5%"></ItemStyle>
                                                                </rad:GridBoundColumn>
                                                                <rad:GridBoundColumn DataField="AliasFilial" HeaderText="Filial" SortExpression="AliasFilial"
                                                                    UniqueName="AliasFilial">
                                                                    <ItemStyle Width="20%" HorizontalAlign="Center"></ItemStyle>
                                                                </rad:GridBoundColumn>
                                                                <rad:GridBoundColumn DataField="Status" HeaderText="Status" SortExpression="Status"
                                                                    UniqueName="Status">
                                                                    <ItemStyle Width="25%"></ItemStyle>
                                                                </rad:GridBoundColumn>
                                                                <rad:GridBoundColumn DataField="CodStatus" SortExpression="CodStatus" UniqueName="CodStatus"
                                                                    Visible="false" />
                                                                <rad:GridBoundColumn DataField="CodUsuSolic" SortExpression="CodUsuSolic" UniqueName="CodUsuSolic"
                                                                    Visible="false" />
                                                                <rad:GridTemplateColumn Visible="False">
                                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="imgVisualizar" runat="server" ImageUrl="~/imagens/icones/ico_editar.gif"
                                                                            CommandName="Visualizar" Visible="false"></asp:ImageButton>
                                                                    </ItemTemplate>
                                                                </rad:GridTemplateColumn>
                                                                <rad:GridButtonColumn CommandName="Ativar" UniqueName="Ativar" ButtonType="ImageButton"
                                                                    ImageUrl="%%PATH%%/imagens/icones/ico_Ativo.gif" Text="Ativar">
                                                                    <ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
                                                                </rad:GridButtonColumn>
                                                                <rad:GridButtonColumn CommandName="Editar" UniqueName="Editar" ButtonType="ImageButton"
                                                                    ImageUrl="%%PATH%%/imagens/icones/ico_editar.gif" Text="Editar" >
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
                                        <!-- ********************* START: BARRA DE BOTÕES INFERIOR ************************** -->
                                        <table cellspacing="0" cellpadding="0" width="760" align="center" border="0">
                                            <caption>
                                            </caption>
                                            <tr>
                                                <td class="backBarraBotoes" align="right" style="height: 30px">
                                                    <asp:Button ID="btnIncluir" runat="server" Text="Incluir" CssClass="cadbutton100"
                                                        OnClick="btnIncluir_Click"></asp:Button>
                                                </td>
                                            </tr>
                                        </table>
                                        <!-- ********************* END: BARRA DE BOTÕES INFERIOR ************************** -->
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
                                    <td class="cadBarraTitulo" align="left" style="height: 25px; width: 750px">
                                        <img src="../../Imagens/comum/bulletbarra.gif" align="absMiddle">
                                        <asp:Label ID="Label2" runat="server" Text="Cadastro de Solicitação"></asp:Label>
                                    </td>
                                    <td class="cadBarraTitulo" align="right" height="25" style="width: 9px">
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
                                                    <asp:Label ID="lblMensagem" runat="server" Text="Label" Visible="false" CssClass="cadlbl"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <rada:RadAjaxPanel ID="RadAjaxPanelVisitante" runat="server" ClientEvents-OnRequestStart="OnRequestStart"
                                                        LoadingPanelID="lpaCadastro">
                                                        <table>
                                                            <tr>
                                                                <td class="cadlbl">
                                                                    <asp:Label ID="lblDadosVisita" runat="server" Text="Dados Visita"></asp:Label>
                                                                    <table style="border: 1px solid black" width="740">
                                                                        <tr>
                                                                            <td class="cadlbl">
                                                                                <asp:Label ID="lblRegionalCad" runat="server" Text="Regional:"></asp:Label>
                                                                            </td>
                                                                            <td class="cadlbl" colspan="2">
                                                                                <asp:Label ID="lblFilialCad" runat="server" Text="Filial:"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                            </td>
                                                                            <td class="cadlbl">
                                                            <asp:Label ID="Label4" runat="server" Text="Motivo da Visita:"></asp:Label>
                                                        </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:DropDownList ID="ddlRegionalCad" runat="server" CssClass="cadddl" OnSelectedIndexChanged="ddlRegionalCad_SelectedIndexChanged"
                                                                                    AutoPostBack="True">
                                                                                </asp:DropDownList>
                                                                                <asp:RequiredFieldValidator ID="rfvRegional" runat="server" ControlToValidate="ddlRegionalCad"
                                                                                    ErrorMessage="Campo Obrigatório: Regional." ValidationGroup="Buscar" InitialValue="0"
                                                                SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                                        </td>
                                                                            <td colspan="2">
                                                                                <asp:DropDownList ID="ddlFilialCad" runat="server" CssClass="cadddl" AutoPostBack="True"
                                                                                    Enabled="False" OnSelectedIndexChanged="ddlFilialCad_SelectedIndexChanged">
                                                                                </asp:DropDownList>
                                                                                <asp:RequiredFieldValidator ID="rfvFilial" runat="server" ControlToValidate="ddlFilialCad"
                                                                                    ErrorMessage="Campo Obrigatório: Filial." ValidationGroup="Buscar" InitialValue="0"
                                                                SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                                        </td>
                                                                            <td>
                                                                            </td>
                                                                            <td>
                                                                                <asp:DropDownList ID="ddlMotivoVisita" runat="server" CssClass="cadddl" AutoPostBack="True"
                                                                                    OnSelectedIndexChanged="ddlMotivoVisita_SelectedIndexChanged">
                                                                                </asp:DropDownList>
                                                                                <asp:RequiredFieldValidator ID="rfvMotivo" runat="server" ControlToValidate="ddlMotivoVisita"
                                                                ErrorMessage="Campo Obrigatório: Motivo Visita." ValidationGroup="Buscar" InitialValue="0"
                                                                SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                                        </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="cadlbl" style="width: 168px">
                                                                                <asp:Label ID="lblAreaVisitada" runat="server" Text="Áreas:"></asp:Label>
                                                                            </td>
                                                                            <td style="width: 54px">
                                                                            </td>
                                                                            <td class="cadlbl">
                                                                                <asp:Label ID="lblAreaSelecionada" runat="server" Text="Áreas Selecionadas:"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                            </td>
                                                                            <td class="cadlbl">
                                                            <asp:Label ID="lblObservacao" runat="server" Text="Observação:"></asp:Label>
                                                        </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td valign="top">
                                                                                <asp:ListBox ID="lstAreaVisita" runat="server" Height="137px" Width="145px" CssClass="cadlstBox"
                                                                                    SelectionMode="Multiple"></asp:ListBox>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Button ID="btnAddTodos" runat="server" Text=" >> " CssClass="cadbuttonfiltro"
                                                                                    OnClick="btnAddTodos_Click" Width="25px" /><br />
                                                                                <br />
                                                                                <asp:Button ID="btnAddUm" runat="server" Text=" > " CssClass="cadbuttonfiltro" OnClick="btnAddUm_Click"
                                                                                    Width="25px" /><br />
                                                                                <br />
                                                                                <asp:Button ID="btnRemoverUm" runat="server" Text="<" CssClass="cadbuttonfiltro"
                                                                                    OnClick="btnRemoverUm_Click" Width="25px" /><br />
                                                                                <br />
                                                                                <asp:Button ID="btnRemoverTodos" runat="server" CssClass="cadbuttonfiltro" Text=" &lt;&lt; "
                                                                                    OnClick="btnRemoverTodos_Click" Width="25px" />&nbsp;
                                                                            </td>
                                                                            <td valign="top">
                                                                                <asp:ListBox ID="lstAreaSelecionada" runat="server" Height="125px" Width="145px"
                                                                                    CssClass="cadlstBox" SelectionMode="Multiple"></asp:ListBox>
                                                                            </td>
                                                                            <td>
                                                                            </td>
                                                                            <td valign="top">
                                                                                <asp:TextBox ID="txtObservacao" runat="server" CssClass="cadlstBox" MaxLength="10"
                                                                                    Width="200px" Height="80px" TextMode="MultiLine"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="cadlbl" style="width: 168px; height: 21px;">
                                                                                <asp:Label ID="Label5" runat="server" Text="Data/Hora Início"></asp:Label>
                                                                            </td>
                                                                            <td style="width: 54px; height: 21px;">
                                                                            </td>
                                                                            <td class="cadlbl" style="height: 21px">
                                                            <asp:Label ID="Label6" runat="server" Text="Data/Hora Fim"></asp:Label>
                                                        </td>
                                                                            <td style="height: 21px">
                                                                            </td>
                                                                            <td class="cadlbl" style="height: 21px">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 168px">
                                                                                <asp:TextBox ID="txtDataInicioCad" runat="server" CssClass="cadtxt" Width="115px"
                                                                                    MaxLength="16" AutoPostBack="True"></asp:TextBox>
                                                                                <a hidefocus onclick="if(self.gfPop)gfPop.fPopCalendar(document.forms[0].all('<% = this.txtDataInicioCad.ClientID.ToString() %>'));return false;"
                                                                                    href="javascript:void(0)">
                                                                                    <img height="19" alt="" src="../../Agenda/calbtn.gif" width="20" align="absMiddle"
                                                                                        border="0" name="popcal" id="IMG1"></a>
                                                                                <asp:RequiredFieldValidator ID="rfvDataInicio" runat="server" ControlToValidate="txtDataInicioCad"
                                                                                    ErrorMessage="Campo Obrigatório: Data/Hora Início." ValidationGroup="Buscar"
                                                                SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                                        </td>
                                                                            <td style="width: 54px">
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtDataFimCad" runat="server" CssClass="cadtxt" Width="115px" MaxLength="16"></asp:TextBox>
                                                                                <a hidefocus onclick="if(self.gfPop)gfPop.fPopCalendar(document.forms[0].all('<% = this.txtDataFimCad.ClientID.ToString() %>'));return false;"
                                                                                    href="javascript:void(0)">
                                                                                    <img height="19" alt="" src="../../Agenda/calbtn.gif" width="20" align="absMiddle"
                                                                                        border="0" name="popcal"></a>
                                                                                <asp:RequiredFieldValidator ID="rfvDataFim" runat="server" ControlToValidate="txtDataFimCad"
                                                                ErrorMessage="Campo Obrigatório: Data/Hora Fim." ValidationGroup="Buscar" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                                        </td>
                                                                            <td>
                                                                            </td>
                                                                            <td>
                                                                                <asp:CheckBox ID="chkSabado" runat="server" Text="Acesso Sábado" CssClass="cadchk" />
                                                                                <asp:CheckBox ID="chkDomingo" runat="server" Text="Acesso Domingo" CssClass="cadchk" />
                                                            <asp:CheckBox ID="chkFeriado" runat="server" Text="Acesso Feriado" CssClass="cadchk" />
                                                        </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="cadlbl">
                                                                    <br />
                                                                    <asp:Label ID="lblDadosVisitante" runat="server" Text="Dados Visitante"></asp:Label>
                                                                    <asp:Panel ID="pnlListaVisitante" runat="server">
                                                                    </asp:Panel>
                                                                    <table style="border: 1px solid black" width="740">
                                                                        <tr>
                                                                            <td class="cadlbl" width="177">
                                                            <asp:Label ID="lblTipoVisitante" runat="server" Text="Tipo Visitante:"></asp:Label>
                                                        </td>
                                                                            <td width="5">
                                                                            </td>
                                                                            <td class="cadlbl" width="150">
                                                            <asp:Label ID="lblLista" runat="server" Text="Lista de Visitantes:" Visible="False"></asp:Label>
                                                        </td>
                                                                            <td width="5">
                                                                            </td>
                                                                            <td class="cadlbl" style="width: 153px">
                                                                            </td>
                                                                            <td width="150">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:DropDownList ID="ddlTipoVisitante" runat="server" CssClass="cadddl" AutoPostBack="True"
                                                                                    OnSelectedIndexChanged="ddlTipoVisitanteCad_SelectedIndexChanged">
                                                                                </asp:DropDownList>
                                                                                <asp:RequiredFieldValidator ID="rfvTipoVisitante" runat="server" ControlToValidate="ddlTipoVisitante"
                                                                                    ErrorMessage="Campo Obrigatório: Tipo Visitante." ValidationGroup="Buscar" InitialValue="0"
                                                                                    SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                                                            </td>
                                                                            <td>
                                                                            </td>
                                                                             <td colspan="3">
                                                                                <asp:DropDownList ID="ddlLista" runat="server" CssClass="cadddl" Visible="false"
                                                                                    Enabled="False">
                                                                                </asp:DropDownList>
                                                                              </td>
                                                                            <td>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="cadlbl">
                                                                                <asp:Label ID="lblNomeVisitante" runat="server" Text="Nome Visitante:"></asp:Label>
                                                                                <asp:HiddenField ID="HiddenVisitante" runat="server" />
                                                                            </td>
                                                                            <td>
                                                                            </td>
                                                                            <td>
                                                                            </td>
                                                                            <td>
                                                                            </td>
                                                                            <td class="cadlbl" style="width: 153px">
                                                            <asp:Label ID="lblTipoDocumento" runat="server" Text="Tipo Documento:"></asp:Label>
                                                        </td>
                                                                            <td class="cadlbl">
                                                                                <asp:Label ID="lblDocumento" runat="server" Text="Nº Documento:"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="3">
                                                                                <asp:TextBox ID="txtNomeVisitanteCad" runat="server" CssClass="cadtxt" MaxLength="50"
                                                                                    Width="385px" Enabled="False"></asp:TextBox><asp:ImageButton ID="btnListarVisitante"
                                                                                        runat="server" ImageUrl="~/Imagens/icones/ico_Visualizar.gif" AlternateText="Buscar"
                                                                                        OnClick="btnListarVisitante_Click1" ValidationGroup="Buscar" />
                                                                            </td>
                                                                            <td>
                                                                            </td>
                                                                            <td style="width: 153px">
                                                                                <asp:DropDownList ID="ddlTipoDocumento" runat="server" CssClass="cadddl" AutoPostBack="True"
                                                                                    Enabled="False" OnSelectedIndexChanged="ddlTipoDocumento_SelectedIndexChanged">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                            <td>
                                                                                <cc1:FWMascara ID="txtDocumento" runat="server" AutoPostBack="True" CssClass="cadtxt"
                                                                                    Enabled="False" Mascara="NENHUMA" MaxLength="15" Width="99px"></cc1:FWMascara>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="cadlbl" style="height: 21px">
                                                                                <asp:Label ID="lblEmpresaCad" runat="server" Text="Empresa:"></asp:Label>
                                                                            </td>
                                                                            <td style="height: 21px">
                                                                            </td>
                                                                            <td class="cadlbl" style="height: 21px">
                                                                                <asp:Label ID="lblOutraEmpresa" runat="server" Text="Outra Empresa:" Visible="false"></asp:Label>
                                                                            </td>
                                                                            <td style="height: 21px">
                                                                            </td>
                                                                            <td style="width: 153px; height: 21px;">
                                                                            </td>
                                                                            <td style="height: 21px">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="height: 25px; width: 177px;">
                                                                                <asp:DropDownList ID="ddlEmpresaCad" runat="server" CssClass="cadddl" OnSelectedIndexChanged="ddlEmpresaCad_SelectedIndexChanged"
                                                                                    Width="269px" AutoPostBack="True" Enabled="False">
                                                            </asp:DropDownList>
                                                        </td>
                                                                            <td>
                                                                            </td>
                                                                            <td colspan="4">
                                                                                <asp:TextBox ID="txtOutraEmpresa" runat="server" CssClass="cadtxt" Visible="false"
                                                                                    Width="250px"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="7" class="cadlbl">
                                                                                <br />
                                                                                <table align="center">
                                                                                    <tr>
                                                                                        <td class="cadlbl">
                                                                                            <asp:Label ID="lblDadosVeículo" runat="server" Text="Dados Veículo">
                                                                                            </asp:Label>
                                                                                            <table style="border: 1px solid black" width="740">
                                                                                                <tr>
                                                                                                    <td class="cadlbl" style="height: 19px; width: 114px;">
                                                                                    <asp:Label ID="lblEstado" runat="server" Text="Estado:"></asp:Label>
                                                                                </td>
                                                                                                    <td class="cadlbl" style="height: 19px">
                                                                                    <asp:Label ID="lblPlaca" runat="server" Text="Placa:"></asp:Label>
                                                                                </td>
                                                                                <td class="cadlbl" style="height: 19px">
                                                                                    <asp:Label ID="lblListaVeiculos" runat="server" Text="Lista de Veículos:"></asp:Label>
                                                                                </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td width="200">
                                                                                                        <asp:DropDownList ID="ddlEstado" runat="server" CssClass="cadddl" AutoPostBack="True"
                                                                                                            OnSelectedIndexChanged="ddlEstado_SelectedIndexChanged">
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                                                    <td>
                                                                                                        <asp:DropDownList ID="ddlPlaca" runat="server" CssClass="cadddl" AutoPostBack="True"
                                                                                                            Enabled="False" OnSelectedIndexChanged="ddlPlaca_SelectedIndexChanged">
                                                                                                        </asp:DropDownList>
                                                                                                        <asp:TextBox ID="txtPlaca" runat="server" CssClass="cadtxt" MaxLength="7" Width="87px"
                                                                                                            Visible="False"></asp:TextBox>
                                                                                                    </td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="ddlListaVeiculos" runat="server" CssClass="cadddl" AutoPostBack="True"
                                                                                        Enabled="True" OnSelectedIndexChanged="ddlListaVeiculos_SelectedIndexChanged">
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="cadlbl">
                                                                                            <br />
                                                                                            <asp:Label ID="lblDadosVisitado" runat="server" Text="Dados Visitado/ Acompanhante"></asp:Label>
                                                                                            <rada:RadAjaxPanel ID="RadAjaxPanelVisitado" runat="server" ClientEvents-OnRequestStart="OnRequestStart"
                                                                                                LoadingPanelID="lpaCadastro">
                                                                                                <asp:Panel ID="pnlListaVisitado" runat="server">
                                                                                                </asp:Panel>
                                                                                                <table style="border: 1px solid black" width="740">
                                                                                                    <tr>
                                                                                                        <td class="cadlbl" style="width: 446px">
                                                                                                            <asp:Label ID="lblNomeVisitado" runat="server" Text="Nome Visitado/ Acompanhante:"></asp:Label>
                                                                                                            <asp:HiddenField ID="HiddenVisitado" runat="server" />
                                                                                                        </td>
                                                                                                        <td width="5">
                                                                                                        </td>
                                                                                                        <td class="cadlbl" width="300">
                                                                                                            <asp:Label ID="lblREVisitado" runat="server" Text="RE:"></asp:Label>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="width: 446px">
                                                                                                            <asp:TextBox ID="txtNomeVisitadoCad" runat="server" CssClass="cadtxt" MaxLength="50"
                                                                                                                Width="385px" Enabled="False"></asp:TextBox><asp:ImageButton ID="btnListarVisitado"
                                                                                                                    runat="server" ImageUrl="~/Imagens/icones/ico_Visualizar.gif" AlternateText="Buscar"
                                                                                                                    OnClick="btnListarVisitado_Click" />
                                                                                                            <asp:RequiredFieldValidator ID="rfvNomeVisitado" runat="server" ControlToValidate="txtNomeVisitadoCad"
                                                                                                                ErrorMessage="Campo Obrigatório: Nome Visitado." ValidationGroup="Adicionar"
                                                                                                                SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:TextBox ID="txtREVisitado" runat="server" CssClass="cadtxt" MaxLength="50" Width="85px"
                                                                                                                Height="20px" Enabled="false"></asp:TextBox>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </rada:RadAjaxPanel>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="7">
                                                                                <table align="center" border="0" cellpadding="0" cellspacing="0" width="740">
                                                                                    <tr>
                                                                                        <td align="right" class="backBarraBotoes">
                                                                                            <asp:Button ID="btnLimpar" runat="server" CssClass="cadbutton100" OnClick="btnLimpar_Click"
                                                                                                Text="Limpar" />
                                                                                            <asp:Button ID="btnAdicionar" runat="server" CssClass="cadbutton100" Text="Adicionar"
                                                                                                OnClick="btnAdicionar_Click" ValidationGroup="Adicionar" />
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="center" valign="middle" visible="false" colspan="7">
                                                                                <br />
                                                                                <rada:RadAjaxPanel ID="RadAjaxPanelGrid" runat="server" ClientEvents-OnRequestStart="OnRequestStart"
                                                                                    LoadingPanelID="lpaCadastro">
                                                                                    <asp:Panel ID="pnlLista" runat="server">
                                                                                    </asp:Panel>
                                                                                    <asp:HiddenField ID="HiddenLista" runat="server" />
                                                                                    <asp:HiddenField ID="HiddenColRetirados" runat="server" />
                                                                                </rada:RadAjaxPanel>
                                                                                <rad:RadGrid ID="radGridVisitantes" runat="server" AllowPaging="True" AllowSorting="True"
                                                                                    AutoGenerateColumns="False" CssClass="dtg" GridLines="None" Skin="None" Width="100%"
                                                                                    OnItemCommand="radGridVisitantes_ItemCommand" OnItemDataBound="radGridVisitantes_ItemDataBound"
                                                                                    OnNeedDataSource="radGridVisitantes_NeedDataSource">
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
                                                                                            <rad:GridBoundColumn DataField="CodVisitante" Visible="False" HeaderText="CodVisitante"
                                                                                                UniqueName="CodVisitante">
                                                                                            </rad:GridBoundColumn>
                                                                                            <rad:GridBoundColumn DataField="CodLista" Visible="False" HeaderText="CodLista" UniqueName="CodLista">
                                                                                            </rad:GridBoundColumn>
                                                                                            <rad:GridTemplateColumn DataField="NomeVisitante" SortExpression="NomeVisitante"
                                                                                                HeaderText="Visitante" UniqueName="LVisitante">
                                                                                                <ItemStyle Width="25px" />
                                                                                                <ItemTemplate>
                                                                                                    <asp:LinkButton ID="lnkVisitante" runat="server" CommandName="Lista" Width="75px"></asp:LinkButton>
                                                                                                </ItemTemplate>
                                                                                            </rad:GridTemplateColumn>
                                                                                            <rad:GridBoundColumn DataField="NomeVisitante" HeaderText="Nome Visitante" SortExpression="NomeVisitante"
                                                                                                UniqueName="NomeVisitante" Visible="false">
                                                                                            </rad:GridBoundColumn>
                                                                                            <rad:GridBoundColumn DataField="NumeroDocumento" HeaderText="Documento" SortExpression="NumeroDocumento"
                                                                                                UniqueName="NumeroDocumento">
                                                                                                <ItemStyle Width="10%"></ItemStyle>
                                                                                            </rad:GridBoundColumn>
                                                                                            <rad:GridBoundColumn DataField="InicioVisita" HeaderText="Data/Hora Início" SortExpression="InicioVisita"
                                                                                                UniqueName="InicioVisita" DataFormatString="{0:dd/MM/yyyy HH:mm}">
                                                                                                <ItemStyle Width="10%"></ItemStyle>
                                                                                            </rad:GridBoundColumn>
                                                                                            <rad:GridBoundColumn DataField="FimVisita" HeaderText="Data/Hora Fim" SortExpression="FimVisita"
                                                                                                UniqueName="FimVisita" DataFormatString="{0:dd/MM/yyyy HH:mm}">
                                                                                                <ItemStyle Width="10%"></ItemStyle>
                                                                                            </rad:GridBoundColumn>
                                                                                            <rad:GridBoundColumn DataField="AliasFilial" HeaderText="Filial" SortExpression="AliasFilial"
                                                                                                UniqueName="AliasFilial">
                                                                                                <ItemStyle Width="10%"></ItemStyle>
                                                                                            </rad:GridBoundColumn>
                                                                                            <rad:GridBoundColumn DataField="NomeArea" HeaderText="Áreas Visitadas" SortExpression="NomeArea"
                                                                                                UniqueName="NomeArea">
                                                                                                <ItemStyle Width="10%"></ItemStyle>
                                                                                            </rad:GridBoundColumn>
                                                                                            <rad:GridBoundColumn DataField="PlacaVeiculo" HeaderText="Veículo" SortExpression="PlacaVeiculo"
                                                                                                UniqueName="PlacaVeiculo">
                                                                                                <ItemStyle Width="10%"></ItemStyle>
                                                                                            </rad:GridBoundColumn>
                                                                                            <rad:GridBoundColumn DataField="MotivoVisita" HeaderText="Motivo Visita" SortExpression="MotivoVisita"
                                                                                                UniqueName="MotivoVisita">
                                                                                                <ItemStyle Width="10%"></ItemStyle>
                                                                                            </rad:GridBoundColumn>
                                                                                            <rad:GridBoundColumn DataField="Observacao" HeaderText="Observação" SortExpression="Observacao"
                                                                                                UniqueName="Observacao">
                                                                                                <ItemStyle Width="10%"></ItemStyle>
                                                                                            </rad:GridBoundColumn>
                                                                                            <rad:GridTemplateColumn UniqueName="TemplateColumn" Visible="False">
                                                                                                <ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
                                                                                                <ItemTemplate>
                                                                                                    <asp:ImageButton ID="btnEditar" runat="server" AlternateText="Editar" CausesValidation="False"
                                                                                                        CommandArgument='<%# DataBinder.Eval(Container.DataItem, "CodSolicitacao") %>'
                                                                                                        CommandName="Editar" ImageUrl="%%PATH%%/imagens/icones/ico_editar.gif" ToolTip="Editar">
                                                                                                    </asp:ImageButton>
                                                                                                </ItemTemplate>
                                                                                            </rad:GridTemplateColumn>
                                                                                            <rad:GridButtonColumn CommandName="Excluir" UniqueName="Ativar" ButtonType="ImageButton"
                                                                                                ImageUrl="%%PATH%%/imagens/icones/ico_deletar.gif" Text="Excluir">
                                                                                                <ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
                                                                                            </rad:GridButtonColumn>
                                                                                        </Columns>
                                                                                        <RowIndicatorColumn Visible="False">
                                                                                            <HeaderStyle Width="0px" />
                                                                                        </RowIndicatorColumn>
                                                                                        <ExpandCollapseColumn Resizable="False" Visible="False">
                                                                                            <HeaderStyle Width="0px" />
                                                                                        </ExpandCollapseColumn>
                                                                                    </MasterTableView>
                                                                                </rad:RadGrid>
                                                                                <!-- ********************* END: RADGRID ************************** -->
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="cadlbl">
                                                                    <br />
                                                                    <asp:Label ID="lblDadosAporvador" runat="server" Text="Dados Aprovador"></asp:Label>
                                                                    <table style="border: 1px solid black" width="740">
                                                                        <tr>
                                                                            <td class="cadlbl" width="500">
                                                                                <asp:Label ID="lblAprovadorCad" runat="server" Text="Aprovador:"></asp:Label>
                                                                            </td>
                                                                            <td width="5">
                                                                            </td>
                                                                            <td class="cadlbl">
                                                                                <asp:Label ID="lblREAprovador" runat="server" Text="RE:"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:DropDownList ID="ddlAprovadorCad" runat="server" CssClass="cadddl" Width="449px"
                                                                                    Enabled="False" AutoPostBack="True" OnSelectedIndexChanged="ddlAprovadorCad_SelectedIndexChanged">
                                                                                </asp:DropDownList>
                                                                                <asp:RequiredFieldValidator ID="rfvAprovador" runat="server" ControlToValidate="ddlAprovadorCad"
                                                                ErrorMessage="Campo Obrigatório: Tipo Visitante." InitialValue="0" ValidationGroup="Enviar">*</asp:RequiredFieldValidator>
                                                        </td>
                                                                            <td>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtREAprovador" runat="server" CssClass="cadtxt" Enabled="false"
                                                                                    Height="20px" MaxLength="50" Width="85px"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </rada:RadAjaxPanel>
                                                </td>
                                            </tr>
                                        </table>
                                        <!-- ********************* START: BARRA DE BOTÕES INFERIOR ************************** -->
                                        <table align="center" border="0" cellpadding="0" cellspacing="0" width="760">
                                            <caption>
                                            </caption>
                                            <tr>
                                                <td align="right" class="backBarraBotoes">
                                                    &nbsp;<asp:Button ID="btnGravar" runat="server" CssClass="cadbutton100" Text="Enviar Solicitação"
                                                        OnClick="btnGravar_Click" ValidationGroup="Enviar" />
                                                    &nbsp;
                                                    <asp:Button ID="btnVoltar" runat="server" CausesValidation="False" CssClass="cadbutton100"
                                                        Text="Voltar" Width="50px" OnClick="btnVoltar_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                        <!-- ********************* END: BARRA DE BOTÕES INFERIOR ************************** -->
                                    </td>
                                </tr>
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
                </td>
                <!-- ********************* END: CONTEÚDO ****************** -->
            </tr>
            <tr>
                <td style="width: 1010px">
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
