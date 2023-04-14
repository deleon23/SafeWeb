/* Variáveis Publicas*/
var textoAnt, textoComp = "";
//---------------------------------------------------------------------------
/*********************************/
/* Funções para Mascara CPF      */
/*********************************/
function mascara_CPF_Especial(objCampo, evt, proxControl, bolAutoPost, bolReadOnly){
		var key = (evt) ? evt.which : event.keyCode;
		var pos = getCaretPos(objCampo)-1;
		try{ event.keyCode = null; } catch(e){};
		
		if(bolReadOnly == "True")
		{
				return;
		}
		
		if(key == 13 && bolAutoPost == "True")
		{
			var theform;
			var eventTarget;
			eventTarget = objCampo.name;
			if (window.navigator.appName.toLowerCase().indexOf("microsoft") > -1) {
				theform = document.Form1;
			}
			else 
			{
				theform = document.forms["Form1"];
			}
			theform.__EVENTTARGET.value = eventTarget.split("$").join(":");
			theform.submit();
		}

		if(key >= 48 && key <= 57){
			if(objCampo.value.length == 0){
				objCampo.value= String.fromCharCode(key)+"__.___.___-__";
				setCaret(objCampo, 1);
				return;
			}
			var existeBarra = false;

			if(objCampo.value.substring(getCaretPos(objCampo), getCaretPos(objCampo)+1) == ".")	{
				existeBarra = true;
			}
			if(objCampo.value.substring(getCaretPos(objCampo), getCaretPos(objCampo)+1) == "-"){
				existeBarra = true;
			}
			objCampo.value = objCampo.value.substring(0, getCaretPos(objCampo)-1) + String.fromCharCode(key) +
			objCampo.value.substring(getCaretPos(objCampo), objCampo.value.length+1);

			if(existeBarra)
				pos+=2;
			else
				pos++;
			
			setCaret(objCampo, pos)
			if(objCampo.value.length == 15){
				objCampo.value = textoAnt;
			}
		}
		
		if(pos == 14 && proxControl != null){
			var prox = document.getElementById(proxControl)
			prox.focus();
			prox.onfocus();
		}
}
//---------------------------------------------------------------------------
/*********************************/
/* Funções para Mascara CNPJ     */
/*********************************/
function mascara_CNPJ_Especial(objCampo, evt, proxControl, bolAutoPost, bolReadOnly){
		var key = (evt) ? evt.which : event.keyCode;
		var pos = getCaretPos(objCampo)-1;
		try{ event.keyCode = null; } catch(e){};
		
		if(bolReadOnly == "True")
		{
				return;
		}
		
		if(key == 13 && bolAutoPost == "True")
		{
			var theform;
			var eventTarget;
			eventTarget = objCampo.name;
			if (window.navigator.appName.toLowerCase().indexOf("microsoft") > -1) {
				theform = document.Form1;
			}
			else 
			{
				theform = document.forms["Form1"];
			}
			theform.__EVENTTARGET.value = eventTarget.split("$").join(":");
			theform.submit();
		}

		if(key >= 48 && key <= 57){
			if(objCampo.value.length == 0){
				objCampo.value= String.fromCharCode(key)+"_.___.___/____-__";
				setCaret(objCampo, 1);
				return;
			}
			var existeBarra = false;

			if(objCampo.value.substring(getCaretPos(objCampo), getCaretPos(objCampo)+1) == "."){
				existeBarra = true;
			}
			if(objCampo.value.substring(getCaretPos(objCampo), getCaretPos(objCampo)+1) == "/"){
				existeBarra = true;
			}
			if(objCampo.value.substring(getCaretPos(objCampo), getCaretPos(objCampo)+1) == "-"){
				existeBarra = true;
			}
			objCampo.value = objCampo.value.substring(0, getCaretPos(objCampo)-1) + String.fromCharCode(key) +
			objCampo.value.substring(getCaretPos(objCampo), objCampo.value.length+1);

			if(existeBarra)
				pos+=2;
			else
				pos++;
			
			setCaret(objCampo, pos)
			if(objCampo.value.length == 19){
				objCampo.value = textoAnt;
			}
		}
		
		if(pos == 18 && proxControl != null){
			var prox = document.getElementById(proxControl)
			prox.focus();
			prox.onfocus();
		}
}
//---------------------------------------------------------------------------
/*********************************/
/* Funções para Mascara Numero   */
/*********************************/
function mascara_Numero(objCampo, intKeyCode)
{
        if(parseInt(intKeyCode) != 13)
		{
				if((parseInt(intKeyCode) < 48) || (parseInt(intKeyCode) > 57))
				{
				    try{ event.keyCode = null; } catch(e){};
					return false;
				}
				else
				{
					return true;
				}
		}
		else
		{
				return true;
		}
}
//---------------------------------------------------------------------------
/*********************************/
/* Funções para Mascara Reais    */
/*********************************/
function mascara_Reais(objCampo, evt, tammax) {
	var milSep = '.';
	var decSep = ',';
	var sep = 0;
	var key = '';
	var i = j = 0;
	var len = len2 = 0;
	var strCheck = '0123456789';
	var aux = aux2 = '';
	var whichCode = (window.Event) ? evt.which : evt.keyCode;
	if (whichCode == 13) return true;  
	key = String.fromCharCode(whichCode);  
	if (strCheck.indexOf(key) == -1) return false;  
	len = objCampo.value.length;
	if (len == tammax) return false;
	for(i = 0; i < len; i++) if ((objCampo.value.charAt(i) != '0') && (objCampo.value.charAt(i) != decSep)) break;
	aux = '';
	for(; i < len; i++) if (strCheck.indexOf(objCampo.value.charAt(i))!=-1) aux += objCampo.value.charAt(i);
	aux += key;
	len = aux.length;
	if (len == 0) objCampo.value = '';
	if (len == 1) objCampo.value = '0'+ decSep + '0' + aux;
	if (len == 2) objCampo.value = '0'+ decSep + aux;
	if (len > 2) {
		aux2 = '';
		for (j = 0, i = len - 3; i >= 0; i--) {
			if (j == 3) {
				aux2 += milSep;
				j = 0;
			}
			aux2 += aux.charAt(i);
			j++;
		}
		objCampo.value = '';
		len2 = aux2.length;
		for (i = len2 - 1; i >= 0; i--) objCampo.value += aux2.charAt(i);
		objCampo.value += decSep + aux.substr(len - 2, len);
	}
	return false;
}	
//---------------------------------------------------------------------------
/*********************************/
/* Funções para Mascara Dolar    */
/*********************************/
function mascara_Dolar(objCampo, evt, tammax) {
	var milSep = ',';
	var decSep = '.';
	var sep = 0;
	var key = '';
	var i = j = 0;
	var len = len2 = 0;
	var strCheck = '0123456789';
	var aux = aux2 = '';
	var whichCode = (window.Event) ? evt.which : evt.keyCode;
	if (whichCode == 13) return true;  
	key = String.fromCharCode(whichCode);  
	if (strCheck.indexOf(key) == -1) return false;  
	len = objCampo.value.length;
	if (len == tammax) return false;
	for(i = 0; i < len; i++) if ((objCampo.value.charAt(i) != '0') && (objCampo.value.charAt(i) != decSep)) break;
	aux = '';
	for(; i < len; i++) if (strCheck.indexOf(objCampo.value.charAt(i))!=-1) aux += objCampo.value.charAt(i);
	aux += key;
	len = aux.length;
	if (len == 0) objCampo.value = '';
	if (len == 1) objCampo.value = '0'+ decSep + '0' + aux;
	if (len == 2) objCampo.value = '0'+ decSep + aux;
	if (len > 2) {
		aux2 = '';
		for (j = 0, i = len - 3; i >= 0; i--) {
			if (j == 3) {
				aux2 += milSep;
				j = 0;
			}
			aux2 += aux.charAt(i);
			j++;
		}
		objCampo.value = '';
		len2 = aux2.length;
		for (i = len2 - 1; i >= 0; i--) objCampo.value += aux2.charAt(i);
		objCampo.value += decSep + aux.substr(len - 2, len);
	}
	return false;
}	
//---------------------------------------------------------------------------
/*********************************/
/* Funções para Mascara CEP      */
/*********************************/
function mascara_CEP_Especial(objCampo, evt, proxControl, bolAutoPost, bolReadOnly){
		var key = (evt) ? evt.which : event.keyCode;
		var pos = getCaretPos(objCampo)-1;
		try{ event.keyCode = null; } catch(e){};
		
		if(bolReadOnly == "True")
		{
				return;
		}
		
		if(key == 13 && bolAutoPost == "True")
		{
			var theform;
			var eventTarget;
			eventTarget = objCampo.name;
			if (window.navigator.appName.toLowerCase().indexOf("microsoft") > -1) {
				theform = document.Form1;
			}
			else 
			{
				theform = document.forms["Form1"];
			}
			theform.__EVENTTARGET.value = eventTarget.split("$").join(":");
			theform.submit();
		}

		if(key >= 48 && key <= 57){
			if(objCampo.value.length == 0){
				objCampo.value= String.fromCharCode(key)+"____-___";
				setCaret(objCampo, 1);
				return;
			}
			var existeBarra = false;

			if(objCampo.value.substring(getCaretPos(objCampo), getCaretPos(objCampo)+1) == "-"){
				existeBarra = true;
			}
			objCampo.value = objCampo.value.substring(0, getCaretPos(objCampo)-1) + String.fromCharCode(key) +
			objCampo.value.substring(getCaretPos(objCampo), objCampo.value.length+1);

			if(existeBarra)
				pos+=2;
			else
				pos++;
			
			setCaret(objCampo, pos)
			if(objCampo.value.length == 10){
				objCampo.value = textoAnt;
			}
		}
			
		if(pos == 9 && proxControl != null)	{
			var prox = document.getElementById(proxControl)
			prox.focus();
			prox.onfocus();
		}
}
//---------------------------------------------------------------------------
/*********************************/
/* Funções para Mascara TELEFONE */
/*********************************/
function mascara_TELEFONE_Especial(objCampo, evt, proxControl, bolAutoPost, bolReadOnly){
		var key = (evt) ? evt.which : event.keyCode;
		var pos = getCaretPos(objCampo)-1;
		try{ event.keyCode = null; } catch(e){};

		if(bolReadOnly == "True")
		{
				return;
		}
		
		if(key == 13 && bolAutoPost == "True")
		{
			var theform;
			var eventTarget;
			eventTarget = objCampo.name;
			if (window.navigator.appName.toLowerCase().indexOf("microsoft") > -1) {
				theform = document.Form1;
			}
			else 
			{
				theform = document.forms["Form1"];
			}
			theform.__EVENTTARGET.value = eventTarget.split("$").join(":");
			theform.submit();
		}
			
		if(key >= 48 && key <= 57)
		{
			if(objCampo.value.length == 0){
				objCampo.value= String.fromCharCode(key)+"_-________";
				setCaret(objCampo, 1);
				return;
			}
			var existeBarra = false;

			if(objCampo.value.substring(getCaretPos(objCampo), getCaretPos(objCampo)+1) == "-"){
				existeBarra = true;
			}
			objCampo.value = objCampo.value.substring(0, getCaretPos(objCampo)-1) + String.fromCharCode(key) +
			objCampo.value.substring(getCaretPos(objCampo), objCampo.value.length+1);

			if(existeBarra)
				pos+=2;
			else
				pos++;
			
			setCaret(objCampo, pos)
			if(objCampo.value.length == 12){
				objCampo.value = textoAnt;
			}
		}
		
		if(pos == 11 && proxControl != null){
			var prox = document.getElementById(proxControl)
			prox.focus();
			prox.onfocus();
		}
}
//---------------------------------------------------------------------------
/*********************************/
/* Funções para Mascara Data     */
/*********************************/
function mascara_Data_Especial(objCampo, evt, proxControl, bolAutoPost, bolReadOnly){
		var key = (evt) ? evt.which : event.keyCode;
		var pos = getCaretPos(objCampo)-1;
		try{ event.keyCode = null; } catch(e){};
		
		if(bolReadOnly == "True")
		{
				return;
		}
		
		if(key == 13 && bolAutoPost == "True")
		{
			var theform;
			var eventTarget;
			eventTarget = objCampo.name;
			if (window.navigator.appName.toLowerCase().indexOf("microsoft") > -1) {
				theform = document.Form1;
			}
			else 
			{
				theform = document.forms["Form1"];
			}
			theform.__EVENTTARGET.value = eventTarget.split("$").join(":");
			theform.submit();
		}

		if(key >= 48 && key <= 57)
		{
			if(objCampo.value.length == 0){
				objCampo.value= String.fromCharCode(key)+"_/__/____";
				setCaret(objCampo, 1);
				return;
			}
			var existeBarra = false;

			if(objCampo.value.substring(getCaretPos(objCampo), getCaretPos(objCampo)+1) == "/"){
				existeBarra = true;
			}
			objCampo.value = objCampo.value.substring(0, getCaretPos(objCampo)-1) + String.fromCharCode(key) +
			objCampo.value.substring(getCaretPos(objCampo), objCampo.value.length+1);

			if(existeBarra)
				pos+=2;
			else
				pos++;
			
			setCaret(objCampo, pos)
			if(objCampo.value.length == 11){
				objCampo.value = textoAnt;
			}
		}
			
		if(pos == 10 && proxControl != null){
			var prox = document.getElementById(proxControl)
			prox.focus();
			prox.onfocus();
		}
}
//---------------------------------------------------------------------------
/*********************************/
/* Funções para Mascara Hora     */
/*********************************/
function mascara_Hora_Especial(objCampo, evt, proxControl, bolAutoPost, bolReadOnly){
		var key = (evt) ? evt.which : event.keyCode;
		var pos = getCaretPos(objCampo)-1;
		try{ event.keyCode = null; } catch(e){};
		
		if(bolReadOnly == "True")
		{
				return;
		}
		
		if(key == 13 && bolAutoPost == "True")
		{
			var theform;
			var eventTarget;
			eventTarget = objCampo.name;
			if (window.navigator.appName.toLowerCase().indexOf("microsoft") > -1) {
				theform = document.Form1;
			}
			else 
			{
				theform = document.forms["Form1"];
			}
			theform.__EVENTTARGET.value = eventTarget.split("$").join(":");
			theform.submit();
		}

		if(key >= 48 && key <= 57){
			if(objCampo.value.length == 0){
				objCampo.value= String.fromCharCode(key)+"_:__";
				setCaret(objCampo, 1);
				return;
			}
			var existeBarra = false;

			if(objCampo.value.substring(getCaretPos(objCampo), getCaretPos(objCampo)+1) == ":")
			{
				existeBarra = true;
			}
			objCampo.value = objCampo.value.substring(0, getCaretPos(objCampo)-1) + String.fromCharCode(key) +
			objCampo.value.substring(getCaretPos(objCampo), objCampo.value.length+1);

			if(existeBarra)
				pos+=2;
			else
				pos++;
			
			setCaret(objCampo, pos)
			if(objCampo.value.length == 6){
				objCampo.value = textoAnt;
			}
		}
			
		if(pos == 7 && proxControl != null)	{
			var prox = document.getElementById(proxControl)
			prox.focus();
			prox.onfocus();
		}
}
//---------------------------------------------------------------------------
/*********************************/
/* Funções para Mascara Especial */
/*********************************/
//---------------------------------------------------------------------------
function getCaretPos(objCampo){
	var i=objCampo.value.length+1;
	if (objCampo.createTextRange){
		theCaret = document.selection.createRange().duplicate();
		while ( theCaret.parentElement() == objCampo	&& theCaret.move("character",1)==1 ) --i;
	}
	return i==objCampo.value.length+1?-1:i;
}
//---------------------------------------------------------------------------
function setCaret(objCampo,pos){ 
	var r =objCampo.createTextRange()
	r.moveStart('character',pos)
	r.collapse(true)
	r.select()
}
//---------------------------------------------------------------------------
function mascara_Inicio(objCampo){
	setCaret(objCampo, 0)
	textoAnt = objCampo.value;
	textoComp = objCampo.value;
}
//---------------------------------------------------------------------------
function getValorAnt(objCampo){
	textoAnt = objCampo.value
}
//---------------------------------------------------------------------------
function mascara_Especial(objCampo, evt, bolReadOnly){
	if(bolReadOnly == "True")
	{
			return;
	}
	
	var key = (evt) ? evt.which : event.keyCode;
	var pos = getCaretPos(objCampo)-1;
	try{ event.keyCode = null; } catch(e){};
	if(key == 46){
		objCampo.value = '';
		setCaret(objCampo, 0);
	}
	else if(key == 8){
		objCampo.value = textoAnt;
		setCaret(objCampo, pos);
	}
}
//---------------------------------------------------------------------------
function verificaAutoPost(objCampo, bolAutoPost){
	
	if (bolAutoPost != "True") return;
	
	var theform;
	var eventTarget;
	eventTarget = objCampo.name;
	if (window.navigator.appName.toLowerCase().indexOf("microsoft") > -1) {
		theform = document.Form1;
	}
	else 
	{
		theform = document.forms["Form1"];
	}
	theform.__EVENTTARGET.value = eventTarget.split("$").join(":");
	theform.submit();
}
//---------------------------------------------------------------------------