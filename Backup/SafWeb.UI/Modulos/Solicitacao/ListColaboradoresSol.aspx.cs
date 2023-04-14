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
using SafWeb.BusinessLayer.Colaborador;
using FrameWork.Model.Utilitarios;
using System.Collections.ObjectModel;
using FrameWork.BusinessLayer.Utilitarios;

namespace SafWeb.UI.Modulos.Solicitacao
{
    public partial class ListColaboradoresSol :  FWPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["Tipo"] == "3" || Request.QueryString["Tipo"] == "13")
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
                if (Request.QueryString["Tipo"] != null)
                {
                    //Chama o javaScript da página Pai para retornar o código do Colaborador selecionado.
                    ClientScript.RegisterStartupScript(String.Empty.GetType(), "Preencher", "<script>window.parent.PreencherHiddenVisitante('" + e.CommandArgument.ToString().Trim() + "');</script>");
                    ClientScript.RegisterStartupScript(String.Empty.GetType(), "CloseRad", "<script>CloseWin();</script>");
                }
                else
                {
                    //Chama o javaScript da página Pai para retornar o código do Colaborador selecionado.
                    ClientScript.RegisterStartupScript(String.Empty.GetType(), "Preencher", "<script>window.parent.PreencherHiddenVisitado('" + e.CommandArgument.ToString().Trim() + "');</script>");
                    ClientScript.RegisterStartupScript(String.Empty.GetType(), "CloseRad", "<script>CloseWin();</script>");
                }   
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

        #region Botão

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
            if (Request.QueryString["Tipo"] == "3")
            {
                //Chama o javaScript da página Pai para retornar o código do Visitante selecionado.
                ClientScript.RegisterStartupScript(String.Empty.GetType(), "Preencher", "<script>window.parent.PreencherHiddenVisitante('0');</script>");
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
