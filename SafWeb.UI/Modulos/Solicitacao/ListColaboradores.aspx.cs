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
using FrameWork.Model.Utilitarios;
using FrameWork.Model.Idioma;
using FrameWork.BusinessLayer.Idioma;
using FrameWork.BusinessLayer.Utilitarios;
using System.Collections.ObjectModel;
using SafWeb.BusinessLayer.Colaborador;

namespace SafWeb.UI.Modulos.Solicitacao
{
    public partial class ListColaboradores : FWPage
    {
        private SafWeb.Model.Colaborador.Colaborador gobjColaborador;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                hdTipo.Text = Request.QueryString["Tipo"];

                if (Request.QueryString["Tipo"] == "1")
                {
                    txtDocumento.Mascara = global::FrameWork.WebControl.FWMascara.TipoMascara.NENHUMA;
                    txtDocumento.MaxLength = 9;
                }
                else
                {
                    txtDocumento.Mascara = global::FrameWork.WebControl.FWMascara.TipoMascara.NUMERO;
                    txtDocumento.MaxLength = 12;
                }
            }
        }        

        #region DataGrid

        protected void radGridPessoa_ItemCommand(object source, Telerik.WebControls.GridCommandEventArgs e)
        {
            if (e.CommandName.Trim() == "Editar")
            {
                //Chama o javaScript da p�gina Pai para retornar o c�digo do Colaborador selecionado.
                ClientScript.RegisterStartupScript(String.Empty.GetType(), "Preencher", "<script>window.parent.PreencherHidden('" + e.CommandArgument.ToString().Trim() + "');</script>");
                ClientScript.RegisterStartupScript(String.Empty.GetType(), "CloseRad", "<script>CloseWin();</script>");
            }

            if (e.CommandName.Trim() == "IrPagina")
            {
                string strPageIndexString = ((TextBox)e.Item.FindControl("txtPagina")).Text;
                int intPageIndex = e.Item.OwnerTableView.CurrentPageIndex;
                if ((!object.ReferenceEquals(strPageIndexString.Trim(), string.Empty)))
                {
                    try
                    {
                        if (e.Item.OwnerTableView.PageCount < Convert.ToInt32(strPageIndexString))
                        {
                            strPageIndexString = (e.Item.OwnerTableView.PageCount).ToString();
                            ((TextBox)e.Item.FindControl("txtPagina")).Text = strPageIndexString;
                        }
                        intPageIndex = Convert.ToInt32(strPageIndexString) - 1;
                    }
                    catch
                    {
                        //process incorrect input here if needed 
                    }
                }

                if (!(e.Item.OwnerTableView.CurrentPageIndex == intPageIndex))
                {
                    e.Item.OwnerTableView.CurrentPageIndex = intPageIndex;
                    e.Item.OwnerTableView.Rebind();
                }
            }
        }

        protected void radGridPessoa_ItemDataBound(object sender, Telerik.WebControls.GridItemEventArgs e)
        {
            if (e.Item.ItemType == Telerik.WebControls.GridItemType.AlternatingItem | e.Item.ItemType == Telerik.WebControls.GridItemType.Item)
            {
                ImageButton btnEditar;
                btnEditar = (ImageButton)e.Item.FindControl("btnEditar");
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
                
                e.Item.ToolTip = "Selecione";
                
            }
        }

        #endregion

        #region Bot�o

        /// <summary>
        ///     Fecha a RadWindow
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [mribeiro] created 04/07/2009
        /// </history>
        protected void btnFechar_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["Tipo"] == "1")
            {
                //Chama o javaScript da p�gina Pai para retornar o c�digo do Visitante selecionado.
                ClientScript.RegisterStartupScript(String.Empty.GetType(), "Preencher", "<script>window.parent.PreencherHidden('0');</script>");
            }
            ClientScript.RegisterStartupScript(String.Empty.GetType(), "CloseRad", "<script>CloseWin();</script>");
        }

        /// <summary>
        ///     Filtra os colaboradores
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [mribeiro] created 04/07/2009
        /// </history>
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            ObjectDataSource1.DataBind();
            TxtNovaBusca.Value = string.Empty;
        }

        #endregion
    }
}
