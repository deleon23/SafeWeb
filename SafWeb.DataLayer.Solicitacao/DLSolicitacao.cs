using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using FrameWork.DataLayer.Utilitarios;
using System.Data;

namespace SafWeb.DataLayer.Solicitacao
{
    public class DLSolicitacao : DALFWBase
    {
        #region Construtor

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// Constructor 
        /// </summary> 
        /// <history> 
        /// [vsantos] 06/07/2009 Created 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public DLSolicitacao()
        {
            strConnection = DALUtilitarios.GetStringConnection(DALUtilitarios.ConexoesDisponiveis.ConnectionFW);
            conProvider = new DALProvider(strConnection);
            intCommandTimeOut = DALUtilitarios.ObterCommandTimeOut();
        }

        #endregion

        #region Listar Solicitações Cracha Titular Pendentes Aprovação

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        ///  Lista de solicitações cracha titular pendentes aprovação
        /// </summary> 
        /// <returns>Collection Solicitação</returns> 
        /// <history> 
        ///     [haguiar] 28/02/2012 09:18
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public Collection<SafWeb.Model.Solicitacao.PermissaoCrachaTitular> ListarSolicPendAprovCrachaTitular(int pintId_UsuarioAprovador)
        {
            Collection<SafWeb.Model.Solicitacao.PermissaoCrachaTitular> colRetorno = null;

            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_SOLICITACAO_APROV_CRACHATITULAR");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Parâmetros de entrada
            if (pintId_UsuarioAprovador > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Id_UsuarioAprovador", FWDbType.Int32));
                cmdCommand.Parameters["@Id_UsuarioAprovador"].Value = pintId_UsuarioAprovador;
            }

            //Executa a função no banco de dados 
            System.Data.IDataReader idrRetorno;
            SafWeb.Model.Solicitacao.PermissaoCrachaTitular objRetorno = null;
            colRetorno = new Collection<SafWeb.Model.Solicitacao.PermissaoCrachaTitular>();

            idrRetorno = conProvider.ExecuteDataReader(cmdCommand);

            while (idrRetorno.Read())
            {
                objRetorno = new SafWeb.Model.Solicitacao.PermissaoCrachaTitular();
                objRetorno.FromIDataReader(idrRetorno);
                colRetorno.Add(objRetorno);
            }

            return colRetorno;
        }

        #endregion

        #region Listar Solicitações Pendentes Aprovação

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        ///  Lista de solicitações pendentes aprovação
        /// </summary> 
        /// <returns>Collection Solicitação</returns> 
        /// <history> 
        ///     [vsantos] 06/07/2009 Created 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public Collection<SafWeb.Model.Solicitacao.Solicitacao> ListarSolicPendAprov(decimal pdecCodUsuAprov)
        {
            Collection<SafWeb.Model.Solicitacao.Solicitacao> colRetorno = null;

            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_SOLICITACAO_APROV");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Parâmetros de entrada
            if (pdecCodUsuAprov > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@USU_N_CODIGO", FWDbType.Int32));
                cmdCommand.Parameters["@USU_N_CODIGO"].Value = pdecCodUsuAprov;
            }

            //Executa a função no banco de dados 
            System.Data.IDataReader idrRetorno;
            SafWeb.Model.Solicitacao.Solicitacao objRetorno = null;
            colRetorno = new Collection<SafWeb.Model.Solicitacao.Solicitacao>();

            idrRetorno = conProvider.ExecuteDataReader(cmdCommand);

            while (idrRetorno.Read())
            {
                objRetorno = new SafWeb.Model.Solicitacao.Solicitacao();
                objRetorno.FromIDataReader(idrRetorno);
                colRetorno.Add(objRetorno);
            }

            return colRetorno;
        }

        #endregion

        #region Alterar Fluxo Aprovação

        /// <summary>
        ///      Alterar Fluxo Aprovação
        /// </summary>
        /// <history>
        ///      [vsantos] created 06/07/2009
        ///      [haguiar] modify 25/04/2011 15:13
        ///      incluir parametro Flg_AprovaAreaTI
        ///      [haguiar] modify 24/02/2012 14:16
        ///      incluir parametro Flg_AprovaCracha
        /// </history>
        /// <returns></returns>
        public bool AlterarFluxoAprov(SafWeb.Model.Solicitacao.FluxoAprovacao pbojFluxoAprov)
        {
            bool blnRetorno = false;

            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_UPD_FLUXO_APROV");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Parâmetros de entrada
            cmdCommand.Parameters.Add(new FWParameter("@Id_FluxoAprovacao", FWDbType.Int32));
            cmdCommand.Parameters["@Id_FluxoAprovacao"].Value = pbojFluxoAprov.CodFluxoAprovacao;

            if (pbojFluxoAprov.CodNivelAprovacao > -1)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Id_NivelAprovacao", FWDbType.Int32));
                cmdCommand.Parameters["@Id_NivelAprovacao"].Value = pbojFluxoAprov.CodNivelAprovacao;
            }

            if (pbojFluxoAprov.CodOrdemAprovacao > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Id_OrdemAprovacao", FWDbType.Int32));
                cmdCommand.Parameters["@Id_OrdemAprovacao"].Value = pbojFluxoAprov.CodOrdemAprovacao;
            }

            if (pbojFluxoAprov.CodStatusSolicitacao > -1)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Id_StatusSolicitacao", FWDbType.Int32));
                cmdCommand.Parameters["@Id_StatusSolicitacao"].Value = pbojFluxoAprov.CodStatusSolicitacao;
            }

            if (pbojFluxoAprov.CodTipoSolicitacao > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Id_TipoSolicitacao", FWDbType.Int32));
                cmdCommand.Parameters["@Id_TipoSolicitacao"].Value = pbojFluxoAprov.CodTipoSolicitacao;
            }

            if (pbojFluxoAprov.FlgAprovaAreaSeg != null)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Flg_AprovaAreaSeg", FWDbType.Int16));
                cmdCommand.Parameters["@Flg_AprovaAreaSeg"].Value = (pbojFluxoAprov.FlgAprovaAreaSeg.Value ? 1 : 0);
            }

            if (pbojFluxoAprov.FlgAprovaContingencia != null)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Flg_AprovaContingencia", FWDbType.Int16));
                cmdCommand.Parameters["@Flg_AprovaContingencia"].Value = (pbojFluxoAprov.FlgAprovaContingencia.Value ? 1 : 0);
            }

            if (pbojFluxoAprov.FlgSituacao != null)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Flg_Situacao", FWDbType.Int16));
                cmdCommand.Parameters["@Flg_Situacao"].Value = (pbojFluxoAprov.FlgSituacao.Value ? 1 : 0);
            }

            if (pbojFluxoAprov.FlgAprovaAreaTI != null)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Flg_AprovaAreaTI", FWDbType.Int16));
                cmdCommand.Parameters["@Flg_AprovaAreaTI"].Value = (pbojFluxoAprov.FlgAprovaAreaTI.Value ? 1 : 0);
            }

            if (pbojFluxoAprov.FlgAprovaCracha != null)
            {
                cmdCommand.Parameters.Add(new FWParameter("@FLG_APROVACRACHA", FWDbType.Int16));
                cmdCommand.Parameters["@FLG_APROVACRACHA"].Value = (pbojFluxoAprov.FlgAprovaCracha.Value ? 1 : 0);
            }

            //Executa a função no banco de dados 
            conProvider.ExecuteDataReader(cmdCommand);

            blnRetorno = true;

            return blnRetorno;
        }

        #endregion

        #region Inserir Fluxo Aprovação

        /// <summary>
        ///      Inserir Fluxo Aprovação
        /// </summary>
        /// <history>
        ///      [vsantos] created 06/07/2009
        ///      [haguiar] modify 25/04/2011 15:09
        ///      incluir parametro Flg_AprovaAreaTI
        ///      [haguiar] modify 24/02/2012 14:16
        ///      incluir parametro Flg_AprovaCracha
        /// </history>
        /// <returns></returns>
        public int InserirFluxoAprov(SafWeb.Model.Solicitacao.FluxoAprovacao pbojFluxoAprov)
        {
            int intRetorno;

            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_INS_FLUXO_APROV");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Parâmetros de entrada
            cmdCommand.Parameters.Add(new FWParameter("@Id_NivelAprovacao", FWDbType.Int32));
            cmdCommand.Parameters["@Id_NivelAprovacao"].Value = pbojFluxoAprov.CodNivelAprovacao;

            cmdCommand.Parameters.Add(new FWParameter("@Id_OrdemAprovacao", FWDbType.Int32));
            cmdCommand.Parameters["@Id_OrdemAprovacao"].Value = pbojFluxoAprov.CodOrdemAprovacao;

            cmdCommand.Parameters.Add(new FWParameter("@Id_StatusSolicitacao", FWDbType.Int32));
            cmdCommand.Parameters["@Id_StatusSolicitacao"].Value = pbojFluxoAprov.CodStatusSolicitacao;

            cmdCommand.Parameters.Add(new FWParameter("@Id_TipoSolicitacao", FWDbType.Int32));
            cmdCommand.Parameters["@Id_TipoSolicitacao"].Value = pbojFluxoAprov.CodTipoSolicitacao;

            cmdCommand.Parameters.Add(new FWParameter("@Flg_AprovaAreaSeg", FWDbType.Int16));
            cmdCommand.Parameters["@Flg_AprovaAreaSeg"].Value = (pbojFluxoAprov.FlgAprovaAreaSeg.Value ? 1 : 0);

            cmdCommand.Parameters.Add(new FWParameter("@Flg_AprovaContingencia", FWDbType.Int16));
            cmdCommand.Parameters["@Flg_AprovaContingencia"].Value = (pbojFluxoAprov.FlgAprovaContingencia.Value ? 1 : 0);

            cmdCommand.Parameters.Add(new FWParameter("@Flg_Situacao", FWDbType.Int16));
            cmdCommand.Parameters["@Flg_Situacao"].Value = (pbojFluxoAprov.FlgSituacao.Value ? 1 : 0);

            cmdCommand.Parameters.Add(new FWParameter("@Flg_AprovaAreaTI", FWDbType.Int16));
            cmdCommand.Parameters["@Flg_AprovaAreaTI"].Value = (pbojFluxoAprov.FlgAprovaAreaTI.Value ? 1 : 0);

            cmdCommand.Parameters.Add(new FWParameter("@Flg_AprovaCracha", FWDbType.Int16));
            cmdCommand.Parameters["@Flg_AprovaCracha"].Value = (pbojFluxoAprov.FlgAprovaCracha.Value ? 1 : 0);

            //Executa a função no banco de dados 
            intRetorno = Convert.ToInt32(conProvider.ExecuteScalar(cmdCommand));

            return intRetorno;
        }

        #endregion

        #region Listar Solicitações Cracha Titular

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        ///  Lista de solicitações Cracha Titular
        /// </summary> 
        /// <returns>Collection PermissaoCrachaTitular</returns> 
        /// <history> 
        ///     [haguiar] 25/02/2012 15:11
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public Collection<SafWeb.Model.Solicitacao.PermissaoCrachaTitular> ListarSolicitacoesCrachaTitular
            (            
	            int pintId_UsuarioSolicitante,
	            int pintId_SolicitacaoCrachaTitular,
	            int pintID_REGIONAL,
	            int pintID_FILIAL,	
	            int pintID_STATUSSOLICITACAO,	
                int pintId_TipoSolicitacao,
	            DateTime? Dt_PERIODOINI,
	            DateTime? Dt_PERIODOFIM,
	            string pstrNOM_COLABORADOR,
	            string pstrUSU_C_NOME
            )
        {
            Collection<SafWeb.Model.Solicitacao.PermissaoCrachaTitular> colRetorno = null;

            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_SolicitacaoCrachaTitular");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Parâmetros de entrada
            if (pintId_UsuarioSolicitante > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Id_UsuarioSolicitante", FWDbType.Int32));
                cmdCommand.Parameters["@Id_UsuarioSolicitante"].Value = pintId_UsuarioSolicitante;
            }

            if (pintId_SolicitacaoCrachaTitular > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Id_SolicitacaoCrachaTitular", FWDbType.Int32));
                cmdCommand.Parameters["@Id_SolicitacaoCrachaTitular"].Value = pintId_SolicitacaoCrachaTitular;
            }

            if (pintID_REGIONAL > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Id_Regional", FWDbType.Int32));
                cmdCommand.Parameters["@Id_Regional"].Value = pintID_REGIONAL;
            }

            if (pintID_FILIAL > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Id_Filial", FWDbType.Int32));
                cmdCommand.Parameters["@Id_Filial"].Value = pintID_FILIAL;
            }

            if (pintID_STATUSSOLICITACAO > -1)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Id_StatusSolicitacao", FWDbType.Int32));
                cmdCommand.Parameters["@Id_StatusSolicitacao"].Value = pintID_STATUSSOLICITACAO;
            }

            if (pintId_TipoSolicitacao > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Id_TipoSolicitacao", FWDbType.Int32));
                cmdCommand.Parameters["@Id_TipoSolicitacao"].Value = pintId_TipoSolicitacao;
            }

            if (Dt_PERIODOINI.HasValue)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Dt_PERIODOINI", FWDbType.DateTime));
                cmdCommand.Parameters["@Dt_PERIODOINI"].Value = Dt_PERIODOINI.Value.ToString("yyyy-MM-dd") + " 00:00:00.000";
            }

            if (Dt_PERIODOFIM.HasValue)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Dt_PERIODOFIM", FWDbType.DateTime));
                cmdCommand.Parameters["@Dt_PERIODOFIM"].Value = Dt_PERIODOFIM.Value.ToString("yyyy-MM-dd") + " 23:59:59.998";
            }


            if (pstrNOM_COLABORADOR != String.Empty)
            {
                cmdCommand.Parameters.Add(new FWParameter("@NOM_COLABORADOR", FWDbType.Varchar, 50));
                cmdCommand.Parameters["@NOM_COLABORADOR"].Value = pstrNOM_COLABORADOR;
            }
      
            if (pstrUSU_C_NOME != String.Empty)
            {
                cmdCommand.Parameters.Add(new FWParameter("@USU_C_NOME", FWDbType.Varchar, 50));
                cmdCommand.Parameters["@USU_C_NOME"].Value = pstrUSU_C_NOME;
            }

            //Executa a função no banco de dados 
            System.Data.IDataReader idrRetorno;
            SafWeb.Model.Solicitacao.PermissaoCrachaTitular objRetorno = null;
            colRetorno = new Collection<SafWeb.Model.Solicitacao.PermissaoCrachaTitular>();

            idrRetorno = conProvider.ExecuteDataReader(cmdCommand);

            while (idrRetorno.Read())
            {
                objRetorno = new SafWeb.Model.Solicitacao.PermissaoCrachaTitular();
                objRetorno.FromIDataReader(idrRetorno);
                colRetorno.Add(objRetorno);
            }

            return colRetorno;
        }

        #endregion

        #region Listar Solicitações

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        ///  Lista de solicitações 
        /// </summary> 
        /// <returns>Collection Solicitação</returns> 
        /// <history> 
        ///     [vsantos] 06/07/2009 Created 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public Collection<SafWeb.Model.Solicitacao.Solicitacao> ListarSolicitacoes(int pintCodSolicitacao,
                                                                                   int pintCodEmpresa,
                                                                                   int pintCodRegional,
                                                                                   int pintCodFilial,
                                                                                   int pintCodStatusSolic,
                                                                                   int pintCodTipoSolic,
                                                                                   DateTime? pdatInicioVisita,
                                                                                   DateTime? pdatFimVisita,
                                                                                   string pstrNomVisitado,
                                                                                   string pstrNomVisitante,
                                                                                   string pstrNomSolicitante,
                                                                                   string pstrNomAprovador,
                                                                                   int pintUsuario)
        {
            Collection<SafWeb.Model.Solicitacao.Solicitacao> colRetorno = null;

            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_SOLICITACAO");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

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

            if (pstrNomSolicitante != String.Empty)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Nom_Solicitante", FWDbType.Varchar, 50));
                cmdCommand.Parameters["@Nom_Solicitante"].Value = pstrNomSolicitante;
            }

            if (pstrNomAprovador != String.Empty)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Nom_Aprovador", FWDbType.Varchar, 50));
                cmdCommand.Parameters["@Nom_Aprovador"].Value = pstrNomAprovador;
            }

            if (pintUsuario > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@USU_N_CODIGO", FWDbType.Int32));
                cmdCommand.Parameters["@USU_N_CODIGO"].Value = pintUsuario;
            }

            //Executa a função no banco de dados 
            System.Data.IDataReader idrRetorno;
            SafWeb.Model.Solicitacao.Solicitacao objRetorno = null;
            colRetorno = new Collection<SafWeb.Model.Solicitacao.Solicitacao>();

            idrRetorno = conProvider.ExecuteDataReader(cmdCommand);

            while (idrRetorno.Read())
            {
                objRetorno = new SafWeb.Model.Solicitacao.Solicitacao();
                objRetorno.FromIDataReader(idrRetorno);
                colRetorno.Add(objRetorno);
            }

            return colRetorno;
        }

        #endregion

        #region Listar Status Solicitação

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        ///  Lista status solicitação
        /// </summary> 
        /// <returns>Collection Status</returns> 
        /// <history> 
        ///     [vsantos] 06/07/2009 Created 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public Collection<SafWeb.Model.Solicitacao.Status> ListarStatus()
        {
            Collection<SafWeb.Model.Solicitacao.Status> colRetorno = null;

            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_STATUS");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Executa a função no banco de dados 
            System.Data.IDataReader idrRetorno;
            SafWeb.Model.Solicitacao.Status objRetorno = null;
            colRetorno = new Collection<SafWeb.Model.Solicitacao.Status>();

            idrRetorno = conProvider.ExecuteDataReader(cmdCommand);

            while (idrRetorno.Read())
            {
                objRetorno = new SafWeb.Model.Solicitacao.Status();
                objRetorno.FromIDataReader(idrRetorno);
                colRetorno.Add(objRetorno);
            }

            return colRetorno;
        }

        #endregion

        #region Listar Tipo Solicitação

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        ///  Lista tipo solicitação
        /// </summary> 
        /// <returns>Collection Tiposolicitacao</returns> 
        /// <history> 
        ///     [vsantos] 06/07/2009 Created 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public Collection<SafWeb.Model.Solicitacao.TipoSolicitacao> ListarTipoSolicitacao()
        {
            Collection<SafWeb.Model.Solicitacao.TipoSolicitacao> colRetorno = null;

            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_TIPO_SOLICITACAO");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Executa a função no banco de dados 
            System.Data.IDataReader idrRetorno;
            SafWeb.Model.Solicitacao.TipoSolicitacao objRetorno = null;
            colRetorno = new Collection<SafWeb.Model.Solicitacao.TipoSolicitacao>();

            idrRetorno = conProvider.ExecuteDataReader(cmdCommand);

            while (idrRetorno.Read())
            {
                objRetorno = new SafWeb.Model.Solicitacao.TipoSolicitacao();
                objRetorno.FromIDataReader(idrRetorno);
                colRetorno.Add(objRetorno);
            }

            return colRetorno;
        }

        #endregion

        #region Listar Tipo Solicitação Escala Todos

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        ///  Lista tipo solicitação Todos
        /// </summary> 
        /// <returns>Collection Tipo solicitacao</returns> 
        /// <history> 
        ///     [cmarchi] 17/2/2010 Created 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public Collection<SafWeb.Model.Solicitacao.TipoSolicitacao> ListarTipoSolicitacaoTodos()
        {
            Collection<SafWeb.Model.Solicitacao.TipoSolicitacao> colRetorno = null;

            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_TIPO_SOLICITACAO");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Parâmetros de entrada
            cmdCommand.Parameters.Add(new FWParameter("@Escala", FWDbType.Byte));
            cmdCommand.Parameters["@Escala"].Value = 0;

            //Executa a função no banco de dados 
            System.Data.IDataReader idrRetorno;
            SafWeb.Model.Solicitacao.TipoSolicitacao objRetorno = null;
            colRetorno = new Collection<SafWeb.Model.Solicitacao.TipoSolicitacao>();

            idrRetorno = conProvider.ExecuteDataReader(cmdCommand);

            while (idrRetorno.Read())
            {
                objRetorno = new SafWeb.Model.Solicitacao.TipoSolicitacao();
                objRetorno.FromIDataReader(idrRetorno);
                colRetorno.Add(objRetorno);
            }

            return colRetorno;
        }

        #endregion

        #region Listar Tipo Solicitação

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        ///  Lista tipo Escalas
        /// </summary> 
        /// <returns>Collection Tiposolicitacao</returns>
        /// <param name="pblnEscala">True - Lista Escalas / False - Lista Solicitações</param>
        /// <history> 
        ///     [tgerevini] 31/05/2010 Created 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public Collection<SafWeb.Model.Solicitacao.TipoSolicitacao> ListarTipoSolicitacao(Boolean pblnEscala)
        {
            Collection<SafWeb.Model.Solicitacao.TipoSolicitacao> colRetorno = null;

            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_TIPO_SOLICITACAO");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;


            cmdCommand.Parameters.Add(new FWParameter("@Escala", FWDbType.Byte));
            cmdCommand.Parameters["@Escala"].Value = pblnEscala;

            //Executa a função no banco de dados 
            System.Data.IDataReader idrRetorno;
            SafWeb.Model.Solicitacao.TipoSolicitacao objRetorno = null;
            colRetorno = new Collection<SafWeb.Model.Solicitacao.TipoSolicitacao>();

            idrRetorno = conProvider.ExecuteDataReader(cmdCommand);

            while (idrRetorno.Read())
            {
                objRetorno = new SafWeb.Model.Solicitacao.TipoSolicitacao();
                objRetorno.FromIDataReader(idrRetorno);
                colRetorno.Add(objRetorno);
            }

            return colRetorno;
        }

        #endregion

        #region Listar Tipo Solicitação Grupo

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        ///  Lista tipo solicitação Filtrado por grupo
        /// </summary> 
        /// <returns>Collection Tiposolicitacao</returns> 
        /// <history> 
        ///     [cfrancisco] 05/04/2012 Created 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public Collection<SafWeb.Model.Solicitacao.TipoSolicitacao> ListarTipoSolicitacaoGrupo(int Id_TipoSolcitacaoGrp)
        {
            Collection<SafWeb.Model.Solicitacao.TipoSolicitacao> colRetorno = null;

            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_TIPO_SOLICITACAO_GRUPO");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Parâmetros de entrada
            cmdCommand.Parameters.Add(new FWParameter("@Id_TipoSolicitacaoGrp", FWDbType.Int32));
            cmdCommand.Parameters["@Id_TipoSolicitacaoGrp"].Value = Id_TipoSolcitacaoGrp;

            //Executa a função no banco de dados 
            System.Data.IDataReader idrRetorno;
            SafWeb.Model.Solicitacao.TipoSolicitacao objRetorno = null;
            colRetorno = new Collection<SafWeb.Model.Solicitacao.TipoSolicitacao>();

            idrRetorno = conProvider.ExecuteDataReader(cmdCommand);

            while (idrRetorno.Read())
            {
                objRetorno = new SafWeb.Model.Solicitacao.TipoSolicitacao();
                objRetorno.FromIDataReader(idrRetorno);
                colRetorno.Add(objRetorno);
            }

            return colRetorno;
        }

        #endregion

        #region Lista Fluxo Aprovação

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        ///  Lista fluxo aprovação
        /// </summary> 
        /// <returns>Collection FluxoAprovacao</returns> 
        /// <history> 
        ///     [vsantos] 07/07/2009 Created 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public Collection<SafWeb.Model.Solicitacao.FluxoAprovacao> ListarFluxoAprovacao(int pintCodTipoSolic)
        {
            Collection<SafWeb.Model.Solicitacao.FluxoAprovacao> colRetorno = null;

            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_FLUXO_APROV");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Parâmetros de entrada
            cmdCommand.Parameters.Add(new FWParameter("@Id_TipoSolicitacao", FWDbType.Int32));
            cmdCommand.Parameters["@Id_TipoSolicitacao"].Value = pintCodTipoSolic;

            //Executa a função no banco de dados 
            System.Data.IDataReader idrRetorno;
            SafWeb.Model.Solicitacao.FluxoAprovacao objRetorno = null;
            colRetorno = new Collection<SafWeb.Model.Solicitacao.FluxoAprovacao>();

            idrRetorno = conProvider.ExecuteDataReader(cmdCommand);

            while (idrRetorno.Read())
            {
                objRetorno = new SafWeb.Model.Solicitacao.FluxoAprovacao();
                objRetorno.FromIDataReader(idrRetorno);
                colRetorno.Add(objRetorno);
            }

            return colRetorno;
        }

        #endregion

        #region Lista Nível Aprovação

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        ///  Lista nível aprovação
        /// </summary> 
        /// <returns>Collection NivelAprovacao</returns> 
        /// <history> 
        ///     [vsantos] 10/07/2009 Created 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public Collection<SafWeb.Model.Solicitacao.NivelAprovacao> ListarNivelAprovacao()
        {
            Collection<SafWeb.Model.Solicitacao.NivelAprovacao> colRetorno = null;

            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_NIVEL_APROV");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Executa a função no banco de dados 
            System.Data.IDataReader idrRetorno;
            SafWeb.Model.Solicitacao.NivelAprovacao objRetorno = null;
            colRetorno = new Collection<SafWeb.Model.Solicitacao.NivelAprovacao>();

            idrRetorno = conProvider.ExecuteDataReader(cmdCommand);

            while (idrRetorno.Read())
            {
                objRetorno = new SafWeb.Model.Solicitacao.NivelAprovacao();
                objRetorno.FromIDataReader(idrRetorno);
                colRetorno.Add(objRetorno);
            }

            return colRetorno;
        }

        #endregion

        #region Listar Motivo Visita

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        ///     Lista o motivo da visita
        /// </summary> 
        /// <returns>Collection MotivoVisita</returns> 
        /// <history> 
        ///     [mribeiro] 07/07/2009 Created 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public Collection<SafWeb.Model.Solicitacao.MotivoSolicitacao> ListarMotivoVisita()
        {
            Collection<SafWeb.Model.Solicitacao.MotivoSolicitacao> colRetorno = null;

            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_MOTIVO_VISITA");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Executa a função no banco de dados 
            System.Data.IDataReader idrRetorno;
            SafWeb.Model.Solicitacao.MotivoSolicitacao objRetorno = null;
            colRetorno = new Collection<SafWeb.Model.Solicitacao.MotivoSolicitacao>();

            idrRetorno = conProvider.ExecuteDataReader(cmdCommand);

            while (idrRetorno.Read())
            {
                objRetorno = new SafWeb.Model.Solicitacao.MotivoSolicitacao();
                objRetorno.FromIDataReader(idrRetorno);
                colRetorno.Add(objRetorno);
            }

            return colRetorno;
        }

        #endregion

        #region Listar Colaboradores Lista

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        ///     Lista colaboradores da lista de uma solicitação 
        /// </summary> 
        /// <returns>DataTable</returns> 
        /// <param name="pintLista">Código da Lista</param>
        /// <param name="pintSolicitacao">Código da Solicitação</param>
        /// <history> 
        ///     [mribeiro] 06/07/2009 Created 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public DataTable ListarColaboradoresLista(int pintSolicitacao,
                                                  int pintLista)
        {
            DataTable dttRetorno = new DataTable();

            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_SOLICITACAO_LISTA");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Parâmetros de entrada
            cmdCommand.Parameters.Add(new FWParameter("@Id_Solicitacao", FWDbType.Int32));
            cmdCommand.Parameters["@Id_Solicitacao"].Value = pintSolicitacao;

            cmdCommand.Parameters.Add(new FWParameter("@Id_ColaboradorLista", FWDbType.Int32));
            cmdCommand.Parameters["@Id_ColaboradorLista"].Value = pintLista;

            //Executa a função no banco de dados 
            dttRetorno = conProvider.ExecuteDataTable(cmdCommand);

            return dttRetorno;
        }

        #endregion

        #region Listar Veiculos Lista

        public DataTable ListarVeiculosLista(int pintSolicitacao)
        {
            DataTable dttRetorno = new DataTable();

            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_SOLICITACAOLISTAVEICULOS");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Parâmetros de entrada
            cmdCommand.Parameters.Add(new FWParameter("@Id_Solicitacao", FWDbType.Int32));
            cmdCommand.Parameters["@Id_Solicitacao"].Value = pintSolicitacao;

            //Executa a função no banco de dados 
            dttRetorno = conProvider.ExecuteDataTable(cmdCommand);

            return dttRetorno;
        }

        #endregion


        #region Aprovar Solicitação Cracha Titular

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        ///  Aprovar solicitação Cracha TItular
        /// </summary> 
        /// <returns></returns> 
        /// <history> 
        ///     [haguiar] 28/02/2012 08:50
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public bool AprovarSolicitacaoCrachaTitular(int pintId_SolicitacaoCrachaTitular, int pintId_UsuarioAprovador)
        {
            bool blnRetorno = false;

            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_UPD_APROV_SOLICITACAO_CRACHATITULAR");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Parâmetros de entrada
            if (pintId_SolicitacaoCrachaTitular > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Id_SolicitacaoCrachaTitular", FWDbType.Int32));
                cmdCommand.Parameters["@Id_SolicitacaoCrachaTitular"].Value = pintId_SolicitacaoCrachaTitular;
            }

            if (pintId_UsuarioAprovador > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Id_UsuarioAprovador", FWDbType.Int32));
                cmdCommand.Parameters["@Id_UsuarioAprovador"].Value = pintId_UsuarioAprovador;
            }

            //Executa a função no banco de dados 
            conProvider.ExecuteNonQuery(cmdCommand);

            blnRetorno = true;

            return blnRetorno;
        }

        #endregion

        #region Aprovar Solicitação

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        ///  Aprovar solicitação
        /// </summary> 
        /// <returns></returns> 
        /// <history> 
        ///     [vsantos] 06/07/2009 Created 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public bool AprovarSolicitacao(int pintCodSolic, decimal pdecCodUsuarioAprov)
        {
            bool blnRetorno = false;

            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_UPD_APROV_SOLICITACAO");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Parâmetros de entrada
            if (pintCodSolic > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Id_Solicitacao", FWDbType.Int32));
                cmdCommand.Parameters["@Id_Solicitacao"].Value = pintCodSolic;
            }

            if (pdecCodUsuarioAprov > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Id_UsuarioAprovador", FWDbType.Int32));
                cmdCommand.Parameters["@Id_UsuarioAprovador"].Value = pdecCodUsuarioAprov;
            }

            //Executa a função no banco de dados 
            conProvider.ExecuteNonQuery(cmdCommand);

            blnRetorno = true;

            return blnRetorno;
        }

        #endregion

        #region Reprovar Solicitação Cracha Titular

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        ///  Reprovar solicitação Cracha Titular
        /// </summary> 
        /// <returns></returns> 
        /// <history> 
        ///     [haguiar] 28/02/2012 08:52
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public bool ReprovarSolicitacaoCrachaTitular(int pintId_SolicitacaoCrachaTitular, decimal pintId_UsuarioAprovador, string pstrMotivoReprov)
        {
            bool blnRetorno = false;

            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_UPD_REPROV_SOLICITACAO_CRACHATITULAR");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Parâmetros de entrada
            if (pintId_SolicitacaoCrachaTitular > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Id_SolicitacaoCrachaTitular", FWDbType.Int32));
                cmdCommand.Parameters["@Id_SolicitacaoCrachaTitular"].Value = pintId_SolicitacaoCrachaTitular;
            }

            if (pintId_UsuarioAprovador > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Id_UsuarioAprovador", FWDbType.Int32));
                cmdCommand.Parameters["@Id_UsuarioAprovador"].Value = pintId_UsuarioAprovador;
            }

            if (pstrMotivoReprov != String.Empty)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Des_MotivoReprovacao", FWDbType.Varchar, 150));
                cmdCommand.Parameters["@Des_MotivoReprovacao"].Value = pstrMotivoReprov;
            }

            //Executa a função no banco de dados 
            conProvider.ExecuteNonQuery(cmdCommand);

            blnRetorno = true;

            return blnRetorno;
        }

        #endregion

        #region Reprovar Solicitação

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        ///  Reprovar solicitação
        /// </summary> 
        /// <returns></returns> 
        /// <history> 
        ///     [vsantos] 06/07/2009 Created 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public bool ReprovarSolicitacao(int pintCodSolic, decimal pdecCodUsuarioAprov, string pstrMotivoReprov)
        {
            bool blnRetorno = false;

            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_UPD_REPROV_SOLICITACAO");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Parâmetros de entrada
            if (pintCodSolic > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Id_Solicitacao", FWDbType.Int32));
                cmdCommand.Parameters["@Id_Solicitacao"].Value = pintCodSolic;
            }

            if (pdecCodUsuarioAprov > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Id_UsuarioAprovador", FWDbType.Int32));
                cmdCommand.Parameters["@Id_UsuarioAprovador"].Value = pdecCodUsuarioAprov;
            }

            if (pstrMotivoReprov != String.Empty)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Des_MotivoReprovacao", FWDbType.Varchar, 150));
                cmdCommand.Parameters["@Des_MotivoReprovacao"].Value = pstrMotivoReprov;
            }

            //Executa a função no banco de dados 
            conProvider.ExecuteNonQuery(cmdCommand);

            blnRetorno = true;

            return blnRetorno;
        }

        #endregion

        #region Listar Histórico Solicitação Cracha Titular

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        ///     Listar Histórico Solicitação Cracha Titular
        /// </summary> 
        /// <param name="pintId_SolicitacaoCrachaTitular">Código da Solicitação Cracha Titular</param>
        /// <returns>DataSet</returns> 
        /// <history> 
        ///     [haguiar] 27/02/2012 Created 19:33
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public DataSet ListarHistoricoCrachaTitular(int pintId_SolicitacaoCrachaTitular)
        {
            DataSet dtsRetorno = null;

            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_HISTORICO_SOLICITACAOCRACHATITULAR");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Parâmetros de entrada
            cmdCommand.Parameters.Add(new FWParameter("@Id_SolicitacaoCrachaTitular", FWDbType.Int32));
            cmdCommand.Parameters["@Id_SolicitacaoCrachaTitular"].Value = pintId_SolicitacaoCrachaTitular;

            //Executa a função no banco de dados 
            dtsRetorno = conProvider.ExecuteDataSet(cmdCommand);

            return dtsRetorno;
        }

        #endregion

        #region Listar Histórico Solicitação

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        ///     Listar Histórico Solicitação
        /// </summary> 
        /// <param name="pintCodSolicitacao">Código da Solicitação</param>
        /// <param name="pintCodVisitante">Código do Visitante</param>
        /// <returns>DataSet</returns> 
        /// <history> 
        ///     [vsantos] 07/07/2009 Created 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public DataSet ListarHistoricoSolic(int pintCodSolicitacao,
                                            int pintCodVisitante)
        {
            DataSet dtsRetorno = null;

            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_HISTORICO_SOLIC");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Parâmetros de entrada
            cmdCommand.Parameters.Add(new FWParameter("@Id_Solicitacao", FWDbType.Int32));
            cmdCommand.Parameters["@Id_Solicitacao"].Value = pintCodSolicitacao;

            if (pintCodVisitante > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Id_Visitante", FWDbType.Int32));
                cmdCommand.Parameters["@Id_Visitante"].Value = pintCodVisitante;
            }

            //Executa a função no banco de dados 
            dtsRetorno = conProvider.ExecuteDataSet(cmdCommand);

            return dtsRetorno;
        }

        #endregion

        #region Inserir Solicitação Cracha Titular

        /// <summary>
        ///     Insere os registros na base
        /// </summary>
        /// <param name="pobjSolicitacao">Objeto Solicitacao Cracha Titular</param>
        /// <returns>Código da Solicitacao Cadastrado</returns>
        /// <history>
        ///     [haguiar] created 25/02/2012 15:41
        /// </history>
        public int InserirPermissaoCrachaTitular(SafWeb.Model.Solicitacao.PermissaoCrachaTitular pobjSolicitacao)
        {
            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_INS_SolicitacaoCrachaTitular");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            cmdCommand.Parameters.Add(new FWParameter("@Id_UsuarioSolicitante", FWDbType.Int32));
            cmdCommand.Parameters["@Id_UsuarioSolicitante"].Value = pobjSolicitacao.Id_UsuarioSolicitante;

            cmdCommand.Parameters.Add(new FWParameter("@Id_Colaborador", FWDbType.Int32));
            cmdCommand.Parameters["@Id_Colaborador"].Value = pobjSolicitacao.Id_Colaborador;

            cmdCommand.Parameters.Add(new FWParameter("@Dt_Solicitacao", FWDbType.DateTime));
            cmdCommand.Parameters["@Dt_Solicitacao"].Value = pobjSolicitacao.Data_Solicitacao;

            cmdCommand.Parameters.Add(new FWParameter("@Id_Area", FWDbType.Varchar, 60));
            cmdCommand.Parameters["@Id_Area"].Value = pobjSolicitacao.Id_Area;

            cmdCommand.Parameters.Add(new FWParameter("@Id_TipoSolicitacao", FWDbType.Int32));
            cmdCommand.Parameters["@Id_TipoSolicitacao"].Value = pobjSolicitacao.Id_TipoSolicitacao;

            if (!pobjSolicitacao.Des_MotivoSolicitacao.Equals(string.Empty))
            {
                cmdCommand.Parameters.Add(new FWParameter("@Des_MotivoSolicitacao", FWDbType.Varchar, 200));
                cmdCommand.Parameters["@Des_MotivoSolicitacao"].Value = pobjSolicitacao.Des_MotivoSolicitacao;
            }

            //Executa a função no banco de dados 
            int intIdRetorno;

            try
            {
                intIdRetorno = Convert.ToInt32(conProvider.ExecuteScalar(cmdCommand));
            }
            catch (Exception ex)
            {
                throw;
            }

            return intIdRetorno;
        }

        #endregion

        #region Inserir Solicitação

        /// <summary>
        ///     Insere os registros na base
        /// </summary>
        /// <param name="pobjSolicitacao">Objeto Solicitacao</param>
        /// <returns>Código da Solicitacao Cadastrado</returns>
        /// <history>
        ///     [mribeiro] created 13/07/2009
        /// </history>
        public int InserirSolicitacao(SafWeb.Model.Solicitacao.Solicitacao pobjSolicitacao)
        {
            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_INS_SOLICITACAO");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            cmdCommand.Parameters.Add(new FWParameter("@Id_UsuarioSolicitante", FWDbType.Int32));
            cmdCommand.Parameters["@Id_UsuarioSolicitante"].Value = pobjSolicitacao.CodUsuSolic;

            cmdCommand.Parameters.Add(new FWParameter("@Id_Visitado", FWDbType.Int32));
            cmdCommand.Parameters["@Id_Visitado"].Value = pobjSolicitacao.CodVisitado;

            if (pobjSolicitacao.CodVisitante > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Id_Visitante", FWDbType.Int32));
                cmdCommand.Parameters["@Id_Visitante"].Value = pobjSolicitacao.CodVisitante;
            }

            if (pobjSolicitacao.CodVeiculo > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Id_Veiculo", FWDbType.Int32));
                cmdCommand.Parameters["@Id_Veiculo"].Value = pobjSolicitacao.CodVeiculo;
            }

            if (pobjSolicitacao.CodVeiculoLista > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Id_VeiculoLista", FWDbType.Int32));
                cmdCommand.Parameters["@Id_VeiculoLista"].Value = pobjSolicitacao.CodVeiculoLista;
            }

            cmdCommand.Parameters.Add(new FWParameter("@Dt_InicioVisita", FWDbType.DateTime));
            cmdCommand.Parameters["@Dt_InicioVisita"].Value = pobjSolicitacao.InicioVisita;

            cmdCommand.Parameters.Add(new FWParameter("@Dt_FimVisita", FWDbType.DateTime));
            cmdCommand.Parameters["@Dt_FimVisita"].Value = pobjSolicitacao.FimVisita;

            cmdCommand.Parameters.Add(new FWParameter("@Flg_AcSabado", FWDbType.Byte));
            cmdCommand.Parameters["@Flg_AcSabado"].Value = pobjSolicitacao.AcSabado;

            cmdCommand.Parameters.Add(new FWParameter("@Flg_AcDomingo", FWDbType.Byte));
            cmdCommand.Parameters["@Flg_AcDomingo"].Value = pobjSolicitacao.AcDomingo;

            cmdCommand.Parameters.Add(new FWParameter("@Flg_AcFeriado", FWDbType.Byte));
            cmdCommand.Parameters["@Flg_AcFeriado"].Value = pobjSolicitacao.AcFeriado;

            cmdCommand.Parameters.Add(new FWParameter("@Id_Area", FWDbType.Varchar, 60));
            cmdCommand.Parameters["@Id_Area"].Value = pobjSolicitacao.CodArea;

            cmdCommand.Parameters.Add(new FWParameter("@Id_TipoSolicitacao", FWDbType.Int32));
            cmdCommand.Parameters["@Id_TipoSolicitacao"].Value = pobjSolicitacao.CodTipoSolicitacao;

            cmdCommand.Parameters.Add(new FWParameter("@Id_MotivoVisita", FWDbType.Int32));
            cmdCommand.Parameters["@Id_MotivoVisita"].Value = pobjSolicitacao.CodMotivoVisita;

            cmdCommand.Parameters.Add(new FWParameter("@Des_ObsSolicitacao", FWDbType.Varchar, 200));
            cmdCommand.Parameters["@Des_ObsSolicitacao"].Value = pobjSolicitacao.Observacao;

            //Executa a função no banco de dados 
            int intIdRetorno;

            try
            {
                intIdRetorno = Convert.ToInt32(conProvider.ExecuteScalar(cmdCommand));
            }
            catch (Exception ex)
            {
                throw;
            }

            return intIdRetorno;
        }

        #endregion

        #region Inserir Solicitação Lista

        /// <summary>
        ///     Insere os registros na base
        /// </summary>
        /// <param name="pintCodColaborador">Codigo do Colaborador da lista</param>
        /// <param name="pintCodColaboradorLista">Código da Lista</param>
        /// <param name="pintCodSolicitacao">Código da Solicitação</param>
        /// <history>
        ///     [mribeiro] created 13/07/2009
        /// </history>
        public bool InserirSolicitacaoLista(int pintCodSolicitacao,
                                            int pintCodColaboradorLista,
                                            int pintCodColaborador)
        {
            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_INS_SOLICITACAO_LISTA");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            cmdCommand.Parameters.Add(new FWParameter("@Id_Solicitacao", FWDbType.Int32));
            cmdCommand.Parameters["@Id_Solicitacao"].Value = pintCodSolicitacao;

            cmdCommand.Parameters.Add(new FWParameter("@Id_ColaboradorLista", FWDbType.Int32));
            cmdCommand.Parameters["@Id_ColaboradorLista"].Value = pintCodColaboradorLista;

            cmdCommand.Parameters.Add(new FWParameter("@Id_Colaborador", FWDbType.Int32));
            cmdCommand.Parameters["@Id_Colaborador"].Value = pintCodColaborador;

            //Executa a função no banco de dados 
            bool blnRetorno = false;

            try
            {
                conProvider.ExecuteScalar(cmdCommand);
                blnRetorno = true;
            }
            catch (Exception ex)
            {
                throw;
            }

            return blnRetorno;
        }

        #endregion

        #region Inserir SolicitaçãoLista

        /// <summary>
        ///      Insere uma solicitação que possui uma lista de colaboradors
        /// </summary>
        /// <param name="pintCodColaboradorLista">Codigo da lista</param>
        /// <param name="pintCodSolicitacao">Codigo da Solicitação</param>
        /// <history>
        ///      [mribeiro] created 13/07/2009
        /// </history>
        public bool InserirSolicitacaoListaVeiculos(int pintCodSolicitacao,
                                            int pintCodVeiculoLista)
        {
            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_INS_SOLICITACAOLISTAVEICULOS");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            cmdCommand.Parameters.Add(new FWParameter("@Id_Solicitacao", FWDbType.Int32));
            cmdCommand.Parameters["@Id_Solicitacao"].Value = pintCodSolicitacao;

            cmdCommand.Parameters.Add(new FWParameter("@Id_VeiculoLista", FWDbType.Int32));
            cmdCommand.Parameters["@Id_VeiculoLista"].Value = pintCodVeiculoLista;

            //Executa a função no banco de dados 
            bool boolRetorno = false;

            try
            {

                using (System.Data.IDataReader idrRetorno = conProvider.ExecuteDataReader(cmdCommand))
                {
                    if (idrRetorno.Read())
                    {
                        if (idrRetorno["status"].ToString().Equals("1"))
                            boolRetorno = true;
                        else
                            throw new Exception(idrRetorno["descricao"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return boolRetorno;
        }

        #endregion

        #region Inserir Status Permissao Cracha Titular

        /// <summary>
        ///     Insere os registros na base
        /// </summary>
        /// <param name="pintId_SolicitacaoCrachaTitular">Codigo da Solicitação Cracha Titular</param>
        /// <param name="pintID_STATUSSOLICITACAO">Código do Status da Solicitação</param>
        /// <param name="pintID_USUARIOAPROVADOR">Código do Usuário</param>
        /// <history>
        ///     [haguiar] created 25/02/2012 15:47
        /// </history>
        public bool InserirStatusPermissaoCrachaTitular(int pintId_SolicitacaoCrachaTitular,
                                                         int pintID_STATUSSOLICITACAO,
                                                         int pintID_USUARIOAPROVADOR,
                                                         DateTime? datAprovacao)
        {
            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_INS_AprovCrachaTitular");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            cmdCommand.Parameters.Add(new FWParameter("@Id_SolicitacaoCrachaTitular", FWDbType.Int32));
            cmdCommand.Parameters["@Id_SolicitacaoCrachaTitular"].Value = pintId_SolicitacaoCrachaTitular;

            cmdCommand.Parameters.Add(new FWParameter("@ID_STATUSSOLICITACAO", FWDbType.Int32));
            cmdCommand.Parameters["@ID_STATUSSOLICITACAO"].Value = pintID_STATUSSOLICITACAO;

            if (datAprovacao.HasValue)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Dt_Aprovacao", FWDbType.DateTime));
                cmdCommand.Parameters["@Dt_Aprovacao"].Value = datAprovacao;
            }

            cmdCommand.Parameters.Add(new FWParameter("@Id_UsuarioAprovador", FWDbType.Int32));
            cmdCommand.Parameters["@Id_UsuarioAprovador"].Value = pintID_USUARIOAPROVADOR;

            //Executa a função no banco de dados 
            bool blnRetorno = false;

            try
            {
                conProvider.ExecuteScalar(cmdCommand);
                blnRetorno = true;
            }
            catch (Exception ex)
            {
                throw;
            }

            return blnRetorno;
        }

        #endregion

        #region Inserir Status Solicitacao

        /// <summary>
        ///     Insere os registros na base
        /// </summary>
        /// <param name="pintCodSolicitacao">Codigo da Solicitação</param>
        /// <param name="pintCodStatus">Código do Status da Solicitação</param>
        /// <param name="pintCodUsuAprov">Código do Usuário</param>
        /// <history>
        ///     [mribeiro] created 13/07/2009
        /// </history>
        public bool InserirStatusSolicitacao(int pintCodSolicitacao,
                                             int pintCodStatus,
                                             int pintCodUsuAprov,
                                             DateTime? datAprovacao)
        {
            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_INS_APROV_SOLICITACAO");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            cmdCommand.Parameters.Add(new FWParameter("@Id_Solicitacao", FWDbType.Int32));
            cmdCommand.Parameters["@Id_Solicitacao"].Value = pintCodSolicitacao;

            cmdCommand.Parameters.Add(new FWParameter("@Id_StatusSolicitacao", FWDbType.Int32));
            cmdCommand.Parameters["@Id_StatusSolicitacao"].Value = pintCodStatus;

            if (datAprovacao.HasValue)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Dt_Aprovacao", FWDbType.DateTime));
                cmdCommand.Parameters["@Dt_Aprovacao"].Value = datAprovacao;
            }

            cmdCommand.Parameters.Add(new FWParameter("@Id_UsuarioAprovador", FWDbType.Int32));
            cmdCommand.Parameters["@Id_UsuarioAprovador"].Value = pintCodUsuAprov;

            //Executa a função no banco de dados 
            bool blnRetorno = false;

            try
            {
                conProvider.ExecuteScalar(cmdCommand);
                blnRetorno = true;
            }
            catch (Exception ex)
            {
                throw;
            }

            return blnRetorno;
        }

        #endregion

        #region Obter Primeiro Status Solicitacao

        /// <summary>
        ///     Obtem os registros na base
        /// </summary>
        /// <param name="pintTipoSolicitacao">Tipo da Solicitacao</param>
        /// <history>
        ///     [mribeiro] created 13/07/2009
        /// </history>
        /// <returns>Status da solicitação</returns>
        public int ObterPrimeiroStatus(int pintTipoSolicitacao)
        {
            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_PRIMEIRO_STATUS");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            cmdCommand.Parameters.Add(new FWParameter("@Id_TipoSolicitacao", FWDbType.Int32));
            cmdCommand.Parameters["@Id_TipoSolicitacao"].Value = pintTipoSolicitacao;

            //Executa a função no banco de dados 
            int intRetorno;

            try
            {
                intRetorno = Convert.ToInt32(conProvider.ExecuteScalar(cmdCommand));
            }
            catch (Exception ex)
            {
                throw;
            }

            return intRetorno;
        }

        #endregion

        #region Obter Último Status Solicitacao Cracha Titular

        /// <summary>
        ///     Obtem os registros na base
        /// </summary>
        /// <param name="pintId_SolicitacaoCrachaTitular">Id da Solicitacao</param>
        /// <history>
        ///     [haguiar] created 26/02/2012 08:13
        /// </history>
        /// <returns>Status da solicitação</returns>
        public string ObterUltimoStatusCrachaTitular(int pintId_SolicitacaoCrachaTitular)
        {
            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_ULTIMO_STATUS_CRACHATITULAR");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            cmdCommand.Parameters.Add(new FWParameter("@Id_SolicitacaoCrachaTitular", FWDbType.Int32));
            cmdCommand.Parameters["@Id_SolicitacaoCrachaTitular"].Value = pintId_SolicitacaoCrachaTitular;

            //Executa a função no banco de dados 
            string strRetorno;

            try
            {
                strRetorno = Convert.ToString(conProvider.ExecuteScalar(cmdCommand));
            }
            catch (Exception ex)
            {
                throw;
            }

            return strRetorno;
        }

        #endregion

        #region Obter Último Status Solicitacao

        /// <summary>
        ///     Obtem os registros na base
        /// </summary>
        /// <param name="pintSolicitacao">Id da Solicitacao</param>
        /// <history>
        ///     [mribeiro] created 22/10/2009
        /// </history>
        /// <returns>Status da solicitação</returns>
        public string ObterUltimoStatus(int pintSolicitacao)
        {
            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_ULTIMO_STATUS");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            cmdCommand.Parameters.Add(new FWParameter("@Id_Solicitacao", FWDbType.Int32));
            cmdCommand.Parameters["@Id_Solicitacao"].Value = pintSolicitacao;

            //Executa a função no banco de dados 
            string strRetorno;

            try
            {
                strRetorno = Convert.ToString(conProvider.ExecuteScalar(cmdCommand));
            }
            catch (Exception ex)
            {
                throw;
            }

            return strRetorno;
        }

        #endregion

        #region Obter Codigo Aprovador

        /// <summary>
        ///     Obtem os registros na base
        /// </summary>
        /// <param name="pintIdAprovador">Id do aprovador</param>
        /// <history>
        ///     [mribeiro] created 13/07/2009
        /// </history>
        /// <returns>Codigo (RE) do aprovador</returns>
        public DataTable ObterCodigoAprovador(int pintIdAprovador)
        {
            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_CODIGO_APROVADOR");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            cmdCommand.Parameters.Add(new FWParameter("@USU_N_CODIGO", FWDbType.Int32));
            cmdCommand.Parameters["@USU_N_CODIGO"].Value = pintIdAprovador;

            //Executa a função no banco de dados 
            DataTable dttRetorno;

            try
            {
                dttRetorno = conProvider.ExecuteDataTable(cmdCommand);
            }
            catch (Exception ex)
            {
                throw;
            }

            return dttRetorno;
        }

        #endregion

        #region Listar Aprovadores

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        ///  Lista os aprovadores 
        /// </summary> 
        /// <param name="pstrFilial">String com as regionais</param>
        /// <param name="pstrRegional">String com as filiais</param>
        /// <param name="pstrTipoSolicitacao">String com os tipos de solicitações</param>
        /// <returns>Collection Aprovador</returns> 
        /// <history> 
        ///     [mribeiro] 15/07/2009 Created 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public Collection<SafWeb.Model.Solicitacao.Aprovador> ListarAprovadores(string pstrRegional,
                                                                                string pstrFilial,
                                                                                string pstrTipoSolicitacao)
        {
            Collection<SafWeb.Model.Solicitacao.Aprovador> colRetorno = null;

            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_APROVADOR");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Parâmetros de entrada
            cmdCommand.Parameters.Add(new FWParameter("@Id_Regional", FWDbType.Varchar));
            cmdCommand.Parameters["@Id_Regional"].Value = pstrRegional;

            cmdCommand.Parameters.Add(new FWParameter("@Id_Filial", FWDbType.Varchar));
            cmdCommand.Parameters["@Id_Filial"].Value = pstrFilial;

            cmdCommand.Parameters.Add(new FWParameter("@Id_TipoSolicitacao", FWDbType.Varchar));
            cmdCommand.Parameters["@Id_TipoSolicitacao"].Value = pstrTipoSolicitacao;

            //Executa a função no banco de dados 
            System.Data.IDataReader idrRetorno;
            SafWeb.Model.Solicitacao.Aprovador objRetorno = null;
            colRetorno = new Collection<SafWeb.Model.Solicitacao.Aprovador>();

            idrRetorno = conProvider.ExecuteDataReader(cmdCommand);

            while (idrRetorno.Read())
            {
                objRetorno = new SafWeb.Model.Solicitacao.Aprovador();
                objRetorno.FromIDataReader(idrRetorno);
                colRetorno.Add(objRetorno);
            }

            return colRetorno;
        }

        #endregion

        #region Listar Todos Aprovadores

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        ///  Lista registros da base
        /// </summary> 
        /// <returns>Collection Aprovador</returns> 
        /// <history> 
        ///     [mribeiro] 25/10/2009 Created 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public Collection<SafWeb.Model.Solicitacao.Aprovador> ListarTodosAprovadores()
        {
            Collection<SafWeb.Model.Solicitacao.Aprovador> colRetorno = null;

            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_TODOS_APROVADORES");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Executa a função no banco de dados 
            System.Data.IDataReader idrRetorno;
            SafWeb.Model.Solicitacao.Aprovador objRetorno = null;
            colRetorno = new Collection<SafWeb.Model.Solicitacao.Aprovador>();

            idrRetorno = conProvider.ExecuteDataReader(cmdCommand);

            while (idrRetorno.Read())
            {
                objRetorno = new SafWeb.Model.Solicitacao.Aprovador();
                objRetorno.FromIDataReader(idrRetorno);
                colRetorno.Add(objRetorno);
            }

            return colRetorno;
        }

        #endregion

        #region Listar Aprovadores Contingencia

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        ///  Lista os aprovadores de contingencia
        /// </summary> 
        /// <param name="pintRegional">int com a regionais</param>
        /// <param name="pintFilial">int com a filiais</param>
        /// <returns>Collection Aprovador</returns> 
        /// <history> 
        ///     [haguiar] 08/04/2011 Created 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public Collection<SafWeb.Model.Solicitacao.Aprovador> ListarAprovadoresConting(int pintRegional,
                                                                                int pintFilial)
        {
            Collection<SafWeb.Model.Solicitacao.Aprovador> colRetorno = null;

            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_APROVADOR_CONTING");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Parâmetros de entrada
            cmdCommand.Parameters.Add(new FWParameter("@Id_Regional", FWDbType.Int32));
            cmdCommand.Parameters["@Id_Regional"].Value = pintRegional;

            cmdCommand.Parameters.Add(new FWParameter("@Id_Filial", FWDbType.Int32));
            cmdCommand.Parameters["@Id_Filial"].Value = pintFilial;

            //Executa a função no banco de dados 
            System.Data.IDataReader idrRetorno;
            SafWeb.Model.Solicitacao.Aprovador objRetorno = null;
            colRetorno = new Collection<SafWeb.Model.Solicitacao.Aprovador>();

            idrRetorno = conProvider.ExecuteDataReader(cmdCommand);

            while (idrRetorno.Read())
            {
                objRetorno = new SafWeb.Model.Solicitacao.Aprovador();
                objRetorno.FromIDataReader(idrRetorno);
                colRetorno.Add(objRetorno);
            }

            return colRetorno;
        }

        #endregion

        #region Inserir Aporovador Segundo Nível

        /// <summary>
        ///     Insere os registros na base
        /// </summary>
        /// <param name="pintAprovador">Id do aprovador</param>
        /// <param name="pintAprovadorSegNivel">id do aprovador do segundo nível</param>
        /// <history>
        ///     [mribeiro] created 13/07/2009
        /// </history>
        public bool InserirAprovadorSegNiveil(int pintAprovador,
                                              int pintAprovadorSegNivel)
        {
            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_UPD_APROVADOR");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            cmdCommand.Parameters.Add(new FWParameter("@Id_Usuario", FWDbType.Int32));
            cmdCommand.Parameters["@Id_Usuario"].Value = pintAprovador;

            cmdCommand.Parameters.Add(new FWParameter("@Id_AprovaSegNivel", FWDbType.Int32));
            cmdCommand.Parameters["@Id_AprovaSegNivel"].Value = pintAprovadorSegNivel;

            //Executa a função no banco de dados 
            bool blnRetorno = false;

            try
            {
                conProvider.ExecuteScalar(cmdCommand);
                blnRetorno = true;
            }
            catch (Exception ex)
            {
                throw;
            }

            return blnRetorno;
        }

        #endregion

        #region Alterar Situação Solicitacao Cracha Titular

        /// <summary>
        ///      Alterar a situação da solicitação  cracha titular (Ativar/Inativar)
        /// </summary>
        /// <param name="pintId_SolicitacaoCrachaTitular">Código da Solicitação Cracha Titular</param>
        /// <param name="pblnFlgSituacao"></param>
        /// <history>
        ///      [haguiar] created 25/02/2012 23:24
        /// </history>
        /// <returns></returns>
        public bool AlterarSituacaoCrachaTitular(int pintId_SolicitacaoCrachaTitular,
                                    bool pblnFlgSituacao,
                                    int pintUsuario)
        {
            bool blnRetorno = false;

            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_UPD_STATUS_SOLICITACAOCRACHATITULAR");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Parâmetros de entrada
            cmdCommand.Parameters.Add(new FWParameter("@Id_SolicitacaoCrachaTitular", FWDbType.Int32));
            cmdCommand.Parameters["@Id_SolicitacaoCrachaTitular"].Value = pintId_SolicitacaoCrachaTitular;

            cmdCommand.Parameters.Add(new FWParameter("@Id_UsuarioAprovador", FWDbType.Int32));
            cmdCommand.Parameters["@Id_UsuarioAprovador"].Value = pintUsuario;

            cmdCommand.Parameters.Add(new FWParameter("@Flg_Situacao", FWDbType.Boolean));
            cmdCommand.Parameters["@Flg_Situacao"].Value = pblnFlgSituacao;

            try
            {
                //Executa a função no banco de dados 
                conProvider.ExecuteDataReader(cmdCommand);
                blnRetorno = true;
            }
            catch (Exception ex)
            {
                throw;
            }

            return blnRetorno;
        }

        #endregion

        #region Alterar Situação Solicitção

        /// <summary>
        ///      Alterar a situação da solicitação (Ativar/Inativar)
        /// </summary>
        /// <param name="intIdSolicitacao">Código da Solicitação</param>
        /// <param name="intStuacao">1 - True/ 2 - False</param>
        /// <history>
        ///      [mribeiro] created 15/07/2009
        /// </history>
        /// <returns></returns>
        public bool AlterarSituacao(int intIdSolicitacao,
                                    int intStuacao,
                                    int intUsuario)
        {
            bool blnRetorno = false;

            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_UPD_STATUS_SOLICITACAO");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Parâmetros de entrada
            cmdCommand.Parameters.Add(new FWParameter("@Id_Solicitacao", FWDbType.Int32));
            cmdCommand.Parameters["@Id_Solicitacao"].Value = intIdSolicitacao;

            cmdCommand.Parameters.Add(new FWParameter("@Flg_Situacao", FWDbType.Int32));
            cmdCommand.Parameters["@Flg_Situacao"].Value = intStuacao;

            cmdCommand.Parameters.Add(new FWParameter("@Id_UsuarioAprovador", FWDbType.Int32));
            cmdCommand.Parameters["@Id_UsuarioAprovador"].Value = intUsuario;

            try
            {
                //Executa a função no banco de dados 
                conProvider.ExecuteDataReader(cmdCommand);
                blnRetorno = true;
            }
            catch (Exception ex)
            {
                throw;
            }

            return blnRetorno;
        }

        #endregion

        #region Listar Feriados

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        ///     Lista os feriados 
        /// </summary> 
        /// <param name="pintRegional">Id Regional</param>
        /// <param name="pintFilial">Id Filial</param>
        /// <returns>DataTable com os feriados</returns> 
        /// <history> 
        ///     [mribeiro] 17/07/2009 Created 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public DataTable ListarFeriado(int pintRegional,
                                       int pintFilial)
        {
            DataTable dttRetorno;

            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_FERIADOS");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Parâmetros de entrada
            cmdCommand.Parameters.Add(new FWParameter("@Id_Regional", FWDbType.Varchar));
            cmdCommand.Parameters["@Id_Regional"].Value = pintRegional;

            cmdCommand.Parameters.Add(new FWParameter("@Id_Filial", FWDbType.Varchar));
            cmdCommand.Parameters["@Id_Filial"].Value = pintFilial;

            //Executa a função no banco de dados 
            dttRetorno = new DataTable();

            try
            {
                dttRetorno = conProvider.ExecuteDataTable(cmdCommand);
            }
            catch (Exception ex)
            {
                throw;
            }

            return dttRetorno;
        }

        #endregion

        #region Alterar Solicitação

        /// <summary>
        ///     Altera os registros na base
        /// </summary>
        /// <param name="pobjSolicitacao">Objeto Solicitacao</param>
        /// <returns>True/False</returns>
        /// <history>
        ///     [mribeiro] created 20/07/2009
        /// </history>
        public bool AlterarSolicitacao(SafWeb.Model.Solicitacao.Solicitacao pobjSolicitacao)
        {
            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_UPD_SOLICITACAO");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            cmdCommand.Parameters.Add(new FWParameter("@Id_Solicitacao", FWDbType.Int32));
            cmdCommand.Parameters["@Id_Solicitacao"].Value = pobjSolicitacao.Codigo;

            cmdCommand.Parameters.Add(new FWParameter("@Id_UsuarioSolicitante", FWDbType.Int32));
            cmdCommand.Parameters["@Id_UsuarioSolicitante"].Value = pobjSolicitacao.CodUsuSolic;

            cmdCommand.Parameters.Add(new FWParameter("@Id_Visitado", FWDbType.Int32));
            cmdCommand.Parameters["@Id_Visitado"].Value = pobjSolicitacao.CodVisitado;

            if (pobjSolicitacao.CodVisitante > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Id_Visitante", FWDbType.Int32));
                cmdCommand.Parameters["@Id_Visitante"].Value = pobjSolicitacao.CodVisitante;
            }

            if (pobjSolicitacao.CodVeiculo > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Id_Veiculo", FWDbType.Int32));
                cmdCommand.Parameters["@Id_Veiculo"].Value = pobjSolicitacao.CodVeiculo;
            }

            if (pobjSolicitacao.CodVeiculoLista > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Id_VeiculoLista", FWDbType.Int32));
                cmdCommand.Parameters["@Id_VeiculoLista"].Value = pobjSolicitacao.CodVeiculoLista;
            }

            cmdCommand.Parameters.Add(new FWParameter("@Dt_InicioVisita", FWDbType.DateTime));
            cmdCommand.Parameters["@Dt_InicioVisita"].Value = pobjSolicitacao.InicioVisita;

            cmdCommand.Parameters.Add(new FWParameter("@Dt_FimVisita", FWDbType.DateTime));
            cmdCommand.Parameters["@Dt_FimVisita"].Value = pobjSolicitacao.FimVisita;

            cmdCommand.Parameters.Add(new FWParameter("@Flg_AcSabado", FWDbType.Byte));
            cmdCommand.Parameters["@Flg_AcSabado"].Value = pobjSolicitacao.AcSabado;

            cmdCommand.Parameters.Add(new FWParameter("@Flg_AcDomingo", FWDbType.Byte));
            cmdCommand.Parameters["@Flg_AcDomingo"].Value = pobjSolicitacao.AcDomingo;

            cmdCommand.Parameters.Add(new FWParameter("@Flg_AcFeriado", FWDbType.Byte));
            cmdCommand.Parameters["@Flg_AcFeriado"].Value = pobjSolicitacao.AcFeriado;

            cmdCommand.Parameters.Add(new FWParameter("@Id_Area", FWDbType.Varchar, 60));
            cmdCommand.Parameters["@Id_Area"].Value = pobjSolicitacao.CodArea;

            cmdCommand.Parameters.Add(new FWParameter("@Id_TipoSolicitacao", FWDbType.Int32));
            cmdCommand.Parameters["@Id_TipoSolicitacao"].Value = pobjSolicitacao.CodTipoSolicitacao;

            cmdCommand.Parameters.Add(new FWParameter("@Id_MotivoVisita", FWDbType.Int32));
            cmdCommand.Parameters["@Id_MotivoVisita"].Value = pobjSolicitacao.CodMotivoVisita;

            cmdCommand.Parameters.Add(new FWParameter("@Des_ObsSolicitacao", FWDbType.Varchar, 200));
            cmdCommand.Parameters["@Des_ObsSolicitacao"].Value = pobjSolicitacao.Observacao;

            //Executa a função no banco de dados 
            bool blnRetorno = false;

            try
            {
                conProvider.ExecuteScalar(cmdCommand);
                blnRetorno = true;
            }
            catch (Exception ex)
            {
                throw;
            }

            return blnRetorno;
        }

        #endregion

        #region Obter Solicitacao

        /// <summary>
        ///     Obtem os registros na base
        /// </summary>
        /// <param name="pintIdSolicitacao">Id da Solicitacao</param>
        /// <history>
        ///     [mribeiro] created 20/07/2009
        /// </history>
        /// <returns>Codigo (RE) do aprovador</returns>
        public SafWeb.Model.Solicitacao.SolicitacaoColaborador ObterSolicitacao(int pintIdSolicitacao)
        {
            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_OBTER_SOLICITACAO");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            cmdCommand.Parameters.Add(new FWParameter("@Id_Solicitacao", FWDbType.Int32));
            cmdCommand.Parameters["@Id_Solicitacao"].Value = pintIdSolicitacao;

            cmdCommand.Parameters.Add(new FWParameter("@OUT_Id_Solicitacao", FWDbType.Int32));
            cmdCommand.Parameters["@OUT_Id_Solicitacao"].Direction = ParameterDirection.Output;

            cmdCommand.Parameters.Add(new FWParameter("@OUT_Id_Filial", FWDbType.Int32));
            cmdCommand.Parameters["@OUT_Id_Filial"].Direction = ParameterDirection.Output;

            cmdCommand.Parameters.Add(new FWParameter("@OUT_Id_Regional", FWDbType.Int32));
            cmdCommand.Parameters["@OUT_Id_Regional"].Direction = ParameterDirection.Output;

            cmdCommand.Parameters.Add(new FWParameter("@OUT_Id_Visitado", FWDbType.Int32));
            cmdCommand.Parameters["@OUT_Id_Visitado"].Direction = ParameterDirection.Output;

            cmdCommand.Parameters.Add(new FWParameter("@OUT_Id_Visitante", FWDbType.Int32));
            cmdCommand.Parameters["@OUT_Id_Visitante"].Direction = ParameterDirection.Output;

            cmdCommand.Parameters.Add(new FWParameter("@OUT_Nom_Visitante", FWDbType.Varchar, 100));
            cmdCommand.Parameters["@OUT_Nom_Visitante"].Direction = ParameterDirection.Output;

            cmdCommand.Parameters.Add(new FWParameter("@OUT_Id_MotivoVisita", FWDbType.Int32));
            cmdCommand.Parameters["@OUT_Id_MotivoVisita"].Direction = ParameterDirection.Output;

            cmdCommand.Parameters.Add(new FWParameter("@OUT_Id_Area", FWDbType.Varchar, 60));
            cmdCommand.Parameters["@OUT_Id_Area"].Direction = ParameterDirection.Output;

            cmdCommand.Parameters.Add(new FWParameter("@OUT_Des_ObsSolicitacao", FWDbType.Varchar, 200));
            cmdCommand.Parameters["@OUT_Des_ObsSolicitacao"].Direction = ParameterDirection.Output;

            cmdCommand.Parameters.Add(new FWParameter("@OUT_Dt_InicioVisita", FWDbType.DateTime));
            cmdCommand.Parameters["@OUT_Dt_InicioVisita"].Direction = ParameterDirection.Output;

            cmdCommand.Parameters.Add(new FWParameter("@OUT_Dt_FimVisita", FWDbType.DateTime));
            cmdCommand.Parameters["@OUT_Dt_FimVisita"].Direction = ParameterDirection.Output;

            cmdCommand.Parameters.Add(new FWParameter("@OUT_Flg_AcSabado", FWDbType.Byte));
            cmdCommand.Parameters["@OUT_Flg_AcSabado"].Direction = ParameterDirection.Output;

            cmdCommand.Parameters.Add(new FWParameter("@OUT_Flg_AcDomingo", FWDbType.Byte));
            cmdCommand.Parameters["@OUT_Flg_AcDomingo"].Direction = ParameterDirection.Output;

            cmdCommand.Parameters.Add(new FWParameter("@OUT_Flg_AcFeriado", FWDbType.Byte));
            cmdCommand.Parameters["@OUT_Flg_AcFeriado"].Direction = ParameterDirection.Output;

            cmdCommand.Parameters.Add(new FWParameter("@OUT_Id_Veiculo", FWDbType.Int32));
            cmdCommand.Parameters["@OUT_Id_Veiculo"].Direction = ParameterDirection.Output;

            cmdCommand.Parameters.Add(new FWParameter("@OUT_Id_Aprovador", FWDbType.Int32));
            cmdCommand.Parameters["@OUT_Id_Aprovador"].Direction = ParameterDirection.Output;

            cmdCommand.Parameters.Add(new FWParameter("@OUT_Id_Estado", FWDbType.Int32));
            cmdCommand.Parameters["@OUT_Id_Estado"].Direction = ParameterDirection.Output;

            cmdCommand.Parameters.Add(new FWParameter("@OUT_Id_TipoSolicitacao", FWDbType.Int32));
            cmdCommand.Parameters["@OUT_Id_TipoSolicitacao"].Direction = ParameterDirection.Output;

            cmdCommand.Parameters.Add(new FWParameter("@OUT_Id_ColLista", FWDbType.Varchar, 4000));
            cmdCommand.Parameters["@OUT_Id_ColLista"].Direction = ParameterDirection.Output;

            cmdCommand.Parameters.Add(new FWParameter("@OUT_Id_ListaVeiculo", FWDbType.Int32));
            cmdCommand.Parameters["@OUT_Id_ListaVeiculo"].Direction = ParameterDirection.Output;
            

            // Execucao
            SafWeb.Model.Solicitacao.SolicitacaoColaborador objRetorno = new SafWeb.Model.Solicitacao.SolicitacaoColaborador();

            try
            {
                conProvider.ExecuteScalar(cmdCommand);

                if (!(cmdCommand.Parameters["@OUT_Id_Solicitacao"].Value == DBNull.Value))
                    objRetorno.CodSolicitacao = Convert.ToInt32(cmdCommand.Parameters["@OUT_Id_Solicitacao"].Value);

                //Popula Objeto Lista
                if (!(cmdCommand.Parameters["@OUT_Id_Filial"].Value == DBNull.Value))
                    objRetorno.CodFilial = Convert.ToInt32(cmdCommand.Parameters["@OUT_Id_Filial"].Value);
                if (!(cmdCommand.Parameters["@OUT_Id_Regional"].Value == DBNull.Value))
                    objRetorno.CodRegional = Convert.ToInt32(cmdCommand.Parameters["@OUT_Id_Regional"].Value);
                if (!(cmdCommand.Parameters["@OUT_Id_Visitado"].Value == DBNull.Value))
                    objRetorno.CodVisitado = Convert.ToInt32(cmdCommand.Parameters["@OUT_Id_Visitado"].Value);
                if (!(cmdCommand.Parameters["@OUT_Id_Visitante"].Value == DBNull.Value))
                    objRetorno.CodVisitante = Convert.ToInt32(cmdCommand.Parameters["@OUT_Id_Visitante"].Value);
                if (!(cmdCommand.Parameters["@OUT_Nom_Visitante"].Value == DBNull.Value))
                    objRetorno.NomeVisitante = Convert.ToString(cmdCommand.Parameters["@OUT_Nom_Visitante"].Value);
                if (!(cmdCommand.Parameters["@OUT_Id_MotivoVisita"].Value == DBNull.Value))
                    objRetorno.CodMotivoVisita = Convert.ToInt32(cmdCommand.Parameters["@OUT_Id_MotivoVisita"].Value);
                if (!(cmdCommand.Parameters["@OUT_Id_Area"].Value == DBNull.Value))
                    objRetorno.CodArea = Convert.ToString(cmdCommand.Parameters["@OUT_Id_Area"].Value);
                if (!(cmdCommand.Parameters["@OUT_Des_ObsSolicitacao"].Value == DBNull.Value))
                    objRetorno.Observacao = Convert.ToString(cmdCommand.Parameters["@OUT_Des_ObsSolicitacao"].Value);
                if (!(cmdCommand.Parameters["@OUT_Dt_InicioVisita"].Value == DBNull.Value))
                    objRetorno.InicioVisita = Convert.ToDateTime(cmdCommand.Parameters["@OUT_Dt_InicioVisita"].Value);
                if (!(cmdCommand.Parameters["@OUT_Dt_FimVisita"].Value == DBNull.Value))
                    objRetorno.FimVisita = Convert.ToDateTime(cmdCommand.Parameters["@OUT_Dt_FimVisita"].Value);
                if (!(cmdCommand.Parameters["@OUT_Flg_AcSabado"].Value == DBNull.Value))
                    objRetorno.AcSabado = Convert.ToBoolean(cmdCommand.Parameters["@OUT_Flg_AcSabado"].Value);
                if (!(cmdCommand.Parameters["@OUT_Flg_AcDomingo"].Value == DBNull.Value))
                    objRetorno.AcDomingo = Convert.ToBoolean(cmdCommand.Parameters["@OUT_Flg_AcDomingo"].Value);
                if (!(cmdCommand.Parameters["@OUT_Flg_AcFeriado"].Value == DBNull.Value))
                    objRetorno.AcFeriado = Convert.ToBoolean(cmdCommand.Parameters["@OUT_Flg_AcFeriado"].Value);
                if (!(cmdCommand.Parameters["@OUT_Id_Veiculo"].Value == DBNull.Value))
                    objRetorno.CodVeiculo = Convert.ToInt32(cmdCommand.Parameters["@OUT_Id_Veiculo"].Value);
                if (!(cmdCommand.Parameters["@OUT_Id_Aprovador"].Value == DBNull.Value))
                    objRetorno.CodAprovador = Convert.ToInt32(cmdCommand.Parameters["@OUT_Id_Aprovador"].Value);
                if (!(cmdCommand.Parameters["@OUT_Id_Estado"].Value == DBNull.Value))
                    objRetorno.CodEstadoVeiculo = Convert.ToInt32(cmdCommand.Parameters["@OUT_Id_Estado"].Value);
                if (!(cmdCommand.Parameters["@OUT_Id_TipoSolicitacao"].Value == DBNull.Value))
                    objRetorno.CodTipoSolicitacao = Convert.ToInt32(cmdCommand.Parameters["@OUT_Id_TipoSolicitacao"].Value);
                if (!(cmdCommand.Parameters["@OUT_Id_ColLista"].Value == DBNull.Value))
                    objRetorno.CodInclusos = Convert.ToString(cmdCommand.Parameters["@OUT_Id_ColLista"].Value);
                if (!(cmdCommand.Parameters["@OUT_Id_ListaVeiculo"].Value == DBNull.Value))
                    objRetorno.CodListaVeiculos = Convert.ToInt32(cmdCommand.Parameters["@OUT_Id_ListaVeiculo"].Value);
            }
            catch (Exception ex)
            {
                throw;
            }

            return objRetorno;
        }

        #endregion

        #region Excluir Status

        /// <summary>
        ///     Exclui os registros na base
        /// </summary>
        /// <param name="pintIdSolicitacao">Id da Solicitacao</param>
        /// <returns>True/False</returns>
        /// <history>
        ///     [mribeiro] created 21/07/2009
        /// </history>
        public bool ExcluirStatus(int pintIdSolicitacao)
        {
            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_DEL_APROV_SOLICITACAO");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            cmdCommand.Parameters.Add(new FWParameter("@Id_Solicitacao", FWDbType.Int32));
            cmdCommand.Parameters["@Id_Solicitacao"].Value = pintIdSolicitacao;

            //Executa a função no banco de dados 
            bool blnRetorno = false;

            try
            {
                conProvider.ExecuteScalar(cmdCommand);
                blnRetorno = true;
            }
            catch (Exception ex)
            {
                throw;
            }

            return blnRetorno;
        }

        #endregion

        #region Excluir SolicitaçãoLista

        /// <summary>
        ///     Exclui os registros na base
        /// </summary>
        /// <param name="pintIdSolicitacao">Id da Solicitacao</param>
        /// <param name="pintIdLista">Código da Lista</param>
        /// <returns>True/False</returns>
        /// <history>
        ///     [mribeiro] created 21/07/2009
        /// </history>
        public bool ExcluirSolicitacaoLista(int pintIdSolicitacao,
                                            int pintIdLista)
        {
            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_DEL_SOLICITACAO_LISTA");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            cmdCommand.Parameters.Add(new FWParameter("@Id_Solicitacao", FWDbType.Int32));
            cmdCommand.Parameters["@Id_Solicitacao"].Value = pintIdSolicitacao;

            cmdCommand.Parameters.Add(new FWParameter("@Id_ColaboradorLista", FWDbType.Int32));
            cmdCommand.Parameters["@Id_ColaboradorLista"].Value = pintIdLista;

            //Executa a função no banco de dados 
            bool blnRetorno = false;

            try
            {
                conProvider.ExecuteScalar(cmdCommand);
                blnRetorno = true;
            }
            catch (Exception ex)
            {
                throw;
            }

            return blnRetorno;
        }

        #endregion

        #region Verificar Status

        /// <summary>
        ///     Seleciona os registros na base
        /// </summary>
        /// <param name="pintIdSolicitacao">Id da Solicitacao</param>
        /// <returns>True/False</returns>
        /// <history>
        ///     [mribeiro] created 22/07/2009
        /// </history>
        public bool VerificarStatus(int pintIdSolicitacao)
        {
            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_VERIFICA_APROVACAO");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            cmdCommand.Parameters.Add(new FWParameter("@Id_Solicitacao", FWDbType.Int32));
            cmdCommand.Parameters["@Id_Solicitacao"].Value = pintIdSolicitacao;

            //Executa a função no banco de dados 
            bool blnRetorno;

            try
            {
                blnRetorno = Convert.ToBoolean(conProvider.ExecuteScalar(cmdCommand));
            }
            catch (Exception ex)
            {
                throw;
            }

            return blnRetorno;
        }


        #endregion

        #region Obter Aprovadores

        /// <summary>
        ///     Obtem os registros na base
        /// </summary>
        /// <param name="pintIdAprovador">Id da solicitacao</param>
        /// <history>
        ///     [mribeiro] created 22/10/2009
        /// </history>
        /// <returns>Aprovadores da solicitação</returns>
        public DataTable ObterAprovadores(int pintIdSolicitacao)
        {
            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_APROVADORES");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            cmdCommand.Parameters.Add(new FWParameter("@Id_Solicitacao", FWDbType.Int32));
            cmdCommand.Parameters["@Id_Solicitacao"].Value = pintIdSolicitacao;

            //Executa a função no banco de dados 
            DataTable dttRetorno;

            try
            {
                dttRetorno = conProvider.ExecuteDataTable(cmdCommand);
            }
            catch (Exception ex)
            {
                throw;
            }

            return dttRetorno;
        }

        #endregion

        #region Obter Aprovadores Cracha Titlar

        /// <summary>
        ///     Obtem os registros na base
        /// </summary>
        /// <param name="pintId_SolicitacaoCrachaTitular">Id da solicitacao cracha titular</param>
        /// <history>
        ///     [haguiar] created 26/02/2012 08:57
        /// </history>
        /// <returns>Aprovadores da solicitação</returns>
        public DataTable ObterAprovadoresCrachaTitular(int pintId_SolicitacaoCrachaTitular)
        {
            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_APROVADORES_CRACHATITULAR");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            cmdCommand.Parameters.Add(new FWParameter("@Id_SolicitacaoCrachaTitular", FWDbType.Int32));
            cmdCommand.Parameters["@Id_SolicitacaoCrachaTitular"].Value = pintId_SolicitacaoCrachaTitular;

            //Executa a função no banco de dados 
            DataTable dttRetorno;

            try
            {
                dttRetorno = conProvider.ExecuteDataTable(cmdCommand);
            }
            catch (Exception ex)
            {
                throw;
            }

            return dttRetorno;
        }

        #endregion

        #region Obter Ultimo Usuario Aprovador
        /// <summary>
        ///     Obtém o nome do Ultimo Usuario Aprovador
        /// </summary>
        /// <param name="pintIdEscalacao">Id da Escalação</param>
        /// <history>
        ///     [tgerevini] created 23/6/2010
        /// </history>
        public String ObterUltimoAprovador(int pintIdEscalacao)
        {

            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_ObterUltimoAprovadorSolicitacao");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Parâmetros de entrada
            cmdCommand.Parameters.Add(new FWParameter("@Id_Solicitacao", FWDbType.Int32));
            cmdCommand.Parameters["@Id_Solicitacao"].Value = pintIdEscalacao;

            //Executa a função no banco de dados 
            System.Data.IDataReader idrRetorno;
            string strRetorno = string.Empty;

            idrRetorno = conProvider.ExecuteDataReader(cmdCommand);

            while (idrRetorno.Read())
            {
                strRetorno = idrRetorno["USU_C_NOME"].ToString();
            }

            return strRetorno;
        }
        #endregion

    }
}
