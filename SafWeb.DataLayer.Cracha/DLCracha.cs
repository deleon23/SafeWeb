using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections.ObjectModel;
using FrameWork.DataLayer.Utilitarios;

namespace SafWeb.DataLayer.Cracha
{
    public class DLCracha : DALFWBase
    {
        #region Construtor

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        ///     Constructor 
        /// </summary> 
        /// <history> 
        ///     [vsantos] 15/7/2009 Created 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public DLCracha()
        {
            strConnection = DALUtilitarios.GetStringConnection(DALUtilitarios.ConexoesDisponiveis.ConnectionFW);
            conProvider = new DALProvider(strConnection);
            intCommandTimeOut = DALUtilitarios.ObterCommandTimeOut();
        }

        #endregion

        #region Inserir

        /// <summary>
        ///     Insere os registros na base
        /// </summary>
        /// <param name="pobjCracha">Model Cracha</param>
        /// <returns>Código do Crachá</returns>
        /// <history>
        ///     [vsantos] 15/07/2009 Created 
        /// </history>
        public int Inserir(SafWeb.Model.Cracha.Cracha pobjCracha)
        {
            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_INS_CRACHA");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            cmdCommand.Parameters.Add(new FWParameter("@Num_Cracha", FWDbType.Decimal));
            cmdCommand.Parameters["@Num_Cracha"].Precision = 18;
            cmdCommand.Parameters["@Num_Cracha"].Scale = 0;
            cmdCommand.Parameters["@Num_Cracha"].Value = pobjCracha.NumCracha;

            cmdCommand.Parameters.Add(new FWParameter("@Id_CrachaTipo", FWDbType.Int32));
            cmdCommand.Parameters["@Id_CrachaTipo"].Value = pobjCracha.CodCrachaTipo;

            cmdCommand.Parameters.Add(new FWParameter("@Id_Filial", FWDbType.Int32));
            cmdCommand.Parameters["@Id_Filial"].Value = pobjCracha.CodFilial;

            cmdCommand.Parameters.Add(new FWParameter("@Flg_Situacao", FWDbType.Int16));
            cmdCommand.Parameters["@Flg_Situacao"].Value = (pobjCracha.FlgSituacao.Value ? 1 : 0);
            
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

        #region Alterar

        /// <summary>
        ///     Altera registros na base
        /// </summary>
        /// <param name="pobjCracha">Model Cracha</param>
        /// <returns>Boeleano indicando sucesso da operação</returns>
        /// <history>
        ///     [vsantos] created 15/07/2009
        /// </history>
        public bool Alterar(SafWeb.Model.Cracha.Cracha pobjCracha)
        {
            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_UPD_CRACHA");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            cmdCommand.Parameters.Add(new FWParameter("@Id_Cracha", FWDbType.Int32));
            cmdCommand.Parameters["@Id_Cracha"].Value = pobjCracha.CodCracha;

            if (pobjCracha.NumCracha != string.Empty)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Num_Cracha", FWDbType.Varchar, 18));
                cmdCommand.Parameters["@Num_Cracha"].Value = pobjCracha.NumCracha;
            }

            if (pobjCracha.CodCrachaTipo > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Id_CrachaTipo", FWDbType.Int32));
                cmdCommand.Parameters["@Id_CrachaTipo"].Value = pobjCracha.CodCrachaTipo;
            }

            if (pobjCracha.CodFilial > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Id_Filial", FWDbType.Int32));
                cmdCommand.Parameters["@Id_Filial"].Value = pobjCracha.CodFilial;
            }

            if (pobjCracha.FlgSituacao != null)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Flg_Situacao", FWDbType.Int16));
                cmdCommand.Parameters["@Flg_Situacao"].Value = (pobjCracha.FlgSituacao.Value ? 1 : 0);

                if (pobjCracha.DesObservacao != string.Empty)
                {
                    if (pobjCracha.FlgSituacao.Value)
                    {
                        cmdCommand.Parameters.Add(new FWParameter("@Des_ObsReativacao", FWDbType.Varchar, 100));
                        cmdCommand.Parameters["@Des_ObsReativacao"].Value = pobjCracha.DesObservacao;
                    }
                    else
                    {
                        cmdCommand.Parameters.Add(new FWParameter("@Des_ObsInativacao", FWDbType.Varchar, 100));
                        cmdCommand.Parameters["@Des_ObsInativacao"].Value = pobjCracha.DesObservacao;
                    }
                }
            }

            //Executa a função no banco de dados 
            bool blnRetorno = false;

            try
            {
                conProvider.ExecuteNonQuery(cmdCommand);

                blnRetorno = true;
            }
            catch (Exception ex)
            {
                throw;
            }

            return blnRetorno;
        }

        #endregion

        #region Listar

        /// <summary>
        ///     Lista registros da base
        /// </summary>
        /// <param name="pobjCracha">Model Cracha</param>
        /// <returns>Coleção de objetos tipo Cracha</returns>
        /// <history>
        ///     [vsantos] created 15/07/2009
        /// </history>
        public Collection<Model.Cracha.Cracha> Listar(Model.Cracha.Cracha pobjCracha,
                                                      bool pblnEntrada)
        {
            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_CRACHA");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            if (pobjCracha.CodCracha > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Id_Cracha", FWDbType.Int32));
                cmdCommand.Parameters["@Id_Cracha"].Value = pobjCracha.CodCracha;
            }

            if (pobjCracha.NumCracha != string.Empty)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Num_Cracha", FWDbType.Varchar, 18));
                cmdCommand.Parameters["@Num_Cracha"].Value = pobjCracha.NumCracha;
            }

            if (pobjCracha.CodCrachaTipo > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Id_CrachaTipo", FWDbType.Int32));
                cmdCommand.Parameters["@Id_CrachaTipo"].Value = pobjCracha.CodCrachaTipo;
            }

            if (pobjCracha.CodFilial > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Id_Filial", FWDbType.Int32));
                cmdCommand.Parameters["@Id_Filial"].Value = pobjCracha.CodFilial;
            }

            if (pobjCracha.CodRegional > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Id_Regional", FWDbType.Int32));
                cmdCommand.Parameters["@Id_Regional"].Value = pobjCracha.CodRegional;
            }

            if (pobjCracha.FlgSituacao != null)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Flg_Situacao", FWDbType.Int16));
                cmdCommand.Parameters["@Flg_Situacao"].Value = (pobjCracha.FlgSituacao.Value ? 1 : 0);
            }

            if (pobjCracha.DesObservacao != null)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Des_Observacao", FWDbType.Varchar, 100));
                cmdCommand.Parameters["@Des_Observacao"].Value = pobjCracha.DesObservacao;
            }

            if (pblnEntrada)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Entrada", FWDbType.Boolean));
                cmdCommand.Parameters["@Entrada"].Value = pblnEntrada;
            }

            //Executa a função no banco de dados 
            Collection<Model.Cracha.Cracha> colRetorno = null;

            try
            {
                IDataReader idrRetorno;
                Model.Cracha.Cracha objRetorno = null;
                colRetorno = new Collection<Model.Cracha.Cracha>();

                idrRetorno = conProvider.ExecuteDataReader(cmdCommand);

                while (idrRetorno.Read())
                {
                    objRetorno = new Model.Cracha.Cracha();
                    objRetorno.FromIDataReader(idrRetorno);
                    colRetorno.Add(objRetorno);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return colRetorno;
        }

        #endregion

        #region Listar Crachá Tipo

        /// <summary>
        ///     Lista registros da base
        /// </summary>
        /// <returns>Coleção de objetos tipo CrachaTipo</returns>
        /// <history>
        ///     [vsantos] created 15/07/2009
        /// </history>
        public Collection<Model.Cracha.CrachaTipo> ListarCrachaTipo()
        {
            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_CRACHA_TIPO");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Executa a função no banco de dados 
            Collection<Model.Cracha.CrachaTipo> colRetorno = null;

            try
            {
                IDataReader idrRetorno;
                Model.Cracha.CrachaTipo objRetorno = null;
                colRetorno = new Collection<Model.Cracha.CrachaTipo>();

                idrRetorno = conProvider.ExecuteDataReader(cmdCommand);

                while (idrRetorno.Read())
                {
                    objRetorno = new Model.Cracha.CrachaTipo();
                    objRetorno.FromIDataReader(idrRetorno);
                    colRetorno.Add(objRetorno);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return colRetorno;
        }

        #endregion

        #region Obter Id Cracha

        /// <summary>
        /// Obtem registro da base
        /// </summary>
        /// <param name="strNumero">Número do Crachá</param>
        /// <returns>Id do Crachá</returns>
        /// <history>
        ///     [mribeiro] created 04/08/2009
        /// </history>
        public int ObterIdCracha(string pstrNumero)
        {
            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_ID_CRACHA");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //parametros de entrada
            cmdCommand.Parameters.Add(new FWParameter("@Num_Cracha", FWDbType.Varchar, 18));
            cmdCommand.Parameters["@Num_Cracha"].Value = pstrNumero;

            //Executa a função no banco de dados 
            int intRetorno;

            try
            {
                intRetorno = Convert.ToInt32(conProvider.ExecuteScalar(cmdCommand));

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return intRetorno;
        }

        #endregion

        #region Obter Numéro Cracha

        /// <summary>
        /// Obtem registro da base
        /// </summary>
        /// <param name="pintIdFilial">Id da Filial</param>
        /// <returns>7 digitos inciais do crahchá</returns>
        /// <history>
        ///     [mribeiro] created 15/10/2009
        /// </history>
        public string ObterNumeroCracha(int pintIdFilial)
        {
            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_NUM_CRACHA");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //parametros de entrada
            cmdCommand.Parameters.Add(new FWParameter("@Id_Filial", FWDbType.Int32));
            cmdCommand.Parameters["@Id_Filial"].Value = pintIdFilial;

            //Executa a função no banco de dados 
            string strRetorno;

            try
            {
                strRetorno = Convert.ToString(conProvider.ExecuteScalar(cmdCommand));

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return strRetorno;
        }

        #endregion
    }
}
