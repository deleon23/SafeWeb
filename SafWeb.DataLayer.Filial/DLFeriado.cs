using System;
using System.Collections.Generic;
using System.Text;
using FrameWork.DataLayer.Utilitarios;

namespace SafWeb.DataLayer.Filial
{
    public class DLFeriado : DALFWBase
    {
        #region Construtor
        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// Constructor 
        /// </summary> 
        /// <history> 
        /// [cfrancisco] 23/5/2012 Created 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public DLFeriado()
        {
            strConnection = DALUtilitarios.GetStringConnection(DALUtilitarios.ConexoesDisponiveis.ConnectionFW);
            conProvider = new DALProvider(strConnection);
            intCommandTimeOut = DALUtilitarios.ObterCommandTimeOut();
        }

        #endregion

        #region Listar

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// Lista registros da base em uma List - Feriados da filial no periodo
        /// </summary> 
        /// <returns>Lista Feriados</returns> 
        /// <history> 
        ///     [cfrancisco] 22/5/2012 Created 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public List<SafWeb.Model.Filial.Feriado> Listar(int idCidade, int idFilial, DateTime datInicio, DateTime datFinal)
        {
            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_FERIADOS");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Parâmetros de entrada

            if (idCidade > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@COD_CIDADE", FWDbType.Int32));
                cmdCommand.Parameters["@COD_CIDADE"].Value = idCidade;
            }

            if (idFilial > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@ID_FILIAL", FWDbType.Int32));
                cmdCommand.Parameters["@ID_FILIAL"].Value = idFilial;
            }

            cmdCommand.Parameters.Add(new FWParameter("@DATA_INICIO", FWDbType.DateTime));
            cmdCommand.Parameters["@DATA_INICIO"].Value = datInicio;

            cmdCommand.Parameters.Add(new FWParameter("@DATA_FINAL", FWDbType.DateTime));
            cmdCommand.Parameters["@DATA_FINAL"].Value = datFinal;


            SafWeb.Model.Filial.Feriado objRetorno = null;

            var lstRetorno = new List<SafWeb.Model.Filial.Feriado>();

            using (var idrRetorno = conProvider.ExecuteDataReader(cmdCommand))
            {
                while (idrRetorno.Read())
                {
                    objRetorno = new SafWeb.Model.Filial.Feriado();
                    objRetorno.FromIDataReader(idrRetorno);
                    lstRetorno.Add(objRetorno);
                }
                return lstRetorno;
            }
        }

        #endregion
    }
}
