<%@ Page Language="C#" AutoEventWireup="true" Codebehind="CadHorario.aspx.cs" Inherits="SafWeb.UI.Modulos.CadHorarrio" %>

<%@ Register TagPrefix="rada" Namespace="Telerik.WebControls" Assembly="RadAjax.Net2" %>
<%@ Register TagPrefix="cc1" Namespace="FrameWork.WebControl" Assembly="FrameWork.WebControl" %>
<html>
<head>
    <title>%%SITE%%</title>
    <link href="../Estilos/FrameWork.css" type="text/css" rel="stylesheet">
    <link href="../Estilos/Brinks.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form" method="post" runat="server">
        <table cellspacing="0" cellpadding="0" width="778" align="center" border="0">
            <tr>
                <td>
                    <!-- ********************* START: CABEÇALHO ****************** -->
                     <cc1:FWServerControl ID="FWServerControlCabecalho" runat="server" Controle="Pagebanner"
                        Arquivo="/Modulos/Framework/CabecalhoBrinks.ascx"></cc1:FWServerControl>
                    <!-- ********************* END: CABEÇALHO ****************** -->
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
                                                            <td colspan="3" class="cadmsg">
                                                                <asp:Label ID="lblMensagemCad" runat="server" Text="Label" Visible="False" CssClass="cadlbl"></asp:Label>
                                                            </td>
                                                        </tr>
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
                                                                            &nbsp;</td>
                                                                    </tr>
                                                                    <tr id="Tr1" runat="server">
                                                                        <td class="cadlbl">
                                                                            <asp:Label ID="lblDesc1" runat="server"></asp:Label>
                                                                        </td>
                                                                        <td class="cadlbl">
                                                                            <asp:Label ID="lblHora1" runat="server"></asp:Label>
                                                                        </td>
                                                                        <td class="cadlbl">
                                                                            <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="True" Text="Receber e-mail" Visible="False" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr id="Tr2" runat="server">
                                                                        <td class="cadlbl">
                                                                            <asp:Label ID="lblDesc2" runat="server"></asp:Label>
                                                                        </td>
                                                                        <td class="cadlbl">
                                                                            <asp:Label ID="lblHora2" runat="server"></asp:Label>
                                                                        </td>
                                                                        <td class="cadlbl">
                                                                            <asp:CheckBox ID="CheckBox2" runat="server" AutoPostBack="True" Text="Receber e-mail" Visible="False" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr id="Tr3" runat="server">
                                                                        <td class="cadlbl">
                                                                            <asp:Label ID="lblDesc3" runat="server"></asp:Label>
                                                                        </td>
                                                                        <td class="cadlbl">
                                                                            <asp:Label ID="lblHora3" runat="server"></asp:Label>
                                                                        </td>
                                                                        <td class="cadlbl">
                                                                            <asp:CheckBox ID="CheckBox3" runat="server" AutoPostBack="True" Text="Receber e-mail" Visible="False" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr id="Tr4" runat="server">
                                                                        <td class="cadlbl">
                                                                            <asp:Label ID="lblDesc4" runat="server"></asp:Label>
                                                                        </td>
                                                                        <td class="cadlbl">
                                                                            <asp:Label ID="lblHora4" runat="server"></asp:Label>
                                                                        </td>
                                                                        <td class="cadlbl">
                                                                            <asp:CheckBox ID="CheckBox4" runat="server" AutoPostBack="True" Text="Receber e-mail" Visible="False" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr id="Tr5" runat="server">
                                                                        <td class="cadlbl">
                                                                            <asp:Label ID="lblDesc5" runat="server"></asp:Label>
                                                                        </td>
                                                                        <td class="cadlbl">
                                                                            <asp:Label ID="lblHora5" runat="server"></asp:Label>
                                                                        </td>
                                                                        <td class="cadlbl">
                                                                            <asp:CheckBox ID="CheckBox5" runat="server" AutoPostBack="True" Text="Receber e-mail" Visible="False" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr id="Tr6" runat="server">
                                                                        <td class="cadlbl">
                                                                            <asp:Label ID="lblDesc6" runat="server"></asp:Label>
                                                                        </td>
                                                                        <td class="cadlbl">
                                                                            <asp:Label ID="lblHora6" runat="server"></asp:Label>
                                                                        </td>
                                                                        <td class="cadlbl">
                                                                            <asp:CheckBox ID="CheckBox6" runat="server" AutoPostBack="True" Text="Receber e-mail" Visible="False" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr id="Tr7" runat="server">
                                                                        <td class="cadlbl">
                                                                            <asp:Label ID="lblDesc7" runat="server"></asp:Label>
                                                                        </td>
                                                                        <td class="cadlbl">
                                                                            <asp:Label ID="lblHora7" runat="server"></asp:Label>
                                                                        </td>
                                                                        <td class="cadlbl">
                                                                            <asp:CheckBox ID="CheckBox7" runat="server" AutoPostBack="True" Text="Receber e-mail" Visible="False" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr id="Tr8" runat="server">
                                                                        <td class="cadlbl">
                                                                            <asp:Label ID="lblDesc8" runat="server"></asp:Label>
                                                                        </td>
                                                                        <td class="cadlbl">
                                                                            <asp:Label ID="lblHora8" runat="server"></asp:Label>
                                                                        </td>
                                                                        <td class="cadlbl">
                                                                            <asp:CheckBox ID="CheckBox8" runat="server" AutoPostBack="True" Text="Receber e-mail" Visible="False" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr id="Tr9" runat="server">
                                                                        <td class="cadlbl">
                                                                            <asp:Label ID="lblDesc9" runat="server"></asp:Label>
                                                                        </td>
                                                                        <td class="cadlbl">
                                                                            <asp:Label ID="lblHora9" runat="server"></asp:Label>
                                                                        </td>
                                                                        <td class="cadlbl">
                                                                            <asp:CheckBox ID="CheckBox9" runat="server" AutoPostBack="True" Text="Receber e-mail" Visible="False" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr id="Tr10" runat="server">
                                                                        <td class="cadlbl">
                                                                            <asp:Label ID="lblDesc10" runat="server"></asp:Label>
                                                                        </td>
                                                                        <td class="cadlbl">
                                                                            <asp:Label ID="lblHora10" runat="server"></asp:Label>
                                                                        </td>
                                                                        <td class="cadlbl">
                                                                            <asp:CheckBox ID="CheckBox10" runat="server" AutoPostBack="True" Text="Receber e-mail" Visible="False" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr id="Tr11" runat="server">
                                                                        <td class="cadlbl" colspan="3">
                                                                            <br />
                                                                            <asp:Label ID="lblAprovadores" runat="server" Text="Aprovador 2º nível:"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr id="Tr12" runat="server">
                                                                        <td class="cadlbl" colspan="3">
                                                                            <asp:DropDownList ID="ddlAprovadores" runat="server" CssClass="cadddl">
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                        <table cellspacing="0" cellpadding="0" width="760" align="center" border="0">
                                            <tr>
                                                <td class="backBarraBotoes">
                                                    <asp:Button ID="btnSalvarCadastro" runat="server" CssClass="cadbutton100" Text="Salvar"
                                                        ValidationGroup="Salvar" OnClick="btnSalvarCadastro_Click"></asp:Button>&nbsp;&nbsp;
                                                    &nbsp;
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
