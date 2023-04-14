<%@ Control Language="vb" AutoEventWireup="false" Codebehind="ModuloLogin.ascx.vb" Inherits="FrameWork.UI.ModuloLogin" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<TABLE id="tbLogin" cellSpacing="0" cellPadding="0" width="370" border="0" runat="server">
	<TR>
		<TD width="20"><IMG src="%%PATH%%/imagens/bullet-titulo-laranja.gif"></TD>
		<TD class="loginlabel" align="right" width="30"><asp:label id="lblLogin" Runat="server">login:</asp:label>&nbsp;</TD>
		<TD width="65">
			<asp:TextBox id="signonlogin" runat="server" CssClass="logininput" MaxLength="50"></asp:TextBox></TD>
		<TD class="loginlabel" align="right" width="45"><asp:label id="lblSenha" Runat="server">senha:</asp:label>&nbsp;</TD>
		<TD width="65">
			<asp:TextBox id="signonsenha" runat="server" CssClass="logininput" TextMode="Password" MaxLength="50"></asp:TextBox></TD>
		<TD align="right" width="23" style="WIDTH: 23px">
			<asp:Button id="btnLogin" CssClass="forminput" runat="server" Text="ok" Font-Bold="True" Width="22px"></asp:Button></TD>
		<TD align="center" width="120"><asp:LinkButton id="lnkEsqueciSenha" runat="server" CssClass="loginlink" CausesValidation="False">esqueci a senha</asp:LinkButton></TD>
	</TR>
</TABLE>
