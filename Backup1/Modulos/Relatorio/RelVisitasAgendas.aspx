<%@ Page Language="C#" AutoEventWireup="true" Codebehind="RelVisitasAgendas.aspx.cs"
    Inherits="SafWeb.UI.Modulos.Relatorio.RelVisitasAgendas" %>

<%@ Register TagPrefix="rada" Namespace="Telerik.WebControls" Assembly="RadAjax.Net2" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.WebControls" Assembly="RadGrid.Net2" %>
<%@ Register TagPrefix="cc1" Namespace="FrameWork.WebControl" Assembly="FrameWork.WebControl" %>
<html>
<head>
    <title>%%SITE%%</title>
    <link href="../../Estilos/FrameWork.css" type="text/css" rel="stylesheet">
    <link href="../../Estilos/Brinks.css" type="text/css" rel="stylesheet">

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
                                        <asp:Label ID="lblListagem" runat="server" Text="Visitas Agendadas"></asp:Label>
                                    </td>
                                    <td class="cadBarraTitulo" align="right" height="25" style="width: 09px">
                                    <asp:ImageButton ID="btnHelp" runat="server" AlternateText="Ajuda" ImageUrl="%%PATH%%/imagens/icones/ico_help.jpg"
                                        ImageAlign="AbsMiddle" Visible="False" />
                                </tr>
                                <!-- ***************** END: BARRA DE TITUTO ******************* -->
                                <tr>
                                    <td class="backboxconteudo" valign="top" colspan="2">
                                        <!-- ****************** START: FILTROS ************************** -->
                                        <table class="MarginTopConteudo" cellspacing="0" cellpadding="0" width="740" align="center"
                                            border="0">
                                            <tr>
                                                <td colspan="7" class="cadmsg">
                                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td  class="cadlbl" colspan="7">
                                                    <asp:Label ID="lblIdSolicitacao" runat="server" Text="Nº Solicitação:"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="7">
                                                    <cc1:FWMascara ID="txtIdSolicitacao" Width="150px" runat="server" Mascara="NUMERO" CssClass="cadtxt"></cc1:FWMascara>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="cadlbl" width="180">
                                                    <asp:Label ID="lblEmpresa" runat="server" Text="Empresa:"></asp:Label></td>
                                                <td width="5">
                                                </td>
                                                <td class="cadlbl" width="180">
                                                    <asp:Label ID="lblRegional" runat="server" Text="Regional:"></asp:Label></td>
                                                <td width="5" style="height: 19px">
                                                </td>
                                                <td class="cadlbl">
                                                    <asp:Label ID="lblFilial" runat="server" Text="Filial:"></asp:Label></td>
                                                <td width="5">
                                                </td>
                                                <td class="cadlbl" width="180">
                                                    <asp:Label ID="lblArea" runat="server" Text="Área:"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td style="height: 19px">
                                                    <asp:DropDownList ID="ddlEmpresa" CssClass="cadddl" runat="server" Width="150px">
                                                    </asp:DropDownList></td>
                                                <td style="height: 19px">
                                                </td>
                                                <td style="height: 19px">
                                                    <asp:DropDownList ID="ddlRegional" CssClass="cadddl" runat="server" Width="150px"
                                                        AutoPostBack="True" OnSelectedIndexChanged="ddlRegional_SelectedIndexChanged">
                                                    </asp:DropDownList></td>
                                                <td style="height: 19px">
                                                </td>
                                                <td style="height: 19px">
                                                    <asp:DropDownList ID="ddlFilial" CssClass="cadddl" runat="server" Width="150px" Enabled="False"
                                                        AutoPostBack="True" OnSelectedIndexChanged="ddlFilial_SelectedIndexChanged">
                                                    </asp:DropDownList></td>
                                                <td style="height: 19px">
                                                </td>
                                                <td style="height: 19px">
                                                    <asp:DropDownList ID="ddlArea" CssClass="cadddl" runat="server" Width="150px" Enabled="False">
                                                    </asp:DropDownList></td>
                                            </tr>
                                            <tr>
                                                <td class="cadlbl">
                                                    <asp:Label ID="Label4" runat="server" Text="Tipo Visitante:"></asp:Label></td>
                                                <td style="height: 19px">
                                                </td>
                                                <td class="cadlbl">
                                                    <asp:Label ID="lblTipoSolicitacao" runat="server" Text="Tipo Solicitação:"></asp:Label></td>
                                                <td>
                                                </td>
                                                <td class="cadlbl" style="width: 215px; height: 19px;">
                                                    <asp:Label ID="lblDataInicio" runat="server" Text="Data Início:"></asp:Label></td>
                                                <td>
                                                </td>
                                                <td class="cadlbl">
                                                    <asp:Label ID="lblDataFim" runat="server" Text="Data Fim:"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:DropDownList ID="ddlTipoVisitante" CssClass="cadddl" runat="server" Width="150px">
                                                    </asp:DropDownList></td>
                                                <td>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlTipoSolicitacao" CssClass="cadddl" runat="server" Width="150px">
                                                    </asp:DropDownList></td>
                                                <td>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtDataInicio" runat="server" CssClass="cadtxt" Width="105px" MaxLength="16"></asp:TextBox>
                                                    <a hidefocus onclick="if(self.gfPop)gfPop.fPopCalendar(document.forms[0].all('<% = this.txtDataInicio.ClientID.ToString() %>'));return false;"
                                                        href="javascript:void(0)">
                                                        <img height="19" alt="" src="../../Agenda/calbtn.gif" width="20" align="absMiddle"
                                                            border="0" name="popcal"></a>
                                                    <asp:CompareValidator ID="CompareValidator1" runat="server"  ValidationGroup="ValidarData"
                                                        ControlToValidate="txtDataInicio" ErrorMessage="Data início do período inválida."
                                                        Operator="DataTypeCheck"  Type="Date">*</asp:CompareValidator>

                                                    <asp:RequiredFieldValidator ID="rfvDataInicio" runat="server" ControlToValidate="txtDataInicio"
                                                        ErrorMessage="Campo Data Início é obrigatório." SetFocusOnError="True">*</asp:RequiredFieldValidator></td>
                                                <td>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtDataFim" runat="server" CssClass="cadtxt" Width="105px" MaxLength="16"></asp:TextBox>
                                                    <a hidefocus onclick="if(self.gfPop)gfPop.fPopCalendar(document.forms[0].all('<% = this.txtDataFim.ClientID.ToString() %>'));return false;"
                                                        href="javascript:void(0)">
                                                        <img height="19" alt="" src="../../Agenda/calbtn.gif" width="20" align="absMiddle"
                                                            border="0" name="popcal"></a>
                                                    <asp:CompareValidator ID="CompareValidator2" runat="server"  ValidationGroup="ValidarData"
                                                        ControlToValidate="txtDataFim" ErrorMessage="Data fim do período inválida."
                                                        Operator="DataTypeCheck"  Type="Date">*</asp:CompareValidator>

                                                    <asp:CustomValidator ID="cpvDataFim" runat="server" ErrorMessage="Data do período inválida."
                                                        onservervalidate="cpvDataFim_ServerValidate">*</asp:CustomValidator>

                                                    <asp:RequiredFieldValidator ID="rfvDataFim" runat="server" ControlToValidate="txtDataFim"
                                                        ErrorMessage="Campo Data Fim é obrigatório." SetFocusOnError="True">*</asp:RequiredFieldValidator></td>                                                    
                                                    
                                            </tr>
                                            <tr>
                                                <td class="cadlbl" colspan="5">
                                                    <asp:Label ID="lblVisitado" runat="server" Text="Visitado:"></asp:Label>
                                                </td>
                                                <td>
                                                </td>
                                                <td class="cadlbl">
                                                    <asp:Label ID="lblStatus" runat="server" Text="Status:"></asp:Label></td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="5">
                                                    <asp:TextBox ID="txtVisitado" runat="server" CssClass="cadtxt" MaxLength="50" Width="460px"></asp:TextBox></td>
                                                <td>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlStatus" CssClass="cadddl" runat="server" Width="150px">
                                                    </asp:DropDownList></td>
                                            </tr>
                                            <tr>
                                                <td class="cadlbl" colspan="5">
                                                    <asp:Label ID="lblVisitante" runat="server" Text="Visitante:"></asp:Label>
                                                </td>
                                                <td>
                                                </td>
                                                <td class="cadlbl">
                                                    <td>
                                                    </td>
                                            </tr>
                                            <tr>
                                                <td colspan="5">
                                                    <asp:TextBox ID="txtVisitante" runat="server" CssClass="cadtxt" MaxLength="50" Width="460px"></asp:TextBox></td>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="8" align="center" style="height: 19px">
                                                    <br />
                                                    <asp:Button ID="btnBuscar" runat="server" Text="Apresentar na Tela" CssClass="cadbuttonfiltro"
                                                        OnClick="btnBuscar_Click" />
                                                    &nbsp;<asp:Button ID="btnExportWord" runat="server" Text="Exportar para Word" CssClass="cadbuttonfiltro"
                                                        OnClick="btnExportWord_Click" />
                                                    &nbsp;<asp:Button ID="btnExportExcel" runat="server" Text="Exportar para Excel" CssClass="cadbuttonfiltro"
                                                        OnClick="btnExportExcel_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                        <!-- ********************** END: FILTROS ************************** -->
                                        <table class="MarginTopConteudo" cellspacing="0" cellpadding="0" width="740" align="center"
                                            border="0">
                                            <tr>
                                                <td align="center" colspan="3">
                                                    <!-- ********************* STAR: RADGRID ************************** -->
                                                    <rad:RadGrid ID="radGridRelatorio" runat="server" AllowPaging="True" AllowSorting="True"
                                                        AutoGenerateColumns="False" CssClass="dtg" GridLines="None" Skin="None" Width="100%"
                                                        OnItemCommand="radGridRelatorio_ItemCommand" OnNeedDataSource="radGridRelatorio_NeedDataSource">
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
                                                                <rad:GridBoundColumn DataField="TipoVisitante" HeaderText="Tipo Visit" SortExpression="TipoVisitante"
                                                                    UniqueName="TipoVisitante">
                                                                    <ItemStyle Width="5%" HorizontalAlign="Center"></ItemStyle>
                                                                </rad:GridBoundColumn>
                                                                <rad:GridBoundColumn DataField="Visitante" HeaderText="Nome Visitante" SortExpression="Visitante"
                                                                    UniqueName="Visitante">
                                                                    <ItemStyle Width="15%" HorizontalAlign="Center"></ItemStyle>
                                                                </rad:GridBoundColumn>
                                                                <rad:GridBoundColumn DataField="Documento" HeaderText="Documento" SortExpression="Documento"
                                                                    UniqueName="Documento">
                                                                    <ItemStyle Width="5%" HorizontalAlign="Center"></ItemStyle>
                                                                </rad:GridBoundColumn>
                                                                <rad:GridBoundColumn DataField="Empresa" HeaderText="Empresa Visitante" SortExpression="Empresa"
                                                                    UniqueName="Empresa">
                                                                    <ItemStyle Width="10%" HorizontalAlign="Center"></ItemStyle>
                                                                </rad:GridBoundColumn>
                                                                <rad:GridBoundColumn DataField="Visitado" HeaderText="Nome Visitado" SortExpression="Visitado"
                                                                    UniqueName="Visitado">
                                                                    <ItemStyle Width="15%" HorizontalAlign="Center"></ItemStyle>
                                                                </rad:GridBoundColumn>
                                                                <rad:GridBoundColumn DataField="DataEntrada" HeaderText="Data/Hora In&#237;cio" SortExpression="DataEntrada"
                                                                    UniqueName="DataEntrada" DataFormatString="{0:dd/MM/yy HH:mm}">
                                                                    <ItemStyle Width="5%" HorizontalAlign="Center"></ItemStyle>
                                                                </rad:GridBoundColumn>
                                                                <rad:GridBoundColumn DataField="DataSaida" HeaderText="Data/Hora Fim" SortExpression="DataSaida"
                                                                    UniqueName="DataSaida" DataFormatString="{0:dd/MM/yy HH:mm}">
                                                                    <ItemStyle Width="5%" HorizontalAlign="Center"></ItemStyle>
                                                                </rad:GridBoundColumn>
                                                                <rad:GridBoundColumn DataField="Filial" HeaderText="Filial Visitada" SortExpression="Filial"
                                                                    UniqueName="Filial">
                                                                    <ItemStyle Width="5%" HorizontalAlign="Center"></ItemStyle>
                                                                </rad:GridBoundColumn>
                                                                <rad:GridBoundColumn DataField="Area" HeaderText="&#193;rea Visitada" SortExpression="Area"
                                                                    UniqueName="Area">
                                                                    <ItemStyle Width="5%" HorizontalAlign="Center"></ItemStyle>
                                                                </rad:GridBoundColumn>
                                                                <rad:GridBoundColumn DataField="TipoSolicitacao" HeaderText="Tipo Solicita&#231;&#227;o"
                                                                    SortExpression="TipoSolicitacao" UniqueName="TipoSolicitacao">
                                                                    <ItemStyle Width="10%" HorizontalAlign="Center"></ItemStyle>
                                                                </rad:GridBoundColumn>
                                                                <rad:GridBoundColumn DataField="Status" HeaderText="Status" SortExpression="Status"
                                                                    UniqueName="Status">
                                                                    <ItemStyle Width="10%" HorizontalAlign="Center"></ItemStyle>
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
                                        </table>
                                        <br />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <!-- ****************** END: PAINEL ************************** -->
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
