<%@ Page Language="C#" AutoEventWireup="true" Codebehind="HelpSolCadastro.aspx.cs"
    Inherits="SafWeb.UI.Modulos.Solicitacao.HelpSolCadastro" %>

<html>
<head>
    <link href="../../Estilos/Brinks.css" type="text/css" rel="stylesheet">
    <title>Ajuda - Cadastro de Solicitação</title>
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
                                Especificação da tela:<br />
                            </td>
                        </tr>
                        <tr>
                            <td class="cadrad">
                                Esta tela serve para cadastrar/editar uma solicitação. As solicitações servem para
                                liberar acesso a um ou mais visitantes em uma ou mais áreas de uma filial.
                                <br />
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td class="cadlbl">
                                Regra de Negócio:<br />
                            </td>
                        </tr>
                        <tr>
                            <td class="cadrad">
                                - Dados Visitado<br />
                                <li>Clique no botão
                                    <asp:Image ID="Image6" runat="server" ImageUrl="~/Imagens/icones/ico_Visualizar.gif" />
                                    exibe uma pop-up com a listagem de funcionários/terceiros.
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
                                <li>Campo Regional, Filial, Motivo da Visita, Área da Visita (pode ser selecionada mais
                                    de uma área), Data/Hora Início, Data/Hora Fim são obrigatórios.
                                <br />
                                <br />
                                </li>
                                <li>Campo Observação, Acesso Sábado, Acesso Domingo, Acesso Feriado são opcionais.
                                <br />
                                <br />
                                <br />
                                - Dados Visitante<br />
                                </li>
                                <li>Selecione o tipo de visitante e clique no botão
                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Imagens/icones/ico_Visualizar.gif" />
                                    para exibir uma pop-up com a listagem do tipo de visitante selecionado.
                                <br />
                                <br />
                                </li>
                                <li>Clique em um registro exibido na pop-up, preenche os campos Tipo do Documento, Documento
                                    e Empresa conforme dados do registro selecionado e são bloqueados para edição. 
                                <br />
                                <br />
                                </li>
                                <li>Se for selecionado o tipo de visitante “visitante” e na pop-up selecionado o botão
                                    <asp:Image ID="Image2" runat="server" ImageUrl="~/Imagens/icones/fechar.jpg" />,
                                    os campos Tipo do Documento, Documento e Empresa são liberados para edição.
                                <br />
                                <br />
                                </li>
                                <li>No campo empresa, se for selecionada a opção “Outra...”, exibe um campo para inserir
                                    o nome da empresa.
                                <br />
                                <br />
                                - Dados Visitante<br />
                                </li>
                                <li>Seleção de um veículo não é obrigatório.
                                <br />
                                <br />
                                </li>
                                <li>Selecione um estado para que apareçam as placas de veículos cadastrados nesse estado.
                                <br />
                                <br />
                                </li>
                                <li>No campo placa, se for selecionada a opção “Outra...”, exibe um campo para inserir
                                    a placa.
                                <br />
                                <br />
                                </li>
                                <li>Botão
                                    <asp:Image ID="Image3" runat="server" ImageUrl="~/Imagens/icones/adicionar.jpg" />
                                    insere o visitante na lista.
                                <br />
                                <br />
                                </li>
                                <li>Botão
                                    <asp:Image ID="Image4" runat="server" ImageUrl="~/Imagens/icones/limpar.jpg" />
                                    limpa os campos.
                                <br />
                                <br />
                                </li>
                                <li>Clique no botão
                                    <asp:Image ID="Image5" runat="server" ImageUrl="~/Imagens/icones/ico_deletar.gif" />
                                    exclui o visitante inserido da lista.
                                <br />
                                <br />
                                - Dados Aprovador<br />
                                </li>
                                <li>Selecione um aprovador, o qual irá aprovar/reprovar a (s) solicitação (ões).
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
                                <b>Documento:</b> sem pontos e/ traços.<br />
                                <br />
                                <b>Data/Hora Início e/ou Data/Hora Fim:</b> Clique na imagem
                                <asp:Image ID="Image7" runat="server" ImageUrl="~/Agenda/calbtn.gif" />
                                exibe um calendário, ao selecionar uma data o campo é preenchido com a data selecionada.
                                Após a data é possível inserir também o horário.
                                <asp:Image ID="Image8" runat="server" ImageUrl="~/Imagens/icones/data.jpg" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
