using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections.ObjectModel;
using FrameWork.BusinessLayer.Utilitarios;
using SafWeb.DataLayer.Acesso;
using SafWeb.Model.Acesso;

namespace SafWeb.BusinessLayer.Acesso
{
    public class BLAcessoColaborador
    {
        #region Listar Escalas Colaboradores

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        ///     Lista registros da base 
        /// </summary> 
        /// <param name="pobjAcesso">Model Acesso</param>
        /// <returns>Cole��o de objetos do tipo Acesso</returns> 
        /// <history> 
        ///     [tgerevini] 5/4/2010 Created 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public Collection<ListaAcessoColaborador> Listar(int pintCodigoFilial, string pstrCodigoColaborador, string pstrNomeColaborador,
                                                                        int pintCodigoEscalacao, int pintHoraInicio, int pintHoraFim, int pintMinutoInicio,
                                                                        int pintMinutoFim, int pintCodigoEscalaDepto, int pintFlgStatusAcesso, string pstrHoraAtual,
                                                                        int startIndex, int pageSize, string sortBy, ref int totalRegistros)
        {
            DLAcessoColaborador objDLAcessoColaborador = new DLAcessoColaborador();

            try
            {
                return objDLAcessoColaborador.ListarEscalaColaborador(pintCodigoFilial, pstrCodigoColaborador, pstrNomeColaborador,
                                                                        pintCodigoEscalacao, pintHoraInicio, pintHoraFim, pintMinutoInicio,
                                                                        pintMinutoFim, pintCodigoEscalaDepto, pintFlgStatusAcesso,
                                                                        pstrHoraAtual, startIndex, pageSize, sortBy, ref totalRegistros);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " M�todo: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLAcessoColaborador.Finalizar();
            }
        }

        #endregion

        #region Inserir

        /// <summary>
        ///     Insere os registros na base
        /// </summary>
        /// <param name="pobjCracha">Model AcessoColaborador</param>
        /// <returns>C�digo do AcessoColaborador</returns>
        /// <history>
        ///     [tgerevini] 7/4/2010 Created 
        /// </history>
        public int Inserir(AcessoColaborador pobjAcessoColaborador)
        {
            DLAcessoColaborador objDLAcessoColaborador = null;

            try
            {
                objDLAcessoColaborador = new DLAcessoColaborador();

                return objDLAcessoColaborador.Inserir(pobjAcessoColaborador);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " M�todo: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLAcessoColaborador.Finalizar();
            }
        }

        #endregion


        #region Inserir Acesso Contingencial

        /// <summary>
        ///     Insere acesso contingencial
        /// </summary>
        /// <param name="pobjAcessoContingencai">Model AcessoContingencia</param>
        /// <returns>C�digo do AcessoContingencia</returns>
        /// <history>
        ///     [haguiar] 08/04/2011 09:39 created
        /// </history>
        public int InserirAcessoContingencia(AcessoContingencia pobjAcessoContingencia)
        {
            DLAcessoColaborador objDLAcessoColaborador = null;

            try
            {
                objDLAcessoColaborador = new DLAcessoColaborador();

                return objDLAcessoColaborador.InserirAcessoContingencia(pobjAcessoContingencia);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " M�todo: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLAcessoColaborador.Finalizar();
            }
        }

        #endregion


        #region Alterar

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        ///     Edita registros da base 
        /// </summary> 
        /// <param name="pobjAcesso">Model AcessoColaborador</param>
        /// <returns>Boleano indicando sucesso da atualiza��o</returns> 
        /// <history> 
        ///     [tgerevini] 7/4/2010 Created 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public bool Alterar(AcessoColaborador pobjAcessoColaborador)
        {
            DLAcessoColaborador objDLAcessoColaborador = new DLAcessoColaborador();

            try
            {
                return objDLAcessoColaborador.Alterar(pobjAcessoColaborador);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " M�todo: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLAcessoColaborador.Finalizar();
            }
        }

        #endregion

        #region Obter Ultimo Acesso
        /// <summary>
        /// Obt�m o �ltimo acesso do colaborador
        /// </summary>
        /// <param name="pintIdFilial">Id da Filial</param>
        /// <param name="pintIdEscalacao">Id da Escala��o</param>
        /// <param name="pintIdColaborador">Id do Colaborador</param>
        /// <returns>Objeto AcessoColaborador</returns>
        /// <history>
        ///     [tgerevini] created 13/4/2010
        /// </history>
        public AcessoColaborador ObterUltimoAcesso(int pintIdFilial, int pintIdEscalacao, int pintIdColaborador)
        {
            DLAcessoColaborador objDLAcessoColaborador = new DLAcessoColaborador();

            try
            {
                return objDLAcessoColaborador.ObterUltimoAcesso(pintIdFilial, pintIdEscalacao, pintIdColaborador);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " M�todo: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLAcessoColaborador.Finalizar();
            }
        }
        #endregion

        #region Obter Id do Crach�

        /// <summary>
        ///     Obtem o Id do Usu�rio do Framework
        /// </summary>
        /// <param name="pintIdColaborador">Id do Colaborador</param>
        /// <returns>Id do Usu�rio</returns>
        /// <history>
        ///     [tgerevini] created 27/08/2010
        /// </history>
        public int ObterIdUsuario(int pintIdColaborador)
        {
            DLAcessoColaborador objDLAcessoColaborador = null;

            try
            {
                objDLAcessoColaborador = new DLAcessoColaborador();

                return objDLAcessoColaborador.ObterIdUsuario(pintIdColaborador);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " M�todo: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLAcessoColaborador.Finalizar();
            }
        }

        #endregion

    }
}
