<%@ Register TagPrefix="chart" Namespace="ChartDirector" Assembly="netchartdir" %>

<%@ Page Language="vb" AutoEventWireup="false" Codebehind="RelatorioHorarios.aspx.vb"
    Inherits="FrameWork.UI.RelatorioHorarios" %>

<%@ Register TagPrefix="cc1" Namespace="FrameWork.WebControl" Assembly="FrameWork.WebControl" %>
<%@ Register TagPrefix="rada" Namespace="Telerik.WebControls" Assembly="RadAjax.Net2" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>%%SITE%%</title>
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="%%PATH%%/estilos/FrameWork.css" type="text/css" rel="stylesheet">
</head>
<body>
    <form id="Form1" method="post" runat="server">
        <table cellspacing="0" cellpadding="0" width="778" align="center" border="0">
            <tr>
                <td>
                    <cc1:FWServerControl ID="Fwservercontrol3" runat="server" Controle="PagebannerAdmin">
                    </cc1:FWServerControl>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <table class="backPadrao" height="424" cellspacing="0" cellpadding="0" width="100%"
                        align="center">
                        <tr>
                            <td class="BordaDefaultLRB" valign="top" align="center">
                                <!-- cccccccccccc START: Panel AJAX ccccccccccc-->
                                <rada:RadAjaxPanel ID="RadAjaxPanel1" runat="server" ClientEvents-OnRequestStart="OnRequestStart"
                                    LoadingPanelID="lpaCadastro">
                                    <!--cccccccccccc START: Panel Listagem ccccccccccc-->
                                    <asp:Panel ID="pnlErro" runat="server">
                                    </asp:Panel>
                                    <table class="MarginTopBarra" cellspacing="0" cellpadding="0" width="7460" align="center"
                                        border="0">
                                        <tr>
                                            <td class="CantoEsq">
                                            </td>
                                            <td class="cadBarraTitulo" width="100%" height="15">
                                                <img src="%%PATH%%/Imagens/comum/bulletbarra.gif" align="middle" />
                                                <asp:Label ID="lblTitulo" runat="server">Relatórios de Horários</asp:Label></td>
                                            <td class="CantoDir">
                                            </td>
                                        </tr>
                                        <tr>
                                            <!-- cccccccccccc START: Conteudo  ccccccccccc -->
                                            <td valign="top" align="center" colspan="3">
                                                <table class="ContainerBordaLRB" cellspacing="0" cellpadding="5" width="100%" align="center"
                                                    border="0">
                                                    <tr>
                                                        <td class="cadMsg" align="left">
                                                            <asp:Label ID="lblMensagem" runat="server" ForeColor="Red"></asp:Label>
                                                            <asp:ValidationSummary ID="Validationsummary1" runat="server" DisplayMode="List"></asp:ValidationSummary>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table id="dtgItemStyle" cellspacing="2" cellpadding="2" width="100%" align="center"
                                                                border="0">
                                                                <tr>
                                                                    <td class="cadlbl">
                                                                        <asp:Label ID="lblGrupos" runat="server"><u>G</u>rupo:</asp:Label><br>
                                                                        <asp:DropDownList ID="ddlGrupo" AccessKey="G" runat="server" CssClass="cadddl">
                                                                        </asp:DropDownList></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="cadlbl">
                                                                        <asp:Label ID="lblUsuario" runat="server"><u>U</u>suário:</asp:Label><br>
                                                                        <asp:DropDownList ID="ddlUsuario" AccessKey="U" runat="server" CssClass="cadddl">
                                                                        </asp:DropDownList></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="cadlbl">
                                                                        <asp:Label ID="lblDataInicial" runat="server"><u>D</u>ata:</asp:Label><br>
                                                                        <cc1:FWMascara ID="txtData" AccessKey="D" runat="server" CssClass="cadtxt" Mascara="DATA"
                                                                            Width="100px" MaxLength="10"></cc1:FWMascara><a hidefocus onclick="if(self.gfPop)gfPop.fPopCalendar(document.forms[0].all('<% = me.txtData.ClientID.ToString() %>'));return false;"
                                                                                href="javascript:void(0)">
                                                                                <img height="19" alt="" src="%%PATH%%/agenda/calbtn.gif" width="20" align="absMiddle"
                                                                                    border="0" name="popcal"></a>
                                                                        <asp:RangeValidator ID="rgvtxtInicioPeriodo" runat="server" CssClass="nomecampos"
                                                                            ControlToValidate="txtData" ErrorMessage="- Data inválida: A data deve estar entre 01/01/1900 e 31/12/2099: Data Cadastro"
                                                                            Display="Dynamic">*</asp:RangeValidator>
                                                                        <asp:RequiredFieldValidator ID="rfvDataInicial" runat="server" ControlToValidate="txtData"
                                                                            ErrorMessage="- Campo Obrigatório: Data">*</asp:RequiredFieldValidator></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="cadlbl">
                                                                        <asp:Label ID="lblGrafico" runat="server">G<u>r</u>áfico:</asp:Label><br>
                                                                        <asp:DropDownList ID="ddlTipoGrafico" AccessKey="R" runat="server" CssClass="cadddl">
                                                                            <asp:ListItem Value="1">3D Bar Chart</asp:ListItem>
                                                                            <asp:ListItem Value="0" Selected="True">Cylinder Bar Shape</asp:ListItem>
                                                                            <asp:ListItem Value="2">3D Line Chart</asp:ListItem>
                                                                            <asp:ListItem Value="3">Pie with Small Sectors</asp:ListItem>
                                                                        </asp:DropDownList></td>
                                                                </tr>
                                                            </table>
                                                            <table id="tblBotoes" height="50" cellspacing="0" cellpadding="0" width="100%" align="center"
                                                                border="0">
                                                                <tr>
                                                                    <td valign="middle" align="center">
                                                                        <asp:Button ID="btnGerar" runat="server" CssClass="cadbutton" Text="Gerar Relatório"
                                                                            Width="106px"></asp:Button></td>
                                                                </tr>
                                                            </table>
                                                            <table cellspacing="0" cellpadding="0" width="100%" align="center" border="0">
                                                                <tr>
                                                                    <td align="center">
                                                                        <chart:WebChartViewer ID="WebChartViewer1" runat="server"></chart:WebChartViewer></td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <table cellspacing="0" cellpadding="0" width="100%" align="center" border="0">
                                                    <tr>
                                                        <td class="backBarraBotoes">
                                                            <input class="cadbutton100" id="btnVoltar" onclick="javascript:window.navigate('%%PATH%%/Admin/Admin.aspx')"
                                                                type="button" value="Voltar" name="btnVoltar" runat="server">
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                    <!-- cccccccccccc END: Panel Cadastro ccccccccccc-->
                                </rada:RadAjaxPanel>
                                <rada:AjaxLoadingPanel ID="lpaCadastro" runat="server" Width="75px" HorizontalAlign="Center"
                                    Transparency="30" Height="75px">
                                    <asp:Image ID="Image1" Style="margin-top: 100px" runat="server" AlternateText="Aguarde ..."
                                        ImageUrl="%%PATH%%/RadControls/Ajax/Skins/Default/Loading.gif"></asp:Image>
                                </rada:AjaxLoadingPanel>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <cc1:FWServerControl ID="RodapeAdmin" runat="server" Controle="RodapeAdmin"></cc1:FWServerControl>
                </td>
            </tr>
        </table>
        <asp:HtmlIframe id="IframeAgenda" style="z-index: 101; left: -500px; visibility: visible;
            position: absolute; top: 0px" name="gToday:contrast:agenda.js" src="%%PATH%%/agenda/calendario.htm"
            frameborder="0" width="132" scrolling="no" height="142" runat="server"/>
    </form>
</body>
</html>
