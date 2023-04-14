using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using FrameWork.DataLayer.Utilitarios;
using System.Collections.ObjectModel;
using SafWeb.Model.Filial;

namespace SafWeb.DataLayer.Filial
{
    public class DLFilial : DALFWBase
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
        public DLFilial()
        {
            strConnection = DALUtilitarios.GetStringConnection(DALUtilitarios.ConexoesDisponiveis.ConnectionFW);
            conProvider = new DALProvider(strConnection);
            intCommandTimeOut = DALUtilitarios.ObterCommandTimeOut();
        }
        #endregion

        #region AlterarFilial
        /// <summary>
        /// Altera a Filial
        /// </summary>
        /// <param name="pobjFilial">Objeto de Filial</param>
        /// <returns>Valor do Id da Filial ou Erro</returns>
        /// <history>
        ///     [haguiar_4] created 13/01/2011
        /// </history>
        public int Alterar(SafWeb.Model.Filial.Filial pobjFilial)
        {
            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_UPD_Filial");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Parâmetros de entrada
            cmdCommand.Parameters.Add(new FWParameter("@Id_Filial", FWDbType.Int32));
            cmdCommand.Parameters["@Id_Filial"].Value = pobjFilial.IdFilial;

            cmdCommand.Parameters.Add(new FWParameter("@Des_Filial", FWDbType.Varchar, 50));
            cmdCommand.Parameters["@Des_Filial"].Value = pobjFilial.DescricaoFilial;

            cmdCommand.Parameters.Add(new FWParameter("@Alias_Filial", FWDbType.Varchar, 50));
            cmdCommand.Parameters["@Alias_Filial"].Value = pobjFilial.AliasFilial;

            cmdCommand.Parameters.Add(new FWParameter("@Id_Regional", FWDbType.Int32));
            cmdCommand.Parameters["@Id_Regional"].Value = pobjFilial.IdRegional;

            cmdCommand.Parameters.Add(new FWParameter("@Id_Cidade", FWDbType.Int32));
            cmdCommand.Parameters["@Id_Cidade"].Value = pobjFilial.IdCidade;

            cmdCommand.Parameters.Add(new FWParameter("@Cod_Filial", FWDbType.Int32));
            cmdCommand.Parameters["@Cod_Filial"].Value = pobjFilial.CodFilial;

            cmdCommand.Parameters.Add(new FWParameter("@Flg_PortValAcesso", FWDbType.Byte));
            cmdCommand.Parameters["@Flg_PortValAcesso"].Value = pobjFilial.FlgPortValAcesso;

            cmdCommand.Parameters.Add(new FWParameter("@Flg_ContrAcessoOnline", FWDbType.Byte));
            cmdCommand.Parameters["@Flg_ContrAcessoOnline"].Value = pobjFilial.FlgContrAcessoOnline;

            cmdCommand.Parameters.Add(new FWParameter("@Id_FusoHorario", FWDbType.Int32));
            cmdCommand.Parameters["@Id_FusoHorario"].Value = pobjFilial.IdFusoHorario;

            cmdCommand.Parameters.Add(new FWParameter("@Qtd_ToleranciaAcesso", FWDbType.Int32));
            cmdCommand.Parameters["@Qtd_ToleranciaAcesso"].Value = pobjFilial.QtdToleranciaAcesso;

            //Executa a função no banco de dados 
            object objRetorno = conProvider.ExecuteScalar(cmdCommand);
            return Convert.ToInt32(objRetorno);
        }
        #endregion

        #region Inserir

        /// <summary>
        ///     Insere uma Filial.
        /// </summary>
        /// <param name="pobjFilial">Objeto Filial</param>
        /// <returns>Id_Filial ou Erro</returns>
        /// <history>
        ///     [haguiar_4] created 13/01/2011
        /// </history>
        public int Inserir(SafWeb.Model.Filial.Filial pobjFilial)
        {
            
            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_INS_Filial");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            cmdCommand.Parameters.Add(new FWParameter("@Des_Filial", FWDbType.Varchar, 50));
            cmdCommand.Parameters["@Des_Filial"].Value = pobjFilial.DescricaoFilial;

            cmdCommand.Parameters.Add(new FWParameter("@Alias_Filial", FWDbType.Varchar, 50));
            cmdCommand.Parameters["@Alias_Filial"].Value = pobjFilial.AliasFilial;

            cmdCommand.Parameters.Add(new FWParameter("@Id_Regional", FWDbType.Int32));
            cmdCommand.Parameters["@Id_Regional"].Value = pobjFilial.IdRegional;

            cmdCommand.Parameters.Add(new FWParameter("@Id_Cidade", FWDbType.Int32));
            cmdCommand.Parameters["@Id_Cidade"].Value = pobjFilial.IdCidade;

            cmdCommand.Parameters.Add(new FWParameter("@Cod_Filial", FWDbType.Int32));
            cmdCommand.Parameters["@Cod_Filial"].Value = pobjFilial.CodFilial;

            cmdCommand.Parameters.Add(new FWParameter("@Flg_PortValAcesso", FWDbType.Byte));
            cmdCommand.Parameters["@Flg_PortValAcesso"].Value = pobjFilial.FlgPortValAcesso;

            cmdCommand.Parameters.Add(new FWParameter("@Flg_ContrAcessoOnline", FWDbType.Byte));
            cmdCommand.Parameters["@Flg_ContrAcessoOnline"].Value = pobjFilial.FlgContrAcessoOnline;

            cmdCommand.Parameters.Add(new FWParameter("@Id_FusoHorario", FWDbType.Int32));
            cmdCommand.Parameters["@Id_FusoHorario"].Value = pobjFilial.IdFusoHorario;

            cmdCommand.Parameters.Add(new FWParameter("@Qtd_ToleranciaAcesso", FWDbType.Int32));
            cmdCommand.Parameters["@Qtd_ToleranciaAcesso"].Value = pobjFilial.QtdToleranciaAcesso;


            //Executa a função no banco de dados 
            object objRetorno = conProvider.ExecuteScalar(cmdCommand);
            return Convert.ToInt32(objRetorno);
        }
        #endregion

        #region Listar

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// Lista registros da base em uma Collection - filiais com área
        /// </summary> 
        /// <returns>Collection Regional</returns> 
        /// <history> 
        ///     [mribeiro] 22/6/2009 Created 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public Collection<SafWeb.Model.Filial.Filial> Listar(int pintRegional)
        {
            Collection<SafWeb.Model.Filial.Filial> colRetorno = null;

            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_FILIAL");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Parâmetros de entrada
            if( pintRegional > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Id_Regional", FWDbType.Int32));
                cmdCommand.Parameters["@Id_Regional"].Value = pintRegional;
            }

            //Executa a função no banco de dados 
            System.Data.IDataReader idrRetorno;
            SafWeb.Model.Filial.Filial objRetorno = null;
            colRetorno = new Collection<SafWeb.Model.Filial.Filial>();

            idrRetorno = conProvider.ExecuteDataReader(cmdCommand);

            while (idrRetorno.Read())
            {
                objRetorno = new SafWeb.Model.Filial.Filial();
                objRetorno.FromIDataReader(idrRetorno);
                colRetorno.Add(objRetorno);
            }

            return colRetorno;
        }

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// Lista todas as Filiais - filiais sem área
        /// </summary> 
        /// <returns>Collection Filiais</returns> 
        /// <history> 
        ///     [cmarchi] 11/2/2010 Created 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public Collection<SafWeb.Model.Filial.Filial> Listar()
        {
            Collection<SafWeb.Model.Filial.Filial> colRetorno = null;

            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_FilialSArea");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;
                        
            //Executa a função no banco de dados 
            System.Data.IDataReader idrRetorno;
            SafWeb.Model.Filial.Filial objRetorno = null;
            colRetorno = new Collection<SafWeb.Model.Filial.Filial>();

            idrRetorno = conProvider.ExecuteDataReader(cmdCommand);

            while (idrRetorno.Read())
            {
                objRetorno = new SafWeb.Model.Filial.Filial();
                objRetorno.FromIDataReader(idrRetorno);
                colRetorno.Add(objRetorno);
            }

            return colRetorno;
        }


        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// Lista registros da base em uma DataTable
        /// </summary> 
        /// <returns>Datatable Filiais</returns> 
        /// <history> 
        ///     [haguiar_4] 12/01/2011 Created 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public DataTable Listar_DataTable(int pintRegional, int pintIdFilial, string pstrDes_Filial)
        {
            DataTable dttRetorno;
            
            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_FILIALSAREA");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Parâmetros de entrada
            if (pintRegional > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Id_Regional", FWDbType.Int32));
                cmdCommand.Parameters["@Id_Regional"].Value = pintRegional;
            }

            //Parâmetros de entrada
            if (pintIdFilial > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Id_Filial", FWDbType.Int32));
                cmdCommand.Parameters["@Id_Filial"].Value = pintIdFilial;
            }

            if (pstrDes_Filial != string.Empty)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Des_Filial", FWDbType.Varchar));
                cmdCommand.Parameters["@Des_Filial"].Value = pstrDes_Filial;
            }

            dttRetorno = new DataTable();
            dttRetorno = conProvider.ExecuteDataTable(cmdCommand);

            return dttRetorno;

        }
        #endregion

        #region AlterarSituacao
        /// <summary>
        /// Altera o status da Situação.
        /// </summary>
        /// <param name="pintIdEscalaDepartamental">Id da Filial</param>
        /// <param name="pblnSituacao">Flag da Situação</param>
        /// <history>
        ///     [haguiar_4] created 13/01/2011
        /// </history>
        public void AlterarSituacao(int pintIdFilial, bool pblnSituacao)
        {
            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_UPD_StatusFilial");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Parâmetros de entrada
            cmdCommand.Parameters.Add(new FWParameter("@Id_Filial", FWDbType.Int32));
            cmdCommand.Parameters["@Id_Filial"].Value = pintIdFilial;

            cmdCommand.Parameters.Add(new FWParameter("@Flg_Situacao", FWDbType.Boolean));
            cmdCommand.Parameters["@Flg_Situacao"].Value = pblnSituacao;

            //Executa a função no banco de dados 
            conProvider.ExecuteDataReader(cmdCommand);
        }
        #endregion

        #region Obter
        /// <summary>
        /// Obtém uma Filial
        /// </summary>
        /// <param name="pintIdFilial">Id da Filial</param>
        /// <returns>Objeto Filial</returns>
        /// <history>
        ///     [tgerevini] created 6/4/2010
        ///     [tgerevini] Modified 27/4/2010
        ///     [haguiar_4] Modified 13/01/2011
        /// </history>
        public SafWeb.Model.Filial.Filial Obter(int pintIdFilial)
        {
            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_ObterFilial");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            cmdCommand.Parameters.Add(new FWParameter("@Id_Filial", FWDbType.Int32));
            cmdCommand.Parameters["@Id_Filial"].Value = pintIdFilial;

            cmdCommand.Parameters.Add(new FWParameter("@OUT_Id_Filial", FWDbType.Int32));
            cmdCommand.Parameters["@OUT_Id_Filial"].Direction = ParameterDirection.Output;

            cmdCommand.Parameters.Add(new FWParameter("@OUT_Des_Filial", FWDbType.Varchar,50));
            cmdCommand.Parameters["@OUT_Des_Filial"].Direction = ParameterDirection.Output;

            cmdCommand.Parameters.Add(new FWParameter("@OUT_Alias_Filial", FWDbType.Varchar, 20));
            cmdCommand.Parameters["@OUT_Alias_Filial"].Direction = ParameterDirection.Output;

            cmdCommand.Parameters.Add(new FWParameter("@OUT_Id_Regional", FWDbType.Int32));
            cmdCommand.Parameters["@OUT_Id_Regional"].Direction = ParameterDirection.Output;

            cmdCommand.Parameters.Add(new FWParameter("@OUT_Id_Cidade", FWDbType.Int32));
            cmdCommand.Parameters["@OUT_Id_Cidade"].Direction = ParameterDirection.Output;

            cmdCommand.Parameters.Add(new FWParameter("@OUT_Id_Estado", FWDbType.Int32));
            cmdCommand.Parameters["@OUT_Id_Estado"].Direction = ParameterDirection.Output;

            cmdCommand.Parameters.Add(new FWParameter("@OUT_Cod_Filial", FWDbType.Int32));
            cmdCommand.Parameters["@OUT_Cod_Filial"].Direction = ParameterDirection.Output;

            cmdCommand.Parameters.Add(new FWParameter("@OUT_Flg_PortValAcesso", FWDbType.Byte));
            cmdCommand.Parameters["@OUT_Flg_PortValAcesso"].Direction = ParameterDirection.Output;

            cmdCommand.Parameters.Add(new FWParameter("@OUT_Flg_ContrAcessoOnline", FWDbType.Byte));
            cmdCommand.Parameters["@OUT_Flg_ContrAcessoOnline"].Direction = ParameterDirection.Output;

            cmdCommand.Parameters.Add(new FWParameter("@OUT_Qtd_ToleranciaAcesso", FWDbType.Int32));
            cmdCommand.Parameters["@OUT_Qtd_ToleranciaAcesso"].Direction = ParameterDirection.Output;

            cmdCommand.Parameters.Add(new FWParameter("@OUT_Flg_Situacao", FWDbType.Boolean));
            cmdCommand.Parameters["@OUT_Flg_Situacao"].Direction = ParameterDirection.Output;

            cmdCommand.Parameters.Add(new FWParameter("@OUT_Dt_Inclusao", FWDbType.DateTime));
            cmdCommand.Parameters["@OUT_Dt_Inclusao"].Direction = ParameterDirection.Output;

            cmdCommand.Parameters.Add(new FWParameter("@OUT_Dt_Alteracao", FWDbType.DateTime));
            cmdCommand.Parameters["@OUT_Dt_Alteracao"].Direction = ParameterDirection.Output;

            cmdCommand.Parameters.Add(new FWParameter("@OUT_Id_FusoHorario", FWDbType.Int32));
            cmdCommand.Parameters["@OUT_Id_FusoHorario"].Direction = ParameterDirection.Output;

            cmdCommand.Parameters.Add(new FWParameter("@OUT_Vlr_FusoHorario", FWDbType.Double));
            cmdCommand.Parameters["@OUT_Vlr_FusoHorario"].Direction = ParameterDirection.Output;

            // Execucao
            SafWeb.Model.Filial.Filial objRetorno = new SafWeb.Model.Filial.Filial();

            conProvider.ExecuteScalar(cmdCommand);

            if (!(cmdCommand.Parameters["@OUT_Id_Filial"].Value == DBNull.Value))
                objRetorno.IdFilial = Convert.ToInt32(cmdCommand.Parameters["@OUT_Id_Filial"].Value);

            //Popula Objeto Lista
            if (!(cmdCommand.Parameters["@OUT_Des_Filial"].Value == DBNull.Value))
                objRetorno.DescricaoFilial = Convert.ToString(cmdCommand.Parameters["@OUT_Des_Filial"].Value);
            if (!(cmdCommand.Parameters["@OUT_Alias_Filial"].Value == DBNull.Value))
                objRetorno.AliasFilial = Convert.ToString(cmdCommand.Parameters["@OUT_Alias_Filial"].Value);
            if (!(cmdCommand.Parameters["@OUT_Id_Regional"].Value == DBNull.Value))
                objRetorno.IdRegional = Convert.ToInt32(cmdCommand.Parameters["@OUT_Id_Regional"].Value);
            if (!(cmdCommand.Parameters["@OUT_Id_Cidade"].Value == DBNull.Value))
                objRetorno.IdCidade = Convert.ToInt32(cmdCommand.Parameters["@OUT_Id_Cidade"].Value);

            if (!(cmdCommand.Parameters["@OUT_Id_Estado"].Value == DBNull.Value))
                objRetorno.IdEstado = Convert.ToInt32(cmdCommand.Parameters["@OUT_Id_Estado"].Value);

            if (!(cmdCommand.Parameters["@OUT_Cod_Filial"].Value == DBNull.Value))
                objRetorno.CodFilial = Convert.ToInt32(cmdCommand.Parameters["@OUT_Cod_Filial"].Value);
            if (!(cmdCommand.Parameters["@OUT_Flg_PortValAcesso"].Value == DBNull.Value))
                objRetorno.FlgPortValAcesso = Convert.ToBoolean(cmdCommand.Parameters["@OUT_Flg_PortValAcesso"].Value);
            if (!(cmdCommand.Parameters["@OUT_Flg_ContrAcessoOnline"].Value == DBNull.Value))
                objRetorno.FlgContrAcessoOnline = Convert.ToBoolean(cmdCommand.Parameters["@OUT_Flg_ContrAcessoOnline"].Value);
            if (!(cmdCommand.Parameters["@OUT_Qtd_ToleranciaAcesso"].Value == DBNull.Value))
                objRetorno.QtdToleranciaAcesso = Convert.ToInt32(cmdCommand.Parameters["@OUT_Qtd_ToleranciaAcesso"].Value);
            if (!(cmdCommand.Parameters["@OUT_Flg_Situacao"].Value == DBNull.Value))
                objRetorno.Situacao = Convert.ToBoolean(cmdCommand.Parameters["@OUT_Flg_Situacao"].Value);
            if (!(cmdCommand.Parameters["@OUT_Dt_Inclusao"].Value == DBNull.Value))
                objRetorno.DataInclusao = Convert.ToDateTime(cmdCommand.Parameters["@OUT_Dt_Inclusao"].Value);
            if (!(cmdCommand.Parameters["@OUT_Dt_Alteracao"].Value == DBNull.Value))
                objRetorno.DataAlteracao = Convert.ToDateTime(cmdCommand.Parameters["@OUT_Dt_Alteracao"].Value);

            if (!(cmdCommand.Parameters["@OUT_Id_FusoHorario"].Value == DBNull.Value))
                objRetorno.IdFusoHorario = Convert.ToInt32(cmdCommand.Parameters["@OUT_Id_FusoHorario"].Value);

            if (!(cmdCommand.Parameters["@OUT_Vlr_FusoHorario"].Value == DBNull.Value))
                objRetorno.Vlr_FusoHorario = Convert.ToDouble(cmdCommand.Parameters["@OUT_Vlr_FusoHorario"].Value);

            return objRetorno;
        }
        #endregion
    }
}