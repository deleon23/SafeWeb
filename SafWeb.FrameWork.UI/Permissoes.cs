using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualBasic.CompilerServices;
using System.Web;
using System.Web.UI;
using System.Runtime.InteropServices;
using Microsoft.VisualBasic;
using FrameWork.BusinessLayer.Usuarios;
using FrameWork.BusinessLayer.Utilitarios;

namespace SafWeb.FW.UI
{
    [StandardModule]
    internal sealed class Permissoes
    {
        // Methods
        public static bool AcessoAdm()
        {
            bool flag2 = false;
            if (HttpContext.Current.Items["objPermissoes"] != null)
            {
                if (HttpContext.Current.Items["objPermissoes"].ToString().IndexOf("PG") != -1)
                {
                    flag2 = true;
                }
                return flag2;
            }
            return true;
        }

        public static bool Alteração()
        {
            bool flag2 = false;
            if (HttpContext.Current.Items["objPermissoes"] != null)
            {
                if (HttpContext.Current.Items["objPermissoes"].ToString().IndexOf("A") != -1)
                {
                    flag2 = true;
                }
                return flag2;
            }
            return true;
        }

        public static void CarregaPermissoes(Page pobjPage, [Optional, DefaultParameterValue("")] string pstrFuncionalidade)
        {
            if (!Information.IsNothing(get_PageID(pobjPage)))
            {
                BLAcesso.CarregaAcesso(pobjPage.ToString(), pstrFuncionalidade);
                if (!Leitura() | (Convert.ToBoolean(((!Information.IsNothing(BLAcesso.ObterUsuario()) && (BLAcesso.ObterUsuario().SuperUser | BLAcesso.ObterUsuario().UserAdmin)) && !BLConfiguracao.Seguranca.VerificaSuperGrupo()) ? 1 : 0) & (pobjPage.ToString() == "ASP.CadSuperGrupo_aspx")))
                {
                    if (get_PageID(pobjPage) == "ASP.Admin_aspx")
                    {
                        if (decimal.Compare(BLAcesso.IdUsuarioLogado(), decimal.Zero) > 0)
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
                    else if (decimal.Compare(BLAcesso.IdUsuarioLogado(), decimal.Zero) > 0)
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

        public static void CarregaPermissoesGlobal(string pstrPage, HttpResponse pobjHttpResponse, HttpRequest pobjHttpRequest, string pstrRawUrl)
        {
            if (!Information.IsNothing(get_PageIDGlobal(pstrPage)))
            {
                BLAcesso.CarregaAcesso(pstrPage, pstrPage);
                if (
                    !Leitura() | 
                        (
                            Convert.ToBoolean(
                                (
                                    (!Information.IsNothing(BLAcesso.ObterUsuario()) && 
                                    (BLAcesso.ObterUsuario().SuperUser | BLAcesso.ObterUsuario().UserAdmin)
                                ) && !BLConfiguracao.Seguranca.VerificaSuperGrupo()
                            ) ? 1 : 0) & (pstrPage == "CadSuperGrupo")))
                {
                    if (pstrPage == "Admin")
                    {
                        if (decimal.Compare(BLAcesso.IdUsuarioLogado(), decimal.Zero) > 0)
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
                        else if (pobjHttpRequest.HttpMethod == "GET")
                        {
                            pobjHttpResponse.Redirect(BLDiretorios.AppPath() + "/Login.aspx?UrlRet=" + pstrRawUrl);
                        }
                        else
                        {
                            pobjHttpResponse.RedirectLocation = BLDiretorios.AppPath() + "/Login.aspx?UrlRet=" + pstrRawUrl;
                        }
                    }
                    else if (decimal.Compare(BLAcesso.IdUsuarioLogado(), decimal.Zero) > 0)
                    {
                        if (pstrPage.ToString() != "Default")
                        {
                            pobjHttpResponse.Redirect(BLConfiguracao.Sistema.ObterUrlPaginaAcesso());
                        }
                    }
                    else if (pobjHttpRequest.HttpMethod == "GET")
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

        public static bool Exclusão()
        {
            bool flag = false;
            if (HttpContext.Current.Items["objPermissoes"] != null)
            {
                if (HttpContext.Current.Items["objPermissoes"].ToString().IndexOf("E") != -1)
                {
                    flag = true;
                }
                return flag;
            }
            return true;
        }

        public static bool Inclusão()
        {
            bool flag = false;
            if (HttpContext.Current.Items["objPermissoes"] != null)
            {
                if (HttpContext.Current.Items["objPermissoes"].ToString().IndexOf("I") != -1)
                {
                    flag = true;
                }
                return flag;
            }
            return true;
        }

        public static bool Leitura()
        {
            bool flag = false;
            if (HttpContext.Current.Items["objPermissoes"] != null)
            {
                if (HttpContext.Current.Items["objPermissoes"].ToString().IndexOf("L") != -1)
                {
                    flag = true;
                }
                return flag;
            }
            return true;
        }

        //// Properties
        //public static string this[Page pobjPage]
        //{
        //    get
        //    {
        //        if (BLConfiguracao.Seguranca.VerificaAcessoDefault())
        //        {
        //            if ((((((((pobjPage.ToString() == "ASP.Acesso_aspx") | (pobjPage.ToString() == "ASP.AcessoAdm_aspx")) | (pobjPage.ToString() == "ASP.Erro_aspx")) | (pobjPage.ToString() == "ASP.ErroAdm_aspx")) | (pobjPage.ToString() == "ASP.ErroAjax_aspx")) | (pobjPage.ToString() == "ASP.ErroPopup_aspx")) | (pobjPage.ToString() == "ASP.Cadastro_aspx")) | (pobjPage.ToString() == "ASP.ErroAdm_aspx"))
        //            {
        //                return null;
        //            }
        //            return pobjPage.ToString();
        //        }
        //        if (((((((((pobjPage.ToString() == "ASP.Acesso_aspx") | (pobjPage.ToString() == "ASP.AcessoAdm_aspx")) | (pobjPage.ToString() == "ASP.Erro_aspx")) | (pobjPage.ToString() == "ASP.ErroAdm_aspx")) | (pobjPage.ToString() == "ASP.ErroAjax_aspx")) | (pobjPage.ToString() == "ASP.ErroPopup_aspx")) | (pobjPage.ToString() == "ASP.Default_aspx")) | (pobjPage.ToString() == "ASP.Cadastro_aspx")) | (pobjPage.ToString() == "ASP.ErroAdm_aspx"))
        //        {
        //            return null;
        //        }
        //        return pobjPage.ToString();
        //    }
        //}

        //public static string this[string pstrPage]
        //{
        //    get
        //    {
        //        if (BLConfiguracao.Seguranca.VerificaAcessoDefault())
        //        {
        //            if ((((((((pstrPage.ToLower() == "acesso") | (pstrPage.ToLower() == "acessoadm")) | (pstrPage.ToLower() == "erro")) | (pstrPage.ToLower() == "erroajax")) | (pstrPage.ToLower() == "erropopup")) | (pstrPage.ToLower() == "cadastro")) | (pstrPage.ToLower() == "login")) | (pstrPage.ToLower() == "erroadm"))
        //            {
        //                return null;
        //            }
        //            return pstrPage;
        //        }
        //        if (((((((((pstrPage.ToLower() == "acesso") | (pstrPage.ToLower() == "acessoadm")) | (pstrPage.ToLower() == "erro")) | (pstrPage.ToLower() == "erroajax")) | (pstrPage.ToLower() == "erropopup")) | (pstrPage.ToLower() == "default")) | (pstrPage.ToLower() == "cadastro")) | (pstrPage.ToLower() == "login")) | (pstrPage.ToLower() == "erroadm"))
        //        {
        //            return null;
        //        }
        //        return pstrPage;
        //    }
        //}

        public static string get_PageID(Page pobjPage)
        {
            if (BLConfiguracao.Seguranca.VerificaAcessoDefault())
            {
                if ((((((((pobjPage.ToString() == "ASP.Acesso_aspx") | (pobjPage.ToString() == "ASP.AcessoAdm_aspx")) | (pobjPage.ToString() == "ASP.Erro_aspx")) | (pobjPage.ToString() == "ASP.ErroAdm_aspx")) | (pobjPage.ToString() == "ASP.ErroAjax_aspx")) | (pobjPage.ToString() == "ASP.ErroPopup_aspx")) | (pobjPage.ToString() == "ASP.Cadastro_aspx")) | (pobjPage.ToString() == "ASP.ErroAdm_aspx"))
                {
                    return null;
                }
                return pobjPage.ToString();
            }
            if (((((((((pobjPage.ToString() == "ASP.Acesso_aspx") | (pobjPage.ToString() == "ASP.AcessoAdm_aspx")) | (pobjPage.ToString() == "ASP.Erro_aspx")) | (pobjPage.ToString() == "ASP.ErroAdm_aspx")) | (pobjPage.ToString() == "ASP.ErroAjax_aspx")) | (pobjPage.ToString() == "ASP.ErroPopup_aspx")) | (pobjPage.ToString() == "ASP.Default_aspx")) | (pobjPage.ToString() == "ASP.Cadastro_aspx")) | (pobjPage.ToString() == "ASP.ErroAdm_aspx"))
            {
                return null;
            }
            return pobjPage.ToString();
        }

        public static string get_PageIDGlobal(string pstrPage)
        {
            if (BLConfiguracao.Seguranca.VerificaAcessoDefault())
            {
                if ((((((((pstrPage.ToLower() == "acesso") | (pstrPage.ToLower() == "acessoadm")) | (pstrPage.ToLower() == "erro")) | (pstrPage.ToLower() == "erroajax")) | (pstrPage.ToLower() == "erropopup")) | (pstrPage.ToLower() == "cadastro")) | (pstrPage.ToLower() == "login")) | (pstrPage.ToLower() == "erroadm"))
                {
                    return null;
                }
                return pstrPage;
            }
            if (((((((((pstrPage.ToLower() == "acesso") | (pstrPage.ToLower() == "acessoadm")) | (pstrPage.ToLower() == "erro")) | (pstrPage.ToLower() == "erroajax")) | (pstrPage.ToLower() == "erropopup")) | (pstrPage.ToLower() == "default")) | (pstrPage.ToLower() == "cadastro")) | (pstrPage.ToLower() == "login")) | (pstrPage.ToLower() == "erroadm"))
            {
                return null;
            }
            return pstrPage;
        }



    }

}
