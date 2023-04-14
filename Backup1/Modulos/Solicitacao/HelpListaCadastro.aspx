<%@ Page Language="C#" AutoEventWireup="true" Codebehind="HelpListaCadastro.aspx.cs"
    Inherits="SafWeb.UI.Modulos.Solicitacao.HelpListaCadastro" %>

<html>
<head>
    <link href="../../Estilos/Brinks.css" type="text/css" rel="stylesheet">
    <title>Ajuda - Cadastro de Listas</title>
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
                                Esta tela serve para cadastrar/editar uma "lista de pessoas". As "listas de pessoas"
                                servem para cadastrar grupos de visitantes cujas solicitações são feitas periódicamente
                                de forma a facilitar o lançamento das solicitações de acesso.
                                <br />
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td class="cadlbl">
                                Regra de Negócio:</td>
                        </tr>
                        <tr>
                            <td class="cadrad">
                                - Dados da Lista<br />
                                <br />
                                <li>Campo Código da Lista é preenchido automáticamente.</li>
                                <br />
                                <br />
                                <li>Campos Nome da Lista, Regional e Filial são obrigatórios.</li>
                                <br />
                                <br />
                                <br />
                                - Dados do Visitante<br />
                                <li>Selecione o tipo de visitante e clique no botão
                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Imagens/icones/ico_Visualizar.gif" />
                                    para exibir uma pop-up com a listagem do tipo de visitante selecionado.</li>
                                <br />
                                <br />
                                <li>Clique em um registro exibido na pop-up, preenche os campos Tipo do Documento, Documento
                                    e Empresa conforme dados do registro selecionado e são bloqueados para edição. </li>
                                <br />
                                <br />
                                <li>Se for selecionado o tipo de visitante “visitante” e na pop-up selecionado o botão
                                    <asp:Image ID="Image2" runat="server" ImageUrl="~/Imagens/icones/fechar.jpg" />,
                                    os campos Tipo do Documento, Documento e Empresa são liberados para edição.</li>
                                <br />
                                <br />
                                <li>No campo empresa, se for selecionada a opção “Outra...”, exibe um campo para inserir
                                    o nome da empresa.</li>
                                <br />
                                <br />
                                <li>Botão
                                    <asp:Image ID="Image3" runat="server" ImageUrl="~/Imagens/icones/adicionar.jpg" />
                                    insere o visitante na lista.</li>
                                <br />
                                <br />
                                <li>Botão
                                    <asp:Image ID="Image4" runat="server" ImageUrl="~/Imagens/icones/limpar.jpg" />
                                    limpa os campos.</li>
                                <br />
                                <br />
                                <li>Clique no botão
                                    <asp:Image ID="Image5" runat="server" ImageUrl="~/Imagens/icones/ico_deletar.gif" />
                                    exclui o visitante inserido da lista.</li>
                                <br />
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td class="cadlbl">
                                Campos:</td>
                        </tr>
                        <tr>
                            <td class="cadrad">
                                Documento - sem pontos e/ traços.<br />
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
