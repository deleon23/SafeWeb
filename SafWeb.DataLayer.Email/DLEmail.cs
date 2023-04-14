using System;
using System.Collections.Generic;
using System.Text;
using FrameWork.DataLayer.Utilitarios;
using System.Data;

namespace SafWeb.DataLayer.Email
{
    public class DLEmail : DALFWBase
    {
         
        #region Construtor
        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// Constructor 
        /// </summary> 
        /// <history> 
        /// [mribeiro] 25/10/2009 Created 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public DLEmail()
        {
            strConnection = DALUtilitarios.GetStringConnection(DALUtilitarios.ConexoesDisponiveis.ConnectionFW);
            conProvider = new DALProvider(strConnection);
            intCommandTimeOut = DALUtilitarios.ObterCommandTimeOut();
        }
        #endregion

        #region Listar Horarios

        /// <summary>
        ///     Lista os registros na base
        /// </summary>
        /// <returns>Horários cadastrados</returns>
        /// <history>
        ///     [mribeiro] created 25/10/2009
        /// </history>
        public DataTable ListarHorarios()
        {
            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_HORARIO_ALERTA");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Executa a função no banco de dados 
            DataTable dttRetorno;

            try
            {
                dttRetorno = conProvider.ExecuteDataTable(cmdCommand);
            }
            catch (Exception ex)
            {
                throw;
            }

            return dttRetorno;
        }

        #endregion

        #region Listar Alertas

        /// <summary>
        ///     Lista os registros na base
        /// </summary>
        /// <returns>Alertas cadastrados</returns>
        /// <history>
        ///     [haguiar] 21/11/2011 14:27
        /// </history>
        public DataTable ListarAlertaRH()
        {
            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_LISTAREGRAEMAILALERTARH");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Executa a função no banco de dados 
            DataTable dttRetorno;

            try
            {
                dttRetorno = conProvider.ExecuteDataTable(cmdCommand);
            }
            catch (Exception ex)
            {
                throw;
            }

            return dttRetorno;
        }

        #endregion

        #region Obter Alerta

        /// <summary>
        ///     Obter registro de alerta
        /// </summary>
        /// <param name="pintId_RegraEmailAlertaRH">Id do alerta</param>
        /// <returns>Alerta cadastrado</returns>
        /// <history>
        ///     [haguiar] created 21/11/2011 14:29
        /// </history>
        public DataTable ObterAlertaRH(int pintId_RegraEmailAlertaRH)
        {
            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_REGRAEMAILALERTARH");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            cmdCommand.Parameters.Add(new FWParameter("@Id_RegraEmailAlertaRH", FWDbType.Int32));
            cmdCommand.Parameters["@Id_RegraEmailAlertaRH"].Value = pintId_RegraEmailAlertaRH;

            //Executa a função no banco de dados 
            DataTable dttRetorno;

            try
            {
                dttRetorno = conProvider.ExecuteDataTable(cmdCommand);
            }
            catch (Exception ex)
            {
                throw;
            }

            return dttRetorno;
        }

        #endregion

        #region Obter Horarios

        /// <summary>
        ///     Lista os registros na base
        /// </summary>
        /// <param name="pintIdUsuario">Id do usuário</param>
        /// <returns>Horários cadastrados</returns>
        /// <history>
        ///     [mribeiro] created 25/10/2009
        /// </history>
        public DataTable ObterHorarios(int pintIdUsuario)
        {
            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_OBTER_HORARIO_ALERTA");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            cmdCommand.Parameters.Add(new FWParameter("@USU_N_CODIGO", FWDbType.Int32));
            cmdCommand.Parameters["@USU_N_CODIGO"].Value = pintIdUsuario;

            //Executa a função no banco de dados 
            DataTable dttRetorno;

            try
            {
                dttRetorno = conProvider.ExecuteDataTable(cmdCommand);
            }
            catch (Exception ex)
            {
                throw;
            }

            return dttRetorno;
        }

        #endregion

        #region Inserir

        /// <summary>
        ///     Insere um novo registro
        /// </summary>
        /// <param name="pintIdUsuario">Id do usuario logado</param>
        /// <param name="pintIdAlerta">Id do alerta</param>
        /// <history>
        ///     [mribeiro] 25/10/2009 created
        /// </history>
        public void Inserir(int pintIdUsuario,
                            int pintIdAlerta)
        {
            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_INS_REGRA_EMAIL");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            cmdCommand.Parameters.Add(new FWParameter("@USU_N_CODIGO", FWDbType.Int32));
            cmdCommand.Parameters["@USU_N_CODIGO"].Value = pintIdUsuario;
        
            cmdCommand.Parameters.Add(new FWParameter("@Id_Alerta", FWDbType.Int32));
            cmdCommand.Parameters["@Id_Alerta"].Value = pintIdAlerta;
            

            //Executa a função no banco de dados 
            try
            {
                conProvider.ExecuteScalar(cmdCommand);
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        /// <summary>
        ///     Insere um novo registro para alerta RH
        /// </summary>
        /// <param name="pstrNom_Colaborador">Nome do colaborador</param>
        /// <param name="pstrDes_Email">Email do colaborador para alerta</param>
        /// <history>
        ///     [haguiar] created 21/11/2011 14:21
        /// </history>
        public int InserirEmailAlertaRH(string pstrNom_Colaborador,
                            string pstrDes_Email)
        {
            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_INS_REGRAEMAILALERTARH");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            cmdCommand.Parameters.Add(new FWParameter("@Nom_Colaborador", FWDbType.Varchar, 100));
            cmdCommand.Parameters["@Nom_Colaborador"].Value = pstrNom_Colaborador;

            cmdCommand.Parameters.Add(new FWParameter("@Des_Email", FWDbType.Varchar, 50));
            cmdCommand.Parameters["@Des_Email"].Value = pstrDes_Email;

            object objRetorno = conProvider.ExecuteScalar(cmdCommand);
            return Convert.ToInt32(objRetorno);
        }

        #endregion

        #region Excluir

        /// <summary>
        ///     Exclui um novo registro
        /// </summary>
        /// <param name="pintIdUsuario">Id do usuario logado</param>
        /// <history>
        ///     [mribeiro] 25/10/2009 created
        /// </history>
        public void Excluir(int pintIdUsuario)
        {
            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_DEL_REGRA_EMAIL");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            cmdCommand.Parameters.Add(new FWParameter("@USU_N_CODIGO", FWDbType.Int32));
            cmdCommand.Parameters["@USU_N_CODIGO"].Value = pintIdUsuario;

            //Executa a função no banco de dados 
            try
            {
                conProvider.ExecuteScalar(cmdCommand);
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        /// <summary>
        ///     Exclui um registro de alerta de RH
        /// </summary>
        /// <param name="pintId_RegraEmailAlertaRH">Id do alerta</param>
        /// <history>
        ///     [haguiar] 21/11/2011 14:24
        /// </history>
        public void ExcluirEmailAlertaRH(int pintId_RegraEmailAlertaRH)
        {
            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_DEL_REGRAEMAILALERTARH");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            cmdCommand.Parameters.Add(new FWParameter("@Id_RegraEmailAlertaRH", FWDbType.Int32));
            cmdCommand.Parameters["@Id_RegraEmailAlertaRH"].Value = pintId_RegraEmailAlertaRH;


            conProvider.ExecuteScalar(cmdCommand);
        }
        #endregion

        #region "Alterar"
        /// <summary>
        ///     Altera um registro do alerta de RH
        /// </summary>
        /// <param name="pintId_RegraEmailAlertaRH">Id do alerta</param>
        /// <param name="pstrNom_Colaborador">Nome do colaborador</param>
        /// <param name="pstrDes_Email">Email do colaborador para alerta</param>
        /// <history>
        ///     [haguiar] created 21/11/2011 18:08
        /// </history>
        public int AlterarEmailAlertaRH(int pintId_RegraEmailAlertaRH,
                                        string pstrNom_Colaborador,
                                        string pstrDes_Email)
        {
            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_UPD_REGRAEMAILALERTARH");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            cmdCommand.Parameters.Add(new FWParameter("@Id_RegraEmailAlertaRH", FWDbType.Int32));
            cmdCommand.Parameters["@Id_RegraEmailAlertaRH"].Value = pintId_RegraEmailAlertaRH;

            cmdCommand.Parameters.Add(new FWParameter("@Nom_Colaborador", FWDbType.Varchar, 100));
            cmdCommand.Parameters["@Nom_Colaborador"].Value = pstrNom_Colaborador;

            cmdCommand.Parameters.Add(new FWParameter("@Des_Email", FWDbType.Varchar, 50));
            cmdCommand.Parameters["@Des_Email"].Value = pstrDes_Email;

            object objRetorno = conProvider.ExecuteScalar(cmdCommand);
            return Convert.ToInt32(objRetorno);
        }
        #endregion

    }
}
