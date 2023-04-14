/* 
''' -----------------------------------------------------------------------------
''' <summary>
''' Função para mudar a linha ao de-selecionar
''' </summary>
''' <example>
'''	onMouseOver="selecionaGrid(this);"
'''	onMouseOut="deSelecionaGrid(this);"
''' </example>
''' <history>
''' 	[eneves]	16/4/2007		Created
''' </history>
''' -----------------------------------------------------------------------------
*/

		
/* 
''' -----------------------------------------------------------------------------
''' <summary>
''' Função para mudar a linha ao de-selecionar
''' </summary>
''' <example>
'''	onMouseOver="selecionaGrid(this);"
'''	onMouseOut="deSelecionaGrid(this);"
''' </example>
''' <history>
''' 	[eneves]	16/4/2007		Created
''' </history>
''' -----------------------------------------------------------------------------
*/
var cor; 
function selecionaGrid(elemento) 
{ 
	cor=elemento.style.backgroundColor; 
	elemento.style.backgroundColor = '#d5e2ef'; 
}  

function deSelecionaGrid(elemento) 
{ 
	elemento.style.backgroundColor = cor; 
} 

/* 
''' -----------------------------------------------------------------------------
''' <summary>
''' Seleciona todos os checkboxes da mesma coluna da grid de permissões
''' </summary>
''' <example>
'''	onClick="return selecionarTodosCheckBox('L');"
''' </example>
''' <history>
''' 	[eneves]	16/4/2007		Created
''' </history>
''' -----------------------------------------------------------------------------
*/
function selecionarTodosCheckBox( pstrPermissao )
{
	 
	blnChecked = document.getElementById('chkHeader' + pstrPermissao).checked;
	objElements = document.forms[0].elements;
	
	for ( i = 0; i < objElements.length; i++ )
	{
	
		if ( objElements[i].type == 'checkbox' )
		{					
			if ( endsWith( objElements[i].name, 'chk' + pstrPermissao ) )
			{
				objElements[i].checked = blnChecked;
			}
		}
	}	
	
	return true;
	
}

/* 
''' -----------------------------------------------------------------------------
''' <summary>
''' Remove a seleção do checkbox do cabeçalho na lista de permissões
''' </summary>
''' <example>
'''	onClick="return removerSelecaoCheckBox('L');"
''' </example>
''' <history>
''' 	[eneves]	16/4/2007		Created
''' </history>
''' -----------------------------------------------------------------------------
*/
function removerSelecaoCheckBox( pstrPermissao )
{
	document.getElementById('chkHeader' + pstrPermissao).checked = false;
	return true;			
}


/* 
''' -----------------------------------------------------------------------------
''' <summary>
''' Remove a seleção do checkbox do cabeçalho na lista de permissões
''' </summary>
''' <example>
'''	onClick="return removerSelecaoCheckBox('L');"
''' </example>
''' <history>
''' 	[eneves]	16/4/2007		Created
''' </history>
''' -----------------------------------------------------------------------------
*/
function endsWith( pstrTexto, pstrFinal )
{
	var intStart = pstrTexto.length - pstrFinal.length;
	return (pstrTexto.substr(intStart) == pstrFinal);
}
