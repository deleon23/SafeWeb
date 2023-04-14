using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.ComponentModel;
using FrameWork.BusinessLayer.Utilitarios;
using FrameWork.BusinessLayer.Usuarios;
using FrameWork.Model.Utilitarios;
using FrameWork.BusinessLayer.Idioma;
using Microsoft.VisualBasic.CompilerServices;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace SafWeb.FW.UI
{
    public class Global : HttpApplication
    {
        // Fields
        private static List<WeakReference> __ENCList = new List<WeakReference>();
        private IContainer components;

        // Methods
        public Global()
        {
            base.AuthorizeRequest += new EventHandler(this.Global_AuthorizeRequest);
            List<WeakReference> list = __ENCList;
            lock (list)
            {
                __ENCList.Add(new WeakReference(this));
            }
            this.InitializeComponent();
        }

        public void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            string[] segments = this.Request.Url.Segments;
            int index = segments.Length - 1;

            Regex ER = new Regex(@".*?\.[a-zA-Z]{2,4}$", RegexOptions.IgnoreCase);
            if (!ER.IsMatch(segments[index]))
            {
                index--;
            }

            if (segments[index].Split(".".ToCharArray())[1].ToLower() != "axd")
            {
                if (BLConfiguracao.Seguranca.VerificaTipoAuthentication() == Enums.TipoAuthentication.Form)
                {
                    BLAutenticacao.Autentica(false);
                }
                else
                {
                    BLAutenticacao.Autentica(true);
                }
                BLIdiomas.ObterIdioma();
                BLConfig.GravaTraceAcesso(BLAcesso.IdUsuarioLogado());
            }
        }

        public void Application_BeginRequest(object sender, EventArgs e)
        {
            BLConfig.InicializaRequest();
        }

        public void Application_End(object sender, EventArgs e)
        {
        }

        public void Application_Error(object sender, EventArgs e)
        {
        }

        public void Application_Start(object sender, EventArgs e)
        {
            BLConfig.MontaConfiguracao();
        }

        private void Global_AuthorizeRequest(object sender, EventArgs e)
        {
            if ((HttpContext.Current != null) && HttpContext.Current.Request.IsAuthenticated)
            {
                try
                {
                    string[] segments = this.Request.Url.Segments;
                    int index = segments.Length - 1;
                    if (segments[index].Split(".".ToCharArray())[1].ToLower() != "axd")
                    {
                        Permissoes.CarregaPermissoesGlobal(segments[index].Split(".".ToCharArray())[0], this.Response, this.Request, this.Request.RawUrl.ToString());
                    }
                }
                catch (Exception exception1)
                {
                    ProjectData.SetProjectError(exception1);
                    Exception exception = exception1;
                    ProjectData.ClearProjectError();
                }
            }
            else if (BLConfiguracao.Seguranca.VerificaTipoAuthentication() == Enums.TipoAuthentication.Form)
            {
                try
                {
                    string[] strArray2 = this.Request.Url.Segments;
                    int num2 = strArray2.Length - 1;
                    if (strArray2[num2].Split(".".ToCharArray())[1].ToLower() != "axd")
                    {
                        Permissoes.CarregaPermissoesGlobal(strArray2[num2].Split(".".ToCharArray())[0], this.Response, this.Request, this.Request.RawUrl.ToString());
                    }
                }
                catch (Exception exception3)
                {
                    ProjectData.SetProjectError(exception3);
                    Exception exception2 = exception3;
                    ProjectData.ClearProjectError();
                }
            }
        }

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            this.components = new Container();
        }

        public void Session_End(object sender, EventArgs e)
        {
        }

        public void Session_Start(object sender, EventArgs e)
        {
        }
    }



}
