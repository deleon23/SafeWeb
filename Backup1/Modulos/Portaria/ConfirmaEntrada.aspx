<%@ Page Language="C#" AutoEventWireup="true" Codebehind="ConfirmaEntrada.aspx.cs"
    Inherits="SafWeb.UI.Modulos.Portaria.ConfirmaEntrada" %>

<%@ Register TagPrefix="rad" Namespace="Telerik.WebControls" Assembly="RadGrid.Net2" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <script src="../../Scripts/jquery-1.7.2.js" type="text/javascript"></script>
    <link href="../../Estilos/Brinks.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript">

       
                  
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

         // --------------------------------------------------------------
         // Fecha e atualiza a janela atual
         // --------------------------------------------------------------
         function CloseAndReload() {
             //Get the RadWindow   
             var oWindow = GetRadWindow();
             oWindow.BrowserWindow.location.reload();
             //Call its Close() method   
             oWindow.Close();
         }

    </script>

</head>
<body style="background-color: #ecf1f7">
    <form id="form" method="post" runat="server">
            <table class="MarginTopConteudo" cellspacing="0" cellpadding="0" border="0">
                <tr>
                    <td>
                        <table cellspacing="0" cellpadding="0" border="0">
                            <tr>
                                <td>
                                    <table>
                                        <tr>
                                            <td class="cadlbl" style="height: 28px;" align="left">
                                                <center>
                                                    <asp:Label ID="lblHorario" runat="server" Text="Horário no Sistema:"></asp:Label></center>
                                            </td>
                                            <td class="dtgItemStyleAlternate" style="height: 20px;" align="left">
                                                <center>
                                                    <asp:Label ID="lblHora" runat="server" Text=""></asp:Label>
                                                    <asp:HiddenField ID="hdHora" runat="server" />
                                                </center>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="cadlbl" style="height: 20px;" >
                                    <center>
                                        <asp:Label ID="lblMensagem" runat="server" CssClass="cadalert"></asp:Label>
                                    </center>
                                    <br />
                                    <i><asp:Label ID="lblColaborador" runat="server" Text="Informações do Colaborador:"></asp:Label></i>
                                </td>
                            </tr>
                            <tr>
                                <td class="cadlbl" width="590px" style="height: 20px;">
                                    <center>
                                        <table>
                                            <tr>
                                                <td class="cadlblRigth" style="height: 0px;">
                                                        <asp:Label ID="Label1" runat="server" Text="Horário de Entrada:"></asp:Label>
                                                </td>
                                                <td class="dtgItemStyleAlternate" style="height: 0px;" align="left">
                                                        <asp:Label ID="lbl_HoraEntrada" runat="server" Text=""></asp:Label>
                                                </td>
                                                <td rowspan="6">
                                                    <table>
                                                        <tr>
                                                            <td class="cadlbl">
                                                                <asp:Label ID="Label6" runat="server" Text="Foto:"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="cadlbl">
                                                                <asp:image ID="imgFoto" runat="server" Visible="false" Width="79" Height="105" BorderWidth="1px"/>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="cadlblRigth" style="height: 0px;">
                                                        <asp:Label ID="Label2" runat="server" Text="Nome do Colaborador:"></asp:Label>
                                                </td>
                                                <td class="dtgItemStyleAlternate" style="height: 0px;" align="left">
                                                        <asp:Label ID="lbl_NomeColaborador" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="cadlblRigth" style="height: 0px;">
                                                        <asp:Label ID="Label3" runat="server" Text="RE:"></asp:Label>
                                                </td>
                                                <td class="dtgItemStyleAlternate" style="height: 0px;" align="left">
                                                        <asp:Label ID="lbl_RE" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="cadlblRigth" style="height: 0px;">
                                                   <asp:Label ID="Label7" runat="server" Text="Cargo:"></asp:Label>
                                                </td>
                                                <td class="dtgItemStyleAlternate" style="height: 0px;" align="left">
                                                        <asp:Label ID="lbl_Cargo" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="cadlblRigth" style="height: 0px;">
                                                        <asp:Label ID="Label4" runat="server" Text="Nº da Escala:"></asp:Label>
                                                </td>
                                                <td class="dtgItemStyleAlternate" style="height: 0px;" align="left">
                                                        <asp:Label ID="lbl_NumEscala" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="cadlblRigth" style="height: 0px;">
                                                   <asp:Label ID="Label5" runat="server" Text="Escala Departamental:"></asp:Label>
                                                </td>
                                                <td class="dtgItemStyleAlternate" style="height: 0px;" align="left">
                                                        <asp:Label ID="lbl_EscalaDpto" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>                                            

                                        </table>
                                    </center>
                                </td>
                            </tr>
                            <tr>
                               <td style="height: 30px;" align="center">
                                    <asp:Button ID="btnConfirmar" runat="server" CssClass="cadbutton100" Text="Confirmar"
                                        OnClick="btnConfirmar_Click" />
                               </td>
                            </tr>
                            <tr>
                                <td></td>
                            </tr>                            
                        </table>
                    </td>
                </tr>
            </table>

    </form>
</body>
</html>
