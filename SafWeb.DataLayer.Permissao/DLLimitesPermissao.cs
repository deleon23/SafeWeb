using System;
using FrameWork.DataLayer.Utilitarios;
using SafWeb.Model.Permissao;
using System.Collections.Generic;

namespace SafWeb.DataLayer.Permissao
{
    public class DLLimitePermissao : DALFWBase, IDisposable
    {
        #region Construtor
        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// Constructor 
        /// </summary> 
        /// <history> 
        /// [cfrancisco] 17/09/2012 Created 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public DLLimitePermissao()
        {
            strConnection = DALUtilitarios.GetStringConnection(DALUtilitarios.ConexoesDisponiveis.ConnectionFW);
            conProvider = new DALProvider(strConnection);
            intCommandTimeOut = DALUtilitarios.ObterCommandTimeOut();
        }
        #endregion

        #region Insere

        public LimitePermissao InsereLimitePermissao(LimitePermissao objLimitePermissao)
        {
            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_INS_LIMITEPERMISSAO");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Parâmetros de entrada
            cmdCommand.Parameters.Add(new FWParameter("@Limite", FWDbType.Decimal));
            cmdCommand.Parameters["@Limite"].Value = objLimitePermissao.Limite;

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
                        objLimitePermissao.idLimite = objRET.Codigo;
                    else
                        throw new Exception(objRET.Mensagem);

                }

                return objLimitePermissao;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        #endregion

        #region Exclui

        public bool ExcluiLimitePermissao(LimitePermissao objLimitePermissao)
        {
            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_DEL_LIMITEPERMISSAO");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Parâmetros de entrada
            cmdCommand.Parameters.Add(new FWParameter("@Id_Limite", FWDbType.Int32));
            cmdCommand.Parameters["@Id_Limite"].Value = objLimitePermissao.idLimite;

            //Executa a função no banco de dados 
            System.Data.IDataReader idrRetorno;

            try
            {
                idrRetorno = conProvider.ExecuteDataReader(cmdCommand);

                var objRET = new SafWeb.Model.Util.RetornoProc(true);

                if (idrRetorno.Read())
                {

                    objRET.FromIDataReader(idrRetorno);

                    if (!objRET.Erro)
                        objLimitePermissao.idLimite = objRET.Codigo;
                    else
                        throw new Exception(objRET.Mensagem);

                }

                return objRET.Erro;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        #endregion

        #region Edita

        public LimitePermissao EditaLimitePermissao(LimitePermissao objLimitePermissao)
        {
            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_UPD_LIMITEPERMISSAO");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Parâmetros de entrada
            cmdCommand.Parameters.Add(new FWParameter("@Id_Limite", FWDbType.Int32));
            cmdCommand.Parameters["@Id_Limite"].Value = objLimitePermissao.idLimite;

            cmdCommand.Parameters.Add(new FWParameter("@Limite", FWDbType.Double));
            cmdCommand.Parameters["@Limite"].Value = objLimitePermissao.Limite;

            //Executa a função no banco de dados 
            System.Data.IDataReader idrRetorno;

            try
            {
                idrRetorno = conProvider.ExecuteDataReader(cmdCommand);

                if (idrRetorno.Read())
                {
                    var objRET = new SafWeb.Model.Util.RetornoProc();
                    objRET.FromIDataReader(idrRetorno);

                    if (objRET.Erro)
                        throw new Exception(objRET.Mensagem);
                }
                return objLimitePermissao;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        #endregion

        #region Busca

        public IList<LimitePermissao> BuscaLimitePermissao(int idLimite, double dblLimite, SafWeb.Model.Util.Enum.ECriterio Criterio)
        {
            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_LIMITEPERMISSAO");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            if (idLimite > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Id_Limite", FWDbType.Int32));
                cmdCommand.Parameters["@Id_Limite"].Value = idLimite;
            }

            if (dblLimite > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Limite", FWDbType.Double));
                cmdCommand.Parameters["@Limite"].Value = dblLimite;
            }

            if ((int)Criterio > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Criterio", FWDbType.Int32));
                cmdCommand.Parameters["@Criterio"].Value = (int)Criterio;
            }


            //Executa a função no banco de dados 
            System.Data.IDataReader idrRetorno;

            try
            {
                var objListRetorno = new List<LimitePermissao>();
                LimitePermissao objTemp;

                idrRetorno = conProvider.ExecuteDataReader(cmdCommand);
                while (idrRetorno.Read())
                {
                    objTemp = new LimitePermissao();
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

        #region IDisposable
        void IDisposable.Dispose()
        {
            this.Finalizar();
        }
        #endregion
    }
}
