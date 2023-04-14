
// Desabilita os objetos da página
function DisableControls()
{
    for (var i=0; i<document.forms[0].elements.length; i++)
    {            
        var obj = document.forms[0].elements[i];
        obj.disabled = true;
    }
}

// Habilita os objetos da página
function EnableControls()
{
    for (var i=0; i<document.forms[0].elements.length; i++)
    {        
        var obj = document.forms[0].elements[i];
        obj.disabled = false;
    }
}