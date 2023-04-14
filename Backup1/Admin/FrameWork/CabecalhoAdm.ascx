<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="CabecalhoAdm.ascx.vb" Inherits="FrameWork.UI.CabecalhoAdm1" %>
<%@ Register TagPrefix="cc1" Namespace="FrameWork.WebControl" Assembly="FrameWork.WebControl" %>

<script language="javascript" type="text/javascript">
    function RetornaPortal() 
    {
        window.location = "https://www.brinks.com.br";
    }         

</script>
<table border="0" cellpadding="0" cellspacing="0" width="100%" align="center">
	<tr>
		<td rowspan="2" width="179" height="82"><a href="%%PATH%%/Default.aspx"><img name="logo" src="%%PATH%%/Imagens/logotipo.jpg" width="179" height="82" border="0" alt=""></a></td>
		<td width="599" height="26"  align="right" background="%%PATH%%/Imagens/top_Logon.jpg">
			<img id="imgBandeira" src="%%PATH%%/Imagens/Bandeiras/Default.gif">
			<asp:Label id="lblMensagem" cssclass="textoPageBanner" runat="server">Bem Vindo,</asp:Label>&nbsp;
			<asp:Label id="lblNomeUsuario" runat="server" cssclass="LinkUser">%%NOME%%</asp:Label>&nbsp;&nbsp;

            <asp:LinkButton  OnClientClick="RetornaPortal();return false;" id="lnkLogOff" runat="server" cssclass="LinkLogoff" CausesValidation="False"><a href="https://www.brinks.com.br"><img src="%%PATH%%/Imagens/icones/ico_logoff.gif" border="0" align="absMiddle">Logoff</a></asp:LinkButton>

			<asp:LinkButton id="lnkLogOn" runat="server" cssclass="LinkLogon" CausesValidation="False" ><img src="%%PATH%%/Imagens/admin/ico_logoff.gif" border="0" align="absMiddle">Logon</asp:LinkButton>
			<asp:LinkButton id="lnkAdmin" runat="server" cssclass="LinkAdm" CausesValidation="False">><img src="%%PATH%%/Imagens/icones/ico_areaAdm.gif" border="0" align="absMiddle">Área Administrativa</asp:LinkButton>&nbsp;
		</td>
	</tr>
	<tr>
		<td width="599" height="56"><img name="Top_BemVindo" src="%%PATH%%/Imagens/Top_BemVindo.jpg" width="599" height="56"
				border="0" alt=""></td>
	</tr>
</table>
<table border="0" cellpadding="0" cellspacing="0" width="100%" align="center">
	<TR>
		<TD width="100%" height="28" valign="middle" background="%%PATH%%/Imagens/comum/backmenu.jpg">
			<cc1:FWServerControl id="Fwservercontrol3" runat="server" Controle="Menu" Parametro="1;30"></cc1:FWServerControl>
		</TD>
	</TR>
</table>