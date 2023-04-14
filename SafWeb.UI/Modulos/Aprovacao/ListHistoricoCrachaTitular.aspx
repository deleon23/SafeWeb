<%@ Page Language="C#" AutoEventWireup="true" Codebehind="ListHistoricoCrachaTitular.aspx.cs"
    Inherits="SafWeb.UI.Modulos.Aprovacao.ListHistoricoCrachaTitular" %>

<%@ Register TagPrefix="rad" Namespace="Telerik.WebControls" Assembly="RadGrid.Net2" %>
<%@ Register TagPrefix="cc1" Namespace="FrameWork.WebControl" Assembly="FrameWork.WebControl" %>
<%@ Register TagPrefix="rada" Namespace="Telerik.WebControls" Assembly="RadAjax.Net2" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>%%SITE%%</title>
    <link href="../../Estilos/FrameWork.css" type="text/css" rel="stylesheet">
    <link href="../../Estilos/Brinks.css" type="text/css" rel="stylesheet">
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
                    <rada:RadAjaxPanel ID="RadAjaxPanel1" runat="server" ClientEvents-OnRequestStart="OnRequestStart" LoadingPanelID="lpaCadastro">
                    <asp:Panel ID="pnlErro" runat="server"></asp:Panel>
                    <asp:Panel ID="pnlLista" runat="server"></asp:Panel>
                    <!-- ****************** START: PAINEL DE CADASTRO ************************** -->
                    <asp:Panel runat="server" ID="pnlCadastro">
                        <!-- ***************** START: BARRA DE TITUTO ******************* -->
                        <table class="MarginTopBarra" cellspacing="0" cellpadding="0" width="760" align="center"
                            border="0">
                            <tr>
                                <td class="cadBarraTitulo" align="left" height="25">
                                    <img src="../../Imagens/comum/bulletbarra.gif" align="middle" />
                                    <asp:Label ID="lblListagem" runat="server" Text="Histórico"></asp:Label>
                                </td>
                            </tr>
                            <!-- ***************** END: BARRA DE TITUTO ******************* -->
                            <tr>
                                <td class="backboxconteudo" valign="top" style="height: 250px">
                                    <!-- ****************** START: FORMULARIO *********************** -->
                                    <table class="MarginTopConteudo" cellspacing="0" cellpadding="0" width="740" align="center"
                                        border="0">
                                        <tr>
                                            <td class="cadlbl" style="height: 223px">
                                                <asp:Label ID="lblDadosVisita" runat="server" Text="Dados da Permissão de Crachá Titular"></asp:Label>
                                                <table style="border: 1px solid black" width="740">
                                                    <tr>
                                                        <td class="cadlbl" width="300" colspan="3">
                                                            <asp:Label ID="lblCodSolic" runat="server" Text="Código da Solicitação:"></asp:Label></td>
                                                        <td width="7">
                                                        </td>
                                                        <td width="300" class="cadlbl">
                                                            <asp:Label ID="lblDataInicio" runat="server" Text="Data da Solicitação:"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3">
                                                            <asp:TextBox ID="txtCodSolic" runat="server" CssClass="cadtxt" Width="100px" Enabled="False"></asp:TextBox></td>
                                                        <td>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtDataInicio" runat="server" CssClass="cadtxt" Width="110px" Enabled="False"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="cadlbl" width="300" colspan="3">
                                                            <asp:Label ID="lblNomeVisitado" runat="server" Text="Nome do Colaborador:"></asp:Label></td>
                                                        <td width="7">
                                                        </td>
                                                        <td width="300" class="cadlbl">
                                                            <asp:Label ID="lblRE" runat="server" Text="RE:"></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3">
                                                            <asp:TextBox ID="txtNomeVisitado" runat="server" CssClass="cadtxt" Width="350px"
                                                                Enabled="False"></asp:TextBox></td>
                                                        <td>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtRE" runat="server" CssClass="cadtxt" MaxLength="50" Width="110px"
                                                                Enabled="False"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="cadlbl" colspan="3">
                                                            <asp:Label ID="lblFilial" runat="server" Text="Filial:"></asp:Label>
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td class="cadlbl">
                                                            &nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3">
                                                            <asp:TextBox ID="txtFilial" runat="server" CssClass="cadtxt" Enabled="False" MaxLength="50"
                                                                Width="200px"></asp:TextBox></td>
                                                        <td>
                                                        </td>
                                                        <td>
                                                            &nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td class="cadlbl" colspan="3">
                                                            <asp:Label ID="lblAreaVisitada" runat="server" Text="Áreas Visitadas:"></asp:Label>
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td class="cadlbl">
                                                            <asp:Label ID="lblObservacao" runat="server" Text="Motivo da Solicitação:"></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3">
                                                            <asp:ListBox ID="lstAreaVisita" runat="server" CssClass="cadlstBox" Height="80px"
                                                                Width="200px"></asp:ListBox></td>
                                                        <td>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtObservacao" runat="server" CssClass="cadlstBox" MaxLength="50"
                                                                Width="200px" Enabled="False" Height="80px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="cadlbl">
                                                            &nbsp;</td>
                                                        <td>
                                                        </td>
                                                        <td class="cadlbl">
                                                            &nbsp;</td>
                                                        <td>
                                                        </td>
                                                        <td class="cadlbl">
                                                        </td>
                                                    </tr>
                                                    </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="cadlbl">
                                                <br />
                                                <asp:Label ID="lblHistoricoSolicitacao" runat="server" Text="Histórico da Solicitação"></asp:Label>
                                                <table style="border: 1px solid black" width="740">
                                                    <tr>
                                                        <td class="cadlbl" width="350">
                                                            <table class="MarginTopConteudo" cellspacing="0" cellpadding="0" width="740" align="center"
                                                                border="0">
                                                                <tr>
                                                                    <td align="center">
                                                                        <!-- ********************* STAR: RADGRID ************************** -->
                                                                        <rad:radgrid ID="radSolicitacoes" runat="server" AllowPaging="True" AllowSorting="True"
                                                                            AutoGenerateColumns="False" SortingSettings-SortToolTip="Clique para filtrar" CssClass="dtg" GridLines="None" 
                                                                            Skin="None" Width="740px" OnItemCommand="radSolicitacoes_ItemCommand" 
                                                                            OnNeedDataSource="radSolicitacoes_NeedDataSource">
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
                                                                                    <rad:GridBoundColumn DataField="TIPOPESSOA" SortExpression="TIPOPESSOA" HeaderText="Tipo Pessoa" UniqueName="TIPOPESSOA">
                                                                                        <ItemStyle HorizontalAlign="Center" Width="20%" />
                                                                                    </rad:GridBoundColumn>
                                                                                    <rad:GridBoundColumn DataField="NOME" SortExpression="NOME" HeaderText="Nome" UniqueName="NOME">
                                                                                        <ItemStyle HorizontalAlign="Center" Width="35%" />
                                                                                    </rad:GridBoundColumn>
                                                                                    <rad:GridBoundColumn DataField="STATUS" SortExpression="STATUS" HeaderText="Status" UniqueName="STATUS">
                                                                                        <ItemStyle HorizontalAlign="Center" Width="20%" />
                                                                                    </rad:GridBoundColumn>
                                                                                    <rad:GridBoundColumn DataField="DATA" SortExpression="DATA" HeaderText="Data Aprovação/Criação" UniqueName="DATA" DataFormatString="{0:dd/MM/yyyy HH:mm}">
                                                                                        <ItemStyle HorizontalAlign="Center" Width="25%" />
                                                                                    </rad:GridBoundColumn>                                                               
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
                                                                <asp:Panel ID="pnlObservacao" runat="server" Visible="False">
                                                                    <tr>
                                                                        <td align="left">
                                                                            <br />
                                                                            <asp:Label ID="lblObservacaoReprovacao" runat="server" Text="Observação:" CssClass="cadlbl"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="center">
                                                                            <asp:TextBox ID="txtObservacaoReprovacao" CssClass="cadlstBox" runat="server" Enabled="false" TextMode="multiline" Width="740px" Height="50px"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                </asp:Panel>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>                                                
                                            </td>
                                        </tr>
                                    </table>                                    
                                    <br />
                                    <!-- ********************* START: BARRA DE BOTÕES INFERIOR ************************** -->
                                    <table cellspacing="0" cellpadding="0" width="760" align="center" border="0">
                                        <caption>
                                        </caption>
                                        <tr>
                                            <td class="backBarraBotoes" align="right" style="height: 30px">
                                                <asp:Button ID="btnVoltar" runat="server" Text="Voltar" CssClass="cadbutton100" OnClick="btnVoltar_Click">
                                                </asp:Button>
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
                    <rada:AjaxLoadingPanel ID="lpaCadastro" Width="75px" runat="server" HorizontalAlign="Center" Transparency="30" Height="75px">
                        <asp:Image ID="Image1" Style="margin-top: 100px" runat="server" ImageUrl="%%PATH%%/RadControls/Ajax/Skins/Default/Loading.gif" AlternateText="Aguarde ..."></asp:Image>
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
    </form>
</body>
</html>
