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
    public partial class CadSelecaoEscalaColaborador : FWPage
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
            this.VerificarHiddenColaboradores();
            this.InicializaScripts();

            /*
             * VERIFICA SE É RETORNO DO CADASTRO E DA UMA MENSAGEM DE OK
             * 23/06/2010 by [abranco] 
             */
            
            string strReturnCad = this.Request.QueryString["MsgCadOk"];

            if (strReturnCad != string.Empty && strReturnCad != null && this.blnApresentouMensagem == false)
            {
                this.RadAjaxPanel1.Alert(strReturnCad);
                
                //retorna para o estado inicial            
                this.blnApresentouMensagem = true;
            }
            
            if (!this.Page.IsPostBack)
            {
                if (!VerificarQueryEditarCadastrar(this.Request.QueryString["mod"]))
                {
                    this.BindModel(Enums.TipoTransacao.Novo, Enums.TipoPainel.Listagem);
                }
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
            BLEscala objBLEscala = new BLEscala();

            try
            {
                if (this.ValidarCampos())
                {
                    this.BindModel(Enums.TipoTransacao.CarregarDados, Enums.TipoPainel.Cadastro);

                    if (!this.BlnEditar)
                    {
                        this.gobjEscalacao.IdEscalacao = objBLEscala.Inserir(this.gobjEscalacao, this.gcolColaboradores);

                        string strParametro = BLEncriptacao.EncQueryStr("CadSel," + this.gobjEscalacao.IdEscalacao.ToString());
                        this.Response.Redirect("CadJornadaEscala.aspx?mod=" + strParametro);
                    }
                    else
                    {
                        string strParametro = BLEncriptacao.EncQueryStr("CadSel," + this.gobjEscalacao.IdEscalacao.ToString());
                        objBLEscala.Editar(this.gobjEscalacao, this.gcolColaboradores);
                        this.Response.Redirect("CadJornadaEscala.aspx?mod=" + strParametro);
                    }
                }
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
                //painel de listagem
                this.PopularRegional(ref this.ddlRegionalList);
                this.PopularFilial(ref this.ddlFilialList);

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
                Collection<Colaborador> colColaboradores = null;

                this.lstColaboradores.Items.Clear();

                this.PopularEscalaDepartamental(Enums.TipoPainel.Cadastro, false);

                BLUtilitarios.ConsultarValorCombo(ref this.ddlEscalaDepartamental,
                    this.gobjEscalacao.IdEscalaDepartamental.ToString());

                this.PopularCombos(Enums.TipoPainel.Cadastro);

                try
                {
                    colColaboradores = objBLEscalaDepartamental.ObterColaboradores(
                                                    this.gobjEscalacao.IdEscalaDepartamental);

                    this.PopularListaColaboradores(colColaboradores);

                }
                catch (Exception ex)
                {
                    ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
                }
            }
            //inicializa as informações na tela
            else if (penmTipoTransacao == Enums.TipoTransacao.Novo)
            {
                this.Response.Redirect("CadEscalaHorario.aspx");
                //this.ddlEscalaDepartamental.Items.Clear();
                ////this.ddlRegional.Items.Clear();
                ////this.ddlFilial.Items.Clear();
                //this.ddlPeriodo.Items.Clear();

                //this.lstColaboradores.Items.Clear();

                //BLUtilitarios.InseriMensagemDropDownList(ref this.ddlRegional, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0);
                //BLUtilitarios.InseriMensagemDropDownList(ref this.ddlFilial, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0);
                //BLUtilitarios.InseriMensagemDropDownList(ref this.ddlPeriodo, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0);

                //this.PopularEscalaDepartamental(Enums.TipoPainel.Cadastro, false);
            }
        }
        #endregion

        #region BindEscala
        /// <summary>
        /// Bind Escala
        /// </summary>     
        /// <history>
        ///     [cmarchi] created 10/1/2010
        ///     [cmarchi] modify  29/1/2010
        ///     [haguiar] modify 27/10/2010
        ///     [haguiar] modify 04/11/2010
        /// </history>
        protected void BindEscala(Enums.TipoBind penmTipoBind)
        {
            BLEscala objBLEscala = new BLEscala();

            int intNumeroEscala = 0;
            int intEscalaDepartamental = 0;
            int intRegional = 0;
            int intFilial = 0;
            int intStatus = 0;
            int intTipoSolicitacao = 0;

            DateTime? datDataInicio = null;
            DateTime? datDataFinal = null;

            DateTime datIni;
            DateTime datFim;

            DataTable colLista;

            try
            {
                //verifica os parâmetros da consulta
                if (DateTime.TryParse(this.txtDataInicioList.Text.Trim(), out datIni))
                    datDataInicio = datIni;

                if (DateTime.TryParse(this.txtDataFimList.Text.Trim(), out datFim))
                    datDataFinal = datFim;

                if (!string.IsNullOrEmpty(this.txtNumeroEscalaList.Text))
                {
                    int intResultado;
                    if (Int32.TryParse(this.txtNumeroEscalaList.Text.Trim(), out intResultado))
                        intNumeroEscala = intResultado;
                }

                if (this.ddlEscalaDepartamentalList.SelectedIndex > 0)
                    intEscalaDepartamental = Convert.ToInt32(
                                         this.ddlEscalaDepartamentalList.SelectedValue);

                /*if (this.ddlRegionalList.SelectedIndex > 0)
                    intRegional = Convert.ToInt32(
                                         this.ddlRegionalList.SelectedValue);

                if (this.ddlFilialList.SelectedIndex > 0)
                    intFilial = Convert.ToInt32(
                                         this.ddlFilialList.SelectedValue);
                */

                intRegional = IdRegional;
                intFilial = IdFilial;

                //status nao pode ter o valor 0
                if (this.ddlStatusList.SelectedIndex > 0)
                {
                    intStatus = Convert.ToInt32(
                                         this.ddlStatusList.SelectedValue);
                }

                if (this.ddlTipoSolicitacaoList.SelectedIndex > 0)
                    intTipoSolicitacao = Convert.ToInt32(
                                         this.ddlTipoSolicitacaoList.SelectedValue);

               colLista = objBLEscala.Listar(intNumeroEscala, intEscalaDepartamental,
                    intRegional, intFilial, this.txtSolicitanteList.Text.Trim(),
                    this.txtColaboradorList.Text.Trim(),
                    this.txtAprovadorList.Text.Trim(),
                    intStatus, intTipoSolicitacao, datDataInicio, datDataFinal,
                    Convert.ToInt32(BLAcesso.IdUsuarioLogado()));

               this.radGridEscala.DataSource = colLista;


               radGridEscala.CurrentPageIndex = BLUtilitarios.ObterPageIndiceGrid(this.radGridEscala.CurrentPageIndex,
                                                                                  this.radGridEscala.PageCount,
                                                                                  colLista.Rows.Count,
                                                                                  this.radGridEscala.PageSize);


                if (penmTipoBind == Enums.TipoBind.DataBind)
                {
                    this.radGridEscala.DataBind();
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }
        #endregion

        #region BindListagem
        /// <summary>
        /// Bind Listagem
        /// </summary>
        /// <param name="pintTipoTransacao">Tipo da Transação</param>        
        /// <history>
        ///     [cmarchi] created 7/1/2010
        ///     [haguiar] modify 26/10/2010
        /// </history>
        protected void BindListagem(Enums.TipoTransacao penmTipoTransacao)
        {
            if (penmTipoTransacao == Enums.TipoTransacao.Novo)
            {
                this.PeriodoSelecionado = string.Empty;

                this.txtAprovadorList.Text = string.Empty;
                this.txtColaboradorList.Text = string.Empty;

                this.txtDataInicioList.Text = string.Empty;
                this.txtDataFimList.Text = string.Empty;

                this.txtNumeroEscalaList.Text = string.Empty;
                this.txtSolicitanteList.Text = string.Empty;

                //try
                //{
                //    //painel de listagem
                //    this.PopularRegional(ref this.ddlRegionalList);
                //    this.PopularFilial(ref this.ddlFilialList);

                //    //painel de cadastro
                //    this.PopularRegional(ref this.ddlRegional);
                //    this.PopularFilial(ref this.ddlFilial);

                //}
                //catch (Exception)
                //{
                //    throw;
                //}

                //BLUtilitarios.InseriMensagemDropDownList(ref this.ddlRegionalList, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0);
                //BLUtilitarios.InseriMensagemDropDownList(ref this.ddlFilialList, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0);

                this.ddlRegionalList.Enabled = false;
                this.ddlFilialList.Enabled = false;

                this.ddlRegional.Enabled = false;
                this.ddlFilial.Enabled = false;

                this.PopularEscalaDepartamental(Enums.TipoPainel.Listagem, true);
                this.PopularTipoSolicitacao();
                this.PopularTipoStatus();

                this.BindEscala(Enums.TipoBind.DataBind);
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

            //try
            //{
            //    //seleciona a regional e a filial do usuário logado.
            //    BLColaborador objBlColaborador = new BLColaborador();
            //    DataTable dtt = new DataTable();

            //    dtt = objBlColaborador.ObterRegFilUsuario(Convert.ToInt32(BLAcesso.IdUsuarioLogado()));

            //    this.IdRegional = Convert.ToInt32(dtt.Rows[0][0].ToString());
            //    this.IdFilial = Convert.ToInt32(dtt.Rows[0][1].ToString());
            //}
            //catch (Exception ex)
            //{
            //    ex = null;
            //}

            //verifica se  listagem ou cadastro na página
            if (penmTipoPainel == Enums.TipoPainel.Cadastro)
            {
                this.ControlaPaineis(Enums.TipoPainel.Cadastro);
                this.BindCadastro(penmTipoTransacao);
            }
            else
            {
                this.ControlaPaineis(Enums.TipoPainel.Listagem);
                this.BindListagem(penmTipoTransacao);
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

        #region Buscar
        /// <summary>
        /// Faz a busca de escalações de acordo com os parâmetros preenchidos.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [cmarchi] 17/12/2009 created
        ///     [cmarchi] 4/1/2010 modify
        /// </history>
        protected void btnBuscarList_Click(object sender, EventArgs e)
        {
            radGridEscala.CurrentPageIndex = 0;
            this.BindEscala(Enums.TipoBind.DataBind);
        }
        #endregion

        #region Montar Nova Escala
        /// <summary>
        /// Chama a tela de cadastro de seleção de escala e colaboradores.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [cmarchi] created 24/10/2009
        /// </history>
        protected void btnMontarNovaEscala_Click(object sender, EventArgs e)
        {
            this.BindModel(Enums.TipoTransacao.Novo, Enums.TipoPainel.Cadastro);
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

        #region Voltar
        /// <summary>
        /// Chama a tela de listagem de escalas periódicas de colaboradores
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [cmarchi] created 24/10/2009
        ///     [cmarchi] modify 7/1/2010
        /// </history>
        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            this.IdEscalaDepartamental = 0;
            this.IdEscalacao = 0;
            this.PeriodoSelecionado = string.Empty;

            this.gobjEscalacao = null;
            this.gcolColaboradores = null;

            this.BlnEditar = false;

            this.HabilitarCampos();

            this.BindModel(Enums.TipoTransacao.Novo, Enums.TipoPainel.Listagem);
        }
        #endregion

        #endregion

        #region Controla Painels
        /// <summary>
        /// Controla os Painels de Listagem e Cadastro
        /// </summary>
        /// <param name="penmPainel">Painel a ser exibido</param>
        /// <history>
        ///     [cmarchi] created 23/11/2009
        /// </history>
        protected void ControlaPaineis(Enums.TipoPainel penmPainel)
        {
            if (penmPainel == Enums.TipoPainel.Listagem)
            {
                pnlCadastro.Visible = false;
                pnlListagem.Visible = true;
            }
            else
            {
                pnlCadastro.Visible = true;
                pnlListagem.Visible = false;
            }

        }
        #endregion

        #region DesabilitarCampos
        /// <summary>
        ///    Desabilita os campos da tela de cadastro.
        /// </summary>
        /// <history>
        ///     [cmarchi] created 12/1/2010
        ///     [cmarchi] modify 8/2/2010
        /// </history>
        private void DesabilitarCampos()
        {
            this.ddlEscalaDepartamental.Enabled = false;
            this.ddlPeriodo.Enabled = false;

            if (this.BlnEditar)
            {
                this.btnAdicionar.Enabled = true;
                this.btnRemover.Enabled = true;
                this.btnAvancar.Enabled = true;
            }
            else
            {
                this.btnAdicionar.Enabled = false;
                this.btnRemover.Enabled = false;
                this.btnAvancar.Enabled = false;
            }
        }
        #endregion

        #region Editar
        /// <summary>
        ///     Editar uma Escalação.
        /// </summary>
        /// <param name="pintIdEscalacao">Id da Escalação</param>
        /// <history>
        ///     [cmarchi] created 12/1/2010
        ///     [haguiar] modify 17/10/2010
        ///     Exclui o status pendente da solicitacao
        ///     [haguiar_2] modify 30/11/2010
        ///     permite editar troca de horário
        /// </history>
        private void Editar(int pintIdEscalacao)
        {
            BLEscala objBLEscala = new BLEscala();

            try
            {
                if (this.IdStatusSolicitacao > 0)
                {
                    //exclui status pendente da solicitacao
                    objBLEscala.ExcluirStatusPendente(pintIdEscalacao);
                }

                //verifica troca de horário
                if (this.IdTipoSolicitacao == 9)
                {
                    string strParametro = BLEncriptacao.EncQueryStr("CarregaDatas," + pintIdEscalacao);
                    this.Response.Redirect("CadEscalaHorarioColaborador.aspx?mod=" + strParametro, false);

                    return;
                }

                this.gblnEdicao = true;
                this.gobjEscalacao = objBLEscala.Obter(pintIdEscalacao);
                this.BlnEditar = true;
                this.IdEscalacao = pintIdEscalacao;
                this.BindModel(Enums.TipoTransacao.DescarregarDados, Enums.TipoPainel.Cadastro);
                this.DesabilitarCampos();
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }
        #endregion

        #region Eventos

        #region Listagem

        #region Escala Departamental
        /// <summary>
        ///     Seleciona uma regional de acordo com a escala departamental
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [cmarchi] created 6/1/2010
        /// </history>
        protected void ddlEscalaDepartamentalList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.PopularCombos(Enums.TipoPainel.Listagem);
        }
        #endregion

        #endregion

        #region Cadastro

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

            this.PopularCombos(Enums.TipoPainel.Cadastro);

            try
            {
                colColaboradores = objBLEscalaDepartamental.ObterColaboradores(
                                                   this.IdEscalaDepartamental);

                this.PopularListaColaboradores(colColaboradores);
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }
        #endregion

        #endregion

        #endregion

        #region Grid Escala Listagem

        #region NeedDataSource
        protected void radGridEscala_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (Page.IsPostBack)
            {
                this.BindEscala(Enums.TipoBind.SemDataBind);
            }
        }
        #endregion

        #region ItemCommand

        /// <summary>
        ///     radGridEscala_ItemCommand
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [no history]
        ///     [haguiar] modify 29/10/2010
        ///     [haguiar] modify 03/11/2010
        ///     [haguiar] modify 05/11/2010
        ///     [haguiar_2] modify 24/11/2010
        ///     Inicia o processo de troca de horário ao clicar no botao editar (lápis)
        ///     [haguiar} modify 26/09/2011 17:07
        ///     somente ativa escalacao se nao houver outro período igual
        /// </history>
        protected void radGridEscala_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName.Trim() == "Ativar")
            {
                bool blnSituacao = true;
                int intIdEscalacao = Convert.ToInt32(e.CommandArgument.ToString().Trim());

                Telerik.WebControls.GridDataItem dataItem = (Telerik.WebControls.GridDataItem)e.Item;
                ImageButton btnAtivar = (ImageButton)dataItem["Ativar"].Controls[0];

                try
                {
                    BLEscala objBLEscala = new BLEscala();
                    
                    //altera o botão
                    if (btnAtivar.ToolTip == "Ativar")
                    {
                        //btnAtivar.ImageUrl = "~/imagens/icones/ico_ativo.gif";
                        //btnAtivar.ToolTip = "Inativar";
                        blnSituacao = true;
                    }
                    else if (btnAtivar.ToolTip == "Inativar")
                    {
                        //btnAtivar.ImageUrl = "~/imagens/icones/ico_inativo.gif";
                        //btnAtivar.ToolTip = "Ativar";
                        blnSituacao = false;
                    }

                    if (objBLEscala.AlterarSituacao(intIdEscalacao,
                                                    blnSituacao,
                                                    Convert.ToInt32(BLAcesso.IdUsuarioLogado())) > 0)
                    {
                        //altera o botão
                        if (btnAtivar.ToolTip == "Ativar")
                        {
                            btnAtivar.ImageUrl = "~/imagens/icones/ico_ativo.gif";
                            btnAtivar.ToolTip = "Inativar";
                        }
                        else if (btnAtivar.ToolTip == "Inativar")
                        {
                            btnAtivar.ImageUrl = "~/imagens/icones/ico_inativo.gif";
                            btnAtivar.ToolTip = "Ativar";
                        }
                    }
                    else
                    {
                        this.RadAjaxPanel1.Alert("Já existe uma solicitação ATIVA para este período. Verifique!");
                    }
                    //inativa linha e habilita apos postback
                    e.Item.Enabled = false;

                }
                catch (Exception ex)
                {
                    ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
                }
            }

            if (e.CommandName.Trim() == "Editar")
            {
                this.IdStatusSolicitacao = 0;
                this.IdTipoSolicitacao = 0;

                if (e.CommandArgument.ToString().Trim() != string.Empty)
                {
                    if (Permissoes.Alteração())
                    {
                        //Flg_Editando
                        if (Convert.ToBoolean(e.Item.Cells[13].Text))
                        {
                            if (Convert.ToInt32(e.Item.Cells[15].Text).Equals(9))
                            {
                                string strParametro = BLEncriptacao.EncQueryStr("TrocaHor," + Convert.ToInt32(e.CommandArgument.ToString().Trim()));
                                this.Response.Redirect("CadTrocaEscalaHorario.aspx?mod=" + strParametro, false);
                        }
                        else
                        {
                                string strParametro = BLEncriptacao.EncQueryStr("CadHor," + Convert.ToInt32(e.CommandArgument.ToString().Trim()));
                                this.Response.Redirect("CadEscalaHorario.aspx?mod=" + strParametro, false);
                            }
                        }
                        else
                        {
                            Telerik.WebControls.GridDataItem dataItem = (Telerik.WebControls.GridDataItem)e.Item;

                            //verifica se ainda está em aprovaçao
                            if (Convert.ToInt32(Convert.ToInt32(e.Item.Cells[14].Text)) < 3)
                            {
                                if (Convert.ToInt32( e.Item.Cells[15].Text).Equals(9))
                                {
                                    string strParametro = BLEncriptacao.EncQueryStr("TrocaHor," + Convert.ToInt32(e.CommandArgument.ToString().Trim()));
                                    this.Response.Redirect("CadTrocaEscalaHorario.aspx?mod=" + strParametro, false);
                                }
                                else
                                {
                                    string strParametro = BLEncriptacao.EncQueryStr("CadHor," + Convert.ToInt32(e.CommandArgument.ToString().Trim()));
                                    this.Response.Redirect("CadEscalaHorario.aspx?mod=" + strParametro, false);
                                }

                            }
                            else
                            {
                                //TODO CAIO
                                //escala finalizada, abre troca de horário
                                if (Convert.ToInt32(Convert.ToInt32(e.Item.Cells[14].Text)) == 3 && Convert.ToInt32(e.Item.Cells[15].Text).Equals(7))
                                {
                                    string strParametro = BLEncriptacao.EncQueryStr("CadHor," + Convert.ToInt32(e.CommandArgument.ToString().Trim()));
                                    this.Response.Redirect("CadTrocaEscalaHorario.aspx?mod=" + strParametro, false);
                                }
                                else
                                {
                                    this.RadAjaxPanel1.Alert("Ímpossível editar a escala.");
                                }
                            }

                        }
                    }
                }
            }

            if (e.CommandName.Trim() == "Visualizar" && !this.gblnEdicao)
            {
                //nao exibe o item se estiver inativo (ativou ou desativou o item)
                if (e.CommandArgument.ToString().Trim() != string.Empty && e.Item.Enabled)                
                {
                    Telerik.WebControls.GridDataItem dataItem = (Telerik.WebControls.GridDataItem)e.Item;

                    //tipo da solicitacao para troca de horario
                    this.IdTipoSolicitacao = Convert.ToInt32(dataItem["ID_TipoSolicitacao"].Text.ToString()); 

                    if (Permissoes.Alteração())
                    {
                        this.Visualizar(Convert.ToInt32(e.CommandArgument.ToString().Trim()));
                    }
                }
                else
                {
                    //ativa o item para visualizacao novamente
                    e.Item.Enabled = true;
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

            */
        }
        #endregion

        #region ItemDataBound
        /// <summary>
        ///     Bind GridEscala
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [no history]
        ///     [haguiar] modify 29/10/2010
        ///     [haguiar_2] modify 24/11/2010
        ///     A escala pode ser editada mesmo com status finalizada, possibilitando a troca de horário.
        ///     [haguiar_2] modify 16/12/2010
        ///     O botão editar não deve aparecer em troca de horário finalizada.
        ///     [haguiar_3] modify 07/01/2011
        ///     O botão editar deve aparecer desabilitado caso a data final da escala já tenha passado.
        /// </history>
        protected void radGridEscala_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item.ItemType == Telerik.WebControls.GridItemType.AlternatingItem | e.Item.ItemType == Telerik.WebControls.GridItemType.Item)
            {
                ImageButton btnVisualizar;
                btnVisualizar = (ImageButton)e.Item.FindControl("Visualizar");

                Telerik.WebControls.GridDataItem dataItemEditar = (Telerik.WebControls.GridDataItem)e.Item;
                ImageButton btnEditar = (ImageButton)dataItemEditar["Editar"].Controls[0];

                Telerik.WebControls.GridDataItem dataItem = (Telerik.WebControls.GridDataItem)e.Item;
                ImageButton btnAtivar = (ImageButton)dataItem["Ativar"].Controls[0];

                if ((btnAtivar != null && btnVisualizar != null && btnEditar != null))
                {
                    int intIdUsuario = Convert.ToInt32(BLAcesso.IdUsuarioLogado());
                    DataTable dttAprovadores = new DataTable();
                    BLEscala objBLEscala = new BLEscala();


                    //escala ou troca de horário não pode ser editada se já passou a data final
                    bool blnDataFinal;
                    TimeSpan ts;

                    ts =  DateTime.Now - Convert.ToDateTime(DataBinder.Eval(e.Item.DataItem, "Dt_FinalPeriodo"));
                    blnDataFinal = (ts.Days > 0);

                    try
                    {
                        dttAprovadores = objBLEscala.ObterAprovadores(
                            Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "Id_Escalacao")));
                    }
                    catch (Exception ex)
                    {
                        ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
                    }

                    for (int i = 0; i < dttAprovadores.Rows.Count; i++)
                    {

                        if (intIdUsuario == Convert.ToInt32(dttAprovadores.Rows[i][0]))
                        {
                            if (intIdUsuario == Convert.ToInt32(
                                DataBinder.Eval(e.Item.DataItem, "USU_N_CODIGO").ToString()))
                            {
                                btnVisualizar.Visible = false;
                                e.Item.Attributes.Add("onMouseOver", "selecionaGrid(this)");
                                e.Item.Attributes.Add("onMouseOut", "deSelecionaGrid(this)");
                                e.Item.Style["cursor"] = "hand";
                                e.Item.ToolTip = BLIdiomas.TraduzirMensagens(Mensagens.VISUALIZAR);

                                // Cursor 
                                int intCell = e.Item.Cells.Count;
                                for (int @int = 0; @int <= intCell - 4; @int++)
                                {
                                    e.Item.Cells[@int].Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(btnVisualizar, ""));
                                }
                            }

                            if (Convert.ToBoolean(DataBinder.Eval(e.Item.DataItem, "Flg_Editando")))
                            {
                                if (!blnDataFinal)
                                {
                                    btnEditar.ToolTip = BLIdiomas.TraduzirMensagens(Mensagens.EDITAR);
                                    btnEditar.CommandArgument = DataBinder.Eval(e.Item.DataItem, "Id_Escalacao").ToString();
                                    btnEditar.Visible = true;
                                }
                                else
                                {
                                    btnEditar.Visible = false;
                                }
                            }
                            else
                            {
                                //verifica possível alteraçao de solicitacao, caso ainda esteja em aprovacao ou finalizada
                                if (Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "ID_STATUSSOLICITACAO")) <= 3)
                                {
                                    bool blnBotaoEditar = true;
                                    
                                    if (Convert.ToInt32(dataItem["ID_TipoSolicitacao"].Text.ToString()) == 9 && Convert.ToInt32(dataItem["ID_STATUSSOLICITACAO"].Text.ToString()) == 3)
                                        blnBotaoEditar = false;

                                    if (!blnDataFinal)
                                    {

                                        btnEditar.ToolTip = BLIdiomas.TraduzirMensagens(Mensagens.EDITAR);
                                        btnEditar.CommandArgument = DataBinder.Eval(e.Item.DataItem, "Id_Escalacao").ToString();
                                        btnEditar.Visible = blnBotaoEditar;
                                    }
                                    else
                                    {
                                        btnEditar.Visible = false;
                                    }

                                }
                                else
                                {
                                    btnEditar.Visible = false;
                                }
                            }

                            if (Convert.ToBoolean(DataBinder.Eval(e.Item.DataItem, "Flg_Situacao")))
                            {
                                btnAtivar.ImageUrl = "~/imagens/icones/ico_ativo.gif";
                                btnAtivar.ToolTip = "Inativar";
                            }
                            else
                            {
                                btnAtivar.ImageUrl = "~/imagens/icones/ico_inativo.gif";
                                btnAtivar.ToolTip = "Ativar";
                            }

                            btnAtivar.CommandArgument = DataBinder.Eval(e.Item.DataItem, "Id_Escalacao").ToString();
                        }
                    }
                }
            }
        }
        #endregion

        #endregion

        #region HabilitarCampos
        /// <summary>
        ///    Habilita os campos da tela de cadastro.
        /// </summary>
        /// <history>
        ///     [cmarchi] created 12/1/2010
        ///     [cmarchi] modify 8/2/2010
        /// </history>
        private void HabilitarCampos()
        {
            this.ddlEscalaDepartamental.Enabled = true;
            this.ddlPeriodo.Enabled = true;

            this.btnAdicionar.Enabled = true;
            this.btnRemover.Enabled = true;
            this.btnAvancar.Enabled = true;
        }
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

            this.txtDataInicioList.Attributes.Add("OnKeyPress", "return FormataData(event,this);");
            this.txtDataFimList.Attributes.Add("OnKeyPress", "return FormataData(event,this);");

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
        protected void PopularEscalaDepartamental(Enums.TipoPainel penmTipoPainel, bool pblnListarTudo)
        {
            BLEscalaDepartamental objBLEscalaDepartamental = new BLEscalaDepartamental();
            Collection<EscalaDepartamental> colEscalaDepartamental = new Collection<EscalaDepartamental>();

            try
            {
                //colEscalaDepartamental = objBLEscalaDepartamental.ListarEscalaDepartamentalByFlagSituacao(pblnListarTudo);
                //DataTable dtt = objBLEscalaDepartamental.ListarEscalaDepartamental(this.IdRegional, this.IdFilial, string.Empty, 0, pblnListarTudo, true);

                DataTable dtt;

                if (penmTipoPainel == Enums.TipoPainel.Listagem || this.BlnEditar)
                {
                    //listar apenas escala departamental da regional/filial do logado, ativas ou nao
                    //dtt = objBLEscalaDepartamental.ListarEscalaDepartamental(this.IdRegional, this.IdFilial, string.Empty, 0);
                    dtt = objBLEscalaDepartamental.ListarEscalaDepartamental(this.IdRegional, this.IdFilial, string.Empty, 0, pblnListarTudo, true);
                }
                else
                {
                    //listar apenas escala departamental da regional/filial do logado, apenas ativas
                    dtt = objBLEscalaDepartamental.ListarEscalaDepartamental(this.IdRegional, this.IdFilial, string.Empty, 0, false, false);
                }
                
                foreach (DataRow dr in dtt.Rows)
                {
                    EscalaDepartamental objEscalaDepartamental = new EscalaDepartamental();
                    
                    objEscalaDepartamental.DescricaoEscalaDpto = Convert.ToString(dr[1]);
                    objEscalaDepartamental.IdEscalaDpto = Convert.ToInt32(dr[0]);

                    colEscalaDepartamental.Add(objEscalaDepartamental);
                }

                if (penmTipoPainel == Enums.TipoPainel.Listagem)
                {
                    //preenche escala departamental da parte de listagem
                    this.ddlEscalaDepartamentalList.DataSource = colEscalaDepartamental;
                    this.ddlEscalaDepartamentalList.DataTextField = "DescricaoEscalaDpto";
                    this.ddlEscalaDepartamentalList.DataValueField = "IdEscalaDpto";
                    this.ddlEscalaDepartamentalList.DataBind();

                    BLUtilitarios.InseriMensagemDropDownList(ref this.ddlEscalaDepartamentalList, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0);
                }
                else
                {
                    //preenche escala departamental da parte de cadastro
                    this.ddlEscalaDepartamental.DataSource = colEscalaDepartamental;
                    this.ddlEscalaDepartamental.DataTextField = "DescricaoEscalaDpto";
                    this.ddlEscalaDepartamental.DataValueField = "IdEscalaDpto";
                    this.ddlEscalaDepartamental.DataBind();

                    BLUtilitarios.InseriMensagemDropDownList(ref this.ddlEscalaDepartamental, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0);
                }
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
            
            try
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
                    if (!gblnEdicao )
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
                    }
                }
                
                else
                {
                    /*
                    objEscalaDepartamental = objBLEscalaDepartamental.Obter(
                        Convert.ToInt32(this.ddlEscalaDepartamentalList.SelectedValue));
                   
                    if (objEscalaDepartamental != null && objEscalaDepartamental.IdEscalaDpto > 0)
                    {
                        //this.PopularRegional(objEscalaDepartamental, ref this.ddlRegionalList);
                        //this.PopularFilial(objEscalaDepartamental, ref this.ddlFilialList);
                    }
                    else
                    {
                        this.ddlRegionalList.Items.Clear();
                        this.ddlFilialList.Items.Clear();

                        BLUtilitarios.InseriMensagemDropDownList(ref this.ddlRegionalList,
                            BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0);

                        BLUtilitarios.InseriMensagemDropDownList(ref this.ddlFilialList,
                            BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0);
                    }
                    */
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

                this.ddlTipoSolicitacaoList.DataTextField = "Descricao";
                this.ddlTipoSolicitacaoList.DataValueField = "Codigo";

                this.ddlTipoSolicitacaoList.DataSource = colSolicitacao;
                this.ddlTipoSolicitacaoList.DataBind();

                BLUtilitarios.InseriMensagemDropDownList(ref this.ddlTipoSolicitacaoList, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0);
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }

        }
        #endregion

        #region Tipo Status
        /// <summary>
        ///     Popula o combo Tipo Status
        /// </summary>
        /// <history>
        ///     [cmarchi] created 11/1/2010 
        ///</history>
        protected void PopularTipoStatus()
        {
            BLSolicitacao objBLSolicitacao = new BLSolicitacao();
            Collection<Status> colStatus = null;

            try
            {
                colStatus = objBLSolicitacao.ListarStatus();

                foreach (Status objStatus in colStatus)
                {
                    if (objStatus.Codigo == 0)
                    {
                        objStatus.Codigo = -1;
                        break;
                    }
                }

                this.ddlStatusList.DataTextField = "Descricao";
                this.ddlStatusList.DataValueField = "Codigo";

                this.ddlStatusList.DataSource = colStatus;
                this.ddlStatusList.DataBind();

                BLUtilitarios.InseriMensagemDropDownList(ref this.ddlStatusList, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0);
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

        #region Validar Campos

        /// <summary>
        ///     Verifica se os campos foram preenchidos corretamente
        /// </summary>
        /// <returns>True - Válidos, False - Erros</returns>
        /// <history>
        ///     [cmarchi] created 7/01/2010
        ///     [haguiar] modify 28/10/2010
        /// </history>
        protected bool ValidarCampos()
        {
            bool blnRetorno = true;

            if (this.ddlEscalaDepartamental.SelectedIndex == 0)
            {
                RadAjaxPanel1.Alert("Selecione uma Escala Departamental.");
                blnRetorno = false;
            }

            if (this.ddlRegional.SelectedItem == null)
            {
                RadAjaxPanel1.Alert("Selecione uma Regional.");
                blnRetorno = false;
            }

            if (this.ddlFilial.SelectedItem == null)
            {
                RadAjaxPanel1.Alert("Selecione uma Filial.");
                blnRetorno = false;
            }

            if (this.ddlPeriodo.SelectedIndex == 0)
            {
                RadAjaxPanel1.Alert("Selecione uma Período.");
                blnRetorno = false;
            }

            return blnRetorno;
        }

        #endregion

        #endregion

        #region VerificarQueryVoltarJornada
        /// <summary>
        /// Verifica a query Voltar da Tela de Jornadas.
        /// </summary>
        ///<param name="pstrQuery">Query</param>
        /// <returns>True - Voltou para a tela, False - inicia a tela</returns>
        /// <history>
        ///     [cmarchi] created 18/1/2010
        /// </history>
        private bool VerificarQueryEditarCadastrar(string pstrQuery)
        {
            if (!string.IsNullOrEmpty(pstrQuery))
            {
                string[] arrParametros = BLEncriptacao.DecQueryStr(pstrQuery).Split(',');

                //verifica a tela que a chamou
                if (arrParametros[0] == "CadJor")
                {
                    int intIdEscalacao;

                    if (Int32.TryParse(arrParametros[1], out intIdEscalacao))
                    {
                        BLEscala objBLEscala = new BLEscala();

                        this.gobjEscalacao = objBLEscala.Obter(intIdEscalacao);
                        this.BlnEditar = true;
                        this.IdEscalacao = intIdEscalacao;
                        this.BindModel(Enums.TipoTransacao.DescarregarDados, Enums.TipoPainel.Cadastro);
                        this.DesabilitarCampos();
                        return true;
                    }
                }
            }

            return false;
        }
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

        #region Visualizar
        /// <summary>
        ///     Visualiza uma Escalação.
        /// </summary>
        /// <param name="pintIdEscalacao">Id da Escalação</param>
        /// <history>
        ///     [cmarchi] created 12/1/2010
        ///     [cmarchi] modify 10/2/2010
        ///     [haguiar_2] modify 01/12/2010
        ///     parametro para troca de horários
        /// </history>
        private void Visualizar(int pintIdEscalacao)
        {
            string strParametros = "CadSelEscList," + pintIdEscalacao.ToString() + "," + this.IdTipoSolicitacao;

            Response.Redirect("CadEscalaFinalizacao.aspx?mod=" +
                        BLEncriptacao.EncQueryStr(strParametros));
        }
        #endregion
    }
}
