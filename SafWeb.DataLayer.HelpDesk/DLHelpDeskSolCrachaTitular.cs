using System;
using FrameWork.DataLayer.Utilitarios;
using SafWeb.Model.HelpDesk;
using System.Collections.Generic;

namespace SafWeb.DataLayer.HelpDesk
{
    public class DLHelpDeskSolCrachaTitular : DALFWBase, IDisposable
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
        public DLHelpDeskSolCrachaTitular()
        {
            strConnection = DALUtilitarios.GetStringConnection(DALUtilitarios.ConexoesDisponiveis.ConnectionFW);
            conProvider = new DALProvider(strConnection);
            intCommandTimeOut = DALUtilitarios.ObterCommandTimeOut();
        }
        #endregion


        #region Listar

        public IList<HelpDeskSolCrachaTitular> ListarCrachaTitularPendentes(int pintUsuario, int piniIdSolicitacao, int pintIdRegional, int pintIdFilial, string pstrNomeColaborador, int pintIdTipoSolicitacao)
        {
            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_HELPDESKSOLCRACHATITULAR");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            cmdCommand.Parameters.Add(new FWParameter("@USU_N_CODIGO_LOGADO", FWDbType.Int32));
            cmdCommand.Parameters["@USU_N_CODIGO_LOGADO"].Value = pintUsuario;


            if (piniIdSolicitacao > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Id_SolicitacaoCrachaTitular", FWDbType.Int32));
                cmdCommand.Parameters["@Id_SolicitacaoCrachaTitular"].Value = piniIdSolicitacao;
            }


            if (pintIdRegional > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Id_Regional", FWDbType.Int32));
                cmdCommand.Parameters["@Id_Regional"].Value = pintIdRegional;
            }

            if (pintIdFilial > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Id_Filial", FWDbType.Int32));
                cmdCommand.Parameters["@Id_Filial"].Value = pintIdFilial;
            }

            if (!string.IsNullOrEmpty(pstrNomeColaborador))
            {
                cmdCommand.Parameters.Add(new FWParameter("@Nom_Colaborador", FWDbType.Varchar));
                cmdCommand.Parameters["@Nom_Colaborador"].Value = pstrNomeColaborador;
            }

            if (pintIdTipoSolicitacao > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Id_TipoSolicitacao", FWDbType.Int32));
                cmdCommand.Parameters["@Id_TipoSolicitacao"].Value = pintIdTipoSolicitacao;
            }


            //Executa a função no banco de dados 
            System.Data.IDataReader idrRetorno;

            try
            {
                var objListRetorno = new List<HelpDeskSolCrachaTitular>();
                HelpDeskSolCrachaTitular objTemp;

                idrRetorno = conProvider.ExecuteDataReader(cmdCommand);
                while (idrRetorno.Read())
                {
                    objTemp = new HelpDeskSolCrachaTitular();
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



        public bool AdicionarPermissaoCrachaTitularColaborador(int pintUsuario, int idSolicitacaoCrachaTitular, int idPermissao)
        {

            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_INS_PERMCRACHATITULARCOLABRONDA");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Parâmetros de entrada
            cmdCommand.Parameters.Add(new FWParameter("@Id_SolicitacaoCrachaTitular", FWDbType.Int32));
            cmdCommand.Parameters["@Id_SolicitacaoCrachaTitular"].Value = idSolicitacaoCrachaTitular;

            cmdCommand.Parameters.Add(new FWParameter("@ID_PERMISSAO", FWDbType.Int32));
            cmdCommand.Parameters["@ID_PERMISSAO"].Value = idPermissao;

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

        #region IDisposable
        void IDisposable.Dispose()
        {
            this.Finalizar();
        }
        #endregion
    }
}
