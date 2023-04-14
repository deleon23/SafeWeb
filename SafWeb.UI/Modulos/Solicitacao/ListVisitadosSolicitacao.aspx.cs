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
using FrameWork.BusinessLayer.Idioma;
using FrameWork.Model.Idioma;

namespace SafWeb.UI.Modulos.Solicitacao
{
    public partial class ListVisitadosSolicitacao : FWPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                DataTable dtt = new DataTable();
                DataRow dtr;

                dtt.Columns.Add(new DataColumn("NOME"));
                dtt.Columns.Add(new DataColumn("TIPODOCUMENTO"));
                dtt.Columns.Add(new DataColumn("DOCUMENTO"));

                dtr = dtt.NewRow();

                dtr["NOME"] = "José Francisco Carvalho";
                dtr["TIPODOCUMENTO"] = "RE";
                dtr["DOCUMENTO"] = "87263";

                dtt.Rows.Add(dtr);

                dtgPessoa.DataSource = dtt;
                dtgPessoa.DataBind();
            }
        }

        #region DataGrid

        #region ItemCommand

        protected void dtgPessoa_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName.Trim() == "Editar")
            {
                //Response.Redirect("CadSolicitacao.aspx?Visitado=" + dtgPessoa.Items[0].Cells[0].Text);
            }
        }

        #endregion

        #region ItemDataBound

        protected void dtgPessoa_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            ImageButton btnEditar;

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                try
                {
                    btnEditar = (ImageButton)(e.Item.FindControl("imgEditar"));
                    btnEditar.Visible = false;
                    e.Item.Attributes.Add("onMouseOver", "selecionaGrid(this)");
                    e.Item.Attributes.Add("onMouseOut", "deSelecionaGrid(this)");
                    e.Item.Style["cursor"] = "hand";

                    // Cursor 
                    int intCell = e.Item.Cells.Count;
                    for (int @int = 0; @int <= intCell - 2; @int++)
                    {
                        e.Item.Cells[@int].Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(btnEditar, ""));
                    }

                    if (Permissoes.Alteração())
                    {
                        e.Item.ToolTip = BLIdiomas.TraduzirMensagens(Mensagens.EDITAR);
                    }
                    else
                    {
                        e.Item.ToolTip = BLIdiomas.TraduzirMensagens(Mensagens.VISUALIZAR);
                    }

                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        #endregion

        #endregion
    }
}
