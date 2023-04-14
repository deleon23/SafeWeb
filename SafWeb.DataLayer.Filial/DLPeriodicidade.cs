using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using SafWeb.Model.Escala;
using FrameWork.DataLayer.Utilitarios;

namespace SafWeb.DataLayer.Escala 
{
    /// ----------------------------------------------------------------------------- 
    /// Project : 
    /// Class : DLPeriodicidade
    /// ----------------------------------------------------------------------------- 
    /// <summary>  
    /// Implementação da classe DLPeriodicidade
    /// </summary> 
    /// <history> 
    /// [cmarchi] created 30/12/2009 
    /// </history> 
    /// ----------------------------------------------------------------------------- 
    public class DLPeriodicidade : DALFWBase
    {
        #region Construtor

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// Constructor 
        /// </summary> 
        /// <history> 
        /// [cmarchi] created 30/12/2009 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public DLPeriodicidade()
        {
            strConnection = DALUtilitarios.GetStringConnection(DALUtilitarios.ConexoesDisponiveis.ConnectionFW);
            conProvider = new DALProvider(strConnection);
            intCommandTimeOut = DALUtilitarios.ObterCommandTimeOut();
        }

        #endregion

        #region Listar Periodicidade

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        ///  Lista Periodicidade
        /// </summary> 
        /// <returns>Collection Periodicidade</returns> 
        /// <history> 
        ///     [cmarchi] created 30/12/2009 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public Collection<Periodicidade> Listar()
        {
            Collection<Periodicidade> colRetorno = null;

            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_PERIODICIDADE");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Executa a função no banco de dados 
            System.Data.IDataReader idrRetorno;
            Periodicidade objRetorno = null;
            colRetorno = new Collection<Periodicidade>();

            idrRetorno = conProvider.ExecuteDataReader(cmdCommand);

            while (idrRetorno.Read())
            {
                objRetorno = new Periodicidade();
                objRetorno.FromIDataReader(idrRetorno);
                colRetorno.Add(objRetorno);
            }

            return colRetorno;
        }

        #endregion
    }
}
