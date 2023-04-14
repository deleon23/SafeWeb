using System;
using System.Collections.Generic;
using System.Text;
using SafWeb.DataLayer.Filial;
using FrameWork.BusinessLayer.Utilitarios;

namespace SafWeb.BusinessLayer.Filial
{
    public class BLFeriado
    {
        #region Listar

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// Lista registros da base em uma List - Feriados da filial no periodo
        /// </summary> 
        /// <returns>Lista Feriados</returns> 
        /// <history> 
        ///     [cfrancisco] 22/5/2012 Created 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public List<SafWeb.Model.Filial.Feriado> Listar(SafWeb.Model.Filial.Filial objFilial, DateTime datInicio, DateTime datFinal)
        {
            try
            {
                return this.Listar(objFilial.IdCidade, 0, datInicio, datFinal);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<DateTime> ObterDatas(SafWeb.Model.Filial.Filial objFilial, DateTime datInicio, DateTime datFinal)
        {
            try
            {
                return this.ObterDatas(objFilial.IdCidade, 0, datInicio, datFinal);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// Lista registros da base em uma List - Feriados da filial no periodo
        /// </summary> 
        /// <returns>Lista Feriados</returns> 
        /// <history> 
        ///     [cfrancisco] 22/5/2012 Created 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public List<SafWeb.Model.Filial.Feriado> Listar(Nullable<int> idCidade, Nullable<int> idFilial, DateTime datInicio, DateTime datFinal)
        {
            var objDLFeriado = new DLFeriado();
            try
            {
                return objDLFeriado.Listar(idCidade.GetValueOrDefault(0), idFilial.GetValueOrDefault(0), datInicio, datFinal);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLFeriado.Finalizar();
            }
        }

        public List<DateTime> ObterDatas(Nullable<int> idCidade, Nullable<int> idFilial, DateTime datInicio, DateTime datFinal)
        {
            var objDLFeriado = new DLFeriado();
            try
            {
                var datas = new List<DateTime>();

                foreach (var item in objDLFeriado.Listar(idCidade.GetValueOrDefault(0), idFilial.GetValueOrDefault(0), datInicio, datFinal))
                {
                    datas.Add(item.data);
                }

                return datas;
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLFeriado.Finalizar();
            }
        }

        #endregion
    }
}
