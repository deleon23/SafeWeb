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
    public class BLHorarioVerao
    {
        #region Listar

        /// <summary>
        ///      Lista horários de verao
        /// </summary>
        /// <history>
        ///      [haguiar_4] created 14/01/2011
        /// </history>
        /// <returns></returns>
        public DataTable Listar()
        {
            DLHorarioVerao objDLHorarioVerao = new DLHorarioVerao();

            try
            {
                return objDLHorarioVerao.Listar();
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

    }
}