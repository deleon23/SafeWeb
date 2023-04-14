using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using FrameWork.DataLayer.Utilitarios;
using System.Data;
using SafWeb.Model.Colaborador;

namespace SafWeb.DataLayer.Colaborador
{
    public class DLColaborador: DALFWBase
    {
        #region Construtor
        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// Constructor 
        /// </summary> 
        /// <history> 
        /// [mribeiro] 22/6/2009 Created 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public DLColaborador()
        {
            strConnection = DALUtilitarios.GetStringConnection(DALUtilitarios.ConexoesDisponiveis.ConnectionFW);
            conProvider = new DALProvider(strConnection);
            intCommandTimeOut = DALUtilitarios.ObterCommandTimeOut();
        }
        #endregion

        #region Inserir

        /// <summary>
        ///     Insere os registros na base
        /// </summary>
        /// <param name="pobjColaborador">Objeto Colaborador</param>
        /// <returns>Código do Colaborador Cadastrado</returns>
        /// <history>
        ///     [mribeiro] created 02/07/2009
        /// </history>
        public int Inserir(SafWeb.Model.Colaborador.Colaborador pobjColaborador)
        {
            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_INS_COLABORADORES");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            cmdCommand.Parameters.Add(new FWParameter("@Id_TipoColaborador", FWDbType.Int32));
            cmdCommand.Parameters["@Id_TipoColaborador"].Value = pobjColaborador.IdTipoColaborador;

            //cmdCommand.Parameters.Add(new FWParameter("@Cod_Colaborador", FWDbType.Int32));
            //cmdCommand.Parameters["@Cod_Colaborador"].Value = pobjColaborador.CodigoColaborador;

            cmdCommand.Parameters.Add(new FWParameter("@Nom_Colaborador", FWDbType.Varchar, 50));
            cmdCommand.Parameters["@Nom_Colaborador"].Value = pobjColaborador.NomeColaborador;

            cmdCommand.Parameters.Add(new FWParameter("@Id_Empresa", FWDbType.Int32));
            cmdCommand.Parameters["@Id_Empresa"].Value = pobjColaborador.IdEmpresa;

            //Executa a função no banco de dados 
            int intIdColaborador;

            try
            {
                intIdColaborador = Convert.ToInt32(conProvider.ExecuteScalar(cmdCommand));
            }
            catch (Exception ex)
            {
                throw;
            }

            return intIdColaborador;
        }

        #endregion

        #region Inserir Documento Visitante

        /// <summary>
        ///     Insere os registros na base
        /// </summary>
        /// <param name="pobjColaborador">Objeto Colaborador</param>
        /// <returns>True/False</returns>
        /// <history>
        ///     [mribeiro] created 02/07/2009
        /// </history>
        public bool InserirDocumentoVisitante(SafWeb.Model.Colaborador.Colaborador pobjColaborador)
        {
            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_INS_DOCUMENTO_VISITANTE");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            cmdCommand.Parameters.Add(new FWParameter("@Id_Colaborador", FWDbType.Int32));
            cmdCommand.Parameters["@Id_Colaborador"].Value = pobjColaborador.IdColaborador;

            cmdCommand.Parameters.Add(new FWParameter("@Num_Documento", FWDbType.Varchar, 15));
            cmdCommand.Parameters["@Num_Documento"].Value = pobjColaborador.NumeroDocumento;

            cmdCommand.Parameters.Add(new FWParameter("@Id_DocumentoTipo", FWDbType.Int32));
            cmdCommand.Parameters["@Id_DocumentoTipo"].Value = pobjColaborador.IdTipoDocumento;

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

        #region Inserir e Alterar Foto Colaborador

        /// <summary> 
        ///     Insere Registro na Base
        /// </summary>
        /// <param name="pintIdColaborador">Id do Colaborador</param>
        /// <param name="pintIdTipoVisitante">Id do Tipo de Visitante(Funcionário, Terceiro)</param>
        /// <param name="pdtDataFoto">Data da Foto</param>
        /// <returns>True/False</returns>
        /// <history>
        ///     [aoliveira] created 05/02/2013
        /// </history>
        public bool InserirFotoVisitante(int pintIdColaborador, byte[] pFoto, DateTime pdtDataFoto)
        {
            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_INS_FOTO_VISITANTE");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            cmdCommand.Parameters.Add(new FWParameter("@Id_Colaborador", FWDbType.Int32));
            cmdCommand.Parameters["@Id_Colaborador"].Value = pintIdColaborador;

            cmdCommand.Parameters.Add(new FWParameter("@Img_ColaboradorFoto", FWDbType.VarBinary));
            cmdCommand.Parameters["@Img_ColaboradorFoto"].Value = pFoto;

            cmdCommand.Parameters.Add(new FWParameter("@Dt_ColaboradorFoto", FWDbType.DateTime));
            cmdCommand.Parameters["@Dt_ColaboradorFoto"].Value = pdtDataFoto;

            //Executa a função no banco de dados 
            bool blnRetorno = false;

            try
            {
                conProvider.ExecuteScalar(cmdCommand);
                blnRetorno = true;
            }
            catch
            {
                throw;
            }

            return blnRetorno;
        }

        /// <summary> 
        ///     Altera Registro na Base
        /// </summary>
        /// <param name="pintIdColaborador">Id do Colaborador</param>
        /// <param name="pintIdTipoVisitante">Id do Tipo de Visitante(Funcionário, Terceiro)</param>
        /// <param name="pdtDataFoto">Data da Foto</param>
        /// <returns>True/False</returns>
        /// <history>
        ///     [aoliveira] created 05/02/2013
        /// </history>
        public bool AlteraFotoVisitante(int pintIdColaborador, byte[] pFoto, DateTime pdtDataFoto)
        {
            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_UPD_FOTO_VISITANTE");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            cmdCommand.Parameters.Add(new FWParameter("@Id_Colaborador", FWDbType.Int32));
            cmdCommand.Parameters["@Id_Colaborador"].Value = pintIdColaborador;

            cmdCommand.Parameters.Add(new FWParameter("@Img_ColaboradorFoto", FWDbType.VarBinary));
            cmdCommand.Parameters["@Img_ColaboradorFoto"].Value = pFoto;

            cmdCommand.Parameters.Add(new FWParameter("@Dt_ColaboradorFoto", FWDbType.DateTime));
            cmdCommand.Parameters["@Dt_ColaboradorFoto"].Value = pdtDataFoto;

            //Executa a função no banco de dados 
            bool blnRetorno = false;

            try
            {
                conProvider.ExecuteScalar(cmdCommand);
                blnRetorno = true;
            }
            catch
            {
                throw;
            }

            return blnRetorno;
        }

        #endregion 

        #region Listar Colaborador

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        ///     Lista registros da base em uma Collection 
        /// </summary> 
        /// <param name="pstrNome">Nome do Colaborador</param>
        /// <param name="pstrDocumento">Documento do Colaborador</param>
        /// <param name="pintTipoVisitante">Tipo do Colaborador</param>
        /// <returns>Collection Colaborador</returns> 
        /// <history> 
        ///     [mribeiro] 29/6/2009 Created 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public Collection<SafWeb.Model.Colaborador.Colaborador> ListarColaborador(string pstrNome,
                                                                                  string pstrDocumento,
                                                                                  int pintTipoVisitante,
                                                                                  int pintIgnorarEmFérias,
                                                                                  int startIndex, int pageSize, string sortBy, ref int totalRegistros)
        {
            totalRegistros = 0;
            Collection<SafWeb.Model.Colaborador.Colaborador> colRetorno = null;

            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_COLABORADORES");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Parâmetros de entrada
            if (pstrNome != "")
            {
                cmdCommand.Parameters.Add(new FWParameter("@Nom_Colaborador", FWDbType.Varchar, 100));
                cmdCommand.Parameters["@Nom_Colaborador"].Value = pstrNome;
            }

            if (pstrDocumento != "")
            {
                cmdCommand.Parameters.Add(new FWParameter("@NUMERO", FWDbType.Varchar, 10));
                cmdCommand.Parameters["@NUMERO"].Value = pstrDocumento;
            }

            if (pintTipoVisitante > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Id_TipoColaborador", FWDbType.Int32));
                cmdCommand.Parameters["@Id_TipoColaborador"].Value = pintTipoVisitante;
            }

            if (pintIgnorarEmFérias > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@IgnorarEmFerias", FWDbType.Int32));
                cmdCommand.Parameters["@IgnorarEmFerias"].Value = pintIgnorarEmFérias;
            }

            cmdCommand.Parameters.Add(new FWParameter("@startIndex", FWDbType.Int32));
            cmdCommand.Parameters["@startIndex"].Value = startIndex;

            cmdCommand.Parameters.Add(new FWParameter("@pageSize", FWDbType.Int32));
            cmdCommand.Parameters["@pageSize"].Value = pageSize;

            cmdCommand.Parameters.Add(new FWParameter("@sortBy", FWDbType.Varchar, 100));
            cmdCommand.Parameters["@sortBy"].Value = sortBy;

            //Executa a função no banco de dados 
            System.Data.IDataReader idrRetorno;
            SafWeb.Model.Colaborador.Colaborador objRetorno = null;
            colRetorno = new Collection<SafWeb.Model.Colaborador.Colaborador>();

            idrRetorno = conProvider.ExecuteDataReader(cmdCommand);

            while (idrRetorno.Read())
            {
                objRetorno = new SafWeb.Model.Colaborador.Colaborador();
                objRetorno.FromIDataReader(idrRetorno);
                colRetorno.Add(objRetorno);
                totalRegistros = Convert.ToInt32(idrRetorno["totalRegistros"]);
            }

            return colRetorno;
        }

        #endregion

        #region Listar Colaborador Funcionário e Terceiro

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        ///     Lista Colaboradores do tipo Funcionário e Terceiro 
        /// </summary> 
        /// <param name="pstrNome">Nome do Colaborador</param>
        /// <param name="pstrDocumento">Documento do Colaborador</param>
        /// <param name="pintIdFilial">Id da Filial</param>
        /// <returns>Collection Colaborador</returns> 
        /// <history> 
        ///     [cmarchi] 6/1/2010 Created 
        ///     [cmarchi] 11/2/2010 Modify
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public Collection<SafWeb.Model.Colaborador.Colaborador> ListarColaboradorFuncionarioTerceiro(string pstrNome,
                                                                                  string pstrDocumento, int pintIdFilial)
        {
            Collection<SafWeb.Model.Colaborador.Colaborador> colRetorno = null;

            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_ColaboradorForTerceiro");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Parâmetros de entrada
            if (pstrNome != "")
            {
                cmdCommand.Parameters.Add(new FWParameter("@Nom_Colaborador", FWDbType.Varchar, 100));
                cmdCommand.Parameters["@Nom_Colaborador"].Value = pstrNome;
            }

            if (pstrDocumento != "")
            {
                cmdCommand.Parameters.Add(new FWParameter("@Numero", FWDbType.Varchar, 12));
                cmdCommand.Parameters["@Numero"].Value = pstrDocumento;
            }

            if (pintIdFilial > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Id_Filial", FWDbType.Int32));
                cmdCommand.Parameters["@Id_Filial"].Value = pintIdFilial;
            }

            //Executa a função no banco de dados 
            System.Data.IDataReader idrRetorno;
            SafWeb.Model.Colaborador.Colaborador objRetorno = null;
            colRetorno = new Collection<SafWeb.Model.Colaborador.Colaborador>();

            idrRetorno = conProvider.ExecuteDataReader(cmdCommand);

            while (idrRetorno.Read())
            {
                objRetorno = new SafWeb.Model.Colaborador.Colaborador();
                objRetorno.FromIDataReader(idrRetorno);
                colRetorno.Add(objRetorno);
            }

            return colRetorno;
        }

        /// ----------------------------------------------------------------------------- 
        /// <summary>
        /// Lista os colaboradores do tipo Funcionário e Terceiro buscando somente os registros que aparecerão na página
        /// </summary>
        /// <param name="pstrNome">Nome do Colaborador</param>
        /// <param name="pstrDocumento">Documento</param>
        /// <param name="pintIdFilial">Id da Filial</param>
        /// <param name="startIndex">página a ser exibida</param>
        /// <param name="pageSize">tamanho da página</param>
        /// <param name="sortBy">ordenado por</param>
        /// <param name="totalRegistros">total de registros no banco</param>
        /// <history>
        ///     [aoliveira] created 06/01/2013
        /// </history>
        /// <returns>Lista de Colaboradores</returns>
        /// ----------------------------------------------------------------------------- 
        public Collection<SafWeb.Model.Colaborador.Colaborador> ListarColaboradorFuncionarioTerceiro(string pstrNome,
                                                                                  string pstrDocumento, int pintIdFilial,
                                                                                    int startIndex, int pageSize, string sortBy, ref int totalRegistros)
        {
            totalRegistros = 0;
            Collection<SafWeb.Model.Colaborador.Colaborador> colRetorno = null;

            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_COLABORADORFORTERCEIROOTIMIZADO");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Parâmetros de entrada
            if (pstrNome != "")
            {
                cmdCommand.Parameters.Add(new FWParameter("@Nom_Colaborador", FWDbType.Varchar, 100));
                cmdCommand.Parameters["@Nom_Colaborador"].Value = pstrNome;
            }

            if (pstrDocumento != "")
            {
                cmdCommand.Parameters.Add(new FWParameter("@Numero", FWDbType.Varchar, 12));
                cmdCommand.Parameters["@Numero"].Value = pstrDocumento;
            }

            if (pintIdFilial > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Id_Filial", FWDbType.Int32));
                cmdCommand.Parameters["@Id_Filial"].Value = pintIdFilial;
            }

            cmdCommand.Parameters.Add(new FWParameter("@startIndex", FWDbType.Int32));
            cmdCommand.Parameters["@startIndex"].Value = startIndex;

            cmdCommand.Parameters.Add(new FWParameter("@pageSize", FWDbType.Int32));
            cmdCommand.Parameters["@pageSize"].Value = pageSize;

            cmdCommand.Parameters.Add(new FWParameter("@sortBy", FWDbType.Varchar, 100));
            cmdCommand.Parameters["@sortBy"].Value = sortBy;

            //Executa a função no banco de dados 
            System.Data.IDataReader idrRetorno;
            SafWeb.Model.Colaborador.Colaborador objRetorno = null;
            colRetorno = new Collection<SafWeb.Model.Colaborador.Colaborador>();

            idrRetorno = conProvider.ExecuteDataReader(cmdCommand);

            while (idrRetorno.Read())
            {
                objRetorno = new SafWeb.Model.Colaborador.Colaborador();
                objRetorno.FromIDataReader(idrRetorno);
                colRetorno.Add(objRetorno);
                totalRegistros = Convert.ToInt32(idrRetorno["totalRegistros"]);
            }

            return colRetorno;
        }

        #endregion

        #region Listar Colaborador Visitado

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        ///     Lista registros da base em uma Collection 
        /// </summary> 
        /// <param name="pstrNome">Nome do Visitado</param>
        /// <param name="pstrDocumento">Documento do Visitado</param>
        /// <returns>Collection Colaborador</returns> 
        /// <history> 
        ///     [mribeiro] 06/07/2009 Created 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public Collection<SafWeb.Model.Colaborador.Colaborador> ListarColaboradorVisitado(string pstrNome,
                                                                                          string pstrDocumento,
                                                                                          int startIndex, int pageSize, string sortBy, ref int totalRegistros)
        {
            totalRegistros = 0;
            Collection<SafWeb.Model.Colaborador.Colaborador> colRetorno = null;

            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_COLABORADORES_VISITADOS");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Parâmetros de entrada
            if (pstrNome != "")
            {
                cmdCommand.Parameters.Add(new FWParameter("@Nom_Colaborador", FWDbType.Varchar, 100));
                cmdCommand.Parameters["@Nom_Colaborador"].Value = pstrNome;
            }

            if (pstrDocumento != "")
            {
                cmdCommand.Parameters.Add(new FWParameter("@Num_Documento", FWDbType.Varchar, 10));
                cmdCommand.Parameters["@Num_Documento"].Value = pstrDocumento;
            }

            cmdCommand.Parameters.Add(new FWParameter("@startIndex", FWDbType.Int32));
            cmdCommand.Parameters["@startIndex"].Value = startIndex;

            cmdCommand.Parameters.Add(new FWParameter("@pageSize", FWDbType.Int32));
            cmdCommand.Parameters["@pageSize"].Value = pageSize;

            cmdCommand.Parameters.Add(new FWParameter("@sortBy", FWDbType.Varchar, 100));
            cmdCommand.Parameters["@sortBy"].Value = sortBy;

            //Executa a função no banco de dados 
            System.Data.IDataReader idrRetorno;
            SafWeb.Model.Colaborador.Colaborador objRetorno = null;
            colRetorno = new Collection<SafWeb.Model.Colaborador.Colaborador>();

            idrRetorno = conProvider.ExecuteDataReader(cmdCommand);

            while (idrRetorno.Read())
            {
                objRetorno = new SafWeb.Model.Colaborador.Colaborador();
                objRetorno.FromIDataReader(idrRetorno);
                colRetorno.Add(objRetorno);
                totalRegistros = Convert.ToInt32(idrRetorno["totalRegistros"]);
            }

            return colRetorno;
        }

        #endregion

        #region Listar Tipo Colaborador

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// Lista registros da base em uma Collection 
        /// </summary> 
        /// <returns>Collection TipoColaborador</returns> 
        /// <history> 
        ///     [mribeiro] 30/6/2009 Created 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public Collection<SafWeb.Model.Colaborador.TipoColaborador> ListarTipoColaborador()
        {
            Collection<SafWeb.Model.Colaborador.TipoColaborador> colRetorno = null;

            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_TIPO_COLABORADOR");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Executa a função no banco de dados 
            System.Data.IDataReader idrRetorno;
            SafWeb.Model.Colaborador.TipoColaborador objRetorno = null;
            colRetorno = new Collection<SafWeb.Model.Colaborador.TipoColaborador>();

            idrRetorno = conProvider.ExecuteDataReader(cmdCommand);

            while (idrRetorno.Read())
            {
                objRetorno = new SafWeb.Model.Colaborador.TipoColaborador();
                objRetorno.FromIDataReader(idrRetorno);
                colRetorno.Add(objRetorno);
            }

            return colRetorno;
        }

        #endregion

        #region Listar Tipo Documento

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// Lista registros da base em uma Collection 
        /// </summary> 
        /// <returns>Collection TipoDocumento</returns> 
        /// <history> 
        ///     [mribeiro] 30/6/2009 Created 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public Collection<SafWeb.Model.Colaborador.TipoDocumento> ListarTipoDocumento()
        {
            Collection<SafWeb.Model.Colaborador.TipoDocumento> colRetorno = null;

            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_TIPO_DOCUMENTO");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Executa a função no banco de dados 
            System.Data.IDataReader idrRetorno;
            SafWeb.Model.Colaborador.TipoDocumento objRetorno = null;
            colRetorno = new Collection<SafWeb.Model.Colaborador.TipoDocumento>();

            idrRetorno = conProvider.ExecuteDataReader(cmdCommand);

            while (idrRetorno.Read())
            {
                objRetorno = new SafWeb.Model.Colaborador.TipoDocumento();
                objRetorno.FromIDataReader(idrRetorno);
                colRetorno.Add(objRetorno);
            }

            return colRetorno;
        }

        #endregion

        #region Obter

        /// <summary> 
        ///     Função para popular a model do objeto
        /// </summary> 
        /// <param name="pintIdLista">Id da lista</param> 
        /// <returns> Model Lista </returns> 
        /// <history> 
        ///     [mribeiro] created  01/07/2009
        ///     [aoliveira] modify 05/02/2013 inclusão da Des_Funcao
        /// </history> 
        public SafWeb.Model.Colaborador.Colaborador Obter(int pintIdColaborador)
        {
            // Comando de execucao
            FWCommand cmdCommand = new FWCommand("[SP_SAF_SEL_OBTER_COLABORADOR]");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            // Parametros de entrada 
            cmdCommand.Parameters.Add(new FWParameter("@Id_Colaborador", FWDbType.Int32));
            cmdCommand.Parameters["@Id_Colaborador"].Value = pintIdColaborador;
          
            
            // Execucao
            System.Data.IDataReader idrRetorno;
            SafWeb.Model.Colaborador.Colaborador objRetorno = new SafWeb.Model.Colaborador.Colaborador();
            idrRetorno = conProvider.ExecuteDataReader(cmdCommand);

            if (idrRetorno.Read())
            {
                if (!(idrRetorno["Id_Colaborador"] == DBNull.Value))
                    objRetorno.IdColaborador = Convert.ToInt32(idrRetorno["Id_Colaborador"]);

                //Popula Objeto Lista
                if (!(idrRetorno["Id_TipoColaborador"] == DBNull.Value))
                    objRetorno.IdTipoColaborador = Convert.ToInt32(idrRetorno["Id_TipoColaborador"]);
                if (!(idrRetorno["Cod_Colaborador"] == DBNull.Value))
                    objRetorno.CodigoColaborador = Convert.ToString(idrRetorno["Cod_Colaborador"]);
                if (!(idrRetorno["Nom_Colaborador"] == DBNull.Value))
                    objRetorno.NomeColaborador = Convert.ToString(idrRetorno["Nom_Colaborador"]);
                if (!(idrRetorno["Id_Empresa"] == DBNull.Value))
                    objRetorno.IdEmpresa = Convert.ToInt32(idrRetorno["Id_Empresa"]);
                if (!(idrRetorno["Id_DocumentoTipo"] == DBNull.Value))
                    objRetorno.IdTipoDocumento = Convert.ToInt32(idrRetorno["Id_DocumentoTipo"]);
                if (!(idrRetorno["Num_Documento"] == DBNull.Value))
                    objRetorno.NumeroDocumento = Convert.ToString(idrRetorno["Num_Documento"]);
                if (!(idrRetorno["Des_Funcao"] == DBNull.Value))
                    objRetorno.Des_Funcao = (string)idrRetorno["Des_Funcao"];
                
            }
            return objRetorno;
        }

        /// <summary> 
        ///     Função para popular a model do objeto
        /// </summary>
        /// <param name="pintIdColaborador">Id do Colaborador</param>
        /// <param name="pintIdTipoVisitante">Id do Tipo de Visitante(Funcionário, Terceiro)</param>
        /// <returns>Objeto Colaborador</returns>
        /// <history>
        ///     [aoliveira] created 05/02/2013
        /// </history>
        public SafWeb.Model.Colaborador.Colaborador ObterFoto(int pintIdColaborador, int pintIdTipoVisitante)
        {
            // Comando de execucao
            FWCommand cmdCommand = new FWCommand("[SP_SAF_SEL_OBTER_COLABORADORFOTO]");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            // Parametros de entrada 
            cmdCommand.Parameters.Add(new FWParameter("@Id_Colaborador", FWDbType.Int32));
            cmdCommand.Parameters["@Id_Colaborador"].Value = pintIdColaborador;

            cmdCommand.Parameters.Add(new FWParameter("@Id_TipoVisitante", FWDbType.Int32));
            cmdCommand.Parameters["@Id_TipoVisitante"].Value = pintIdTipoVisitante;

            // Execucao
            System.Data.IDataReader idrRetorno;
            SafWeb.Model.Colaborador.Colaborador objRetorno = new SafWeb.Model.Colaborador.Colaborador();
            idrRetorno = conProvider.ExecuteDataReader(cmdCommand);

            if (idrRetorno.Read())
            {
                if (!(idrRetorno["Id_Colaborador"] == DBNull.Value))
                    objRetorno.IdColaborador = Convert.ToInt32(idrRetorno["Id_Colaborador"]);

                //Popula Objeto Lista
                if (!(idrRetorno["Id_TipoColaborador"] == DBNull.Value))
                    objRetorno.IdTipoColaborador = Convert.ToInt32(idrRetorno["Id_TipoColaborador"]);
                if (!(idrRetorno["Cod_Colaborador"] == DBNull.Value))
                    objRetorno.CodigoColaborador = Convert.ToString(idrRetorno["Cod_Colaborador"]);
                if (!(idrRetorno["Nom_Colaborador"] == DBNull.Value))
                    objRetorno.NomeColaborador = Convert.ToString(idrRetorno["Nom_Colaborador"]);
                if (!(idrRetorno["Id_Empresa"] == DBNull.Value))
                    objRetorno.IdEmpresa = Convert.ToInt32(idrRetorno["Id_Empresa"]);
                if (!(idrRetorno["Id_DocumentoTipo"] == DBNull.Value))
                    objRetorno.IdTipoDocumento = Convert.ToInt32(idrRetorno["Id_DocumentoTipo"]);
                if (!(idrRetorno["Num_Documento"] == DBNull.Value))
                    objRetorno.NumeroDocumento = Convert.ToString(idrRetorno["Num_Documento"]);
                if (!(idrRetorno["fotEmp"] == DBNull.Value))
                    objRetorno.Imagem = (byte[])idrRetorno["fotEmp"];
                if (!(idrRetorno["DatFot"] == DBNull.Value))
                    objRetorno.DataFoto = (DateTime)idrRetorno["DatFot"];
                
            }
            return objRetorno;
        }
        #endregion

        #region Obter Regional e Filial Usuario

        /// <summary> 
        ///     Obtem registro do base
        /// </summary> 
        /// <param name="pintIdUsuario">Id do usuário logado</param> 
        /// <returns> Data Table </returns> 
        /// <history> 
        ///     [mribeiro] created  02/09/2009
        /// </history> 
        public DataTable ObterRegFilUsuario(int pintIdUsuario)
        {
            // Comando de execucao
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_USUARIO");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            // Parametros de entrada 
            cmdCommand.Parameters.Add(new FWParameter("@USU_N_CODIGO", FWDbType.Int32));
            cmdCommand.Parameters["@USU_N_CODIGO"].Value = pintIdUsuario;

            DataTable dttRetorno = new DataTable();

            // Execucao
            try
            {
                dttRetorno = conProvider.ExecuteDataTable(cmdCommand);
            }
            catch (Exception)
            {
                throw;
            }
            

            return dttRetorno;
        }

        #endregion

        #region Obter Login Usuario Portal

        /// <summary> 
        ///     Obtem registro do base
        /// </summary> 
        /// <param name="pstrLogin">Recebe o Login do usuário do portal</param> 
        /// <returns> Data Table </returns> 
        /// <history> 
        ///     [mribeiro] created  14/09/2009
        /// </history> 
        public DataTable ObterLoginUsuario(string pstrLogin)
        {
            // Comando de execucao
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_LOGIN");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            // Parametros de entrada 
            cmdCommand.Parameters.Add(new FWParameter("@Login", FWDbType.Varchar, 20));
            cmdCommand.Parameters["@Login"].Value = pstrLogin;

            DataTable dttRetorno = new DataTable();

            // Execucao
            try
            {
                dttRetorno = conProvider.ExecuteDataTable(cmdCommand);
            }
            catch (Exception)
            {
                throw;
            }


            return dttRetorno;
        }

        #endregion

        #region Obter colaborador atraves da Filial

        /// <summary> 
        ///        Obtem o Colaborador através do Id da filial
        /// </summary> 
        /// <param name="pstrLogin">Recebe o Login do usuário do portal</param> 
        /// <returns> Data Table </returns> 
        /// <history> 
        ///     [icruz] 24/09/2012
        /// </history> 
        public IList<FWCUsuario> BuscaPorFilialColoborador(int intIdFilial,int intIdColaborador)
        {
            // Comando de execucao
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_COLABORADORFILIAL");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            // Parametros de entrada 
            if (intIdFilial > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Id_Filial", FWDbType.Int32));
                cmdCommand.Parameters["@Id_Filial"].Value = intIdFilial;
            }

            if (intIdColaborador > 0)
            {
                // Parametros de entrada 
                cmdCommand.Parameters.Add(new FWParameter("@Id_Colaborador", FWDbType.Int32));
                cmdCommand.Parameters["@Id_Colaborador"].Value = intIdColaborador;
            }

            //Executa a função no banco de dados 
            System.Data.IDataReader idrRetorno;

            try
            {
                var objListRetorno = new List<FWCUsuario>();
                FWCUsuario objTemp;

                idrRetorno = conProvider.ExecuteDataReader(cmdCommand);
                while (idrRetorno.Read())
                {
                    objTemp = new FWCUsuario();
                    objTemp.FromIDataReader(idrRetorno);
                    objListRetorno.Add(objTemp);
                }

                return objListRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Consultar se colaborador está em férias

        /// <summary>
        /// Verifica se um colaborador está/estará em férias em algum momento do período solicitado
        /// </summary>
        /// <param name="CodColaborador">Codigo do colaborador a ser pesquisado</param>
        /// <param name="datInicio">inicio do período a ser pesquisado</param>
        /// <param name="datFim">fim do periodo a ser pesquisado</param>
        /// <returns>verdadeiro ou falso</returns>
        /// <history>
        ///     [aoliveira] created 28/02/2013 10:00
        ///     Verifica se um colaborador está/estará em férias em algum momento do período solicitado
        /// </history>
        public bool ColaboradorEmFeriasLicenca(int CodColaborador, DateTime datInicio, DateTime datFim)
        {
            // Comando de execucao
            //FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_USUARIO");
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_ColaboradorEmFeriasLicenca");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            // Parametros de entrada 
            cmdCommand.Parameters.Add(new FWParameter("@CodColaborador", FWDbType.Int32));
            cmdCommand.Parameters["@CodColaborador"].Value = CodColaborador;

            cmdCommand.Parameters.Add(new FWParameter("@datInicio", FWDbType.DateTime));
            cmdCommand.Parameters["@datInicio"].Value = datInicio;

            cmdCommand.Parameters.Add(new FWParameter("@datFim", FWDbType.DateTime));
            cmdCommand.Parameters["@datFim"].Value = datFim;

            DataTable dttRetorno = new DataTable();

            // Execucao
            try
            {
                dttRetorno = conProvider.ExecuteDataTable(cmdCommand);
            }
            catch (Exception)
            {
                throw;
            }
            if (dttRetorno.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion
    }
}
