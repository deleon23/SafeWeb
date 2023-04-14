<%@ Page Language="C#" AutoEventWireup="true" Codebehind="CadSolicitacaoPermissaoCrachaTitular.aspx.cs"
    Inherits="SafWeb.UI.Modulos.Solicitacao.CadSolicitacaoPermissaoCrachaTitular" %>

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
        
        function FormataData(e,obj)
        {
        //usar no evento keypress
        //bloqueia caracteres alfa e coloca as barras nas posições        
    
 
            var key = window.event ? e.keyCode : e.which;
            var keychar = String.fromCharCode(key); 
            var vrRetorno = false; 
 
            //Backspace e Tab
            if(key != 8 && key != 0)
            {  
                goodChars = "0123456789";
                if (goodChars.indexOf(keychar) != -1) 
                {     
                    if((obj.value.length == 2 || obj.value.length == 5) && key != 8)
                    {
                        obj.value += "/";      
                    }
                    if(obj.value.length == 10 && key != 8)
                    {
                        obj.value += " ";
                    }
                    if(obj.value.length == 13 && key != 8)
                    {
                        obj.value += ":";
                    }
                    vrRetorno = true;
                }
            }
            else
            {
                
                vrRetorno = true;
            }
 
            return vrRetorno;
        }

        function PreencherHiddenVisitante(codigo)
		{
		    document.getElementById("HiddenColaborador").value = codigo

		    AjaxNS.AR('Visitado', '', 'RadAjaxPanelColaborador');
		}
		
		function blocTexto(valor)
	    {
		    quant = 199;
		    total = valor.length;
		    if(total > quant)
		    {
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
                                        <asp:Label ID="lblListagem" runat="server" Text="Listagem de Permissão do Crachá Titular"></asp:Label>
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
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td>
                                                <cc1:FWMascara ID="txtNumSolicitacao" runat="server" CssClass="cadtxt" Mascara="NUMERO"
                                                    MaxLength="5" Width="75px"></cc1:FWMascara></td>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlRegional" Enabled="false" CssClass="cadddl" runat="server" Width="150px"
                                                        OnSelectedIndexChanged="ddlRegional_SelectedIndexChanged" AutoPostBack="True">
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                </td>
                                                <td style="width: 215px">
                                                    <asp:DropDownList ID="ddlFilial" Enabled="false" CssClass="cadddl" runat="server" Width="150px">
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td class="cadlbl">
                                                    <asp:Label ID="lblStatus" runat="server" Text="Status:"></asp:Label></td>
                                                <td>
                                                </td>
                                                <td class="cadlbl">
                                                    <asp:Label ID="lblDataInicio" runat="server" Text="Data Início:"></asp:Label></td>
                                                <td>
                                                </td>
                                                <td class="cadlbl" style="width: 215px">
                                                    <asp:Label ID="lblDataFim" runat="server" Text="Data Fim:"></asp:Label></td>
                                                <td>
                                                </td>
                                                <td class="cadlbl">
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td style="height: 21px">
                                                    <asp:DropDownList ID="ddlStatus" CssClass="cadddl" runat="server" Width="150px">
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="height: 21px">
                                                </td>
                                                <td style="height: 21px">
                                                <cc1:FWMascara ID="txtDataInicio" runat="server" AccessKey="I" CssClass="cadtxt"
                                                    Mascara="DATA" MaxLength="10" Width="100px"></cc1:FWMascara><a hidefocus="" href="javascript:void(0)"
                                                        onclick="if(self.gfPopComp)gfPopComp.fStartPop(document.forms[0].all('<% = txtDataInicio.ClientID.ToString() %>'),document.forms[0].all('<% = txtDataInicio.ClientID.ToString() %>'));return false;">
                                                        <img align="absMiddle" alt="" border="0" height="19" name="popcal" src="%%PATH%%/agenda/calbtn.gif"
                                                            width="20" /></a><asp:CompareValidator ID="cpvtxtInicioPeriodo" runat="server" ControlToValidate="txtDataInicio"
                                                    Display="Dynamic" ErrorMessage="- Data Inválida: Data Início"
                                                    Operator="DataTypeCheck" Type="Date">*</asp:CompareValidator>
                                                <asp:CompareValidator ID="cpvDataInicialXFinal" runat="server" ControlToCompare="txtDataFim"
                                                    ControlToValidate="txtDataInicio" ErrorMessage="- A data início deve ser menor que a data fim."
                                                    Operator="LessThanEqual">*</asp:CompareValidator>
                                                </td>
                                                <td style="height: 21px">
                                                </td>
                                                <td style="width: 215px; height: 21px;">
                                                <a hidefocus="" href="javascript:void(0)"
                                                        onclick="if(self.gfPopComp)gfPopComp.fStartPop(document.forms[0].all('<% = txtDataInicio.ClientID.ToString() %>'),document.forms[0].all('<% = txtDataInicio.ClientID.ToString() %>'));return false;">&nbsp;</a><cc1:FWMascara ID="txtDataFim" runat="server" AccessKey="F" CssClass="cadtxt" Mascara="DATA"
                                                                            MaxLength="10" Width="100px"></cc1:FWMascara><a hidefocus="" href="javascript:void(0)"
                                                                                onclick="if(self.gfPopComp)gfPopComp.fEndPop(document.forms[0].all('<% = txtDataInicio.ClientID.ToString() %>'),document.forms[0].all('<% = txtDataFim.ClientID.ToString() %>'));return false;">
                                                                                <img align="absMiddle" alt="" border="0" height="19" name="popcal" src="%%PATH%%/agenda/calbtn.gif"
                                                                                    width="20" /></a><asp:CompareValidator ID="cpvtxtFimPeriodo" runat="server" ControlToValidate="txtDataFim"
                                                                             Display="Dynamic" ErrorMessage="- Data Inválida: Data fim"
                                                                            Operator="DataTypeCheck" Type="Date">*</asp:CompareValidator>
                                                </td>
                                                <td style="height: 21px">
                                                </td>
                                                <td style="height: 21px">
                                                                        <a hidefocus="" href="javascript:void(0)"
                                                                                onclick="if(self.gfPopComp)gfPopComp.fEndPop(document.forms[0].all('<% = txtDataInicio.ClientID.ToString() %>'),document.forms[0].all('<% = txtDataFim.ClientID.ToString() %>'));return false;">&nbsp;</a></td>
                                            </tr>
                                            <tr>
                                                <td class="cadlbl" colspan="3">
                                                    <asp:Label ID="lblColaborador" runat="server" Text="Colaborador:"></asp:Label>
                                                </td>
                                                <td>
                                                </td>
                                                <td class="cadlbl" colspan="3">
                                                    <asp:Label ID="lblAprovador" runat="server" Text="Aprovador:"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td colspan="3" style="height: 27px">
                                                    <asp:TextBox ID="txtNomeColaborador" runat="server" CssClass="cadtxt" MaxLength="50"
                                                        Width="340px"></asp:TextBox></td>
                                                <td style="height: 27px">
                                                </td>
                                                <td colspan="3" style="height: 27px">
                                                    <asp:TextBox ID="txtNomeAprovador" runat="server" CssClass="cadtxt" MaxLength="50"
                                                        Width="335px"></asp:TextBox></td>
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
                                                    </asp:DropDownList></td>
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
                                        <!-- ********************** END: FILTROS ************************** -->

                                        <table class="MarginTopConteudo" cellspacing="0" cellpadding="0" width="740" align="center"
                                            border="0">
                                            <tr>
                                                <td align="center" colspan="3">
                                                    <!-- ********************* STAR: RADGRID ************************** -->
                                                    <rad:RadGrid ID="radGridSolicitacao" runat="server" AllowPaging="True" AllowSorting="True"
                                                        AutoGenerateColumns="False" SortingSettings-SortToolTip="Clique para filtrar"  CssClass="dtg" GridLines="None" Skin="None" Width="100%"
                                                        OnItemCommand="radGridSolicitacao_ItemCommand" OnItemDataBound="radGridSolicitacao_ItemDataBound"
                                                        OnNeedDataSource="radGridSolicitacao_NeedDataSource">
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
                                                                <rad:GridBoundColumn DataField="Id_SolicitacaoCrachaTitular" HeaderText="Nº Solicitação" SortExpression="Id_SolicitacaoCrachaTitular"
                                                                    UniqueName="Id_SolicitacaoCrachaTitular">
                                                                    <ItemStyle Width="5%"></ItemStyle>
                                                                </rad:GridBoundColumn>
                                                                <rad:GridBoundColumn DataField="Nom_Colaborador" HeaderText="Colaborador" SortExpression="Nom_Colaborador"
                                                                    UniqueName="Nom_Colaborador" AllowFiltering="true">
                                                                    <ItemStyle Width="25%"></ItemStyle>
                                                                </rad:GridBoundColumn>
                                                                <rad:GridBoundColumn DataField="Data_Solicitacao" HeaderText="Data Solicitação" SortExpression="Data_Solicitacao"
                                                                    UniqueName="Data_Solicitacao" DataFormatString="{0:dd/MM/yyyy}">
                                                                    <ItemStyle Width="5%"></ItemStyle>
                                                                </rad:GridBoundColumn>

                                                                <rad:GridBoundColumn DataField="Alias_Filial" HeaderText="Filial" SortExpression="Alias_Filial"
                                                                    UniqueName="Alias_Filial">
                                                                    <ItemStyle Width="20%" HorizontalAlign="Center"></ItemStyle>
                                                                </rad:GridBoundColumn>

                                                                <rad:GridBoundColumn DataField="Des_Area" HeaderText="Áreas Solicitadas" SortExpression="Des_Area"
                                                                    UniqueName="Des_Area">
                                                                    <ItemStyle Width="25%" HorizontalAlign="Center"></ItemStyle>
                                                                </rad:GridBoundColumn>

                                                                <rad:GridBoundColumn DataField="Des_StatusAprovacao" HeaderText="Status" SortExpression="Des_StatusAprovacao"
                                                                    UniqueName="Des_StatusAprovacao">
                                                                    <ItemStyle Width="20%"></ItemStyle>
                                                                </rad:GridBoundColumn>

                                                                <rad:GridBoundColumn DataField="Id_StatusSolicitacao" SortExpression="Id_StatusSolicitacao" UniqueName="Id_StatusSolicitacao"
                                                                    Visible="false" />
                                                                <rad:GridBoundColumn DataField="Id_UsuarioSolicitante" SortExpression="Id_UsuarioSolicitante" UniqueName="Id_UsuarioSolicitante"
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
                                        <asp:Label ID="Label2" runat="server" Text="Cadastro de Permissão do Crachá Titular"></asp:Label>
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
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <rada:RadAjaxPanel ID="RadAjaxPanelColaborador" runat="server" ClientEvents-OnRequestStart="OnRequestStart"
                                                        LoadingPanelID="lpaCadastro">
                                                        <asp:Panel ID="pnlCadColaborador" runat="server" />
                                                        <asp:Label ID="Label7" class="cadlbl" runat="server" Text="Dados da Permissão do Crachá Titular"></asp:Label>

															<table>
                                                            <tr>
                                                                <td class="cadlbl">
                                                                    <table style="border: 1px solid black" width="740">

                                                                        <tr>

                                                                            <td class="cadlbl">
			                                                                    <asp:Label ID="lblListaColaboradores" runat="server" Text="Lista de Colaboradores:"></asp:Label>
			                                                                    <asp:ImageButton ID="ImageButton1" CssClass="BotaoAdicionar" runat="server" ImageUrl="~/Imagens/icones/ico_btn_adiconar.png"
			                                                                        OnClick="btnAdicionarColaborador_Click" CausesValidation="False" ToolTip="Adicionar Colaboradores" />
			                                                                    <asp:ImageButton ID="btnRemoverColaborador" CssClass="BotaoAdicionar" runat="server" ImageUrl="~/Imagens/icones/ico_btn_remover.png"
			                                                                        CausesValidation="False" ToolTip="Remover Colaborador(es)" OnClick="btnRemoverColaborador_Click" />
			                                                                    
                                                                            </td>
                                                                            <td class="cadlbl">
                                                                                &nbsp;</td>
                                                                            <td class="cadlbl" colspan="2">
                                                                                <asp:Label ID="lblRegionalCad" runat="server" Text="Regional:"></asp:Label>
                                                                            </td>
                                                                            <td  class="cadlbl">
                                                                                <asp:Label ID="lblFilialCad" runat="server" Text="Filial:"></asp:Label>
                                                                            </td>
                                                                            <td class="cadlbl">
                                                                                &nbsp;
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td rowspan="3" class="cadlbl">
			                                                                    <asp:ListBox ID="lstColaboradores" runat="server" Height="206px" Width="300px" SelectionMode="Multiple"
			                                                                        CssClass="cadlstBox"></asp:ListBox>

                                                                                    <asp:HiddenField ID="HiddenColaborador" runat="server" />
                                                                			</td>
                                                                            <td>
                                                                                &nbsp;
                                                                            </td>
                                                                            <td colspan="2" class="cadlbl">
                                                                                <asp:DropDownList ID="ddlRegionalCad" Enabled="false" runat="server" CssClass="cadddl" OnSelectedIndexChanged="ddlRegionalCad_SelectedIndexChanged"
                                                                                    AutoPostBack="True">
                                                                                </asp:DropDownList>
                                                                                <asp:RequiredFieldValidator ID="rfvRegional" runat="server" ControlToValidate="ddlRegionalCad"
                                                                                    ErrorMessage="Campo Obrigatório: Regional." ValidationGroup="Cadastro" InitialValue="0"
                                                                                    SetFocusOnError="True">*</asp:RequiredFieldValidator></td>
                                                                            <td>
                                                                                <asp:DropDownList ID="ddlFilialCad" Enabled="false" runat="server" CssClass="cadddl" AutoPostBack="True"
                                                                                    OnSelectedIndexChanged="ddlFilialCad_SelectedIndexChanged">
                                                                                </asp:DropDownList>
                                                                                <asp:RequiredFieldValidator ID="rfvFilial" runat="server" ControlToValidate="ddlFilialCad"
                                                                                    ErrorMessage="Campo Obrigatório: Filial." ValidationGroup="Cadastro" InitialValue="0"
                                                                                    SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                                                            </td>
                                                                            <td>
                                                                                &nbsp;
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 54px">
                                                                            &nbsp;</td>
                                                                            <td style="width: 54px" class="cadlbl">
                                                                                <asp:Label ID="lblAreaVisitada" runat="server" Text="Áreas:"></asp:Label>
                                                                            </td>
                                                                            <td class="cadlbl">
                                                                                &nbsp;</td>
                                                                            <td class="cadlbl">
                                                                                <asp:Label ID="lblAreaSelecionada" runat="server" Text="Áreas Selecionadas:"></asp:Label>
                                                                            </td>
                                                                            <td class="cadlbl">
                                                                                &nbsp;</td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                &nbsp;
                                                                            </td>
                                                                            <td class="cadlbl">
                                                                                <asp:ListBox ID="lstAreaVisita" runat="server" Height="140px" Width="145px" CssClass="cadlstBox"
                                                                                    SelectionMode="Multiple"></asp:ListBox>
                                                                            </td>
                                                                            <td valign="top" class="cadlbl">
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
                                                                            <td class="cadlbl">
                                                                                <asp:ListBox ID="lstAreaSelecionada" runat="server" Height="140px" Width="145px"
                                                                                    CssClass="cadlstBox" SelectionMode="Multiple"></asp:ListBox>

                                                                            </td>
                                                                            <td valign="top">
                                                                                &nbsp;
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="cadlbl" style="width: 168px; height: 21px;">
                                                                                <asp:Label ID="lblObservacao" runat="server" Text="Observação:"></asp:Label>
                                                                            </td>
                                                                            <td style="width: 54px; height: 21px;">
                                                                            	&nbsp;
                                                                            </td>
                                                                            <td style="width: 54px; height: 21px;">
                                                                            </td>
                                                                            <td class="cadlbl" style="height: 21px">
                                                                                &nbsp;
                                                                            </td>
                                                                            <td style="height: 21px">
                                                                            	&nbsp;
                                                                            </td>
                                                                            <td class="cadlbl" style="height: 21px">
                                                                            	&nbsp;
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="cadlbl">
                                                                                <asp:TextBox ID="txtObservacao" runat="server" CssClass="cadlstBox" MaxLength="10"
                                                                                    Width="300px" Height="80px" TextMode="MultiLine"></asp:TextBox>

                                                                            </td>
                                                                            <td style="width: 54px">
                                                                            	&nbsp;
                                                                            </td>
                                                                            <td style="width: 54px">
                                                                            </td>
                                                                            <td class="cadlbl">
                                                                               
                                                                            </td>
                                                                            <td>
                                                                            	&nbsp;
                                                                            </td>
                                                                            <td>
                                                                                &nbsp;
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 168px">
                                                                                &nbsp;</td>
                                                                            <td style="width: 54px">
                                                                            	&nbsp;</td>
                                                                            <td style="width: 54px">
                                                                            &nbsp;</td>
                                                                            <td>
                                                                                &nbsp;</td>
                                                                            <td>
                                                                            	&nbsp;</td>
                                                                            <td>
                                                                                &nbsp;</td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="6">
                                                                                <table align="center" border="0" cellpadding="0" cellspacing="0" width="740">
                                                                                    <tr>
                                                    
                                                                                        <asp:Label CssClass="cadlbl" ID="Label8" ForeColor="Red" runat="server" Text="Atenção: O cadastro desta permissão substitui a permissão atual do colaborador."></asp:Label>
                                                                                        <asp:ValidationSummary  CssClass="cadlbl" ID="ValidationSummary2" runat="server" DisplayMode="List" ValidationGroup="Cadastro" />
                                                                                                                                            
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
                                                                            <td style="width: 168px">
                                                                                &nbsp;</td>
                                                                            <td style="width: 54px">
                                                                            	&nbsp;</td>
                                                                            <td style="width: 54px">
                                                                            &nbsp;</td>
                                                                            <td>
                                                                                &nbsp;</td>
                                                                            <td>
                                                                            	&nbsp;</td>
                                                                            <td>
                                                                                &nbsp;</td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="6" align="center">
                                                                                <rad:RadGrid ID="radGridColaboradores" runat="server" AllowPaging="True" AllowSorting="True"
                                                                                    AutoGenerateColumns="False" CssClass="dtg" GridLines="None" Skin="None" Width="100%"
                                                                                    SortingSettings-SortToolTip="Clique para filtrar" OnItemCommand="radGridColaboradores_ItemCommand" OnItemDataBound="radGridColaboradores_ItemDataBound"
                                                                                    OnNeedDataSource="radGridColaboradores_NeedDataSource">
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
                                                                                                        <asp:Panel ID="pnlPaginaAtual0" runat="server" DefaultButton="btnIr">
                                                                                                            <asp:ImageButton ID="imgPrimeira0" runat="server" CausesValidation="false" CommandArgument="First"
                                                                                                                CommandName="Page" CssClass="pagImg" ImageUrl="~/imagens/icones/ico_setaesq.gif" />
                                                                                                            <asp:LinkButton ID="LinkButton5" runat="server" CausesValidation="False" CommandArgument="First"
                                                                                                                CommandName="Page" CssClass="pagLink" Enabled="True">Primeira</asp:LinkButton>
                                                                                                            <asp:ImageButton ID="imgAnterior0" runat="server" CausesValidation="false" CommandArgument="Prev"
                                                                                                                CommandName="Page" CssClass="pagImg" ImageUrl="~/imagens/icones/ico_setaesq2.gif" />
                                                                                                            <asp:LinkButton ID="LinkButton6" runat="server" CausesValidation="False" CommandArgument="Prev"
                                                                                                                CommandName="Page" CssClass="pagLink" Enabled="True">Anterior</asp:LinkButton>&#160;&#160;
                                                                                                            <asp:Label ID="Label4" runat="server" CssClass="paglbl">| Página:</asp:Label>
                                                                                                            <cc1:FWMascara ID="FWMascara2" runat="server" AutoPostBack="False" CssClass="pagtxtbox"
                                                                                                                Mascara="NUMERO" MaxLength="4" Text='<%# (int)(DataBinder.Eval(Container, "OwnerTableView.CurrentPageIndex")) + 1 %>'
                                                                                                                Width="50px"></cc1:FWMascara>
                                                                                                            <asp:Label ID="Label5" runat="server" CssClass="paglbl">de</asp:Label>
                                                                                                            <asp:Label ID="Label6" runat="server" CssClass="paglbl" Enabled="True"><%#DataBinder.Eval(Container, "Paging.PageCount").ToString()%></asp:Label>
                                                                                                            <asp:LinkButton ID="btnIr0" runat="server" CommandName="IrPagina" CssClass="pagLink">Ir</asp:LinkButton>
                                                                                                            <span class="paglbl">&#160;|&#160;</span>
                                                                                                            <asp:LinkButton ID="LinkButton7" runat="server" CausesValidation="False" CommandArgument="Next"
                                                                                                                CommandName="Page" CssClass="pagLink" Enabled="True">Próxima</asp:LinkButton>
                                                                                                            <asp:ImageButton ID="imgProxima0" runat="server" CausesValidation="false" CommandArgument="Next"
                                                                                                                CommandName="Page" CssClass="pagImg" ImageUrl="~/imagens/icones/ico_setadir2.gif" />
                                                                                                            <asp:LinkButton ID="LinkButton8" runat="server" CausesValidation="False" CommandArgument="Last"
                                                                                                                CommandName="Page" CssClass="pagLink" Enabled="True">Última</asp:LinkButton>
                                                                                                            <asp:ImageButton ID="imgUltima0" runat="server" CausesValidation="false" CommandArgument="Last"
                                                                                                                CommandName="Page" CssClass="pagImg" ImageUrl="~/imagens/icones/ico_setadir.gif" />
                                                                                                        </asp:Panel>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </PagerTemplate>
                                                                                        <Columns>
                                                                                            <rad:GridBoundColumn DataField="Id_Colaboradores" Visible="False" HeaderText="Id_Colaboradores"
                                                                                                UniqueName="Id_Colaboradores">
                                                                                            </rad:GridBoundColumn>
                                                                                            
                                                                                            <rad:GridBoundColumn DataField="Nom_Colaboradores" HeaderText="Colaboradores" SortExpression="Nom_Colaboradores"
                                                                                                UniqueName="Nom_Colaboradores">
                                                                                                <ItemStyle Width="10%"></ItemStyle>
                                                                                            </rad:GridBoundColumn>
 

                                                                                            <rad:GridBoundColumn DataField="Alias_Filial" HeaderText="Filial" SortExpression="Alias_Filial"
                                                                                                UniqueName="Alias_Filial">
                                                                                                <ItemStyle Width="10%"></ItemStyle>
                                                                                            </rad:GridBoundColumn>
                                                                                            <rad:GridBoundColumn DataField="Des_Area" HeaderText="Áreas Solicitadas" SortExpression="Des_Area"
                                                                                                UniqueName="Des_Area">
                                                                                                <ItemStyle Width="10%"></ItemStyle>
                                                                                            </rad:GridBoundColumn>

                                                                                            <rad:GridBoundColumn DataField="Des_MotivoSolicitacao" HeaderText="Motivo Solicitação" SortExpression="Des_MotivoSolicitacao"
                                                                                                UniqueName="Des_MotivoSolicitacao">
                                                                                                <ItemStyle Width="10%"></ItemStyle>
                                                                                            </rad:GridBoundColumn>
                               

                                                                                            <rad:GridTemplateColumn UniqueName="TemplateColumn" Visible="False">
                                                                                                <ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
                                                                                                <ItemTemplate>
                                                                                                    <asp:ImageButton ID="btnEditar" runat="server" AlternateText="Editar" CausesValidation="False"
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
                                                                                </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 168px">
                                                                                &nbsp;</td>
                                                                            <td style="width: 54px">
                                                                            	&nbsp;</td>
                                                                            <td style="width: 54px">
                                                                            &nbsp;</td>
                                                                            <td>
                                                                                &nbsp;</td>
                                                                            <td>
                                                                            	&nbsp;</td>
                                                                            <td>
                                                                                &nbsp;</td>
                                                                        </tr>
                                                                        </table>
                                                                </td>
                                                            </tr>



                                                            <tr>
                                                                <td class="cadlbl">
                                                                    <br />
                                                                    <asp:Label ID="lblDadosAporvador" runat="server" Text="Dados Aprovador"></asp:Label>
                                                                    <table style="border: 1px solid black; width: 748px;">
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
                                                                                    ErrorMessage="Campo Obrigatório: Tipo Visitante." InitialValue="0" ValidationGroup="Enviar">*</asp:RequiredFieldValidator></td>
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
