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
using SafWeb.BusinessLayer.Regional;
using SafWeb.BusinessLayer.Area;
using SafWeb.BusinessLayer.Filial;
using SafWeb.BusinessLayer.Empresa;
using FrameWork.Model.Utilitarios;
using FrameWork.BusinessLayer.Utilitarios;
using FrameWork.BusinessLayer.Usuarios;
using SafWeb.BusinessLayer.Solicitacao;
using SafWeb.BusinessLayer.Colaborador;
using Telerik.WebControls;
using System.Collections.Generic;
using SafWeb.BusinessLayer.Escala;
using SafWeb.Model.Escala;
using System.Text;
using SafWeb.Model.Filial;

namespace SafWeb.UI.Modulos.Aprovacao
{
    public partial class CadCaixaEntrada : FWPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.InicializaScripts();

            if (!Page.IsPostBack)
            {
                this.PopularCombos();

                if (Convert.ToString(Request.QueryString["Hist"]) == "true")
                {
                    this.ControlaPaineis(TipoAba.HistoricoVis);
                }
                else
                {
                    this.ControlaPaineis(TipoAba.Pendentes);
                }

                this.VerificaPermissoes();

                /*
                this.PopularRadGridPendentes(true);
                this.PopularRadGridEscalaPendentes(true);
                this.PopularRadGridHistorico(true);
                this.PopularRadGridHistoricoColab(true);
                */

                //seleciona as checkbox que já estavam selecionadas
                if (Session["lstCheck"] != null)
                {
                    this.lstCheck = (List<int>)Session["lstCheck"];

                    //seleciona as solicitações
                    foreach (Telerik.WebControls.GridDataItem rdiPendentes in radPendentes.Items)
                    {
                        try
                        {
                            if (rdiPendentes["TemplateColumn"].FindControl("chkItem") != null)
                                for (int i = 0; i < this.lstCheck.Count; i++)
                                {
                                    if (Convert.ToInt32(rdiPendentes["Codigo"].Text) == this.lstCheck[i])
                                        ((CheckBox)rdiPendentes["TemplateColumn"].FindControl("chkItem")).Checked = true;
                                }
                        }
                        catch (Exception ex)
                        {
                            ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
                        }
                    }

                    Session["lstCheck"] = null;
                    this.lstCheck = null;
                }

                //seleciona as checkbox que já estavam selecionadas
                if (Session["lstCheckEsc"] != null)
                {
                    this.ListaCheckEscalas = (List<int>)Session["lstCheckEsc"];

                    //seleciona as escalações
                    foreach (Telerik.WebControls.GridDataItem rdiPendentes in radEscalasPendente.Items)
                    {
                        try
                        {
                            if (rdiPendentes["TemplateColumn"].FindControl("chkeItem") != null)
                                for (int i = 0; i < this.ListaCheckEscalas.Count; i++)
                                {
                                    if (Convert.ToInt32(rdiPendentes["IdEscalacao"].Text) == this.ListaCheckEscalas[i])
                                        ((CheckBox)rdiPendentes["TemplateColumn"].FindControl("chkeItem")).Checked = true;
                                }
                        }
                        catch (Exception ex)
                        {
                            ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
                        }
                    }

                    Session["lstCheckEsc"] = null;
                    this.ListaCheckEscalas = null;
                }


                //verifica atributos do aprovador
                BLAprovadorFilial objBLAprovadorFilial = new BLAprovadorFilial();

                try
                {
                    this.ColAtributosAprovador = objBLAprovadorFilial.ListarAtributos(Convert.ToInt32(BLAcesso.IdUsuarioLogado()));
            }
                catch (Exception ex)
                {
                    ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
        }

                this.divCrachaTitular.Visible = false;
                this.tdCrachaTitular.Visible = false;

                if (ColAtributosAprovador != null)
                {

                    foreach (AprovadorFilial gobjAprovadorFilial in ColAtributosAprovador)
                    {
                        if (gobjAprovadorFilial.Flg_AprovaCracha)
                        {
                            this.tdCrachaTitular.Visible = true;
                            this.divCrachaTitular.Visible = true;
                            break;
                        }
                    }
                }

            }
        }

        #region Inicializar Scripts

        private void InicializaScripts()
        {
            this.txtDataInicio.Attributes.Add("OnKeyPress", "return FormataData(event,this);");
            this.txtDataFim.Attributes.Add("OnKeyPress", "return FormataData(event,this);");
            this.txtNumSolicitacao.Attributes.Add("OnKeyPress", "return mascara_Numero(this, event.keyCode);");
            this.btnHelp.Attributes.Add("OnClick", "AbrirHelp();");

        }

        #endregion

        #region Controla Paineis

        public enum TipoAba
        {
            Pendentes = 0,
            HistoricoVis = 1,
            HistoricoColb = 2,
        }

        /// <history>
        ///     [no history]
        ///     [haguiar] 17/03/2011 16:22
        ///     popular apenas dados da aba pertinente
        /// </history>
        private void ControlaPaineis(TipoAba pintPainel)
        {
            
            if (pintPainel == TipoAba.HistoricoVis)
            {
                this.PopularRadGridHistorico(true);

                divAbaPendentes.Visible = false;
                divAbaHistorico.Visible = true;
                divAbaHistoricoColab.Visible = false;

                btnAbaHistoricoColab.CssClass = "cadbuttonAbaInativa";
                btnAbaPendente.CssClass = "cadbuttonAbaInativa";
                btnAbaHistorico.CssClass = "cadbuttonAbaAtiva";

                imgAbaHistorico.ImageUrl = "%%PATH%%/Imagens/right-abaAtiva.gif";
                imgAbaPendetes.ImageUrl = "%%PATH%%/Imagens/right-abaInativa.gif";
                imgAbaHistoricoColab.ImageUrl = "%%PATH%%/Imagens/right-abaInativa.gif";
            }
            else if (pintPainel == TipoAba.Pendentes)
            {

                this.PopularRadGridPendentes(true);
                this.PopularRadGridEscalaPendentes(true);
                this.PopularRadGridCrachaTitular(true);

                divAbaPendentes.Visible = true;
                divAbaHistorico.Visible = false;
                divAbaHistoricoColab.Visible = false;

                btnAbaHistoricoColab.CssClass = "cadbuttonAbaInativa";
                btnAbaPendente.CssClass = "cadbuttonAbaAtiva";
                btnAbaHistorico.CssClass = "cadbuttonAbaInativa";

                imgAbaHistorico.ImageUrl = "%%PATH%%/Imagens/right-abaInativa.gif";
                imgAbaPendetes.ImageUrl = "%%PATH%%/Imagens/right-abaAtiva.gif";
                imgAbaHistoricoColab.ImageUrl = "%%PATH%%/Imagens/right-abaInativa.gif";
            }
            else
            {
                this.PopularRadGridHistoricoColab(true);

                divAbaPendentes.Visible = false;
                divAbaHistorico.Visible = false;
                divAbaHistoricoColab.Visible = true;

                btnAbaHistoricoColab.CssClass = "cadbuttonAbaAtiva";
                btnAbaPendente.CssClass = "cadbuttonAbaInativa";
                btnAbaHistorico.CssClass = "cadbuttonAbaInativa";

                imgAbaHistorico.ImageUrl = "%%PATH%%/Imagens/right-abaInativa.gif";
                imgAbaPendetes.ImageUrl = "%%PATH%%/Imagens/right-abaInativa.gif";
                imgAbaHistoricoColab.ImageUrl = "%%PATH%%/Imagens/right-abaAtiva.gif";
            }
        }

        #endregion

        #region Verificar Permissõe

        private void VerificaPermissoes()
        {
            this.btnAprovar.Enabled = Permissoes.Inclusão();
            this.btnReprovar.Enabled = Permissoes.Inclusão();
        }

        #endregion

        #region RadWindow

        private void AbrirRadWindow(int pintCodLista, int pintCodSolicitacao)
        {
            //cria radwindow
            RadWindow rwdWindow = new RadWindow();
            RadWindowManager rwmWindowManager = new RadWindowManager();

            rwmWindowManager.Skin = "Office2007";
            //Não exibe nenhum botão na janela
            rwmWindowManager.Behavior = Telerik.WebControls.RadWindowBehaviorFlags.None;

            rwdWindow.Width = Unit.Pixel(530);
            rwdWindow.Height = Unit.Pixel(400);
            rwdWindow.Modal = true;
            rwdWindow.ReloadOnShow = true;
            rwdWindow.DestroyOnClose = true;
            rwdWindow.VisibleOnPageLoad = true;
            rwdWindow.Title = "Lista de Visitantes";
            rwdWindow.NavigateUrl = "ListSolicitacaoLista.aspx?CodSolicitacao=" + pintCodSolicitacao.ToString() + "&CodLista=" + pintCodLista.ToString();
            rwmWindowManager.Windows.Add(rwdWindow);

            Panel pnlExclusao = null;

            //Tenta encontrar na master
            pnlExclusao = (Panel)this.FindControl("pnlLista");
            pnlExclusao.Controls.Add(rwmWindowManager);
        }

        #region AbrirRadWindow Lista Colaboradores
        /// <summary>
        /// AbrirRadWindow dos Colaboradores.
        /// </summary>
        /// <param name="pintIdEscalacao">Id Escalação</param>
        /// <history>
        ///     [cmarchi] created 27/1/2010
        /// </history>
        private void AbrirRadWindow(int pintIdEscalacao)
        {
            //cria radwindow
            RadWindow rwdWindow = new RadWindow();
            RadWindowManager rwmWindowManager = new RadWindowManager();

            rwmWindowManager.Skin = "Office2007";
            //Não exibe nenhum botão na janela
            rwmWindowManager.Behavior = Telerik.WebControls.RadWindowBehaviorFlags.None;
            rwmWindowManager.ShowContentDuringLoad = false;
            rwmWindowManager.VisibleStatusbar = false;

            rwdWindow.Width = Unit.Pixel(527);
            rwdWindow.Height = Unit.Pixel(406);
            rwdWindow.Modal = true;
            rwdWindow.ReloadOnShow = true;
            rwdWindow.DestroyOnClose = true;
            rwdWindow.VisibleOnPageLoad = true;

            rwdWindow.Title = "Listagem de Colaboradores";

            rwdWindow.NavigateUrl = "../Escala/ListColaboradoresEscala.aspx?open=" + BLEncriptacao.EncQueryStr("CadCaixa#"
                + pintIdEscalacao.ToString());
            rwmWindowManager.Windows.Add(rwdWindow);

            Panel pnlEscala = null;

            //Tenta encontrar na master
            pnlEscala = (Panel)this.FindControl("pnlLista");
            pnlEscala.Controls.Add(rwmWindowManager);
        }
        #endregion

        #endregion

        #region Pendentes

        #region ViewState

        public List<int> lstCheck
        {
            get
            {
                if (ViewState["vsList"] == null)
                {
                    ViewState["vsList"] = new List<int>();
                }
                return (List<int>)ViewState["vsList"];
            }
            set
            {
                ViewState["vsList"] = value;
            }
        }

        #endregion

        #region Métodos

        private void PopularRadGridPendentes(bool pblnBind)
        {
            BLSolicitacao objBLSolicitacao = null;
            Collection<SafWeb.Model.Solicitacao.Solicitacao> colSolicitacoes = null;

            try
            {
                objBLSolicitacao = new BLSolicitacao();
                colSolicitacoes = new Collection<SafWeb.Model.Solicitacao.Solicitacao>();

                colSolicitacoes = objBLSolicitacao.ListarSolicPendAprov(BLAcesso.IdUsuarioLogado());

                this.radPendentes.DataSource = colSolicitacoes;
                if (pblnBind)
                    this.radPendentes.DataBind();
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }

        #endregion

        #region Eventos

        #region Botões

        /// <history>
        ///     [no history]
        ///     [haguiar_4] 21/01/2011 15:21
        ///     apresentar mensagem da aprovação em alert
        ///     [haguiar] 18/03/2011 15:06
        ///     listar apenas solicitacoes para nao comprometer a performance
        /// </history>
        protected void btnAprovar_Click(object sender, EventArgs e)
        {
            BLSolicitacao objBLSolicitacao = null;
            BLEscala objBLEscala = new BLEscala();

            bool blnAprovacaoSolicitacao = false;
            bool blnAprovacaoEscala = false;
            bool blnAprovacaoCrachaTitular = false;

            StringBuilder strIdEscalacao = new StringBuilder();

            try
            {
                //Aprova as escalas
                foreach (Telerik.WebControls.GridDataItem rdiPendente in radEscalasPendente.Items)
                {
                    if (((CheckBox)rdiPendente.Cells[0].FindControl("chkeItem")).Checked)
                    {
                        strIdEscalacao.Append(rdiPendente.Cells[3].Text + ",");
                    }
                }

                if (strIdEscalacao.Length > 0)
                {
                    strIdEscalacao.Remove(strIdEscalacao.Length - 1, 1);

                    objBLEscala.AprovarEscalacao(
                        strIdEscalacao.ToString(), BLAcesso.IdUsuarioLogado());

                    blnAprovacaoEscala = true;
                }


                //APROVA as solicitações
                objBLSolicitacao = new BLSolicitacao();

                foreach (Telerik.WebControls.GridDataItem rdiPendente in radPendentes.Items)
                {
                    if (((CheckBox)rdiPendente.Cells[0].FindControl("chkItem")).Checked)
                    {
                        objBLSolicitacao.AprovarSolicitacao(Convert.ToInt32(rdiPendente.Cells[3].Text), BLAcesso.IdUsuarioLogado());
                        blnAprovacaoSolicitacao = true;
                    }
                }

                //APROVA as solicitações
                objBLSolicitacao = new BLSolicitacao();

                foreach (Telerik.WebControls.GridDataItem rdiPendente in this.radCrachaTitular.Items)
                {
                    if (((CheckBox)rdiPendente.Cells[0].FindControl("chkItem")).Checked)
                    {
                        objBLSolicitacao.AprovarSolicitacaoCrachaTitular(Convert.ToInt32(rdiPendente.Cells[3].Text), Convert.ToInt32(BLAcesso.IdUsuarioLogado()));

                        blnAprovacaoCrachaTitular = true;
                    }
                }

                if (blnAprovacaoSolicitacao || blnAprovacaoEscala || blnAprovacaoCrachaTitular)
                {
                    this.txtObservacao.Text = string.Empty;
                    this.lblMensagem.ForeColor = System.Drawing.Color.Empty;
                    this.lblMensagem.Visible = true;
                    this.lblMensagem.Text = "Aprovação realizada com sucesso!";
                    this.lblMensagem.ForeColor = System.Drawing.Color.Red;
                    this.pnlObservacao.Visible = false;

                    this.PopularRadGridPendentes(true);
                    this.PopularRadGridEscalaPendentes(true);
                    this.PopularRadGridCrachaTitular(true);

                    this.RadAjaxPanel1.Alert("Aprovação realizada com sucesso!");

                }
                else
                {
                    this.txtObservacao.Text = string.Empty;
                    this.lblMensagem.Visible = true;
                    this.lblMensagem.Text = "Por favor, selecione uma ou mais escalas, solicitações ou permissões de crachá titular!";
                    this.lblMensagem.ForeColor = System.Drawing.Color.FromName("#154E7A");
                    this.pnlObservacao.Visible = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }

        protected void btnReprovar_Click(object sender, EventArgs e)
        {
            bool blnReprovacaoSolicitacao = false;
            bool blnReprovacaoEscala = false;
            bool blnReprovacaoCrachaTitular = false;

            //verifica as escalas selecionadas para reprovação
            foreach (Telerik.WebControls.GridDataItem rdiPendente in radEscalasPendente.Items)
            {
                if (((CheckBox)rdiPendente.Cells[0].FindControl("chkeItem")).Checked)
                {
                    blnReprovacaoEscala = true;
                }
            }

            //verifica as solicitações selecionadas para reprovação
            foreach (Telerik.WebControls.GridDataItem rdiPendente in radPendentes.Items)
            {
                if (((CheckBox)rdiPendente.Cells[0].FindControl("chkItem")).Checked)
                {
                    blnReprovacaoSolicitacao = true;
                }
            }

            //verifica as solicitações cracha titular selecionadas para reprovação
            foreach (Telerik.WebControls.GridDataItem rdiPendente in this.radCrachaTitular.Items)
            {
                if (((CheckBox)rdiPendente.Cells[0].FindControl("chkItem")).Checked)
                {
                    blnReprovacaoSolicitacao = true;
                }
            }


            if (blnReprovacaoSolicitacao || blnReprovacaoEscala || blnReprovacaoCrachaTitular)
            {
                this.pnlObservacao.Visible = true;
            }
            else if (!blnReprovacaoSolicitacao)
            {
                this.lblMensagem.Visible = true;
                this.lblMensagem.Text = "Por favor, selecione uma ou mais solicitações!";
                this.lblMensagem.ForeColor = System.Drawing.Color.FromName("#154E7A");
            }
            else if (!blnReprovacaoEscala)
            {
                this.lblMensagem.Visible = true;
                this.lblMensagem.Text = "Por favor, selecione uma ou mais escalas!";
                this.lblMensagem.ForeColor = System.Drawing.Color.FromName("#154E7A");
            }
            else if (!blnReprovacaoCrachaTitular)
            {
                this.lblMensagem.Visible = true;
                this.lblMensagem.Text = "Por favor, selecione uma ou mais permissões de crachá titular!";
                this.lblMensagem.ForeColor = System.Drawing.Color.FromName("#154E7A");
        }
        }

        /// <history>
        ///     [no history]
        ///     [haguiar_4] 21/01/2011 15:21
        ///     apresentar mensagem da reprovação em alert
        /// </history>
        protected void btnGravar_Click(object sender, EventArgs e)
        {
            BLSolicitacao objBLSolicitacao = null;
            BLEscala objBLEscala = new BLEscala();

            bool blnReprovacaoEscala = false;
            bool blnReprovacaoSolicitacao = false;
            bool blnReprovacaoCrachaTitular = false;

            StringBuilder strIdEscalacao = new StringBuilder();

            try
            {
                //reprova as escalas
                foreach (Telerik.WebControls.GridDataItem rdiPendente in radEscalasPendente.Items)
                {
                    if (((CheckBox)rdiPendente.Cells[0].FindControl("chkeItem")).Checked)
                    {
                        strIdEscalacao.Append(rdiPendente.Cells[3].Text + ",");
                    }
                }

                if (strIdEscalacao.Length > 0)
                {
                    strIdEscalacao.Remove(strIdEscalacao.Length - 1, 1);

                    objBLEscala.ReprovarEscalacao(
                        strIdEscalacao.ToString(), BLAcesso.IdUsuarioLogado(), txtObservacao.Text.Trim());

                    blnReprovacaoEscala = true;
                }


                //reprova as solicitações
                objBLSolicitacao = new BLSolicitacao();

                foreach (Telerik.WebControls.GridDataItem rdiPendente in radPendentes.Items)
                {
                    if (((CheckBox)rdiPendente.Cells[0].FindControl("chkItem")).Checked)
                    {
                        objBLSolicitacao.ReprovarSolicitacao(Convert.ToInt32(rdiPendente.Cells[3].Text), BLAcesso.IdUsuarioLogado(), txtObservacao.Text);
                        blnReprovacaoSolicitacao = true;
                    }
                }


                //reprova as solicitações cracha titular
                objBLSolicitacao = new BLSolicitacao();

                foreach (Telerik.WebControls.GridDataItem rdiPendente in this.radCrachaTitular.Items)
                {
                    if (((CheckBox)rdiPendente.Cells[0].FindControl("chkItem")).Checked)
                    {
                        objBLSolicitacao.ReprovarSolicitacaoCrachaTitular(Convert.ToInt32(rdiPendente.Cells[3].Text), Convert.ToInt32(BLAcesso.IdUsuarioLogado()), txtObservacao.Text);
                        blnReprovacaoCrachaTitular = true;
                    }
                }

                if (blnReprovacaoSolicitacao || blnReprovacaoEscala || blnReprovacaoCrachaTitular)
                {
                    this.txtObservacao.Text = string.Empty;
                    this.lblMensagem.ForeColor = System.Drawing.Color.Empty;
                    this.lblMensagem.Visible = true;
                    this.lblMensagem.Text = "Reprovação realizada com sucesso!";
                    this.lblMensagem.ForeColor = System.Drawing.Color.Red;
                    this.pnlObservacao.Visible = false;

                    this.PopularRadGridPendentes(true);
                    this.PopularRadGridEscalaPendentes(true);
                    this.PopularRadGridCrachaTitular(true);

                    this.RadAjaxPanel1.Alert("Reprovação realizada com sucesso!");
                }
                else
                {
                    this.txtObservacao.Text = string.Empty;
                    this.lblMensagem.Visible = true;
                    this.lblMensagem.Text = "Por favor, selecione uma ou mais escalas ou solicitações!";
                    this.lblMensagem.ForeColor = System.Drawing.Color.FromName("#154E7A");
                    this.pnlObservacao.Visible = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }

        protected void btnAbaPendente_Click(object sender, EventArgs e)
        {
            this.ControlaPaineis(TipoAba.Pendentes);
        }

        protected void btnAbaHistorico_Click(object sender, EventArgs e)
        {
            this.ControlaPaineis(TipoAba.HistoricoVis);
        }

        protected void btnAbaHistoricoColab_Click(object sender, EventArgs e)
        {
            this.ControlaPaineis(TipoAba.HistoricoColb);
        }

        #endregion

        #region RadGrid

        protected void radPendentes_ItemDataBound(object sender, Telerik.WebControls.GridItemEventArgs e)
        {
            LinkButton lnkVisitante;
            ImageButton btnEditar;

            if (e.Item.ItemType == Telerik.WebControls.GridItemType.Item || e.Item.ItemType == Telerik.WebControls.GridItemType.AlternatingItem)
            {
                try
                {
                    e.Item.ForeColor = System.Drawing.Color.FromName(e.Item.Cells[30].Text);

                    lnkVisitante = (LinkButton)(e.Item.FindControl("lnkVisitante"));
                    lnkVisitante.Text = e.Item.Cells[27].Text;
                    lnkVisitante.Enabled = false;
                    if (e.Item.Cells[6].Text == "" || e.Item.Cells[6].Text == "0")
                        lnkVisitante.Enabled = true;

                    btnEditar = (ImageButton)(e.Item.FindControl("imgEditar"));
                    btnEditar.Visible = false;
                    e.Item.Attributes.Add("onMouseOver", "selecionaGrid(this)");
                    e.Item.Attributes.Add("onMouseOut", "deSelecionaGrid(this)");
                    e.Item.Style["cursor"] = "hand";

                    int intCell = e.Item.Cells.Count;
                    for (int @int = 0; @int <= intCell - 2; @int++)
                    {
                        if (@int != 7 && @int != 2)
                            e.Item.Cells[@int].Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(btnEditar, ""));
                    }

                    e.Item.ToolTip = BLIdiomas.TraduzirMensagens(Mensagens.VISUALIZAR);

                    CheckBox chkAcSabado = (CheckBox)e.Item.FindControl("chkAcSabado");
                    chkAcSabado.Checked = Convert.ToBoolean(e.Item.Cells[14].Text);

                    CheckBox chkAcDomingo = (CheckBox)e.Item.FindControl("chkAcDomingo");
                    chkAcDomingo.Checked = Convert.ToBoolean(e.Item.Cells[16].Text);

                    CheckBox chkAcFeriado = (CheckBox)e.Item.FindControl("chkAcFeriado");
                    chkAcFeriado.Checked = Convert.ToBoolean(e.Item.Cells[18].Text);

                    Label lblUsuarioAprov;

                    BLSolicitacao objBLSolicitacao = new BLSolicitacao();
                    string strNomeUltimoAprov = string.Empty;

                    //Obtém o Nome do Ultimo Aprovador
                    strNomeUltimoAprov = objBLSolicitacao.ObterUltimoAprovador(Convert.ToInt32(e.Item.Cells[3].Text));

                    lblUsuarioAprov = (Label)(e.Item.FindControl("lblUsuarioAprov"));
                    lblUsuarioAprov.Text = strNomeUltimoAprov;

                }
                catch (Exception ex)
                {
                    ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
                }
            }
        }

        protected void radPendentes_ItemCommand(object source, Telerik.WebControls.GridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.Trim() == "Lista")
                {
                    AbrirRadWindow(Convert.ToInt32(e.Item.Cells[28].Text), Convert.ToInt32(e.Item.Cells[3].Text));
                }

                if (e.CommandName.Trim() == "Visualizar")
                {
                    //armazena o codigo das solicitações que foram selecionadas.
                    foreach (Telerik.WebControls.GridDataItem rdiPendentes in radPendentes.Items)
                    {
                        try
                        {
                            if (rdiPendentes["TemplateColumn"].FindControl("chkItem") != null)
                                if (((CheckBox)rdiPendentes["TemplateColumn"].FindControl("chkItem")).Checked)
                                    this.lstCheck.Add(Convert.ToInt32(rdiPendentes["Codigo"].Text));

                        }
                        catch (Exception ex)
                        {
                            ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
                        }
                    }

                    Session["lstCheck"] = this.lstCheck;

                    Response.Redirect("ListHistorico.aspx?Id_Solicitacao=" + e.Item.Cells[3].Text);
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

        protected void radPendentes_NeedDataSource(object source, Telerik.WebControls.GridNeedDataSourceEventArgs e)
        {
            if (Page.IsPostBack)
            {
                this.PopularRadGridPendentes(false);
            }
        }

        #endregion

        #region CheckBox

        protected void chkItemCrachaTitular_CheckedChanged(object sender, EventArgs e)
        {
            foreach (Telerik.WebControls.GridDataItem rdiPendentes in this.radCrachaTitular.Items)
            {
                try
                {
                    if (rdiPendentes["TemplateColumn"].FindControl("chkItem") != null)
                        ((CheckBox)rdiPendentes["TemplateColumn"].FindControl("chkItem")).Checked = ((CheckBox)sender).Checked;
                }
                catch (Exception ex)
                {
                    ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
                }
            }
        }

        protected void chkItem_CheckedChanged(object sender, EventArgs e)
        {
            foreach (Telerik.WebControls.GridDataItem rdiPendentes in radPendentes.Items)
            {
                try
                {
                    if (rdiPendentes["TemplateColumn"].FindControl("chkItem") != null)
                        ((CheckBox)rdiPendentes["TemplateColumn"].FindControl("chkItem")).Checked = ((CheckBox)sender).Checked;
                }
                catch (Exception ex)
                {
                    ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
                }
            }
        }

        #endregion

        #endregion

        #region Escala

        #region Eventos

        #region CheckBox Escala
        /// <summary>
        /// Checa os check box selecionar todos.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [cmarchi] created 27/1/2010
        /// </history>
        protected void chkItemEscala_CheckedChanged(object sender, EventArgs e)
        {
            foreach (Telerik.WebControls.GridDataItem rdiPendentes in radEscalasPendente.Items)
            {
                try
                {
                    if (rdiPendentes["TemplateColumn"].FindControl("chkeItem") != null)
                        ((CheckBox)rdiPendentes["TemplateColumn"].FindControl("chkeItem")).Checked = ((CheckBox)sender).Checked;
                }
                catch (Exception ex)
                {
                    ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
                }
            }
        }

        #endregion

        #region RadGrid

        /// <summary>
        /// radGridEscalas_ItemCommand
        /// </summary>
        /// <history>
        ///     [haguiar_2] modify 07/12/2010
        ///     permitir visualizacao de troca de horario
        /// </history>
        protected void radGridEscalas_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.Trim() == "VisualizarCol")
                {
                    AbrirRadWindow(Convert.ToInt32(e.Item.Cells[3].Text));
                }

                if (e.CommandName.Trim() == "Visualizar")
                {
                    //armazena o codigo das solicitações que foram selecionadas.
                    foreach (Telerik.WebControls.GridDataItem rdiPendentes in radEscalasPendente.Items)
                    {
                        try
                        {
                            if (rdiPendentes["TemplateColumn"].FindControl("chkeItem") != null)
                                if (((CheckBox)rdiPendentes["TemplateColumn"].FindControl("chkeItem")).Checked)
                                    this.ListaCheckEscalas.Add(Convert.ToInt32(rdiPendentes["IdEscalacao"].Text));

                        }
                        catch (Exception ex)
                        {
                            ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
                        }
                    }

                    Session["lstCheckEsc"] = this.ListaCheckEscalas;

                    string strParametros = "CadCaixa," + e.Item.Cells[3].Text + "," + e.Item.Cells[9].Text;

                    Response.Redirect("../Escala/CadEscalaFinalizacao.aspx?mod=" +
                        BLEncriptacao.EncQueryStr(strParametros));
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

                    e.Item.ToolTip = BLIdiomas.TraduzirMensagens(Mensagens.VISUALIZAR);
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }

        protected void radGridEscalas_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item.ItemType == Telerik.WebControls.GridItemType.AlternatingItem | e.Item.ItemType == Telerik.WebControls.GridItemType.Item)
            {
                ImageButton btnVisualizar;
                LinkButton lnkColaborador;
                Label lblUsuarioAprov;

                e.Item.ForeColor = System.Drawing.Color.FromName(e.Item.Cells[15].Text);
                btnVisualizar = (ImageButton)e.Item.FindControl("btnVisualizar");

                BLEscala objBLEscala = new BLEscala();
                string strNomeUltimoAprov = string.Empty;

                //Obtém o Nome do Ultimo Aprovador
                strNomeUltimoAprov = objBLEscala.ObterUltimoAprovador(Convert.ToInt32(e.Item.Cells[3].Text));

                lblUsuarioAprov = (Label)(e.Item.FindControl("lblUsuarioAprov"));
                lblUsuarioAprov.Text = strNomeUltimoAprov;

                lnkColaborador = (LinkButton)(e.Item.FindControl("lnkColaborador"));
                lnkColaborador.Text = e.Item.Cells[7].Text;
                lnkColaborador.Enabled = true;

                if (btnVisualizar != null)
                {
                    btnVisualizar.Visible = false;
                    e.Item.Attributes.Add("onMouseOver", "selecionaGrid(this)");
                    e.Item.Attributes.Add("onMouseOut", "deSelecionaGrid(this)");
                    e.Item.Style["cursor"] = "hand";

                    // Cursor 
                    int intCell = e.Item.Cells.Count;
                    for (int @int = 0; @int <= intCell - 1; @int++)
                    {
                        if (@int != 2 && @int != 8)
                        {
                            e.Item.Cells[@int].Attributes.Add("onclick",
                                Page.ClientScript.GetPostBackEventReference(btnVisualizar, ""));
                        }
                    }

                    e.Item.ToolTip = BLIdiomas.TraduzirMensagens(Mensagens.VISUALIZAR);
                }
            }
        }

        protected void radGridEscalas_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (Page.IsPostBack)
            {
                this.PopularRadGridEscalaPendentes(false);
            }
        }

        #endregion

        #endregion

        #region Métodos

        #region Popular RadGridEscalaPendentes
        /// <summary>
        /// Popula a radgrid escalação.
        /// </summary>
        /// <param name="pblnBind">True - Faz o bind, False - não faz o bind</param>
        /// <history>
        ///     [cmarchi] created 26/1/2010
        /// </history>
        private void PopularRadGridEscalaPendentes(bool pblnBind)
        {
            BLEscala objBLEscala = new BLEscala();
            Collection<Escalacao> colEscalacoes = new Collection<Escalacao>();

            try
            {
                colEscalacoes = objBLEscala.ListarEscalasPendAprov(BLAcesso.IdUsuarioLogado());

                this.radEscalasPendente.DataSource = colEscalacoes;

                if (pblnBind)
                    this.radEscalasPendente.DataBind();
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }
        #endregion

        #endregion

        #region Propriedades
        /// <summary>
        /// Propriedade Lista de chacados das Escalas.
        /// </summary>
        /// <history>
        ///     [cmarchi] created 27/1/2010
        /// </history>
        public List<int> ListaCheckEscalas
        {
            get
            {
                if (ViewState["vsListEscala"] == null)
                {
                    ViewState["vsListEscala"] = new List<int>();
                }
                return (List<int>)ViewState["vsListEscala"];
            }
            set
            {
                ViewState["vsListEscala"] = value;
            }
        }


        #region ColAtributosAprovador
        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        ///     Propriedade ColAtributosAprovador
        /// </summary> 
        /// <history> 
        ///     [haguiar_5] 17/02/2011 Created
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public Collection<SafWeb.Model.Filial.AprovadorFilial> ColAtributosAprovador
        {
            get
            {
                if (this.ViewState["vsColAtributosAprovador"] == null)
                {
                    this.ViewState.Add("vsColAtributosAprovador", new Collection<AprovadorFilial>());
                }

                return (Collection<AprovadorFilial>)this.ViewState["vsColAtributosAprovador"];

            }
            set
            {
                this.ViewState.Add("vsColAtributosAprovador", value);
            }
        }
        #endregion
        #endregion

        #endregion

        #region CrachaTitular
            #region RadGrid

            protected void radCrachaTitular_ItemDataBound(object sender, Telerik.WebControls.GridItemEventArgs e)
            {
                ImageButton btnEditar;

                if (e.Item.ItemType == Telerik.WebControls.GridItemType.Item || e.Item.ItemType == Telerik.WebControls.GridItemType.AlternatingItem)
                {
                    try
                    {
                        //e.Item.ForeColor = System.Drawing.Color.FromName(e.Item.Cells[30].Text);

                        btnEditar = (ImageButton)(e.Item.FindControl("imgEditar"));
                        btnEditar.Visible = false;

                        e.Item.Attributes.Add("onMouseOver", "selecionaGrid(this)");
                        e.Item.Attributes.Add("onMouseOut", "deSelecionaGrid(this)");
                        e.Item.Style["cursor"] = "hand";

                        int intCell = e.Item.Cells.Count;
                        for (int @int = 0; @int <= intCell - 2; @int++)
                        {
                            if (@int != 7 && @int != 2)
                                e.Item.Cells[@int].Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(btnEditar, ""));
                        }

                        e.Item.ToolTip = BLIdiomas.TraduzirMensagens(Mensagens.VISUALIZAR);

                        /*
                        Label lblNom_UltimoAprovador;

                        BLSolicitacao objBLSolicitacao = new BLSolicitacao();
                        string strNomeUltimoAprov = string.Empty;
                        
                        //Obtém o Nome do Ultimo Aprovador
                        strNomeUltimoAprov = objBLSolicitacao.ObterUltimoStatusCrachaTitular(Convert.ToInt32(e.Item.Cells[3].Text));

                        lblNom_UltimoAprovador = (Label)(e.Item.FindControl("lblNom_UltimoAprovador"));
                        lblNom_UltimoAprovador.Text = strNomeUltimoAprov;
                        */
                    }
                    catch (Exception ex)
                    {
                        ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
                    }
                }
            }

            protected void radCrachaTitular_ItemCommand(object source, Telerik.WebControls.GridCommandEventArgs e)
            {
                try
                {
                    if (e.CommandName.Trim() == "Visualizar")
                    {
                        //armazena o codigo das solicitações que foram selecionadas.
                        foreach (Telerik.WebControls.GridDataItem rdiPendentes in radCrachaTitular.Items)
                        {
                            try
                            {
                                if (rdiPendentes["TemplateColumn"].FindControl("chkItem") != null)
                                    if (((CheckBox)rdiPendentes["TemplateColumn"].FindControl("chkItem")).Checked)
                                        this.lstCheck.Add(Convert.ToInt32(rdiPendentes["Id_SolicitacaoCrachaTitular"].Text));

                            }
                            catch (Exception ex)
                            {
                                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
                            }
                        }

                        Session["lstCheck"] = this.lstCheck;

                        Response.Redirect("ListHistoricoCrachaTitular.aspx?Id_SolicitacaoCrachaTitular=" + e.Item.Cells[3].Text);
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

            protected void radCrachaTitular_NeedDataSource(object source, Telerik.WebControls.GridNeedDataSourceEventArgs e)
            {
                if (Page.IsPostBack)
                {
                    this.PopularRadGridCrachaTitular(false);
                }
            }

        #endregion

            #region Métodos

            private void PopularRadGridCrachaTitular(bool pblnBind)
            {
                BLSolicitacao objBLSolicitacao = null;
                Collection<SafWeb.Model.Solicitacao.PermissaoCrachaTitular> colSolicitacoes = null;

                try
                {
                    objBLSolicitacao = new BLSolicitacao();
                    colSolicitacoes = new Collection<SafWeb.Model.Solicitacao.PermissaoCrachaTitular>();

                    colSolicitacoes = objBLSolicitacao.ListarSolicPendAprovCrachaTitular(Convert.ToInt32(BLAcesso.IdUsuarioLogado()));

                    this.radCrachaTitular.DataSource = colSolicitacoes;
                    if (pblnBind)
                        this.radCrachaTitular.DataBind();
                }
                catch (Exception ex)
                {
                    ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
                }
            }

            #endregion
        #endregion



        #endregion

        #region Historico

        #region Métodos



        private void PopularCombos()
        {
            this.PopularComboRegional();
            this.PopularComboStatus();
            this.PopularComboFilial();
            this.PopularComboEmpresa();
            this.PopularComboTipoSolicitacao();

            this.PopularComboTipoSolicitacaoHistColab();
            this.PopularComboRegionalHistColab();
            this.PopularComboFilialHistColab();
            this.PopularComboStatusHistColab();
            this.PopularComboEscalaDepartamental();
        }

        private void PopularComboTipoSolicitacao()
        {
            BLSolicitacao objBLSolicitacao = null;
            Collection<SafWeb.Model.Solicitacao.TipoSolicitacao> colTipoSolicitacao = null;

            try
            {
                objBLSolicitacao = new BLSolicitacao();

                colTipoSolicitacao = objBLSolicitacao.ListarTipoSolicitacao();

                this.ddlTipoSolicitacao.DataSource = colTipoSolicitacao;
                this.ddlTipoSolicitacao.DataTextField = "Descricao";
                this.ddlTipoSolicitacao.DataValueField = "Codigo";
                this.ddlTipoSolicitacao.DataBind();

                this.ddlTipoSolicitacao.Items.Insert(0, BLIdiomas.TraduzirMensagens(Convert.ToInt32(Mensagens.SELECIONE)));
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }

        private void PopularComboRegional()
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

                this.ddlRegional.Items.Insert(0, BLIdiomas.TraduzirMensagens(Convert.ToInt32(Mensagens.SELECIONE)));
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }

        private void PopularComboFilial()
        {
            BLFilial objBlFilial = null;
            Collection<SafWeb.Model.Filial.Filial> colFilial = null;

            try
            {
                objBlFilial = new BLFilial();

                if (ddlRegional.SelectedIndex > 0)
                {
                    colFilial = objBlFilial.Listar(Convert.ToInt32(ddlRegional.SelectedItem.Value));
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

                this.ddlFilial.Items.Insert(0, BLIdiomas.TraduzirMensagens(Convert.ToInt32(Mensagens.SELECIONE)));
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }

        private void PopularComboStatus()
        {
            BLSolicitacao objBLSolicitacao = null;
            Collection<SafWeb.Model.Solicitacao.Status> colStatus = null;

            try
            {
                objBLSolicitacao = new BLSolicitacao();

                colStatus = objBLSolicitacao.ListarStatus();

                this.ddlStatus.DataSource = colStatus;
                this.ddlStatus.DataTextField = "Descricao";
                this.ddlStatus.DataValueField = "Codigo";
                this.ddlStatus.DataBind();

                this.ddlStatus.Items.Insert(0, BLIdiomas.TraduzirMensagens(Convert.ToInt32(Mensagens.SELECIONE)));
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }

        private void PopularComboEmpresa()
        {
            BLEmpresa objBlEmpresa = null;
            Collection<SafWeb.Model.Empresa.Empresa> colEmpresa = null;

            try
            {
                objBlEmpresa = new BLEmpresa();

                colEmpresa = objBlEmpresa.Listar();

                this.ddlEmpresa.DataSource = colEmpresa;
                this.ddlEmpresa.DataTextField = "DescricaoEmpresa";
                this.ddlEmpresa.DataValueField = "IdEmpresa";
                this.ddlEmpresa.DataBind();

                this.ddlEmpresa.Items.Insert(0, BLIdiomas.TraduzirMensagens(Convert.ToInt32(Mensagens.SELECIONE)));
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }

        #region PopularComboEscalaDepto
        /// <summary>
        ///     Popula o combo com a Escala Departamental
        /// </summary>
        /// <history>
        ///     [tgerevini] created 24/5/2010 
        ///</history>
        protected void PopularComboEscalaDepartamental()
        {
            BLEscalaDepartamental objBLEscalaDepartamental = new BLEscalaDepartamental();
            Collection<EscalaDepartamental> colEscalaDepartamental;

            try
            {
                colEscalaDepartamental = objBLEscalaDepartamental.ListarEscalaDepartamental(false);

                //preenche escala departamental da parte de cadastro
                this.ddlEscalaHistColab.DataSource = colEscalaDepartamental;
                this.ddlEscalaHistColab.DataTextField = "DescricaoEscalaDpto";
                this.ddlEscalaHistColab.DataValueField = "IdEscalaDpto";
                this.ddlEscalaHistColab.DataBind();

                this.ddlEscalaHistColab.Items.Insert(0, BLIdiomas.TraduzirMensagens(Convert.ToInt32(Mensagens.SELECIONE)));

            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }
        #endregion

        private void PopularRadGridHistorico(bool pblnBind)
        {
            BLSolicitacao objBLSolicitacao = null;
            Collection<SafWeb.Model.Solicitacao.Solicitacao> colSolicitacoes = null;

            try
            {
                objBLSolicitacao = new BLSolicitacao();
                colSolicitacoes = new Collection<SafWeb.Model.Solicitacao.Solicitacao>();

                colSolicitacoes = objBLSolicitacao.ListarSolicitacoes((this.txtNumSolicitacao.Text != "" ? Convert.ToInt32(this.txtNumSolicitacao.Text) : 0),
                                                                      (this.ddlEmpresa.SelectedIndex > 0 ? Convert.ToInt32(this.ddlEmpresa.SelectedValue) : 0),
                                                                      (this.ddlRegional.SelectedIndex > 0 ? Convert.ToInt32(this.ddlRegional.SelectedValue) : 0),
                                                                      (this.ddlFilial.SelectedIndex > 0 ? Convert.ToInt32(this.ddlFilial.SelectedValue) : 0),
                                                                      (this.ddlStatus.SelectedIndex > 0 ? Convert.ToInt32(this.ddlStatus.SelectedValue) : -1),
                                                                      (this.ddlTipoSolicitacao.SelectedIndex > 0 ? Convert.ToInt32(this.ddlTipoSolicitacao.SelectedValue) : 0),
                                                                      (this.txtDataInicio.Text != "" ? (DateTime?)Convert.ToDateTime(this.txtDataInicio.Text) : null),
                                                                      (this.txtDataFim.Text != "" ? (DateTime?)Convert.ToDateTime(this.txtDataFim.Text) : null),
                                                                      this.txtNomeVisitado.Text,
                                                                      this.txtNomeVisitante.Text,
                                                                      this.txtNomeSolicitante.Text,
                                                                      this.txtNomeAprovador.Text,
                                                                      0);

                this.radHistorico.DataSource = colSolicitacoes;
                if (pblnBind)
                    this.radHistorico.DataBind();
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
            this.PopularRadGridHistorico(true);
        }

        #endregion

        #region Combo

        protected void ddlRegional_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.PopularComboFilial();
        }

        protected void ddlRegionalHistColab_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.PopularComboFilialHistColab();
        }

        #endregion

        #region RadGrid

        /// <summary>
        ///     radHistorico
        /// </summary>
        /// <history>
        ///     [no history]
        ///     [haguiar] modified 05/11/2010
        /// </history>
        protected void radHistorico_ItemCommand(object source, Telerik.WebControls.GridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.Trim() == "Lista")
                {
                    AbrirRadWindow(Convert.ToInt32(e.Item.Cells[21].Text), Convert.ToInt32(e.Item.Cells[2].Text));
                }

                if (e.CommandName.Trim() == "Visualizar")
                {
                    Response.Redirect("ListHistorico.aspx?Id_Solicitacao=" + e.Item.Cells[2].Text + "&Hist=true");
                }

                if (e.CommandName.Trim() == "IrPagina")
                {
                    string strPageIndexString = ((TextBox)e.Item.FindControl("txtPagina2")).Text;
                    int intPageIndex = e.Item.OwnerTableView.CurrentPageIndex;
                    if ((!object.ReferenceEquals(strPageIndexString.Trim(), string.Empty)))
                    {
                        if (e.Item.OwnerTableView.PageCount < Convert.ToInt32(strPageIndexString))
                        {
                            strPageIndexString = (e.Item.OwnerTableView.PageCount).ToString();
                            ((TextBox)e.Item.FindControl("txtPagina2")).Text = strPageIndexString;
                        }
                        intPageIndex = Convert.ToInt32(strPageIndexString) - 1;
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
                    string strPageIndexString = ((TextBox)e.Item.FindControl("txtPagina2")).Text;
                    int intPageIndex = e.Item.OwnerTableView.CurrentPageIndex;
                    if ((!object.ReferenceEquals(strPageIndexString.Trim(), string.Empty)))
                    {
                        if (e.Item.OwnerTableView.PageCount < Convert.ToInt32(strPageIndexString))
                        {
                            strPageIndexString = (e.Item.OwnerTableView.PageCount).ToString();
                            ((TextBox)e.Item.FindControl("txtPagina2")).Text = strPageIndexString;
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

                    if (!(e.Item.OwnerTableView.CurrentPageIndex == intPageIndex))
                    {
                        e.Item.OwnerTableView.CurrentPageIndex = intPageIndex;
                        e.Item.OwnerTableView.Rebind();
                    }
                }

                */
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }

        protected void radHistorico_ItemDataBound(object sender, Telerik.WebControls.GridItemEventArgs e)
        {
            LinkButton lnkVisitante;
            ImageButton imgVisualizar;

            if (e.Item.ItemType == Telerik.WebControls.GridItemType.Item || e.Item.ItemType == Telerik.WebControls.GridItemType.AlternatingItem)
            {
                try
                {
                    e.Item.ForeColor = System.Drawing.Color.FromName(e.Item.Cells[24].Text);

                    lnkVisitante = (LinkButton)(e.Item.FindControl("lnkVisitante"));
                    lnkVisitante.Text = e.Item.Cells[22].Text;
                    lnkVisitante.Enabled = false;
                    if (e.Item.Cells[5].Text == "" || e.Item.Cells[5].Text == "0")
                        lnkVisitante.Enabled = true;

                    imgVisualizar = (ImageButton)(e.Item.FindControl("imgVisualizar"));
                    imgVisualizar.Visible = false;
                    e.Item.Attributes.Add("onMouseOver", "selecionaGrid(this)");
                    e.Item.Attributes.Add("onMouseOut", "deSelecionaGrid(this)");
                    e.Item.Style["cursor"] = "hand";

                    int intCell = e.Item.Cells.Count;
                    for (int @int = 0; @int <= intCell - 2; @int++)
                    {
                        if (@int != 6)
                            e.Item.Cells[@int].Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(imgVisualizar, ""));
                    }

                    e.Item.ToolTip = BLIdiomas.TraduzirMensagens(Mensagens.VISUALIZAR);

                    CheckBox chkAcSabado = (CheckBox)e.Item.FindControl("chkAcSabado");
                    chkAcSabado.Checked = Convert.ToBoolean(e.Item.Cells[13].Text);

                    CheckBox chkAcDomingo = (CheckBox)e.Item.FindControl("chkAcDomingo");
                    chkAcDomingo.Checked = Convert.ToBoolean(e.Item.Cells[15].Text);

                    CheckBox chkAcFeriado = (CheckBox)e.Item.FindControl("chkAcFeriado");
                    chkAcFeriado.Checked = Convert.ToBoolean(e.Item.Cells[17].Text);

                }
                catch (Exception ex)
                {
                    ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
                }
            }
        }

        protected void radHistorico_NeedDataSource(object source, Telerik.WebControls.GridNeedDataSourceEventArgs e)
        {
            if (Page.IsPostBack)
            {
                this.PopularRadGridHistorico(false);
            }
        }


        #endregion

        #endregion

        #endregion

        #region Histórico Colaboradores

        #region Métodos

        private void PopularComboTipoSolicitacaoHistColab()
        {
            BLSolicitacao objBLSolicitacao = null;
            Collection<SafWeb.Model.Solicitacao.TipoSolicitacao> colTipoSolicitacao = null;

            try
            {
                objBLSolicitacao = new BLSolicitacao();

                colTipoSolicitacao = objBLSolicitacao.ListarTipoSolicitacao(true);

                this.ddlTipoSolHistColab.DataSource = colTipoSolicitacao;
                this.ddlTipoSolHistColab.DataTextField = "Descricao";
                this.ddlTipoSolHistColab.DataValueField = "Codigo";
                this.ddlTipoSolHistColab.DataBind();

                this.ddlTipoSolHistColab.Items.Insert(0, BLIdiomas.TraduzirMensagens(Convert.ToInt32(Mensagens.SELECIONE)));
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }

        private void PopularComboRegionalHistColab()
        {
            BLRegional objBlRegional = null;
            Collection<SafWeb.Model.Regional.Regional> colRegional = null;

            try
            {
                objBlRegional = new BLRegional();

                colRegional = objBlRegional.Listar();

                this.ddlRegionalHistColab.DataSource = colRegional;
                this.ddlRegionalHistColab.DataTextField = "DescricaoRegional";
                this.ddlRegionalHistColab.DataValueField = "IdRegional";
                this.ddlRegionalHistColab.DataBind();

                this.ddlRegionalHistColab.Items.Insert(0, BLIdiomas.TraduzirMensagens(Convert.ToInt32(Mensagens.SELECIONE)));
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }

        private void PopularComboFilialHistColab()
        {
            BLFilial objBlFilial = null;
            Collection<SafWeb.Model.Filial.Filial> colFilial = null;

            try
            {
                objBlFilial = new BLFilial();

                if (ddlRegionalHistColab.SelectedIndex > 0)
                {
                    colFilial = objBlFilial.Listar(Convert.ToInt32(ddlRegionalHistColab.SelectedItem.Value));
                    ddlFilialHistColab.Enabled = true;
                }
                else
                {
                    ddlFilialHistColab.Enabled = false;
                }

                this.ddlFilialHistColab.Items.Clear();

                this.ddlFilialHistColab.DataSource = colFilial;
                this.ddlFilialHistColab.DataTextField = "AliasFilial";
                this.ddlFilialHistColab.DataValueField = "IdFilial";
                this.ddlFilialHistColab.DataBind();

                this.ddlFilialHistColab.Items.Insert(0, BLIdiomas.TraduzirMensagens(Convert.ToInt32(Mensagens.SELECIONE)));
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }

        private void PopularComboStatusHistColab()
        {
            BLSolicitacao objBLSolicitacao = null;
            Collection<SafWeb.Model.Solicitacao.Status> colStatus = null;

            try
            {
                objBLSolicitacao = new BLSolicitacao();

                colStatus = objBLSolicitacao.ListarStatus();

                this.ddlStatusHistColab.DataSource = colStatus;
                this.ddlStatusHistColab.DataTextField = "Descricao";
                this.ddlStatusHistColab.DataValueField = "Codigo";
                this.ddlStatusHistColab.DataBind();

                this.ddlStatusHistColab.Items.Insert(0, BLIdiomas.TraduzirMensagens(Convert.ToInt32(Mensagens.SELECIONE)));
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }

        private void PopularRadGridHistoricoColab(bool pblnBind)
        {
            BLEscala objBLEscala = null;
            Collection<HistoricoEscala> colHistoricoEscala = null;

            try
            {
                objBLEscala = new BLEscala();
                colHistoricoEscala = new Collection<HistoricoEscala>();

                colHistoricoEscala = objBLEscala.ListarHistoricoEscala(BLAcesso.IdUsuarioLogado(),
                                                                      (this.txtNumSolicitacaoColab.Text != "" ? Convert.ToInt32(this.txtNumSolicitacaoColab.Text) : 0),
                                                                      (this.ddlRegionalHistColab.SelectedIndex > 0 ? Convert.ToInt32(this.ddlRegionalHistColab.SelectedValue) : 0),
                                                                      (this.ddlFilialHistColab.SelectedIndex > 0 ? Convert.ToInt32(this.ddlFilialHistColab.SelectedValue) : 0),
                                                                      (this.ddlStatusHistColab.SelectedIndex > 0 ? Convert.ToInt32(this.ddlStatusHistColab.SelectedValue) : -1),
                                                                      (this.ddlTipoSolHistColab.SelectedIndex > 0 ? Convert.ToInt32(this.ddlTipoSolHistColab.SelectedValue) : 0),
                                                                      (this.ddlEscalaHistColab.SelectedIndex > 0 ? Convert.ToInt32(this.ddlEscalaHistColab.SelectedValue) : 0),
                                                                      this.txtSolicitanteHistColab.Text,
                                                                      this.txtColabEscalado.Text,
                                                                      this.txtAprovadorHistColab.Text,
                                                                      (this.txtDataInicioHistColab.Text != "" ? (DateTime?)Convert.ToDateTime(this.txtDataInicioHistColab.Text) : null),
                                                                      (this.txtDataFimHistColab.Text != "" ? (DateTime?)Convert.ToDateTime(this.txtDataFimHistColab.Text) : null));

                this.radHistoricoColab.DataSource = colHistoricoEscala;
                if (pblnBind)
                    this.radHistoricoColab.DataBind();
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }


        #endregion

        #region Eventos
        protected void btnBuscarHistColab_Click(object sender, EventArgs e)
        {
            this.PopularRadGridHistoricoColab(true);
        }

        #region RadGrid

        protected void radHistoricoColab_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (Page.IsPostBack)
            {
                this.PopularRadGridHistoricoColab(false);
            }
        }



        /// <summary>
        /// radHistoricoColab_ItemCommand
        /// </summary>
        /// <history>
        ///     [haguiar_2] modify 07/12/2010
        ///     permitir visualizacao de troca de horario
        /// </history>
        protected void radHistoricoColab_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.Trim() == "VisualizarColHist")
                {
                    AbrirRadWindow(Convert.ToInt32(e.Item.Cells[2].Text));
                }

                if (e.CommandName.Trim() == "Visualizar")
                {

                    string strParametros = "CadCaixa," + e.Item.Cells[2].Text + "," + e.Item.Cells[8].Text;

                    Response.Redirect("../Escala/CadEscalaFinalizacao.aspx?mod=" +
                        BLEncriptacao.EncQueryStr(strParametros));
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

                    e.Item.ToolTip = BLIdiomas.TraduzirMensagens(Mensagens.VISUALIZAR);
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }

        protected void radHistoricoColab_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item.ItemType == Telerik.WebControls.GridItemType.AlternatingItem | e.Item.ItemType == Telerik.WebControls.GridItemType.Item)
            {
                ImageButton btnVisualizar;
                LinkButton lnkColaboradorEscalado;
                Label lblUsuarioAprov;

                e.Item.ForeColor = System.Drawing.Color.FromName(e.Item.Cells[13].Text);
                btnVisualizar = (ImageButton)e.Item.FindControl("btnVisualizar");
                
                BLEscala objBLEscala = new BLEscala();
                string strNomeUltimoAprov = string.Empty;

                //Obtém o Nome do Ultimo Aprovador
                strNomeUltimoAprov = objBLEscala.ObterUltimoAprovador(Convert.ToInt32(e.Item.Cells[2].Text));

                lblUsuarioAprov = (Label)(e.Item.FindControl("lblUsuarioAprov"));
                lblUsuarioAprov.Text = strNomeUltimoAprov;

                lnkColaboradorEscalado = (LinkButton)(e.Item.FindControl("lnkColaboradorEscalado"));
                lnkColaboradorEscalado.Text = e.Item.Cells[6].Text;
                lnkColaboradorEscalado.Enabled = true;

                if (btnVisualizar != null)
                {
                    btnVisualizar.Visible = false;
                    e.Item.Attributes.Add("onMouseOver", "selecionaGrid(this)");
                    e.Item.Attributes.Add("onMouseOut", "deSelecionaGrid(this)");
                    e.Item.Style["cursor"] = "hand";

                    // Cursor 
                    int intCell = e.Item.Cells.Count;
                    for (int @int = 0; @int <= intCell - 1; @int++)
                    {
                        if (@int != 7 && @int != 17)
                        {
                            e.Item.Cells[@int].Attributes.Add("onclick",
                                Page.ClientScript.GetPostBackEventReference(btnVisualizar, ""));
                        }
                    }

                    e.Item.ToolTip = BLIdiomas.TraduzirMensagens(Mensagens.VISUALIZAR);
                }

            }
        }

        #endregion

        #endregion

        #endregion
    }
}
