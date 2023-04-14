<%@ Page Language="C#" AutoEventWireup="true" Codebehind="ListColaboradoresLista.aspx.cs"
    Inherits="SafWeb.UI.Modulos.Solicitacao.ListColaboradoresLista" %>

<%@ Register TagPrefix="rad" Namespace="Telerik.WebControls" Assembly="RadGrid.Net2" %>
<%@ Register TagPrefix="cc1" Namespace="FrameWork.WebControl" Assembly="FrameWork.WebControl" %>
<%@ Register TagPrefix="rada" Namespace="Telerik.WebControls" Assembly="RadAjax.Net2" %>
<html>
<head>
    <link href="../../Estilos/Brinks.css" type="text/css" rel="stylesheet">

    <script language="javascript">
    
        function GetRadWindow()
        {
            var oWindow = null;
            if (window.radWindow) oWindow = window.radWindow;
            else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;

            return oWindow;

        } 
          
        //Fecha a RadWindow
        function CloseWin()   
        {   
             //Get the RadWindow   
             var oWindow = GetRadWindow();  
             //oWindow.BrowserWindow.location.reload(); 
             //Call its Close() method   
             oWindow.Close();   
        }
        
    </script>

</head>
<body>
    <form id="form" method="post" runat="server">
        <rada:RadAjaxPanel ID="RadAjaxPanel1" runat="server" ClientEvents-OnRequestStart="OnRequestStart"
            LoadingPanelID="lpaCadastro">
            <table border="0" cellpadding="0" cellspacing="0" class="MarginTopConteudo" width="500px">
                <tr>
                    <td colspan="3" style="height: 303px">
                        <!-- ********************* STAR: RADGRID ************************** -->
                        &nbsp;<rad:RadGrid ID="radGridColaborador" runat="server" AllowPaging="false" AllowSorting="false"
                            AutoGenerateColumns="False" Height="265px" CssClass="dtg" GridLines="None" Skin="None"
                            Width="100%" OnItemCommand="radGridColaborador_ItemCommand" OnItemDataBound="radGridColaborador_ItemDataBound"
                            OnNeedDataSource="radGridColaborador_NeedDataSource">
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
                                    <rad:GridBoundColumn DataField="IdColaborador" SortExpression="IdColaborador" UniqueName="IdColaborador"
                                        Visible="false">
                                    </rad:GridBoundColumn>
                                    <rad:GridBoundColumn DataField="NomeColaborador" HeaderText="Nome Visitante" SortExpression="NomeColaborador"
                                        UniqueName="NomeColaborador">
                                        <ItemStyle Width="50%"></ItemStyle>
                                    </rad:GridBoundColumn>
                                    <rad:GridBoundColumn DataField="DescricaoEmpresa" HeaderText="Empresa Visitante"
                                        SortExpression="DescricaoEmpresa" UniqueName="DescricaoEmpresa">
                                        <ItemStyle Width="50%"></ItemStyle>
                                    </rad:GridBoundColumn>
                                    <rad:GridButtonColumn CommandName="Ativar" UniqueName="Ativar" ButtonType="ImageButton"
                                        ImageUrl="%%PATH%%/imagens/icones/ico_Ativo.gif" Text="Ativar">
                                        <ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
                                    </rad:GridButtonColumn>
                                </Columns>
                            </MasterTableView>
                        </rad:RadGrid>
                    </td>
                </tr>
            </table>
        </rada:RadAjaxPanel>
        <rada:AjaxLoadingPanel ID="lpaCadastro" Width="75px" runat="server" HorizontalAlign="Center"
            Transparency="30" Height="75px">
            <asp:Image ID="Image1" Style="margin-top: 100px" runat="server" ImageUrl="%%PATH%%/RadControls/Ajax/Skins/Default/Loading.gif"
                AlternateText="Aguarde ..."></asp:Image>
        </rada:AjaxLoadingPanel>
        <rada:RadAjaxPanel ID="RadAjaxPanel2" runat="server" ClientEvents-OnRequestStart="OnRequestStart"
            LoadingPanelID="lpaCadastro" EnableAJAX="false">
            <table border="0" cellpadding="0" cellspacing="0" class="MarginTopConteudo" width="500px">
                <tr>
                    <td colspan="3">
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td align="right" class="backBarraBotoes" style="height: 31px">
                                    <asp:Button ID="btnFechar" runat="server" CausesValidation="False" CssClass="cadbutton100"
                                        OnClick="btnFechar_Click" Text="Fechar" Width="50px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </rada:RadAjaxPanel>
    </form>
</body>
</html>
