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
                                Especifica��o da tela:</td>
                        </tr>
                        <tr>
                            <td class="cadrad">
                                Esta tela serve para visualizar as "listas de pessoas" cadastradas no sistema. As
                                "listas de pessoas" servem para cadastrar grupos de visitantes cujas solicita��es
                                s�o feitas peri�dicamente de forma a facilitar o lan�amento das solicita��es de
                                acesso.<br />
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td class="cadlbl">
                                Regra de Neg�cio:</td>
                        </tr>
                        <tr>
                            <td class="cadrad">
                                <li>� poss�vel Ativar/Inativar um registro, basta clicar sobre o bot�o (Bot�o
                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Imagens/icones/ico_ativo.gif" />
                                    significa que a lista est� ativa e bot�o
                                    <asp:Image ID="Image2" runat="server" ImageUrl="~/Imagens/icones/ico_inativo.gif" />
                                    significa que a lista est� inativa).</li>
                                <li>Para realizar uma consulta, basta preencher os campos da tela e clicar no bot�o
                                    <asp:Image ID="Image3" runat="server" ImageUrl="~/Imagens/icones/buscar.jpg" />.<br />
                                    <br />
                                </li>
                                <li>Clique em um registro exibido abre tela para edi��o do cadastro.</li>
                                <li>Bot�o
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
