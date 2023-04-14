using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FrameWork.BusinessLayer.Utilitarios;
using FrameWork.Model.Utilitarios;
using System.Data.SqlClient;
using Telerik.WebControls;

#region Enums

/// ----------------------------------------------------------------------------- 
/// <summary> 
/// Tipo Mensagem 
/// </summary> 
/// <history> 
/// [eneves] 28/8/2006 Created 
/// [cveber] 08/08/2008 
/// </history>  
/// ----------------------------------------------------------------------------- 
public enum TipoMensagem : byte
{
    Adm = 0,
    Popup = 1,
    Publico = 2
}

#endregion

/// ----------------------------------------------------------------------------- 
/// <summary> 
/// Módulo responsável pelo gerenciamento de erro na aplicação 
/// </summary> 
/// <history> 
/// [eneves] 27/11/2005 Created 
/// [cveber] 08/08/2008
/// [resquicato] 29/08/2008 Modify
/// </history> 
/// ----------------------------------------------------------------------------- 
internal sealed class ExceptionHandler
{

    /// ----------------------------------------------------------------------------- 
    /// <summary> 
    /// Recebe a exceção como parâmentro e procura um erro amigável na tabela 
    /// de sistema ERR_ERRO_DESCRICAO 
    /// </summary> 
    /// <param name="pobjException">Exception</param> 
    /// <param name="penmTipoMensagem">Enum Tipo Mensagem</param> 
    /// <param name="pobjPage">WebPage</param> 
    /// <remarks>pstrTipoMensagem = "Popup" - abre mensagem sem o menu e pagebanner 
    /// </remarks> 
    /// <history> 
    /// [eneves] 27/11/2005 Created 
    /// [cveber] 08/08/2008 Modify
    /// [resquicato] 29/08/2008 Modify
    /// </history> 
    /// ----------------------------------------------------------------------------- 
    public static void TrataErro(Exception pobjException, TipoMensagem penmTipoMensagem, Page pobjPage)
    {
        if (!BLConfiguracao.Sistema.VerificaExibirErrosDebug()) return;

        ErroDescricao objErroDescricao = new ErroDescricao();

        // Erros SQL
        if (ReferenceEquals(pobjException.GetType(), typeof(SqlException)))
        {
            SqlErrorCollection   objErros = ((SqlException)pobjException).Errors;

            objErroDescricao.Codigo = objErros[0].Number.ToString();
            // objErroDescricao.Tipo = "SQLServer";

            // Consulta mensagem de erro amigável
            BLFuncoes.MensagemErro(ref objErroDescricao);

            // Usa mensagem original se não encontrar nenhum resultado
            if (objErroDescricao.Mensagem == null)
                objErroDescricao.Mensagem = objErros[0].Message;

            objErroDescricao.MensagemOriginal = objErros[0].Message;
        }
        // Outros erros
        else
        {
            objErroDescricao.Mensagem = pobjException.Message;
            objErroDescricao.MensagemOriginal = pobjException.Message;
        }

        objErroDescricao.StackTrace = pobjException.StackTrace;

        // Atualiza HttpContext com erro atual
        if (HttpContext.Current.Items.Contains("objErroDescricao"))
            HttpContext.Current.Items.Remove("objErroDescricao");

        HttpContext.Current.Items.Add("objErroDescricao", objErroDescricao);

        // Se for informado página
        if ((pobjPage != null))
        {
            if (pobjPage.IsPostBack)
            {
                if (objErroDescricao.Codigo == "17")
                    HttpContext.Current.Response.RedirectLocation = BLDiretorios.AppPath() + "/manutencao.htm?msg=DBACCESSNOK";

                RadWindow rwdWindow = new RadWindow();
                RadWindowManager rwmRadWindowManager = new RadWindowManager();

                rwdWindow.Width = Unit.Pixel(600);
                rwdWindow.Height = Unit.Pixel(500);
                rwdWindow.Modal = true;
                rwdWindow.ReloadOnShow = false;
                rwdWindow.DestroyOnClose = true;
                rwdWindow.VisibleOnPageLoad = true;

                pobjPage.Session.Add("objErroDescricao", objErroDescricao);

                rwdWindow.NavigateUrl = BLConfiguracao.Sistema.ObterUrlPaginaErroAjax();
                rwmRadWindowManager.Windows.Add(rwdWindow);

                Panel pnlErro = null;

                //Tenta encontrar na master
                pnlErro = (Panel)pobjPage.FindControl("pnlErro");                
                pnlErro.Controls.Add(rwmRadWindowManager);                
            }
            else
            {
                if (objErroDescricao.Codigo == "17")
                {
                    HttpContext.Current.Response.Redirect(BLDiretorios.AppPath() + "/manutencao.htm?msg=DBACCESSNOK");
                }
                else
                {
                    switch (penmTipoMensagem)
                    {
                        case TipoMensagem.Adm:
                            HttpContext.Current.Server.Transfer(BLConfiguracao.Sistema.ObterUrlPaginaErroAdm());
                            break;

                        case TipoMensagem.Popup:
                            HttpContext.Current.Server.Transfer(BLConfiguracao.Sistema.ObterUrlPaginaErroPopup());
                            break;

                        default:
                            HttpContext.Current.Server.Transfer(BLConfiguracao.Sistema.ObterUrlPaginaErro());
                            break;
                    }

                }
            }
        }
        else
        {
            if (objErroDescricao.Codigo == "17")
            {
                HttpContext.Current.Response.Redirect(BLDiretorios.AppPath() + "/manutencao.htm?msg=DBACCESSNOK");
            }
            else
            {
                switch (penmTipoMensagem)
                {
                    case TipoMensagem.Adm:
                        HttpContext.Current.Server.Transfer(BLConfiguracao.Sistema.ObterUrlPaginaErroAdm());
                        break;

                    case TipoMensagem.Popup:
                        HttpContext.Current.Server.Transfer(BLConfiguracao.Sistema.ObterUrlPaginaErroPopup());
                        break;

                    default:
                        HttpContext.Current.Server.Transfer(BLConfiguracao.Sistema.ObterUrlPaginaErro());
                        break;
                }
            }
        }
    }
}