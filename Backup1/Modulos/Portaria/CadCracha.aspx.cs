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
using FrameWork.Model.Utilitarios;
using SafWeb.BusinessLayer.Regional;
using SafWeb.BusinessLayer.Filial;
using SafWeb.BusinessLayer.Cracha;
using System.Collections.ObjectModel;
using FrameWork.BusinessLayer.Utilitarios;

namespace SafWeb.UI.Modulos.Portaria
{
    public partial class CadCracha : FWPage
    {
        private Model.Cracha.Cracha gobjCracha;
        private Collection<Model.Cracha.Cracha> gcolCracha;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            this.InicializaScripts();

            if (!Page.IsPostBack)
            {
                this.ControlaPaineis(Enums.TipoPainel.Listagem);
                this.VerificaPermissoes();

                this.PopulaCombos();
                this.PopulaCombosCad();

                this.PopulaRadGridCrachas(Enums.TipoBind.DataBind);
            }

            this.pnlObservacao.Visible = false;
        }

        #region ViewState

        public int CodCracha
        {
            get
            {
                if (ViewState["vsCodCracha"] == null)
                {
                    ViewState["vsCodCracha"] = 0;
                }
                return Convert.ToInt32(ViewState["vsCodCracha"]);
            }
            set
            {
                ViewState["vsCodCracha"] = value;
            }
        }

        #endregion

        #region Inicializa Script

        private void InicializaScripts()
        {
            this.txtCracha.Attributes.Add("OnKeyPress", "return mascara_Numero(this, event.keyCode);");
            this.txtCrachaCad.Attributes.Add("OnKeyPress", "return mascara_Numero(this, event.keyCode);");
            this.btnHelpCad.Attributes.Add("OnClick", "AbrirHelpCad();");
            this.btnHelpList.Attributes.Add("OnClick", "AbrirHelpList();");
        }

        #endregion

        #region Controla Painéis

        private void ControlaPaineis(Enums.TipoPainel pintPainel)
        {
            if (pintPainel == Enums.TipoPainel.Listagem)
            {
                pnlListagem.Visible = true;
                pnlCadastro.Visible = false;
            }
            else
            {
                pnlCadastro.Visible = true;
                pnlListagem.Visible = false;
            }
        }

        #endregion

        #region Permissões

        private void VerificaPermissoes()
        {
            this.btnIncluir.Enabled = Permissoes.Inclusão();
            this.btnGravar.Enabled = Permissoes.Inclusão();
            this.btnGravarSair.Enabled = Permissoes.Inclusão();
        }

        #endregion

        #region Listagem

        #region Métodos

        protected void PopulaRadGridCrachas(Enums.TipoBind pintTipoBind)
        {
            BLCracha objBLCracha = null;

            try
            {
                this.lblMensagemListagem.Visible = false;

                objBLCracha = new BLCracha();
                gobjCracha = new Model.Cracha.Cracha();

                gobjCracha.CodRegional = Convert.ToInt32(this.ddlRegional.SelectedValue);
                gobjCracha.CodFilial = Convert.ToInt32(this.ddlFilial.SelectedValue);
                gobjCracha.CodCrachaTipo = Convert.ToInt32(this.ddlTipoCracha.SelectedValue);
                if(this.txtCracha.Text != string.Empty)
                    gobjCracha.NumCracha = this.txtCracha.Text;

                gcolCracha = objBLCracha.Listar(gobjCracha,
                                                false);

                this.radCrachas.DataSource = gcolCracha;

                if (pintTipoBind == Enums.TipoBind.DataBind)
                    this.radCrachas.DataBind();
            }
            catch(Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }

        private void PopulaCombos()
        {
            this.PopulaComboRegional();
            this.PopulaComboFilial(Convert.ToInt32(this.ddlRegional.SelectedValue));
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

        private void PopulaComboFilial(int pintCodRegional)
        {
            BLFilial objBlFilial = null;
            Collection<SafWeb.Model.Filial.Filial> colFilial = null;

            try
            {
                objBlFilial = new BLFilial();

                if (pintCodRegional > 0)
                {
                    colFilial = objBlFilial.Listar(pintCodRegional);
                    ddlFilial.Enabled = true;
                }
                else
                {
                    ddlFilial.Enabled = false;
                }

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

        protected void btnIncluir_Click(object sender, EventArgs e)
        {
            this.ControlaPaineis(Enums.TipoPainel.Cadastro);
        }

        protected void btnGravarObs_Click(object sender, EventArgs e)
        {
            BLCracha objBLCracha;

            try
            {
                gobjCracha = new SafWeb.Model.Cracha.Cracha();

                gobjCracha.CodCracha = this.CodCracha;
                gobjCracha.DesObservacao = txtObservacao.Text;

                foreach (Telerik.WebControls.GridDataItem dataItem in radCrachas.Items)
                {
                    if (Convert.ToInt32(dataItem.Cells[2].Text) == gobjCracha.CodCracha)
                    {
                        ImageButton btnAtivar = (ImageButton)dataItem["Ativar"].Controls[0];

                        if (btnAtivar.ToolTip == "Ativar")
                        {
                            btnAtivar.ImageUrl = "~/imagens/icones/ico_ativo.gif";
                            btnAtivar.ToolTip = "Inativar";
                            gobjCracha.FlgSituacao = true;
                        }
                        else if (btnAtivar.ToolTip == "Inativar")
                        {
                            btnAtivar.ImageUrl = "~/imagens/icones/ico_inativo.gif";
                            btnAtivar.ToolTip = "Ativar";
                            gobjCracha.FlgSituacao = false;
                        }
                        break;
                    }
                }

                objBLCracha = new BLCracha();

                if (objBLCracha.Alterar(gobjCracha))
                {
                    // Modificar a imagem para inativa
                    this.PopulaRadGridCrachas(Enums.TipoBind.DataBind);

                    this.lblMensagemListagem.Visible = true;
                    this.lblMensagemListagem.Text = BLIdiomas.TraduzirMensagens(Convert.ToInt32(Mensagens.REGISTRO_GRAVAR));
                }

                this.txtObservacao.Text = string.Empty;
                this.pnlObservacao.Visible = false;
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }

        #endregion

        #region Combo

        protected void ddlRegional_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.PopulaComboFilial(Convert.ToInt32(this.ddlRegional.SelectedValue));
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

        /// <summary>
        ///     radCrachas
        /// </summary>
        /// <history>
        ///     [no history]
        ///     [haguiar] modified 05/11/2010
        /// </history>
        protected void radCrachas_ItemCommand(object source, Telerik.WebControls.GridCommandEventArgs e)
        {
            if (e.CommandName.Trim() == "Ativar")
            {
                try
                {
                    Telerik.WebControls.GridDataItem dataItem = (Telerik.WebControls.GridDataItem)e.Item;
                    ImageButton btnAtivar = (ImageButton)dataItem["Ativar"].Controls[0];

                    this.CodCracha = Convert.ToInt32(e.CommandArgument.ToString().Trim());

                    this.pnlObservacao.Visible = true;
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
            
            /*
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

                        //digitou 0, volta para a primeira página
                        if (Convert.ToInt32(strPageIndexString) == 0)
                        {
                            ((TextBox)e.Item.FindControl("txtPagina")).Text = "1";
                            intPageIndex = 0;
                        }
                        else
                        {
                            intPageIndex = Convert.ToInt32(strPageIndexString) - 1;
                        }
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

            */
        }

        protected void radCrachas_ItemDataBound(object sender, Telerik.WebControls.GridItemEventArgs e)
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
                            if (Convert.ToBoolean(DataBinder.Eval(e.Item.DataItem, "FlgSituacao")))
                            {
                                btnAtivar.ImageUrl = "~/imagens/icones/ico_ativo.gif";
                                btnAtivar.ToolTip = "Inativar";
                            }
                            else
                            {
                                btnAtivar.ImageUrl = "~/imagens/icones/ico_inativo.gif";
                                btnAtivar.ToolTip = "Ativar";
                            }
                            btnAtivar.CommandArgument = DataBinder.Eval(e.Item.DataItem, "CodCracha").ToString();
                        }
                        else
                        {
                            btnAtivar.Enabled = false;
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }

        #endregion

        #endregion

        #endregion

        #region Cadastro

        #region Métodos

        #region Gravar 

        private bool Gravar()
        {
            bool blnRetorno = false;
            BLCracha objBLCracha = null;

            try
            {
                this.PopulaModelCracha(Enums.TipoTransacao.CarregarDados);

                objBLCracha = new BLCracha();

                gobjCracha.CodCracha = objBLCracha.Inserir(gobjCracha);

                if (gobjCracha.CodCracha == -1)
                {
                    RadAjaxPanel1.Alert("Este crachá já está cadastrado.");
                    blnRetorno = false;
                }
                else
                {
                    txtCrachaCad.Text = string.Empty;

                    this.PopulaRadGridCrachas(Enums.TipoBind.DataBind);

                    blnRetorno = true;
                }
               
            }
            catch(Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }

            return blnRetorno;
        }

        #endregion

        #region Model

        private void PopulaModelCracha(Enums.TipoTransacao pintTipoTransacao)
        {
            try
            {
                if (pintTipoTransacao == Enums.TipoTransacao.CarregarDados)
                {
                    gobjCracha = new Model.Cracha.Cracha();

                    gobjCracha.CodCracha = this.CodCracha;
                    gobjCracha.CodCrachaTipo = Convert.ToInt32(this.ddlTipoCrachaCad.SelectedValue);
                    gobjCracha.DesCrachaTipo = this.ddlTipoCrachaCad.SelectedItem.Text;
                    gobjCracha.CodFilial = Convert.ToInt32(this.ddlFilialCad.SelectedValue);
                    gobjCracha.AliasFilial = this.ddlFilialCad.SelectedItem.Text;
                    gobjCracha.CodRegional = Convert.ToInt32(this.ddlRegionalCad.SelectedValue);
                    gobjCracha.DesRegional = this.ddlRegionalCad.SelectedItem.Text;
                    gobjCracha.NumCracha = this.txtCrachaCad.Text;
                    gobjCracha.FlgSituacao = true;
                }
                else if (pintTipoTransacao == Enums.TipoTransacao.DescarregarDados)
                {
                    this.CodCracha = gobjCracha.CodCracha;

                    if (this.ddlTipoCrachaCad.Items.FindByValue(gobjCracha.CodCrachaTipo.ToString()) != null)
                        this.ddlTipoCrachaCad.SelectedValue = gobjCracha.CodCrachaTipo.ToString();

                    if (this.ddlFilialCad.Items.FindByValue(gobjCracha.CodFilial.ToString()) != null)
                        this.ddlFilialCad.SelectedValue = gobjCracha.CodFilial.ToString();

                    if (this.ddlRegionalCad.Items.FindByValue(gobjCracha.CodRegional.ToString()) != null)
                        this.ddlRegionalCad.SelectedValue = gobjCracha.CodRegional.ToString();

                    this.txtCracha.Text = gobjCracha.NumCracha.ToString();
                }
                else if (pintTipoTransacao == Enums.TipoTransacao.Novo)
                {
                    this.CodCracha = 0;
                    this.ddlTipoCrachaCad.SelectedIndex = 0;
                    this.ddlRegionalCad.SelectedIndex = 0;
                    this.PopulaComboFilialCad(Convert.ToInt32(ddlRegionalCad.SelectedValue));
                    this.txtCrachaCad.Text = string.Empty;
                }
            }
            catch(Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }

        #endregion

        #region Popular Combo

        private void PopulaCombosCad()
        {
            this.PopulaComboRegionalCad();
            this.PopulaComboFilialCad(Convert.ToInt32(this.ddlRegionalCad.SelectedValue));
            this.PopulaComboTipoCrachaCad();
        }

        private void PopulaComboRegionalCad()
        {
            BLRegional objBlRegional = null;
            Collection<SafWeb.Model.Regional.Regional> colRegional = null;

            try
            {
                objBlRegional = new BLRegional();

                colRegional = objBlRegional.Listar();

                this.ddlRegionalCad.DataSource = colRegional;
                this.ddlRegionalCad.DataTextField = "DescricaoRegional";
                this.ddlRegionalCad.DataValueField = "IdRegional";
                this.ddlRegionalCad.DataBind();

                this.ddlRegionalCad.Items.Insert(0, new ListItem(BLIdiomas.TraduzirMensagens(Convert.ToInt32(Mensagens.SELECIONE)), "0"));
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }

        private void PopulaComboFilialCad(int pintCodRegional)
        {
            BLFilial objBlFilial = null;
            Collection<SafWeb.Model.Filial.Filial> colFilial = null;

            try
            {
                objBlFilial = new BLFilial();

                if (pintCodRegional > 0)
                {
                    colFilial = objBlFilial.Listar(pintCodRegional);
                    ddlFilialCad.Enabled = true;
                }
                else
                {
                    ddlFilialCad.Enabled = false;
                }

                this.ddlFilialCad.Items.Clear();

                this.ddlFilialCad.DataSource = colFilial;
                this.ddlFilialCad.DataTextField = "AliasFilial";
                this.ddlFilialCad.DataValueField = "IdFilial";
                this.ddlFilialCad.DataBind();

                this.ddlFilialCad.Items.Insert(0, new ListItem(BLIdiomas.TraduzirMensagens(Convert.ToInt32(Mensagens.SELECIONE)), "0"));
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }

        private void PopulaComboTipoCrachaCad()
        {
            BLCracha objBLCracha = null;
            Collection<SafWeb.Model.Cracha.CrachaTipo> colCrachaTipo = null;

            try
            {
                objBLCracha = new BLCracha();

                colCrachaTipo = objBLCracha.ListarCrachaTipo();

                this.ddlTipoCrachaCad.Items.Clear();

                this.ddlTipoCrachaCad.DataSource = colCrachaTipo;
                this.ddlTipoCrachaCad.DataTextField = "DesCrachaTipo";
                this.ddlTipoCrachaCad.DataValueField = "CodCrachaTipo";
                this.ddlTipoCrachaCad.DataBind();

                //this.ddlTipoCrachaCad.Items.Insert(0, new ListItem(BLIdiomas.TraduzirMensagens(Convert.ToInt32(Mensagens.SELECIONE)), "0"));
                BLUtilitarios.ConsultarTextoCombo(ref ddlTipoCrachaCad, "Visitante");
                this.ddlTipoCracha.Enabled = false;
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }

        #endregion

        #endregion

        #region Eventos

        #region Botões

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            this.ControlaPaineis(Enums.TipoPainel.Listagem);
        }

        protected void btnGravar_Click(object sender, EventArgs e)
        {
            if (Gravar())
            {
                lblMensagemCad.Visible = true;
                lblMensagemCad.Text = BLIdiomas.TraduzirMensagens(Convert.ToInt32(Mensagens.REGISTRO_GRAVAR));
            }
        }

        protected void btnGravarSair_Click(object sender, EventArgs e)
        {
            if (Gravar())
            {
                lblMensagemListagem.Visible = true;
                lblMensagemListagem.Text = BLIdiomas.TraduzirMensagens(Convert.ToInt32(Mensagens.REGISTRO_GRAVAR));

                this.ControlaPaineis(Enums.TipoPainel.Listagem);
            }
        }

        #endregion

        #region Combo

        protected void ddlRegionalCad_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.PopulaComboFilialCad(Convert.ToInt32(this.ddlRegionalCad.SelectedValue));
        }

        #endregion

        #endregion

        #endregion
    }
}
