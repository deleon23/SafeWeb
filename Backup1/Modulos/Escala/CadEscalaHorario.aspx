<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CadEscalaHorario.aspx.cs"
    Inherits="SafWeb.UI.Modulos.Escala.CadEscalaHorario" %>

<%@ Register TagPrefix="rada" Namespace="Telerik.WebControls" Assembly="RadAjax.Net2" %>
<%@ Register TagPrefix="cc1" Namespace="FrameWork.WebControl" Assembly="FrameWork.WebControl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <!--Compatibilidade entre IE8 e JSON. Descomentar tag abaixo se estiver com erro "JSON não está definido" -->
    <meta http-equiv="X-UA-Compatible" content="IE=EDGE" />
    <title>%%SITE%%</title>
    <link href="../../Estilos/FrameWork.css" type="text/css" rel="stylesheet" />
    <link href="../../Estilos/Brinks.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../Scripts/jquery-1.7.2.min.js"></script>
    <%--<script src="../../Scripts/jquery-1.7.2-vsdoc.js" type="text/javascript"></script>--%>
    <style type="text/css">
        .dtgHeaderStyle
        {
            text-align: left;
            height: 36px;
        }
        
        .dtgItemStyleAlternate, .dtgItemStyle
        {
            height: 26px;
        }
        
        .cellRep
        {
            display: inline-block;
            overflow: hidden;
            white-space: nowrap;
            height: 26px;
        }
        
        .dtgHeaderStyle .cellRep
        {
            height: 38px;
        }
        
        .cellColaborador
        {
            width: 263px;
        }
        
        .cellJornada
        {
            width: 82px;
        }
        
        .cellFolgando
        {
            width: 36px;
        }
        
        .dtgHeaderStyle .cellFolgando
        {
            margin-left: -4px;
            padding-right: 4px;
        }
        
        .cellHorario
        {
            width: 126px;
        }
        
        .cellSabado
        {
            width: 105px;
        }
        
        .cellDomingo
        {
            width: 105px;
        }
        
        .cellRep .cadddl
        {
            width: 90%;
        }
    </style>
    <script language="javascript" type="text/javascript">

        var jsonColaboradores = new Object();
        var jsonListEscalaHorarios = new Object();

        function PreencherHiddenColaboradores(colaboradores) {
            document.getElementById("txtHiddenColaboradores").value = colaboradores;

            AjaxNS.AR('Colaboradores', '', 'RadAjaxPanelCadastro');
        }

        function confirmaExclusao() {

            if (confirm("Confirma a exclusão do colaborador da escala departamental?")) {
                return true;
            } else {
                return false;
            }

        }
        //--

        $(document).ready(function () {
            SetEventos();
        });

        function OnResponseEnd(x, y) {
            SetEventos();
        }

        function toggleComboCompensacao(Container, idJornada) {
            $("select[id$=ddlListCompensacao]", Container)
                .attr("disabled", true)
                .val(0);

            $("select[id$=ddlHorarioCompensacao]", Container)
                .attr("disabled", true)
                .val(0);

            $("input:checkbox[id$=chkFolgando]", Container)
                .attr("disabled", true)
                .attr("checked", false);

            switch (parseInt(idJornada)) {
                case 1:
                    $("select[id$=ddlListCompensacao]", Container).removeAttr("disabled");
                    $("select[id$=ddlHorarioCompensacao]", Container).removeAttr("disabled");
                    break;
                case 3:
                    $("input:checkbox[id$=chkFolgando]", Container).attr("disabled", false).removeAttr("disabled");
                    break;
            }

        }

        function AtualizaHidden() {
            if (jsonColaboradores.length > 0)
                $("#hdfColaboradores").val(JSON.stringify(jsonColaboradores));

            if (jsonListEscalaHorarios.length > 0)
                $("#hdfListEscalaHorarios").val(JSON.stringify(jsonListEscalaHorarios));
        }

        function AtualizaListas() {
            if ($("#hdfColaboradores").val() != "")
                jsonColaboradores = JSON.parse($("#hdfColaboradores").val());

            if ($("#hdfListEscalaHorarios").val() != "")
                jsonListEscalaHorarios = JSON.parse($("#hdfListEscalaHorarios").val());
        }

        function SetEventos() {

            AtualizaListas();

            $("input:checkbox[id$=chkFolgando]").closest('span').removeAttr('disabled');
            //$("input:checkbox[id$=chkFolgando]").attr("disabled", true);

            $("input[id$=imgbRemover]").focusin(function () {

                var inputs = $(this).closest('#dtlEscala').find(':input');
                inputs.eq(inputs.index(this) + 1).focus();

            });

            $("select[id$=ddlHorarioCompensacao]").change(function (e) {
                var divContainer = $(this).parents("div[id^=linha]").first();
                var idHorarioCompensacao = $(this).val();

                //Para o combo do header
                if (divContainer.attr("id") == "linha0000") {
                    $("select[id$=ddlHorarioCompensacao]").each(function (key, objCombo) {
                        if ($(objCombo).parents("div[id^=linha]").first().attr("id") != "linha0000") {
                            $(objCombo).val(idHorarioCompensacao);
                            //$("#" + $(objCombo).attr("id")).trigger("change");
                        }
                    });

                    //Seta os valores para todos os colaboradores

                    $.each(jsonColaboradores, function (key, colaborador) {
                        colaborador.idHorarioCompensacao = parseInt(idHorarioCompensacao);
                    });

                } else {
                    //Atualiza dados da lista de colaboradores
                    var codigoColaborador = divContainer.attr("id").replace("linha", "");

                    $.each(jsonColaboradores, function (key, colaborador) {
                        if (colaborador.CodigoColaborador == codigoColaborador) {
                            colaborador.idHorarioCompensacao = parseInt(idHorarioCompensacao);
                            //Abandona o each
                            return false;
                        }
                    });
                }

                AtualizaHidden();
            });


            $("select[id$=ddlListCompensacao]").change(function (e) {
                var divContainer = $(this).parents("div[id^=linha]").first();
                var idCompensacao = $(this).val();

                //Para o combo do header
                if (divContainer.attr("id") == "linha0000") {
                    $("select[id$=ddlListCompensacao]").each(function (key, objCombo) {
                        if ($(objCombo).parents("div[id^=linha]").first().attr("id") != "linha0000") {
                            $(objCombo).val(idCompensacao);
                        }
                    });

                    //Seta os valores para todos os colaboradores
                    $.each(jsonColaboradores, function (key, colaborador) {
                        colaborador.idCompensacao = parseInt(idCompensacao);
                    });

                } else {
                    //Atualiza dados da lista de colaboradores
                    var codigoColaborador = divContainer.attr("id").replace("linha", "");

                    $.each(jsonColaboradores, function (key, colaborador) {
                        if (colaborador.CodigoColaborador == codigoColaborador) {
                            colaborador.idCompensacao = parseInt(idCompensacao);
                            //Abandona o each
                            return false;
                        }
                    });
                }

                AtualizaHidden();
            });


            $("select[id$=ddlHorario]").change(function (e) {
                var divContainer = $(this).parents("div[id^=linha]").first();
                var idHorario = $(this).val();

                //Para o combo do header
                if (divContainer.attr("id") == "linha0000") {
                    $("select[id$=ddlHorario]").each(function (key, objCombo) {
                        if ($(objCombo).parents("div[id^=linha]").first().attr("id") != "linha0000") {
                            $(objCombo).val(idHorario);
                        }
                    });

                    //Seta os valores para todos os colaboradores

                    $.each(jsonColaboradores, function (key, colaborador) {
                        colaborador.idHorario = parseInt(idHorario);
                    });

                } else {
                    //Atualiza dados da lista de colaboradores
                    var codigoColaborador = divContainer.attr("id").replace("linha", "");

                    $.each(jsonColaboradores, function (key, colaborador) {
                        if (colaborador.CodigoColaborador == codigoColaborador) {
                            colaborador.idHorario = parseInt(idHorario);
                            //Abandona o each
                            return false;
                        }
                    });
                }

                AtualizaHidden();
            });

            $("select[id$=ddlJornada]").change(function (e) {
                var divContainer = $(this).parents("div[id^=linha]").first();
                var idJornada = $(this).val();
                PopulaComboEscalaHorariosByJornada(idJornada, $("select[id$=ddlHorario]", divContainer));
                PopulaComboEscalaHorariosByJornada(idJornada, $("select[id$=ddlHorarioCompensacao]", divContainer));

                toggleComboCompensacao(divContainer, idJornada);

                var ddddd = divContainer.attr("id");
                //Para o combo do header
                if (divContainer.attr("id") == "linha0000") {
                    $("select[id$=ddlJornada]").each(function (key, objCombo) {
                        if ($(objCombo).parents("div[id^=linha]").first().attr("id") != "linha0000") {
                            if (idJornada == -1) {
                                if ($(objCombo).val() == 1)
                                    $(objCombo).val(2)
                                else if ($(objCombo).val() == 2)
                                    $(objCombo).val(1)
                            } else {
                                $(objCombo).val(idJornada);
                            }
                            //var ddd = "#" + $(objCombo).attr("id");
                            $("#" + $(objCombo).attr("id")).trigger("change");
                        }
                    });

                    $.each(jsonColaboradores, function (key, colaborador) {
                        colaborador.idJornada = parseInt(idJornada);
                    });

                } else {
                    //Atualiza dados da lista de colaboradores
                    var codigoColaborador = divContainer.attr("id").replace("linha", "");

                    $.each(jsonColaboradores, function (key, colaborador) {
                        if (colaborador.CodigoColaborador == codigoColaborador) {
                            colaborador.idJornada = parseInt(idJornada);
                            return false;
                        }
                    });
                }

                AtualizaHidden();
            });

            $("input:checkbox[id$=chkFolgando]").change(function (e) {
                var divContainer = $(this).parents("div[id^=linha]").first();
                //var idJornada = $(this).val();
                var FlgIniciaFolgando = $(this).is(":checked");

                //Atualiza dados da lista de colaboradores
                var codigoColaborador = divContainer.attr("id").replace("linha", "");

                $.each(jsonColaboradores, function (key, colaborador) {
                    if (colaborador.CodigoColaborador == codigoColaborador) {
                        colaborador.FlgIniciaFolgando = FlgIniciaFolgando; //($(this).is(":checked") ? 1 : 0);
                        //Abandona o each
                        return false;
                    }
                });

                AtualizaHidden();
            });
        }

        function PopulaComboEscalaHorariosByJornada(idJornada, objHorario) {
            if ($("#hdfListEscalaHorarios").val() != "") {

                $(objHorario).html("");

                var options = "";

                $.each(jsonListEscalaHorarios, function (key, value) {
                    if (value.IdJornada == idJornada || value.IdJornada == 0)
                        options += '<option value="' + value.IdEscala + '">' + value.IdHorario + '</option>';
                });

                $(objHorario).html(options);
            }
        }

    </script>
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
                                    <asp:Label ID="lblTituloCad" runat="server" Text="Lançamento de Escalas - Seleção de Escala e Colaboradores"></asp:Label>
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
                                        ClientEvents-OnRequestStart="OnRequestStart" ClientEvents-OnResponseEnd="OnResponseEnd">
                                        <!-- ****************** START: FORMULARIO *********************** -->
                                        <table class="MarginTopConteudo" cellspacing="0" cellpadding="0" width="740" align="center"
                                            border="0">
                                            <tr>
                                                <td class="cadmsg">
                                                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List"
                                                        CssClass="cadlbl" />
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
                                                                    AutoPostBack="true" OnSelectedIndexChanged="ddlPeriodo_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvPeriodo" runat="server" ControlToValidate="ddlPeriodo"
                                                                    ErrorMessage="Campo Obrigatório: Período." InitialValue="0" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="cadlbl" colspan="3" style="padding-top: 10px;">
                                                                <asp:HiddenField ID="hdfListEscalaHorarios" runat="server" />
                                                                <asp:HiddenField ID="hdfColaboradores" runat="server" />
                                                                <asp:DataList ID="dtlEscala" runat="server" OnItemDataBound="dtlEscala_ItemDataBound">
                                                                    <HeaderTemplate>
                                                                        <div class="dtgHeaderStyle" id='linha0000'>
                                                                            <span class="cellRep cellColaborador">Colaborador
                                                                                <asp:ImageButton ID="btnAdicionar" CssClass="BotaoAdicionar" runat="server" ImageUrl="~/Imagens/icones/ico_btn_adiconar.png"
                                                                                    OnClick="btnAdicionar_Click" CausesValidation="False" ToolTip="Adicionar Colaboradores" />
                                                                            </span><span class="cellRep cellJornada">Jornada
                                                                                <br />
                                                                                <asp:DropDownList ID="ddlJornada" DataSource='<%# GetHorarioEscala() %>' DataTextField="DescricaoJornada"
                                                                                    DataValueField="IdJornada" runat="server" AutoPostBack="false" class="cadddl">
                                                                                </asp:DropDownList>
                                                                            </span><span class="cellRep cellHorario">Horário
                                                                                <br />
                                                                                <asp:DropDownList ID="ddlHorario" DataTextField="IdHorario" DataValueField="IdEscala"
                                                                                    runat="server" class="cadddl" AutoPostBack="false">
                                                                                </asp:DropDownList>
                                                                            </span><span class="cellRep cellFolgando">Inicia<br />
                                                                                em<br />
                                                                                Folga </span><span class="cellRep cellSabado">Final de Semana
                                                                                <br />
                                                                                <asp:DropDownList ID="ddlListCompensacao" runat="server" class="cadddl" Enabled="false"
                                                                                        AutoPostBack="false">
                                                                                    <asp:ListItem Text="Selecione" Value="" />
                                                                                    <asp:ListItem Text="Sábado" Value="7" />
                                                                                    <asp:ListItem Text="Domingo" Value="1" />
                                                                                </asp:DropDownList>
                                                                            </span><span class="cellRep cellDomingo">Horário
                                                                                <br />
                                                                                <asp:DropDownList ID="ddlHorarioCompensacao" DataTextField="IdHorario" DataValueField="IdEscala"
                                                                                        runat="server" class="cadddl" Enabled="false" AutoPostBack="false">
                                                                                </asp:DropDownList>
                                                                            </span>
                                                                        </div>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <div class="dtgItemStyleAlternate" id='linha<%# DataBinder.Eval(Container.DataItem, "CodigoColaborador")%>'>
                                                                            <span class="cellRep cellColaborador">
                                                                                <%# DataBinder.Eval(Container.DataItem, "Nome")%>
                                                                                <br />
                                                                                <%# DataBinder.Eval(Container.DataItem, "CodigoColaborador")%></span><span class="cellRep cellJornada">
                                                                                    <asp:DropDownList ID="ddlJornada" DataSource='<%# GetHorarioEscala() %>' DataTextField="DescricaoJornada"
                                                                                        DataValueField="IdJornada" runat="server" AutoPostBack="false" class="cadddl">
                                                                                    </asp:DropDownList>
                                                                                    <%--<asp:RequiredFieldValidator ID="rfvPeriodo" runat="server" ControlToValidate="ddlJornada"
                                                                                        InitialValue="0" SetFocusOnError="True">*</asp:RequiredFieldValidator>--%>
                                                                                </span><span class="cellRep cellHorario">
                                                                                    <asp:DropDownList ID="ddlHorario" DataSource='<%# GetEscalaHorariosByJornada(Container.DataItem) %>'
                                                                                        DataTextField="IdHorario" DataValueField="IdEscala" runat="server" AutoPostBack="false"
                                                                                        class="cadddl">
                                                                                    </asp:DropDownList>
                                                                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlHorario"
                                                                                        InitialValue="0" SetFocusOnError="True">*</asp:RequiredFieldValidator>--%>
                                                                                </span><span class="cellRep cellFolgando">
                                                                                    <asp:CheckBox ID="chkFolgando" runat="server" Enabled='<%# (Convert.ToInt32(DataBinder.Eval(Container.DataItem, "idJornada"))==3) %>'
                                                                                        Checked='<%# DataBinder.Eval(Container.DataItem, "FlgIniciaFolgando")%>' />
                                                                                </span><span class="cellRep cellSabado">
                                                                                    <asp:DropDownList ID="ddlListCompensacao" runat="server" class="cadddl" Enabled="false"
                                                                                        AutoPostBack="false">
                                                                                        <asp:ListItem Text="Selecione" Value="0" />
                                                                                        <asp:ListItem Text="Sábado" Value="7" />
                                                                                        <asp:ListItem Text="Domingo" Value="1" />
                                                                                    </asp:DropDownList>
                                                                                    <%--<asp:RequiredFieldValidator ID="rfvListCompensacao" runat="server" ControlToValidate="ddlListCompensacao"
                                                                                        InitialValue="0" SetFocusOnError="True" ValidationGroup='<%# (Convert.ToInt32(DataBinder.Eval(Container.DataItem, "idJornada"))==1?"":"NV") %>'>*</asp:RequiredFieldValidator>--%>
                                                                                </span><span class="cellRep cellDomingo">
                                                                                    <asp:DropDownList ID="ddlHorarioCompensacao" DataSource='<%# GetEscalaHorariosByJornada(Container.DataItem) %>'
                                                                                        DataTextField="IdHorario" DataValueField="IdEscala" runat="server" AutoPostBack="false"
                                                                                        class="cadddl" Enabled="false">
                                                                                    </asp:DropDownList>
                                                                                    <%--<asp:RequiredFieldValidator ID="rfvHorarioCompensacao" runat="server" ControlToValidate="ddlHorarioCompensacao"
                                                                                        InitialValue="0" SetFocusOnError="True" ValidationGroup='<%# (Convert.ToInt32(DataBinder.Eval(Container.DataItem, "idJornada"))==1?"":"NV") %>'>*</asp:RequiredFieldValidator>--%>
                                                                                </span><span class="cellRep">
                                                                                    <asp:ImageButton ID="imgbRemover" runat="server" CausesValidation="false" ImageUrl="%%PATH%%/imagens/icones/ico_deletar.gif"
                                                                                        Text="Excluir" OnClick="imgbRemover_Click" OnClientClick="javascript:return confirmaExclusao();" />
                                                                                </span>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                    <AlternatingItemTemplate>
                                                                        <div class="dtgItemStyle" id='linha<%# DataBinder.Eval(Container.DataItem, "CodigoColaborador")%>'>
                                                                            <span class="cellRep cellColaborador">
                                                                                <%# DataBinder.Eval(Container.DataItem, "Nome")%>
                                                                                <br />
                                                                                <%# DataBinder.Eval(Container.DataItem, "CodigoColaborador")%></span><span class="cellRep cellJornada">
                                                                                    <asp:DropDownList ID="ddlJornada" DataSource='<%# GetHorarioEscala() %>' DataTextField="DescricaoJornada"
                                                                                        DataValueField="IdJornada" runat="server" AutoPostBack="false" class="cadddl">
                                                                                    </asp:DropDownList>
                                                                                    <%--<asp:RequiredFieldValidator ID="rfvPeriodo" runat="server" ControlToValidate="ddlJornada"
                                                                                        InitialValue="0" SetFocusOnError="True">*</asp:RequiredFieldValidator>--%>
                                                                                </span><span class="cellRep cellHorario">
                                                                                    <asp:DropDownList ID="ddlHorario" DataSource='<%# GetEscalaHorariosByJornada(Container.DataItem) %>'
                                                                                        DataTextField="IdHorario" DataValueField="IdEscala" runat="server" AutoPostBack="false"
                                                                                        class="cadddl">
                                                                                    </asp:DropDownList>
                                                                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlHorario"
                                                                                        InitialValue="0" SetFocusOnError="True">*</asp:RequiredFieldValidator>--%>
                                                                                </span><span class="cellRep cellFolgando">
                                                                                    <asp:CheckBox ID="chkFolgando" runat="server" Enabled='<%# (Convert.ToInt32(DataBinder.Eval(Container.DataItem, "idJornada"))==3) %>'
                                                                                        Checked='<%# DataBinder.Eval(Container.DataItem, "FlgIniciaFolgando")%>' />
                                                                                </span><span class="cellRep cellSabado">
                                                                                    <asp:DropDownList ID="ddlListCompensacao" runat="server" class="cadddl" Enabled="false"
                                                                                        AutoPostBack="false">
                                                                                        <asp:ListItem Text="Selecione" Value="0" />
                                                                                        <asp:ListItem Text="Sábado" Value="7" />
                                                                                        <asp:ListItem Text="Domingo" Value="1" />
                                                                                    </asp:DropDownList>
                                                                                    <%--<asp:RequiredFieldValidator ID="rfvListCompensacao" runat="server" ControlToValidate="ddlListCompensacao"
                                                                                        InitialValue="0" SetFocusOnError="True" ValidationGroup='<%# (Convert.ToInt32(DataBinder.Eval(Container.DataItem, "idJornada"))==1?"":"NV") %>'>*</asp:RequiredFieldValidator>--%>
                                                                                </span><span class="cellRep cellDomingo">
                                                                                    <asp:DropDownList ID="ddlHorarioCompensacao" DataSource='<%# GetEscalaHorariosByJornada(Container.DataItem) %>'
                                                                                        DataTextField="IdHorario" DataValueField="IdEscala" runat="server" AutoPostBack="false"
                                                                                        class="cadddl" Enabled="false">
                                                                                    </asp:DropDownList>
                                                                                    <%--<asp:RequiredFieldValidator ID="rfvHorarioCompensacao" runat="server" ControlToValidate="ddlHorarioCompensacao"
                                                                                        InitialValue="0" SetFocusOnError="True" ValidationGroup='<%# (Convert.ToInt32(DataBinder.Eval(Container.DataItem, "idJornada"))==1?"":"NV") %>'>*</asp:RequiredFieldValidator>--%>
                                                                                </span><span class="cellRep">
                                                                                    <asp:ImageButton ID="imgbRemover" runat="server" CausesValidation="false" ImageUrl="%%PATH%%/imagens/icones/ico_deletar.gif"
                                                                                        Text="Excluir" OnClick="imgbRemover_Click" OnClientClick="javascript:return confirmaExclusao();" />
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
                                                    <asp:CustomValidator ID="cvPreencherDadosGrade" runat="server" ErrorMessage="Selecionar jornada e horário para todos os colaboradores!"
                                                        OnServerValidate="ValidaGrade">*</asp:CustomValidator>
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
    <script type="text/javascript" language="javascript">
        // Workaround because this function from AJAX has a glitch
        function ValidatorOnChange(event) {
            if (!event) {
                event = window.event;
            }
            Page_InvalidControlToBeFocused = null;
            var targetedControl;
            if ((typeof (event.srcElement) != "undefined") && (event.srcElement != null)) {
                targetedControl = event.srcElement;
            }
            else {
                targetedControl = event.target;
            }
            var vals;
            if (typeof (targetedControl.Validators) != "undefined") {
                vals = targetedControl.Validators;
            }
            else {
                if (targetedControl.tagName.toLowerCase() == "label") {
                    targetedControl = document.getElementById(targetedControl.htmlFor);
                    vals = targetedControl.Validators;
                }
            }
            if (vals) // Correction: checks if this variable was assigned a value...
            {
                var i;
                for (i = 0; i < vals.length; i++) {
                    ValidatorValidate(vals[i], null, event);
                }
            }
            ValidatorUpdateIsValid();
        }
    </script>
</body>
</html>
