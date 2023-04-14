using System;
using System.Data;
using FrameWork.BusinessLayer.Utilitarios;
using SafWeb.BusinessLayer.Solicitacao;
using SafWeb.Util.Extension;
using System.Web.UI.WebControls;
using System.Web.UI;
using Telerik.WebControls;

namespace SafWeb.UI.Modulos.Aprovacao
{
    public partial class ListSolicitacaoListaVeiculos : FWPage
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

                dttColaborador = objBlSolicitacao.ListarVeiculosLista(Request.QueryString["CodSolicitacao"].ToInt32());



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


        protected void radGridColaborador_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item.ItemType == Telerik.WebControls.GridItemType.AlternatingItem | e.Item.ItemType == Telerik.WebControls.GridItemType.Item)
            {
                try
                {
                    Telerik.WebControls.GridDataItem dataItem = (Telerik.WebControls.GridDataItem)e.Item;
                    ImageButton btnAtivar = (ImageButton)dataItem["Ativar"].Controls[0];

                    if ((btnAtivar != null))
                    {
                        btnAtivar.Enabled = false;
                        if (Convert.ToBoolean(DataBinder.Eval(e.Item.DataItem, "Situacao")))
                        {
                            btnAtivar.ImageUrl = "~/imagens/icones/ico_ativo.gif";
                           
                        }
                        else
                        {
                            btnAtivar.ImageUrl = "~/imagens/icones/ico_inativo.gif";
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
