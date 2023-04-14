using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using SafWeb.DataLayer.Solicitacao;
using FrameWork.BusinessLayer.Utilitarios;
using System.Data;
using SafWeb.DataLayer.Lista;
using FrameWork.BusinessLayer.Usuarios;

namespace SafWeb.BusinessLayer.Solicitacao
{
    namespace Enumeradores
    {
        public enum ETipoSolicitacaoGrupo
        {
            Solicitacao = 1,
            Escala = 2,
            Permissao = 3
        }
    }

    public class BLSolicitacao
    {


        #region Listar Solicita��es Aprova��o Cracha Titular

        /// <summary>
        ///      Lista Solicita��es pendente aprova��o Cracha Titular
        /// </summary>
        /// <history>
        ///      [haguiar] created 28/02/2012 10:04
        /// </history>
        /// <returns></returns>
        public Collection<SafWeb.Model.Solicitacao.PermissaoCrachaTitular> ListarSolicPendAprovCrachaTitular(int pintId_UsuarioAprovador)
        {
            DLSolicitacao objDLSolicitacao = new DLSolicitacao();

            try
            {
                return objDLSolicitacao.ListarSolicPendAprovCrachaTitular(pintId_UsuarioAprovador);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " M�todo: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLSolicitacao.Finalizar();
            }
        }

        #endregion

        #region Listar Solicita��es Aprova��o

        /// <summary>
        ///      Lista Solicita��es pendente aprova��o
        /// </summary>
        /// <history>
        ///      [vsantos] created 06/07/2009
        /// </history>
        /// <returns></returns>
        public Collection<SafWeb.Model.Solicitacao.Solicitacao> ListarSolicPendAprov(decimal pdecCodUsuAprov)
        {
            DLSolicitacao objDLSolicitacao = new DLSolicitacao();

            try
            {
                return objDLSolicitacao.ListarSolicPendAprov(pdecCodUsuAprov);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " M�todo: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLSolicitacao.Finalizar();
            }
        }

        #endregion

        #region Alterar Fluxo Aprova��o

        /// <summary>
        ///      Alterar Fluxo Aprova��o
        /// </summary>
        /// <history>
        ///      [vsantos] created 06/07/2009
        /// </history>
        /// <returns></returns>
        public bool AlterarFluxoAprov(SafWeb.Model.Solicitacao.FluxoAprovacao pobjFluxoAprov)
        {
            DLSolicitacao objDLSolicitacao = new DLSolicitacao();

            try
            {
                return objDLSolicitacao.AlterarFluxoAprov(pobjFluxoAprov);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " M�todo: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLSolicitacao.Finalizar();
            }
        }

        #endregion

        #region Inserir Fluxo Aprova��o

        /// <summary>
        ///      Inserir Fluxo Aprova��o
        /// </summary>
        /// <history>
        ///      [vsantos] created 06/07/2009
        /// </history>
        /// <returns></returns>
        public int InserirFluxoAprov(SafWeb.Model.Solicitacao.FluxoAprovacao pobjFluxoAprov)
        {
            DLSolicitacao objDLSolicitacao = new DLSolicitacao();

            try
            {
                return objDLSolicitacao.InserirFluxoAprov(pobjFluxoAprov);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " M�todo: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLSolicitacao.Finalizar();
            }
        }

        #endregion

        #region Listar Solicita��es Cracha Titular

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        ///  Lista de solicita��es  Cracha Titular
        /// </summary> 
        /// <returns>Collection PermissaoCrachaTitular</returns> 
        /// <history> 
        ///     [haguiar] 25/02/2012 15:45
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public Collection<SafWeb.Model.Solicitacao.PermissaoCrachaTitular> ListarSolicitacoesCrachaTitular
            (            
	            int pintId_UsuarioSolicitante,
	            int pintId_SolicitacaoCrachaTitular,
	            int pintID_REGIONAL,
	            int pintID_FILIAL,	
	            int pintID_STATUSSOLICITACAO,	
                int pintId_TipoSolicitacao,
	            DateTime? Dt_PERIODOINI,
	            DateTime? Dt_PERIODOFIM,
	            string pstrNOM_COLABORADOR,
	            string pstrUSU_C_NOME
            )
        {
            DLSolicitacao objDLSolicitacao = new DLSolicitacao();

            try
            {
                return objDLSolicitacao.ListarSolicitacoesCrachaTitular(pintId_UsuarioSolicitante,
                                                                        pintId_SolicitacaoCrachaTitular,
                                                                        pintID_REGIONAL,
                                                                        pintID_FILIAL,
                                                                        pintID_STATUSSOLICITACAO,
                                                                        pintId_TipoSolicitacao,
                                                                        Dt_PERIODOINI,
                                                                        Dt_PERIODOFIM,
                                                                        pstrNOM_COLABORADOR,
                                                                        pstrUSU_C_NOME);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " M�todo: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLSolicitacao.Finalizar();
            }
        }

        #endregion

        #region Listar Solicita��es

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        ///  Lista de solicita��es 
        /// </summary> 
        /// <returns>Collection Solicita��o</returns> 
        /// <history> 
        ///     [vsantos] 06/07/2009 Created 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public Collection<SafWeb.Model.Solicitacao.Solicitacao> ListarSolicitacoes(int pintCodSolicitacao,
                                                                                   int pintCodEmpresa,
                                                                                   int pintCodRegional,
                                                                                   int pintCodFilial,
                                                                                   int pintCodStatusSolic,
                                                                                   int pintCodTipoSolic,
                                                                                   DateTime? pdatInicioVisita,
                                                                                   DateTime? pdatFimVisita,
                                                                                   string pstrNomVisitado,
                                                                                   string pstrNomVisitante,
                                                                                   string pstrNomSolicitante,
                                                                                   string pstrNomAprovador,
                                                                                   int pintUsuario)
        {
            DLSolicitacao objDLSolicitacao = new DLSolicitacao();

            try
            {
                return objDLSolicitacao.ListarSolicitacoes(pintCodSolicitacao,
                                                           pintCodEmpresa,
                                                           pintCodRegional,
                                                           pintCodFilial,
                                                           pintCodStatusSolic,
                                                           pintCodTipoSolic,
                                                           pdatInicioVisita,
                                                           pdatFimVisita,
                                                           pstrNomVisitado,
                                                           pstrNomVisitante,
                                                           pstrNomSolicitante,
                                                           pstrNomAprovador,
                                                           pintUsuario);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " M�todo: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLSolicitacao.Finalizar();
            }
        }

        #endregion

        #region Listar Status Solicita��o

        /// <summary>
        ///      Lista status da solicita��o
        /// </summary>
        /// <history>
        ///      [vsantos] created 06/07/2009
        /// </history>
        /// <returns></returns>
        public Collection<SafWeb.Model.Solicitacao.Status> ListarStatus()
        {
            DLSolicitacao objDLSolicitacao = new DLSolicitacao();

            try
            {
                return objDLSolicitacao.ListarStatus();
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " M�todo: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLSolicitacao.Finalizar();
            }
        }

        #endregion
                
        #region Listar Tipo Solicita��o

        /// <summary>
        ///      Lista Tipo da solicita��o
        /// </summary>
        /// <history>
        ///      [vsantos] created 06/07/2009
        /// </history>
        /// <returns></returns>
        public Collection<SafWeb.Model.Solicitacao.TipoSolicitacao> ListarTipoSolicitacao()
        {
            DLSolicitacao objDLSolicitacao = new DLSolicitacao();

            try
            {
                return objDLSolicitacao.ListarTipoSolicitacao();
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " M�todo: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLSolicitacao.Finalizar();
            }
        }

        #endregion

        #region Listar Tipo Solicita��o Escala Todos

        /// <summary>
        ///      Lista Tipo da solicita��o Todos
        /// </summary>
        /// <history>
        ///      [cmarchi] created 17/2/2009
        /// </history>
        /// <returns></returns>
        public Collection<SafWeb.Model.Solicitacao.TipoSolicitacao> ListarTipoSolicitacaoTodos()
        {
            DLSolicitacao objDLSolicitacao = new DLSolicitacao();

            try
            {
                return objDLSolicitacao.ListarTipoSolicitacaoTodos();
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " M�todo: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLSolicitacao.Finalizar();
            }
        }
        #endregion

        #region Listar Tipo Escala

        /// <summary>
        ///      Lista Tipo da solicita��o ou Escalas
        /// </summary>
        /// <history>
        ///      [tgerevini] created 31/5/2010
        /// </history>
        /// <returns></returns>
        public Collection<SafWeb.Model.Solicitacao.TipoSolicitacao> ListarTipoSolicitacao(Boolean pblnEscala)
        {
            DLSolicitacao objDLSolicitacao = new DLSolicitacao();

            try
            {
                return objDLSolicitacao.ListarTipoSolicitacao(pblnEscala);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " M�todo: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLSolicitacao.Finalizar();
            }
        }
        #endregion

        #region Listar Tipo Solicita��o Escala Todos

        /// <summary>
        ///      Lista Tipo da solicita��o Todos
        /// </summary>
        /// <history>
        ///      [cmarchi] created 17/2/2009
        /// </history>
        /// <returns></returns>
        public Collection<SafWeb.Model.Solicitacao.TipoSolicitacao> ListarTipoSolicitacaoGrupo(Enumeradores.ETipoSolicitacaoGrupo Id_TipoSolcitacaoGrp)
        {
            DLSolicitacao objDLSolicitacao = new DLSolicitacao();

            try
            {
                return objDLSolicitacao.ListarTipoSolicitacaoGrupo((int)Id_TipoSolcitacaoGrp);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " M�todo: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLSolicitacao.Finalizar();
            }
        }
        #endregion

        #region Listar Fluxo Aprova��o

        /// <summary>
        ///      Lista fluxo aprova��o
        /// </summary>
        /// <history>
        ///      [vsantos] created 07/07/2009
        /// </history>
        /// <returns></returns>
        public Collection<SafWeb.Model.Solicitacao.FluxoAprovacao> ListarFluxoAprovacao(int pintCodTipoSolic)
        {
            DLSolicitacao objDLSolicitacao = new DLSolicitacao();

            try
            {
                return objDLSolicitacao.ListarFluxoAprovacao(pintCodTipoSolic);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " M�todo: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLSolicitacao.Finalizar();
            }
        }

        #endregion

        #region Listar N�vel Aprova��o

        /// <summary>
        ///      Lista n�vel aprova��o
        /// </summary>
        /// <history>
        ///      [vsantos] created 10/07/2009
        /// </history>
        /// <returns></returns>
        public Collection<SafWeb.Model.Solicitacao.NivelAprovacao> ListarNivelAprovacao()
        {
            DLSolicitacao objDLSolicitacao = new DLSolicitacao();

            try
            {
                return objDLSolicitacao.ListarNivelAprovacao();
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " M�todo: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLSolicitacao.Finalizar();
            }
        }

        #endregion

        #region Listar Motivo Visita

        /// <summary>
        ///      Lista o motivo da visita
        /// </summary>
        /// <history>
        ///      [mribeiro] created 07/07/2009
        /// </history>
        /// <returns></returns>
        public Collection<SafWeb.Model.Solicitacao.MotivoSolicitacao> ListarMotivoSolicitacao()
        {
            DLSolicitacao objDLSolicitacao = new DLSolicitacao();

            try
            {
                return objDLSolicitacao.ListarMotivoVisita();
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " M�todo: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLSolicitacao.Finalizar();
            }
        }

        #endregion

        #region Listar Colaboradores Lista

        /// <summary>
        ///      Lista Colaboradores da Lista de uma solicita��o
        /// </summary>
        /// <history>
        ///      [mribeiro] created 07/07/2009
        /// </history>
        /// <returns></returns>
        public DataTable ListarColaboradoresLista(int pintSolicitacao,
                                                  int pintLista)
        {
            DLSolicitacao objDLSolicitacao = new DLSolicitacao();

            try
            {
                return objDLSolicitacao.ListarColaboradoresLista(pintSolicitacao, pintLista);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " M�todo: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLSolicitacao.Finalizar();
            }
        }

        #endregion


        #region Listar Ve�culos Lista


        public DataTable ListarVeiculosLista(int pintSolicitacao)
        {
            DLSolicitacao objDLSolicitacao = new DLSolicitacao();

            try
            {
                return objDLSolicitacao.ListarVeiculosLista(pintSolicitacao);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " M�todo: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLSolicitacao.Finalizar();
            }
        }

        #endregion

        #region Aprovar Solicita��o cracha titular

        /// <summary>
        ///      Aprovar solicita��o de cracha titular
        /// </summary>
        /// <history>
        ///      [haguiar] created 28/02/2012 08:53
        /// </history>
        /// <returns></returns>
        public bool AprovarSolicitacaoCrachaTitular(int pintId_SolicitacaoCrachaTitular, int pintId_UsuarioAprovador)
        {
            DLSolicitacao objDLSolicitacao = new DLSolicitacao();

            try
            {
                return objDLSolicitacao.AprovarSolicitacaoCrachaTitular(pintId_SolicitacaoCrachaTitular, pintId_UsuarioAprovador);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " M�todo: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLSolicitacao.Finalizar();
            }
        }

        #endregion

        #region Aprovar Solicita��o

        /// <summary>
        ///      Aprovar solicita��o
        /// </summary>
        /// <history>
        ///      [vsantos] created 06/07/2009
        /// </history>
        /// <returns></returns>
        public bool AprovarSolicitacao(int pintCodSolicitacao, decimal pdecCodUsuarioAprov)
        {
            DLSolicitacao objDLSolicitacao = new DLSolicitacao();

            try
            {
                return objDLSolicitacao.AprovarSolicitacao(pintCodSolicitacao, pdecCodUsuarioAprov);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " M�todo: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLSolicitacao.Finalizar();
            }
        }

        #endregion

        #region Reprovar Solicita��o Cracha Titular

        /// <summary>
        ///      Reprovar solicita��o Cracha Titular
        /// </summary>
        /// <history>
        ///      [haguiar] created 28/02/2012 08:54
        /// </history>
        /// <returns></returns>
        public bool ReprovarSolicitacaoCrachaTitular(int pintId_SolicitacaoCrachaTitular, int pintId_UsuarioAprovador, string pstrMotivoReprov)
        {
            DLSolicitacao objDLSolicitacao = new DLSolicitacao();

            try
            {
                return objDLSolicitacao.ReprovarSolicitacaoCrachaTitular(pintId_SolicitacaoCrachaTitular, pintId_UsuarioAprovador, pstrMotivoReprov);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " M�todo: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLSolicitacao.Finalizar();
            }
        }

        #endregion

        #region Reprovar Solicita��o

        /// <summary>
        ///      Reprovar solicita��o
        /// </summary>
        /// <history>
        ///      [vsantos] created 06/07/2009
        /// </history>
        /// <returns></returns>
        public bool ReprovarSolicitacao(int pintCodSolicitacao, decimal pdecCodUsuarioAprov, string pstrMotivoReprov)
        {
            DLSolicitacao objDLSolicitacao = new DLSolicitacao();

            try
            {
                return objDLSolicitacao.ReprovarSolicitacao(pintCodSolicitacao, pdecCodUsuarioAprov, pstrMotivoReprov);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " M�todo: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLSolicitacao.Finalizar();
            }
        }

        #endregion

        #region Listar Hist�rico Solicita��o Cracha Titular

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        ///     Listar Hist�rico Solicita��o Cracha Titular
        /// </summary> 
        /// <param name="pintId_SolicitacaoCrachaTitular">C�digo da Solicita��o</param>
        /// <returns>DataSet</returns> 
        /// <history> 
        ///     [haguiar] 27/02/2012 19:42
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public DataSet ListarHistoricoCrachaTitular(int pintId_SolicitacaoCrachaTitular)
        {
            DLSolicitacao objDLSolicitacao = new DLSolicitacao();

            try
            {
                return objDLSolicitacao.ListarHistoricoCrachaTitular(pintId_SolicitacaoCrachaTitular);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " M�todo: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLSolicitacao.Finalizar();
            }
        }

        #endregion

        #region Listar Hist�rico Solicita��o

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        ///     Listar Hist�rico Solicita��o
        /// </summary> 
        /// <param name="pintCodSolicitacao">C�digo da Solicita��o</param>
        /// <param name="pintCodVisitante">C�digo do Visitante</param>
        /// <returns>DataSet</returns> 
        /// <history> 
        ///     [vsantos] 07/07/2009 Created 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public DataSet ListarHistoricoSolic(int pintCodSolicitacao,
                                            int pintCodVisitante)
        {
            DLSolicitacao objDLSolicitacao = new DLSolicitacao();

            try
            {
                return objDLSolicitacao.ListarHistoricoSolic(pintCodSolicitacao,
                                                             pintCodVisitante);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " M�todo: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLSolicitacao.Finalizar();
            }
        }

        #endregion

        #region Inserir Solicita��o Cracha Titular

        /// <summary>
        ///      Insere uma solicita��o Cracha Titular
        /// </summary>
        /// <history>
        ///      [haguiar] created 25/02/2012 15:40
        /// </history>
        /// <param name="pobjSolicitacao">Objeto Solicitacao Cracha Titular</param>
        /// <returns>C�digo da Solicitacao</returns>
        public int InserirPermissaoCrachaTitular(SafWeb.Model.Solicitacao.PermissaoCrachaTitular pobjSolicitacao)
        {
            DLSolicitacao objDLSolicitacao = new DLSolicitacao();

            try
            {
                return objDLSolicitacao.InserirPermissaoCrachaTitular(pobjSolicitacao);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " M�todo: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLSolicitacao.Finalizar();
            }
        }

        #endregion

        #region Inserir Solicita��o

        /// <summary>
        ///      Insere uma solicita��o
        /// </summary>
        /// <history>
        ///      [mribeiro] created 01/07/2009
        /// </history>
        /// <param name="pobjSolicitacao">Objeto Solicitacao</param>
        /// <returns>C�digo da Solicitacao</returns>
        public int Inserir(SafWeb.Model.Solicitacao.Solicitacao pobjSolicitacao)
        {
            DLSolicitacao objDLSolicitacao = new DLSolicitacao();

            try
            {
                return objDLSolicitacao.InserirSolicitacao(pobjSolicitacao);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " M�todo: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLSolicitacao.Finalizar();
            }
        }

        #endregion

        #region Inserir Solicita��oLista

        /// <summary>
        ///      Insere uma solicita��o que possui uma lista de colaboradors
        /// </summary>
        /// <param name="pintCodColaboradorLista">Codigo da lista</param>
        /// <param name="pintCodSolicitacao">Codigo da Solicita��o</param>
        /// <history>
        ///      [mribeiro] created 13/07/2009
        /// </history>
        public void InserirSolicitacaoLista(int pintCodSolicitacao,
                                            int pintCodColaboradorLista,
                                            string pstrColRetirados)
        {
            DLSolicitacao objDLSolicitacao = new DLSolicitacao();
            DLLista objDLLista = new DLLista();
            bool blnGravar;
            string[] strRetirados = { "" };
            int intCount = 0;

            Collection<SafWeb.Model.Colaborador.Colaborador> colColaborador;

            objDLSolicitacao.BeginTransaction();

            try
            {
                //obtem os colaboradores da lista
                colColaborador = objDLLista.ListarColaboradoresDaLista(pintCodColaboradorLista);

                if (pstrColRetirados != null)
                {
                    strRetirados = pstrColRetirados.Split(new char[] { ',' });
                    intCount = strRetirados.Length;
                }

                //colaboradores da lista
                for (int i = 0; i < colColaborador.Count; i++)
                {
                    if (colColaborador[i].Situacao)
                    {
                    if (intCount != 0)
                    {
                        //colaboradores da lista que v�o ser inclusos na solicita��o
                        for (int j = 0; j < intCount; j++)
                        {
                            if (Convert.ToInt32(strRetirados[j]) == colColaborador[i].IdColaborador)
                            {
                                if (objDLSolicitacao.InserirSolicitacaoLista(pintCodSolicitacao,
                                                                     pintCodColaboradorLista,
                                                                     colColaborador[i].IdColaborador) == false)
                                {
                                    objDLSolicitacao.RollBackTransaction();
                                }
                                break;
                            }
                        }
                    }
                    else
                    {
                        if (objDLSolicitacao.InserirSolicitacaoLista(pintCodSolicitacao,
                                                                     pintCodColaboradorLista,
                                                                     colColaborador[i].IdColaborador) == false)
                        {
                            objDLSolicitacao.RollBackTransaction();
                        }
                    }
                }
                }

                objDLSolicitacao.CommitTransaction();
                
            }
            catch (Exception ex)
            {
                objDLSolicitacao.RollBackTransaction();
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " M�todo: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLSolicitacao.Finalizar();
            }
        }

        #endregion

        #region Inserir Solicita��oListaVeiculos

        /// <summary>
        ///      Insere uma solicita��o que possui uma lista de colaboradors
        /// </summary>
        /// <param name="pintCodColaboradorLista">Codigo da lista</param>
        /// <param name="pintCodSolicitacao">Codigo da Solicita��o</param>
        /// <history>
        ///      [cfrancisco] created 04/05/2012
        /// </history>
        public void InserirSolicitacaoListaVeiculos(int pintCodSolicitacao,
                                            int pintCodVeiculoLista)
        {
            DLSolicitacao objDLSolicitacao = new DLSolicitacao();

            try
            {
                objDLSolicitacao.InserirSolicitacaoListaVeiculos(pintCodSolicitacao, pintCodVeiculoLista);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " M�todo: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLSolicitacao.Finalizar();
            }
        }

        #endregion

        #region Inserir Status Solicitacao Cracha Titular

        /// <summary>
        ///      Insere o status inicial da solicitacao de cracha titular
        /// </summary>
        /// <param name="pintId_SolicitacaoCrachaTitular">Codigo da Solicita��o Cracha Titular</param>
        /// <param name="pintID_STATUSSOLICITACAO">C�digo do Status da Solicita��o</param>
        /// <param name="pintID_USUARIOAPROVADOR">C�digo do Usu�rio</param>
        /// <history>
        ///      [haguiar] created 25/02/2012 15:50
        /// </history>
        public void InserirStatusPermissaoCrachaTitular(int pintId_SolicitacaoCrachaTitular,
                                                         int pintID_USUARIOAPROVADOR,
                                                         int pintIdTipoSolicitacao)
        {
            DLSolicitacao objDLSolicitacao = new DLSolicitacao();

            objDLSolicitacao.BeginTransaction();

            try
            {
                //insere o status de solicita��o aberta
                if (objDLSolicitacao.InserirStatusPermissaoCrachaTitular(pintId_SolicitacaoCrachaTitular,
                                                                             0,
                                                                             Convert.ToInt32(BLAcesso.IdUsuarioLogado()),
                                                                             DateTime.Now))
                {
                    //insere o primeiro status da solicita��o
                    if (objDLSolicitacao.InserirStatusPermissaoCrachaTitular(pintId_SolicitacaoCrachaTitular,
                                                                              this.ObterPrimeiroStatus(pintIdTipoSolicitacao),
                                                                              pintID_USUARIOAPROVADOR,
                                                                              null))
                    {
                        objDLSolicitacao.CommitTransaction();
                    }
                    else
                    {
                        objDLSolicitacao.RollBackTransaction();
                    }
                }
                else
                {
                    objDLSolicitacao.RollBackTransaction();
                }

            }
            catch (Exception ex)
            {
                objDLSolicitacao.RollBackTransaction();
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " M�todo: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLSolicitacao.Finalizar();
            }
        }

        #endregion

        #region Inserir Status Solicitacao

        /// <summary>
        ///      Insere o status inicial da solicitacao
        /// </summary>
        /// <param name="pintCodSolicitacao">Codigo da Solicita��o</param>
        /// <history>
        ///      [mribeiro] created 13/07/2009
        /// </history>
        public void InserirStatusSolicitacao(int pintCodSolicitacao,
                                             int intIdUsuarioAprov,
                                             int intIdTipoSolicitacao)
        {
            DLSolicitacao objDLSolicitacao = new DLSolicitacao();

            objDLSolicitacao.BeginTransaction();

            try
            {
                //insere o status de solicita��o aberta
                if(objDLSolicitacao.InserirStatusSolicitacao(pintCodSolicitacao, 
                                                             0, 
                                                             Convert.ToInt32(BLAcesso.IdUsuarioLogado()),
                                                             DateTime.Now))
                {
                    //insere o primeiro status da solicita��o
                    if (objDLSolicitacao.InserirStatusSolicitacao(pintCodSolicitacao, 
                                                                  this.ObterPrimeiroStatus(intIdTipoSolicitacao), 
                                                                  intIdUsuarioAprov,
                                                                  null))
                    {
                        objDLSolicitacao.CommitTransaction();
                    }
                    else
                    {
                        objDLSolicitacao.RollBackTransaction();
                    }
                }
                else
                {
                    objDLSolicitacao.RollBackTransaction();
                }

            }
            catch (Exception ex)
            {
                objDLSolicitacao.RollBackTransaction();
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " M�todo: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLSolicitacao.Finalizar();
            }
        }

        #endregion

        #region Obter Primeiro Status Solicitacao

        /// <summary>
        ///      Obtem o primeiro status da solicita��o de acordo com o tipo da solicita��o
        /// </summary>
        /// <param name="pintTipoSolicitacao">Tipo da Solicita��o</param>
        /// <history>
        ///      [mribeiro] created 13/07/2009
        /// </history>
        /// <returns>Status da Solicitacao</returns>
        public int ObterPrimeiroStatus(int pintTipoSolicitacao)
        {
            DLSolicitacao objDLSolicitacao = new DLSolicitacao();

            try
            {
                return objDLSolicitacao.ObterPrimeiroStatus(pintTipoSolicitacao);              
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " M�todo: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLSolicitacao.Finalizar();
            }
        }

        #endregion

        #region Obter �ltimo Status Solicitacao Cracha Titular

        /// <summary>
        ///      Obtem o ultimo status da solicita��o
        /// </summary>
        /// <param name="pintId_SolicitacaoCrachaTitular">Id da Solicita��o Cracha Titular</param>
        /// <history>
        ///      [haguiar] created 26/02/2012 09:12
        /// </history>
        /// <returns>Status da Solicitacao</returns>
        public string ObterUltimoStatusCrachaTitular(int pintId_SolicitacaoCrachaTitular)
        {
            DLSolicitacao objDLSolicitacao = new DLSolicitacao();

            try
            {
                return objDLSolicitacao.ObterUltimoStatusCrachaTitular(pintId_SolicitacaoCrachaTitular);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " M�todo: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLSolicitacao.Finalizar();
            }
        }

        #endregion

        #region Obter �ltimo Status Solicitacao

        /// <summary>
        ///      Obtem o ultimo status da solicita��o
        /// </summary>
        /// <param name="pintSolicitacao">Id da Solicita��o</param>
        /// <history>
        ///      [mribeiro] created 22/10/2009
        /// </history>
        /// <returns>Status da Solicitacao</returns>
        public string ObterUltimoStatus(int pintSolicitacao)
        {
            DLSolicitacao objDLSolicitacao = new DLSolicitacao();

            try
            {
                return objDLSolicitacao.ObterUltimoStatus(pintSolicitacao);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " M�todo: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLSolicitacao.Finalizar();
            }
        }

        #endregion

        #region Obter Codigo Aprovador

        /// <summary>
        ///      Obtem o codigo (RE) do aprovador e o ID
        /// </summary>
        /// <param name="pintIdAprovador">Id do Aprovador</param>
        /// <history>
        ///      [mribeiro] created 16/07/2009
        /// </history>
        /// <returns>Codigo (RE) do aprovador</returns>
        public DataTable ObterCodigoAprovador(int pintIdAprovador)
        {
            DLSolicitacao objDLSolicitacao = new DLSolicitacao();

            try
            {
                return objDLSolicitacao.ObterCodigoAprovador(pintIdAprovador);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " M�todo: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLSolicitacao.Finalizar();
            }
        }

        #endregion

        #region Listar Aprovadores

        /// <summary>
        ///      Lista os aprovadores
        /// </summary>
        /// <param name="pstrRegional">String com as Regionais</param>
        /// <param name="pstrFilial">String com a Filiais</param>
        /// <param name="pstrTipoSolicitacao">String com os tipos de solicita��es</param>
        /// <history>
        ///      [mribeiro] created 15/07/2009
        /// </history>
        /// <returns>Objeto Aprovador</returns>
        public Collection<SafWeb.Model.Solicitacao.Aprovador> ListarAprovadores(string pstrRegional,
                                                                                string pstrFilial,
                                                                                string pstrTipoSolicitacao)                   
        {
            DLSolicitacao objDLSolicitacao = new DLSolicitacao();

            try
            {
                return objDLSolicitacao.ListarAprovadores(pstrRegional,
                                                          pstrFilial,
                                                          pstrTipoSolicitacao);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " M�todo: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLSolicitacao.Finalizar();
            }
        }

        #endregion

        #region Listar Aprovadores de Contingencia

        /// <summary>
        ///      Lista os aprovadores de contingencia
        /// </summary>
        /// <param name="pintRegional">int com as Regionais</param>
        /// <param name="pintFilial">int com a Filiais</param>
        /// <history>
        ///      [haguiar] created 08/04/2011 08:31
        /// </history>
        /// <returns>Objeto Aprovador</returns>
        public Collection<SafWeb.Model.Solicitacao.Aprovador> ListarAprovadoresConting(int pintRegional,
                                                                                int pintFilial)
        {
            DLSolicitacao objDLSolicitacao = new DLSolicitacao();

            try
            {
                return objDLSolicitacao.ListarAprovadoresConting(pintRegional,
                                                          pintFilial);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " M�todo: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLSolicitacao.Finalizar();
            }
        }

        #endregion

        #region Listar Todos Aprovadores

        /// <summary>
        ///      Lista todos os aprovadores
        /// </summary>
        /// <history>
        ///      [mribeiro] created 25/10/2009
        /// </history>
        /// <returns>Objeto Aprovador</returns>
        public Collection<SafWeb.Model.Solicitacao.Aprovador> ListarTodosAprovadores()
        {
            DLSolicitacao objDLSolicitacao = new DLSolicitacao();

            try
            {
                return objDLSolicitacao.ListarTodosAprovadores();
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " M�todo: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLSolicitacao.Finalizar();
            }
        }

        #endregion

        #region Inserir Aprovador Segundo N�vel

        /// <summary>
        ///      Insere o aprovador prefencial de segundo n�vel
        /// </summary>
        /// <param name="pintIdAprovador">Id aprovador</param>
        /// <param name="pintIdAprovadorSegNivel">Id aprovador de segundo n�vel</param>
        /// <history>
        ///      [mribeiro] created  26/10/2009
        /// </history>
        public bool InserirAprovadorSegNiveil(int pintIdAprovador,
                                             int pintIdAprovadorSegNivel)
        {
            DLSolicitacao objDLSolicitacao = new DLSolicitacao();

            try
            {
                return objDLSolicitacao.InserirAprovadorSegNiveil(pintIdAprovador,
                                                                  pintIdAprovadorSegNivel);

            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " M�todo: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLSolicitacao.Finalizar();
            }
        }

        #endregion

        #region Alterar Situa��o Solicita��o Cracha Titular

        /// <summary>
        ///      Alterar a situa��o da solicita��o Cracha Titular (Ativar/Inativar)
        /// </summary>
        /// <param name="pintId_SolicitacaoCrachaTitular">C�digo da Solicita��o Cracha Titular</param>
        /// <param name="pblnFlgSituacao"></param>
        /// <history>
        ///      [haguiar] created 25/02/2012 23:22
        /// </history>
        /// <returns></returns>
        public bool AlterarSituacaoCrachaTitular(int pintId_SolicitacaoCrachaTitular,
                                    bool pblnFlgSituacao,
                                    int pintUsuario)
        {
            DLSolicitacao objDLSolicitacao = new DLSolicitacao();

            try
            {
                return objDLSolicitacao.AlterarSituacaoCrachaTitular(pintId_SolicitacaoCrachaTitular,
                                                        pblnFlgSituacao,
                                                        pintUsuario);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " M�todo: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLSolicitacao.Finalizar();
            }
        }

        #endregion

        #region Alterar Situa��o Solicita��o

        /// <summary>
        ///      Alterar a situa��o da solicita��o (Ativar/Inativar)
        /// </summary>
        /// <param name="intIdSolicitacao">C�digo da Solicita��o</param>
        /// <param name="intStuacao">1 - True/ 2 - False</param>
        /// <history>
        ///      [mribeiro] created 15/07/2009
        /// </history>
        /// <returns></returns>
        public bool AlterarSituacao(int intIdSolicitacao,
                                    int intStuacao,
                                    int intUsuario)
        {
            DLSolicitacao objDLSolicitacao = new DLSolicitacao();

            try
            {
                return objDLSolicitacao.AlterarSituacao(intIdSolicitacao,
                                                        intStuacao,
                                                        intUsuario);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " M�todo: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLSolicitacao.Finalizar();
            }
        }

         #endregion

        #region Alterar Solicita��o

        /// <summary>
        ///      Alterar a solicita��o 
        /// </summary>
        /// <param name="pobjSolicitacao">Objeto Solicita��o</param>
        /// <history>
        ///      [mribeiro] created 21/07/2009
        /// </history>
        /// <returns>True/False</returns>
        public bool AlterarSolicitacao(SafWeb.Model.Solicitacao.Solicitacao pobjSolicitacao,
                                       int pintIdListaAnterior)
        {
            DLSolicitacao objDLSolicitacao = new DLSolicitacao();
            bool blnRetorno = false;

            try
            {
                objDLSolicitacao.BeginTransaction();

                //verifica se ainda nao foi aprovado
                if (objDLSolicitacao.VerificarStatus(pobjSolicitacao.Codigo) == true)
                {
                    if (objDLSolicitacao.AlterarSolicitacao(pobjSolicitacao))
                    {
                        //exclui a lista anterior
                        if (pintIdListaAnterior > 0)
                        {
                            //insere as solicitacoes dos colaboradores da lista.
                            objDLSolicitacao.ExcluirSolicitacaoLista(pobjSolicitacao.Codigo,
                                                                     pintIdListaAnterior);
                        }

                        //insere as solicitacoes dos colaboradores da lista.
                        objDLSolicitacao.ExcluirStatus(pobjSolicitacao.Codigo);

                        objDLSolicitacao.CommitTransaction();
                        blnRetorno = true;
                    }
                    else
                    {
                        objDLSolicitacao.RollBackTransaction();
                    }
                }
                else
                {
                    blnRetorno = false;
                }

                return blnRetorno;
            }
            catch (Exception ex)
            {
                objDLSolicitacao.RollBackTransaction();
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " M�todo: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLSolicitacao.Finalizar();
            }
        }

        #endregion

        #region Listar Feriados

        /// <summary>
        ///      Lista os feriados
        /// </summary>
        /// <param name="pintRegional">Id Regional</param>
        /// <param name="pintFilial"Id Filial</param>
        /// <history>
        ///      [mribeiro] created 17/07/2009
        /// </history>
        /// <returns>DataTable com os feriados</returns>
        public DataTable ListarFeriado(int pintRegional,
                                       int pintFilial)
        {
            DLSolicitacao objDLSolicitacao = new DLSolicitacao();

            try
            {
                return objDLSolicitacao.ListarFeriado(pintRegional,
                                                      pintFilial);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " M�todo: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLSolicitacao.Finalizar();
            }
        }

        #endregion

        #region Obter Solicita��o

        /// <summary>
        ///      Obtem os dados da solicita��o
        /// </summary>
        /// <param name="pintIdAprovador">Id da Solicita��o</param>
        /// <history>
        ///      [mribeiro] created 21/07/2009
        /// </history>
        /// <returns>Objeto SolicitacaoColaborador</returns>
        public SafWeb.Model.Solicitacao.SolicitacaoColaborador ObterSolicitacao(int pintIdSolicitacao)
        {
            DLSolicitacao objDLSolicitacao = new DLSolicitacao();

            try
            {
                return objDLSolicitacao.ObterSolicitacao(pintIdSolicitacao);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " M�todo: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLSolicitacao.Finalizar();
            }
        }

        #endregion

        #region Exluir Status

        /// <summary>
        ///      Exclui os status anteriores da solicita��o 
        /// </summary>
        /// <param name="pintIdSolicitacao">C�digo da Solicita��o</param>
        /// <history>
        ///      [mribeiro] created 21/07/2009
        /// </history>
        /// <returns>True/False</returns>
        public bool ExcluirStatus(int pintIdSolicitacao)
        {
            DLSolicitacao objDLSolicitacao = new DLSolicitacao();

            try
            {
                return objDLSolicitacao.ExcluirStatus(pintIdSolicitacao);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " M�todo: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLSolicitacao.Finalizar();
            }
        }

        #endregion

        #region Excluir Solicita��oLista

        /// <summary>
        ///      Exclui o registro da Solicita��oLista em caso de altera��o
        /// </summary>
        /// <param name="pintIdSolicitacao">C�digo da Solicita��o</param>
        /// <param name="pintIdLIsta">C�digo da Lista</param>
        /// <history>
        ///      [mribeiro] created 21/07/2009
        /// </history>
        /// <returns>True/False</returns>
        public bool ExcluirSolicitacaoLista(int pintIdSolicitacao,
                                            int pintIdLIsta)
        {
            DLSolicitacao objDLSolicitacao = new DLSolicitacao();

            try
            {
                return objDLSolicitacao.ExcluirSolicitacaoLista(pintIdSolicitacao,
                                                                pintIdLIsta);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " M�todo: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLSolicitacao.Finalizar();
            }
        }

        #endregion

        #region Verificar Status

        /// <summary>
        ///      Verifica se a solicita��o j� saiu do status inicial
        /// </summary>
        /// <param name="pintIdSolicitacao">C�digo da Solicita��o</param>
        /// <history>
        ///      [mribeiro] created 22/07/2009
        /// </history>
        /// <returns>True/False</returns>
        public bool VerificarStatus(int pintIdSolicitacao)
        {
            DLSolicitacao objDLSolicitacao = new DLSolicitacao();

            try
            {
                return objDLSolicitacao.VerificarStatus(pintIdSolicitacao);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " M�todo: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLSolicitacao.Finalizar();
            }
        }

        #endregion

        #region Obter Aprovadores Cracha Titular

        /// <summary>
        ///      Obtem os aprovadores da solicita��o
        /// </summary>
        /// <param name="pintId_SolicitacaoCrachaTitular">Id da Solicita��o cracha titular</param>
        /// <history>
        ///      [haguiar] created 26/02/2012
        /// </history>
        /// <returns>Aprovadores da Solicita��o</returns>
        public DataTable ObterAprovadoresCrachaTitular(int pintId_SolicitacaoCrachaTitular)
        {
            DLSolicitacao objDLSolicitacao = new DLSolicitacao();

            try
            {
                return objDLSolicitacao.ObterAprovadoresCrachaTitular(pintId_SolicitacaoCrachaTitular);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " M�todo: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLSolicitacao.Finalizar();
            }
        }

        #endregion

        #region Obter Aprovadores

        /// <summary>
        ///      Obtem os aprovadores da solicita��o
        /// </summary>
        /// <param name="pintIdAprovador">Id da Solicita��o</param>
        /// <history>
        ///      [mribeiro] created 22/10/2009
        /// </history>
        /// <returns>Aprovadores da Solicita��o</returns>
        public DataTable ObterAprovadores(int pintIdSolicitacao)
        {
            DLSolicitacao objDLSolicitacao = new DLSolicitacao();

            try
            {
                return objDLSolicitacao.ObterAprovadores(pintIdSolicitacao);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " M�todo: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLSolicitacao.Finalizar();
            }
        }

        #endregion

        #region Obter Ultimo Aprovador
        /// <summary>
        /// Obt�m o nome do ultimo aprovador da escala
        /// </summary>
        /// <param name="pintIdSolicitacao">Id da Solicitacao</param>
        /// <returns>Nome do Ultimo Aprovador</returns>
        /// <history>
        ///     [tgerevini] created 23/06/2010
        /// </history>
        public String ObterUltimoAprovador(int pintIdSolicitacao)
        {
            DLSolicitacao objDLSolicitacao = new DLSolicitacao();

            try
            {
                return objDLSolicitacao.ObterUltimoAprovador(pintIdSolicitacao);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " M�todo: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLSolicitacao.Finalizar();
            }
        }
        #endregion
    }
}
