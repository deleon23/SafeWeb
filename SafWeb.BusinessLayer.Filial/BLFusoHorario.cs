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
    public class BLFusoHorario
    {
        /// <summary>
        ///      Lista fuso horários
        /// </summary>
        /// <history>
        ///      [haguiar_4] created 13/01/2011
        /// </history>
        /// <returns>Retorna collection de fuso horário</returns>
        public Collection<FusoHorario> ListarFusoHorario()
        {
            DLFusoHorario objDLFusoHorario = new DLFusoHorario();

            try
            {
                return objDLFusoHorario.ListarFusoHorario();
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLFusoHorario.Finalizar();
            }
        }
    }
}