<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="teste.aspx.cs" Inherits="SafWeb.UI.Modulos.Escala.teste" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../Estilos/PopUp.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery-1.7.2.js" type="text/javascript"></script>
    <%--<script src="../../Scripts/jquery-1.7.2-vsdoc.js" type="text/javascript"></script>--%>
    <script src="../../Scripts/jquery-popup.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {

            CriarPopUp({
                'titulo': 'Adicionar Limite',
                'conteudo': '',
                'width': 800,
                'height': 200
            });
            

        });

        
    </script>
</head>
<body>
    <form id="form1" runat="server">
    </form>
</body>
</html>
