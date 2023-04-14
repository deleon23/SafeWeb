using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using SafWeb.DataLayer.Email;
using FrameWork.BusinessLayer.Utilitarios;

namespace SafWeb.BusinessLayer.Email
{
    public class BLEmail
    {
        #region Listar Horarios

        /// <summary>
        ///     Lista os horarios de envio de e-mail 
        /// </summary>
        /// <returns>DataTable com os horários</returns>
        /// <history>
        ///     [mribeiro] 25/10/2009 Created 
        /// </history>
        public DataTable ListarHorarios()
        {
            DLEmail objDLEmail = null;

            try
            {
                objDLEmail = new DLEmail();

                return objDLEmail.ListarHorarios();
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLEmail.Finalizar();
            }
        }

        #endregion

        #region Listar Horarios

        /// <summary>
        ///     Obtem os horarios de envio de e-mail  do colaborador
        /// </summary>
        /// <param name="pintIdUsuario">Id do usuario</param>
        /// <returns>DataTable com os horários</returns>
        /// <history>
        ///     [mribeiro] 25/10/2009 Created 
        /// </history>
        public DataTable ObterHorarios(int pintIdUsuario)
        {
            DLEmail objDLEmail = null;

            try
            {
                objDLEmail = new DLEmail();

                return objDLEmail.ObterHorarios(pintIdUsuario);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLEmail.Finalizar();
            }
        }

        #endregion

        #region Inserir

        /// <summary>
        ///     Inserir os horarios de envio de e-mail 
        /// </summary>
        /// <param name="pintIdUsuario">Id do usuario</param>
        /// <param name="pintIdAlerta">Id do alerta</param>
        /// <history>
        ///     [mribeiro] 25/10/2009 Created 
        /// </history>
        public void Inserir(int pintIdUsuario,
                            int pintIdAlerta)
        {
            DLEmail objDLEmail = null;

            try
            {
                objDLEmail = new DLEmail();

                objDLEmail.Inserir(pintIdUsuario,
                                pintIdAlerta);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLEmail.Finalizar();
            }
        }

        #endregion

        #region Excluir

        /// <summary>
        ///     Exclui os horarios de envio de e-mail de um usuario
        /// </summary
        /// <param name="pintIdUsuario">Id do colaborador(usuario)</param>
        /// <history>
        ///     [mribeiro] 25/10/2009 Created 
        /// </history>
        public void Excluir(int pintIdUsuario)
        {
            DLEmail objDLEmail = null;

            try
            {
                objDLEmail = new DLEmail();

                objDLEmail.Excluir(pintIdUsuario);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLEmail.Finalizar();
            }
        }

        #endregion

        public DataTable ObterAlertaRH(int pintId_RegraEmailAlertaRH)
        {
            DLEmail dlEmail = (DLEmail)null;
            try
            {
                dlEmail = new DLEmail();
                return dlEmail.ObterAlertaRH(pintId_RegraEmailAlertaRH);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ((object)ex.TargetSite.ReflectedType.Name).ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, new Decimal(0));
                throw;
            }
            finally
            {
                ((DALFWBase)dlEmail).Finalizar();
            }
        }
        public int InserirEmailAlertaRH(string pstrNom_Colaborador, string pstrDes_Email)
        {
            DLEmail dlEmail = (DLEmail)null;
            try
            {
                dlEmail = new DLEmail();
                return dlEmail.InserirEmailAlertaRH(pstrNom_Colaborador, pstrDes_Email);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ((object)ex.TargetSite.ReflectedType.Name).ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, new Decimal(0));
                throw;
            }
            finally
            {
                ((DALFWBase)dlEmail).Finalizar();
            }
        }
        public int AlterarEmailAlertaRH(int pintId_RegraEmailAlertaRH, string pstrNom_Colaborador, string pstrDes_Email)
        {
            DLEmail dlEmail = (DLEmail)null;
            try
            {
                dlEmail = new DLEmail();
                return dlEmail.AlterarEmailAlertaRH(pintId_RegraEmailAlertaRH, pstrNom_Colaborador, pstrDes_Email);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ((object)ex.TargetSite.ReflectedType.Name).ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, new Decimal(0));
                throw;
            }
            finally
            {
                ((DALFWBase)dlEmail).Finalizar();
            }
        }
        public void ExcluirEmailAlertaRH(int pintId_RegraEmailAlertaRH)
        {
            DLEmail dlEmail = (DLEmail)null;
            try
            {
                dlEmail = new DLEmail();
                dlEmail.ExcluirEmailAlertaRH(pintId_RegraEmailAlertaRH);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ((object)ex.TargetSite.ReflectedType.Name).ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, new Decimal(0));
                throw;
            }
            finally
            {
                ((DALFWBase)dlEmail).Finalizar();
            }
        }

        public DataTable ListarAlertaRH()
        {
            DLEmail dlEmail = (DLEmail)null;
            try
            {
                dlEmail = new DLEmail();
                return dlEmail.ListarAlertaRH();
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ((object)ex.TargetSite.ReflectedType.Name).ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, new Decimal(0));
                throw;
            }
            finally
            {
                ((DALFWBase)dlEmail).Finalizar();
            }
        }
    }
}
