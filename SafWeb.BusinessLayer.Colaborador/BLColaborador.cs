using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using SafWeb.DataLayer.Colaborador;
using FrameWork.BusinessLayer.Utilitarios;
using System.Data;
using SafWeb.Model.Colaborador;

namespace SafWeb.BusinessLayer.Colaborador
{
    public class BLColaborador
    {
        #region Inserir

        /// <summary>
        ///      Insere um colaborador
        /// </summary>
        /// <history>
        ///      [mribeiro] created 01/07/2009
        /// </history>
        /// <param name="pobjColaborador">Objeto Colaborador</param>
        /// <returns>Código do colaborador</returns>
        public int Inserir(SafWeb.Model.Colaborador.Colaborador pobjColaborador)
        {
            DLColaborador objDLColaborador = new DLColaborador();

            try
            {
                return objDLColaborador.Inserir(pobjColaborador);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLColaborador.Finalizar();
            }
        }

        #endregion

        #region Inserir Documento Visitante

        /// <summary>
        ///      Insere um documento do colaborador
        /// </summary>
        /// <history>
        ///      [mribeiro] created 02/07/2009
        /// </history>
        /// <param name="pobjColaborador">Objeto Colaborador</param>
        /// <returns>True/False</returns>
        public bool InserirDocumentoVisitante(SafWeb.Model.Colaborador.Colaborador pobjColaborador)
        {
            DLColaborador objDLColaborador = new DLColaborador();

            try
            {
                return objDLColaborador.InserirDocumentoVisitante(pobjColaborador);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLColaborador.Finalizar();
            }
        }

        #endregion

        #region Inserir Foto de Visitante

        /// <summary>
        ///      Insere a foto do um visitante
        /// </summary>
        /// <param name="pintIdColaborador">Id do Colaborador</param>
        /// <param name="pintIdTipoVisitante">Id do Tipo de Visitante(Funcionário, Terceiro)</param>
        /// <param name="pdtDataFoto">Data da Foto</param>
        /// <returns>True/False</returns>
        /// <history>
        ///     [aoliveira] created 05/02/2013
        /// </history>
        public bool InserirFotoVisitante(int pintIdColaborador, byte[] pFoto, DateTime pdtDataFoto)
        {
            DLColaborador objDLColaborador = new DLColaborador();
            Model.Colaborador.Colaborador objColaborador = new Model.Colaborador.Colaborador();
            bool blnRetorno = false;

            try
            {
                objColaborador = objDLColaborador.ObterFoto(pintIdColaborador, 3);
                if (objColaborador.IdColaborador != null)
                {
                    if (objColaborador.DataFoto != null)
                    {
                        TimeSpan diferenca = pdtDataFoto - objColaborador.DataFoto;
                        if (diferenca.TotalMinutes > 30)
                        {
                            blnRetorno = objDLColaborador.InserirFotoVisitante(pintIdColaborador, pFoto, pdtDataFoto);
                        }
                        else
                        {
                            // só atualiza, pois a nova foto é muito recente
                            blnRetorno = objDLColaborador.AlteraFotoVisitante(pintIdColaborador, pFoto, pdtDataFoto);
                        }

                    }
                    else
                    {
                        blnRetorno = objDLColaborador.InserirFotoVisitante(pintIdColaborador, pFoto, pdtDataFoto);
                    }
                }
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLColaborador.Finalizar();
            }
            return blnRetorno;
        }

        #endregion

        #region Listar Colaborador

        /// <summary>
        ///      Lita os colaboradores
        /// </summary>
        /// <history>
        ///      [mribeiro] created 29/06/2009
        ///      [aoliveira] modify 06/01/2013
        /// </history>
        /// <returns></returns>
        public Collection<SafWeb.Model.Colaborador.Colaborador> ListarColaborador(string pstrNome, 
                                                                                  string pstrDocumento,
                                                                                  int pintTipoVisitante,
                                                                                  int pintIgnorarEmFérias,
                                                                                  int startIndex, int pageSize, string sortBy, ref int totalRegistros)
        {
            DLColaborador objDLColaborador = new DLColaborador();

            try
            {
                return objDLColaborador.ListarColaborador(pstrNome, pstrDocumento, pintTipoVisitante, pintIgnorarEmFérias, startIndex, pageSize, sortBy, ref totalRegistros);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLColaborador.Finalizar();
            }
        }

        #endregion

        #region Listar Colaborador com mais de um Tipo Visitante

        /// <summary>
        ///      Lista os colaboradores do tipo Funcionário e Terceiro
        /// </summary>
        /// <param name="pstrNome">Nome do Colaborador</param>
        /// <param name="pstrDocumento">Documento</param>
        /// <param name="pstrTipoVisitante">Números dos Tipos Visitantes</param>
        /// <param name="pintIdFilial">Id da Filial</param>
        /// <history>
        ///     [cmarchi] created 6/01/2010
        ///     [cmarchi] modify 11/2/2010  
        /// </history>
        /// <returns>Lista de Colaboradores</returns>
        public Collection<SafWeb.Model.Colaborador.Colaborador> ListarColaboradorFuncionarioTerceiro(string pstrNome,
                                                                                  string pstrDocumento, int pintIdFilial)
        {
            DLColaborador objDLColaborador = new DLColaborador();

            try
            {
                return objDLColaborador.ListarColaboradorFuncionarioTerceiro(pstrNome, pstrDocumento,
                        pintIdFilial);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLColaborador.Finalizar();
            }
        }

       
        /// <summary>
        /// Lista os colaboradores do tipo Funcionário e Terceiro buscando somente os registros que aparecerão na página
        /// </summary>
        /// <param name="pstrNome">Nome do Colaborador</param>
        /// <param name="pstrDocumento">Documento</param>
        /// <param name="pintIdFilial">Id da Filial</param>
        /// <param name="startIndex">página a ser exibida</param>
        /// <param name="pageSize">tamanho da página</param>
        /// <param name="sortBy">ordenado por</param>
        /// <param name="totalRegistros">total de registros no banco</param>
        /// <history>
        ///     [aoliveira] created 06/01/2013
        /// </history>
        /// <returns>Lista de Colaboradores</returns>
        public Collection<SafWeb.Model.Colaborador.Colaborador> ListarColaboradorFuncionarioTerceiro(string pstrNome,
                                                                                  string pstrDocumento, int pintIdFilial,
                                                                                  int startIndex, int pageSize, string sortBy, ref int totalRegistros)
        {
            DLColaborador objDLColaborador = new DLColaborador();

            try
            {
                return objDLColaborador.ListarColaboradorFuncionarioTerceiro(pstrNome, pstrDocumento,
                        pintIdFilial, startIndex, pageSize, sortBy, ref totalRegistros);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLColaborador.Finalizar();
            }
        }

        #endregion

        #region Listar Colaborador Visitado

        /// <summary>
        ///      Lita os colaboradores que podem ser visitados
        /// </summary>
        /// <param name="pstrNome">Nome do Visitado</param>
        /// <param name="pstrDocumento">Documento do Visitado</param>
        /// <param name="startIndex">página a ser exibida</param>
        /// <param name="pageSize">tamanho da página</param>
        /// <param name="sortBy">ordenado por</param>
        /// <param name="totalRegistros">total de registros no banco</param>
        /// <history>
        ///      [mribeiro] created 06/07/2009
        ///      [aoliveira] modify 06/01/2013
        /// </history>
        /// <returns></returns>
        public Collection<SafWeb.Model.Colaborador.Colaborador> ListarColaboradorVisitado(string pstrNome,
                                                                                          string pstrDocumento,
                                                                                          int startIndex, int pageSize, string sortBy, ref int totalRegistros)
        {
            DLColaborador objDLColaborador = new DLColaborador();

            try
            {
                return objDLColaborador.ListarColaboradorVisitado(pstrNome, pstrDocumento, startIndex, pageSize, sortBy, ref totalRegistros);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLColaborador.Finalizar();
            }
        }

        #endregion

        #region Listar Tipo Colaborador

        /// <summary>
        ///      Lita os tipos de colaboradores
        /// </summary>
        /// <history>
        ///      [mribeiro] created 30/06/2009
        /// </history>
        /// <returns></returns>
        public Collection<SafWeb.Model.Colaborador.TipoColaborador> ListarTipoColaborador()
        {
            DLColaborador objDLColaborador = new DLColaborador();

            try
            {
                return objDLColaborador.ListarTipoColaborador();
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLColaborador.Finalizar();
            }
        }

        #endregion

        #region Listar Tipo Documento

        /// <summary>
        ///      Lita os tipos de documentos
        /// </summary>
        /// <history>
        ///      [mribeiro] created 30/06/2009
        /// </history>
        /// <returns></returns>
        public Collection<SafWeb.Model.Colaborador.TipoDocumento> ListarTipoDocumento()
        {
            DLColaborador objDLColaborador = new DLColaborador();

            try
            {
                return objDLColaborador.ListarTipoDocumento();
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLColaborador.Finalizar();
            }
        }

        #endregion

        #region Obter

        /// <summary>
        ///      Obtem os registros de um colaborador
        /// </summary>
        /// <param name="pintIdColaborador">Id do Colaborador</param>
        /// <returns>Objeto Colaborador</returns>
        /// <history>
        ///     [mribeiro] created 03/07/2009
        /// </history>
        public SafWeb.Model.Colaborador.Colaborador Obter(int pintIdColaborador)
        {
            DLColaborador objDLColaborador = new DLColaborador();

            try
            {
                return objDLColaborador.Obter(pintIdColaborador);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLColaborador.Finalizar();
            }
        }

        /// <summary>
        ///      Obtem os registros de um colaborador
        /// </summary>
        /// <param name="pintIdColaborador">Id do Colaborador</param>
        /// <param name="pintIdTipoVisitante">Id do Tipo de Visitante(Funcionário, Terceiro)</param>
        /// <returns>Objeto Colaborador</returns>
        /// <history>
        ///     [aoliveira] created 03/02/2013
        /// </history>
        public SafWeb.Model.Colaborador.Colaborador ObterFoto(int pintIdColaborador, int pintIdTipoVisitante)
        {
            DLColaborador objDLColaborador = new DLColaborador();

            try
            {
                return objDLColaborador.ObterFoto(pintIdColaborador, pintIdTipoVisitante);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLColaborador.Finalizar();
            }
        }

        #endregion

        #region Obter

        /// <summary>
        ///      Obtem os registros de um colaborador
        /// </summary>
        /// <param name="parrIdColaborador">array contendo os Ids dos Colaboradores</param>
        /// <returns>Lista de Objetos Colaboradores</returns>
        /// <history>
        ///     [cmarchi] created 7/01/2010
        /// </history>
        public Collection<SafWeb.Model.Colaborador.Colaborador> Obter(string []parrIdColaboradores)
        {
            DLColaborador objDLColaborador = new DLColaborador();
            Collection<SafWeb.Model.Colaborador.Colaborador> colColaboradores = new Collection<SafWeb.Model.Colaborador.Colaborador>();
            SafWeb.Model.Colaborador.Colaborador objColaborador = null;

            try
            {
                foreach (string strId in parrIdColaboradores)
                {
                    objColaborador = objDLColaborador.Obter(Convert.ToInt32(strId));

                    if(objColaborador != null)
                    {
                        colColaboradores.Add(objColaborador);
                        objColaborador = null;
                    }
                }

                return colColaboradores;
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLColaborador.Finalizar();
            }
        }

        #endregion

        #region Obter Regional e Filial Usuário

        /// <summary>
        ///      Obtem a regional e a filial do usuário logado
        /// </summary>
        /// <param name="pintIdUsuario">Id do Usuário Logado</param>
        /// <returns>DataTable</returns>
        /// <history>
        ///     [mribeiro] created 02/09/2009
        /// </history>
        public DataTable ObterRegFilUsuario(int pintIdUsuario)
        {
            DLColaborador objDLColaborador = new DLColaborador();

            try
            {
                return objDLColaborador.ObterRegFilUsuario(pintIdUsuario);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLColaborador.Finalizar();
            }
        }

        #endregion

        #region Obter Login Usuário Portal

        /// <summary>
        ///      Obtem o login do usuário logado no portal
        /// </summary>
        /// <param name="pstrLogin">Login usuário do portal</param>
        /// <returns>DataTable</returns>
        /// <history>
        ///     [mribeiro] created 14/09/2009
        /// </history>
        public DataTable ObterLoginUsuario(string pstrLogin)
        {
            DLColaborador objDLColaborador = new DLColaborador();

            try
            {
                return objDLColaborador.ObterLoginUsuario(pstrLogin);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLColaborador.Finalizar();
            }
        }

        #endregion


        #region Obter Login Usuário por Filial
        public IList<FWCUsuario> BuscaPorFilialColoborador(int intIdFilial)
        {
            DLColaborador objDLColaborador = new DLColaborador();
            {
                try
                {
                    var objRetorno = objDLColaborador.BuscaPorFilialColoborador(intIdFilial,0);
                    return objRetorno;
                }
                catch (Exception ex)
                {
                    BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                    throw ex;
                }
            }
        }
        #endregion

        #region Obter Login Usuário por id Colaborador
        public IList<FWCUsuario> BuscaPorIdColoborador(int intIdColaborador)
        {
            DLColaborador objDLColaborador = new DLColaborador();
            {
                try
                {
                    var objRetorno = objDLColaborador.BuscaPorFilialColoborador(0, intIdColaborador);
                    return objRetorno;
                }
                catch (Exception ex)
                {
                    BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                    throw ex;
                }
            }
        }
        #endregion

        #region Consultar se colaborador está em férias

        /// <summary>
        /// Verifica se um colaborador está/estará em férias em algum momento do período solicitado
        /// </summary>
        /// <param name="CodColaborador">Codigo do colaborador a ser pesquisado</param>
        /// <param name="datInicio">inicio do período a ser pesquisado</param>
        /// <param name="datFim">fim do periodo a ser pesquisado</param>
        /// <returns>verdadeiro ou falso</returns>
        /// <history>
        ///     [aoliveira] created 28/02/2013 10:00
        ///     Verifica se um colaborador está/estará em férias em algum momento do período solicitado
        /// </history>
        public bool ColaboradorEmFeriasLicenca(int CodColaborador, DateTime datInicio, DateTime datFim)
        {
            DLColaborador objDLColaborador = new DLColaborador();

            try
            {
                return objDLColaborador.ColaboradorEmFeriasLicenca(CodColaborador, datInicio, datFim);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLColaborador.Finalizar();
            }
        }

        #endregion
    }
}
