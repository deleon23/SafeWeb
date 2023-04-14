<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CadPermissaoCrachaTitular.aspx.cs"
    Inherits="SafWeb.UI.Modulos.HelpDesk.CadPermissaoCrachaTitular" %>

<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../Estilos/FrameWork.css" rel="stylesheet" type="text/css" />
    <link href="../../Estilos/Brinks.css" rel="stylesheet" type="text/css" />
    <%--<script src="../../Scripts/jquery-1.7.2-vsdoc.js" type="text/javascript"></script>--%>
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            BindEventsCadastro();
            ExibirSelecionados();
        });

        function BindEventsCadastro() {
            //console.log("bind");
            $("#btnSalvarCadastro").unbind("click");
            $("#btnSalvarCadastro").bind("click", function () {
                //console.log("evento 1");
                $(this).unbind("click");
                $(this).bind("click", function () {
                    //console.log("evento 2");
                    alert("Aguarde!!!");
                });
                SalvarPermissaoColaborador(this);
            });

            $("#chkGrupoColetores").change(function () {
                ExibirSelecionados();
            });
        }


        function ExibirSelecionados() {
            var arrayIDGrupoColetores = [];
            $("#chkGrupoColetores:checked").each(function () {
                arrayIDGrupoColetores.push($(this).next().html().trim());
            })
            $("#spnSelecionados").html(arrayIDGrupoColetores.join("<br />"));
        }

        //Funçaõ do Botão Salvar(btnSalvar)
        function SalvarPermissaoColaborador(obj) {

            var parametro = new Object();
            var erro = false;
            parametro.idPermissao = parseInt($("#idPermissao").val());
            parametro.desGrupoColetores = $("#txtNome").val();
            parametro.idGrupoColetores = "";

            var arrayIDGrupoColetores = [];
            $("#chkGrupoColetores:checked").each(function () {
                arrayIDGrupoColetores.push($(this).val());
            })
            parametro.idGrupoColetores = arrayIDGrupoColetores.join(",");

            if (parametro.idGrupoColetores == "") {
                alert("Por favor selecione um grupo de Coletores");
                erro = true;
            }
            if (erro == false) {
                ShowLoad("lpaCadastro");
                PageMethods.SalvarGrupoColetor(
                    parametro.idPermissao,
                    parametro.desGrupoColetores,
                    parametro.idGrupoColetores,
                    function (r) {
                        HideLoad("lpaCadastro");
                        BindEventsCadastro();
                        if (r.erro == false) {
                            var config = $(obj).parents(".PopUp").data('PopUp');

                            if (config.callbackSalvar != undefined) {

                                config.callbackSalvar(r.lista, (parseInt($("#idPermissao").val()) == 0 ? "N" : "E"));
                            }

                            $(obj).parents(".PopUp").popup("Destruir");
                        } else {
                            alert(r.mensagem);
                        }
                    },
                    function (r) {
                        HideLoad("lpaCadastro");
                        alert("Tente novamente!");
                        BindEventsCadastro();
                    }
                );
            } else {
                BindEventsCadastro();
            }
        }

    </script>
    <style type="text/css">
        .CheckBoxList
        {
            width: 155px;
            color: #154E7A;
            font: 11px Tahoma,Verdana;
        }
        
        .CheckBoxList td
        {
            /*border: 1px solid red;*/
            padding: 0;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" onsubmit="return false;">
    <input runat="server" type="hidden" id="idPermissao" value="0" />
    <div style="display: inline-block; margin-left: 5px; margin-top: 4px; vertical-align: top;
        float: left;">
        <span id="Span1" class="cadlbl">Nome:</span>
        <div id="Cadastro">
            <asp:TextBox ID="txtNome" runat="server" CssClass="cadtxt" Width="150px">
            </asp:TextBox>
            <br />
            <span id="spnSelecionados" style="display: block; border: 1px solid; height: 144px;
                margin-top: 13px; overflow-y: auto; padding-left: 4px; color: #154E7A; font: 11px Tahoma,Verdana; width:149px;">
            </span>
        </div>
    </div>
    <div style="display: inline-block; margin-left: 11px; margin-top: 4px; width: 188px;
        float: left;">
        <span id="Span2" class="cadlbl">Grupos de Coletores:</span>
        <div style="height: 100px; overflow-y: auto; border: 1px solid; height: 176px; background: #E5E9F1;
            color: #154E7A; font: 11px Tahoma,Verdana;">
            <%--<asp:CheckBoxList ID="chkGrupoColetores" runat="server" Width="155px" class="CheckBoxList"
                CellSpacing="0" CellPadding="0" EnableViewState="false" RepeatLayout="Flow" >
            </asp:CheckBoxList>--%>
            <asp:Repeater runat="server" ID="rptGrupoColetores">
                <HeaderTemplate>
                    <span id="chkGrupoColetores" class="CheckBoxList" style="display: inline-block; width: 155px;">
                </HeaderTemplate>
                <ItemTemplate>
                    <input id="chkGrupoColetores" type="checkbox" value='<%# Eval("idGrupoColetores") %>'
                        <%# (Convert.ToBoolean(Eval("Selecionado"))==true?"checked='checked'":"")%>>
                    <label for="chkGrupoColetores">
                        <%# Eval("desGrupoColetores")%></label>
                </ItemTemplate>
                <FooterTemplate>
                    </span>
                </FooterTemplate>
                <SeparatorTemplate>
                    <br />
                </SeparatorTemplate>
            </asp:Repeater>
        </div>
    </div>
    <div style="clear: both;">
    </div>
    <p>
    </p>
    <div id="popup" class="backBarraBotoes" style="display: block; width: 344px; text-align: right;
        height: 25px; padding-top: 5px; margin-left: 5px;">
        <input id="btnSalvarCadastro" type="button" class="cadbutton80" value="Salvar" />
    </div>
    </form>
</body>
</html>
