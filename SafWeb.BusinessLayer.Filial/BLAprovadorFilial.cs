using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using FrameWork.BusinessLayer.Utilitarios;
using SafWeb.DataLayer.Filial;
using SafWeb.Model.Filial;
using System.Data;

namespace SafWeb.BusinessLayer.Filial
{
    public class BLAprovadorFilial
    {
        #region Listar

        /// <summary>
        ///      Lista registros de usuários
        /// </summary>
        /// <history>
        ///      [haguiar_5] created 16/02/2011
        /// </history>
        /// <returns>DataTable Usuários</returns>
        public DataTable Listar(int pintRegional, int pintIdFilial, int pintId_Superior, string pstrUSU_C_NOME)
        {
            DLAprovadorFilial objDLAprovadorFilial = new DLAprovadorFilial();

            try
            {
                return objDLAprovadorFilial.Listar(pintRegional, pintIdFilial, pintId_Superior, pstrUSU_C_NOME);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLAprovadorFilial.Finalizar();
            }
        }
        #endregion

        #region ListarAtributos

        /// <summary>
        ///      Lista atributos do aprovador
        /// </summary>
        /// <param name="pintId_Usuario">Id do usuário</param>
        /// <history>
        ///      [haguiar_5] created 17/02/2011
        /// </history>
        /// <returns>Collection AprovadorFilial</returns>
        public Collection<AprovadorFilial> ListarAtributos(int pintId_Usuario)
        {
            DLAprovadorFilial objDLAprovadorFilial = new DLAprovadorFilial();

            try
            {
                return objDLAprovadorFilial.ListarAtributos(pintId_Usuario);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLAprovadorFilial.Finalizar();
            }
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
            DLAprovadorFilial objDLAprovadorFilial = new DLAprovadorFilial();

            try
            {
                return objDLAprovadorFilial.ObterUsuario(pintId_Usuario);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLAprovadorFilial.Finalizar();
            }
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

            DLAprovadorFilial objDLAprovadorFilial = new DLAprovadorFilial();

            try
            {
                return objDLAprovadorFilial.ListarOrigemChamado();
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLAprovadorFilial.Finalizar();
            }

        }
        #endregion


        #region Inserir
        /// <summary>
        /// Insere um atributo de aprovador
        /// </summary>
        /// <param name="pcolAprovadorFilial">Coleçao de atributos do aprovador</param>
        /// <returns>0 ou Erro</returns>
        /// <history>
        ///     [haguiar_5] created 21/02/2011
        /// </history>
        public int InserirAlterarAtributosAprovador(Collection<SafWeb.Model.Filial.AprovadorFilial> pcolAprovadorFilial)
        {
            DLAprovadorFilial objDLAprovadorFilial = new DLAprovadorFilial();
            int intRetorno = 0;

            try
            {
                if (pcolAprovadorFilial.Count > 0)
                {
                    objDLAprovadorFilial.BeginTransaction();

                    foreach (SafWeb.Model.Filial.AprovadorFilial objAprovadorFilial in pcolAprovadorFilial)
                    {
                        if (Convert.ToInt32(objAprovadorFilial.Id_AprovadorFilial) == 0)
                        {
                            intRetorno = objDLAprovadorFilial.Inserir(objAprovadorFilial);
                        }
                        else
                        {
                            intRetorno = objDLAprovadorFilial.Alterar(objAprovadorFilial);
                        }
                    }

                    objDLAprovadorFilial.CommitTransaction();
                }

                return intRetorno;
            }
            catch (Exception ex)
            {
                objDLAprovadorFilial.RollBackTransaction();

                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLAprovadorFilial.Finalizar();
            }
        }
        #endregion

        /*
        #region AlterarSituacao
        /// <summary>
        /// Altera o status da Situação.
        /// </summary>
        /// <param name="pintIdHorarioVerao">Id do horário de verao</param>
        /// <param name="pblSituacao">Flag da Situação</param>
        /// <history>
        ///     [haguiar_4] created 17/01/2011
        /// </history>
        public void AlterarSituacao(int pintIdHorarioVerao, bool pblnSituacao)
        {
            DLHorarioVerao objDLHorarioVerao = new DLHorarioVerao();

            try
            {
                objDLHorarioVerao.AlterarSituacao(pintIdHorarioVerao, pblnSituacao);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLHorarioVerao.Finalizar();
            }
        }
        #endregion

        #region Inserir
        /// <summary>
        /// Insere um horário de verão.
        /// </summary>
        /// <returns>Id_HorarioVerao ou Erro</returns>
        /// <history>
        ///     [haguiar_4] created 17/01/2011
        /// </history>
        public int Inserir(SafWeb.Model.Filial.HorarioVerao pobjHorarioVerao)
        {
            DLHorarioVerao objDLHorarioVerao = new DLHorarioVerao();

            try
            {
                return objDLHorarioVerao.Inserir(pobjHorarioVerao);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLHorarioVerao.Finalizar();
            }
        }
        #endregion

        #region AlterarHorarioVerao
        /// <summary>
        /// Altera um Horário de Verao
        /// </summary>
        /// <param name="gobjHorarioVerao">Objeto Horario Verao</param>
        /// <returns>Valor do Id do Horario de Verao ou Erro</returns>
        /// <history>
        ///     [haguiar_4] created 18/01/2011
        /// </history>
        public int Alterar(SafWeb.Model.Filial.HorarioVerao pobjHorarioVerao)
        {
            DLHorarioVerao objDLHorarioVerao = new DLHorarioVerao();

            try
            {
                return objDLHorarioVerao.Alterar(pobjHorarioVerao);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLHorarioVerao.Finalizar();
            }
        }
        #endregion
        
        #region Obter

        /// <summary>
        ///      Obtem os registros de um horário de verao
        /// </summary>
        /// <param name="pintIdColaborador">Id do horário de verao</param>
        /// <returns>Objeto horário de verao</returns>
        /// <history>
        ///     [haguiar_4] created 18/01/2011
        /// </history>
        public SafWeb.Model.Filial.HorarioVerao Obter(int pintIdHorarioVerao)
        {
            DLHorarioVerao objDLHorarioVerao = new DLHorarioVerao();

            try
            {
                return objDLHorarioVerao.Obter(pintIdHorarioVerao);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLHorarioVerao.Finalizar();
            }
        }

        #endregion
        */
    }
}