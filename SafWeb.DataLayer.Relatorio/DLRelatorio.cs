using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using FrameWork.DataLayer.Utilitarios;
using SafWeb.Model.Relatorio;
using FrameWork.BusinessLayer.Utilitarios;

namespace SafWeb.DataLayer.Relatorio
{
    public class DLRelatorio : DALFWBase
    {
        #region Construtor
        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// Constructor 
        /// </summary> 
        /// <history> 
        /// [mribeiro] 23/07/2009 Created 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public DLRelatorio()
        {
            strConnection = DALUtilitarios.GetStringConnection(DALUtilitarios.ConexoesDisponiveis.ConnectionFW);
            conProvider = new DALProvider(strConnection);
            intCommandTimeOut = DALUtilitarios.ObterCommandTimeOut();
        }
        #endregion

        #region Listar Visitantes Agendados

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// Lista registros da base em uma Collection 
        /// </summary> 
        /// <returns>Collection RelatorioVisitas</returns> 
        /// <history> 
        ///     [mribeiro] 22/6/2009 Created 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
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
            Collection<SafWeb.Model.Relatorio.RelatorioVisitas> colRetorno = null;

            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_RELATORIO_VISITAS_AGENDADAS");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Executa a função no banco de dados 
            System.Data.IDataReader idrRetorno;
            SafWeb.Model.Relatorio.RelatorioVisitas objRetorno = null;
            colRetorno = new Collection<SafWeb.Model.Relatorio.RelatorioVisitas>();

            //Parâmetros de entrada
            if (pintCodSolicitacao > 0) {
                cmdCommand.Parameters.Add(new FWParameter("@Id_Solicitacao", FWDbType.Int32));
                cmdCommand.Parameters["@Id_Solicitacao"].Value = pintCodSolicitacao;
            }

            if (pintCodEmpresa > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Id_Empresa", FWDbType.Int32));
                cmdCommand.Parameters["@Id_Empresa"].Value = pintCodEmpresa;
            }

            if (pintCodArea > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Id_Area", FWDbType.Int32));
                cmdCommand.Parameters["@Id_Area"].Value = pintCodArea;
            }

            if (pintCodRegional > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Id_Regional", FWDbType.Int32));
                cmdCommand.Parameters["@Id_Regional"].Value = pintCodRegional;
            }

            if (pintCodFilial > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Id_Filial", FWDbType.Int32));
                cmdCommand.Parameters["@Id_Filial"].Value = pintCodFilial;
            }

            if (pintCodStatusSolic > -1)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Id_StatusSolicitacao", FWDbType.Int32));
                cmdCommand.Parameters["@Id_StatusSolicitacao"].Value = pintCodStatusSolic;
            }

            if (pintCodTipoSolic > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Id_TipoSolicitacao", FWDbType.Int32));
                cmdCommand.Parameters["@Id_TipoSolicitacao"].Value = pintCodTipoSolic;
            }

            if (pintCodTipoCol > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Id_TipoColaborador", FWDbType.Int32));
                cmdCommand.Parameters["@Id_TipoColaborador"].Value = pintCodTipoCol;
            }

            if (pdatInicioVisita != null)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Dt_InicioVisita", FWDbType.DateTime));
                cmdCommand.Parameters["@Dt_InicioVisita"].Value = pdatInicioVisita.Value.ToString("yyyy-MM-dd") + " 00:00:00.000";
            }

            if (pdatFimVisita != null)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Dt_FimVisita", FWDbType.DateTime));
                cmdCommand.Parameters["@Dt_FimVisita"].Value = pdatFimVisita.Value.ToString("yyyy-MM-dd") + " 23:59:59.998";
            }

            if (pdatInicioVisita != null && pdatFimVisita == null)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Dt_FimVisita", FWDbType.DateTime));
                cmdCommand.Parameters["@Dt_FimVisita"].Value = pdatInicioVisita.Value.ToString("yyyy-MM-dd") + " 23:59:59.998";
            }

            if (pdatInicioVisita == null && pdatFimVisita != null)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Dt_InicioVisita", FWDbType.DateTime));
                cmdCommand.Parameters["@Dt_InicioVisita"].Value = pdatFimVisita.Value.ToString("yyyy-MM-dd") + " 00:00:00.000";
            }

            if (pstrNomVisitado != String.Empty)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Nom_Visitado", FWDbType.Varchar, 50));
                cmdCommand.Parameters["@Nom_Visitado"].Value = pstrNomVisitado;
            }

            if (pstrNomVisitante != String.Empty)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Nom_Visitante", FWDbType.Varchar, 50));
                cmdCommand.Parameters["@Nom_Visitante"].Value = pstrNomVisitante;
            }

            if (pintLista == 1)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Lista", FWDbType.Byte));
                cmdCommand.Parameters["@Lista"].Value = pintLista;
            }

            try
            {
                idrRetorno = conProvider.ExecuteDataReader(cmdCommand);

                while (idrRetorno.Read())
                {
                    objRetorno = new SafWeb.Model.Relatorio.RelatorioVisitas();
                    objRetorno.FromIDataReader(idrRetorno);
                    colRetorno.Add(objRetorno);
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return colRetorno;
        }

        #endregion

        #region Listar Escalas Agendadas
        /// <summary>
        ///      Lista Escalas Agendadas
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
        /// <returns>Lista Escalas</returns>
        /// /// <history>
        ///      [tgerevini] created 15/06/2010
        /// </history>
        public Collection<RelatorioEscalas> ListarEscalasAgendadas(decimal pdecCodUsurio, decimal pdecCodEscalacao, decimal pdecCodRegional, decimal pdecCodFilial,
            decimal pdecCodStatusAprov, decimal pdecCodTipoSol, decimal pdecCodEscalaDepto, string pstrDesSolicitante, string pstrDesColaborador,
            string pstrDesAprovador, DateTime? pdatDataInicio, DateTime? pdatDataFinal)
        {
            Collection<RelatorioEscalas> colRetorno = null;

            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_RelatorioEscalasAgendadas");
            //FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_HistoricoEscala");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            if (pdecCodEscalacao > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@ID_ESCALACAO", FWDbType.Int32));
                cmdCommand.Parameters["@ID_ESCALACAO"].Value = pdecCodEscalacao;
            }

            if (pdecCodRegional > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@ID_REGIONAL", FWDbType.Int32));
                cmdCommand.Parameters["@ID_REGIONAL"].Value = pdecCodRegional;
            }

            if (pdecCodFilial > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@ID_FILIAL", FWDbType.Int32));
                cmdCommand.Parameters["@ID_FILIAL"].Value = pdecCodFilial;
            }

            if (pdecCodStatusAprov > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@ID_STATUSAPROVACAO", FWDbType.Int32));
                cmdCommand.Parameters["@ID_STATUSAPROVACAO"].Value = pdecCodStatusAprov;
            }

            if (pdecCodTipoSol > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@ID_TIPOSOLICITACAO", FWDbType.Int32));
                cmdCommand.Parameters["@ID_TIPOSOLICITACAO"].Value = pdecCodTipoSol;
            }

            if (pdecCodEscalaDepto > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@ID_ESCALADEPARTAMENTAL", FWDbType.Int32));
                cmdCommand.Parameters["@ID_ESCALADEPARTAMENTAL"].Value = pdecCodEscalaDepto;
            }

            if (pstrDesSolicitante != string.Empty)
            {
                cmdCommand.Parameters.Add(new FWParameter("@DES_SOLICITANTE", FWDbType.Varchar, 50));
                cmdCommand.Parameters["@DES_SOLICITANTE"].Value = pstrDesSolicitante;
            }

            if (pstrDesColaborador != string.Empty)
            {
                cmdCommand.Parameters.Add(new FWParameter("@DES_COLABORADOR", FWDbType.Varchar, 50));
                cmdCommand.Parameters["@DES_COLABORADOR"].Value = pstrDesColaborador;
            }

            if (pstrDesAprovador != string.Empty)
            {
                cmdCommand.Parameters.Add(new FWParameter("@DES_APROVADOR", FWDbType.Varchar, 50));
                cmdCommand.Parameters["@DES_APROVADOR"].Value = pstrDesAprovador;
            }

            if (pdatDataInicio.HasValue)
            {
                cmdCommand.Parameters.Add(new FWParameter("@DT_DATAINICIO", FWDbType.DateTime));
                cmdCommand.Parameters["@DT_DATAINICIO"].Value = pdatDataInicio;
            }

            if (pdatDataFinal.HasValue)
            {
                cmdCommand.Parameters.Add(new FWParameter("@DT_DATAFINAL", FWDbType.DateTime));
                cmdCommand.Parameters["@DT_DATAFINAL"].Value = pdatDataFinal;
            }

            //Executa a função no banco de dados 
            System.Data.IDataReader idrRetorno;
            RelatorioEscalas objRetorno = null;
            colRetorno = new Collection<RelatorioEscalas>();

            idrRetorno = conProvider.ExecuteDataReader(cmdCommand);

            while (idrRetorno.Read())
            {
                objRetorno = new RelatorioEscalas();
                objRetorno.FromIDataReader(idrRetorno);
                colRetorno.Add(objRetorno);
            }

            return colRetorno;
        }
        #endregion

        #region Listar Entradas

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// Lista registros da base em uma Collection 
        /// </summary> 
        /// <returns>Collection RelatorioEntradas</returns> 
        /// <history> 
        ///     [mribeiro] 27/6/2009 Created 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
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
            Collection<SafWeb.Model.Relatorio.RelatorioEntradas> colRetorno = null;

            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_RELATORIO_ENTRADAS");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Executa a função no banco de dados 
            System.Data.IDataReader idrRetorno;
            SafWeb.Model.Relatorio.RelatorioEntradas objRetorno = null;
            colRetorno = new Collection<SafWeb.Model.Relatorio.RelatorioEntradas>();

            //Parâmetros de entrada
            if (pintCodSolicitacao > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Id_Solicitacao", FWDbType.Int32));
                cmdCommand.Parameters["@Id_Solicitacao"].Value = pintCodSolicitacao;
            }

            if (pintCodEmpresa > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Id_Empresa", FWDbType.Int32));
                cmdCommand.Parameters["@Id_Empresa"].Value = pintCodEmpresa;
            }

            if (pintCodArea > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Id_Area", FWDbType.Int32));
                cmdCommand.Parameters["@Id_Area"].Value = pintCodArea;
            }

            if (pintCodRegional > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Id_Regional", FWDbType.Int32));
                cmdCommand.Parameters["@Id_Regional"].Value = pintCodRegional;
            }

            if (pintCodFilial > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Id_Filial", FWDbType.Int32));
                cmdCommand.Parameters["@Id_Filial"].Value = pintCodFilial;
            }

            if (pintCodTipoCol > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Id_TipoColaborador", FWDbType.Int32));
                cmdCommand.Parameters["@Id_TipoColaborador"].Value = pintCodTipoCol;
            }

            if (pdatInicioVisita != null)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Dt_Entrada", FWDbType.DateTime));
                cmdCommand.Parameters["@Dt_Entrada"].Value = pdatInicioVisita.Value.ToString("yyyy-MM-dd") + " 00:00:00.000";
            }

            if (pdatFimVisita != null)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Dt_Saida", FWDbType.DateTime));
                cmdCommand.Parameters["@Dt_Saida"].Value = pdatFimVisita.Value.ToString("yyyy-MM-dd") + " 23:59:59.998";
            }

            if (pdatInicioVisita != null && pdatFimVisita == null)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Dt_Saida", FWDbType.DateTime));
                cmdCommand.Parameters["@Dt_Saida"].Value = pdatInicioVisita.Value.ToString("yyyy-MM-dd") + " 23:59:59.998";
            }

            if (pdatInicioVisita == null && pdatFimVisita != null)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Dt_Entrada", FWDbType.DateTime));
                cmdCommand.Parameters["@Dt_Entrada"].Value = pdatFimVisita.Value.ToString("yyyy-MM-dd") + " 00:00:00.000";
            }

            if (pstrNomVisitado != String.Empty)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Nom_Visitado", FWDbType.Varchar, 50));
                cmdCommand.Parameters["@Nom_Visitado"].Value = pstrNomVisitado;
            }

            if (pstrNomVisitante != String.Empty)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Nom_Visitante", FWDbType.Varchar, 50));
                cmdCommand.Parameters["@Nom_Visitante"].Value = pstrNomVisitante;
            }

            if (pintLista == 1)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Lista", FWDbType.Byte));
                cmdCommand.Parameters["@Lista"].Value = pintLista;
            }

            if (pstrNumCracha != String.Empty)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Num_Cracha", FWDbType.Varchar, 18));
                cmdCommand.Parameters["@Num_Cracha"].Value = pstrNumCracha;
            }

            if (pintCodEstado > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Id_Estado", FWDbType.Int32));
                cmdCommand.Parameters["@Id_Estado"].Value = pintCodEstado;
            }

            if (pintCodVeiculo > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Id_Veiculo", FWDbType.Int32));
                cmdCommand.Parameters["@Id_Veiculo"].Value = pintCodVeiculo;
            }

            try
            {
                idrRetorno = conProvider.ExecuteDataReader(cmdCommand);

                while (idrRetorno.Read())
                {
                    objRetorno = new SafWeb.Model.Relatorio.RelatorioEntradas();
                    objRetorno.FromIDataReader(idrRetorno);
                    colRetorno.Add(objRetorno);
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return colRetorno;
        }

        #endregion

        public Collection<RelatorioEscalas> ListarEscalasImportadas(int pintNumeroEscala, int pintEscalaDepartamental, int pintRegional, int pintFilial, string pstrSolicitante, string pstrColaborador, string pstrAprovador, int pintStatus, int pintTipoSolicitacao, DateTime? pdatDataInicio, DateTime? pdatDataFinal, int intIdUsuarioSolicitante)
        {
            FWCommand command = new FWCommand("SP_SAF_SEL_ESCALACAO_HIST_CRW");
            command.CommandTimeout = this.intCommandTimeOut;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            if (pintNumeroEscala > 0)
            {
                command.Parameters.Add(new FWParameter("@ID_ESCALACAO", FWDbType.Int32));
                command.Parameters["@ID_ESCALACAO"].Value = (object)pintNumeroEscala;
            }
            if (pintEscalaDepartamental > 0)
            {
                command.Parameters.Add(new FWParameter("@Id_EscalaDepartamental", FWDbType.Int32));
                command.Parameters["@Id_EscalaDepartamental"].Value = (object)pintEscalaDepartamental;
            }
            if (pintRegional > 0)
            {
                command.Parameters.Add(new FWParameter("@Id_Regional", FWDbType.Int32));
                command.Parameters["@Id_Regional"].Value = (object)pintRegional;
            }
            if (pintFilial > 0)
            {
                command.Parameters.Add(new FWParameter("@Id_Filial", FWDbType.Int32));
                command.Parameters["@Id_Filial"].Value = (object)pintFilial;
            }
            if (!string.IsNullOrEmpty(pstrSolicitante))
            {
                command.Parameters.Add(new FWParameter("@Des_Solicitante", FWDbType.Varchar, 50));
                command.Parameters["@Des_Solicitante"].Value = (object)pstrSolicitante;
            }
            if (!string.IsNullOrEmpty(pstrColaborador))
            {
                command.Parameters.Add(new FWParameter("@Des_Colaborador", FWDbType.Varchar, 50));
                command.Parameters["@Des_Colaborador"].Value = (object)pstrColaborador;
            }
            if (!string.IsNullOrEmpty(pstrAprovador))
            {
                command.Parameters.Add(new FWParameter("@Des_Aprovador", FWDbType.Varchar, 50));
                command.Parameters["@Des_Aprovador"].Value = (object)pstrAprovador;
            }
            if (pintStatus == -1 | pintStatus > 0)
            {
                if (pintStatus == -1)
                    pintStatus = 0;
                command.Parameters.Add(new FWParameter("@Id_Status", FWDbType.Int32));
                command.Parameters["@Id_Status"].Value = (object)pintStatus;
            }
            if (pintTipoSolicitacao > 0)
            {
                command.Parameters.Add(new FWParameter("@Id_TipoSolicitacao", FWDbType.Int32));
                command.Parameters["@Id_TipoSolicitacao"].Value = (object)pintTipoSolicitacao;
            }
            if (pdatDataInicio.HasValue)
            {
                command.Parameters.Add(new FWParameter("@Dt_DataInicio", FWDbType.DateTime));
                command.Parameters["@Dt_DataInicio"].Value = (object)pdatDataInicio.Value;
            }
            if (pdatDataFinal.HasValue)
            {
                command.Parameters.Add(new FWParameter("@Dt_DataFinal", FWDbType.DateTime));
                command.Parameters["@Dt_DataFinal"].Value = (object)pdatDataFinal.Value;
            }
            command.Parameters.Add(new FWParameter("@Id_UsuarioSolicitante", FWDbType.Int32));
            command.Parameters["@Id_UsuarioSolicitante"].Value = (object)intIdUsuarioSolicitante;
            Collection<RelatorioEscalas> collection = new Collection<RelatorioEscalas>();
            System.Data.IDataReader dataReader = this.conProvider.ExecuteDataReader(command);
            while (dataReader.Read())
            {
                RelatorioEscalas relatorioEscalas = new RelatorioEscalas();
                relatorioEscalas.FromIDataReader(dataReader);
                collection.Add(relatorioEscalas);
            }
            return collection;
        }

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
    }
}
