using System.Web;
using System.Web.UI;
using FrameWork.BusinessLayer.Utilitarios;
using FrameWork.BusinessLayer.Idioma;
using FrameWork.Model.Idioma;
using FrameWork.Model.Utilitarios;
using FrameWork.BusinessLayer.Usuarios;
using System;

using Microsoft.VisualBasic;
using System.Web.UI.HtmlControls;

/// ----------------------------------------------------------------------------- 
/// Project : FrameWork.UI 
/// Class : UI.FWPage 
/// 
/// ----------------------------------------------------------------------------- 
/// <summary> 
/// Classe de Herança para as criações de páginas 
/// </summary> 
/// <history> 
/// [eneves] 18/4/2007 Created 
/// </history> 
/// ----------------------------------------------------------------------------- 
public class FWPage : System.Web.UI.Page
{

    private const string strScript = "Script";

    protected override void OnInit(EventArgs e)
    {
        string[] arrURL = Request.ServerVariables["URL"].Split('/');

        if (arrURL[arrURL.Length - 1] == "CadEscalaHorarioColaborador.aspx")
        {
            ScriptManager sm = ScriptManager.GetCurrent(this.Page);
            if (sm == null)
            {
                Control objControl = this.FindControl("form");

                if (objControl != null)
                {
                    sm = new ScriptManager();
                    sm.ID = "scriptManager";
                    objControl.Controls.Add(sm);
                }
            }
        }
    }


    //#region "Zip/Unzip ViewState"

    ///// ----------------------------------------------------------------------------- 
    ///// <summary> 
    ///// Overrides LoadPageStateFromPersistenceMedium 
    ///// </summary> 
    ///// <remarks>Metodo para zipar/unzipar o objeto viewstate 
    ///// </remarks> 
    ///// <history> 
    ///// [eneves] 31/10/2005 Created 
    ///// </history> 
    ///// ---------------------------------------------------------------------------- 
    //protected override object LoadPageStateFromPersistenceMedium()
    //{
    //    LosFormatter objformatter = new LosFormatter();
    //    if (!BLConfiguracao.Sistema.VerificaCompressViewState())
    //    {
    //        return base.LoadPageStateFromPersistenceMedium();
    //    }
    //    else
    //    {
    //        string vsString = Request.Form["__FW_VIEWSTATE"];
    //        string outStr = vsString;
    //        return objformatter.Deserialize(outStr);
    //    }

    //}

    ///// ----------------------------------------------------------------------------- 
    ///// <summary> 
    ///// Overrides SavePageStateToPersistenceMedium 
    ///// </summary> 
    ///// <param name="viewState">objeto viewstate</param> 
    ///// <remarks>Metodo para zipar/unzipar o objeto viewstate 
    ///// </remarks> 
    ///// <history> 
    ///// [eneves] 31/10/2005 Created 
    ///// </history> 
    ///// ----------------------------------------------------------------------------- 
    //protected override void SavePageStateToPersistenceMedium(object viewState)
    //{
    //    LosFormatter objformatter = new LosFormatter();
    //    if (!BLConfiguracao.Sistema.VerificaCompressViewState())
    //    {
    //        base.SavePageStateToPersistenceMedium(viewState);
    //    }
    //    else
    //    {
    //        System.IO.StringWriter sw = new System.IO.StringWriter();
    //        objformatter.Serialize(sw, viewState);
    //        string outStr = sw.ToString();
    //        Page.ClientScript.RegisterHiddenField("__FW_VIEWSTATE", outStr);
    //    }
    //}

    //#endregion

    #region "Renderiza Página"
    /// ----------------------------------------------------------------------------- 
    /// <summary> 
    /// Registrar Script do DataGrid e Menu 
    /// </summary> 
    /// <param name="e">EventArgs</param> 
    /// <history> 
    /// [eneves] 25/8/2006 Created 
    /// </history> 
    /// ----------------------------------------------------------------------------- 
    protected override void OnPreRender(System.EventArgs e)
    {
        if (!this.Page.ClientScript.IsClientScriptBlockRegistered(strScript + "MascaraDataGrid"))
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), strScript + "MascaraDataGrid", "<script language=\"javascript\" src=\"%%PATH%%/Scripts/DataGrid.js\"></script>");
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), strScript + "Menu1", "<SCRIPT language=\"JavaScript\" type=text/javascript>scriptpath='%%PATH%%/scripts/'</SCRIPT> ");
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), strScript + "Menu2", "<SCRIPT language=JavaScript src=\"%%PATH%%/scripts/menuctrl.js\" type=text/javascript></script> ");
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), strScript + "Menu3", "<SCRIPT language=JavaScript type=text/javascript>_menuCloseDelay = 500;_menuOpenDelay = 150;_followSpeed = 5;_followRate = 40;_subOffsetLeft = -5;_subOffsetTop = 5;_scrollAmount = 3;_scrollDelay = 20</SCRIPT>");

            if (BLConfiguracao.Sistema.VerificaInterfaceExportacao() == Enums.TipoInterfaceExportacao.Exportacao)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), strScript + "RadAjax.Net2", "<script language=javascript type=text/javascript>" + "function OnRequestStart(ajaxPanel, eventArgs){ " + "var eventTarget = eventArgs.EventTarget; " + "if (eventTarget == 'btnExportWord') " + "{ eventArgs.EnableAjax = false;" + " return true;\t}" + " if (eventTarget == 'btnExportExcel')" + "{ eventArgs.EnableAjax = false; return true;" + "} else { return true; }} </script>");
            }

        }

        base.OnPreRender(e);
    }

    /// ----------------------------------------------------------------------------- 
    /// <summary> 
    /// Renderização da Página 
    /// </summary> 
    /// <param name="writer">Conteudo HTML</param> 
    /// <remarks>Troca os render da página 
    /// </remarks> 
    /// <history> 
    /// [eneves] 8/12/2005 Created 
    /// </history> 
    /// ----------------------------------------------------------------------------- 
    protected override void Render(System.Web.UI.HtmlTextWriter writer)
    {
        System.IO.StringWriter objStringWriter = new System.IO.StringWriter();
        base.Render(new HtmlTextWriter((System.IO.TextWriter)objStringWriter));
        string textStringWriter = objStringWriter.ToString();
        string textStringWriterRetorno;
        textStringWriterRetorno = BLRenderizacaoSite.AcertaData(textStringWriter);
        textStringWriterRetorno = BLRenderizacaoSite.AcertaTituloSite(textStringWriterRetorno);
        textStringWriterRetorno = BLRenderizacaoSite.AcertaPath(textStringWriterRetorno);
        textStringWriterRetorno = BLRenderizacao.AcertaUsuario(textStringWriterRetorno);
        //mensagem de exclusão de registro do radgrid 
        textStringWriterRetorno = textStringWriterRetorno.Replace("%%REGISTRO_CONFIRMA_EXCLUSAO%%", BLIdiomas.TraduzirMensagens(Mensagens.REGISTRO_CONFIRMA_EXCLUSAO));
        writer.Write(textStringWriterRetorno);
    }

    #endregion

    #region "Tratamento de Erro"

    /// ----------------------------------------------------------------------------- 
    /// <summary> 
    /// Tratamento de Erro 
    /// </summary> 
    /// <param name="e">EventArgs</param> 
    /// <history> 
    /// [eneves] 30/5/2007 Created 
    /// </history> 
    /// ----------------------------------------------------------------------------- 
    protected override void OnError(System.EventArgs e)
    {
        HttpContext objHttpContext = HttpContext.Current;
        ExceptionHandler.TrataErro(objHttpContext.Server.GetLastError(), TipoMensagem.Adm, this.Page);
        objHttpContext.Server.ClearError();
        base.OnError(e);
    }

    #endregion

    #region "Property"
    /// <summary>
    /// <value>Código da Página Atual</value>
    /// </summary>
    public decimal PageId
    {
        get
        {
            if (Request.QueryString["pageid"] == null || Request.QueryString["pageid"] == string.Empty)
            {
                return 0;// ContentManager.BusinessLayer.Utilitarios.BLParametroSistema.Obter().IdPaginaHome;      
            }
            else
            {

                if (IsNumeric(FrameWork.BusinessLayer.Utilitarios.BLEncriptacao.DecString(Request.QueryString["pageid"])))
                {
                    return 0;//Convert.ToDecimal(ContentManager.BusinessLayer.Utilitarios.BLEncriptacao.DecQueryStr(Request.QueryString["pageid"].ToString()));
                }
                else
                {
                    return 0;// ContentManager.BusinessLayer.Utilitarios.BLParametroSistema.Obter().IdPaginaHome;
                }
            }
        }
        set
        {
            this.ViewState.Add("vsIdPagId", value);
        }
    }

    /// <summary>
    ///Script Exportacao
    ///</summary>
    ///<value>Boolean</value>
    ///<history>
    ///[Carlos Eduardo Neves]	15/2/2008	Created
    ///</history>
    /// </summary>
    /// 
    public Boolean ScriptExportacao
    {
        get
        {
            if (this.ViewState["vsScriptExportacao"] != null)
            {
                return Convert.ToBoolean(this.ViewState["vsScriptExportacao"]);
            }
            else
            {
                return true;
            }
        }

        set
        {
            this.ViewState.Add("vsScriptExportacao", value);

        }
    }

    #endregion

    #region IsNumeric
    // IsNumeric Function
    static bool IsNumeric(object Expression)
    {
        // Variable to collect the Return value of the TryParse method.
        bool isNum;

        // Define variable to collect out parameter of the TryParse method. If the conversion fails, the out parameter is zero.
        double retNum;

        // The TryParse method converts a string in a specified style and culture-specific format to its double-precision floating point number equivalent.
        // The TryParse method does not generate an exception if the conversion fails. If the conversion passes, True is returned. If it does not, False is returned.
        isNum = Double.TryParse(Convert.ToString(Expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
        return isNum;
    }
    #endregion

}
