using System;
using FrameWork.DataLayer.Utilitarios;
using SafWeb.Model.Permissao;
using System.Collections.Generic;

namespace SafWeb.DataLayer.Permissao
{
    public class DLPermissaoRonda : DALFWBase, IDisposable
    {
        #region Construtor
        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// Constructor 
        /// </summary> 
        /// <history> 
        /// [icruz] 28/09/2012 Created 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public DLPermissaoRonda()
        {
            strConnection = DALUtilitarios.GetStringConnection(DALUtilitarios.ConexoesDisponiveis.ConnectionFW);
            conProvider = new DALProvider(strConnection);
            intCommandTimeOut = DALUtilitarios.ObterCommandTimeOut();
        }
        #endregion


        #region BuscaPermissao

        public IList<PermissaoRonda> BuscaPermissaoRonda(int idPermissao)
        {
            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_PERMISSOESRONDA");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;


            if (idPermissao > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@id_Permissao", FWDbType.Int32));
                cmdCommand.Parameters["@id_Permissao"].Value = idPermissao;
            }

            //Executa a função no banco de dados 
            System.Data.IDataReader idrRetorno;

            try
            {
                var objListRetorno = new List<PermissaoRonda>();
                PermissaoRonda objTemp;

                idrRetorno = conProvider.ExecuteDataReader(cmdCommand);
                while (idrRetorno.Read())
                {
                    objTemp = new PermissaoRonda();
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

        public IList<PermissaoRonda> BuscaPermissaoNaoUtilizadaRonda(int idPermissao)
        {
            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_PERMISSAONAOUTILIZADA");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;


            if (idPermissao > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@id_Permissao", FWDbType.Int32));
                cmdCommand.Parameters["@id_Permissao"].Value = idPermissao;
            }

            //Executa a função no banco de dados 
            System.Data.IDataReader idrRetorno;

            try
            {
                var objListRetorno = new List<PermissaoRonda>();
                PermissaoRonda objTemp;

                idrRetorno = conProvider.ExecuteDataReader(cmdCommand);
                while (idrRetorno.Read())
                {
                    objTemp = new PermissaoRonda();
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

        #region Insere Permissao

        public PermissaoRonda InserePermissaoRONDA(string strDesPermissao, string strIdGrupoColetores)
        {
            PermissaoRonda objPermissaoRonda = new PermissaoRonda();
            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_INS_CRIAPERMISSAOGRPCOLETORES");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Parâmetros de entrada
            cmdCommand.Parameters.Add(new FWParameter("@DES_PERMISSAO", FWDbType.Varchar));
            cmdCommand.Parameters["@DES_PERMISSAO"].Value = strDesPermissao;

            cmdCommand.Parameters.Add(new FWParameter("@ID_GRUPOSCOLETORES", FWDbType.Varchar));
            cmdCommand.Parameters["@ID_GRUPOSCOLETORES"].Value = strIdGrupoColetores;

            //Executa a função no banco de dados 
            System.Data.IDataReader idrRetorno;

            try
            {
                idrRetorno = conProvider.ExecuteDataReader(cmdCommand);

                if (idrRetorno.Read())
                {
                    var objRET = new SafWeb.Model.Util.RetornoProc();
                    objRET.FromIDataReader(idrRetorno);

                    if (!objRET.Erro)
                        objPermissaoRonda.idPermissao = objRET.Codigo;
                    else
                        throw new Exception(objRET.Mensagem);

                }

                return objPermissaoRonda;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        #endregion



        #region Edita Permissao

        public PermissaoRonda EditaPermissaoRONDA(int idPermissao, string strDesPermissao, string strIdGrupoColetores)
        {
            PermissaoRonda objPermissaoRonda = new PermissaoRonda();
            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_UPD_PERMISSAOGRPCOLETORES");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Parâmetros de entrada
            cmdCommand.Parameters.Add(new FWParameter("@ID_PERMISSAO", FWDbType.Int32));
            cmdCommand.Parameters["@ID_PERMISSAO"].Value = idPermissao;

            cmdCommand.Parameters.Add(new FWParameter("@DES_PERMISSAO", FWDbType.Varchar));
            cmdCommand.Parameters["@DES_PERMISSAO"].Value = strDesPermissao;

            cmdCommand.Parameters.Add(new FWParameter("@ID_GRUPOSCOLETORES", FWDbType.Varchar));
            cmdCommand.Parameters["@ID_GRUPOSCOLETORES"].Value = strIdGrupoColetores;

            //Executa a função no banco de dados 
            System.Data.IDataReader idrRetorno;

            try
            {
                idrRetorno = conProvider.ExecuteDataReader(cmdCommand);

                if (idrRetorno.Read())
                {
                    var objRET = new SafWeb.Model.Util.RetornoProc();
                    objRET.FromIDataReader(idrRetorno);

                    if (!objRET.Erro)
                        objPermissaoRonda.idPermissao = objRET.Codigo;
                    else
                        throw new Exception(objRET.Mensagem);

                }

                return objPermissaoRonda;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        #endregion

        #region IDisposable
        void IDisposable.Dispose()
        {
            this.Finalizar();
        }
        #endregion
    }
}
