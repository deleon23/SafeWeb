using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using FrameWork.DataLayer.Utilitarios;
using System.Collections.ObjectModel;
using SafWeb.Model.Filial;

namespace SafWeb.DataLayer.Filial
{
    public class DLFusoHorario : DALFWBase
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
        public DLFusoHorario()
        {
            strConnection = DALUtilitarios.GetStringConnection(DALUtilitarios.ConexoesDisponiveis.ConnectionFW);
            conProvider = new DALProvider(strConnection);
            intCommandTimeOut = DALUtilitarios.ObterCommandTimeOut();
        }
        #endregion

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// Lista Fuso Horario
        /// </summary> 
        /// <returns>Collection FusoHorario</returns> 
        /// <history> 
        ///     [haguiar_4] 14/01/2011 Created 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public Collection<SafWeb.Model.Filial.FusoHorario> ListarFusoHorario()
        {
            Collection<SafWeb.Model.Filial.FusoHorario> colRetorno = null;

            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_FusoHorario");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Executa a função no banco de dados 
            System.Data.IDataReader idrRetorno;
            SafWeb.Model.Filial.FusoHorario objRetorno = null;
            colRetorno = new Collection<SafWeb.Model.Filial.FusoHorario>();

            idrRetorno = conProvider.ExecuteDataReader(cmdCommand);

            while (idrRetorno.Read())
            {
                objRetorno = new SafWeb.Model.Filial.FusoHorario();
                objRetorno.FromIDataReader(idrRetorno);
                colRetorno.Add(objRetorno);
            }

            return colRetorno;
        }
        
    }
}