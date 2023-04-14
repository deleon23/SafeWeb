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
using FrameWork.Model.Utilitarios;
using FrameWork.BusinessLayer.Idioma;
using FrameWork.Model.Idioma;
using SafWeb.BusinessLayer.Regional;
using SafWeb.BusinessLayer.Filial;
using SafWeb.BusinessLayer.Cracha;
using FrameWork.BusinessLayer.Utilitarios;

namespace SafWeb.UI.Modulos.Portaria
{
    public partial class ListCracha : FWPage
    {
        private Model.Cracha.Cracha gobjCracha;
        private Collection<Model.Cracha.Cracha> gcolCracha;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                this.PopulaCombos();

                if (Request.QueryString["Reg"] != string.Empty)
                {
                    BLUtilitarios.ConsultarTextoCombo(ref ddlTipoCracha, "Visitante");
                    ddlTipoCracha.Enabled = false;
                    BLUtilitarios.ConsultarValorCombo(ref ddlRegional, Request.QueryString["Reg"]);
                    ddlRegional.Enabled = false;
                    this.PopulaComboFilial();
                    BLUtilitarios.ConsultarValorCombo(ref ddlFilial, Request.QueryString["Fil"]);
                    ddlFilial.Enabled = false;
                }

                this.PopulaRadGridCrachas(Enums.TipoBind.DataBind);
            }
        }

        #region Listagem

        #region Métodos

        private void InicializaScripts()
        {
            try
            {
                this.txtCracha.Attributes.Add("OnKeyPress", "return mascara_Numero(this, event.keyCode);");
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }

        private void PopulaRadGridCrachas(Enums.TipoBind pintTipoBind)
        {
            BLCracha objBLCracha = null;

            try
            {
                objBLCracha = new BLCracha();
                gobjCracha = new Model.Cracha.Cracha();

                gobjCracha.CodRegional = Convert.ToInt32(this.ddlRegional.SelectedValue);
                gobjCracha.CodFilial = Convert.ToInt32(this.ddlFilial.SelectedValue);
                gobjCracha.CodCrachaTipo = Convert.ToInt32(this.ddlTipoCracha.SelectedValue);
                if (this.txtCracha.Text != string.Empty)
                    gobjCracha.NumCracha = this.txtCracha.Text;
                gobjCracha.FlgSituacao = true;

                gcolCracha = objBLCracha.Listar(gobjCracha,
                                                (Request.QueryString["Entrada"] != string.Empty ? true : false));

                this.radCrachas.DataSource = gcolCracha;

                if (pintTipoBind == Enums.TipoBind.DataBind)
                    this.radCrachas.DataBind();
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }

        private void PopulaCombos()
        {
            this.PopulaComboRegional();
            this.PopulaComboFilial();
            this.PopulaComboTipoCracha();
        }

        private void PopulaComboRegional()
        {
            BLRegional objBlRegional = null;
            Collection<SafWeb.Model.Regional.Regional> colRegional = null;

            try
            {
                objBlRegional = new BLRegional();

                colRegional = objBlRegional.Listar();

                this.ddlRegional.DataSource = colRegional;
                this.ddlRegional.DataTextField = "DescricaoRegional";
                this.ddlRegional.DataValueField = "IdRegional";
                this.ddlRegional.DataBind();


                this.ddlRegional.Items.Insert(0, new ListItem(BLIdiomas.TraduzirMensagens(Convert.ToInt32(Mensagens.SELECIONE)), "0"));
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }

        private void PopulaComboFilial()
        {
            BLFilial objBlFilial = null;
            Collection<SafWeb.Model.Filial.Filial> colFilial = null;

            try
            {
                objBlFilial = new BLFilial();

                if (Convert.ToInt32(ddlRegional.SelectedItem.Value) > 0)
                    colFilial = objBlFilial.Listar(Convert.ToInt32(ddlRegional.SelectedItem.Value));

                this.ddlFilial.Items.Clear();

                this.ddlFilial.DataSource = colFilial;
                this.ddlFilial.DataTextField = "AliasFilial";
                this.ddlFilial.DataValueField = "IdFilial";
                this.ddlFilial.DataBind();

                this.ddlFilial.Items.Insert(0, new ListItem(BLIdiomas.TraduzirMensagens(Convert.ToInt32(Mensagens.SELECIONE)), "0"));
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }

        private void PopulaComboTipoCracha()
        {
            BLCracha objBLCracha = null;
            Collection<SafWeb.Model.Cracha.CrachaTipo> colCrachaTipo = null;

            try
            {
                objBLCracha = new BLCracha();

                colCrachaTipo = objBLCracha.ListarCrachaTipo();

                this.ddlTipoCracha.Items.Clear();

                this.ddlTipoCracha.DataSource = colCrachaTipo;
                this.ddlTipoCracha.DataTextField = "DesCrachaTipo";
                this.ddlTipoCracha.DataValueField = "CodCrachaTipo";
                this.ddlTipoCracha.DataBind();

                this.ddlTipoCracha.Items.Insert(0, new ListItem(BLIdiomas.TraduzirMensagens(Convert.ToInt32(Mensagens.SELECIONE)), "0"));
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }

        #endregion

        #region Eventos

        #region Botões

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            this.PopulaRadGridCrachas(Enums.TipoBind.DataBind);
        }

        protected void btnFechar_Click(object sender, EventArgs e)
        {
            ClientScript.RegisterStartupScript(String.Empty.GetType(), "CloseRad", "<script>CloseWin();</script>");
        }

        #endregion

        #region Combo

        protected void ddlRegional_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.PopulaComboFilial();
        }

        #endregion

        #region RadGrid

        protected void radCrachas_NeedDataSource(object source, Telerik.WebControls.GridNeedDataSourceEventArgs e)
        {
            if (this.Page.IsPostBack)
            {
                this.PopulaRadGridCrachas(Enums.TipoBind.SemDataBind);
            }
        }

        protected void radCrachas_ItemCommand(object source, Telerik.WebControls.GridCommandEventArgs e)
        {
            if (e.CommandName.Trim() == "Selecionar")
            {
                ClientScript.RegisterStartupScript(String.Empty.GetType(), "Preencher", "<script>window.parent.PreencherHiddenCracha('" + e.Item.Cells[3].Text + "', '" + e.Item.Cells[2].Text + "');</script>");
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
                    catch(Exception ex)
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

        protected void radCrachas_ItemDataBound(object sender, Telerik.WebControls.GridItemEventArgs e)
        {
            ImageButton imgAtivar, imgSelecionar;

            try
            {
                if (e.Item.ItemType == Telerik.WebControls.GridItemType.Item || e.Item.ItemType == Telerik.WebControls.GridItemType.AlternatingItem)
                {
                    imgAtivar = (ImageButton)(e.Item.FindControl("imgAtivar"));
                    if (Convert.ToBoolean(DataBinder.Eval(e.Item.DataItem, "FlgSituacao")))
                        imgAtivar.ImageUrl = "~/imagens/icones/ico_ativo.gif";
                    else
                        imgAtivar.ImageUrl = "~/imagens/icones/ico_inativo.gif";
                   
                    imgSelecionar = (ImageButton)(e.Item.FindControl("imgSelecionar"));
                    imgSelecionar.Visible = false;
                    e.Item.Attributes.Add("onMouseOver", "selecionaGrid(this)");
                    e.Item.Attributes.Add("onMouseOut", "deSelecionaGrid(this)");
                    e.Item.Style["cursor"] = "hand";

                    // Cursor 
                    int intCell = e.Item.Cells.Count;
                    for (int @int = 0; @int <= intCell - 2; @int++)
                    {
                        e.Item.Cells[@int].Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(imgSelecionar, ""));
                    }

                    e.Item.ToolTip = "Selecionar";
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }

        #endregion

        #endregion

        #endregion
    }
}
