using System;
using System.Collections.Generic;
using System.Text;
using SafWeb.Model.Escala;
using SafWeb.Model.Colaborador;
using System.Collections.ObjectModel;
using SafWeb.DataLayer.Escala;
using FrameWork.BusinessLayer.Utilitarios;
using System.Data;
using SafWeb.BusinessLayer.Filial;
using SafWeb.BusinessLayer.Regional;
using FrameWork.BusinessLayer.Usuarios;

namespace SafWeb.BusinessLayer.Escala
{
    /// ----------------------------------------------------------------------------- 
    /// Project : 
    /// Class : BLEscala
    /// ----------------------------------------------------------------------------- 
    /// <summary>  
    /// Implementação da classe BLEscala
    /// </summary> 
    /// <history> 
    ///     [cmarchi] created 7/1/2010
    /// </history> 
    /// ----------------------------------------------------------------------------- 
    public class BLEscala
    {
        #region Alterar

        #region AlterarHorariosColaboradoresEscala
        /// <summary>
        /// Altera os horários dos colaboradores de uma escalação
        /// </summary>
        /// <param name="pcolHorarioColaboradores">Lista dos Horarios dos Colaboradores</param>
        /// <history>
        ///     [cmarchi] created 23/1/2010
        ///     [haguiar_2] modify 02/12/2010
        ///     excluir troca de horários
        ///     [haguiar_2] modify 20/12/2010
        ///     horário flex deve ser incluido com data atual + 08:00
        ///     [haguiar_8829] modify 06/07/2011 17:10
        ///     hora extra deve ser incluída com hora escolhida
        ///     [haguiar] modify 02/01/2012 15:12
        ///     adicionar codigo legado
        /// </history>
        public void AlterarHorariosColaboradoresEscala(Collection<HorarioColaboradores> pcolHorarioColaboradores)
        {
            DLEscala objDLEscala = new DLEscala();

            try
            {
                if (pcolHorarioColaboradores.Count > 0)
                {
                    objDLEscala.BeginTransaction();

                    int intTamanho = pcolHorarioColaboradores.Count;
                    int intTamanhoDatas = 0;

                    //percorre todas os horarios
                    for (int i = 0; i < intTamanho; i++)
                    {
                        intTamanhoDatas = pcolHorarioColaboradores[i].DatasColaboradores.Count;

                        //percorre todas as datas
                        for (int j = 0; j < intTamanhoDatas; j++)
                        {
                            //cria o objeto para atualizar e insere os valores 
                            EscalacaoColaboradoresData objEscColData = new EscalacaoColaboradoresData();

                            /*
                            if (pcolHorarioColaboradores[i].HorarioFlex)
                            {
                                objEscColData.DataEscalacao = Convert.ToDateTime(pcolHorarioColaboradores[i].DatasColaboradores[j].ToShortDateString());
                                objEscColData.DataEscalacao.AddHours(8);

                                //objEscColData.DataEscalacao = pcolHorarioColaboradores[i].DatasColaboradores[j];
                            }
                            else
                            {*/
                                objEscColData.DataEscalacao = pcolHorarioColaboradores[i].DatasColaboradores[j];
                            //}

                            objEscColData.IdEscalacao = pcolHorarioColaboradores[i].IdEscalacao;
                            objEscColData.IdColaboradores = pcolHorarioColaboradores[i].CodigosColaboradores;

                            objEscColData.Compensado = pcolHorarioColaboradores[i].Compensado;
                            objEscColData.Licenca = pcolHorarioColaboradores[i].Licenca;
                            objEscColData.Folga = pcolHorarioColaboradores[i].Folga;

                            objEscColData.HoraExtra = pcolHorarioColaboradores[i].HoraExtra;

                            objEscColData.HorarioFlex = pcolHorarioColaboradores[i].HorarioFlex;

                            objEscColData.ExcluirTrocaHorario = pcolHorarioColaboradores[i].ExcluirTrocaHorario;

                            //codigo legado ronda
                            objEscColData.CodLegado = pcolHorarioColaboradores[i].CodLegado;

                            //aualiza o horário dos colaboradores
                            objDLEscala.AlterarHorarioColaboradores(objEscColData);
                        }
                    }

                    objDLEscala.CommitTransaction();
                }
            }
            catch (Exception ex)
            {
                objDLEscala.RollBackTransaction();
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLEscala.Finalizar();
            }
        }
        #endregion

        #region AlterarSituacao
        /// <summary>
        /// Altera a Situação da Escalação.
        /// </summary>
        /// <param name="pobjEscalacao">Objeto Escalacao</param>
        ///<param name="pblnSituacao">Situação</param>
        /// <param name="pintIdUsuarioSolicitante">Usuário Solicitante</param>
        /// <history>
        ///     [cmarchi] created 13/1/2010
        ///     [haguiar] modify 26/09/2011 15:25
        ///     retornar se o registro foi ativado ou nao
        /// </history>
        public int AlterarSituacao(int pintIdEscalacao, bool pblnSituacao, int pintIdUsuarioSolicitante)
        {
            DLEscala objDLEscala = new DLEscala();
            int intRetorno = 0;

            try
            {
                intRetorno = objDLEscala.AlterarSituacao(pintIdEscalacao, pblnSituacao, pintIdUsuarioSolicitante);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLEscala.Finalizar();
            }

            return intRetorno;
        }
        #endregion

        #region AlterarSituacao Código Escala Ronda
        /// <summary>
        /// Altera a Situação do código do horário da escala.
        /// </summary>
        /// <param name="pintIdEscala">Id Escala</param>
        ///<param name="pblnSituacao">Situação</param>
        /// <returns>Id Escala</returns>
        /// <history>
        ///     [haguiar] created 06/01/2012 14:32
        ///     retornar se o registro foi ativado ou nao
        /// </history>
        public int AlterarSituacaoCodEscala(int pintIdEscala, bool pblnSituacao)
        {
            DLEscala objDLEscala = new DLEscala();
            int intRetorno = 0;

            try
            {
                intRetorno = objDLEscala.AlterarSituacaoCodEscala(pintIdEscala, pblnSituacao);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLEscala.Finalizar();
            }

            return intRetorno;
        }
        #endregion

        #region AlterarSituacao Código Horario Ronda
        /// <summary>
        /// Altera a Situação do código do horário da escala.
        /// </summary>
        /// <param name="pintIdHorario">Id Horario</param>
        ///<param name="pblnSituacao">Situação</param>
        /// <returns>Id Escala</returns>
        /// <history>
        ///     [haguiar] created 10/01/2012 10:45
        ///     retornar se o registro foi ativado ou nao
        /// </history>
        public int AlterarSituacaoCodHorario(int pintIdHorario, bool pblnSituacao)
        {
            DLEscala objDLEscala = new DLEscala();
            int intRetorno = 0;

            try
            {
                intRetorno = objDLEscala.AlterarSituacaoCodHorario(pintIdHorario, pblnSituacao);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLEscala.Finalizar();
            }

            return intRetorno;
        }
        #endregion

        #region Alterar Registro Código da escala Ronda
        /// <summary>
        /// Altera o registro do código do horário da escala Ronda
        /// </summary>
        /// <param name="pobjHorarioLegado">Objeto HorarioLegado</param>
        /// <returns>Id Escala</returns>
        /// <history>
        ///     [haguiar] created 09/01/2012 10:04
        ///     retornar se o registro foi alterado ou nao
        /// </history>
        public int AlterarHorarioEscala(HorarioLegado pobjHorarioLegado)
        {
            DLEscala objDLEscala = new DLEscala();
            int intRetorno = 0;

            try
            {
                intRetorno = objDLEscala.AlterarHorarioEscala(pobjHorarioLegado);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLEscala.Finalizar();
            }

            return intRetorno;
        }
        #endregion

        #region Alterar Registro Código Horario
        /// <summary>
        /// Altera o registro de um código de horário Ronda
        /// </summary>
        /// <param name="pobjHorarioLegado">Objeto HorarioLegado</param>
        /// <returns>Id Horario</returns>
        /// <history>
        ///     [haguiar] created 10/01/2012 10:49
        /// </history>
        public int AlterarRegistroHorario(HorarioLegado pobjHorarioLegado)
        {
            DLEscala objDLEscala = new DLEscala();
            int intRetorno = 0;

            try
            {
                intRetorno = objDLEscala.AlterarRegistroHorario(pobjHorarioLegado);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLEscala.Finalizar();
            }

            return intRetorno;
        }
        #endregion

        #region ExcluirStatusPendente
        /// <summary>
        /// Exclui status pendente da Escalação.
        /// </summary>
        /// <param name="pintIdEscalacao">Id Escalacao</param>
        /// <history>
        ///     [haguiar] created 29/10/2010
        /// </history>
        public void ExcluirStatusPendente(int pintIdEscalacao)
        {
            DLEscala objDLEscala = new DLEscala();

            try
            {
                objDLEscala.ExcluirStatusPendente(pintIdEscalacao, Convert.ToInt32(BLAcesso.IdUsuarioLogado()));
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLEscala.Finalizar();
            }
        }
        #endregion
        #endregion

        #region Importar Escalação Ronda

        /// <summary>
        ///      Importa escalações do Ronda
        /// </summary>
        /// <param name="pintId_EscalDpto">Id da escala departamental</param>
        /// <param name="pdtDe_Periodo_ini">De - periodo inicial</param>
        /// <param name="pdtDe_Periodo_Fim">De - periodo final</param>
        /// <param name="pdtPara_Periodo_ini">Para - periodo inicial</param>
        /// <param name="pdtPara_Periodo_Fim">Para - periodo final</param>
        /// <param name="pintId_TipoSolicitacao">Id do tipo da solicitacao</param>
        /// <param name="pintUsu_N_Codigo">Usuário solicitante</param>
        /// <param name="pblnFlg_HoraExtra">Flg para hora extra</param>
        /// <history>
        ///      [haguiar] created 11/11/2011 11:36
        /// </history>
        /// 
        public int ImportarEscalacaoRonda(int pintId_EscalaDpto,
            DateTime pdtDe_Periodo_ini, DateTime pdtDe_Periodo_Fim,
            DateTime pdtPara_Periodo_ini, DateTime pdtPara_Periodo_Fim,
            int pintId_TipoSolicitacao, int pintUsu_N_Codigo, bool pblnFlg_HoraExtra)
        {
            DLEscala objDLEscala = new DLEscala();
            int intRetorno = 0;

            try
            {
                intRetorno = objDLEscala.ImportarEscalacaoRonda(
                                                                pintId_EscalaDpto,
                                                                pdtDe_Periodo_ini,
                                                                pdtDe_Periodo_Fim,
                                                                pdtPara_Periodo_ini,
                                                                pdtPara_Periodo_Fim,
                                                                pintId_TipoSolicitacao,
                                                                pintUsu_N_Codigo,
                                                                pblnFlg_HoraExtra);

                return intRetorno;
            }
            catch (Exception ex)
            {
                objDLEscala.Finalizar();

                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLEscala.Finalizar();
            }
        }
        #endregion

        #region Importar Escalação CREW

        /// <summary>
        ///      Importa escalações do CREW
        /// </summary>
        /// <param name="pstrIdEscalacao">Id das Escalações</param>
        /// <param name="pdecUsuarioAprovador">Id do Usuário Aprovador</param>
        /// <history>
        ///      [haguiar_SDM9004] created 12/08/2011 17:49
        ///      [haguiar] modify 23/03/2012 16:40
        /// </history>
        public int ImportarEscalacaoCREW(int pintUSU_N_Codigo, int pintIdTipo_Solicitacao, int pintId_Filial,
                        DateTime pdatDataInicio, DateTime pdatDataFim, bool pblnFlg_HoraExtra)
        {

            DLEscala objDLEscala = new DLEscala();

                Collection<EscalaColaboradoresCrew> colEscalaColaboradoresCrew = objDLEscala.ListarHorarioColaboradoresCrew(pintUSU_N_Codigo, pintIdTipo_Solicitacao, pdatDataInicio);

                if (colEscalaColaboradoresCrew.Count == 0)
                {
                    return -1;
                }

                Collection<HorarioColaboradores> colHorarioColaboradores = new Collection<HorarioColaboradores>();
                Collection<JornadaColaboradores> colJornadaColaboradores = new Collection<JornadaColaboradores>();

                HorarioColaboradores objHorarioColaboradores;
                JornadaColaboradores objJornadaColaboradores;

                int intId_Escalacao = 0;
                int intId_EscalaDpto = 0;
                Collection<DateTime> colPeriodoEscala = new Collection<DateTime>();
            var datasSabado = new Collection<DateTime>();
            var datasDomingo = new Collection<DateTime>();

                StringBuilder strColaboradores = new StringBuilder();
                StringBuilder strTodosColaboradores = new StringBuilder();

                StringBuilder strJornadaColaboradores_8_48 = new StringBuilder();
                StringBuilder strJornadaColaboradores_7_20 = new StringBuilder();
                DateTime pDatColaborador = pdatDataInicio;
                DateTime pDatFimColaborador = pdatDataFim;

                objHorarioColaboradores = new HorarioColaboradores();

            //prepara collection datas no período
            if (pintIdTipo_Solicitacao.Equals(7))
            {
                for (DateTime dt = pDatColaborador; dt <= pDatFimColaborador; dt = dt.AddDays(1))
                {

                    if (dt.DayOfWeek == DayOfWeek.Saturday)
                        datasSabado.Add(dt);
                    else if (dt.DayOfWeek == DayOfWeek.Sunday)
                        datasDomingo.Add(dt);
                    else
                    {
                        //Pegar o primeiro dia da semana no periodo para criação dos horarios semanais
                        if (colPeriodoEscala.Count == 0)
                            colPeriodoEscala.Add(dt);
                    }
                }
            }
            else
            {
                colPeriodoEscala.Add(pDatColaborador);
            }

                if (colEscalaColaboradoresCrew.Count > 0)
                {
                    if (!colEscalaColaboradoresCrew[0].COD_FUNC.Equals(0))
                    {
                        if (!colEscalaColaboradoresCrew[0].COD_ESCALA.Equals(0))
                        {
                            objHorarioColaboradores.CodLegado = colEscalaColaboradoresCrew[0].COD_ESCALA;
                            objHorarioColaboradores.HorarioColaborador = colEscalaColaboradoresCrew[0].DES_HOR;
                            objHorarioColaboradores.IdEscalacao = intId_Escalacao;

                            switch (objHorarioColaboradores.CodLegado)
                            {
                                case 95:
                                    objHorarioColaboradores.HorarioFlex = true;
                                    objHorarioColaboradores.HorarioColaborador = colEscalaColaboradoresCrew[0].DES_HOR;
                                    break;
                                case 9998:
                                    objHorarioColaboradores.Compensado = true;
                                    objHorarioColaboradores.HorarioColaborador = "00:00";
                                    break;

                                case 9999:
                                    objHorarioColaboradores.Folga = true;
                                    objHorarioColaboradores.HorarioColaborador = "00:00";
                                    break;
                            }

                            pDatColaborador = pdatDataInicio;
                            pDatFimColaborador = pdatDataFim;

                            if (!objHorarioColaboradores.HorarioColaborador.Equals(string.Empty))
                            {
                                pDatColaborador = pDatColaborador.AddHours(Convert.ToDouble(objHorarioColaboradores.HorarioColaborador.Substring(0, 2)));
                                pDatColaborador = pDatColaborador.AddMinutes(Convert.ToDouble(objHorarioColaboradores.HorarioColaborador.Substring(3, 2)));

                                pDatFimColaborador = pDatFimColaborador.AddHours(Convert.ToDouble(objHorarioColaboradores.HorarioColaborador.Substring(0, 2)));
                                pDatFimColaborador = pDatFimColaborador.AddMinutes(Convert.ToDouble(objHorarioColaboradores.HorarioColaborador.Substring(3, 2)));
                            }

                        //colPeriodoEscala = new Collection<DateTime>();

                            objHorarioColaboradores.DatasColaboradores = colPeriodoEscala;

                            if (colEscalaColaboradoresCrew[0].COD_JOR.Equals(1))
                            {
                            if (!strJornadaColaboradores_7_20.ToString().Contains(colEscalaColaboradoresCrew[0].COD_FUNC.ToString()))
                                strJornadaColaboradores_7_20.Append(colEscalaColaboradoresCrew[0].COD_FUNC.ToString() + ",");
                            }
                            else
                            {
                            if (!strJornadaColaboradores_8_48.ToString().Contains(colEscalaColaboradoresCrew[0].COD_FUNC.ToString()))
                                strJornadaColaboradores_8_48.Append(colEscalaColaboradoresCrew[0].COD_FUNC.ToString() + ",");
                            }

                        if (!strColaboradores.ToString().Contains(colEscalaColaboradoresCrew[0].COD_FUNC.ToString()))
                            strColaboradores.Append(colEscalaColaboradoresCrew[0].COD_FUNC.ToString() + ",");

                        if (!strTodosColaboradores.ToString().Contains(colEscalaColaboradoresCrew[0].COD_FUNC.ToString()))
                            strTodosColaboradores.Append(colEscalaColaboradoresCrew[0].COD_FUNC.ToString() + ",");
                        }
                    }
                }

                //prepara colaboradores
                int intPosicaoInicial = 1;

                for (int i = intPosicaoInicial; i < colEscalaColaboradoresCrew.Count; i++)
                {
                if (!strTodosColaboradores.ToString().Contains(colEscalaColaboradoresCrew[i].COD_FUNC.ToString()))
                {
                        //insere primeiro indice

                        if (!colEscalaColaboradoresCrew[i].COD_FUNC.Equals(0))
                        {
                            if (!colEscalaColaboradoresCrew[i].COD_ESCALA.Equals(0))
                            {
                                //Jornadas
                                if (colEscalaColaboradoresCrew[i].COD_JOR.Equals(1))
                                {
                                if (!strJornadaColaboradores_7_20.ToString().Contains(colEscalaColaboradoresCrew[i].COD_FUNC.ToString()))
                                    strJornadaColaboradores_7_20.Append(colEscalaColaboradoresCrew[i].COD_FUNC.ToString() + ",");
                                }
                                else
                                {
                                if (!strJornadaColaboradores_8_48.ToString().Contains(colEscalaColaboradoresCrew[i].COD_FUNC.ToString()))
                                    strJornadaColaboradores_8_48.Append(colEscalaColaboradoresCrew[i].COD_FUNC.ToString() + ",");
                                }


                                if (objHorarioColaboradores.CodLegado == colEscalaColaboradoresCrew[i].COD_ESCALA)
                                {
                                if (!strColaboradores.ToString().Contains(colEscalaColaboradoresCrew[i].COD_FUNC.ToString()))
                                    strColaboradores.Append(colEscalaColaboradoresCrew[i].COD_FUNC.ToString() + ",");

                                if (!strTodosColaboradores.ToString().Contains(colEscalaColaboradoresCrew[i].COD_FUNC.ToString()))
                                    strTodosColaboradores.Append(colEscalaColaboradoresCrew[i].COD_FUNC.ToString() + ",");
                                }
                                else
                                {
                                    if (strColaboradores.Length > 0)
                                    {
                                        strColaboradores.Remove(strColaboradores.Length - 1, 1);
                                    }

                                    objHorarioColaboradores.CodigosColaboradores = strColaboradores.ToString();
                                    colHorarioColaboradores.Add(objHorarioColaboradores);

                                //colHorarioColaboradores.Add(objHorarioColaboradores);

                                    //novo objeto
                                    objHorarioColaboradores = new HorarioColaboradores();
                                    strColaboradores = new StringBuilder();

                                    objHorarioColaboradores.CodLegado = colEscalaColaboradoresCrew[i].COD_ESCALA;
                                    objHorarioColaboradores.HorarioColaborador = colEscalaColaboradoresCrew[i].DES_HOR;
                                    objHorarioColaboradores.IdEscalacao = intId_Escalacao;

                                    //objHorarioColaboradores.DatasColaboradores = colPeriodoEscala;

                                    switch (objHorarioColaboradores.CodLegado)
                                    {
                                        case 95:
                                            objHorarioColaboradores.HorarioFlex = true;
                                            objHorarioColaboradores.HorarioColaborador = colEscalaColaboradoresCrew[i].DES_HOR;

                                            break;
                                        case 9998:
                                            objHorarioColaboradores.Compensado = true;
                                            objHorarioColaboradores.HorarioColaborador = "00:00";

                                            break;

                                        case 9999:
                                            objHorarioColaboradores.Folga = true;
                                            objHorarioColaboradores.HorarioColaborador = "00:00";

                                            break;
                                    }

                                    pDatColaborador = pdatDataInicio;
                                    pDatFimColaborador = pdatDataFim;

                                    if (!objHorarioColaboradores.HorarioColaborador.Equals(string.Empty))
                                    {
                                        pDatColaborador = pDatColaborador.AddHours(Convert.ToDouble(objHorarioColaboradores.HorarioColaborador.Substring(0, 2)));
                                        pDatColaborador = pDatColaborador.AddMinutes(Convert.ToDouble(objHorarioColaboradores.HorarioColaborador.Substring(3, 2)));

                                        pDatFimColaborador = pDatFimColaborador.AddHours(Convert.ToDouble(objHorarioColaboradores.HorarioColaborador.Substring(0, 2)));
                                        pDatFimColaborador = pDatFimColaborador.AddMinutes(Convert.ToDouble(objHorarioColaboradores.HorarioColaborador.Substring(3, 2)));
                                    }

                                //colPeriodoEscala = new Collection<DateTime>();

                                    objHorarioColaboradores.DatasColaboradores = colPeriodoEscala;

                                if (!strColaboradores.ToString().Contains(colEscalaColaboradoresCrew[i].COD_FUNC.ToString()))
                                    strColaboradores.Append(colEscalaColaboradoresCrew[i].COD_FUNC.ToString() + ",");

                                if (!strTodosColaboradores.ToString().Contains(colEscalaColaboradoresCrew[i].COD_FUNC.ToString()))
                                    strTodosColaboradores.Append(colEscalaColaboradoresCrew[i].COD_FUNC.ToString() + ",");
                                }
                            }
                        }
                }
            }

            //TEMP CAIO

            if (strColaboradores.Length > 0)
            {
                strColaboradores.Remove(strColaboradores.Length - 1, 1);
            }

            objHorarioColaboradores.CodigosColaboradores = strColaboradores.ToString();
            colHorarioColaboradores.Add(objHorarioColaboradores);

            //TEMP CAIO

                //Verificar escala departamental CREW
                BLEscalaDepartamental objBLEscalaDepartamental = new BLEscalaDepartamental();
                intId_EscalaDpto = objBLEscalaDepartamental.ObterEscalaDptoCrew(pintId_Filial);

                if (intId_EscalaDpto.Equals(0))
                {
                    //incluir escala departamental com parametro CREW

                    Collection<Colaborador> objcolColaboradores = new Collection<Colaborador>();

                    string[] arrColaboradores = strTodosColaboradores.ToString().Remove(strTodosColaboradores.Length - 1, 1).Split(',');
                    int intTotalColaboradores = arrColaboradores.Length;

                    for (int i = 0; i < intTotalColaboradores; i++)
                    {
                        Colaborador objColaborador = new Colaborador();
                        objColaborador.IdColaborador = Convert.ToInt32(arrColaboradores[i]);

                        objcolColaboradores.Add(objColaborador);
                    }

                    Collection<HorarioEscala> colHorarios = objBLEscalaDepartamental.GerarHorarios();
                    EscalaDepartamental gobjEscalaDepartamental = new EscalaDepartamental();

                    BLFilial objBLFilial = new BLFilial();
                    BLRegional objRegional = new BLRegional();

                    Model.Filial.Filial objModelFilial = new Model.Filial.Filial();

                    objModelFilial = objBLFilial.Obter(pintId_Filial);

                    gobjEscalaDepartamental.ObjRegional = new Model.Regional.Regional();
                    gobjEscalaDepartamental.ObjRegional.IdRegional = objModelFilial.IdRegional;

                    gobjEscalaDepartamental.ObjFilial = new Model.Filial.Filial();
                    gobjEscalaDepartamental.ObjFilial.IdFilial = Convert.ToInt32(pintId_Filial);

                    gobjEscalaDepartamental.ObjPeriodicidade = new Periodicidade();
                    gobjEscalaDepartamental.ObjPeriodicidade.IdPeriodicidade = 2; //fixo semanal

                    gobjEscalaDepartamental.DescricaoEscalaDpto = "CREW - " + objModelFilial.AliasFilial;
                    gobjEscalaDepartamental.IdUsuarioAlteracao = pintUSU_N_Codigo;

                    gobjEscalaDepartamental.Situacao = true;
                    gobjEscalaDepartamental.Flg_ReplicaRH = true;
                    gobjEscalaDepartamental.Flg_EscalaCrew = true;

            try
            {
                        intId_EscalaDpto = objBLEscalaDepartamental.Inserir(gobjEscalaDepartamental, colHorarios, objcolColaboradores);
                    }
                    catch (Exception ex)
                {
                        BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                        throw;
                    }
                }

                //insere escalacao
                Escalacao objEscala = new Escalacao();
                objEscala.IdEscalaDepartamental = intId_EscalaDpto;
                objEscala.IdTipoSolicitacao = pintIdTipo_Solicitacao;
                objEscala.IdUsuarioSolicitante = pintUSU_N_Codigo;
                objEscala.IdFilial = pintId_Filial;
                objEscala.DataInicioPeriodo = pdatDataInicio;



                if (pintIdTipo_Solicitacao == 9)
                    {
                    //troca de horário
                    objEscala.DataFinalPeriodo = pdatDataInicio;
                    }
                else
                {
                    //escala
                    objEscala.DataFinalPeriodo = pdatDataFim;
                }

                try
                {
                    intId_Escalacao = objDLEscala.Inserir(objEscala, strTodosColaboradores.ToString());

                if (intId_Escalacao > 0)
                    objDLEscala.CrwOperacaoLog(pintUSU_N_Codigo, pdatDataInicio, intId_Escalacao);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLEscala.Finalizar();
            }

                foreach (HorarioColaboradores col in colHorarioColaboradores)
                {
                    col.IdEscalacao = intId_Escalacao;
                    col.HoraExtra = pblnFlg_HoraExtra;
        }

                //insere jornada dos colaboradores
                BLJornada objBLJornada = new BLJornada();
                colJornadaColaboradores.Clear();

                if (strJornadaColaboradores_7_20.Length > 0)
                {
                    strJornadaColaboradores_7_20.Remove(strJornadaColaboradores_7_20.Length - 1, 1);

                    objJornadaColaboradores = new JornadaColaboradores();
                    objJornadaColaboradores.CodigosColaboradores = strJornadaColaboradores_7_20.ToString();
                    objJornadaColaboradores.IdJornada = 1;
                    objJornadaColaboradores.IdEscalacao = intId_Escalacao;

                    colJornadaColaboradores.Add(objJornadaColaboradores);
                }

                if (strJornadaColaboradores_8_48.Length > 0)
                {
                    strJornadaColaboradores_8_48.Remove(strJornadaColaboradores_8_48.Length - 1, 1);

                    objJornadaColaboradores = new JornadaColaboradores();
                    objJornadaColaboradores.CodigosColaboradores = strJornadaColaboradores_8_48.ToString();
                    objJornadaColaboradores.IdJornada = 2;
                    objJornadaColaboradores.IdEscalacao = intId_Escalacao;

                    colJornadaColaboradores.Add(objJornadaColaboradores);
                }

                try
                {
                    if (colJornadaColaboradores.Count > 0)
                        objBLJornada.Inserir(colJornadaColaboradores);
                }
                catch (Exception ex)
                {
                    BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                    throw;
                }

            var temp = (new List<HorarioColaboradores>(colHorarioColaboradores)).FindAll(m => m.CodigosColaboradores.Contains("27329"));

            try
            {
                //insere horário dos colaboradores
                InserirHorariosColaboradoresEscala(colHorarioColaboradores, false);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }

                return intId_Escalacao;


        }
        #endregion

        #region Aprovar Escalação

        /// <summary>
        ///      Aprova as Escalações
        /// </summary>
        /// <param name="pstrIdEscalacao">Id das Escalações</param>
        /// <param name="pdecUsuarioAprovador">Id do Usuário Aprovador</param>
        /// <history>
        ///      [cmarchi] created 28/01/2010
        /// </history>
        public void AprovarEscalacao(string pstrIdEscalacao, decimal pdecUsuarioAprovador)
        {
            DLEscala objDLEscala = new DLEscala();
            string[] arrIdEscalacao = pstrIdEscalacao.Split(',');

            int intIdUsuarioAprovador = Convert.ToInt32(pdecUsuarioAprovador);
            int intTotal = 0;
            try
            {
                if (arrIdEscalacao != null)
                {
                    intTotal = arrIdEscalacao.Length;

                    for (int i = 0; i < intTotal; i++)
                    {
                        objDLEscala.AprovarEscalacao(Convert.ToInt32(arrIdEscalacao[i]),
                        intIdUsuarioAprovador);
                    }
                }
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLEscala.Finalizar();
            }
        }
        #endregion

        #region Editar
        /// <summary>
        /// Edita os Colaboradores da Escala.
        /// </summary>
        /// <param name="pobjEscalacao">Objeto Escalacao</param>
        /// <param name="pcolColaboradores">Lista de Colaboradores</param>
        /// <returns>Id_EscalaDepartamental</returns>
        /// <history>
        ///     [cmarchi] created 13/1/2010
        /// </history>
        public int Editar(Escalacao pobjEscalacao, Collection<Colaborador> pcolColaboradores)
        {
            if (pcolColaboradores != null)
            {
                int intTamanho = pcolColaboradores.Count;

                StringBuilder strColaboradores = new StringBuilder();
                DLEscala objDLEscala = new DLEscala();

                if (intTamanho > 0)
                {
                    //formatando os colaboradores da escala
                    for (int i = 0; i < intTamanho - 1; i++)
                    {
                        strColaboradores.Append(pcolColaboradores[i].IdColaborador.ToString() + ",");
                    }

                    //insere o último colaborador da escala
                    strColaboradores.Append(pcolColaboradores[intTamanho - 1].IdColaborador.ToString());
                }

                try
                {
                    return objDLEscala.Editar(pobjEscalacao, strColaboradores.ToString());
                }
                catch (Exception ex)
                {
                    BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                    throw;
                }
                finally
                {
                    objDLEscala.Finalizar();
                }
            }
            else
                return -1;
        }
        #endregion

        #region ExcluirColaborador
        /// <summary>
        /// Exclui colaborador(es).
        /// </summary>
        /// <param name="pcolHorarioColaboradores">Lista de Horários dos Colaboradores</param>
        /// <param name="pintIndice">Índice</param>
        /// <param name="pstrHorario">Horário</param>
        /// <param name="parrIdColaboradores">Id dos Colaboradoradores</param>
        /// <history>
        ///     [cmarchi] created 18/1/2010
        /// </history>
        public static void ExcluirColaborador(ref Collection<HorarioColaboradores> pcolHorarioColaboradores,
            int pintIndice, string pstrHorario, string[] parrIdColaboradores)
        {
            if (pintIndice >= 0)
            {
                if (pcolHorarioColaboradores[pintIndice].HorarioColaborador == pstrHorario)
                {
                    string[] arrColaboradores = pcolHorarioColaboradores[pintIndice].NomesColaboradores.Split(',');
                    string[] arrIdColaboradores = pcolHorarioColaboradores[pintIndice].CodigosColaboradores.Split(',');

                    //verifica a quantidade de colaboradores no índice
                    if (arrColaboradores.Length == 1 && arrIdColaboradores.Length == 1)
                    {
                        pcolHorarioColaboradores.RemoveAt(pintIndice);
                    }
                    else if (arrColaboradores.Length > 1 && arrIdColaboradores.Length > 1 &&
                        arrColaboradores.Length == arrIdColaboradores.Length)
                    {
                        int intPosicao = -1;

                        StringBuilder strIdColaboradores = new StringBuilder();
                        StringBuilder strColaboradores = new StringBuilder();

                        for (int i = 0; i < parrIdColaboradores.Length; i++)
                        {
                            intPosicao = Array.IndexOf(arrIdColaboradores, parrIdColaboradores[i]);

                            //verifica se encontrou o id do colaborador
                            //e "remove" o id e o nome do colaborador não encontrado
                            if (intPosicao >= 0)
                            {
                                arrColaboradores[intPosicao] = "-1";
                                arrIdColaboradores[intPosicao] = "-1";

                                intPosicao = -1;
                            }
                        }

                        //copia os item não "excluídos"
                        for (int i = 0; i < arrIdColaboradores.Length; i++)
                        {
                            if (arrIdColaboradores[i] != "-1" &&
                                arrIdColaboradores[i] != arrColaboradores[i])
                            {
                                strIdColaboradores.Append(arrIdColaboradores[i] + ",");
                                strColaboradores.Append(arrColaboradores[i] + ",");
                            }
                        }

                        //verifica se removeu todos os colaboradores de um horário
                        if (strIdColaboradores.Length > 0 && strColaboradores.Length > 0)
                        {
                            //remove os "," das strings
                            strIdColaboradores.Remove(strIdColaboradores.Length - 1, 1);
                            strColaboradores.Remove(strColaboradores.Length - 1, 1);

                            //insere os ids e nome dos colaboradores
                            pcolHorarioColaboradores[pintIndice].CodigosColaboradores =
                                strIdColaboradores.ToString();

                            pcolHorarioColaboradores[pintIndice].NomesColaboradores =
                                strColaboradores.ToString();
                        }
                        else
                        {
                            pcolHorarioColaboradores.RemoveAt(pintIndice);
                        }
                    }
                }
            }
        }
        #endregion

        #region FormatarDatasSelecionadas

        /// <summary>
        ///      Formata as Datas selecionadas
        /// </summary>
        /// <param name="pcolDatas">Datas selecionadas</param>
        /// <returns>Datas selecionadas formatada</returns>
        /// <history>
        ///      [cmarchi] created 18/02/2010
        ///      [cmarchi] modify 22/2/2010
        ///      [haguiar] modify 22/10/2010
        /// </history>      
        public static string FormatarDatasSelecionadas(List<DateTime> pcolDatas)
        {
            StringBuilder strDatasFormatada = new StringBuilder();

            int intDia = 0;
            int intMes = 0;
            int intAno = 0;

            if (pcolDatas.Count > 0)
            {
                //organiza a lista de datas
                pcolDatas.Sort(delegate(DateTime p1, DateTime p2) { return p1.CompareTo(p2); });

                for (int i = 0; i < pcolDatas.Count - 1; i++)
                {
                    intDia = pcolDatas[i].Date.Day;
                    intMes = pcolDatas[i].Date.Month;
                    intAno = pcolDatas[i].Date.Year;

                    if (i != 0)
                    {
                        if (i + 1 <= pcolDatas.Count)
                        {
                            //verifica se mudou o mes
                            if (intMes != pcolDatas[i + 1].Date.Month)
                            {
                                strDatasFormatada.Append(" " + String.Format("{0:dd/MM/yyyy}", new DateTime(intAno, intMes, intDia)) + ",");
                            }
                            else
                            {
                                strDatasFormatada.Append(" " + String.Format("{0:00}", intDia) + ",");
                            }
                        }
                        else
                        {
                            //verifica se mudou o mes
                            if (intMes != pcolDatas[i - 1].Date.Month)
                            {
                                strDatasFormatada.Append(" " + String.Format("{0:dd/MM/yyyy}", new DateTime(intAno, intMes, intDia)) + ",");
                            }
                            else
                            {
                                strDatasFormatada.Append(" " + String.Format("{0:00}", intDia) + ",");
                        }
                    }
                    }
                    else
                    {
                        strDatasFormatada.Append(" " + String.Format("{0:00}", intDia) + ",");
                }
                }

                intDia = pcolDatas[pcolDatas.Count - 1].Date.Day;
                intMes = pcolDatas[pcolDatas.Count - 1].Date.Month;
                intAno = pcolDatas[pcolDatas.Count - 1].Date.Year;

                //dia/mes/ano
                strDatasFormatada.Append(" " + String.Format("{0:dd/MM/yyyy}", new DateTime(intAno, intMes, intDia)));
            }

            return strDatasFormatada.ToString();
        }

        /// <summary>
        ///      Formata as Datas selecionadas
        /// </summary>
        /// <param name="pdatData">Data</param>
        /// <returns>Datas selecionadas formatada</returns>
        /// <history>
        ///      [cmarchi] created 22/2/2010
        ///      [haguiar] modify 08/11/2010
        /// </history>
        public static string FormatarDatasSelecionadas(DateTime pdatData)
        {
            StringBuilder strDatasFormatada = new StringBuilder();

            /*             
            int intDia = 0;
            int intMes = 0;
            int intAno = 0;

            intDia = pdatData.Day;
            intMes = pdatData.Month;
            intAno = pdatData.Year;
            */

            /*
            if (intDia > 9)
                strDatasFormatada.Append(" " + intDia.ToString());
            else
                strDatasFormatada.Append(" 0" + intDia.ToString());

            if (intMes > 9)
                strDatasFormatada.Append("/" + intMes.ToString() + "/" + intAno.ToString());
            else
                strDatasFormatada.Append("/" + intMes.ToString() + "/" + intAno.ToString());
            */

            strDatasFormatada.Append(" " + String.Format("{0:dd/MM/yyyy}", pdatData));

            return strDatasFormatada.ToString();
        }
        #endregion

        #region IndiceHorarioColaborador
        /// <summary>
        /// Obtém o índice de um horário.
        /// </summary>
        /// <param name="pcolHorarioColaboradores">Coleção de Horários dos Colaboradores</param>
        /// <param name="strHorario">Horário dos Colaboradores</param>
        /// <returns>índice do objeto Horário</returns>
        /// <history>
        ///     [cmarchi] created 18/1/2010
        /// </history>
        public static int IndiceHorarioColaborador(Collection<HorarioColaboradores> pcolHorarioColaboradores, string strHorario)
        {
            if (pcolHorarioColaboradores != null && pcolHorarioColaboradores.Count > 0)
            {
                int intTamanho = pcolHorarioColaboradores.Count;

                for (int i = 0; i < intTamanho; i++)
                {
                    if (pcolHorarioColaboradores[i].HorarioColaborador == strHorario &&
                        !pcolHorarioColaboradores[i].Bloqueado)
                    {
                        return i;
                    }
                }
            }

            return -1;
        }
        #endregion

        #region Inserir

        #region Insere Código de horário
        /// <summary>
        /// Insere o código de um horário Ronda
        /// </summary>
        /// <param name="pobjHorarioLegado">Objeto HorarioLegado</param>
        /// <returns>Id Escala</returns>
        /// <history>
        ///     [haguiar] created 12/01/2012 09:17
        /// </history>
        public int InsereCodigoHorario(HorarioLegado pobjHorarioLegado)
        {
            DLEscala objDLEscala = new DLEscala();
            try
            {
                return objDLEscala.InsereCodigoHorario(pobjHorarioLegado);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLEscala.Finalizar();
            }
        }
        #endregion

        #region Insere Código de Escala
        /// <summary>
        /// Insere o código de uma escala Ronda
        /// </summary>
        /// <param name="pobjHorarioLegado">Objeto HorarioLegado</param>
        /// <returns>Id Escala</returns>
        /// <history>
        ///     [haguiar] created 11/01/2012 16:45
        /// </history>
        public int InsereCodigoEscala(HorarioLegado pobjHorarioLegado)
        {
            DLEscala objDLEscala = new DLEscala();
            try
            {
                return objDLEscala.InsereCodigoEscala(pobjHorarioLegado);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLEscala.Finalizar();
            }
        }
        #endregion

        #region InserirHrColabTrocaHorario
        /// <summary>
        /// Inclui as datas de troca de horário dos colaboradores selecionados
        /// </summary>
        /// <param name="pHorarioColaboradores">Horários dos colaboradores</param>
        /// <history>
        ///     [haguiar_2] created 30/11/2010
        ///     [haguiar_8829] modify 06/07/2011 14:54
        ///     incluir propriedade horaextra
        ///     [haguiar] modify 02/01/2012 17:44
        ///     incluir codlegado
        /// </history>
        public void InserirHrColabTrocaHorario(Collection<HorarioColaboradores> pHorarioColaboradores)
        {
            Collection<DataHorarioColaboradores> colDataHorCol = null;
            Collection<HorarioColaboradores> pRefHorarioColaboradores = new Collection<HorarioColaboradores>();
            HorarioColaboradores pHorarioColaborador;

            foreach (HorarioColaboradores pHorario in pHorarioColaboradores)
            {
                if (!pHorario.ExcluirTrocaHorario)
                {
                    pHorarioColaborador = new HorarioColaboradores();

                    pHorarioColaborador.Bloqueado = pHorario.Bloqueado;
                    pHorarioColaborador.CodigosColaboradores = pHorario.CodigosColaboradores;
                    pHorarioColaborador.Compensado = pHorario.Compensado;
                    pHorarioColaborador.DatasColaboradores = pHorario.DatasColaboradores;
                    pHorarioColaborador.Folga = pHorario.Folga;
                    pHorarioColaborador.HorarioColaborador = pHorario.HorarioColaborador;
                    pHorarioColaborador.IdEscalacao = pHorario.IdEscalacao;
                    pHorarioColaborador.Licenca = pHorario.Licenca;
                    pHorarioColaborador.NomesColaboradores = pHorario.NomesColaboradores;

                    pHorarioColaborador.CodLegado = pHorario.CodLegado;

                    pHorarioColaborador.ExcluirTrocaHorario = pHorario.ExcluirTrocaHorario;
                    pHorarioColaborador.HorarioFlex = pHorario.HorarioFlex;
                    pHorarioColaborador.HoraExtra = pHorario.HoraExtra;

                    pRefHorarioColaboradores.Add(pHorarioColaborador);
                }
            }

            try
            {
                if (pHorarioColaboradores.Count > 0)
                {

                    int intTamanho = pHorarioColaboradores.Count;
                    int intTamanhoDatas = 0;

                    //percorre todas os horarios
                    for (int i = 0; i < intTamanho; i++)
                    {
                        //datas com horários escalados
                        colDataHorCol = ObterDtHorColEscalacao(pHorarioColaboradores[i].IdEscalacao, null, null, null);

                        intTamanhoDatas = pHorarioColaboradores[i].DatasColaboradores.Count;

                        //percorre todas as datas
                        for (int j = 0; j < intTamanhoDatas; j++)
                        {
                            if (colDataHorCol != null && colDataHorCol.Count > 0)
                            {
                                foreach (DataHorarioColaboradores datHorCol in colDataHorCol)
                                {
                                    if (pRefHorarioColaboradores.Count > 0)
                                    {
                                        foreach (DateTime datTroca in pHorarioColaboradores[i].DatasColaboradores)
                                        {
                                            if (datTroca.ToShortDateString() == datHorCol.DataColaboradores.ToShortDateString())
                                            {
                                                //remove os colaboradores que já possuem uma data na lista
                                                string[] arrColaboradores = datHorCol.CodigosColaboradores.Split(',');

                                                int countHorarios = pRefHorarioColaboradores.Count;

                                                ExcluirColaborador(ref pRefHorarioColaboradores, 0, pHorarioColaboradores[i].HorarioColaborador, arrColaboradores);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        //nao existem mais colaboradores para comparar
                                        break;
                                    }
                                }
                            }
                        }
                    }

                    if (pRefHorarioColaboradores != null && pRefHorarioColaboradores.Count > 0)
                    {
                        InserirHorariosColaboradoresEscala(pRefHorarioColaboradores, false);
                    }
                }
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
            }
        }
        #endregion

        #region Inserir Colaboradores da Escala
        /// <summary>
        /// Insere Colaboradores da Escala e a Escala.
        /// </summary>
        /// <param name="pobjEscalacao">Objeto Escalacao</param>
        /// <param name="pcolColaboradores">Lista de Colaboradores</param>
        /// <returns>Id_EscalaDepartamental</returns>
        /// <history>
        ///     [cmarchi] created 7/1/2010
        ///     [haguiar_2] modify 24/11/2010
        ///     Permitir inserir escalacao com TipoSolicitacao = 9 (troca de horário)
        /// </history>
        public int Inserir(Escalacao pobjEscalacao, Collection<Colaborador> pcolColaboradores)
        {
            if (pcolColaboradores != null)
            {
                int intTamanho = pcolColaboradores.Count;

                StringBuilder strColaboradores = new StringBuilder();
                DLEscala objDLEscala = new DLEscala();

                if (intTamanho > 0)
                {
                    //formatando os colaboradores da escala
                    for (int i = 0; i < intTamanho - 1; i++)
                    {
                        strColaboradores.Append(pcolColaboradores[i].IdColaborador.ToString() + ",");
                    }

                    //insere o último colaborador da escala
                    strColaboradores.Append(pcolColaboradores[intTamanho - 1].IdColaborador.ToString());
                }

                try
                {
                    //troca de horário
                    if (pobjEscalacao.IdTipoSolicitacao == 0)
                    {
                        //solicitacao normal
                        pobjEscalacao.IdTipoSolicitacao = 7;
                    }
                    else
                    {
                        //troca de horario
                        pobjEscalacao.IdTipoSolicitacao = 9;
                    }

                    return objDLEscala.Inserir(pobjEscalacao, strColaboradores.ToString());
                }
                catch (Exception ex)
                {
                    BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                    throw;
                }
                finally
                {
                    objDLEscala.Finalizar();
                }
            }
            else
                return -1;
        }
        #endregion

        #region Inserir Aprovação de uma Escalação

        /// <summary>
        ///      Insere uma aprovação para a escala
        /// </summary>
        /// <param name="escalaAprovacao"></param>
        /// <history>
        ///      [cmarchi] created 28/01/2010
        /// </history>
        public void InserirEscalaAprovacao(EscalaAprovacao escalaAprovacao)
        {
            DLEscala objDLEscala = new DLEscala();

            try
            {
                objDLEscala.InserirEscalaAprovacao(escalaAprovacao, Convert.ToInt32(BLAcesso.IdUsuarioLogado()));

            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLEscala.Finalizar();
            }
        }
        #endregion

        #region InserirHorarioColaborador
        /// <summary>
        /// Insere horário de um Colaborador.
        /// </summary>
        /// <param name="pcolHorarioCol">Coleção de Horários dos Colaboradores</param>
        /// <param name="pintIndice">Índice</param>
        /// <param name="pobjHorariosEscala">Objeto horário dos Colaboradores</param>
        /// <history>
        ///     [cmarchi] created 18/1/2010
        /// </history>
        public void InserirHorarioColaborador(ref Collection<HorarioColaboradores> pcolHorarioCol, int pintIndice,
            HorarioColaboradores pobjHorarioColaboradores)
        {
            if (pcolHorarioCol != null)
            {
                if (pintIndice >= 0)
                {
                    string[] arrIdColaboradores = pcolHorarioCol[pintIndice].CodigosColaboradores.Split(',');

                    if (arrIdColaboradores != null && arrIdColaboradores.Length > 0)
                    {
                        Array.Sort(arrIdColaboradores);

                        int intPosicao = Array.BinarySearch(arrIdColaboradores, pobjHorarioColaboradores.CodigosColaboradores);

                        if (intPosicao < 0)
                        {
                            pcolHorarioCol[pintIndice].CodigosColaboradores += "," + pobjHorarioColaboradores.CodigosColaboradores;
                            pcolHorarioCol[pintIndice].NomesColaboradores += "," + pobjHorarioColaboradores.NomesColaboradores;
                        }
                    }
                    else
                    {
                        pcolHorarioCol[pintIndice].CodigosColaboradores = pobjHorarioColaboradores.CodigosColaboradores;
                        pcolHorarioCol[pintIndice].NomesColaboradores = pobjHorarioColaboradores.NomesColaboradores;
                    }
                }
                else
                {
                    pcolHorarioCol.Add(pobjHorarioColaboradores);
                }
            }
        }
        #endregion

        #region InserirHorariosColaboradoresEscala
        /// <summary>
        /// Insere os horários dos colaboradores de uma escalação
        /// </summary>
        /// <param name="pcolHorarioColaboradores">Lista dos Horarios dos Colaboradores</param>
        /// <history>
        ///     [cmarchi] created 19/1/2010
        ///     [haguiar_2] modify 04/12/2010
        ///     permitir incluir horário flex
        ///     [haguiar_8829] modify 06/07/2011 14:54
        ///     incluir propriedade horaextra
        ///     [haguiar] modify 02/12/2012 17:49
        ///     incluir codlegado
        /// </history>
        public void InserirHorariosColaboradoresEscala(Collection<HorarioColaboradores> pcolHorarioColaboradores, bool DeletarRegistrosAntigos)
        {
            DLEscala objDLEscala = new DLEscala();
            try
            {
                if (pcolHorarioColaboradores.Count > 0)
                {
                    objDLEscala.BeginTransaction();

                    if (DeletarRegistrosAntigos)
                    {
                        var IdEscalacao = 0;
                        var CodigosColaboradores = new List<string>();
                        foreach (HorarioColaboradores item in pcolHorarioColaboradores)
                        {
                            IdEscalacao = item.IdEscalacao;
                            CodigosColaboradores.Add(item.CodigosColaboradores);
                        }

                        objDLEscala.DeletaHorarioColaboradores(new EscalacaoColaboradoresData
                        {
                            IdEscalacao = IdEscalacao,
                            IdColaboradores = string.Join(",", CodigosColaboradores.ToArray())
                        });
                    }


                    int intTamanho = pcolHorarioColaboradores.Count;
                    int intTamanhoDatas = 0;

                    //percorre todas os horarios
                    for (int i = 0; i < intTamanho; i++)
                    {
                        intTamanhoDatas = pcolHorarioColaboradores[i].DatasColaboradores.Count;

                        //percorre todas as datas
                        for (int j = 0; j < intTamanhoDatas; j++)
                        {
                            //cria o objeto para inserir e insere os valores 
                            EscalacaoColaboradoresData objEscColData = new EscalacaoColaboradoresData();

                            objEscColData.DataEscalacao = pcolHorarioColaboradores[i].DatasColaboradores[j];
                            objEscColData.IdEscalacao = pcolHorarioColaboradores[i].IdEscalacao;
                            objEscColData.IdColaboradores = pcolHorarioColaboradores[i].CodigosColaboradores;

                            objEscColData.CodLegado = pcolHorarioColaboradores[i].CodLegado;

                            objEscColData.Compensado = pcolHorarioColaboradores[i].Compensado;
                            objEscColData.Licenca = pcolHorarioColaboradores[i].Licenca;
                            objEscColData.Folga = pcolHorarioColaboradores[i].Folga;

                            objEscColData.HorarioFlex = pcolHorarioColaboradores[i].HorarioFlex;
                            objEscColData.HoraExtra = pcolHorarioColaboradores[i].HoraExtra;

                            objEscColData.flgIniciaFolgando = pcolHorarioColaboradores[i].FlgIniciaFolgando;

                            //insere o horário dos colaboradores
                            if (objEscColData.IdColaboradores != "")
                            {
                                objDLEscala.InserirHorarioColaboradores(objEscColData);
                            }
                        }
                    }

                    objDLEscala.CommitTransaction();
                }
            }
            catch (Exception ex)
            {
                objDLEscala.RollBackTransaction();
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLEscala.Finalizar();
            }
        }
        #endregion

        #endregion

        #region Listar

        #region Listar CREW

        /// <summary>
        ///      Lista Escalas para importação do CREW
        /// </summary>
        /// <param name="pdecCodUsuAprov">Código do Usuário logado</param>
        /// <history>
        ///      [haguiar SDM 9004] created 10/08/2011 14:41
        /// </history>
        /// <returns>Lista Escalas para importação</returns>
        public Collection<EscalaImportarCrew> ListarEscalasImportarCREW(decimal pdecCodUsuAprov)
        {
            DLEscala objDLEscala = new DLEscala();

            try
            {
                return objDLEscala.ListarEscalasImportarCREW(pdecCodUsuAprov);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLEscala.Finalizar();
            }
        }
        #endregion

        #region Listar Escalas
        /// <summary>
        ///     Lista todas as Escalas de acordo com os parâmetros.
        /// </summary>
        /// <param name="pintNumeroEscala">Número da Escala</param>
        /// <param name="pintEscalaDepartamental">Código da Escala Departamental</param>
        /// <param name="pintRegional">Código da Regional</param>
        /// <param name="pintFilial">Código da Filial</param>
        /// <param name="pstrSolicitante">Nome do Solicitante</param>
        /// <param name="pstrColaborador">Nome do Colaborador</param>
        /// <param name="pstrAprovador">Nome do Aprovador</param>
        /// <param name="pintStatus">Código do Status</param>
        /// <param name="pintTipoSolicitacao">Código do Tipo da Solicitação</param>
        /// <param name="pdatDataInicio">Data de Início</param>
        /// <param name="pdatDataFinal">Data Final</param>
        /// <param name="intIdUsuarioSolicitante">Usuário Solicitante da Escalação</param>
        /// <returns>Lista Contendo as Escalas</returns>
        /// <history>
        ///     [cmarchi] created 11/1/2010
        ///     [cmarchi] modify 29/1/2010
        /// </history>
        public DataTable Listar(int pintNumeroEscala, int pintEscalaDepartamental,
            int pintRegional, int pintFilial, string pstrSolicitante, string pstrColaborador,
            string pstrAprovador, int pintStatus, int pintTipoSolicitacao, DateTime? pdatDataInicio,
            DateTime? pdatDataFinal, int intIdUsuarioSolicitante)
        {
            DLEscala objDLEscala = new DLEscala();

            try
            {
                return objDLEscala.Listar(pintNumeroEscala, pintEscalaDepartamental, pintRegional, pintFilial,
                    pstrSolicitante, pstrColaborador, pstrAprovador, pintStatus, pintTipoSolicitacao,
                    pdatDataInicio, pdatDataFinal, intIdUsuarioSolicitante);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLEscala.Finalizar();
            }
        }
        #endregion

        #region Listar Escalas Aprovação

        /// <summary>
        ///      Lista Escalas pendente de aprovação
        /// </summary>
        /// <param name="pdecCodUsuAprov">Código do Usuário aprovador</param>
        /// <history>
        ///      [cmarchi] created 26/01/2010
        /// </history>
        /// <returns>Lista de Escalação</returns>
        public Collection<Escalacao> ListarEscalasPendAprov(decimal pdecCodUsuAprov)
        {
            DLEscala objDLEscala = new DLEscala();

            try
            {
                return objDLEscala.ListarEscalaPendAprov(pdecCodUsuAprov);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLEscala.Finalizar();
            }
        }
        #endregion

        #region Listar Histórico de Escalas
        /// <summary>
        ///     Lista historico de Escalas dos colaboradores
        /// </summary>
        /// <param name="pdecCodUsurio"></param>
        /// <param name="pdecCodEscalacao"></param>
        /// <param name="pdecCodRegional"></param>
        /// <param name="pdecCodFilial"></param>
        /// <param name="pdecCodStatusAprov"></param>
        /// <param name="pdecCodTipoSol"></param>
        /// <param name="pdecCodEscalaDepto"></param>
        /// <param name="pstrDesSolicitante"></param>
        /// <param name="pstrDesColaborador"></param>
        /// <param name="pstrDesAprovador"></param>
        /// <param name="pdatDataInicio"></param>
        /// <param name="pdatDataFinal"></param>
        /// <returns>Lista de Escalação</returns>
        ///  <history>
        ///      [tgerevini] created 24/05/2010
        /// </history>
        public Collection<HistoricoEscala> ListarHistoricoEscala(decimal pdecCodUsurio, decimal pdecCodEscalacao, decimal pdecCodRegional, decimal pdecCodFilial,
            decimal pdecCodStatusAprov, decimal pdecCodTipoSol, decimal pdecCodEscalaDepto, string pstrDesSolicitante, string pstrDesColaborador,
            string pstrDesAprovador, DateTime? pdatDataInicio, DateTime? pdatDataFinal)
        {
            DLEscala objDLEscala = new DLEscala();

            try
            {
                return objDLEscala.ListarHistoricoEscala(pdecCodUsurio, pdecCodEscalacao, pdecCodRegional, pdecCodFilial, pdecCodStatusAprov, pdecCodTipoSol,
                    pdecCodEscalaDepto, pstrDesSolicitante, pstrDesColaborador, pstrDesAprovador, pdatDataInicio, pdatDataFinal);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLEscala.Finalizar();
            }
        }
        #endregion

        #region Listar Tipo Solicitação Escala

        /// <summary>
        ///      Lista Tipo da solicitação de Escala
        /// </summary>
        /// <history>
        ///      [cmarchi] created 11/2/2009
        /// </history>
        /// <returns></returns>
        public Collection<SafWeb.Model.Solicitacao.TipoSolicitacao> ListarTipoSolicitacao()
        {
            DLEscala objDLEscala = new DLEscala();

            try
            {
                return objDLEscala.ListarTipoSolicitacao();
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLEscala.Finalizar();
            }
        }

        #endregion

        #region "Listar horário de escalas Ronda"
        /// <summary>
        ///      Lista horários de escalas Ronda
        /// </summary>
        /// <param name="pintIdEscala"></param>
        /// <param name="pintIdJornada"></param>
        /// <param name="pstrDesEscala"></param>
        /// <param name="pintCodLegado"></param>
        /// <returns>Lista horários de escalas Ronda</returns>
        /// /// <history>
        ///      [haguiar] created 06/01/2012
        /// </history>
        public Collection<HorarioLegado> ListarHorarioEscalas(int pintIdEscala, int pintIdJornada, string pstrDesEscala, int pintCodLegado)
        {
            DLEscala objDLEscala = new DLEscala();

            try
            {
                return objDLEscala.ListarHorarioEscalas(pintIdEscala, pintIdJornada, pstrDesEscala, pintCodLegado);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLEscala.Finalizar();
            }
        }
        #endregion

        #region Listar registro de horários Ronda
        /// <summary>
        ///      Listar registro de horários Ronda
        /// </summary>
        /// <param name="pintIdHorario"></param>
        /// <param name="pstrDesHorario"></param>
        /// <param name="pintCodLegado"></param>
        /// <returns>Lista horários de escalas Ronda</returns>
        /// /// <history>
        ///      [haguiar] created 10/01/2012 10:53
        /// </history>
        public Collection<HorarioLegado> ListarRegistroHorarios(int pintIdHorario, string pstrDesHorario, int pintCodLegado, int pintIdJornada)
        {
            DLEscala objDLEscala = new DLEscala();

            try
            {
                return objDLEscala.ListarRegistroHorarios(pintIdHorario, pstrDesHorario, pintCodLegado, pintIdJornada);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLEscala.Finalizar();
            }
        }
        #endregion

        #region Listar escalas Ronda

        /// <summary>
        ///      Listar escalas Ronda
        /// </summary>
        /// <returns>Lista escalas Ronda</returns>
        /// /// <history>
        ///      [haguiar] created 11/01/2012 14:24
        /// </history>
        public Collection<EscalaLegadoRonda> ListarEscalasRonda()
        {
            DLEscala objDLEscala = new DLEscala();

            try
            {
                return objDLEscala.ListarEscalasRonda();
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLEscala.Finalizar();
            }
        }
        #endregion

        #region Listar horários Ronda

        /// <summary>
        ///      Listar horários Ronda
        /// </summary>
        /// <returns>Lista horários Ronda</returns>
        /// /// <history>
        ///      [haguiar] created 12/01/2012 09:18
        /// </history>
        public Collection<HorarioLegadoRonda> ListarHorarioRonda()
        {
            DLEscala objDLEscala = new DLEscala();

            try
            {
                return objDLEscala.ListarHorariosRonda();
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLEscala.Finalizar();
            }
        }
        #endregion
        #endregion

        #region Obter

        #region ObterDatasFolgaColaborador
        /// <summary>
        /// Obtém todas as Datas de folga para um colaborador
        /// </summary>
        /// <history>
        ///     [cfrancisco] created 26/05/2012
        /// </history>
        public IList<DateTime> ObterDatasFolgaColaborador(int idColaborador, DateTime dtInicioPeriodo, DateTime dtFinalPeriodo, int idEscalaDpto)
        {
            DLEscala objDLEscala = new DLEscala();

            try
            {
                return objDLEscala.ObterDatasFolgaColaborador(idColaborador, dtInicioPeriodo, dtFinalPeriodo, idEscalaDpto);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLEscala.Finalizar();
            }
        }

        #endregion

        #region Obter horário escala

        /// <summary>
        ///      Obter horário escala
        /// </summary>
        /// <param name="pintIdEscala"></param>
        /// <returns>Obter horário de uma escala RONDA</returns>
        /// /// <history>
        ///      [haguiar] created 06/01/2012 16:36
        /// </history>
        public HorarioLegado ObterHorarioEscala(int pintIdEscala)
        {
            DLEscala objDLEscala = new DLEscala();

            try
            {
                return objDLEscala.ObterHorarioEscala(pintIdEscala);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLEscala.Finalizar();
            }
        }
        #endregion

        #region Obter registro de horário

        /// <summary>
        ///      Obter registro de horário
        /// </summary>
        /// <param name="pintIdHorario"></param>
        /// <returns>Obter registro de um horário RONDA</returns>
        /// /// <history>
        ///      [haguiar] created 10/01/2012 10:57
        /// </history>
        public HorarioLegado ObterRegistroHorario(int pintIdHorario)
        {
            DLEscala objDLEscala = new DLEscala();

            try
            {
                return objDLEscala.ObterRegistroHorario(pintIdHorario);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLEscala.Finalizar();
            }
        }
        #endregion


        #region Obter
        /// <summary>
        /// Obtém datas escaladas de uma escalaçao.
        /// </summary>
        /// <param name="pintIdEscalacao">Id da Escalação</param>
        /// <returns>Coleçao de datas</returns>
        /// <history>
        ///     [haguiar_2] created 1/12/2010
        /// </history>
        public Collection<DateTime> ObterDtEscaladas(int pintIdEscalacao)
        {
            DLEscala objDLEscala = new DLEscala();

            Collection<DataHorarioColaboradores> colDataHorarioColaboradores =
                    new Collection<DataHorarioColaboradores>();

            Collection<DateTime> colDatas = null;

            try
            {
                colDataHorarioColaboradores = ObterDtHorColEscalacao(pintIdEscalacao, null, null, null);

                if (colDataHorarioColaboradores != null && colDataHorarioColaboradores.Count > 0)
                {
                    int i = 0;
                    DateTime[] arrDatas = new DateTime[colDataHorarioColaboradores.Count];

                    colDatas = new Collection<DateTime>();

                    foreach (DataHorarioColaboradores datCol in colDataHorarioColaboradores)
                    {
                        if (!colDatas.Contains(new DateTime(datCol.DataColaboradores.Year, datCol.DataColaboradores.Month, datCol.DataColaboradores.Day)))
                        {
                            colDatas.Add(new DateTime(datCol.DataColaboradores.Year, datCol.DataColaboradores.Month, datCol.DataColaboradores.Day));
                            arrDatas[i] = new DateTime(datCol.DataColaboradores.Year, datCol.DataColaboradores.Month, datCol.DataColaboradores.Day);

                            i += 1;
                        }
                    }
                }

                return colDatas;
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLEscala.Finalizar();
            }
        }
        #endregion

        #region Obter
        /// <summary>
        /// Obtém uma Escalação.
        /// </summary>
        /// <param name="pintIdEscalacao">Id da Escalação</param>
        /// <returns>Objeto Escala</returns>
        /// <history>
        ///     [cmarchi] created 12/1/2010
        /// </history>
        public Escalacao Obter(int pintIdEscalacao)
        {
            DLEscala objDLEscala = new DLEscala();

            try
            {
                return objDLEscala.Obter(pintIdEscalacao);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLEscala.Finalizar();
            }
        }
        #endregion

        #region Obter Aprovadores

        /// <summary>
        ///      Obtem os aprovadores da Escalação
        /// </summary>
        /// <param name="pintIdEscalacao">Id da Escalação</param>
        /// <history>
        ///      [cmarchi] created 29/1/2010
        /// </history>
        /// <returns>Aprovadores da Escalacao</returns>
        public DataTable ObterAprovadores(int pintIdEscalacao)
        {
            DLEscala objDLEscala = new DLEscala();

            try
            {
                return objDLEscala.ObterAprovadores(pintIdEscalacao);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLEscala.Finalizar();
            }
        }
        #endregion

        #region ObterColaboradores
        /// <summary>
        /// Obtém Colaboradores da Escalação.
        /// </summary>
        /// <param name="pintIdEscalacao">Id da Escalação</param>
        /// <returns>Objeto Colaboradores</returns>
        /// <history>
        ///     [cmarchi] created 12/1/2010
        /// </history>
        public Collection<Colaborador> ObterColaboradores(int pintIdEscalacao)
        {
            DLEscala objDLEscala = new DLEscala();

            try
            {
                return objDLEscala.ObterColaborador(pintIdEscalacao);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLEscala.Finalizar();
            }
        }
        #endregion

        #region Obter Escala Colaborador
        /// <summary>
        /// Obtém o código de Escalação se o colaborador já estiver em uma escala
        /// 0 = não está em nenhuma escala
        /// </summary>
        /// <param name="pintIdColaborador">Id do Colaborador</param>
        /// <returns>Código Escalação</returns>
        /// <history>
        ///     [tgerevini] created 18/5/2010
        /// </history>
        public int ObterEscalaColaborador(int pintIdColaborador)
        {
            DLEscala objDLEscala = new DLEscala();

            try
            {
                return objDLEscala.ObterEscalaColaborador(pintIdColaborador);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLEscala.Finalizar();
            }
        }
        #endregion

        #region VerificarDataColaboradorTroca

        public List<int> VerificarDataColaboradorTroca(DateTime dtEscalacao, int? idColaborador, int? idEscalaDpto)
        {
            DLEscala objDLEscala = new DLEscala();

            try
            {
                return objDLEscala.VerificarDataColaboradorTroca(dtEscalacao, idColaborador.GetValueOrDefault(), idEscalaDpto.GetValueOrDefault());
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLEscala.Finalizar();
            }
        }
        #endregion

        #region VerificarDataColaboradorEscala

        public List<int> VerificarDataColaboradorEscala(DateTime dtPeriodoInicio, DateTime dtPeriodoFim, int? idColaborador, int? idEscalaDpto)
        {
            DLEscala objDLEscala = new DLEscala();

            try
            {
                return objDLEscala.VerificarDataColaboradorEscala(dtPeriodoInicio, dtPeriodoFim, idColaborador.GetValueOrDefault(), idEscalaDpto.GetValueOrDefault());
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLEscala.Finalizar();
            }
        }
        #endregion

        #region ObterColaboradoresNaoContemHorarioEscala
        /// <summary>
        /// Obtém Colaboradores da Escalação que ainda não possui uma Escala.
        /// </summary>
        /// <param name="pintIdEscalacao">Id da Escalação</param>
        /// <returns>Lista de Objetos Colaborador</returns>
        /// <history>
        ///     [cmarchi] created 15/1/2010
        /// </history>
        public Collection<Colaborador> ObterColaboradoresNaoContemHorarioEscala(int pintIdEscalacao)
        {
            DLEscala objDLEscala = new DLEscala();

            try
            {
                return objDLEscala.ObterColaboradoresNaoContemHorarioEscala(pintIdEscalacao);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLEscala.Finalizar();
            }
        }
        #endregion

        #region ObterDatas
        /// <summary>
        /// Obtém todas as datas de uma Escalacao.
        /// </summary>
        /// <param name="pintIdEscalacao">Id da Escalação</param>
        /// <returns>Coleção de Datas</returns>
        /// <history>
        ///     [cmarchi] created 20/1/2010
        /// </history>
        public Collection<DateTime> ObterDatas(int pintIdEscalacao)
        {
            DLEscala objDLEscala = new DLEscala();

            try
            {
                return objDLEscala.ObterDatas(pintIdEscalacao);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLEscala.Finalizar();
            }
        }
        #endregion

        #region Obter Ultimo Aprovador
        /// <summary>
        /// Obtém o nome do ultimo aprovador da escala
        /// </summary>
        /// <param name="pintIdEscalacao">Id da Escalação</param>
        /// <returns>Nome do Ultimo Aprovador</returns>
        /// <history>
        ///     [tgerevini] created 01/06/2010
        /// </history>
        public String ObterUltimoAprovador(int pintIdEscalacao)
        {
            DLEscala objDLEscala = new DLEscala();

            try
            {
                return objDLEscala.ObterUltimoAprovador(pintIdEscalacao);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLEscala.Finalizar();
            }
        }
        #endregion

        #region ObterDtHorColEscalacao CREW
        /// <summary>
        /// Obtém datas e horários dos colaboradores de uma escalação ou troca de horário CREW
        /// </summary>
        /// <param name="pintIdEscalacao">Id da Escalação</param>
        /// <param name="datDataSelecionada">Data selecionada</param>
        /// <param name="pblnCompararTime">True - Compara a data com o Time, Flase - Compara a data sem o Time</param>
        /// <returns>Lista de Objetos de data e horários dos colaboradores</returns>
        /// <history>
        ///     [haguiar_9004] modify 11/08/2011 15:05
        /// </history>
        public Collection<DataHorarioColaboradores> ObterDtHorColEscalacaoCrew(DateTime? pdatDataSelecionada, bool? pblnCompararTime,
            Collection<DateTime> colDatas, int pintId_Usuario,  bool pblnDivergencia, int pintId_TipoSolicitacao)
        {
            DLEscala objDLEscala = new DLEscala();

            Collection<DataHorarioColaboradores> colDataHorarioColaboradores =
                new Collection<DataHorarioColaboradores>();

            Collection<EscalacaoColaboradoresData> colEscalacaoColaboradoresData =
                new Collection<EscalacaoColaboradoresData>();

            //Collection<DateTime> colDatas = new Collection<DateTime>();

            try
            {
                //obtendo os colaboradores com a data e horário da escalação
                colEscalacaoColaboradoresData = objDLEscala.ObterDtHorColEscalacaoCrew(pdatDataSelecionada, pintId_Usuario, pblnDivergencia, pintId_TipoSolicitacao);

                if (pblnDivergencia)
                {
                    //return colEscalacaoColaboradoresData;

                    foreach (EscalacaoColaboradoresData colEscalacao in colEscalacaoColaboradoresData)
                    {
                        DataHorarioColaboradores objDatHor = new DataHorarioColaboradores();

                        objDatHor.CodigosColaboradores = colEscalacao.IdColaborador.ToString();

                        objDatHor.NomesColaboradores = colEscalacao.NomeColaborador;

                        objDatHor.IdEscalacao = colEscalacao.IdEscalacao;

                        if (colEscalacao.Compensado || colEscalacao.Folga || colEscalacao.Licenca)
                        {
                            objDatHor.DataColaboradores = pdatDataSelecionada.Value;
                        }
                        else
                        {
                        objDatHor.DataColaboradores = colEscalacao.DataEscalacao;
                        }

                        //insere as flags
                        objDatHor.Compensado = string.Empty;
                        objDatHor.Folga = string.Empty;
                        objDatHor.Licenca = string.Empty;
                        objDatHor.HorarioFlex = string.Empty;
                        objDatHor.HoraExtra = string.Empty;

                        objDatHor.DesJornada = colEscalacao.DesJornada;

                        colDataHorarioColaboradores.Add(objDatHor);
                    }

                    return colDataHorarioColaboradores;
                }

                //obtém todos as datas de uma escalação
                //colDatas = this.ObterDatas(pintIdEscalacao);

                //agrupa os horarios dos colaboradores de acordo com a mesma data
                int j = 0;
                int intTotal = colEscalacaoColaboradoresData.Count;
                int intTotalDatas = colDatas.Count;
                int intPosicaoInicial = 0;

                StringBuilder strIdColaboradores = new StringBuilder();
                StringBuilder strColaboradores = new StringBuilder();

                DateTime datDataComparacao = DateTime.Now;

                string strDesJornadaComparacao = string.Empty;
                bool blnLicencaComparacao = false;
                bool blnCompensadoComparacao = false;
                bool blnFolgaComparacao = false;
                bool blnHorarioFlexComparacao = false;
                bool blnHoraExtraComparacao = false;

                string strDesJornadaColab = string.Empty;
                bool blnLicencaColab = false;
                bool blnCompensadoColab = false;
                bool blnFolgaColab = false;
                bool blnHorarioFlexColab = false;
                bool blnHoraExtraColab = false;

                if (intTotal > 0)
                {

                    if (colEscalacaoColaboradoresData[0].Compensado || colEscalacaoColaboradoresData[0].Folga || colEscalacaoColaboradoresData[0].Licenca)
                    {
                        datDataComparacao = pdatDataSelecionada.Value;
                        colEscalacaoColaboradoresData[0].DataEscalacao = pdatDataSelecionada.Value;
                    }
                    else
                    {
                    datDataComparacao = colEscalacaoColaboradoresData[0].DataEscalacao;
                    }

                    strDesJornadaComparacao = colEscalacaoColaboradoresData[0].DesJornada;

                    blnLicencaComparacao = colEscalacaoColaboradoresData[0].Licenca;
                    blnCompensadoComparacao = colEscalacaoColaboradoresData[0].Compensado;
                    blnFolgaComparacao = colEscalacaoColaboradoresData[0].Folga;
                    blnHorarioFlexComparacao = colEscalacaoColaboradoresData[0].HorarioFlex;
                    blnHoraExtraComparacao = colEscalacaoColaboradoresData[0].HoraExtra;

                    intPosicaoInicial = colDatas.IndexOf(
                        Convert.ToDateTime(colEscalacaoColaboradoresData[0].DataEscalacao.ToShortDateString()));

                    if (intTotalDatas > 0)
                    {
                        for (int i = intPosicaoInicial; i < intTotalDatas; i++)
                        {
                            while (j < intTotal)
                            {
                                DateTime data;

                                if (colEscalacaoColaboradoresData[j].Compensado || colEscalacaoColaboradoresData[j].Folga || colEscalacaoColaboradoresData[j].Licenca)
                                {
                                    colEscalacaoColaboradoresData[j].DataEscalacao = pdatDataSelecionada.Value;
                                }

                                data = Convert.ToDateTime(colEscalacaoColaboradoresData[j].DataEscalacao.ToShortDateString());

                                //parametros do colaborador do loop
                                blnLicencaColab = colEscalacaoColaboradoresData[j].Licenca;
                                blnCompensadoColab = colEscalacaoColaboradoresData[j].Compensado;
                                blnFolgaColab = colEscalacaoColaboradoresData[j].Folga;
                                blnHorarioFlexColab = colEscalacaoColaboradoresData[j].HorarioFlex;
                                blnHoraExtraColab = colEscalacaoColaboradoresData[j].HoraExtra;

                                strDesJornadaColab = colEscalacaoColaboradoresData[j].DesJornada;

                                //comparar as datas
                                if (
                                    colDatas[0].CompareTo(data) == 0 &&
                                    colEscalacaoColaboradoresData[j].DataEscalacao == datDataComparacao &&

                                    //comparar parametros
                                    blnLicencaColab == blnLicencaComparacao &&
                                    blnCompensadoColab == blnCompensadoComparacao &&
                                    blnFolgaColab == blnFolgaComparacao &&
                                    blnHorarioFlexColab == blnHorarioFlexComparacao &&
                                    blnHoraExtraColab == blnHoraExtraComparacao &&
                                    strDesJornadaColab == strDesJornadaComparacao
                                    )
                                {
                                    //insere os id e os nomes dos colaboradores
                                    strIdColaboradores.Append(colEscalacaoColaboradoresData[j].IdColaborador + ",");
                                    strColaboradores.Append(colEscalacaoColaboradoresData[j].NomeColaborador.Trim() + ",");

                                    j++;
                                }
                                else
                                {
                                    //insere 
                                    DataHorarioColaboradores objDatHorCol = new DataHorarioColaboradores();

                                    objDatHorCol.CodigosColaboradores = strIdColaboradores.Remove(
                                        strIdColaboradores.Length - 1, 1).ToString();

                                    objDatHorCol.NomesColaboradores = strColaboradores.Remove(
                                        strColaboradores.Length - 1, 1).ToString();

                                    objDatHorCol.IdEscalacao = colEscalacaoColaboradoresData[j - 1].IdEscalacao;

                                    objDatHorCol.DataColaboradores = colEscalacaoColaboradoresData[j - 1].DataEscalacao;

                                    //insere as flags
                                    objDatHorCol.Compensado =
                                        (colEscalacaoColaboradoresData[j - 1].Compensado) ? "Compensado" : string.Empty;

                                    objDatHorCol.Folga =
                                        (colEscalacaoColaboradoresData[j - 1].Folga) ? "Folga/DSR" : string.Empty;

                                    objDatHorCol.Licenca =
                                        (colEscalacaoColaboradoresData[j - 1].Licenca) ? "Férias/Licença" : string.Empty;

                                    objDatHorCol.HorarioFlex =
                                        (colEscalacaoColaboradoresData[j - 1].HorarioFlex) ? "08 às 09 flex" : string.Empty;

                                    objDatHorCol.HoraExtra =
                                        (colEscalacaoColaboradoresData[j - 1].HoraExtra) ? "Hora Extra" : string.Empty;

                                    objDatHorCol.DesJornada =
                                        colEscalacaoColaboradoresData[j - 1].DesJornada;

                                    colDataHorarioColaboradores.Add(objDatHorCol);

                                    //inicializa as variáveis
                                    objDatHorCol = null;
                                    objDatHorCol = new DataHorarioColaboradores();

                                    strIdColaboradores = null;
                                    strColaboradores = null;

                                    strIdColaboradores = new StringBuilder();
                                    strColaboradores = new StringBuilder();

                                    datDataComparacao = colEscalacaoColaboradoresData[j].DataEscalacao;

                                    strDesJornadaComparacao = colEscalacaoColaboradoresData[j].DesJornada;

                                    blnLicencaComparacao = colEscalacaoColaboradoresData[j].Licenca;
                                    blnCompensadoComparacao = colEscalacaoColaboradoresData[j].Compensado;
                                    blnFolgaComparacao = colEscalacaoColaboradoresData[j].Folga;

                                    blnHorarioFlexComparacao = colEscalacaoColaboradoresData[j].HorarioFlex;
                                    blnHoraExtraComparacao = colEscalacaoColaboradoresData[j].HoraExtra;

                                    if (colDatas[0].CompareTo(Convert.ToDateTime(
                                        datDataComparacao.ToShortDateString())) != 0)
                                    {
                                        break;
                                    }
                                }
                            }
                        }


                        if (strIdColaboradores.Length > 0)
                        {
                        //insere a última data
                        DataHorarioColaboradores objDatHorCol2 = new DataHorarioColaboradores();

                        objDatHorCol2.CodigosColaboradores = strIdColaboradores.Remove(
                            strIdColaboradores.Length - 1, 1).ToString();

                        objDatHorCol2.NomesColaboradores = strColaboradores.Remove(
                            strColaboradores.Length - 1, 1).ToString();

                        objDatHorCol2.IdEscalacao = colEscalacaoColaboradoresData[j - 1].IdEscalacao;

                        objDatHorCol2.DataColaboradores = colEscalacaoColaboradoresData[j - 1].DataEscalacao;

                        //insere as flags
                        objDatHorCol2.Compensado =
                            (colEscalacaoColaboradoresData[j - 1].Compensado) ? "Compensado" : string.Empty;

                        objDatHorCol2.Folga =
                            (colEscalacaoColaboradoresData[j - 1].Folga) ? "Folga/DSR" : string.Empty;

                        objDatHorCol2.Licenca =
                            (colEscalacaoColaboradoresData[j - 1].Licenca) ? "Férias/Licença" : string.Empty;

                        objDatHorCol2.HorarioFlex =
                            (colEscalacaoColaboradoresData[j - 1].HorarioFlex) ? "08 às 09 flex" : string.Empty;

                        objDatHorCol2.HoraExtra =
                            (colEscalacaoColaboradoresData[j - 1].HoraExtra) ? "Hora Extra" : string.Empty;

                        objDatHorCol2.DesJornada =
                            colEscalacaoColaboradoresData[j - 1].DesJornada;

                        colDataHorarioColaboradores.Add(objDatHorCol2);
                    }
                }
                }

                return colDataHorarioColaboradores;
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLEscala.Finalizar();
            }
        }
        #endregion

        #region ObterDtHorColEscalacao RONDA
        /// <summary>
        /// Obtém datas e horários dos colaboradores de uma escalação ou troca de horário RONDA
        /// </summary>
        /// <param name="pintIdEscalaDpto">Id da escala departamental</param>
        /// <param name="pintIdTipoSolicitacao">Id do tipo da solicitação</param>
        /// <param name="colDatas">Coleção com data de início e fim</param>
        /// <returns>Lista de Objetos de data e horários dos colaboradores</returns>
        /// <history>
        ///     [haguiar] create 07/11/2011 16:13
        /// </history>
        public Collection<DataHorarioColaboradores> ObterDtHorColEscalacaoRonda(int pintIdEscalaDpto, int pintIdTipoSolicitacao,
            Collection<DateTime> colDatas)
        {
            DLEscala objDLEscala = new DLEscala();

            Collection<DataHorarioColaboradores> colDataHorarioColaboradores =
                new Collection<DataHorarioColaboradores>();

            Collection<EscalacaoColaboradoresData> colEscalacaoColaboradoresData =
                new Collection<EscalacaoColaboradoresData>();

            //Collection<DateTime> colDatas = new Collection<DateTime>();

            try
            {
                //obtendo os colaboradores com a data e horário da escalação
                colEscalacaoColaboradoresData = objDLEscala.ObterDtHorColEscalacaoRonda(pintIdEscalaDpto,
                    pintIdTipoSolicitacao, colDatas);

                DateTime dtInicial = colDatas[0];
                int d = 0;

                if (pintIdTipoSolicitacao == 7)
                {
                    colDatas.Clear();

                    while (d < 7)
                    {
                        colDatas.Add(dtInicial.AddDays(d));
                        d += 1;
                    }
                }

                //agrupa os horarios dos colaboradores de acordo com a mesma data
                int j = 0;
                int intTotal = colEscalacaoColaboradoresData.Count;
                int intTotalDatas = colDatas.Count;
                int intPosicaoInicial = 0;

                StringBuilder strIdColaboradores = new StringBuilder();
                StringBuilder strColaboradores = new StringBuilder();

                DateTime datDataComparacao = DateTime.Now;

                string strDesJornadaComparacao = string.Empty;
                bool blnLicencaComparacao = false;
                bool blnCompensadoComparacao = false;
                bool blnFolgaComparacao = false;
                bool blnHorarioFlexComparacao = false;
                bool blnHoraExtraComparacao = false;

                string strDesJornadaColab = string.Empty;
                bool blnLicencaColab = false;
                bool blnCompensadoColab = false;
                bool blnFolgaColab = false;
                bool blnHorarioFlexColab = false;
                bool blnHoraExtraColab = false;

                if (intTotal > 0)
                {
                    datDataComparacao = colEscalacaoColaboradoresData[0].DataEscalacao;

                    strDesJornadaComparacao = colEscalacaoColaboradoresData[0].DesJornada;

                    blnLicencaComparacao = colEscalacaoColaboradoresData[0].Licenca;
                    blnCompensadoComparacao = colEscalacaoColaboradoresData[0].Compensado;
                    blnFolgaComparacao = colEscalacaoColaboradoresData[0].Folga;
                    blnHorarioFlexComparacao = colEscalacaoColaboradoresData[0].HorarioFlex;
                    blnHoraExtraComparacao = colEscalacaoColaboradoresData[0].HoraExtra;

                    intPosicaoInicial = colDatas.IndexOf(
                        Convert.ToDateTime(colEscalacaoColaboradoresData[0].DataEscalacao.ToShortDateString()));

                    if (intTotalDatas > 0)
                    {
                        for (int i = intPosicaoInicial; i < intTotalDatas; i++)
                        {
                            while (j < intTotal)
                            {
                                DateTime data = Convert.ToDateTime(colEscalacaoColaboradoresData[j].DataEscalacao.ToShortDateString());

                                //parametros do colaborador do loop
                                blnLicencaColab = colEscalacaoColaboradoresData[j].Licenca;
                                blnCompensadoColab = colEscalacaoColaboradoresData[j].Compensado;
                                blnFolgaColab = colEscalacaoColaboradoresData[j].Folga;
                                blnHorarioFlexColab = colEscalacaoColaboradoresData[j].HorarioFlex;
                                blnHoraExtraColab = colEscalacaoColaboradoresData[j].HoraExtra;

                                strDesJornadaColab = colEscalacaoColaboradoresData[j].DesJornada;

                                //comparar as datas
                                if (
                                    colDatas[i].CompareTo(data) == 0 &&
                                    colEscalacaoColaboradoresData[j].DataEscalacao == datDataComparacao &&

                                    //comparar parametros
                                    blnLicencaColab == blnLicencaComparacao &&
                                    blnCompensadoColab == blnCompensadoComparacao &&
                                    blnFolgaColab == blnFolgaComparacao &&
                                    blnHorarioFlexColab == blnHorarioFlexComparacao &&
                                    blnHoraExtraColab == blnHoraExtraComparacao &&
                                    strDesJornadaColab == strDesJornadaComparacao
                                    )
                                {
                                    //insere os id e os nomes dos colaboradores
                                    strIdColaboradores.Append(colEscalacaoColaboradoresData[j].IdColaborador + ",");
                                    strColaboradores.Append(colEscalacaoColaboradoresData[j].NomeColaborador.Trim() + ",");

                                    j++;
                                }
                                else
                                {
                                    //insere 
                                    DataHorarioColaboradores objDatHorCol = new DataHorarioColaboradores();

                                    objDatHorCol.CodigosColaboradores = strIdColaboradores.Remove(
                                        strIdColaboradores.Length - 1, 1).ToString();

                                    objDatHorCol.NomesColaboradores = strColaboradores.Remove(
                                        strColaboradores.Length - 1, 1).ToString();

                                    objDatHorCol.IdEscalacao = colEscalacaoColaboradoresData[j - 1].IdEscalacao;

                                    objDatHorCol.DataColaboradores = colEscalacaoColaboradoresData[j - 1].DataEscalacao;

                                    //insere as flags
                                    objDatHorCol.Compensado =
                                        (colEscalacaoColaboradoresData[j - 1].Compensado) ? "Compensado" : string.Empty;

                                    objDatHorCol.Folga =
                                        (colEscalacaoColaboradoresData[j - 1].Folga) ? "Folga/DSR" : string.Empty;

                                    objDatHorCol.Licenca =
                                        (colEscalacaoColaboradoresData[j - 1].Licenca) ? "Férias/Licença" : string.Empty;

                                    objDatHorCol.HorarioFlex =
                                        (colEscalacaoColaboradoresData[j - 1].HorarioFlex) ? "08 às 09 flex" : string.Empty;

                                    objDatHorCol.HoraExtra =
                                        (colEscalacaoColaboradoresData[j - 1].HoraExtra) ? "Hora Extra" : string.Empty;

                                    objDatHorCol.DesJornada =
                                        colEscalacaoColaboradoresData[j - 1].DesJornada;

                                    colDataHorarioColaboradores.Add(objDatHorCol);

                                    //inicializa as variáveis
                                    objDatHorCol = null;
                                    objDatHorCol = new DataHorarioColaboradores();

                                    strIdColaboradores = null;
                                    strColaboradores = null;

                                    strIdColaboradores = new StringBuilder();
                                    strColaboradores = new StringBuilder();

                                    datDataComparacao = colEscalacaoColaboradoresData[j].DataEscalacao;

                                    strDesJornadaComparacao = colEscalacaoColaboradoresData[j].DesJornada;

                                    blnLicencaComparacao = colEscalacaoColaboradoresData[j].Licenca;
                                    blnCompensadoComparacao = colEscalacaoColaboradoresData[j].Compensado;
                                    blnFolgaComparacao = colEscalacaoColaboradoresData[j].Folga;

                                    blnHorarioFlexComparacao = colEscalacaoColaboradoresData[j].HorarioFlex;
                                    blnHoraExtraComparacao = colEscalacaoColaboradoresData[j].HoraExtra;

                                    if (colDatas[i].CompareTo(Convert.ToDateTime(
                                        datDataComparacao.ToShortDateString())) != 0)
                                    {
                                        break;
                                    }
                                }
                            }
                        }

                        //insere a última data
                        DataHorarioColaboradores objDatHorCol2 = new DataHorarioColaboradores();

                        objDatHorCol2.CodigosColaboradores = strIdColaboradores.Remove(
                            strIdColaboradores.Length - 1, 1).ToString();

                        objDatHorCol2.NomesColaboradores = strColaboradores.Remove(
                            strColaboradores.Length - 1, 1).ToString();

                        objDatHorCol2.IdEscalacao = colEscalacaoColaboradoresData[j - 1].IdEscalacao;

                        objDatHorCol2.DataColaboradores = colEscalacaoColaboradoresData[j - 1].DataEscalacao;

                        //insere as flags
                        objDatHorCol2.Compensado =
                            (colEscalacaoColaboradoresData[j - 1].Compensado) ? "Compensado" : string.Empty;

                        objDatHorCol2.Folga =
                            (colEscalacaoColaboradoresData[j - 1].Folga) ? "Folga/DSR" : string.Empty;

                        objDatHorCol2.Licenca =
                            (colEscalacaoColaboradoresData[j - 1].Licenca) ? "Férias/Licença" : string.Empty;

                        objDatHorCol2.HorarioFlex =
                            (colEscalacaoColaboradoresData[j - 1].HorarioFlex) ? "08 às 09 flex" : string.Empty;

                        objDatHorCol2.HoraExtra =
                            (colEscalacaoColaboradoresData[j - 1].HoraExtra) ? "Hora Extra" : string.Empty;

                        objDatHorCol2.DesJornada =
                            colEscalacaoColaboradoresData[j - 1].DesJornada;

                        colDataHorarioColaboradores.Add(objDatHorCol2);
                    }
                }

                return colDataHorarioColaboradores;
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLEscala.Finalizar();
            }
        }
        #endregion

        #region ObterDtHorColEscalacao
        /// <summary>
        /// Obtém datas e horários dos colaboradores de uma escalação
        /// </summary>
        /// <param name="pintIdEscalacao">Id da Escalação</param>
        /// <param name="datDataSelecionada">Data selecionada</param>
        /// <param name="pblnCompararTime">True - Compara a data com o Time, Flase - Compara a data sem o Time</param>
        /// <returns>Lista de Objetos de data e horários dos colaboradores</returns>
        /// <history>
        ///     [cmarchi] created 20/1/2010
        ///     [cmarchi] modify 18/2/2010
        ///     [haguiar] modify 08/11/2010
        ///     [haguiar_2] modify 04/12/2010
        ///     obter horário flex
        ///     [haguiar_8829] modify 06/07/2011 14:56
        ///     obter hora extra
        ///     [haguiar] modify 02/01/2012 16:19
        ///     adicionar codlegado
        /// </history>
        public Collection<DataHorarioColaboradores> ObterDtHorColEscalacao(int pintIdEscalacao,
            DateTime? pdatDataSelecionada, bool? pblnCompararTime, int? intCodLegado)
        {
            DLEscala objDLEscala = new DLEscala();

            Collection<DataHorarioColaboradores> colDataHorarioColaboradores =
                new Collection<DataHorarioColaboradores>();

            Collection<EscalacaoColaboradoresData> colEscalacaoColaboradoresData =
                new Collection<EscalacaoColaboradoresData>();

            Collection<DateTime> colDatas = new Collection<DateTime>();

            try
            {
                //obtendo os colaboradores com a data e horário da escalação
                colEscalacaoColaboradoresData = objDLEscala.ObterDtHorColEscalacao(pintIdEscalacao,
                    pdatDataSelecionada, pblnCompararTime, intCodLegado);

                //obtém todos as datas de uma escalação
                colDatas = this.ObterDatas(pintIdEscalacao);

                //agrupa os horarios dos colaboradores de acordo com a mesma data
                int j = 0;
                int intTotal = colEscalacaoColaboradoresData.Count;
                int intTotalDatas = colDatas.Count;
                int intPosicaoInicial = 0;

                StringBuilder strIdColaboradores = new StringBuilder();
                StringBuilder strColaboradores = new StringBuilder();

                DateTime datDataComparacao = DateTime.Now;

                bool blnLicencaComparacao = false;
                bool blnCompensadoComparacao = false;
                bool blnFolgaComparacao = false;
                bool blnHorarioFlexComparacao = false;
                bool blnHoraExtraComparacao = false;
                bool blnIniciaFolgandoComparacao = false;
                bool blnFlgSituacaoComparacao = false;

                int intCodLegadoComparacao;
                int intIdJornadaComparacao;

                bool blnLicencaColab = false;
                bool blnCompensadoColab = false;
                bool blnFolgaColab = false;
                bool blnHorarioFlexColab = false;
                bool blnHoraExtraColab = false;
                bool blnIniciaFolgando = false;
                bool blnFlgSituacao = false;
                int intCodLegadoColab;
                int intIdJornada;

                if (intTotal > 0)
                {
                    datDataComparacao = colEscalacaoColaboradoresData[0].DataEscalacao;

                    blnLicencaComparacao = colEscalacaoColaboradoresData[0].Licenca;
                    blnCompensadoComparacao = colEscalacaoColaboradoresData[0].Compensado;
                    blnFolgaComparacao = colEscalacaoColaboradoresData[0].Folga;
                    blnHorarioFlexComparacao = colEscalacaoColaboradoresData[0].HorarioFlex;
                    blnHoraExtraComparacao = colEscalacaoColaboradoresData[0].HoraExtra;
                    blnIniciaFolgandoComparacao = colEscalacaoColaboradoresData[0].flgIniciaFolgando;
                    blnFlgSituacaoComparacao = colEscalacaoColaboradoresData[0].flgSituacao;

                    intCodLegadoComparacao = colEscalacaoColaboradoresData[0].CodLegado;
                    intIdJornadaComparacao = colEscalacaoColaboradoresData[0].IdJornada;

                    intPosicaoInicial = colDatas.IndexOf(
                        Convert.ToDateTime(colEscalacaoColaboradoresData[0].DataEscalacao.ToShortDateString()));

                    if (intTotalDatas > 0)
                    {
                        for (int i = intPosicaoInicial; i < intTotalDatas; i++)
                        {
                            while (j < intTotal)
                            {
                                DateTime data = Convert.ToDateTime(colEscalacaoColaboradoresData[j].DataEscalacao.ToShortDateString());

                                //parametros do colaborador do loop
                                blnLicencaColab = colEscalacaoColaboradoresData[j].Licenca;
                                blnCompensadoColab = colEscalacaoColaboradoresData[j].Compensado;
                                blnFolgaColab = colEscalacaoColaboradoresData[j].Folga;
                                blnHorarioFlexColab = colEscalacaoColaboradoresData[j].HorarioFlex;
                                blnHoraExtraColab = colEscalacaoColaboradoresData[j].HoraExtra;
                                blnIniciaFolgando = colEscalacaoColaboradoresData[j].flgIniciaFolgando;
                                intCodLegadoColab = colEscalacaoColaboradoresData[j].CodLegado;
                                intIdJornada = colEscalacaoColaboradoresData[j].IdJornada;
                                blnFlgSituacao = colEscalacaoColaboradoresData[j].flgSituacao;

                                //comparar as datas
                                if (
                                    colDatas[i].CompareTo(data) == 0 &&
                                    colEscalacaoColaboradoresData[j].DataEscalacao == datDataComparacao &&

                                    //comparar parametros
                                    blnLicencaColab == blnLicencaComparacao &&
                                    blnCompensadoColab == blnCompensadoComparacao &&
                                    blnFolgaColab == blnFolgaComparacao &&
                                    blnHorarioFlexColab == blnHorarioFlexComparacao &&
                                    blnHoraExtraColab == blnHoraExtraComparacao &&
                                    intCodLegadoColab == intCodLegadoComparacao &&
                                    blnIniciaFolgando == blnIniciaFolgandoComparacao &&
                                    intIdJornada == intIdJornadaComparacao &&
                                    blnFlgSituacao == blnFlgSituacaoComparacao
                                    )
                                {
                                    //insere os id e os nomes dos colaboradores
                                    strIdColaboradores.Append(colEscalacaoColaboradoresData[j].IdColaborador + ",");
                                    strColaboradores.Append(colEscalacaoColaboradoresData[j].NomeColaborador.Trim() + ",");

                                    j++;
                                }
                                else
                                {
                                    //insere 
                                    DataHorarioColaboradores objDatHorCol = new DataHorarioColaboradores();

                                    objDatHorCol.DesJornada = colEscalacaoColaboradoresData[j - 1].DesJornada;

                                    objDatHorCol.CodigosColaboradores = strIdColaboradores.Remove(
                                        strIdColaboradores.Length - 1, 1).ToString();

                                    objDatHorCol.NomesColaboradores = strColaboradores.Remove(
                                        strColaboradores.Length - 1, 1).ToString();

                                    objDatHorCol.IdEscalacao = colEscalacaoColaboradoresData[j - 1].IdEscalacao;

                                    objDatHorCol.CodLegado = colEscalacaoColaboradoresData[j - 1].CodLegado;

                                    objDatHorCol.DataColaboradores = colEscalacaoColaboradoresData[j - 1].DataEscalacao;

                                    //descrição do horário
                                    objDatHorCol.HorarioColaborador = colEscalacaoColaboradoresData[j - 1].HorarioColaborador;

                                    //insere as flags
                                    objDatHorCol.Compensado =
                                        (colEscalacaoColaboradoresData[j - 1].Compensado) ? "Compensado" : string.Empty;

                                    objDatHorCol.Folga =
                                        (colEscalacaoColaboradoresData[j - 1].Folga) ? "Folga/DSR" : string.Empty;

                                    objDatHorCol.Licenca =
                                        (colEscalacaoColaboradoresData[j - 1].Licenca) ? "Férias/Licença" : string.Empty;

                                    objDatHorCol.HorarioFlex =
                                        (colEscalacaoColaboradoresData[j - 1].HorarioFlex) ? "08 às 09 flex" : string.Empty;

                                    objDatHorCol.HoraExtra =
                                        (colEscalacaoColaboradoresData[j - 1].HoraExtra) ? "Hora Extra" : string.Empty;

                                    objDatHorCol.flgIniciaFolgando = colEscalacaoColaboradoresData[j - 1].flgIniciaFolgando;

                                    objDatHorCol.flgSituacao = colEscalacaoColaboradoresData[j - 1].flgSituacao;

                                    objDatHorCol.IdJornada = colEscalacaoColaboradoresData[j - 1].IdJornada;

                                    colDataHorarioColaboradores.Add(objDatHorCol);

                                    //inicializa as variáveis
                                    objDatHorCol = null;
                                    objDatHorCol = new DataHorarioColaboradores();

                                    strIdColaboradores = null;
                                    strColaboradores = null;

                                    strIdColaboradores = new StringBuilder();
                                    strColaboradores = new StringBuilder();

                                    datDataComparacao = colEscalacaoColaboradoresData[j].DataEscalacao;

                                    blnLicencaComparacao = colEscalacaoColaboradoresData[j].Licenca;
                                    blnCompensadoComparacao = colEscalacaoColaboradoresData[j].Compensado;
                                    blnFolgaComparacao = colEscalacaoColaboradoresData[j].Folga;

                                    blnHorarioFlexComparacao = colEscalacaoColaboradoresData[j].HorarioFlex;
                                    blnHoraExtraComparacao = colEscalacaoColaboradoresData[j].HoraExtra;
                                    blnIniciaFolgandoComparacao = colEscalacaoColaboradoresData[j].flgIniciaFolgando;

                                    intCodLegadoComparacao = colEscalacaoColaboradoresData[j].CodLegado;
                                    intIdJornadaComparacao = colEscalacaoColaboradoresData[j].IdJornada;

                                    blnFlgSituacaoComparacao = colEscalacaoColaboradoresData[j].flgSituacao;

                                    if (colDatas[i].CompareTo(Convert.ToDateTime(
                                        datDataComparacao.ToShortDateString())) != 0)
                                    {
                                        break;
                                    }
                                }
                            }
                        }

                        //insere a última data
                        DataHorarioColaboradores objDatHorCol2 = new DataHorarioColaboradores();

                        //objDatHorCol.DesJornada = colEscalacaoColaboradoresData[j - 1].DesJornada;
                        objDatHorCol2.DesJornada = colEscalacaoColaboradoresData[j - 1].DesJornada;

                        objDatHorCol2.CodigosColaboradores = strIdColaboradores.Remove(
                            strIdColaboradores.Length - 1, 1).ToString();

                        objDatHorCol2.NomesColaboradores = strColaboradores.Remove(
                            strColaboradores.Length - 1, 1).ToString();

                        objDatHorCol2.IdEscalacao = colEscalacaoColaboradoresData[j - 1].IdEscalacao;

                        objDatHorCol2.CodLegado = colEscalacaoColaboradoresData[j - 1].CodLegado;

                        objDatHorCol2.DataColaboradores = colEscalacaoColaboradoresData[j - 1].DataEscalacao;

                        //descrição do horário
                        objDatHorCol2.HorarioColaborador = colEscalacaoColaboradoresData[j - 1].HorarioColaborador;

                        //insere as flags
                        objDatHorCol2.Compensado =
                            (colEscalacaoColaboradoresData[j - 1].Compensado) ? "Compensado" : string.Empty;

                        objDatHorCol2.Folga =
                            (colEscalacaoColaboradoresData[j - 1].Folga) ? "Folga/DSR" : string.Empty;

                        objDatHorCol2.Licenca =
                            (colEscalacaoColaboradoresData[j - 1].Licenca) ? "Férias/Licença" : string.Empty;

                        objDatHorCol2.HorarioFlex =
                            (colEscalacaoColaboradoresData[j - 1].HorarioFlex) ? "08 às 09 flex" : string.Empty;

                        objDatHorCol2.HoraExtra =
                            (colEscalacaoColaboradoresData[j - 1].HoraExtra) ? "Hora Extra" : string.Empty;

                        objDatHorCol2.flgIniciaFolgando = colEscalacaoColaboradoresData[j - 1].flgIniciaFolgando;

                        objDatHorCol2.flgSituacao = colEscalacaoColaboradoresData[j - 1].flgSituacao;

                        objDatHorCol2.IdJornada = colEscalacaoColaboradoresData[j - 1].IdJornada;

                        colDataHorarioColaboradores.Add(objDatHorCol2);
                    }
                }

                return colDataHorarioColaboradores;
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLEscala.Finalizar();
            }
        }
        #endregion

        #region ObterEscalasExportação
        /// <summary>
        /// Obtém a Escalação .
        /// </summary>
        /// <param name="pintIdEscalacao">Id da Escalação</param>
        /// <returns>Data de Escalação dos colaboradores</returns>
        /// <history>
        ///     [cmarchi] created 22/1/2010
        ///     [cmarchi] modify 22/2/2010
        ///     [haguiar] modify 27/10/2010
        ///     [haguiar_2] modify 04/12/2010
        ///     adicionar coluna horário flex
        ///     [haguiar_8829] modify 06/07/2011 15:06
        ///     adicionar coluna hora extra
        /// </history>
        public DataTable ObterEscalasExportação(int pintIdEscalacao)
        {
            DLEscala objDLEscala = new DLEscala();

            DataTable dttEscalacao = new DataTable();
            Collection<Escalacao> colEscalacao = null;

            try
            {
                colEscalacao = objDLEscala.ObterEscalaColaboradores(pintIdEscalacao);

                //adicionando as colunas
                dttEscalacao.Columns.Add("Escalação", Type.GetType("System.Int32"));
                dttEscalacao.Columns.Add("Início do Período", Type.GetType("System.String"));
                dttEscalacao.Columns.Add("Fim do Período", Type.GetType("System.String"));
                dttEscalacao.Columns.Add("Jornada", Type.GetType("System.String"));
                dttEscalacao.Columns.Add("Data/Hora", Type.GetType("System.String"));
                dttEscalacao.Columns.Add("Id Colaborador", Type.GetType("System.Int32"));
                dttEscalacao.Columns.Add("Colaborador", Type.GetType("System.String"));
                dttEscalacao.Columns.Add("Compensado", Type.GetType("System.Char"));
                dttEscalacao.Columns.Add("Férias/Licença", Type.GetType("System.Char"));
                dttEscalacao.Columns.Add("Folga", Type.GetType("System.Char"));
                dttEscalacao.Columns.Add("08 às 09 flex", Type.GetType("System.Char"));
                dttEscalacao.Columns.Add("Hora Extra", Type.GetType("System.Char"));

                for (int i = 0; i < colEscalacao.Count; i++)
                {
                    DataRow dtrLinha = dttEscalacao.NewRow();

                    dtrLinha[0] = colEscalacao[i].IdEscalacao;

                    //Data de Inicio e Final do Periodo
                    //Se for Compensado, licença ou folga, não exibe as horas / minutos
                    if (colEscalacao[i].ObjEscalacaoColDatas.Compensado == true ||
                        colEscalacao[i].ObjEscalacaoColDatas.Licenca == true ||
                        colEscalacao[i].ObjEscalacaoColDatas.Folga == true ||
                        colEscalacao[i].ObjEscalacaoColDatas.HorarioFlex == true)
                    {

                        dtrLinha[1] = colEscalacao[i].DataInicioPeriodo.ToShortDateString();
                        dtrLinha[2] = colEscalacao[i].DataFinalPeriodo.ToShortDateString();

                        dtrLinha[4] = colEscalacao[i].ObjEscalacaoColDatas.DataEscalacao.ToShortDateString();
                    }
                    else
                    {
                        dtrLinha[1] = colEscalacao[i].DataInicioPeriodo;
                        dtrLinha[2] = colEscalacao[i].DataFinalPeriodo;

                        dtrLinha[4] = colEscalacao[i].ObjEscalacaoColDatas.DataEscalacao;

                    }
                    dtrLinha[3] = colEscalacao[i].DescricaoJornada;

                    dtrLinha[5] = colEscalacao[i].ObjEscalacaoColDatas.IdColaborador;
                    dtrLinha[6] = colEscalacao[i].ObjEscalacaoColDatas.NomeColaborador;
                    dtrLinha[7] = (colEscalacao[i].ObjEscalacaoColDatas.Compensado == true) ? 'S' : 'N';
                    dtrLinha[8] = (colEscalacao[i].ObjEscalacaoColDatas.Licenca == true) ? 'S' : 'N';
                    dtrLinha[9] = (colEscalacao[i].ObjEscalacaoColDatas.Folga == true) ? 'S' : 'N';
                    dtrLinha[10] = (colEscalacao[i].ObjEscalacaoColDatas.HorarioFlex == true) ? 'S' : 'N';
                    dtrLinha[11] = (colEscalacao[i].ObjEscalacaoColDatas.HoraExtra == true) ? 'S' : 'N';

                    dttEscalacao.Rows.Add(dtrLinha);
                }

                return dttEscalacao;
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLEscala.Finalizar();
            }
        }
        #endregion


        #region ObterPeriodos Ronda
        /// <summary>
        /// Obtém últimos 5 períodos (semanal)
        /// </summary>
        /// <param name="pintId_Solicitacao">Id do tipo da solicitação</param>
        /// <returns>Lista de períodos</returns>
        /// <history>
        ///     [haguiar] created 08/11/2011 16:05
        /// </history>
        public Collection<string> ObterPeriodosAnteriores(int pintId_Solicitacao)
        {
            Collection<string> colPeriodos = new Collection<string>();
            //TODO Caio: Descomentar
            DateTime datDataAgora = DateTime.Now;
            //DateTime datDataAgora = new DateTime(2011, 6, 4);
            //TODO Caio: Descomentar
            DateTime datDataInicialSemanaCorrente = DateTime.Now;
            //DateTime datDataInicialSemanaCorrente = new DateTime(2011, 6, 4);

            //new DateTime(2011, 7, 4); //DateTime.Now;
            int intDiaDaSemana = (int)datDataInicialSemanaCorrente.DayOfWeek; //(int)DateTime.Now.DayOfWeek;

            switch (pintId_Solicitacao)
            {
                case (7):
                    //escalas

                    #region Períodos para as semanas anteriores

                    int i = 0;

                    //calcula a quantidade de dias para chegar na segunda-feira
                    int intSegundaFeira = 1 - (int)datDataAgora.DayOfWeek;

                    //calculando o primeiro período
                    DateTime datDataInicial = datDataAgora.AddDays(intSegundaFeira);

                    datDataInicial = datDataInicial.AddDays(-28); //4 semanas
                    DateTime datDataFinal = datDataInicial.AddDays(6);

                    //gerando os períodos
                    while (i < 5)
                    {
                        colPeriodos.Add(String.Format("{0:dd/MM/yyyy}", datDataInicial)
                                        + " à " + String.Format("{0:dd/MM/yyyy}", datDataFinal));

                        i++;

                        datDataInicial = datDataFinal.AddDays(1);
                        datDataFinal = datDataInicial.AddDays(6);
                    }
                    #endregion

                    break;
                case (9):
                    //trocas de horario
                    #region Períodos para as semanas anteriores

                    int j = 0;

                    //gerando os períodos
                    while (j < 16)
                    {
                        colPeriodos.Add(String.Format("{0:dd/MM/yyyy}", datDataAgora));

                        j++;

                        datDataAgora = datDataAgora.AddDays(-1);
                    }
                    #endregion


                    break;
            }



            return colPeriodos;
        }
        #endregion

        #region ObterHorariosColaboradoresNBloqueados
        /// <summary>
        /// Obtém os horários dos colaboradores que não estejam bloqueados.
        /// </summary>
        /// <returns>coleção de horários dos colaboradores não bloqueados</returns>
        /// <history>
        ///     [cmarchi] created 18/2/2010
        /// </history>
        public static Collection<HorarioColaboradores> ObterHorariosColaboradoresNBloqueados(
            Collection<HorarioColaboradores> pcolHorarioColaboradores)
        {
            Collection<HorarioColaboradores> colHorarioColaboradores = new Collection<HorarioColaboradores>();

            for (int i = 0; i < pcolHorarioColaboradores.Count; i++)
            {
                if (!pcolHorarioColaboradores[i].Bloqueado)
                {
                    colHorarioColaboradores.Add(pcolHorarioColaboradores[i]);
                }
            }

            return colHorarioColaboradores;
        }
        #endregion

        #region ObterPeriodos para troca de horário
        /// <summary>
        /// Obtém as datas disponíveis para importação de trocas de horário
        /// </summary>
        /// <param name="pintIdEscalaDpto">Id da Escala Departamental</param>
        /// <returns>Lista de períodos</returns>
        /// <history>
        ///     [haguiar] created 11/11/2011 16:23
        /// </history>
        public Collection<string> ObterPeriodosTrocaHorario(int pintIdEscalaDpto)
        {
            DLEscala objDLEscala = new DLEscala();

            try
            {
                return objDLEscala.ObterPeriodosTrocaHorario(pintIdEscalaDpto);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLEscala.Finalizar();
            }
        }
        #endregion

        #region ObterPeriodos
        /// <summary>
        /// Obtém um(s) período(s) da Escalação .
        /// </summary>
        /// <param name="pstrPeriodicidade">Descrição da Periodicidade</param>
        /// <param name="pintIdEscalaDpto">Id da Escala Departamental</param>
        /// <param name="pintIdEscalacao">Id da Escalação</param>
        /// <returns>Lista de períodos</returns>
        /// <history>
        ///     [cmarchi] created 21/1/2010
        ///     [haguiar] modify 27/10/2010
        ///     [haguiar_2] modify 16/12/2010
        ///     criar escala de segunda à domingo, sem restricoes.
        /// </history>
        public Collection<string> ObterPeriodos(string pstrPeriodicidade, int pintIdEscalaDpto, int pintIdEscalacao)
        {
            Collection<string> colPeriodos = new Collection<string>();

            //verifica a escalação
            if (pintIdEscalacao > 0)
            {
                //obtém o período da escalação
                Escalacao objEscalacao = this.Obter(pintIdEscalacao);

                if (objEscalacao != null)
                {

                    colPeriodos.Add(String.Format("{0:dd/MM/yyyy}", objEscalacao.DataInicioPeriodo)
                                    + " à " + String.Format("{0:dd/MM/yyyy}", objEscalacao.DataFinalPeriodo));

                    /*
                    colPeriodos.Add(objEscalacao.DataInicioPeriodo.ToShortDateString()
                                    + " à " + objEscalacao.DataFinalPeriodo.ToShortDateString());
                    */

                }
            }
            else
            {
                //cria os periodos de acordo com a descrição da periodicidade
                if (pstrPeriodicidade == "Mensal")
                {
                    //coleçao para ordenar
                    //List<string> pcolSort = new List<string>();

                    #region Primeiro período para o mês corrente
                        //Período para o Mês corrente
                        DateTime datDataInicialMesCorrente = ObterDataInicialProximoPeriodo(pintIdEscalaDpto).AddDays(1);
                    DateTime datDataAtual = datDataInicialMesCorrente;
                    DateTime datDataFinalMesCorrente = datDataInicialMesCorrente.AddMonths(1).AddDays(-1);

                        DataTable dtt = ObterPeriodosJaCadastrados(pintIdEscalaDpto, datDataInicialMesCorrente, datDataFinalMesCorrente);

                        if (dtt.Rows.Count == 0)
                        {

                            /*
                            colPeriodos.Add(datDataInicialMesCorrente.ToShortDateString()
                                       + " à " + datDataFinalMesCorrente.ToShortDateString());
                            */

                            colPeriodos.Add(String.Format("{0:dd/MM/yyyy}", datDataInicialMesCorrente)
                                            + " à " + String.Format("{0:dd/MM/yyyy}", datDataFinalMesCorrente));

                            /*
                            pcolSort.Add(datDataInicialMesCorrente.ToShortDateString()
                                       + " à " + datDataFinalMesCorrente.ToShortDateString());
                             */
                        }
                    #endregion

                    #region Período para o próximo mês
                        //calcula a quantidade de dias para chegar na segunda-feira

                        int intQtdadeMeses = 1;

                        //DateTime datDataAgora = DateTime.Now.AddMonths(intQtdadeMeses);

                        DateTime datDataAgora = datDataAtual.AddMonths(intQtdadeMeses);

                        int intMes = datDataAgora.Month;
                        int intAno = datDataAgora.Year;
                        int intTotalDiasMes = DateTime.DaysInMonth(intAno, intMes);

                        DateTime datDataInicial = DateTime.Parse("1/" + intMes.ToString() + "/" + intAno.ToString());
                        DateTime datDataFinal = DateTime.Parse(intTotalDiasMes + "/" + intMes.ToString() + "/" + intAno.ToString());

                        //verifca se há alguma escalação com o mesmo período
                        while (this.VerificarPeriodoEscalacao(pintIdEscalaDpto, datDataInicial, datDataFinal) == 1)
                        {
                            datDataAgora = datDataAgora.AddMonths(intQtdadeMeses);

                            intMes = datDataAgora.Month;
                            intAno = datDataAgora.Year;
                            intTotalDiasMes = DateTime.DaysInMonth(intAno, intMes);

                            datDataInicial = DateTime.Parse("1/" + intMes.ToString() + "/" + intAno.ToString());
                            datDataFinal = DateTime.Parse(intTotalDiasMes + "/" + intMes.ToString() + "/" + intAno.ToString());
                        }

                        colPeriodos.Add(String.Format("{0:dd/MM/yyyy}", datDataInicial)
                                        + " à " + String.Format("{0:dd/MM/yyyy}", datDataFinal));

                    /*
                        colPeriodos.Add(datDataInicial.ToShortDateString()
                                        + " à " + datDataFinal.ToShortDateString());
                    */

                    /*
                        pcolSort.Add(datDataInicial.ToShortDateString()
                                        + " à " + datDataFinal.ToShortDateString());

                        //organiza a lista de datas
                        //pcolSort.Sort(delegate(DateTime p1, DateTime p2) { return p1.CompareTo(p2); });
                    */
                    #endregion

                    #region "Períodos reprovados"
                        Collection<DateTime> colRetorno = null;

                        colRetorno = this.ObterListaPeriodosReprovados(pintIdEscalaDpto);

                        foreach (DateTime DataReprovada in colRetorno)
                        {
                            //foreach (string DataPeriodo in colPeriodos)
                            //{
                            //    if (Convert.ToDateTime(DataPeriodo) == DataReprovada)
                            //    {

                            //    }
                            //}

                            intMes = DataReprovada.Month;
                            intAno = DataReprovada.Year;
                            intTotalDiasMes = DateTime.DaysInMonth(intAno, intMes);

                            datDataInicial = DataReprovada; //DateTime.Parse("1/" + intMes.ToString() + "/" + intAno.ToString());
                            datDataFinal = DateTime.Parse(intTotalDiasMes + "/" + intMes.ToString() + "/" + intAno.ToString());

                            colPeriodos.Add(String.Format("{0:dd/MM/yyyy}", datDataInicial)
                                            + " à " + String.Format("{0:dd/MM/yyyy}", datDataFinal));

                            /*
                            colPeriodos.Add(datDataInicial.ToShortDateString()
                                            + " à " + datDataFinal.ToShortDateString());
                            */

                            /*
                            pcolSort.Add(datDataInicial.ToShortDateString()
                                            + " à " + datDataFinal.ToShortDateString());

                            pcolSort.Sort();
                            */

                        }

                    #endregion

                }
                else if (pstrPeriodicidade == "Semanal")
                {

                    #region Período para a semana corrente

                        DateTime datDataAgora = ObterDataInicialProximoPeriodo(pintIdEscalaDpto);

                        //De segunda-feira até quinta-feira adiciona-se um período para semana corrente
                        int intDiaDaSemana = (int)datDataAgora.DayOfWeek;

                        DateTime datDataInicialSemanaCorrente = datDataAgora.AddDays(1);
                        DateTime datDataFinalSemanaCorrente = DateTime.MinValue;

                        switch (intDiaDaSemana)
                        {
                            case (0): //Domingo
                                {
                                    datDataFinalSemanaCorrente = datDataInicialSemanaCorrente.AddDays(6);
                                    break;
                                }
                            case (1): //segunda-feira
                                {
                                    datDataFinalSemanaCorrente = datDataInicialSemanaCorrente.AddDays(5);
                                    break;
                                }
                            case (2): //terça-feira
                                {
                                    datDataFinalSemanaCorrente = datDataInicialSemanaCorrente.AddDays(4);
                                    break;
                                }
                            case (3): //quarta-feira
                                {
                                    datDataFinalSemanaCorrente = datDataInicialSemanaCorrente.AddDays(3);
                                    break;
                                }
                            case (4): //quinta-feira
                                {
                                    datDataFinalSemanaCorrente = datDataInicialSemanaCorrente.AddDays(2);
                                    break;
                                }

                            //se o primeiro dia da jornada for sábado/domingo, pula para a próxima semana.


                            case (5): //sexta-feira
                                {
                                    datDataFinalSemanaCorrente = datDataInicialSemanaCorrente.AddDays(1);
                                    break;
                                }
                            case (6): //sábado
                                {
                                    //encerra o período no domingo.
                                    datDataFinalSemanaCorrente = datDataInicialSemanaCorrente;
                                    break;
                        }
                    }

                        //if (intDiaDaSemana <= 4)
                        //{
                            if (this.VerificarPeriodoEscalacao(pintIdEscalaDpto, datDataInicialSemanaCorrente, datDataFinalSemanaCorrente) == 0)
                            {
                                /*
                                colPeriodos.Add(datDataInicialSemanaCorrente.ToShortDateString() + " à " +
                                    datDataFinalSemanaCorrente.ToShortDateString());
                                */

                                colPeriodos.Add(String.Format("{0:dd/MM/yyyy}", datDataInicialSemanaCorrente)
                                                + " à " + String.Format("{0:dd/MM/yyyy}", datDataFinalSemanaCorrente));

                            }
                        //}
                    #endregion

                    #region Períodos para as próximas semanas

                    int i = 0;

                    //calcula a quantidade de dias para chegar na segunda-feira
                    int intSegundaFeira = 7 - (int)datDataAgora.DayOfWeek;

                    //calculando o primeiro período
                    DateTime datDataInicial = datDataAgora.AddDays(intSegundaFeira + 1);
                    DateTime datDataFinal = datDataInicial.AddDays(6);

                    //gerando os períodos
                    while (i < 5)
                    {
                        if (this.VerificarPeriodoEscalacao(pintIdEscalaDpto, datDataInicial, datDataFinal) == 1)
                        {
                            datDataAgora = datDataInicial.AddDays(1);

                            //calcula a quantidade de dias para chegar na segunda-feira
                            intSegundaFeira = 7 - (int)datDataAgora.DayOfWeek;

                            //calculando o primeiro período
                            datDataInicial = datDataAgora.AddDays(intSegundaFeira + 1);
                            datDataFinal = datDataInicial.AddDays(6);
                        }
                        else
                        {

                            colPeriodos.Add(String.Format("{0:dd/MM/yyyy}", datDataInicial)
                                            + " à " + String.Format("{0:dd/MM/yyyy}", datDataFinal));

                            /*
                            colPeriodos.Add(datDataInicial.ToShortDateString() + " à " +
                                            datDataFinal.ToShortDateString());
                            */

                            i++;

                            datDataInicial = datDataFinal.AddDays(1);
                            datDataFinal = datDataInicial.AddDays(6);
                        }
                    }
                    #endregion

                    #region "Períodos reprovados"
                        Collection<DateTime> colRetorno = null;

                        colRetorno = this.ObterListaPeriodosReprovados(pintIdEscalaDpto);

                        foreach (DateTime DataReprovada in colRetorno)
                        {
                            //De segunda-feira até quinta-feira adiciona-se um período para semana corrente
                            intDiaDaSemana = (int)DataReprovada.DayOfWeek;

                            datDataInicialSemanaCorrente = DataReprovada;
                            datDataFinalSemanaCorrente = DateTime.MinValue;

                            switch (intDiaDaSemana)
                            {
                                case (0): //Domingo
                                    {
                                        datDataFinalSemanaCorrente = datDataInicialSemanaCorrente.AddDays(7);
                                        break;
                                    }
                                case (1): //segunda-feira
                                    {
                                        datDataFinalSemanaCorrente = datDataInicialSemanaCorrente.AddDays(6);
                                        break;
                                    }
                                case (2): //terça-feira
                                    {
                                        datDataFinalSemanaCorrente = datDataInicialSemanaCorrente.AddDays(5);
                                        break;
                                    }
                                case (3): //quarta-feira
                                    {
                                        datDataFinalSemanaCorrente = datDataInicialSemanaCorrente.AddDays(4);
                                        break;
                                    }
                                case (4): //quinta-feira
                                    {
                                        datDataFinalSemanaCorrente = datDataInicialSemanaCorrente.AddDays(3);
                                        break;
                                    }

                                //se o primeiro dia da jornada for sábado/domingo, pula para a próxima semana.


                                case (5): //sexta-feira
                                    {
                                        datDataFinalSemanaCorrente = datDataInicialSemanaCorrente.AddDays(2);
                                        break;
                                    }
                                case (6): //sábado
                                    {
                                        //encerra o período no domingo.
                                        datDataFinalSemanaCorrente = datDataInicialSemanaCorrente;
                                        break;
                                    }

                            }

                           // if (intDiaDaSemana <= 4)
                            //{

                                //já sabemos que o período existe (reprovado)
                                //if (this.VerificarPeriodoEscalacao(pintIdEscalaDpto, datDataInicialSemanaCorrente, datDataFinalSemanaCorrente) == 0)
                                //{


                                colPeriodos.Add(String.Format("{0:dd/MM/yyyy}", datDataInicialSemanaCorrente)
                                                + " à " + String.Format("{0:dd/MM/yyyy}", datDataFinalSemanaCorrente));


                                /*
                                colPeriodos.Add(datDataInicialSemanaCorrente.ToShortDateString() + " à " +
                                    datDataFinalSemanaCorrente.ToShortDateString());
                                */

                                //}
                            //}
                        }

                    #endregion
                }

            }
            return colPeriodos;
        }

        /// <summary>
        /// Obtém um(s) período(s) da Escalação .
        /// </summary>
        /// <param name="pstrPeriodicidade">Descrição da Periodicidade</param>
        /// <param name="pintIdEscalaDpto">Id da Escala Departamental</param>
        /// <param name="pintIdEscalacao">Id da Escalação</param>
        /// <returns>Lista de períodos</returns>
        /// <history>
        ///     [cfrancisco] created 23/05/2012
        /// </history>
        public Collection<string> ObterPeriodosNovaEscala(string pstrPeriodicidade, int pintIdEscalaDpto, int pintIdEscalacao)
        {
            Collection<string> colPeriodos = new Collection<string>();
            //verifica a escalação
            if (pintIdEscalacao > 0)
            {
                //obtém o período da escalação
                Escalacao objEscalacao = this.Obter(pintIdEscalacao);

                if (objEscalacao != null)
                {
                    colPeriodos.Add(String.Format("{0:dd/MM/yyyy} à {1:dd/MM/yyyy}", objEscalacao.DataInicioPeriodo, objEscalacao.DataFinalPeriodo));
                }
            }
            else
            {
                //cria os periodos de acordo com a descrição da periodicidade
                if (pstrPeriodicidade == "Mensal")
                {
                    #region Gerar datas
                    var dataInicialPrimeiroPeriodo = DateTime.Now.AddDays(1);
                    var dataFinalPrimeiroPeriodo = new DateTime(
                                                        dataInicialPrimeiroPeriodo.Year,
                                                        dataInicialPrimeiroPeriodo.Month,
                                                        DateTime.DaysInMonth(dataInicialPrimeiroPeriodo.Year, dataInicialPrimeiroPeriodo.Month)
                                                    );
                    DateTime dataFinalUltimoPeriodoCadastrado = ObterDataInicialProximoPeriodo(pintIdEscalaDpto);
                    double intQtdMeses = (dataFinalUltimoPeriodoCadastrado.Subtract(dataInicialPrimeiroPeriodo).Days / (365.25 / 12)) + 2;

                    //Add no primeiro periodo
                    colPeriodos.Add(
                        String.Format(
                            "{0:dd/MM/yyyy} à {1:dd/MM/yyyy}",
                            dataInicialPrimeiroPeriodo,
                            dataFinalPrimeiroPeriodo
                        )
                    );

                    int tempMes = dataFinalPrimeiroPeriodo.Month;
                    int tempAno = dataFinalPrimeiroPeriodo.Year;

                    //var dtTemp = dataFinalPrimeiroPeriodo;
                    for (int i = 0; i < intQtdMeses; i++)
                    {

                        if (tempMes + 1 > 12)
                        {
                            tempMes = 1;
                            tempAno++;
                        }
                        else
                        {
                            tempMes++;
                        }

                        colPeriodos.Add(
                            String.Format(
                                "{0:dd/MM/yyyy} à {1:dd/MM/yyyy}",
                                new DateTime(
                                    tempAno,
                                    tempMes,
                                    1
                                ),
                                new DateTime(
                                    tempAno,
                                    tempMes,
                                    DateTime.DaysInMonth(tempAno, tempMes)
                                )
                            )
                        );

                        //colPeriodos.Add(
                        //    String.Format(
                        //        "{0:dd/MM/yyyy} à {1:dd/MM/yyyy}",
                        //        new DateTime(
                        //            dataFinalPrimeiroPeriodo.Year,
                        //            dataFinalPrimeiroPeriodo.Month + i + 1,
                        //            1
                        //        ),
                        //        new DateTime(
                        //            dataFinalPrimeiroPeriodo.Year,
                        //            dataFinalPrimeiroPeriodo.Month + i + 1,
                        //            DateTime.DaysInMonth(dataFinalPrimeiroPeriodo.Year, dataFinalPrimeiroPeriodo.Month + i + 1)
                        //        )
                        //    )
                        //);
                    }

        #endregion

                }
                else if (pstrPeriodicidade == "Semanal")
                {

                    #region Gerar datas

                    var dataInicialPrimeiroPeriodo = DateTime.Now.AddDays(1);
                    var dataFinalPrimeiroPeriodo = dataInicialPrimeiroPeriodo.AddDays(7 - (int)dataInicialPrimeiroPeriodo.DayOfWeek);
                    DateTime dataFinalUltimoPeriodoCadastrado = ObterDataInicialProximoPeriodo(pintIdEscalaDpto);

                    int intQtdSemanas = (dataFinalUltimoPeriodoCadastrado.Subtract(dataInicialPrimeiroPeriodo).Days / 7) + 2;

                    //Add no primeiro periodo
                    colPeriodos.Add(
                        String.Format(
                            "{0:dd/MM/yyyy} à {1:dd/MM/yyyy}",
                            dataInicialPrimeiroPeriodo,
                            dataFinalPrimeiroPeriodo
                        )
                    );

                    for (int i = 0; i < intQtdSemanas; i++)
                    {
                        colPeriodos.Add(
                            String.Format(
                                "{0:dd/MM/yyyy} à {1:dd/MM/yyyy}",
                                dataFinalPrimeiroPeriodo.AddDays((i * 7) + 1),
                                dataFinalPrimeiroPeriodo.AddDays((i * 7) + 7)
                            )
                        );
                    }

                    #endregion
                }

            }
            return colPeriodos;
        }
        #endregion

        #region ObterPeriodos
        /// <summary>
        ///     Obtém um(s) período(s) da Escalação.
        /// </summary>
        /// <param name="pstrPeriodicidade">Descrição da Periodicidade</param>
        /// <param name="pintIdEscalaDpto">Id da Escala Departamental</param>
        /// <param name="pintIdEscalacao">Id da Escalação</param>
        /// <param name="pdatDataInicial">Data Inicial do próx período</param>
        /// <returns>Lista de períodos</returns>
        /// <history>
        ///     [tgerevini] created 20/9/2010
        ///     [haguiar] modify 08/11/2010
        /// </history>
        public Collection<string> ObterPeriodos(string pstrPeriodicidade, int pintIdEscalaDpto,
            int pintIdEscalacao, DateTime pdatDataInicial)
        {
            Collection<string> colPeriodos = new Collection<string>();

            //verifica a escalação
            if (pintIdEscalacao > 0)
            {
                //obtém o período da escalação
                Escalacao objEscalacao = this.Obter(pintIdEscalacao);

                if (objEscalacao != null)
                {
                    /*
                    colPeriodos.Add(objEscalacao.DataInicioPeriodo.ToShortDateString()
                                    + " à " + objEscalacao.DataFinalPeriodo.ToShortDateString());
                    */

                    colPeriodos.Add(String.Format("{0:dd/MM/yyyy}", objEscalacao.DataInicioPeriodo)
                                    + " à " + String.Format("{0:dd/MM/yyyy}", objEscalacao.DataFinalPeriodo));

                }
            }
            else
            {
                //cria os periodos de acordo com a descrição da periodicidade
                if (pstrPeriodicidade == "Mensal")
                {

                    #region Primeiro período para o mês corrente
                        //Período para o Mês corrente
                        DateTime datDataAtual = pdatDataInicial;

                        int intDiaAtual = datDataAtual.Day;
                        int intMesAtual = datDataAtual.Month;
                        int intAnoAtual = datDataAtual.Year;
                        int inTotalDiasMesCorrente = DateTime.DaysInMonth(intAnoAtual, intMesAtual);


                        DateTime datDataInicialMesCorrente = ObterDataInicialProximoPeriodo(pintIdEscalaDpto).AddDays(1);
                        DateTime datDataFinalMesCorrente = DateTime.Parse(inTotalDiasMesCorrente + "/" + intMesAtual.ToString() + "/" + intAnoAtual.ToString());

                        DataTable dtt = ObterPeriodosJaCadastrados(pintIdEscalaDpto, datDataInicialMesCorrente, datDataFinalMesCorrente);

                        if (dtt.Rows.Count == 0)
                        {
                            /*
                            colPeriodos.Add(datDataInicialMesCorrente.ToShortDateString()
                                       + " à " + datDataFinalMesCorrente.ToShortDateString());
                            */

                            colPeriodos.Add(String.Format("{0:dd/MM/yyyy}", datDataInicialMesCorrente)
                                            + " à " + String.Format("{0:dd/MM/yyyy}", datDataFinalMesCorrente));

                        }
                    #endregion

                    #region Período para o próximo mês
                        //calcula a quantidade de dias para chegar na segunda-feira
                        int intQtdadeMeses = 1;
                        DateTime datDataAgora = pdatDataInicial.AddMonths(intQtdadeMeses);

                        int intMes = datDataAgora.Month;
                        int intAno = datDataAgora.Year;
                        int intTotalDiasMes = DateTime.DaysInMonth(intAno, intMes);

                        DateTime datDataInicial = DateTime.Parse("1/" + intMes.ToString() + "/" + intAno.ToString());
                        DateTime datDataFinal = DateTime.Parse(intTotalDiasMes + "/" + intMes.ToString() + "/" + intAno.ToString());

                        //verifca se há alguma escalação com o mesmo período
                        while (this.VerificarPeriodoEscalacao(pintIdEscalaDpto, datDataInicial, datDataFinal) == 1)
                        {
                            datDataAgora = datDataAgora.AddMonths(intQtdadeMeses);

                            intMes = datDataAgora.Month;
                            intAno = datDataAgora.Year;
                            intTotalDiasMes = DateTime.DaysInMonth(intAno, intMes);

                            datDataInicial = DateTime.Parse("1/" + intMes.ToString() + "/" + intAno.ToString());
                            datDataFinal = DateTime.Parse(intTotalDiasMes + "/" + intMes.ToString() + "/" + intAno.ToString());
                        }

                        colPeriodos.Add(String.Format("{0:dd/MM/yyyy}", datDataInicial)
                                        + " à " + String.Format("{0:dd/MM/yyyy}", datDataFinal));

                    /*
                        colPeriodos.Add(datDataInicial.ToShortDateString()
                                        + " à " + datDataFinal.ToShortDateString());
                    */
                    #endregion


                }
                else if (pstrPeriodicidade == "Semanal")
                {
                    #region Período para a semana corrente

                    DateTime datDataAgora = Convert.ToDateTime(pdatDataInicial.ToShortDateString());

                    //De segunda-feira até quinta-feira adiciona-se um período para semana corrente
                    int intDiaDaSemana = (int)datDataAgora.DayOfWeek;

                    if (intDiaDaSemana >= 1 && intDiaDaSemana <= 4)
                    {
                        DateTime datDataInicialSemanaCorrente = datDataAgora.AddDays(1);
                        DateTime datDataFinalSemanaCorrente = DateTime.MinValue;

                        switch (intDiaDaSemana)
                        {
                            case (1): //segunda-feira
                                {
                                    datDataFinalSemanaCorrente = datDataInicialSemanaCorrente.AddDays(5);
                                    break;
                                }
                            case (2): //terça-feira
                                {
                                    datDataFinalSemanaCorrente = datDataInicialSemanaCorrente.AddDays(4);
                                    break;
                                }
                            case (3): //quarta-feira
                                {
                                    datDataFinalSemanaCorrente = datDataInicialSemanaCorrente.AddDays(3);
                                    break;
                                }
                            case (4): //quinta-feira
                                {
                                    datDataFinalSemanaCorrente = datDataInicialSemanaCorrente.AddDays(2);
                                    break;
                                }

                        }
                        if (this.VerificarPeriodoEscalacao(pintIdEscalaDpto, datDataInicialSemanaCorrente, datDataFinalSemanaCorrente) == 0)
                        {
                            /*
                            colPeriodos.Add(datDataInicialSemanaCorrente.ToShortDateString() + " à " +
                                datDataFinalSemanaCorrente.ToShortDateString());
                            */

                            colPeriodos.Add(String.Format("{0:dd/MM/yyyy}", datDataInicialSemanaCorrente)
                                            + " à " + String.Format("{0:dd/MM/yyyy}", datDataFinalSemanaCorrente));

                        }

                    }
                    #endregion

                    #region Períodos para as próximas semanas

                    int i = 0;


                    //calcula a quantidade de dias para chegar na segunda-feira
                    int intSegundaFeira = 7 - (int)datDataAgora.DayOfWeek;

                    //calculando o primeiro período
                    DateTime datDataInicial = datDataAgora.AddDays(intSegundaFeira + 1);
                    DateTime datDataFinal = datDataInicial.AddDays(6);


                    //gerando os períodos
                    while (i < 5)
                    {
                        if (this.VerificarPeriodoEscalacao(pintIdEscalaDpto, datDataInicial, datDataFinal) == 1)
                        {
                            datDataAgora = datDataInicial.AddDays(1);

                            //calcula a quantidade de dias para chegar na segunda-feira
                            intSegundaFeira = 7 - (int)datDataAgora.DayOfWeek;

                            //calculando o primeiro período
                            datDataInicial = datDataAgora.AddDays(intSegundaFeira + 1);
                            datDataFinal = datDataInicial.AddDays(6);
                        }
                        else
                        {
                            /*
                            colPeriodos.Add(datDataInicial.ToShortDateString() + " à " +
                                            datDataFinal.ToShortDateString());
                            */

                            colPeriodos.Add(String.Format("{0:dd/MM/yyyy}", datDataInicial)
                                            + " à " + String.Format("{0:dd/MM/yyyy}", datDataFinal));

                            i++;

                            datDataInicial = datDataFinal.AddDays(1);
                            datDataFinal = datDataInicial.AddDays(6);
                        }
                    }
                    #endregion
                }

            }
            return colPeriodos;
        }
        #endregion

        #region ObterPeríodosJaCadastrados
        /// ----------------------------------------------------------------------------- 
        /// <summary>
        ///     Lista os períodos das escalações já cadastrados
        /// </summary>
        /// <param name="pintIdEscalacao"></param>
        /// <param name="pdatDataInicio"></param>
        /// <param name="pdatDataFim"></param>
        /// <returns></returns>
        /// <history>
        ///      [tgerevini] created 19/08/2010
        /// </history>
        /// ----------------------------------------------------------------------------- 
        public DataTable ObterPeriodosJaCadastrados(int pintIdEscalacao, DateTime pdatDataInicio, DateTime pdatDataFim)
        {
            DLEscala objDLEscala = new DLEscala();

            try
            {
                return objDLEscala.ObterPeriodosCadastrados(pintIdEscalacao, pdatDataInicio, pdatDataFim);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLEscala.Finalizar();
            }
        }

        #endregion

        #region ObterDataInicialProximoPeriodo
        /// ----------------------------------------------------------------------------- 
        /// <summary>
        ///      Obtém a data inicial para o próximo período de uma escalação
        /// </summary>
        /// <param name="pintIdEscalacao"></param>
        /// <returns>DateTime</returns>
        /// <history>
        ///      [tgerevini] created 20/09/2010
        /// </history>
        /// ----------------------------------------------------------------------------- 
        public DateTime ObterDataInicialProximoPeriodo(int pintIdEscalacao)
        {
            DLEscala objDLEscala = new DLEscala();

            try
            {
                return objDLEscala.ObterDataInicialProximoPeriodo(pintIdEscalacao);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLEscala.Finalizar();
            }
        }

        #endregion

        #region ObterListaPeriodosReprovados
        /// ----------------------------------------------------------------------------- 
        /// <summary>
        ///      Obtém a lista dos períodos reprovados
        /// </summary>
        /// <param name="pintIdEscalaDpto">Id da Escala Departamental</param>
        /// <history>
        ///      [haguiar] created 08/11/2010
        /// </history>
        /// ----------------------------------------------------------------------------- 
        public Collection<DateTime> ObterListaPeriodosReprovados(int pintIdEscalaDpto)
        {
            DLEscala objDLEscala = new DLEscala();

            try
            {
                return objDLEscala.ObterListaPeriodosReprovados(pintIdEscalaDpto);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLEscala.Finalizar();
            }
        }

        #endregion
        #endregion

        #region Reprovar Escalação

        /// <summary>
        ///      Reprova as Escalações
        /// </summary>
        /// <param name="pstrIdEscalacao">Id das Escalações</param>
        /// <param name="pdecUsuarioAprovador">Id do Usuário Aprovador/Reprovador</param>
        /// <param name="pstrMotivoReprovacao">Motivo de Reprovação das Escalações</param>
        /// <history>
        ///      [cmarchi] created 28/01/2010
        /// </history>
        public void ReprovarEscalacao(string pstrIdEscalacao, decimal pdecUsuarioAprovador,
            string pstrMotivoReprovacao)
        {
            DLEscala objDLEscala = new DLEscala();

            try
            {
                objDLEscala.ReprovarEscalacao(pstrIdEscalacao, Convert.ToInt32(pdecUsuarioAprovador),
                    pstrMotivoReprovacao);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLEscala.Finalizar();
            }
        }
        #endregion

        #region VerificarPeriodoEscalacao
        /// <summary>
        /// Verifica se existe uma escalação para o período informado.
        /// </summary>
        /// <param name="pintIdEscalaDpto">Id da Escala Departamental</param>
        /// <param name="pdatInicioPeriodo">Data do início do período</param>
        /// <param name="pdatFinalPeriodo">Data final do período</param>
        /// <returns>Escalação existe - 1, Escalação não existe - 0, Erro - -1</returns>
        /// <history>
        ///     [cmarchi] created 22/2/2010
        /// </history>
        public int VerificarPeriodoEscalacao(int pintIdEscalaDpto, DateTime pdatInicioPeriodo,
            DateTime pdatFinalPeriodo)
        {
            DLEscala objDLEscala = new DLEscala();

            try
            {
                return objDLEscala.VerificarPeriodoEscalacao(pintIdEscalaDpto, pdatInicioPeriodo, pdatFinalPeriodo);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLEscala.Finalizar();
            }
        }
        #endregion
    }
}
