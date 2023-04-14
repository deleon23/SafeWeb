<%@ Page Language="C#" AutoEventWireup="true" Codebehind="ConfirmaVariosEntrada.aspx.cs"
    Inherits="SafWeb.UI.Modulos.Portaria.ConfirmaVariosEntrada" %>

<%@ Register TagPrefix="rad" Namespace="Telerik.WebControls" Assembly="RadGrid.Net2" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <script src="../../Scripts/jquery-1.7.2.js" type="text/javascript"></script>
    <link href="../../Estilos/Brinks.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript">

        var allCheckBoxSelector = '#<%=gridFuncionariosAtrasados.ClientID%> input[id*="checkClientesHeader"]:checkbox';
        var checkBoxSelector = '#<%=gridFuncionariosAtrasados.ClientID%> input[id*="checkClientes"]:checkbox';

        function ToggleCheckUncheckAllOptionAsNeeded() {
            var totalCheckboxes = $(checkBoxSelector),
             checkedCheckboxes = totalCheckboxes.filter(":checked"),
             noCheckboxesAreChecked = (checkedCheckboxes.length === 0),
             allCheckboxesAreChecked = (totalCheckboxes.length === checkedCheckboxes.length);

            $(allCheckBoxSelector).attr('checked', allCheckboxesAreChecked);
        }

        $(document).ready(function () {

            $(allCheckBoxSelector).live('click', function () {
                $(checkBoxSelector).attr('checked', $(this).is(':checked'));

                ToggleCheckUncheckAllOptionAsNeeded();
            });

            $(checkBoxSelector).live('click', ToggleCheckUncheckAllOptionAsNeeded);

            ToggleCheckUncheckAllOptionAsNeeded();


        });
           
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
        
	    window.onbeforeunload = confirmExit;
	    function confirmExit() {
	        if ($("[id*='hdSaindo']").val() == '') {
	            window.parent.PreencherHiddenColaboradores(' ');
	        }
	    }
	    
	    // --------------------------------------------------------------
	    function CancelaAtualizacaoPai() {
	        $("[id*='hdSaindo']").val('false');
	    }


    </script>

</head>
<body style="background-color: #ecf1f7">
    <form id="form" method="post" runat="server">

            <asp:HiddenField ID="hdProcessado" runat="server" />
            <asp:HiddenField ID="hdSaindo" runat="server" />
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
                                    <asp:Label ID="lblColaborador" runat="server" Text="Colaboradores no Horário:"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="cadlbl" width="590px" style="height: 20px;">
                                    <div style="height: 125px; overflow: auto; width:590px;">
                                        <rad:RadGrid ID="gridFuncionarios" runat="server" CssClass="dtg" Skin="None"
                                        PageSize="50" GridLines="None" AutoGenerateColumns="False" AllowSorting="True" 
                                        Width="573px" SortingSettings-SortToolTip="Clique para filtrar" OnNeedDataSource="gridFuncionarios_OnNeedDataSource"
                                        OnItemDataBound="gridFuncionarios_ItemDataBound">
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

                                                    <rad:GridBoundColumn DataField="HoraEntrada" HeaderText="Hora Entrada" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="dtgHeaderStyle"
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
                                        OnClick="btnConfirmar_Click" OnClientClick="CancelaAtualizacaoPai()" />
                               </td>
                            </tr>
                            <tr>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="cadlbl" style="height: 20px;" >
                                    <center>
                                        <asp:Label ID="lblMensagemAtrasado" runat="server" CssClass="cadalert"></asp:Label>
                                    </center>
                                    <asp:Label ID="Label1" runat="server" Text="Colaboradores Atrasados:"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="cadlbl" width="590px" style="height: 28px;">
                                    <div style="height: 125px; overflow: auto; width:590px;">
                                        <rad:RadGrid ID="gridFuncionariosAtrasados" runat="server" CssClass="dtg" Skin="None"
                                        PageSize="50" GridLines="None" AutoGenerateColumns="False" AllowSorting="True" 
                                        Width="573px" SortingSettings-SortToolTip="Clique para filtrar" OnNeedDataSource="gridFuncionariosAtrasados_OnNeedDataSource" 
                                        OnItemDataBound="gridFuncionariosAtrasados_ItemDataBound" >
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

                                                    <rad:GridTemplateColumn UniqueName="CheckAcesso">
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="checkClientesHeader" runat="server"></asp:CheckBox>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="checkClientes" runat="server"></asp:CheckBox>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" Width="5px" />
                                                        <ItemStyle HorizontalAlign="Center" Width="5px" />
                                                    </rad:GridTemplateColumn>

                                                    <rad:GridBoundColumn DataField="CodColaborador" HeaderText="Codigo do Colaborador" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol"
                                                        SortExpression="CodColaborador"  UniqueName="CodColaborador">
                                                        <ItemStyle></ItemStyle>
                                                    </rad:GridBoundColumn>                                                                 

                                                    <rad:GridBoundColumn DataField="HoraEntrada" HeaderText="Hora Entrada" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="dtgHeaderStyle"
                                                        SortExpression="HoraEntrada" UniqueName="HoraEntrada">
                                                        <ItemStyle ForeColor="Red"></ItemStyle>
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
                                    <asp:Button ID="btnConfirmarAtrasados" runat="server" CssClass="cadbutton100" Text="Confirmar" 
                                        OnClick="btnConfirmarAtrasados_Click" OnClientClick="CancelaAtualizacaoPai()" /></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>

    </form>
</body>
</html>
