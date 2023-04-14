<%@ Page Language="C#" AutoEventWireup="true" Codebehind="ListVisitantes.aspx.cs"
    Inherits="SafWeb.UI.Modulos.Solicitacao.ListVisitados" %>

<%@ Register TagPrefix="rad" Namespace="Telerik.WebControls" Assembly="RadGrid.Net2" %>
<%@ Register TagPrefix="cc1" Namespace="FrameWork.WebControl" Assembly="FrameWork.WebControl" %>
<html xmlns="http://www.w3.org/1999/xhtml">
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
        <table class="MarginTopConteudo" cellspacing="0" cellpadding="0" width="500px" border="0">
            <tr>
                <!-- ********************* START: CONTEÚDO ****************** -->
                <td width="325">
                    <asp:Label ID="lblNome" runat="server" Text="Nome:" CssClass="cadlbl"></asp:Label>
                </td>
                <td width="175">
                    <asp:Label ID="lblDocumento" runat="server" Text="Documento:" CssClass="cadlbl"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txtNome" runat="server" Width="300px" CssClass="cadtxt" OnTextChanged="txtNome_TextChanged"
                        AutoPostBack="True"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="txtDocumento" runat="server" Width="85px" CssClass="cadtxt" OnTextChanged="txtDocumento_TextChanged"
                        AutoPostBack="True"></asp:TextBox></td>
            </tr>
            <tr>
                <td colspan="2">
                    <br />
                    <!-- ********************* STAR: RADGRID ************************** -->
                    <rad:RadGrid ID="radGridPessoa" runat="server" AllowPaging="True" AllowSorting="True"
                        AutoGenerateColumns="False" CssClass="dtg" GridLines="None" OnItemCommand="radGridPessoa_ItemCommand"
                        OnItemDataBound="radGridPessoa_ItemDataBound" OnNeedDataSource="radGridPessoa_NeedDataSource"
                        Skin="None" Width="100%">
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
                                <rad:GridBoundColumn DataField="NomeColaborador" HeaderText="Nome" SortExpression="NomeColaborador"
                                    UniqueName="NomeColaborador">
                                    <ItemStyle Width="40%"></ItemStyle>
                                </rad:GridBoundColumn>
                                <rad:GridBoundColumn DataField="TipoDocumento" HeaderText="Tipo Documento" SortExpression="TipoDocumento"
                                    UniqueName="TipoDocumento">
                                    <ItemStyle Width="20%"></ItemStyle>
                                </rad:GridBoundColumn>
                                <rad:GridBoundColumn DataField="NumeroDocumento" HeaderText="Nº Documento" SortExpression="NumeroDocumento"
                                    UniqueName="NumeroDocumento">
                                    <ItemStyle Width="20%"></ItemStyle>
                                </rad:GridBoundColumn>
                                <rad:GridTemplateColumn UniqueName="TemplateColumn" Visible="false">
                                    <ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnEditar" runat="server" AlternateText="Editar" CausesValidation="False"
                                            CommandArgument='<%# DataBinder.Eval(Container.DataItem, "IdColaborador") %>'
                                            CommandName="Editar" ImageUrl="%%PATH%%/imagens/icones/ico_editar.gif" ToolTip="Editar">
                                        </asp:ImageButton>
                                    </ItemTemplate>
                                </rad:GridTemplateColumn>
                            </Columns>
                        </MasterTableView>
                    </rad:RadGrid>
                    <!-- ********************* END: RADGRID ************************** -->
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td align="right" class="backBarraBotoes" style="height: 31px" >
                                
                                <asp:Button ID="btnFechar" runat="server" CausesValidation="False" CssClass="cadbutton100"
                                    Text="Fechar" Width="50px" OnClick="btnFechar_Click"/>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
