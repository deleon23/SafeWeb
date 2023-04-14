<%@ Register TagPrefix="radG" Namespace="Telerik.WebControls" Assembly="RadGrid.Net2" %>
<%@ Register TagPrefix="rada" Namespace="Telerik.WebControls" Assembly="RadAjax.Net2" %>
<%@ Register TagPrefix="cc1" Namespace="FrameWork.WebControl" Assembly="FrameWork.WebControl" %>

<%@ Page Language="vb" AutoEventWireup="false" Codebehind="CadGrupos.aspx.vb" Inherits="FrameWork.UI.CadGrupos" %>

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
            <!-- cccccccccccc START: Cabecalho  ccccccccccc -->
            <tr>
                <td>
                    <cc1:FWServerControl ID="Fwservercontrol3" runat="server" Controle="PagebannerAdmin">
                    </cc1:FWServerControl>
                </td>
            </tr>
            <!-- cccccccccccc END: Cabecalho  ccccccccccc -->
            <tr>
                <td valign="top">
                    <table class="backPadrao" height="424" cellspacing="0" cellpadding="0" width="100%"
                        align="center">
                        <tr>
                            <td class="BordaDefaultLRB" valign="top" align="center">
                                <!-- cccccccccccc START: Panel AJAX ccccccccccc-->
                                <rada:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="lpaCadastro"
                                    ClientEvents-OnRequestStart="OnRequestStart">
                                    <!-- cccccccccccc START: Panel Listagem ccccccccccc-->
                                    <asp:Panel ID="pnlErro" runat="server">
                                    </asp:Panel>
                                    <asp:Panel ID="pnlListagem" runat="server">
                                        <!-- cccccccccccc START: Titulo  ccccccccccc -->
                                        <table class="MarginTopBarra" cellspacing="0" cellpadding="0" width="760" align="center"
                                            border="0">
                                            <tr>
                                                <td class="CantoEsq">
                                                </td>
                                                <td class="cadBarraTitulo" width="100%" height="15">
                                                    <img src="%%PATH%%/Imagens/comum/bulletbarra.gif" align="middle" />
                                                    <asp:Label ID="lblTituloListagem" runat="server">Listagem de Grupos</asp:Label></td>
                                                <td class="CantoDir">
                                                </td>
                                            </tr>
                                            <!-- cccccccccccc END: Titulo  ccccccccccc -->
                                            <!-- cccccccccccc START: Conteudo  ccccccccccc -->
                                            <tr>
                                                <td valign="top" align="center" colspan="3">
                                                    <table class="ContainerBordaLRB" cellspacing="0" cellpadding="5" width="100%" align="center"
                                                        border="0">
                                                        <tr>
                                                            <td class="cadMsg" align="left">
                                                                <asp:Label ID="lblMensagemListagem" runat="server">Pesquisa</asp:Label>
                                                                <asp:ValidationSummary ID="Validationsummary3" runat="server" DisplayMode="List"></asp:ValidationSummary>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <table cellspacing="2" cellpadding="2" width="100%" align="center" border="0">
                                                                    <tr>
                                                                        <td class="cadlbl">
                                                                            <asp:Label ID="lblSuperGrupo" runat="server">Super <u>G</u>rupo:</asp:Label><br>
                                                                            <asp:DropDownList ID="ddlSuperGrupo" AccessKey="G" runat="server" CssClass="cadddl">
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="cadlbl">
                                                                            <asp:Label ID="lblNomeListagem" runat="server"><u>N</u>ome Grupo:</asp:Label><br>
                                                                            <asp:TextBox ID="txtNomeListagem" AccessKey="N" runat="server" CssClass="cadtxt"
                                                                                Width="300px" MaxLength="50"></asp:TextBox></td>
                                                                    </tr>
                                                                </table>
                                                                <table id="tblBotoes" height="50" cellspacing="0" cellpadding="0" width="100%" align="center"
                                                                    border="0">
                                                                    <tr>
                                                                        <td valign="middle" align="center">
                                                                            <asp:Button ID="btnBuscar" runat="server" CssClass="cadbutton" Text="Buscar" Width="90px">
                                                                            </asp:Button></td>
                                                                    </tr>
                                                                </table>
                                                                <table height="22" cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                    <tr>
                                                                        <td id="ColunaExportacao" align="right" runat="server">
                                                                            <asp:ImageButton ID="btnExportWord" runat="server" ImageUrl="%%PATH%%/imagens/icones/ico_doc.gif">
                                                                            </asp:ImageButton>&nbsp;
                                                                            <asp:ImageButton ID="btnExportExcel" runat="server" ImageUrl="%%PATH%%/imagens/icones/ico_xls.gif">
                                                                            </asp:ImageButton></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <radG:RadGrid ID="radGridDados" runat="server" CssClass="dtg" Width="100%" Skin="None"
                                                                                GridLines="None" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False">
                                                                                <GroupHeaderItemStyle CssClass="dtgGroupHeader"></GroupHeaderItemStyle>
                                                                                <GroupPanel Text="">
                                                                                    <PanelStyle CssClass="dtgGroupPanel"></PanelStyle>
                                                                                    <PanelItemsStyle CssClass="dtgGroupPanelItem"></PanelItemsStyle>
                                                                                </GroupPanel>
                                                                                <AlternatingItemStyle CssClass="dtgItemStyle"></AlternatingItemStyle>
                                                                                <ItemStyle CssClass="dtgItemStyleAlternate"></ItemStyle>
                                                                                <HeaderStyle CssClass="dtgHeaderStyle"></HeaderStyle>
                                                                                <MasterTableView>
                                                                                    <NoRecordsTemplate>
                                                                                        <div>
                                                                                            <table width="100%" height="300" cellpadding="0" cellspacing="0">
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <% = FrameWork.BusinessLayer.Idioma.BLIdiomas.TraduzirMensagens(FrameWork.Model.Idioma.Mensagens.CONSULTA_SEM_RETORNO) %>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </div>
                                                                                    </NoRecordsTemplate>
                                                                                    <PagerTemplate>
                                                                                        <table height="35" cellspacing="0" class="pag" cellpadding="5" width="100%" align="center"
                                                                                            border="0">
                                                                                            <tr>
                                                                                                <td valign="center" align="center">
                                                                                                    <asp:Panel runat="server" ID="pnlPaginaAtual" DefaultButton="btnIr">
                                                                                                        <asp:ImageButton runat="server" ID="imgPrimeira" CssClass="pagImg" CausesValidation="false"
                                                                                                            CommandArgument="First" CommandName="Page" ImageUrl="~/imagens/icones/ico_setaesq.gif" />
                                                                                                        <asp:LinkButton ID="btnPrimeira" runat="server" CssClass="pagLink" CausesValidation="False"
                                                                                                            CommandArgument="First" Enabled="True" CommandName="Page">Primeira</asp:LinkButton>
                                                                                                        <asp:ImageButton runat="server" ID="imgAnterior" CssClass="pagImg" CausesValidation="false"
                                                                                                            CommandArgument="Prev" CommandName="Page" ImageUrl="~/imagens/icones/ico_setaesq2.gif" />
                                                                                                        <asp:LinkButton ID="btnAnterior" CommandName="Page" runat="server" CssClass="pagLink"
                                                                                                            CausesValidation="False" CommandArgument="Prev" Enabled="True">Anterior</asp:LinkButton>&nbsp;&nbsp;
                                                                                                        <asp:Label CssClass="paglbl" ID="lblPaginaDescricao" runat="server">| P�gina:</asp:Label>
                                                                                                        <cc1:FWMascara ID="txtPagina" Text='<%# cint(DataBinder.Eval(Container, "OwnerTableView.CurrentPageIndex")) + 1 %>'
                                                                                                            runat="server" CssClass="pagtxtbox" Width="50px" MaxLength="4" AutoPostBack="False"
                                                                                                            Mascara="NUMERO"></cc1:FWMascara>
                                                                                                        <asp:Label CssClass="paglbl" ID="lblPaginaDe" runat="server">de</asp:Label>
                                                                                                        <asp:Label CssClass="paglbl" ID="lblPagina" runat="server" Enabled="True"><%#Right("00000" + DataBinder.Eval(Container, "Paging.PageCount").ToString(), 4)%></asp:Label>
                                                                                                        <asp:LinkButton ID="btnIr" runat="server" CssClass="pagLink" CommandName="IrPagina">Ir</asp:LinkButton><span
                                                                                                            class="paglbl">&nbsp;|&nbsp;</span>
                                                                                                        <asp:LinkButton ID="btnProxima" runat="server" CssClass="pagLink" CausesValidation="False"
                                                                                                            CommandName="Page" CommandArgument="Next" Enabled="True">Pr�xima</asp:LinkButton>
                                                                                                        <asp:ImageButton runat="server" ID="imgProxima" CssClass="pagImg" CausesValidation="false"
                                                                                                            CommandArgument="Next" CommandName="Page" ImageUrl="~/imagens/icones/ico_setadir2.gif" />
                                                                                                        <asp:LinkButton ID="btnUltima" runat="server" CssClass="pagLink" CausesValidation="False"
                                                                                                            CommandName="Page" CommandArgument="Last" Enabled="True">�ltima</asp:LinkButton>
                                                                                                        <asp:ImageButton runat="server" ID="imgUltima" CssClass="pagImg" CausesValidation="false"
                                                                                                            CommandArgument="Last" CommandName="Page" ImageUrl="~/imagens/icones/ico_setadir.gif" />
                                                                                                    </asp:Panel>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </PagerTemplate>
                                                                                    <Columns>
                                                                                        <radG:GridBoundColumn HeaderText="Grupos" SortExpression="GRP_C_NOME" DataField="GRP_C_NOME"
                                                                                            UniqueName="GRP_C_NOME">
                                                                                            <ItemStyle Width="95%"></ItemStyle>
                                                                                        </radG:GridBoundColumn>
                                                                                        <radG:GridTemplateColumn UniqueName="TemplateColumn" Visible="false">
                                                                                            <ItemStyle Width="5%" HorizontalAlign="Center"></ItemStyle>
                                                                                            <ItemTemplate>
                                                                                                <asp:ImageButton ID="btnEditar" AlternateText="Editar" runat="server" CausesValidation="False"
                                                                                                    CommandArgument='<%# DataBinder.Eval(Container.DataItem, "GRP_N_CODIGO") %>'
                                                                                                    ToolTip="Editar" ImageUrl="%%PATH%%/imagens/icones/ico_editar.gif" CommandName="Editar">
                                                                                                </asp:ImageButton>
                                                                                            </ItemTemplate>
                                                                                        </radG:GridTemplateColumn>
                                                                                        <radG:GridButtonColumn CommandName="Excluir" ConfirmText="%%REGISTRO_CONFIRMA_EXCLUSAO%%"
                                                                                            UniqueName="DeleteColumn" ButtonType="ImageButton" ImageUrl="%%PATH%%/imagens/icones/ico_deletar.gif">
                                                                                            <ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
                                                                                        </radG:GridButtonColumn>
                                                                                    </Columns>
                                                                                </MasterTableView>
                                                                            </radG:RadGrid></td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <table cellspacing="0" cellpadding="0" width="100%" align="center" border="0">
                                                        <tr>
                                                            <td class="backBarraBotoes">
                                                                <asp:Button ID="btnNovoListagem" runat="server" CssClass="cadbutton100" Text="Novo"
                                                                    CausesValidation="False"></asp:Button>&nbsp;<input class="cadbutton100" id="btnVoltarListagem"
                                                                        onclick="javascript:window.navigate('%%PATH%%/Admin/Admin.aspx')" type="button"
                                                                        value="Voltar" name="btnVoltarListagem" runat="server">
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                        <!-- cccccccccccc END: Conteudo  ccccccccccc -->
                                    </asp:Panel>
                                    <!-- cccccccccccc END: Panel Listagem ccccccccccc-->
                                    <!-- cccccccccccc START: Panel Cadastro ccccccccccc-->
                                    <asp:Panel ID="pnlCadastro" runat="server">
                                        <!-- cccccccccccc START: Titulo Cadastro ccccccccccc -->
                                        <table class="MarginTopBarra" cellspacing="0" cellpadding="0" width="760" align="center"
                                            border="0">
                                            <tr>
                                                <td class="CantoEsq">
                                                </td>
                                                <td class="cadBarraTitulo" width="100%" height="15">
                                                    <img src="%%PATH%%/Imagens/comum/bulletbarra.gif" align="middle" />
                                                    <asp:Label ID="lblTituloCadastro" runat="server">Cadastro de Grupos</asp:Label></td>
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
                                                                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                    <tr>
                                                                        <td class="cadMsg" colspan="2">
                                                                            <asp:Label ID="lblMensagemCadastro" runat="server">Formul�rio de Cadastro</asp:Label>
                                                                            <asp:ValidationSummary ID="Validationsummary1" runat="server" DisplayMode="List"></asp:ValidationSummary>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                                <table cellspacing="2" cellpadding="2" width="100%" align="center" border="0">
                                                                    <tr>
                                                                        <td class="cadlbl">
                                                                            <asp:Label ID="lblDescricao" runat="server"><u>D</u>escri��o:</asp:Label><br>
                                                                            <asp:TextBox ID="txtDescricao" AccessKey="D" runat="server" CssClass="cadtxt" Width="300px"
                                                                                MaxLength="50"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator ID="rfvNome" runat="server" CssClass="nomecampos" ErrorMessage="- Campo Obrigat�rio: Nome"
                                                                                Display="Dynamic" ControlToValidate="txtDescricao">*</asp:RequiredFieldValidator></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="cadlbl">
                                                                            <asp:Label ID="lblSuperGrupoCadastro" runat="server"><u>S</u>uper Grupo:</asp:Label><br>
                                                                            <asp:DropDownList ID="ddlSuperGrupoCadastro" AccessKey="S" runat="server" CssClass="cadddl">
                                                                            </asp:DropDownList>
                                                                            <asp:RequiredFieldValidator ID="rfvSuperGrupo" runat="server" CssClass="nomecampos"
                                                                                ErrorMessage="- Sele��o Obrigat�ria: Super Grupo" Display="Dynamic" ControlToValidate="ddlSuperGrupoCadastro"
                                                                                InitialValue="0">*</asp:RequiredFieldValidator></td>
                                                                    </tr>
                                                                </table>
                                                                <table cellspacing="2" cellpadding="2" border="0">
                                                                    <tr>
                                                                        <td class="cadlbl" width="150">
                                                                            <asp:Label ID="lblUsuarios" runat="server">Usu�rio:</asp:Label></td>
                                                                        <td>
                                                                        </td>
                                                                        <td class="cadlbl">
                                                                            <asp:Label ID="lblUsuariosSelecionados" runat="server">Usu�rios Selecionados:</asp:Label></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:ListBox ID="lstUsuariosRemovidos" runat="server" CssClass="cadlstBox" Width="200px"
                                                                                Rows="10" SelectionMode="Multiple"></asp:ListBox></td>
                                                                        <td align="center">
                                                                            &nbsp;&nbsp;
                                                                            <asp:Button ID="cmdUsuAdiciona" runat="server" CssClass="cadbutton" Text=">>" Width="30px"
                                                                                CausesValidation="False"></asp:Button>&nbsp;&nbsp;<br>
                                                                            <br>
                                                                            &nbsp;&nbsp;
                                                                            <asp:Button ID="cmdUsuRemove" runat="server" CssClass="cadbutton" Text="<<" Width="30px"
                                                                                CausesValidation="False"></asp:Button>&nbsp;&nbsp;</td>
                                                                        <td>
                                                                            <asp:ListBox ID="lstUsuariosAdicionados" runat="server" CssClass="cadlstBox" Width="200px"
                                                                                Rows="10" SelectionMode="Multiple"></asp:ListBox></td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <table cellspacing="0" cellpadding="0" width="100%" align="center" border="0">
                                                        <tr>
                                                            <td class="backBarraBotoes">
                                                                <asp:Button ID="btnSalvarCadastro" runat="server" CssClass="cadbutton100" Text="Salvar">
                                                                </asp:Button>&nbsp;
                                                                <asp:Button ID="btnNovoCadastro" runat="server" CssClass="cadbutton100" Text="Novo"
                                                                    CausesValidation="False"></asp:Button>&nbsp;
                                                                <asp:Button ID="btnVoltarCadastro" runat="server" CssClass="cadbutton100" Text="Voltar"
                                                                    CausesValidation="False"></asp:Button></td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                        <!-- cccccccccccc END: Conteudo  ccccccccccc -->
                                    </asp:Panel>
                                    <!-- cccccccccccc END: Panel Cadastro ccccccccccc-->
                                </rada:RadAjaxPanel>
                                <rada:AjaxLoadingPanel ID="lpaCadastro" Width="75px" Height="75px" runat="server"
                                    Transparency="30" HorizontalAlign="Center">
                                    <asp:Image ID="Image1" Style="margin-top: 100px" runat="server" ImageUrl="%%PATH%%/RadControls/Ajax/Skins/Default/Loading.gif"
                                        AlternateText="Aguarde ..."></asp:Image>
                                </rada:AjaxLoadingPanel>
                                <!-- cccccccccccc END: Panel AJAX ccccccccccc-->
                            </td>
                        </tr>
                    </table>
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