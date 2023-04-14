using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using SafWeb.DataLayer.Lista;
using SafWeb.DataLayer.Colaborador;
using FrameWork.BusinessLayer.Utilitarios;

namespace SafWeb.BusinessLayer.Lista
{
    public class BLLista
    {
        #region Inserir

        /// <summary>
        ///      Insere uma lista e os colaboradores dessa lista
        /// </summary>
        /// <history>
        ///      [mribeiro] created 01/07/2009
        /// </history>
        /// <returns></returns>
        public int Inserir(SafWeb.Model.Lista.Lista pobjLista,
                            List<SafWeb.Model.Colaborador.Colaborador> plstColaborador)                  
        {
            DLLista objDLLista = new DLLista();

            try
            {
                objDLLista.BeginTransaction();

                //insere a lista
                pobjLista.IdLista = objDLLista.Inserir(pobjLista);

                //Se o nome da lista ainda não existe
                if (pobjLista.IdLista != -1)
                {

                    for (int i = 0; i < plstColaborador.Count; i++)
                    {
                        //insere os colaboradores da lista
                        objDLLista.InserirColaboradorLista(pobjLista.IdLista, plstColaborador[i].IdColaborador, pobjLista.IdUsuario, plstColaborador[i].Situacao);
                    }

                    objDLLista.CommitTransaction();
                }

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

        /// <summary>
        ///     Altera os registros da Lista
        /// </summary>
        /// <param name="pobjLista">Objeto Lista</param>
        /// <param name="plstColaborador">Colaboradores da Lista</param>
        /// <returns></returns>
        /// <history>
        ///     [mribeiro] created 02/07/2009
        /// </history>
        public int Alterar(SafWeb.Model.Lista.Lista pobjLista,
                            List<SafWeb.Model.Colaborador.Colaborador> plstColaborador)
        {
            DLLista objDLLista = new DLLista();
            int intIdLista;

            try
            {
                objDLLista.BeginTransaction();

                //Exclui todos os colaboradores da lista
                if (objDLLista.ExcluirColaboradoresLista(pobjLista.IdLista))
                {

                    for (int i = 0; i < plstColaborador.Count; i++)
                    {
                        //insere os colaboradores da lista
                        objDLLista.InserirColaboradorLista(pobjLista.IdLista, plstColaborador[i].IdColaborador, pobjLista.IdUsuario, plstColaborador[i].Situacao);
                    }
                }
                else
                {
                    objDLLista.RollBackTransaction();
                }

                intIdLista = objDLLista.Alterar(pobjLista);

                if (intIdLista != -1)
                {
                    objDLLista.CommitTransaction();
                }
                else
                {
                    objDLLista.RollBackTransaction();
                }

                return intIdLista;                
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

        #region Alterar Situação

        /// <summary>
        ///     Altera a situação da Lista
        /// </summary>
        /// <param name="pobjLista">Objeto Lista</param>
        /// <returns></returns>
        /// <history>
        ///     [mribeiro] created 14/07/2009
        /// </history>
        public int Alterar(SafWeb.Model.Lista.Lista pobjLista)
        {
            DLLista objDLLista = new DLLista();

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

        #region Alterar Situação

        /// <summary>
        ///     Altera a situação da Lista (Ativo/Inativo)
        /// </summary>
        /// <param name="pobjLista">Objeto Lista</param>
        /// <returns>True/False</returns>
        /// <history>
        ///     [mribeiro] created 02/07/2009
        /// </history>
        public void AlterarSituacao(SafWeb.Model.Lista.Lista pobjLista)
        {
            DLLista objDLLista = new DLLista();

            try
            {
                objDLLista.AlterarSituacao(pobjLista);

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

        #region Alterar Situacao Colaborador da lista

        public int AlterarSituacaoColaboradorDaLista(SafWeb.Model.Colaborador.Colaborador pobjColaborador, int IdLista, int IdUsuario)
        {
            DLLista objDLLista = new DLLista();

            try
            {
                return objDLLista.AlterarSituacaoColaboradorDaLista(pobjColaborador, IdLista, IdUsuario);

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
        ///      [mribeiro] created 30/06/2009
        /// </history>
        /// <returns></returns>
        public Collection<SafWeb.Model.Lista.Lista> ListarLista(SafWeb.Model.Lista.Lista pobjLista)
        {
            DLLista objDLLista = new DLLista();

            try
            {
                return objDLLista.ListarLista(pobjLista);
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

        #region Listar Listas Ativas

        /// <summary>
        ///      Lita as Listas Ativas
        /// </summary>
        /// <param name="pintRegional">Id da Regional</param>
        /// <param name="pintFilial">Id da Filial</param>
        /// <history>
        ///      [mribeiro] created 14/07/2009
        /// </history>
        /// <returns>Objeto Lista</returns>
        public Collection<SafWeb.Model.Lista.Lista> ListarListasAtivas(int pintRegional,
                                                                       int pintFilial)
        {
            DLLista objDLLista = new DLLista();

            try
            {
                return objDLLista.ListarListasAtivas(pintRegional,
                                                     pintFilial);
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

        #region Listar Colaborador da Lista

        /// <summary>
        ///      Lita as Listas
        /// </summary>
        /// <history>
        ///      [mribeiro] created 30/06/2009
        /// </history>
        /// <returns></returns>
        public Collection<SafWeb.Model.Colaborador.Colaborador> ListarColaboradorDaLista(int pintIdLista)
        {
            DLLista objDLLista = new DLLista();

            try
            {
                return objDLLista.ListarColaboradoresDaLista(pintIdLista);
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
        ///      [mribeiro] created 01/07/2009
        /// </history>
        /// <returns>Objeto Lista</returns>
        public SafWeb.Model.Lista.Lista Obter(int pintIdLista)
        {
            DLLista objDLLista = new DLLista();

            try
            {
                return objDLLista.Obter(pintIdLista);
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

        #region Verificar Colaborador Lista

        /// <summary>
        ///      Verifica se um colaborador está contido em alguma das listas
        /// </summary>
        /// <param name="pstrIdLista">String com as listas inseridas</param>
        /// <param name="pintIdColaborador">Id do Colaborador</param>
        /// <history>
        ///      [mribeiro] created 30/07/2009
        /// </history>
        /// <returns>True/False</returns>
        public bool VerificarColaboradorLista(string pstrIdLista,
                                              int pintIdColaborador)
        {
            DLLista objDLLista = new DLLista();

            try
            {
                return objDLLista.VerificarColaboradorLista(pstrIdLista,
                                                            pintIdColaborador);
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
    }
}
