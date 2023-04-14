using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using SafWeb.Model.Escala;
using SafWeb.DataLayer.Escala;
using FrameWork.BusinessLayer.Utilitarios;
using System.Data;
using SafWeb.Model.Colaborador;

namespace SafWeb.BusinessLayer.Escala
{
    /// ----------------------------------------------------------------------------- 
    /// Project : 
    /// Class : BLEscalaDepartamental
    /// ----------------------------------------------------------------------------- 
    /// <summary>  
    /// Implementa��o da classe BLEscalaDepartamental
    /// </summary> 
    /// <history> 
    ///     [cmarchi] created 30/12/2009 
    /// </history> 
    /// ----------------------------------------------------------------------------- 
    public class BLEscalaDepartamental
    {        
        #region AlterarEscalaDepartamental
        /// <summary>
        /// Altera uma Escala Departamental
        /// </summary>
        /// <param name="gobjEscalaDepartamental">Objeto Escala Departamental</param>
        /// <param name="gcolHorarios">Lista de Hor�rios da Escala Departamental</param>
        /// <param name="pcolColaboradores">Lista de Colaboradores da Escala Departamental</param>
        /// <returns>Valor do Id da Escala Depto ou Erro</returns>
        /// <history>
        ///     [cmarchi] created 5/1/2010
        ///     [haguiar_5] modify 14/02/2011 09:39
        /// </history>
        public int Alterar(EscalaDepartamental pobjEscalaDepartamental, Collection<HorarioEscala> gcolHorarios, Collection<Colaborador> pcolColaboradores)
        {
            int intTamanho = gcolHorarios.Count;
            StringBuilder strHorarios = new StringBuilder();
            StringBuilder strColaboradores = new StringBuilder(); 

            DLEscalaDepartamental objDLEscalaDepartamental = new DLEscalaDepartamental();

            if (intTamanho > 0)
            {
                //formatando os hor�rios da escala
                for (int i = 0; i < intTamanho - 1; i++)
                {
                    strHorarios.Append(gcolHorarios[i].IdHorario + ",");
                }

                //insere o �ltimo hor�rio da escala
                strHorarios.Append(gcolHorarios[gcolHorarios.Count - 1].IdHorario);
            }

            if (pcolColaboradores != null)
            {
                //int 
                intTamanho = pcolColaboradores.Count;

                //StringBuilder strColaboradores = new StringBuilder();

                if (intTamanho > 0)
                {
                    //formatando os colaboradores da escala
                    for (int i = 0; i < intTamanho - 1; i++)
                    {
                        strColaboradores.Append(pcolColaboradores[i].IdColaborador.ToString() + ",");
                    }

                    //insere o �ltimo colaborador da escala
                    strColaboradores.Append(pcolColaboradores[intTamanho - 1].IdColaborador.ToString());
                }
            }

            try
            {
                return objDLEscalaDepartamental.Alterar(pobjEscalaDepartamental, strHorarios.ToString(), strColaboradores.ToString());
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " M�todo: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLEscalaDepartamental.Finalizar();
            }
        }
        #endregion

        #region AlterarSituacao
        /// <summary>
        /// Altera o status da Situa��o.
        /// </summary>
        /// <param name="pintIdEscalaDepartamental">Id da Escala Departamental</param>
        /// <param name="pblSituacao">Flag da Situa��o</param>
        /// <param name="pintIdUsuarioAlteracao">Id do Usu�rio</param>
        /// <history>
        ///     [cmarchi] created 5/1/2010
        /// </history>
        public void AlterarSituacao(int pintIdEscalaDepartamental, bool pblnSituacao, int pintIdUsuarioAlteracao)
        {
            DLEscalaDepartamental objDLEscalaDepartamental = new DLEscalaDepartamental();

            try
            {
                objDLEscalaDepartamental.AlterarSituacao(pintIdEscalaDepartamental,
                                                         pblnSituacao,
                                                         pintIdUsuarioAlteracao);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " M�todo: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLEscalaDepartamental.Finalizar();
            }
        }
        #endregion

        #region Gerar Hor�rios
        /// <summary>
        /// Gera os hor�rios para um dia.
        /// </summary>
        /// <history>
        ///     [cmarchi] created 30/12/2009
        ///     [cmarchi] modify 4/1/2010
        /// </history>
        public Collection<HorarioEscala> GerarHorarios()
        {
            Collection<HorarioEscala> colHorarios = new Collection<HorarioEscala>();
            int intMinuto = 0;
            int intIntervalo = 5;
            string strHorario = string.Empty;

            for (int i = 0; i < 24; i++)
            {
                for (int j = 0; j < 60; j++)
                {
                    intMinuto = j % intIntervalo;
                    if (intMinuto == 0)
                    {
                        strHorario = (i < 10) ? "0" + i.ToString() + ":" : i.ToString() + ":";
                        strHorario += (j < 10) ? "0" + j.ToString() : j.ToString();

                        HorarioEscala objHorarioEscala = new HorarioEscala();

                        objHorarioEscala.IdHorario = strHorario;
                        colHorarios.Add(objHorarioEscala);
                    }
                }
            }

            //insere os hor�rios restantes
            //HorarioEscala objHorarioEscala1 = new HorarioEscala();

            //objHorarioEscala1.IdHorario = "Compensado";
            //colHorarios.Add(objHorarioEscala1);

            //HorarioEscala objHorarioEscala2 = new HorarioEscala();

            //objHorarioEscala2.IdHorario = "Folga/DSR";
            //colHorarios.Add(objHorarioEscala2);

            //HorarioEscala objHorarioEscala3 = new HorarioEscala();

            //objHorarioEscala3.IdHorario = "F�rias/Licen�a";
            //colHorarios.Add(objHorarioEscala3);

            return colHorarios;
        }
        #endregion

        #region Inserir
        /// <summary>
        /// Insere uma escala departamental com seus hor�rios e colaboradores.
        /// </summary>
        /// <returns>Id_EscalaDepartamental ou Erro</returns>
        /// <history>
        ///     [cmarchi] created 4/1/2010
        ///     [haguiar_5] modify 11/02/2011
        /// </history>
        public int Inserir(EscalaDepartamental pobjEscalaDepartamental,
            Collection<HorarioEscala> pcolHorariosEscala, Collection<Colaborador> pcolColaboradores)
        {
            int intTamanho = pcolHorariosEscala.Count;
            StringBuilder strHorarios = new StringBuilder();
            StringBuilder strColaboradores = new StringBuilder(); 
            
            DLEscalaDepartamental objDLEscalaDepartamental = new DLEscalaDepartamental();

            if (intTamanho > 0)
            {
                //formatando os hor�rios da escala
                for (int i = 0; i < intTamanho - 1; i++)
                {
                    strHorarios.Append(pcolHorariosEscala[i].IdHorario + ",");
                }

                //insere o �ltimo hor�rio da escala
                strHorarios.Append(pcolHorariosEscala[pcolHorariosEscala.Count - 1].IdHorario);
            }


            if (pcolColaboradores != null)
            {
                //int 
                intTamanho = pcolColaboradores.Count;

                //StringBuilder strColaboradores = new StringBuilder();

                if (intTamanho > 0)
                {
                    //formatando os colaboradores da escala
                    for (int i = 0; i < intTamanho - 1; i++)
                    {
                        strColaboradores.Append(pcolColaboradores[i].IdColaborador.ToString() + ",");
                    }

                    //insere o �ltimo colaborador da escala
                    strColaboradores.Append(pcolColaboradores[intTamanho - 1].IdColaborador.ToString());
                }
            }

            try
            {
                return objDLEscalaDepartamental.Inserir(pobjEscalaDepartamental, strHorarios.ToString(), strColaboradores.ToString());
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " M�todo: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLEscalaDepartamental.Finalizar();
            }
        }
        #endregion
                
        #region Listar

        #region Listar Escala Departamental Exclu�do ou N�o
        /// <summary>
        ///     Lista todas as Escalas Departmentais de acordo com o par�metro.
        /// </summary>
        /// <param name="pblnListarTudo">True - Lista Exclu�das e n�o exclu�das, False Todas n�o Esxclu�das</param>
        /// <returns>Lista Contendo  as Escalas Departamental</returns>
        /// <history>
        ///     [cmarchi] created 6/1/2010
        /// </history>
        public Collection<EscalaDepartamental> ListarEscalaDepartamental(bool pblnListarTudo)
        {
            DLEscalaDepartamental objDLEscalaDepartamental = new DLEscalaDepartamental();

            try
            {
                return objDLEscalaDepartamental.ListarEscalaDepartamental(pblnListarTudo);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " M�todo: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLEscalaDepartamental.Finalizar();
            }
        }
        #endregion

        #region Listar Escala Departamental com Itens Exclu�dos ou N�o, Por flagSitua��o
        /// <summary>
        ///     Lista todas as Escalas Departmentais de acordo com o par�metro Filtro por flag situa��o.
        /// </summary>
        /// <param name="pblnListarTudo">True - Lista Exclu�das e n�o exclu�das, False Todas n�o Esxclu�das</param>
        /// <returns>Lista Contendo  as Escalas Departamental</returns>
        /// <history>
        ///     [abranco] created 24/06/2010
        /// </history>
        public Collection<EscalaDepartamental> ListarEscalaDepartamentalByFlagSituacao(bool pblnListarTudo)
        {
            DLEscalaDepartamental objDLEscalaDepartamental = new DLEscalaDepartamental();

            try
            {
                return objDLEscalaDepartamental.ListarEscalaDepartamentalByFlagSituacao(pblnListarTudo);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " M�todo: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLEscalaDepartamental.Finalizar();
            }
        }
        #endregion

        #region Listar Escala Departamental
        /// <summary>
        ///     Lista todas as Escalas Departmentais de acordo com os par�metros.
        /// </summary>
        /// <param name="pintRegional">C�digo da Regional</param>
        /// <param name="pintFilial">C�digo da Filial</param>
        /// <param name="pstrDescricaoEscalaDepartamental">Descri��o da EscalaDepartamental</param>
        /// <param name="pintPeriodicidade">C�digo de Periodicidade</param>
        /// <param name="pblnListarTudo">True - Lista Exclu�das e n�o exclu�das, False Todas n�o Esxclu�das</param>
        /// <returns>Lista Contendo  as Escalas Departamental</returns>
        /// <history>
        ///     [cmarchi] created 4/1/2010
        /// </history>
        public DataTable ListarEscalaDepartamental(int pintRegional, int pintFilial,
            string pstrDescricaoEscalaDepartamental, int pintPeriodicidade)
        {
            DLEscalaDepartamental objDLEscalaDepartamental = new DLEscalaDepartamental();

            try
            {
                return objDLEscalaDepartamental.ListarEscalaDepartamental(pintRegional,
                                                      pintFilial, pstrDescricaoEscalaDepartamental,
                                                      pintPeriodicidade, true, false);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " M�todo: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLEscalaDepartamental.Finalizar();
            }
        }

        /// <summary>
        ///     Lista todas as Escalas Departmentais de acordo com os par�metros.
        /// </summary>
        /// <param name="pintRegional">C�digo da Regional</param>
        /// <param name="pintFilial">C�digo da Filial</param>
        /// <param name="pstrDescricaoEscalaDepartamental">Descri��o da EscalaDepartamental</param>
        /// <param name="pintPeriodicidade">C�digo de Periodicidade</param>
        /// <param name="pblnListarTudo">True - Lista Exclu�das e n�o exclu�das, False Todas n�o Esxclu�das</param>
        /// <returns>Lista Contendo  as Escalas Departamental</returns>
        /// <history>
        ///     [haguiar_3] created 07/01/2011
        ///     [haguiar_SDM9004] modify 26/08/2011 16:08
        ///     listar escalas departamentais com flag crew
        /// </history>
        public DataTable ListarEscalaDepartamental(int pintRegional, int pintFilial,
            string pstrDescricaoEscalaDepartamental, int pintPeriodicidade, bool pblnListarTudo, bool pblnListarCrw)
        {
            DLEscalaDepartamental objDLEscalaDepartamental = new DLEscalaDepartamental();

            try
            {
                return objDLEscalaDepartamental.ListarEscalaDepartamental(pintRegional,
                                                      pintFilial, pstrDescricaoEscalaDepartamental,
                                                      pintPeriodicidade, pblnListarTudo, pblnListarCrw);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " M�todo: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLEscalaDepartamental.Finalizar();
            }
        }

        #endregion

        #endregion

        #region Obter

        #region ObterColaboradores
        /// <summary>
        /// Obt�m Colaboradores da Escala��o.
        /// </summary>
        /// <param name="pintIdEscalacao">Id da Escala��o</param>
        /// <returns>Objeto Colaboradores</returns>
        /// <history>
        ///     [cmarchi] created 12/1/2010
        /// </history>
        public Collection<Colaborador> ObterColaboradores(int pintIdEscalaDepartamental)
        {
            DLEscalaDepartamental objDLEscalaDepartamental = new DLEscalaDepartamental();

            try
            {
                return objDLEscalaDepartamental.ObterColaborador(pintIdEscalaDepartamental);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " M�todo: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLEscalaDepartamental.Finalizar();
            }
        }
        #endregion

        #region Obter
        /// <summary>
        /// Obt�m uma Escala Departamental
        /// </summary>
        /// <param name="pintIdEscalaDepartamental">Id da Escala Departamental</param>
        /// <param name="pblnFlg_Descricao">Retorna descri��o dos hor�rios ou somente hor�rios</param>
        /// <history>
        ///     [cmarchi] created 5/1/2010
        ///     [haguiar] modify 16/01/2012 17:21
        /// </history>
        public EscalaDepartamental Obter(int pintIdEscalaDepartamental, bool pblnFlg_Descricao, int ? pintIdTipoSolicitacao)
        {
            DLEscalaDepartamental objDLEscalaDepartamental = new DLEscalaDepartamental();
            EscalaDepartamental objEscalaDepartamental = null;

            try
            {
                objEscalaDepartamental = objDLEscalaDepartamental.ObterEscalaDepartamental(pintIdEscalaDepartamental);
                objEscalaDepartamental.HorariosEscala = objDLEscalaDepartamental.ListarEscalaHorarios(pintIdEscalaDepartamental, pblnFlg_Descricao, pintIdTipoSolicitacao);

                return objEscalaDepartamental;
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " M�todo: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLEscalaDepartamental.Finalizar();
            }
        }
        #endregion


        #region Obter
        /// <summary>
        /// Obt�m uma Escala Departamental CREW
        /// </summary>
        /// <param name="pintId_Filial">Retorna escala departamental</param>
        /// <history>
        ///     [haguiar] created 22/03/2012 14:21
        /// </history>
        public int ObterEscalaDptoCrew(int pintId_Filial)
        {
            DLEscalaDepartamental objDLEscalaDepartamental = new DLEscalaDepartamental();
            int pId_EscalaDpto;

            try
            {
                pId_EscalaDpto = objDLEscalaDepartamental.ObterEscalaDptoCrew(pintId_Filial);

                return pId_EscalaDpto;
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " M�todo: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLEscalaDepartamental.Finalizar();
            }
        }
        #endregion

        #endregion

        #region ExcluirColaborador

        public bool ExcluirColaborador(int idColaborador, int idEscalaDpto, int? idEscalacao)
        {
            DLEscalaDepartamental objDLEscalaDepartamental = new DLEscalaDepartamental();

            try
            {
                return objDLEscalaDepartamental.ExcluirColaborador(idColaborador, idEscalaDpto, idEscalacao.GetValueOrDefault(0));
    }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " M�todo: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
}
            finally
            {
                objDLEscalaDepartamental.Finalizar();
            }
        }


        #endregion
    }
}
