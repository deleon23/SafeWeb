<%@ Page Language="C#" AutoEventWireup="true" Codebehind="ConfirmaVariosSaida.aspx.cs"
    Inherits="SafWeb.UI.Modulos.Portaria.ConfirmaVariosSaida" %>

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

            <asp:HiddenField ID="hdProcessado" runat="server" />
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
                                    <asp:Label ID="lblColaborador" runat="server" Text="Colaboradores:"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="cadlbl" width="590px" style="height: 20px;">
                                    <div style="height: 125px; overflow: auto; width:590px;">
                                        <rad:RadGrid ID="gridFuncionarios" runat="server" CssClass="dtg" Skin="None"
                                        PageSize="50" GridLines="None" AutoGenerateColumns="False" AllowSorting="True" 
                                        Width="573px" SortingSettings-SortToolTip="Clique para filtrar">
                                            <GroupHeaderItemStyle CssClass="dtgGroupHeader"></GroupHeaderItemStyle>
                                            <GroupPanel Text="">
                                                <PanelStyle CssClass="dtgGroupPanel"></PanelStyle>
                                                <PanelItemsStyle CssClass="dtgGroupPanelItem"></PanelItemsStyle>
                                            </GroupPanel>
                                            <AlternatingItemStyle CssClass="dtgItemStyle"></AlternatingItemStyle>
                                            <ItemStyle CssClass="dtgItemStyleAlternate"></ItemStyle>
                                            <HeaderStyle CssClass="dtgHeaderStyle"></HeaderStyle>
                                            <MasterTableView NoMasterRecordsText="Nenhum Colaborador">
                                                <Columns>   
                                                    <rad:GridBoundColumn DataField="CodColaborador" HeaderText="Codigo do Colaborador" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol"
                                                        SortExpression="CodColaborador" UniqueName="CodColaborador">
                                                        <ItemStyle></ItemStyle>
                                                    </rad:GridBoundColumn>                                                                 

                                                    <rad:GridBoundColumn DataField="HoraEntrada" HeaderText="Hora Entrada" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol"
                                                        SortExpression="HoraEntrada" UniqueName="HoraEntrada">
                                                        <ItemStyle></ItemStyle>
                                                    </rad:GridBoundColumn> 

                                                    <rad:GridBoundColumn DataField="NomeColaborador" HeaderText="Nome do Colaborador" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="dtgHeaderStyle"
                                                        SortExpression="NomeColaborador" UniqueName="HoraEntrada">
                                                        <ItemStyle></ItemStyle>
                                                    </rad:GridBoundColumn>

                                                    <rad:GridBoundColumn DataField="RE" HeaderText="RE" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="dtgHeaderStyle"
                                                        SortExpression="RE" UniqueName="RE">
                                                        <ItemStyle></ItemStyle>
                                                    </rad:GridBoundColumn>

                                                    <rad:GridBoundColumn DataField="NumeroEscalado" HeaderText="Nº da Escala" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="dtgHeaderStyle"
                                                        SortExpression="NumeroEscalado" UniqueName="NumeroEscalado">
                                                        <ItemStyle></ItemStyle>
                                                    </rad:GridBoundColumn>

                                                    <rad:GridBoundColumn DataField="EscalaDepartamental" HeaderText="Escala Departamental" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="dtgHeaderStyle"
                                                        SortExpression="EscalaDepartamental" UniqueName="EscalaDepartamental">
                                                        <ItemStyle></ItemStyle>
                                                    </rad:GridBoundColumn>

                                                </Columns>
                                            </MasterTableView>
                                        </rad:RadGrid>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                               <td style="height: 30px;" align="center">
                                    <asp:Button ID="btnConfirmar" runat="server" CssClass="cadbutton100" Text="Confirmar"
                                        OnClick="btnConfirmar_Click"/>
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
