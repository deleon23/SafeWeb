<%@ Page Language="C#" AutoEventWireup="true" Codebehind="CadRegra.aspx.cs" Inherits="SafWeb.UI.Admin.Seguranca.Usuarios.CadRegra" %>

<%@ Register TagPrefix="radG" Namespace="Telerik.WebControls" Assembly="RadGrid.Net2" %>
<%@ Register TagPrefix="rada" Namespace="Telerik.WebControls" Assembly="RadAjax.Net2" %>
<%@ Register TagPrefix="cc1" Namespace="FrameWork.WebControl" Assembly="FrameWork.WebControl" %>
<html>
<head runat="server">
    <title>%%SITE%%</title>
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../../../Estilos/FrameWork.css" type="text/css" rel="stylesheet">
</head>
<body>
    <form id="Form1" method="post" runat="server">
        <table cellspacing="0" cellpadding="0" width="778" align="center" border="0">
            <!-- cccccccccccc START: Cabecalho  ccccccccccc -->
            <tr>
                <td>
                    <cc1:FWServerControl ID="Fwservercontrol3" runat="server" Controle="PagebannerAdmin">
                    </cc1:FWServerControl>
                </td>
            </tr>
            <!-- cccccccccccc END: Cabecalho  ccccccccccc -->
            <tr>
                <td valign="top" class="backbox">
                    <table class="backPadrao" height="424" cellspacing="0" cellpadding="0" width="100%"
                        align="center">
                        <tr>
                            <td class="BordaDefaultLRB" valign="top" align="center">
                                <!-- cccccccccccc START: Panel AJAX ccccccccccc-->
                                <rada:RadAjaxPanel ID="RadAjaxPanel1" runat="server" ClientEvents-OnRequestStart="OnRequestStart"
                                    LoadingPanelID="lpaCadastro">
                                    <!-- cccccccccccc START: Panel Listagem ccccccccccc-->
                                    <asp:Panel ID="pnlErro" runat="server">
                                    </asp:Panel>
                                    <!-- cccccccccccc START: Panel Cadastro ccccccccccc-->
                                    <asp:Panel ID="pnlCadastro" runat="server">
                                        <!-- cccccccccccc START: Titulo Cadastro ccccccccccc -->
                                        <table class="MarginTopBarra" cellspacing="0" cellpadding="0" width="760" align="center"
                                            border="0">
                                            <tr>
                                                <td class="CantoEsq">
                                                </td>
                                                <td class="cadBarraTitulo" width="100%" height="15">
                                                    <img src="../Imagens/comum/bulletbarra.gif" align="middle" />
                                                    <asp:Label ID="lblTitulo" runat="server">Regras de Envio de E-mail</asp:Label>
                                                </td>
                                                <td class="CantoDir">
                                                </td>
                                            </tr>
                                            <tr>
                                                <!-- cccccccccccc START: Conteudo  ccccccccccc -->
                                                <td valign="top" align="center" colspan="3">
                                                    <table class="ContainerBordaLRB" cellspacing="0" cellpadding="5" width="100%" align="center"
                                                        border="0">
                                                        <tr>
                                                            <td>
                                                                <table id="dtgItemStyle" cellspacing="2" cellpadding="2" width="100%" align="center"
                                                                    border="0">
                                                                    <tr id="trIdioma" runat="server">
                                                                        <td class="cadlbl">
                                                                            <asp:Label ID="Label1" runat="server" Text="Descrição:" Font-Bold="True"></asp:Label>
                                                                        </td>
                                                                        <td class="cadlbl">
                                                                            <asp:Label ID="Label2" runat="server" Text="Horário:" Font-Bold="True"></asp:Label>
                                                                        </td>
                                                                        <td class="cadlbl">
                                                                            <asp:Label ID="Label3" runat="server" Text="Receber e-mail:" Font-Bold="True"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr runat="server">
                                                                        <td class="cadlbl">
                                                                            <asp:Label ID="lblDesc1" runat="server"></asp:Label>
                                                                        </td>
                                                                        <td class="cadlbl">
                                                                            <asp:Label ID="lblHora1" runat="server"></asp:Label>
                                                                        </td>
                                                                        <td class="cadlbl">
                                                                            <asp:CheckBox ID="CheckBox1" runat="server" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr id="Tr1" runat="server">
                                                                        <td class="cadlbl">
                                                                            <asp:Label ID="lblDesc2" runat="server"></asp:Label>
                                                                        </td>
                                                                        <td class="cadlbl">
                                                                            <asp:Label ID="lblHora2" runat="server"></asp:Label>
                                                                        </td>
                                                                        <td class="cadlbl">
                                                                            <asp:CheckBox ID="CheckBox2" runat="server" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr id="Tr2" runat="server">
                                                                        <td class="cadlbl">
                                                                            <asp:Label ID="lblDesc3" runat="server"></asp:Label>
                                                                        </td>
                                                                        <td class="cadlbl">
                                                                            <asp:Label ID="lblHora3" runat="server"></asp:Label>
                                                                        </td>
                                                                        <td class="cadlbl">
                                                                            <asp:CheckBox ID="CheckBox3" runat="server" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr id="Tr3" runat="server">
                                                                        <td class="cadlbl">
                                                                            <asp:Label ID="lblDesc4" runat="server"></asp:Label>
                                                                        </td>
                                                                        <td class="cadlbl">
                                                                            <asp:Label ID="lblHora4" runat="server"></asp:Label>
                                                                        </td>
                                                                        <td class="cadlbl">
                                                                            <asp:CheckBox ID="CheckBox4" runat="server" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr id="Tr4" runat="server">
                                                                        <td class="cadlbl">
                                                                            <asp:Label ID="lblDesc5" runat="server"></asp:Label>
                                                                        </td>
                                                                        <td class="cadlbl">
                                                                            <asp:Label ID="lblHora5" runat="server"></asp:Label>
                                                                        </td>
                                                                        <td class="cadlbl">
                                                                            <asp:CheckBox ID="CheckBox5" runat="server" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr id="Tr5" runat="server">
                                                                        <td class="cadlbl">
                                                                            <asp:Label ID="lblDesc6" runat="server"></asp:Label>
                                                                        </td>
                                                                        <td class="cadlbl">
                                                                            <asp:Label ID="lblHora6" runat="server"></asp:Label>
                                                                        </td>
                                                                        <td class="cadlbl">
                                                                            <asp:CheckBox ID="CheckBox6" runat="server" />
                                                                        </td>
                                                                    </tr>
                                                                    
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                        <table cellspacing="0" cellpadding="0" width="100%" align="center" border="0">
                                            <tr>
                                                <td class="backBarraBotoes">
                                                    <asp:Button ID="btnSalvarCadastro" runat="server" CssClass="cadbutton100" Text="Salvar"
                                                        ValidationGroup="Salvar"></asp:Button>&nbsp;&nbsp; &nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </rada:RadAjaxPanel>
                            </td>
                        </tr>
                    </table>
                    <rada:AjaxLoadingPanel ID="lpaCadastro" Width="75px" runat="server" HorizontalAlign="Center"
                        Transparency="30" Height="75px">
                        <asp:Image ID="Image1" Style="margin-top: 100px" runat="server" ImageUrl="%%PATH%%/RadControls/Ajax/Skins/Default/Loading.gif"
                            AlternateText="Aguarde ..."></asp:Image>
                    </rada:AjaxLoadingPanel>
                    <!-- cccccccccccc END: Panel AJAX ccccccccccc-->
                </td>
            </tr>
            <!-- cccccccccccc START: Rodape  ccccccccccc -->
            <tr>
                <td>
                    <cc1:FWServerControl ID="Fwservercontrol2" runat="server" Controle="RodapeAdmin"></cc1:FWServerControl>
                </td>
            </tr>
            <!-- cccccccccccc END: Rodape  ccccccccccc -->
        </table>
    </form>
</body>
</html>
