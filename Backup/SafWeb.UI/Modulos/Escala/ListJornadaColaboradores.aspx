<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ListJornadaColaboradores.aspx.cs" Inherits="SafWeb.UI.Modulos.Escala.ListJornadaColaboradores" %>

<%@ Register TagPrefix="rada" Namespace="Telerik.WebControls" Assembly="RadAjax.Net2" %>

<html>
<head id="Head1" runat="server">
    <link href="../../Estilos/Brinks.css" rel="stylesheet" type="text/css" /> 
    <script language="javascript">
    
        function GetRadWindow()
        {
            var oWindow = null;
            if (window.radWindow) oWindow = window.radWindow;
            else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;

            return oWindow;

        } 
          
        //Fecha a RadWindow
        function CloseWin()   
        {   
             //Get the RadWindow   
             var oWindow = GetRadWindow();  
             //oWindow.BrowserWindow.location.reload(); 
             //Call its Close() method   
             oWindow.Close();   
        }
        
    </script> 
</head>
<body>
    <form id="form" method="post" runat="server">        
        <table class="MarginTopConteudo" cellspacing="0" cellpadding="0" width="500px" border="0">                              
            <tr>
                <td>
                    <table cellspacing="0" cellpadding="0" width="450px" border="0" align="center">
                        <tr>
                            <td class="cadlbl"  width="250px">
                                <asp:Label ID="lblJornada" runat="server" Text="Jornada diária:"></asp:Label>
                            </td>                            
                        </tr>
                        <tr>
                            <td width="250px" class="cadlblBlack">
                                <asp:Label ID="lblRespJornada" runat="server"></asp:Label>
                            </td>                            
                        </tr>
                        
                        <tr><td>&nbsp;</td></tr>
                        
                        <tr>
                            <td class="cadlbl"  width="300px" >
                                <asp:Label ID="lblcolaboradores" runat="server" Text="Selecione os colaboradores que deseje alterar a jornada:"></asp:Label>&nbsp;
                            </td>                            
                        </tr>
                        <tr>
                            <td style="height: 190px">
                                <asp:ListBox ID="lstColaboradores" runat="server" Height="190px" Width="295px" 
                                    SelectionMode="Multiple" CssClass="cadlstBox"></asp:ListBox>                         
                            </td>
                            <td valign="bottom" width="200px" style="padding-left:35px; padding-bottom:0px; height: 190px;">                             &nbsp;<asp:Button ID="btnSelecionar" runat="server" CssClass="cadbutton100"
                                    Text="Selecionar" OnClick="btnSelecionar_Click"/>                           
                            </td>
                        </tr>                    
                    </table>
                </td>                
            </tr>                       
        </table>        
    </form>
</body>
</html>
