using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using FrameWork.DataLayer.Utilitarios;
using System.Data;
using SafWeb.Model.Acesso;

namespace SafWeb.DataLayer.Acesso
{
    public class DLAcessoColaborador : DALFWBase
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
        public DLAcessoColaborador()
        {
            strConnection = DALUtilitarios.GetStringConnection(DALUtilitarios.ConexoesDisponiveis.ConnectionFW);
            conProvider = new DALProvider(strConnection);
            intCommandTimeOut = DALUtilitarios.ObterCommandTimeOut();
        }
        #endregion

        #region Listar Escalas Colaboradores

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        ///     Lista registros da base em uma Collection 
        /// </summary> 
        /// <param name="pintCodigoFilial">Código Filial</param>
        /// <returns>Collection Colaborador</returns> 
        /// <history> 
        ///     [tgerevini] 05/04/2010 Created 
        ///     [aoliveira] 05/01/2013  Modify
        ///     melhora na consulta a banco
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public Collection<ListaAcessoColaborador> ListarEscalaColaborador(int pintCodigoFilial, string pstrCodigoColaborador, string pstrNomeColaborador,
                                                                        int pintCodigoEscalacao, int pintHoraInicio, int pintHoraFim, int pintMinutoInicio,
                                                                        int pintMinutoFim, int pintCodigoEscalaDepto, int pintFlgStatusAcesso, 
                                                                        string pstrHoraAtual, 
                                                                        int startIndex, int pageSize, string sortBy, ref int totalRegistros)
        {
            totalRegistros = 0;
            Collection<SafWeb.Model.Acesso.ListaAcessoColaborador> colRetorno  = new Collection<ListaAcessoColaborador>();

            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_EscalacaoColaboradores");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Parâmetros de entrada
            cmdCommand.Parameters.Add(new FWParameter("@Id_Filial", FWDbType.Int32));
            cmdCommand.Parameters["@Id_Filial"].Value = pintCodigoFilial;


            if (pstrCodigoColaborador != string.Empty)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Cod_Colaborador",FWDbType.Varchar,30));
                cmdCommand.Parameters["@Cod_Colaborador"].Value = pstrCodigoColaborador;
            }

            if (pstrNomeColaborador != string.Empty)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Nom_Colaborador", FWDbType.Varchar, 100));
                cmdCommand.Parameters["@Nom_Colaborador"].Value = pstrNomeColaborador;
            }

            if (pintCodigoEscalacao > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Id_Escalacao", FWDbType.Int32));
                cmdCommand.Parameters["@Id_Escalacao"].Value = pintCodigoEscalacao;
            }

            if (pintHoraInicio > -1)
            {
                cmdCommand.Parameters.Add(new FWParameter("@HoraInicio", FWDbType.Int32));
                cmdCommand.Parameters["@HoraInicio"].Value = pintHoraInicio;

                cmdCommand.Parameters.Add(new FWParameter("@MinutoInicio", FWDbType.Int32));
                cmdCommand.Parameters["@MinutoInicio"].Value = pintMinutoInicio;
            }

            if (pintHoraFim > -1)
            {
                cmdCommand.Parameters.Add(new FWParameter("@HoraFim", FWDbType.Int32));
                cmdCommand.Parameters["@HoraFim"].Value = pintHoraFim;

                cmdCommand.Parameters.Add(new FWParameter("@MinutoFim", FWDbType.Int32));
                cmdCommand.Parameters["@MinutoFim"].Value = pintMinutoFim;
            }

            if (pintCodigoEscalaDepto > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Id_EscalaDpto", FWDbType.Int32));
                cmdCommand.Parameters["@Id_EscalaDpto"].Value = pintCodigoEscalaDepto;
            }

            if (pintFlgStatusAcesso >= 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@FLG_STATUSACESSO", FWDbType.Int32));
                cmdCommand.Parameters["@FLG_STATUSACESSO"].Value = pintFlgStatusAcesso;
            }

            cmdCommand.Parameters.Add(new FWParameter("@HORAATUAL", FWDbType.Varchar,8));
            cmdCommand.Parameters["@HORAATUAL"].Value = pstrHoraAtual;

            cmdCommand.Parameters.Add(new FWParameter("@startIndex", FWDbType.Int32));
            cmdCommand.Parameters["@startIndex"].Value = startIndex;

            cmdCommand.Parameters.Add(new FWParameter("@pageSize", FWDbType.Int32));
            cmdCommand.Parameters["@pageSize"].Value = pageSize;

            cmdCommand.Parameters.Add(new FWParameter("@sortBy", FWDbType.Varchar, 100));
            cmdCommand.Parameters["@sortBy"].Value = sortBy;

            //Executa a função no banco de dados 
            System.Data.IDataReader idrRetorno;
            ListaAcessoColaborador objRetorno = null;
            colRetorno = new Collection<ListaAcessoColaborador>();

            idrRetorno = conProvider.ExecuteDataReader(cmdCommand);

            while (idrRetorno.Read())
            {
                objRetorno = new ListaAcessoColaborador();
                objRetorno.FromIDataReader(idrRetorno);
                colRetorno.Add(objRetorno);
                totalRegistros = Convert.ToInt32(idrRetorno["totalRegistros"]);
            }


            return colRetorno;
        }

        #endregion

        #region Inserir

        /// <summary>
        ///     Insere os registros na base
        /// </summary>
        /// <param name="pobjCracha">Model AcessoColaborador</param>
        /// <returns>Código do AcessoColaborador</returns>
        /// <history>
        ///     [tgerevini] 7/4/2010 Created 
        /// </history>
        public int Inserir(AcessoColaborador pobjAcessoColaborador)
        {
            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_INS_AcessoColEscalado");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            cmdCommand.Parameters.Add(new FWParameter("@Id_Filial", FWDbType.Int32));
            cmdCommand.Parameters["@Id_Filial"].Value = pobjAcessoColaborador.CodFilial;

            cmdCommand.Parameters.Add(new FWParameter("@Id_Escalacao", FWDbType.Int32));
            cmdCommand.Parameters["@Id_Escalacao"].Value = pobjAcessoColaborador.CodEscalacao;

            cmdCommand.Parameters.Add(new FWParameter("@Dt_Entrada", FWDbType.DateTime));
            cmdCommand.Parameters["@Dt_Entrada"].Value = pobjAcessoColaborador.DataEntrada;

            cmdCommand.Parameters.Add(new FWParameter("@Id_UsrLiberaEntrada", FWDbType.Int32));
            cmdCommand.Parameters["@Id_UsrLiberaEntrada"].Value = pobjAcessoColaborador.CodUsuarioLiberaEntrada;

            cmdCommand.Parameters.Add(new FWParameter("@Id_Colaborador", FWDbType.Int32));
            cmdCommand.Parameters["@Id_Colaborador"].Value = pobjAcessoColaborador.CodColaborador;

            cmdCommand.Parameters.Add(new FWParameter("@Data_Escalacao", FWDbType.DateTime));
            cmdCommand.Parameters["@Data_Escalacao"].Value = pobjAcessoColaborador.DataEscalacao;

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

        #region Inserir Acesso Contingencia

        /// <summary>
        ///     Insere acesso contingencia
        /// </summary>
        /// <param name="pobjAcessoContingencia">Model AcessoContingencia</param>
        /// <returns>Código do AcessoContingencia</returns>
        /// <history>
        ///     [haguiar] 08/04/2011 09:40 created
        /// </history>
        public int InserirAcessoContingencia(AcessoContingencia pobjAcessoContingencia)
        {
            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_INS_ACESSOCONTING");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            cmdCommand.Parameters.Add(new FWParameter("@Id_Colaborador", FWDbType.Int32));
            cmdCommand.Parameters["@Id_Colaborador"].Value = pobjAcessoContingencia.Id_Colaborador;

            cmdCommand.Parameters.Add(new FWParameter("@Cod_Colaborador", FWDbType.Varchar));
            cmdCommand.Parameters["@Cod_Colaborador"].Value = pobjAcessoContingencia.Cod_Colaborador;

            cmdCommand.Parameters.Add(new FWParameter("@Id_UsuarioLibAcesso", FWDbType.Int32));
            cmdCommand.Parameters["@Id_UsuarioLibAcesso"].Value = pobjAcessoContingencia.Id_UsuarioLibAcesso;
            
            cmdCommand.Parameters.Add(new FWParameter("@Id_Aprovador", FWDbType.Int32));
            cmdCommand.Parameters["@Id_Aprovador"].Value = pobjAcessoContingencia.Id_Aprovador;
            
            //cmdCommand.Parameters.Add(new FWParameter("@Dt_Inclusao", FWDbType.DateTime));
            //cmdCommand.Parameters["@Dt_Inclusao"].Value = pobjAcessoContingencia.Dt_Inclusao;

            cmdCommand.Parameters.Add(new FWParameter("@Des_Motivo", FWDbType.Varchar));
            cmdCommand.Parameters["@Des_Motivo"].Value = pobjAcessoContingencia.Des_Motivo;

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

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        ///     Edita registros da base 
        /// </summary> 
        /// <param name="pobjAcesso">Model AcessoColaborador</param>
        /// <returns>Boleano indicando sucesso da atualização</returns> 
        /// <history> 
        ///     [tgerevini] 7/4/2010 Created 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public bool Alterar(AcessoColaborador pobjAcessoColaborador)
        {
            bool blnRetorno = false;

            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_UPD_AcessoColEscalado");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Parâmetros de entrada
            cmdCommand.Parameters.Add(new FWParameter("@Id_Filial", FWDbType.Int32));
            cmdCommand.Parameters["@Id_Filial"].Value = pobjAcessoColaborador.CodFilial;

            cmdCommand.Parameters.Add(new FWParameter("@Id_Escalacao", FWDbType.Int32));
            cmdCommand.Parameters["@Id_Escalacao"].Value = pobjAcessoColaborador.CodEscalacao;

            cmdCommand.Parameters.Add(new FWParameter("@Dt_Saida", FWDbType.DateTime));
            cmdCommand.Parameters["@Dt_Saida"].Value = pobjAcessoColaborador.DataSaida;

            cmdCommand.Parameters.Add(new FWParameter("@Id_UsrLiberaSaida", FWDbType.Int32));
            cmdCommand.Parameters["@Id_UsrLiberaSaida"].Value = pobjAcessoColaborador.CodUsuarioLiberaSaida;

            cmdCommand.Parameters.Add(new FWParameter("@Id_Colaborador", FWDbType.Int32));
            cmdCommand.Parameters["@Id_Colaborador"].Value = pobjAcessoColaborador.CodColaborador;

            cmdCommand.Parameters.Add(new FWParameter("@Data_Escalacao", FWDbType.DateTime));
            cmdCommand.Parameters["@Data_Escalacao"].Value = pobjAcessoColaborador.DataEscalacao;

            //Executa a função no banco de dados 
            try
            {
                conProvider.ExecuteNonQuery(cmdCommand);

                blnRetorno = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return blnRetorno;
        }

        #endregion
                
        #region Obter Ultimo Acesso
        /// <summary>
        ///     Obtém último acesso de um colaborador
        /// </summary>
        /// <param name="pintIdFilial">Id da Filial</param>
        /// /// <param name="pintIdEscalacao">Id da Escalação</param>
        /// /// <param name="pintIdColaborador">Id do Colaborador</param>
        /// <returns>Objeto AcessoColaborador</returns>
        /// <history>
        ///     [tgerevini] created 13/4/2010
        /// </history>
        public AcessoColaborador ObterUltimoAcesso(int pintIdFilial, int pintIdEscalacao, int pintIdColaborador)
        {

            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_ObterUltimaDataAcesso");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Parâmetros de entrada
            cmdCommand.Parameters.Add(new FWParameter("@Id_Filial", FWDbType.Int32));
            cmdCommand.Parameters["@Id_Filial"].Value = pintIdFilial;

            cmdCommand.Parameters.Add(new FWParameter("@Id_Escalacao", FWDbType.Int32));
            cmdCommand.Parameters["@Id_Escalacao"].Value = pintIdEscalacao;

            cmdCommand.Parameters.Add(new FWParameter("@Id_Colaborador", FWDbType.Int32));
            cmdCommand.Parameters["@Id_Colaborador"].Value = pintIdColaborador;

            //Parâmetros de Saída
            cmdCommand.Parameters.Add(new FWParameter("@OUT_Id_AcessoColEscalado", FWDbType.Int32));
            cmdCommand.Parameters["@OUT_Id_AcessoColEscalado"].Direction = ParameterDirection.Output;

            cmdCommand.Parameters.Add(new FWParameter("@OUT_Id_Filial", FWDbType.Int32));
            cmdCommand.Parameters["@OUT_Id_Filial"].Direction = ParameterDirection.Output;

            cmdCommand.Parameters.Add(new FWParameter("@OUT_Id_Escalacao", FWDbType.Int32));
            cmdCommand.Parameters["@OUT_Id_Escalacao"].Direction = ParameterDirection.Output;

            cmdCommand.Parameters.Add(new FWParameter("@OUT_Dt_Entrada", FWDbType.DateTime));
            cmdCommand.Parameters["@OUT_Dt_Entrada"].Direction = ParameterDirection.Output;

            cmdCommand.Parameters.Add(new FWParameter("@OUT_Dt_Saida", FWDbType.DateTime));
            cmdCommand.Parameters["@OUT_Dt_Saida"].Direction = ParameterDirection.Output;

            cmdCommand.Parameters.Add(new FWParameter("@OUT_Id_UsrLiberaEntrada", FWDbType.Int32));
            cmdCommand.Parameters["@OUT_Id_UsrLiberaEntrada"].Direction = ParameterDirection.Output;

            cmdCommand.Parameters.Add(new FWParameter("@OUT_Id_UsrLiberaSaida", FWDbType.Int32));
            cmdCommand.Parameters["@OUT_Id_UsrLiberaSaida"].Direction = ParameterDirection.Output;

            cmdCommand.Parameters.Add(new FWParameter("@OUT_Id_Colaborador", FWDbType.Int32));
            cmdCommand.Parameters["@OUT_Id_Colaborador"].Direction = ParameterDirection.Output;

            cmdCommand.Parameters.Add(new FWParameter("@OUT_Dt_Escalacao", FWDbType.DateTime));
            cmdCommand.Parameters["@OUT_Dt_Escalacao"].Direction = ParameterDirection.Output;

            // Execucao
            AcessoColaborador objRetorno = new AcessoColaborador();

            conProvider.ExecuteScalar(cmdCommand);

            if (!(cmdCommand.Parameters["@OUT_Id_AcessoColEscalado"].Value == DBNull.Value))
                objRetorno.CodAcessoColEscalado = Convert.ToInt32(cmdCommand.Parameters["@OUT_Id_AcessoColEscalado"].Value);

            //Popula Objeto Lista
            if (!(cmdCommand.Parameters["@OUT_Id_Filial"].Value == DBNull.Value))
            {
                objRetorno.CodFilial = Convert.ToInt32(cmdCommand.Parameters["@OUT_Id_Filial"].Value);
            }
            if (!(cmdCommand.Parameters["@OUT_Id_Escalacao"].Value == DBNull.Value))
            {
                objRetorno.CodEscalacao = Convert.ToInt32(cmdCommand.Parameters["@OUT_Id_Escalacao"].Value);
            }
            if (!(cmdCommand.Parameters["@OUT_Dt_Entrada"].Value == DBNull.Value))
            {
                objRetorno.DataEntrada = Convert.ToDateTime(cmdCommand.Parameters["@OUT_Dt_Entrada"].Value);
            }
            if (!(cmdCommand.Parameters["@OUT_Dt_Saida"].Value == DBNull.Value))
            {
                objRetorno.DataSaida = Convert.ToDateTime(cmdCommand.Parameters["@OUT_Dt_Saida"].Value);
            }
            if (!(cmdCommand.Parameters["@OUT_Id_UsrLiberaEntrada"].Value == DBNull.Value))
            {
                objRetorno.CodUsuarioLiberaEntrada = Convert.ToInt32(cmdCommand.Parameters["@OUT_Id_UsrLiberaEntrada"].Value);
            }
            if (!(cmdCommand.Parameters["@OUT_Id_UsrLiberaSaida"].Value == DBNull.Value))
            {
                objRetorno.CodUsuarioLiberaSaida = Convert.ToInt32(cmdCommand.Parameters["@OUT_Id_UsrLiberaSaida"].Value);
            }
            if (!(cmdCommand.Parameters["@OUT_Id_Colaborador"].Value == DBNull.Value))
            {
                objRetorno.CodColaborador = Convert.ToInt32(cmdCommand.Parameters["@OUT_Id_Colaborador"].Value);
            }
            if (!(cmdCommand.Parameters["@OUT_Dt_Escalacao"].Value == DBNull.Value))
            {
                objRetorno.DataEscalacao = Convert.ToDateTime(cmdCommand.Parameters["@OUT_Dt_Escalacao"].Value);
            }

            return objRetorno;

        }
        #endregion       

        #region Obter Id Usuario

        /// <summary>
        /// Obtem registro da base
        /// </summary>
        /// <param name="pintIdColaborador">Código do Colaborador</param>
        /// <returns>Id do Usuário do framewoek</returns>
        /// <history>
        ///     [tgerevini] created 27/08/2010
        /// </history>
        public int ObterIdUsuario(int pintIdColaborador)
        {
            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_IdUsuario");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //parametros de entrada
            cmdCommand.Parameters.Add(new FWParameter("@Id_Colaborador", FWDbType.Varchar, 18));
            cmdCommand.Parameters["@Id_Colaborador"].Value = pintIdColaborador;

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

    }
}
