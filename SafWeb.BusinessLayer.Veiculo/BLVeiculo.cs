using System;
using System.Collections.Generic;
using System.Text;
using SafWeb.DataLayer.Veiculo;
using System.Collections.ObjectModel;
using FrameWork.BusinessLayer.Utilitarios;

namespace SafWeb.BusinessLayer.Veiculo
{
    public class BLVeiculo
    {
        #region Inserir

        /// <summary>
        ///      Insere um Veiculo
        /// </summary>
        /// <history>
        ///      [mribeiro] created 10/07/2009
        /// </history>
        /// <returns></returns>
        public int Inserir(SafWeb.Model.Veiculo.Veiculo pobjVeiculo)
        {
            DLVeiculo objDLVeiculo = new DLVeiculo();

            try
            {
                return objDLVeiculo.Inserir(pobjVeiculo);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLVeiculo.Finalizar();
            }
        }

        #endregion

        #region Listar Veículo

        /// <summary>
        ///      Lita os Veículos 
        /// </summary>
        /// <param name="pintEstado">Código do Estado</param>
        /// <history>
        ///      [mribeiro] created 10/08/2009
        /// </history>
        /// <returns>Objeto Veiculo</returns>
        public Collection<SafWeb.Model.Veiculo.Veiculo> ListarVeiculo(int pintEstado)
        {
            DLVeiculo objDLVeiculo = new DLVeiculo();

            try
            {
                return objDLVeiculo.ListarVeiculo(pintEstado);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLVeiculo.Finalizar();
            }
        }

        #endregion

        #region Listar Estado

        /// <summary>
        ///      Lita os Estados 
        /// </summary>
        /// <history>
        ///      [mribeiro] created 24/06/2009
        /// </history>
        /// <returns></returns>
        public Collection<SafWeb.Model.Veiculo.Estado> ListarEstado()
        {
            DLVeiculo objDLVeiculo = new DLVeiculo();

            try
            {
                return objDLVeiculo.ListarEstado();
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLVeiculo.Finalizar();
            }
        }

        #endregion

        #region Obter

        /// <summary> 
        ///     Função para popular a model do objeto
        /// </summary> 
        /// <param name="pintIdLista">Id da lista</param> 
        /// <returns> Model Lista </returns> 
        /// <history> 
        ///      [cfrancisco] created 24/04/2012
        /// </history> 
        public SafWeb.Model.Veiculo.Veiculo Obter(SafWeb.Model.Veiculo.Veiculo pobjVeiculo)
        {
            DLVeiculo objDLVeiculos = new DLVeiculo();

            try
            {
                return objDLVeiculos.Obter(pobjVeiculo);
    }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
}
            finally
            {
                objDLVeiculos.Finalizar();
            }
        }

        #endregion
    }
}
