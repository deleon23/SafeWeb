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
using FrameWork.BusinessLayer.Utilitarios;
using Telerik.WebControls;
using FrameWork.Model.Utilitarios;
using FrameWork.BusinessLayer.Idioma;
using FrameWork.Model.Idioma;
using SafWeb.BusinessLayer.Regional;
using System.Collections.ObjectModel;
using SafWeb.Model.Regional;
using SafWeb.BusinessLayer.Escala;
using SafWeb.Model.Escala;
using SafWeb.BusinessLayer.Filial;
using SafWeb.Model.Filial;
using SafWeb.BusinessLayer.Colaborador;
using SafWeb.Model.Colaborador;
using FrameWork.BusinessLayer.Usuarios;
using SafWeb.BusinessLayer.Solicitacao;
using SafWeb.Model.Solicitacao;
using System.Text;

namespace SafWeb.UI.Modulos.Escala
{
    public partial class ImportarEscalaRonda : FWPage
    {
        private Escalacao gobjEscalacao;
        private Collection<Colaborador> gcolColaboradores;
        private Boolean gblnEdicao = false;

        #region Property
        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        ///     Propriedade blnApresentouMensagem
        /// </summary> 
        /// <history> 
        ///     [haguiar] 26/10/2010 Created
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public bool blnApresentouMensagem
        {
            get
            {
                if ((this.ViewState["vsblnApresentouMensagem"] != null))
                {
                    return Convert.ToBoolean(this.ViewState["vsblnApresentouMensagem"]);
                }
                else
                {
                    return false;
                }
            }
            set
            {
                this.ViewState.Add("vsblnApresentouMensagem", value);
            }
        }
        
        
        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        ///     Propriedade IdFilial do Usuario Logado
        /// </summary> 
        /// <history> 
        ///     [haguiar] 26/10/2010 Created
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public int IdFilial
        {
            get
            {
                if ((this.ViewState["vsIdFilial"] != null))
                {
                    return Convert.ToInt32(this.ViewState["vsIdFilial"]);
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                this.ViewState.Add("vsIdFilial", value);
            }
        }

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        ///     Propriedade IdRegional do Usuario Logado
        /// </summary> 
        /// <history> 
        ///     [haguiar] 26/10/2010 Created
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public int IdRegional
        {
            get
            {
                if ((this.ViewState["vsIdRegional"] != null))
                {
                    return Convert.ToInt32(this.ViewState["vsIdRegional"]);
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                this.ViewState.Add("vsIdRegional", value);
            }
        }
        #endregion

        /// <summary>
        /// Inicialização da página
        /// </summary>
        /// <history>
        ///     [sem informações do criador]
        ///     [haguiar] modify 22/10/2010
        /// </history>
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Remove("CodigosColaboradores");

            this.VerificarHiddenColaboradores();
            this.InicializaScripts();

            //verifica importacao
            if (!string.IsNullOrEmpty(this.Request.QueryString["mod"]) && !blnApresentouMensagem)
            {
                blnApresentouMensagem = true;

                string[] arrParametros = BLEncriptacao.DecQueryStr(this.Request.QueryString["mod"]).Split(',');

                string strEscalacao = string.Empty;

                strEscalacao = "Escala n°: " + arrParametros[0] +
                    " - Usuário Solicitante: " + arrParametros[1];

                this.RadAjaxPanel1.Alert(strEscalacao);
            }

            
            if (!this.Page.IsPostBack)
            {/*
                if (!VerificarQueryEditarCadastrar(this.Request.QueryString["mod"]))
                {
                    this.BindModel(Enums.TipoTransacao.Novo, Enums.TipoPainel.Listagem);
                }
              */

                this.BindModel(Enums.TipoTransacao.DescarregarDados, Enums.TipoPainel.Cadastro);
            }
        }

        #region Avançar
        /// <summary>
        /// Chama a tela de definição de jornadas dos colaboradores e cadastra os colaboradores
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [cmarchi] created 7/1/2010
        ///     [haguiar_2] modify 24/11/2010
        ///     Verifica escala finalizada - troca de horário
        /// </history>
        private void Avancar()
        {
            try
            {
                StringBuilder strCodigoColaboradores = new StringBuilder();

                foreach (ListItem var in this.lstColaboradores.Items)
                {
                    strCodigoColaboradores.Append(var.Value + ",");
                }

                if (strCodigoColaboradores.Length > 0)
                {
                    strCodigoColaboradores.Remove(strCodigoColaboradores.Length - 1, 1);
                    Session.Add("CodigosColaboradores", strCodigoColaboradores.ToString());
                }

                string strParametro = BLEncriptacao.EncQueryStr(this.ddlTipoSolicitacao.SelectedValue + "," + this.ddlPeriodoRonda.SelectedValue + "," + this.ddlPeriodo.SelectedValue + "," + this.ddlEscalaDepartamental.SelectedValue);
                this.Response.Redirect("ImportarEscalaRondaVisualizar.aspx?mod=" + strParametro);
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }
        #endregion

        private void BindRegionalFilial()
        {
            try
            {
                //seleciona a regional e a filial do usuário logado.
                BLColaborador objBlColaborador = new BLColaborador();
                DataTable dtt = new DataTable();

                dtt = objBlColaborador.ObterRegFilUsuario(Convert.ToInt32(BLAcesso.IdUsuarioLogado()));

                this.IdRegional = Convert.ToInt32(dtt.Rows[0][0].ToString());
                this.IdFilial = Convert.ToInt32(dtt.Rows[0][1].ToString());

            }
            catch (Exception ex)
            {
                ex = null;
            }

            try
            {
                //painel de cadastro
                this.PopularRegional(ref this.ddlRegional);
                this.PopularFilial(ref this.ddlFilial);

            }
            catch (Exception ex)
            {
                ex = null;
            }
        }

        #region Bind

        #region BindCadastro
        /// <summary>
        /// Bind Cadastro
        /// </summary>
        /// <param name="pintTipoTransacao">Tipo da Transação</param>
        /// <history>
        ///     [cmarchi] created 29/12/2009
        ///     [cmarchi] modify 11/2/2010
        ///     [haguiar_3] modify 07/01/2011
        /// </history>
        protected void BindCadastro(Enums.TipoTransacao penmTipoTransacao)
        {
            //atribuir as informações na tela para os objetos
            if (penmTipoTransacao == Enums.TipoTransacao.CarregarDados)
            {
                this.gobjEscalacao = new Escalacao();

                this.gobjEscalacao.IdEscalaDepartamental = Convert.ToInt32(
                    this.ddlEscalaDepartamental.SelectedValue);

                this.gobjEscalacao.IdUsuarioSolicitante = Convert.ToInt32(BLAcesso.IdUsuarioLogado());

                this.ObterListaColaboradores();

                string[] arrPeriodos = this.ddlPeriodo.SelectedValue.Split('à');

                this.gobjEscalacao.DataInicioPeriodo = Convert.ToDateTime(arrPeriodos[0].Trim());
                this.gobjEscalacao.DataFinalPeriodo = Convert.ToDateTime(arrPeriodos[1].Trim());

                if (this.BlnEditar)
                {
                    this.gobjEscalacao.IdEscalacao = this.IdEscalacao;
                }
            }
            //atribuir as informações dos objetos para tela
            else if (penmTipoTransacao == Enums.TipoTransacao.DescarregarDados)
            {
                BLEscalaDepartamental objBLEscalaDepartamental = new BLEscalaDepartamental();
                //Collection<Colaborador> colColaboradores = null;

                this.lstColaboradores.Items.Clear();

                this.PopularTipoSolicitacao();

                this.PopularEscalaDepartamental(Enums.TipoPainel.Cadastro);

                /*
                BLUtilitarios.ConsultarValorCombo(ref this.ddlEscalaDepartamental,
                    this.gobjEscalacao.IdEscalaDepartamental.ToString());
                */

                this.PopularCombos(Enums.TipoPainel.Cadastro);
                /*
                try
                {
                    colColaboradores = objBLEscalaDepartamental.ObterColaboradores(
                                                    this.gobjEscalacao.IdEscalaDepartamental);

                    this.PopularListaColaboradores(colColaboradores);

                }
                catch (Exception ex)
                {
                    ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
                }*/
            }
        }
        #endregion
        
        #region BindModel
        /// <summary>
        /// Bind Model 
        /// </summary>
        /// <param name="pintTipoTransacao">Tipo de Transação</param>
        /// <param name="penmTipoPainel">Tipo do Painel</param>
        /// <history>
        ///     [cmarchi] created 7/1/2010
        /// </history>
        protected void BindModel(Enums.TipoTransacao penmTipoTransacao,
                                 Enums.TipoPainel penmTipoPainel)
        {
            //verifica se  listagem ou cadastro na página
            if (penmTipoPainel == Enums.TipoPainel.Cadastro)
            {
                this.BindCadastro(penmTipoTransacao);
            }
        }
        #endregion

        #endregion

        #region Botões

        #region Adicionar Escala
        /// <summary>
        /// Chama a tela de cadastro de escala.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [cmarchi] created 19/10/2009
        /// </history>
        protected void btnAdicionar_Click(object sender, ImageClickEventArgs e)
        {
            this.RadWindowCadastroEscala();
        }
        #endregion

        #region Avançar
        /// <summary>
        /// Chama a tela de definição de jornadas dos colaboradores
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [cmarchi] created 26/10/2009
        /// </history>
        protected void btnAvancar_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                this.Avancar();
            }
        }
        #endregion
       
        #region Remover
        /// <summary>
        /// remove os colaboradores da lista
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [cmarchi] created 12/1/2010
        /// </history>
        protected void btnRemover_Click(object sender, ImageClickEventArgs e)
        {
            int intTotal = this.lstColaboradores.Items.Count;
            StringBuilder strColaboradores = new StringBuilder();

            for (int i = 0; i < intTotal; i++)
            {
                if (this.lstColaboradores.Items[i].Selected)
                {
                    strColaboradores.Append(this.lstColaboradores.Items[i].Value + ",");
                }
            }

            if (strColaboradores.Length > 0)
            {
                //removendo o caracter ,
                strColaboradores.Remove(strColaboradores.Length - 1, 1);

                string[] arrColaboradores = strColaboradores.ToString().Split(',');
                intTotal = arrColaboradores.Length;

                for (int i = 0; i < intTotal; i++)
                {
                    ListItem ltmColaborador = this.lstColaboradores.Items.FindByValue(arrColaboradores[i]);

                    if (ltmColaborador != null)
                    {
                        this.lstColaboradores.Items.Remove(ltmColaborador);
                    }
                }
            }
        }
        #endregion

        #endregion
        
        #region Eventos

        #region Cadastro

        #region Tipo da solicitação
        /// <summary>
        ///     Seleciona períodos de acordo com o tipo da solicitação
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [haguiar] created 22/11/2011 10:23
        /// </history>
        protected void ddlTipoSolicitacao_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlEscalaDepartamental.SelectedIndex > 0)
            {
                //limpa a lista de períodos
                gblnEdicao = false;
                this.PeriodoSelecionado = string.Empty;

                this.ddlPeriodo.Items.Clear();
                BLUtilitarios.InseriMensagemDropDownList(ref this.ddlPeriodo, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0);

                this.ddlPeriodoRonda.Items.Clear();
                BLUtilitarios.InseriMensagemDropDownList(ref this.ddlPeriodoRonda, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0);

                this.PopularCombos(Enums.TipoPainel.Cadastro);
            }
        }
        #endregion

        #region Escala Departamental
        /// <summary>
        ///     Seleciona periodicidade de acordo com a escala departamental
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [cmarchi] created 6/1/2010
        /// </history>
        protected void ddlEscalaDepartamental_SelectedIndexChanged(object sender, EventArgs e)
        {
            BLEscalaDepartamental objBLEscalaDepartamental = new BLEscalaDepartamental();
            Collection<Colaborador> colColaboradores = null;

            //limpa a lista de períodos
            gblnEdicao = false;
            this.PeriodoSelecionado = string.Empty;

            this.ddlPeriodo.Items.Clear();
            BLUtilitarios.InseriMensagemDropDownList(ref this.ddlPeriodo, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0);

            this.ddlPeriodoRonda.Items.Clear();
            BLUtilitarios.InseriMensagemDropDownList(ref this.ddlPeriodoRonda, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0);

            this.PopularCombos(Enums.TipoPainel.Cadastro);

            try
            {
                colColaboradores = objBLEscalaDepartamental.ObterColaboradores(
                                                   this.IdEscalaDepartamental);

                this.PopularListaColaboradores(colColaboradores);

                //botoes habilitados quando selecionado uma escala departamental
                btnAdicionar.Enabled = (ddlEscalaDepartamental.SelectedIndex > 0);
                btnRemover.Enabled = (ddlEscalaDepartamental.SelectedIndex > 0);
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }
        #endregion

        #endregion

        #endregion

        #region Inicializar Scripts
        /// <summary>
        /// Inicializa os Scripts.
        /// </summary>        
        /// <history>
        ///     [cmarchi] created 29/12/2009
        ///     [cmarchi] modify 12/1/2010
        ///     [haguiar] modify 26/10/2010
        /// </history>
        private void InicializaScripts()
        {
            lblMensagem.Visible = false;
            lblMensagem.Text = string.Empty;

            this.BindRegionalFilial();

            if (this.ddlEscalaDepartamental.Items.Count > 0 && this.IdEscalaDepartamental > 0)
            {
                if (this.ddlPeriodo.SelectedIndex > 0)
                    this.PeriodoSelecionado = this.ddlPeriodo.SelectedValue;

                this.PopularCombos(Enums.TipoPainel.Cadastro);
            }

        }
        #endregion

        #region ObterListaColaboradores
        /// <summary>
        /// Obteém os Colaboradores da lista de Colaboradores
        /// </summary>
        /// <history>
        ///     [cmarchi] created 7/1/2010
        /// </history>
        private void ObterListaColaboradores()
        {
            this.gcolColaboradores = new Collection<Colaborador>();

            foreach (ListItem var in this.lstColaboradores.Items)
            {
                Colaborador objColaborador = new Colaborador();
                objColaborador.IdColaborador = Convert.ToInt32(var.Value);

                this.gcolColaboradores.Add(objColaborador);
            }
        }
        #endregion

        #region Propriedades

        #region IdTipoSolicitacao
        /// <summary>
        ///     Propriedade IdTipoSolicitacao que grava o tipo da solicitacao
        /// </summary>
        /// <history>
        ///     [haguiar] created 30/11/2010
        /// </history>

        private int IdTipoSolicitacao;

        //private int IdTipoSolicitacao
        //{
        //    get
        //    {
        //        if (this.ViewState["vsIdTipoSolicitacao"] == null)
        //        {
        //            this.ViewState.Add("vsIdTipoSolicitacao", 0);
        //        }

        //        return Convert.ToInt32(this.ViewState["vsIdTipoSolicitacao"]);
        //    }

        //    set
        //    {
        //        this.ViewState.Add("vsIdTipoSolicitacao", value);
        //    }
        //}
        #endregion

        #region IdStatusSolicitacao
        /// <summary>
        ///     Propriedade IdStatusSolicitacao que grava o status da solicitacao
        /// </summary>
        /// <history>
        ///     [haguiar] created 29/10/2010
        /// </history>

        private int IdStatusSolicitacao;
        
        //private int IdStatusSolicitacao
        //{
        //    get
        //    {
        //        if (this.ViewState["vsIdStatusSolicitacao"] == null)
        //        {
        //            this.ViewState.Add("vsIdStatusSolicitacao", 0);
        //        }

        //        return Convert.ToInt32(this.ViewState["vsIdStatusSolicitacao"]);
        //    }

        //    set
        //    {
        //        this.ViewState.Add("vsIdStatusSolicitacao", value);
        //    }
        //}
        #endregion

        #region Editar
        /// <summary>
        ///     Propriedade Editar que verifica se está ou não editar
        /// </summary>
        /// <history>
        ///     [cmarchi] created 5/1/2010
        /// </history>
        private bool BlnEditar
        {
            get
            {
                if (this.ViewState["vsEditar"] == null)
                {
                    this.ViewState.Add("vsEditar", false);
                }

                return Convert.ToBoolean(this.ViewState["vsEditar"]);
            }

            set
            {
                this.ViewState.Add("vsEditar", value);
            }
        }
        #endregion

        #region IdEscalaDepartamental
        /// <summary>
        ///     Propriedade Id_EscalaDepartamental utilizada para Editar uma Escala Departamental
        /// </summary>
        /// <history>
        ///     [cmarchi] created 5/1/2009 
        /// </history>
        private int IdEscalaDepartamental
        {
            get
            {
                if (this.ViewState["vsIdEscalaDepartamental"] == null)
                {
                    this.ViewState.Add("vsIdEscalaDepartamental", 0);
                }

                return Convert.ToInt32(this.ViewState["vsIdEscalaDepartamental"]);
            }

            set
            {
                this.ViewState.Add("vsIdEscalaDepartamental", value);
            }
        }
        #endregion

        #region IdEscalacao
        /// <summary>
        ///     Propriedade Id_Escalacao utilizada para Editar uma Escalacao
        /// </summary>
        /// <history>
        ///     [cmarchi] created 13/1/2010
        /// </history>
        private int IdEscalacao
        {
            get
            {
                if (this.ViewState["vsIdEscalacao"] == null)
                {
                    this.ViewState.Add("vsIdEscalacao", 0);
                }

                return Convert.ToInt32(this.ViewState["vsIdEscalacao"]);
            }

            set
            {
                this.ViewState.Add("vsIdEscalacao", value);
            }
        }
        #endregion

        #region PeriodoSelecionado
        /// <summary>
        ///     Propriedade Editar que verifica se está ou não editar
        /// </summary>
        /// <history>
        ///     [cmarchi] created 5/1/2010
        /// </history>
        private string PeriodoSelecionado
        {
            get
            {
                if (this.Session["ssPeriodo"] == null)
                {
                    this.Session.Timeout = 30;
                    this.Session.Add("ssPeriodo", string.Empty);
                }

                return this.Session["ssPeriodo"].ToString();
            }

            set
            {
                if (string.IsNullOrEmpty(value))
                    this.Session.Remove("ssPeriodo");
                else
                    this.Session["ssPeriodo"] = value;
            }
        }
        #endregion

        #region Visualizar
        /// <summary>
        ///     Propriedade Visualizar que verifica se está ou não visualizando
        /// </summary>
        /// <history>
        ///     [cmarchi] created 12/1/2010
        /// </history>
        private bool BlnVisualizar
        {
            get
            {
                if (this.ViewState["vsVisualizar"] == null)
                {
                    this.ViewState.Add("vsVisualizar", false);
                }

                return Convert.ToBoolean(this.ViewState["vsVisualizar"]);
            }

            set
            {
                this.ViewState.Add("vsVisualizar", value);
            }
        }
        #endregion

        #endregion

        #region Popular Combos

        #region Regional
        /// <summary>
        ///     Popula o combo com as regionais
        /// </summary>
        /// <history>
        ///     [haguiar] created 26/10/2010
        ///</history>
        protected void PopularRegional(ref DropDownList ddlRegionalTemp)
        {
            BLRegional objBlRegional = new BLRegional();
            Collection<SafWeb.Model.Regional.Regional> colRegional;

            try
            {
                //limpa a lista
                ddlRegionalTemp.Items.Clear();
                colRegional = objBlRegional.Listar();

                //preenche reginal do usuário logado
                
                foreach (Regional gobjRegional in colRegional)
                {
                    if (gobjRegional.IdRegional == this.IdRegional)
                    {                        
                        ddlRegionalTemp.Items.Add(new ListItem(gobjRegional.DescricaoRegional,
                                                             Convert.ToString(gobjRegional.IdRegional)));

                        ddlRegionalTemp.SelectedIndex = 0;
                        break;
                    }
                }

                if (colRegional == null | colRegional.Count == 0)
                    BLUtilitarios.InseriMensagemDropDownList(ref ddlRegionalTemp, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0);
                
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }
        #endregion
        
        #region Filial
        /// <summary>
        ///     Popula os combos com a filial da regional do logado
        /// </summary>
        /// <param name="ddlRegional">Regional</param>
        /// <param name="ddlFilial">Filial</param>
        /// <history>
        ///     [haguiar] created 26/10/2010
        /// </history>
        protected void PopularFilial(ref DropDownList ddlFilialTemp)
        {
            BLFilial objBlFilial = new BLFilial();
            SafWeb.Model.Filial.Filial gobjFilial;

            //limpa a lista
            ddlFilialTemp.Items.Clear();

            try
            {
                gobjFilial = objBlFilial.Obter(IdFilial);

                if (gobjFilial != null)
                {
                    ddlFilialTemp.Items.Add(new ListItem(gobjFilial.DescricaoFilial,
                                                        Convert.ToString(gobjFilial.IdFilial)));
                    ddlFilialTemp.SelectedIndex = 0;
                }
                else
                {
                    BLUtilitarios.InseriMensagemDropDownList(ref ddlFilialTemp, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0); 
                    ddlFilialTemp.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }
        #endregion

        #region Escala Departamental
        /// <summary>
        ///     Popula o combo com a Escala Departamental
        /// </summary>
        /// <history>
        ///     [cmarchi] created 6/1/2010 
        ///     [cmarchi] modify 11/2/2010
        ///     [haguiar] modify 27/10/2010
        ///     [haguiar_3] modify 07/11/2010
        ///     [haguiar_sdm9004] 02/09/2011 15:48
        ///</history>
        protected void PopularEscalaDepartamental(Enums.TipoPainel penmTipoPainel)
        {
            BLEscalaDepartamental objBLEscalaDepartamental = new BLEscalaDepartamental();
            Collection<EscalaDepartamental> colEscalaDepartamental = new Collection<EscalaDepartamental>();

            try
            {
                DataTable dtt;

                //listar apenas escala departamental da regional/filial do logado, apenas ativas
                dtt = objBLEscalaDepartamental.ListarEscalaDepartamental(this.IdRegional, this.IdFilial, string.Empty, 0, false, false);
                
                foreach (DataRow dr in dtt.Rows)
                {
                    EscalaDepartamental objEscalaDepartamental = new EscalaDepartamental();
                    
                    objEscalaDepartamental.DescricaoEscalaDpto = Convert.ToString(dr[1]);
                    objEscalaDepartamental.IdEscalaDpto = Convert.ToInt32(dr[0]);

                    colEscalaDepartamental.Add(objEscalaDepartamental);
                }

                //preenche escala departamental da parte de cadastro
                this.ddlEscalaDepartamental.DataSource = colEscalaDepartamental;
                this.ddlEscalaDepartamental.DataTextField = "DescricaoEscalaDpto";
                this.ddlEscalaDepartamental.DataValueField = "IdEscalaDpto";
                this.ddlEscalaDepartamental.DataBind();

                BLUtilitarios.InseriMensagemDropDownList(ref this.ddlEscalaDepartamental, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0);

                //botoes habilitados quando selecionado uma escala departamental
                btnAdicionar.Enabled = (ddlEscalaDepartamental.SelectedIndex > 0);
                btnRemover.Enabled = (ddlEscalaDepartamental.SelectedIndex > 0);
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }
        #endregion

        #region Lista Colaboradores
        /// <summary>
        /// Preenche a Lista de Colaboradores.
        /// </summary>
        /// <param name="parrColaboradores">Array com os Id dos Colaboradores</param>
        /// <history>
        ///     [cmarchi] created 7/1/2010
        /// </history>
        private void PopularListaColaboradores(string[] parrColaboradores)
        {
            if (parrColaboradores != null)
            {
                BLColaborador objBLColaborador = new BLColaborador();
                Collection<Colaborador> colColaborador = null;

                try
                {
                    colColaborador = objBLColaborador.Obter(parrColaboradores);
                    ListItem limColaborador = null;

                    if (colColaborador != null)
                    {
                        foreach (Colaborador objColaborador in colColaborador)
                        {
                            limColaborador = this.lstColaboradores.Items.FindByValue(objColaborador.IdColaborador.ToString());

                            if (limColaborador == null)
                            {
                                limColaborador = new ListItem();
                                limColaborador.Text = objColaborador.NomeColaborador + " - " + objColaborador.CodigoColaborador;
                                limColaborador.Value = objColaborador.IdColaborador.ToString();

                                this.lstColaboradores.Items.Add(limColaborador);
                            }
                            limColaborador = null;
                        }
                    }
                    this.txtHiddenColaboradores.Value = string.Empty;
                }
                catch (Exception ex)
                {
                    ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
                }
            }
        }

        /// <summary>
        /// Preenche a Lista de Colaboradores.
        /// </summary>
        /// <param name="parrColaboradores">Array com os Id dos Colaboradores</param>
        /// <history>
        ///     [cmarchi] created 7/1/2010
        /// </history>
        private void PopularListaColaboradores(Collection<Colaborador> pcolColaboradores)
        {
            ListItem limColaborador = null;

            this.lstColaboradores.Items.Clear();

            foreach (Colaborador objColaborador in pcolColaboradores)
            {
                limColaborador = this.lstColaboradores.Items.FindByValue(objColaborador.IdColaborador.ToString());

                if (limColaborador == null)
                {
                    limColaborador = new ListItem();
                    limColaborador.Text = objColaborador.NomeColaborador + " - " + objColaborador.CodigoColaborador;
                    limColaborador.Value = objColaborador.IdColaborador.ToString();

                    this.lstColaboradores.Items.Add(limColaborador);
                }

                limColaborador = null;
            }
        }
        #endregion

        #region Periodo
        /// <summary>
        ///     Popula o combo de Período
        /// </summary>
        /// <param name="pobjEscalaDepartamental">Objeto Escala Departamental</param>
        /// <param name="pddlPeriodicidade">Periodicidade</param>
        /// <history>
        ///     [cmarchi] created 21/1/2010 
        ///     [cmarchi] modify 23/2/2010
        ///     [tgerevini] modify 19/08/2010 - não exibir períodos já preenchidos parcialmente
        ///     [haguiar] modify 27/10/2010
        ///</history>
        protected void PopularPeriodo(EscalaDepartamental pobjEscalaDepartamental, ref DropDownList pddlPeriodo)
        {
            BLEscala objBLEscala = new BLEscala();
            Collection<string> colPeriodos = null;

            if (this.ddlTipoSolicitacao.SelectedIndex > 0)
            {
                this.IdTipoSolicitacao = Convert.ToInt32(this.ddlTipoSolicitacao.SelectedValue);
            }
            else
            {
                this.IdTipoSolicitacao = 7;
            }

            if (this.ddlPeriodoRonda.Items.Count <= 1)
            {//**5 últimos períodos
                colPeriodos = objBLEscala.ObterPeriodosAnteriores(this.IdTipoSolicitacao);
                this.ddlPeriodoRonda.Items.Clear();

                for (int i = 0; i < colPeriodos.Count; i++)
                {
                    ListItem ltmPeridoRonda = new ListItem(colPeriodos[i],
                                                            colPeriodos[i]);

                    ddlPeriodoRonda.Items.Add(ltmPeridoRonda);
                }

                BLUtilitarios.InseriMensagemDropDownList(ref ddlPeriodoRonda, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0);

                colPeriodos.Clear();
                //**
            }

            try
            {
                if (this.IdTipoSolicitacao == 7)
                {
                    if (pobjEscalaDepartamental != null)
                    {

                        colPeriodos = objBLEscala.ObterPeriodos(pobjEscalaDepartamental.ObjPeriodicidade.DescricaoPeriodicidade,
                           pobjEscalaDepartamental.IdEscalaDpto, this.IdEscalacao);

                        Collection<string> colPeriodosDisponiveis = new Collection<string>();

                        if (this.PeriodoSelecionado != string.Empty)
                        {
                            gblnEdicao = true;
                        }

                        //Se for edição, será populado normalmente
                        if (!gblnEdicao)
                        {
                            //Exclui períodos já preenchidos parcialmente
                            foreach (string strPeriodo in colPeriodos)
                            {
                                string[] strDatas = strPeriodo.Split('à');
                                DateTime datDataInicio, datDataFim;
                                int intIdEscalaDpto;

                                intIdEscalaDpto = Convert.ToInt32(ddlEscalaDepartamental.SelectedItem.Value);
                                datDataInicio = Convert.ToDateTime(strDatas[0]);
                                datDataFim = Convert.ToDateTime(strDatas[1]);


                                DataTable dtt = objBLEscala.ObterPeriodosJaCadastrados(intIdEscalaDpto, datDataInicio, datDataFim);

                                //período ainda não cadastrado para a escalação
                                if (dtt.Rows.Count == 0)
                                {
                                    colPeriodosDisponiveis.Add(strPeriodo);
                                }
                            }

                            //se todos os períodos já foram cadastrados, obtém-se os próx períodos
                            if (colPeriodosDisponiveis.Count == 0)
                            {
                                string[] strDatas = colPeriodos[colPeriodos.Count - 1].Split('à');
                                DateTime datDataFim = Convert.ToDateTime(strDatas[1]);

                                //Passa como parâmetro a última data escalada para o próx período
                                colPeriodosDisponiveis = objBLEscala.ObterPeriodos(pobjEscalaDepartamental.ObjPeriodicidade.DescricaoPeriodicidade,
                                  pobjEscalaDepartamental.IdEscalaDpto, this.IdEscalacao, datDataFim);

                                Collection<string> colProximosPeriodos = new Collection<string>();

                                //Exclui períodos já preenchidos parcialmente
                                foreach (string strPeriodo in colPeriodosDisponiveis)
                                {
                                    string[] strDatasProxPeriodo = strPeriodo.Split('à');
                                    DateTime datDataInicioProxPeriodo, datDataFimProxPeriodo;
                                    int intIdEscalaDptoProxPeriodo;

                                    intIdEscalaDptoProxPeriodo = Convert.ToInt32(ddlEscalaDepartamental.SelectedItem.Value);
                                    datDataInicioProxPeriodo = Convert.ToDateTime(strDatasProxPeriodo[0]);
                                    datDataFimProxPeriodo = Convert.ToDateTime(strDatasProxPeriodo[1]);


                                    DataTable dtt = objBLEscala.ObterPeriodosJaCadastrados(intIdEscalaDptoProxPeriodo, datDataInicioProxPeriodo, datDataFimProxPeriodo);

                                    //período ainda não cadastrado para a escalação
                                    if (dtt.Rows.Count == 0)
                                    {
                                        colProximosPeriodos.Add(strPeriodo);
                                    }
                                    else
                                    {
                                        DateTime datInicioPrimeiroPeriodo = Convert.ToDateTime(dtt.Rows[dtt.Rows.Count - 1][1]).AddDays(1);
                                        DataTable dttPrimeiroPeriodo = objBLEscala.ObterPeriodosJaCadastrados(intIdEscalaDptoProxPeriodo, datInicioPrimeiroPeriodo, datDataFimProxPeriodo);

                                        if (dttPrimeiroPeriodo.Rows.Count == 0)
                                        {
                                            colProximosPeriodos.Add(datInicioPrimeiroPeriodo.ToShortDateString()
                                            + " à " + datDataFimProxPeriodo.ToShortDateString());
                                        }
                                    }

                                }
                                colPeriodosDisponiveis = new Collection<string>();
                                colPeriodosDisponiveis = colProximosPeriodos;
                            }
                        }
                        else
                        {
                            colPeriodosDisponiveis = colPeriodos;
                        }

                        pddlPeriodo.Items.Clear();

                        for (int i = 0; i < colPeriodosDisponiveis.Count; i++)
                        {
                            ListItem ltmPerido = new ListItem(colPeriodosDisponiveis[i],
                                                              colPeriodosDisponiveis[i]);

                            pddlPeriodo.Items.Add(ltmPerido);
                        }

                        BLUtilitarios.InseriMensagemDropDownList(ref pddlPeriodo, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0);

                        if (colPeriodosDisponiveis.Count == 1)
                        {
                            pddlPeriodo.SelectedIndex = 1;
                        }
                    }
                    else
                    {
                        //limpa a lista de períodos
                        pddlPeriodo.Items.Clear();

                        BLUtilitarios.InseriMensagemDropDownList(ref pddlPeriodo, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0);
                        pddlPeriodo.SelectedIndex = 0;
                    }
                }
                else
                {
                    //periodos troca de horario

                    colPeriodos = objBLEscala.ObterPeriodosTrocaHorario(pobjEscalaDepartamental.IdEscalaDpto);
                    this.ddlPeriodo.Items.Clear();

                    for (int i = 0; i < colPeriodos.Count; i++)
                    {
                        ListItem ltmPeridoRonda = new ListItem(colPeriodos[i],
                                                                colPeriodos[i]);

                        ddlPeriodo.Items.Add(ltmPeridoRonda);
                    }

                    BLUtilitarios.InseriMensagemDropDownList(ref ddlPeriodo, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0);

                    colPeriodos.Clear();
                }

                if (!string.IsNullOrEmpty(this.PeriodoSelecionado))
                {
                    ListItem ltmPeriodo = pddlPeriodo.Items.FindByValue(this.PeriodoSelecionado);

                    if (ltmPeriodo != null)
                    {
                        ltmPeriodo.Selected = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }
        #endregion

        #region Popular Combos
        /// <summary>
        ///     Popula Combos
        /// </summary>
        /// <param name="penmTipoPainel">Tipo do Painel</param>
        /// <history>
        ///     [cmarchi] created 7/1/2010
        ///     [cmarchi] modify 9/2/2010
        ///     [haguiar] modify 26/10/2010
        /// </history>
        private void PopularCombos(Enums.TipoPainel penmTipoPainel)
        {
            BLEscalaDepartamental objBLEscalaDepartamental = new BLEscalaDepartamental();
            EscalaDepartamental objEscalaDepartamental = null;

            try
            {
                if (penmTipoPainel == Enums.TipoPainel.Cadastro)
                {
                    objEscalaDepartamental = objBLEscalaDepartamental.Obter(
                        Convert.ToInt32(this.ddlEscalaDepartamental.SelectedValue), false, null);

                    if (objEscalaDepartamental != null && this.ddlEscalaDepartamental.SelectedIndex != 0)
                    {
                        //grava o id da escala
                        this.IdEscalaDepartamental = objEscalaDepartamental.IdEscalaDpto;
                        this.PopularPeriodo(objEscalaDepartamental, ref this.ddlPeriodo);
                    }
                    else
                    {
                        this.IdEscalaDepartamental = 0;
                        this.PeriodoSelecionado = null;

                        //limpa a lista de períodos
                        this.ddlPeriodo.Items.Clear();
                        BLUtilitarios.InseriMensagemDropDownList(ref this.ddlPeriodo, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0);
                        
                        ddlPeriodoRonda.Items.Clear();
                        BLUtilitarios.InseriMensagemDropDownList(ref ddlPeriodoRonda, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0);
                    }
                }                
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }
        #endregion
        
        #region Tipo Solicitação
        /// <summary>
        ///     Popula o combo Tipo Solicitação
        /// </summary>
        /// <history>
        ///     [cmarchi] created 11/1/2010 
        ///</history>
        protected void PopularTipoSolicitacao()
        {
            BLEscala objBLEscala = new BLEscala();
            Collection<TipoSolicitacao> colSolicitacao = null;

            try
            {
                colSolicitacao = objBLEscala.ListarTipoSolicitacao();

                this.ddlTipoSolicitacao.DataTextField = "Descricao";
                this.ddlTipoSolicitacao.DataValueField = "Codigo";

                this.ddlTipoSolicitacao.DataSource = colSolicitacao;
                this.ddlTipoSolicitacao.DataBind();

                BLUtilitarios.InseriMensagemDropDownList(ref this.ddlTipoSolicitacao, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0);
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }

        }
        #endregion

        #endregion

        #region RadWindow

        /// <summary>
        ///     Abre a RadWindow com a tela de cadastro de escala.
        /// </summary>
        /// <history>
        ///     [cmarchi] created 19/11/2009
        /// </history>
        protected void RadWindowCadastroEscala()
        {
            //cria radwindow
            RadWindow rwdWindow = new RadWindow();
            RadWindowManager rwmWindowManager = new RadWindowManager();

            rwmWindowManager.Skin = "Office2007";
            //Não exibe nenhum botão na janela
            rwmWindowManager.Behavior = Telerik.WebControls.RadWindowBehaviorFlags.None;
            rwmWindowManager.ShowContentDuringLoad = false;
            rwmWindowManager.VisibleStatusbar = false;

            rwdWindow.Width = Unit.Pixel(530);
            rwdWindow.Height = Unit.Pixel(460);
            rwdWindow.Modal = true;
            rwdWindow.ReloadOnShow = true;
            rwdWindow.DestroyOnClose = true;
            rwdWindow.VisibleOnPageLoad = true;

            rwdWindow.Title = "Listagem de Colaboradores";

            rwdWindow.NavigateUrl = "ListColaboradoresEscala.aspx?open=" + BLEncriptacao.EncQueryStr("sim");
            rwmWindowManager.Windows.Add(rwdWindow);

            Panel pnlEscala = null;

            //Tenta encontrar na master
            pnlEscala = (Panel)this.FindControl("pnlCadEscala");
            pnlEscala.Controls.Add(rwmWindowManager);
        }
        #endregion

        #region Validações

        #region cvrListaColaboradores_ServerValidate
        /// <summary>
        /// Valida o a lista de Colaboradores.
        /// </summary>
        /// <history>
        ///     [cmarchi] Created 7/01/2010
        /// </history>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cvrListaColaboradores_ServerValidate(object sender, ServerValidateEventArgs e)
        {
            if (this.lstColaboradores.Items.Count > 0)
                e.IsValid = true;
            else
                e.IsValid = false;
        }
        #endregion

        #endregion

        #region VerificarHiddenColaboradores
        /// <summary>
        /// Verifica o valor da query string.
        /// </summary>
        /// <history>
        ///     [cmarchi] created 6/1/2010
        /// </history>
        private void VerificarHiddenColaboradores()
        {
            string[] arrColaboradores = null;

            if (!string.IsNullOrEmpty(this.txtHiddenColaboradores.Value))
                arrColaboradores = BLEncriptacao.DecQueryStr(this.txtHiddenColaboradores.Value).Split(',');

            this.PopularListaColaboradores(arrColaboradores);
        }

        #endregion
    }
}