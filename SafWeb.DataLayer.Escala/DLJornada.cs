using System;
using System.Collections.Generic;
using System.Text;
using FrameWork.DataLayer.Utilitarios;
using SafWeb.Model.Colaborador;
using System.Collections.ObjectModel;
using SafWeb.Model.Escala;

namespace SafWeb.DataLayer.Escala
{
    /// ----------------------------------------------------------------------------- 
    /// Project : 
    /// Class : DLJornada
    /// ----------------------------------------------------------------------------- 
    /// <summary>  
    /// Implementação da classe DLJornada
    /// </summary> 
    /// <history> 
    ///     [cmarchi] created 15/1/2010 
    /// </history> 
    /// ----------------------------------------------------------------------------- 
    public class DLJornada : DALFWBase
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
        public DLJornada()
        {
            strConnection = DALUtilitarios.GetStringConnection(DALUtilitarios.ConexoesDisponiveis.ConnectionFW);
            conProvider = new DALProvider(strConnection);
            intCommandTimeOut = DALUtilitarios.ObterCommandTimeOut();
        }

        #endregion

        #region Inserir
        /// <summary>
        /// Insere os colaboradores de uma jornada.
        /// </summary>
        /// <param name="pobjJornadaColaboradores">objeto Colaborador</param>
        /// <history>
        ///     [cmarchi] created 15/1/2010
        /// </history>
        public void Inserir(JornadaColaboradores pobjJornadaColaboradores)
        {
            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_INS_Jornada");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Parâmetros de entrada
            cmdCommand.Parameters.Add(new FWParameter("@Id_Escalacao", FWDbType.Int32));
            cmdCommand.Parameters["@Id_Escalacao"].Value = pobjJornadaColaboradores.IdEscalacao;

            cmdCommand.Parameters.Add(new FWParameter("@Id_Jornada", FWDbType.Int32));
            cmdCommand.Parameters["@Id_Jornada"].Value = pobjJornadaColaboradores.IdJornada;

            cmdCommand.Parameters.Add(new FWParameter("@IdColaboradores", FWDbType.Varchar, 5000));
            cmdCommand.Parameters["@IdColaboradores"].Value = pobjJornadaColaboradores.CodigosColaboradores;

            //Executa a função no banco de dados 
            object objRetorno = conProvider.ExecuteScalar(cmdCommand);
        }
        #endregion

        #region Listar

        #region ListarJornadas
        /// <summary>
        /// Obtém Jornadas.
        /// </summary>
        /// <returns>Lista de Objetos Jornada</returns>
        /// <history>
        ///     [cmarchi] created 15/1/2010
        /// </history>
        public Collection<Jornada> ListarJornadas()
        {
            Collection<Jornada> colRetorno = null;

            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_Jornada");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Executa a função no banco de dados 
            System.Data.IDataReader idrRetorno;
            Jornada objRetorno = null;
            colRetorno = new Collection<Jornada>();

            idrRetorno = conProvider.ExecuteDataReader(cmdCommand);

            while (idrRetorno.Read())
            {
                objRetorno = new Jornada();
                objRetorno.FromIDataReader(idrRetorno);
                colRetorno.Add(objRetorno);
            }

            return colRetorno;
        }
        #endregion               

        #endregion

        #region Obter

        #region ObterColaboradoresNaoContemJornada
        /// <summary>
        /// Obtém Colaboradores da Escalação que ainda não possui uma Jornada.
        /// </summary>
        /// <param name="pintIdEscalacao">Id da Escalação</param>
        /// <returns>Lista de Objetos Colaborador</returns>
        /// <history>
        ///     [cmarchi] created 15/1/2010
        /// </history>
        public Collection<Colaborador> ObterColaboradoresNaoContemJornada(int pintIdEscalacao)
        {
            Collection<Colaborador> colRetorno = null;

            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_ObterEscalacaoColNJor");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Parâmetros de entrada
            cmdCommand.Parameters.Add(new FWParameter("@Id_Escalacao", FWDbType.Int32));
            cmdCommand.Parameters["@Id_Escalacao"].Value = pintIdEscalacao;

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

        #region ObterJornadaColaboradores
        /// <summary>
        /// Obtém Jornada dos Colaboradores da Escalação.
        /// </summary>
        /// <param name="pintIdEscalacao">Id da Escalação</param>
        /// <returns>Lista de Jornada dos Colaboradores</returns>
        /// <history>
        ///     [cmarchi] created 8/2/2010
        /// </history>
        public Collection<EscalacaoJornadaColaboradores> ObterJornadaColaboradores(int pintIdEscalacao)
        {
            Collection<EscalacaoJornadaColaboradores> colRetorno = null;

            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_ObterEscalacaoColJor");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Parâmetros de entrada
            cmdCommand.Parameters.Add(new FWParameter("@Id_Escalacao", FWDbType.Int32));
            cmdCommand.Parameters["@Id_Escalacao"].Value = pintIdEscalacao;

            //Executa a função no banco de dados 
            System.Data.IDataReader idrRetorno;
            EscalacaoJornadaColaboradores objRetorno = null;
            colRetorno = new Collection<EscalacaoJornadaColaboradores>();

            idrRetorno = conProvider.ExecuteDataReader(cmdCommand);

            while (idrRetorno.Read())
            {
                objRetorno = new EscalacaoJornadaColaboradores();
                objRetorno.FromIDataReader(idrRetorno);
                colRetorno.Add(objRetorno);
            }

            return colRetorno;
        }
        #endregion

        #endregion
    }
}
