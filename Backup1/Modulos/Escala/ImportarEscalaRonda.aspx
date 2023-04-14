<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ImportarEscalaRonda.aspx.cs"
    Inherits="SafWeb.UI.Modulos.Escala.ImportarEscalaRonda" %>

<%@ Register TagPrefix="rad" Namespace="Telerik.WebControls" Assembly="RadGrid.Net2" %>
<%@ Register TagPrefix="rada" Namespace="Telerik.WebControls" Assembly="RadAjax.Net2" %>
<%@ Register TagPrefix="cc1" Namespace="FrameWork.WebControl" Assembly="FrameWork.WebControl" %>
<html>
<head>
    <title>%%SITE%%</title>
    <link href="../../Estilos/FrameWork.css" type="text/css" rel="stylesheet" />
    <link href="../../Estilos/Brinks.css" rel="stylesheet" type="text/css" />
    <script language="javascript">
        function FormataData(e, obj) {
            //usar no evento keypress
            //bloqueia caracteres alfa e coloca as barras nas posições        
            var key = window.event ? e.keyCode : e.which;
            var keychar = String.fromCharCode(key);
            var vrRetorno = false;

            //Backspace e Tab
            if (key != 8 && key != 0) {
                goodChars = "0123456789";
                if (goodChars.indexOf(keychar) != -1) {
                    if ((obj.value.length == 2 || obj.value.length == 5) && key != 8) {
                        obj.value += "/";
                    }
                    if (obj.value.length == 10 && key != 8) {
                        obj.value += " ";
                    }

                    vrRetorno = true;
                }
            }
            else {
                vrRetorno = true;
            }

            return vrRetorno;
        }

        function PreencherHiddenColaboradores(colaboradores) {
            document.getElementById("txtHiddenColaboradores").value = colaboradores;

            AjaxNS.AR('Colaboradores', '', 'RadAjaxPanelCadastro');
        }
    </script>
</head>
<body>
    <form id="form" method="post" runat="server">
    <table cellspacing="0" cellpadding="0" width="778" align="center" border="0">
        <tr>
            <td style="width: 1010px">
                <!-- ********************* START: CABEÇALHO ****************** -->
                <cc1:FWServerControl ID="FWServerControlCabecalho" runat="server" Controle="Pagebanner"
                    Arquivo="/Modulos/Framework/CabecalhoBrinks.ascx"></cc1:FWServerControl>
                <!-- ********************* END: CABEÇALHO ****************** -->
            </td>
        </tr>
        <tr>
            <!-- ********************* START: CONTEÚDO ****************** -->
            <td class="backbox" valign="top" height="250" style="width: 1010px">
                <rada:RadAjaxPanel ID="RadAjaxPanel1" runat="server" ClientEvents-OnRequestStart="OnRequestStart"
                    LoadingPanelID="lpaCadastro">
                    <!-- ********************* PAINEL DE ERRO ****************** -->
                    <asp:Panel ID="pnlErro" runat="server">
                    </asp:Panel>
                    <!-- ****************** START: PAINEL CADASTRO *********************** -->
                    <asp:Panel ID="pnlCadastro" Visible="true" runat="server">
                        <!-- ***************** START: BARRA DE TITUTO ******************* -->
                        <table class="MarginTopBarra" cellspacing="0" cellpadding="0" width="760" align="center"
                            border="0">
                            <tbody>
                                <tr>
                                    <td style="width: 750px; height: 25px" class="cadBarraTitulo" align="left">
                                        <img src="../../Imagens/comum/bulletbarra.gif" align="absMiddle" />
                                        <asp:Label ID="lblTituloCad" runat="server" 
                                            Text="Importação de Escalas do Ronda - Seleção de período e colaboradores"></asp:Label>
                                    </td>
                                    <td style="width: 9px" class="cadBarraTitulo" align="right" height="25">
                                        <asp:ImageButton ID="btnHelpCad" runat="server" Visible="False" ImageAlign="AbsMiddle"
                                            ImageUrl="%%PATH%%/imagens/icones/ico_help.jpg" AlternateText="Ajuda"></asp:ImageButton>
                                    </td>
                                </tr>
                                <!-- ***************** END: BARRA DE TITUTO ******************* -->
                                <tr>
                                    <td style="height: 250px" class="backboxconteudo" valign="top" colspan="2">
                                        <rada:RadAjaxPanel ID="RadAjaxPanelCadastro" runat="server" LoadingPanelID="lpaCadastro"
                                            ClientEvents-OnRequestStart="OnRequestStart">
                                            <!-- ****************** START: FORMULARIO *********************** -->
                                            <table class="MarginTopConteudo" cellspacing="0" cellpadding="0" width="740" align="center"
                                                border="0">
                                                <tr>
                                                    <td class="cadmsg">
                                                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List" />
                                                        <asp:Label ID="lblMensagem" runat="server" Text="Label" Visible="false" CssClass="cadlbl"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Panel ID="pnlCadEscala" runat="server" />
                                                        <asp:HiddenField ID="txtHiddenColaboradores" runat="server" />
                                                        <table width="740" cellspacing="0" cellpadding="0" align="center" border="0">
                                                            <tr>
                                                                <td class="cadlbl" width="240">
                                                                    <asp:Label ID="lblTipoSolicitacao" runat="server" Text="Tipo da Solicitação:" 
                                                                        CssClass="cadlbl"></asp:Label>
                                                                </td>
                                                                <td class="cadlbl" width="240">
                                                                    &nbsp;</td>
                                                                <td class="cadlbl" width="240">
                                                                    &nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td class="cadlbl" width="240">
                                                                    <asp:DropDownList ID="ddlTipoSolicitacao" runat="server" CssClass="cadddl" AutoPostBack="True"
                                                                        OnSelectedIndexChanged="ddlTipoSolicitacao_SelectedIndexChanged"  Width="228px">
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="rfvTipoSolicitacao" runat="server" ControlToValidate="ddlTipoSolicitacao"
                                                                        ErrorMessage="Campo Obrigatório: Tipo da Solicitação" InitialValue="0" 
                                                                        SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                                                </td>
                                                                <td class="cadlbl" width="240">
                                                                    &nbsp;</td>
                                                                <td class="cadlbl" width="240">
                                                                    &nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td class="cadlbl" width="240">
                                                                    <asp:Label ID="lblEscalaDepartamental" runat="server" 
                                                                        Text="Escala Departamental:" CssClass="cadlbl"></asp:Label>
                                                                </td>
                                                                <td class="cadlbl" width="240">
                                                                    <asp:Label ID="lblRegional" runat="server" Text="Regional:" CssClass="cadlbl"></asp:Label>
                                                                </td>
                                                                <td class="cadlbl" width="240">
                                                                    <asp:Label ID="lblFilial" runat="server" Text="Filial:" CssClass="cadlbl"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td width="240" style="height: 19px">
                                                                    <asp:DropDownList ID="ddlEscalaDepartamental" runat="server" CssClass="cadddl" AutoPostBack="True"
                                                                        Width="228px" 
                                                                        OnSelectedIndexChanged="ddlEscalaDepartamental_SelectedIndexChanged" 
                                                                        Height="16px">
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="rfvEscalaDepartamental" runat="server" ControlToValidate="ddlEscalaDepartamental"
                                                                        ErrorMessage="Campo Obrigatório: Escala Departamental." InitialValue="0" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                                                </td>
                                                                <td width="240" style="height: 19px">
                                                                    <asp:DropDownList ID="ddlRegional" runat="server" CssClass="cadddl" AutoPostBack="false"
                                                                        Width="150px" Enabled="False">
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="rfvRegional" runat="server" ControlToValidate="ddlRegional"
                                                                        ErrorMessage="Campo Obrigatório: Regional." InitialValue="0" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                                                </td>
                                                                <td width="240" style="height: 19px">
                                                                    <asp:DropDownList ID="ddlFilial" runat="server" CssClass="cadddl" Enabled="False"
                                                                        Width="150px">
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="rfvFilial" runat="server" ControlToValidate="ddlFilial"
                                                                        ErrorMessage="Campo Obrigatório: Filial." InitialValue="0" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td width="240" style="height: 19px">
                                                                    <asp:Label ID="lblPeriodoRonda" runat="server" 
                                                                        Text="Período:" CssClass="cadlbl"></asp:Label>
                                                                </td>
                                                                <td width="240" style="height: 19px">
                                                                    <asp:Label ID="lblPeríodo" runat="server" Text="Escalação anterior para espelhamento:" 
                                                                        CssClass="cadlbl"></asp:Label>
                                                                </td>
                                                                <td width="240" style="height: 19px">
                                                                    &nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td width="240" style="height: 19px">
                                                                    <asp:DropDownList ID="ddlPeriodo" runat="server" CssClass="cadddl" 
                                                                        Width="228px" Height="16px">
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="rfvPeriodo" runat="server" ControlToValidate="ddlPeriodo"
                                                                        ErrorMessage="Campo Obrigatório: Período." 
                                                                        InitialValue="0" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                                                </td>
                                                                <td width="240" style="height: 19px">
                                                                    <asp:DropDownList  BackColor="DarkRed" ForeColor="White" ID="ddlPeriodoRonda" runat="server" CssClass="cadddl" 
                                                                        Width="228px" Height="16px">
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="rfvPeriodoRonda" runat="server" ControlToValidate="ddlPeriodoRonda"
                                                                        ErrorMessage="Campo Obrigatório: Escalação anterior para espelhamento." 
                                                                        InitialValue="0" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                                                </td>
                                                                <td width="240" style="height: 19px">
                                                                    &nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td class="cadlbl" colspan="3">
                                                                    &nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td class="cadlbl" colspan="3" style="padding-top: 10px;">
                                                                    <asp:Label ID="lblListaColaboradores" runat="server" 
                                                                        Text="Lista de Colaboradores:" CssClass="cadlbl"></asp:Label>
                                                                    <asp:ImageButton ID="btnAdicionar" CssClass="BotaoAdicionar" runat="server" ImageUrl="~/Imagens/icones/ico_btn_adiconar.png"
                                                                        OnClick="btnAdicionar_Click" CausesValidation="False" ToolTip="Adicionar Colaboradores" />
                                                                    <asp:ImageButton ID="btnRemover" CssClass="BotaoAdicionar" runat="server" ImageUrl="~/Imagens/icones/ico_btn_remover.png"
                                                                        CausesValidation="False" ToolTip="Remover Colaborador(es)" OnClick="btnRemover_Click" />
                                                                    <asp:CustomValidator ID="cvrListaColaboradores" runat="server" ErrorMessage="Campo Obrigatório: Lista de Colaboradores"
                                                                        OnServerValidate="cvrListaColaboradores_ServerValidate">*</asp:CustomValidator>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="3" style="padding-bottom: 5px;">
                                                                    <asp:ListBox ID="lstColaboradores" runat="server" Height="300px" Width="643px" SelectionMode="Multiple"
                                                                        CssClass="cadlstBox"></asp:ListBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>

                                        </rada:RadAjaxPanel>
                                        <!-- ********************* START: BARRA DE BOTÕES INFERIOR ************************** -->
                                        <table cellspacing="0" cellpadding="0" width="760" align="center" border="0">
                                            <caption>
                                            </caption>
                                            <tbody>
                                                <tr>
                                                    <td style="height: 28px" class="backBarraBotoes" align="right">
                                                        &nbsp;
                                                        <asp:Button ID="btnAvancar" OnClick="btnAvancar_Click" runat="server" Text="Avançar"
                                                            CssClass="cadbutton100"></asp:Button>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                        <!-- ********************* END: BARRA DE BOTÕES INFERIOR ************************** -->
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </asp:Panel>
                    <!-- ********************* END: PAINEL CADASTRO ************************** -->
                </rada:RadAjaxPanel>
                <rada:AjaxLoadingPanel ID="lpaCadastro" runat="server" Width="75px" Height="75px"
                    Transparency="30" HorizontalAlign="Center">
                    <asp:Image ID="Image1" Style="margin-top: 100px" runat="server" ImageUrl="%%PATH%%/RadControls/Ajax/Skins/Default/Loading.gif"
                        AlternateText="Aguarde ..."></asp:Image>
                </rada:AjaxLoadingPanel>
            </td>
        </tr>
    </table>
    <iframe id="IframeAgenda" runat="server" style="z-index: 101; left: -500px; visibility: visible;
        position: absolute; top: 0px" name="gToday:contrast:agenda.js" src="../../Agenda/calendario.htm"
        frameborder="0" width="132" scrolling="no" height="142"></iframe>
    </form>
</body>
</html>
