using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using SafWeb.Model.Escala;
using SafWeb.DataLayer.Escala;
using SafWeb.Model.Colaborador;
using FrameWork.BusinessLayer.Utilitarios;

namespace SafWeb.BusinessLayer.Escala
{
    /// ----------------------------------------------------------------------------- 
    /// Project : 
    /// Class : BLJornada
    /// ----------------------------------------------------------------------------- 
    /// <summary>  
    /// Implementação da classe BLJornada
    /// </summary> 
    /// <history> 
    ///     [cmarchi] created 15/1/2010
    /// </history> 
    /// ----------------------------------------------------------------------------- 
    public class BLJornada
    {
        #region ExcluirColaborador
        /// <summary>
        /// Exclui colaborador(es).
        /// </summary>
        /// <param name="pcolJornadasColaboradores">Lista de Jornada dos Colaboradores</param>
        /// <param name="pintIdJornada">Id da Jornada</param>
        /// <param name="arrIdColaboradores">Id dos Colaboradoradores</param>
        /// <history>
        ///     [cmarchi] created 15/1/2010
        /// </history>
        public static void ExcluirColaborador(ref Collection<JornadaColaboradores> pcolJornadasColaboradores,
            int pintIndice, int pintIdJornada, string[] parrIdColaboradores)
        {
            if (pintIndice >= 0)
            {
                if (pcolJornadasColaboradores[pintIndice].IdJornada == pintIdJornada)
                {
                    string[] arrColaboradores = pcolJornadasColaboradores[pintIndice].NomesColaboradores.Split(',');
                    string[] arrIdColaboradores = pcolJornadasColaboradores[pintIndice].CodigosColaboradores.Split(',');

                    //verifica a quantidade de colaboradores no índice
                    if (arrColaboradores.Length == 1 && arrIdColaboradores.Length == 1)
                    {
                        pcolJornadasColaboradores.RemoveAt(pintIndice);
                    }
                    else if (arrColaboradores.Length > 1 && arrIdColaboradores.Length > 1 &&
                        arrColaboradores.Length == arrIdColaboradores.Length)
                    {
                        int intPosicao = -1;

                        StringBuilder strIdColaboradores = new StringBuilder();
                        StringBuilder strColaboradores = new StringBuilder();

                        for (int i = 0; i < parrIdColaboradores.Length; i++)
                        {
                            intPosicao = Array.IndexOf(arrIdColaboradores, parrIdColaboradores[i]);

                            //verifica se encontrou o id do colaborador
                            //e "remove" o id e o nome do colaborador não encontrado
                            if (intPosicao >= 0)
                            {
                                arrColaboradores[intPosicao] = "-1";
                                arrIdColaboradores[intPosicao] = "-1";

                                intPosicao = -1;
                            }
                        }

                        //copia os item não "excluídos"
                        for (int i = 0; i < arrIdColaboradores.Length; i++)
                        {
                            if (arrIdColaboradores[i] != "-1" &&
                                arrIdColaboradores[i] != arrColaboradores[i])
                            {
                                strIdColaboradores.Append(arrIdColaboradores[i] + ",");
                                strColaboradores.Append(arrColaboradores[i] + ",");
                            }
                        }

                        //verifica se removeu todos os colaboradores de uma jornada
                        if (strIdColaboradores.Length > 0 && strColaboradores.Length > 0)
                        {
                            //remove os "," das strings
                            strIdColaboradores.Remove(strIdColaboradores.Length - 1, 1);
                            strColaboradores.Remove(strColaboradores.Length - 1, 1);

                            //insere os ids e nome dos colaboradores
                            pcolJornadasColaboradores[pintIndice].CodigosColaboradores =
                                strIdColaboradores.ToString();

                            pcolJornadasColaboradores[pintIndice].NomesColaboradores =
                                strColaboradores.ToString();
                        }
                        else
                        {
                            pcolJornadasColaboradores.RemoveAt(pintIndice);
                        }
                    }
                }
            }
        }
        #endregion

        #region IndiceJornadaColaborador
        /// <summary>
        /// Obtém o índice de uma Jornada.
        /// </summary>
        /// <param name="pcolJornadasColaboradores">Coleção de Jornada dos Colaboradores</param>
        /// <param name="intIdJornada">Id da Jornada</param>
        /// <returns>índice do objeto Jornada</returns>
        /// <history>
        ///     [cmarchi] created 13/1/2010
        /// </history>
        public static int IndiceJornadaColaborador(Collection<JornadaColaboradores> pcolJornadasColaboradores, int intIdJornada)
        {
            if (pcolJornadasColaboradores != null && pcolJornadasColaboradores.Count > 0)
            {
                int intTamanho = pcolJornadasColaboradores.Count;

                for (int i = 0; i < intTamanho; i++)
                {
                    if (pcolJornadasColaboradores[i].IdJornada == intIdJornada)
                    {
                        return i;
                    }
                }
            }

            return -1;
        }
        #endregion

        #region Inserir

        #region Inserir
        /// <summary>
        /// Insere
        /// </summary>
        /// <param name="pcolJornadasColaboradores"></param>
        /// <history>
        ///     [cmarchi] created 18/1/2010
        /// </history>
        public void Inserir(Collection<JornadaColaboradores> pcolJornadaColaboradores)
        {
            DLJornada objDLJornada = new DLJornada();

            try
            {
                if(pcolJornadaColaboradores.Count > 0)
                {
                    objDLJornada.BeginTransaction();

                        foreach (JornadaColaboradores objJornadaColaboradores in pcolJornadaColaboradores)
                        {
                            objDLJornada.Inserir(objJornadaColaboradores);
                        } 
                   
                    objDLJornada.CommitTransaction();
                }
            }
            catch (Exception ex)
            {
                objDLJornada.RollBackTransaction();
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLJornada.Finalizar();
            }
        }
        #endregion

        #region InserirJornadaColaborador
        /// <summary>
        /// Insere a jornada de um Colaborador.
        /// </summary>
        /// <param name="pcolJornadasColaboradores">Coleção de Jornada dos Colaboradores</param>
        /// <param name="pintIndice">Índice</param>
        /// <param name="pobjJornadaColaboradores">Objeto Jornada de Colaboradores</param>
        /// <history>
        ///     [cmarchi] created 13/1/2010
        /// </history>
        public void InserirJornadaColaborador(ref Collection<JornadaColaboradores> pcolJornadasColaboradores, int pintIndice,
           JornadaColaboradores pobjJornadaColaboradores)
        {
            if (pcolJornadasColaboradores != null)
            {
                if (pintIndice >= 0)
                {
                    string[] arrIdColaboradores = pcolJornadasColaboradores[pintIndice].CodigosColaboradores.Split(',');

                    if (arrIdColaboradores != null && arrIdColaboradores.Length > 0)
                    {
                        Array.Sort(arrIdColaboradores);

                        int intPosicao = Array.BinarySearch(arrIdColaboradores, pobjJornadaColaboradores.CodigosColaboradores);

                        if (intPosicao < 0)
                        {
                            pcolJornadasColaboradores[pintIndice].CodigosColaboradores += "," + pobjJornadaColaboradores.CodigosColaboradores;
                            pcolJornadasColaboradores[pintIndice].NomesColaboradores += "," + pobjJornadaColaboradores.NomesColaboradores;
                        }
                    }
                    else
                    {
                        pcolJornadasColaboradores[pintIndice].CodigosColaboradores = pobjJornadaColaboradores.CodigosColaboradores;
                        pcolJornadasColaboradores[pintIndice].NomesColaboradores = pobjJornadaColaboradores.NomesColaboradores;
                    }
                }
                else
                {
                    pcolJornadasColaboradores.Add(pobjJornadaColaboradores);
                }
            }
        }
        #endregion

        #endregion

        #region Listar

        #region ListarJornadas
        /// <summary>
        /// Obtém Jornadas.
        /// </summary>
        /// <returns>Lista de Objetos Jornada</returns>
        /// <history>
        ///     [cmarchi] created 15/1/2010
        /// </history>
        public Collection<Jornada> ListarJornadas()
        {
            DLJornada objDLJornada = new DLJornada();

            try
            {
                return objDLJornada.ListarJornadas();
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLJornada.Finalizar();
            }
        }
        #endregion        

        #endregion

        #region Obter

        #region ObterColaboradoresNaoContemJornada
        /// <summary>
        /// Obtém Colaboradores da Escalação que ainda não tem uma Jornada.
        /// </summary>
        /// <param name="pintIdEscalacao">Id da Escalação</param>
        /// <returns>Lista de Objetos Colaborador</returns>
        /// <history>
        ///     [cmarchi] created 15/1/2010
        /// </history>
        public Collection<Colaborador> ObterColaboradoresNaoContemJornada(int pintIdEscalacao)
        {
            DLJornada objDLJornada = new DLJornada();

            try
            {
                return objDLJornada.ObterColaboradoresNaoContemJornada(pintIdEscalacao);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLJornada.Finalizar();
            }
        }
        #endregion        
    
        #region ObterJornadaColaboradores
        /// <summary>
        /// Obtém Jornada dos Colaboradores da Escalação.
        /// </summary>
        /// <param name="pintIdEscalacao">Id da Escalação</param>
        /// <returns>Lista de Jornada dos Colaboradores</returns>
        /// <history>
        ///     [cmarchi] created 8/2/2010
        /// </history>
        public Collection<JornadaColaboradores> ObterJornadaColaboradores(int pintIdEscalacao)
        {
            DLJornada objDLJornada = new DLJornada();

            Collection<JornadaColaboradores> colJornadaColaboradores =
                new Collection<JornadaColaboradores>();

            Collection<EscalacaoJornadaColaboradores> colEscalacaoJornadaColaboradores =
                new Collection<EscalacaoJornadaColaboradores>();

            int intPosicaoJornada = -1;
                        
            try
            {
                //obtendo as jornadas dos colaboradores da escalação
                colEscalacaoJornadaColaboradores = objDLJornada.ObterJornadaColaboradores(pintIdEscalacao);

                //inserindo as jornadas na lista colJornadaColaboradores                
                for (int i = 0; i < colEscalacaoJornadaColaboradores.Count ; i++)
			    {
                    intPosicaoJornada = BLJornada.IndiceJornadaColaborador(colJornadaColaboradores,
                                                       colEscalacaoJornadaColaboradores[i].IdJornada);

                    JornadaColaboradores objJornadaColaboradores = new JornadaColaboradores();

                    objJornadaColaboradores.IdEscalacao      = colEscalacaoJornadaColaboradores[i].IdEscalacao;
                    objJornadaColaboradores.IdJornada        = colEscalacaoJornadaColaboradores[i].IdJornada;
                    objJornadaColaboradores.DescricaoJornada = colEscalacaoJornadaColaboradores[i].NomeJornada;

                    objJornadaColaboradores.CodigosColaboradores = colEscalacaoJornadaColaboradores[i].CodigoColaborador.ToString();
                    objJornadaColaboradores.NomesColaboradores   = colEscalacaoJornadaColaboradores[i].NomeColaborador;

                    this.InserirJornadaColaborador(ref colJornadaColaboradores, intPosicaoJornada,
                        objJornadaColaboradores);
			    }

                return colJornadaColaboradores;
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLJornada.Finalizar();
            }
        }
        #endregion

        #endregion
    }
}
