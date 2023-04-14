using System;
using System.Collections.ObjectModel;
using System.Data;
using FrameWork.DataLayer.Utilitarios;

namespace SafWeb.DataLayer.Area
{
    public class DLArea : DALFWBase
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
        public DLArea()
        {
            strConnection = DALUtilitarios.GetStringConnection(DALUtilitarios.ConexoesDisponiveis.ConnectionFW);
            conProvider = new DALProvider(strConnection);
            intCommandTimeOut = DALUtilitarios.ObterCommandTimeOut();
        }
        #endregion

        #region Listar Area Segurança

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        ///     Lista as areas (codigo + flag area segurança)
        /// </summary> 
        /// <returns>Collection Area</returns> 
        /// <history> 
        ///     [mribeiro] 07/07/2009 Created 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public Collection<SafWeb.Model.Area.Area> ListarAreaSeg(int pintFilial, int pintTipo)
        {
            Collection<SafWeb.Model.Area.Area> colRetorno = null;

            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_AREA_SEGURANCA");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Parâmetros de entrada
            if (pintFilial > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Id_Filial", FWDbType.Int32));
                cmdCommand.Parameters["@Id_Filial"].Value = pintFilial;
            }

            if (!pintTipo.Equals(-1))
            {
                cmdCommand.Parameters.Add(new FWParameter("@FLG_COLETORESPONTO", FWDbType.Int32));
                cmdCommand.Parameters["@FLG_COLETORESPONTO"].Value = pintTipo;

            }

            //Executa a função no banco de dados 
            System.Data.IDataReader idrRetorno;
            SafWeb.Model.Area.Area objRetorno = null;
            colRetorno = new Collection<SafWeb.Model.Area.Area>();

            idrRetorno = conProvider.ExecuteDataReader(cmdCommand);

            while (idrRetorno.Read())
            {
                objRetorno = new SafWeb.Model.Area.Area();
                objRetorno.FromIDataReader(idrRetorno);
                colRetorno.Add(objRetorno);
            }

            return colRetorno;
        }

        #endregion

        #region Listar Area

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        ///     Lista as areas
        /// </summary> 
        /// <returns>Collection Area</returns> 
        /// <history> 
        ///     [mribeiro] 07/07/2009 Created 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public Collection<SafWeb.Model.Area.Area> ListarArea(int pintFilial)
        {
            Collection<SafWeb.Model.Area.Area> colRetorno = null;

            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_AREA");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Parâmetros de entrada
            if (pintFilial > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Id_Filial", FWDbType.Int32));
                cmdCommand.Parameters["@Id_Filial"].Value = pintFilial;
            }

            //Executa a função no banco de dados 
            System.Data.IDataReader idrRetorno;
            SafWeb.Model.Area.Area objRetorno = null;
            colRetorno = new Collection<SafWeb.Model.Area.Area>();

            idrRetorno = conProvider.ExecuteDataReader(cmdCommand);

            while (idrRetorno.Read())
            {
                objRetorno = new SafWeb.Model.Area.Area();
                objRetorno.FromIDataReader(idrRetorno);
                colRetorno.Add(objRetorno);
            }

            return colRetorno;
        }

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        ///     Lista as areas
        /// </summary> 
        /// <returns>DataTable Area</returns> 
        /// <history> 
        ///     [haguiar_4] 26/01/2011 Created 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public DataTable ListarArea_DataTable(int pintFilial)
        {
            DataTable dttRetorno;

            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_AREA");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Parâmetros de entrada
            if (pintFilial > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Id_Filial", FWDbType.Int32));
                cmdCommand.Parameters["@Id_Filial"].Value = pintFilial;
            }

            dttRetorno = new DataTable();
            dttRetorno = conProvider.ExecuteDataTable(cmdCommand);

            return dttRetorno;
        }

        #endregion

        #region Listar Grupo Coletores

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        ///     Lista os grupos de coletores
        /// </summary> 
        /// <returns>Collection GrupoColetores</returns> 
        /// <history> 
        ///     [haguiar_4] 26/01/2011 Created 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public Collection<SafWeb.Model.Area.GrupoColetores> ListarGrupoColetores()
        {
            Collection<SafWeb.Model.Area.GrupoColetores> colRetorno = null;

            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_GRUPOCOLET");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Executa a função no banco de dados 
            System.Data.IDataReader idrRetorno;
            SafWeb.Model.Area.GrupoColetores objRetorno = null;
            colRetorno = new Collection<SafWeb.Model.Area.GrupoColetores>();

            idrRetorno = conProvider.ExecuteDataReader(cmdCommand);

            while (idrRetorno.Read())
            {
                objRetorno = new SafWeb.Model.Area.GrupoColetores();
                objRetorno.FromIDataReader(idrRetorno);
                colRetorno.Add(objRetorno);
            }

            return colRetorno;
        }
        #endregion

        #region Excluir Areas
        /// <summary>
        /// Exclui Áreas de uma Filial
        /// </summary>
        /// <param name="pintIdFilial">Id da Filial</param>
        /// <history>
        ///     [haguiar_4] created 27/01/2011
        /// </history>
        public void ExcluirAreas(int pintIdFilial)
        {
            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_DEL_AREAFILIAL");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Parâmetros de entrada
            cmdCommand.Parameters.Add(new FWParameter("@Id_Filial", FWDbType.Int32));
            cmdCommand.Parameters["@Id_Filial"].Value = pintIdFilial;

            //Executa a função no banco de dados 
            conProvider.ExecuteDataReader(cmdCommand);
        }
        #endregion

        #region Inserir

        /// <summary>
        ///     Insere uma Área.
        /// </summary>
        /// <param name="pobjArea">Objeto Área</param>
        /// <returns>Id_Area ou Erro</returns>
        /// <history>
        ///     [haguiar_4] created 13/01/2011
        ///     [haguiar] modify 26/04/2011 10:46
        ///     incluir Flg_AreaTI
        /// </history>
        public int Inserir(SafWeb.Model.Area.Area pobjArea)
        {
            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_INS_AREA");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            cmdCommand.Parameters.Add(new FWParameter("@Des_AREA", FWDbType.Varchar, 50));
            cmdCommand.Parameters["@Des_AREA"].Value = pobjArea.Descricao;

            cmdCommand.Parameters.Add(new FWParameter("@ID_FILIAL", FWDbType.Int32));
            cmdCommand.Parameters["@ID_FILIAL"].Value = pobjArea.IdFilial;

            cmdCommand.Parameters.Add(new FWParameter("@Flg_AREASEG", FWDbType.Byte));
            if (pobjArea.Flg_AreaSeg.Equals("SIM"))
            {
                cmdCommand.Parameters["@Flg_AREASEG"].Value = 1;
            }
            else
            {
                cmdCommand.Parameters["@Flg_AREASEG"].Value = 0;
            }

            cmdCommand.Parameters.Add(new FWParameter("@Flg_Situacao", FWDbType.Boolean));
            cmdCommand.Parameters["@Flg_Situacao"].Value = pobjArea.Flg_Situacao;

            cmdCommand.Parameters.Add(new FWParameter("@Flg_AreaTI", FWDbType.Boolean));
            cmdCommand.Parameters["@Flg_AreaTI"].Value = pobjArea.Flg_AreaTI;

            cmdCommand.Parameters.Add(new FWParameter("@ID_GRUPOCOLETORES", FWDbType.Int32));
            cmdCommand.Parameters["@ID_GRUPOCOLETORES"].Value = pobjArea.IdGrupoColetores;

            cmdCommand.Parameters.Add(new FWParameter("@FLG_COLETORESPONTO", FWDbType.Boolean));
            cmdCommand.Parameters["@FLG_COLETORESPONTO"].Value = pobjArea.flg_ColetoresPonto;


            //Executa a função no banco de dados 
            object objRetorno = conProvider.ExecuteScalar(cmdCommand);
            return Convert.ToInt32(objRetorno);
        }
        #endregion

        #region Alterar

        /// <summary>
        ///     Altera uma Área.
        /// </summary>
        /// <param name="pobjArea">Objeto Área</param>
        /// <returns>Id_Area ou Erro</returns>
        /// <history>
        ///     [haguiar_4] created 02/02/2011
        ///     [haguiar] modify 26/04/2011 10:47
        ///     incluir Flg_AreaTI
        /// </history>
        public int Alterar(SafWeb.Model.Area.Area pobjArea)
        {
            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_UPD_AREA");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            cmdCommand.Parameters.Add(new FWParameter("@ID_AREA", FWDbType.Int32));
            cmdCommand.Parameters["@ID_AREA"].Value = pobjArea.Codigo;

            cmdCommand.Parameters.Add(new FWParameter("@Des_AREA", FWDbType.Varchar, 50));
            cmdCommand.Parameters["@Des_AREA"].Value = pobjArea.Descricao;

            cmdCommand.Parameters.Add(new FWParameter("@Flg_AREASEG", FWDbType.Byte));
            if (pobjArea.Flg_AreaSeg.Equals("SIM"))
            {
                cmdCommand.Parameters["@Flg_AREASEG"].Value = 1;
            }
            else
            {
                cmdCommand.Parameters["@Flg_AREASEG"].Value = 0;
            }

            cmdCommand.Parameters.Add(new FWParameter("@Flg_Situacao", FWDbType.Boolean));
            cmdCommand.Parameters["@Flg_Situacao"].Value = pobjArea.Flg_Situacao;

            cmdCommand.Parameters.Add(new FWParameter("@Flg_AreaTI", FWDbType.Boolean));
            cmdCommand.Parameters["@Flg_AreaTI"].Value = pobjArea.Flg_AreaTI;

            cmdCommand.Parameters.Add(new FWParameter("@ID_GRUPOCOLETORES", FWDbType.Int32));
            cmdCommand.Parameters["@ID_GRUPOCOLETORES"].Value = pobjArea.IdGrupoColetores;

            cmdCommand.Parameters.Add(new FWParameter("@FLG_COLETORESPONTO", FWDbType.Boolean));
            cmdCommand.Parameters["@FLG_COLETORESPONTO"].Value = pobjArea.flg_ColetoresPonto;


            //Executa a função no banco de dados 
            object objRetorno = conProvider.ExecuteScalar(cmdCommand);
            return Convert.ToInt32(objRetorno);
        }
        #endregion
    }
}
