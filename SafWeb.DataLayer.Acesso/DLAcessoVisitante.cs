using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using FrameWork.DataLayer.Utilitarios;
using System.Data;

namespace SafWeb.DataLayer.Acesso
{
    public class DLAcessoVisitante : DALFWBase
    {
        #region Construtor

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        ///     Constructor 
        /// </summary> 
        /// <history> 
        ///     [vsantos] 16/07/2009 Created 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public DLAcessoVisitante()
        {
            strConnection = DALUtilitarios.GetStringConnection(DALUtilitarios.ConexoesDisponiveis.ConnectionFW);
            conProvider = new DALProvider(strConnection);
            intCommandTimeOut = DALUtilitarios.ObterCommandTimeOut();
        }

        #endregion

        #region Listar Visitas Agendadas

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        ///     Lista registros da base 
        /// </summary> 
        /// <param name="pintCodRegional">Código da Regional</param>
        /// <param name="pintCodFilial">Código da Filial</param>
        /// <param name="pintCodTipoDoc">Código do Tipo de Documento</param>
        /// <param name="pstrDocumento">Documento</param>
        /// <param name="pstrVisitante">Nome do Visitante</param>
        /// <param name="pstrAprovador">Nome do Aprovador</param>
        /// <param name="pdatInicioVis">Data início da visita</param>
        /// <param name="pdatFimVis">Data fim da visita</param>
        /// <returns>Datatable</returns> 
        /// <history> 
        ///     [vsantos] 16/07/2009 Created 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public DataTable ListarVisitasAgendadas(int pintCodTipoDoc,
                                                string pstrDocumento,
                                                string pstrVisitante,
                                                string pstrAprovador,
                                                int pintIdUsuario,
                                                int pintIdSolicitacao,
                                                int pintIdEmpresa)
        {
            DataTable dttRetorno = null;

            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_VISITA_AGENDADA");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Parâmetros de entrada
            if (pintCodTipoDoc > -1)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Id_TipoDocumento", FWDbType.Int32));
                cmdCommand.Parameters["@Id_TipoDocumento"].Value = pintCodTipoDoc;
            }

            if (pstrDocumento != "")
            {
                cmdCommand.Parameters.Add(new FWParameter("@Num_Documento", FWDbType.Varchar, 30));
                cmdCommand.Parameters["@Num_Documento"].Value = pstrDocumento;
            }

            if (pstrVisitante != "")
            {
                cmdCommand.Parameters.Add(new FWParameter("@Nom_Visitante", FWDbType.Varchar, 100));
                cmdCommand.Parameters["@Nom_Visitante"].Value = pstrVisitante;
            }

            if (pstrAprovador != "")
            {
                cmdCommand.Parameters.Add(new FWParameter("@Nom_Aprovador", FWDbType.Varchar, 100));
                cmdCommand.Parameters["@Nom_Aprovador"].Value = pstrAprovador;
            }

            if (pintIdUsuario > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@USU_N_CODIGO", FWDbType.Int32));
                cmdCommand.Parameters["@USU_N_CODIGO"].Value = pintIdUsuario;
            }

            if (pintIdSolicitacao > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Id_Solicitacao", FWDbType.Int32));
                cmdCommand.Parameters["@Id_Solicitacao"].Value = pintIdSolicitacao;
            }

            if (pintIdEmpresa > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Id_Empresa", FWDbType.Int32));
                cmdCommand.Parameters["@Id_Empresa"].Value = pintIdEmpresa;
            }

            //Executa a função no banco de dados 
            dttRetorno = new DataTable();

            try
            {
                dttRetorno = conProvider.ExecuteDataTable(cmdCommand);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dttRetorno;
        }

        #endregion

        #region Listar

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        ///     Lista registros da base 
        /// </summary> 
        /// <param name="pobjAcesso">Model Acesso</param>
        /// <returns>Coleção de objetos do tipo Acesso</returns> 
        /// <history> 
        ///     [vsantos] 17/07/2009 Created 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public Collection<Model.Acesso.AcessoVisitante> Listar(Model.Acesso.AcessoVisitante pobjAcesso, bool pblnBuscaCracha)
        {
            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_ACESSO_VISITANTE");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Parâmetros de entrada
            cmdCommand.Parameters.Add(new FWParameter("@Busca_Cracha", FWDbType.Byte));
            cmdCommand.Parameters["@Busca_Cracha"].Value = pblnBuscaCracha;

            if (pobjAcesso.NomVisitante != string.Empty)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Nom_Visitante", FWDbType.Varchar, 100));
                cmdCommand.Parameters["@Nom_Visitante"].Value = pobjAcesso.NomVisitante;
            }

            if (pobjAcesso.NumCracha != string.Empty)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Num_Cracha", FWDbType.Varchar, 18));
                cmdCommand.Parameters["@Num_Cracha"].Value = pobjAcesso.NumCracha;
            }

            if (pobjAcesso.Placa != string.Empty)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Des_Placa", FWDbType.Varchar, 8));
                cmdCommand.Parameters["@Des_Placa"].Value = pobjAcesso.Placa;
            }

            if (pobjAcesso.CodTipoDocumento > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Id_DocumentoTipo", FWDbType.Int32));
                cmdCommand.Parameters["@Id_DocumentoTipo"].Value = pobjAcesso.CodTipoDocumento;
            }

            if (pobjAcesso.NumDocumento != "")
            {
                cmdCommand.Parameters.Add(new FWParameter("@Num_Documento", FWDbType.Varchar, 10));
                cmdCommand.Parameters["@Num_Documento"].Value = pobjAcesso.NumDocumento;
            }

            cmdCommand.Parameters.Add(new FWParameter("@USU_N_CODIGO", FWDbType.Int32));
            cmdCommand.Parameters["@USU_N_CODIGO"].Value = pobjAcesso.CodUsuario;

            //Executa a função no banco de dados 
            Collection<Model.Acesso.AcessoVisitante> colRetorno = null;

            try
            {
                IDataReader idrRetorno;
                Model.Acesso.AcessoVisitante objRetorno = null;
                colRetorno = new Collection<Model.Acesso.AcessoVisitante>();

                idrRetorno = conProvider.ExecuteDataReader(cmdCommand);

                while (idrRetorno.Read())
                {
                    objRetorno = new Model.Acesso.AcessoVisitante();
                    objRetorno.FromIDataReader(idrRetorno);
                    colRetorno.Add(objRetorno);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return colRetorno;
        }

        #endregion

        #region Verificar Entrada

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        ///     Obtem registros da base 
        /// </summary> 
        /// <param name="pintIdVisitante">Id do Visitante</param>
        /// <returns>True/False</returns> 
        /// <history> 
        ///     [mribeiro] 29/07/2009 Created 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public bool VerificarEntrada(int pintIdVisitante)
        {
            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_VERIFICAR_ENTRADA");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Parâmetros de entrada
            cmdCommand.Parameters.Add(new FWParameter("@Id_Visitante", FWDbType.Int32));
            cmdCommand.Parameters["@Id_Visitante"].Value = pintIdVisitante;

            //Executa a função no banco de dados 
            bool blnRetorno;

            try
            {
                blnRetorno = Convert.ToBoolean(conProvider.ExecuteScalar(cmdCommand));
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return blnRetorno;
        }

        #endregion

        #region Inserir

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        ///     Inserir registros da base 
        /// </summary> 
        /// <param name="pobjAcesso">Model de Acesso</param>
        /// <returns>Código do registro inserido</returns> 
        /// <history> 
        ///     [vsantos] 17/07/2009 Created 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public int Inserir(Model.Acesso.AcessoVisitante pobjAcesso)
        {
            int intRetorno;

            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_INS_ACESSO_VISITANTE");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Parâmetros de entrada
            cmdCommand.Parameters.Add(new FWParameter("@Id_Solicitacao", FWDbType.Int32));
            cmdCommand.Parameters["@Id_Solicitacao"].Value = pobjAcesso.CodSolicitacao;

            cmdCommand.Parameters.Add(new FWParameter("@Id_Visitante", FWDbType.Int32));
            cmdCommand.Parameters["@Id_Visitante"].Value = pobjAcesso.CodVisitante;

            cmdCommand.Parameters.Add(new FWParameter("@Dt_Entrada", FWDbType.DateTime));
            cmdCommand.Parameters["@Dt_Entrada"].Value = pobjAcesso.Entrada;

            cmdCommand.Parameters.Add(new FWParameter("@Dt_ValidadeVisita", FWDbType.DateTime));
            cmdCommand.Parameters["@Dt_ValidadeVisita"].Value = pobjAcesso.ValidadeVisita;

            cmdCommand.Parameters.Add(new FWParameter("@Id_ColaboradorAcomp", FWDbType.Int32));
            cmdCommand.Parameters["@Id_ColaboradorAcomp"].Value = pobjAcesso.CodAcompanhante;

            if (pobjAcesso.CodCracha > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Id_Cracha", FWDbType.Int32));
                cmdCommand.Parameters["@Id_Cracha"].Value = pobjAcesso.CodCracha;
            }

            if (pobjAcesso.CodVeiculo > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Id_Veiculo", FWDbType.Int32));
                cmdCommand.Parameters["@Id_Veiculo"].Value = pobjAcesso.CodVeiculo;
            }

            cmdCommand.Parameters.Add(new FWParameter("@Id_UsuarioLibEntrada", FWDbType.Int32));
            cmdCommand.Parameters["@Id_UsuarioLibEntrada"].Value = pobjAcesso.CodUsuLibEnt;

            cmdCommand.Parameters.Add(new FWParameter("@Des_ObsVisita", FWDbType.Varchar, 100));
            cmdCommand.Parameters["@Des_ObsVisita"].Value = pobjAcesso.ObsVisita;

            //Executa a função no banco de dados 
            try
            {
                intRetorno = Convert.ToInt32(conProvider.ExecuteScalar(cmdCommand));

                if (intRetorno < 0)
                {
                    throw new Exception("A criação de permissões no RONDA excedeu o limite máximo permitido.\nEntre em contato com o SERVICE DESK (11) 3321-3200.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return intRetorno;
        }

        #endregion

        #region Alterar

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        ///     Edita registros da base 
        /// </summary> 
        /// <param name="pobjAcesso">Model Acesso</param>
        /// <returns>Boleano indicando sucesso da atualização</returns> 
        /// <history> 
        ///     [vsantos] 17/07/2009 Created 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public bool Alterar(Model.Acesso.AcessoVisitante pobjAcesso)
        {
            bool blnRetorno = false;

            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_UPD_ACESSO_VISITANTE");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Parâmetros de entrada
            cmdCommand.Parameters.Add(new FWParameter("@Id_AcessoVisitante", FWDbType.Int32));
            cmdCommand.Parameters["@Id_AcessoVisitante"].Value = pobjAcesso.CodAcessoVisitante;

            cmdCommand.Parameters.Add(new FWParameter("@Dt_Saida", FWDbType.DateTime));
            cmdCommand.Parameters["@Dt_Saida"].Value = pobjAcesso.Saida;

            cmdCommand.Parameters.Add(new FWParameter("@Id_UsuarioLibSaida", FWDbType.Int32));
            cmdCommand.Parameters["@Id_UsuarioLibSaida"].Value = pobjAcesso.CodUsuLibSai;

            //Executa a função no banco de dados 
            try
            {
                conProvider.ExecuteNonQuery(cmdCommand);

                blnRetorno = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return blnRetorno;
        }

        #endregion

        #region Id Acesso Visitante

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        ///     Obtem registros da base 
        /// </summary> 
        /// <param name="pstrCracha">Número do Crachá</param>
        /// <param name="pintUsuario">Id do Usuário Logado</param>
        /// <returns>Id do Acesso do Visitante</returns> 
        /// <history> 
        ///     [mribeiro] 19/08/2009 Created 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public DataTable IdAcessoVisitante(string pstrNumero,
                                          int pintUsuario,
                                          bool pblnCracha)
        {
            DataTable intRetorno;

            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_ID_ACESSO");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Parâmetros de entrada
            cmdCommand.Parameters.Add(new FWParameter("@Numero", FWDbType.Varchar, 18));
            cmdCommand.Parameters["@Numero"].Value = pstrNumero;

            cmdCommand.Parameters.Add(new FWParameter("@USU_N_CODIGO", FWDbType.Int32));
            cmdCommand.Parameters["@USU_N_CODIGO"].Value = pintUsuario;

            cmdCommand.Parameters.Add(new FWParameter("@Cracha", FWDbType.Byte));
            cmdCommand.Parameters["@Cracha"].Value = pblnCracha;

            //Executa a função no banco de dados 
            try
            {
                intRetorno = conProvider.ExecuteDataTable(cmdCommand);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return intRetorno;
        }

        #endregion

        #region Obter Visitante

        /// <summary>
        ///     Obtém dados do Visitante
        /// </summary>
        /// <param name="pintIdColaborador">Código do Colaborador</param>
        /// <returns>DataTable</returns>
        /// <history>
        ///     [tgerevini] created 18/05/2010
        /// </history>
        public DataTable ObterVisitante(int pintIdColaborador)
        {
            DataTable dttRetorno;

            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_ObterDadosVisitante");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Parâmetros de entrada

            cmdCommand.Parameters.Add(new FWParameter("@Id_Colaborador", FWDbType.Int32));
            cmdCommand.Parameters["@Id_Colaborador"].Value = pintIdColaborador;

            //Executa a função no banco de dados 
            dttRetorno = new DataTable();
            dttRetorno = conProvider.ExecuteDataTable(cmdCommand);

            return dttRetorno;
        }
        #endregion
    }
}
