using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections.ObjectModel;
using FrameWork.BusinessLayer.Utilitarios;
using SafWeb.DataLayer.Acesso;

namespace SafWeb.BusinessLayer.Acesso
{
    public class BLAcesso
    {
        #region Listar Visitas Agendadas

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        ///     Lista registros da base 
        /// </summary> 
        /// <param name="pintCodRegional">Código da Regional</param>
        /// <param name="pintCodFilial">Código da Filial</param>
        /// <param name="pintCodTipoDoc">Código do Tipo de Documento</param>
        /// <param name="pstrDocumento">Documento</param>
        /// <param name="pstrVisitante">Nome do Visitante</param>
        /// <param name="pstrAprovador">Nome do Aprovador</param>
        /// <param name="pdatInicioVis">Data início da visita</param>
        /// <param name="pdatFimVis">Data fim da visita</param>
        /// <returns>Datatable</returns> 
        /// <history> 
        ///     [vsantos] 16/07/2009 Created 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public DataTable ListarVisitasAgendadas(int pintCodRegional,
                                                int pintCodFilial,
                                                int pintCodTipoDoc,
                                                string pstrDocumento,
                                                string pstrVisitante,
                                                string pstrAprovador,
                                                DateTime? pdatInicioVis,
                                                DateTime? pdatFimVis)
        {
            DLAcesso objDLAcesso = new DLAcesso();

            try
            {
                return objDLAcesso.ListarVisitasAgendadas(pintCodRegional,
                                                          pintCodFilial,
                                                          pintCodTipoDoc,
                                                          pstrDocumento,
                                                          pstrVisitante,
                                                          pstrAprovador,
                                                          pdatInicioVis,
                                                          pdatFimVis);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
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
        /// <returns>Coleção de objetos do tipo Acesso</returns> 
        /// <history> 
        ///     [vsantos] 17/07/2009 Created 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public Collection<Model.Acesso.Acesso> Listar(Model.Acesso.Acesso pobjAcesso)
        {
            DLAcesso objDLAcesso = new DLAcesso();

            try
            {
                return objDLAcesso.Listar(pobjAcesso);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
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
        /// <returns>Código do registro inserido</returns> 
        /// <history> 
        ///     [vsantos] 17/07/2009 Created 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public int Inserir(Model.Acesso.Acesso pobjAcesso)
        {
            DLAcesso objDLAcesso = new DLAcesso();

            try
            {
                return objDLAcesso.Inserir(pobjAcesso);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
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
        /// <returns>Boleano indicando sucesso da atualização</returns> 
        /// <history> 
        ///     [vsantos] 17/07/2009 Created 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public bool Alterar(Model.Acesso.Acesso pobjAcesso)
        {
            DLAcesso objDLAcesso = new DLAcesso();

            try
            {
                return objDLAcesso.Alterar(pobjAcesso);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLAcesso.Finalizar();
            }
        }

        #endregion
    }
}
