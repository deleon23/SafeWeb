using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using FrameWork.DataLayer.Utilitarios;

namespace SafWeb.DataLayer.Empresa
{
    public class DLEmpresa: DALFWBase
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
        public DLEmpresa()
        {
            strConnection = DALUtilitarios.GetStringConnection(DALUtilitarios.ConexoesDisponiveis.ConnectionFW);
            conProvider = new DALProvider(strConnection);
            intCommandTimeOut = DALUtilitarios.ObterCommandTimeOut();
        }
        #endregion

        #region Inserir

        public int Inserir(SafWeb.Model.Empresa.Empresa pobjEmpresa)
        {
            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_INS_EMPRESA");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            cmdCommand.Parameters.Add(new FWParameter("@Des_Empresa", FWDbType.Varchar, 100));
            cmdCommand.Parameters["@Des_Empresa"].Value = pobjEmpresa.DescricaoEmpresa;

            //Executa a função no banco de dados 
            int intIdEmpresa;

            try
            {
                intIdEmpresa = Convert.ToInt32(conProvider.ExecuteScalar(cmdCommand));
            }
            catch (Exception ex)
            {
                throw;
            }

            return intIdEmpresa;
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
        public Collection<SafWeb.Model.Empresa.Empresa> Listar()
        {
            Collection<SafWeb.Model.Empresa.Empresa> colRetorno = null;

            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_EMPRESA");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Executa a função no banco de dados 
            System.Data.IDataReader idrRetorno;
            SafWeb.Model.Empresa.Empresa objRetorno = null;
            colRetorno = new Collection<SafWeb.Model.Empresa.Empresa>();

            idrRetorno = conProvider.ExecuteDataReader(cmdCommand);

            while (idrRetorno.Read())
            {
                objRetorno = new SafWeb.Model.Empresa.Empresa();
                objRetorno.FromIDataReader(idrRetorno);
                colRetorno.Add(objRetorno);
            }

            return colRetorno;
        }

        #endregion

        #region Obter

        /// <summary> 
        ///     Função para popular a model do objeto
        /// </summary> 
        /// <param name="pintIdLista">Id da lista</param> 
        /// <returns> Model Lista </returns> 
        /// <history> 
        ///      [cfrancisco] created 24/04/2012
        /// </history> 
        public SafWeb.Model.Empresa.Empresa Obter(SafWeb.Model.Empresa.Empresa pobjEmpresa)
        {
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_OBTEREMPRESA");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            if (string.IsNullOrEmpty(pobjEmpresa.CodigoEmpresa))
            {
                cmdCommand.Parameters.Add(new FWParameter("@ID_EMPRESA", FWDbType.Int32));
                cmdCommand.Parameters["@ID_EMPRESA"].Value = Convert.ToInt32(pobjEmpresa.CodigoEmpresa);
    }

            if (string.IsNullOrEmpty(pobjEmpresa.DescricaoEmpresa))
            {
                cmdCommand.Parameters.Add(new FWParameter("@Des_Empresa", FWDbType.Varchar, 100));
                cmdCommand.Parameters["@Des_Empresa"].Value = pobjEmpresa.DescricaoEmpresa;
}

            SafWeb.Model.Empresa.Empresa objRetorno = new SafWeb.Model.Empresa.Empresa();

            try
            {
                using (System.Data.IDataReader idrRetorno = conProvider.ExecuteDataReader(cmdCommand))
                {
                    if (idrRetorno.Read())
                        objRetorno.FromIDataReader(idrRetorno);
                }

            }
            catch (Exception)
            {
                throw;
            }

            return objRetorno;
        }
        #endregion
    }
}
