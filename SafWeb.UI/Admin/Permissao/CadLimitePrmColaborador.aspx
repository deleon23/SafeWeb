<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CadLimitePrmColaborador.aspx.cs"
    Inherits="SafWeb.UI.Admin.Permissao.CadLimitePrmColaborador" %>

<html>
<head>
    <title></title>
    <link href="../../Estilos/FrameWork.css" rel="stylesheet" type="text/css" />
    <link href="../../Estilos/Brinks.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {

            BindEventsCadastro();

            $("#ddlRegional").change(function () {
                BuscarFilial($(this));
            });

            $("#ddlFilial").change(function () {
                BuscarColaborador($(this));
            });

        });

        function BindEventsCadastro() {
            $("#btnSalvar").unbind("click");
            $("#btnSalvar").removeAttr("desable");
            $("#btnSalvar").bind("click", function () {
                //console.log("evento 1");
                $(this).unbind("click");
                $("#btnSalvar").remove("desable","desable");
//                $(this).bind("click", function () {
//                    console.log("evento 2");
//                    //alert("Aguarde!!!");
//                });
                SalvarLimite(this);
            });
        }

        function BuscarFilial(obj) {
            var options = "<option value='0'><-- Selecione --></option>";

            var IdRegional = parseInt($(obj).val());
            if (IdRegional == 0) {
                $("#ddlFilial").attr("disabled", "disabled");
            }
            else {
                $("#ddlFilial").attr("disabled", "");
                $("#ddlFilial").removeAttr("disabled");
                ShowLoad("lpaCadastro");
                PageMethods.PopularFilial(
                    IdRegional,
                    function (r) {
                        if (r.erro == false) {
                            $.each(r.lista, function (key, value) {
                                options += "<option value='" + value.IdFilial + "'>" + value.AliasFilial + "</option>";
                            });


                        } else {
                            alert(r.mensagem);
                        }
                        $("#ddlFilial").html(options);
                        HideLoad("lpaCadastro");
                    },
                    function (r) {
                        HideLoad("lpaCadastro");
                        alert("Tente novamente!");
                    }
                );
            }
        }

        function BuscarColaborador(obj) {
            var options = "<option value='0'><-- Selecione --></option>";

            var IdFilial = parseInt($(obj).val());
            if (IdFilial == 0) {
                $("#ddlColaborador").attr("disabled", "disabled");
            }
            else {
                $("#ddlColaborador").attr("disabled", "");
                $("#ddlColaborador").removeAttr("disabled");
                ShowLoad("lpaCadastro");
                PageMethods.PopularColaborador(
                    IdFilial,
                    function (r) {
                        if (r.erro == false) {
                            $.each(r.lista, function (key, value) {
                                options += "<option value='" + value.UsuNCodigo + "'>" + value.UsuCNome + "</option>";
                            });


                        } else {
                            alert(r.mensagem);
                        }
                        $("#ddlColaborador").html(options);
                        HideLoad("lpaCadastro");
                    },
                    function (r) {
                        HideLoad("lpaCadastro");
                        alert("Tente novamente!");
                    }
                );
            }

        }

        //Função do Botão Salvar(btnSalvar)
        function SalvarLimite(obj) {

            var parametro = new Object();
            var erro = false;

            parametro.idLimiteColaborador = parseInt($("#idLimitePrmColaborador").val());
            parametro.idColaborador = parseInt($("#ddlColaborador").val());
            parametro.idLimite = parseInt($("#ddlLimite").val());
            if (parametro.idColaborador == 0) {
                alert("Por favor selecione um colaborador");
                erro = true;

            } else if (parametro.idLimite == 0) {
                alert("Por favor selecione um limite");
                erro = true;
            }

            if (erro == false) {
                ShowLoad("lpaCadastro");
                PageMethods.SalvarPrmColaborador(
                    parametro,
                    function (r) {
                        HideLoad("lpaCadastro");
                        if (r.erro == false) {
                            $(obj).parents(".PopUp").popup("Destruir");
                            $("#btnBuscarList").click();
                        } else {
                            alert(r.mensagem);
                        };
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
</head>
<body>
    <form id="form" method="post" runat="server" onsubmit="return false;">
    <input runat="server" type="hidden" id="idLimitePrmColaborador" value="0" />
    <div runat="server" id="dvRegional" style="display: inline-block; margin-left: 5px; margin-top: 4px;">
        <span id="Span3" class="cadlbl">Regional</span>
        <div>
            <asp:dropdownlist id="ddlRegional" runat="server" cssclass="cadddl" width="180px">
            </asp:dropdownlist>
        </div>
    </div>
    <div runat="server" id="dvFilial" style="display: inline-block; margin-left: 6px;">
        <span id="Span4" class="cadlbl">Filial</span>
        <div>
            <asp:dropdownlist id="ddlFilial" runat="server" cssclass="cadddl" width="120px" enabled="False">
            </asp:dropdownlist>
        </div>
    </div>
    <div style="margin-left: 5px; margin-top: 11px;">
        <span id="Span2" class="cadlbl">Colaborador</span>
        <div>
            <asp:dropdownlist id="ddlColaborador" runat="server" cssclass="cadddl" width="311px"
                enabled="False">
            </asp:dropdownlist>
        </div>
    </div>
    <div style="display: inline-block; margin-top: 11px; margin-left: 5px;">
        <span id="Span1" class="cadlbl">Limite</span>
        <div>
            <asp:dropdownlist id="ddlLimite" runat="server" cssclass="cadddl" maxlength="50"
                width="120px">
            </asp:dropdownlist>
        </div>
    </div>
    <p>
    </p>
    <div class="backBarraBotoes" style="display: block; width: 311px; text-align: right;
        height: 25px; padding-top: 5px; margin-left: 5px;">
        <input id="btnSalvar" type="button" class="cadbutton80" value="Salvar" />
    </div>
    </form>
</body>
</html>
