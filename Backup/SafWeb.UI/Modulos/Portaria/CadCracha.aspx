<%@ Page Language="C#" AutoEventWireup="true" Codebehind="CadCracha.aspx.cs" Inherits="SafWeb.UI.Modulos.Portaria.CadCracha" %>

<%@ Register TagPrefix="rad" Namespace="Telerik.WebControls" Assembly="RadGrid.Net2" %>
<%@ Register TagPrefix="cc1" Namespace="FrameWork.WebControl" Assembly="FrameWork.WebControl" %>
<%@ Register TagPrefix="rada" Namespace="Telerik.WebControls" Assembly="RadAjax.Net2" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>%%SITE%%</title>
    <link href="../../Estilos/FrameWork.css" type="text/css" rel="stylesheet">
    <link href="../../Estilos/Brinks.css" type="text/css" rel="stylesheet">
    <link href="../../Scripts/Mascara.js" type="text/javascript" rel="Stylesheet">

    <script language="javascript">
        
        function AbrirHelpCad()
        {
            window.open("HelpCrachaCadastro.aspx", "JANELA", "height = 400, width = 530");
        }
        
        function AbrirHelpList()
        {
            window.open("HelpCrachaListagem.aspx", "JANELA", "height = 400, width = 530");
        }
    </script>

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
                    <!-- ********************* START: CONTEÚDO ****************** -->
                    <!-- ****************** START: PAINEL AJAX ************************** -->
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
                                        <asp:Label ID="lblListagem" runat="server" Text="Listagem de Crachá"></asp:Label>
                                    </td>
                                    <td class="cadBarraTitulo" align="right" height="25" style="width: 9px">
                                        <asp:ImageButton ID="btnHelpList" runat="server" AlternateText="Ajuda" ImageUrl="%%PATH%%/imagens/icones/ico_help.jpg"
                                            ImageAlign="AbsMiddle" Visible="False" />
                                    </td>
                                </tr>
                                <!-- ***************** END: BARRA DE TITUTO ******************* -->
                                <tr>
                                    <td class="backboxconteudo" valign="top" height="250" colspan="2">
                                        <!-- ****************** START: FILTROS ************************** -->
                                        <table class="MarginTopConteudo" cellspacing="0" cellpadding="0" width="740" align="center"
                                            border="0">
                                            <tr>
                                                <td colspan="7" class="cadmsg">
                                                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List" />
                                                    <asp:Label ID="lblMensagemListagem" runat="server" Text="Label" Visible="false" CssClass="cadlbl"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="cadlbl">
                                                    <asp:Label ID="lblRegional" runat="server" Text="Regional:"></asp:Label>
                                                </td>
                                                <td width="5">
                                                </td>
                                                <td class="cadlbl">
                                                    <asp:Label ID="lblFilial" runat="server" Text="Filial:"></asp:Label>
                                                </td>
                                                <td width="5">
                                                </td>
                                                <td class="cadlbl">
                                                    <asp:Label ID="lblTipoCracha" runat="server" Text="Tipo de Crachá:"></asp:Label>
                                                </td>
                                                <td width="5">
                                                </td>
                                                <td class="cadlbl">
                                                    <asp:Label ID="lblDocumento" runat="server" Text="Crachá:"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:DropDownList ID="ddlRegional" CssClass="cadddl" runat="server" AutoPostBack="True"
                                                        OnSelectedIndexChanged="ddlRegional_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlFilial" CssClass="cadddl" runat="server" Enabled="False">
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlTipoCracha" CssClass="cadddl" runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCracha" runat="server" CssClass="cadtxt" MaxLength="12"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="7" align="center">
                                                    <br />
                                                    <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="cadbuttonfiltro"
                                                        OnClick="btnBuscar_Click" CausesValidation="False" />
                                                    <br />
                                                </td>
                                            </tr>
                                        </table>
                                        <!-- ********************** END: FILTROS ************************** -->
                                        <table class="MarginTopConteudo" cellspacing="0" cellpadding="0" width="740" align="center"
                                            border="0">
                                            <tr>
                                                <td align="center">
                                                    <!-- ********************* STAR: RADGRID ************************** -->
                                                    <rad:RadGrid ID="radCrachas" runat="server" AllowPaging="True" AllowSorting="True"
                                                        AutoGenerateColumns="False" CssClass="dtg" GridLines="None" Skin="None" Width="720px"
                                                        OnItemCommand="radCrachas_ItemCommand" OnItemDataBound="radCrachas_ItemDataBound"
                                                        OnNeedDataSource="radCrachas_NeedDataSource">
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

                                                                                <cc1:FWMascara ID="FWMascara1" runat="server" AutoPostBack="False" CssClass="pagtxtbox"
                                                                                    Mascara="NUMERO" MaxLength="4" Text='<%# (int)(DataBinder.Eval(Container, "OwnerTableView.CurrentPageIndex")) + 1 %>'
                                                                                    Width="50px"></cc1:FWMascara>

                                                                                <!-- ***************** MarkUp *******************
                                                                                <cc1:FWMascara ID="txtPagina" runat="server" AutoPostBack="False" CssClass="pagtxtbox"
                                                                                    Mascara="NUMERO" MaxLength="4" Text='<%# (int)(DataBinder.Eval(Container, "OwnerTableView.CurrentPageIndex")) + 1 %>'
                                                                                    Width="50px"></cc1:FWMascara>
                                                                                -->    

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
                                                                <rad:GridBoundColumn DataField="CodCracha" Visible="false" HeaderText="CodCracha"
                                                                    UniqueName="CodCracha" SortExpression="CodCracha">
                                                                </rad:GridBoundColumn>
                                                                <rad:GridBoundColumn DataField="NumCracha" HeaderText="Crachá" UniqueName="NumCracha"
                                                                    SortExpression="NumCracha">
                                                                    <ItemStyle HorizontalAlign="Center" Width="20%"></ItemStyle>
                                                                </rad:GridBoundColumn>
                                                                <rad:GridBoundColumn DataField="CodCrachaTipo" Visible="false" HeaderText="CodCrachaTipo"
                                                                    UniqueName="CodCrachaTipo" SortExpression="CodCrachaTipo">
                                                                </rad:GridBoundColumn>
                                                                <rad:GridBoundColumn DataField="DesCrachaTipo" HeaderText="Tipo Crachá" UniqueName="DesCrachaTipo"
                                                                    SortExpression="DesCrachaTipo">
                                                                    <ItemStyle HorizontalAlign="Center" Width="20%"></ItemStyle>
                                                                </rad:GridBoundColumn>
                                                                <rad:GridBoundColumn DataField="CodFilial" Visible="false" HeaderText="CodFilial"
                                                                    UniqueName="CodFilial" SortExpression="CodFilial">
                                                                </rad:GridBoundColumn>
                                                                <rad:GridBoundColumn DataField="AliasFilial" HeaderText="Filial" UniqueName="AliasFilial"
                                                                    SortExpression="AliasFilial">
                                                                    <ItemStyle HorizontalAlign="Center" Width="20%"></ItemStyle>
                                                                </rad:GridBoundColumn>
                                                                <rad:GridBoundColumn DataField="CodRegional" Visible="false" HeaderText="CodRegional"
                                                                    UniqueName="CodRegional" SortExpression="CodRegional">
                                                                </rad:GridBoundColumn>
                                                                <rad:GridBoundColumn DataField="FlgSituacao" Visible="false" HeaderText="FlgSituacao"
                                                                    UniqueName="FlgSituacao" SortExpression="FlgSituacao">
                                                                </rad:GridBoundColumn>
                                                                <rad:GridBoundColumn DataField="DesObservacao" HeaderText="Observação" UniqueName="DesObservacao"
                                                                    SortExpression="DesObservacao">
                                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                </rad:GridBoundColumn>
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
                                            <tr>
                                                <td>
                                                    <!-- ********************* START: PAINEL OBSERVACAO ************************** -->
                                                    <asp:Panel ID="pnlObservacao" Visible="false" runat="server" HorizontalAlign="Center">
                                                        <br />
                                                        <table class="MarginTopConteudo" cellspacing="0" cellpadding="0" width="740" align="center"
                                                            border="0">
                                                            <tr>
                                                                <td class="cadlbl">
                                                                    <asp:Label ID="lblObservacao" runat="server" Text="Observação:"></asp:Label>
                                                                    <asp:RequiredFieldValidator ID="rfvObs" runat="server" ControlToValidate="txtObservacao"
                                                                        CssClass="cadlbl" ErrorMessage="Campo Obrigatório" ValidationGroup="Obs">*</asp:RequiredFieldValidator></td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox ID="txtObservacao" runat="server" CssClass="cadlstBox" MaxLength="100"
                                                                        Width="100%" Height="50px" Font-Names="Verdana" Font-Size="XX-Small" TextMode="MultiLine"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="7">
                                                                    <!-- ********************* START: BARRA DE BOTÕES INFERIOR ************************** -->
                                                                    <table align="center" border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                        <tr>
                                                                            <td align="center">
                                                                                <asp:Button ID="btnGravarObs" runat="server" CssClass="cadbutton100" Text="Gravar"
                                                                                    ValidationGroup="Obs" OnClick="btnGravarObs_Click" />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                    <!-- ********************* END: BARRA DE BOTÕES INFERIOR ************************** -->
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <br />
                                                    </asp:Panel>
                                                    <!-- ********************* END: PAINEL OBSERVACAO ************************** -->
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            <!-- ********************* START: BARRA DE BOTÕES INFERIOR ************************** -->
                            <table align="center" border="0" cellpadding="0" cellspacing="0" width="760">
                                <tr>
                                    <td align="right" class="backBarraBotoes">
                                        <asp:Button ID="btnIncluir" runat="server" Text="Incluir" CssClass="cadbutton100"
                                            OnClick="btnIncluir_Click" CausesValidation="False"></asp:Button>
                                    </td>
                                </tr>
                            </table>
                            <!-- ********************* END: BARRA DE BOTÕES INFERIOR ************************** -->
                        </asp:Panel>
                        <!-- ****************** END: PAINEL DE LISTAGEM ************************** -->
                        <!-- ****************** START: PAINEL DE CADASTRO ************************** -->
                        <asp:Panel runat="server" ID="pnlCadastro">
                            <!-- ***************** START: BARRA DE TITUTO ******************* -->
                            <table class="MarginTopBarra" cellspacing="0" cellpadding="0" width="760" align="center"
                                border="0">
                                <tr>
                                    <td class="cadBarraTitulo" align="left" height="25" style="width: 750px">
                                        <img src="../../Imagens/comum/bulletbarra.gif" align="middle">
                                        <asp:Label ID="lblCadastro" runat="server" Text="Cadastro de Crachá"></asp:Label>
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
                                                <td colspan="7" class="cadmsg">
                                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" />
                                                    <asp:Label ID="lblMensagemCad" runat="server" Text="Label" Visible="False" CssClass="cadlbl"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="cadlbl">
                                                    <asp:Label ID="lblRegionalCad" runat="server" Text="Regional:"></asp:Label>
                                                </td>
                                                <td width="5">
                                                </td>
                                                <td class="cadlbl">
                                                    <asp:Label ID="lblFilialCad" runat="server" Text="Filial:"></asp:Label>
                                                </td>
                                                <td width="5">
                                                </td>
                                                <td class="cadlbl">
                                                    <asp:Label ID="lblTipoCrachaCad" runat="server" Text="Tipo de Crachá:"></asp:Label>
                                                </td>
                                                <td width="5">
                                                </td>
                                                <td class="cadlbl">
                                                    <asp:Label ID="lblCrachaCad" runat="server" Text="Crachá:"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:DropDownList ID="ddlRegionalCad" CssClass="cadddl" runat="server" AutoPostBack="True"
                                                        OnSelectedIndexChanged="ddlRegionalCad_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvRegional" runat="server" ControlToValidate="ddlRegionalCad"
                                                        CssClass="cadlbl" InitialValue="0" ValidationGroup="vgCadCracha">*</asp:RequiredFieldValidator></td>
                                                <td>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlFilialCad" CssClass="cadddl" runat="server" Enabled="False">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvFilialCad" runat="server" ControlToValidate="ddlFilialCad"
                                                        CssClass="cadlbl" InitialValue="0" ValidationGroup="vgCadCracha">*</asp:RequiredFieldValidator></td>
                                                <td>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlTipoCrachaCad" CssClass="cadddl" runat="server" Enabled="False">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvTipoCrachaCad" runat="server" ControlToValidate="ddlTipoCrachaCad"
                                                        CssClass="cadlbl" InitialValue="0" ValidationGroup="vgCadCracha">*</asp:RequiredFieldValidator></td>
                                                <td>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCrachaCad" runat="server" CssClass="cadtxt" MaxLength="12"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvCrachaCad" runat="server" ControlToValidate="txtCrachaCad"
                                                        CssClass="cadlbl" ValidationGroup="vgCadCracha">*</asp:RequiredFieldValidator></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            <!-- ********************* START: BARRA DE BOTÕES INFERIOR ************************** -->
                            <table align="center" border="0" cellpadding="0" cellspacing="0" width="760">
                                <tr>
                                    <td align="right" class="backBarraBotoes">
                                        <asp:Button ID="btnGravarSair" runat="server" CssClass="cadbutton100" Text="Gravar e Sair"
                                            OnClick="btnGravarSair_Click" ValidationGroup="vgCadCracha" />
                                        &nbsp; &nbsp;<asp:Button ID="btnGravar" runat="server" CssClass="cadbutton100" Text="Gravar"
                                            OnClick="btnGravar_Click" ValidationGroup="vgCadCracha" />
                                        &nbsp;&nbsp;<asp:Button ID="Button1" runat="server" CausesValidation="False" CssClass="cadbutton100"
                                            Text="Voltar" Width="50px" OnClick="btnVoltar_Click" />
                                    </td>
                                </tr>
                            </table>
                            <!-- ********************* END: BARRA DE BOTÕES INFERIOR ************************** -->
                        </asp:Panel>
                        <!-- ****************** END: PAINEL DE CADASTRO ************************** -->
                        <br />
                    </rada:RadAjaxPanel>
                    <rada:AjaxLoadingPanel ID="lpaCadastro" Width="75px" runat="server" HorizontalAlign="Center"
                        Transparency="30" Height="75px">
                        <asp:Image ID="Image1" Style="margin-top: 100px" runat="server" ImageUrl="%%PATH%%/RadControls/Ajax/Skins/Default/Loading.gif"
                            AlternateText="Aguarde ..."></asp:Image>
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
