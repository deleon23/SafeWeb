using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections.ObjectModel;
using FrameWork.BusinessLayer.Utilitarios;
using SafWeb.DataLayer.Acesso;

namespace SafWeb.BusinessLayer.Acesso
{
    public class BLAcessoVisitante
    {
        #region Listar Visitas Agendadas

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        ///     Lista registros da base 
        /// </summary> 
        /// <param name="pintCodRegional">C�digo da Regional</param>
        /// <param name="pintCodFilial">C�digo da Filial</param>
        /// <param name="pintCodTipoDoc">C�digo do Tipo de Documento</param>
        /// <param name="pstrDocumento">Documento</param>
        /// <param name="pstrVisitante">Nome do Visitante</param>
        /// <param name="pstrAprovador">Nome do Aprovador</param>
        /// <param name="pdatInicioVis">Data in�cio da visita</param>
        /// <param name="pdatFimVis">Data fim da visita</param>
        /// <param name="pintIdSolicitacao">Id da Solicitacao</param>
        /// <param name="pintIdUsuario">Id do Usu�rio</param>
        /// <returns>Datatable</returns> 
        /// <history> 
        ///     [vsantos] 16/07/2009 Created 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public DataTable ListarVisitasAgendadas(int pintCodTipoDoc,
                                                string pstrDocumento,
                                                string pstrVisitante,
                                                string pstrAprovador,
                                                int pintIdUsuario,
                                                int pintIdSolicitacao,
                                                int pintIdEmpresa)
        {
            DLAcessoVisitante objDLAcesso = new DLAcessoVisitante();

            try
            {
                return objDLAcesso.ListarVisitasAgendadas(pintCodTipoDoc,
                                                          pstrDocumento,
                                                          pstrVisitante,
                                                          pstrAprovador,
                                                          pintIdUsuario,
                                                          pintIdSolicitacao,
                                                          pintIdEmpresa);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " M�todo: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLAcesso.Finalizar();
            }
        }

        #endregion

        #region Listar

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        ///     Lista registros da base 
        /// </summary> 
        /// <param name="pobjAcesso">Model Acesso</param>
        /// <returns>Cole��o de objetos do tipo Acesso</returns> 
        /// <history> 
        ///     [vsantos] 17/07/2009 Created 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public Collection<Model.Acesso.AcessoVisitante> Listar(Model.Acesso.AcessoVisitante pobjAcesso, bool pblnBuscaCracha)
        {
            DLAcessoVisitante objDLAcesso = new DLAcessoVisitante();

            try
            {
                return objDLAcesso.Listar(pobjAcesso, pblnBuscaCracha);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " M�todo: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLAcesso.Finalizar();
            }
        }

        #endregion

        #region Verificar Entrada

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        ///     Verifica se a solicita��o possui alguma entrada sem sa�da
        /// </summary> 
        /// <param name="pintIdVisitante">Id do Visitante</param>
        /// <returns>True/False</returns> 
        /// <history> 
        ///     [mribeiro] 29/07/2009 Created 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public bool VerificarEntrada(int pintIdVisitante)
        {
            DLAcessoVisitante objDLAcesso = new DLAcessoVisitante();

            try
            {
                return objDLAcesso.VerificarEntrada(pintIdVisitante);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " M�todo: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLAcesso.Finalizar();
            }
        }

        #endregion

        #region Inserir

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        ///     Inserir registros da base 
        /// </summary> 
        /// <param name="pobjAcesso">Model de Acesso</param>
        /// <returns>C�digo do registro inserido</returns> 
        /// <history> 
        ///     [vsantos] 17/07/2009 Created 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public int Inserir(Model.Acesso.AcessoVisitante pobjAcesso)
        {
            DLAcessoVisitante objDLAcesso = new DLAcessoVisitante();

            try
            {
                return objDLAcesso.Inserir(pobjAcesso);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " M�todo: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLAcesso.Finalizar();
            }
        }

        #endregion

        #region Alterar

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        ///     Edita registros da base 
        /// </summary> 
        /// <param name="pobjAcesso">Model Acesso</param>
        /// <returns>Boleano indicando sucesso da atualiza��o</returns> 
        /// <history> 
        ///     [vsantos] 17/07/2009 Created 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public bool Alterar(Model.Acesso.AcessoVisitante pobjAcesso)
        {
            DLAcessoVisitante objDLAcesso = new DLAcessoVisitante();

            try
            {
                return objDLAcesso.Alterar(pobjAcesso);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " M�todo: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLAcesso.Finalizar();
            }
        }

        #endregion

        #region Id Acesso Visitante

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        ///     Obtem o Id do Acesso do Visitante
        /// </summary> 
        /// <param name="pstrCracha">N�mero do Crach�</param>
        /// <param name="pintUsuario">Id do Usu�rio Logado</param>
        /// <returns>Id do Acesso do Visitante</returns> 
        /// <history> 
        ///     [mribeiro] 19/08/2009 Created 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public DataTable IdAcessoVisitante(string pstrNumero,
                                           int pintUsuario,
                                           bool pblnCracha)
        {
            DLAcessoVisitante objDLAcesso = new DLAcessoVisitante();

            try
            {
                return objDLAcesso.IdAcessoVisitante(pstrNumero,
                                                     pintUsuario,
                                                     pblnCracha);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " M�todo: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLAcesso.Finalizar();
            }
        }

        #endregion

        #region Obter um visitante
        /// <summary>
        ///     Obt�m um visitante
        /// </summary>
        /// <param name="pintCodigoColaborador">Id Colaborador</param>
        /// <returns>DataTable</returns>
        /// <history>
        ///     [tgerevini] created 18/05/2010
        /// </history>
        public DataTable ObterVisitante(int pintIdColaborador)
        {
            DLAcessoVisitante objDLAcessoVisitante = new DLAcessoVisitante();

            try
            {
                return objDLAcessoVisitante.ObterVisitante(pintIdColaborador);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " M�todo: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLAcessoVisitante.Finalizar();
            }
        }
        #endregion
    }
}
