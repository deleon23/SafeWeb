<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CadEscala.aspx.cs" Inherits="SafWeb.UI.Modulos.Escala.CadEscala" %>

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
                <td colspan="2" >
                    <table cellspacing="0" cellpadding="0" width="450px" border="0" align="center">
                        <tr>
                            <td class="cadlbl"  width="250px">
                                <asp:Label ID="lblRegional" runat="server" Text="Regional:"></asp:Label>
                            </td>
                            <td class="cadlbl" width="250px">
                                <asp:Label ID="lblFilial" runat="server" Text="Filial:"></asp:Label>
                            </td> 
                        </tr>
                        <tr>
                            <td width="250px" style="height: 19px">
                                <asp:DropDownList ID="ddlRegional" runat="server" CssClass="cadddl" 
                                    AutoPostBack="True" Width="150px">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvRegional" runat="server" ControlToValidate="ddlRegional"
                                    ErrorMessage="Campo Obrigatório: Regional." InitialValue="0"
                                    SetFocusOnError="True">*</asp:RequiredFieldValidator>
                            </td>
                            <td width="250px" style="height: 19px">
                                <asp:DropDownList ID="ddlFilial" runat="server" CssClass="cadddl" AutoPostBack="True"
                                    Enabled="False" Width="150px">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvFilial" runat="server" ControlToValidate="ddlFilial"
                                    ErrorMessage="Campo Obrigatório: Filial." InitialValue="0"
                                    SetFocusOnError="True">*</asp:RequiredFieldValidator>
                            </td>  
                        </tr>
                        
                        <tr><td>&nbsp;</td></tr>
                        
                        <tr>
                            <td class="cadlbl" colspan="2">
                                <asp:Label ID="lbldescricaoEscala" runat="server" Text="Descrição:"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="cadlbl" colspan="2">
                                <asp:TextBox ID="txtDescricao" runat="server" MaxLength="50"
                                     Width="376px" Enabled="False"/>
                                <asp:RequiredFieldValidator ID="rfvDescricao" runat="server" ControlToValidate="txtDescricao"
                                    ErrorMessage="Campo Obrigatório: Descrição." SetFocusOnError="True">*</asp:RequiredFieldValidator>                                                                                    
                            </td>
                        </tr>
                        
                        <tr><td>&nbsp;</td></tr>
                        
                        <tr>
                            <td class="cadlbl" colspan="2">
                                <asp:Label ID="lblPeriodicidade" runat="server" Text="Periodicidade:"></asp:Label>
                            </td>                            
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:DropDownList ID="ddlPeriodicidade" runat="server" CssClass="cadddl" 
                                    AutoPostBack="True" Width="226px">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvPeriodicidade" runat="server" ControlToValidate="ddlPeriodicidade"
                                    ErrorMessage="Campo Obrigatório: Periodicidade." InitialValue="0"
                                    SetFocusOnError="True">*</asp:RequiredFieldValidator>
                            </td>                             
                        </tr>
                        
                        <tr><td>&nbsp;</td></tr>
                        
                        <tr>
                            <td colspan="2" style="padding-bottom:15px;">
                                <table cellspacing="0" cellpadding="0" width="450px" border="0" align="center">
                                    <tr>
                                        <td style="width: 210px">
                                            <asp:ListBox ID="lstAreaVisita" runat="server" Height="137px" Width="195px" CssClass="cadlstBox"
                                                SelectionMode="Multiple"></asp:ListBox>
                                        </td>
                                        <td align="center" style="width: 30px">
                                            <asp:Button ID="btnAddUm" runat="server" Text=" > " CssClass="cadbuttonfiltro" Width="25px" /><br />
                                            <br />
                                            <asp:Button ID="btnRemoverUm" runat="server" Text="<" CssClass="cadbuttonfiltro" Width="25px" /><br />
                                        </td>
                                        <td align="right" style="width: 210px">
                                            <asp:ListBox ID="ListBox1" runat="server" Height="137px" Width="195px" CssClass="cadlstBox"
                                                SelectionMode="Multiple"></asp:ListBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>                             
                        </tr>
                        
                        <tr>
                            <td colspan="2"> 
                                <table border="0" cellpadding="0" cellspacing="0" width="450px" align="center">
                                    <tr>
                                        <td align="right" class="backBarraBotoes" style="height: 31px"> 
                                            <asp:Button ID="btnGravar" runat="server" CssClass="cadbutton100" Text="Gravar"/>
                                            &nbsp;
                                            <asp:Button ID="btnFechar" runat="server" CausesValidation="False" CssClass="cadbutton100"
                                                Text="Fechar" Width="50px" OnClick="btnFechar_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        
                    </table>
                </td>                
            </tr>                       
        </table>
    </form>
</body>
</html>
