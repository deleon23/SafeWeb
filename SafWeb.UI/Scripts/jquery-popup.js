/// <reference path="jquery-1.7.2-vsdoc.js" />
/*
*
* Para utilizar o drag você tem que adicionar o script jquery.event.drag-2.2.js e a versão do jquery deve ser superior a 1.7.2
*
*/
(function ($) {
    $.Path = function () {
        return $(location).attr("pathname").split("/")[1];
    };
})(jQuery);

(function ($) {
    $.CriarPopUp = function (settings) {
        var config = {
            'titulo': '',
            'conteudo': '',
            'width': 300,
            'height': 300,
            'drag': true,
            'id': 'dvPopUp',
            "modal": true,
            "closekey": 27, /*keyCode Esc, 0 para desligado*/
            "closeCallback": null//,
//            "EventosTeclas":[
//                {"key": 13, "executar":null}
//            ]
        };
        if (settings) { $.extend(config, settings); }

        if ($("#" + config.id)[0] != undefined) {
            $("#" + config.id).remove();
        }

        var divPrincipal = $("<div />").attr({
            "id": config.id,
            "class": "PopUp"
        }).css({
            'height': config.height,
            'width': config.width,
            'margin-top': (config.height / 2) * -1,
            'margin-left': (config.width / 2) * -1,
            'top': ($(window).height() / 2) + $(document).scrollTop()
        });

        divPrincipal.data("PopUp", config);

        var divConteudo = $("<div />").attr({
            "id": "dvConteudo",
            "class": "centroConteudo"
        }).html(config.conteudo);

        var divCantos = $("<div />").addClass("canto").addClass("handle");
        var divCentros = $("<div>").addClass("centro").addClass("handle");

        var imgFechar = $("<img />").attr({
            "id": "imgfechar",
            "src": "/"+ $.Path() + "/Imagens/icones/fechar_icon.png",
            "class": "fecharIcon"
        }).click(function () { divPrincipal.popup("Destruir"); });

        var spanTitulo = $("<span />").attr({
            "id": "spTitulo",
            "class": "tituloPopup handle"
        }).html(config.titulo);

        divPrincipal
            .append(divCantos.clone().addClass("cantoLT"))
            .append(divCentros.clone().addClass("centroRT").append(imgFechar))
            .append(divCentros.clone().addClass("centroRL").append(spanTitulo))
            .append(divCantos.clone().addClass("cantoRT"))
            .append(divConteudo)
            .append(divCantos.clone().addClass("cantoLB").removeClass("handle"))
            .append(divCentros.clone().addClass("centroLB").removeClass("handle"))
            .append(divCentros.clone().addClass("centroRB").removeClass("handle"))
            .append(divCantos.clone().addClass("cantoRB").removeClass("handle"))
            .hide();

        $("body").append(divPrincipal);

        if (config.closekey > 0) {
            $(document).bind("keypress." + config.id, function (event) {
                if (event.keyCode == config.closekey) {
                    var fechar = true;

                    if (config.closeCallback != null) {
                        fechar = config.closeCallback();
                    }

                    if (fechar)
                        divPrincipal.popup("Destruir");
                }
            });
        }


        if (config.drag == true) {

            divPrincipal.drag(function (ev, event) {

                $(this).css({
                    top: event.offsetY - parseFloat($(this).css("margin-top")), //Tive que subtrair o valor das margens que centraliza a popup
                    left: event.offsetX - parseFloat($(this).css("margin-left"))
                });
            }, { handle: ".handle" });

        } else {
            $(".handle").each(function (i) {
                $(this).removeClass("handle");
            });
        }

        if ($("#imgFundo")[0] == undefined) {

            var dvModal = $("<img />").attr({
                "id": "imgFundo",
                "src": "/"+ $.Path() + "/RadControls/Window/Skins/Office2007/Img/transp.gif",
                "class": "modalPopup"
            }).css("height", $(document).innerHeight());

            dvModal.hide();
            $("body").append(dvModal);
        }
        return divPrincipal;

    };

    var methodsPopUp = {
        loadUrl: function (settings) {
            var config = {
                'url': null
            };

            $.extend(config, $(this).data('PopUp'));

            if (settings) { $.extend(config, settings); }

            var obj = $(this);

            obj.data("PopUp", config);

            if (config.url != null) {
                $.ajax({
                    type: "POST",
                    url: config.url,
                    success: function (data) {
                        $("#dvConteudo", $(obj)).html(data);
                        $(obj).show();
                        if ($("#imgFundo")[0] != undefined)
                            if (config.modal == true)
                                $("#imgFundo").show();

                        if (config.callback != undefined) {
                            config.callback(this);
                        }
                    }
                });
            }

            return $(this);
        },
        show: function () {
            var config = $(this).data('PopUp');

            $(this).show();

            if (config.modal == true)
                if ($("#imgFundo")[0] != undefined)
                    $("#imgFundo").show();


            return $(this);
        },
        hide: function () {
            var config = $(this).data('PopUp');
            $(this).hide();

            if (config.modal == true)
                if ($("#imgFundo")[0] != undefined)
                    $("#imgFundo").hide();

            return $(this);
        },
        Destruir: function () {
            $(document).unbind("keypress." + $(this).attr("id"));
            $(this).remove();

            if ($("#imgFundo")[0] != undefined)
                $("#imgFundo").hide();

            return $(this);
        }
    };

    $.fn.popup = function (method) {

        if ($(this).data('PopUp') != undefined) {

            if (methodsPopUp[method]) {
                return methodsPopUp[method].apply(this, Array.prototype.slice.call(arguments, 1));
            } else {
                $.error('O metodo "' + method + '" não faz parte do jQuery.popup');
            }
        } else {
            $.error('O objeto "' + $(this).attr("id") + '" não é um popup');
        }

    };

})(jQuery);

/***/
function ShowLoad(objLoad) {
    //lpaCadastro
    $("form").before(
        $('<div style="height: 331px; width: 778px; text-align: center; position: absolute; left: 280.5px; top: 118px; z-index: 120000; opacity: 0.7; display: none;" id="' + objLoad + 'Clone">').append(
            $("#" + objLoad).clone().html()
        )
    )

    $("#" + objLoad + "Clone").css({
        'top': $(document).scrollTop()
    });

    $("#" + objLoad + "Clone").show();
}

function HideLoad(objLoad) {
    $("#" + objLoad + "Clone").remove();
}