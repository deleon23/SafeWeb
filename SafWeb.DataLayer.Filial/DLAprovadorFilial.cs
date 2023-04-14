using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using FrameWork.DataLayer.Utilitarios;
using System.Collections.ObjectModel;
using SafWeb.Model.Filial;

namespace SafWeb.DataLayer.Filial
{
    public class DLAprovadorFilial : DALFWBase
    {
        #region Construtor
        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// Constructor 
        /// </summary> 
        /// <history> 
        /// [haguiar_5] 16/02/2011 Created 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public DLAprovadorFilial()
        {
            strConnection = DALUtilitarios.GetStringConnection(DALUtilitarios.ConexoesDisponiveis.ConnectionFW);
            conProvider = new DALProvider(strConnection);
            intCommandTimeOut = DALUtilitarios.ObterCommandTimeOut();
        }
        #endregion

        #region Listar
        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// Lista registros de usuários de acordo com filtro
        /// </summary> 
        /// <param name="pintRegional">Id da Regional</param>
        /// <param name="pintIdFilial">Id da Filial</param>
        /// <param name="pintId_Superior">Id do superior hierárquico</param>
        /// <param name="pstrUSU_C_NOME">Nome do usuário</param>
        /// <returns>Datatable Usuários</returns> 
        /// <history> 
        ///     [haguiar_5] 16/02/2011 Created 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public DataTable Listar(int pintRegional, int pintIdFilial, int pintId_Superior, string pstrUSU_C_NOME)
        {
            DataTable dttRetorno;
            
            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_APROVADORFILIAL");
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

            //Parâmetros de entrada
            if (pintId_Superior > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Id_Superior", FWDbType.Int32));
                cmdCommand.Parameters["@Id_Superior"].Value = pintIdFilial;
            }

            if (pstrUSU_C_NOME != string.Empty)
            {
                cmdCommand.Parameters.Add(new FWParameter("@USU_C_NOME", FWDbType.Varchar));
                cmdCommand.Parameters["@USU_C_NOME"].Value = pstrUSU_C_NOME;
            }

            dttRetorno = new DataTable();
            dttRetorno = conProvider.ExecuteDataTable(cmdCommand);

            return dttRetorno;

        }
        #endregion

        #region Listar Atributos

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// Lista todas os atributos de um usuário
        /// </summary> 
        /// <param name="pintId_Usuario">Id do usuário</param>
        /// <returns>Collection de atributos</returns> 
        /// <history> 
        ///     [haguiar_5] 17/02/2011 Created
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public Collection<SafWeb.Model.Filial.AprovadorFilial> ListarAtributos(int pintId_Usuario)
        {
            Collection<SafWeb.Model.Filial.AprovadorFilial> colRetorno = null;

            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_APROVADORATRIBUTOS");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            cmdCommand.Parameters.Add(new FWParameter("@Id_Usuario", FWDbType.Int32));
            cmdCommand.Parameters["@Id_Usuario"].Value = pintId_Usuario;

            //Executa a função no banco de dados 
            System.Data.IDataReader idrRetorno;
            SafWeb.Model.Filial.AprovadorFilial objRetorno = null;
            colRetorno = new Collection<SafWeb.Model.Filial.AprovadorFilial>();

            idrRetorno = conProvider.ExecuteDataReader(cmdCommand);

            while (idrRetorno.Read())
            {
                objRetorno = new SafWeb.Model.Filial.AprovadorFilial();
                objRetorno.FromIDataReader(idrRetorno);
                colRetorno.Add(objRetorno);
            }

            return colRetorno;
        }

        #endregion

        #region ObterUsuario

        /// <summary>
        ///      Obtem dados de usuário
        /// </summary>
        /// <param name="pintId_Usuario">Id do usuário</param>
        /// <history>
        ///      [haguiar_5] created 18/02/2011
        /// </history>
        /// <returns>UsuarioAprovadorFilial</returns>
        public UsuarioAprovadorFilial ObterUsuario(int pintId_Usuario)
        {
            
            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("USP_FWC_S_USUARIO");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            cmdCommand.Parameters.Add(new FWParameter("@USU_N_CODIGO", FWDbType.Int32));
            cmdCommand.Parameters["@USU_N_CODIGO"].Value = pintId_Usuario;

            cmdCommand.Parameters.Add(new FWParameter("@FLAG_UPDATEACESSO", FWDbType.Int32));
            cmdCommand.Parameters["@FLAG_UPDATEACESSO"].Value = 0;

            //Executa a função no banco de dados 
            System.Data.IDataReader idrRetorno;
            SafWeb.Model.Filial.UsuarioAprovadorFilial objRetorno = null;
            
            idrRetorno = conProvider.ExecuteDataReader(cmdCommand);

            while (idrRetorno.Read())
            {
                objRetorno = new SafWeb.Model.Filial.UsuarioAprovadorFilial();
                objRetorno.FromIDataReader(idrRetorno);
            }

            return objRetorno;
        }
        
        #endregion

        #region ListarOrigemChamado
        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// Lista Origem da solicitaçao
        /// </summary> 
        /// <returns>Datatable OrigemChamado</returns> 
        /// <history> 
        ///     [haguiar_5] 21/02/2011 Created 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public DataTable ListarOrigemChamado()
        {
            DataTable dttRetorno;

            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_ORIGEMCHAMADO");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            dttRetorno = new DataTable();
            dttRetorno = conProvider.ExecuteDataTable(cmdCommand);

            return dttRetorno;

        }
        #endregion

        #region Inserir

        /// <summary>
        ///     Insere um atributo de aprovador.
        /// </summary>
        /// <param name="pobjAprovadorFilial">Objeto aprovador filial</param>
        /// <returns>Id_AprovadorFilial ou Erro</returns>
        /// <history>
        ///     [haguiar_5] created 21/02/2011
        ///     [haguiar] modified 26/04/2011
        ///     inserir parametro flg_aprovaareati
        /// </history>
        public int Inserir(SafWeb.Model.Filial.AprovadorFilial pobjAprovadorFilial)
        {

            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_INS_APROVFILIAL");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            cmdCommand.Parameters.Add(new FWParameter("@ID_USUARIO", FWDbType.Int32));
            cmdCommand.Parameters["@ID_USUARIO"].Value = pobjAprovadorFilial.Id_Usuario;

            cmdCommand.Parameters.Add(new FWParameter("@Id_Regional", FWDbType.Int32));
            cmdCommand.Parameters["@Id_Regional"].Value = pobjAprovadorFilial.Id_Regional;

            cmdCommand.Parameters.Add(new FWParameter("@Id_Filial", FWDbType.Int32));
            cmdCommand.Parameters["@Id_Filial"].Value = pobjAprovadorFilial.Id_Filial;

            cmdCommand.Parameters.Add(new FWParameter("@Flg_AprovaAreaSeg", FWDbType.Byte));
            cmdCommand.Parameters["@Flg_AprovaAreaSeg"].Value = Convert.ToByte(pobjAprovadorFilial.Flg_AprovaAreaSeg);

            cmdCommand.Parameters.Add(new FWParameter("@Flg_AprovaContingencia", FWDbType.Byte));
            cmdCommand.Parameters["@Flg_AprovaContingencia"].Value = Convert.ToByte(pobjAprovadorFilial.Flg_AprovaContingencia);

            if (pobjAprovadorFilial.Id_AprovaSegNivel > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Id_AprovaSegNivel", FWDbType.Int32));
                cmdCommand.Parameters["@Id_AprovaSegNivel"].Value = pobjAprovadorFilial.Id_AprovaSegNivel;
            }

            cmdCommand.Parameters.Add(new FWParameter("@Id_NivelAprovacao", FWDbType.Int32));
            cmdCommand.Parameters["@Id_NivelAprovacao"].Value = pobjAprovadorFilial.Id_NivelAprovacao;

            if (pobjAprovadorFilial.InicioPeriodo != new DateTime())
            {
                cmdCommand.Parameters.Add(new FWParameter("@DT_INICIOPERIODO", FWDbType.DateTime));
                cmdCommand.Parameters["@DT_INICIOPERIODO"].Value = pobjAprovadorFilial.InicioPeriodo;

                cmdCommand.Parameters.Add(new FWParameter("@DT_FINALPERIODO", FWDbType.DateTime));
                cmdCommand.Parameters["@DT_FINALPERIODO"].Value = pobjAprovadorFilial.FimPeriodo;
            }

            if (!string.IsNullOrEmpty(pobjAprovadorFilial.Des_Justificativa))
            {
                cmdCommand.Parameters.Add(new FWParameter("@DES_JUSTIFICATIVA", FWDbType.Varchar, 50));
                cmdCommand.Parameters["@DES_JUSTIFICATIVA"].Value = pobjAprovadorFilial.Des_Justificativa;
            }

            if (pobjAprovadorFilial.Id_OrigemSol > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Id_OrigemChamado", FWDbType.Int32));
                cmdCommand.Parameters["@Id_OrigemChamado"].Value = pobjAprovadorFilial.Id_OrigemSol;
            }

            if (!string.IsNullOrEmpty(pobjAprovadorFilial.Des_NumeroSol))
            {
                cmdCommand.Parameters.Add(new FWParameter("@DES_NUMEROSOL", FWDbType.Varchar, 10));
                cmdCommand.Parameters["@DES_NUMEROSOL"].Value = pobjAprovadorFilial.Des_NumeroSol;
            }

            cmdCommand.Parameters.Add(new FWParameter("@FLG_SITUACAO", FWDbType.Byte));
            cmdCommand.Parameters["@FLG_SITUACAO"].Value = Convert.ToByte(pobjAprovadorFilial.Flg_Situacao);

            cmdCommand.Parameters.Add(new FWParameter("@Flg_AprovaAreaTI", FWDbType.Byte));
            cmdCommand.Parameters["@Flg_AprovaAreaTI"].Value = Convert.ToByte(pobjAprovadorFilial.Flg_AprovaAreaTI);

            cmdCommand.Parameters.Add(new FWParameter("@ID_USUARIOALTERACAO", FWDbType.Int32));
            cmdCommand.Parameters["@ID_USUARIOALTERACAO"].Value = pobjAprovadorFilial.Id_UsuarioAlteracao;

            cmdCommand.Parameters.Add(new FWParameter("@Flg_AprovaCracha", FWDbType.Byte));
            cmdCommand.Parameters["@Flg_AprovaCracha"].Value = Convert.ToByte(pobjAprovadorFilial.Flg_AprovaCracha);

            //Executa a função no banco de dados 
            object objRetorno = conProvider.ExecuteScalar(cmdCommand);
            return Convert.ToInt32(objRetorno);
        }
        #endregion

        #region Alterar
        /// <summary>
        /// Altera um atributo de aprovador
        /// </summary>
        /// <param name="pobjAprovadorFilial">Objeto aprovador filial</param>
        /// <returns>Id_AprovadorFilial ou Erro</returns>
        /// <history>
        ///     [haguiar_5] created 21/02/2011
        ///     [haguiar] modified 26/04/2011
        ///     inserir parametro flg_aprovaareati
        /// </history>
        public int Alterar(SafWeb.Model.Filial.AprovadorFilial pobjAprovadorFilial)
        {
            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_UPD_APROVFILIAL");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            cmdCommand.Parameters.Add(new FWParameter("@ID_APROVADORFILIAL", FWDbType.Int32));
            cmdCommand.Parameters["@ID_APROVADORFILIAL"].Value = pobjAprovadorFilial.Id_AprovadorFilial;

            cmdCommand.Parameters.Add(new FWParameter("@Id_Regional", FWDbType.Int32));
            cmdCommand.Parameters["@Id_Regional"].Value = pobjAprovadorFilial.Id_Regional;

            cmdCommand.Parameters.Add(new FWParameter("@Id_Filial", FWDbType.Int32));
            cmdCommand.Parameters["@Id_Filial"].Value = pobjAprovadorFilial.Id_Filial;

            cmdCommand.Parameters.Add(new FWParameter("@Flg_AprovaAreaSeg", FWDbType.Byte));
            cmdCommand.Parameters["@Flg_AprovaAreaSeg"].Value = Convert.ToByte(pobjAprovadorFilial.Flg_AprovaAreaSeg);

            cmdCommand.Parameters.Add(new FWParameter("@Flg_AprovaContingencia", FWDbType.Byte));
            cmdCommand.Parameters["@Flg_AprovaContingencia"].Value = Convert.ToByte(pobjAprovadorFilial.Flg_AprovaContingencia);

            cmdCommand.Parameters.Add(new FWParameter("@Id_AprovaSegNivel", FWDbType.Int32));
            cmdCommand.Parameters["@Id_AprovaSegNivel"].Value = pobjAprovadorFilial.Id_AprovaSegNivel;

            cmdCommand.Parameters.Add(new FWParameter("@Id_NivelAprovacao", FWDbType.Int32));
            cmdCommand.Parameters["@Id_NivelAprovacao"].Value = pobjAprovadorFilial.Id_NivelAprovacao;

            if (pobjAprovadorFilial.InicioPeriodo != new DateTime())
            {
                cmdCommand.Parameters.Add(new FWParameter("@DT_INICIOPERIODO", FWDbType.DateTime));
                cmdCommand.Parameters["@DT_INICIOPERIODO"].Value = pobjAprovadorFilial.InicioPeriodo;

                cmdCommand.Parameters.Add(new FWParameter("@DT_FINALPERIODO", FWDbType.DateTime));
                cmdCommand.Parameters["@DT_FINALPERIODO"].Value = pobjAprovadorFilial.FimPeriodo;
            }

            if (!string.IsNullOrEmpty(pobjAprovadorFilial.Des_Justificativa))
            {
                cmdCommand.Parameters.Add(new FWParameter("@DES_JUSTIFICATIVA", FWDbType.Varchar, 50));
                cmdCommand.Parameters["@DES_JUSTIFICATIVA"].Value = pobjAprovadorFilial.Des_Justificativa;
            }

            if (pobjAprovadorFilial.Id_OrigemSol > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Id_OrigemChamado", FWDbType.Int32));
                cmdCommand.Parameters["@Id_OrigemChamado"].Value = pobjAprovadorFilial.Id_OrigemSol;
            }

            if (!string.IsNullOrEmpty(pobjAprovadorFilial.Des_NumeroSol))
            {
                cmdCommand.Parameters.Add(new FWParameter("@DES_NUMEROSOL", FWDbType.Varchar, 10));
                cmdCommand.Parameters["@DES_NUMEROSOL"].Value = pobjAprovadorFilial.Des_NumeroSol;
            }

            cmdCommand.Parameters.Add(new FWParameter("@FLG_SITUACAO", FWDbType.Byte));
            cmdCommand.Parameters["@FLG_SITUACAO"].Value = Convert.ToByte(pobjAprovadorFilial.Flg_Situacao);

            cmdCommand.Parameters.Add(new FWParameter("@Flg_AprovaAreaTI", FWDbType.Byte));
            cmdCommand.Parameters["@Flg_AprovaAreaTI"].Value = Convert.ToByte(pobjAprovadorFilial.Flg_AprovaAreaTI);


            cmdCommand.Parameters.Add(new FWParameter("@ID_USUARIOALTERACAO", FWDbType.Int32));
            cmdCommand.Parameters["@ID_USUARIOALTERACAO"].Value = pobjAprovadorFilial.Id_UsuarioAlteracao;

            cmdCommand.Parameters.Add(new FWParameter("@Flg_AprovaCracha", FWDbType.Byte));
            cmdCommand.Parameters["@Flg_AprovaCracha"].Value = Convert.ToByte(pobjAprovadorFilial.Flg_AprovaCracha);

            //Executa a função no banco de dados 
            object objRetorno = conProvider.ExecuteScalar(cmdCommand);
            return Convert.ToInt32(objRetorno);
        }
        #endregion        
    }
}