<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CadAtributosAprovador.aspx.cs" Inherits="SafWeb.UI.Admin.CadAtributosAprovador" %>
<%@ Register TagPrefix="rada" Namespace="Telerik.WebControls" Assembly="RadAjax.Net2" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.WebControls" Assembly="RadGrid.Net2" %>
<%@ Register TagPrefix="cc1" Namespace="FrameWork.WebControl" Assembly="FrameWork.WebControl" %>
<%@ Register TagPrefix="radc" Namespace="Telerik.WebControls" Assembly="RadCalendar.Net2" %>

<html>
<head runat="server">
    <link href="../../Estilos/Brinks.css" rel="stylesheet" type="text/css" />   
    <link href="../../Estilos/FrameWork.css" type="text/css" rel="stylesheet">   


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

        function FormataCodigo(e, obj) {
            //usar no evento keypress
            //bloqueia caracteres alfa

            var key = window.event ? e.keyCode : e.which;
            var keychar = String.fromCharCode(key);
            var vrRetorno = false;

            //Backspace e Tab
            if (key != 8 && key != 0) 
            {
                goodChars = "0123456789";

                if (goodChars.indexOf(keychar) != -1) 
                {
                    vrRetorno = true;
                }
            }
            else 
            {
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
        
    </script>


</head>
<body>
    <form id="form" method="post" runat="server">
        <table cellspacing="0" cellpadding="0" width="778" align="center" border="0">
            <tr>
                <td style="width: 1010px">
                    <!-- ********************* START: CABEÇALHO ****************** -->
                    <cc1:FWServerControl ID="FWServerControlCabecalho" runat="server" Controle="Pagebanner"
                        Arquivo="/Admin/Framework/CabecalhoAdm.ascx"></cc1:FWServerControl>
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
                                    <td class="cadBarraTitulo" align="left" style="height: 25px; width: 750px">
                                        <img src="../../Imagens/comum/bulletbarra.gif" align="absMiddle">
                                        <asp:Label ID="Label1" runat="server" Text="Listagem de atributos de aprovadores"></asp:Label>
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
                                                <td class="cadlbl" style="width: 230px">
                                                    <asp:Label ID="lblRegionalList" runat="server" Text="Regional:"></asp:Label>
                                                </td>
                                                <td class="cadlbl">
                                                    <asp:Label ID="lblFilialList" runat="server" Text="Filial:"></asp:Label>
                                                </td> 
                                            </tr>
                                            <tr>
                                                <td style="height: 19px; width: 230px;">
                                                    <asp:DropDownList ID="ddlRegionalList" runat="server" CssClass="cadddl" 
                                                        AutoPostBack="true" Width="150px" OnSelectedIndexChanged="ddlRegionalList_SelectedIndexChanged" Enabled="true">
                                                    </asp:DropDownList>                                                    
                                                </td>
                                                <td style="height: 19px">
                                                    <asp:DropDownList ID="ddlFilialList" runat="server" CssClass="cadddl" AutoPostBack="false"
                                                        Enabled="false" Width="150px">
                                                    </asp:DropDownList>                                                    
                                                </td>  
                                            </tr>    
                                            
                                            
                                            
                                            <tr>
                                                <td class="cadlbl" style="width: 230px">
                                                    <asp:Label ID="Label8" runat="server" Text="Nome do usuário:"></asp:Label>
                                                </td>
                                                <td class="cadlbl">
                                                    <asp:Label ID="Label9" runat="server" Text="Superior hierárquico:"></asp:Label>
                                                </td> 
                                            </tr>
                                            <tr>
                                                <td style="height: 19px; width: 150px;">
                                                    <asp:TextBox ID="txtNomeUsuarioList" runat="server" MaxLength="50"
                                                         Width="150px" CssClass="cadtxt"/>
                                                </td>
                                                <td style="height: 19px">
                                                    <asp:DropDownList ID="ddlSuperiorHierarquicoList" runat="server" CssClass="cadddl" 
                                                        Width="234px">
                                                    </asp:DropDownList>                                                    
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
                                                    <rad:RadGrid ID="radGridUsuario" runat="server" AllowPaging="True" AllowSorting="True"
                                                        AutoGenerateColumns="False" CssClass="dtg" GridLines="None" Skin="None" Width="100%" 
                                                        OnItemCommand="radGridUsuario_ItemCommand" OnItemDataBound="radGridUsuario_ItemDataBound"
                                                        OnNeedDataSource="radGridUsuario_NeedDataSource">
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
                                                                                <asp:Label ID="Label5" runat="server" CssClass="paglbl" Enabled="True"><%#DataBinder.Eval(Container, "Paging.PageCount").ToString()%></asp:Label>
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
                                                                <rad:GridBoundColumn DataField="Id_Usuario" SortExpression="Id_Usuario"
                                                                    UniqueName="Id_Usuario" Visible="false">
                                                                    <ItemStyle Width="5%"></ItemStyle>
                                                                </rad:GridBoundColumn>
                                                                <rad:GridBoundColumn DataField="USU_C_NOME" HeaderText="Usuário" SortExpression="USU_C_NOME"
                                                                    UniqueName="USU_C_NOME">
                                                                    <ItemStyle Width="15%"></ItemStyle>
                                                                </rad:GridBoundColumn>  
                                                                <rad:GridBoundColumn DataField="USU_C_LOGIN" HeaderText="Login" SortExpression="USU_C_LOGIN"
                                                                    UniqueName="USU_C_LOGIN">
                                                                    <ItemStyle Width="15%"></ItemStyle>
                                                                </rad:GridBoundColumn>  
                                                                <rad:GridTemplateColumn Visible="False">
                                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="imgVisualizar" runat="server" ImageUrl="~/imagens/icones/ico_editar.gif"
                                                                            CommandName="Visualizar" Visible="false"></asp:ImageButton>
                                                                    </ItemTemplate>
                                                                </rad:GridTemplateColumn>
                                                                 <rad:GridTemplateColumn UniqueName="TemplateColumn" Visible="False">
                                                                    <ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="btnEditar" runat="server" AlternateText="Editar" CausesValidation="False"
                                                                            CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id_Usuario") %>' CommandName="Editar"
                                                                            ImageUrl="%%PATH%%/imagens/icones/ico_editar.gif" ToolTip="Editar"></asp:ImageButton>
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
                                        <!-- ********************* START: BARRA DE BOTÕES INFERIOR ************************** -->

                                        <!--
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
                                        -->

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
                                        <img src="../../Imagens/comum/bulletbarra.gif" align="absMiddle"/>

                                        <asp:Label ID="Label6" runat="server" Text="Cadastro de atributos de aprovador"></asp:Label>
                                    </td>
                                    <td class="cadBarraTitulo" align="right" height="25" style="width: 9px">
                                        <asp:ImageButton ID="btnHelpCad" runat="server" AlternateText="Ajuda" ImageUrl="%%PATH%%/imagens/icones/ico_help.jpg"
                                            ImageAlign="AbsMiddle" Visible="False" />
                                    </td>
                                </tr>
                                <!-- ***************** END: BARRA DE TITUTO ******************* -->
                                <tr>
                                    <td colspan="2" class="backboxconteudo" valign="top"  style="padding-top:10px;">
                                        <rada:RadAjaxPanel ID="RadAjaxPanel2" runat="server" ClientEvents-OnRequestStart="OnRequestStart"
                                            LoadingPanelID="lpaCadastro">


                                        <table cellspacing="0" cellpadding="0" width="450px" border="0" align="center">
                                            <table width="740" cellspacing="0" cellpadding="0" align="center">    

                                                <tr>
                                                    <td class="cadmsg" colspan="2">
                                                        <asp:ValidationSummary id="ValidationSummary3" runat="server" DisplayMode="List"  ValidationGroup="Todos">
                                                        </asp:ValidationSummary>
                                                    
                                                        <asp:Label id="lblMensagem" runat="server" Visible="false" 
                                                        Text="Label" CssClass="cadlbl"></asp:Label>
                                                    </td>

                                                    <td class="cadmsg" colspan="2">
                                                        <asp:ValidationSummary id="ValidationSummary1" runat="server" DisplayMode="List" ValidationGroup="ValidarData">
                                                        </asp:ValidationSummary>
                                                    
                                                    </td>
                                                </tr>
                                                
                                                <tr>
                                                    <td class="cadlbl">
                                                    <asp:Label ID="lblNomUsuario" runat="server" Text="Nome do usuário:"></asp:Label>
                                                    </td>

                                                    <td class="cadlbl">
                                                        &nbsp;
                                                    </td> 
                                                    <td class="cadlbl" width="250px" colspan="2">
                                                        &nbsp;
                                                    </td> 
                                                </tr>

                                            
                                                <tr>
                                                    <td class="cadlbl">
                                                        <asp:Label ID="lblNomeUsuario" runat="server" Text=""></asp:Label>
                                                    </td>
                                                    <td class="cadlbl">
                                                        &nbsp;
                                                    </td> 
                                                    <td class="cadlbl" width="250px" colspan="2">
                                                        <asp:Label ID="Label4" runat="server" Text="Vigência" style="text-align: center"></asp:Label>
                                                    </td> 
                                                </tr>

                                            
                                                <tr>
                                                    <td class="cadlbl">
                                                        &nbsp;
                                                    </td>
                                                    <td class="cadlbl">
                                                        &nbsp;
                                                    </td> 
                                                    <td class="cadlbl" width="250px" style="width: 0" >
                                                        <asp:RadioButton ID="rdbVigencia_Definitiva" runat="server" 
                                                        AutoPostBack="true" Text="Definitiva" Width="98px" CssClass="cadchk" OnCheckedChanged="rdbVigencia_Definitiva_CheckedChanged" GroupName="Vigencia">
                                                        </asp:RadioButton>
                                                    </td> 
                                                    <td class="cadlbl" width="250px" style="width: 125px" >
                                                        <asp:RadioButton ID="rdbVigencia_Temporaria" runat="server" 
                                                        AutoPostBack="true" Text="Temporária" Width="98px" CssClass="cadchk"  OnCheckedChanged="rdbVigencia_Temporaria_CheckedChanged" GroupName="Vigencia">
                                                        </asp:RadioButton>
                                                    </td> 
                                                </tr>
                                            
                                                <tr>
                                                    <td class="cadlbl">
                                                        <asp:Label ID="lblRegionalCad" runat="server" Text="Regional:"></asp:Label>
                                                    </td>
                                                    <td class="cadlbl">
                                                        <asp:Label ID="lblFilial" runat="server" Text="Filial:"></asp:Label>
                                                    </td> 
                                                    <td class="cadlbl">
                                                        <asp:Label ID="lblInicio" runat="server" Text="Início:" 
                                                        style="text-align: center"></asp:Label>
                                                    </td> 
                                                    <td class="cadlbl">
                                                        <asp:Label ID="lblFim" runat="server" Text="Fim:" 
                                                        style="text-align: center"></asp:Label>
                                                    </td> 
                                                </tr>
                                            
                                                <tr>
                                                    <td class="cadlbl">
                                                        <asp:DropDownList ID="ddlRegionalCad" runat="server" CssClass="cadddl" 
                                                        AutoPostBack="true" Width="150px" OnSelectedIndexChanged="ddlRegionalCad_SelectedIndexChanged" Enabled="true">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvRegional" runat="server" ControlToValidate="ddlRegionalCad" ValidationGroup="Todos"
                                                        ErrorMessage="Campo Obrigatório: Regional." InitialValue="0"
                                                        SetFocusOnError="True" >*</asp:RequiredFieldValidator>
                                                    </td>
                                                
                                                    <td class="cadlbl">
                                                        <asp:DropDownList ID="ddlFilialCad" runat="server" CssClass="cadddl" 
                                                        AutoPostBack="false" Width="150px" Enabled="false">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvFilialCad" runat="server" ControlToValidate="ddlFilialCad" ValidationGroup="Todos"
                                                        ErrorMessage="Campo Obrigatório: Filial." SetFocusOnError="True"  InitialValue="0">*</asp:RequiredFieldValidator>
                                                    </td>  
                                                
                                                <td>
                                                    <asp:TextBox ID="txtInicio" runat="server" Enabled="false" CssClass="cadtxt" Width="105px" MaxLength="16"></asp:TextBox>
                                                    <a hidefocus onclick="if(self.gfPop)gfPop.fPopCalendar(document.forms[0].all('<% = this.txtInicio.ClientID.ToString() %>'));return false;"
                                                        href="javascript:void(0)">
                                                        <img height="19" alt="" src="../../Agenda/calbtn.gif" width="20" align="absMiddle"
                                                            border="0" name="popcal"></a>
                                                    <asp:RequiredFieldValidator ID="rfvDataInicio" runat="server"  ValidationGroup="ValidarData" ControlToValidate="txtInicio"
                                                        ErrorMessage="Campo Data Início é obrigatório." SetFocusOnError="True">*</asp:RequiredFieldValidator>

                                                    <asp:CompareValidator ID="CompareValidator2" runat="server"  ValidationGroup="ValidarData"
                                                        ControlToValidate="txtInicio" ErrorMessage="Data início do período inválida."
                                                        Operator="DataTypeCheck"  Type="Date">*</asp:CompareValidator>
                                                </td>

                                                <td>
                                                    <asp:TextBox ID="txtFim" runat="server" Enabled="false" CssClass="cadtxt" Width="105px" MaxLength="16"></asp:TextBox>
                                                    <a hidefocus onclick="if(self.gfPop)gfPop.fPopCalendar(document.forms[0].all('<% = this.txtFim.ClientID.ToString() %>'));return false;"
                                                        href="javascript:void(0)">
                                                        <img height="19" alt="" src="../../Agenda/calbtn.gif" width="20" align="absMiddle"
                                                            border="0" name="popcal"></a>
                                                    <asp:RequiredFieldValidator ID="rfvDataFim" runat="server"  ValidationGroup="ValidarData" ControlToValidate="txtFim"
                                                        ErrorMessage="Campo Data Fim é obrigatório." SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                                    <asp:CompareValidator ID="cpvDataFim" runat="server"  ValidationGroup="ValidarData" ControlToCompare="txtInicio"
                                                        ControlToValidate="txtFim" ErrorMessage="A Data Fim deve ser maior que a Data Início"
                                                        Operator="GreaterThanEqual" Type="Date">*</asp:CompareValidator>

                                                    <asp:CompareValidator ID="CompareValidator1" runat="server"  ValidationGroup="ValidarData"
                                                        ControlToValidate="txtFim" ErrorMessage="Data final do período inválida."
                                                        Operator="DataTypeCheck"  Type="Date">*</asp:CompareValidator>
                                                        
                                                </td>

                                                </tr>                                       
                                                 
                                                <tr>
                                                    <td class="cadlbl">
                                                        <asp:CheckBox ID="chkAprovaAreaSeg" runat="server" 
                                                        Text="Aprova área de segurança" Width="188px" CssClass="cadchk">
                                                        </asp:CheckBox>
                                                    </td>
                                                    <td class="cadlbl">
                                                        <asp:Label ID="lblNivelAprovacao" runat="server" Text="Nível de aprovação"></asp:Label>
                                                    </td> 
                                                    <td class="cadlbl" width="200px" colspan="2">
                                                        <asp:Label ID="lblJustificativa" runat="server" Text="Justificativa:" 
                                                        style="text-align: center"></asp:Label>
                                                    </td> 
                                                </tr>
                                                <tr>
                                                    <td class="cadlbl">
                                                        <asp:CheckBox ID="chkAprovaCont" runat="server" 
                                                        Text="Aprova contingência" Width="188px" CssClass="cadchk">
                                                        </asp:CheckBox>
                                                    </td>

                                                    <td class="cadlbl">
                                                        <asp:DropDownList ID="ddlNivelAprovacao" runat="server" CssClass="cadddl" 
                                                        AutoPostBack="false" Width="150px" Enabled="true">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvTeste" runat="server" ControlToValidate="ddlNivelAprovacao" InitialValue="-1" ValidationGroup="Todos"
                                                        ErrorMessage="Campo Obrigatório: Nível de aprovação." SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                                    </td>  

                                                    <td class="cadlbl" width="200px" colspan="2">
                                                        <asp:TextBox ID="txtJustificativa" Visible="true" Enabled="false" runat="server" MaxLength="50"
                                                        Width="280px" CssClass="cadtxt"></asp:TextBox>

                                                        <asp:RequiredFieldValidator ID="rfv2" runat="server" ControlToValidate="txtJustificativa" ValidationGroup="ValidarData"
                                                        ErrorMessage="Campo Obrigatório: Justificativa." SetFocusOnError="True">*</asp:RequiredFieldValidator>

                                                    </td> 
                                                </tr>

                                                <tr>
                                                    <td class="cadlbl">
                                                        <asp:CheckBox ID="chkAprovaAreaTI" runat="server" 
                                                        Text="Aprova Área de TI" Width="188px" CssClass="cadchk">
                                                        </asp:CheckBox>

                                                     </td>

                                                    <td class="cadlbl">
                                                        &nbsp;</td>  

                                                    <td class="cadlbl" width="200px" colspan="2">
                                                        &nbsp;</td> 
                                                </tr>

                                                <tr>
                                                    <td class="cadlbl">
                                                        <asp:CheckBox ID="chkAprovaCracha" runat="server" 
                                                        Text="Aprova Permissão Crachá" Width="188px" CssClass="cadchk">
                                                        </asp:CheckBox>

                                                     </td>

                                                    <td class="cadlbl">
                                                        &nbsp;</td>  

                                                    <td class="cadlbl" width="200px" colspan="2">
                                                        &nbsp;</td> 
                                                </tr>

                                                <tr>
                                                    <td class="cadlbl">
                                                        <asp:Label ID="lblSupHier" runat="server" Text="Superior hierárquico:"></asp:Label>
                                                    </td>

                                                    <td class="cadlbl">
                                                        &nbsp;
                                                    </td>

                                                    <td class="cadlbl" colspan="2">
                                                        &nbsp;
                                                    </td> 
                                                </tr>
                                                <tr>
                                                    <td class="cadlbl" colspan="2">
                                                        <asp:DropDownList ID="ddlSuperHierarquico" runat="server" CssClass="cadddl" 
                                                        AutoPostBack="false" Width="380px" Enabled="true" Height="16px">
                                                        </asp:DropDownList>

                                                        <asp:RequiredFieldValidator ID="rfvSupHierarquico" runat="server" ControlToValidate="ddlSuperHierarquico" ValidationGroup="Todos"
                                                        ErrorMessage="Campo Obrigatório: Superior hierárquico." InitialValue="-1"
                                                        SetFocusOnError="True" >*</asp:RequiredFieldValidator>
                                                    </td>
                                                
                                                    <td width="250px" style="height: 19px" colspan="2">
                                                        &nbsp;
                                                    </td>
                                                </tr>                                       

                                                <tr>
                                                    <td class="cadlbl">
                                                        <asp:Label ID="lblOrigemSol" runat="server" Text="Origem da solicitação:"></asp:Label>
                                                    </td>

                                                    <td class="cadlbl">
                                                        <asp:Label ID="lblNSolicitacao" runat="server" Text="N. Solicitação:"></asp:Label>                                                    
                                                    </td>

                                                    <td class="cadlbl" width="250px" colspan="2">
                                                        &nbsp;
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td class="cadlbl">
                                                        <asp:DropDownList ID="ddlOrigemSol" runat="server" CssClass="cadddl" 
                                                        AutoPostBack="false" Width="153px" Enabled="true" Height="16px">
                                                        </asp:DropDownList>

                                                    </td>
                                                    <td class="cadlbl">
                                                        <asp:TextBox ID="txtNSolicitacao" runat="server" MaxLength="50"
                                                        Width="150px" CssClass="cadtxt"> </asp:TextBox>
                                                        
                                                    </td>
                                                    <td class="cadlbl" width="250px" colspan="2">
                                                        <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" 
                                                        CssClass="BotaoAdicionar" ImageUrl="~/Imagens/icones/ico_btn_adiconar.png" 
                                                        OnClick="btnAdicionar_Click" ToolTip="Adicionar atributos do aprovador" />
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td class="cadlbl">
                                                        &nbsp;
                                                    </td>
                                                    <td class="cadlbl">
                                                        &nbsp;
                                                    </td>
                                                    <td class="cadlbl" width="250px" colspan="2">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                            </table>


                                            <asp:Panel ID="pnlAtributosAprovador" runat="server">

                                                <!-- ***************** START: BARRA DE TITUTO ******************* -->
                                                        
                                                    <table width="740" cellspacing="0" cellpadding="0" align="center">
                                                        <tr>
                                                            <td>                                                            
                                                            </td>
                                                        </tr>

                                                        <tr>
                                                            <td class="cadlbl">
                                                                <asp:Label ID="lblAtributosAprovador" runat="server" Text="Atributos do aprovador"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>

                                                    <table width="740" cellspacing="0" cellpadding="0" align="center" style="border: 1px solid black">
                                                        <tr>
                                                            <td align="center" colspan="3">

                                                                <!-- ********************* STAR: RADGRID ************************** -->
                                                                <rad:RadGrid ID="radGridAtributosAprovador" runat="server" AllowPaging="True" AllowSorting="True"
                                                                    AutoGenerateColumns="False" CssClass="dtg" GridLines="None" Skin="None" Width="100%" 
                                                                    OnItemCommand="radGridAtributosAprovador_ItemCommand" OnItemDataBound="radGridAtributosAprovador_ItemDataBound"
                                                                    OnNeedDataSource="radGridAtributosAprovador_NeedDataSource">
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


                                                                                            <cc1:FWMascara ID="txtPagina_Perfil" runat="server" AutoPostBack="False" CssClass="pagtxtbox"
                                                                                                Mascara="NUMERO" MaxLength="4" Text='<%# (int)(DataBinder.Eval(Container, "OwnerTableView.CurrentPageIndex")) + 1 %>'
                                                                                                Width="50px"></cc1:FWMascara>

                                                                                            <asp:Label ID="Label4" runat="server" CssClass="paglbl">de</asp:Label>
                                                                                            <asp:Label ID="Label5" runat="server" CssClass="paglbl" Enabled="True"><%#DataBinder.Eval(Container, "Paging.PageCount").ToString()%></asp:Label>
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
                                                                            <rad:GridBoundColumn DataField="Id_AprovadorFilial" SortExpression="Id_AprovadorFilial"
                                                                                UniqueName="Id_AprovadorFilial" Visible="false">
                                                                                <ItemStyle Width="5%"></ItemStyle>
                                                                            </rad:GridBoundColumn>
                                                                            <rad:GridBoundColumn DataField="Des_Regional" HeaderText="Regional" SortExpression="Des_Regional"
                                                                                UniqueName="Des_Regional">
                                                                                <ItemStyle Width="20%"></ItemStyle>
                                                                            </rad:GridBoundColumn>  
                                                                            <rad:GridBoundColumn DataField="Des_Filial" HeaderText="Filial" SortExpression="Des_Filial"
                                                                                UniqueName="Des_Filial">
                                                                                <ItemStyle Width="20%"></ItemStyle>
                                                                            </rad:GridBoundColumn>  
                                                                            <rad:GridBoundColumn DataField="Flg_AprovaAreaSeg" HeaderText="AS" SortExpression="Flg_AprovaAreaSeg"
                                                                                UniqueName="Flg_AprovaAreaSeg">
                                                                                <ItemStyle Width="10%"></ItemStyle>
                                                                            </rad:GridBoundColumn>  

                                                                            <rad:GridBoundColumn DataField="Flg_AprovaContingencia" HeaderText="CNT" SortExpression="Flg_AprovaContingencia"
                                                                                UniqueName="Flg_AprovaContingencia">
                                                                                <ItemStyle Width="10%"></ItemStyle>
                                                                            </rad:GridBoundColumn>  

                                                                            <rad:GridBoundColumn DataField="Flg_AprovaAreaTI" HeaderText="TI" SortExpression="Flg_AprovaAreaTI"
                                                                                UniqueName="Flg_AprovaAreaTI">
                                                                                <ItemStyle Width="10%"></ItemStyle>
                                                                            </rad:GridBoundColumn>  

                                                                            <rad:GridBoundColumn DataField="Flg_AprovaCracha" HeaderText="Aprova Crachá" SortExpression="Flg_AprovaCracha"
                                                                                UniqueName="Flg_AprovaCracha">
                                                                                <ItemStyle Width="10%"></ItemStyle>
                                                                            </rad:GridBoundColumn>  

                                                                            <rad:GridBoundColumn DataField="Des_NivelAprovacao" HeaderText="Nível aprovação" SortExpression="Des_NivelAprovacao"
                                                                                UniqueName="Des_NivelAprovacao">
                                                                                <ItemStyle Width="20%" HorizontalAlign="Center"></ItemStyle>
                                                                            </rad:GridBoundColumn>

                                                                            <rad:GridBoundColumn DataField="Nom_Superior" HeaderText="Superior" SortExpression="Nom_Superior"
                                                                                UniqueName="Nom_Superior">
                                                                                <ItemStyle Width="20%" HorizontalAlign="Center"></ItemStyle>
                                                                            </rad:GridBoundColumn>

                                                                            <rad:GridBoundColumn DataField="Des_Vigencia" HeaderText="Vigência" SortExpression="Des_Vigencia"
                                                                                UniqueName="Des_Vigencia">
                                                                                <ItemStyle Width="20%" HorizontalAlign="Center"></ItemStyle>
                                                                            </rad:GridBoundColumn>

                                                                            <rad:GridBoundColumn DataField="Des_Justificativa" HeaderText="Justificativa" SortExpression="Des_Justificativa"
                                                                                UniqueName="Des_Justificativa">
                                                                                <ItemStyle Width="20%" HorizontalAlign="Center"></ItemStyle>
                                                                            </rad:GridBoundColumn>

                                                                            <rad:GridBoundColumn DataField="Des_OrigemSol" HeaderText="Origem" SortExpression="Des_OrigemSol"
                                                                                UniqueName="Des_OrigemSol">
                                                                                <ItemStyle Width="20%" HorizontalAlign="Center"></ItemStyle>
                                                                            </rad:GridBoundColumn>

                                                                            <rad:GridTemplateColumn Visible="False">
                                                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <asp:ImageButton ID="imgVisualizar" runat="server" ImageUrl="~/imagens/icones/ico_editar.gif"
                                                                                        CommandName="Visualizar" Visible="false"></asp:ImageButton>
                                                                                </ItemTemplate>
                                                                            </rad:GridTemplateColumn>
                                                                                <rad:GridTemplateColumn UniqueName="TemplateColumn" Visible="False">
                                                                                <ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <asp:ImageButton ID="btnEditar" runat="server" AlternateText="Editar" CausesValidation="False"
                                                                                        CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id_AprovadorFilial") %>' CommandName="Editar"
                                                                                        ImageUrl="%%PATH%%/imagens/icones/ico_editar.gif" ToolTip="Editar"></asp:ImageButton>
                                                                                </ItemTemplate>
                                                                            </rad:GridTemplateColumn>

                                                                            <rad:GridButtonColumn CommandName="Ativar" UniqueName="Ativar" ButtonType="ImageButton"
                                                                                ImageUrl="%%PATH%%/imagens/icones/ico_Ativo.gif" Text="Ativar">
                                                                                <ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
                                                                            </rad:GridButtonColumn>                                                                

                                                                            <rad:GridButtonColumn CommandName="Remover" UniqueName="Remover" ButtonType="ImageButton"
                                                                                ImageUrl="%%PATH%%/imagens/icones/ico_deletar.gif" Text="Remover">
                                                                                <ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
                                                                            </rad:GridButtonColumn>    
                     
                                                                        </Columns>
                                                                        <RowIndicatorColumn Visible="True">
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
                                                            <td class="cadlbl" style="width: 240px">
                                                            </td>
                                                            <td class="cadlbl" width="250px">
                                                            </td>
                                                        </tr>
                                                    </table>
                                            </asp:Panel>
                                        </table>

                                        </rada:RadAjaxPanel> 
                                        <table border="0" cellpadding="0" cellspacing="0" width="760" align="center">
                                            <tr>
                                                <td align="right" class="backBarraBotoes" style="height: 31px"> 
                                                    <asp:Button ID="btnVoltar" runat="server" CausesValidation="False" CssClass="cadbutton100"
                                                        Text="Voltar" Width="50px" OnClick="btnVoltar_Click" />                                            
                                                    &nbsp;
                                                    <asp:Button ID="btnGravar" runat="server" CssClass="cadbutton100" Text="Gravar" OnClick="btnGravar_Click"/>
                                                </td>
                                            </tr>
                                        </table>
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
                        Arquivo="/Admin/Framework/RodapeAdm.ascx"></cc1:FWServerControl>
                    <!-- ********************* END: RODAPÉ ************************** -->
                </td>
            </tr>
        </table>
    </form>
</body>
</html>