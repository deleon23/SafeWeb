<%@ Control Language="vb" AutoEventWireup="false" Codebehind="Idioma.ascx.vb" Inherits="FrameWork.UI.Idioma" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<table cellpadding="0" cellspacing="0" width="100%" border="0" runat="server" id="tblIdiomas">
	<tr>
		<td align="right" class="LinkLogon">
			<asp:LinkButton CssClass="LinkIdiomas" id="lnkPortugues" runat="server" CausesValidation="False">Portugu�s</asp:LinkButton>
			<asp:Label id="lblDivisorPortugues" runat="server">|</asp:Label>
			<asp:LinkButton CssClass="LinkIdiomas" id="lnkEspanhol" runat="server" CausesValidation="False">Espanhol</asp:LinkButton>
			<asp:Label id="lblDivisorEspanholIngles" runat="server">|</asp:Label>
			<asp:LinkButton CssClass="LinkIdiomas" id="lnkIngl�s" runat="server" CausesValidation="False">Ingl�s</asp:LinkButton>
		</td>
	</tr>
</table>
