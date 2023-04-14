using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using FrameWork.DataLayer.Utilitarios;
using System.Collections.ObjectModel;
using SafWeb.Model.Filial;

namespace SafWeb.DataLayer.Filial
{
    public class DLHorarioVerao : DALFWBase
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
        public DLHorarioVerao()
        {
            strConnection = DALUtilitarios.GetStringConnection(DALUtilitarios.ConexoesDisponiveis.ConnectionFW);
            conProvider = new DALProvider(strConnection);
            intCommandTimeOut = DALUtilitarios.ObterCommandTimeOut();
        }
        #endregion

        #region Listar

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// Lista registros da base em uma datatable 
        /// </summary> 
        /// <returns>DataTable Horario de Verao</returns> 
        /// <history> 
        ///     [haguiar_4] 14/01/2011 Created 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public DataTable Listar()
        {
            DataTable dttRetorno;
            
            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_HORARIOVERAO");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;
            
            dttRetorno = new DataTable();
            dttRetorno = conProvider.ExecuteDataTable(cmdCommand);
  
            return dttRetorno;
        }

        #endregion


        #region AlterarSituacao
        /// <summary>
        /// Altera o status da Situação.
        /// </summary>
        /// <param name="pintIdHorarioVerao">Id do Horario de Verão</param>
        /// <param name="pblnSituacao">Flag da Situação</param>
        /// <history>
        ///     [haguiar_4] created 17/01/2011
        /// </history>
        public void AlterarSituacao(int pintIdHorarioVerao, bool pblnSituacao)
        {
            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_UPD_StatusHorarioVerao");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Parâmetros de entrada
            cmdCommand.Parameters.Add(new FWParameter("@Id_HorarioVerao", FWDbType.Int32));
            cmdCommand.Parameters["@Id_HorarioVerao"].Value = pintIdHorarioVerao;

            cmdCommand.Parameters.Add(new FWParameter("@Flg_Situacao", FWDbType.Boolean));
            cmdCommand.Parameters["@Flg_Situacao"].Value = pblnSituacao;

            //Executa a função no banco de dados 
            conProvider.ExecuteDataReader(cmdCommand);
        }
        #endregion

        #region Inserir

        /// <summary>
        ///     Insere um horário de verão.
        /// </summary>
        /// <param name="pobjHorarioVerao">Objeto Filial</param>
        /// <returns>Id_HorarioVerao ou Erro</returns>
        /// <history>
        ///     [haguiar_4] created 17/01/2011
        /// </history>
        public int Inserir(SafWeb.Model.Filial.HorarioVerao pobjHorarioVerao)
        {
            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_INS_HorarioVerao");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            cmdCommand.Parameters.Add(new FWParameter("@Dt_InicioPeriodo", FWDbType.DateTime));
            cmdCommand.Parameters["@Dt_InicioPeriodo"].Value = pobjHorarioVerao.InicioPeriodo;

            cmdCommand.Parameters.Add(new FWParameter("@Dt_FinalPeriodo", FWDbType.DateTime));
            cmdCommand.Parameters["@Dt_FinalPeriodo"].Value = pobjHorarioVerao.FinalPeriodo;

            cmdCommand.Parameters.Add(new FWParameter("@Id_Filiais", FWDbType.Varchar, 5000));
            cmdCommand.Parameters["@Id_Filiais"].Value = pobjHorarioVerao.IdFiliais;

            //Executa a função no banco de dados 
            object objRetorno = conProvider.ExecuteScalar(cmdCommand);
            return Convert.ToInt32(objRetorno);
        }
        #endregion

        #region AlterarHorarioVerao
        /// <summary>
        /// Altera o Horario de verao
        /// </summary>
        /// <param name="pobjHorarioVerao">Objeto de Horario Verao</param>
        /// <returns>Valor do Id do Horario Verao ou Erro</returns>
        /// <history>
        ///     [haguiar_4] created 18/01/2011
        /// </history>
        public int Alterar(SafWeb.Model.Filial.HorarioVerao pobjHorarioVerao)
        {
            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_UPD_HorarioVerao");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Parâmetros de entrada
            cmdCommand.Parameters.Add(new FWParameter("@Id_HorarioVerao", FWDbType.Int32));
            cmdCommand.Parameters["@Id_HorarioVerao"].Value = pobjHorarioVerao.IdHorarioVerao ;

            cmdCommand.Parameters.Add(new FWParameter("@Dt_InicioPeriodo", FWDbType.DateTime));
            cmdCommand.Parameters["@Dt_InicioPeriodo"].Value = pobjHorarioVerao.InicioPeriodo;

            cmdCommand.Parameters.Add(new FWParameter("@Dt_FinalPeriodo", FWDbType.DateTime));
            cmdCommand.Parameters["@Dt_FinalPeriodo"].Value = pobjHorarioVerao.FinalPeriodo;

            cmdCommand.Parameters.Add(new FWParameter("@Id_Filiais", FWDbType.Varchar, 5000));
            cmdCommand.Parameters["@Id_Filiais"].Value = pobjHorarioVerao.IdFiliais;

            //Executa a função no banco de dados 
            object objRetorno = conProvider.ExecuteScalar(cmdCommand);
            return Convert.ToInt32(objRetorno);
        }
        #endregion
        
        #region Obter
        /// <summary>
        /// Obtém um Horario de Verao
        /// </summary>
        /// <param name="pintIdHorarioVerap">Id do Horario de verao</param>
        /// <returns>Objeto Horario de Verao</returns>
        /// <history>
        ///     [haguiar_4] Created 17/01/2011
        /// </history>
        public SafWeb.Model.Filial.HorarioVerao Obter(int pintIdHorarioVerao)
        {
            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_ObterHorarioVerao");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            cmdCommand.Parameters.Add(new FWParameter("@Id_HorarioVerao", FWDbType.Int32));
            cmdCommand.Parameters["@Id_HorarioVerao"].Value = pintIdHorarioVerao;

            cmdCommand.Parameters.Add(new FWParameter("@OUT_Id_HorarioVerao", FWDbType.Int32));
            cmdCommand.Parameters["@OUT_Id_HorarioVerao"].Direction = ParameterDirection.Output;

            cmdCommand.Parameters.Add(new FWParameter("@OUT_Dt_InicioPeriodo", FWDbType.DateTime));
            cmdCommand.Parameters["@OUT_Dt_InicioPeriodo"].Direction = ParameterDirection.Output;

            cmdCommand.Parameters.Add(new FWParameter("@OUT_Dt_FinalPeriodo", FWDbType.DateTime));
            cmdCommand.Parameters["@OUT_Dt_FinalPeriodo"].Direction = ParameterDirection.Output;

            cmdCommand.Parameters.Add(new FWParameter("@OUT_Id_Filiais", FWDbType.Varchar, 5000));
            cmdCommand.Parameters["@OUT_Id_Filiais"].Direction = ParameterDirection.Output;

            // Execucao
            SafWeb.Model.Filial.HorarioVerao objRetorno = new SafWeb.Model.Filial.HorarioVerao();

            conProvider.ExecuteScalar(cmdCommand);

            if (!(cmdCommand.Parameters["@OUT_Id_HorarioVerao"].Value == DBNull.Value))
                objRetorno.IdHorarioVerao = Convert.ToInt32(cmdCommand.Parameters["@OUT_Id_HorarioVerao"].Value);

            //Popula Objeto Lista
            if (!(cmdCommand.Parameters["@OUT_Dt_InicioPeriodo"].Value == DBNull.Value))
                objRetorno.InicioPeriodo = Convert.ToDateTime(cmdCommand.Parameters["@OUT_Dt_InicioPeriodo"].Value);


            if (!(cmdCommand.Parameters["@OUT_Dt_FinalPeriodo"].Value == DBNull.Value))
                objRetorno.FinalPeriodo = Convert.ToDateTime(cmdCommand.Parameters["@OUT_Dt_FinalPeriodo"].Value);


            if (!(cmdCommand.Parameters["@OUT_Id_Filiais"].Value == DBNull.Value))
            {
                objRetorno.IdFiliais = Convert.ToString(cmdCommand.Parameters["@OUT_Id_Filiais"].Value.ToString().Trim());
                objRetorno.IdFiliais.Remove(objRetorno.IdFiliais.Length - 1, 1);
            }


            return objRetorno;
        }
        #endregion
        
    }
}