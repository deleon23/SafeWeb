<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HelpSaidaListagem.aspx.cs" Inherits="SafWeb.UI.Modulos.Portaria.HelpSaidaListagem" %>

<html>
<head>
    <link href="../../Estilos/Brinks.css" type="text/css" rel="stylesheet">
    <title>Ajuda - Busca de entradas em aberto</title>
</head>
<body>
    <form id="form" method="post" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" class="MarginTopConteudo" width="500px" style="height: 500px">
            <tr valign="top">
                <td colspan="3" style="height: 280px">
                   <table>
                   <tr>
                        <td class="cadlbl">Especificação da tela:</td>
                   </tr>
                   <tr>
                        <td class="cadrad">Tela que lista as solicitações pendentes de aprovação para o usuário logado.<br /><br /></td>
                   </tr>
                   <tr>
                        <td class="cadlbl">Regra de Negócio:</td>
                   </tr>
                   <tr>
                        <td class="cadrad">Selecione as solicitações que deseja aprovar ou reprovar.<br /><br /></td>
                   </tr>
                   <tr>
                        <td class="cadlbl">Campos:</td>
                   </tr>
                   <tr>
                        <td class="cadrad">Caso seja uma reprovação é necessário preencher o campo observação.<br /><br /></td>
                   </tr>
                   </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
