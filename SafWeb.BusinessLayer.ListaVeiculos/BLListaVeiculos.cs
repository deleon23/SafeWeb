using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using FrameWork.BusinessLayer.Utilitarios;
using SafWeb.DataLayer.ListaVeiculos;

namespace SafWeb.BusinessLayer.ListaVeiculos
{
    public class BLListaVeiculos
    {
        #region Inserir

        public int Inserir(SafWeb.Model.ListaVeiculos.ListaVeiculos pobjLista, List<SafWeb.Model.Veiculo.Veiculo> lstGrid)
        {
            DLListaVeiculos objDLLista = new DLListaVeiculos();

            try
            {
                objDLLista.BeginTransaction();

                pobjLista.IdLista = objDLLista.Inserir(pobjLista);

                if (pobjLista.IdLista > 0)
                {
                    if (objDLLista.RemoverVeiculoLista(pobjLista.IdLista, 0))
                        foreach (var item in lstGrid)
                        {
                            objDLLista.InserirVeiculoLista(pobjLista.IdLista, item.Codigo, item.IdUsuario, item.Situacao);
                        }
                }

                objDLLista.CommitTransaction();

                return pobjLista.IdLista;
            }
            catch (Exception ex)
            {
                objDLLista.RollBackTransaction();
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLLista.Finalizar();
            }
        }

        #endregion

        #region Alterar

        public int Alterar(SafWeb.Model.ListaVeiculos.ListaVeiculos pobjLista, List<SafWeb.Model.Veiculo.Veiculo> lstGrid)
        {
            DLListaVeiculos objDLLista = new DLListaVeiculos();

            try
            {
                objDLLista.BeginTransaction();

                int retorno = objDLLista.Alterar(pobjLista);

                if (retorno == 0)
                {
                    if (objDLLista.RemoverVeiculoLista(pobjLista.IdLista, 0))
                        foreach (var item in lstGrid)
                        {
                            objDLLista.InserirVeiculoLista(pobjLista.IdLista, item.Codigo, item.IdUsuario,item.Situacao);
                        }
                }

                objDLLista.CommitTransaction();

                return pobjLista.IdLista;
            }
            catch (Exception ex)
            {
                objDLLista.RollBackTransaction();
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLLista.Finalizar();
            }
        }


        public bool AlterarSituacao(SafWeb.Model.ListaVeiculos.ListaVeiculos pobjLista)
        {
            DLListaVeiculos objDLLista = new DLListaVeiculos();

            try
            {
                return objDLLista.AlterarSituacao(pobjLista);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLLista.Finalizar();
            }
        }

        #endregion

        #region Listar

        /// <summary>
        ///      Lita as Listas
        /// </summary>
        /// <history>
        ///      [cfrancisco] created 24/04/2012
        /// </history>
        /// <returns></returns>
        public Collection<SafWeb.Model.ListaVeiculos.ListaVeiculos> ListarListaVeiculos(SafWeb.Model.ListaVeiculos.ListaVeiculos pobjLista)
        {
            DLListaVeiculos objDLLista = new DLListaVeiculos();

            try
            {
                return objDLLista.ListarListaVeiculos(pobjLista);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLLista.Finalizar();
            }
        }

        #endregion

        #region Listar Veículos da Lista

        /// <summary>
        ///      Lita as Listas
        /// </summary>
        /// <history>
        ///      [cfrancisco] created 24/04/2012
        /// </history>
        /// <returns></returns>
        public Collection<SafWeb.Model.Veiculo.Veiculo> ListarVeiculosDaLista(int pintIdLista)
        {
            DLListaVeiculos objDLListaVeiculos = new DLListaVeiculos();

            try
            {
                return objDLListaVeiculos.ListarVeiculosDaLista(pintIdLista);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLListaVeiculos.Finalizar();
            }
        }

        #endregion

        #region Alterar Situacao Veículo da lista

        public int AlterarSituacaoVeiculoDaLista(SafWeb.Model.Veiculo.Veiculo pobjVeiculo, int IdLista, int IdUsuario)
        {
            DLListaVeiculos objDLLista = new DLListaVeiculos();

            try
            {
                return objDLLista.AlterarSituacaoVeiculoDaLista(pobjVeiculo, IdLista, IdUsuario);

            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLLista.Finalizar();
            }
        }
        #endregion

        #region Obter

        /// <summary>
        ///      Retorna o objeto Lista 
        /// </summary>
        /// <param name="pintIdLista">Id da lista</param>
        /// <history>
        ///      [cfrancisco] created 24/04/2012
        /// </history>
        /// <returns>Objeto Lista</returns>
        public SafWeb.Model.ListaVeiculos.ListaVeiculos Obter(int pintIdLista)
        {
            DLListaVeiculos objDLListaVeiculos = new DLListaVeiculos();

            try
            {
                return objDLListaVeiculos.Obter(pintIdLista);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLListaVeiculos.Finalizar();
            }
        }

        #endregion
    }
}
