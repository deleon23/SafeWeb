<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CadFilial.aspx.cs" Inherits="SafWeb.UI.Modulos.Escala.CadFilial" %>
<%@ Register TagPrefix="rada" Namespace="Telerik.WebControls" Assembly="RadAjax.Net2" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.WebControls" Assembly="RadGrid.Net2" %>
<%@ Register TagPrefix="cc1" Namespace="FrameWork.WebControl" Assembly="FrameWork.WebControl" %>
<%@ Register TagPrefix="radc" Namespace="Telerik.WebControls" Assembly="RadCalendar.Net2" %>

<html>
<head runat="server">
    <link href="../Estilos/Brinks.css" rel="stylesheet" type="text/css" />   
    <link href="../Estilos/FrameWork.css" type="text/css" rel="stylesheet">   


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
                                    <td class="cadBarraTitulo" align="left" style="height: 25px; width: 750px">
                                        <img src="../Imagens/comum/bulletbarra.gif" align="absMiddle">
                                        <asp:Label ID="Label1" runat="server" Text="Listagem de Filial"></asp:Label>
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
                                                        AutoPostBack="false" Width="150px" OnSelectedIndexChanged="ddlRegionalList_SelectedIndexChanged" Enabled="true">
                                                    </asp:DropDownList>                                                    
                                                </td>
                                                <td style="height: 19px">
                                                    <asp:DropDownList ID="ddlFilialList" runat="server" CssClass="cadddl" AutoPostBack="false"
                                                        Enabled="false" Width="150px">
                                                    </asp:DropDownList>                                                    
                                                </td>  
                                            </tr>                                    
                                            <tr>
                                                <td class="cadlbl" colspan="2">
                                                    <asp:Label ID="lblDescricaoList" runat="server" Text="Descrição:"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="cadlbl" colspan="2">
                                                    <asp:TextBox ID="txtDescricaoList" runat="server" MaxLength="50"
                                                         Width="382px" CssClass="cadtxt"/>                                                                                              
                                                </td>
                                            </tr>                                     

                                            <!--
                                            <tr>
                                                <td class="cadlbl" colspan="2">
                                                    <asp:Label ID="lblPeriodicidadeList" runat="server" Text="Periodicidade:"></asp:Label>
                                                </td>                            
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:DropDownList ID="ddlPeriodicidadeList" runat="server" CssClass="cadddl" 
                                                        Width="234px">
                                                    </asp:DropDownList>                                                    
                                                </td>                             
                                            </tr>
                                            -->

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
                                                    <rad:RadGrid ID="radGridFilial" runat="server" AllowPaging="True" AllowSorting="True"
                                                        AutoGenerateColumns="False" CssClass="dtg" GridLines="None" Skin="None" Width="100%" 
                                                        OnItemCommand="radGridFilial_ItemCommand" OnItemDataBound="radGridFilial_ItemDataBound"
                                                        OnNeedDataSource="radGridFilial_NeedDataSource">
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
                                                                <rad:GridBoundColumn DataField="Id_Filial" SortExpression="Id_Filial"
                                                                    UniqueName="Id_Filial" Visible="false">
                                                                    <ItemStyle Width="5%"></ItemStyle>
                                                                </rad:GridBoundColumn>
                                                                <rad:GridBoundColumn DataField="Des_Regional" HeaderText="Regional" SortExpression="Des_Regional"
                                                                    UniqueName="Des_Regional">
                                                                    <ItemStyle Width="25%"></ItemStyle>
                                                                </rad:GridBoundColumn>  
                                                                <rad:GridBoundColumn DataField="Des_Filial" HeaderText="Filial" SortExpression="Des_Filial"
                                                                    UniqueName="Des_Filial">
                                                                    <ItemStyle Width="25%"></ItemStyle>
                                                                </rad:GridBoundColumn>  
                                                                <rad:GridBoundColumn DataField="Alias_Filial" HeaderText="Sigla Filial" SortExpression="Alias_Filial"
                                                                    UniqueName="Alias_Filial">
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
                                                                            CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id_Filial") %>' CommandName="Editar"
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
                                        <img src="../Imagens/comum/bulletbarra.gif" align="absMiddle">
                                        <asp:Label ID="Label6" runat="server" Text="Cadastro de Filial"></asp:Label>
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
                                                    <asp:ValidationSummary id="ValidationSummary1" runat="server" DisplayMode="List">
                                                    </asp:ValidationSummary><asp:Label id="lblMensagem" runat="server" Visible="false" 
                                                        Text="Label" CssClass="cadlbl"></asp:Label>
                                                </td>
                                                <td class="cadmsg">
                                                    &nbsp;</td>
                                            </tr>

                                            
                                            <tr>
                                                <td class="cadlbl">
                                                    <asp:Label ID="lblRegionalCad" runat="server" Text="Regional:"></asp:Label>
                                                </td>
                                                <td class="cadlbl">
                                                    <asp:Label ID="lblFilialCad" runat="server" Text="Sigla Filial:"></asp:Label>
                                                </td> 
                                                <td class="cadlbl" width="250px">
                                                    <asp:Label ID="lblFilial" runat="server" Text="Filial:"></asp:Label>
                                                </td> 
                                            </tr>
                                            <tr>
                                                <td class="cadlbl">
                                                    <asp:DropDownList ID="ddlRegionalCad" runat="server" CssClass="cadddl" 
                                                        AutoPostBack="false" Width="150px" OnSelectedIndexChanged="ddlRegionalCad_SelectedIndexChanged" Enabled="true">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvRegional" runat="server" ControlToValidate="ddlRegionalCad"
                                                        ErrorMessage="Campo Obrigatório: Regional." InitialValue="0"
                                                        SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                                </td>
                                                
                                                <td class="cadlbl">
                                                    <asp:TextBox ID="txtSiglaFilial" runat="server" MaxLength="50"
                                                         Width="150px" CssClass="cadtxt"/>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtSiglaFilial"
                                                        ErrorMessage="Campo Obrigatório: Sigla Filial." SetFocusOnError="True">*</asp:RequiredFieldValidator>                                                                                    
                                                </td>  
                                                
                                                <td width="250px" style="height: 19px">
                                                    <asp:TextBox ID="txtFilial" runat="server" MaxLength="50"
                                                         Width="150px" CssClass="cadtxt"/>
                                                    <asp:RequiredFieldValidator ID="rfvFilial_" runat="server" ControlToValidate="txtFilial"
                                                        ErrorMessage="Campo Obrigatório: Filial." SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                                </td>  
                                            </tr>                                       
                                            </tr>                                       
                                            <tr>
                                                <td class="cadlbl">
                                                    <asp:Label ID="lblEstado" runat="server" Text="Estado:"></asp:Label>
                                                </td>
                                                <td class="cadlbl">
                                                    <asp:Label ID="lblCidade" runat="server" Text="Cidade:"></asp:Label>
                                                </td> 
                                                <td class="cadlbl" width="200px">
                                                    <asp:Label ID="lblFusoHorario" runat="server" Text="Fuso horário:"></asp:Label>
                                                </td> 
                                            </tr>
                                            <tr>
                                                <td class="cadlblcadlbl">
                                                    <asp:DropDownList ID="ddlEstado" runat="server" CssClass="cadddl" 
                                                        AutoPostBack="true" Width="150px" OnSelectedIndexChanged="ddlEstado_SelectedIndexChanged" Enabled="true">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvEstado" runat="server" ControlToValidate="ddlEstado"
                                                        ErrorMessage="Campo Obrigatório: Estado." InitialValue="0"
                                                        SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                                </td>
                                                <td class="cadlbl">
                                                    <asp:DropDownList ID="ddlCidade" runat="server" CssClass="cadddl" 
                                                        AutoPostBack="false" Width="150px" Enabled="false">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvCidade" runat="server" ControlToValidate="ddlCidade"
                                                        ErrorMessage="Campo Obrigatório: Cidade." InitialValue="0"
                                                        SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                                </td> 
                                                <td class="cadlbl" width="200px">
                                                    <asp:DropDownList ID="ddlFusoHorario" runat="server" CssClass="cadddl" 
                                                        AutoPostBack="false" Width="150px" Enabled="true" Height="16px">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvFusoHorario" runat="server" ControlToValidate="ddlFusoHorario"
                                                        ErrorMessage="Campo Obrigatório: Fuso Horário." InitialValue="0"
                                                        SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                                </td> 
                                            </tr>
                                            <tr>
                                                <td class="cadlbl">
                                                    &nbsp;</td>
                                                <td class="cadlbl">
                                                    &nbsp;</td>
                                                <td width="200px" style="height: 19px">
                                                    &nbsp;</td>
                                            </tr>






                                            <tr>
                                                <td class="cadlbl">
                                                    <asp:Label ID="Label2" runat="server" Text="Código Filial Corporate:"></asp:Label>
                                                </td>
                                                <td class="cadlbl">
                                                    &nbsp;</td> 
                                                <td class="cadlbl" width="250px">
                                                    &nbsp;</td> 
                                            </tr>
                                            <tr>
                                                <td class="cadlbl">
                                                    <asp:TextBox ID="txtCodigoFilialCorporate" runat="server" CssClass="cadddl" 
                                                        AutoPostBack="false" Width="149px" Enabled="true"/>
                                                    <asp:RequiredFieldValidator ID="rfvCodigoFilialCorporate" runat="server" ControlToValidate="txtCodigoFilialCorporate"
                                                        ErrorMessage="Campo Obrigatório: Código Filial Corporate." SetFocusOnError="True">*</asp:RequiredFieldValidator>

                                                </td>
                                                
                                                <td class="cadlbl">
                                                    <asp:CheckBox ID="chkPortariaValAcesso" runat="server" Text="Portaria valida acesso?" Width="188px" CssClass="cadchk"/>
                                                </td>
                                                
                                                <td width="250px" style="height: 19px">
                                                    <asp:CheckBox ID="chkColetorOnline" runat="server" CssClass="cadchk" 
                                                        Text="Possui coletores de acesso online?" Width="231px" Enabled="true" 
                                                        Height="16px"/>
                                                </td>
                                            </tr>                                       



                                            <tr>
                                                <td class="cadlbl">
                                                </td>
                                                <td class="cadlbl">
                                                </td>
                                                <td class="cadlbl" width="250px">
                                                    &nbsp;</td>
                                            </tr>


                                            </table>


                                            <asp:Panel ID="pnlPerfilAcesso" runat="server">

                                                <!-- ***************** START: BARRA DE TITUTO ******************* -->
                                                        
                                                <table width="740" cellspacing="0" cellpadding="0" align="center">
                                                    <tr>
                                                        <td>                                                            
                                                        </td>
                                                    </tr>

                                                    <tr>
                                                        <td class="cadlbl">
                                                            <asp:Label ID="lblDadosPerfilAcesso" runat="server" Text="Dados do Perfil de Acesso"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                                                                                
                                                <table width="740" cellspacing="0" cellpadding="0" align="center" style="border: 1px solid black">
                                                    <tr>
                                                        <td class="cadmsg" colspan="1">
                                                            <asp:ValidationSummary id="vsPerfilAcesso" runat="server" DisplayMode="List" validationgroup="GrupoColetor" />                                                         
                                                            <asp:Label id="lblPerfilAcesso" runat="server" Visible="false" Text="" CssClass="cadlbl"></asp:Label>
                                                        </td>
                                                    </tr>

                                                    <tr>
                                                        <td class="cadlbl"  width="190px" style="padding-top: 20px;">
                                                            <asp:Label ID="lblDes_Area" runat="server" Text="Descrição da Área:"></asp:Label>
                                                        </td>

                                                        <td class="cadlbl"  width="190px" style="padding-top: 20px;">
                                                            <asp:Label ID="lblAreaSeg" runat="server" Text=""></asp:Label>
                                                        </td>

                                                        <td class="cadlbl"  width="190px" style="padding-top: 20px;">
                                                            <asp:Label ID="lblGrupoColetores" runat="server" Text="Grupo de Coletores:"></asp:Label>
                                                        </td>
                                                    </tr>

                                                    <tr>
                                                        <td width="250px" style="height: 19px">
                                                            <asp:TextBox ID="txtDes_Area" runat="server" MaxLength="50"
                                                                 Width="150px" CssClass="cadtxt"/>
                                                            <asp:RequiredFieldValidator ID="rfvDes_Area" runat="server" ControlToValidate="txtDes_Area" validationgroup="GrupoColetor"
                                                                ErrorMessage="Campo Obrigatório: Descrição da Área." SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                                        </td>
                                                
                                                        <td width="250px" style="height: 19px">
                                                            <asp:CheckBox ID="chkAreaSeg" runat="server" Text="Área de Segurança" Width="188px" CssClass="cadchk"/>
                                                        </td>


                                                        <td width="250px" style="height: 19px">
                                                            <asp:DropDownList ID="ddlGrupoColetores" runat="server" CssClass="cadddl" 
                                                                AutoPostBack="false" Width="150px" Enabled="true">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvGrupoColetores" runat="server" ControlToValidate="ddlGrupoColetores" validationgroup="GrupoColetor"
                                                                ErrorMessage="Campo Obrigatório: Grupo de Coletores." InitialValue="0"
                                                                SetFocusOnError="True">*</asp:RequiredFieldValidator>

                                                            <asp:ImageButton ID="btnAdicionar" CssClass="BotaoAdicionar" runat="server" ImageUrl="~/Imagens/icones/ico_btn_adiconar.png"
                                                                OnClick="btnAdicionar_Click" CausesValidation="False" ToolTip="Adicionar Perfil de Acesso" />

                                                        </td>
                                                    </tr>                                       


                                                    <tr>
                                                        <td class="style7">
                                                            &nbsp;</td>
                                                
                                                        <td width="250px" style="height: 19px">
                                                            &nbsp;</td>


                                                        <td width="250px" style="height: 19px">
                                                            &nbsp;</td>
                                                    </tr>    


                                                    </table>


                                                    <table width="740" cellspacing="0" cellpadding="0" align="center">
                                                        <tr>
                                                            <td>                                                            
                                                            </td>
                                                        </tr>
                                                    
                                                        <tr>
                                                        <td class="cadlbl">
                                                                <asp:Label ID="lblPerfilAcesso_Lista" runat="server" Text="Perfil de Acesso"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>

                                                    <table width="740" cellspacing="0" cellpadding="0" align="center" style="border: 1px solid black">
                                                       <tr>
                                                        <td align="center" colspan="3">
                                                            <!-- ********************* STAR: RADGRID ************************** -->
                                                            <rad:RadGrid ID="radGridPerfilAcesso" runat="server" AllowPaging="True" AllowSorting="True"
                                                                AutoGenerateColumns="False" CssClass="dtg" GridLines="None" Skin="None" Width="100%" 
                                                                OnItemCommand="radGridPerfilAcesso_ItemCommand" OnItemDataBound="radGridPerfilAcesso_ItemDataBound"
                                                                OnNeedDataSource="radGridPerfilAcesso_NeedDataSource">
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
                                                                        <rad:GridBoundColumn DataField="Codigo" SortExpression="Codigo"
                                                                            UniqueName="Codigo" Visible="false">
                                                                            <ItemStyle Width="5%"></ItemStyle>
                                                                        </rad:GridBoundColumn>
                                                                        <rad:GridBoundColumn DataField="Descricao" HeaderText="Área" SortExpression="Descricao"
                                                                            UniqueName="Descricao">
                                                                            <ItemStyle Width="20%"></ItemStyle>
                                                                        </rad:GridBoundColumn>  
                                                                        <rad:GridBoundColumn DataField="Flg_AreaSeg" HeaderText="Área de Segurança" SortExpression="Flg_AreaSeg"
                                                                            UniqueName="Flg_AreaSeg">
                                                                            <ItemStyle Width="10%"></ItemStyle>
                                                                        </rad:GridBoundColumn>  
                                                                        <rad:GridBoundColumn DataField="Des_GrupoColetores" HeaderText="Grupo de Coletores" SortExpression="Des_GrupoColetores"
                                                                            UniqueName="Des_GrupoColetores">
                                                                            <ItemStyle Width="20%" HorizontalAlign="Center"></ItemStyle>
                                                                        </rad:GridBoundColumn>

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
                        Arquivo="/Modulos/Framework/RodapeBrinks.ascx"></cc1:FWServerControl>
                    <!-- ********************* END: RODAPÉ ************************** -->
                </td>
            </tr>
        </table>
    </form>
</body>
</html>