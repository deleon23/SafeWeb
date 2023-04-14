<%@ Page Language="C#" AutoEventWireup="true" Codebehind="HelpListaListagem.aspx.cs"
    Inherits="SafWeb.UI.Modulos.Solicitacao.HelpListaListagem" %>

<html>
<head>
    <link href="../../Estilos/Brinks.css" type="text/css" rel="stylesheet">
    <title>Ajuda - Listagem de Listas</title>
</head>
<body>
    <form id="form" method="post" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" class="MarginTopConteudo" width="500px"
            style="height: 500px">
            <tr valign="top">
                <td colspan="3" style="height: 280px">
                    <table>
                        <tr>
                            <td class="cadlbl">
                                Especificação da tela:</td>
                        </tr>
                        <tr>
                            <td class="cadrad">
                                Esta tela serve para visualizar as "listas de pessoas" cadastradas no sistema. As
                                "listas de pessoas" servem para cadastrar grupos de visitantes cujas solicitações
                                são feitas periódicamente de forma a facilitar o lançamento das solicitações de
                                acesso.<br />
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td class="cadlbl">
                                Regra de Negócio:</td>
                        </tr>
                        <tr>
                            <td class="cadrad">
                                <li>É possível Ativar/Inativar um registro, basta clicar sobre o botão (Botão
                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Imagens/icones/ico_ativo.gif" />
                                    significa que a lista está ativa e botão
                                    <asp:Image ID="Image2" runat="server" ImageUrl="~/Imagens/icones/ico_inativo.gif" />
                                    significa que a lista está inativa).</li>
                                <li>Para realizar uma consulta, basta preencher os campos da tela e clicar no botão
                                    <asp:Image ID="Image3" runat="server" ImageUrl="~/Imagens/icones/buscar.jpg" />.<br />
                                    <br />
                                </li>
                                <li>Clique em um registro exibido abre tela para edição do cadastro.</li>
                                <li>Botão
                                    <asp:Image ID="Image4" runat="server" ImageUrl="~/Imagens/icones/incluir.jpg" />
                                    exibe a tela de cadastro.</li>
                                <br />
                                <br />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
