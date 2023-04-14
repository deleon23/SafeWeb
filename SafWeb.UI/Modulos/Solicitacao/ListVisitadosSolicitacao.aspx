<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ListVisitadosSolicitacao.aspx.cs" Inherits="SafWeb.UI.Modulos.Solicitacao.ListVisitadosSolicitacao" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <link href="../../Estilos/Brinks.css" type="text/css" rel="stylesheet">
</head>
<body>
    <form id="form" method="post" runat="server" c>
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
                    <asp:TextBox ID="txtNome" runat="server" Width="300px" CssClass="cadtxt"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="txtDocumento" runat="server" Width="85px" CssClass="cadtxt"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <br />
                    <!-- ********************* STAR: DATAGRID ************************** -->
                    <asp:DataGrid ID="dtgPessoa" runat="server" CssClass="dtg" Width="500px" AllowPaging="True"
                        AllowSorting="True" AutoGenerateColumns="False" GridLines="None" CellPadding="0"
                        BorderWidth="0" OnItemDataBound="dtgPessoa_ItemDataBound" OnItemCommand="dtgPessoa_ItemCommand">
                        <AlternatingItemStyle CssClass="dtgItemStyleAlternate"></AlternatingItemStyle>
                        <ItemStyle CssClass="dtgItemStyle"></ItemStyle>
                        <HeaderStyle HorizontalAlign="Center" CssClass="dtgHeaderStyle"></HeaderStyle>
                        <Columns>
                            <asp:BoundColumn DataField="NOME" SortExpression="NOME" HeaderText="Nome">
                                <ItemStyle Width="40%" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="TIPODOCUMENTO" SortExpression="TIPODOCUMENTO" HeaderText="Tipo Documento">
                                <ItemStyle Width="20%" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="DOCUMENTO" SortExpression="DOCUMENTO" HeaderText="Documento">
                                <ItemStyle Width="20%" />
                            </asp:BoundColumn>
                            <asp:TemplateColumn Visible="false">
                                <HeaderStyle Width="5%"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgEditar" runat="server" ImageUrl="~/imagens/icones/ico_editar.gif"
                                        CommandName="Editar" Visible="False"></asp:ImageButton>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <PagerStyle Visible="False"></PagerStyle>
                    </asp:DataGrid>
                    <!-- ********************* START: DIV ************************** -->
                    <div runat="server" id="divPessoas" visible="false">
                        <table style="border: 1px solid #A9BECD; padding: 2px 2px 2px 2px;" width="500px"
                            height="50px">
                            <tr>
                                <td class="cadlbl">
                                    <asp:Label ID="lblNenhumRegistro" runat="server" Text="Nenhum Registro Encontrado..."></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <!-- ********************* END: DIV ************************** -->
                    <!-- ********************* END: DATAGRID ************************** -->
                </td>
                <!-- ********************* END: CONTEÚDO ****************** -->
            </tr>
        </table>
    </form>
</body>
</html>

