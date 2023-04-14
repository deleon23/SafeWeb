<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CadEscalaDepartamental.aspx.cs" Inherits="SafWeb.UI.Modulos.Escala.CadEscalaDepartamental" %>
<%@ Register TagPrefix="rada" Namespace="Telerik.WebControls" Assembly="RadAjax.Net2" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.WebControls" Assembly="RadGrid.Net2" %>
<%@ Register TagPrefix="cc1" Namespace="FrameWork.WebControl" Assembly="FrameWork.WebControl" %>
<%@ Register TagPrefix="radc" Namespace="Telerik.WebControls" Assembly="RadCalendar.Net2" %>

<html>
<head runat="server">
    <title>%%SITE%%</title>
    <link href="../../Estilos/Brinks.css" rel="stylesheet" type="text/css" />   
    <link href="../../Estilos/FrameWork.css" type="text/css" rel="stylesheet"/>   

    <script language="javascript">
        function PreencherHiddenColaboradores(colaboradores) {
            document.getElementById("txtHiddenColaboradores").value = colaboradores;

            AjaxNS.AR('Colaboradores', '', 'RadAjaxPanel2');
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
                                        <img src="../../Imagens/comum/bulletbarra.gif" align="absMiddle">
                                        <asp:Label ID="Label1" runat="server" Text="Listagem de Parâmetros de Escala Departamental"></asp:Label>
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
                                                        AutoPostBack="false" Width="150px" OnSelectedIndexChanged="ddlRegionalList_SelectedIndexChanged" Enabled="false">
                                                    </asp:DropDownList>                                                    
                                                </td>
                                                <td style="height: 19px">
                                                    <asp:DropDownList ID="ddlFilialList" runat="server" CssClass="cadddl" AutoPostBack="false"
                                                        Enabled="False" Width="150px">
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
                                                    <rad:RadGrid ID="radGridEscalaDepartamental" runat="server" AllowPaging="True" AllowSorting="True"
                                                        AutoGenerateColumns="False" CssClass="dtg" GridLines="None" Skin="None" Width="100%" 
                                                        OnItemCommand="radGridEscalaDepartamental_ItemCommand" OnItemDataBound="radGridEscalaDepartamental_ItemDataBound"
                                                        OnNeedDataSource="radGridEscalaDepartamental_NeedDataSource">
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
                                                                <rad:GridBoundColumn DataField="Id_Regional" SortExpression="Id_Regional"
                                                                    UniqueName="Id_Regional" Visible="false">
                                                                    <ItemStyle Width="5%"></ItemStyle>
                                                                </rad:GridBoundColumn>
                                                                <rad:GridBoundColumn DataField="Des_Regional" HeaderText="Regional" SortExpression="Des_Regional"
                                                                    UniqueName="Des_Regional">
                                                                    <ItemStyle Width="25%"></ItemStyle>
                                                                </rad:GridBoundColumn>  
                                                                <rad:GridBoundColumn DataField="Id_Filial" SortExpression="Id_Filial"
                                                                    UniqueName="Id_Filial" Visible="false">
                                                                    <ItemStyle Width="5%"></ItemStyle>
                                                                </rad:GridBoundColumn>                                                      
                                                                <rad:GridBoundColumn DataField="Alias_Filial" HeaderText="Filial" SortExpression="Alias_Filial"
                                                                    UniqueName="Alias_Filial">
                                                                    <ItemStyle Width="20%" HorizontalAlign="Center"></ItemStyle>
                                                                </rad:GridBoundColumn>
                                                                <rad:GridBoundColumn DataField="Des_EscalaDpto" HeaderText="Escala Departamental" SortExpression="Des_EscalaDpto"
                                                                    UniqueName="Des_EscalaDpto">
                                                                    <ItemStyle Width="25%"></ItemStyle>
                                                                </rad:GridBoundColumn>  
                                                                <rad:GridBoundColumn DataField="Id_Periodicidade" SortExpression="Id_Periodicidade"
                                                                    UniqueName="Id_Periodicidade" Visible="false">
                                                                    <ItemStyle Width="5%"></ItemStyle>
                                                                </rad:GridBoundColumn>
                                                                <rad:GridBoundColumn DataField="Des_Periodicidade" HeaderText="Períodicidade" SortExpression="Des_Periodicidade"
                                                                    UniqueName="Des_Periodicidade">
                                                                    <ItemStyle Width="25%"></ItemStyle>
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
                                                                            CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id_EscalaDpto") %>' CommandName="Editar"
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
                                        <img src="../../Imagens/comum/bulletbarra.gif" align="absMiddle">
                                        <asp:Label ID="Label6" runat="server" Text="Cadastro de Escala Departamental"></asp:Label>
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
                                            

                                        <asp:Panel ID="pnlCadColab" runat="server" />
                                        <asp:HiddenField ID="txtHiddenColaboradores" runat="server" />

                                        <table cellspacing="0" cellpadding="0" width="450px" border="0" align="center">
                                            <tr>
                                                <td class="cadmsg" colspan="2">
                                                    <asp:ValidationSummary id="ValidationSummary1" runat="server" DisplayMode="List">
                                                    </asp:ValidationSummary><asp:Label id="lblMensagem" runat="server" Visible="false" 
                                                        Text="Label" CssClass="cadlbl"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="cadlbl">
                                                    <asp:Label ID="lblRegionalCad" runat="server" Text="Regional:"></asp:Label>
                                                </td>
                                                <td class="cadlbl" width="250px">
                                                    <asp:Label ID="lblFilialCad" runat="server" Text="Filial:"></asp:Label>
                                                </td> 
                                            </tr>
                                            <tr>
                                                <td width="280px" style="height: 19px; ">
                                                    <asp:DropDownList ID="ddlRegionalCad" runat="server" CssClass="cadddl" 
                                                        AutoPostBack="false" Width="194px" OnSelectedIndexChanged="ddlRegionalCad_SelectedIndexChanged" Enabled="false" Height="19px">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvRegional" runat="server" ControlToValidate="ddlRegionalCad"
                                                        ErrorMessage="Campo Obrigatório: Regional." InitialValue="0"
                                                        SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                                </td>
                                                <td width="250px" style="height: 19px" class="auto-style1">
                                                    <asp:DropDownList ID="ddlFilialCad" runat="server" CssClass="cadddl"
                                                        Enabled="False" Width="150px" style="height: 22px">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvFilial" runat="server" ControlToValidate="ddlFilialCad"
                                                        ErrorMessage="Campo Obrigatório: Filial." InitialValue="0"
                                                        SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                                </td>  
                                            </tr>                                       
                                            <tr>
                                                <td class="cadlbl" colspan="2">
                                                    <asp:Label ID="lbldescricaoEscalaCad" runat="server" Text="Descrição:"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="cadlbl" colspan="2">
                                                    <asp:TextBox ID="txtDescricaoCad" runat="server" MaxLength="50"
                                                         Width="397px" CssClass="cadtxt"/>
                                                    <asp:RequiredFieldValidator ID="rfvDescricao" runat="server" ControlToValidate="txtDescricaoCad"
                                                        ErrorMessage="Campo Obrigatório: Descrição." SetFocusOnError="True">*</asp:RequiredFieldValidator>                                                                                    
                                                </td>
                                            </tr>                                      
                                            <tr>
                                                <td class="cadlbl" colspan="2">
                                                    <asp:Label ID="lblPeriodicidadeCad" runat="server" Text="Periodicidade:"></asp:Label>
                                                </td>                            
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:DropDownList ID="ddlPeriodicidadeCad" runat="server" CssClass="cadddl" Width="194px">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvPeriodicidade" runat="server" ControlToValidate="ddlPeriodicidadeCad"
                                                        ErrorMessage="Campo Obrigatório: Periodicidade." InitialValue="0"
                                                        SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                                </td>                             
                                                <td class="cadlbl">
                                                    
                                                    <asp:CheckBox id="chkReplicaRH" runat="server" Checked="True" Text="Replicar para o Ronda" />
													</td>                             
                                            </tr>                                     
                                            <tr>
                                                <td colspan="2" style="padding-bottom:15px;">
                                                    <table cellspacing="0" cellpadding="0" width="450px" border="0" align="center" >
                                                        <tr>
                                                            <td class="cadlbl"  style="width: 210px">
                                                                <asp:Label ID="lblHorariosCad" runat="server" Text="Possíveis Horários de Entrada:"></asp:Label>
                                                            </td>
                                                            <td style="width: 32px">
															&nbsp;</td>
                                                            <td class="cadlbl" style="width: 206px; padding-left:11px;">
                                                                <asp:Label ID="lblHorariosSelecionadosCad" runat="server" Text="Horários selecionados:"></asp:Label>
                                                                <asp:CustomValidator ID="cvrHorariosSelecionadosCad" runat="server" ErrorMessage="Campo Obrigatório: Horários Selecionados."
                                                                    onservervalidate="cvrHorariosSelecionadosCad_ServerValidate">*</asp:CustomValidator>
                                                            </td> 
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 210px">
                                                                <asp:ListBox ID="lstHorariosCad" runat="server" Height="137px" Width="195px" CssClass="cadlstBox"
                                                                    SelectionMode="Multiple"></asp:ListBox>
                                                            </td>
                                                            <td align="center" style="width: 32px">
                                                                <asp:Button ID="btnAddUmCad" runat="server" Text=" > " CssClass="cadbuttonfiltro" Width="25px" CausesValidation="False" OnClick="btnAddUmCad_Click" /><br />
                                                                <br />
                                                                <asp:Button ID="btnRemoverUmCad" runat="server" Text="<" CssClass="cadbuttonfiltro" Width="25px" CausesValidation="False" OnClick="btnRemoverUmCad_Click" /><br />
                                                            </td>
                                                            <td align="right" style="width: 206px">
                                                                <asp:ListBox ID="lstHorariosSelecionadosCad" runat="server" Height="137px" Width="195px" CssClass="cadlstBox"
                                                                    SelectionMode="Multiple" style="margin-left: 0px"></asp:ListBox>
                                                            </td>
                                                        </tr>

                                                            <tr>
                                                                <td class="cadlbl" colspan="3" style="padding-top: 10px;">
                                                                    <asp:Label ID="lblListaColaboradores" runat="server" Text="Lista de Colaboradores:"></asp:Label>
                                                                    <asp:ImageButton ID="btnAdicionar" CssClass="BotaoAdicionar" runat="server" ImageUrl="~/Imagens/icones/ico_btn_adiconar.png"
                                                                        OnClick="btnAdicionar_Click" CausesValidation="False" ToolTip="Adicionar Colaboradores" />
                                                                    <asp:ImageButton ID="btnRemover" CssClass="BotaoAdicionar" runat="server" ImageUrl="~/Imagens/icones/ico_btn_remover.png"
                                                                        CausesValidation="False" ToolTip="Remover Colaborador(es)" OnClick="btnRemover_Click" />
                                                                    <!--
                                                                    <asp:CustomValidator ID="cvrListaColaboradores" runat="server" ErrorMessage="Campo Obrigatório: Lista de Colaboradores"
                                                                        OnServerValidate="cvrListaColaboradores_ServerValidate">*</asp:CustomValidator>-->
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="3" style="padding-bottom: 5px;">
                                                                    <asp:ListBox ID="lstColaboradores" runat="server" Height="300px" Width="446" SelectionMode="Multiple"
                                                                        CssClass="cadlstBox"></asp:ListBox>
                                                                </td>
                                                            </tr>

                                                    </table>
                                                </td>                             
                                            </tr>                                   
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
