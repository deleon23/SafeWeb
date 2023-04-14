<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ListCracha.aspx.cs" Inherits="SafWeb.UI.Modulos.Portaria.ListCracha" %>

<%@ Register TagPrefix="rad" Namespace="Telerik.WebControls" Assembly="RadGrid.Net2" %>
<%@ Register TagPrefix="cc1" Namespace="FrameWork.WebControl" Assembly="FrameWork.WebControl" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="../../Estilos/Brinks.css" type="text/css" rel="stylesheet">
    <link href="../../Scripts/Mascara.js" type="text/css" rel="stylesheet">

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
    <form id="form1" runat="server">
        <table class="MarginTopConteudo" cellspacing="0" cellpadding="0" width="500px" border="0">
            <tr>
                <!-- ********************* START: CONTEÚDO ****************** -->
                <td align="center">
                    <!-- ****************** START: FILTROS ************************** -->
                    <table class="MarginTopConteudo" cellspacing="0" cellpadding="0" width="490" align="center"
                        border="0">
                        <tr>
                            <td colspan="7" class="cadmsg">
                                &nbsp;
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
                                <asp:DropDownList ID="ddlRegional" vi CssClass="cadddl" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlRegional_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td></td>
                            <td>
                                <asp:DropDownList ID="ddlFilial" CssClass="cadddl" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td></td>
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
                            </td>
                        </tr>
                    </table>
                    <!-- ********************** END: FILTROS ************************** -->
                </td>        
            </tr>
            <tr>
                <td align="center">
                    <br />
                    <!-- ********************* STAR: RADGRID ************************** -->
                    <rad:RadGrid ID="radCrachas" runat="server" AllowPaging="True" AllowSorting="True"
                        AutoGenerateColumns="False" CssClass="dtg" GridLines="None" 
                        Skin="None" Width="490px" OnItemCommand="radCrachas_ItemCommand" OnItemDataBound="radCrachas_ItemDataBound" OnNeedDataSource="radCrachas_NeedDataSource">
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
                                <rad:GridBoundColumn DataField="CodCracha" Visible="false" HeaderText="CodCracha" UniqueName="CodCracha" SortExpression="CodCracha"></rad:GridBoundColumn>
                                <rad:GridBoundColumn DataField="NumCracha" HeaderText="Crachá" UniqueName="NumCracha" SortExpression="NumCracha">
                                    <ItemStyle HorizontalAlign="Center" Width="20%"></ItemStyle>
                                </rad:GridBoundColumn>
                                <rad:GridBoundColumn DataField="CodCrachaTipo" Visible="false" HeaderText="CodCrachaTipo" UniqueName="CodCrachaTipo" SortExpression="CodCrachaTipo"></rad:GridBoundColumn>
                                <rad:GridBoundColumn DataField="DesCrachaTipo" HeaderText="Tipo Crachá" UniqueName="DesCrachaTipo" SortExpression="DesCrachaTipo">
                                    <ItemStyle HorizontalAlign="Center" Width="20%"></ItemStyle>
                                </rad:GridBoundColumn>
                                <rad:GridBoundColumn DataField="CodFilial" Visible="false" HeaderText="CodFilial" UniqueName="CodFilial" SortExpression="CodFilial"></rad:GridBoundColumn>
                                <rad:GridBoundColumn DataField="AliasFilial" HeaderText="Filial" UniqueName="AliasFilial" SortExpression="AliasFilial">
                                    <ItemStyle HorizontalAlign="Center" Width="20%"></ItemStyle>
                                </rad:GridBoundColumn>
                                <rad:GridBoundColumn DataField="CodRegional" Visible="false" HeaderText="CodRegional" UniqueName="CodRegional" SortExpression="CodRegional"></rad:GridBoundColumn>
                                <rad:GridBoundColumn DataField="FlgSituacao" Visible="false" HeaderText="FlgSituacao" UniqueName="FlgSituacao" SortExpression="FlgSituacao"></rad:GridBoundColumn>
                                <rad:GridBoundColumn DataField="DesObservacao" HeaderText="Observação" UniqueName="DesObservacao" SortExpression="DesObservacao">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </rad:GridBoundColumn>
                                <rad:GridTemplateColumn UniqueName="TemplateColumn4">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgAtivar" runat="server" ImageUrl="~/imagens/icones/ico_Ativo.gif"></asp:ImageButton>
                                    </ItemTemplate>
                                </rad:GridTemplateColumn>  
                                <rad:GridTemplateColumn Visible="False" UniqueName="TemplateColumn5">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgSelecionar" runat="server" ImageUrl="~/imagens/icones/ico_Ativo.gif"
                                            CommandName="Selecionar" Visible="false"></asp:ImageButton>
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
                    <br />
                </td>
            </tr>
            <tr>
                <td>
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td align="right" class="backBarraBotoes" style="height: 31px" >
                                
                                <asp:Button ID="Button2" runat="server" CausesValidation="False" CssClass="cadbutton100"
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
