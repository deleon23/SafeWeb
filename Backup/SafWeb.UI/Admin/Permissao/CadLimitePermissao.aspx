<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CadLimitePermissao.aspx.cs"
    Inherits="SafWeb.UI.Admin.Permissao.CadLimitePermissao" %>

<%--<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions" %>--%>
<html>
<head>
    <title></title>
    <link href="../../Estilos/FrameWork.css" rel="stylesheet" type="text/css" />
    <link href="../../Estilos/Brinks.css" rel="stylesheet" type="text/css" />
    <%--<script type="text/javascript" src="../../Scripts/jquery-1.7.2-vsdoc.js"></script>--%>
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            $('input[type="text"]').setMask();
        });

        $("#btnSalvar").live("click", function () {
            $(this).click(function () {
                alert("Aguarde!!!");
            });
            SalvarLimite(this);
        });

        //Função do Botão Salvar(btnSalvar)
        function SalvarLimite(obj) {

            var parametro = new Object();
            var erro = false;

            parametro.Limite = parseFloat($("#txtLimite", "#Cadastro").val().replace(".", "").replace(",", "."));
            parametro.idLimite = parseInt($("#idLimitePermissao").val());
            if (parametro.Limite == "") {
                alert("Por favor digite um Limite para ser cadastrado");
                erro = true;
            }
            if (erro == false) {
                ShowLoad("lpaCadastro");
                PageMethods.SalvarLimitePermissao(
                    parametro,
                    function (r) {
                        if (r.erro == false) {
                            HideLoad("lpaCadastro");
                            $(obj).parents(".PopUp").popup("Destruir");
                            $("#txtCodLimite").val("");
                            $("#txtLimite").val("0,00");
                            $("#btnBuscarList").click();
                        } else {
                            alert(r.mensagem);
                        }
                    },
                    function (r) {
                        HideLoad("lpaCadastro");
                        alert("Tente novamente!");
                    }
                );
            }
        }
    </script>
</head>
<body>
    <form id="form" method="post" runat="server" onsubmit="return false;">
    <input runat="server" type="hidden" id="idLimitePermissao" value="0" />
    <div style="display: inline-block; margin-left: 5px; margin-top: 4px;">
        <span id="Span1" class="cadlbl">Limite</span>
        <div id="Cadastro" style="">
            <input type="text" runat="server" id="txtLimite" style="width: 80px; text-align: right;"
                mask="porcentagem" class="cadtxt" name="txtLimite" value="0,00" />
        </div>
    </div>
    <p>
    </p>
    <div class="backBarraBotoes" style="display: block; width: 130px; text-align: right;
        height: 25px; padding-top: 5px; margin-left: 5px;">
        <input id="btnSalvar" type="button" class="cadbutton80" value="Salvar" />
    </div>
    </form>
</body>
</html>
