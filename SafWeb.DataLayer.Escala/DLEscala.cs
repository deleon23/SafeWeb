using System;
using System.Collections.Generic;
using System.Text;
using SafWeb.Model.Escala;
using System.Collections.ObjectModel;
using SafWeb.Model.Colaborador;
using FrameWork.DataLayer.Utilitarios;
using System.Data;

namespace SafWeb.DataLayer.Escala
{
    /// ----------------------------------------------------------------------------- 
    /// Project : 
    /// Class : DLEscala
    /// ----------------------------------------------------------------------------- 
    /// <summary>  
    /// Implementação da classe DLEscala
    /// </summary> 
    /// <history> 
    ///     [cmarchi] created 4/1/2010 
    /// </history> 
    /// ----------------------------------------------------------------------------- 
    public class DLEscala : DALFWBase
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
        public DLEscala()
        {
            strConnection = DALUtilitarios.GetStringConnection(DALUtilitarios.ConexoesDisponiveis.ConnectionFW);
            conProvider = new DALProvider(strConnection);
            intCommandTimeOut = DALUtilitarios.ObterCommandTimeOut();
        }

        #endregion

        #region Importar Escalação do Ronda

        /// <summary>
        ///      Importa escalações do Ronda
        /// </summary>
        /// <param name="pintId_EscalDpto">Id da escala departamental</param>
        /// <param name="pdtDe_Periodo_ini">De - periodo inicial</param>
        /// <param name="pdtDe_Periodo_Fim">De - periodo final</param>
        /// <param name="pdtPara_Periodo_ini">Para - periodo inicial</param>
        /// <param name="pdtPara_Periodo_Fim">Para - periodo final</param>
        /// <param name="pintId_TipoSolicitacao">Id do tipo da solicitacao</param>
        /// <param name="pintUsu_N_Codigo">Usuário solicitante</param>
        /// <param name="pblnFlg_HoraExtra">Flg para hora extra</param>
        /// <history>
        ///      [haguiar] created 11/11/2011 11:44
        /// </history>
        /// 
        public int ImportarEscalacaoRonda(int pintId_EscalaDpto, 
            DateTime pdtDe_Periodo_ini, DateTime pdtDe_Periodo_Fim,
            DateTime pdtPara_Periodo_ini, DateTime pdtPara_Periodo_Fim,
            int pintId_TipoSolicitacao, int pintUsu_N_Codigo, bool pblnFlg_HoraExtra)
        {
            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_INS_RONDA_IMPORTAR_ESCALACAO");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            cmdCommand.Parameters.Add(new FWParameter("@ID_ESCALADPTO", FWDbType.Int32));
            cmdCommand.Parameters["@ID_ESCALADPTO"].Value = pintId_EscalaDpto;

            //periodo de
            cmdCommand.Parameters.Add(new FWParameter("@DE_PERIODO_INI", FWDbType.DateTime));
            cmdCommand.Parameters["@DE_PERIODO_INI"].Value = pdtDe_Periodo_ini;

            cmdCommand.Parameters.Add(new FWParameter("@DE_PERIODO_FIM", FWDbType.DateTime));
            cmdCommand.Parameters["@DE_PERIODO_FIM"].Value = pdtDe_Periodo_Fim;

            //periodo para
            cmdCommand.Parameters.Add(new FWParameter("@PARA_PERIODO_INI", FWDbType.DateTime));
            cmdCommand.Parameters["@PARA_PERIODO_INI"].Value = pdtPara_Periodo_ini;

            cmdCommand.Parameters.Add(new FWParameter("@PARA_PERIODO_FIM", FWDbType.DateTime));
            cmdCommand.Parameters["@PARA_PERIODO_FIM"].Value = pdtPara_Periodo_Fim;

            cmdCommand.Parameters.Add(new FWParameter("@Id_TipoSolicitacao", FWDbType.Int32));
            cmdCommand.Parameters["@Id_TipoSolicitacao"].Value = pintId_TipoSolicitacao;

            cmdCommand.Parameters.Add(new FWParameter("@USU_N_CODIGO", FWDbType.Int32));
            cmdCommand.Parameters["@USU_N_CODIGO"].Value = pintUsu_N_Codigo;

            cmdCommand.Parameters.Add(new FWParameter("@FLG_HORAEXTRA", FWDbType.Boolean));
            cmdCommand.Parameters["@FLG_HORAEXTRA"].Value = pblnFlg_HoraExtra;

            //Executa a função no banco de dados 
            object objRetorno = conProvider.ExecuteScalar(cmdCommand);
            return Convert.ToInt32(objRetorno);
        }
        #endregion

        #region Importar Escalação CREW

        /// <summary>
        ///      Importar as Escalações do CREW
        /// </summary>
        /// <param name="pintIdEscalacao">Id das Escalações</param>
        /// <param name="pintUsuarioAprovador">Id do Usuário Aprovador</param>
        /// <history>
        ///      [haguiar_SDM9004] created 12/08/2011 17:50
        /// </history>
        public int ImportarEscalacaoCREW(int pintIdEscalacao, int pintIdUsuarioAprovador, bool pblnFlg_HoraExtra)
        {
            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_INS_CREW_IMPORTAR_ESCALACAO");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Parâmetros de entrada
            if (pintIdEscalacao > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Id_Escalacao_TEMP", FWDbType.Int32));
                cmdCommand.Parameters["@Id_Escalacao_TEMP"].Value = pintIdEscalacao;
            }

            if (pintIdUsuarioAprovador > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@USU_N_CODIGO", FWDbType.Int32));
                cmdCommand.Parameters["@USU_N_CODIGO"].Value = pintIdUsuarioAprovador;
            }

            cmdCommand.Parameters.Add(new FWParameter("@ID_ESCALACAO_NOVO", FWDbType.Int32));
            cmdCommand.Parameters["@ID_ESCALACAO_NOVO"].Direction = ParameterDirection.Output;

            cmdCommand.Parameters.Add(new FWParameter("@FLG_HORAEXTRA", FWDbType.Boolean));
            cmdCommand.Parameters["@FLG_HORAEXTRA"].Value = pblnFlg_HoraExtra;

            //Executa a função no banco de dados 
            object objRetorno = conProvider.ExecuteScalar(cmdCommand);
            return Convert.ToInt32(objRetorno);
        }
        #endregion

        #region Aprovar Escalação

        /// <summary>
        ///      Aprovar as Escalações
        /// </summary>
        /// <param name="pintIdEscalacao">Id das Escalações</param>
        /// <param name="pintUsuarioAprovador">Id do Usuário Aprovador</param>
        /// <history>
        ///      [cmarchi] created 28/01/2010
        /// </history>
        public void AprovarEscalacao(int pintIdEscalacao, int pintIdUsuarioAprovador)
        {
            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_UPD_AprovEscalacao");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Parâmetros de entrada
            if (pintIdEscalacao > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Id_Escalacao", FWDbType.Int32));
                cmdCommand.Parameters["@Id_Escalacao"].Value = pintIdEscalacao;
            }

            if (pintIdUsuarioAprovador > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Id_UsuarioAprovador", FWDbType.Int32));
                cmdCommand.Parameters["@Id_UsuarioAprovador"].Value = pintIdUsuarioAprovador;
            }

            //Executa a função no banco de dados 
            conProvider.ExecuteNonQuery(cmdCommand);
        }
        #endregion

        #region Alterar

        #region Deleta Horario dos Colaboradores

        public void DeletaHorarioColaboradores(EscalacaoColaboradoresData objEscColData)
        {
            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_DEL_ESCALACAOCOLDATA");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Parâmetros de entrada
            cmdCommand.Parameters.Add(new FWParameter("@Id_Escalacao", FWDbType.Int32));
            cmdCommand.Parameters["@Id_Escalacao"].Value = objEscColData.IdEscalacao;

            cmdCommand.Parameters.Add(new FWParameter("@Id_Colaboradores", FWDbType.Varchar, 5000));
            cmdCommand.Parameters["@Id_Colaboradores"].Value = objEscColData.IdColaboradores;

            //Executa a função no banco de dados 
            conProvider.ExecuteScalar(cmdCommand);
        }

        #endregion

        #region Alterar Horario dos Colaboradores
        /// <summary>
        /// Altera os horários e datas dos colaboradores
        /// </summary>
        /// <param name="EscalacaoColaboradoresData">Objeto Escalação dos colaboradores</param>
        /// <history>
        ///     [cmarchi] created 20/1/2010
        ///     [haguiar_2] modify 02/12/2010
        ///     excluir troca de horários
        ///     [haguiar_2] modify 04/12/2010
        ///     alterar horário flex
        ///     [haguiar_8829] modify 06/07/2011 14:52
        ///     incluir flg_horaextra
        ///     [haguiar] modify 02/01/2012 15:12
        ///     adicionar codigo legado
        /// </history>
        public void AlterarHorarioColaboradores(EscalacaoColaboradoresData objEscColData)
        {
            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_UPD_EscalacaoColData");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Parâmetros de entrada
            cmdCommand.Parameters.Add(new FWParameter("@Id_Escalacao", FWDbType.Int32));
            cmdCommand.Parameters["@Id_Escalacao"].Value = objEscColData.IdEscalacao;

            cmdCommand.Parameters.Add(new FWParameter("@Id_Colaboradores", FWDbType.Varchar, 5000));
            cmdCommand.Parameters["@Id_Colaboradores"].Value = objEscColData.IdColaboradores;

            cmdCommand.Parameters.Add(new FWParameter("@Dat_DataHorario", FWDbType.DateTime));
            cmdCommand.Parameters["@Dat_DataHorario"].Value = objEscColData.DataEscalacao;

            cmdCommand.Parameters.Add(new FWParameter("@Flg_Compensado", FWDbType.Boolean));
            cmdCommand.Parameters["@Flg_Compensado"].Value = objEscColData.Compensado;

            cmdCommand.Parameters.Add(new FWParameter("@Flg_Licenca", FWDbType.Boolean));
            cmdCommand.Parameters["@Flg_Licenca"].Value = objEscColData.Licenca;

            cmdCommand.Parameters.Add(new FWParameter("@Flg_Folga", FWDbType.Boolean));
            cmdCommand.Parameters["@Flg_Folga"].Value = objEscColData.Folga;

            cmdCommand.Parameters.Add(new FWParameter("@Flg_ExcluirHorario", FWDbType.Boolean));
            cmdCommand.Parameters["@Flg_ExcluirHorario"].Value = objEscColData.ExcluirTrocaHorario;

            cmdCommand.Parameters.Add(new FWParameter("@Flg_Flex", FWDbType.Boolean));
            cmdCommand.Parameters["@Flg_Flex"].Value = objEscColData.HorarioFlex;

            cmdCommand.Parameters.Add(new FWParameter("@Flg_HoraExtra", FWDbType.Boolean));
            cmdCommand.Parameters["@Flg_HoraExtra"].Value = objEscColData.HoraExtra;

            cmdCommand.Parameters.Add(new FWParameter("@COD_LEGADO", FWDbType.Int32));
            cmdCommand.Parameters["@COD_LEGADO"].Value = objEscColData.CodLegado;

            //Executa a função no banco de dados 
            conProvider.ExecuteScalar(cmdCommand);
        }
        #endregion

        #region AlterarSituacao
        /// <summary>
        /// Altera a Situação da Escalação.
        /// </summary>
        /// <param name="pobjEscalacao">Objeto Escalacao</param>
        ///<param name="pblnSituacao">Situação</param>
        /// <param name="pintIdUsuarioSolicitante">Id do Usuário Solicitante</param>
        /// <returns>Id_EscalaDepartamental</returns>
        /// <history>
        ///     [cmarchi] created 13/1/2010
        ///     [haguiar] modify 26/09/2011 15:25
        ///     retornar se o registro foi ativado ou nao
        /// </history>
        public int AlterarSituacao(int pintIdEscalacao, bool pblnSituacao, int pintIdUsuarioSolicitante)
        {
            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_UPD_StatusEscalacao");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Parâmetros de entrada
            cmdCommand.Parameters.Add(new FWParameter("@Id_Escalacao", FWDbType.Int32));
            cmdCommand.Parameters["@Id_Escalacao"].Value = pintIdEscalacao;

            cmdCommand.Parameters.Add(new FWParameter("@Flg_Situacao", FWDbType.Boolean));
            cmdCommand.Parameters["@Flg_Situacao"].Value = pblnSituacao;

            cmdCommand.Parameters.Add(new FWParameter("@Id_UsuarioSolicitante", FWDbType.Int32));
            cmdCommand.Parameters["@Id_UsuarioSolicitante"].Value = pintIdUsuarioSolicitante;

            //Executa a função no banco de dados 
            object objRetorno = conProvider.ExecuteScalar(cmdCommand);

            return Convert.ToInt32(objRetorno);
        }


        /// <summary>
        /// Exclui status pendente da Escalação.
        /// </summary>
        /// <param name="pintIdEscalacao">Id Escalacao</param>
        /// <history>
        ///     [haguiar] created 29/10/2010
        ///     [cfrancisco] created 23/08/2012
        /// </history>
        public void ExcluirStatusPendente(int pintIdEscalacao, int pintUsuario)
        {
            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("[SP_SAF_DEL_ESCALASTATUSAPROVACAOPENDENTE]");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Parâmetros de entrada
            cmdCommand.Parameters.Add(new FWParameter("@Id_Escalacao", FWDbType.Int32));
            cmdCommand.Parameters["@Id_Escalacao"].Value = pintIdEscalacao;
            
            //Parâmetros de entrada
            cmdCommand.Parameters.Add(new FWParameter("@ID_USUARIOSOLICITANTE", FWDbType.Int32));
            cmdCommand.Parameters["@ID_USUARIOSOLICITANTE"].Value = pintUsuario;
            
            //Executa a função no banco de dados 
            conProvider.ExecuteDataReader(cmdCommand);
        }
        #endregion

        #region AlterarSituacaoCodEscala
        /// <summary>
        /// Altera a Situação do código do horário da escala.
        /// </summary>
        /// <param name="pintIdEscala">Id Escala</param>
        ///<param name="pblnSituacao">Situação</param>
        /// <returns>Id Escala</returns>
        /// <history>
        ///     [haguiar] created 06/01/2012 14:32
        ///     retornar se o registro foi ativado ou nao
        /// </history>
        public int AlterarSituacaoCodEscala(int pintIdEscala, bool pblnSituacao)
        {
            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_UPD_SITUACAOCODESCALA");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Parâmetros de entrada
            cmdCommand.Parameters.Add(new FWParameter("@Id_Escala", FWDbType.Int32));
            cmdCommand.Parameters["@Id_Escala"].Value = pintIdEscala;

            cmdCommand.Parameters.Add(new FWParameter("@Flg_Situacao", FWDbType.Boolean));
            cmdCommand.Parameters["@Flg_Situacao"].Value = pblnSituacao;

            //Executa a função no banco de dados 
            object objRetorno = conProvider.ExecuteScalar(cmdCommand);

            return Convert.ToInt32(objRetorno);
        }
        #endregion

        #region AlterarSituacaoCodHorario
        /// <summary>
        /// Altera a Situação do código do horário da escala.
        /// </summary>
        /// <param name="pintIdHorario">Id Horario</param>
        ///<param name="pblnSituacao">Situação</param>
        /// <returns>Id Horario</returns>
        /// <history>
        ///     [haguiar] created 10/01/2012 10:45
        ///     retornar se o registro foi ativado ou nao
        /// </history>
        public int AlterarSituacaoCodHorario(int pintIdHorario, bool pblnSituacao)
        {
            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_UPD_SITUACAOCODHORARIO");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Parâmetros de entrada
            cmdCommand.Parameters.Add(new FWParameter("@Id_Horario", FWDbType.Int32));
            cmdCommand.Parameters["@Id_Horario"].Value = pintIdHorario;

            cmdCommand.Parameters.Add(new FWParameter("@Flg_Situacao", FWDbType.Boolean));
            cmdCommand.Parameters["@Flg_Situacao"].Value = pblnSituacao;

            //Executa a função no banco de dados 
            object objRetorno = conProvider.ExecuteScalar(cmdCommand);

            return Convert.ToInt32(objRetorno);
        }
        #endregion

        #region AlterarHorarioEscala
        /// <summary>
        /// Altera o horário de um código de escala Ronda
        /// </summary>
        /// <param name="pobjHorarioLegado">Objeto HorarioLegado</param>
        /// <returns>Id Escala</returns>
        /// <history>
        ///     [haguiar] created 09/01/2012 09:55
        /// </history>
        public int AlterarHorarioEscala(HorarioLegado pobjHorarioLegado)
        {
            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_UPD_CODESCALA");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Parâmetros de entrada
            cmdCommand.Parameters.Add(new FWParameter("@Id_Escala", FWDbType.Int32));
            cmdCommand.Parameters["@Id_Escala"].Value = pobjHorarioLegado.IdEscala;

            //Parâmetros de entrada
            cmdCommand.Parameters.Add(new FWParameter("@DES_ESCALA", FWDbType.Varchar));
            cmdCommand.Parameters["@DES_ESCALA"].Value = pobjHorarioLegado.DesEscala;

            //Parâmetros de entrada
            cmdCommand.Parameters.Add(new FWParameter("@ID_JORNADA", FWDbType.Int32));
            cmdCommand.Parameters["@ID_JORNADA"].Value = pobjHorarioLegado.IdJornada;

            //Parâmetros de entrada
            cmdCommand.Parameters.Add(new FWParameter("@HR_ENTRADA", FWDbType.DateTime));
            cmdCommand.Parameters["@HR_ENTRADA"].Value = Convert.ToDateTime("1900-01-01 " + pobjHorarioLegado.HrEntrada);

            //Parâmetros de entrada
            cmdCommand.Parameters.Add(new FWParameter("@DUR_REFEICAO", FWDbType.DateTime));
            cmdCommand.Parameters["@DUR_REFEICAO"].Value = Convert.ToDateTime("1900-01-01 " + pobjHorarioLegado.DurRefeicao); ;

            //Parâmetros de entrada
            cmdCommand.Parameters.Add(new FWParameter("@COD_ESCALALEGADO", FWDbType.Int32));
            cmdCommand.Parameters["@COD_ESCALALEGADO"].Value = pobjHorarioLegado.CodLegado;

            //Executa a função no banco de dados 
            object objRetorno = conProvider.ExecuteScalar(cmdCommand);

            return Convert.ToInt32(objRetorno);
        }
        #endregion

        #region Alterar Registro Código Horario
        /// <summary>
        /// Altera o registro de um código de horário Ronda
        /// </summary>
        /// <param name="pobjHorarioLegado">Objeto HorarioLegado</param>
        /// <returns>Id Horario</returns>
        /// <history>
        ///     [haguiar] created 10/01/2012 10:49
        /// </history>
        public int AlterarRegistroHorario(HorarioLegado pobjHorarioLegado)
        {
            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_UPD_CODHORARIO");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Parâmetros de entrada
            cmdCommand.Parameters.Add(new FWParameter("@Id_Horario", FWDbType.Int32));
            cmdCommand.Parameters["@Id_Horario"].Value = pobjHorarioLegado.IdEscala;

            //Parâmetros de entrada
            cmdCommand.Parameters.Add(new FWParameter("@DES_HORARIO", FWDbType.Varchar));
            cmdCommand.Parameters["@DES_HORARIO"].Value = pobjHorarioLegado.DesEscala;

            //Parâmetros de entrada
            cmdCommand.Parameters.Add(new FWParameter("@HR_ENTRADA", FWDbType.DateTime));
            cmdCommand.Parameters["@HR_ENTRADA"].Value = Convert.ToDateTime("1900-01-01 " + pobjHorarioLegado.HrEntrada);

            //Parâmetros de entrada
            cmdCommand.Parameters.Add(new FWParameter("@COD_HORARIOLEGADO", FWDbType.Int32));
            cmdCommand.Parameters["@COD_HORARIOLEGADO"].Value = pobjHorarioLegado.CodLegado;

            //Parâmetros de entrada
            cmdCommand.Parameters.Add(new FWParameter("@ID_JORNADA", FWDbType.Int32));
            cmdCommand.Parameters["@ID_JORNADA"].Value = pobjHorarioLegado.IdJornada;

            //Executa a função no banco de dados 
            object objRetorno = conProvider.ExecuteScalar(cmdCommand);

            return Convert.ToInt32(objRetorno);
        }
        #endregion
        #endregion

        #region Editar Colaboradores da Escala
        /// <summary>
        /// Edita os Colaboradores da Escala.
        /// </summary>
        /// <param name="pobjEscalacao">Objeto Escalacao</param>
        /// <param name="pstrColaboradores">Lista de Colaboradores</param>
        /// <returns>Id_EscalaDepartamental</returns>
        /// <history>
        ///     [cmarchi] created 13/1/2010
        /// </history>
        public int Editar(Escalacao pobjEscalacao, string pstrColaboradores)
        {
            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_UPD_EscalaDptoCol");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Parâmetros de entrada
            cmdCommand.Parameters.Add(new FWParameter("@Id_Escalacao", FWDbType.Int32));
            cmdCommand.Parameters["@Id_Escalacao"].Value = pobjEscalacao.IdEscalacao;

            cmdCommand.Parameters.Add(new FWParameter("@Id_UsuarioSolicitante", FWDbType.Int32));
            cmdCommand.Parameters["@Id_UsuarioSolicitante"].Value = pobjEscalacao.IdUsuarioSolicitante;

            cmdCommand.Parameters.Add(new FWParameter("@Id_EscalaDpto", FWDbType.Int32));
            cmdCommand.Parameters["@Id_EscalaDpto"].Value = pobjEscalacao.IdEscalaDepartamental;

            cmdCommand.Parameters.Add(new FWParameter("@Colaboradores", FWDbType.Varchar, 5000));
            cmdCommand.Parameters["@Colaboradores"].Value = pstrColaboradores;

            //Executa a função no banco de dados 
            object objRetorno = conProvider.ExecuteScalar(cmdCommand);
            return Convert.ToInt32(objRetorno);
        }
        #endregion

        #region Inserir

        #region Insere Horário de Escala
        /// <summary>
        /// Insere o código de um horário Ronda
        /// </summary>
        /// <param name="pobjHorarioLegado">Objeto HorarioLegado</param>
        /// <returns>Id Escala</returns>
        /// <history>
        ///     [haguiar] created 12/01/2012 09:15
        /// </history>
        public int InsereCodigoHorario(HorarioLegado pobjHorarioLegado)
        {
            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_INS_CODHORARIO");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Parâmetros de entrada
            cmdCommand.Parameters.Add(new FWParameter("@DES_HORARIO", FWDbType.Varchar));
            cmdCommand.Parameters["@DES_HORARIO"].Value = pobjHorarioLegado.DesEscala;

            //Parâmetros de entrada
            cmdCommand.Parameters.Add(new FWParameter("@HR_ENTRADA", FWDbType.Varchar));
            cmdCommand.Parameters["@HR_ENTRADA"].Value = pobjHorarioLegado.HrEntrada;

            //Parâmetros de entrada
            cmdCommand.Parameters.Add(new FWParameter("@COD_HORARIOLEGADO", FWDbType.Int32));
            cmdCommand.Parameters["@COD_HORARIOLEGADO"].Value = pobjHorarioLegado.CodLegado;

            //Parâmetros de entrada
            cmdCommand.Parameters.Add(new FWParameter("@ID_JORNADA", FWDbType.Int32));
            cmdCommand.Parameters["@ID_JORNADA"].Value = pobjHorarioLegado.IdJornada;


            //Executa a função no banco de dados 
            object objRetorno = conProvider.ExecuteScalar(cmdCommand);

            return Convert.ToInt32(objRetorno);
        }
        #endregion

        #region Insere Código de Escala
        /// <summary>
        /// Insere o código de uma escala Ronda
        /// </summary>
        /// <param name="pobjHorarioLegado">Objeto HorarioLegado</param>
        /// <returns>Id Escala</returns>
        /// <history>
        ///     [haguiar] created 11/01/2012 16:45
        /// </history>
        public int InsereCodigoEscala(HorarioLegado pobjHorarioLegado)
        {
            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_INS_CODESCALA");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Parâmetros de entrada
            cmdCommand.Parameters.Add(new FWParameter("@DES_ESCALA", FWDbType.Varchar));
            cmdCommand.Parameters["@DES_ESCALA"].Value = pobjHorarioLegado.DesEscala;

            //Parâmetros de entrada
            cmdCommand.Parameters.Add(new FWParameter("@ID_JORNADA", FWDbType.Int32));
            cmdCommand.Parameters["@ID_JORNADA"].Value = pobjHorarioLegado.IdJornada;

            //Parâmetros de entrada
            cmdCommand.Parameters.Add(new FWParameter("@HR_ENTRADA", FWDbType.DateTime));
            cmdCommand.Parameters["@HR_ENTRADA"].Value = Convert.ToDateTime("1900-01-01 " + pobjHorarioLegado.HrEntrada);

            //Parâmetros de entrada
            cmdCommand.Parameters.Add(new FWParameter("@DUR_REFEICAO", FWDbType.DateTime));
            cmdCommand.Parameters["@DUR_REFEICAO"].Value = Convert.ToDateTime("1900-01-01 " + pobjHorarioLegado.DurRefeicao); ;

            //Parâmetros de entrada
            cmdCommand.Parameters.Add(new FWParameter("@COD_ESCALALEGADO", FWDbType.Int32));
            cmdCommand.Parameters["@COD_ESCALALEGADO"].Value = pobjHorarioLegado.CodLegado;

            //Executa a função no banco de dados 
            object objRetorno = conProvider.ExecuteScalar(cmdCommand);

            return Convert.ToInt32(objRetorno);
        }
        #endregion

        #region Inserir Aprovação da Escalação

        /// <summary>
        ///      Aprovar a Escalação
        /// </summary>
        /// <param name="escalaAprovacao">Objeto Aprovação Escalação</param>
        /// <history>
        ///      [cmarchi] created 28/01/2010
        /// </history>
        public void InserirEscalaAprovacao(EscalaAprovacao escalaAprovacao, int idUsuarioSolicitante)
        {
            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_INS_AprovEscalacao");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            cmdCommand.Parameters.Add(new FWParameter("@Id_Escalacao", FWDbType.Int32));
            cmdCommand.Parameters["@Id_Escalacao"].Value = escalaAprovacao.IdEscalacao;

            cmdCommand.Parameters.Add(new FWParameter("@Id_StatusSolicitacao", FWDbType.Int32));
            cmdCommand.Parameters["@Id_StatusSolicitacao"].Value = escalaAprovacao.IdStatusSolicitacao;

            cmdCommand.Parameters.Add(new FWParameter("@Id_UsuarioAprovador", FWDbType.Int32));
            cmdCommand.Parameters["@Id_UsuarioAprovador"].Value = escalaAprovacao.IdUsuarioAprovador;

            cmdCommand.Parameters.Add(new FWParameter("@ID_USUARIOSOLICITANTE", FWDbType.Int32));
            cmdCommand.Parameters["@ID_USUARIOSOLICITANTE"].Value = idUsuarioSolicitante;
            

            //Executa a função no banco de dados 
            conProvider.ExecuteScalar(cmdCommand);
        }
        #endregion

        #region Inserir Colaboradores da Escala
        /// <summary>
        /// Insere Colaboradores da Escala e a Escala.
        /// </summary>
        /// <param name="pobjEscalacao">Objeto Escalacao</param>
        /// <param name="pstrColaboradores">Lista de Colaboradores</param>
        /// <returns>Id_EscalaDepartamental</returns>
        /// <history>
        ///     [cmarchi] created 7/1/2010
        ///     [cmarchi] modify 21/1/2010
        ///     [haguiar_2] modify 24/11/2010
        ///     permitir inclusao de escala com qualquer tiposolicitacao (troca de horario)
        ///     [haguiar SDM9004] modify 28/07/2011 17:04
        ///     incluir parametro Id_Escalacao OUTPUT
        /// </history>
        public int Inserir(Escalacao pobjEscalacao, string pstrColaboradores)
        {
            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_INS_ESCALACAO");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Parâmetros de entrada
            cmdCommand.Parameters.Add(new FWParameter("@Id_UsuarioSolicitante", FWDbType.Int32));
            cmdCommand.Parameters["@Id_UsuarioSolicitante"].Value = pobjEscalacao.IdUsuarioSolicitante;

            cmdCommand.Parameters.Add(new FWParameter("@Id_EscalaDpto", FWDbType.Int32));
            cmdCommand.Parameters["@Id_EscalaDpto"].Value = pobjEscalacao.IdEscalaDepartamental;

            cmdCommand.Parameters.Add(new FWParameter("@Id_TipoSolicitacao", FWDbType.Int32));
            cmdCommand.Parameters["@Id_TipoSolicitacao"].Value = pobjEscalacao.IdTipoSolicitacao;

            cmdCommand.Parameters.Add(new FWParameter("@Dt_DataInicio", FWDbType.DateTime));
            cmdCommand.Parameters["@Dt_DataInicio"].Value = pobjEscalacao.DataInicioPeriodo;

            cmdCommand.Parameters.Add(new FWParameter("@Dt_DataFinal", FWDbType.DateTime));
            cmdCommand.Parameters["@Dt_DataFinal"].Value = pobjEscalacao.DataFinalPeriodo;

            cmdCommand.Parameters.Add(new FWParameter("@Colaboradores", FWDbType.Varchar, 5000));
            cmdCommand.Parameters["@Colaboradores"].Value = pstrColaboradores;
            
            cmdCommand.Parameters.Add(new FWParameter("@Id_Escalacao", FWDbType.Int32));
            cmdCommand.Parameters["@Id_Escalacao"].Direction = ParameterDirection.Output;
            

            //Executa a função no banco de dados 
            object objRetorno = conProvider.ExecuteScalar(cmdCommand);
            return Convert.ToInt32(objRetorno);
        }
        #endregion

        #region Inserir Horario dos Colaboradores
        /// <summary>
        /// Insere os horários e datas dos colaboradores
        /// </summary>
        /// <param name="EscalacaoColaboradoresData">Objeto Escalação dos colaboradores</param>
        /// <history>
        ///     [cmarchi] created 20/1/2010
        ///     [haguiar_2] modify 04/12/2010
        ///     incluir horario flex
        ///     [haguiar_8829] modify 06/07/2011 14:52
        ///     incluir flg_horaextra
        ///     [haguiar] modify 02/12/2012 17:79
        ///     incluir codlegado
        /// </history>
        public void InserirHorarioColaboradores(EscalacaoColaboradoresData objEscColData)
        {
            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_INS_EscalacaoColData");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Parâmetros de entrada
            cmdCommand.Parameters.Add(new FWParameter("@Id_Escalacao", FWDbType.Int32));
            cmdCommand.Parameters["@Id_Escalacao"].Value = objEscColData.IdEscalacao;

            cmdCommand.Parameters.Add(new FWParameter("@Id_Colaboradores", FWDbType.Varchar, 5000));
            cmdCommand.Parameters["@Id_Colaboradores"].Value = objEscColData.IdColaboradores;

            cmdCommand.Parameters.Add(new FWParameter("@Dat_DataHorario", FWDbType.DateTime));
            cmdCommand.Parameters["@Dat_DataHorario"].Value = objEscColData.DataEscalacao;

            cmdCommand.Parameters.Add(new FWParameter("@Flg_Compensado", FWDbType.Boolean));
            cmdCommand.Parameters["@Flg_Compensado"].Value = objEscColData.Compensado;

            cmdCommand.Parameters.Add(new FWParameter("@Flg_Licenca", FWDbType.Boolean));
            cmdCommand.Parameters["@Flg_Licenca"].Value = objEscColData.Licenca;

            cmdCommand.Parameters.Add(new FWParameter("@Flg_Folga", FWDbType.Boolean));
            cmdCommand.Parameters["@Flg_Folga"].Value = objEscColData.Folga;

            cmdCommand.Parameters.Add(new FWParameter("@Flg_Flex", FWDbType.Boolean));
            cmdCommand.Parameters["@Flg_Flex"].Value = objEscColData.HorarioFlex;

            cmdCommand.Parameters.Add(new FWParameter("@Flg_HoraExtra", FWDbType.Boolean));
            cmdCommand.Parameters["@Flg_HoraExtra"].Value = objEscColData.HoraExtra;

            cmdCommand.Parameters.Add(new FWParameter("@Cod_Legado", FWDbType.Int32));
            cmdCommand.Parameters["@Cod_Legado"].Value = objEscColData.CodLegado;

            cmdCommand.Parameters.Add(new FWParameter("@Flg_InicioFolga", FWDbType.Boolean));
            cmdCommand.Parameters["@Flg_InicioFolga"].Value = objEscColData.flgIniciaFolgando;
            

            //Executa a função no banco de dados 
            conProvider.ExecuteScalar(cmdCommand);
        }
        #endregion

        #endregion

        #region Listar

        #region Listar Horários de colaboradores do CREW

        /// <summary>
        ///      Lista Horários de colaboradores do CREW
        /// </summary>
        /// <param name="pintUSU_N_Codigo">Código do Usuário logado</param>
        /// <param name="pintId_TipoSolicitacao">Tipo da solicitacao</param>
        /// <param name="pdatDataSelecao">Data da selecao</param>
        /// <returns>Lista horários de colaboradores CREW para importação</returns>
        /// <history>
        ///      [haguiar] created 22/03/2012 09:47
        /// </history>
        public Collection<EscalaColaboradoresCrew> ListarHorarioColaboradoresCrew(int pintUSU_N_Codigo, int pintId_TipoSolicitacao, DateTime pdatDataSelecao)
        {
            Collection<EscalaColaboradoresCrew> colRetorno = null;

            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_CREW_ESCALACAO");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Parâmetros de entrada
            if (pintUSU_N_Codigo > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@USU_N_CODIGO", FWDbType.Int32));
                cmdCommand.Parameters["@USU_N_CODIGO"].Value = pintUSU_N_Codigo;
            }

            if (pintId_TipoSolicitacao > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@ID_TIPOSOLICITACAO", FWDbType.Int32));
                cmdCommand.Parameters["@ID_TIPOSOLICITACAO"].Value = pintId_TipoSolicitacao;
            }

            cmdCommand.Parameters.Add(new FWParameter("@DATASELECAO", FWDbType.DateTime));
            cmdCommand.Parameters["@DATASELECAO"].Value = pdatDataSelecao;

            //Executa a função no banco de dados 
            System.Data.IDataReader idrRetorno;
            EscalaColaboradoresCrew objRetorno = null;
            colRetorno = new Collection<EscalaColaboradoresCrew>();

            idrRetorno = conProvider.ExecuteDataReader(cmdCommand);

            while (idrRetorno.Read())
            {
                objRetorno = new EscalaColaboradoresCrew();
                objRetorno.FromIDataReader(idrRetorno);
                colRetorno.Add(objRetorno);
            }

            idrRetorno.Close();
            idrRetorno.Dispose();

            return colRetorno;
        }

        public void CrwOperacaoLog(int pintUSU_N_Codigo, DateTime pdatDataSelecao, int pintIDEscalacao)
        {
            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_INS_CRWOPERACAOLOG");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

 
            cmdCommand.Parameters.Add(new FWParameter("@USU_N_CODIGO", FWDbType.Int32));
            cmdCommand.Parameters["@USU_N_CODIGO"].Value = pintUSU_N_Codigo;
            
            cmdCommand.Parameters.Add(new FWParameter("@DATASELECAO", FWDbType.DateTime));
            cmdCommand.Parameters["@DATASELECAO"].Value = pdatDataSelecao;

            cmdCommand.Parameters.Add(new FWParameter("@IDESCALACAO", FWDbType.Int32));
            cmdCommand.Parameters["@IDESCALACAO"].Value = pintIDEscalacao;

            //Executa a função no banco de dados 
            conProvider.ExecuteNonQuery(cmdCommand);
        }
        #endregion

        #region Listar CREW

        /// <summary>
        ///      Lista Escalas para importação do CREW
        /// </summary>
        /// <param name="pdecCodUsuAprov">Código do Usuário logado</param>
        /// <returns>Lista Escalas para importação</returns>
        /// <history>
        ///      [haguiar SDM 9004] created 10/08/2011 14:41
        /// </history>
        public Collection<EscalaImportarCrew> ListarEscalasImportarCREW(decimal pdecCodUsuAprov)
        {
            Collection<EscalaImportarCrew> colRetorno = null;

            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_CREW_PERIODOS");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Parâmetros de entrada
            if (pdecCodUsuAprov > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@USU_N_CODIGO", FWDbType.Int32));
                cmdCommand.Parameters["@USU_N_CODIGO"].Value = pdecCodUsuAprov;
            }

            //Executa a função no banco de dados 
            System.Data.IDataReader idrRetorno;
            EscalaImportarCrew objRetorno = null;
            colRetorno = new Collection<EscalaImportarCrew>();

            idrRetorno = conProvider.ExecuteDataReader(cmdCommand);

            while (idrRetorno.Read())
            {
                objRetorno = new EscalaImportarCrew();
                objRetorno.FromIDataReader(idrRetorno);
                colRetorno.Add(objRetorno);
            }

            return colRetorno;
        }
        #endregion

        #region Listar Escalas
        /// <summary>
        ///     Lista todas as Escalas de acordo com os parâmetros.
        /// </summary>
        /// <param name="pintNumeroEscala">Número da Escala</param>
        /// <param name="pintEscalaDepartamental">Código da Escala Departamental</param>
        /// <param name="pintRegional">Código da Regional</param>
        /// <param name="pintFilial">Código da Filial</param>
        /// <param name="pstrSolicitante">Nome do Solicitante</param>
        /// <param name="pstrColaborador">Nome do Colaborador</param>
        /// <param name="pstrAprovador">Nome do Aprovador</param>
        /// <param name="pintStatus">Código do Status</param>
        /// <param name="pintTipoSolicitacao">Código do Tipo da Solicitação</param>
        /// <param name="pdatDataInicio">Data de Início</param>
        /// <param name="pdatDataFinal">Data Final</param>
        /// <param name="intIdUsuarioSolicitante">Id do Usuário Solicitante</param>
        /// <returns>Lista Contendo as Escalas</returns>
        /// <history>
        ///     [cmarchi] created 11/1/2010
        ///     [cmarchi] modify 29/1/2010
        ///     [haguiar] modify 04/11/2010
        /// </history>
        public DataTable Listar(int pintNumeroEscala, int pintEscalaDepartamental, int pintRegional,
            int pintFilial, string pstrSolicitante, string pstrColaborador,
            string pstrAprovador, int pintStatus, int pintTipoSolicitacao, DateTime? pdatDataInicio,
            DateTime? pdatDataFinal, int intIdUsuarioSolicitante)
        {
            DataTable dttRetorno;

            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_Escalacao");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Parâmetros de entrada
            if (pintNumeroEscala > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Numero_Escala", FWDbType.Int32));
                cmdCommand.Parameters["@Numero_Escala"].Value = pintNumeroEscala;
            }

            if (pintEscalaDepartamental > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Id_EscalaDepartamental", FWDbType.Int32));
                cmdCommand.Parameters["@Id_EscalaDepartamental"].Value = pintEscalaDepartamental;
            }

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

            if (!string.IsNullOrEmpty(pstrSolicitante))
            {
                cmdCommand.Parameters.Add(new FWParameter("@Des_Solicitante", FWDbType.Varchar, 50));
                cmdCommand.Parameters["@Des_Solicitante"].Value = pstrSolicitante;
            }

            if (!string.IsNullOrEmpty(pstrColaborador))
            {
                cmdCommand.Parameters.Add(new FWParameter("@Des_Colaborador", FWDbType.Varchar, 50));
                cmdCommand.Parameters["@Des_Colaborador"].Value = pstrColaborador;
            }

            if (!string.IsNullOrEmpty(pstrAprovador))
            {
                cmdCommand.Parameters.Add(new FWParameter("@Des_Aprovador", FWDbType.Varchar, 50));
                cmdCommand.Parameters["@Des_Aprovador"].Value = pstrAprovador;
            }

            if (pintStatus == -1 | pintStatus > 0)
            {
                if (pintStatus == -1)
                    pintStatus = 0;

                cmdCommand.Parameters.Add(new FWParameter("@Id_Status", FWDbType.Int32));
                cmdCommand.Parameters["@Id_Status"].Value = pintStatus;
            }

            if (pintTipoSolicitacao > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Id_TipoSolicitacao", FWDbType.Int32));
                cmdCommand.Parameters["@Id_TipoSolicitacao"].Value = pintTipoSolicitacao;
            }

            if (pdatDataInicio.HasValue)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Dt_DataInicio", FWDbType.DateTime));
                cmdCommand.Parameters["@Dt_DataInicio"].Value = pdatDataInicio.Value;
            }

            if (pdatDataFinal.HasValue)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Dt_DataFinal", FWDbType.DateTime));
                cmdCommand.Parameters["@Dt_DataFinal"].Value = pdatDataFinal.Value;
            }

            cmdCommand.Parameters.Add(new FWParameter("@Id_UsuarioSolicitante", FWDbType.Int32));
            cmdCommand.Parameters["@Id_UsuarioSolicitante"].Value = intIdUsuarioSolicitante;

            //Executa a função no banco de dados 
            dttRetorno = new DataTable();
            dttRetorno = conProvider.ExecuteDataTable(cmdCommand);

            return dttRetorno;
        }
        #endregion

        #region Listar Escalas Pendentes de Aprovação

        /// <summary>
        ///      Lista Escalas pendentes de aprovação
        /// </summary>
        /// <param name="pdecCodUsuAprov">Código do Usuário Aprovador</param>
        /// <returns>Lista de Escalação</returns>
        /// <history>
        ///      [cmarchi] created 26/01/2010
        /// </history>
        public Collection<Escalacao> ListarEscalaPendAprov(decimal pdecCodUsuAprov)
        {
            Collection<Escalacao> colRetorno = null;

            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_EscalaAprovacao");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Parâmetros de entrada
            if (pdecCodUsuAprov > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@USU_N_CODIGO", FWDbType.Int32));
                cmdCommand.Parameters["@USU_N_CODIGO"].Value = pdecCodUsuAprov;
            }

            //Executa a função no banco de dados 
            System.Data.IDataReader idrRetorno;
            Escalacao objRetorno = null;
            colRetorno = new Collection<Escalacao>();

            idrRetorno = conProvider.ExecuteDataReader(cmdCommand);

            while (idrRetorno.Read())
            {
                objRetorno = new Escalacao();
                objRetorno.FromIDataReader(idrRetorno);
                colRetorno.Add(objRetorno);
            }

            return colRetorno;
        }
        #endregion

        #region Listar Tipo Solicitação Escala

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        ///  Lista tipo solicitação Escala
        /// </summary> 
        /// <returns>Collection Tiposolicitacao</returns> 
        /// <history> 
        ///     [cmarchi] 11/2/2010 Created 
        ///     [cmarchi] 17/2/2010 Modify 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public Collection<SafWeb.Model.Solicitacao.TipoSolicitacao> ListarTipoSolicitacao()
        {
            Collection<SafWeb.Model.Solicitacao.TipoSolicitacao> colRetorno = null;

            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_TIPO_SOLICITACAO");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Parâmetros de entrada
            cmdCommand.Parameters.Add(new FWParameter("@Escala", FWDbType.Byte));
            cmdCommand.Parameters["@Escala"].Value = 1;

            //Executa a função no banco de dados 
            System.Data.IDataReader idrRetorno;
            SafWeb.Model.Solicitacao.TipoSolicitacao objRetorno = null;
            colRetorno = new Collection<SafWeb.Model.Solicitacao.TipoSolicitacao>();

            idrRetorno = conProvider.ExecuteDataReader(cmdCommand);

            while (idrRetorno.Read())
            {
                objRetorno = new SafWeb.Model.Solicitacao.TipoSolicitacao();
                objRetorno.FromIDataReader(idrRetorno);
                colRetorno.Add(objRetorno);
            }

            return colRetorno;
        }

        #endregion

        #region Listar Historico Escalas Colaboradores      
        /// <summary>
        ///      Lista Histórico de Escalas dos colaboradores
        /// </summary>
        /// <param name="pdecCodUsurio"></param>
        /// <param name="pdecCodEscalacao"></param>
        /// <param name="pdecCodRegional"></param>
        /// <param name="pdecCodFilial"></param>
        /// <param name="pdecCodStatusAprov"></param>
        /// <param name="pdecCodTipoSol"></param>
        /// <param name="pdecCodEscalaDepto"></param>
        /// <param name="pstrDesSolicitante"></param>
        /// <param name="pstrDesColaborador"></param>
        /// <param name="pstrDesAprovador"></param>
        /// <param name="pdatDataInicio"></param>
        /// <param name="pdatDataFinal"></param>
        /// <returns>Lista de Historico de Escalação</returns>
        /// /// <history>
        ///      [tgerevini] created 24/05/2010
        /// </history>
        public Collection<HistoricoEscala> ListarHistoricoEscala(decimal pdecCodUsurio, decimal pdecCodEscalacao, decimal pdecCodRegional, decimal pdecCodFilial,
            decimal pdecCodStatusAprov, decimal pdecCodTipoSol, decimal pdecCodEscalaDepto, string pstrDesSolicitante, string pstrDesColaborador, 
            string pstrDesAprovador, DateTime? pdatDataInicio, DateTime? pdatDataFinal)
        {
            Collection<HistoricoEscala> colRetorno = null;

            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_HistoricoEscala");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Parâmetros de entrada
            cmdCommand.Parameters.Add(new FWParameter("@USU_N_CODIGO", FWDbType.Int32));
            cmdCommand.Parameters["@USU_N_CODIGO"].Value = pdecCodUsurio;

            if (pdecCodEscalacao > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@ID_ESCALACAO", FWDbType.Int32));
                cmdCommand.Parameters["@ID_ESCALACAO"].Value = pdecCodEscalacao;
            }

            if (pdecCodRegional > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@ID_REGIONAL", FWDbType.Int32));
                cmdCommand.Parameters["@ID_REGIONAL"].Value = pdecCodRegional;
            }
            
            if (pdecCodFilial > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@ID_FILIAL", FWDbType.Int32));
                cmdCommand.Parameters["@ID_FILIAL"].Value = pdecCodFilial;
            }

            if (pdecCodStatusAprov > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@ID_STATUSAPROVACAO", FWDbType.Int32));
                cmdCommand.Parameters["@ID_STATUSAPROVACAO"].Value = pdecCodStatusAprov;
            }

            if (pdecCodTipoSol > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@ID_TIPOSOLICITACAO", FWDbType.Int32));
                cmdCommand.Parameters["@ID_TIPOSOLICITACAO"].Value = pdecCodTipoSol;
            }

            if (pdecCodEscalaDepto > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@ID_ESCALADEPARTAMENTAL", FWDbType.Int32));
                cmdCommand.Parameters["@ID_ESCALADEPARTAMENTAL"].Value = pdecCodEscalaDepto;
            }

            if (pstrDesSolicitante != string.Empty)
            {
                cmdCommand.Parameters.Add(new FWParameter("@DES_SOLICITANTE", FWDbType.Varchar,50));
                cmdCommand.Parameters["@DES_SOLICITANTE"].Value = pstrDesSolicitante;
            }

            if (pstrDesColaborador != string.Empty)
            {
                cmdCommand.Parameters.Add(new FWParameter("@DES_COLABORADOR", FWDbType.Varchar, 50));
                cmdCommand.Parameters["@DES_COLABORADOR"].Value = pstrDesColaborador;
            }

            if (pstrDesAprovador != string.Empty)
            {
                cmdCommand.Parameters.Add(new FWParameter("@DES_APROVADOR", FWDbType.Varchar, 50));
                cmdCommand.Parameters["@DES_APROVADOR"].Value = pstrDesAprovador;
            }

            if (pdatDataInicio.HasValue)
            {
                cmdCommand.Parameters.Add(new FWParameter("@DT_DATAINICIO", FWDbType.DateTime));
                cmdCommand.Parameters["@DT_DATAINICIO"].Value = pdatDataInicio;
            }

            if (pdatDataFinal.HasValue)
            {
                cmdCommand.Parameters.Add(new FWParameter("@DT_DATAFINAL", FWDbType.DateTime));
                cmdCommand.Parameters["@DT_DATAFINAL"].Value = pdatDataFinal;
            }

            //Executa a função no banco de dados 
            System.Data.IDataReader idrRetorno;
            HistoricoEscala objRetorno = null;
            colRetorno = new Collection<HistoricoEscala>();

            idrRetorno = conProvider.ExecuteDataReader(cmdCommand);

            while (idrRetorno.Read())
            {
                objRetorno = new HistoricoEscala();
                objRetorno.FromIDataReader(idrRetorno);
                colRetorno.Add(objRetorno);
            }

            return colRetorno;
        }
        #endregion

        #region "Listar horário de escalas Ronda"

        /// <summary>
        ///      Lista horários de escalas Ronda
        /// </summary>
        /// <param name="pintIdEscala"></param>
        /// <param name="pintIdJornada"></param>
        /// <param name="pstrDesEscala"></param>
        /// <param name="pintCodLegado"></param>
        /// <returns>Lista horários de escalas Ronda</returns>
        /// /// <history>
        ///      [haguiar] created 06/01/2012
        /// </history>
        public Collection<HorarioLegado> ListarHorarioEscalas(int pintIdEscala, int pintIdJornada, string pstrDesEscala, int pintCodLegado)
        {
            Collection<HorarioLegado> colRetorno = null;

            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_CODESCALA");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Parâmetros de entrada
            if (pintIdEscala > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@ID_ESCALA", FWDbType.Int32));
                cmdCommand.Parameters["@ID_ESCALA"].Value = pintIdEscala;
            }

            if (pintIdJornada > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@ID_JORNADA", FWDbType.Int32));
                cmdCommand.Parameters["@ID_JORNADA"].Value = pintIdJornada;
            }

            if (!string.IsNullOrEmpty(pstrDesEscala))
            {
                cmdCommand.Parameters.Add(new FWParameter("@DES_ESCALA", FWDbType.Varchar));
                cmdCommand.Parameters["@DES_ESCALA"].Value = pstrDesEscala;
            }

            if (pintCodLegado > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@COD_ESCALALEGADO", FWDbType.Int32));
                cmdCommand.Parameters["@COD_ESCALALEGADO"].Value = pintCodLegado;
            }

            //Executa a função no banco de dados 
            System.Data.IDataReader idrRetorno;
            HorarioLegado objRetorno = null;
            colRetorno = new Collection<HorarioLegado>();

            idrRetorno = conProvider.ExecuteDataReader(cmdCommand);

            while (idrRetorno.Read())
            {
                objRetorno = new HorarioLegado();
                objRetorno.FromIDataReader(idrRetorno);
                colRetorno.Add(objRetorno);
            }

            return colRetorno;
        }

        #endregion

        #region Listar registro de horários Ronda

        /// <summary>
        ///      Listar registro de horários Ronda
        /// </summary>
        /// <param name="pintIdHorario"></param>
        /// <param name="pstrDesHorario"></param>
        /// <param name="pintCodLegado"></param>
        /// <returns>Lista horários de escalas Ronda</returns>
        /// /// <history>
        ///      [haguiar] created 10/01/2012 10:53
        /// </history>
        public Collection<HorarioLegado> ListarRegistroHorarios(int pintIdHorario, string pstrDesHorario, int pintCodLegado, int pintIdJornada)
        {
            Collection<HorarioLegado> colRetorno = null;

            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_CODHORARIO");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Parâmetros de entrada
            if (pintIdHorario > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@ID_HORARIO", FWDbType.Int32));
                cmdCommand.Parameters["@ID_HORARIO"].Value = pintIdHorario;
            }

            if (!string.IsNullOrEmpty(pstrDesHorario))
            {
                cmdCommand.Parameters.Add(new FWParameter("@DES_HORARIO", FWDbType.Varchar));
                cmdCommand.Parameters["@DES_HORARIO"].Value = pstrDesHorario;
            }

            if (pintCodLegado > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@COD_HORARIOLEGADO", FWDbType.Int32));
                cmdCommand.Parameters["@COD_HORARIOLEGADO"].Value = pintCodLegado;
            }

            if (pintIdJornada > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@ID_JORNADA", FWDbType.Int32));
                cmdCommand.Parameters["@ID_JORNADA"].Value = pintIdJornada;
            }


            //Executa a função no banco de dados 
            System.Data.IDataReader idrRetorno;
            HorarioLegado objRetorno = null;
            colRetorno = new Collection<HorarioLegado>();

            idrRetorno = conProvider.ExecuteDataReader(cmdCommand);

            while (idrRetorno.Read())
            {
                objRetorno = new HorarioLegado();
                objRetorno.FromIDataReader(idrRetorno);
                colRetorno.Add(objRetorno);
            }

            return colRetorno;
        }

        #endregion

        #region Listar escalas Ronda

        /// <summary>
        ///      Listar escalas Ronda
        /// </summary>
        /// <returns>Lista escalas Ronda</returns>
        /// /// <history>
        ///      [haguiar] created 11/01/2012 14:24
        /// </history>
        public Collection<EscalaLegadoRonda> ListarEscalasRonda()
        {
            Collection<EscalaLegadoRonda> colRetorno = null;

            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_CODESCALA_RONDA");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Executa a função no banco de dados 
            System.Data.IDataReader idrRetorno;
            EscalaLegadoRonda objRetorno = null;
            colRetorno = new Collection<EscalaLegadoRonda>();

            idrRetorno = conProvider.ExecuteDataReader(cmdCommand);

            while (idrRetorno.Read())
            {
                objRetorno = new EscalaLegadoRonda();
                objRetorno.FromIDataReader(idrRetorno);
                colRetorno.Add(objRetorno);
            }

            return colRetorno;
        }

        #endregion

        #region Listar horários Ronda

        /// <summary>
        ///      Listar horários Ronda
        /// </summary>
        /// <returns>Lista horários Ronda</returns>
        /// /// <history>
        ///      [haguiar] created 12/01/2012 09:05
        /// </history>
        public Collection<HorarioLegadoRonda> ListarHorariosRonda()
        {
            Collection<HorarioLegadoRonda> colRetorno = null;

            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_CODHORARIO_RONDA");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Executa a função no banco de dados 
            System.Data.IDataReader idrRetorno;
            HorarioLegadoRonda objRetorno = null;
            colRetorno = new Collection<HorarioLegadoRonda>();

            idrRetorno = conProvider.ExecuteDataReader(cmdCommand);

            while (idrRetorno.Read())
            {
                objRetorno = new HorarioLegadoRonda();
                objRetorno.FromIDataReader(idrRetorno);
                colRetorno.Add(objRetorno);
            }

            return colRetorno;
        }

        #endregion
        #endregion

        #region Obter

        #region Obter Ultimo Usuario Aprovador
        /// <summary>
        ///     Obtém o nome do Ultimo Usuario Aprovador
        /// </summary>
        /// <param name="pintIdEscalacao">Id da Escalação</param>
        /// <history>
        ///     [tgerevini] created 01/6/2010
        /// </history>
        public String ObterUltimoAprovador(int pintIdEscalacao)
        {

            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_ObterUltimoAprovador");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Parâmetros de entrada
            cmdCommand.Parameters.Add(new FWParameter("@Id_Escalacao", FWDbType.Int32));
            cmdCommand.Parameters["@Id_Escalacao"].Value = pintIdEscalacao;

            //Executa a função no banco de dados 
            System.Data.IDataReader idrRetorno;
            string strRetorno = string.Empty;

            idrRetorno = conProvider.ExecuteDataReader(cmdCommand);

            while (idrRetorno.Read())
            {
                strRetorno = idrRetorno["USU_C_NOME"].ToString();
            }

            return strRetorno;
        }
        #endregion

        #region Obter
        /// <summary>
        /// Obtém uma Escalação.
        /// </summary>
        /// <param name="pintIdEscalacao">Id da Escalação</param>
        /// <returns>Objeto Escala</returns>
        /// <history>
        ///     [cmarchi] created 12/1/2010
        ///     [cmarchi] modify 18/1/2010
        /// </history>
        public Escalacao Obter(object pintIdEscalacao)
        {
            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_ObterEscalacao");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            cmdCommand.Parameters.Add(new FWParameter("@Id_Escalacao", FWDbType.Int32));
            cmdCommand.Parameters["@Id_Escalacao"].Value = pintIdEscalacao;

            cmdCommand.Parameters.Add(new FWParameter("@OUT_Id_Escalacao", FWDbType.Int32));
            cmdCommand.Parameters["@OUT_Id_Escalacao"].Direction = ParameterDirection.Output;

            cmdCommand.Parameters.Add(new FWParameter("@OUT_Id_EscalaDpto", FWDbType.Int32));
            cmdCommand.Parameters["@OUT_Id_EscalaDpto"].Direction = ParameterDirection.Output;

            cmdCommand.Parameters.Add(new FWParameter("@OUT_Dt_InicioPeriodo", FWDbType.DateTime));
            cmdCommand.Parameters["@OUT_Dt_InicioPeriodo"].Direction = ParameterDirection.Output;

            cmdCommand.Parameters.Add(new FWParameter("@OUT_Dt_FinalPeriodo", FWDbType.DateTime));
            cmdCommand.Parameters["@OUT_Dt_FinalPeriodo"].Direction = ParameterDirection.Output;

            cmdCommand.Parameters.Add(new FWParameter("@OUT_Id_TipoSolicitacao", FWDbType.Int32));
            cmdCommand.Parameters["@OUT_Id_TipoSolicitacao"].Direction = ParameterDirection.Output;

            cmdCommand.Parameters.Add(new FWParameter("@OUT_Id_UsuarioSolicitante", FWDbType.Int32));
            cmdCommand.Parameters["@OUT_Id_UsuarioSolicitante"].Direction = ParameterDirection.Output;

            cmdCommand.Parameters.Add(new FWParameter("@OUT_Flg_Situacao", FWDbType.Boolean));
            cmdCommand.Parameters["@OUT_Flg_Situacao"].Direction = ParameterDirection.Output;

            cmdCommand.Parameters.Add(new FWParameter("@OUT_Dt_Inclusao", FWDbType.DateTime));
            cmdCommand.Parameters["@OUT_Dt_Inclusao"].Direction = ParameterDirection.Output;

            cmdCommand.Parameters.Add(new FWParameter("@OUT_Dt_Alteracao", FWDbType.DateTime));
            cmdCommand.Parameters["@OUT_Dt_Alteracao"].Direction = ParameterDirection.Output;


            // Execucao
            Escalacao objRetorno = new Escalacao();

            conProvider.ExecuteScalar(cmdCommand);

            if (!(cmdCommand.Parameters["@OUT_Id_Escalacao"].Value == DBNull.Value))
                objRetorno.IdEscalacao = Convert.ToInt32(cmdCommand.Parameters["@OUT_Id_Escalacao"].Value);

            if (!(cmdCommand.Parameters["@OUT_Id_EscalaDpto"].Value == DBNull.Value))
                objRetorno.IdEscalaDepartamental = Convert.ToInt32(cmdCommand.Parameters["@OUT_Id_EscalaDpto"].Value);

            if (!(cmdCommand.Parameters["@OUT_Dt_InicioPeriodo"].Value == DBNull.Value))
                objRetorno.DataInicioPeriodo = Convert.ToDateTime(cmdCommand.Parameters["@OUT_Dt_InicioPeriodo"].Value);

            if (!(cmdCommand.Parameters["@OUT_Dt_FinalPeriodo"].Value == DBNull.Value))
                objRetorno.DataFinalPeriodo = Convert.ToDateTime(cmdCommand.Parameters["@OUT_Dt_FinalPeriodo"].Value);

            if (!(cmdCommand.Parameters["@OUT_Id_TipoSolicitacao"].Value == DBNull.Value))
                objRetorno.IdTipoSolicitacao = Convert.ToInt32(cmdCommand.Parameters["@OUT_Id_TipoSolicitacao"].Value);

            if (!(cmdCommand.Parameters["@OUT_Id_UsuarioSolicitante"].Value == DBNull.Value))
                objRetorno.IdUsuarioSolicitante = Convert.ToInt32(cmdCommand.Parameters["@OUT_Id_UsuarioSolicitante"].Value);

            if (!(cmdCommand.Parameters["@OUT_Flg_Situacao"].Value == DBNull.Value))
                objRetorno.Situacao = Convert.ToBoolean(cmdCommand.Parameters["@OUT_Flg_Situacao"].Value);

            if (!(cmdCommand.Parameters["@OUT_Dt_Inclusao"].Value == DBNull.Value))
                objRetorno.DataInclusao = Convert.ToDateTime(cmdCommand.Parameters["@OUT_Dt_Inclusao"].Value);

            if (!(cmdCommand.Parameters["@OUT_Dt_Alteracao"].Value == DBNull.Value))
                objRetorno.DataAlteracao = Convert.ToDateTime(cmdCommand.Parameters["@OUT_Dt_Alteracao"].Value);


            return objRetorno;
        }
        #endregion

        #region ObterColaborador
        /// <summary>
        /// Obtém Colaboradores da Escalação.
        /// </summary>
        /// <param name="pintIdEscalacao">Id da Escalação</param>
        /// <returns>Objeto Colaboradores</returns>
        /// <history>
        ///     [cmarchi] created 12/1/2010
        /// </history>
        public Collection<Colaborador> ObterColaborador(int pintIdEscalacao)
        {
            Collection<Colaborador> colRetorno = null;

            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_ObterColEscalacao");
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

        #region Obter
        /// <summary>
        /// Obtém o nº de escalação do colaborador se ele já estiver em uma escala
        /// </summary>
        /// <param name="pintIdColaborador">Id Colaborador</param>
        /// <returns>Objeto Escala</returns>
        /// <history>
        ///     [tgerevini] created 18/05/2010
        /// </history>
        public int ObterEscalaColaborador(object pintIdColaborador)
        {
            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_ObterEscalaColaborador");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            cmdCommand.Parameters.Add(new FWParameter("@Id_Colaborador", FWDbType.Int32));
            cmdCommand.Parameters["@Id_Colaborador"].Value = pintIdColaborador;

            cmdCommand.Parameters.Add(new FWParameter("@OUT_Id_Escalacao", FWDbType.Int32));
            cmdCommand.Parameters["@OUT_Id_Escalacao"].Direction = ParameterDirection.Output;

            // Execucao
            int intRetorno = 0;

            conProvider.ExecuteScalar(cmdCommand);

            if (!(cmdCommand.Parameters["@OUT_Id_Escalacao"].Value == DBNull.Value))
                intRetorno = Convert.ToInt32(cmdCommand.Parameters["@OUT_Id_Escalacao"].Value);

            return intRetorno;
        }
        #endregion

        #region ObterColaboradoresNaoContemHorarioEscala
        /// <summary>
        /// Obtém Colaboradores da Escalação que ainda não possui uma Escala.
        /// </summary>
        /// <param name="pintIdEscalacao">Id da Escalação</param>
        /// <returns>Lista de Objetos Colaborador</returns>
        /// <history>
        ///     [cmarchi] created 15/1/2010
        /// </history>
        public Collection<Colaborador> ObterColaboradoresNaoContemHorarioEscala(int pintIdEscalacao)
        {
            Collection<Colaborador> colRetorno = null;

            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_ObterEscalacaoColNDt");
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

        #region ObterDatasFolgaColaborador
        /// <summary>
        /// Obtém todas as Datas de folga para um colaborador
        /// </summary>
        /// <history>
        ///     [cfrancisco] created 26/05/2012
        /// </history>
        public IList<DateTime> ObterDatasFolgaColaborador(int idColaborador, DateTime dtInicioPeriodo, DateTime dtFinalPeriodo, int idEscalaDpto)
        {
            IList<DateTime> colRetorno = null;
            /*EXEC SP_SAF_SEL_OBTERDATASCOLABORADORPERIODO @Id_Colaborador = 51476,@dt_InicioPeriodo = '2012-07-01',@dt_FinalPeriodo = '2012-07-30', @Id_EscalaDpto = 10, @Flg_Folga = 1*/
            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_OBTERDATASCOLABORADORPERIODO");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Parâmetros de entrada
            cmdCommand.Parameters.Add(new FWParameter("@Id_Colaborador", FWDbType.Int32));
            cmdCommand.Parameters["@Id_Colaborador"].Value = idColaborador;

            cmdCommand.Parameters.Add(new FWParameter("@Id_EscalaDpto", FWDbType.Int32));
            cmdCommand.Parameters["@Id_EscalaDpto"].Value = idEscalaDpto;

            cmdCommand.Parameters.Add(new FWParameter("@dt_InicioPeriodo", FWDbType.DateTime));
            cmdCommand.Parameters["@dt_InicioPeriodo"].Value = dtInicioPeriodo;

            cmdCommand.Parameters.Add(new FWParameter("@dt_FinalPeriodo", FWDbType.DateTime));
            cmdCommand.Parameters["@dt_FinalPeriodo"].Value = dtFinalPeriodo;

            cmdCommand.Parameters.Add(new FWParameter("@Flg_Folga", FWDbType.Int32));
            cmdCommand.Parameters["@Flg_Folga"].Value = 1;

            

            //Executa a função no banco de dados 
            System.Data.IDataReader idrRetorno;
            DateTime objRetorno;
            colRetorno = new Collection<DateTime>();

            idrRetorno = conProvider.ExecuteDataReader(cmdCommand);

            while (idrRetorno.Read())
            {
                objRetorno = Convert.ToDateTime(idrRetorno["dt_data"]);
                colRetorno.Add(objRetorno);
            }

            return colRetorno;
        }
        #endregion

        #region ObterDatas
        /// <summary>
        /// Obtém todas as Datas de uma Escalação
        /// </summary>
        /// <param name="pintIdEscalacao">Id da Escalação</param>
        /// <returns>Objeto Colaboradores</returns>
        /// <history>
        ///     [cmarchi] created 20/1/2010
        /// </history>
        public Collection<DateTime> ObterDatas(int pintIdEscalacao)
        {
            Collection<DateTime> colRetorno = null;

            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_ObterDatasEscalacao");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Parâmetros de entrada
            cmdCommand.Parameters.Add(new FWParameter("@Id_Escalacao", FWDbType.Int32));
            cmdCommand.Parameters["@Id_Escalacao"].Value = pintIdEscalacao;

            //Executa a função no banco de dados 
            System.Data.IDataReader idrRetorno;
            DateTime objRetorno;
            colRetorno = new Collection<DateTime>();

            idrRetorno = conProvider.ExecuteDataReader(cmdCommand);

            while (idrRetorno.Read())
            {
                objRetorno = Convert.ToDateTime(idrRetorno["Dt_Escalacao"]);
                colRetorno.Add(objRetorno);
            }

            return colRetorno;
        }
        #endregion

        #region Obter Data Inicial Próximo Período
        /// <summary>
        ///     Obtém a data inicial para o próximo período de uma escalação
        /// </summary>
        /// <param name="pintIdEscalacao">Id da Escalação</param>
        /// <returns>Objeto DateTime</returns>
        /// <history>
        ///     [tgerevini] created 20/09/2010
        ///     [haguiar] modified 21/10/2010
        /// </history>
        public DateTime ObterDataInicialProximoPeriodo(int pintIdEscalacao)
        {
            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_ObterDataInicialProximoPeriodo");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Parâmetros de entrada
            cmdCommand.Parameters.Add(new FWParameter("@Id_EscalaDpto", FWDbType.Int32));
            cmdCommand.Parameters["@Id_EscalaDpto"].Value = pintIdEscalacao;

            //Executa a função no banco de dados 
            System.Data.IDataReader idrRetorno;
            DateTime objRetorno = new DateTime();
            bool blnHasRows = false;

            idrRetorno = conProvider.ExecuteDataReader(cmdCommand);

            while (idrRetorno.Read())
            {
                objRetorno = Convert.ToDateTime(idrRetorno["Dt_FinalPeriodo"]);

                TimeSpan ts;
                ts = DateTime.Now - objRetorno;

                if (ts.Days > 0)
                    objRetorno = DateTime.Now;

                blnHasRows = true;
            }

            /*
            if (!blnHasRows)//(idrRetorno.RecordsAffected == -1)
                objRetorno = DateTime.Now;
          */

            if (!blnHasRows)
                objRetorno = DateTime.Now;


            return objRetorno;
        }
        #endregion

        #region ObterPeriodosTrocaHorario
        /// <summary>
        /// Obtém todas as Datas de uma Escalação
        /// </summary>
        /// <param name="pintIdEscalacao">Id da Escalação</param>
        /// <returns>Lista de períodos</returns>
        /// <history>
        ///     [haguiar] created 11/11/2011 16:25
        /// </history>
        public Collection<string> ObterPeriodosTrocaHorario(int pintIdEscalaDpto)
        {
            Collection<string> colRetorno = null;

            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_PERIODOS_TROCAHORARIO");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Parâmetros de entrada
            cmdCommand.Parameters.Add(new FWParameter("@ID_ESCALADPTO", FWDbType.Int32));
            cmdCommand.Parameters["@ID_ESCALADPTO"].Value = pintIdEscalaDpto;

            //Executa a função no banco de dados 
            System.Data.IDataReader idrRetorno;
            string objRetorno;
            colRetorno = new Collection<string>();

            idrRetorno = conProvider.ExecuteDataReader(cmdCommand);

            while (idrRetorno.Read())
            {
                objRetorno = Convert.ToString(idrRetorno["Dt_InicioPeriodo"]);
                colRetorno.Add(objRetorno);
            }

            return colRetorno;
        }
        #endregion

        #region VerificarDataColaboradorTroca

        public List<int> VerificarDataColaboradorTroca(DateTime dtEscalacao, int idColaborador, int idEscalaDpto)
        {
            var colRetorno = new List<int>();

            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_VERIFICARDATACOLABORADORTROCA");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Parâmetros de entrada
            cmdCommand.Parameters.Add(new FWParameter("@Dt_Escalacao", FWDbType.DateTime));
            cmdCommand.Parameters["@Dt_Escalacao"].Value = dtEscalacao;

            if (!idColaborador.Equals(0))
            {
                cmdCommand.Parameters.Add(new FWParameter("@Id_Colaborador", FWDbType.Int32));
                cmdCommand.Parameters["@Id_Colaborador"].Value = idColaborador;
            }

            if (!idEscalaDpto.Equals(0))
            {
                cmdCommand.Parameters.Add(new FWParameter("@Id_EscalaDpto", FWDbType.Int32));
                cmdCommand.Parameters["@Id_EscalaDpto"].Value = idEscalaDpto;
            }

            using (var idrRetorno = conProvider.ExecuteDataReader(cmdCommand))
            {
                while (idrRetorno.Read())
                {
                    colRetorno.Add(Convert.ToInt32(idrRetorno["Id_Colaborador"]));
                }

                return colRetorno;
            }
        }
        
        #endregion

        #region VerificarDataColaboradorEscala

        public List<int> VerificarDataColaboradorEscala(DateTime dtPeriodoInicio, DateTime dtPeriodoFim, int idColaborador, int idEscalaDpto)
        {
            var colRetorno = new List<int>();

            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_VERIFICARDATACOLABORADORESCALA");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Parâmetros de entrada
            cmdCommand.Parameters.Add(new FWParameter("@dt_InicioPeriodo", FWDbType.DateTime));
            cmdCommand.Parameters["@dt_InicioPeriodo"].Value = dtPeriodoInicio;

            cmdCommand.Parameters.Add(new FWParameter("@dt_FinalPeriodo", FWDbType.DateTime));
            cmdCommand.Parameters["@dt_FinalPeriodo"].Value = dtPeriodoFim;

            if (!idColaborador.Equals(0))
            {
                cmdCommand.Parameters.Add(new FWParameter("@Id_Colaborador", FWDbType.Int32));
                cmdCommand.Parameters["@Id_Colaborador"].Value = idColaborador;
            }

            if (!idEscalaDpto.Equals(0))
            {
                cmdCommand.Parameters.Add(new FWParameter("@Id_EscalaDpto", FWDbType.Int32));
                cmdCommand.Parameters["@Id_EscalaDpto"].Value = idEscalaDpto;
            }

            using (var idrRetorno = conProvider.ExecuteDataReader(cmdCommand))
            {
                while (idrRetorno.Read())
                {
                    colRetorno.Add(Convert.ToInt32(idrRetorno["Id_Colaborador"]));
                }

                return colRetorno;
            }
        }

        #endregion


        #region ObterListaPeriodosReprovados
        /// ----------------------------------------------------------------------------- 
        /// <summary>
        ///      Obtém a lista dos períodos reprovados
        /// </summary>
        /// <param name="pintIdEscalaDpto">Id da Escala Departamental</param>
        /// <history>
        ///      [haguiar] created 08/11/2010
        /// </history>
        /// ----------------------------------------------------------------------------- 
        public Collection<DateTime> ObterListaPeriodosReprovados(int pintIdEscalaDpto)
        {
            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_ESCALAPERIODOSREPROVADOS");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Parâmetros de entrada
            cmdCommand.Parameters.Add(new FWParameter("@Id_EscalaDpto", FWDbType.Int32));
            cmdCommand.Parameters["@Id_EscalaDpto"].Value = pintIdEscalaDpto;

            //Executa a função no banco de dados 
            System.Data.IDataReader idrRetorno;
            DateTime objRetorno = new DateTime();

            //coleção de datas
            Collection<DateTime> colRetorno = new Collection<DateTime>();

            idrRetorno = conProvider.ExecuteDataReader(cmdCommand);

            while (idrRetorno.Read())
            {
                objRetorno = Convert.ToDateTime(idrRetorno["Dt_InicioPeriodo"]);
                colRetorno.Add(objRetorno);
            }

            return colRetorno;
        }
        #endregion

        #region ObterDtHorColEscalacao RONDA
        /// <summary>
        /// Obtém datas e horários dos colaboradores de uma escalação ou troca de horário RONDA
        /// </summary>
        /// <param name="pintIdEscalacao">Id da Escalação</param>
        /// <param name="datDataSelecionada">Data selecionada</param>
        /// <returns>Lista de Objetos Colaborador</returns>
        /// <history>
        ///     [haguiar] create 07/11/2011 16:16
        /// </history>
        public Collection<EscalacaoColaboradoresData> ObterDtHorColEscalacaoRonda(int pintIdEscalaDpto, int pintIdTipoSolicitacao, Collection<DateTime> colDatas)
        {
            Collection<EscalacaoColaboradoresData> colRetorno = null;

            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_RONDA_DatHorColEscalacao");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Parâmetros de entrada
            cmdCommand.Parameters.Add(new FWParameter("@Id_EscalaDpto", FWDbType.Int32));
            cmdCommand.Parameters["@Id_EscalaDpto"].Value = pintIdEscalaDpto;

            cmdCommand.Parameters.Add(new FWParameter("@Id_TipoSolicitacao", FWDbType.Int32));
            cmdCommand.Parameters["@Id_TipoSolicitacao"].Value = pintIdTipoSolicitacao;

            cmdCommand.Parameters.Add(new FWParameter("@PERIODO_INI", FWDbType.DateTime));
            cmdCommand.Parameters["@PERIODO_INI"].Value = colDatas[0];

            cmdCommand.Parameters.Add(new FWParameter("@PERIODO_FIM", FWDbType.DateTime));

            if (pintIdTipoSolicitacao == 7)
            {
                cmdCommand.Parameters["@PERIODO_FIM"].Value = colDatas[1];
            }
            else
            {
                cmdCommand.Parameters["@PERIODO_FIM"].Value = colDatas[0];
            }

            //Executa a função no banco de dados 
            System.Data.IDataReader idrRetorno;
            EscalacaoColaboradoresData objRetorno = null;
            colRetorno = new Collection<EscalacaoColaboradoresData>();

            idrRetorno = conProvider.ExecuteDataReader(cmdCommand);

            while (idrRetorno.Read())
            {
                objRetorno = new EscalacaoColaboradoresData();
                objRetorno.FromIDataReader(idrRetorno);
                colRetorno.Add(objRetorno);
            }

            return colRetorno;
        }
        #endregion

        #region ObterDtHorColEscalacao CREW
        /// <summary>
        /// Obtém datas e horários dos colaboradores de uma escalação ou troca de horário CREW
        /// </summary>
        /// <param name="pintIdEscalacao">Id da Escalação</param>
        /// <param name="datDataSelecionada">Data selecionada</param>
        /// <param name="pblnCompararTime">True - Compara a data com o Time, Flase - Compara a data sem o Time</param>
        /// <returns>Lista de Objetos Colaborador</returns>
        /// <history>
        ///     [haguiar_9004] modify 11/08/2011 15:09
        /// </history>
        public Collection<EscalacaoColaboradoresData> ObterDtHorColEscalacaoCrew(DateTime? pdatDataSelecionada,  int pintId_Usuario,
                            bool pblnDivergencia, int pintId_TipoSolicitacao)
        {
            Collection<EscalacaoColaboradoresData> colRetorno = null;

            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_CREW_DatHorColEscalacao");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

                cmdCommand.Parameters.Add(new FWParameter("@DT_ESCALACAO", FWDbType.DateTime));
            cmdCommand.Parameters["@DT_ESCALACAO"].Value = pdatDataSelecionada;

            cmdCommand.Parameters.Add(new FWParameter("@DIVERGECIA", FWDbType.Boolean));
            cmdCommand.Parameters["@DIVERGECIA"].Value = pblnDivergencia;

            cmdCommand.Parameters.Add(new FWParameter("@ID_TIPOSOLICITACAO", FWDbType.Int32));
            cmdCommand.Parameters["@ID_TIPOSOLICITACAO"].Value = pintId_TipoSolicitacao;

                cmdCommand.Parameters.Add(new FWParameter("@USU_N_CODIGO", FWDbType.Int32));
            cmdCommand.Parameters["@USU_N_CODIGO"].Value = pintId_Usuario;

            //Executa a função no banco de dados 
            System.Data.IDataReader idrRetorno;
            EscalacaoColaboradoresData objRetorno = null;
            colRetorno = new Collection<EscalacaoColaboradoresData>();

            idrRetorno = conProvider.ExecuteDataReader(cmdCommand);

            while (idrRetorno.Read())
            {
                objRetorno = new EscalacaoColaboradoresData();
                objRetorno.FromIDataReader(idrRetorno);
                colRetorno.Add(objRetorno);
            }

            return colRetorno;
        }
        #endregion

        #region ObterDtHorColEscalacao
        /// <summary>
        /// Obtém as datas e horários dos colaboradores de uma escalação
        /// </summary>
        /// <param name="pintIdEscalacao">Id da Escalação</param>
        /// <param name="datDataSelecionada">Data selecionada</param>
        /// <param name="pblnCompararTime">True - Compara a data com o Time, Flase - Compara a data sem o Time</param>
        /// <returns>Lista de Objetos Colaborador</returns>
        /// <history>
        ///     [cmarchi] created 15/1/2010
        ///     [cmarchi] modify 18/2/2010
        /// </history>
        public Collection<EscalacaoColaboradoresData> ObterDtHorColEscalacao(int pintIdEscalacao,
            DateTime? pdatDataSelecionada, bool? pblnCompararTime, int? intCodLegado)
        {
            Collection<EscalacaoColaboradoresData> colRetorno = null;

            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_DatHorColEscalacao");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Parâmetros de entrada
            cmdCommand.Parameters.Add(new FWParameter("@Id_Escalacao", FWDbType.Int32));
            cmdCommand.Parameters["@Id_Escalacao"].Value = pintIdEscalacao;

            if (pdatDataSelecionada.HasValue)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Dt_Data", FWDbType.DateTime));
                cmdCommand.Parameters["@Dt_Data"].Value = pdatDataSelecionada.Value;
            }

            if (pblnCompararTime.HasValue)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Flg_CompararTime", FWDbType.Boolean));
                cmdCommand.Parameters["@Flg_CompararTime"].Value = pblnCompararTime.Value;
            }

            if (intCodLegado.HasValue)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Cod_Legado", FWDbType.Int32));
                cmdCommand.Parameters["@Cod_Legado"].Value = intCodLegado.Value;
            }

            //Executa a função no banco de dados 
            System.Data.IDataReader idrRetorno;
            EscalacaoColaboradoresData objRetorno = null;
            colRetorno = new Collection<EscalacaoColaboradoresData>();

            idrRetorno = conProvider.ExecuteDataReader(cmdCommand);

            while (idrRetorno.Read())
            {
                objRetorno = new EscalacaoColaboradoresData();
                objRetorno.FromIDataReader(idrRetorno);
                colRetorno.Add(objRetorno);
            }

            return colRetorno;
        }
        #endregion

        #region ObterEscalaColaboradores
        /// <summary>
        /// Obtém uma escalação com seus respectivos colaboradores
        /// </summary>
        /// <param name="pintIdEscalacao">Id da Escalação</param>
        /// <returns>Lista de Escalação</returns>
        /// <history>
        ///     [cmarchi] created 22/1/2010
        ///     [cmarchi] modify 22/2/2010
        /// </history>
        public Collection<Escalacao> ObterEscalaColaboradores(int pintIdEscalacao)
        {
            Collection<Escalacao> colRetorno = null;

            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_ObterEscalasColaboradores");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Parâmetros de entrada
            cmdCommand.Parameters.Add(new FWParameter("@Id_Escalacao", FWDbType.Int32));
            cmdCommand.Parameters["@Id_Escalacao"].Value = pintIdEscalacao;

            //Executa a função no banco de dados 
            System.Data.IDataReader idrRetorno;
            Escalacao objRetorno = null;
            colRetorno = new Collection<Escalacao>();

            idrRetorno = conProvider.ExecuteDataReader(cmdCommand);

            while (idrRetorno.Read())
            {
                objRetorno = new Escalacao();
                objRetorno.FromIDataReader(idrRetorno);
                colRetorno.Add(objRetorno);
            }

            return colRetorno;
        }
        #endregion

        #region ObterPeriodosJaCadastrados

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        ///     Lista as escalas com períodos já cadastrados
        /// </summary> 
        /// <returns>DataTable</returns> 
        /// <param name="pintLista">Código da Escalação</param>
        /// <param name="pdatDataInicio">Data de início do período</param>
        /// <param name="pdatDataFim">Data final do período</param>
        /// <history> 
        ///     [tgerevini] 19/08/2010 Created 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public DataTable ObterPeriodosCadastrados(int pintIdEscalacao,
                                                  DateTime pdatDataInicio, DateTime pdatDataFim)
        {
            DataTable dttRetorno = new DataTable();

            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_PeriodosCadastrados");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Parâmetros de entrada
            cmdCommand.Parameters.Add(new FWParameter("@ID_ESCALADPTO", FWDbType.Int32));
            cmdCommand.Parameters["@ID_ESCALADPTO"].Value = pintIdEscalacao;

            cmdCommand.Parameters.Add(new FWParameter("@DT_INICIOPERIODO", FWDbType.DateTime));
            cmdCommand.Parameters["@DT_INICIOPERIODO"].Value = pdatDataInicio;

            cmdCommand.Parameters.Add(new FWParameter("@DT_FINALPERIODO", FWDbType.DateTime));
            cmdCommand.Parameters["@DT_FINALPERIODO"].Value = pdatDataFim;

            //Executa a função no banco de dados 
            dttRetorno = conProvider.ExecuteDataTable(cmdCommand);

            return dttRetorno;
        }

        #endregion


        #region Obter horário escala

        /// <summary>
        ///      Obter horário escala
        /// </summary>
        /// <param name="pintIdEscala"></param>
        /// <returns>Obter horário de uma escala RONDA</returns>
        /// /// <history>
        ///      [haguiar] created 06/01/2012 16:36
        /// </history>
        public HorarioLegado ObterHorarioEscala(int pintIdEscala)
        {

            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_CODESCALA");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            cmdCommand.Parameters.Add(new FWParameter("@ID_ESCALA", FWDbType.Int32));
            cmdCommand.Parameters["@ID_ESCALA"].Value = pintIdEscala;

            //Executa a função no banco de dados 
            System.Data.IDataReader idrRetorno;
            HorarioLegado objRetorno = null;

            idrRetorno = conProvider.ExecuteDataReader(cmdCommand);

            while (idrRetorno.Read())
            {
                objRetorno = new HorarioLegado();
                objRetorno.FromIDataReader(idrRetorno);
            }

            return objRetorno;
        }

        #endregion


        #region Obter registro de horário

        /// <summary>
        ///      Obter registro de horário
        /// </summary>
        /// <param name="pintIdHorario"></param>
        /// <returns>Obter registro de um horário RONDA</returns>
        /// /// <history>
        ///      [haguiar] created 10/01/2012 10:57
        /// </history>
        public HorarioLegado ObterRegistroHorario(int pintIdHorario)
        {

            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_CODHORARIO");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            cmdCommand.Parameters.Add(new FWParameter("@ID_HORARIO", FWDbType.Int32));
            cmdCommand.Parameters["@ID_HORARIO"].Value = pintIdHorario;

            //Executa a função no banco de dados 
            System.Data.IDataReader idrRetorno;
            HorarioLegado objRetorno = null;

            idrRetorno = conProvider.ExecuteDataReader(cmdCommand);

            while (idrRetorno.Read())
            {
                objRetorno = new HorarioLegado();
                objRetorno.FromIDataReader(idrRetorno);
            }

            return objRetorno;
        }

        #endregion
        #endregion

        #region Reprovar Escalação

        /// <summary>
        ///      Reprova as Escalações
        /// </summary>
        /// <param name="pstrIdEscalacao">Id das Escalações</param>
        /// <param name="pintUsuarioAprovador">Id do Usuário Aprovador/Reprovador</param>
        /// <param name="pstrMotivoReprovacao">Motivo de Reprovação das Escalações</param>
        /// <history>
        ///      [cmarchi] created 28/01/2010
        /// </history>
        public void ReprovarEscalacao(string pstrIdEscalacao, int pintIdUsuarioAprovador, string pstrMotivoReprovacao)
        {
            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_UPD_ReprovEscalacao");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Parâmetros de entrada
            if (!string.IsNullOrEmpty(pstrIdEscalacao))
            {
                cmdCommand.Parameters.Add(new FWParameter("@Id_Escalacao", FWDbType.Varchar, 5000));
                cmdCommand.Parameters["@Id_Escalacao"].Value = pstrIdEscalacao;
            }

            if (pintIdUsuarioAprovador > 0)
            {
                cmdCommand.Parameters.Add(new FWParameter("@Id_UsuarioAprovador", FWDbType.Int32));
                cmdCommand.Parameters["@Id_UsuarioAprovador"].Value = pintIdUsuarioAprovador;
            }

            if (!string.IsNullOrEmpty(pstrMotivoReprovacao))
            {
                cmdCommand.Parameters.Add(new FWParameter("@Des_MotivoReprovacao", FWDbType.Varchar, 150));
                cmdCommand.Parameters["@Des_MotivoReprovacao"].Value = pstrMotivoReprovacao.Trim();
            }

            //Executa a função no banco de dados 
            conProvider.ExecuteNonQuery(cmdCommand);
        }
        #endregion

        #region Obter Aprovadores

        /// <summary>
        ///     Obtem os registros na base
        /// </summary>
        /// <param name="pintIdAprovador">Id da Escalacao</param>
        /// <history>
        ///     [cmarchi] created 29/1/2010
        /// </history>
        /// <returns>Aprovadores da Escalação</returns>
        public DataTable ObterAprovadores(int pintIdEscalacao)
        {
            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_AprovadoresEscala");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            cmdCommand.Parameters.Add(new FWParameter("@Id_Escalacao", FWDbType.Int32));
            cmdCommand.Parameters["@Id_Escalacao"].Value = pintIdEscalacao;

            //Executa a função no banco de dados 
            DataTable dttRetorno;

            dttRetorno = conProvider.ExecuteDataTable(cmdCommand);

            return dttRetorno;
        }
        #endregion

        #region VerificarPeriodoEscalacao
        /// <summary>
        /// Verifica se existe uma escalação para o período informado.
        /// </summary>
        /// <param name="pintIdEscalaDpto">Id da Escala Departamental</param>
        /// <param name="pdatInicioPeriodo">Data de Inicio do Período</param>
        /// <param name="pdatFinalPeriodo">Data final do Periodo</param>
        /// <returns>Escalação existe - 1, Escalação não existe - 0, Erro - -1</returns>
        /// <history>
        ///     [cmarchi] created 22/2/2010
        /// </history>
        public int VerificarPeriodoEscalacao(int pintIdEscalaDpto, DateTime pdatInicioPeriodo,
            DateTime pdatFinalPeriodo)
        {
            // Cria os objetos de conexão ao banco de dados
            FWCommand cmdCommand = new FWCommand("SP_SAF_SEL_VerificarPerEsc");
            cmdCommand.CommandTimeout = intCommandTimeOut;
            cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Parâmetros de entrada
            cmdCommand.Parameters.Add(new FWParameter("@Id_EscalaDpto", FWDbType.Int32));
            cmdCommand.Parameters["@Id_EscalaDpto"].Value = pintIdEscalaDpto;

            cmdCommand.Parameters.Add(new FWParameter("@Dt_InicioPeriodo", FWDbType.DateTime));
            cmdCommand.Parameters["@Dt_InicioPeriodo"].Value = pdatInicioPeriodo;

            cmdCommand.Parameters.Add(new FWParameter("@Dt_FinalPeriodo", FWDbType.DateTime));
            cmdCommand.Parameters["@Dt_FinalPeriodo"].Value = pdatFinalPeriodo;

            object objRetorno = conProvider.ExecuteScalar(cmdCommand);

            return Convert.ToInt32(objRetorno);
        }
        #endregion
    }
}