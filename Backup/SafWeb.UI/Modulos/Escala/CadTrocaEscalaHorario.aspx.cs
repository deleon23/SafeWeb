using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FrameWork.Model.Utilitarios;
using SafWeb.BusinessLayer.Escala;
using System.Collections.ObjectModel;
using SafWeb.Model.Escala;
using System.Data;
using FrameWork.BusinessLayer.Utilitarios;
using FrameWork.BusinessLayer.Idioma;
using FrameWork.Model.Idioma;
using SafWeb.Model.Colaborador;
using SafWeb.Util.Extension;
using SafWeb.BusinessLayer.Colaborador;
using FrameWork.BusinessLayer.Usuarios;
using SafWeb.BusinessLayer.Regional;
using SafWeb.Model.Regional;
using SafWeb.BusinessLayer.Filial;

namespace SafWeb.UI.Modulos.Escala
{
    public partial class CadTrocaEscalaHorario : FWPage
    {
        private System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("pt-BR");
        private System.Globalization.DateTimeFormatInfo dtfi;


        private Boolean gblnEdicao
        {
            get
            {
                if (ViewState["vsgblnEdicao"] == null)
                    ViewState["vsgblnEdicao"] = false;
                return (Boolean)ViewState["vsgblnEdicao"];
            }
            set { ViewState["vsgblnEdicao"] = value; }
        }


        private Escalacao gobjEscalacao
        {
            get
            {
                if (ViewState["vsgobjEscalacao"] == null)
                    ViewState["vsgobjEscalacao"] = "0";
                return (Escalacao)ViewState["vsgobjEscalacao"];
            }
            set { ViewState["vsgobjEscalacao"] = value; }
        }

        private string idPeriodoSelecionado
        {
            get
            {
                if (ViewState["vsidPeriodoSelecionado"] == null)
                    ViewState["vsidPeriodoSelecionado"] = "0";
                return (string)ViewState["vsidPeriodoSelecionado"];
            }
            set { ViewState["vsidPeriodoSelecionado"] = value; }
        }

        private int idJornada
        {
            get
            {
                if (ViewState["vsidJornada"] == null)
                    ViewState["vsidJornada"] = 0;
                return (int)ViewState["vsidJornada"];
            }
            set { ViewState["vsidJornada"] = value; }
        }

        private int idHorario
        {
            get
            {
                if (ViewState["vsidHorario"] == null)
                    ViewState["vsidHorario"] = 0;
                return (int)ViewState["vsidHorario"];
            }
            set { ViewState["vsidHorario"] = value; }
        }

        private int idCompensacao
        {
            get
            {
                if (ViewState["vsidCompensacao"] == null)
                    ViewState["vsidCompensacao"] = 0;
                return (int)ViewState["vsidCompensacao"];
            }
            set { ViewState["vsidCompensacao"] = value; }
        }

        private int idHorarioCompensacao
        {
            get
            {
                if (ViewState["vsidHorarioCompensacao"] == null)
                    ViewState["vsidHorarioCompensacao"] = 0;
                return (int)ViewState["vsidHorarioCompensacao"];
            }
            set { ViewState["vsidHorarioCompensacao"] = value; }
        }


        private List<ColaboradorGradeHorario> Colaboradores
        {
            get
            {
                if (ViewState["vsColaboradores"] == null)
                    ViewState["vsColaboradores"] = new List<ColaboradorGradeHorario>();
                return (List<ColaboradorGradeHorario>)ViewState["vsColaboradores"];
            }
            set { ViewState["vsColaboradores"] = value; }
        }


        private List<HorarioEscala> ListEscalaHorarios
        {
            get
            {
                if (ViewState["vsListEscalaHorarios"] == null)
                    ViewState["vsListEscalaHorarios"] = new List<HorarioEscala>();
                return (List<HorarioEscala>)ViewState["vsListEscalaHorarios"];
            }
            set { ViewState["vsListEscalaHorarios"] = value; }
        }

        private List<Jornada> ListJornadas
        {
            get
            {
                if (ViewState["vsListJornadas"] == null)
                    ViewState["vsListJornadas"] = new List<Jornada>();
                return (List<Jornada>)ViewState["vsListJornadas"];
            }
            set { ViewState["vsListJornadas"] = value; }
        }

        private List<PaginacaoView> paginacaoView
        {
            get
            {
                if (ViewState["vspaginacaoView"] == null)
                {
                    var pv = new List<PaginacaoView>();
                    pv.Add(new PaginacaoView { CurrentPageIndex = 0, qtdRegistroPagina = 30 });
                    ViewState["vspaginacaoView"] = pv;
                }
                return (List<PaginacaoView>)ViewState["vspaginacaoView"];
            }
            set { ViewState["vspaginacaoView"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            dtfi = culture.DateTimeFormat;

            if (!Page.IsPostBack)
            {
                InicializaScripts();
                PopularEscalaDepartamental();

                this.VerificarQueryTela(this.Request.QueryString["mod"]);

                this.idCompensacao = 0;
                this.idJornada = 0;
                this.idHorario = 0;
                this.idHorarioCompensacao = 0;
            }

            //ddlPeriodo.SelectedValue = this.idPeriodoSelecionado.ToString();
        }

        private void VerificarQueryTela(string pstrQuery)
        {
            if (!string.IsNullOrEmpty(pstrQuery))
            {
                string[] arrParametros = BLEncriptacao.DecQueryStr(pstrQuery).Split(',');
                BLEscala objBLEscala = new BLEscala();

                //cadastro de Troca de horários
                if (arrParametros[0] == "CadHor")
                {
                    int intIdEscalacao;

                    if (Int32.TryParse(arrParametros[1], out intIdEscalacao))
                    {
                        this.IdEscalacao = intIdEscalacao;

                        this.gobjEscalacao = objBLEscala.Obter(this.IdEscalacao);

                        this.ddlEscalaDepartamental.SelectedValue = this.gobjEscalacao.IdEscalaDepartamental.ToString();
                        this.ddlEscalaDepartamental.Enabled = false;

                        BLEscalaDepartamental objBLEscalaDepartamental = new BLEscalaDepartamental();

                        this.PeriodoSelecionado = "0";

                        this.ddlPeriodo.Items.Clear();
                        BLUtilitarios.InseriMensagemDropDownList(ref this.ddlPeriodo, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0);
                        PopularPeriodo(ref this.ddlPeriodo, this.gobjEscalacao.DataInicioPeriodo, this.gobjEscalacao.DataFinalPeriodo);
                        this.PopularCombos();

                    }
                }
                else if (arrParametros[0] == "TrocaHor")
                {
                    int intIdEscalacao;

                    if (Int32.TryParse(arrParametros[1], out intIdEscalacao))
                    {
                        this.gblnEdicao = true;

                        this.IdEscalacao = intIdEscalacao;
                        this.IdEscalacaoTroca = intIdEscalacao;

                        objBLEscala.ExcluirStatusPendente(this.IdEscalacao);

                        this.gobjEscalacao = objBLEscala.Obter(this.IdEscalacao);

                        this.ddlEscalaDepartamental.SelectedValue = this.gobjEscalacao.IdEscalaDepartamental.ToString();
                        this.ddlEscalaDepartamental.Enabled = false;

                        BLEscalaDepartamental objBLEscalaDepartamental = new BLEscalaDepartamental();

                        this.PeriodoSelecionado = string.Format("{0:dd/MM/yyyy}", this.gobjEscalacao.DataInicioPeriodo);

                        this.ddlPeriodo.Items.Clear();
                        BLUtilitarios.InseriMensagemDropDownList(ref this.ddlPeriodo, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0);
                        PopularPeriodo(ref this.ddlPeriodo, this.gobjEscalacao.DataInicioPeriodo, this.gobjEscalacao.DataFinalPeriodo);

                        this.PopularCombos();

                        this.PopularListaColaboradores(objBLEscala.ObterColaboradores(this.IdEscalacao));

                        ddlPeriodo.Enabled = false;

                        this.DataBindColaboradoresGrade();
                        //dtlEscala.DataSource = this.Colaboradores;
                        //dtlEscala.DataBind();

                    }
                }
            }
        }

        private void GetHorarioColaboradores(int _idEscalacao)
        {
            BLEscala objBLEscala = new BLEscala();
            var objHorarioColaboradores = objBLEscala.ObterDtHorColEscalacao(_idEscalacao, null, null, null);

            ColaboradorGradeHorario objColabGrade;
            foreach (var item in objHorarioColaboradores)
            {
                foreach (var idColaborador in item.CodigosColaboradores.Split(','))
                {
                    objColabGrade = this.Colaboradores.Find(m => m.idColaborador == idColaborador);
                    if (objColabGrade != null)
                    {
                        var objEscalaHorario = this.ListEscalaHorarios.Find(m => m.IdHorario == item.HorarioColaborador);

                        if (objEscalaHorario == null)
                        {
                            objEscalaHorario = this.ListEscalaHorarios.Find(m => m.CodLegado == item.CodLegado && m.IdJornada == objColabGrade.idJornada);
                        }

                        if (item.Folga.Equals("Folga/DSR"))
                        {
                            objColabGrade.blnFolga = true;
                            objColabGrade.idHorario = 0;
                        }
                        else
                        {
                            if (objColabGrade.blnFolga == true)
                            {
                                objColabGrade.idHorarioPagarFolga = objEscalaHorario.IdEscala;
                                objColabGrade.dtDataPagarFolga = item.DataColaboradores.ToString("dd/MM/yyyy");
                            }
                            else
                            {
                                if (objEscalaHorario != null)
                                {
                                    objColabGrade.idHorario = objEscalaHorario.IdEscala;
                                    if (item.HoraExtra.Equals("Hora Extra"))
                                        objColabGrade.blnHExtra = true;
                                }
                            }
                        }

                    }
                }
            }

        }

        private void GetJornadaColaboradores(int _idEscalacao)
        {
            BLJornada objBLJornada = new BLJornada();
            var objJornadaColaborador = objBLJornada.ObterJornadaColaboradores(_idEscalacao);

            ColaboradorGradeHorario objColabGrade;
            foreach (var item in objJornadaColaborador)
            {
                foreach (var idColaborador in item.CodigosColaboradores.Split(','))
                {
                    objColabGrade = this.Colaboradores.Find(m => m.idColaborador == idColaborador);
                    if (objColabGrade != null)
                    {
                        objColabGrade.idJornada = item.IdJornada;
                    }
                }
            }

        }

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
        ///     [cfrancisco] modify 10/05/2011 15:01
        ///</history>
        protected void PopularEscalaDepartamental()
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

            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
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
        ///     [cfrancisco] modify 10/05/2011 15:01
        /// </history>
        protected void ddlEscalaDepartamental_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.paginacaoView = new List<PaginacaoView>();

            BLEscalaDepartamental objBLEscalaDepartamental = new BLEscalaDepartamental();
            Collection<Colaborador> colColaboradores = null;

            //limpa a lista de períodos
            this.PeriodoSelecionado = string.Empty;

            this.ddlPeriodo.Items.Clear();
            BLUtilitarios.InseriMensagemDropDownList(ref this.ddlPeriodo, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0);

            this.PopularCombos();

            try
            {
                colColaboradores = objBLEscalaDepartamental.ObterColaboradores(
                                                   this.IdEscalaDepartamental);

                //TODO Caio
                this.PopularListaColaboradores(colColaboradores);

                this.DataBindColaboradoresGrade();
                //dtlEscala.DataSource = this.Colaboradores;
                //dtlEscala.DataBind();
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }
        #endregion


        protected void ddlPeriodo_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.idPeriodoSelecionado = ((DropDownList)sender).SelectedValue;

            if (!this.idPeriodoSelecionado.Equals("0"))
            {

                this.PopularListaColaboradores(new BLEscala().ObterColaboradores(this.IdEscalacao));


                foreach (var item in this.Colaboradores)
                {
                    item.dtData = ((DropDownList)sender).SelectedItem.Value;
                }

                this.DataBindColaboradoresGrade();
                //dtlEscala.DataSource = this.Colaboradores;
                //dtlEscala.DataBind();
            }
            else
            {
                dtlEscala.DataSource = new List<ColaboradorGradeHorario>();
                dtlEscala.DataBind();
            }
        }


        protected void ddlHorario_SelectedIndexChanged(object sender, EventArgs e)
        {
            var ddlHorario = (DropDownList)sender;
            var dtlEscalaItem = (DataListItem)ddlHorario.NamingContainer;

            int itemIndex = dtlEscalaItem.ItemIndex;
            if (this.paginacaoView.Count > 0)
                if (this.paginacaoView[0] != null)
                    itemIndex = itemIndex + (this.paginacaoView[0].qtdRegistroPagina * this.paginacaoView[0].CurrentPageIndex);

            if (ddlHorario.ID.Contains("PFolga"))
                this.Colaboradores[itemIndex].idHorarioPagarFolga = ddlHorario.SelectedValue.ToInt32();
            else
                this.Colaboradores[itemIndex].idHorario = ddlHorario.SelectedValue.ToInt32();

            //if (ddlHorario.ID.Contains("PFolga"))
            //{
            //    this.Colaboradores[dtlEscalaItem.ItemIndex].idHorarioPagarFolga = ddlHorario.SelectedValue.ToInt32();
            //}
            //else
            //    this.Colaboradores[dtlEscalaItem.ItemIndex].idHorario = ddlHorario.SelectedValue.ToInt32();

            this.DataBindColaboradoresGrade();
            //dtlEscala.DataSource = this.Colaboradores;
            //dtlEscala.DataBind();
        }

        protected void ddlDataPagarFolga_SelectedIndexChanged(object sender, EventArgs e)
        {
            var ddlDataPagarFolga = (DropDownList)sender;
            //var dtlEscalaItem = (DataListItem)ddlDataPagarFolga.NamingContainer;
            //this.Colaboradores[dtlEscalaItem.ItemIndex].dtDataPagarFolga = ddlDataPagarFolga.SelectedValue;

            int itemIndex = ((DataListItem)((DropDownList)sender).NamingContainer).ItemIndex;
            if (this.paginacaoView.Count > 0)
                if (this.paginacaoView[0] != null)
                    itemIndex = itemIndex + (this.paginacaoView[0].qtdRegistroPagina * this.paginacaoView[0].CurrentPageIndex);

            this.Colaboradores[itemIndex].dtDataPagarFolga = ddlDataPagarFolga.SelectedValue;

            this.DataBindColaboradoresGrade();
            //dtlEscala.DataSource = this.Colaboradores;
            //dtlEscala.DataBind();
        }

        protected void chkFolga_CheckedChanged(object sender, EventArgs e)
        {
            int itemIndex = ((DataListItem)((CheckBox)sender).NamingContainer).ItemIndex;
            if (this.paginacaoView.Count > 0)
                if (this.paginacaoView[0] != null)
                    itemIndex = itemIndex + (this.paginacaoView[0].qtdRegistroPagina * this.paginacaoView[0].CurrentPageIndex);

            this.Colaboradores[itemIndex].blnFolga = ((CheckBox)sender).Checked;

            //this.Colaboradores[((DataListItem)((CheckBox)sender).NamingContainer).ItemIndex].blnFolga = ((CheckBox)sender).Checked;

            this.DataBindColaboradoresGrade();
            //dtlEscala.DataSource = this.Colaboradores;
            //dtlEscala.DataBind();
        }

        protected void chkHExtra_CheckedChanged(object sender, EventArgs e)
        {
            int itemIndex = ((DataListItem)((CheckBox)sender).NamingContainer).ItemIndex;
            if (this.paginacaoView.Count > 0)
                if (this.paginacaoView[0] != null)
                    itemIndex = itemIndex + (this.paginacaoView[0].qtdRegistroPagina * this.paginacaoView[0].CurrentPageIndex);

            this.Colaboradores[itemIndex].blnHExtra = ((CheckBox)sender).Checked;

            //this.Colaboradores[((DataListItem)((CheckBox)sender).NamingContainer).ItemIndex].blnHExtra = ((CheckBox)sender).Checked;

            this.DataBindColaboradoresGrade();
            //dtlEscala.DataSource = this.Colaboradores;
            //dtlEscala.DataBind();
        }

        #region Lista Colaboradores

        /// <summary>
        /// Preenche a Lista de Colaboradores.
        /// </summary>
        /// <param name="parrColaboradores">Array com os Id dos Colaboradores</param>
        /// <history>
        ///     [cmarchi] created 7/1/2010
        /// </history>
        private void PopularListaColaboradores(Collection<Colaborador> pcolColaboradores)
        {
            ColaboradorGradeHorario limColaborador;
            this.Colaboradores = new List<ColaboradorGradeHorario>();

            if (Session["CodigosColaboradores"] != null)
            {
                var listTemp = new List<Colaborador>(pcolColaboradores);

                pcolColaboradores = new Collection<Colaborador>();

                foreach (var IdColaborador in Session["CodigosColaboradores"].ToString().Split(','))
                {
                    pcolColaboradores.Add(listTemp.Find(m => m.IdColaborador == IdColaborador.ToInt32()));
                    listTemp.RemoveAt(listTemp.FindIndex(m => m.IdColaborador == IdColaborador.ToInt32()));
                }
            }

            //Remove os colaboradores que já tem escala para o periodo
            if (!this.idPeriodoSelecionado.Equals("0") && !this.gblnEdicao)
            {
                var objBLEscala = new BLEscala();
                var tempIdColaboradores = objBLEscala.VerificarDataColaboradorTroca(Convert.ToDateTime(this.idPeriodoSelecionado), null, gobjEscalacao.IdEscalaDepartamental);

                var listTemp = new List<Colaborador>(pcolColaboradores);

                foreach (var IdColaborador in tempIdColaboradores)
                {
                    listTemp.RemoveAt(listTemp.FindIndex(m => m.IdColaborador == IdColaborador));
                }

                pcolColaboradores = new Collection<Colaborador>(listTemp);
            }

            foreach (Colaborador objColaborador in pcolColaboradores)
            {
                limColaborador = new ColaboradorGradeHorario();
                limColaborador.Nome = objColaborador.NomeColaborador;
                limColaborador.CodigoColaborador = objColaborador.CodigoColaborador;
                limColaborador.idColaborador = objColaborador.IdColaborador.ToString();

                if (this.gblnEdicao)
                    limColaborador.dtData = this.PeriodoSelecionado;

                this.Colaboradores.Add(limColaborador);
            }

            if (this.gblnEdicao)
            {
                GetHorarioColaboradores(this.IdEscalacao);
            }

            this.GetJornadaColaboradores(this.IdEscalacao);
        }
        #endregion

        #region PeriodoSelecionado
        /// <summary>
        ///     Propriedade Editar que verifica se está ou não editar
        /// </summary>
        /// <history>
        ///     [cmarchi] created 5/1/2010
        ///     [cfrancisco] modify 10/05/2011 15:01
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

        #region IdEscalaDepartamental
        /// <summary>
        ///     Propriedade Id_EscalaDepartamental utilizada para Editar uma Escala Departamental
        /// </summary>
        /// <history>
        ///     [cmarchi] created 5/1/2009 
        ///     [cfrancisco] modify 10/05/2011 15:01
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

        #region Property
        /// <summary> 
        ///     Propriedade blnApresentouMensagem
        /// </summary> 
        /// <history> 
        ///     [haguiar] 26/10/2010 Created
        ///     [cfrancisco] modify 10/05/2011 15:01
        /// </history> 
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


        /// <summary> 
        ///     Propriedade IdFilial do Usuario Logado
        /// </summary> 
        /// <history> 
        ///     [haguiar] 26/10/2010 Created
        ///     [cfrancisco] modify 10/05/2011 15:01
        /// </history> 
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

        /// <summary> 
        ///     Propriedade IdRegional do Usuario Logado
        /// </summary> 
        /// <history> 
        ///     [haguiar] 26/10/2010 Created
        ///     [cfrancisco] modify 10/05/2011 15:01
        /// </history> 
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

        #region Popular Combos
        /// <summary>
        ///     Popula Combos
        /// </summary>
        /// <param name="penmTipoPainel">Tipo do Painel</param>
        /// <history>
        ///     [cmarchi] created 7/1/2010
        ///     [cmarchi] modify 9/2/2010
        ///     [haguiar] modify 26/10/2010
        ///     [cfrancisco] modify 10/05/2011 15:01
        /// </history>
        private void PopularCombos()
        {
            BLEscalaDepartamental objBLEscalaDepartamental = new BLEscalaDepartamental();
            EscalaDepartamental objEscalaDepartamental = null;

            try
            {
                objEscalaDepartamental = objBLEscalaDepartamental.Obter(Convert.ToInt32(this.ddlEscalaDepartamental.SelectedValue), true, 9);

                //Popula lista de horarios
                this.ListEscalaHorarios = new List<HorarioEscala>(objEscalaDepartamental.HorariosEscala);

                this.ListEscalaHorarios.Insert(0, new HorarioEscala { IdHorario = "Selecione" });

                this.ListEscalaHorarios.Add(new HorarioEscala { CodLegado = 95, IdJornada = 2, IdHorario = "08 às 09 flex", IdEscala = 86 });

                //Popula lista jornada
                this.PopularListaJornadas();

                if (objEscalaDepartamental != null && this.ddlEscalaDepartamental.SelectedIndex != 0)
                {
                    //grava o id da escala
                    this.IdEscalaDepartamental = objEscalaDepartamental.IdEscalaDpto;
                    this.PopularPeriodo(ref this.ddlPeriodo, this.gobjEscalacao.DataInicioPeriodo, this.gobjEscalacao.DataFinalPeriodo);
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
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
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
        ///     [cfrancisco] modify 10/05/2011 15:01
        ///</history>
        protected void PopularPeriodo(ref DropDownList pddlPeriodo, DateTime dtPeriodoInicio, DateTime dtPeriodoFinal)
        {
            try
            {
                pddlPeriodo.Items.Clear();

                var dateBase = DateTime.Now.AddDays(1) > dtPeriodoInicio ? DateTime.Now.AddDays(1) : dtPeriodoInicio;

                TimeSpan diff = dtPeriodoFinal.Subtract(dateBase);

                //int qtdDias = 

                for (int i = 0; i <= (diff.Days + 1); i++)
                {
                    ListItem ltmPeridoRonda = new ListItem(string.Format("{0:dd/MM/yyyy} {1}", dateBase.AddDays(i), dtfi.GetDayName(dateBase.AddDays(i).DayOfWeek).UppercaseFirst()), dateBase.AddDays(i).ToString("dd/MM/yyyy"));
                    pddlPeriodo.Items.Add(ltmPeridoRonda);
                }

                BLUtilitarios.InseriMensagemDropDownList(ref pddlPeriodo, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0);

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

        #region IdEscalacao
        /// <summary>
        ///     Propriedade Id_Escalacao utilizada para Editar uma Escalacao
        /// </summary>
        /// <history>
        ///     [cmarchi] created 13/1/2010
        ///     [cfrancisco] modify 10/05/2011 15:01
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

        private int IdEscalacaoTroca
        {
            get
            {
                if (this.ViewState["vsIdEscalacaoTroca"] == null)
                {
                    this.ViewState.Add("vsIdEscalacaoTroca", 0);
                }

                return Convert.ToInt32(this.ViewState["vsIdEscalacaoTroca"]);
            }
            set
            {
                this.ViewState.Add("vsIdEscalacaoTroca", value);
            }
        }

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

                this.PopularCombos();
            }
        }
        #endregion

        #region PopularListaJornadas
        /// <summary>
        /// Preenche a Lista de Jornadas.
        /// </summary>
        /// <history>
        ///     [cfrancisco] created 13/1/2010
        /// </history>
        private void PopularListaJornadas()
        {
            BLJornada objBLJornada = new BLJornada();
            try
            {
                ListJornadas = new List<Jornada>(objBLJornada.ListarJornadas());

                ListJornadas.Insert(0, new Jornada { DescricaoJornada = "Selecione", IdJornada = 0 });
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

        public void dtlEscala_ItemDataBound(Object Sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var ddlHorario = (DropDownList)e.Item.FindControl("ddlHorario");
                var ddlDataPagarFolga = (DropDownList)e.Item.FindControl("ddlDataPagarFolga");
                var ddlHorarioPFolga = (DropDownList)e.Item.FindControl("ddlHorarioPFolga");
                var dtlEscalaDataItem = (ColaboradorGradeHorario)e.Item.DataItem;

                ddlHorario.SelectedValue = dtlEscalaDataItem.idHorario.ToString();
                if (dtlEscalaDataItem.dtDataPagarFolga != null)
                {
                    ddlDataPagarFolga.SelectedValue = dtlEscalaDataItem.dtDataPagarFolga.ToString();
                }
                ddlHorarioPFolga.SelectedValue = dtlEscalaDataItem.idHorarioPagarFolga.ToString();

                if (dtlEscalaDataItem.idHorario > 0)
                {

                }


            }

        }

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

        public List<Jornada> GetHorarioEscala()
        {
            return this.ListJornadas;
        }

        public List<HorarioEscala> GetEscalaHorariosByJornada(object DataItem)
        {
            return GetEscalaHorariosByJornada(((ColaboradorGradeHorario)DataItem).idJornada);
        }

        public ListItemCollection GetDatasPagarFolgaByColaboradores(object DataItem)
        {
            ListItemCollection items = new ListItemCollection();
            items.Add(new ListItem("Selecione", "0"));

            if (((ColaboradorGradeHorario)DataItem).blnFolga)
            {
                var objBLEscala = new BLEscala();

                var datas = objBLEscala.ObterDatasFolgaColaborador(((ColaboradorGradeHorario)DataItem).idColaborador.ToInt32(), Convert.ToDateTime(((ColaboradorGradeHorario)DataItem).dtData), gobjEscalacao.DataFinalPeriodo, gobjEscalacao.IdEscalaDepartamental);

                if (datas.Count <= 0)
                {
                    if (((ColaboradorGradeHorario)DataItem).dtDataPagarFolga != null)
                        datas = objBLEscala.ObterDatasFolgaColaborador(((ColaboradorGradeHorario)DataItem).idColaborador.ToInt32(), Convert.ToDateTime(((ColaboradorGradeHorario)DataItem).dtData), Convert.ToDateTime(((ColaboradorGradeHorario)DataItem).dtDataPagarFolga).AddDays(14), gobjEscalacao.IdEscalaDepartamental);
                    else
                        datas = objBLEscala.ObterDatasFolgaColaborador(((ColaboradorGradeHorario)DataItem).idColaborador.ToInt32(), Convert.ToDateTime(((ColaboradorGradeHorario)DataItem).dtData), Convert.ToDateTime(((ColaboradorGradeHorario)DataItem).dtData).AddDays(14), gobjEscalacao.IdEscalaDepartamental);
                }

                foreach (var data in datas)
                {
                    items.Add(new ListItem(string.Format("{0:dd/MM/yyyy} {1}", data, dtfi.GetAbbreviatedDayName(data.DayOfWeek).UppercaseFirst()), data.ToString("dd/MM/yyyy")));
                }
            }
            return items;
        }

        public List<HorarioEscala> GetEscalaHorariosByJornada(int _idJornada)
        {
            return this.ListEscalaHorarios.FindAll(m => m.IdJornada == _idJornada || m.IdJornada == 0);
        }


        protected void btnAvancar_Click(object sender, EventArgs e)
        {
            BLEscala objBLEscala = new BLEscala();
            var blnDeletarHorariosAntigos = false;
            //var blnEditar = false;
            BindCadastro(Enums.TipoTransacao.CarregarDados);

            var colJornadaColaboradoresGrade = GetJornadaColaboradoresGrade(0);

            if (colJornadaColaboradoresGrade.Count > 0)
            {

                if (this.IdEscalacaoTroca <= 0)
                {
                    this.gobjEscalacao.IdEscalacao = objBLEscala.Inserir(this.gobjEscalacao, GetColaboradoresGrade());
                }
                else
                {
                    objBLEscala.Editar(this.gobjEscalacao, GetColaboradoresGrade());
                    this.gobjEscalacao.IdEscalacao = this.IdEscalacaoTroca;
                    blnDeletarHorariosAntigos = true;
                }

                BLJornada objBLJornada = new BLJornada();
                objBLJornada.Inserir(GetJornadaColaboradoresGrade(this.gobjEscalacao.IdEscalacao));

                //if (!blnEditar)

                objBLEscala.InserirHorariosColaboradoresEscala(GetHorarioColaboradoresGrade(this.gobjEscalacao.IdEscalacao, this.gobjEscalacao.DataInicioPeriodo, this.gobjEscalacao.DataFinalPeriodo), blnDeletarHorariosAntigos);
                //else
                //{
                //    objBLEscala.AlterarHorariosColaboradoresEscala(GetHorarioColaboradoresGrade(this.gobjEscalacao.IdEscalacao, this.gobjEscalacao.DataInicioPeriodo, this.gobjEscalacao.DataFinalPeriodo));
                //}

                Session["CodigosColaboradores"] = null;
                Session.Remove("CodigosColaboradores");
                string strParametro = BLEncriptacao.EncQueryStr("TrocaHor," + this.gobjEscalacao.IdEscalacao.ToString() + "," + this.IdEscalacao.ToString());
                this.Response.Redirect("CadEscalaFinalizacao.aspx?mod=" + strParametro);
            }
            else
            {
                RadAjaxPanelCadastro.Alert("Nenhuma troca de horário foi definada!");
            }
        }

        protected void DataBindColaboradoresGrade()
        {
            var pgitems = new PagedDataSource();

            pgitems.DataSource = this.Colaboradores;

            pgitems.AllowPaging = true;
            pgitems.PageSize = 30;

            if (pgitems.PageCount > 1)
            {
                //this.paginacaoView = new List<PaginacaoView>();
                if (this.paginacaoView.Count == 0)
                {
                    this.paginacaoView.Add(new PaginacaoView { CurrentPageIndex = 0, PageCount = pgitems.PageCount, qtdRegistroPagina = pgitems.PageSize });
                    pgitems.CurrentPageIndex = 0;
                }
                else
                {
                    pgitems.CurrentPageIndex = this.paginacaoView[0].CurrentPageIndex;
                    this.paginacaoView[0].PageCount = pgitems.PageCount;
                    this.paginacaoView[0].qtdRegistroPagina = pgitems.PageSize;
                }

                repeaterPager.DataSource = this.paginacaoView;
                repeaterPager.DataBind();
                repeaterPager.Visible = true;
            }
            else
            {
                repeaterPager.Visible = false;
            }

            dtlEscala.DataSource = pgitems;
            dtlEscala.DataBind();
        }

        protected void repeaterPager_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            //PageNumber = Convert.ToInt32(e.CommandArgument) - 1;
            var pgView = this.paginacaoView[0];

            if (e.CommandName.Trim().Equals("Page"))
            {
                switch (e.CommandArgument.ToString().Trim())
                {
                    case "First":
                        pgView.CurrentPageIndex = 0;
                        break;
                    case "Prev":
                        if ((pgView.CurrentPageIndex - 1) >= 0)
                            pgView.CurrentPageIndex = pgView.CurrentPageIndex - 1;
                        break;
                    case "Next":
                        if ((pgView.CurrentPageIndex + 1) < (pgView.PageCount))
                            pgView.CurrentPageIndex = pgView.CurrentPageIndex + 1;
                        break;
                    case "Last":
                        pgView.CurrentPageIndex = pgView.PageCount - 1;
                        break;

                    case "IrPagina":
                        var PageIr = ((TextBox)e.Item.FindControl("FWMascara2")).Text.ToInt32() - 1;

                        if (PageIr < pgView.PageCount && PageIr >= 0)
                            pgView.CurrentPageIndex = PageIr;
                        else if (PageIr >= pgView.PageCount)
                            pgView.CurrentPageIndex = pgView.PageCount - 1;
                        else if (PageIr < 0)
                            pgView.CurrentPageIndex = 0;

                        break;
                }
            }

            this.DataBindColaboradoresGrade();
        }

        private Collection<HorarioColaboradores> GetHorarioColaboradoresGrade(int _idEscalacao, DateTime dataInicio, DateTime dataFim)
        {
            var colGrade = new Collection<HorarioColaboradores>();
            var datasSemanas = new Collection<DateTime>();
            var datasSabado = new Collection<DateTime>();
            var datasDomingo = new Collection<DateTime>();
            TimeSpan diff = dataFim.Subtract(dataInicio);
            var codigosColab = new List<string>();
            var nomesColab = new List<string>();
            datasSemanas.Add(dataInicio);

            for (int i = 0; i < diff.Days; i++)
            {
                if (dataInicio.AddDays(i).DayOfWeek == DayOfWeek.Saturday)
                    datasSabado.Add(dataInicio.AddDays(i));
                else if (dataInicio.AddDays(i).DayOfWeek == DayOfWeek.Sunday)
                    datasDomingo.Add(dataInicio.AddDays(i));
            }

            var colab = new List<ColaboradorGradeHorario>();

            foreach (var itemEscalaHorario in this.ListEscalaHorarios)
            {
                if (itemEscalaHorario.IdJornada > 0)
                {
                    //Semana Normal Troca
                    colab = new List<ColaboradorGradeHorario>();
                    colab = this.Colaboradores.FindAll(m => m.idHorario == itemEscalaHorario.IdEscala && m.idHorario > 0 && m.blnHExtra == false);

                    //colab = this.Colaboradores.FindAll(m => m.idHorario == itemEscalaHorario.IdEscala && (m.idHorario > 0 || m.blnFolga == true));
                    if (colab.Count > 0)
                    {
                        codigosColab = new List<string>();
                        nomesColab = new List<string>();
                        foreach (var item in colab)
                        {
                            codigosColab.Add(item.idColaborador);
                            nomesColab.Add(item.Nome);
                        }

                        colGrade.Add(new HorarioColaboradores
                        {
                            IdEscalacao = _idEscalacao,
                            CodLegado = itemEscalaHorario.CodLegado,
                            HorarioColaborador = itemEscalaHorario.IdHorario,
                            CodigosColaboradores = string.Join(",", codigosColab.ToArray()),
                            NomesColaboradores = string.Join(",", nomesColab.ToArray()),
                            DatasColaboradores = datasSemanas
                        });
                    }

                    //Semana Normal Hora Extra
                    colab = new List<ColaboradorGradeHorario>();
                    colab = this.Colaboradores.FindAll(m => m.idHorario == itemEscalaHorario.IdEscala && m.idHorario > 0 && m.blnHExtra == true);

                    //colab = this.Colaboradores.FindAll(m => m.idHorario == itemEscalaHorario.IdEscala && (m.idHorario > 0 || m.blnFolga == true));
                    if (colab.Count > 0)
                    {
                        codigosColab = new List<string>();
                        nomesColab = new List<string>();
                        foreach (var item in colab)
                        {
                            codigosColab.Add(item.idColaborador);
                            nomesColab.Add(item.Nome);
                        }

                        colGrade.Add(new HorarioColaboradores
                        {
                            IdEscalacao = _idEscalacao,
                            CodLegado = itemEscalaHorario.CodLegado,
                            HorarioColaborador = itemEscalaHorario.IdHorario,
                            CodigosColaboradores = string.Join(",", codigosColab.ToArray()),
                            NomesColaboradores = string.Join(",", nomesColab.ToArray()),
                            DatasColaboradores = datasSemanas,
                            HoraExtra = true
                        });
                    }
                }
            }

            //Semana Normal
            colab = new List<ColaboradorGradeHorario>();
            colab = this.Colaboradores.FindAll(m => m.blnFolga == true);

            if (colab.Count > 0)
            {
                codigosColab = new List<string>();
                nomesColab = new List<string>();
                foreach (var item in colab)
                {
                    codigosColab.Add(item.idColaborador);
                    nomesColab.Add(item.Nome);
                }

                colGrade.Add(new HorarioColaboradores
                {
                    IdEscalacao = _idEscalacao,
                    CodigosColaboradores = string.Join(",", codigosColab.ToArray()),
                    NomesColaboradores = string.Join(",", nomesColab.ToArray()),
                    DatasColaboradores = datasSemanas,
                    Folga = true
                });
            }

            //Pagamento Hora Extra
            colab = new List<ColaboradorGradeHorario>();
            colab = this.Colaboradores.FindAll(m => m.blnFolga == true && m.idHorarioPagarFolga > 0);

            if (colab.Count > 0)
            {
                codigosColab = new List<string>();
                nomesColab = new List<string>();
                foreach (var item in colab)
                {

                    var dtTemp = new Collection<DateTime>();
                    dtTemp.Add(Convert.ToDateTime(item.dtDataPagarFolga));

                    var itemEscalaHorario = this.ListEscalaHorarios.Find(m => m.IdEscala == item.idHorarioPagarFolga);

                    colGrade.Add(new HorarioColaboradores
                    {
                        IdEscalacao = _idEscalacao,
                        CodLegado = itemEscalaHorario.CodLegado,
                        HorarioColaborador = itemEscalaHorario.IdHorario,
                        CodigosColaboradores = item.idColaborador,
                        NomesColaboradores = item.Nome,
                        DatasColaboradores = dtTemp
                    });

                }


            }

            return colGrade;
        }

        private Collection<JornadaColaboradores> GetJornadaColaboradoresGrade(int _idEscalacao)
        {
            var colGrade = new Collection<JornadaColaboradores>();

            foreach (var itemJornada in this.ListJornadas)
            {
                if (itemJornada.IdJornada > 0)
                {
                    var colab = this.Colaboradores.FindAll(m => m.idJornada == itemJornada.IdJornada && (m.idHorario > 0 || m.blnFolga == true));

                    if (colab.Count > 0)
                    {
                        var codigosColab = new List<string>();
                        var nomesColab = new List<string>();

                        foreach (var item in colab)
                        {
                            codigosColab.Add(item.idColaborador);
                            nomesColab.Add(item.Nome);
                        }

                        colGrade.Add(new JornadaColaboradores
                        {
                            IdEscalacao = _idEscalacao,
                            IdJornada = itemJornada.IdJornada,
                            CodigosColaboradores = string.Join(",", codigosColab.ToArray()),
                            NomesColaboradores = string.Join(",", nomesColab.ToArray())
                        });
                    }
                }
            }

            return colGrade;
        }

        private Collection<Colaborador> GetColaboradoresGrade()
        {
            var colGrade = new Collection<Colaborador>();

            foreach (var item in this.Colaboradores)
            {
                colGrade.Add(new Colaborador { IdColaborador = item.idColaborador.ToInt32(), NomeColaborador = item.Nome });
            }

            return colGrade;
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            //volta para a tela inicial de listagem de escalas
            this.Response.Redirect("CadSelecaoEscalaColaborador.aspx");
        }

        #region BindCadastro
        /// <summary>
        /// Bind Cadastro
        /// </summary>
        /// <param name="pintTipoTransacao">Tipo da Transação</param>
        /// <history>
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

                this.gobjEscalacao.DataInicioPeriodo = Convert.ToDateTime(this.ddlPeriodo.SelectedValue.Trim());
                this.gobjEscalacao.DataFinalPeriodo = Convert.ToDateTime(this.ddlPeriodo.SelectedValue.Trim());

                this.gobjEscalacao.IdTipoSolicitacao = 9;
            }
            //atribuir as informações dos objetos para tela
            else if (penmTipoTransacao == Enums.TipoTransacao.DescarregarDados)
            {
                BLEscalaDepartamental objBLEscalaDepartamental = new BLEscalaDepartamental();
                Collection<Colaborador> colColaboradores = null;

                this.Colaboradores.Clear();

                this.PopularEscalaDepartamental();

                BLUtilitarios.ConsultarValorCombo(ref this.ddlEscalaDepartamental,
                    this.IdEscalaDepartamental.ToString());

                this.PopularCombos();

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
            //inicializa as informações na tela
            else if (penmTipoTransacao == Enums.TipoTransacao.Novo)
            {
                this.ddlEscalaDepartamental.Items.Clear();
                this.ddlPeriodo.Items.Clear();

                this.Colaboradores.Clear();

                this.PopularEscalaDepartamental();
            }
        }
        #endregion

        #region DataList

        [Serializable]
        public class PaginacaoView
        {
            public int CurrentPageIndex { get; set; }

            public int PageCount { get; set; }

            public int qtdRegistroPagina { get; set; }
        }

        [Serializable]
        public class ColaboradorGradeHorario
        {
            public string Nome { get; set; }

            public string CodigoColaborador { get; set; }

            public string idColaborador { get; set; }

            public int idJornada { get; set; }

            public int idHorario { get; set; }

            public bool blnFolga { get; set; }

            public bool blnHExtra { get; set; }

            public string dtData { get; set; }

            public string dtDataPagarFolga { get; set; }

            public int idHorarioPagarFolga { get; set; }
        }

        #endregion
    }
}