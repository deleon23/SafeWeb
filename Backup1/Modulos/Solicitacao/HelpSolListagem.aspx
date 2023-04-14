<%@ Page Language="C#" AutoEventWireup="true" Codebehind="HelpSolListagem.aspx.cs"
    Inherits="SafWeb.UI.Modulos.Solicitacao.HelpSolListagem" %>

<html>
<head>
    <link href="../../Estilos/Brinks.css" type="text/css" rel="stylesheet">
    <title>Ajuda - Listagem de Solicitação</title>
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
                                Esta tela serve para visualizar as solicitações cadastradas no sistema. As solicitações
                                servem para liberar acesso a um ou mais visitantes em uma ou mais áreas de uma filial.<br />
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td class="cadlbl">
                                Regra de Negócio:</td>
                        </tr>
                        <tr>
                            <td class="cadrad">
                                <li>É possível Ativar/Inativar uma solicitação, caso a solicitação ainda não tenha sido
                                    aprovado por nenhum nível (Pendente aprovação nível 1), a data de Início seja menor
                                    ou igual a data atual e somente pelo criador a solicitação (Solicitante), basta
                                    clicar sobre o botão (Botão
                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Imagens/icones/ico_ativo.gif" />
                                    significa que a lista está ativa e botão
                                    <asp:Image ID="Image2" runat="server" ImageUrl="~/Imagens/icones/ico_inativo.gif" />
                                    significa que a lista está inativa).</li>
                                <li>Para realizar uma consulta, basta preencher os campos da tela e clicar no botão
                                    <asp:Image ID="Image3" runat="server" ImageUrl="~/Imagens/icones/buscar.jpg" />.<br /><br />
                                </li>
                                <li>Clique em um registro exibido abre tela para edição do cadastro, caso a solicitação
                                    ainda não tenha sido aprovado por nenhum nível (Pendente aprovação nível 1), a data
                                    de Início seja menor ou igual a data atual e somente pelo criador a solicitação
                                    (Solicitante).</li>
                                <li>Botão
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
                                <b>Data Início e/ou Data Fim:</b> Clique na imagem
                                <asp:Image ID="Image5" runat="server" ImageUrl="~/Agenda/calbtn.gif" />
                                exibe um calendário, ao selecionar uma data o campo é preenchido com a data selecionada.
                                Após a data é possível inserir também o horário.
                                <asp:Image ID="Image6" runat="server" ImageUrl="~/Imagens/icones/data.jpg" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
