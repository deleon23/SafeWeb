using System;
using System.Collections.Generic;
using System.Text;
using FrameWork.DataLayer.Utilitarios;
using System.Collections.ObjectModel;

namespace SafWeb.DataLayer.Regional
{
    public class DLRegional : DALFWBase
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
        public DLRegional()
        {
            strConnection = DALUtilitarios.GetStringConnection(DALUtilitarios.ConexoesDisponiveis.ConnectionFW);
            conProvider = new DALProvider(strConnection);
            intCommandTimeOut = DALUtilitarios.ObterCommandTimeOut();
        }
        #endregion

        #region Listar

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// Lista registros da base em uma Collection 
        /// </summary> 
        /// <returns>Collection Regional</returns> 
        /// <history> 
        ///     [mribeiro] 22/6/2009 Created 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public Collection<SafWeb.Model.Regional.Regional> Listar()
        {
            Collection<SafWeb.Model.Regional.Regional> colRetorno = null;

            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_REGIONAL");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Executa a função no banco de dados 
            System.Data.IDataReader idrRetorno;
            SafWeb.Model.Regional.Regional objRetorno = null;
            colRetorno = new Collection<SafWeb.Model.Regional.Regional>();

            try
            {
                idrRetorno = conProvider.ExecuteDataReader(cmdCommand);

                while (idrRetorno.Read())
                {
                    objRetorno = new SafWeb.Model.Regional.Regional();
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

    }
}
