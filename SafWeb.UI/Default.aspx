<%@ Page Language="vb" AutoEventWireup="false" Codebehind="Default.aspx.cs" Inherits="FrameWork.UI._Default" %>

<%@ Register TagPrefix="cc1" Namespace="FrameWork.WebControl" Assembly="FrameWork.WebControl" %>

<html>
<head id="Head1" runat="server">
    <title>%%SITE%%</title>
    <link href="Estilos/Brinks.css" type="text/css" rel="stylesheet" />
    <link href="Estilos/FrameWork.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="Form1" method="post" runat="server">
        <table cellspacing="0" cellpadding="0" width="778" align="center" border="0">
            <tr>
                <td style="height: 18px">
                    <!-- ********************* START: CABEÇALHO ****************** -->
                    <cc1:FWServerControl ID="FWServerControlCabecalho" runat="server" Controle="Pagebanner"
                        Arquivo="/Modulos/Framework/CabecalhoBrinks.ascx"></cc1:FWServerControl>
                    <!-- ********************* END: CABEÇALHO ****************** -->
                </td>
            </tr>
            <tr>
                <td height="424">
                    <table align="center" cellpadding="0" cellspacing="0" class="backPadrao" width="100%"
                        height="426">
                        <tr>
                            <td align="center" valign="middle" class="BordaDefaultLRB">
                                <img src="%%PATH%%/Imagens/menuballs/background_capaBrinks.jpg"></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td valign="bottom">
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
