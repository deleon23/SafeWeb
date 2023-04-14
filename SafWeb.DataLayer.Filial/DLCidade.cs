using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using FrameWork.DataLayer.Utilitarios;
using System.Collections.ObjectModel;
using SafWeb.Model.Filial;

namespace SafWeb.DataLayer.Filial
{
    public class DLCidade : DALFWBase
    {
        #region Construtor
        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// Constructor 
        /// </summary> 
        /// <history> 
        /// [haguiar_4] 14/01/2011 Created 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public DLCidade()
        {
            strConnection = DALUtilitarios.GetStringConnection(DALUtilitarios.ConexoesDisponiveis.ConnectionFW);
            conProvider = new DALProvider(strConnection);
            intCommandTimeOut = DALUtilitarios.ObterCommandTimeOut();
        }
        #endregion

        #region Listar Cidade

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        ///     Lista registros da base em uma Collection 
        /// </summary> 
        /// <returns>Collection Cidade</returns> 
        /// <history> 
        ///     [haguiar_4] 14/01/2011 Created 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public Collection<SafWeb.Model.Filial.Cidade> ListarCidade(int pintId_Estado)
        {
            Collection<SafWeb.Model.Filial.Cidade> colRetorno = null;

            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_Cidade");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Executa a função no banco de dados 
            System.Data.IDataReader idrRetorno;
            SafWeb.Model.Filial.Cidade objRetorno = null;
            colRetorno = new Collection<SafWeb.Model.Filial.Cidade>();

            //Parâmetros de entrada
            if (pintId_Estado > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Id_Estado", FWDbType.Int32));
                cmdCommand.Parameters["@Id_Estado"].Value = pintId_Estado;
            }

            idrRetorno = conProvider.ExecuteDataReader(cmdCommand);

            while (idrRetorno.Read())
            {
                objRetorno = new SafWeb.Model.Filial.Cidade();
                objRetorno.FromIDataReader(idrRetorno);
                colRetorno.Add(objRetorno);
            }

            return colRetorno;
        }

        #endregion
        
    }
}