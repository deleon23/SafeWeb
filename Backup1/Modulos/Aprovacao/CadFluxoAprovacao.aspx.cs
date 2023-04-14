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
using SafWeb.BusinessLayer.Solicitacao;
using System.Collections.ObjectModel;

namespace SafWeb.UI.Modulos.Aprovacao
{
    public partial class CadFluxoAprovacao : FWPage
    {
        private Model.Solicitacao.FluxoAprovacao gobjFluxoAprov = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.InicializaScripts();

            if (!Page.IsPostBack)
            {
                this.ControlaPaineis(Enums.TipoPainel.Listagem);
                this.VerificaPermissoes();

                this.PopulaCombo();
                this.PopulaRadTipoSolic(true);
            }
        }

        #region Métodos

        #region InicializaScripts

        private void InicializaScripts()
        {
            this.txtOrdem.Attributes.Add("OnKeyPress", "return mascara_Numero(this, event.keyCode);");
            this.btnHelpCad.Attributes.Add("OnClick", "AbrirHelpCad();");
            this.btnHelpList.Attributes.Add("OnClick", "AbrirHelpList();");
        }

        #endregion

        #region Controla Paineis

        private void ControlaPaineis(Enums.TipoPainel pintPainel)
        {
            if (pintPainel == Enums.TipoPainel.Listagem)
            {
                this.BindModelFluxoAprov(Enums.TipoTransacao.Novo);
                
                pnlListagem.Visible = true;
                pnlCadastro.Visible = false;
            }
            else
            {
                pnlCadastro.Visible = true;
                pnlListagem.Visible = false;

                if (!Permissoes.Alteração())
                {
                    btnAdicionar.Enabled = false;
                    btnGravar.Enabled = false;
                    btnGravarSair.Enabled = false;
                }
                else
                {
                    btnAdicionar.Enabled = true;
                    btnGravar.Enabled = true;
                    btnGravarSair.Enabled = true;
                }
            }
        }

        #endregion

        #region Verifica Permissoes

        private void VerificaPermissoes()
        {
            btnGravar.Enabled = Permissoes.Inclusão();
            btnGravarSair.Enabled = Permissoes.Inclusão();
        }

        #endregion

        #region Popular Combo

        private void PopulaCombo()
        {
            PopulaComboNivel();
            PopulaComboStatus();
            PopulaComboTipoSolicitacao();
        }

        private void PopulaComboTipoSolicitacao()
        {
            BLSolicitacao objBLSolicitacao = new BLSolicitacao();
            Collection<SafWeb.Model.Solicitacao.TipoSolicitacao> colTipoSolicitacao;

            try
            {
                colTipoSolicitacao = objBLSolicitacao.ListarTipoSolicitacaoTodos();

                ddlTipoSolicitacao.DataSource = colTipoSolicitacao;
                ddlTipoSolicitacao.DataTextField = "Descricao";
                ddlTipoSolicitacao.DataValueField = "Codigo";
                ddlTipoSolicitacao.DataBind();
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }

        private void PopulaComboNivel()
        {
            BLSolicitacao objBLSolicitacao = new BLSolicitacao();
            Collection<SafWeb.Model.Solicitacao.NivelAprovacao> colNivelAprovacao;

            try
            {
                colNivelAprovacao = objBLSolicitacao.ListarNivelAprovacao();

                ddlNivelAprovador.DataSource = colNivelAprovacao;
                ddlNivelAprovador.DataTextField = "Descricao";
                ddlNivelAprovador.DataValueField = "Codigo";
                ddlNivelAprovador.DataBind();

                ddlNivelAprovador.Items.Insert(0, new ListItem(BLIdiomas.TraduzirMensagens(Convert.ToInt32(Mensagens.SELECIONE)), "-1"));
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }

        private void PopulaComboStatus()
        {
            BLSolicitacao objBLSolicitacao = new BLSolicitacao();
            Collection<SafWeb.Model.Solicitacao.Status> colStatus;

            try
            {
                colStatus = objBLSolicitacao.ListarStatus();

                ddlStatus.DataSource = colStatus;
                ddlStatus.DataTextField = "Descricao";
                ddlStatus.DataValueField = "Codigo";
                ddlStatus.DataBind();

                ddlStatus.Items.Insert(0, new ListItem(BLIdiomas.TraduzirMensagens(Convert.ToInt32(Mensagens.SELECIONE)), "-1"));
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }

        #endregion

        #region Gravar

        /// <history>
        ///      [no history]
        ///      [haguiar] modify 25/04/2011 15:13
        ///      armazenar propriedade FlgAprovaAreaTI
        ///      [haguiar] modify 24/02/2012 11:19
        ///      armazenar propriedade FlgAprovaCracha
        /// </history>
        /// 
        private bool Gravar()
        {
            bool blnGravado = false;

            try
            {
                for (int i = 0; i < this.FluxoAprovacao.Count; i++)
                {
                    BLSolicitacao objBLSolicitacao = new BLSolicitacao();
                    gobjFluxoAprov = new Model.Solicitacao.FluxoAprovacao();

                    gobjFluxoAprov.CodFluxoAprovacao = this.FluxoAprovacao[i].CodFluxoAprovacao;
                    gobjFluxoAprov.CodNivelAprovacao = this.FluxoAprovacao[i].CodNivelAprovacao;
                    gobjFluxoAprov.CodOrdemAprovacao = this.FluxoAprovacao[i].CodOrdemAprovacao;
                    gobjFluxoAprov.CodStatusSolicitacao = this.FluxoAprovacao[i].CodStatusSolicitacao;
                    gobjFluxoAprov.CodTipoSolicitacao = this.FluxoAprovacao[i].CodTipoSolicitacao;
                    gobjFluxoAprov.FlgAprovaAreaSeg = this.FluxoAprovacao[i].FlgAprovaAreaSeg;
                    gobjFluxoAprov.FlgAprovaContingencia = this.FluxoAprovacao[i].FlgAprovaContingencia;
                    gobjFluxoAprov.FlgSituacao = (this.FluxoAprovacao[i].FlgSituacao == null ? false : this.FluxoAprovacao[i].FlgSituacao);
                    gobjFluxoAprov.FlgAprovaAreaTI = (this.FluxoAprovacao[i].FlgAprovaAreaTI == null ? false : this.FluxoAprovacao[i].FlgAprovaAreaTI);
                    gobjFluxoAprov.FlgAprovaCracha = (this.FluxoAprovacao[i].FlgAprovaCracha == null ? false : this.FluxoAprovacao[i].FlgAprovaCracha);

                    if (this.FluxoAprovacao[i].CodFluxoAprovacao == 0)
                    {

                        gobjFluxoAprov.CodFluxoAprovacao = objBLSolicitacao.InserirFluxoAprov(gobjFluxoAprov);

                        this.FluxoAprovacao[i].CodFluxoAprovacao = gobjFluxoAprov.CodFluxoAprovacao;

                        blnGravado = true;
                    }
                    else
                    {
                        blnGravado = objBLSolicitacao.AlterarFluxoAprov(gobjFluxoAprov);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }

            return blnGravado;
        }

        #endregion

        #region Desabilitar Campos

        private void DesabilitarCampos(bool blnDesabillitar)
        {
            if (blnDesabillitar)
            {
                this.ddlNivelAprovador.Enabled = false;
                this.txtOrdem.Enabled = false;
                this.ddlStatus.Enabled = false;
                this.chkSegurancao.Enabled = false;
                this.chkContigencia.Enabled = false;
                this.chkAprovadorPermissaoCracha.Enabled = false;
            }
            else 
            {
                this.ddlNivelAprovador.Enabled = true;
                this.txtOrdem.Enabled = true;
                this.ddlStatus.Enabled = true;
                this.chkSegurancao.Enabled = true;
                this.chkContigencia.Enabled = true;
                this.chkAprovadorPermissaoCracha.Enabled = true;
            }
        }

        #endregion

        #region Bind Model
        /// <history>
        ///      [no history]
        ///      [haguiar] modify 25/04/2011 15:13
        ///      carregar e armazenar objeto e propriedade FlgAprovaAreaTI
        ///      [haguiar] modify 22/02/2012 11:21
        ///      carregar e armazenar objeto e propriedade FlgAprovaCracha
        /// </history>
        private void BindModelFluxoAprov(Enums.TipoTransacao pintTipoTransacao)
        {
            try
            {
                if (pintTipoTransacao == Enums.TipoTransacao.CarregarDados)
                {
                    gobjFluxoAprov = new Model.Solicitacao.FluxoAprovacao();

                    gobjFluxoAprov.CodFluxoAprovacao = this.CodFluxoAprovacao;
                    gobjFluxoAprov.CodNivelAprovacao = Convert.ToInt32(this.ddlNivelAprovador.SelectedValue);
                    gobjFluxoAprov.CodOrdemAprovacao = Convert.ToInt32(this.txtOrdem.Text);
                    gobjFluxoAprov.CodStatusSolicitacao = Convert.ToInt32(this.ddlStatus.SelectedValue);
                    gobjFluxoAprov.CodTipoSolicitacao = Convert.ToInt32(this.ddlTipoSolicitacao.SelectedValue);
                    gobjFluxoAprov.DesNivelAprovacao = (this.ddlNivelAprovador.SelectedValue == "0" ? String.Empty : this.ddlNivelAprovador.SelectedItem.Text);
                    gobjFluxoAprov.DesStatusAprovacao = this.ddlStatus.SelectedItem.Text;
                    gobjFluxoAprov.DesTipoSolicitacao = this.ddlTipoSolicitacao.SelectedItem.Text;
                    
                    gobjFluxoAprov.FlgAprovaAreaSeg = this.chkSegurancao.Checked;
                    gobjFluxoAprov.FlgAprovaContingencia = this.chkContigencia.Checked;
                    gobjFluxoAprov.FlgAprovaAreaTI = this.chkTI.Checked;
                    gobjFluxoAprov.FlgAprovaCracha = this.chkAprovadorPermissaoCracha.Checked;
                }
                else if (pintTipoTransacao == Enums.TipoTransacao.DescarregarDados)
                {
                    this.CodFluxoAprovacao = gobjFluxoAprov.CodFluxoAprovacao;

                    if (this.ddlNivelAprovador.Items.FindByValue(gobjFluxoAprov.CodNivelAprovacao.ToString()) != null)
                        this.ddlNivelAprovador.SelectedValue = gobjFluxoAprov.CodNivelAprovacao.ToString();
                    else
                        this.ddlNivelAprovador.SelectedIndex = 0;

                    this.txtOrdem.Text = gobjFluxoAprov.CodOrdemAprovacao.ToString();
                    
                    if (this.ddlStatus.Items.FindByValue(gobjFluxoAprov.CodStatusSolicitacao.ToString()) != null)
                        this.ddlStatus.SelectedValue = gobjFluxoAprov.CodStatusSolicitacao.ToString();
                    else
                        this.ddlStatus.SelectedIndex = 0;
                    
                    if (this.ddlTipoSolicitacao.Items.FindByValue(gobjFluxoAprov.CodTipoSolicitacao.ToString()) != null)
                        this.ddlTipoSolicitacao.SelectedValue = gobjFluxoAprov.CodTipoSolicitacao.ToString();
                    else
                        this.ddlTipoSolicitacao.SelectedIndex = 0;
                    
                    this.chkSegurancao.Checked = gobjFluxoAprov.FlgAprovaAreaSeg.Value;
                    this.chkContigencia.Checked = gobjFluxoAprov.FlgAprovaContingencia.Value;

                    this.chkTI.Checked = gobjFluxoAprov.FlgAprovaAreaTI.Value;
                    this.chkAprovadorPermissaoCracha.Checked = gobjFluxoAprov.FlgAprovaCracha.Value;

                }
                else if (pintTipoTransacao == Enums.TipoTransacao.Novo)
                {
                    this.CodFluxoAprovacao = 0;
                    this.ddlNivelAprovador.SelectedIndex = 0;
                    this.txtOrdem.Text = String.Empty;
                    this.ddlStatus.SelectedIndex = 0;
                    this.chkSegurancao.Checked = false;
                    this.chkContigencia.Checked = false;
                    this.chkTI.Checked = false;
                    this.chkAprovadorPermissaoCracha.Checked = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }


        #endregion

        #region RadGrid

        private void PopulaRadTipoSolic(bool blnBind)
        {
            BLSolicitacao objBLSolicitacao;
            Collection<SafWeb.Model.Solicitacao.TipoSolicitacao> colTipoSolic;

            try
            {
                objBLSolicitacao = new BLSolicitacao();
                colTipoSolic = new Collection<SafWeb.Model.Solicitacao.TipoSolicitacao>();

                colTipoSolic = objBLSolicitacao.ListarTipoSolicitacaoTodos();

                radTipoSolic.DataSource = colTipoSolic;
                if (blnBind)
                    radTipoSolic.DataBind();
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }

        private void PopulaRadFluxo(int pintCodTipoSolic, bool pblnBind)
        {
            BLSolicitacao objBLSolicitacao;
            
            try
            {
                objBLSolicitacao = new BLSolicitacao();
                
                FluxoAprovacao = objBLSolicitacao.ListarFluxoAprovacao(pintCodTipoSolic);

                radFluxoAprov.DataSource = FluxoAprovacao;
                if (pblnBind)
                    radFluxoAprov.DataBind();

                this.BindModelFluxoAprov(Enums.TipoTransacao.Novo);
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }

        #endregion

        #endregion

        #region Eventos

        #region Botoes

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            this.ControlaPaineis(Enums.TipoPainel.Listagem);
            this.lblMensagemCadastro.Text = String.Empty;
        }

        protected void btnGravarSair_Click(object sender, EventArgs e)
        {
            if (this.FluxoAprovacao.Count > 0)
            {
                if (this.Gravar())
                {
                    this.BindModelFluxoAprov(Enums.TipoTransacao.Novo);

                    this.ControlaPaineis(Enums.TipoPainel.Listagem);

                    this.lblMensagemListagem.Visible = true;
                    this.lblMensagemListagem.Text = BLIdiomas.TraduzirMensagens(Convert.ToInt32(Mensagens.REGISTRO_GRAVAR));
                    this.lblMensagemListagem.ForeColor = System.Drawing.Color.FromName("#154E7A");
                }
            }
        }

        protected void btnGravar_Click(object sender, EventArgs e)
        {
            if (this.FluxoAprovacao.Count > 0)
            {
                if (this.Gravar())
                {
                    this.BindModelFluxoAprov(Enums.TipoTransacao.Novo);

                    this.ControlaPaineis(Enums.TipoPainel.Cadastro);

                    this.PopulaRadFluxo(Convert.ToInt32(this.ddlTipoSolicitacao.SelectedValue), true);

                    this.lblMensagemCadastro.Visible = true;
                    this.lblMensagemCadastro.Text = BLIdiomas.TraduzirMensagens(Convert.ToInt32(Mensagens.REGISTRO_GRAVAR));
                    this.lblMensagemCadastro.ForeColor = System.Drawing.Color.FromName("#154E7A");
                }
            }
        }

        protected void btnAdicionar_Click(object sender, EventArgs e)
        {
            bool blnOrdem = false;

            try
            {

                if (this.CodFluxoAprovacao > 0)
                {
                    int intIndice = -1;

                    for (int i = 0; i < this.FluxoAprovacao.Count; i++)
                    {
                        if (this.FluxoAprovacao[i].CodOrdemAprovacao == Convert.ToInt32(this.txtOrdem.Text) &&
                            this.FluxoAprovacao[i].CodFluxoAprovacao != this.CodFluxoAprovacao)
                        {
                            blnOrdem = true;
                            break;
                        }

                        if (this.FluxoAprovacao[i].CodFluxoAprovacao == this.CodFluxoAprovacao)
                            intIndice = i;
                    }

                    if (!blnOrdem)
                    {
                        this.BindModelFluxoAprov(Enums.TipoTransacao.CarregarDados);
                        this.FluxoAprovacao.RemoveAt(intIndice);
                        this.FluxoAprovacao.Insert(intIndice, gobjFluxoAprov);

                        radFluxoAprov.DataSource = this.FluxoAprovacao;
                        radFluxoAprov.DataBind();
                    }
                    else
                    {
                        this.lblMensagemCadastro.Text = "Por favor, informe uma ordem de aprovação diferente!";
                        this.lblMensagemCadastro.Visible = true;
                        this.lblMensagemCadastro.ForeColor = System.Drawing.Color.Red;
                    }
                }
                else
                {
                    for (int i = 0; i < this.FluxoAprovacao.Count; i++)
                    {
                        if (this.FluxoAprovacao[i].CodOrdemAprovacao == Convert.ToInt32(this.txtOrdem.Text))
                        {
                            blnOrdem = true;
                            break;
                        }
                    }

                    if (!blnOrdem)
                    {
                        this.BindModelFluxoAprov(Enums.TipoTransacao.CarregarDados);
                        gobjFluxoAprov.FlgSituacao = true;
                        this.FluxoAprovacao.Add(gobjFluxoAprov);

                        radFluxoAprov.DataSource = this.FluxoAprovacao;
                        radFluxoAprov.DataBind();
                    }
                    else
                    {
                        this.lblMensagemCadastro.Text = "Por favor, informe uma ordem de aprovação diferente!";
                        this.lblMensagemCadastro.Visible = true;
                        this.lblMensagemCadastro.ForeColor = System.Drawing.Color.Red;
                    }
                }

                this.BindModelFluxoAprov(Enums.TipoTransacao.Novo);
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }

        #endregion

        #region ViewState

        public int CodFluxoAprovacao
        {
            get
            {
                if (ViewState["vsCodFluxoAprovacao"] == null)
                {
                    ViewState["vsCodFluxoAprovacao"] = 0;
                }
                return (int)ViewState["vsCodFluxoAprovacao"];
            }
            set
            {
                ViewState["vsCodFluxoAprovacao"] = value;
            }
        }

        public Collection<Model.Solicitacao.FluxoAprovacao> FluxoAprovacao
        {
            get
            {
                if (ViewState["vsFluxoAprovacao"] == null)
                    return new Collection<SafWeb.Model.Solicitacao.FluxoAprovacao>();
                else
                    return (Collection<Model.Solicitacao.FluxoAprovacao>)ViewState["vsFluxoAprovacao"];
            }
            set
            {
                ViewState["vsFluxoAprovacao"] = value;
            }
        }

        #endregion

        #region RadGrid

        protected void radFluxoAprov_NeedDataSource(object source, Telerik.WebControls.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (this.Page.IsPostBack)
                {
                    radFluxoAprov.DataSource = FluxoAprovacao;
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }

        /// <history>
        ///      [no history]
        ///      [haguiar] modify 25/04/2011 15:47
        ///      carregar valor chkTI de FlgAprovaAreaTI
        /// </history>
        protected void radFluxoAprov_ItemCommand(object source, Telerik.WebControls.GridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.Trim() == "Editar")
                {
                    this.CodFluxoAprovacao = Convert.ToInt32(e.Item.Cells[2].Text);

                    this.txtOrdem.Text = e.Item.Cells[5].Text;

                    if (this.ddlStatus.Items.FindByValue(e.Item.Cells[6].Text) != null)
                        this.ddlStatus.SelectedValue = e.Item.Cells[6].Text;
                    else
                        this.ddlStatus.SelectedIndex = 0;

                    if (this.ddlNivelAprovador.Items.FindByValue(e.Item.Cells[16].Text) != null)
                        this.ddlNivelAprovador.SelectedValue = e.Item.Cells[16].Text;
                    else
                        this.ddlNivelAprovador.SelectedIndex = 0;

                    if (this.ddlTipoSolicitacao.Items.FindByValue(e.Item.Cells[3].Text) != null)
                        this.ddlTipoSolicitacao.SelectedValue = e.Item.Cells[3].Text;
                    else
                        this.ddlTipoSolicitacao.SelectedIndex = 0;

                    this.chkSegurancao.Checked = Convert.ToBoolean(e.Item.Cells[8].Text);
                    this.chkContigencia.Checked = Convert.ToBoolean(e.Item.Cells[10].Text);

                    bool blnchkTI;
                    Boolean.TryParse(e.Item.Cells[12].Text, out blnchkTI);
                    chkTI.Checked = blnchkTI;

                    bool blnchkPermCracha;
                    Boolean.TryParse(e.Item.Cells[14].Text, out blnchkPermCracha);
                    this.chkAprovadorPermissaoCracha.Checked = blnchkPermCracha;

                    if (!Permissoes.Alteração())
                        DesabilitarCampos(true);
                    else
                        DesabilitarCampos(false);
                }

                if (e.CommandName.Trim() == "Ativar")
                {
                    Model.Solicitacao.FluxoAprovacao objFluxoAprovacao = new Model.Solicitacao.FluxoAprovacao();

                    objFluxoAprovacao.CodNivelAprovacao = -1;
                    objFluxoAprovacao.CodStatusSolicitacao = -1;

                    Telerik.WebControls.GridDataItem dataItem = (Telerik.WebControls.GridDataItem)e.Item;
                    ImageButton btnAtivar = (ImageButton)dataItem["Ativar"].Controls[0];

                    if (btnAtivar.ToolTip == "Ativar")
                    {
                        btnAtivar.ImageUrl = "~/imagens/icones/ico_ativo.gif";
                        btnAtivar.ToolTip = "Inativar";
                        objFluxoAprovacao.FlgSituacao = true;
                    }
                    else if (btnAtivar.ToolTip == "Inativar")
                    {
                        btnAtivar.ImageUrl = "~/imagens/icones/ico_inativo.gif";
                        btnAtivar.ToolTip = "Ativar";
                        objFluxoAprovacao.FlgSituacao = false;
                    }

                    BLSolicitacao objBLSolicitacao = new BLSolicitacao();

                    objFluxoAprovacao.CodFluxoAprovacao = Convert.ToInt32(e.CommandArgument.ToString().Trim());

                    objBLSolicitacao.AlterarFluxoAprov(objFluxoAprovacao);

                    // alterando na colection
                    for (int i = 0; i < this.FluxoAprovacao.Count; i++)
                    {
                        if (this.FluxoAprovacao[i].CodFluxoAprovacao == Convert.ToInt32(e.CommandArgument.ToString()))
                        {
                            this.FluxoAprovacao[i].FlgSituacao = objFluxoAprovacao.FlgSituacao;
                        }
                    }
                }

                if (e.CommandName.Trim() == "IrPagina")
                {
                    string strPageIndexString = ((TextBox)e.Item.FindControl("txtPagina")).Text;
                    int intPageIndex = e.Item.OwnerTableView.CurrentPageIndex;
                    if ((!object.ReferenceEquals(strPageIndexString.Trim(), string.Empty)))
                    {
                        if (e.Item.OwnerTableView.PageCount < Convert.ToInt32(strPageIndexString))
                        {
                            strPageIndexString = (e.Item.OwnerTableView.PageCount).ToString();
                            ((TextBox)e.Item.FindControl("txtPagina")).Text = strPageIndexString;
                        }
                        intPageIndex = Convert.ToInt32(strPageIndexString) - 1;
                    }

                    if (!(e.Item.OwnerTableView.CurrentPageIndex == intPageIndex))
                    {
                        e.Item.OwnerTableView.CurrentPageIndex = intPageIndex;
                        e.Item.OwnerTableView.Rebind();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }

        /// <history>
        ///      [no history]
        ///      [haguiar] modify 25/04/2011 15:47
        ///      carregar valor chkTI de FlgAprovaAreaTI
        /// </history>
        protected void radFluxoAprov_ItemDataBound(object sender, Telerik.WebControls.GridItemEventArgs e)
        {
            ImageButton imgEditar = null;
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
                            btnAtivar.CommandArgument = DataBinder.Eval(e.Item.DataItem, "CodFluxoAprovacao").ToString();
                        }
                        else
                        {
                            btnAtivar.Enabled = false;
                        }
                    }

                    imgEditar = (ImageButton)(e.Item.FindControl("imgEditar"));
                    imgEditar.Visible = false;
                    e.Item.Attributes.Add("onMouseOver", "selecionaGrid(this)");
                    e.Item.Attributes.Add("onMouseOut", "deSelecionaGrid(this)");
                    e.Item.Style["cursor"] = "hand";

                    // Cursor 
                    int intCell = e.Item.Cells.Count;
                    for (int @int = 0; @int <= intCell - 2; @int++)
                    {
                        if (@int < 15)
                            e.Item.Cells[@int].Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(imgEditar, ""));
                    }

                    if (Permissoes.Alteração())
                    {
                        e.Item.ToolTip = BLIdiomas.TraduzirMensagens(Mensagens.EDITAR);
                    }
                    else
                    {
                        e.Item.ToolTip = BLIdiomas.TraduzirMensagens(Mensagens.VISUALIZAR);
                    }

                    CheckBox chkAprovaAreaSeg = (CheckBox)e.Item.FindControl("chkAprovaAreaSeg");
                    chkAprovaAreaSeg.Checked = Convert.ToBoolean(e.Item.Cells[8].Text);

                    CheckBox chkAprovaContingencia = (CheckBox)e.Item.FindControl("chkAprovaContingencia");
                    chkAprovaContingencia.Checked = Convert.ToBoolean(e.Item.Cells[10].Text);

                    CheckBox chkTI = (CheckBox)e.Item.FindControl("chkTI");

                    bool blnchkTI;
                    Boolean.TryParse(e.Item.Cells[12].Text, out blnchkTI);
                    
                    chkTI.Checked = blnchkTI;

                    CheckBox chkAprovaCracha = (CheckBox)e.Item.FindControl("chkAprovaCracha");

                    bool blnchkAprovaCracha;
                    Boolean.TryParse(e.Item.Cells[14].Text, out blnchkAprovaCracha);

                    chkAprovaCracha.Checked = blnchkAprovaCracha;
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }

        protected void radTipoSolic_NeedDataSource(object source, Telerik.WebControls.GridNeedDataSourceEventArgs e)
        {
            if(this.Page.IsPostBack)
            {
                PopulaRadTipoSolic(false);
            }
        }

        protected void radTipoSolic_ItemCommand(object source, Telerik.WebControls.GridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.Trim() == "Editar")
                {
                    this.PopulaRadFluxo(Convert.ToInt32(e.Item.Cells[2].Text), true);

                    this.ddlTipoSolicitacao.SelectedValue = e.Item.Cells[2].Text;

                    this.ControlaPaineis(Enums.TipoPainel.Cadastro);
                }

                if (e.CommandName.Trim() == "IrPagina")
                {
                    string strPageIndexString = ((TextBox)e.Item.FindControl("txtPagina")).Text;
                    int intPageIndex = e.Item.OwnerTableView.CurrentPageIndex;
                    if ((!object.ReferenceEquals(strPageIndexString.Trim(), string.Empty)))
                    {
                        if (e.Item.OwnerTableView.PageCount < Convert.ToInt32(strPageIndexString))
                        {
                            strPageIndexString = (e.Item.OwnerTableView.PageCount).ToString();
                            ((TextBox)e.Item.FindControl("txtPagina")).Text = strPageIndexString;
                        }
                        intPageIndex = Convert.ToInt32(strPageIndexString) - 1;
                    }

                    if (!(e.Item.OwnerTableView.CurrentPageIndex == intPageIndex))
                    {
                        e.Item.OwnerTableView.CurrentPageIndex = intPageIndex;
                        e.Item.OwnerTableView.Rebind();
                    }
                }
            }
            catch(Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }

        protected void radTipoSolic_ItemDataBound(object sender, Telerik.WebControls.GridItemEventArgs e)
        {
            ImageButton imgEditar = null;

            try
            {
                if (e.Item.ItemType == Telerik.WebControls.GridItemType.Item || e.Item.ItemType == Telerik.WebControls.GridItemType.AlternatingItem)
                {
                    imgEditar = (ImageButton)(e.Item.FindControl("imgEditar"));
                    imgEditar.Visible = false;
                    e.Item.Attributes.Add("onMouseOver", "selecionaGrid(this)");
                    e.Item.Attributes.Add("onMouseOut", "deSelecionaGrid(this)");
                    e.Item.Style["cursor"] = "hand";

                    int intCell = e.Item.Cells.Count;
                    for (int @int = 0; @int <= intCell - 2; @int++)
                    {
                        e.Item.Cells[@int].Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(imgEditar, ""));
                    }

                    if (Permissoes.Alteração())
                        e.Item.ToolTip = BLIdiomas.TraduzirMensagens(Mensagens.EDITAR);
                    else
                        e.Item.ToolTip = BLIdiomas.TraduzirMensagens(Mensagens.VISUALIZAR);
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        
        }

        #endregion

        #region Combo

        protected void ddlTipoSolicitacao_SelectedIndexChanged(object sender, EventArgs e)
        {
            //try
            //{ 
            //    this.PopulaRadFluxo(Convert.ToInt32(this.ddlTipoSolicitacao.SelectedValue), true);
            //}
            //catch(Exception ex)
            //{
            //  ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            //}
        }

        #endregion

        #endregion

    }
}
