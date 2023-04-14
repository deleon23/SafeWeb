using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using SafWeb.BusinessLayer.Email;

namespace SafWeb.UI.Admin.Seguranca.Usuarios
{
    public partial class CadRegra : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BLEmail objBlEmail;
                DataTable dttHorarios = new DataTable();
                int i = 0;

                try
                {
                    objBlEmail = new BLEmail();

                    dttHorarios = objBlEmail.ListarHorarios();

                    foreach (DataRow dtr in dttHorarios.Rows)
                    {
                        i++;
                        
                        if(i == 1)
                        {
                            lblDesc1.Text = dtr["Des_HorarioAlerta"].ToString();
                            lblHora1.Text = dtr["Hor_Alerta"].ToString();
                            CheckBox1.ID = dtr["Id_HorarioAlerta"].ToString();
                        }
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
