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
using System.Collections.ObjectModel;
using FrameWork.BusinessLayer.Idioma;
using FrameWork.Model.Idioma;
using FrameWork.BusinessLayer.Utilitarios;
using SafWeb.BusinessLayer.Colaborador;
using FrameWork.Model.Utilitarios;

namespace SafWeb.UI.Modulos.Solicitacao
{
    public partial class ListVisitados : FWPage
    {
        private SafWeb.Model.Colaborador.Colaborador gobjColaborador;

        protected void Page_Load(object sender, EventArgs e)
        { 
            if (! Page.IsPostBack)
            {
                this.Bind(Enums.TipoBind.DataBind);
            }
        }

        #region Bind

        protected void Bind(Enums.TipoBind pintTipoBind)
        {
            BLColaborador objBlColaborador = new BLColaborador();
            Collection<SafWeb.Model.Colaborador.Colaborador> colColaborador;

            try
            {

                colColaborador = objBlColaborador.ListarColaborador(txtNome.Text, txtDocumento.Text, Convert.ToInt32(Request.QueryString["Tipo"]));

                radGridPessoa.DataSource = colColaborador;
                radGridPessoa.CurrentPageIndex = BLUtilitarios.ObterPageIndiceGrid(this.radGridPessoa.CurrentPageIndex,
                                                                                   this.radGridPessoa.PageCount, 
                                                                                   colColaborador.Count,
                                                                                   this.radGridPessoa.PageSize);

                if (pintTipoBind == Enums.TipoBind.DataBind)
                {
                    this.radGridPessoa.DataBind();
                }
            }

            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }

        }

        #endregion

        #region Evento

        protected void txtNome_TextChanged(object sender, EventArgs e)
        {
            this.Bind(Enums.TipoBind.DataBind);
        }

        protected void txtDocumento_TextChanged(object sender, EventArgs e)
        {
            this.Bind(Enums.TipoBind.DataBind);
        }

        #endregion

        #region DataGrid

        protected void radGridPessoa_NeedDataSource(object source, Telerik.WebControls.GridNeedDataSourceEventArgs e)
        {
            if (Page.IsPostBack)
            {
                this.Bind(Enums.TipoBind.SemDataBind);
            }
        }

        protected void radGridPessoa_ItemCommand(object source, Telerik.WebControls.GridCommandEventArgs e)
        {
            if (e.CommandName.Trim() == "Editar")
            {
                //this.Obter(Convert.ToInt32(e.CommandArgument.ToString().Trim()));

                //CadLista.Obter(Convert.ToInt32(e.CommandArgument.ToString().Trim()));

                //Page.ClientScript.RegisterStartupScript(String.Empty.GetType(), "fechar", "<script>returnToParent(" + e.CommandArgument.ToString().Trim() + ")</script>");
                ClientScript.RegisterStartupScript(String.Empty.GetType(), "Preencher", "<script>window.parent.PreencherCampos('" + e.CommandArgument.ToString().Trim() + "');</script>");
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
                if (Permissoes.Alteração())
                {
                    e.Item.ToolTip = BLIdiomas.TraduzirMensagens(Mensagens.EDITAR);
                }
                else
                {
                    e.Item.ToolTip = BLIdiomas.TraduzirMensagens(Mensagens.VISUALIZAR);
                }
            }
        }

        #endregion

        #region Obter Colaborador

        /// <summary>
        ///     Obtem as informações do colaborador selecionado
        /// </summary>
        /// <history>
        ///     [mribeiro] created 03/07/2009
        /// </history>
        protected void Obter(int pintIdColaborador)
        {
            BLColaborador objBlColaborador = new BLColaborador();

            try
            {
                gobjColaborador = objBlColaborador.Obter(pintIdColaborador);
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }

        #endregion
    }
}
