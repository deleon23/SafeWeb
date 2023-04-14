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
using SafWeb.BusinessLayer.Solicitacao;
using FrameWork.BusinessLayer.Utilitarios;

namespace SafWeb.UI.Modulos.Portaria
{
    public partial class ListLista : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Bind();
        }

        protected void Bind()
        {
            BLSolicitacao objBlSolicitacao = new BLSolicitacao();
            DataTable dttColaborador = new DataTable();

            try
            {

                dttColaborador = objBlSolicitacao.ListarColaboradoresLista(Convert.ToInt32(Request.QueryString["CodSolicitacao"]),
                                                                           Convert.ToInt32(Request.QueryString["CodLista"]));



                radGridColaborador.DataSource = dttColaborador;
                radGridColaborador.CurrentPageIndex = BLUtilitarios.ObterPageIndiceGrid(this.radGridColaborador.CurrentPageIndex,
                                                                                       this.radGridColaborador.PageCount,
                                                                                       dttColaborador.Rows.Count,
                                                                                       this.radGridColaborador.PageSize);

                this.radGridColaborador.DataBind();
            }

            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }

        }

        protected void btnFechar_Click(object sender, EventArgs e)
        {
            ClientScript.RegisterStartupScript(String.Empty.GetType(), "CloseRad", "<script>CloseWin();</script>");
        }
    }
}
