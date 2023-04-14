using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using FrameWork.DataLayer.Utilitarios;

namespace SafWeb.DataLayer.ListaVeiculos
{
    public class DLListaVeiculos : DALFWBase
    {
        #region Construtor
        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// Constructor 
        /// </summary> 
        /// <history> 
        /// [mribeiro] 23/6/2009 Created 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public DLListaVeiculos()
        {
            strConnection = DALUtilitarios.GetStringConnection(DALUtilitarios.ConexoesDisponiveis.ConnectionFW);
            conProvider = new DALProvider(strConnection);
            intCommandTimeOut = DALUtilitarios.ObterCommandTimeOut();
        }
        #endregion

        #region Inserir

        public int Inserir(SafWeb.Model.ListaVeiculos.ListaVeiculos pobjLista)
        {
            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_INS_VEICULOLISTA");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            cmdCommand.Parameters.Add(new FWParameter("@Des_VeiculoLista", FWDbType.Varchar));
            cmdCommand.Parameters["@Des_VeiculoLista"].Value = pobjLista.DescricaoLista;

            cmdCommand.Parameters.Add(new FWParameter("@Id_UsuarioInclusao", FWDbType.Int32));
            cmdCommand.Parameters["@Id_UsuarioInclusao"].Value = pobjLista.IdUsuario;

            cmdCommand.Parameters.Add(new FWParameter("@Id_Filial", FWDbType.Varchar, 15));
            cmdCommand.Parameters["@Id_Filial"].Value = pobjLista.IdFilial;

            //Executa a função no banco de dados 
            int intRetorno = 0;

            try
            {

                using (System.Data.IDataReader idrRetorno = conProvider.ExecuteDataReader(cmdCommand))
                {
                    if (idrRetorno.Read())
                    {
                        if (idrRetorno["status"].ToString().Equals("1"))
                            intRetorno = Convert.ToInt32(idrRetorno["retorno"]);
                        else
                            throw new Exception(idrRetorno["descricao"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return intRetorno;
        }

        #endregion

        #region Alterar

        public int Alterar(SafWeb.Model.ListaVeiculos.ListaVeiculos pobjLista)
        {
            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_UPD_VEICULOLISTA");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            cmdCommand.Parameters.Add(new FWParameter("@Id_VeiculoLista", FWDbType.Int32));
            cmdCommand.Parameters["@Id_VeiculoLista"].Value = pobjLista.IdLista;
            //if (string.IsNullOrEmpty(pobjLista.DescricaoLista))
            //{
            cmdCommand.Parameters.Add(new FWParameter("@Des_VeiculoLista", FWDbType.Varchar));
            cmdCommand.Parameters["@Des_VeiculoLista"].Value = pobjLista.DescricaoLista;
            //}

            cmdCommand.Parameters.Add(new FWParameter("@Id_UsuarioAlteracao", FWDbType.Int32));
            cmdCommand.Parameters["@Id_UsuarioAlteracao"].Value = pobjLista.IdUsuario;

            //if (pobjLista.IdFilial > 0)
            //{
            cmdCommand.Parameters.Add(new FWParameter("@Id_Filial", FWDbType.Int32));
            cmdCommand.Parameters["@Id_Filial"].Value = pobjLista.IdFilial;
            //}

            //cmdCommand.Parameters.Add(new FWParameter("@Flg_Situacao", FWDbType.Byte));
            //cmdCommand.Parameters["@Flg_Situacao"].Value = pobjLista.Situacao;


            //Executa a função no banco de dados 
            int intRetorno = 0;

            try
            {

                using (System.Data.IDataReader idrRetorno = conProvider.ExecuteDataReader(cmdCommand))
                {
                    if (idrRetorno.Read())
                    {
                        if (idrRetorno["status"].ToString().Equals("1"))
                            intRetorno = Convert.ToInt32(idrRetorno["retorno"]);
                        else
                            throw new Exception(idrRetorno["descricao"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return intRetorno;
        }

        public bool AlterarSituacao(SafWeb.Model.ListaVeiculos.ListaVeiculos pobjLista)
        {

            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("[SP_SAF_UPD_VEICULOLISTA]");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            cmdCommand.Parameters.Add(new FWParameter("@Id_VeiculoLista", FWDbType.Int32));
            cmdCommand.Parameters["@Id_VeiculoLista"].Value = pobjLista.IdLista;

            cmdCommand.Parameters.Add(new FWParameter("@Id_UsuarioAlteracao", FWDbType.Int32));
            cmdCommand.Parameters["@Id_UsuarioAlteracao"].Value = pobjLista.IdUsuario;

            cmdCommand.Parameters.Add(new FWParameter("@Flg_Situacao", FWDbType.Byte));
            cmdCommand.Parameters["@Flg_Situacao"].Value = pobjLista.Situacao;

            //Executa a função no banco de dados 
            bool blnRetorno = false;

            try
            {
                conProvider.ExecuteScalar(cmdCommand);
                blnRetorno = true;
            }
            catch (Exception ex)
            {
                throw;
            }

            return blnRetorno;
        }

        #endregion

        #region Inserir Veiculos

        public bool InserirVeiculoLista(int pintIdLista,
                                        int pintVeiculo,
                                        int pintUsuarioAlteracao,
                                        byte pbitSituacao)
        {

            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("[SP_SAF_INS_VEICULODALISTA]");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            cmdCommand.Parameters.Add(new FWParameter("@Id_VeiculoLista", FWDbType.Int32));
            cmdCommand.Parameters["@Id_VeiculoLista"].Value = pintIdLista;

            cmdCommand.Parameters.Add(new FWParameter("@Id_Veículo", FWDbType.Int32));
            cmdCommand.Parameters["@Id_Veículo"].Value = pintVeiculo;

            cmdCommand.Parameters.Add(new FWParameter("@Id_UsuarioAlteracao", FWDbType.Int32));
            cmdCommand.Parameters["@Id_UsuarioAlteracao"].Value = pintUsuarioAlteracao;

            cmdCommand.Parameters.Add(new FWParameter("@Flg_Situacao", FWDbType.Byte));
            cmdCommand.Parameters["@Flg_Situacao"].Value = pbitSituacao;

            //Executa a função no banco de dados 
            bool blnRetorno = false;

            try
            {
                conProvider.ExecuteScalar(cmdCommand);
                blnRetorno = true;
            }
            catch (Exception ex)
            {
                throw;
            }

            return blnRetorno;
        }

        #endregion

        public bool RemoverVeiculoLista(int pintIdLista, int pintIdVeiculo)
        {

            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("[SP_SAF_DEL_VEICULODALISTA]");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            cmdCommand.Parameters.Add(new FWParameter("@Id_VeiculoLista", FWDbType.Int32));
            cmdCommand.Parameters["@Id_VeiculoLista"].Value = pintIdLista;

            if (pintIdVeiculo > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Id_Veículo", FWDbType.Int32));
                cmdCommand.Parameters["@Id_Veículo"].Value = pintIdVeiculo;
            }

            //Executa a função no banco de dados 
            bool blnRetorno = false;

            try
            {
                conProvider.ExecuteScalar(cmdCommand);
                blnRetorno = true;
            }
            catch (Exception ex)
            {
                throw;
            }

            return blnRetorno;

        }

        #region Listar Lista

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        ///  Lista registros da base em uma Collection 
        /// </summary> 
        /// <returns>Collection Lista</returns> 
        /// <history> 
        ///      [cfrancisco] created 24/04/2012
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public Collection<SafWeb.Model.ListaVeiculos.ListaVeiculos> ListarListaVeiculos(SafWeb.Model.ListaVeiculos.ListaVeiculos pobjLista)
        {
            Collection<SafWeb.Model.ListaVeiculos.ListaVeiculos> colRetorno = null;

            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_LISTAVEICULOS");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Parâmetros de entrada
            if (pobjLista.IdRegional > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Id_Regional", FWDbType.Int32));
                cmdCommand.Parameters["@Id_Regional"].Value = pobjLista.IdRegional;
            }

            if (pobjLista.IdFilial > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Id_Filial", FWDbType.Int32));
                cmdCommand.Parameters["@Id_Filial"].Value = pobjLista.IdFilial;
            }

            if (pobjLista.IdLista > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Id_VeiculoLista", FWDbType.Int32));
                cmdCommand.Parameters["@Id_VeiculoLista"].Value = pobjLista.IdLista;
            }

            if (pobjLista.DescricaoLista != string.Empty)
            {
                cmdCommand.Parameters.Add(new FWParameter("@DES_COLABORADORLISTA", FWDbType.Varchar, 50));
                cmdCommand.Parameters["@DES_COLABORADORLISTA"].Value = pobjLista.DescricaoLista;
            }

            if (pobjLista.Situacao > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@FLG_SITUACAO", FWDbType.Byte));
                cmdCommand.Parameters["@FLG_SITUACAO"].Value = pobjLista.Situacao;
            }

            try
            {
                //Executa a função no banco de dados 
                System.Data.IDataReader idrRetorno;
                SafWeb.Model.ListaVeiculos.ListaVeiculos objRetorno = null;
                colRetorno = new Collection<SafWeb.Model.ListaVeiculos.ListaVeiculos>();

                idrRetorno = conProvider.ExecuteDataReader(cmdCommand);

                while (idrRetorno.Read())
                {
                    objRetorno = new SafWeb.Model.ListaVeiculos.ListaVeiculos();
                    objRetorno.FromIDataReader(idrRetorno);
                    colRetorno.Add(objRetorno);
                }

            }
            catch (Exception)
            {
                throw;
            }

            return colRetorno;
        }

        #endregion

        #region Listar Veículos da Lista

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        ///     Lista registros da base em uma Collection 
        /// </summary> 
        /// <returns>Collection Regional</returns> 
        /// <history> 
        ///      [cfrancisco] created 24/04/2012
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public Collection<SafWeb.Model.Veiculo.Veiculo> ListarVeiculosDaLista(int pintIdLista)
        {
            Collection<SafWeb.Model.Veiculo.Veiculo> colRetorno = null;

            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_VEICULODALISTA");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Parâmetro de entrada
            cmdCommand.Parameters.Add(new FWParameter("@Id_VeiculoLista", FWDbType.Int32));
            cmdCommand.Parameters["@Id_VeiculoLista"].Value = pintIdLista;

            try
            {
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
            }
            catch (Exception)
            {
                throw;
            }

            return colRetorno;
        }

        #endregion

        #region Alterar Situacao Colaborador da lista

        public int AlterarSituacaoVeiculoDaLista(SafWeb.Model.Veiculo.Veiculo pobjVeiculo, int IdLista, int IdUsuario)
        {
            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_UPD_VEICULODALISTA");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            cmdCommand.Parameters.Add(new FWParameter("@Id_Veiculo", FWDbType.Int32));
            cmdCommand.Parameters["@Id_Veiculo"].Value = pobjVeiculo.Codigo;

            cmdCommand.Parameters.Add(new FWParameter("@Id_VeiculoLista", FWDbType.Int32));
            cmdCommand.Parameters["@Id_VeiculoLista"].Value = IdLista;

            cmdCommand.Parameters.Add(new FWParameter("@Flg_Situacao", FWDbType.Byte));
            cmdCommand.Parameters["@Flg_Situacao"].Value = pobjVeiculo.Situacao;

            cmdCommand.Parameters.Add(new FWParameter("@Id_UsuarioAlteracao", FWDbType.Int32));
            cmdCommand.Parameters["@Id_UsuarioAlteracao"].Value = IdUsuario;

            //Executa a função no banco de dados 
            int intRetorno = 0;

            try
            {

                using (System.Data.IDataReader idrRetorno = conProvider.ExecuteDataReader(cmdCommand))
                {
                    if (idrRetorno.Read())
                    {
                        if (idrRetorno["status"].ToString().Equals("1"))
                            intRetorno = Convert.ToInt32(idrRetorno["retorno"]);
                        else
                            throw new Exception(idrRetorno["descricao"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return intRetorno;
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
        public SafWeb.Model.ListaVeiculos.ListaVeiculos Obter(int IdLista)
        {
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_VEICULOLISTA");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            cmdCommand.Parameters.Add(new FWParameter("@Id_VeiculoLista", FWDbType.Int32));
            cmdCommand.Parameters["@Id_VeiculoLista"].Value = IdLista;

            SafWeb.Model.ListaVeiculos.ListaVeiculos objRetorno = new SafWeb.Model.ListaVeiculos.ListaVeiculos();

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
