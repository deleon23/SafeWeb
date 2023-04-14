using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using FrameWork.DataLayer.Utilitarios;
using System.Data;

namespace SafWeb.DataLayer.Lista
{
    public class DLLista: DALFWBase
    {
        #region Construtor
        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// Constructor 
        /// </summary> 
        /// <history> 
        /// [mribeiro] 23/6/2009 Created 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public DLLista()
        {
            strConnection = DALUtilitarios.GetStringConnection(DALUtilitarios.ConexoesDisponiveis.ConnectionFW);
            conProvider = new DALProvider(strConnection);
            intCommandTimeOut = DALUtilitarios.ObterCommandTimeOut();
        }
        #endregion

        #region Inserir

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        ///     Insere registros da base em uma Collection 
        /// </summary> 
        /// <returns>Collection Regional</returns> 
        /// <history> 
        ///     [mribeiro] 22/6/2009 Created 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public int Inserir(SafWeb.Model.Lista.Lista pobjLista)
        {

            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_INS_LISTA");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            cmdCommand.Parameters.Add(new FWParameter("@Des_ColaboradorLista", FWDbType.Varchar, 50));
            cmdCommand.Parameters["@Des_ColaboradorLista"].Value = pobjLista.DescricaoLista;

            cmdCommand.Parameters.Add(new FWParameter("@Id_Filial", FWDbType.Int32));
            cmdCommand.Parameters["@Id_Filial"].Value = pobjLista.IdFilial;

            cmdCommand.Parameters.Add(new FWParameter("@Id_UsuarioInclusao", FWDbType.Int32));
            cmdCommand.Parameters["@Id_UsuarioInclusao"].Value = pobjLista.IdUsuario;

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

        #region Inserir Colaborador

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        ///     Insere registros da base em uma Collection 
        /// </summary> 
        /// <returns>Collection Regional</returns> 
        /// <history> 
        ///     [mribeiro] 22/6/2009 Created 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public bool InserirColaboradorLista(int pintIdLista, 
                                            int pintColaborador,
                                            int pintUsuario,
                                            bool situacao)
        {

            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("[SP_SAF_INS_COLABORADOR_DA_LISTA]");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            cmdCommand.Parameters.Add(new FWParameter("@Id_ColaboradorLista", FWDbType.Int32));
            cmdCommand.Parameters["@Id_ColaboradorLista"].Value = pintIdLista;

            cmdCommand.Parameters.Add(new FWParameter("@Id_Colaborador", FWDbType.Int32));
            cmdCommand.Parameters["@Id_Colaborador"].Value = pintColaborador;

            cmdCommand.Parameters.Add(new FWParameter("@Id_UsuarioInclusao", FWDbType.Int32));
            cmdCommand.Parameters["@Id_UsuarioInclusao"].Value = pintUsuario;

            cmdCommand.Parameters.Add(new FWParameter("@Flg_Situacao", FWDbType.Boolean));
            cmdCommand.Parameters["@Flg_Situacao"].Value = situacao;


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

        #region Alterar Situacao

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        ///     Altera registros da base em uma Collection 
        /// </summary> 
        /// <returns>Collection Lista</returns> 
        /// <history> 
        ///     [mribeiro] 14/07/2009 Created 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public int AlterarSituacao(SafWeb.Model.Lista.Lista pobjLista)
        {

            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_UPD_LISTA");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            cmdCommand.Parameters.Add(new FWParameter("@Id_ColaboradorLista", FWDbType.Int32));
            cmdCommand.Parameters["@Id_ColaboradorLista"].Value = pobjLista.IdLista;

            cmdCommand.Parameters.Add(new FWParameter("@Flg_Situacao", FWDbType.Byte));
            cmdCommand.Parameters["@Flg_Situacao"].Value = pobjLista.Situacao;
       
            cmdCommand.Parameters.Add(new FWParameter("@Id_UsuarioAlteracao", FWDbType.Int32));
            cmdCommand.Parameters["@Id_UsuarioAlteracao"].Value = pobjLista.IdUsuario;

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

        #region Alterar Situacao Colaborador da lista

        public int AlterarSituacaoColaboradorDaLista(SafWeb.Model.Colaborador.Colaborador pobjColaborador, int IdLista, int IdUsuario)
        {
            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_UPD_COLABORADORDALISTA");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            cmdCommand.Parameters.Add(new FWParameter("@Id_Colaborador", FWDbType.Int32));
            cmdCommand.Parameters["@Id_Colaborador"].Value = pobjColaborador.IdColaborador;

            cmdCommand.Parameters.Add(new FWParameter("@Id_ColaboradorLista", FWDbType.Int32));
            cmdCommand.Parameters["@Id_ColaboradorLista"].Value = IdLista;

            cmdCommand.Parameters.Add(new FWParameter("@Flg_Situacao", FWDbType.Byte));
            cmdCommand.Parameters["@Flg_Situacao"].Value = pobjColaborador.Situacao;

            cmdCommand.Parameters.Add(new FWParameter("@Id_UsuarioAlteracao", FWDbType.Int32));
            cmdCommand.Parameters["@Id_UsuarioAlteracao"].Value =IdUsuario;

            //Executa a função no banco de dados 
            int intRetorno = 0;

            try
            {

                using (System.Data.IDataReader idrRetorno = conProvider.ExecuteDataReader(cmdCommand))
                {
                    if (idrRetorno.Read())
                    {
                        if (idrRetorno["status"].ToString().Equals("1"))
                            intRetorno = Convert.ToInt32(idrRetorno["retorno"]);
                        else
                            throw new Exception(idrRetorno["descricao"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return intRetorno;
        }

        #endregion

        #region Alterar

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        ///     Altera registros da base em uma Collection 
        /// </summary> 
        /// <returns>Collection Lista</returns> 
        /// <history> 
        ///     [mribeiro] 01/07/2009 Created 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public int Alterar(SafWeb.Model.Lista.Lista pobjLista)
        {

            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_UPD_LISTA");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            cmdCommand.Parameters.Add(new FWParameter("@Id_ColaboradorLista", FWDbType.Int32));
            cmdCommand.Parameters["@Id_ColaboradorLista"].Value = pobjLista.IdLista;

            if ( pobjLista.DescricaoLista != null)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Des_ColaboradorLista", FWDbType.Varchar, 50));
                cmdCommand.Parameters["@Des_ColaboradorLista"].Value = pobjLista.DescricaoLista;
            }

            if (pobjLista.IdFilial > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Id_Filial", FWDbType.Int32));
                cmdCommand.Parameters["@Id_Filial"].Value = pobjLista.IdFilial;
            }

            cmdCommand.Parameters.Add(new FWParameter("@Id_UsuarioAlteracao", FWDbType.Int32));
            cmdCommand.Parameters["@Id_UsuarioAlteracao"].Value = pobjLista.IdUsuario;

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

        #region Excluir Colaboradores da Lista

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        ///     Exclui registros da base
        /// </summary> 
        /// <param name="pintIdLista">Código da Lista</param>
        /// <returns>True/False</returns> 
        /// <history> 
        ///     [mribeiro] 02/07/2009 Created 
        /// </history> 
        public bool ExcluirColaboradoresLista(int pintIdLista)
        {

            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("[SP_SAF_DEL_COLABORADORES_LISTA]");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

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

        #region Listar Lista

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        ///  Lista registros da base em uma Collection 
        /// </summary> 
        /// <returns>Collection Lista</returns> 
        /// <history> 
        ///     [mribeiro] 22/6/2009 Created 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public Collection<SafWeb.Model.Lista.Lista> ListarLista(SafWeb.Model.Lista.Lista pobjLista)
        {
            Collection<SafWeb.Model.Lista.Lista> colRetorno = null;

            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_LISTA");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Parâmetros de entrada
            if (pobjLista.IdRegional > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Id_Regional", FWDbType.Int32));
                cmdCommand.Parameters["@Id_Regional"].Value = pobjLista.IdRegional;
            }

            if (pobjLista.IdFilial > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Id_Filial", FWDbType.Int32));
                cmdCommand.Parameters["@Id_Filial"].Value = pobjLista.IdFilial;
            }

            if (pobjLista.IdLista > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Id_ColaboradorLista", FWDbType.Int32));
                cmdCommand.Parameters["@Id_ColaboradorLista"].Value = pobjLista.IdLista;
            }

            if (pobjLista.DescricaoLista != string.Empty)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Des_ColaboradorLista", FWDbType.Varchar, 50));
                cmdCommand.Parameters["@Des_ColaboradorLista"].Value = pobjLista.DescricaoLista;
            }

            try
            {
                //Executa a função no banco de dados 
                System.Data.IDataReader idrRetorno;
                SafWeb.Model.Lista.Lista objRetorno = null;
                colRetorno = new Collection<SafWeb.Model.Lista.Lista>();

                idrRetorno = conProvider.ExecuteDataReader(cmdCommand);

                while (idrRetorno.Read())
                {
                    objRetorno = new SafWeb.Model.Lista.Lista();
                    objRetorno.FromIDataReader(idrRetorno);
                    colRetorno.Add(objRetorno);
                }

            }
            catch (Exception)
            {                
                throw;
            }
            
            return colRetorno;
        }

        #endregion

        #region Listar Listas Ativas

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        ///  Lista registros da base em uma Collection 
        /// </summary> 
        /// <param name="pintRegional">Id da Regional</param>
        /// <param name="pintFilial">Id da Filial</param>
        /// <returns>Collection Lista</returns> 
        /// <history> 
        ///     [mribeiro] 14/07/2009 Created 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public Collection<SafWeb.Model.Lista.Lista> ListarListasAtivas(int pintRegional,
                                                                       int pintFilial)
        {
            Collection<SafWeb.Model.Lista.Lista> colRetorno = null;

            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_LISTA");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Parâmetros de entrada
            if (pintRegional > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Id_Regional", FWDbType.Int32));
                cmdCommand.Parameters["@Id_Regional"].Value = pintRegional;
            }

            if (pintFilial > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Id_Filial", FWDbType.Int32));
                cmdCommand.Parameters["@Id_Filial"].Value = pintFilial;
            }

            cmdCommand.Parameters.Add(new FWParameter("@Flg_Situacao", FWDbType.Byte));
            cmdCommand.Parameters["@Flg_Situacao"].Value = 1;

            try
            {
                //Executa a função no banco de dados 
                System.Data.IDataReader idrRetorno;
                SafWeb.Model.Lista.Lista objRetorno = null;
                colRetorno = new Collection<SafWeb.Model.Lista.Lista>();

                idrRetorno = conProvider.ExecuteDataReader(cmdCommand);

                while (idrRetorno.Read())
                {
                    objRetorno = new SafWeb.Model.Lista.Lista();
                    objRetorno.FromIDataReader(idrRetorno);
                    colRetorno.Add(objRetorno);
                }

            }
            catch (Exception)
            {    
                throw;
            }

            return colRetorno;
        }

        #endregion

        #region Listar Colaboradores da Lista

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        ///     Lista registros da base em uma Collection 
        /// </summary> 
        /// <returns>Collection Regional</returns> 
        /// <history> 
        ///     [mribeiro] 01/07/2009 Created 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public Collection<SafWeb.Model.Colaborador.Colaborador> ListarColaboradoresDaLista(int pintIdLista)
        {
            Collection<SafWeb.Model.Colaborador.Colaborador> colRetorno = null;

            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("[SP_SAF_SEL_COLABORADOR_DA_LISTA]");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Parâmetro de entrada
            cmdCommand.Parameters.Add(new FWParameter("@Id_ColaboradorLista", FWDbType.Int32));
            cmdCommand.Parameters["@Id_ColaboradorLista"].Value = pintIdLista;

            try
            {
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
            }
            catch (Exception)
            {    
                throw;
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
        /// </history> 
        public SafWeb.Model.Lista.Lista Obter(int pintIdLista)
        {
            // Comando de execucao
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_OBTER_COLABORADOR_LISTA");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            // Parametros de entrada 
            cmdCommand.Parameters.Add(new FWParameter("@Id_ColaboradorLista", FWDbType.Int32));
            cmdCommand.Parameters["@Id_ColaboradorLista"].Value = pintIdLista;

            // Parametros de retorno 
            cmdCommand.Parameters.Add(new FWParameter("@OUT_Id_ColaboradorLista", FWDbType.Int32));
            cmdCommand.Parameters["@OUT_Id_ColaboradorLista"].Direction = ParameterDirection.Output;

            cmdCommand.Parameters.Add(new FWParameter("@OUT_Des_ColaboradorLista", FWDbType.Varchar, 50));
            cmdCommand.Parameters["@OUT_Des_ColaboradorLista"].Direction = ParameterDirection.Output;

            cmdCommand.Parameters.Add(new FWParameter("@OUT_Id_Regional", FWDbType.Int32));
            cmdCommand.Parameters["@OUT_Id_Regional"].Direction = ParameterDirection.Output;

            cmdCommand.Parameters.Add(new FWParameter("@OUT_Id_Filial", FWDbType.Int32));
            cmdCommand.Parameters["@OUT_Id_Filial"].Direction = ParameterDirection.Output;

            // Execucao
            SafWeb.Model.Lista.Lista objRetorno = new SafWeb.Model.Lista.Lista();

            try
            {
                conProvider.ExecuteScalar(cmdCommand);

                if (!(cmdCommand.Parameters["@OUT_Id_ColaboradorLista"].Value == DBNull.Value))
                    objRetorno.IdLista = Convert.ToInt32(cmdCommand.Parameters["@OUT_Id_ColaboradorLista"].Value);

                //Popula Objeto Lista
                if (!(cmdCommand.Parameters["@OUT_Des_ColaboradorLista"].Value == DBNull.Value))
                    objRetorno.DescricaoFilial = Convert.ToString(cmdCommand.Parameters["@OUT_Des_ColaboradorLista"].Value);
                if (!(cmdCommand.Parameters["@OUT_Id_Regional"].Value == DBNull.Value))
                    objRetorno.IdRegional = Convert.ToInt32(cmdCommand.Parameters["@OUT_Id_Regional"].Value);
                if (!(cmdCommand.Parameters["@OUT_Id_Filial"].Value == DBNull.Value))
                    objRetorno.IdFilial = Convert.ToInt32(cmdCommand.Parameters["@OUT_Id_Filial"].Value);

            }
            catch (Exception)
            {
                throw;
            }            

            return objRetorno;
        }
        #endregion

        #region Verificar Colaborador LIsta

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        ///     Obtem registros da base 
        /// </summary> 
        /// <param name="pstrdLista">String com as listas inseridas</param>
        /// <param name="pintIdColaborador">Id do Colaborador</param>
        /// <returns>True/False</returns> 
        /// <history> 
        ///     [mribeiro] 30/07/2009 Created 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public bool VerificarColaboradorLista(string pstrdLista,
                                              int pintIdColaborador)
        {

            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_VERIFICAR_COLABORADOR_LISTA");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            cmdCommand.Parameters.Add(new FWParameter("@Id_ColaboradorLista", FWDbType.Varchar));
            cmdCommand.Parameters["@Id_ColaboradorLista"].Value = pstrdLista;

            cmdCommand.Parameters.Add(new FWParameter("@Id_Colaborador", FWDbType.Int32));
            cmdCommand.Parameters["@Id_Colaborador"].Value = pintIdColaborador;
            
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
    }
}
