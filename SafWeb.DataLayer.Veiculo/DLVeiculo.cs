using System;
using System.Collections.Generic;
using System.Text;
using FrameWork.DataLayer.Utilitarios;
using System.Collections.ObjectModel;

namespace SafWeb.DataLayer.Veiculo
{
    public class DLVeiculo: DALFWBase
    {
        #region Construtor
        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// Constructor 
        /// </summary> 
        /// <history> 
        /// [mribeiro] 10/07/2009 Created 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public DLVeiculo()
        {
            strConnection = DALUtilitarios.GetStringConnection(DALUtilitarios.ConexoesDisponiveis.ConnectionFW);
            conProvider = new DALProvider(strConnection);
            intCommandTimeOut = DALUtilitarios.ObterCommandTimeOut();
        }
        #endregion

        #region Inserir

        /// <summary>
        ///     Insere um novo registro
        /// </summary>
        /// <param name="pobjVeiculo">Objeto Veiculo</param>
        /// <returns>Id do Veículo</returns>
        /// <history>
        ///     [mribeiro] 10/07/2009 created
        /// </history>
        public int Inserir(SafWeb.Model.Veiculo.Veiculo pobjVeiculo)
        {
            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_INS_VEICULO");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            if (pobjVeiculo.IdEmpresa > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Id_Empresa", FWDbType.Int32));
                cmdCommand.Parameters["@Id_Empresa"].Value = pobjVeiculo.IdEmpresa;
            }

            cmdCommand.Parameters.Add(new FWParameter("@Des_Placa", FWDbType.Varchar, 8));
            cmdCommand.Parameters["@Des_Placa"].Value = pobjVeiculo.DescricaoPlaca;

            //cmdCommand.Parameters.Add(new FWParameter("@Des_Prefixo", FWDbType.Varchar, 15));
            //cmdCommand.Parameters["@Des_Prefixo"].Value = pobjVeiculo.DescricaoEmpresa;

            //cmdCommand.Parameters.Add(new FWParameter("@Id_VeiculoModelo", FWDbType.Int32));
            //cmdCommand.Parameters["@Id_VeiculoModelo"].Value = pobjVeiculo.DescricaoEmpresa;

            cmdCommand.Parameters.Add(new FWParameter("@Id_UsuarioInclusao", FWDbType.Int32));
            cmdCommand.Parameters["@Id_UsuarioInclusao"].Value = pobjVeiculo.IdUsuario;

            cmdCommand.Parameters.Add(new FWParameter("@Id_Estado", FWDbType.Int32));
            cmdCommand.Parameters["@Id_Estado"].Value = pobjVeiculo.IdEstado;

            //Executa a função no banco de dados 
            int intRetorno;

            try
            {
                intRetorno = Convert.ToInt32(conProvider.ExecuteScalar(cmdCommand));
            }
            catch (Exception ex)
            {
                throw;
            }

            return intRetorno;
        }

        #endregion

        #region Listar Veiculo

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        ///     Lista registros da base em uma Collection 
        /// </summary> 
        /// <param name="pintEstado">Código do Estado</param>
        /// <returns>Collection Regional</returns> 
        /// <history> 
        ///     [mribeiro] 10/07/2009 Created 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public Collection<SafWeb.Model.Veiculo.Veiculo> ListarVeiculo(int pintEstado)
        {
            Collection<SafWeb.Model.Veiculo.Veiculo> colRetorno = null;

            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_VEICULO");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Parâmetros de entrada
            if (pintEstado > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Id_Estado", FWDbType.Int32));
                cmdCommand.Parameters["@Id_Estado"].Value = pintEstado;
            }

            //Executa a função no banco de dados 
            System.Data.IDataReader idrRetorno;
            SafWeb.Model.Veiculo.Veiculo objRetorno = null;
            colRetorno = new Collection<SafWeb.Model.Veiculo.Veiculo>();

            idrRetorno = conProvider.ExecuteDataReader(cmdCommand);

            while (idrRetorno.Read())
            {
                objRetorno = new SafWeb.Model.Veiculo.Veiculo();
                objRetorno.FromIDataReader(idrRetorno);
                colRetorno.Add(objRetorno);
            }

            return colRetorno;
        }

        #endregion

        #region Listar Estado

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        ///     Lista registros da base em uma Collection 
        /// </summary> 
        /// <returns>Collection Estado</returns> 
        /// <history> 
        ///     [mribeiro] 10/07/2009 Created 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public Collection<SafWeb.Model.Veiculo.Estado> ListarEstado()
        {
            Collection<SafWeb.Model.Veiculo.Estado> colRetorno = null;

            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_ESTADO");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Executa a função no banco de dados 
            System.Data.IDataReader idrRetorno;
            SafWeb.Model.Veiculo.Estado objRetorno = null;
            colRetorno = new Collection<SafWeb.Model.Veiculo.Estado>();

            idrRetorno = conProvider.ExecuteDataReader(cmdCommand);

            while (idrRetorno.Read())
            {
                objRetorno = new SafWeb.Model.Veiculo.Estado();
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
        public SafWeb.Model.Veiculo.Veiculo Obter(SafWeb.Model.Veiculo.Veiculo pobjVeiculo)
        {
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_OBTERVEICULO");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            if (pobjVeiculo.Codigo > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Id_Veiculo", FWDbType.Int32));
                cmdCommand.Parameters["@Id_Veiculo"].Value = pobjVeiculo.Codigo;
    }

            if (!string.IsNullOrEmpty(pobjVeiculo.DescricaoPlaca))
            {
                cmdCommand.Parameters.Add(new FWParameter("@Des_Placa", FWDbType.Varchar));
                cmdCommand.Parameters["@Des_Placa"].Value = pobjVeiculo.DescricaoPlaca;
}
            

            SafWeb.Model.Veiculo.Veiculo objRetorno = new SafWeb.Model.Veiculo.Veiculo();

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
