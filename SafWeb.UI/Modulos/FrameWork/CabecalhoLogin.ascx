<%@ Control Language="vb" AutoEventWireup="false" Codebehind="Cabecalho.ascx.vb" Inherits="FrameWork.UI.Cabecalho" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="cc1" Namespace="FrameWork.WebControl" Assembly="FrameWork.WebControl" %>

<table border="0" cellpadding="0" cellspacing="0" width="100%" align="center">
	<tr>
		<td rowspan="2" width="179" height="82"><a href="%%PATH%%/Default.aspx"><img name="logo" src="%%PATH%%/Imagens/logotipo.jpg" width="179" height="82" border="0" alt=""></a></td>
		<td width="599" height="26"  align="right" background="%%PATH%%/Imagens/top_Logon.jpg">
			<asp:Label id="lblMensagem" cssclass="textoPageBanner" runat="server">Mensagem</asp:Label>&nbsp;
		</td>
	</tr>
	<tr>
		<td width="599" height="56">
		        <img name="Top_BemVindo" src="%%PATH%%/Imagens/Top_BemVindo.jpg" width="599" height="56" border="0" alt=""></td>
	</tr>
</table>
<!--
<asp:LinkButton id="lnkLogOn" runat="server" cssclass="LinkLogon" CausesValidation="False"><img src="%%PATH%%/imagens/admin/ico_logoff.gif" border="0" align="absMiddle">Extranet</asp:LinkButton>
<asp:LinkButton id="lnkLogOff" runat="server" cssclass="LinkLogoff" CausesValidation="False"><img src="%%PATH%%/imagens/admin/ico_logoff.gif" border="0" align="absMiddle">Logoff</asp:LinkButton>
<asp:LinkButton id="lnkAdmin" runat="server" cssclass="LinkAdm" CausesValidation="False"><img src="%%PATH%%/imagens/admin/ico_areaadmin.gif" border="0" align="absMiddle">Área Administrativa</asp:LinkButton>
-->
<table border="0" cellpadding="0" cellspacing="0" width="100%" align="center">
	<TR>
		<TD width="100%" height="28" valign="middle" background="%%PATH%%/Imagens/comum/backmenu.jpg">
			<cc1:FWServerControl id="Fwservercontrol3" runat="server" Controle="Menu" Parametro="32;30"></cc1:FWServerControl>
		</TD>
	</TR>
</table>
