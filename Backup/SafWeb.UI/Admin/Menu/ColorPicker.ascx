<%@ Control Language="vb" AutoEventWireup="false" Codebehind="ColorPicker.ascx.vb" Inherits="FrameWork.UI.ColorPicker" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<script language="javascript">
	var controleCor;
function PegaCor(controle){
	controleCor = controle;
	if (document.all) {
			document.all.corselect.style.background = controle.value;
	}
}

function RetornaCor(cor) {
	if (controleCor != null){
		controleCor.value = cor;
		if (document.all) {
				document.all.corselect.style.background = cor;
		}
	}
}


</script>
<asp:PlaceHolder id="tabela" runat="server"></asp:PlaceHolder>
