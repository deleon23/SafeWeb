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
using SafWeb.BusinessLayer.Lista;
using FrameWork.BusinessLayer.Utilitarios;
using FrameWork.Model.Utilitarios;
using System.Collections.Generic;

namespace SafWeb.UI.Modulos.Solicitacao
{
	public partial class ListColaboradoresLista : FWPage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
            if (!Page.IsPostBack)
            {
                this.Bind(Enums.TipoBind.DataBind);

                if (Request.QueryString["Inclusos"].ToString() != string.Empty)
                {
                    this.ColaboradorLista = Request.QueryString["Inclusos"].ToString();
                }
            }
		}

        #region Bind

        protected void Bind(Enums.TipoBind pintTipoBind)
        {
            BLLista objBlLista = new BLLista();
            Collection<SafWeb.Model.Colaborador.Colaborador> colColaboradores;

            try
            {

                colColaboradores = objBlLista.ListarColaboradorDaLista(Convert.ToInt32(Request.QueryString["CodLista"]));

                radGridColaborador.DataSource = colColaboradores;
                radGridColaborador.CurrentPageIndex = BLUtilitarios.ObterPageIndiceGrid(this.radGridColaborador.CurrentPageIndex,
                                                                                       this.radGridColaborador.PageCount,
                                                                                       colColaboradores.Count,
                                                                                       this.radGridColaborador.PageSize);

                if (pintTipoBind == Enums.TipoBind.DataBind)
                {
                    this.radGridColaborador.DataBind();
                }
            }

            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }

        }

        #endregion

        #region RadGrid

        protected void radGridColaborador_NeedDataSource(object source, Telerik.WebControls.GridNeedDataSourceEventArgs e)
        {
            if (this.Page.IsPostBack)
            {
                //this.Bind(Enums.TipoBind.SemDataBind);
            }
        }

        protected void radGridColaborador_ItemCommand(object source, Telerik.WebControls.GridCommandEventArgs e)
        {
            if (e.CommandName.Trim() == "Ativar")
            {
                try
                {
                    Telerik.WebControls.GridDataItem dataItem = (Telerik.WebControls.GridDataItem)e.Item;
                    ImageButton btnAtivar = (ImageButton)dataItem["Ativar"].Controls[0];

                    if (btnAtivar.ToolTip == "Retirar da Lista")
                    {
                        btnAtivar.ImageUrl = "~/imagens/icones/ico_inativo.gif";
                        btnAtivar.ToolTip = "Incluir na Lista";

                        //armazeno todos os ids
                        string[] strInclusos = this.ColaboradorLista.Split(new char[] { ',' });
                        //limpo a string
                        this.ColaboradorLista = string.Empty;

                        for (int i = 0; i < strInclusos.Length; i++)
                        {
                            //se não for o id que foi ativado novamente, armazena na string novamente
                            if (strInclusos[i] != e.CommandArgument.ToString().Trim())
                            {
                                if (this.ColaboradorLista == string.Empty)
                                {
                                    this.ColaboradorLista = strInclusos[i];
                                }
                                else
                                {
                                    this.ColaboradorLista += "," + strInclusos[i];
                                }
                            }
                        }
                    }
                    else
                    {
                        btnAtivar.ImageUrl = "~/imagens/icones/ico_ativo.gif";
                        btnAtivar.ToolTip = "Retirar da Lista";

                        if (this.ColaboradorLista == string.Empty)
                        {
                            this.ColaboradorLista = e.CommandArgument.ToString().Trim();
                        }
                        else
                        {
                            this.ColaboradorLista += "," + e.CommandArgument.ToString().Trim();
                        }
                    }

                }
                catch (Exception ex)
                {
                    ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
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
                    catch (Exception ex)
                    {
                        ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
                    }
                }

                if (!(e.Item.OwnerTableView.CurrentPageIndex == intPageIndex))
                {
                    e.Item.OwnerTableView.CurrentPageIndex = intPageIndex;
                    e.Item.OwnerTableView.Rebind();
                }
            }
        }

        protected void radGridColaborador_ItemDataBound(object sender, Telerik.WebControls.GridItemEventArgs e)
        {
            ImageButton btnAtivar = null;

            try
            {
                if (e.Item.ItemType == Telerik.WebControls.GridItemType.Item || e.Item.ItemType == Telerik.WebControls.GridItemType.AlternatingItem)
                {
                    Telerik.WebControls.GridDataItem dataItem = (Telerik.WebControls.GridDataItem)e.Item;
                    btnAtivar = (ImageButton)dataItem["Ativar"].Controls[0];

                    if ((btnAtivar != null))
                    {
                        if (Permissoes.Alteração())
                        {
                            if (Request.QueryString["Inclusos"].ToString() != string.Empty)
                            {
                                bool blnAtivar = true;

                                string[] strRetirados = Request.QueryString["Inclusos"].ToString().Split(new char[] { ',' });

                                for (int i = 0; i < strRetirados.Length; i++)
                                {
                                    if (strRetirados[i] == DataBinder.Eval(e.Item.DataItem, "IdColaborador").ToString())
                                    {
                                        btnAtivar.ImageUrl = "~/imagens/icones/ico_ativo.gif";
                                        btnAtivar.ToolTip = "Retirar da Lista";
                                        blnAtivar = false;

                                        break;
                                    }
                                    else
                                    {
                                        blnAtivar = true;
                                    }
                                }

                                if (blnAtivar)
                                {
                                    btnAtivar.ImageUrl = "~/imagens/icones/ico_inativo.gif";
                                    btnAtivar.ToolTip = "Incluir na Lista";
                                }
                            }
                            else
                            {
                                btnAtivar.ImageUrl = "~/imagens/icones/ico_ativo.gif";
                                btnAtivar.ToolTip = "Retirar da Lista";

                                if (this.ColaboradorLista == string.Empty)
                                {
                                    this.ColaboradorLista = DataBinder.Eval(e.Item.DataItem, "IdColaborador").ToString();
                                }
                                else
                                {
                                    this.ColaboradorLista += "," + DataBinder.Eval(e.Item.DataItem, "IdColaborador").ToString();
                                }
                            }

                            btnAtivar.CommandArgument = DataBinder.Eval(e.Item.DataItem, "IdColaborador").ToString();
                        }
                        else
                        {
                            btnAtivar.Enabled = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
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
        ///     [mribeiro] created 20/07/2009
        /// </history>
        protected void btnFechar_Click(object sender, EventArgs e)
        {
            if (this.ColaboradorLista == string.Empty)
            {
                RadAjaxPanel1.Alert("Pelo menos um colaborador deve estar ativo.");
            }
            else
            {
                //Chama o javaScript da página Pai para retornar o código do Colaborador selecionado.
                ClientScript.RegisterStartupScript(String.Empty.GetType(), "Preencher", "<script>window.parent.PreencherHiddenLista('" + this.ColaboradorLista + "');</script>");
                ClientScript.RegisterStartupScript(String.Empty.GetType(), "CloseRad", "<script>CloseWin();</script>");
            }
        }

        #endregion

        #region Property

        public string ColaboradorLista
        {
            get
            {
                if (ViewState["vsLista"] == null)
                {
                    ViewState["vsLista"] = string.Empty;
                }
                return ViewState["vsLista"].ToString();
            }
            set
            {
                ViewState["vsLista"] = value;
            }
        }

        #endregion

    }
}
