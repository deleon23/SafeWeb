using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using SafWeb.BusinessLayer.Colaborador;

namespace SafWeb.UI.Modulos.Portaria
{
    public partial class ImageConversions : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CreatePhoto();
        }

        void CreatePhoto()
        {
            try
            {
                // Recebe o idVisitante e a Imagem do Flash
                string strIdVisitante = Request.Form["idVisitante"];
                string strPhoto = Request.Form["imageData"];
                double dFusoHorario = Convert.ToDouble(Request.Form["FusoHorario"].ToString());
                double FusoHorario = Convert.ToDouble(Request.Form["FusoHorario"].ToString());
                byte[] photo = Convert.FromBase64String(strPhoto);

                BLColaborador objBLColaborador = new BLColaborador();
                bool sucesso = objBLColaborador.InserirFotoVisitante(Convert.ToInt32(strIdVisitante), photo, DateTime.UtcNow.AddHours(dFusoHorario));

                if (sucesso)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "OK", "window.alert('Foto gravada com sucesso!');", true);
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "OK", "window.alert('Houve um erro ao gravar a foto');", true);
                }
            }
            catch (Exception Ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "OK", "window.alert('Houve um erro ao gravar a foto');", true);
            }
        }
    }
}