using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SafWeb.BusinessLayer.Colaborador;
using System.IO;

namespace SafWeb.UI.Modulos.Portaria
{
    public partial class ImageFoto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Request != null) && (Request.QueryString["ImageByte"] != null))
            {
                int idTipoVisitante = 0;
                if(Request.QueryString["idTipoVisitante"]!=null)
                {
                    idTipoVisitante = Convert.ToInt32(Request.QueryString["idTipoVisitante"]);
                }

                BLColaborador objBlColaborador = new BLColaborador();
                try
                {
                    Model.Colaborador.Colaborador gobjColaborador = objBlColaborador.ObterFoto(Convert.ToInt32(Request.QueryString["ImageByte"]), idTipoVisitante);

                    if (gobjColaborador.Imagem != null)
                    {
                        Response.Buffer = true;
                        Response.Charset = "";
                        Response.Cache.SetCacheability(HttpCacheability.NoCache);
                        Response.ContentType = "image/jpeg";
                        Response.BinaryWrite(gobjColaborador.Imagem);
                        Response.Flush();
                    }
                    else
                    {
                        byte[] imgdata = System.IO.File.ReadAllBytes(HttpContext.Current.Server.MapPath("../../Imagens/SemFoto.jpg"));
                        Response.Buffer = true;
                        Response.Charset = "";
                        Response.Cache.SetCacheability(HttpCacheability.NoCache);
                        Response.ContentType = "image/jpeg";
                        Response.BinaryWrite(imgdata);
                        Response.Flush();
                    }
                }
                catch (Exception ex)
                {
                    ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
                }
            }




        }
    }
}