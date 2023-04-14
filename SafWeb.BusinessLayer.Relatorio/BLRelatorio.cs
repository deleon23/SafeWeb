using System;
using System.Collections.Generic;
using System.Text;
using FrameWork.BusinessLayer.Utilitarios;
using SafWeb.DataLayer.Relatorio;
using System.Collections.ObjectModel;
using SafWeb.Model.Relatorio;

namespace SafWeb.BusinessLayer.Relatorio
{
    public class BLRelatorio
    {
        #region Listar Visitantes Agendados

        /// <summary>
        ///      Lita os visitantes agendados 
        /// </summary>
        /// <history>
        ///      [mribeiro] created 22/06/2009
        /// </history>
        /// <returns></returns>
        public Collection<SafWeb.Model.Relatorio.RelatorioVisitas> ListarVisitantesAgendados(int pintCodSolicitacao,
                                                                                             int pintCodEmpresa,
                                                                                             int pintCodArea,
                                                                                             int pintCodRegional,
                                                                                             int pintCodFilial,
                                                                                             int pintCodStatusSolic,
                                                                                             int pintCodTipoSolic,
                                                                                             int pintCodTipoCol,
                                                                                             DateTime? pdatInicioVisita,
                                                                                             DateTime? pdatFimVisita,
                                                                                             string pstrNomVisitado,
                                                                                             string pstrNomVisitante,
                                                                                             int pintLista)
        {
            DLRelatorio objDLRelatorio = new DLRelatorio();

            try
            {
                return objDLRelatorio.ListarVisitantesAgendados(pintCodSolicitacao,
                                                                pintCodEmpresa,
                                                                pintCodArea,
                                                                pintCodRegional,
                                                                pintCodFilial,
                                                                pintCodStatusSolic,
                                                                pintCodTipoSolic,
                                                                pintCodTipoCol,
                                                                pdatInicioVisita,
                                                                pdatFimVisita,
                                                                pstrNomVisitado,
                                                                pstrNomVisitante,
                                                                pintLista);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLRelatorio.Finalizar();
            }
        }

        #endregion

        #region Listar Escalas Agendadas
        /// <summary>
        ///     Lista Escalas Agendadas dos colaboradores
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
        ///      [tgerevini] created 15/06/2010
        /// </history>
        public Collection<RelatorioEscalas> ListarEscalasAgendadas(decimal pdecCodUsurio, decimal pdecCodEscalacao, decimal pdecCodRegional, decimal pdecCodFilial,
            decimal pdecCodStatusAprov, decimal pdecCodTipoSol, decimal pdecCodEscalaDepto, string pstrDesSolicitante, string pstrDesColaborador,
            string pstrDesAprovador, DateTime? pdatDataInicio, DateTime? pdatDataFinal)
        {
            DLRelatorio objDLRelatorio = new DLRelatorio();

            try
            {
                return objDLRelatorio.ListarEscalasAgendadas(pdecCodUsurio, pdecCodEscalacao, pdecCodRegional, pdecCodFilial, pdecCodStatusAprov, pdecCodTipoSol,
                    pdecCodEscalaDepto, pstrDesSolicitante, pstrDesColaborador, pstrDesAprovador, pdatDataInicio, pdatDataFinal);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLRelatorio.Finalizar();
            }
        }
        #endregion

        
        #region Listar Escalas Importadas
        /// <summary>
        ///     Lista Escalas Importadas do CREW
        /// </summary>
        /// <param name="pdecCodUsurio"></param>
        /// <param name="pdecCodEscalacao"></param>
        /// <param name="pdecCodRegional"></param>
        /// <param name="pdecCodFilial"></param>
        /// <param name="pdecCodTipoSol"></param>
        /// <param name="pdecCodEscalaDepto"></param>
        /// <param name="pstrDesSolicitante"></param>
        /// <param name="pstrDesColaborador"></param>
        /// <param name="pdatDataInicio"></param>
        /// <param name="pdatDataFinal"></param>
        /// <returns>Lista de Escalação</returns>
        ///  <history>
        ///      [haguiar SDM 9004] created 01/08/2011 08:07
        /// </history>
        public Collection<RelatorioEscalas> ListarEscalasImportadas(int pintNumeroEscala, int pintEscalaDepartamental, int pintRegional,
            int pintFilial, string pstrSolicitante, string pstrColaborador,
            string pstrAprovador, int pintStatus, int pintTipoSolicitacao, DateTime? pdatDataInicio,
            DateTime? pdatDataFinal, int intIdUsuarioSolicitante)
        {
            DLRelatorio objDLRelatorio = new DLRelatorio();

            try
            {
                return objDLRelatorio.ListarEscalasImportadas(pintNumeroEscala, pintEscalaDepartamental, pintRegional, pintFilial,
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
                objDLRelatorio.Finalizar();
            }
        }
        #endregion
        

        #region Listar Entradas

        /// <summary>
        ///      Lita as entradas de visitantes 
        /// </summary>
        /// <history>
        ///      [mribeiro] created 27/06/2009
        /// </history>
        /// <returns></returns>
        public Collection<SafWeb.Model.Relatorio.RelatorioEntradas> ListarEntradas(int pintCodSolicitacao,
                                                                                   int pintCodEmpresa,
                                                                                   int pintCodArea,
                                                                                   int pintCodRegional,
                                                                                   int pintCodFilial,
                                                                                   int pintCodTipoCol,
                                                                                   DateTime? pdatInicioVisita,
                                                                                   DateTime? pdatFimVisita,
                                                                                   string pstrNomVisitado,
                                                                                   string pstrNomVisitante,
                                                                                   int pintLista,
                                                                                   string pstrNumCracha,
                                                                                   int pintCodEstado,
                                                                                   int pintCodVeiculo)
        {
            DLRelatorio objDLRelatorio = new DLRelatorio();

            try
            {
                return objDLRelatorio.ListarEntradas(pintCodSolicitacao,
                                                     pintCodEmpresa,
                                                     pintCodArea,
                                                     pintCodRegional,
                                                     pintCodFilial,
                                                     pintCodTipoCol,
                                                     pdatInicioVisita,
                                                     pdatFimVisita,
                                                     pstrNomVisitado,
                                                     pstrNomVisitante,
                                                     pintLista,
                                                     pstrNumCracha,
                                                     pintCodEstado,
                                                     pintCodVeiculo);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLRelatorio.Finalizar();
            }
        }

        #endregion

        #region Listar Acesso Contingente

        /// <summary>
        ///     Listar Acesso Contingente
        /// </summary>
        /// <history>
        ///      [haguiar] created 08/04/2011 15:13
        /// </history>
        /// <returns></returns>
        public Collection<SafWeb.Model.Relatorio.RelatorioAcessoContingencia> ListarAcessoContingencia(string pstrNomColaborador,
                                                                                                       int pintIdAprovador,
                                                                                                       DateTime? pdatDtInicio,
                                                                                                       DateTime? pdatDtFim)
        {
            DLRelatorio objDLRelatorio = new DLRelatorio();

            try
            {
                return objDLRelatorio.ListarAcessoContingencia(pstrNomColaborador,
                                                                pintIdAprovador,
                                                                pdatDtInicio,
                                                                pdatDtFim);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLRelatorio.Finalizar();
            }
        }

        #endregion
    }
}
