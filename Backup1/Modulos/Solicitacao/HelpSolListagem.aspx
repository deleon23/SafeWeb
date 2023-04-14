<%@ Page Language="C#" AutoEventWireup="true" Codebehind="HelpSolListagem.aspx.cs"
    Inherits="SafWeb.UI.Modulos.Solicitacao.HelpSolListagem" %>

<html>
<head>
    <link href="../../Estilos/Brinks.css" type="text/css" rel="stylesheet">
    <title>Ajuda - Listagem de Solicita��o</title>
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
                                Esta tela serve para visualizar as solicita��es cadastradas no sistema. As solicita��es
                                servem para liberar acesso a um ou mais visitantes em uma ou mais �reas de uma filial.<br />
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td class="cadlbl">
                                Regra de Neg�cio:</td>
                        </tr>
                        <tr>
                            <td class="cadrad">
                                <li>� poss�vel Ativar/Inativar uma solicita��o, caso a solicita��o ainda n�o tenha sido
                                    aprovado por nenhum n�vel (Pendente aprova��o n�vel 1), a data de In�cio seja menor
                                    ou igual a data atual e somente pelo criador a solicita��o (Solicitante), basta
                                    clicar sobre o bot�o (Bot�o
                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Imagens/icones/ico_ativo.gif" />
                                    significa que a lista est� ativa e bot�o
                                    <asp:Image ID="Image2" runat="server" ImageUrl="~/Imagens/icones/ico_inativo.gif" />
                                    significa que a lista est� inativa).</li>
                                <li>Para realizar uma consulta, basta preencher os campos da tela e clicar no bot�o
                                    <asp:Image ID="Image3" runat="server" ImageUrl="~/Imagens/icones/buscar.jpg" />.<br /><br />
                                </li>
                                <li>Clique em um registro exibido abre tela para edi��o do cadastro, caso a solicita��o
                                    ainda n�o tenha sido aprovado por nenhum n�vel (Pendente aprova��o n�vel 1), a data
                                    de In�cio seja menor ou igual a data atual e somente pelo criador a solicita��o
                                    (Solicitante).</li>
                                <li>Bot�o
                                    <asp:Image ID="Image4" runat="server" ImageUrl="~/Imagens/icones/incluir.jpg" />
                                    exibe a tela de cadastro.
                                <br />
                                <br />
                            </li>
                            </td>
                        </tr>
                        <tr>
                            <td class="cadlbl">
                                Campos:</td>
                        </tr>
                        <tr>
                            <td class="cadrad">
                                <b>Data In�cio e/ou Data Fim:</b> Clique na imagem
                                <asp:Image ID="Image5" runat="server" ImageUrl="~/Agenda/calbtn.gif" />
                                exibe um calend�rio, ao selecionar uma data o campo � preenchido com a data selecionada.
                                Ap�s a data � poss�vel inserir tamb�m o hor�rio.
                                <asp:Image ID="Image6" runat="server" ImageUrl="~/Imagens/icones/data.jpg" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
