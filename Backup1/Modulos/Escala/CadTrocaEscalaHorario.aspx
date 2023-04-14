﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CadTrocaEscalaHorario.aspx.cs"
    Inherits="SafWeb.UI.Modulos.Escala.CadTrocaEscalaHorario" %>

<%@ Register TagPrefix="rada" Namespace="Telerik.WebControls" Assembly="RadAjax.Net2" %>
<%@ Register TagPrefix="cc1" Namespace="FrameWork.WebControl" Assembly="FrameWork.WebControl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>%%SITE%%</title>
    <link href="../../Estilos/FrameWork.css" type="text/css" rel="stylesheet" />
    <link href="../../Estilos/Brinks.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .dtgHeaderStyle
        {
            text-align: left;
            height: 36px;
        }
        
        
        .dtgItemStyleAlternate, .dtgItemStyle
        {
            height: 26px;
            padding: 0 2px;
        }
        
        .cellRep
        {
            /*border:1px solid red;*/
            display: inline-block;
            overflow: hidden;
            white-space: nowrap;
            height: 26px;
        }
        
        .dtgHeaderStyle .cellRep
        {
            height: 34px;
        }
        
        .cellColaborador
        {
            width: 219px; 
        }
        
        .cellData
        {
            width: 90px;
        }
        
        .cellHorario
        {
            width: 105px;
        }
        
        .cellFolga
        {
            text-align: center;
            width: 36px;
        }
        
        .cellBorderLeft
        {
            border-left: 1px solid red;
        }
        
        .cellBorderRight
        {
            border-right: 1px solid red;
        }
        
        .cellPagarFolga
        {
            height: 100%;
            text-align: center;
            width: 141px;
        }
        
        .cellRep .cadddl
        {
            width: 90%;
        }
        
        /*.cellPagarFolga .cadddl
        {
              width: 99%;  
        }*/
    </style>
</head>
<body>
    <form id="form1" runat="server">
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
            <!-- ****************** START: PAINEL CADASTRO *********************** -->
            <td class="backbox" valign="top" height="250" style="width: 1010px">
                <asp:Panel ID="pnlCadastro" runat="server">
                    <!-- ***************** START: BARRA DE TITUTO ******************* -->
                    <table class="MarginTopBarra" cellspacing="0" cellpadding="0" width="760" align="center"
                        border="0">
                        <tbody>
                            <tr>
                                <td style="width: 750px; height: 25px" class="cadBarraTitulo" align="left">
                                    <img src="../../Imagens/comum/bulletbarra.gif" align="absMiddle" />
                                    <asp:Label ID="lblTituloCad" runat="server" Text="Lançamento de Troca de Horário"></asp:Label>
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
                                                                <asp:Label ID="lblEscalaDepartamental" runat="server" Text="Escala Departamental:"></asp:Label>
                                                            </td>
                                                            <td class="cadlbl" width="240">
                                                                <asp:Label ID="lblRegional" runat="server" Text="Regional:"></asp:Label>
                                                            </td>
                                                            <td class="cadlbl" width="240">
                                                                <asp:Label ID="lblFilial" runat="server" Text="Filial:"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="240" style="height: 19px">
                                                                <asp:DropDownList ID="ddlEscalaDepartamental" runat="server" CssClass="cadddl" AutoPostBack="True"
                                                                    Width="233px" OnSelectedIndexChanged="ddlEscalaDepartamental_SelectedIndexChanged">
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
                                                            <td class="cadlbl" colspan="3">
                                                                <asp:Label ID="lblPeríodo" runat="server" Text="Período:"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3" style="height: 19px">
                                                                <asp:DropDownList ID="ddlPeriodo" runat="server" CssClass="cadddl" Width="245px"
                                                                    OnSelectedIndexChanged="ddlPeriodo_SelectedIndexChanged" AutoPostBack="true">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvPeriodo" runat="server" ControlToValidate="ddlPeriodo"
                                                                    ErrorMessage="Campo Obrigatório: Período." InitialValue="0" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="cadlbl" colspan="3" style="padding-top: 10px;">
                                                                <asp:DataList ID="dtlEscala" runat="server" OnItemDataBound="dtlEscala_ItemDataBound">
                                                                    <HeaderTemplate>
                                                                        <div class="dtgHeaderStyle">
                                                                            <span class="cellRep cellColaborador">
                                                                                Colaborador
                                                                            </span><span class="cellRep cellData">
                                                                                Data
                                                                            </span><span class="cellRep cellHorario">
                                                                                Horário
                                                                            </span><span class="cellRep cellFolga">
                                                                                Hora<br />Extra
                                                                            </span><span class="cellRep cellFolga">
                                                                                Folga
                                                                            </span><span class="cellRep cellPagarFolga">
                                                                                Pagar Folga
                                                                            </span><span class="cellRep cellHorario">
                                                                                Horário
                                                                            </span>
                                                                        </div>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <div class="dtgItemStyleAlternate listaRow">
                                                                            <span class="cellRep cellColaborador">
                                                                                <%# DataBinder.Eval(Container.DataItem, "Nome")%>
                                                                                <br />
                                                                                <%# DataBinder.Eval(Container.DataItem, "CodigoColaborador")%>
                                                                            </span><span class="cellRep cellData">
                                                                                <%# DataBinder.Eval(Container.DataItem, "dtData")%>
                                                                            </span><span class="cellRep cellHorario">
                                                                                <asp:DropDownList ID="ddlHorario" DataSource='<%# GetEscalaHorariosByJornada(Container.DataItem) %>'
                                                                                    DataTextField="IdHorario" DataValueField="IdEscala" runat="server" OnSelectedIndexChanged="ddlHorario_SelectedIndexChanged"
                                                                                    AutoPostBack="true" class="cadddl" Enabled='<%# !Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "blnFolga")) %>'>
                                                                                </asp:DropDownList>
                                                                            </span><span class="cellRep cellFolga">
                                                                                <asp:CheckBox ID="chkHExtra" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "blnHExtra")%>'
                                                                                    OnCheckedChanged="chkHExtra_CheckedChanged" AutoPostBack="true" Enabled='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "idHorario"))>0 ? true : false %>' />
                                                                            </span><span class="cellRep cellFolga">
                                                                                <asp:CheckBox ID="chkFolga" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "blnFolga")%>'
                                                                                    OnCheckedChanged="chkFolga_CheckedChanged" AutoPostBack="true" Enabled='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "idHorario"))>0 ? false : true %>' />
                                                                            </span><span class="cellRep cellPagarFolga">
                                                                                <asp:DropDownList ID="ddlDataPagarFolga" 
                                                                                    DataSource='<%# GetDatasPagarFolgaByColaboradores(Container.DataItem) %>'
                                                                                    DataTextField="Text" 
                                                                                    DataValueField="Value" 
                                                                                    runat="server" 
                                                                                    OnSelectedIndexChanged="ddlDataPagarFolga_SelectedIndexChanged"
                                                                                    AutoPostBack="true" class="cadddl" Enabled='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "blnFolga")) %>'>
                                                                                </asp:DropDownList>
                                                                            </span><span class="cellRep cellHorario">
                                                                                <asp:DropDownList ID="ddlHorarioPFolga" DataSource='<%# GetEscalaHorariosByJornada(Container.DataItem) %>'
                                                                                    DataTextField="IdHorario" DataValueField="IdEscala" runat="server" OnSelectedIndexChanged="ddlHorario_SelectedIndexChanged"
                                                                                    AutoPostBack="true" class="cadddl" Enabled='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "blnFolga")) %>'>
                                                                                </asp:DropDownList>
                                                                            </span>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                    <AlternatingItemTemplate>
                                                                        <div class="dtgItemStyle listaRow">
                                                                            <span class="cellRep cellColaborador">
                                                                                <%# DataBinder.Eval(Container.DataItem, "Nome")%>
                                                                                <br />
                                                                                <%# DataBinder.Eval(Container.DataItem, "CodigoColaborador")%>
                                                                            </span><span class="cellRep cellData">
                                                                                <%# DataBinder.Eval(Container.DataItem, "dtData")%>
                                                                            </span><span class="cellRep cellHorario">
                                                                                <asp:DropDownList ID="ddlHorario" DataSource='<%# GetEscalaHorariosByJornada(Container.DataItem) %>'
                                                                                    DataTextField="IdHorario" DataValueField="IdEscala" runat="server" OnSelectedIndexChanged="ddlHorario_SelectedIndexChanged"
                                                                                    AutoPostBack="true" class="cadddl" Enabled='<%# !Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "blnFolga")) %>'>
                                                                                </asp:DropDownList>
                                                                            </span><span class="cellRep cellFolga">
                                                                                <asp:CheckBox ID="chkHExtra" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "blnHExtra")%>'
                                                                                    OnCheckedChanged="chkHExtra_CheckedChanged" AutoPostBack="true" Enabled='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "idHorario"))>0 ? true : false %>' />
                                                                            </span><span class="cellRep cellFolga">
                                                                                <asp:CheckBox ID="chkFolga" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "blnFolga")%>'
                                                                                    OnCheckedChanged="chkFolga_CheckedChanged" AutoPostBack="true" Enabled='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "idHorario"))>0 ? false : true %>' />
                                                                            </span><span class="cellRep cellPagarFolga">
                                                                                <asp:DropDownList ID="ddlDataPagarFolga" 
                                                                                    DataSource='<%# GetDatasPagarFolgaByColaboradores(Container.DataItem) %>'
                                                                                    DataTextField="Text" 
                                                                                    DataValueField="Value" 
                                                                                    runat="server" OnSelectedIndexChanged="ddlDataPagarFolga_SelectedIndexChanged"
                                                                                    AutoPostBack="true" class="cadddl" Enabled='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "blnFolga")) %>'>
                                                                                </asp:DropDownList>
                                                                            </span><span class="cellRep cellHorario">
                                                                                <asp:DropDownList ID="ddlHorarioPFolga" DataSource='<%# GetEscalaHorariosByJornada(Container.DataItem) %>'
                                                                                    DataTextField="IdHorario" DataValueField="IdEscala" runat="server" OnSelectedIndexChanged="ddlHorario_SelectedIndexChanged"
                                                                                    AutoPostBack="true" class="cadddl" Enabled='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "blnFolga")) %>'>
                                                                                </asp:DropDownList>
                                                                            </span>
                                                                        </div>
                                                                    </AlternatingItemTemplate>
                                                                </asp:DataList>

                                                                <asp:Repeater ID="repeaterPager" runat="server" OnItemCommand="repeaterPager_ItemCommand">
                                                                    <ItemTemplate>
                                                                        <table align="center" border="0" cellpadding="5" cellspacing="0" class="pag" height="15"
                                                                            width="100%">
                                                                            <tr>
                                                                                <td align="center" valign="center">
                                                                                    <asp:Panel ID="pnlPaginaAtual" runat="server" DefaultButton="btnIr">
                                                                                        <asp:ImageButton ID="imgPrimeira" runat="server" CausesValidation="false" CommandArgument="First"
                                                                                            CommandName="Page" CssClass="pagImg" ImageUrl="~/imagens/icones/ico_setaesq.gif" />
                                                                                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandArgument="First"
                                                                                            CommandName="Page" CssClass="pagLink" Enabled="True">Primeira</asp:LinkButton>
                                                                                        <asp:ImageButton ID="imgAnterior" runat="server" CausesValidation="false" CommandArgument="Prev"
                                                                                            CommandName="Page" CssClass="pagImg" ImageUrl="~/imagens/icones/ico_setaesq2.gif" />
                                                                                        <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandArgument="Prev"
                                                                                            CommandName="Page" CssClass="pagLink" Enabled="True">Anterior</asp:LinkButton>&#160;&#160;
                                                                                        <asp:Label ID="Label3" runat="server" CssClass="paglbl">| Página:</asp:Label>
                                                                                        <cc1:FWMascara ID="FWMascara2" runat="server" AutoPostBack="False" CssClass="pagtxtbox"
                                                                                            Mascara="NUMERO" MaxLength="4" Text='<%# (int)(DataBinder.Eval(Container.DataItem, "CurrentPageIndex")) + 1 %>'
                                                                                            Width="50px"></cc1:FWMascara>
                                                                                        <asp:Label ID="Label4" runat="server" CssClass="paglbl">de</asp:Label>
                                                                                        <asp:Label ID="Label6" runat="server" CssClass="paglbl" Enabled="True"><%# DataBinder.Eval(Container.DataItem, "PageCount").ToString()%></asp:Label>
                                                                                        <asp:LinkButton ID="btnIr" runat="server" CausesValidation="False" CommandArgument="IrPagina"
                                                                                            CommandName="Page" CssClass="pagLink">Ir</asp:LinkButton>
                                                                                        <span class="paglbl">&#160;|&#160;</span>
                                                                                        <asp:LinkButton ID="LinkButton3" runat="server" CausesValidation="False" CommandArgument="Next"
                                                                                            CommandName="Page" CssClass="pagLink" Enabled="True">Próxima</asp:LinkButton>
                                                                                        <asp:ImageButton ID="imgProxima" runat="server" CausesValidation="false" CommandArgument="Next"
                                                                                            CommandName="Page" CssClass="pagImg" ImageUrl="~/imagens/icones/ico_setadir2.gif" />
                                                                                        <asp:LinkButton ID="LinkButton4" runat="server" CausesValidation="False" CommandArgument="Last"
                                                                                            CommandName="Page" CssClass="pagLink" Enabled="True">Última</asp:LinkButton>
                                                                                        <asp:ImageButton ID="imgUltima" runat="server" CausesValidation="false" CommandArgument="Last"
                                                                                            CommandName="Page" CssClass="pagImg" ImageUrl="~/imagens/icones/ico_setadir.gif" />
                                                                                    </asp:Panel>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </ItemTemplate>
                                                                </asp:Repeater>
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
                                                    &nbsp;<asp:Button ID="btnVoltar" OnClick="btnVoltar_Click" runat="server" Text="Voltar"
                                                        CssClass="cadbutton100" Width="50px" CausesValidation="False"></asp:Button>
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
                    <rada:AjaxLoadingPanel ID="lpaCadastro" runat="server" Width="75px" Height="75px"
                        Transparency="30" HorizontalAlign="Center">
                        <asp:Image ID="Image1" Style="margin-top: 100px" runat="server" ImageUrl="%%PATH%%/RadControls/Ajax/Skins/Default/Loading.gif"
                            AlternateText="Aguarde ..."></asp:Image>
                    </rada:AjaxLoadingPanel>
                </asp:Panel>
            </td>
            <!-- ********************* END: PAINEL CADASTRO ************************** -->
            <!-- ********************* END: CONTEÚDO ****************** -->
        </tr>
        <tr>
            <td style="width: 778px">
                <!-- ********************* START: RODAPÉ ************************** -->
                <cc1:FWServerControl ID="FWServerControlRodape" runat="server" Controle="Rodape"
                    Arquivo="/Modulos/Framework/RodapeBrinks.ascx"></cc1:FWServerControl>
                <!-- ********************* END: RODAPÉ ************************** -->
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
