/// ----------------------------------------------------------------------------- 
/// <summary> 
/// Module de Controle de Idiomas 
/// </summary> 
/// <history> 
/// [eneves] 12/3/2007 Created 
/// </history> 
/// ----------------------------------------------------------------------------- 
internal sealed class ControleIdioma 
{
    private static bool blnMostraControleIdioma = true;

    public void tt()
    {
        
    }
    /// ----------------------------------------------------------------------------- 
    /// <summary> 
    /// Verifica se mostra o controle de idiomas 
    /// </summary> 
    /// <value>Boolean</value> 
    /// <history> 
    /// [eneves] 14/5/2007 Created 
    /// </history> 
    /// ----------------------------------------------------------------------------- 
    public static bool MostraControleIdioma
    {
        get { return blnMostraControleIdioma; }
        set { blnMostraControleIdioma = value; }
    }

}