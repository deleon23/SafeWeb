using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using FrameWork.Model.Utilitarios;
using FrameWork.BusinessLayer.Utilitarios;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using SafWeb.BusinessLayer.Escala;
using SafWeb.Model.Escala;
using SafWeb.Model.Colaborador;
using System.Text;
using FrameWork.Model.Idioma;
using FrameWork.BusinessLayer.Idioma;
using SafWeb.BusinessLayer.Colaborador;

namespace SafWeb.UI.Modulos.Escala
{
    public partial class CadEscalaHorarioColaborador : FWPage
    {
        //objeto para inserir troca de horários
        //[haguiar_2] created 24/11/2010

        private Escalacao gobjEscalacaoTrocaHorario;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.VerificarHiddenColaboradores();

            if (!this.Page.IsPostBack)
            {
                this.VerificarQueryTela(this.Request.QueryString["mod"]);                 
            }
        }               

        #region AdicionarColaboradorLista
        /// <summary>
        /// Adiciona os colaboradores na lista de colaboradores.
        /// </summary>
        /// <history>
        ///     [cmarchi] created 18/1/2010
        /// </history>
        private void AdicionarColaboradorLista(string[] arrIdColaboradores)
        {
            BLColaborador objBLColaborador = new BLColaborador();
            Collection<Colaborador> colColaborador = null;

            //obtém os colaboradores
            colColaborador = objBLColaborador.Obter(arrIdColaboradores);

            //this.lstColaboradores.Items.Clear();

            foreach (Colaborador objColaborador in colColaborador)
            {
                if (this.lstColaboradores.Items.FindByValue(objColaborador.IdColaborador.ToString()) == null)
                {
                    ListItem ltmColaborador = new ListItem(objColaborador.NomeColaborador + " - " +
                                                           objColaborador.CodigoColaborador,
                                                           objColaborador.IdColaborador.ToString());

                    this.lstColaboradores.Items.Add(ltmColaborador);
                }
            }
        }
        #endregion

        #region Armazenar
        /// <summary>
        /// Armazena as Datas Selecionadas pelo usuário.
        /// </summary>
        /// <history>
        ///     [cmarchi] created 20/1/2010
        ///     [haguiar_2] modify 25/11/2010
        /// </history>
        private void ArmazenarDatasSelecionadas()
        {
            int intTotal = this.clrPeriodoEscala.SelectedDates.Count;

            if (!this.BlnEditar)
            {
                DateTime[] arrDatas = new DateTime[intTotal];
                this.DatasSelecionadas = null;

                for (int i = 0; i < intTotal; i++)
                {
                    if (!Datas.Contains(this.clrPeriodoEscala.SelectedDates[i].Date))
                    {
                        this.Datas.Add(this.clrPeriodoEscala.SelectedDates[i].Date);
                        arrDatas[i] = this.clrPeriodoEscala.SelectedDates[i].Date;
                    }
                }
                
                //armazenas as datas selecionadas
                this.DatasSelecionadas = arrDatas;
                this.clrPeriodoEscala.SelectedDates.Clear();  
            }
            else if (intTotal > 0)
            {
                int intPosicao = this.Datas.IndexOf(this.clrPeriodoEscala.SelectedDates[0].Date);

                if (intPosicao < 0)
                {
                    if (!Datas.Contains(this.clrPeriodoEscala.SelectedDates[0].Date))
                        this.Datas.Add(this.clrPeriodoEscala.SelectedDates[0].Date);
                }
                else


                    if (!Datas.Contains(this.clrPeriodoEscala.SelectedDates[0].Date))
                        this.Datas[intPosicao] = this.clrPeriodoEscala.SelectedDates[0].Date;
                

            }
        }
        #endregion

        #region Avancar


        #region Inserir Jornada Colaboradores
        /// <summary>
        ///    Editar Jornadas dos Colaboradores.
        /// </summary>
        /// <param name="pobjJornadaColaboradores">Objeto Jornada Colaboradores</param>
        /// <history>
        ///     [cmarchi] created 13/1/2010
        /// </history>
        private void InserirJornada(ref JornadaColaboradores pobjJornadaColaboradores)
        {
                Collection<ListItem> colColaboradores = new Collection<ListItem>();
                BLJornada objBLJornada = new BLJornada();

                Collection<JornadaColaboradores> colJornadaCol = null;

                StringBuilder strCodigoColaboradores = new StringBuilder();
                StringBuilder strNomeColaboradores = new StringBuilder();

                foreach (ListItem var in this.lstColaboradores.Items)
                {
                    if (var.Selected)
                    {
                        colColaboradores.Add(var);

                        //colJornadaCol = this.JornadaColaboradores;

                        strCodigoColaboradores.Append(var.Value + ",");
                        strNomeColaboradores.Append(var.Text + ",");
                    }
                }

                strCodigoColaboradores.Remove(strCodigoColaboradores.Length - 1, 1);
                strNomeColaboradores.Remove(strNomeColaboradores.Length - 1, 1);

                pobjJornadaColaboradores.CodigosColaboradores = strCodigoColaboradores.ToString();
                pobjJornadaColaboradores.NomesColaboradores = strNomeColaboradores.ToString();

                //int intPosicaoJornada = BLJornada.IndiceJornadaColaborador(this.JornadaColaboradores,
                //                                        pobjJornadaColaboradores.IdJornada);

                //objBLJornada.InserirJornadaColaborador(ref colJornadaCol,
                //    intPosicaoJornada, pobjJornadaColaboradores);

                //remove da lista
                foreach (ListItem var in colColaboradores)
                {
                    this.lstColaboradores.Items.Remove(var);
                }

                //insere na grid
                //this.radGridJornadaColaboradores.DataSource = this.JornadaColaboradores;
                //this.radGridJornadaColaboradores.DataBind();
        }
        #endregion
        
        /// <summary>
        /// Grava os Horários dos Colaboradores e avança para a tela de aprovação de escala.
        /// </summary>
        /// <history>
        ///     [cmarchi] created 19/1/2010
        ///     [cmarchi] modify 12/2/2010
        ///     [haguiar_2] modify 24/11/2010
        ///     cria nova escala contendo a troca de horário das datas e colaboradores
        ///     [haguiar_2] modify 02/12/2010
        ///     exclui troca de horários
        /// </history>
        private void Avancar()
        {
            BLEscala objBLEscala = new BLEscala();

            try
            {
                if (!this.blnTrocaHorario)
                {
                    if (!this.BlnEditar)
                        objBLEscala.InserirHorariosColaboradoresEscala(this.HorarioColaboradores, false);
                    else
                    {
                        objBLEscala.AlterarHorariosColaboradoresEscala(this.HorarioColaboradores);
                    }
                }
                else
                {


                    //troca de horário
                    //verifica colaboradores com alteração no horário

                    Collection<Int32> IdColaboradores = new Collection<Int32>();

                    Collection<Colaborador> colColaboradoresTrocaHorario = new Collection<Colaborador>();
                    Colaborador objColaborador = null;
                    
                    foreach (HorarioColaboradores colHorario in this.HorarioColaboradores)
                    {
                        string[] arrColaboradores = colHorario.CodigosColaboradores.Split(',');

                        foreach (string col in arrColaboradores)
                        {
                            if (!IdColaboradores.Contains(Convert.ToInt32(col)))
                            {
                                IdColaboradores.Add(Convert.ToInt32(col));

                                //objeto colaborador
                                objColaborador = new Colaborador();
                                objColaborador.IdColaborador = Convert.ToInt32(col);

                                //colecao de colaboradores
                                colColaboradoresTrocaHorario.Add(objColaborador);
                            }
                        }
                    }


                    if (!this.BlnEditar && !this.BlnEditarTrocaHorario)
                    {
                        //Obtem informaçoes da escala para troca de horario
                        gobjEscalacaoTrocaHorario = objBLEscala.Obter(this.IdEscalacao);

                        gobjEscalacaoTrocaHorario.IdEscalacao = 0;
                        gobjEscalacaoTrocaHorario.IdTipoSolicitacao = 1; //Troca de horário

                        //Obtem os colaboradores da escala
                        Collection<Colaborador> colColaboradores = new Collection<Colaborador>();
                        colColaboradores = objBLEscala.ObterColaboradores(this.IdEscalacao);

                        //Inserir nova escala com a lista de todos os colaboradores, que ficarao disponíveis para posterior troca de horário
                        gobjEscalacaoTrocaHorario.IdEscalacao = objBLEscala.Inserir(gobjEscalacaoTrocaHorario, colColaboradores);

                        //Inserir jornada para escala
                        BLJornada objJornada = new BLJornada();
                        Collection<JornadaColaboradores> JornadaColaboradores = new Collection<JornadaColaboradores>();

                        JornadaColaboradores =
                            objJornada.ObterJornadaColaboradores(this.IdEscalacao);

                        this.IdEscalacao = gobjEscalacaoTrocaHorario.IdEscalacao;

                        //cria nova jornada com os colaboradores da troca de horário
                        foreach (JornadaColaboradores colJornada in JornadaColaboradores)
                        {
                            colJornada.IdEscalacao = this.IdEscalacao;
                        }

                        objJornada.Inserir(JornadaColaboradores);

                        //troca id de escalacao
                        foreach (HorarioColaboradores colHorario in this.HorarioColaboradores)
                        {
                            colHorario.IdEscalacao = this.IdEscalacao;
                        }

                        //Inserir troca de horário dos colaboradores
                        objBLEscala.InserirHorariosColaboradoresEscala(this.HorarioColaboradores, false);
                    }
                    else
                    {
                        //altera ou exclui horários
                        objBLEscala.AlterarHorariosColaboradoresEscala(this.HorarioColaboradores);

                        //incluir novos horários
                        objBLEscala.InserirHrColabTrocaHorario(this.HorarioColaboradores);
                    }
                }

                this.lstHorarios.Items.Clear();
                this.lstColaboradores.Items.Clear();

                if (!this.blnTrocaHorario)
                {
                    string strParametro = BLEncriptacao.EncQueryStr("CadHor," + this.IdEscalacao.ToString());
                    this.Response.Redirect("CadEscalaFinalizacao.aspx?mod=" + strParametro);
                }
                else
                {
                    //troca de horário
                    string strParametro = BLEncriptacao.EncQueryStr("TrocaHor," + this.IdEscalacao.ToString());
                    this.Response.Redirect("CadEscalaFinalizacao.aspx?mod=" + strParametro);
                }
                             
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }        
        #endregion

        #region Binds

        #region BindData

        /// <summary>
        /// Seleciona datas da escala no calendário
        /// </summary>
        /// <history>
        ///     [haguiar_2] create 25/11/2010
        /// </history>
        private void SelecionaDataSelecionadas()
        {
            this.clrPeriodoEscala.SpecialDays.Clear();

            foreach (DateTime data in this.Datas)
            {
                Telerik.Web.UI.RadCalendarDay day = new Telerik.Web.UI.RadCalendarDay();
                day.Date = data;
                day.IsDisabled = true;
                day.IsSelectable = false;

                this.clrPeriodoEscala.SpecialDays.Add(day);
            } 
        }

        /// <summary>
        /// Bind da Data 
        /// </summary>
        /// <param name="pintTipoTransacao"></param>
        /// <param name="pdatDataSelecionada">Data Selecionada para Edição</param>
        /// <history>
        ///     [cmarchi] created 27/11/2009
        ///     [cmarchi] modify 25/1/2010
        ///     [haguiar_2] modify 24/11/2010
        ///     Permitir selecao de datas para troca de horários
        ///     [haguiar SDM 9004] modify
        ///     troca de horário somente do dia atual ou posterior
        /// </history>
        protected void BindData(Enums.TipoTransacao pintTipoTransacao)
        {
            //atribuir as informações dos objetos para tela
            if (pintTipoTransacao == Enums.TipoTransacao.DescarregarDados)
            {
                BLEscala objBLEscala = new BLEscala();
                Escalacao objEscalacao = null;
                Collection<Colaborador> colColaboradores = null;
                           
                try
                {
                    objEscalacao = objBLEscala.Obter(this.IdEscalacao);
                    
                    if (!blnTrocaHorario)
                    {
                        this.clrPeriodoEscala.RangeMinDate = objEscalacao.DataInicioPeriodo;
                        this.clrPeriodoEscala.RangeMaxDate = objEscalacao.DataFinalPeriodo;

                        colColaboradores = objBLEscala.ObterColaboradoresNaoContemHorarioEscala(
                                                this.IdEscalacao);

                        //verifica se há colaboradores
                        if (colColaboradores.Count == 0)
                        {
                            this.Datas = objBLEscala.ObterDatas(this.IdEscalacao);

                            if (this.VerificarQuantidadeDatasPeriodo())
                                this.btnCriarEscala.Enabled = false;
                        }
                    }
                    else
                    {
                        this.clrPeriodoEscala.RangeMinDate = DateTime.Today; //objEscalacao.DataInicioPeriodo; //DateTime.Today;
                        this.clrPeriodoEscala.RangeMaxDate = objEscalacao.DataFinalPeriodo;

                        //this.Datas = objBLEscala.ObterDatas(this.IdEscalacao);

                        //obtém os colaboradores para troca de horário
                        colColaboradores = objBLEscala.ObterColaboradores(this.IdEscalacao);

                        this.btnCriarEscala.Text = "Trocar horário";
                        this.btnCriarEscala.Enabled = true;
                    }

                    
                    //inserindo as datas selecionadas
                    SelecionaDataSelecionadas();

                    /*
                    foreach (DateTime data in this.Datas)
                    {
                        Telerik.Web.UI.RadCalendarDay day = new Telerik.Web.UI.RadCalendarDay();
                        day.Date = data;
                        day.IsDisabled = true;
                        day.IsSelectable = false;

                        this.clrPeriodoEscala.SpecialDays.Add(day);
                    } 
                    */


                    //this.clrPeriodoEscala.SelectedDates.Clear();  
 
                    //verifica se está editando os horários dos colaboradores de uma data
                    if(
                        (this.BlnEditar) && 
                        (this.DataSelecionada.HasValue) && 
                        (this.Datas.IndexOf(Convert.ToDateTime(this.DataSelecionada.Value.ToShortDateString())) >= 0)  &&
                        (!blnApresentaMsgTrocaHorario)
                      )
                    {
                        int intPosicao = this.clrPeriodoEscala.SpecialDays.IndexOf(
                            Convert.ToDateTime(this.DataSelecionada.Value.ToShortDateString()));

                        if (intPosicao >= 0)
                        {
                            Telerik.Web.UI.RadDate objData = new Telerik.Web.UI.RadDate();
                            objData.Date = this.clrPeriodoEscala.SpecialDays[intPosicao].Date;

                            this.clrPeriodoEscala.SpecialDays.RemoveAt(intPosicao);

                            this.clrPeriodoEscala.SelectedDates.Add(objData);
                            
                            //nao está trocando o horário
                            if (!blnTrocaHorario)
                            {
                                this.btnCriarEscala.Text = "Editar Escala";
                                this.btnCriarEscala.Enabled = true;
                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
                }
            }
        }        
        #endregion

        #region BindHorários
        /// <summary>
        /// Bind de Horários
        /// </summary>
        /// <param name="pintTipoTransacao">Tipo de Transação</param>
        /// <param name="pblnSomenteRadGrid">Bind somente na radgrid</param>
        /// <history>
        ///     [cmarchi] created 27/11/2009
        ///     [cmarchi] modify 22/2/2010
        ///     [haguiar_2] modify 04/12/2010
        ///     incluir horário flex
        ///     [haguiar_8829] modify 06/07/2011 15:27
        ///     incluir hora extra
        ///     [haguiar] modify 16/11/2011 10:57
        ///     preencher grid de horários
        /// </history>
        protected void BindHorarios(Enums.TipoTransacao pintTipoTransacao, bool pblnSomenteRadGrid)
        {
            //atribuir as informações dos objetos para tela
            if (pintTipoTransacao == Enums.TipoTransacao.DescarregarDados)
            {
                if (!pblnSomenteRadGrid)
                {
                    this.PopularListaHorarios();

                    List<DateTime> lstData = new List<DateTime>();

                    foreach (Telerik.Web.UI.RadDate datDataSelecionada in this.clrPeriodoEscala.SelectedDates)
                    {
                        lstData.Add(Convert.ToDateTime(datDataSelecionada.Date));
                    }

                    this.lblDatSelResp.Text = BLEscala.FormatarDatasSelecionadas(lstData);

                    //verifica se está sendo editado um horário dos colaboradores a uma determinada
                    //data
                    if (this.BlnEditar && !string.IsNullOrEmpty(this.ColaboradoresSelecionados) &&
                        this.DataSelecionada.HasValue)
                    {
                        BLEscala objEscala = new BLEscala();
                        Collection<DataHorarioColaboradores> colDataHorCol = null;

                        /*variaveis para preencher o grid de horarios*/
                        HorarioColaboradores objHorarioColaboradores = new HorarioColaboradores();

                        objHorarioColaboradores.IdEscalacao = this.IdEscalacao;
                        objHorarioColaboradores.Bloqueado = false;

                        objHorarioColaboradores.DatasColaboradores = new Collection<DateTime>();
                        objHorarioColaboradores.DatasColaboradores.Add(this.DataSelecionada.Value);

                        //string strHora = this.DataSelecionada.Value.ToShortTimeString();
                        //strData = String.Format("{0:dd/MM/yyyy HH:mm}", dtDateTime);

                        string strHora = String.Format("{0:HH:mm}", this.DataSelecionada.Value);

                        this.lblDatSelResp.Text = BLEscala.FormatarDatasSelecionadas(this.DataSelecionada.Value);

                        Telerik.Web.UI.RadDate objData = new Telerik.Web.UI.RadDate(this.DataSelecionada.Value);

                        if (!blnTrocaHorario)
                            this.clrPeriodoEscala.SelectedDates.Add(objData);


                        this.PopularListaColaboradores(null);
                        //this.PopularListaColaboradores(this.ColaboradoresSelecionados.Split(','));

                        try
                        {
                            //obtém o horário dos colaboradores a uma determinada data
                            colDataHorCol = objEscala.ObterDtHorColEscalacao(this.IdEscalacao, this.DataSelecionada, true, CodLegado);

                            objHorarioColaboradores.CodigosColaboradores = colDataHorCol[0].CodigosColaboradores;
                            objHorarioColaboradores.NomesColaboradores = colDataHorCol[0].NomesColaboradores;

                            objHorarioColaboradores.CodLegado = colDataHorCol[0].CodLegado;

                            strHora = colDataHorCol[0].HorarioColaborador;

                            //verifica as flags
                            if (colDataHorCol != null && colDataHorCol.Count > 0 &&
                                (this.DataSelecionada.Value.ToShortTimeString() == "0:00" || this.DataSelecionada.Value.ToShortTimeString() == "8:00"))
                            {
                                if (!string.IsNullOrEmpty(colDataHorCol[0].Compensado))
                                {
                                    strHora = colDataHorCol[0].Compensado;
                                    objHorarioColaboradores.Compensado = true;
                                }

                                if (!string.IsNullOrEmpty(colDataHorCol[0].Licenca))
                                {
                                    strHora = colDataHorCol[0].Licenca;
                                    objHorarioColaboradores.Licenca = true;
                                }

                                if (!string.IsNullOrEmpty(colDataHorCol[0].Folga))
                                {
                                    strHora = colDataHorCol[0].Folga;
                                    objHorarioColaboradores.Folga = true;
                                }

                                if (!string.IsNullOrEmpty(colDataHorCol[0].HorarioFlex))
                                {
                                    strHora = colDataHorCol[0].HorarioFlex;
                                    objHorarioColaboradores.HorarioFlex = true;
                                }

                                if (!string.IsNullOrEmpty(colDataHorCol[0].HoraExtra))
                                {
                                    strHora = colDataHorCol[0].HoraExtra;
                                    objHorarioColaboradores.HorarioColaborador = strHora + " (Hora Extra)";
                                    objHorarioColaboradores.HoraExtra = true;
                                }
                            }

                            objHorarioColaboradores.HorarioColaborador = strHora;

                            //insere horário no grid                            
                            this.Inserir(ref objHorarioColaboradores);

                            /*
                            ListItem ltmHorario = this.lstHorarios.Items.FindByValue(strHora);

                            if (ltmHorario != null)
                                ltmHorario.Selected = true;
                            else
                                this.lstHorarios.Items[0].Selected = true;*/
                        }
                        catch (Exception ex)
                        {
                            ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
                        }
                    }
                    else
                    {
                        this.PopularListaColaboradores(null);

                        this.radGridHorariosColaboradores.DataSource = BLEscala.ObterHorariosColaboradoresNBloqueados(this.HorarioColaboradores);
                        this.radGridHorariosColaboradores.DataBind();
                    }
                }
                else
                {
                    this.radGridHorariosColaboradores.DataSource = BLEscala.ObterHorariosColaboradoresNBloqueados(this.HorarioColaboradores);
                    this.radGridHorariosColaboradores.DataBind();
                }

                /*
                this.radGridHorariosColaboradores.DataSource = BLEscala.ObterHorariosColaboradoresNBloqueados(
                                                                    this.HorarioColaboradores);
                this.radGridHorariosColaboradores.DataBind();  */
            }

        }
        #endregion   

        #region BindModel
        /// <summary>
        /// Bind Model 
        /// </summary>
        /// <param name="penmTipoTransacao">Tipo de Transação</param>
        /// <param name="blnPainelData">True - Painel data, False - Painel de Horários</param>
        /// <param name="pblnSomenteRadGrid">Bind somente na radgrid</param>
        /// <history>
        ///     [cmarchi] created 27/11/2009
        ///     [cmarchi] modify 25/1/2010
        /// </history>
        protected void BindModel(Enums.TipoTransacao penmTipoTransacao, bool blnPainelData,
            bool pblnSomenteRadGrid)
        {
            this.ControlaPaineis(blnPainelData);
            this.BindHorarios(penmTipoTransacao, pblnSomenteRadGrid);
            this.BindData(penmTipoTransacao);
        }
        #endregion
   
        #endregion

        #region Botões

        #region Avançar
        /// <summary>
        /// Botão Avança para próxima Tela
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [cmarchi] created 26/11/2009
        /// </history>
        protected void btnAvancar_Click(object sender, EventArgs e)
        {
            if (this.ValidarAvancar())
            {
                //grava as datas selecionadas
                this.ArmazenarDatasSelecionadas();

                int intResultado = this.Validar();

                switch (intResultado)
                {
                    //tudo OK
                    case 0:
                        this.Avancar();
                        break;

                    //problema com a data
                    case 1:
                        this.ControlaPaineis(true);
                        this.BindData(Enums.TipoTransacao.DescarregarDados);
                        break;

                    //problema com a data
                    case 2:
                        this.Datas.Clear();
                        break;

                    //problema com horário
                    case 3:
                        DateTime[] arrDatas = this.DatasSelecionadas;

                        //remove as datas que foram inseridas sem definir um horário para elas
                        for (int i = 0; i < arrDatas.Length; i++)
                        {
                            this.Datas.Remove(arrDatas[i]);
                        }

                        break;
                }
            }

            //if (intResultado == 0)
            //{
            //    this.Avancar();
            //}
            //else
            //{                
            //    if (intResultado == 1)
            //    {
            //        this.ControlaPaineis(true);
            //        this.BindData(Enums.TipoTransacao.DescarregarDados);             
            //    }
            //    else if (intResultado == 2)
            //    {
            //        this.Datas.Clear();
            //    }
            //}
        }
        #endregion

        //#region Cancelar
        ///// <summary>
        ///// Cancela os horários dos colaboradores
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        ///// <history>
        /////     [cmarchi] created 26/11/2009
        /////     [cmarchi] modify 17/2/2010
        ///// </history>
        //protected void btnCancelarVoltar_Click(object sender, EventArgs e)
        //{
        //    if (this.BlnEditar)
        //    {
        //        string strParametro = BLEncriptacao.EncQueryStr("CadHor," + this.IdEscalacao.ToString());
        //        this.Response.Redirect("CadEscalaFinalizacao.aspx?mod=" + strParametro);
        //    }
        //    else
        //    {
        //        this.clrPeriodoEscala.SelectedDates.Clear();
        //        this.BindModel(Enums.TipoTransacao.DescarregarDados, true, true);
        //        this.PopularListaColaboradores(null);
        //    }   
        //}        
        //#endregion

        #region Criar Escala
        /// <summary>
        /// Botão de Criar Escalar - habilita para selecionar os horarios dos colaboradores.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [cmarchi] created 26/11/2009
        ///     [cmarchi] modify 25/1/2010
        ///     [haguiar_2] modify 25/11/2010
        /// </history>
        protected void btnCriarEscala_Click(object sender, EventArgs e)
        {
            if (this.ValidarData())
            {
                if (blnTrocaHorario)
                {
                    //armazena as datas selecionadas para troca de horário

                    int intTotal = this.clrPeriodoEscala.SelectedDates.Count;

                    DateTime[] arrDatas = new DateTime[intTotal];
                    this.DatasSelecionadas = null;

                    for (int i = 0; i < intTotal; i++)
                    {
                        if (!Datas.Contains(this.clrPeriodoEscala.SelectedDates[i].Date))
                        {
                            this.Datas.Add(this.clrPeriodoEscala.SelectedDates[i].Date);
                            arrDatas[i] = this.clrPeriodoEscala.SelectedDates[i].Date;
                        }
                    }
                
                    //armazenas as datas selecionadas
                    this.DatasSelecionadas = arrDatas;
                    //this.clrPeriodoEscala.SelectedDates.Clear();  
                }
                
                this.blnApresentaMsgTrocaHorario = false;

                Collection<HorarioColaboradores> colHorariosColaboradores = null;
                colHorariosColaboradores = this.HorarioColaboradores;

                //seta os horários como bloqueados
                foreach (HorarioColaboradores var in colHorariosColaboradores)
                {
                    var.Bloqueado = true;
                }

                this.ControlaPaineis(false);

                this.BindHorarios(Enums.TipoTransacao.DescarregarDados, false);            
            }
        }
        #endregion

        #region Voltar
        /// <summary>
        /// Botão Voltar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [cmarchi] created 26/11/2009
        ///     [cmarchi] modify 22/1/2010
        ///     [haguiar_2] modify 30/11/2010
        ///     modificar a rotina de volta para a troca de horários
        /// </history>
        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            string strParametro = string.Empty;

            if (this.pnlData.Visible)
            {
                if (this.BlnEditar)
                {
                    if (!blnTrocaHorario)
                    {
                        strParametro = BLEncriptacao.EncQueryStr("CadHor," + this.IdEscalacao.ToString());
                    }
                    else
                    {
                        strParametro = BLEncriptacao.EncQueryStr("TrocaHor," + this.IdEscalacao.ToString());
                    }

                    this.Response.Redirect("CadEscalaFinalizacao.aspx?mod=" + strParametro);
                }
                else if (!blnTrocaHorario)
                {
                    strParametro = "CadHor," + this.IdEscalacao.ToString();
                    this.Response.Redirect("CadJornadaEscala.aspx?mod=" + BLEncriptacao.EncQueryStr(strParametro));
                }
                else if (blnTrocaHorario)
                {
                    if (this.BlnEditar)
                    {
                        strParametro = BLEncriptacao.EncQueryStr("TrocaHor," + this.IdEscalacao.ToString());
                        this.Response.Redirect("CadEscalaFinalizacao.aspx?mod=" + strParametro);
                    }
                    else
                    {
                        //volta para a tela inicial de listagem de escalas

                        this.Response.Redirect("CadSelecaoEscalaColaborador.aspx");
                    }
                }
            }
            else
            {
                this.clrPeriodoEscala.SelectedDates.Clear();
                this.BindModel(Enums.TipoTransacao.DescarregarDados, true, true);
                //this.PopularListaColaboradores(null);
            }
        }
        #endregion

        #region Inserir
        /// <summary>
        /// Inseri os horários dos colcaboradores na grid.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [cmarchi] created 26/11/2009
        ///     [cmarchi] modify 18/1/2010
        ///     [haguiar] modify 21/10/2010
        ///     [haguiar_2] modify 02/12/2010
        ///     adicionar excluir horário
        ///     [haguiar_8829] modify 06/07/2011 15:29
        ///     adicionar hora extra
        /// </history>
        protected void btnInserir_Click(object sender, EventArgs e)
        {
            if (this.ValidarInserir())
            {
                DateTime datData;

                double dblHora   = 0;
                double dblMinuto = 0;

                string[] arrHorarios = null;

                string strHorario = string.Empty;

                ListItem ltmHorario = this.lstHorarios.SelectedItem;               

                HorarioColaboradores objHorarioColaboradores = new HorarioColaboradores();

                if (objHorarioColaboradores.HoraExtra = this.chkHoraExtra.Checked)
                {
                    //objHorarioColaboradores.HorarioColaborador = ltmHorario.Value + " (Hora Extra)";
                    objHorarioColaboradores.HorarioColaborador = ltmHorario.Text + " (Hora Extra)";
                }
                else
                {
                    //objHorarioColaboradores.HorarioColaborador = ltmHorario.Value;
                    objHorarioColaboradores.HorarioColaborador = ltmHorario.Text;
                    int intCodLegado;

                    int.TryParse(ltmHorario.Value, out intCodLegado);
                    objHorarioColaboradores.CodLegado = intCodLegado;
                }
                
                objHorarioColaboradores.IdEscalacao = this.IdEscalacao;
                objHorarioColaboradores.Bloqueado   = false;

                objHorarioColaboradores.DatasColaboradores = new Collection<DateTime>();

                //insere as datas selecionadas
                foreach (Telerik.Web.UI.RadDate item in this.clrPeriodoEscala.SelectedDates)
                {
                    //insere a data com os horários 
                    //strHorario = this.lstHorarios.SelectedValue;
                    //strHorario = this.lstHorarios.Text;
                    strHorario = ltmHorario.Text;

                    if (strHorario == "Compensado")
                    {
                        dblHora   = 0;
                        dblMinuto = 0;

                        objHorarioColaboradores.Compensado = true;
                        objHorarioColaboradores.Licenca   = false;
                        objHorarioColaboradores.Folga      = false;

                        objHorarioColaboradores.ExcluirTrocaHorario = false;
                        objHorarioColaboradores.HorarioFlex = false;
                        objHorarioColaboradores.HoraExtra = false;

                    }
                    else if (strHorario == "Férias/Licença")
                    {
                        dblHora   = 0;
                        dblMinuto = 0;

                        objHorarioColaboradores.Compensado = false;
                        objHorarioColaboradores.Licenca   = true;
                        objHorarioColaboradores.Folga      = false;

                        objHorarioColaboradores.ExcluirTrocaHorario = false;
                        objHorarioColaboradores.HorarioFlex = false;
                        objHorarioColaboradores.HoraExtra = false;
                    }
                    else if (strHorario == "Folga/DSR")
                    {
                        dblHora   = 0;
                        dblMinuto = 0;

                        objHorarioColaboradores.Compensado = false;
                        objHorarioColaboradores.Licenca   = false;
                        objHorarioColaboradores.Folga      = true;

                        objHorarioColaboradores.ExcluirTrocaHorario = false;
                        objHorarioColaboradores.HorarioFlex = false;
                        objHorarioColaboradores.HoraExtra = false;
                    }
                    else if (strHorario == "08 às 09 flex")
                    {
                        dblHora = 8;
                        dblMinuto = 0;

                        objHorarioColaboradores.Compensado = false;
                        objHorarioColaboradores.Licenca = false;
                        objHorarioColaboradores.Folga = false;

                        objHorarioColaboradores.ExcluirTrocaHorario = false;
                        objHorarioColaboradores.HorarioFlex = true;
                        objHorarioColaboradores.HoraExtra = false;

                    }

                    else if (strHorario == "Excluir troca de horário")
                    {
                        dblHora   = 0;
                        dblMinuto = 0;

                        objHorarioColaboradores.Compensado = false;
                        objHorarioColaboradores.Licenca   = false;
                        objHorarioColaboradores.Folga      = false;

                        objHorarioColaboradores.ExcluirTrocaHorario = true;
                        objHorarioColaboradores.HorarioFlex = false;
                        objHorarioColaboradores.HoraExtra = false;
                    }                    
                    
                    else
                    {
                        arrHorarios = strHorario.Substring(0,5).Split(':');

                        dblHora   = Convert.ToDouble(arrHorarios[0]);
                        dblMinuto = Convert.ToDouble(arrHorarios[1]);

                        objHorarioColaboradores.Compensado = false;
                        objHorarioColaboradores.Licenca   = false;
                        objHorarioColaboradores.Folga      = false;

                        objHorarioColaboradores.ExcluirTrocaHorario = false;
                        objHorarioColaboradores.HorarioFlex = false;
                        objHorarioColaboradores.HoraExtra = this.chkHoraExtra.Checked;
                    }
                    
                    datData = item.Date.AddHours(dblHora);
                    datData = datData.AddMinutes(dblMinuto);

                    objHorarioColaboradores.DatasColaboradores.Add(datData);
                } 

                //insere o horário
                this.Inserir(ref objHorarioColaboradores);
                this.chkHoraExtra.Checked = false;

                ////grava as datas selecionadas
                //this.ArmazenarDatasSelecionadas();
            }
        }
        #endregion

        #endregion

        #region Controla Painels
        /// <summary>
        /// Controla os Painels de Datas e Horários  Colaboradores.
        /// </summary>
        ///Verifica qual é o painel a ser exibido
        /// <history>
        ///     [cmarchi] created 26/11/2009
        ///     [cmarchi] modify 18/1/2010
        /// </history>
        protected void ControlaPaineis(bool pblnPainelData)
        {
            if (pblnPainelData)
            {
                pnlData.Visible = true;
                pnlHoraColaborador.Visible = false;
            }
            else
            {
                pnlData.Visible = false;
                pnlHoraColaborador.Visible = true;
            }

        }
        #endregion

        #region EditarHorarioColaboradores
        /// <summary>
        ///    Editar Horario dos Colaboradores.
        /// </summary>
        /// <param name="pintLinhaGrid">Linha da Grid</param>
        /// <param name="pintIdEscalacao">Id da Escalação</param>
        /// <param name="pstrHorario">Horário</param>
        /// <param name="pstrIdColaboradores">Id dos Colaboradores</param>
        /// <history>
        ///     [cmarchi] created 18/1/2010
        ///     [cmarchi] modify 8/2/2010
        /// </history>
        private void EditarHorarioColaboradores(int pintLinhaGrid, string pstrHorario,
            string pstrIdColaboradores)
        {
            string strParametro = string.Empty;

            strParametro = BLEncriptacao.EncQueryStr("Sim#" + pintLinhaGrid.ToString() + "#" +
               pstrHorario + "#0"); // + pstrIdColaboradores);

            Session.Add("CodigosColaboradores", pstrIdColaboradores);

            //guardando as datas selecionadas
            this.Session.Add("ssData", this.clrPeriodoEscala);           

            this.RadWindowCadastroEscala(strParametro);
        }
        #endregion    

        #region Grid Horarios Colaboradores

        #region NeedDataSource
        protected void radGridHorariosColaboradores_NeedDataSource(object source, Telerik.WebControls.GridNeedDataSourceEventArgs e)
        {
            if (Page.IsPostBack)
            {
                this.radGridHorariosColaboradores.DataSource = BLEscala.ObterHorariosColaboradoresNBloqueados(
                                                                    this.HorarioColaboradores);
            }
        }
        #endregion

        #region ItemCommand
        protected void radGridHorariosColaboradores_ItemCommand(object source, Telerik.WebControls.GridCommandEventArgs e)
        {
            if (e.CommandName.Trim() == "Editar")
            {
                if (e.CommandArgument.ToString().Trim() != string.Empty)
                {
                    this.EditarHorarioColaboradores(e.Item.ItemIndex,
                        e.Item.Cells[2].Text,
                        e.Item.Cells[4].Text);
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
            */
        }
        #endregion

        #region ItemDataBound
        protected void radGridHorariosColaboradores_ItemDataBound(object sender, Telerik.WebControls.GridItemEventArgs e)
        {
            if (e.Item.ItemType == Telerik.WebControls.GridItemType.AlternatingItem | e.Item.ItemType == Telerik.WebControls.GridItemType.Item)
            {
                ImageButton btnEditar = (ImageButton)e.Item.FindControl("Editar");
                
                //verifica se está bloqueado ou não
                if(Convert.ToBoolean(e.Item.Cells[6].Text))
                    e.Item.Enabled = false;
                else
                {
                    if (btnEditar != null)
                    {
                        e.Item.Attributes.Add("onMouseOver", "selecionaGrid(this)");
                        e.Item.Attributes.Add("onMouseOut", "deSelecionaGrid(this)");
                        e.Item.Style["cursor"] = "hand";
                        e.Item.ToolTip = BLIdiomas.TraduzirMensagens(Mensagens.EDITAR);

                        // Cursor 
                        int intCell = e.Item.Cells.Count;
                        for (int @int = 0; @int <= intCell - 1; @int++)
                        {
                            e.Item.Cells[@int].Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(btnEditar, ""));
                        }

                        btnEditar.ToolTip = BLIdiomas.TraduzirMensagens(Mensagens.EDITAR);
                        btnEditar.CommandArgument = DataBinder.Eval(e.Item.DataItem, "IdEscalacao").ToString();
                        btnEditar.Visible = true;
                    }
                }
            }
        }
        #endregion

        #endregion

        #region Inserir Horário dos Colaboradores
        /// <summary>
        ///    Editar os Horários dos Colaboradores.
        /// </summary>
        /// <param name="pobjHorarioColaboradores">Objeto Horários dos Colaboradores</param>
        /// <history>
        ///     [cmarchi] created 18/1/2010
        ///     [cmarchi] modify 12/2/2010
        ///     [haguiar] modify 16/11/2011 10:57
        /// </history>
        private void Inserir(ref HorarioColaboradores pobjHorarioColaboradores)
        {
            Collection<ListItem> colColaboradores = new Collection<ListItem>();
            BLEscala objBLEscala = new BLEscala();

            Collection<HorarioColaboradores> colHorarioCol = null;

            StringBuilder strCodigoColaboradores = new StringBuilder();
            StringBuilder strNomeColaboradores = new StringBuilder();

            if (!string.IsNullOrEmpty(pobjHorarioColaboradores.CodigosColaboradores))
            {
                strCodigoColaboradores.Append(pobjHorarioColaboradores.CodigosColaboradores + ",");
                strNomeColaboradores.Append(pobjHorarioColaboradores.NomesColaboradores + ",");

                string[] arrCodigoColaboradores = pobjHorarioColaboradores.CodigosColaboradores.Split(',');
                string[] arrColaboradores = pobjHorarioColaboradores.NomesColaboradores.Split(',');
                int i = 0;

                foreach (string col in arrColaboradores)
                {
                    colColaboradores.Add(new ListItem(col.ToString(),arrCodigoColaboradores[i].ToString()));

                    i += 1;
                }

                /*
                foreach (ListItem var in this.lstColaboradores.Items)
                {
                    colColaboradores.Add(var);
                }*/
            }

            foreach (ListItem var in this.lstColaboradores.Items)
            {
                if (var.Selected)
                {
                    colColaboradores.Add(var);

                    strCodigoColaboradores.Append(var.Value + ",");
                    strNomeColaboradores.Append(var.Text + ",");
                }
            }
            colHorarioCol = this.HorarioColaboradores;

            if (strCodigoColaboradores.Length > 0 && strNomeColaboradores.Length > 0)
            {
                strCodigoColaboradores.Remove(strCodigoColaboradores.Length - 1, 1);
                strNomeColaboradores.Remove(strNomeColaboradores.Length - 1, 1);

                pobjHorarioColaboradores.CodigosColaboradores = strCodigoColaboradores.ToString();
                pobjHorarioColaboradores.NomesColaboradores = strNomeColaboradores.ToString();

                int intPosicaoHorario = BLEscala.IndiceHorarioColaborador(this.HorarioColaboradores,
                                                        pobjHorarioColaboradores.HorarioColaborador);

                objBLEscala.InserirHorarioColaborador(ref colHorarioCol,
                    intPosicaoHorario, pobjHorarioColaboradores);

                //remove da lista
                foreach (ListItem var in colColaboradores)
                {
                    this.lstColaboradores.Items.Remove(var);
                }

                //insere na grid
                this.radGridHorariosColaboradores.DataSource = BLEscala.ObterHorariosColaboradoresNBloqueados(
                                                                    this.HorarioColaboradores);
                this.radGridHorariosColaboradores.DataBind();
            }
        }
        #endregion

        #region Popular Listas

        #region PopularListaColaboradores
        /// <summary>
        /// Preenche a Lista de Colaboradores.
        /// </summary>
        /// <param name="parrColaboradores">Array com os Id dos Colaboradores</param>
        /// <history>
        ///     [cmarchi] created 15/1/2010
        ///     [cmarchi] modify  25/1/2010
        ///     [haguiar_2] modify 24/11/2010
        ///     Carregar os colaboradores para troca de horário
        /// </history>
        private void PopularListaColaboradores(string[] parrColaboradores)
        {
            BLEscala objBLEscala         = new BLEscala();
            BLColaborador objBLColaborador = new BLColaborador();
            Collection<Colaborador> colColaboradores = null;

            try
            {
                if (parrColaboradores == null)
                {
                    if (!this.blnTrocaHorario)
                    {
                        colColaboradores = objBLEscala.ObterColaboradoresNaoContemHorarioEscala(
                                                    this.IdEscalacao);                  
                    }
                    else
                    {
                        //carrega os colaboradores da escala para troca de horário
                        colColaboradores = objBLEscala.ObterColaboradores(this.IdEscalacao);
                    }
                }
                else if (parrColaboradores.Length > 0)
                {
                    if (!this.blnTrocaHorario)
                    {
                        //apenas os colaboradores da data selecionada
                        colColaboradores = objBLColaborador.Obter(parrColaboradores);
                    }
                    else
                    {
                        //carrega todos os colaboradores da escala para troca de horário
                        colColaboradores = objBLEscala.ObterColaboradores(this.IdEscalacao);
                    } 
                }

                this.lstColaboradores.Items.Clear();

                foreach (Colaborador objColaborador in colColaboradores)
                {
                    ListItem limColaborador = new ListItem();
                    limColaborador.Text = objColaborador.NomeColaborador + " - " + objColaborador.CodigoColaborador;
                    limColaborador.Value = objColaborador.IdColaborador.ToString();

                    this.lstColaboradores.Items.Add(limColaborador);
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }
        #endregion

        #region PopularListaHorarios
        /// <summary>
        /// Preenche a Lista de Horarios.
        /// </summary>
        /// <history>
        ///     [cmarchi] created 15/1/2010
        ///     [tgerevini] Modified 16/08/2010
        ///     [haguiar_2] modify 02/12/2010
        ///     adicionar excluir troca de horário
        ///     [haguiar_2] modify 04/12/2010
        ///     adicionar o horário 08 às 09 flex
        ///     [haguiar_8829] modify 06/07/2011 15:33
        ///     adicionar hora extra
        ///     [haguiar] modify 02/01/2012 08:39
        ///     adicionar codlegado
        /// </history>
        private void PopularListaHorarios()
        {
            BLEscala objBLEscala = new BLEscala();
            BLEscalaDepartamental objBLEscalaDepartamental = null;

            EscalaDepartamental objEscalaDepartamental = null;

            Escalacao objEscalacao = null;

            try
            {
                objEscalacao = objBLEscala.Obter(this.IdEscalacao);

                if (objEscalacao != null && objEscalacao.IdEscalaDepartamental > 0)
                {
                    objBLEscalaDepartamental = new BLEscalaDepartamental();

                    objEscalaDepartamental = objBLEscalaDepartamental.Obter(objEscalacao.IdEscalaDepartamental, true, objEscalacao.IdTipoSolicitacao);

                    this.lstHorarios.Items.Clear();

                    this.lstHorarios.DataTextField = "IdHorario";
                    this.lstHorarios.DataValueField = "CodLegado";
                    
                    this.lstHorarios.DataSource = objEscalaDepartamental.HorariosEscala;

                    this.lstHorarios.DataBind();

                    this.lstHorarios.Items.Add(new ListItem("08 às 09 flex", "08 às 09 flex"));
                    
                    //this.lstHorarios.Items.Add(new ListItem("08 às 09 flex", "08:00"));

                    this.lstHorarios.Items.Add(new ListItem("Compensado", "Compensado"));
                    this.lstHorarios.Items.Add(new ListItem("Férias/Licença", "Férias/Licença"));
                    this.lstHorarios.Items.Add(new ListItem("Folga/DSR", "Folga/DSR"));

                    if (blnTrocaHorario && BlnEditar)
                    {
                        this.lstHorarios.Items.Add(new ListItem("Excluir troca de horário", "Excluir troca de horário"));
                        this.chkHoraExtra.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }
        #endregion

        #endregion

        #region Propriedades

        #region ColaboradoresSelecionados
        /// <summary>
        ///     Propriedade Colaboradores que contem os colaboradores selecionados.
        /// </summary>
        /// <history>
        ///     [cmarchi] created 25/1/2010 
        /// </history>
        private string ColaboradoresSelecionados
        {
            get
            {
                if (this.ViewState["vsCol"] == null)
                {
                    this.ViewState.Add("vsCol", string.Empty);
                }

                return this.ViewState["vsCol"] as string;
            }

            set
            {
                this.ViewState.Add("vsCol", value);
            }
        }
        #endregion

        #region DataSelecionada
        /// <summary>
        ///     Propriedade Data que contem a data selecionadas dos colaboradores.
        /// </summary>
        /// <history>
        ///     [cmarchi] created 25/1/2010 
        /// </history>
        private DateTime? DataSelecionada
        {
            get
            {
                if (this.ViewState["vsData"] == null)
                {
                    this.ViewState.Add("vsData", null);
                }

                return Convert.ToDateTime(this.ViewState["vsData"]);
            }

            set
            {
                this.ViewState.Add("vsData", value);
            }
        }
        #endregion

        #region DatasSelecionadas
        /// <summary>
        ///     Propriedade Data que as datas selecionadas dos colaboradores pela ultima vez.
        /// </summary>
        /// <history>
        ///     [cmarchi] created 22/2/2010 
        /// </history>
        private DateTime[] DatasSelecionadas
        {
            get
            {
                if (this.ViewState["vsDatasSel"] == null)
                {
                    this.ViewState.Add("vsDatasSel", null);
                }

                return (DateTime[])this.ViewState["vsDatasSel"];
            }

            set
            {
                this.ViewState.Add("vsDatasSel", value);
            }
        }
        #endregion

        #region Datas
        /// <summary>
        ///     Propriedade Datas que contem as datas selecionadas dos colaboradores.
        /// </summary>
        /// <history>
        ///     [cmarchi] created 18/1/2010 
        /// </history>
        private Collection<DateTime> Datas
        {
            get
            {
                if (this.ViewState["vsDatas"] == null)
                {
                    this.ViewState.Add("vsDatas", new Collection<DateTime>());
                }

                return (Collection<DateTime>)this.ViewState["vsDatas"];
            }

            set
            {
                this.ViewState.Add("vsDatas", value);
            }
        }
        #endregion

        #region Editar
        /// <summary>
        ///     Propriedade Editar que verifica se está ou não editar
        /// </summary>
        /// <history>
        ///     [cmarchi] created 13/1/2010
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

        /// <summary>
        ///     Propriedade Editar que verifica se está ou não editar troca de horário
        /// </summary>
        /// <history>
        ///     [haguiar_2] created 26/11/2010
        /// </history>
        private bool BlnEditarTrocaHorario
        {
            get
            {
                if (this.ViewState["vsEditarTrocaHorario"] == null)
                {
                    this.ViewState.Add("vsEditarTrocaHorario", false);
                }

                return Convert.ToBoolean(this.ViewState["vsEditarTrocaHorario"]);
            }

            set
            {
                this.ViewState.Add("vsEditarTrocaHorario", value);
            }
        }
        #endregion

        #region HorarioColaboradores
        /// <summary>
        ///     Propriedade HorarioColaboradores que contem os horários dos colaboradores.
        /// </summary>
        /// <history>
        ///     [cmarchi] created 18/1/2010 
        /// </history>
        private Collection<HorarioColaboradores> HorarioColaboradores
        {
            get
            {
                if (this.ViewState["vsHorarioCol"] == null)
                {
                    this.ViewState.Add("vsHorarioCol", new Collection<HorarioColaboradores>());
                }

                return (Collection<HorarioColaboradores>)this.ViewState["vsHorarioCol"];
            }

            set
            {
                this.ViewState.Add("vsHorarioCol", value);
            }
        }
        #endregion

        #region IdEscalacao
        /// <summary>
        ///     Propriedade Id_Escalacao utilizada para Editar uma Escalacao
        /// </summary>
        /// <history>
        ///     [cmarchi] created 15/1/2010 
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

        #endregion

        #region RadWindow

        /// <summary>
        ///     Abre a RadWindow com a tela de alteração de horários dos colaboradores
        /// </summary>
        /// <param name="pstrParametro">Parametro</param>
        /// <history>
        ///   [cmarchi] created 19/11/2009
        ///   [cmarchi] modify 18/1/2010
        /// </history>
        protected void RadWindowCadastroEscala(string pstrParametro)
        {
            //cria radwindow
            Telerik.WebControls.RadWindow rwdWindow = new Telerik.WebControls.RadWindow();
            Telerik.WebControls.RadWindowManager rwmWindowManager = new Telerik.WebControls.RadWindowManager();

            rwmWindowManager.Skin = "Office2007";
            //Não exibe nenhum botão na janela
            rwmWindowManager.Behavior = Telerik.WebControls.RadWindowBehaviorFlags.Close;
            rwmWindowManager.ShowContentDuringLoad = false;
            rwmWindowManager.VisibleStatusbar = false;

            rwdWindow.Width = Unit.Pixel(527);
            rwdWindow.Height = Unit.Pixel(350);
            rwdWindow.Modal = true;
            rwdWindow.ReloadOnShow = true;
            rwdWindow.DestroyOnClose = true;
            rwdWindow.VisibleOnPageLoad = true;

            rwdWindow.Title = "Atualização de horário dos colaboradores";

            rwdWindow.NavigateUrl = "ListHorarioColaboradores.aspx?open=" + pstrParametro;
            rwmWindowManager.Windows.Add(rwdWindow);

            Panel pnlEscala = null;

            //Tenta encontrar na master
            pnlEscala = (Panel)this.FindControl("pnlListHorarioColaboradores");
            pnlEscala.Controls.Add(rwmWindowManager);
        }
        #endregion       

        #region Validações

        #region Validar

        /// <summary>
        ///     Valida a horas e datas
        /// </summary>
        /// <history>
        ///   [cmarchi] created 12/2/2010
        ///   [haguiar_2] modify 24/11/2010
        ///   Permitir troca de horário - não é necessário preencher horário para todos os colaboradores
        /// </history>
        private int Validar()
        {
            if (this.Datas.Count > 0 && !blnTrocaHorario)
            {
                if (!this.VerificarQuantidadeDatasPeriodo())
                {


                    if ((this.lstColaboradores.Items.Count > 0) && (this.DatasSelecionadas.Length >= 0) )
                    {
                        this.RadAjaxPanel1.Alert("Selecione um Colaborador.");
                        return 3;
                    }
                    else if (this.DatasSelecionadas != null && this.DatasSelecionadas.Length == 0)
                    {
                        if (!this.blnTrocaHorario)
                        {
                            this.RadAjaxPanel1.Alert("Selecione as datas do período.");
                            return 1;
                        }
                        else
                        {
                            //verifica se trocou horário para algum colaborador
                            if (this.HorarioColaboradores.Count == 0)
                            {
                                this.RadAjaxPanel1.Alert("Selecione ao menos uma data para a troca de horário!");
                                return 1;
                            }
                        }

                    }
                    else if (this.DatasSelecionadas != null && this.HorarioColaboradores.Count > 0 && this.DatasSelecionadas.Length > 0)
                    {
                        Collection<DateTime> colDatas = this.HorarioColaboradores[this.HorarioColaboradores.Count - 1].DatasColaboradores;
                        bool blnResposta = false;

                        //faz uma busca por colaboradores nas datas selecionadas
                        for (int i = 0; i < colDatas.Count; i++)
                        {
                            if (colDatas[i].ToShortDateString() == this.DatasSelecionadas[0].ToShortDateString() &&
                                !string.IsNullOrEmpty(this.HorarioColaboradores[this.HorarioColaboradores.Count - 1].NomesColaboradores))
                            {
                                blnResposta = true;
                                break;
                            }
                        }

                        if (!blnResposta)
                        {
                            this.RadAjaxPanel1.Alert("Selecione um Colaborador.");
                            return 3;
                        }
                        else
                        {
                            //if (!this.blnTrocaHorario)
                            //{
                                this.RadAjaxPanel1.Alert("Há datas do período não preenchidas.");
                            //}

                            return 1;
                            
                            //else
                            //{
                            //    //troca de horário
                            //    this.RadAjaxPanel1.Alert("Há datas do período não preenchidas. Verifique!");
                            //    return 1;
                            //}
                        }
                    }

                    else if(!this.BlnEditar)
                    {
                        this.RadAjaxPanel1.Alert("Há datas do período não preenchidas.");
                        return 1;
                    }                    
                }
                else if (this.HorarioColaboradores.Count == 0 &&
                         this.lstColaboradores.Items.Count > 0)
                {
                    this.RadAjaxPanel1.Alert("Selecione um Colaborador.");
                    return 2;
                }
                
                else if( !this.BlnEditar && this.HorarioColaboradores.Count > 0 &&
               this.DatasSelecionadas != null && this.DatasSelecionadas.Length > 0)
                {
                    Collection<DateTime> colDatas = this.HorarioColaboradores[this.HorarioColaboradores.Count - 1].DatasColaboradores;
                    bool blnResposta = false;

                    //faz uma busca por colaboradores nas datas selecionadas
                    for (int i = 0; i < colDatas.Count; i++)
                    {                        
                        if (colDatas[i].ToShortDateString() == this.DatasSelecionadas[0].ToShortDateString() &&                               
                            !string.IsNullOrEmpty(this.HorarioColaboradores[this.HorarioColaboradores.Count - 1].NomesColaboradores))
                        {
                            blnResposta = true;
                            break;
                        } 
                    }

                    if (!blnResposta)
                    {
                        this.RadAjaxPanel1.Alert("Selecione um Colaborador.");
                        return 3;
                    }
                }

                else if (this.HorarioColaboradores.Count == 0 && this.DatasSelecionadas == null &&
                    this.BlnEditar)
                {
                    this.RadAjaxPanel1.Alert("Selecione as datas do período.");
                    return 1;
                }
                    
                return 0;
            }

            if (blnTrocaHorario && this.Datas != null)
            {
                TimeSpan ts;

                Collection<DateTime> colDatas = null;
                BLEscala objBLEscala = new BLEscala(); 
                colDatas = objBLEscala.ObterDtEscaladas(this.IdEscalacao);

                ts = clrPeriodoEscala.RangeMaxDate.Date.AddDays(1) - clrPeriodoEscala.RangeMinDate.Date;

                if (blnNovoHorario)
                {
                    if (ts.Days != this.Datas.Count)
                        blnApresentaMsgTrocaHorario = !blnApresentaMsgTrocaHorario;
                }
                else
                {
                    if (colDatas != null && ts.Days != colDatas.Count)
                        blnApresentaMsgTrocaHorario = !blnApresentaMsgTrocaHorario;

                    if (this.Datas != null && ts.Days == this.Datas.Count)
                        blnApresentaMsgTrocaHorario = false;
                }

                if (blnApresentaMsgTrocaHorario)
                {
                    this.RadAjaxPanel1.Alert("Há datas do período não preenchidas. Efetue a troca de horários ou clique em 'Avançar'.");

                    //apaga selecao de datas
                    this.clrPeriodoEscala.SelectedDates.Clear();
                    this.DataSelecionada = null;
                    
                    //sai do modo edicao
                    this.BlnEditar = false;

                    return 1;
                }
            }

            if (this.clrPeriodoEscala.SelectedDates.Count == 0 && !this.BlnEditar && !blnTrocaHorario)
            {
                this.RadAjaxPanel1.Alert("Selecione as datas do período.");
                return 2;
            }
            else if (this.lstColaboradores.Items.Count > 0 && !blnTrocaHorario)
            {
                this.RadAjaxPanel1.Alert("Selecione um Colaborador.");
                return 2;
            }

            return 0;
        }
        #endregion

        #region Validar Campos
        /// <summary>
        /// Valida os Campos
        /// </summary>
        /// <returns>True - Validação OK, False - Erro</returns>
        /// <history>
        ///     [cmarchi] created 19/1/2010
        ///     [cmarchi] modify 8/2/2010
        /// </history>
        private int ValidarCampos()
        {
            if (this.lstColaboradores.Items.Count > 0)
            {
                this.RadAjaxPanel1.Alert("Selecione um Colaborador.");
                return 1;
            }            
            
            if (this.Datas.Count > 0)
            {
                if (!this.VerificarQuantidadeDatasPeriodo())
                {
                    this.RadAjaxPanel1.Alert("Há datas do período não preenchidas.");
                    return 2;
                }
            }
            else
            {
                this.RadAjaxPanel1.Alert("Selecione as datas do período.");
                return 2;
            }

            return 0;
        }
        #endregion

        #region Validar no Avançar

        /// <summary>
        ///     Verifica se os algum horário e colaboradores foram selecionados.
        /// </summary>
        /// <returns>True - Válidos, False - Erros</returns>
        /// <history>
        ///     [cmarchi] created 1/03/2010
        ///     [haguiar_2] modify 24/11/2010
        ///     Avança mesmo sem ter selecionado horário para todos os colaboradores da lista (troca de horário)
        /// </history>
        private bool ValidarAvancar()
        {
            bool blnRetorno = true;

            if (this.pnlHoraColaborador.Visible && !this.blnTrocaHorario)
            {
                if (this.lstColaboradores.Items.Count > 0)
                {
                    RadAjaxPanel1.Alert("Selecione um Colaborador.");
                    blnRetorno = false;
                }                
            }

            return blnRetorno;
        }
        #endregion

        #region Validar na Data

        /// <summary>
        ///     Verifica se os alguma data selecionada.
        /// </summary>
        /// <returns>True - Válidos, False - Erros</returns>
        /// <history>
        ///     [cmarchi] created 18/01/2010
        ///     [cmarchi] modify 8/2/2010
        /// </history>
        private bool ValidarData()
        {
            bool blnRetorno = true;

            if (this.clrPeriodoEscala.SelectedDates.Count == 0)
            {
                RadAjaxPanel1.Alert("Selecione ao menos uma data.");
                blnRetorno = false;
            }           

            return blnRetorno;
        }
        #endregion

        #region Validar no Inserir

        /// <summary>
        ///     Verifica se os algum horário e colaboradores foram selecionados.
        /// </summary>
        /// <returns>True - Válidos, False - Erros</returns>
        /// <history>
        ///     [cmarchi] created 18/01/2010
        ///     [cmarchi] modify 18/02/2010
        /// </history>
        private bool ValidarInserir()
        {
            bool blnRetorno = true;

            if (this.lstHorarios.SelectedIndex == -1)
            {
                radAjaxHorarios.Alert("Selecione um Horário.");
                blnRetorno = false;
            }

            if (this.lstColaboradores.Items.Count > 0 && this.lstColaboradores.SelectedIndex == -1)
            {
                radAjaxHorarios.Alert("Selecione um Colaborador.");
                blnRetorno = false;
            }

            if (this.lstColaboradores.Items.Count == 0 && this.lstColaboradores.SelectedIndex == -1)
            {
                radAjaxHorarios.Alert("Não há Colaborador.");
                blnRetorno = false;
            }

            return blnRetorno;
        }
        #endregion

        #endregion

        #region VerificarHiddenColaboradores
        /// <summary>
        /// Verifica o valor da query string.
        /// </summary>
        /// <history>
        ///     [cmarchi] created 18/1/2010
        ///     [cmarchi] modify 8/2/2010
        ///     [haguiar] modify 01/04/2011
        ///     remover colaborador da lista de horários
        /// </history>

        string[] arrRemoverIdColaboradores;

        private void VerificarHiddenColaboradores()
        {
            if (!string.IsNullOrEmpty(this.txtHiddenColaboradores.Value))
            {
                string[] arrParametros = BLEncriptacao.DecQueryStr(this.txtHiddenColaboradores.Value).Split('#');
                this.txtHiddenColaboradores.Value = string.Empty;

                int intLinhaPosicao;

                if (Int32.TryParse(arrParametros[0], out intLinhaPosicao))
                {                    
                    string[] arrIdColaboradores = arrParametros[2].Split(',');

                    if (arrIdColaboradores != null && arrIdColaboradores.Length > 0)
                    {
                        arrRemoverIdColaboradores = arrIdColaboradores;

                        Collection<HorarioColaboradores> colHorarioColaboradores = this.HorarioColaboradores;

                        //obtem as datas selecionadas e armazenadas
                        this.clrPeriodoEscala = (Telerik.Web.UI.RadCalendar)this.Session["ssData"];

                        //remove a sessão
                        this.Session["ssData"] = null;
                        this.Session.Remove("ssData");

                        this.AdicionarColaboradorLista(arrIdColaboradores);

                        //remove colaborador da lista de horários
                        int intTotal = colHorarioColaboradores.Count;

                        for (int i = 0; i < intTotal; i++)
                        {
                            foreach (HorarioColaboradores gobjHorarioCol in colHorarioColaboradores)
                            {
                                if (!gobjHorarioCol.Bloqueado)
                                {
                                    BLEscala.ExcluirColaborador(ref colHorarioColaboradores,
                                         intLinhaPosicao, arrParametros[1], arrIdColaboradores);

                                    intLinhaPosicao = 0;
                                    intTotal -= 1;

                                    break;
                                }

                                intLinhaPosicao += 1;
                            }
                        }

                        /*
                        BLEscala.ExcluirColaborador(ref colHorarioColaboradores,
                             intLinhaPosicao, arrParametros[1], arrIdColaboradores);
                        */

                        this.BindModel(Enums.TipoTransacao.DescarregarDados, false, true);
                    } 
                }
            }
        }
        #endregion

        #region VerificarQuantidadeDatasPeriodo
        /// <summary>
        /// Verifica se a quantidade de datas selecionadas corresponde a quantidade de datas no período. 
        /// </summary>
        /// <returns>True - Quantidade iguais, False - Quantidades diferentes</returns>
        /// <history>
        ///     [cmarchi] created 20/1/2010
        /// </history>
        private bool VerificarQuantidadeDatasPeriodo()
        {
            int intTotalDias = 0;

            //verifica se o mês é igual para a maior e menor data do período
            if (this.clrPeriodoEscala.RangeMaxDate.Month == this.clrPeriodoEscala.RangeMinDate.Month)
            {
                intTotalDias = (this.clrPeriodoEscala.RangeMaxDate.Day
                                - this.clrPeriodoEscala.RangeMinDate.Day) + 1;
            }
            else
            {

                intTotalDias = ((DateTime.DaysInMonth(this.clrPeriodoEscala.RangeMinDate.Year, this.clrPeriodoEscala.RangeMinDate.Month) -
                                this.clrPeriodoEscala.RangeMinDate.Day) + this.clrPeriodoEscala.RangeMaxDate.Day) + 1;
            }

            if (intTotalDias != this.Datas.Count)
            {
                return false;
            }

            return true;
        }
        #endregion

        #region VerificarQueryTela
        /// <summary>
        /// Verifica a query da Tela que a chamou.
        /// </summary>
        ///<param name="pstrQuery">Query</param>
        /// <history>
        ///     [cmarchi] created 15/1/2010
        ///     [cmarchi] modify 25/1/2010
        ///     [haguiar_2] modify 24/11/2010
        ///     Verifica cadastro de horários
        /// </history>
        private void VerificarQueryTela(string pstrQuery)
        {
            if (!string.IsNullOrEmpty(pstrQuery))
            {
                string[] arrParametros = BLEncriptacao.DecQueryStr(pstrQuery).Split(',');

                //verifica se a tela
                if (arrParametros[0] == "CadJor")
                {
                    int intIdEscalacao;

                    if (Int32.TryParse(arrParametros[1], out intIdEscalacao))
                    {
                        this.IdEscalacao = intIdEscalacao;
                        
                        this.BindModel(Enums.TipoTransacao.DescarregarDados, true, true);
                        this.PopularListaColaboradores(null);

                        return;
                    }
                }

                //cadastro de horários
                if (arrParametros[0] == "CadHor")
                {
                    this.blnTrocaHorario = true;
                    this.Label2.Text = "Troca de horário - Seleção de horários de entrada";

                    int intIdEscalacao;

                    blnNovoHorario = true;

                    if (Int32.TryParse(arrParametros[1], out intIdEscalacao))
                    {
                        this.IdEscalacao = intIdEscalacao;

                        this.BindModel(Enums.TipoTransacao.DescarregarDados, true, true);
                        this.PopularListaColaboradores(null);

                        this.chkHoraExtra.Visible = true;

                        return;
                    }
                }


                //verifica se a tela
                if (arrParametros[0] == "CarregaDatas")
                {
                    this.Label2.Text = "Troca de horário - Seleção de horários de entrada";

                    //carrega datas de troca de horários
                    int intIdEscalacao;

                    if (Int32.TryParse(arrParametros[1], out intIdEscalacao))
                    {
                        this.IdEscalacao = intIdEscalacao;

                        this.BlnEditarTrocaHorario = true;
                        this.BlnEditar = true;
                        this.blnTrocaHorario = true;

                        //avança sem alertar horários em aberto
                        blnApresentaMsgTrocaHorario = true;

                        this.ControlaPaineis(true);

                        BLEscala objBLEscala = new BLEscala();
                        Collection<DateTime> colDatas = null;

                        try
                        {
                            colDatas = objBLEscala.ObterDtEscaladas(this.IdEscalacao);

                            if (colDatas != null && colDatas.Count > 0)
                            {
                                //armazenas as datas selecionadas
                                this.Datas = colDatas;
                            }
                            else
                            {
                                this.Datas = objBLEscala.ObterDatas(this.IdEscalacao);
                            }

                            this.BindData(Enums.TipoTransacao.DescarregarDados);


                            //verifica datas do periodo da escala
                            TimeSpan ts;

                            ts = clrPeriodoEscala.RangeMaxDate.Date.AddDays(1) - clrPeriodoEscala.RangeMinDate.Date;

                            if (colDatas != null && ts.Days == colDatas.Count)
                            {
                                this.Avancar();

                                return;
                            }
                        }
                        catch (Exception ex)
                        {
                            ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
                        }

                        this.BindHorarios(Enums.TipoTransacao.DescarregarDados, false);
                        return;
                    }
                }

                arrParametros = BLEncriptacao.DecQueryStr(pstrQuery).Split('#');
                //verifica se a tela
                if (arrParametros[0] == "ModHor")
                {
                    this.Label2.Text = "Troca de horário - Seleção de horários de entrada";

                    //modifica troca de horários
                    int intIdEscalacao;

                    if (Int32.TryParse(arrParametros[1], out intIdEscalacao))
                    {
                        this.IdEscalacao = intIdEscalacao;

                        this.BlnEditarTrocaHorario = true;
                        this.BlnEditar = true;
                        this.blnTrocaHorario = true;

                        DateTime data = DateTime.Parse(arrParametros[2]);
                        this.DataSelecionada = data;

                        //verifica codlegado
                        int intCodLegado;
                        if (Int32.TryParse(arrParametros[4], out intCodLegado))
                            this.CodLegado = intCodLegado;

                        if (Session["CodigosColaboradores"] != null)
                        {
                            arrParametros[3] = (string)Session["CodigosColaboradores"];
                            Session["CodigosColaboradores"] = null;
                            Session.Remove("CodigosColaboradores");

                            this.ColaboradoresSelecionados = arrParametros[3];
                        } 

                        this.ControlaPaineis(false);

                        BLEscala objBLEscala = new BLEscala();
                        Collection<DateTime> colDatas = null;

                        try
                        {
                            colDatas = objBLEscala.ObterDtEscaladas(this.IdEscalacao);

                            if (colDatas != null && colDatas.Count > 0)
                            {
                                //armazenas as datas selecionadas
                                this.Datas = colDatas;
                                this.BindData(Enums.TipoTransacao.DescarregarDados);
                            }
                        }
                        catch (Exception ex)
                        {
                            ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
                        }

                        this.BindHorarios(Enums.TipoTransacao.DescarregarDados, false);
                        return;
                    }
                }

                arrParametros = BLEncriptacao.DecQueryStr(pstrQuery).Split('#');
                //verifica se a tela
                if (arrParametros[0] == "CadFim")
                {
                    int intIdEscalacao;

                    if (Int32.TryParse(arrParametros[1], out intIdEscalacao))
                    {
                        this.IdEscalacao = intIdEscalacao;

                        this.BlnEditar = true;

                        DateTime data = DateTime.Parse(arrParametros[2]);

                        this.DataSelecionada = data;

                        if (Session["CodigosColaboradores"] != null)
                        {
                            arrParametros[3] = (string)Session["CodigosColaboradores"];
                            Session["CodigosColaboradores"] = null;
                            Session.Remove("CodigosColaboradores");

                            this.ColaboradoresSelecionados = arrParametros[3];
                        } 

                        //this.BindModel(Enums.TipoTransacao.DescarregarDados, true, true);

                        this.ControlaPaineis(false);

                        //verifica codlegado
                        int intCodLegado;
                        if (Int32.TryParse(arrParametros[4], out intCodLegado))
                            this.CodLegado = intCodLegado;

                        this.BindHorarios(Enums.TipoTransacao.DescarregarDados, false); 
                        return;
                    }
                }
            }

            this.Response.Redirect("CadSelecaoEscalaColaborador.aspx");
        }

        /// <summary>
        ///     Propriedade CodLegado utilizada para armazenar o código do horário legado
        /// </summary>
        /// <history>
        ///     [haguiar] created 02/12/2012 17:08 
        /// </history>
        private int CodLegado
        {
            get
            {
                if (this.ViewState["vsCodLegado"] == null)
                {
                    this.ViewState.Add("vsCodLegado", 0);
                }

                return Convert.ToInt32(this.ViewState["vsCodLegado"]);
            }

            set
            {
                this.ViewState.Add("vsCodLegado", value);
            }
        }

        #region Propriedades para Troca de horário
        /// <summary>
        ///     Propriedade blnTrocaHorario - operando ou nao em modo troca de horário
        /// </summary>
        /// <history>
        ///     [haguiar_2] created 24/11/2010 
        /// </history>
        private bool blnTrocaHorario
        {
            get
            {
                if (this.ViewState["vsblnTrocaHorario"] == null)
                {
                    this.ViewState.Add("vsblnTrocaHorario", false);
                }

                return Convert.ToBoolean(this.ViewState["vsblnTrocaHorario"]);
            }

            set
            {
                this.ViewState.Add("vsblnTrocaHorario", value);
            }
        }

        #region Propriedades para seleçao de datas - Troca de horário
        /// <summary>
        ///     Propriedade blnTrocaDatasHorario - armazena as datas para troca de horário
        /// </summary>
        /// <history>
        ///     [haguiar_2] created 02/12/2010 
        /// </history>
        private bool blnTrocaDatasHorario
        {
            get
            {
                if (this.ViewState["vsblnTrocaDatasHorario"] == null)
                {
                    this.ViewState.Add("vsblnTrocaDatasHorario", false);
                }

                return Convert.ToBoolean(this.ViewState["vsblnTrocaDatasHorario"]);
            }

            set
            {
                this.ViewState.Add("vsblnTrocaDatasHorario", value);
            }
        }
        #endregion

        /// <summary>
        ///     Propriedade blnApresentaMsgTrocaHorario utilizada para Troca de horário
        /// </summary>
        /// <history>
        ///     [haguiar_2] created 25/11/2010 
        /// </history>
        private bool blnApresentaMsgTrocaHorario
        {
            get
            {
                if (this.ViewState["vsblnApresentaMsgTrocaHorario"] == null)
                {
                    this.ViewState.Add("vsblnApresentaMsgTrocaHorario", false);
                }

                return Convert.ToBoolean(this.ViewState["vsblnApresentaMsgTrocaHorario"]);
            }

            set
            {
                this.ViewState.Add("vsblnApresentaMsgTrocaHorario", value);
            }
        }        
        #endregion

        /// <summary>
        ///     Propriedade blnNovoHorario utilizada para Troca de horário
        /// </summary>
        /// <history>
        ///     [haguiar_2] created 08/12/2010 
        /// </history>
        private bool blnNovoHorario
        {
            get
            {
                if (this.ViewState["vsblnNovoHorario"] == null)
                {
                    this.ViewState.Add("vsblnNovoHorario", false);
                }

                return Convert.ToBoolean(this.ViewState["vsblnNovoHorario"]);
            }

            set
            {
                this.ViewState.Add("vsblnNovoHorario", value);
            }
        }


        /// <summary>
        ///     Propriedade blnHorario utilizada para carregar horários
        /// </summary>
        /// <history>
        ///     [haguiar] created 29/11/2011 16:05
        /// </history>
        private bool blnHorario
        {
            get
            {
                if (this.ViewState["vsblnHorario"] == null)
                {
                    this.ViewState.Add("vsblnHorario", false);
                }

                return Convert.ToBoolean(this.ViewState["vsblnHorario"]);
            }

            set
            {
                this.ViewState.Add("vsblnHorario", value);
            }
        }        
        #endregion        
    }
}