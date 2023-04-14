<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CapturarFoto.aspx.cs" Inherits="SafWeb.UI.Modulos.Portaria.CapturarFoto" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="../../Scripts/jquery-1.7.2.js" type="text/javascript"></script>
    <link href="../../Estilos/Brinks.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript">
            
        window.onbeforeunload = confirmExit;
        function confirmExit() {
            window.parent.location.reload();
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <object width="610" height="310">
		    <param name="movie" value="WebcamResources/save_picture.swf?idVisitante=<%=Request.QueryString["idVisitante"].ToString()%>&FusoHorario=<%=Request.QueryString["FusoHorario"].ToString()%>">
		    <embed src="WebcamResources/save_picture.swf?idVisitante=<%=Request.QueryString["idVisitante"].ToString() %>&FusoHorario=<%=Request.QueryString["FusoHorario"].ToString()%>" width="610" height="310"></embed>
	    </object>

    </div>
    </form>
</body>
</html>
