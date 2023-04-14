using System;
using FrameWork.DataLayer.Utilitarios;
using SafWeb.Model.Permissao;
using System.Collections.Generic;

namespace SafWeb.DataLayer.Permissao
{
    public class DLLimitePrmColaborador : DALFWBase, IDisposable
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
        public DLLimitePrmColaborador()
        {
            strConnection = DALUtilitarios.GetStringConnection(DALUtilitarios.ConexoesDisponiveis.ConnectionFW);
            conProvider = new DALProvider(strConnection);
            intCommandTimeOut = DALUtilitarios.ObterCommandTimeOut();
        }
        #endregion

        #region Insere

        public LimitePrmColaborador InsereLimitePrmColaborador(LimitePrmColaborador objLimitePrmColaborador)
        {
            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_INS_LIMITEPRMCOLABORADOR");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Parâmetros de entrada
            cmdCommand.Parameters.Add(new FWParameter("@Id_Colaborador", FWDbType.Int32));
            cmdCommand.Parameters["@Id_Colaborador"].Value = objLimitePrmColaborador.idColaborador;

            cmdCommand.Parameters.Add(new FWParameter("@Id_Limite", FWDbType.Int32));
            cmdCommand.Parameters["@Id_Limite"].Value = objLimitePrmColaborador.idLimite;

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
                        objLimitePrmColaborador.idLimiteColaborador = objRET.Codigo;
                    else
                        throw new Exception(objRET.Mensagem);

                }

                return objLimitePrmColaborador;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        #endregion

        #region Exclui

        public bool ExcluiLimitePrmColaborador(LimitePrmColaborador objLimitePrmColaborador)
        {
            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_DEL_LIMITEPRMCOLABORADOR");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Parâmetros de entrada
            cmdCommand.Parameters.Add(new FWParameter("@Id_LimiteColaborador", FWDbType.Int32));
            cmdCommand.Parameters["@Id_LimiteColaborador"].Value = objLimitePrmColaborador.idLimiteColaborador;

            //Executa a função no banco de dados 
            System.Data.IDataReader idrRetorno;

            try
            {
                idrRetorno = conProvider.ExecuteDataReader(cmdCommand);

                var objRET = new SafWeb.Model.Util.RetornoProc(true);

                if (idrRetorno.Read())
                {

                    objRET.FromIDataReader(idrRetorno);

                    if (objRET.Erro)
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

        public LimitePrmColaborador EditaLimitePrmColaborador(LimitePrmColaborador objLimitePrmColaborador)
        {
            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_UPD_LIMITEPRMCOLABORADOR");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Parâmetros de entrada
            cmdCommand.Parameters.Add(new FWParameter("@Id_LimiteColaborador", FWDbType.Int32));
            cmdCommand.Parameters["@Id_LimiteColaborador"].Value = objLimitePrmColaborador.idLimiteColaborador;

            cmdCommand.Parameters.Add(new FWParameter("@Id_Limite", FWDbType.Int32));
            cmdCommand.Parameters["@Id_Limite"].Value = objLimitePrmColaborador.idLimite;

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
                return objLimitePrmColaborador;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        #endregion

        #region Busca

        public IList<LimitePrmColaborador> BuscaLimitePrmColaborador(int idLimiteColaborador, string nomeColaborador, decimal Limite, SafWeb.Model.Util.Enum.ECriterio Criterio)
        {
            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_LIMITEPRMCOLABORADOR");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;


            if (idLimiteColaborador > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Id_LimiteColaborador", FWDbType.Int32));
                cmdCommand.Parameters["@Id_LimiteColaborador"].Value = idLimiteColaborador;
            }


            if (nomeColaborador != string.Empty)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Nome_Colaborador", FWDbType.Varchar));
                cmdCommand.Parameters["@Nome_Colaborador"].Value = nomeColaborador;
            }

            if (Limite > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Limite", FWDbType.Int32));
                cmdCommand.Parameters["@Limite"].Value = Limite;
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
                var objListRetorno = new List<LimitePrmColaborador>();
                LimitePrmColaborador objTemp;

                idrRetorno = conProvider.ExecuteDataReader(cmdCommand);
                while (idrRetorno.Read())
                {
                    objTemp = new LimitePrmColaborador();
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
