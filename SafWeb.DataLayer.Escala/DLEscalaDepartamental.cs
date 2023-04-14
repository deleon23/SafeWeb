using System;
using System.Collections.Generic;
using System.Text;
using FrameWork.DataLayer.Utilitarios;
using SafWeb.Model.Escala;
using SafWeb.Model.Regional;
using SafWeb.Model.Filial;
using System.Collections.ObjectModel;
using System.Data;
using SafWeb.Model.Colaborador;

namespace SafWeb.DataLayer.Escala
{
    /// ----------------------------------------------------------------------------- 
    /// Project : 
    /// Class : DLEscalaDepartamental
    /// ----------------------------------------------------------------------------- 
    /// <summary>  
    /// Implementação da classe DLEscalaDepartamental
    /// </summary> 
    /// <history> 
    ///     [cmarchi] created 4/1/2010 
    /// </history> 
    /// ----------------------------------------------------------------------------- 
    public class DLEscalaDepartamental : DALFWBase
    {
        #region Construtor

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// Constructor 
        /// </summary> 
        /// <history> 
        /// [cmarchi] created 4/1/2010 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public DLEscalaDepartamental()
        {
            strConnection = DALUtilitarios.GetStringConnection(DALUtilitarios.ConexoesDisponiveis.ConnectionFW);
            conProvider = new DALProvider(strConnection);
            intCommandTimeOut = DALUtilitarios.ObterCommandTimeOut();
        }

        #endregion

        #region AlterarEscalaDepartamental
        /// <summary>
        /// Altera a Escala Departamental.
        /// </summary>
        /// <param name="pobjEscalaDepartamental">Objeto de Escala Departamental</param>
        /// <param name="pcolHorarios">Lista de Horários da Escala Departamental</param>
        /// <param name="pstrColaboradores">Colaboradores da Escala Departamental</param>
        /// <returns>Valor do Id da Escala ou Erro</returns>
        /// <history>
        ///     [cmarchi] created 5/1/2010
        ///     [haguiar_5] modify 11/02/2011
        ///     [haguiar] modify 28/03/2011
        ///     adicionar campo Flg_ReplicaRH
        /// </history>
        public int Alterar(EscalaDepartamental pobjEscalaDepartamental, string pstrHorarios, string pstrColaboradores)
        {
            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_UPD_EscalaDepartamental");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Parâmetros de entrada

            cmdCommand.Parameters.Add(new FWParameter("@Id_EscalaDpto", FWDbType.Int32));
            cmdCommand.Parameters["@Id_EscalaDpto"].Value = pobjEscalaDepartamental.IdEscalaDpto;

            cmdCommand.Parameters.Add(new FWParameter("@Des_EscalaDpto", FWDbType.Varchar, 50));
            cmdCommand.Parameters["@Des_EscalaDpto"].Value = pobjEscalaDepartamental.DescricaoEscalaDpto;

            cmdCommand.Parameters.Add(new FWParameter("@Id_Regional", FWDbType.Int32));
            cmdCommand.Parameters["@Id_Regional"].Value = pobjEscalaDepartamental.ObjRegional.IdRegional;

            cmdCommand.Parameters.Add(new FWParameter("@Id_Filial", FWDbType.Int32));
            cmdCommand.Parameters["@Id_Filial"].Value = pobjEscalaDepartamental.ObjFilial.IdFilial;

            cmdCommand.Parameters.Add(new FWParameter("@Id_Periodicidade", FWDbType.Int32));
            cmdCommand.Parameters["@Id_Periodicidade"].Value = pobjEscalaDepartamental.ObjPeriodicidade.IdPeriodicidade;

            cmdCommand.Parameters.Add(new FWParameter("@Id_UsuarioAlteracao", FWDbType.Int32));
            cmdCommand.Parameters["@Id_UsuarioAlteracao"].Value = pobjEscalaDepartamental.IdUsuarioAlteracao;

            cmdCommand.Parameters.Add(new FWParameter("@Flg_Situacao", FWDbType.Boolean));
            cmdCommand.Parameters["@Flg_Situacao"].Value = pobjEscalaDepartamental.Situacao;

            cmdCommand.Parameters.Add(new FWParameter("@Horarios", FWDbType.Varchar, 5000));
            cmdCommand.Parameters["@Horarios"].Value = pstrHorarios;

            cmdCommand.Parameters.Add(new FWParameter("@Colaboradores", FWDbType.Varchar, 5000));
            cmdCommand.Parameters["@Colaboradores"].Value = pstrColaboradores;

            cmdCommand.Parameters.Add(new FWParameter("@Flg_ReplicaRH", FWDbType.Boolean));
            cmdCommand.Parameters["@Flg_ReplicaRH"].Value = pobjEscalaDepartamental.Flg_ReplicaRH;

            //Executa a função no banco de dados 
            object objRetorno = conProvider.ExecuteScalar(cmdCommand);

            return Convert.ToInt32(objRetorno);


        }
        #endregion

        #region AlterarSituacao
        /// <summary>
        /// Altera o status da Situação.
        /// </summary>
        /// <param name="pintIdEscalaDepartamental">Id da Escala Departamental</param>
        /// <param name="pblnSituacao">Flag da Situação</param>
        /// <param name="pintIdUsuarioAlteracao">Id do Usuário</param>
        /// <history>
        ///     [cmarchi] created 5/1/2010
        /// </history>
        public void AlterarSituacao(int pintIdEscalaDepartamental, bool pblnSituacao, int pintIdUsuarioAlteracao)
        {
            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_UPD_StatusEscalaDpto");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Parâmetros de entrada
            cmdCommand.Parameters.Add(new FWParameter("@Id_EscalaDpto", FWDbType.Int32));
            cmdCommand.Parameters["@Id_EscalaDpto"].Value = pintIdEscalaDepartamental;

            cmdCommand.Parameters.Add(new FWParameter("@Flg_Situacao", FWDbType.Boolean));
            cmdCommand.Parameters["@Flg_Situacao"].Value = pblnSituacao;

            cmdCommand.Parameters.Add(new FWParameter("@Id_UsuarioAlteracao", FWDbType.Int32));
            cmdCommand.Parameters["@Id_UsuarioAlteracao"].Value = pintIdUsuarioAlteracao;

            //Executa a função no banco de dados 
            conProvider.ExecuteDataReader(cmdCommand);  
        }
        #endregion

        #region Inserir

        /// <summary>
        ///     Insere uma escala departamental e seus horários.
        /// </summary>
        /// <param name="pobjEscalaDepartamental">Objeto Escala Departamental</param>
        /// <param name="strHorarios">Horários da Escala</param>
        /// <param name="pstrColaboradores">Colaboradores da Escala Departamental</param>
        /// <returns>Id_EscalaDepartamental ou Erro</returns>
        /// <history>
        ///     [cmarchi] created 4/1/2010
        ///     [haguiar_5] modify 11/02/2011
        ///     incluir colaboradores da escala departamental
        ///     [haguiar] modify 28/03/2011
        ///     adicionar campo Flg_ReplicaRH
        /// </history>
        public int Inserir(EscalaDepartamental pobjEscalaDepartamental, string pstrHorarios, string pstrColaboradores)
        {
            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_INS_EscalaDepartamental");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Parâmetros de entrada
            cmdCommand.Parameters.Add(new FWParameter("@Des_EscalaDpto", FWDbType.Varchar, 50));
            cmdCommand.Parameters["@Des_EscalaDpto"].Value = pobjEscalaDepartamental.DescricaoEscalaDpto;

            cmdCommand.Parameters.Add(new FWParameter("@Id_Regional", FWDbType.Int32));
            cmdCommand.Parameters["@Id_Regional"].Value = pobjEscalaDepartamental.ObjRegional.IdRegional;

            cmdCommand.Parameters.Add(new FWParameter("@Id_Filial", FWDbType.Int32));
            cmdCommand.Parameters["@Id_Filial"].Value = pobjEscalaDepartamental.ObjFilial.IdFilial;

            cmdCommand.Parameters.Add(new FWParameter("@Id_Periodicidade", FWDbType.Int32));
            cmdCommand.Parameters["@Id_Periodicidade"].Value = pobjEscalaDepartamental.ObjPeriodicidade.IdPeriodicidade;

            cmdCommand.Parameters.Add(new FWParameter("@Id_UsuarioAlteracao", FWDbType.Int32));
            cmdCommand.Parameters["@Id_UsuarioAlteracao"].Value = pobjEscalaDepartamental.IdUsuarioAlteracao;

            cmdCommand.Parameters.Add(new FWParameter("@Flg_Situacao", FWDbType.Boolean));
            cmdCommand.Parameters["@Flg_Situacao"].Value = true;

            cmdCommand.Parameters.Add(new FWParameter("@Horarios", FWDbType.Varchar, 5000));
            cmdCommand.Parameters["@Horarios"].Value = pstrHorarios;

            cmdCommand.Parameters.Add(new FWParameter("@Colaboradores", FWDbType.Varchar, 5000));
            cmdCommand.Parameters["@Colaboradores"].Value = pstrColaboradores;

            cmdCommand.Parameters.Add(new FWParameter("@Flg_ReplicaRH", FWDbType.Boolean));
            cmdCommand.Parameters["@Flg_ReplicaRH"].Value = pobjEscalaDepartamental.Flg_ReplicaRH;

            cmdCommand.Parameters.Add(new FWParameter("@Flg_EscalaCrew", FWDbType.Boolean));
            cmdCommand.Parameters["@Flg_EscalaCrew"].Value = pobjEscalaDepartamental.Flg_EscalaCrew;

            //Executa a função no banco de dados 
            object objRetorno = conProvider.ExecuteScalar(cmdCommand);
            return Convert.ToInt32(objRetorno);
        }
        #endregion
                
        #region Listar

        #region Listar Escala Departamental com Itens Excluídos ou Não

        /// <summary>
        ///     Lista todoas as Escalas Departmentais de acordo com os parâmetros.
        /// </summary>
        /// <param name="pintRegional">Código da Regional</param>
        /// <param name="pintFilial">Código da Filial</param>
        /// <param name="pstrDescricaoEscalaDepartamental">Descrição da EscalaDepartamental</param>
        /// <param name="pintPeriodicidade">Código de Periodicidade</param>
        /// <param name="pblnListarTudo">True - Lista Excluídas e não excluídas, False Todas não Esxcluídas</param>
        /// <returns>Lista Contendo  as Escalas Departamental</returns>
        /// <history>
        ///     [cmarchi] created 4/1/2010
        /// </history>
        public Collection<EscalaDepartamental> ListarEscalaDepartamental(bool pblnListarTudo)
        {
            Collection<EscalaDepartamental> colRetorno = null;

            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_EscalaDepartamental");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Parâmetros de entrada
            cmdCommand.Parameters.Add(new FWParameter("@Listar_Tudo", FWDbType.Boolean));
            cmdCommand.Parameters["@Listar_Tudo"].Value = pblnListarTudo;

            //Executa a função no banco de dados 
            System.Data.IDataReader idrRetorno;
            EscalaDepartamental objRetorno = null;
            colRetorno = new Collection<EscalaDepartamental>();

            idrRetorno = conProvider.ExecuteDataReader(cmdCommand);

            while (idrRetorno.Read())
            {
                objRetorno = new EscalaDepartamental();
                objRetorno.FromIDataReader(idrRetorno);
                colRetorno.Add(objRetorno);
            }

            return colRetorno;
        }
        #endregion

        #region Listar Escala Departamental com Itens Excluídos ou Não, Por flagSituação

        /// <summary>
        ///     Lista todoas as Escalas Departmentais de acordo com os parâmetros Filtro por flag situação.
        /// </summary>
        /// <param name="pintRegional">Código da Regional</param>
        /// <param name="pintFilial">Código da Filial</param>
        /// <param name="pstrDescricaoEscalaDepartamental">Descrição da EscalaDepartamental</param>
        /// <param name="pintPeriodicidade">Código de Periodicidade</param>
        /// <param name="pblnListarTudo">True - Lista Excluídas e não excluídas, False Todas não Esxcluídas</param>
        /// <returns>Lista Contendo  as Escalas Departamental</returns>
        /// <history>
        ///     [abranco] created 24/06/2010
        /// </history>
        public Collection<EscalaDepartamental> ListarEscalaDepartamentalByFlagSituacao(bool pblnListarTudo)
        {
            Collection<EscalaDepartamental> colRetorno = null;

            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_Escaladepartamentalflagsituacao");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Parâmetros de entrada
            cmdCommand.Parameters.Add(new FWParameter("@Listar_Tudo", FWDbType.Boolean));
            cmdCommand.Parameters["@Listar_Tudo"].Value = pblnListarTudo;

            //Executa a função no banco de dados 
            System.Data.IDataReader idrRetorno;
            EscalaDepartamental objRetorno = null;
            colRetorno = new Collection<EscalaDepartamental>();

            idrRetorno = conProvider.ExecuteDataReader(cmdCommand);

            while (idrRetorno.Read())
            {
                objRetorno = new EscalaDepartamental();
                objRetorno.FromIDataReader(idrRetorno);
                colRetorno.Add(objRetorno);
            }

            return colRetorno;
        }
        #endregion

        #region Listar Escala Departamental

        /// <summary>
        ///     Lista todoas as Escalas Departmentais de acordo com os parâmetros.
        /// </summary>
        /// <param name="pintRegional">Código da Regional</param>
        /// <param name="pintFilial">Código da Filial</param>
        /// <param name="pstrDescricaoEscalaDepartamental">Descrição da EscalaDepartamental</param>
        /// <param name="pintPeriodicidade">Código de Periodicidade</param>
        /// <returns>Lista Contendo  as Escalas Departamental</returns>
        /// <history>
        ///     [cmarchi] created 4/1/2010
        ///     [haguiar_3] modify 07/01/2011
        /// </history>
        public DataTable ListarEscalaDepartamental(int pintRegional, int pintFilial,
            string pstrDescricaoEscalaDepartamental, int pintPeriodicidade, bool pblnListarTudo, bool pblnListarCrw)
        {
            DataTable dttRetorno;

            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_EscalaDepartamental");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Parâmetros de entrada
            if (pintRegional > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Id_Regional", FWDbType.Int32));
                cmdCommand.Parameters["@Id_Regional"].Value = pintRegional;
            }

            if (pintFilial > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Id_Filial", FWDbType.Int32));
                cmdCommand.Parameters["@Id_Filial"].Value = pintFilial;
            }

            if (!string.IsNullOrEmpty(pstrDescricaoEscalaDepartamental))
            {
                cmdCommand.Parameters.Add(new FWParameter("@Des_EscalaDpto", FWDbType.Varchar, 50));
                cmdCommand.Parameters["@Des_EscalaDpto"].Value = pstrDescricaoEscalaDepartamental;
            }

            if (pintPeriodicidade > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Id_Periodicidade", FWDbType.Int32));
                cmdCommand.Parameters["@Id_Periodicidade"].Value = pintPeriodicidade;
            }

            cmdCommand.Parameters.Add(new FWParameter("@Listar_Tudo", FWDbType.Boolean));
            cmdCommand.Parameters["@Listar_Tudo"].Value = Convert.ToInt32(pblnListarTudo) * -1;

            cmdCommand.Parameters.Add(new FWParameter("@LISTAR_CRW", FWDbType.Boolean));
            cmdCommand.Parameters["@LISTAR_CRW"].Value = Convert.ToInt32(pblnListarCrw) * -1;

            //Executa a função no banco de dados 
            dttRetorno = new DataTable();
            dttRetorno = conProvider.ExecuteDataTable(cmdCommand);
            
            return dttRetorno;
        }
        #endregion

        #region ListarEscalaHorarios
        /// <summary>
        /// Lista todos os horários de uma Escala Departamental.
        /// </summary>
        /// <param name="pintIdEscalaDepartamental">Id da Escala Departamental</param>
        /// <returns>Lista de objetos Escala Departamental</returns>
        /// <history>
        ///     [cmarchi] created 5/1/2010
        ///     [haguiar] modify 02/01/2012 14:14
        ///     retornar horários de acordo com o tipo da escalação
        /// </history>
        public Collection<HorarioEscala> ListarEscalaHorarios(int pintIdEscalaDepartamental, bool pblnFlg_Descricao, int ? pintIdTipoSolicitacao)
        {
            Collection<HorarioEscala> colRetorno = null;

            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_HorariosEscala");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Parâmetros de entrada
            cmdCommand.Parameters.Add(new FWParameter("@Id_EscalaDpto", FWDbType.Int32));
            cmdCommand.Parameters["@Id_EscalaDpto"].Value = pintIdEscalaDepartamental;

            if (pintIdTipoSolicitacao.HasValue)
            {
                cmdCommand.Parameters.Add(new FWParameter("@ID_TIPOSOLICITACAO", FWDbType.Int32));
                cmdCommand.Parameters["@ID_TIPOSOLICITACAO"].Value = pintIdTipoSolicitacao;
            }

            cmdCommand.Parameters.Add(new FWParameter("@FLG_LISTADESCRICAO", FWDbType.Boolean));
            cmdCommand.Parameters["@FLG_LISTADESCRICAO"].Value = pblnFlg_Descricao;

            //Executa a função no banco de dados 
            System.Data.IDataReader idrRetorno;
            HorarioEscala objRetorno = null;
            colRetorno = new Collection<HorarioEscala>();

            idrRetorno = conProvider.ExecuteDataReader(cmdCommand);

            while (idrRetorno.Read())
            {
                objRetorno = new HorarioEscala();
                objRetorno.FromIDataReader(idrRetorno);
                colRetorno.Add(objRetorno);
            }

            return colRetorno;
        }
        #endregion
        #endregion

        #region Obter

        #region ObterColaborador
        /// <summary>
        /// Obtém Colaboradores da Escala Departamental.
        /// </summary>
        /// <param name="pintIdEscalacao">Id da Escala Departamental</param>
        /// <returns>Objeto Colaboradores</returns>
        /// <history>
        ///     [cmarchi] created 21/1/2010
        /// </history>
        public Collection<Colaborador> ObterColaborador(int pintIdEscalaDepartamental)
        {
            Collection<Colaborador> colRetorno = null;

            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_ObterColEscalaDpto");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Parâmetros de entrada
            cmdCommand.Parameters.Add(new FWParameter("@Id_EscalaDpto", FWDbType.Int32));
            cmdCommand.Parameters["@Id_EscalaDpto"].Value = pintIdEscalaDepartamental;

            //Executa a função no banco de dados 
            System.Data.IDataReader idrRetorno;
            Colaborador objRetorno = null;
            colRetorno = new Collection<Colaborador>();

            idrRetorno = conProvider.ExecuteDataReader(cmdCommand);

            while (idrRetorno.Read())
            {
                objRetorno = new Colaborador();
                objRetorno.FromIDataReader(idrRetorno);
                colRetorno.Add(objRetorno);
            }

            return colRetorno;
        }
        #endregion         

        #region ObterEscalaDepartamental
        /// <summary>
        /// Obtém uma Escala Departamental.
        /// </summary>
        /// <param name="pintIdEscalaDepartamental">Id da Escala Departamental</param>
        /// <returns>Objeto Escala Departamental</returns>
        /// <history>
        ///     [cmarchi] created 5/1/2010
        /// </history>
        public EscalaDepartamental ObterEscalaDepartamental(int pintIdEscalaDepartalmental)
        {
            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_ObterEscala");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            cmdCommand.Parameters.Add(new FWParameter("@Id_EscalaDpto", FWDbType.Int32));
            cmdCommand.Parameters["@Id_EscalaDpto"].Value = pintIdEscalaDepartalmental;

            cmdCommand.Parameters.Add(new FWParameter("@OUT_Id_EscalaDpto", FWDbType.Int32));
            cmdCommand.Parameters["@OUT_Id_EscalaDpto"].Direction = ParameterDirection.Output;

            cmdCommand.Parameters.Add(new FWParameter("@OUT_Des_EscalaDpto", FWDbType.Varchar, 50));
            cmdCommand.Parameters["@OUT_Des_EscalaDpto"].Direction = ParameterDirection.Output;

            cmdCommand.Parameters.Add(new FWParameter("@OUT_Id_Filial", FWDbType.Int32));
            cmdCommand.Parameters["@OUT_Id_Filial"].Direction = ParameterDirection.Output;

            cmdCommand.Parameters.Add(new FWParameter("@OUT_Alias_Filial", FWDbType.Varchar, 20));
            cmdCommand.Parameters["@OUT_Alias_Filial"].Direction = ParameterDirection.Output;

            cmdCommand.Parameters.Add(new FWParameter("@OUT_Id_Regional", FWDbType.Int32));
            cmdCommand.Parameters["@OUT_Id_Regional"].Direction = ParameterDirection.Output;

            cmdCommand.Parameters.Add(new FWParameter("@OUT_Des_Regional", FWDbType.Varchar, 50));
            cmdCommand.Parameters["@OUT_Des_Regional"].Direction = ParameterDirection.Output;

            cmdCommand.Parameters.Add(new FWParameter("@OUT_Id_Periodicidade", FWDbType.Int32));
            cmdCommand.Parameters["@OUT_Id_Periodicidade"].Direction = ParameterDirection.Output;

            cmdCommand.Parameters.Add(new FWParameter("@OUT_Id_UsuarioAlteracao", FWDbType.Int32));
            cmdCommand.Parameters["@OUT_Id_UsuarioAlteracao"].Direction = ParameterDirection.Output;

            cmdCommand.Parameters.Add(new FWParameter("@OUT_Des_Periodicidade", FWDbType.Varchar, 50));
            cmdCommand.Parameters["@OUT_Des_Periodicidade"].Direction = ParameterDirection.Output;

            cmdCommand.Parameters.Add(new FWParameter("@OUT_Dt_Alteracao", FWDbType.DateTime));
            cmdCommand.Parameters["@OUT_Dt_Alteracao"].Direction = ParameterDirection.Output;

            cmdCommand.Parameters.Add(new FWParameter("@OUT_Flg_Situacao", FWDbType.Byte));
            cmdCommand.Parameters["@OUT_Flg_Situacao"].Direction = ParameterDirection.Output;

            cmdCommand.Parameters.Add(new FWParameter("@OUT_Flg_ReplicaRH", FWDbType.Byte));
            cmdCommand.Parameters["@OUT_Flg_ReplicaRH"].Direction = ParameterDirection.Output;            

            // Execucao
            EscalaDepartamental objRetorno = new EscalaDepartamental();

            conProvider.ExecuteScalar(cmdCommand);

            if (!(cmdCommand.Parameters["@OUT_Id_EscalaDpto"].Value == DBNull.Value))
                objRetorno.IdEscalaDpto = Convert.ToInt32(cmdCommand.Parameters["@OUT_Id_EscalaDpto"].Value);

            //Popula Objeto Lista
            if (!(cmdCommand.Parameters["@OUT_Des_EscalaDpto"].Value == DBNull.Value))
                objRetorno.DescricaoEscalaDpto = Convert.ToString(cmdCommand.Parameters["@OUT_Des_EscalaDpto"].Value);
            
            if (!(cmdCommand.Parameters["@OUT_Id_Filial"].Value == DBNull.Value))
            {
                objRetorno.ObjFilial = new Filial();
                objRetorno.ObjFilial.IdFilial = Convert.ToInt32(cmdCommand.Parameters["@OUT_Id_Filial"].Value);
            }

            if (!(cmdCommand.Parameters["@OUT_Alias_Filial"].Value == DBNull.Value))
            {
                objRetorno.ObjFilial.AliasFilial = Convert.ToString(cmdCommand.Parameters["@OUT_Alias_Filial"].Value);
            }

            if (!(cmdCommand.Parameters["@OUT_Id_Regional"].Value == DBNull.Value))
            {
                objRetorno.ObjRegional = new Regional();
                objRetorno.ObjRegional.IdRegional = Convert.ToInt32(cmdCommand.Parameters["@OUT_Id_Regional"].Value);
            }

            if (!(cmdCommand.Parameters["@OUT_Des_Regional"].Value == DBNull.Value))
            {
                objRetorno.ObjRegional.DescricaoRegional = Convert.ToString(cmdCommand.Parameters["@OUT_Des_Regional"].Value);
            }

            if (!(cmdCommand.Parameters["@OUT_Id_Periodicidade"].Value == DBNull.Value))
            {
                objRetorno.ObjPeriodicidade = new Periodicidade();
                objRetorno.ObjPeriodicidade.IdPeriodicidade = Convert.ToInt32(cmdCommand.Parameters["@OUT_Id_Periodicidade"].Value);
            }

            if (!(cmdCommand.Parameters["@OUT_Des_Periodicidade"].Value == DBNull.Value))
            {
                objRetorno.ObjPeriodicidade.DescricaoPeriodicidade= Convert.ToString(cmdCommand.Parameters["@OUT_Des_Periodicidade"].Value);
            }

            if (!(cmdCommand.Parameters["@OUT_Id_UsuarioAlteracao"].Value == DBNull.Value))
                objRetorno.IdUsuarioAlteracao = Convert.ToInt32(cmdCommand.Parameters["@OUT_Id_UsuarioAlteracao"].Value);

            if (!(cmdCommand.Parameters["@OUT_Dt_Alteracao"].Value == DBNull.Value))
                objRetorno.DataAlteracao = Convert.ToDateTime(cmdCommand.Parameters["@OUT_Dt_Alteracao"].Value);

            if (!(cmdCommand.Parameters["@OUT_Flg_Situacao"].Value == DBNull.Value))
                objRetorno.Situacao = Convert.ToBoolean(cmdCommand.Parameters["@OUT_Flg_Situacao"].Value);

            if (!(cmdCommand.Parameters["@OUT_Flg_ReplicaRH"].Value == DBNull.Value))
                objRetorno.Flg_ReplicaRH = Convert.ToBoolean(cmdCommand.Parameters["@OUT_Flg_ReplicaRH"].Value);
                
            return objRetorno;
        }
        #endregion

        #region ObterEscalaDepartamentalCrew
        /// <summary>
        /// Obtém uma Escala Departamental Crew.
        /// </summary>
        /// <param name="pintIdEscalaDepartamental">Id da Escala Departamental</param>
        /// <returns>Objeto Escala Departamental</returns>
        /// <history>
        ///     [haguiar] 22/03/2012 14:23
        /// </history>
        public int ObterEscalaDptoCrew(int pintId_Filial)
        {
            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_ObterEscalaDptoCrew");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            cmdCommand.Parameters.Add(new FWParameter("@Id_Filial", FWDbType.Int32));
            cmdCommand.Parameters["@Id_Filial"].Value = pintId_Filial;

            cmdCommand.Parameters.Add(new FWParameter("@OUT_Id_EscalaDpto", FWDbType.Int32));
            cmdCommand.Parameters["@OUT_Id_EscalaDpto"].Direction = ParameterDirection.Output;

            // Execucao
            int objRetorno = 0;

            conProvider.ExecuteScalar(cmdCommand);

            if (!(cmdCommand.Parameters["@OUT_Id_EscalaDpto"].Value == DBNull.Value))
                objRetorno = Convert.ToInt32(cmdCommand.Parameters["@OUT_Id_EscalaDpto"].Value);

            return objRetorno;
        }
        #endregion      
        #endregion      

        #region ExcluirColaborador

        public bool ExcluirColaborador(int idColaborador, int idEscalaDpto, int idEscalacao)
        {
            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_DEL_COLABORADORESESCALADPTO");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Parâmetros de entrada
            cmdCommand.Parameters.Add(new FWParameter("@Id_Colaborador", FWDbType.Int32));
            cmdCommand.Parameters["@Id_Colaborador"].Value = idColaborador;

            cmdCommand.Parameters.Add(new FWParameter("@Id_EscalaDpto", FWDbType.Int32));
            cmdCommand.Parameters["@Id_EscalaDpto"].Value = idEscalaDpto;

            if (idEscalacao > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Id_Escalacao", FWDbType.Int32));
                cmdCommand.Parameters["@Id_Escalacao"].Value = idEscalacao;
    }

            //Executa a função no banco de dados 
            object objRetorno = conProvider.ExecuteScalar(cmdCommand);
            return Convert.ToInt32(objRetorno) == 1;

}

        #endregion
    }
}
