<%@ Page Language="C#" AutoEventWireup="true" Codebehind="HelpSolCadastro.aspx.cs"
    Inherits="SafWeb.UI.Modulos.Solicitacao.HelpSolCadastro" %>

<html>
<head>
    <link href="../../Estilos/Brinks.css" type="text/css" rel="stylesheet">
    <title>Ajuda - Cadastro de Solicita��o</title>
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
                                Especifica��o da tela:<br />
                            </td>
                        </tr>
                        <tr>
                            <td class="cadrad">
                                Esta tela serve para cadastrar/editar uma solicita��o. As solicita��es servem para
                                liberar acesso a um ou mais visitantes em uma ou mais �reas de uma filial.
                                <br />
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td class="cadlbl">
                                Regra de Neg�cio:<br />
                            </td>
                        </tr>
                        <tr>
                            <td class="cadrad">
                                - Dados Visitado<br />
                                <li>Clique no bot�o
                                    <asp:Image ID="Image6" runat="server" ImageUrl="~/Imagens/icones/ico_Visualizar.gif" />
                                    exibe uma pop-up com a listagem de funcion�rios/terceiros.
                                <br />
                                <br />
                                </li>
                                <li>Clique em um registro exibido na pop-up preenche o campo Nome do Visitado e RE conforme
                                    o registro selecionado.
                                <br />
                                <br />
                                <br />
                                - Dados Visita<br />
                                </li>
                                <li>Campo Regional, Filial, Motivo da Visita, �rea da Visita (pode ser selecionada mais
                                    de uma �rea), Data/Hora In�cio, Data/Hora Fim s�o obrigat�rios.
                                <br />
                                <br />
                                </li>
                                <li>Campo Observa��o, Acesso S�bado, Acesso Domingo, Acesso Feriado s�o opcionais.
                                <br />
                                <br />
                                <br />
                                - Dados Visitante<br />
                                </li>
                                <li>Selecione o tipo de visitante e clique no bot�o
                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Imagens/icones/ico_Visualizar.gif" />
                                    para exibir uma pop-up com a listagem do tipo de visitante selecionado.
                                <br />
                                <br />
                                </li>
                                <li>Clique em um registro exibido na pop-up, preenche os campos Tipo do Documento, Documento
                                    e Empresa conforme dados do registro selecionado e s�o bloqueados para edi��o. 
                                <br />
                                <br />
                                </li>
                                <li>Se for selecionado o tipo de visitante �visitante� e na pop-up selecionado o bot�o
                                    <asp:Image ID="Image2" runat="server" ImageUrl="~/Imagens/icones/fechar.jpg" />,
                                    os campos Tipo do Documento, Documento e Empresa s�o liberados para edi��o.
                                <br />
                                <br />
                                </li>
                                <li>No campo empresa, se for selecionada a op��o �Outra...�, exibe um campo para inserir
                                    o nome da empresa.
                                <br />
                                <br />
                                - Dados Visitante<br />
                                </li>
                                <li>Sele��o de um ve�culo n�o � obrigat�rio.
                                <br />
                                <br />
                                </li>
                                <li>Selecione um estado para que apare�am as placas de ve�culos cadastrados nesse estado.
                                <br />
                                <br />
                                </li>
                                <li>No campo placa, se for selecionada a op��o �Outra...�, exibe um campo para inserir
                                    a placa.
                                <br />
                                <br />
                                </li>
                                <li>Bot�o
                                    <asp:Image ID="Image3" runat="server" ImageUrl="~/Imagens/icones/adicionar.jpg" />
                                    insere o visitante na lista.
                                <br />
                                <br />
                                </li>
                                <li>Bot�o
                                    <asp:Image ID="Image4" runat="server" ImageUrl="~/Imagens/icones/limpar.jpg" />
                                    limpa os campos.
                                <br />
                                <br />
                                </li>
                                <li>Clique no bot�o
                                    <asp:Image ID="Image5" runat="server" ImageUrl="~/Imagens/icones/ico_deletar.gif" />
                                    exclui o visitante inserido da lista.
                                <br />
                                <br />
                                - Dados Aprovador<br />
                                </li>
                                <li>Selecione um aprovador, o qual ir� aprovar/reprovar a (s) solicita��o (�es).
                                <br />
                                <br />
                            </li>
                            </td>
                        </tr>
                        <tr>
                            <td class="cadlbl">
                                Campos:<br />
                            </td>
                        </tr>
                        <tr>
                            <td class="cadrad">
                                <b>Documento:</b> sem pontos e/ tra�os.<br />
                                <br />
                                <b>Data/Hora In�cio e/ou Data/Hora Fim:</b> Clique na imagem
                                <asp:Image ID="Image7" runat="server" ImageUrl="~/Agenda/calbtn.gif" />
                                exibe um calend�rio, ao selecionar uma data o campo � preenchido com a data selecionada.
                                Ap�s a data � poss�vel inserir tamb�m o hor�rio.
                                <asp:Image ID="Image8" runat="server" ImageUrl="~/Imagens/icones/data.jpg" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
