using System.Web;
using System.Web.UI;
using FrameWork.BusinessLayer.Utilitarios;
using FrameWork.BusinessLayer.Usuarios;
using FrameWork.Model.Usuarios;  

/// ----------------------------------------------------------------------------- 
/// <summary> 
/// Module de Permissões 
/// </summary> 
/// <history> 
/// [eneves] 17/10/2006 Created 
/// </history> 
/// ----------------------------------------------------------------------------- 
internal sealed class Permissoes
{

    #region "Carrega Permissões"

    /// ----------------------------------------------------------------------------- 
    /// <summary> 
    /// Carrega Permissões Global Asax 
    /// </summary> 
    /// <param name="pstrPage">Nome da Página</param> 
    /// <param name="pobjHttpResponse">HttpResponse</param> 
    /// <param name="pobjHttpRequest">HttpRequest</param> 
    /// <param name="pstrRawUrl">RawUrl</param> 
    /// <history> 
    /// [eneves] 18/10/2006 Created 
    /// </history> 
    /// ----------------------------------------------------------------------------- 
    public static void CarregaPermissoesGlobal(string pstrPage, HttpResponse pobjHttpResponse, HttpRequest pobjHttpRequest, string pstrRawUrl)
    {

        if ((PageIDGlobal(pstrPage) != null))
        {
            BLAcesso.CarregaAcesso(pstrPage, pstrPage);
            if (!Permissoes.Leitura() | (((BLAcesso.ObterUsuario() != null) && (BLAcesso.ObterUsuario().SuperUser == true | BLAcesso.ObterUsuario().UserAdmin == true)) && !BLConfiguracao.Seguranca.VerificaSuperGrupo() & pstrPage == "CadSuperGrupo"))
            {
                if (pstrPage == "Admin")
                {
                    if (BLAcesso.IdUsuarioLogado() > 0)
                    {
                        if (!BLConfiguracao.Seguranca.VerificaAcessoAdmRetornoDefault())
                        {
                            pobjHttpResponse.Redirect(BLConfiguracao.Sistema.ObterUrlPaginaAcessoAdm());
                        }
                        else
                        {
                            pobjHttpResponse.Redirect(BLDiretorios.AppPath() + "/Default.aspx");
                        }
                    }
                    else
                    {
                        if (pobjHttpRequest.HttpMethod == "GET")
                        {
                            pobjHttpResponse.Redirect(BLDiretorios.AppPath() + "/Login.aspx?UrlRet=" + pstrRawUrl);
                        }
                        else
                        {
                            pobjHttpResponse.RedirectLocation = BLDiretorios.AppPath() + "/Login.aspx?UrlRet=" + pstrRawUrl;
                        }
                    }
                }
                else
                {
                    if (BLAcesso.IdUsuarioLogado() > 0)
                    {
                        if (pstrPage.ToString() != "Default")
                        {
                            pobjHttpResponse.Redirect(BLConfiguracao.Sistema.ObterUrlPaginaAcesso());
                        }
                    }
                    else
                    {
                        if (pobjHttpRequest.HttpMethod == "GET")
                        {
                            pobjHttpResponse.Redirect(BLDiretorios.AppPath() + "/Login.aspx?UrlRet=" + pstrRawUrl);
                        }
                        else
                        {
                            pobjHttpResponse.RedirectLocation = BLDiretorios.AppPath() + "/Login.aspx?UrlRet=" + pstrRawUrl;
                        }
                    }
                }
            }
        }
    }

    /// ----------------------------------------------------------------------------- 
    /// <summary> 
    /// Carrega Permissões 
    /// </summary> 
    /// <param name="pobjPage">Web Page</param> 
    /// <param name="pstrFuncionalidade">Nome da Funcionalidade</param> 
    /// <history> 
    /// [eneves] 18/10/2006 Created 
    /// </history> 
    /// ----------------------------------------------------------------------------- 
    public static void CarregaPermissoes(Page pobjPage, string pstrFuncionalidade)
    {

        if ((PageID(pobjPage) != null))
        {
            BLAcesso.CarregaAcesso(pobjPage.ToString(), pstrFuncionalidade);
            if (!Permissoes.Leitura() | (((BLAcesso.ObterUsuario() != null) && (BLAcesso.ObterUsuario().SuperUser == true | BLAcesso.ObterUsuario().UserAdmin == true)) && !BLConfiguracao.Seguranca.VerificaSuperGrupo() & pobjPage.ToString() == "ASP.CadSuperGrupo_aspx"))
            {

                if (PageID(pobjPage) == "ASP.Admin_aspx")
                {
                    if (BLAcesso.IdUsuarioLogado() > 0)
                    {
                        if (!BLConfiguracao.Seguranca.VerificaAcessoAdmRetornoDefault())
                        {
                            pobjPage.Response.Redirect(BLConfiguracao.Sistema.ObterUrlPaginaAcessoAdm());
                        }
                        else
                        {
                            pobjPage.Response.Redirect(BLDiretorios.AppPath() + "/Default.aspx");
                        }
                    }
                    else
                    {
                        pobjPage.Response.Redirect(BLDiretorios.AppPath() + "/Login.aspx?UrlRet=" + pobjPage.Request.RawUrl.ToString());
                    }
                }
                else
                {
                    if (BLAcesso.IdUsuarioLogado() > 0)
                    {
                        if (pobjPage.ToString() != "ASP.Default_aspx")
                        {
                            pobjPage.Response.Redirect(BLConfiguracao.Sistema.ObterUrlPaginaAcesso());
                        }
                    }
                    else
                    {
                        pobjPage.Response.Redirect(BLDiretorios.AppPath() + "/Login.aspx?UrlRet=" + pobjPage.Request.RawUrl.ToString());
                    }
                }
            }
        }
    }

    #endregion

    #region "Inclusão"

    /// ----------------------------------------------------------------------------- 
    /// <summary> 
    /// Controla permissões de inclusão 
    /// </summary> 
    /// <returns>Boolean</returns> 
    /// <history> 
    /// [eneves] 12/3/2007 Created 
    /// </history> 
    /// ----------------------------------------------------------------------------- 
    public static bool Inclusão()
    {
        bool blnRetorno = false;
        string auxPermissoes;

        if ((HttpContext.Current.Items["objPermissoes"] != null))
        {
            auxPermissoes = HttpContext.Current.Items["objPermissoes"].ToString();
            if (auxPermissoes.IndexOf("I") != -1)
            {
                blnRetorno = true;
            }
        }
        else
        {
            blnRetorno = true;
        }
        return blnRetorno;
    }

    #endregion

    #region "Leitura"

    /// ----------------------------------------------------------------------------- 
    /// <summary> 
    /// Controla permissões de leitura 
    /// </summary> 
    /// <returns>Boolean</returns> 
    /// <history> 
    /// [eneves] 12/3/2007 Created 
    /// </history> 
    /// ----------------------------------------------------------------------------- 
    public static bool Leitura()
    {
        bool blnRetorno = false;
        string auxPermissoes;

        if ((HttpContext.Current.Items["objPermissoes"] != null))
        {
            auxPermissoes = HttpContext.Current.Items["objPermissoes"].ToString();
            if (auxPermissoes.IndexOf("L") != -1)
            {
                blnRetorno = true;
            }
        }
        else
        {
            blnRetorno = true;
        }
        return blnRetorno;
    }

    #endregion

    #region "Exclusão"

    /// ----------------------------------------------------------------------------- 
    /// <summary> 
    /// Controla permissões de exclusão 
    /// </summary> 
    /// <returns>Boolean</returns> 
    /// <history> 
    /// [eneves] 12/3/2007 Created 
    /// </history> 
    /// ----------------------------------------------------------------------------- 
    public static bool Exclusão()
    {
        bool blnRetorno = false;
        string auxPermissoes;

        if ((HttpContext.Current.Items["objPermissoes"] != null))
        {
            auxPermissoes = HttpContext.Current.Items["objPermissoes"].ToString();
            if (auxPermissoes.IndexOf("E") != -1)
            {
                blnRetorno = true;
            }
        }
        else
        {
            blnRetorno = true;
        }
        return blnRetorno;
    }

    #endregion

    #region "Alteração"

    /// ----------------------------------------------------------------------------- 
    /// <summary> 
    /// Controla permissões de alteração 
    /// </summary> 
    /// <returns>Boolean</returns> 
    /// <history> 
    /// [eneves] 12/3/2007 Created 
    /// </history> 
    /// ----------------------------------------------------------------------------- 
    public static bool Alteração()
    {
        bool blnRetorno = false;
        string auxPermissoes;

        if ((HttpContext.Current.Items["objPermissoes"] != null))
        {
            auxPermissoes = HttpContext.Current.Items["objPermissoes"].ToString();
            if (auxPermissoes.IndexOf("A") != -1)
            {
                blnRetorno = true;
            }
        }
        else
        {
            blnRetorno = true;
        }
        return blnRetorno;
    }

    #endregion

    #region "Acesso Adm"

    /// ----------------------------------------------------------------------------- 
    /// <summary> 
    /// Controla acesso à area administrativa 
    /// </summary> 
    /// <returns>Boolean</returns> 
    /// <history> 
    /// [eneves] 12/3/2007 Created 
    /// </history> 
    /// ----------------------------------------------------------------------------- 
    public static bool AcessoAdm()
    {
        bool blnRetorno = false;
        string auxPermissoes;

        if ((HttpContext.Current.Items["objPermissoes"] != null))
        {
            auxPermissoes = HttpContext.Current.Items["objPermissoes"].ToString();
            if (auxPermissoes.IndexOf("PG") != -1)
            {
                blnRetorno = true;
            }
        }
        else
        {
            blnRetorno = true;
        }
        return blnRetorno;
    }

    #endregion

    #region "Property"
    /// ----------------------------------------------------------------------------- 
    /// <summary> 
    /// PageID 
    /// </summary> 
    /// <value>String</value> 
    /// <history> 
    /// [eneves] 10/4/2007 Created 
    /// </history> 
    /// ----------------------------------------------------------------------------- 
    public static string PageID(Page pobjPage)
    {
        if (BLConfiguracao.Seguranca.VerificaAcessoDefault())
        {
            if (pobjPage.ToString() == "ASP.Acesso_aspx" |
                pobjPage.ToString() == "ASP.AcessoAdm_aspx" |
                pobjPage.ToString() == "ASP.Erro_aspx" |
                pobjPage.ToString() == "ASP.ErroAdm_aspx" |
                pobjPage.ToString() == "ASP.ErroAjax_aspx" |
                pobjPage.ToString() == "ASP.ErroPopup_aspx" |
                pobjPage.ToString() == "ASP.Cadastro_aspx" |
                pobjPage.ToString() == "ASP.ErroAdm_aspx")
            {
                return null;
            }
            else
            {
                return pobjPage.ToString();
            }
        }
        else
        {
            if (pobjPage.ToString() == "ASP.Acesso_aspx" |
                pobjPage.ToString() == "ASP.AcessoAdm_aspx" |
                pobjPage.ToString() == "ASP.Erro_aspx" |
                pobjPage.ToString() == "ASP.ErroAdm_aspx" |
                pobjPage.ToString() == "ASP.ErroAjax_aspx" |
                pobjPage.ToString() == "ASP.ErroPopup_aspx" |
                pobjPage.ToString() == "ASP.Default_aspx" |
                pobjPage.ToString() == "ASP.Cadastro_aspx" |
                pobjPage.ToString() == "ASP.ErroAdm_aspx")
            {
                return null;
            }
            else
            {
                return pobjPage.ToString();
            }
        }
    }

    /// ----------------------------------------------------------------------------- 
    /// <summary> 
    /// PageID para o global Asax 
    /// </summary> 
    /// <value>String</value> 
    /// <history> 
    /// [eneves] 10/4/2007 Created 
    /// </history> 
    /// ----------------------------------------------------------------------------- 
    public static string PageIDGlobal(string pstrPage)
    {
        if (BLConfiguracao.Seguranca.VerificaAcessoDefault())
        {
            if (pstrPage.ToLower() == "acesso" |
                pstrPage.ToLower() == "acessoadm" |
                pstrPage.ToLower() == "erro" |
                pstrPage.ToLower() == "erroajax" |
                pstrPage.ToLower() == "erropopup" |
                pstrPage.ToLower() == "cadastro" |
                pstrPage.ToLower() == "login" |
                pstrPage.ToLower() == "erroadm")
            {
                return null;
            }
            else
            {
                return pstrPage;
            }
        }
        else
        {
            if (pstrPage.ToLower() == "acesso" |
                pstrPage.ToLower() == "acessoadm" |
                pstrPage.ToLower() == "erro" |
                pstrPage.ToLower() == "erroajax" |
                pstrPage.ToLower() == "erropopup" |
                pstrPage.ToLower() == "default" |
                pstrPage.ToLower() == "cadastro" |
                pstrPage.ToLower() == "login" |
                pstrPage.ToLower() == "erroadm")
            {
                return null;
            }
            else
            {
                return pstrPage;
            }
        }
    }
    #endregion

}

