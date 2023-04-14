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
                                Especifica��o da tela:</td>
                        </tr>
                        <tr>
                            <td class="cadrad">
                                Esta tela serve para cadastrar/editar uma "lista de pessoas". As "listas de pessoas"
                                servem para cadastrar grupos de visitantes cujas solicita��es s�o feitas peri�dicamente
                                de forma a facilitar o lan�amento das solicita��es de acesso.
                                <br />
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td class="cadlbl">
                                Regra de Neg�cio:</td>
                        </tr>
                        <tr>
                            <td class="cadrad">
                                - Dados da Lista<br />
                                <br />
                                <li>Campo C�digo da Lista � preenchido autom�ticamente.</li>
                                <br />
                                <br />
                                <li>Campos Nome da Lista, Regional e Filial s�o obrigat�rios.</li>
                                <br />
                                <br />
                                <br />
                                - Dados do Visitante<br />
                                <li>Selecione o tipo de visitante e clique no bot�o
                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Imagens/icones/ico_Visualizar.gif" />
                                    para exibir uma pop-up com a listagem do tipo de visitante selecionado.</li>
                                <br />
                                <br />
                                <li>Clique em um registro exibido na pop-up, preenche os campos Tipo do Documento, Documento
                                    e Empresa conforme dados do registro selecionado e s�o bloqueados para edi��o. </li>
                                <br />
                                <br />
                                <li>Se for selecionado o tipo de visitante �visitante� e na pop-up selecionado o bot�o
                                    <asp:Image ID="Image2" runat="server" ImageUrl="~/Imagens/icones/fechar.jpg" />,
                                    os campos Tipo do Documento, Documento e Empresa s�o liberados para edi��o.</li>
                                <br />
                                <br />
                                <li>No campo empresa, se for selecionada a op��o �Outra...�, exibe um campo para inserir
                                    o nome da empresa.</li>
                                <br />
                                <br />
                                <li>Bot�o
                                    <asp:Image ID="Image3" runat="server" ImageUrl="~/Imagens/icones/adicionar.jpg" />
                                    insere o visitante na lista.</li>
                                <br />
                                <br />
                                <li>Bot�o
                                    <asp:Image ID="Image4" runat="server" ImageUrl="~/Imagens/icones/limpar.jpg" />
                                    limpa os campos.</li>
                                <br />
                                <br />
                                <li>Clique no bot�o
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
                                Documento - sem pontos e/ tra�os.<br />
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
