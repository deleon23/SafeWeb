<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ListHorarioColaboradores.aspx.cs" Inherits="SafWeb.UI.Modulos.Escala.ListHorarioColaboradores" %>

<html>
<head runat="server">
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
                                <asp:Label ID="lblHorario" runat="server" Text="Horário:"></asp:Label>
                            </td>                            
                        </tr>
                        <tr>
                            <td width="250px" class="cadlblBlack">
                                <asp:Label ID="lblRespHorario" runat="server"></asp:Label>
                            </td>                            
                        </tr>
                        
                        <tr><td>&nbsp;</td></tr>
                        
                        <tr>
                            <td class="cadlbl"  width="300px" >
                                <asp:Label ID="lblcolaboradores" runat="server" Text="Colaboradores:"></asp:Label>
                                <asp:RequiredFieldValidator ID="rfvColaboradores" runat="server" ControlToValidate="lstColaboradores"
                                    ErrorMessage="Campo Obrigatório: Colaboradores." InitialValue="0"
                                    SetFocusOnError="True">*</asp:RequiredFieldValidator>
                            </td>                            
                        </tr>
                        <tr>
                            <td>
                                <asp:ListBox ID="lstColaboradores" runat="server" Height="190px" Width="295px" 
                                    SelectionMode="Multiple" CssClass="cadlstBox"></asp:ListBox>                         
                            </td>
                            <td valign="bottom" width="200px" style="padding-left:35px; padding-bottom:0px;">                             
                                <asp:Button ID="btnSelecionar" runat="server" CssClass="cadbutton100"
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