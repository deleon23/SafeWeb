<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RelEscalasAgendadas.aspx.cs"
    Inherits="SafWeb.UI.Modulos.Relatorio.RelEscalasAgendadas" %>

<%@ Register TagPrefix="rada" Namespace="Telerik.WebControls" Assembly="RadAjax.Net2" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.WebControls" Assembly="RadGrid.Net2" %>
<%@ Register TagPrefix="cc1" Namespace="FrameWork.WebControl" Assembly="FrameWork.WebControl" %>
<html>
<head>
    <title>%%SITE%%</title>
    <link href="../../Estilos/FrameWork.css" type="text/css" rel="stylesheet">
    <link href="../../Estilos/Brinks.css" type="text/css" rel="stylesheet">
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
                                    <asp:Label ID="lblListagem" runat="server" Text="Escalas Agendadas"></asp:Label>
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
                                                <asp:ValidationSummary ID="ValidationSummary3" runat="server" DisplayMode="List" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="cadlbl" width="170px">
                                                <asp:Label ID="Label4" runat="server" Text="Nº Escala:"></asp:Label>
                                            </td>
                                            <td width="5px">
                                            </td>
                                            <td class="cadlbl" style="width: 172px">
                                                <asp:Label ID="Label5" runat="server" Text="Descrição Escala:"></asp:Label>
                                            </td>
                                            <td>
                                            </td>
                                            <td class="cadlbl" width="170px">
                                                <asp:Label ID="Label6" runat="server" Text="Regional:"></asp:Label>
                                            </td>
                                            <td width="5px">
                                            </td>
                                            <td class="cadlbl" width="170px">
                                                <asp:Label ID="Label7" runat="server" Text="Filial:"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtNumSolicitacaoColab" runat="server" CssClass="cadtxt" MaxLength="8"
                                                    Width="75px"></asp:TextBox>
                                            </td>
                                            <td>
                                            </td>
                                            <td style="width: 172px">
                                                <asp:DropDownList ID="ddlEscalaHistColab" CssClass="cadddl" runat="server" Width="165px">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlRegional" CssClass="cadddl" runat="server" Width="165px"
                                                    AutoPostBack="True" OnSelectedIndexChanged="ddlRegional_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlFilial" CssClass="cadddl" runat="server" Width="165px" Enabled="False">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="cadlbl">
                                                <asp:Label ID="Label8" runat="server" Text="Status:"></asp:Label>
                                            </td>
                                            <td>
                                            </td>
                                            <td class="cadlbl" style="width: 172px">
                                                <asp:Label ID="Label9" runat="server" Text="Tipo Escala:"></asp:Label>
                                            </td>
                                            <td>
                                            </td>
                                            <td class="cadlbl">
                                                <asp:Label ID="Label10" runat="server" Text="Data Início:"></asp:Label>
                                            </td>
                                            <td>
                                            </td>
                                            <td class="cadlbl">
                                                <asp:Label ID="Label11" runat="server" Text="Data Fim:"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:DropDownList ID="ddlStatus" CssClass="cadddl" runat="server" Width="165px">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                            </td>
                                            <td style="width: 172px">
                                                <asp:DropDownList ID="ddlEscalaDep" CssClass="cadddl" runat="server" Width="165px">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtDataInicio" runat="server" CssClass="cadtxt" Width="145px" MaxLength="16"></asp:TextBox>
                                                <a hidefocus onclick="if(self.gfPop)gfPop.fPopCalendar(document.forms[0].all('<% = this.txtDataInicio.ClientID.ToString() %>'));return false;"
                                                    href="javascript:void(0)">
                                                    <img height="19" alt="" src="../../Agenda/calbtn.gif" width="20" align="absMiddle"
                                                        border="0" name="popcal"></a>
                                                         <asp:RequiredFieldValidator ID="rfvDataInicio" runat="server" ControlToValidate="txtDataInicio"
                                                        ErrorMessage="Campo Data Início é obrigatório." SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtDataFim" runat="server" CssClass="cadtxt" Width="145px" MaxLength="16"></asp:TextBox>
                                                <a hidefocus onclick="if(self.gfPop)gfPop.fPopCalendar(document.forms[0].all('<% = this.txtDataFim.ClientID.ToString() %>'));return false;"
                                                    href="javascript:void(0)">
                                                    <img height="19" alt="" src="../../Agenda/calbtn.gif" width="20" align="absMiddle"
                                                        border="0" name="popcal"></a>
                                                        <asp:RequiredFieldValidator ID="rfvDataFim" runat="server" ControlToValidate="txtDataFim"
                                                        ErrorMessage="Campo Data Fim é obrigatório." SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                                        <asp:CompareValidator ID="CompareValidator1" runat="server"
                                                            ControlToCompare="txtDataInicio" ControlToValidate="txtDataFim" ErrorMessage="A Data Fim deve ser maior que a Data Início"
                                                            Type="Date" Operator="GreaterThanEqual">*</asp:CompareValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="cadlbl" colspan="3">
                                                <asp:Label ID="Label12" runat="server" Text="Colaborador Escalado:"></asp:Label>
                                            </td>
                                            <td>
                                            </td>
                                            <td class="cadlbl" colspan="3">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                <asp:TextBox ID="txtColabEscalado" runat="server" CssClass="cadtxt" MaxLength="50"
                                                    Width="343px"></asp:TextBox>
                                            </td>
                                            <td>
                                            </td>
                                            <td colspan="3">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="cadlbl" colspan="3">
                                                <asp:Label ID="Label14" runat="server" Text="Solicitante:"></asp:Label>
                                            </td>
                                            <td>
                                            </td>
                                            <td class="cadlbl" colspan="3">
                                                <asp:Label ID="Label15" runat="server" Text="Aprovador:"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                <asp:TextBox ID="txtSolicitanteHistColab" runat="server" CssClass="cadtxt" MaxLength="50"
                                                    Width="343px"></asp:TextBox>
                                            </td>
                                            <td>
                                            </td>
                                            <td colspan="3">
                                                <asp:TextBox ID="txtAprovadorHistColab" runat="server" CssClass="cadtxt" MaxLength="50"
                                                    Width="343px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="7" align="center">
                                                <br />
                                                <asp:Button ID="btnBuscar" runat="server" Text="Apresentar na Tela" CssClass="cadbuttonfiltro"
                                                    OnClick="btnBuscar_Click" />

                                                    &nbsp;<asp:Button ID="btnExportWord" runat="server" Text="Exportar para Word" CssClass="cadbuttonfiltro"
                                                        OnClick="btnExportWord_Click" />
                                                    &nbsp;<asp:Button ID="btnExportExcel" runat="server" Text="Exportar para Excel" CssClass="cadbuttonfiltro"
                                                        OnClick="btnExportExcel_Click" />
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
                                                <rad:RadGrid ID="radGridRelatorio" runat="server" AllowPaging="True" AllowSorting="True"
                                                    AutoGenerateColumns="False" CssClass="dtg" GridLines="None" Skin="None" Width="100%" SortingSettings-SortToolTip="Clique para filtrar"
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
                                                            <rad:GridBoundColumn DataField="IdEscalacao" SortExpression="IdEscalacao" HeaderText="N&#186; Escala"
                                                                UniqueName="IdEscalacao">
                                                                <ItemStyle Width="30px" HorizontalAlign="Center" />
                                                            </rad:GridBoundColumn>
                                                            <rad:GridBoundColumn DataField="IdFilial" Visible="False" HeaderText="IdFilial" UniqueName="IdFilial">
                                                            </rad:GridBoundColumn>
                                                            <rad:GridBoundColumn DataField="AliasFilial" SortExpression="AliasFilial" HeaderText="Filial"
                                                                UniqueName="AliasFilial">
                                                                <ItemStyle Width="100px" HorizontalAlign="center" />
                                                            </rad:GridBoundColumn>
                                                            <rad:GridBoundColumn DataField="IdEscalaDepto" Visible="False" HeaderText="IdEscalaDepto"
                                                                UniqueName="IdEscalaDepto">
                                                            </rad:GridBoundColumn>
                                                             <rad:GridBoundColumn DataField="DesEscalaDepto" SortExpression="DesEscalaDepto" HeaderText="Descrição Escala"
                                                                UniqueName="DesEscalaDepto">
                                                                <ItemStyle Width="100px" HorizontalAlign="center" />
                                                            </rad:GridBoundColumn>
                                                            
                                                            <rad:GridBoundColumn DataField="IdTipoSolicitacao" Visible="False" HeaderText="IdTipoSolicitacao"
                                                                UniqueName="IdTipoSolicitacao">
                                                            </rad:GridBoundColumn>
                                                            <rad:GridBoundColumn DataField="DesTipoSolicitacao" SortExpression="DesTipoSolicitacao"
                                                                HeaderText="Tipo Escala" UniqueName="DesTipoSolicitacao">
                                                                <ItemStyle Width="175px" HorizontalAlign="Center" />
                                                                <HeaderStyle HorizontalAlign="center" />
                                                            </rad:GridBoundColumn>
                                                            <rad:GridBoundColumn DataField="NomeColaborador" SortExpression="NomeColaborador" HeaderText="Nome Colaborador"
                                                                UniqueName="NomeColaborador">
                                                                <HeaderStyle Width="300px" HorizontalAlign="center" />
                                                                <ItemStyle />
                                                            </rad:GridBoundColumn>
                                                            <rad:GridBoundColumn DataField="IdStatusSolicitacao" Visible="False" HeaderText="IdStatus"
                                                                UniqueName="IdStatusSolicitacao">
                                                            </rad:GridBoundColumn>
                                                            <rad:GridBoundColumn DataField="DataEscalacao" SortExpression="DataEscalacao" HeaderText="Data"
                                                                UniqueName="DataEscalacao" DataFormatString="{0:dd/MM/yyyy}">
                                                                <HeaderStyle Width="100px" />
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </rad:GridBoundColumn>
                                                            <rad:GridBoundColumn DataField="Horario" SortExpression="Horario" HeaderText="Horario"
                                                                UniqueName="Horario">
                                                                <HeaderStyle Width="100px" />
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </rad:GridBoundColumn>
                                                            <rad:GridBoundColumn DataField="NomeUsuarioSolicitante" SortExpression="NomeUsuarioSolicitante"
                                                                HeaderText="Solicitante" UniqueName="NomeUsuarioSolicitante">
                                                                <HeaderStyle Width="100px" />
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </rad:GridBoundColumn>

                                                            <rad:GridBoundColumn DataField="NomeUsuarioAprovador" SortExpression="NomeUsuarioAprovador"
                                                                HeaderText="Último Aprovador" UniqueName="NomeUsuarioAprovador">
                                                                <HeaderStyle Width="100px" />
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </rad:GridBoundColumn>
                                                            <rad:GridTemplateColumn UniqueName="TemplateColumn" Visible="False">
                                                                <ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="btnVisualizar" runat="server" AlternateText="Visualizar" CausesValidation="False"
                                                                        CommandArgument='<%# DataBinder.Eval(Container.DataItem, "IdEscalacao") %>' CommandName="Visualizar"
                                                                        ImageUrl="%%PATH%%/imagens/icones/ico_editar.gif"></asp:ImageButton>
                                                                </ItemTemplate>
                                                            </rad:GridTemplateColumn>
                                                            <rad:GridBoundColumn DataField="CorFonteHtml" SortExpression="CorFonteHtml" HeaderText="CorFonteHtml"
                                                                Visible="False" UniqueName="CorFonteHtml">
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
    <asp:HtmlIframe id="IframeAgenda" runat="server" style="z-index: 101; left: -500px; visibility: visible;
        position: absolute; top: 0px" name="gToday:contrast:agenda.js" src="../../Agenda/calendario.htm"
        frameborder="0" width="132" scrolling="no" height="142"/>
    </form>
</body>
</html>
