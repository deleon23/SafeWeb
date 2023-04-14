using System;
using FrameWork.DataLayer.Utilitarios;
using SafWeb.Model.Permissao;
using System.Collections.Generic;

namespace SafWeb.DataLayer.Permissao
{
    public class DLGrupoColetoresRonda : DALFWBase, IDisposable
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
        public DLGrupoColetoresRonda()
        {
            strConnection = DALUtilitarios.GetStringConnection(DALUtilitarios.ConexoesDisponiveis.ConnectionFW);
            conProvider = new DALProvider(strConnection);
            intCommandTimeOut = DALUtilitarios.ObterCommandTimeOut();
        }
        #endregion


        #region BuscaPermissao

        public List<GrupoColetoresRonda> BuscaGrupoColaboradoresRonda(int idPermissao)
        {
            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_GRUPOSCOLETORESRONDA");
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
                var objListRetorno = new List<GrupoColetoresRonda>();
                GrupoColetoresRonda objTemp;

                idrRetorno = conProvider.ExecuteDataReader(cmdCommand);
                while (idrRetorno.Read())
                {
                    objTemp = new GrupoColetoresRonda();
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
