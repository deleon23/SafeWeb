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
using Telerik.WebControls;

namespace SafWeb.UI.Modulos.Escala
{
    public partial class CadEscalaHorario : FWPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.VerificarHiddenColaboradores();

            InicializaScripts();

            if (!Page.IsPostBack)
            {
                PopularEscalaDepartamental();

                this.VerificarQueryTela(this.Request.QueryString["mod"]);

                this.idCompensacao = 0;
                this.idJornada = 0;
                this.idHorario = 0;
                this.idHorarioCompensacao = 0;
            }
        }

        protected void Page_PreLoad(object sender, EventArgs e)
        {
            var json = new System.Web.Script.Serialization.JavaScriptSerializer();

            if (!hdfColaboradores.Value.Equals(""))
            {
                this.Colaboradores = json.Deserialize<List<ColaboradorGradeHorario>>(hdfColaboradores.Value);
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            var json = new System.Web.Script.Serialization.JavaScriptSerializer();

            hdfListEscalaHorarios.Value = json.Serialize(this.ListEscalaHorarios);

            hdfColaboradores.Value = json.Serialize(this.Colaboradores);
        }

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

        private Escalacao gobjEscalacao;

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

        private List<ColaboradorGradeHorario> Colaboradores
        {
            get
            {

                if (ViewState["vsColaboradores"] == null)
                    ViewState["vsColaboradores"] = new List<ColaboradorGradeHorario>();
                return (List<ColaboradorGradeHorario>)ViewState["vsColaboradores"];
            }
            set
            {
                ViewState["vsColaboradores"] = value;
        }
        }


        private List<HorarioEscala> ListEscalaHorarios
        {
            get
            {
                if (ViewState["vsListEscalaHorarios"] == null)
                    ViewState["vsListEscalaHorarios"] = new List<HorarioEscala>();
                return (List<HorarioEscala>)ViewState["vsListEscalaHorarios"];
            }
            set
            {
                ViewState["vsListEscalaHorarios"] = value;

        }
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

        private void VerificarHiddenColaboradores()
        {
            string[] arrColaboradores = null;

            if (!string.IsNullOrEmpty(this.txtHiddenColaboradores.Value))
            {
                arrColaboradores = BLEncriptacao.DecQueryStr(this.txtHiddenColaboradores.Value).Split(',');

                this.PopularListaColaboradores(arrColaboradores);

                this.DataBindColaboradoresGrade();
            }
        }

        private void VerificarQueryTela(string pstrQuery)
        {
            if (!string.IsNullOrEmpty(pstrQuery))
            {
                string[] arrParametros = BLEncriptacao.DecQueryStr(pstrQuery).Split(',');

                //cadastro de horários
                if (arrParametros[0] == "CadHor")
                {
                    int intIdEscalacao;

                    if (Int32.TryParse(arrParametros[1], out intIdEscalacao))
                    {
                        this.gblnEdicao = true;

                        this.IdEscalacao = intIdEscalacao;
                        BLEscala objBLEscala = new BLEscala();

                        objBLEscala.ExcluirStatusPendente(this.IdEscalacao);

                        this.gobjEscalacao = objBLEscala.Obter(this.IdEscalacao);

                        this.ddlEscalaDepartamental.SelectedValue = this.gobjEscalacao.IdEscalaDepartamental.ToString();
                        this.ddlEscalaDepartamental.Enabled = false;

                        BLEscalaDepartamental objBLEscalaDepartamental = new BLEscalaDepartamental();

                        this.PeriodoSelecionado = string.Format("{0:dd/MM/yyyy} à {1:dd/MM/yyyy}", this.gobjEscalacao.DataInicioPeriodo, this.gobjEscalacao.DataFinalPeriodo);

                        this.ddlPeriodo.Items.Clear();
                        BLUtilitarios.InseriMensagemDropDownList(ref this.ddlPeriodo, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0);
                        PopularPeriodo(objBLEscalaDepartamental.Obter(this.gobjEscalacao.IdEscalaDepartamental, true, 7), ref this.ddlPeriodo);

                        this.ddlPeriodo.Enabled = false;

                        this.PopularListaColaboradores(objBLEscala.ObterColaboradores(this.IdEscalacao));

                        this.PopularCombos();

                        this.GetJornadaColaboradores(this.IdEscalacao);
                        this.GetHorarioColaboradores(this.IdEscalacao);

                        this.DataBindColaboradoresGrade();

                        return;
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
                        var objEscalaHorario = this.ListEscalaHorarios.Find(m => m.IdHorario == item.HorarioColaborador && m.IdJornada == item.IdJornada);

                        if (objEscalaHorario == null)
                        {
                            objEscalaHorario = this.ListEscalaHorarios.Find(m => m.CodLegado == item.CodLegado && m.IdJornada == objColabGrade.idJornada);
                        }

                        if (objEscalaHorario != null)
                        {
                            if (item.DataColaboradores.DayOfWeek != DayOfWeek.Saturday && item.DataColaboradores.DayOfWeek != DayOfWeek.Sunday)
                            {
                                objColabGrade.idHorario = objEscalaHorario.IdEscala;
                            }
                            else
                            {
                                objColabGrade.idCompensacao = 1 + (int)item.DataColaboradores.DayOfWeek;
                                objColabGrade.idHorarioCompensacao = objEscalaHorario.IdEscala;
                            }
                            objColabGrade.FlgIniciaFolgando = item.flgIniciaFolgando;
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

            dtlEscala.DataSource = new List<ColaboradorGradeHorario>();
            dtlEscala.DataBind();

            //limpa a lista de períodos
            this.PeriodoSelecionado = string.Empty;

            this.ddlPeriodo.Items.Clear();
            BLUtilitarios.InseriMensagemDropDownList(ref this.ddlPeriodo, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0);

            this.PopularCombos();
        }
        #endregion

        protected void ddlPeriodo_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.idPeriodoSelecionado = ((DropDownList)sender).SelectedValue;
            this.PeriodoSelecionado = string.Empty;

            if (!this.idPeriodoSelecionado.Equals("0"))
            {
                try
                {
                    BLEscalaDepartamental objBLEscalaDepartamental = new BLEscalaDepartamental();
                    Collection<Colaborador> colColaboradores = null;
                    colColaboradores = objBLEscalaDepartamental.ObterColaboradores(
                                                       this.IdEscalaDepartamental);

                    //TODO Caio
                    this.PopularListaColaboradores(colColaboradores);

                    this.DataBindColaboradoresGrade();
                }
                catch (Exception ex)
                {
                    ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
                }
            }
            else
            {
                //this.PeriodoSelecionado = "";
                dtlEscala.DataSource = new List<ColaboradorGradeHorario>();
                dtlEscala.DataBind();
            }
        }

        //protected void ddlHorario_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (((DataListItem)((DropDownList)sender).NamingContainer).ItemIndex < 0)
        //    {
        //        if (((DropDownList)sender).ID.Contains("Compensacao"))
        //            this.idHorarioCompensacao = ((DropDownList)sender).SelectedValue.ToInt32();
        //        else
        //            this.idHorario = ((DropDownList)sender).SelectedValue.ToInt32();

        //        foreach (var item in this.Colaboradores)
        //        {
        //            if (((DropDownList)sender).ID.Contains("Compensacao"))
        //                item.idHorarioCompensacao = this.idHorarioCompensacao;
        //            else
        //                item.idHorario = this.idHorario;
        //        }
        //    }
        //    else
        //    {
        //        int itemIndex = ((DataListItem)((DropDownList)sender).NamingContainer).ItemIndex;
        //        if (this.paginacaoView.Count > 0)
        //            if (this.paginacaoView[0] != null)
        //                itemIndex = itemIndex + (this.paginacaoView[0].qtdRegistroPagina * this.paginacaoView[0].CurrentPageIndex);

        //        if (((DropDownList)sender).ID.Contains("Compensacao"))
        //            this.Colaboradores[itemIndex].idHorarioCompensacao = ((DropDownList)sender).SelectedValue.ToInt32();
        //        else
        //            this.Colaboradores[itemIndex].idHorario = ((DropDownList)sender).SelectedValue.ToInt32();
        //    }

        //    this.DataBindColaboradoresGrade();
        //}

        //protected void ddlJornada_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (((DataListItem)((DropDownList)sender).NamingContainer).ItemIndex < 0)
        //    {
        //        this.idJornada = ((DropDownList)sender).SelectedValue.ToInt32();

        //        foreach (var item in this.Colaboradores)
        //        {
        //            if (this.idJornada.Equals(-1))
        //            {
        //                if (item.idJornada > 0)
        //                    item.idJornada = item.idJornada.Equals(1) ? 2 : 1;
        //            }
        //            else
        //            {
        //                item.idJornada = this.idJornada;
        //            }

        //            if (!item.idJornada.Equals(1))
        //            {
        //                item.idHorarioCompensacao = 0;
        //                item.idCompensacao = 0;
        //            }
        //        }

        //        if (this.idJornada.Equals(-1))
        //            this.idJornada = 0;
        //    }
        //    else
        //    {
        //        int itemIndex = ((DataListItem)((DropDownList)sender).NamingContainer).ItemIndex;
        //        if (this.paginacaoView.Count > 0)
        //            if (this.paginacaoView[0] != null)
        //                itemIndex = itemIndex + (this.paginacaoView[0].qtdRegistroPagina * this.paginacaoView[0].CurrentPageIndex);

        //        this.Colaboradores[itemIndex].idJornada = ((DropDownList)sender).SelectedValue.ToInt32();

        //        if (this.Colaboradores[itemIndex].idJornada == 2)
        //        {
        //            this.Colaboradores[itemIndex].idHorarioCompensacao = 0;
        //            this.Colaboradores[itemIndex].idCompensacao = 0;
        //        }
        //    }

        //    this.DataBindColaboradoresGrade();
        //    //dtlEscala.DataSource = this.Colaboradores;
        //    //dtlEscala.DataBind();
        //}

        //protected void ddlListCompensacao_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (((DataListItem)((DropDownList)sender).NamingContainer).ItemIndex < 0)
        //    {
        //        this.idCompensacao = ((DropDownList)sender).SelectedValue.ToInt32();
        //        foreach (var item in this.Colaboradores)
        //        {
        //            item.idCompensacao = this.idCompensacao;
        //        }
        //    }
        //    else
        //    {
        //        int itemIndex = ((DataListItem)((DropDownList)sender).NamingContainer).ItemIndex;
        //        if (this.paginacaoView.Count > 0)
        //            if (this.paginacaoView[0] != null)
        //                itemIndex = itemIndex + (this.paginacaoView[0].qtdRegistroPagina * this.paginacaoView[0].CurrentPageIndex);

        //        this.Colaboradores[itemIndex].idCompensacao = ((DropDownList)sender).SelectedValue.ToInt32();
        //    }
        //    this.DataBindColaboradoresGrade();
        //}


        #region Lista Colaboradores

        private void PopularListaColaboradores(string[] parrColaboradores)
        {
            if (parrColaboradores != null)
            {
                BLColaborador objBLColaborador = new BLColaborador();
                Collection<Colaborador> colColaborador = null;

                try
                {
                    colColaborador = objBLColaborador.Obter(parrColaboradores);
                    ColaboradorGradeHorario limColaborador = null;

                    if (colColaborador != null)
                    {
                        foreach (Colaborador objColaborador in colColaborador)
                        {
                            limColaborador = this.Colaboradores.Find(m => m.idColaborador.Equals(objColaborador.IdColaborador.ToString()));
                            //limColaborador = this.lstColaboradores.Items.FindByValue(objColaborador.IdColaborador.ToString());

                            if (limColaborador == null)
                            {
                                limColaborador = new ColaboradorGradeHorario();
                                limColaborador.Nome = objColaborador.NomeColaborador;
                                limColaborador.CodigoColaborador = objColaborador.CodigoColaborador;
                                limColaborador.idColaborador = objColaborador.IdColaborador.ToString();

                                this.Colaboradores.Add(limColaborador);
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

                string[] arrPeriodos = this.ddlPeriodo.SelectedValue.Split('à');

                //this.gobjEscalacao.DataFinalPeriodo = Convert.ToDateTime(arrPeriodos[1].Trim());

                var tempIdColaboradores = objBLEscala.VerificarDataColaboradorEscala(Convert.ToDateTime(arrPeriodos[0].Trim()), Convert.ToDateTime(arrPeriodos[1].Trim()), null, this.IdEscalaDepartamental);

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


                this.Colaboradores.Add(limColaborador);
            }

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
                objEscalaDepartamental = objBLEscalaDepartamental.Obter(Convert.ToInt32(this.ddlEscalaDepartamental.SelectedValue), true, 7);

                //Popula lista de horarios
                this.ListEscalaHorarios = new List<HorarioEscala>(objEscalaDepartamental.HorariosEscala);

                this.ListEscalaHorarios.Insert(0, new HorarioEscala { IdHorario = "Selecione", IdEscala = 0 });

                //this.ListEscalaHorarios.Add(new HorarioEscala { CodLegado = 9997, IdJornada = 1, IdHorario = "Férias/Licença", IdEscala = 341 });
                //this.ListEscalaHorarios.Add(new HorarioEscala { CodLegado = 9997, IdJornada = 2, IdHorario = "Férias/Licença", IdEscala = 341 });

                this.ListEscalaHorarios.Add(new HorarioEscala { CodLegado = 95, IdJornada = 2, IdHorario = "08 às 09 flex", IdEscala = 86 });

                //Popula lista jornada
                this.PopularListaJornadas();

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
        protected void PopularPeriodo(EscalaDepartamental pobjEscalaDepartamental, ref DropDownList pddlPeriodo)
        {
            BLEscala objBLEscala = new BLEscala();
            Collection<string> colPeriodos = null;

            try
            {
                if (pobjEscalaDepartamental != null)
                {

                    colPeriodos = objBLEscala.ObterPeriodosNovaEscala(pobjEscalaDepartamental.ObjPeriodicidade.DescricaoPeriodicidade, pobjEscalaDepartamental.IdEscalaDpto, this.IdEscalacao);

                    Collection<string> colPeriodosDisponiveis = new Collection<string>();

                    //if (this.PeriodoSelecionado != string.Empty)
                    //{
                    //    gblnEdicao = true;
                    //}

                    colPeriodosDisponiveis = colPeriodos;

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
                ((DropDownList)e.Item.FindControl("ddlJornada")).SelectedValue = ((ColaboradorGradeHorario)e.Item.DataItem).idJornada.ToString();
                ((DropDownList)e.Item.FindControl("ddlHorario")).SelectedValue = ((ColaboradorGradeHorario)e.Item.DataItem).idHorario.ToString();

                ((DropDownList)e.Item.FindControl("ddlListCompensacao")).SelectedValue = ((ColaboradorGradeHorario)e.Item.DataItem).idCompensacao.ToString();
                ((DropDownList)e.Item.FindControl("ddlHorarioCompensacao")).SelectedValue = ((ColaboradorGradeHorario)e.Item.DataItem).idHorarioCompensacao.ToString();

                ((DropDownList)e.Item.FindControl("ddlListCompensacao")).Enabled = ((ColaboradorGradeHorario)e.Item.DataItem).idJornada.Equals(1);
                ((DropDownList)e.Item.FindControl("ddlHorarioCompensacao")).Enabled = ((ColaboradorGradeHorario)e.Item.DataItem).idJornada.Equals(1);
            }
            else if (e.Item.ItemType == ListItemType.Header)
            {
                ((DropDownList)e.Item.FindControl("ddlHorario")).DataSource = GetEscalaHorariosByJornada(this.idJornada);
                ((DropDownList)e.Item.FindControl("ddlHorario")).DataBind();

                var objDDLJornada = ((DropDownList)e.Item.FindControl("ddlJornada"));
                BLUtilitarios.InseriMensagemDropDownList(ref objDDLJornada, "Inverter", true, -1);

                ((DropDownList)e.Item.FindControl("ddlHorarioCompensacao")).DataSource = GetEscalaHorariosByJornada(this.idJornada);
                ((DropDownList)e.Item.FindControl("ddlHorarioCompensacao")).DataBind();

                ((DropDownList)e.Item.FindControl("ddlJornada")).SelectedValue = this.idJornada.ToString();
                ((DropDownList)e.Item.FindControl("ddlHorario")).SelectedValue = this.idHorario.ToString();

                ((DropDownList)e.Item.FindControl("ddlListCompensacao")).SelectedValue = this.idCompensacao.ToString();
                ((DropDownList)e.Item.FindControl("ddlHorarioCompensacao")).SelectedValue = this.idHorarioCompensacao.ToString();

                ((DropDownList)e.Item.FindControl("ddlListCompensacao")).Enabled = this.idJornada.Equals(1);
                ((DropDownList)e.Item.FindControl("ddlHorarioCompensacao")).Enabled = this.idJornada.Equals(1);

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

        public List<HorarioEscala> GetEscalaHorariosByJornada(int _idJornada)
        {
            return this.ListEscalaHorarios.FindAll(m => m.IdJornada == _idJornada || m.IdJornada == 0);
            //return this.ListEscalaHorarios;
        }


        protected void btnAvancar_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                BLEscala objBLEscala = new BLEscala();
                var blnDeletarHorariosAntigos = false;
                BindCadastro(Enums.TipoTransacao.CarregarDados);
                if (this.IdEscalacao <= 0)
                {
                    this.gobjEscalacao.IdEscalacao = objBLEscala.Inserir(this.gobjEscalacao, GetColaboradoresGrade());
                }
                else
                {
                    objBLEscala.Editar(this.gobjEscalacao, GetColaboradoresGrade());
                    this.gobjEscalacao.IdEscalacao = this.IdEscalacao;
                    blnDeletarHorariosAntigos = true;
                }

                BLJornada objBLJornada = new BLJornada();
                objBLJornada.Inserir(GetJornadaColaboradoresGrade(this.gobjEscalacao.IdEscalacao));

                objBLEscala.InserirHorariosColaboradoresEscala(
                    GetHorarioColaboradoresGrade(this.gobjEscalacao.IdEscalacao, this.gobjEscalacao.DataInicioPeriodo, this.gobjEscalacao.DataFinalPeriodo/*, true, true*/),
                    blnDeletarHorariosAntigos
                );

                Session["CodigosColaboradores"] = null;
                Session.Remove("CodigosColaboradores");

                string strParametro = BLEncriptacao.EncQueryStr("CadHor," + this.gobjEscalacao.IdEscalacao.ToString());
                this.Response.Redirect("CadEscalaFinalizacao.aspx?mod=" + strParametro);
            }
        }

        private Collection<HorarioColaboradores> GetHorarioColaboradoresGrade(int _idEscalacao, DateTime dataInicio, DateTime dataFim/*, bool blnSemana, bool blnCompensacao*/)
        {
            var colGrade = new Collection<HorarioColaboradores>();
            var datasSemanas = new Collection<DateTime>();
            var datasSabado = new Collection<DateTime>();
            var datasDomingo = new Collection<DateTime>();
            TimeSpan diff = dataFim.Subtract(dataInicio);
            var codigosColab = new List<string>();
            var nomesColab = new List<string>();

            //this.gobjEscalacao
            var objBLFeriado = new BLFeriado();

            var listDatasFeriados = objBLFeriado.ObterDatas(null, BLAcesso.ObterUsuario().CodigoFilial, dataInicio, dataFim);

            for (int i = 0; i <= diff.Days; i++)
            {
                if (!listDatasFeriados.Contains(dataInicio.AddDays(i)))
                {
                    if (dataInicio.AddDays(i).DayOfWeek == DayOfWeek.Saturday)
                        datasSabado.Add(dataInicio.AddDays(i));
                    else if (dataInicio.AddDays(i).DayOfWeek == DayOfWeek.Sunday)
                        datasDomingo.Add(dataInicio.AddDays(i));
                    else
                    {
                        //Pegar o primeiro dia da semana no periodo para criação dos horarios semanais
                        if (datasSemanas.Count == 0)
                            datasSemanas.Add(dataInicio.AddDays(i));
                    }
                }
            }

            var colab = new List<ColaboradorGradeHorario>();

            foreach (var itemEscalaHorario in this.ListEscalaHorarios)
            {
                if (itemEscalaHorario.IdJornada > 0)
                {
                    //Caso a jornada diferente de 3 (diferente de 12x36)
                    if (itemEscalaHorario.IdJornada != 3)
                    {
                        //Semana Normal
                        colab = new List<ColaboradorGradeHorario>();
                        colab = this.Colaboradores.FindAll(m => m.idHorario == itemEscalaHorario.IdEscala && m.idJornada == itemEscalaHorario.IdJornada);
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
                                HorarioFlex = itemEscalaHorario.CodLegado.Equals(95)
                            });
                        }

                        //Sabado
                        colab = new List<ColaboradorGradeHorario>();
                        colab = this.Colaboradores.FindAll(m => m.idHorarioCompensacao == itemEscalaHorario.IdEscala && m.idCompensacao == 7 && m.idJornada == itemEscalaHorario.IdJornada);

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
                                DatasColaboradores = datasSabado,
                                HorarioFlex = itemEscalaHorario.CodLegado.Equals(95)
                            });
                        }

                        //Domingo
                        colab = new List<ColaboradorGradeHorario>();
                        colab = this.Colaboradores.FindAll(m => m.idHorarioCompensacao == itemEscalaHorario.IdEscala && m.idCompensacao == 1 && m.idJornada == itemEscalaHorario.IdJornada);

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
                                DatasColaboradores = datasDomingo,
                                HorarioFlex = itemEscalaHorario.CodLegado.Equals(95)
                            });
                        }
                    }
                    else
                    {
                        //Colaboradores que começarão a escala em serviço
                        colab = new List<ColaboradorGradeHorario>();
                        colab = this.Colaboradores.FindAll(m =>
                                m.idHorario == itemEscalaHorario.IdEscala &&
                                m.idJornada == itemEscalaHorario.IdJornada &&
                                m.FlgIniciaFolgando == false
                            );
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
                                HorarioFlex = itemEscalaHorario.CodLegado.Equals(95),
                                FlgIniciaFolgando = false
                            });
            }


                        //Colaboradores que começarão a escala em folga
                        colab = new List<ColaboradorGradeHorario>();
                        colab = this.Colaboradores.FindAll(m =>
                                m.idHorario == itemEscalaHorario.IdEscala &&
                                m.idJornada == itemEscalaHorario.IdJornada &&
                                m.FlgIniciaFolgando == true
                            );
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
                                HorarioFlex = itemEscalaHorario.CodLegado.Equals(95),
                                FlgIniciaFolgando = true
                            });
                        }
                    }
                }
            }

            if (listDatasFeriados.Count > 0)
            {
                if (this.Colaboradores.Count > 0)
                {
                    codigosColab = new List<string>();
                    nomesColab = new List<string>();

                    colab = new List<ColaboradorGradeHorario>();
                    colab = this.Colaboradores.FindAll(m => m.idJornada != 3);

                    foreach (var item in colab)
                    {
                        codigosColab.Add(item.idColaborador);
                        nomesColab.Add(item.Nome);
                    }
                    colGrade.Add(new HorarioColaboradores
                    {
                        IdEscalacao = _idEscalacao,
                        CodLegado = 9997,
                        CodigosColaboradores = string.Join(",", codigosColab.ToArray()),
                        NomesColaboradores = string.Join(",", nomesColab.ToArray()),
                        DatasColaboradores = new Collection<DateTime>(listDatasFeriados),
                        Folga = true
                    });
                }
            }

            return colGrade;
        }



        private Collection<HorarioColaboradores> GetHorarioColaboradoGrade(int _idEscalacao, DateTime dataInicio, DateTime dataFim, string idColaborador, bool blnSemana, bool blnCompensacao)
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
                    if (blnSemana)
                    {
                        //Semana Normal
                        colab = new List<ColaboradorGradeHorario>();
                        colab = this.Colaboradores.FindAll(m => m.idHorario == itemEscalaHorario.IdEscala && m.idColaborador == idColaborador);
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
                    }

                    if (blnCompensacao)
                    {
                        //Sabado
                        colab = new List<ColaboradorGradeHorario>();
                        colab = this.Colaboradores.FindAll(m => m.idHorarioCompensacao == itemEscalaHorario.IdEscala && m.idCompensacao == 7 && m.idColaborador == idColaborador);

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
                                DatasColaboradores = datasSabado
                            });
                        }

                        //Domingo
                        colab = new List<ColaboradorGradeHorario>();
                        colab = this.Colaboradores.FindAll(m => m.idHorarioCompensacao == itemEscalaHorario.IdEscala && m.idCompensacao == 1 && m.idColaborador == idColaborador);

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
                                DatasColaboradores = datasDomingo
                            });
                        }
                    }
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
                    var colab = this.Colaboradores.FindAll(m => m.idJornada == itemJornada.IdJornada);

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
            Session["CodigosColaboradores"] = null;
            //volta para a tela inicial de listagem de escalas
            this.Response.Redirect("CadSelecaoEscalaColaborador.aspx");
        }

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

                //this.ObterListaColaboradores();

                string[] arrPeriodos = this.ddlPeriodo.SelectedValue.Split('à');

                this.gobjEscalacao.DataInicioPeriodo = Convert.ToDateTime(arrPeriodos[0].Trim());
                this.gobjEscalacao.DataFinalPeriodo = Convert.ToDateTime(arrPeriodos[1].Trim());

                //if (this.BlnEditar)
                //{
                //    this.gobjEscalacao.IdEscalacao = this.IdEscalacao;
                //}
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
                //this.ddlRegional.Items.Clear();
                //this.ddlFilial.Items.Clear();
                this.ddlPeriodo.Items.Clear();

                this.Colaboradores.Clear();

                //BLUtilitarios.InseriMensagemDropDownList(ref this.ddlRegional, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0);
                //BLUtilitarios.InseriMensagemDropDownList(ref this.ddlFilial, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0);
                //BLUtilitarios.InseriMensagemDropDownList(ref this.ddlPeriodo, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0);

                this.PopularEscalaDepartamental();
            }
        }
        #endregion

        protected void imgbRemover_Click(object sender, ImageClickEventArgs e)
        {
            var objColaborador = this.Colaboradores[((DataListItem)((ImageButton)sender).NamingContainer).ItemIndex];

            BindCadastro(Enums.TipoTransacao.CarregarDados);

            var objEscalaDepartamental = new BLEscalaDepartamental();

            if (objEscalaDepartamental.ExcluirColaborador(objColaborador.idColaborador.ToInt32(), this.gobjEscalacao.IdEscalaDepartamental, this.IdEscalacao))
                this.Colaboradores.Remove(objColaborador);

            this.DataBindColaboradoresGrade();
            //dtlEscala.DataSource = this.Colaboradores;
            //dtlEscala.DataBind();
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

        protected void ValidaGrade(object sender, ServerValidateEventArgs args)
        {
            args.IsValid = true;

            if (this.Colaboradores.Exists(m => m.idJornada == 0))
            {
                args.IsValid = false;
                return;
            }

            if (this.Colaboradores.Exists(m => m.idHorario == 0))
            {
                args.IsValid = false;
                return;
            }

            if (this.Colaboradores.Exists(m => m.idJornada == 1 && m.idCompensacao == 0))
            {
                args.IsValid = false;
                return;
            }

            if (this.Colaboradores.Exists(m => m.idJornada == 1 && m.idHorarioCompensacao == 0))
            {
                args.IsValid = false;
                return;
            }
        }

        protected void btnAdicionar_Click(object sender, ImageClickEventArgs e)
        {
            this.RadWindowCadastroEscala();
        }

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
            [System.ComponentModel.Localizable(true)]
            [System.ComponentModel.DefaultValue("")]
            public string Nome { get; set; }

            [System.ComponentModel.Localizable(true)]
            [System.ComponentModel.DefaultValue("")]
            public string CodigoColaborador { get; set; }

            [System.ComponentModel.Localizable(true)]
            [System.ComponentModel.DefaultValue("")]
            public string idColaborador { get; set; }

            [System.ComponentModel.Localizable(true)]
            [System.ComponentModel.DefaultValue(0)]
            public int idJornada { get; set; }

            [System.ComponentModel.Localizable(true)]
            [System.ComponentModel.DefaultValue(0)]
            public int idHorario { get; set; }

            [System.ComponentModel.Localizable(true)]
            [System.ComponentModel.DefaultValue(0)]
            public int idHorarioCompensacao { get; set; }

            [System.ComponentModel.Localizable(true)]
            [System.ComponentModel.DefaultValue(0)]
            public int idCompensacao { get; set; }

            [System.ComponentModel.Localizable(true)]
            [System.ComponentModel.DefaultValue(0)]
            public bool FlgIniciaFolgando { get; set; }
        }

        #endregion


    }
}