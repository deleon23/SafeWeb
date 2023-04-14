<%@ Page Language="vb" AutoEventWireup="false" Codebehind="Translate.aspx.vb" Inherits="FrameWork.UI.Translate" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <meta http-equiv="Pragma" content="no-cache">
    <base target="_self">
    <link href="%%PATH%%/estilos/FrameWork.css" type="text/css" rel="stylesheet">
</head>

<script language="javascript">
			// --------------------------------------------------------------
			// Obtem a RadWindow aberta nesta página 
			// --------------------------------------------------------------

			function GetRadWindow()
			{
			var oWindow = null;
			if (window.radWindow) oWindow = window.radWindow;
			else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
			return oWindow;
			} 
			  
			// --------------------------------------------------------------
			// Fecha a janela atual
			// --------------------------------------------------------------
			  
			function CloseWin()   
			{   
				var oWindow = GetRadWindow();   
				oWindow.Close();   
			}   

</script>

<body bgcolor="#FFEBCC">
    <form id="Form1" method="post" runat="server">
        <table border="0" cellpadding="2" cellspacing="4" width="100%">
            <tr>
                <td colspan="2">
                    <hr width="80%" size="1">
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <asp:Label ID="lbltitulo" CssClass="cadlabel" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td colspan="2">
                    <hr width="80%" size="1">
                </td>
            </tr>
            <tr>
                <td colspan="2" class="cadMsg">
                    <asp:Label ID="lblMensagemCadastro" runat="server"></asp:Label></td>
            </tr>
            <asp:PlaceHolder ID="phtermos" runat="server"></asp:PlaceHolder>
            <tr>
                <td colspan="2" align="right">
                    <asp:Button ID="btnok" runat="server" CssClass="cadbutton100" Text="Ok"></asp:Button>&nbsp;
                    <asp:Button runat="server" ID="btnVoltar" CssClass="cadbutton100" Text="Fechar" OnClientClick="CloseWin();" /></td>
            </tr>
        </table>
    </form>
</body>
</html>
