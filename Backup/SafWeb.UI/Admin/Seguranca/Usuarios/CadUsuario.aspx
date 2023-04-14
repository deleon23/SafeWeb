<%@ Register TagPrefix="radG" Namespace="Telerik.WebControls" Assembly="RadGrid.Net2" %>
<%@ Register TagPrefix="rada" Namespace="Telerik.WebControls" Assembly="RadAjax.Net2" %>
<%@ Register TagPrefix="cc1" Namespace="FrameWork.WebControl" Assembly="FrameWork.WebControl" %>

<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="CadUsuario.aspx.vb" Inherits="FrameWork.UI.CadUsuario" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>%%SITE%%</title>
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="%%PATH%%/estilos/FrameWork.css" type="text/css" rel="stylesheet">

    <script language="javascript">

        function PreencheCampo() {
            var conteudo = document.getElementById("txtLogin").value

            document.getElementById("txtWinLogon").value = conteudo
        }

        function PreencherNome(nome) {
            document.getElementById("HiddenNome").value = nome
            document.getElementById("txtNome").value = nome
        }

        function PreencherHiddenVisitado(codigo) {
            document.getElementById("HiddenCodigo").value = codigo
        }
       
    </script>

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
                                <asp:Panel ID="pnlListaVisitado" runat="server">
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
                                                <asp:Label ID="lblTituloListagem" runat="server">Listagem de Usuários</asp:Label>
                                            </td>
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
                                                            <asp:ValidationSummary ID="Validationsummary3" runat="server" DisplayMode="List">
                                                            </asp:ValidationSummary>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table cellspacing="2" cellpadding="2" width="100%" align="center" border="0">
                                                                <tr>
                                                                    <td class="cadlbl" colspan="3">
                                                                        <asp:Label ID="lblSuperGrupo" runat="server">Super <u>G</u>rupo:</asp:Label><br>
                                                                        <asp:DropDownList ID="ddlSuperGrupo" AccessKey="G" runat="server" CssClass="cadddl"
                                                                            AutoPostBack="True">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="cadlbl" width="150">
                                                                        <asp:Label ID="lblGrupo" runat="server"><u>G</u>rupo:</asp:Label><br>
                                                                        <asp:DropDownList ID="ddlGrupo" AccessKey="G" runat="server" CssClass="cadddl">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td class="cadlbl" width="150">
                                                                        <asp:Label ID="lblRegional" runat="server"><u>R</u>egional:</asp:Label><br>
                                                                        <asp:DropDownList ID="ddlRegional" runat="server" AccessKey="G" AutoPostBack="True"
                                                                            CssClass="cadddl">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td class="cadlbl" width="150">
                                                                        <asp:Label ID="lblFilial" runat="server"><u>F</u>ilial:</asp:Label><br>
                                                                        <asp:DropDownList ID="ddlFilial" AccessKey="F" runat="server" CssClass="cadddl">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="cadlbl" colspan="3">
                                                                        <asp:Label ID="lblNomeListagem" runat="server"><u>N</u>ome:</asp:Label><br>
                                                                        <asp:TextBox ID="txtNomeListagem" AccessKey="N" runat="server" CssClass="cadtxt"
                                                                            Width="300px" MaxLength="50"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <table id="tblBotoes" height="50" cellspacing="0" cellpadding="0" width="100%" align="center"
                                                                border="0">
                                                                <tr>
                                                                    <td valign="middle" align="center">
                                                                        <asp:Button ID="btnBuscar" TabIndex="19" runat="server" CssClass="cadbutton" Text="Buscar"
                                                                            Width="90px"></asp:Button>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <table height="22" cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                <tr>
                                                                    <td id="ColunaExportacao" align="right" runat="server">
                                                                        <asp:ImageButton ID="btnExportWord" runat="server" ImageUrl="%%PATH%%/imagens/icones/ico_doc.gif">
                                                                        </asp:ImageButton>&nbsp;
                                                                        <asp:ImageButton ID="btnExportExcel" runat="server" ImageUrl="%%PATH%%/imagens/icones/ico_xls.gif">
                                                                        </asp:ImageButton>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <radG:RadGrid ID="radGridDados" runat="server" CssClass="dtg" Width="100%" AllowSorting="True"
                                                                            AllowPaging="True" GridLines="None" Skin="None" AutoGenerateColumns="False">
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
                                                                                                    <asp:Label CssClass="paglbl" ID="lblPaginaDescricao" runat="server">| Página:</asp:Label>
                                                                                                    <cc1:FWMascara ID="txtPagina" Text='<%# cint(DataBinder.Eval(Container, "OwnerTableView.CurrentPageIndex")) + 1 %>'
                                                                                                        runat="server" CssClass="pagtxtbox" Width="50px" MaxLength="4" AutoPostBack="False"
                                                                                                        Mascara="NUMERO"></cc1:FWMascara>
                                                                                                    <asp:Label CssClass="paglbl" ID="lblPaginaDe" runat="server">de</asp:Label>
                                                                                                    <asp:Label CssClass="paglbl" ID="lblPagina" runat="server" Enabled="True"><%#Right("00000" + DataBinder.Eval(Container, "Paging.PageCount").ToString(), 4)%></asp:Label>
                                                                                                    <asp:LinkButton ID="btnIr" runat="server" CssClass="pagLink" CommandName="IrPagina">Ir</asp:LinkButton><span
                                                                                                        class="paglbl">&nbsp;|&nbsp;</span>
                                                                                                    <asp:LinkButton ID="btnProxima" runat="server" CssClass="pagLink" CausesValidation="False"
                                                                                                        CommandName="Page" CommandArgument="Next" Enabled="True">Próxima</asp:LinkButton>
                                                                                                    <asp:ImageButton runat="server" ID="imgProxima" CssClass="pagImg" CausesValidation="false"
                                                                                                        CommandArgument="Next" CommandName="Page" ImageUrl="~/imagens/icones/ico_setadir2.gif" />
                                                                                                    <asp:LinkButton ID="btnUltima" runat="server" CssClass="pagLink" CausesValidation="False"
                                                                                                        CommandName="Page" CommandArgument="Last" Enabled="True">Última</asp:LinkButton>
                                                                                                    <asp:ImageButton runat="server" ID="imgUltima" CssClass="pagImg" CausesValidation="false"
                                                                                                        CommandArgument="Last" CommandName="Page" ImageUrl="~/imagens/icones/ico_setadir.gif" />
                                                                                                </asp:Panel>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </PagerTemplate>
                                                                                <Columns>
                                                                                    <radG:GridBoundColumn HeaderText="Usuário" SortExpression="USU_C_NOME" DataField="USU_C_NOME"
                                                                                        UniqueName="USU_C_NOME">
                                                                                        <ItemStyle Width="50%"></ItemStyle>
                                                                                    </radG:GridBoundColumn>
                                                                                    <radG:GridBoundColumn HeaderText="Email" SortExpression="USU_C_EMAIL" DataField="USU_C_EMAIL"
                                                                                        UniqueName="USU_C_EMAIL">
                                                                                        <ItemStyle Width="35%" HorizontalAlign="Center"></ItemStyle>
                                                                                    </radG:GridBoundColumn>
                                                                                    <radG:GridBoundColumn HeaderText="Status" SortExpression="USU_N_STATUS" DataField="USU_N_STATUS"
                                                                                        UniqueName="USU_N_STATUS">
                                                                                        <ItemStyle Width="10%" HorizontalAlign="Center"></ItemStyle>
                                                                                    </radG:GridBoundColumn>
                                                                                    <radG:GridTemplateColumn UniqueName="TemplateColumn" Visible="false">
                                                                                        <ItemStyle Width="5%" HorizontalAlign="Center"></ItemStyle>
                                                                                        <ItemTemplate>
                                                                                            <asp:ImageButton ID="btnEditar" AlternateText="Editar" runat="server" CausesValidation="False"
                                                                                                CommandArgument='<%# DataBinder.Eval(Container.DataItem, "USU_N_CODIGO") %>'
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
                                                                        </radG:RadGrid>
                                                                    </td>
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
                                                <asp:Label ID="lblTitulo" runat="server">Cadastro de Usuários</asp:Label>
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
                                                            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                <tr>
                                                                    <td class="cadMsg" colspan="2">
                                                                        <asp:Label ID="lblMensagemCadastro" runat="server">Formulário de Cadastro</asp:Label>
                                                                        <asp:ValidationSummary ID="Validationsummary1" runat="server" DisplayMode="List">
                                                                        </asp:ValidationSummary>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <table id="dtgItemStyle" cellspacing="2" cellpadding="2" width="100%" align="center"
                                                                border="0">
                                                                <tr id="trIdioma" runat="server">
                                                                    <td class="cadlbl" width="25%">
                                                                        <asp:Label ID="lblRegionalCad0" runat="server"><u>R</u>egional:</asp:Label>
                                                                        <br />
                                                                        <asp:DropDownList ID="ddlRegionalCad" runat="server" AccessKey="T" 
                                                                            AutoPostBack="True" CssClass="cadddl">
                                                                            <asp:ListItem Value="0">Ativo</asp:ListItem>
                                                                            <asp:ListItem Value="1">Bloqueado</asp:ListItem>
                                                                            <asp:ListItem Value="2">Inativo</asp:ListItem>
                                                                            <asp:ListItem Value="3">Novo</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="rfvRegional" runat="server" 
                                                                            ControlToValidate="ddlRegionalCad" CssClass="nomecampos" Display="Dynamic" 
                                                                            ErrorMessage="- Seleção Obrigatório: Regional" InitialValue="-1" 
                                                                            ValidationGroup="Salvar">*</asp:RequiredFieldValidator>
                                                                    </td>
                                                                    <td class="cadlbl">
                                                                        <asp:Label ID="lblFilialCad" runat="server"><u>F</u>ilial:</asp:Label>
                                                                        <br />
                                                                        <asp:DropDownList ID="ddlFilialCad" runat="server" AccessKey="F" 
                                                                            AutoPostBack="True" CssClass="cadddl">
                                                                        </asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="rfvFilial0" runat="server" 
                                                                            ControlToValidate="ddlFilialCad" CssClass="nomecampos" Display="Dynamic" 
                                                                            ErrorMessage="- Seleção Obrigatório: Filial" InitialValue="-1" 
                                                                            ValidationGroup="Salvar">*</asp:RequiredFieldValidator>
                                                                    </td>
                                                                    <td class="cadlbl" width="50%">
                                                                        <asp:Label ID="lblArea" runat="server"><u>Á</u>rea:</asp:Label>
                                                                        <br />
                                                                        <asp:DropDownList ID="ddlArea" runat="server" AccessKey="A" CssClass="cadddl" 
                                                                            Enabled="False">
                                                                        </asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="rfvArea" runat="server" 
                                                                            ControlToValidate="ddlArea" CssClass="nomecampos" Display="Dynamic" 
                                                                            ErrorMessage="- Seleção Obrigatório: Área" InitialValue="-1" 
                                                                            ValidationGroup="Salvar">*</asp:RequiredFieldValidator>
                                                                        <br>
                                                                        </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="cadlbl" colspan="2">
                                                                        <asp:Label ID="lblNome" runat="server"><u>N</u>ome:</asp:Label>
                                                                        <asp:HiddenField ID="HiddenCodigo" runat="server" />
                                                                        <asp:HiddenField ID="HiddenNome" runat="server" />
                                                                        <br>
                                                                        <asp:TextBox ID="txtNome" AccessKey="N" runat="server" CssClass="cadtxt" MaxLength="50"
                                                                            Width="300px" Enabled="False"></asp:TextBox>
                                                                        <asp:ImageButton ID="btnListarVisitado" runat="server" ImageUrl="%%PATH%%/Imagens/icones/ico_Visualizar.gif"
                                                                            AlternateText="Buscar" />
                                                                        <asp:RequiredFieldValidator ID="rfvNome" runat="server" CssClass="nomecampos" ControlToValidate="txtNome"
                                                                            ErrorMessage="- Campo Obrigatório: Nome" Display="Dynamic" ValidationGroup="Salvar">*</asp:RequiredFieldValidator>
                                                                    </td>
                                                                    <td class="cadlbl">
                                                                        <asp:Label ID="lblEmail" runat="server"><u>E</u>mail:</asp:Label><br>
                                                                        <asp:TextBox ID="txtEmail" AccessKey="E" runat="server" CssClass="cadtxt" MaxLength="50"
                                                                            Width="300px"></asp:TextBox><asp:RegularExpressionValidator ID="revEmail" runat="server"
                                                                                CssClass="nomecampos" ControlToValidate="txtEmail" ErrorMessage="- O formato do e-mail está incorreto."
                                                                                Display="Dynamic" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                                                ValidationGroup="Salvar">*</asp:RegularExpressionValidator><asp:RequiredFieldValidator
                                                                                    ID="rfvEmail" runat="server" CssClass="nomecampos" ControlToValidate="txtEmail"
                                                                                    ErrorMessage="- Campo Obrigatório: Email" Display="Dynamic" ValidationGroup="Salvar">*</asp:RequiredFieldValidator>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="cadlbl" id="tdLogin" runat="server" colspan="2">
                                                                        <asp:Label ID="lblLogin" runat="server"><u>L</u>ogin:</asp:Label><br>
                                                                        <asp:TextBox ID="txtLogin" AccessKey="L" runat="server" CssClass="cadtxt" MaxLength="50"
                                                                            Width="300px"></asp:TextBox>
                                                                        <asp:RegularExpressionValidator ID="revLogin" runat="server" CssClass="nomecampos"
                                                                            ControlToValidate="txtEmail" ErrorMessage="- O formato do e-mail do Login está incorreto."
                                                                            Display="Dynamic" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                                            EnableClientScript="True" ValidationGroup="Salvar">*</asp:RegularExpressionValidator><asp:RequiredFieldValidator
                                                                                ID="rfvLogin" runat="server" CssClass="nomecampos" ControlToValidate="txtLogin"
                                                                                ErrorMessage="- Campo Obrigatório: Login" Display="Dynamic" ValidationGroup="Salvar">*</asp:RequiredFieldValidator>
                                                                    </td>
                                                                    <td class="cadlbl">
                                                                        <asp:Label ID="lblWinLogon" runat="server"><u>W</u>inLogon:</asp:Label><br>
                                                                        <asp:TextBox ID="txtWinLogon" AccessKey="W" runat="server" CssClass="cadtxt" MaxLength="100"
                                                                            Width="300px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr id="trStatus" runat="server">
                                                                    <td class="cadlbl" width="25%">
                                                                        <asp:Label ID="lblIdioma" runat="server"><u>I</u>dioma:</asp:Label><br>
                                                                        <asp:DropDownList ID="ddlIdioma" AccessKey="I" runat="server" CssClass="cadddl">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td class="cadlbl">
                                                                        <asp:Label ID="lblStatus" runat="server">S<u>t</u>atus:</asp:Label><br>
                                                                        <asp:DropDownList ID="ddlStatus" AccessKey="T" runat="server" CssClass="cadddl">
                                                                            <asp:ListItem Value="0">Ativo</asp:ListItem>
                                                                            <asp:ListItem Value="1">Bloqueado</asp:ListItem>
                                                                            <asp:ListItem Value="2">Inativo</asp:ListItem>
                                                                            <asp:ListItem Value="3">Novo</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="rfvStatus" runat="server" CssClass="nomecampos" ControlToValidate="ddlStatus"
                                                                            ErrorMessage="- Seleção Obrigatório: Status" Display="Dynamic" InitialValue="-1"
                                                                            ValidationGroup="Salvar">*</asp:RequiredFieldValidator>
                                                                    </td>
                                                                    <td class="cadlbl" width="50%">
                                                                        <br>
                                                                        </td>
                                                                </tr>
                                                            </table>
                                                            <table id="Table11" cellspacing="2" cellpadding="2" border="0">
                                                                <tr>
                                                                    <td class="cadlbl">
                                                                        <asp:Label ID="lblGrupos" runat="server">Grupos:</asp:Label>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                    <td class="cadlbl">
                                                                        <asp:Label ID="lblGruposSelecionados" runat="server">Grupos Selecionados:</asp:Label><asp:RequiredFieldValidator
                                                                            ID="rfvlstGruposAdicionados" runat="server" CssClass="nomecampos" ControlToValidate="lstGruposAdicionados"
                                                                            ErrorMessage="- Seleção Obrigatória: Grupos Selecionados" Display="Dynamic" InitialValue="0"
                                                                            ValidationGroup="Salvar">*</asp:RequiredFieldValidator>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:ListBox ID="lstGruposRemovidos" runat="server" CssClass="cadlstBox" Width="150px"
                                                                            Rows="8" SelectionMode="Multiple"></asp:ListBox>
                                                                    </td>
                                                                    <td align="center">
                                                                        &nbsp;&nbsp;
                                                                        <asp:Button ID="cmdGruAdiciona" runat="server" CssClass="cadbutton" Width="30px"
                                                                            Text=">>" CausesValidation="False"></asp:Button>&nbsp;&nbsp;<br>
                                                                        <br />
                                                                        &nbsp;&nbsp;
                                                                        <asp:Button ID="cmdGruRemove" runat="server" CssClass="cadbutton" Width="30px" Text="<<"
                                                                            CausesValidation="False"></asp:Button>&nbsp;&nbsp;
                                                                    </td>
                                                                    <td>
                                                                        <asp:ListBox ID="lstGruposAdicionados" runat="server" CssClass="cadlstBox" Width="150px"
                                                                            Rows="8" SelectionMode="Multiple"></asp:ListBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <table id="tblSenha" cellspacing="2" cellpadding="2" width="100%" border="0" runat="server">
                                                                <tr>
                                                                    <td class="cadlbl" style="width: 155px">
                                                                        <asp:Label ID="lblSenha" runat="server"><u>S</u>enha:</asp:Label><br>
                                                                        <asp:TextBox ID="txtSenha" AccessKey="S" runat="server" CssClass="cadtxt" MaxLength="50"
                                                                            Width="150px" TextMode="Password"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="rfvSenha" runat="server" CssClass="nomecampos" ControlToValidate="txtSenha"
                                                                            ErrorMessage="- Campo Obrigatório: Senha " Display="Dynamic" ValidationGroup="Salvar">*</asp:RequiredFieldValidator>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="cadlbl">
                                                                        <asp:Label ID="lblSenhaConfirmacao" runat="server">Senha <u>C</u>onfirmação:</asp:Label><br>
                                                                        <asp:TextBox ID="txtSenhaConfirmacao" AccessKey="C" runat="server" CssClass="cadtxt"
                                                                            MaxLength="50" Width="150px" TextMode="Password"></asp:TextBox><asp:RequiredFieldValidator
                                                                                ID="rfvConfirmacaoSenha" runat="server" CssClass="nomecampos" ControlToValidate="txtSenhaConfirmacao"
                                                                                ErrorMessage="- Campo Obrigatório: Senha Confirmação" Display="Dynamic" ValidationGroup="Salvar">*</asp:RequiredFieldValidator><asp:CompareValidator
                                                                                    ID="cpvSenha" runat="server" CssClass="nomecampos" ControlToValidate="txtSenhaConfirmacao"
                                                                                    ErrorMessage="A senha não confere!!!" Display="Dynamic" ControlToCompare="txtSenha"
                                                                                    ValidationGroup="Salvar">*</asp:CompareValidator>
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
                                                                ValidationGroup="Salvar"></asp:Button>&nbsp;
                                                            <asp:Button ID="btnNovoCadastro" runat="server" CssClass="cadbutton100" Text="Novo"
                                                                CausesValidation="False"></asp:Button>&nbsp;
                                                            <asp:Button ID="btnVoltarCadastro" runat="server" CssClass="cadbutton100" Text="Voltar"
                                                                CausesValidation="False"></asp:Button>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                    <!-- cccccccccccc END: Conteudo  ccccccccccc -->
                                </asp:Panel>
                                <!-- cccccccccccc END: Panel Cadastro ccccccccccc-->
                            </rada:RadAjaxPanel>
                            <rada:AjaxLoadingPanel ID="lpaCadastro" Width="75px" runat="server" HorizontalAlign="Center"
                                Transparency="30" Height="75px">
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
                <cc1:FWServerControl ID="Fwservercontrol2" runat="server" Controle="RodapeAdmin">
                </cc1:FWServerControl>
            </td>
        </tr>
        <!-- cccccccccccc END: Rodape  ccccccccccc -->
    </table>
    </form>
</body>
</html>
