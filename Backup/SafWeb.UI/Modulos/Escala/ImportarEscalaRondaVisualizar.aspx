<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ImportarEscalaRondaVisualizar.aspx.cs"
    Inherits="SafWeb.UI.Modulos.Escala.ImportarEscalaRondaVisualizar" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp1" %>
    
<%@ Register TagPrefix="rad" Namespace="Telerik.WebControls" Assembly="RadGrid.Net2" %>
<%@ Register TagPrefix="rada" Namespace="Telerik.WebControls" Assembly="RadAjax.Net2" %>
<%@ Register TagPrefix="cc1" Namespace="FrameWork.WebControl" Assembly="FrameWork.WebControl" %>
<html>
<head runat="server">
    <title>%%SITE%%</title>
    <link href="../../Estilos/FrameWork.css" type="text/css" rel="stylesheet" />
    <link href="../../Estilos/Brinks.css" rel="stylesheet" type="text/css" />
    <link href="../../Office2007/Grid.Office2007.css" rel="stylesheet" type="text/css" />
    <script language="javascript">

        //desabilita o botão direito do mouse
        function click() {
            if (event.button == 2 || event.button == 3) {
                oncontextmenu = 'return false';
            }
        }
        document.onmousedown = click;
        document.oncontextmenu = new Function("return false;");


        //bloquea a teclas de voltar alt + setas
        function tecla() {
            var tecla = window.event.keyCode;
            if (tecla == 18) {
                event.keyCode = 0;
                event.returnValue = false;
            }
        }
        document.onkeydown = tecla;
    </script>
</head>
<body>
    <form id="form" runat="server" method="post">
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
                    <!-- ********************* PAINEL DE ERRO ****************** -->
                    <asp:Panel ID="pnlErro" runat="server" />
                    <!-- ***************** START: BARRA DE TITUTO ******************* -->
                    <table class="MarginTopBarra" cellspacing="0" cellpadding="0" width="760" align="center"
                        border="0">
                        <tr>
                            <td class="cadBarraTitulo" align="left" style="height: 25px; width: 750px">
                                <img src="../../Imagens/comum/bulletbarra.gif" align="absMiddle">
                                <asp:Label ID="Label2" runat="server" Text="Visualizar Escala / Troca de horário para importação"></asp:Label>
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
                                        <td class="cadmsg">
                                            &nbsp;<asp:Label ID="lblMensagem" runat="server" CssClass="cadlbl"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding-top: 10px; padding-bottom: 20px;">
        <table class="MarginTopConteudo" cellspacing="0" cellpadding="0" width="740" align="center"
                                                border="0">
                                                <tr>
                                                    <td class="cadlbl" align="right" style="width: 0;" width="100px">
                                                        <asp:Label ID="lblNomeColaborador" runat="server" Text="Colaborador:"></asp:Label>
                                                    </td>
                                                    
                                                    <td class="cadlbl" align="right" style="width: 50px;" width="100px">
                                                        &nbsp;</td>
                                                    
                                                    <td class="cadlbl" align="right" style="width: 50px;" width="100px">
                                                        <asp:Label ID="lblDataBusca" runat="server" Text="De:"></asp:Label>
                                                    </td>
                                                    <td align="right" class="cadlbl" width="520px">
                                                        &nbsp;</td>
                                                    <td align="right" class="cadlbl" width="520px">
                                                        <asp:Label ID="lblDataBusca0" runat="server" Text="Para:"></asp:Label>
                                                    </td>
                                                    <td align="right" class="cadlbl" width="520px" style="width: 260px">
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td width="100px" align="left" style="padding-bottom: 10px; width: 0;">
                                                        <asp:TextBox ID="txtNomeColaborador" runat="server" CssClass="cadddl" Width="150px">
                                                        </asp:TextBox>
                                                    </td>
                                                    <td width="100px" align="left" style="padding-bottom: 10px; width: 50px;">
                                                        &nbsp;</td>
                                                    <td width="100px" align="left" style="padding-bottom: 10px; width: 50px;">
                                                        <asp:DropDownList ID="ddlDatas" runat="server" CssClass="cadddl" Width="150px">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td width="100px" align="left" style="padding-bottom: 10px;  left: 10px; width: 50px;">
                                                        &nbsp;</td>
                                                    <td width="100px" align="left" style="padding-bottom: 10px;  left: 10px; width: 50px;">
                                                        <asp:DropDownList ID="ddlPeriodoImportar" runat="server" CssClass="cadddl" Width="150px">
                                                        </asp:DropDownList>
                                                        </td>
                                                    <td align="left" style="padding-bottom: 10px;" width="520px">
                                                        <asp:Button ID="btnFiltrarData" runat="server" CssClass="cadbutton100right"
                                                            OnClick="btnFiltrarData_Click" Text="Filtrar" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" colspan="6">
                                                        <!-- ********************* STAR: RADGRID ************************** -->

                                                        <!-- MarkUp 
                                                        OnItemCommand="radGridHorariosColaboradores_ItemCommand"
                                                        -->

                                                        <rad:radgrid ID="radGridHorariosColaboradores" runat="server" AllowPaging="True"
                                                            AllowSorting="True" AutoGenerateColumns="False" CssClass="dtg" GridLines="None"
                                                            Skin="None" Width="100%" Height="250px" SortingSettings-SortToolTip="Clique para filtrar" OnNeedDataSource="radGridHorariosColaboradores_NeedDataSource"
                                                            OnItemDataBound="radGridHorariosColaboradores_ItemDataBound" OnItemCommand="radGridHorariosColaboradores_ItemCommand">
                                                            <ClientSettings>
                                                                <Selecting AllowRowSelect="true" />
                                                            </ClientSettings>
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
                                                                        <table cellpadding="0" cellspacing="0" height="250" width="100%">
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


                                                                                    <cc1:FWMascara ID="txtPagina" runat="server" AutoPostBack="False" CssClass="pagtxtbox"
                                                                                        Mascara="NUMERO" MaxLength="4" Text='<%# (int)(DataBinder.Eval(Container, "OwnerTableView.CurrentPageIndex")) + 1 %>'
                                                                                        Width="50px"></cc1:FWMascara>



                                                                                    <asp:Label ID="Label4" runat="server" CssClass="paglbl">de</asp:Label>
                                                                                    <asp:Label ID="Label6" runat="server" CssClass="paglbl" Enabled="True"><%#DataBinder.Eval(Container, "Paging.PageCount").ToString()%></asp:Label>
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
                                                                    <rad:GridBoundColumn DataField="DesJornada" SortExpression="DesJornada"
                                                                        UniqueName="DesJornada" HeaderText="Jornada">
                                                                        <ItemStyle Width="1%"></ItemStyle>
                                                                    </rad:GridBoundColumn>

                                                                    <rad:GridTemplateColumn UniqueName="TemplateColumn">
                                                                        <HeaderTemplate>
                                                                            <asp:LinkButton CommandName="OrdenarData" ID="lblDescUltimoAcesso" Text="Datas/Horários"  runat="server"></asp:LinkButton>
                                                                        </HeaderTemplate>
                                                                        <HeaderStyle Width="25%"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblDataHora" runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </rad:GridTemplateColumn>


                                                                    <rad:GridBoundColumn DataField="NomesColaboradores" HeaderText="Colaboradores" SortExpression="NomesColaboradores"
                                                                        UniqueName="Colaboradores">
                                                                        <ItemStyle Width="25%"></ItemStyle>
                                                                    </rad:GridBoundColumn>
                                                                   
                                                                    <rad:GridBoundColumn DataField="CodigosColaboradores" SortExpression="CodigosColaboradores"
                                                                        UniqueName="CodigosColaboradores" Visible="false">
                                                                    </rad:GridBoundColumn>

                                                                    <rad:GridBoundColumn DataField="DataColaboradores"
                                                                        UniqueName="DataColaboradores" Visible="false">
                                                                    </rad:GridBoundColumn>


                                                                    <rad:GridTemplateColumn UniqueName="TemplateColumn" Visible="False">
                                                                        <ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:ImageButton ID="Editar" runat="server" AlternateText="Editar" CausesValidation="False"
                                                                                CommandArgument='<%# DataBinder.Eval(Container.DataItem, "IdEscalacao") %>' CommandName="Editar"
                                                                                ImageUrl="%%PATH%%/imagens/icones/ico_editar.gif" ToolTip="Editar esta Data">
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
                                                        </rad:radgrid>
                                                        <!-- ********************* END: RADGRID ************************** -->
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="220px">
                                                        &nbsp;</td>
                                                    <td width="520px" align="left" style="padding-left: 95px;">
                                                        &nbsp;</td>
                                                    <td width="520px" align="left" colspan="4" style="padding-left: 95px;">
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="220px">
                                                        &nbsp;
                                                    </td>
                                                    <td class="cadlbl" width="520" align="left" style="width: 0">
                                                        &nbsp;</td>
                                                    <td class="cadlbl" width="520" align="left" style="width: 0">
                                                        &nbsp;</td>
                                                    <td class="cadlbl" width="520" align="left" style="width: 260px">

                                                        &nbsp;</td>
                                                    <td class="cadlbl" width="520" align="left" style="width: 260px" colspan="2">

                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td width="220px">
                                                        &nbsp;
                                                        <asp:Button ID="btnExportExcel" runat="server" Text="Exportar para Excel" CssClass="cadbuttonfiltro"
                                                            OnClick="btnExportExcel_Click" />
                                                    </td>
                                                    <td width="150px">
                                                        &nbsp;</td>
                                                    <td width="150px">
                                                        <asp:CheckBox ID="chkHoraExtra" runat="server" CssClass="cadchk" 
                                                            Font-Bold="true" ForeColor="DarkRed" Height="20px" SelectionMode="Single" 
                                                            style="padding-left: 15px;" Text="Feriado" ToolTip="Feriado (hora extra)" 
                                                            Visible="true" Width="150px" />
                                                    </td>
                                                    <td style="padding-left: 20px;" width="370px">

                                                        &nbsp;</td>
                                                    <td style="padding-left: 20px; width: 0;" width="370px">

                                                        <asp:Button ID="btnEnviarAprovacao" runat="server" CssClass="cadbutton100left" Text="Importar"
                                                            OnClick="btnImportar_Click"/>
                                                    </td>
                                                    <td style="padding-left: 20px; width: 185px;" width="370px">

                                                        <asp:Button ID="btnVoltar" runat="server" CssClass="cadbutton100right" Text="Voltar"
                                                            OnClick="btnVoltar_Click" Enabled="false" Visible="false" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                                <!-- ********************* START: BARRA DE BOTÕES INFERIOR ************************** -->
                                <!-- ********************* END: BARRA DE BOTÕES INFERIOR ************************** -->
                            </td>
                        </tr>
                    </table>
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
