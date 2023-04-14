using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.ComponentModel;
using SafWeb.Model.Acesso;
using SafWeb.BusinessLayer.Acesso;
using System.Collections.ObjectModel;
using System.Globalization;
using FrameWork.BusinessLayer.Usuarios;
using FrameWork.Model.Usuarios;
using SafWeb.Model.Filial;
using SafWeb.BusinessLayer.Filial;

namespace SafWeb.UI.Modulos.Portaria
{
    public class ColaboradoresData
    {

        private int colaboradoresForaCount;
        private int colaboradoresDentroCount;

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        ///     Método GetColaboradoresFora do radgridCriado para melhorar o 
        ///     desempenho no carregamento da lista
        /// </summary> 
        /// <history> 
        ///     [aoliveira] 03/01/2013 Created 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<ListaAcessoColaborador> GetColaboradoresFora(int startIndex, int pageSize, string sortBy, string pNumeroDocumento, string pNomeColaborador, string pHoraInicio, string pHoraFim, string pEscalaColaborador, string pEscalaDep)
        {
            Usuario gobjUsuario = new Usuario();
            Filial gobjFilial = new Filial();
            BLFilial gobjBLFilial = new BLFilial();

            if (startIndex > 0)
            {
                startIndex++; // Para não repetir o ultimo da pagina anterior como primeiro dessa página.
            }

            BLAcessoColaborador objBLAcessoColaborador = new BLAcessoColaborador();
            Collection<ListaAcessoColaborador> colListaAcessoColaborador = new Collection<ListaAcessoColaborador>();

            int intFilial = 0;
            string strNumeroDocumento = string.Empty;
            string strNomeColaborador = string.Empty;
            int intHoraInicio = -1;
            int intHoraFim = -1;
            int intMinutoInicio = 0;
            int intMinutoFim = 0;
            int intNumeroEscala = 0;
            int intEscalaDepto = 0;
            try
            {
                //Obtém Usuário logado
                gobjUsuario = BLAcesso.ObterUsuario();
                gobjFilial = gobjBLFilial.Obter(gobjUsuario.CodigoFilial);
                intFilial = gobjUsuario.CodigoFilial;

                if ((pNumeroDocumento != null) && (pNumeroDocumento != string.Empty))
                {
                    strNumeroDocumento = pNumeroDocumento;
                }
                if ((pNomeColaborador != null) && (pNomeColaborador != string.Empty))
                {
                    strNomeColaborador = pNomeColaborador;
                }
                if ((pHoraInicio != null) && (pHoraInicio != string.Empty))
                {
                    intHoraInicio = Convert.ToInt32(pHoraInicio.Substring(0, 2));
                    intMinutoInicio = Convert.ToInt32(pHoraInicio.Substring(3, 2));
                }
                if ((pHoraFim != null) && (pHoraFim != string.Empty))
                {
                    intHoraFim = Convert.ToInt32(pHoraFim.Substring(0, 2));
                    intMinutoFim = Convert.ToInt32(pHoraFim.Substring(3, 2));
                }
                if ((pEscalaColaborador != null) && (pEscalaColaborador != string.Empty))
                {
                    intNumeroEscala = Convert.ToInt32(pEscalaColaborador);
                }
                if ((pEscalaDep != null) && (pEscalaDep != string.Empty) && (Convert.ToInt32(pEscalaDep) > 0))
                {
                    intEscalaDepto = Convert.ToInt32(pEscalaDep);
                }

                CultureInfo ci = CultureInfo.InvariantCulture;

                string pstrHoraAtual = DateTime.UtcNow.AddHours(gobjFilial.Vlr_FusoHorario).AddHours(-3).ToString("HH:mm", ci);

                colListaAcessoColaborador = objBLAcessoColaborador.Listar(intFilial, strNumeroDocumento, strNomeColaborador,
                                    intNumeroEscala, intHoraInicio, intHoraFim, intMinutoInicio, intMinutoFim, intEscalaDepto, 0, 
                                    pstrHoraAtual,
                                    startIndex, pageSize, sortBy, ref colaboradoresForaCount);
                
                if (colListaAcessoColaborador.Count == 0) // Volta uma página para verificar se a ultima página está sendo consultada, mas foi removida. 
                {
                    startIndex = startIndex - pageSize;
                    colListaAcessoColaborador = objBLAcessoColaborador.Listar(intFilial, strNumeroDocumento, strNomeColaborador,
                                    intNumeroEscala, intHoraInicio, intHoraFim, intMinutoInicio, intMinutoFim, intEscalaDepto, 0, 
                                    pstrHoraAtual,
                                    startIndex, pageSize, sortBy, ref colaboradoresForaCount);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return new List<ListaAcessoColaborador>(colListaAcessoColaborador);
        }

        public int GetColaboradoresForaCount(string pNumeroDocumento, string pNomeColaborador, string pHoraInicio, string pHoraFim, string pEscalaColaborador, string pEscalaDep)
        {
            return colaboradoresForaCount;
        }


        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        ///     Método GetColaboradoresDentro do radgrid. Criado para melhorar o 
        ///     desempenho no carregamento da lista
        /// </summary> 
        /// <history> 
        ///     [aoliveira] 03/01/2013 Created 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<ListaAcessoColaborador> GetColaboradoresDentro(int startIndex, int pageSize, string sortBy, string pNumeroDocumento, string pNomeColaborador, string pHoraInicio, string pHoraFim, string pEscalaColaborador, string pEscalaDep)
        {
            Usuario gobjUsuario = new Usuario();
            Filial gobjFilial = new Filial();
            BLFilial gobjBLFilial = new BLFilial();

            if (startIndex > 0)
            {
                startIndex++; // Para não repetir o ultimo da pagina anterior como primeiro dessa página.
            }

            BLAcessoColaborador objBLAcessoColaborador = new BLAcessoColaborador();
            Collection<ListaAcessoColaborador> colListaAcessoColaborador = new Collection<ListaAcessoColaborador>();

            int intFilial = 0;
            string strNumeroDocumento = string.Empty;
            string strNomeColaborador = string.Empty;
            int intHoraInicio = -1;
            int intHoraFim = -1;
            int intMinutoInicio = 0;
            int intMinutoFim = 0;
            int intNumeroEscala = 0;
            int intEscalaDepto = 0;

            try
            {
                //Obtém Usuário logado
                gobjUsuario = BLAcesso.ObterUsuario();
                gobjFilial = gobjBLFilial.Obter(gobjUsuario.CodigoFilial);
                intFilial = gobjUsuario.CodigoFilial;

                if ((pNumeroDocumento != null) && (pNumeroDocumento != string.Empty))
                {
                    strNumeroDocumento = pNumeroDocumento;
                }
                if ((pNomeColaborador != null) && (pNomeColaborador != string.Empty))
                {
                    strNomeColaborador = pNomeColaborador;
                }
                if ((pHoraInicio != null) && (pHoraInicio != string.Empty))
                {
                    intHoraInicio = Convert.ToInt32(pHoraInicio.Substring(0, 2));
                    intMinutoInicio = Convert.ToInt32(pHoraInicio.Substring(3, 2));
                }
                if ((pHoraFim != null) && (pHoraFim != string.Empty))
                {
                    intHoraFim = Convert.ToInt32(pHoraFim.Substring(0, 2));
                    intMinutoFim = Convert.ToInt32(pHoraFim.Substring(3, 2));
                }
                if ((pEscalaColaborador != null) && (pEscalaColaborador != string.Empty))
                {
                    intNumeroEscala = Convert.ToInt32(pEscalaColaborador);
                }
                if ((pEscalaDep != null) && (pEscalaDep != string.Empty) && (Convert.ToInt32(pEscalaDep) > 0))
                {
                    intEscalaDepto = Convert.ToInt32(pEscalaDep);
                }

                if (sortBy == "")
                {
                    sortBy = "NOM_COLABORADOR";
                }

                CultureInfo ci = CultureInfo.InvariantCulture;
                string pstrHoraAtual = DateTime.UtcNow.AddHours(gobjFilial.Vlr_FusoHorario).ToString("HH:mm", ci);
                
                colListaAcessoColaborador = objBLAcessoColaborador.Listar(intFilial, strNumeroDocumento, strNomeColaborador,
                                    intNumeroEscala, intHoraInicio, intHoraFim, intMinutoInicio, intMinutoFim, intEscalaDepto, 1, 
                                    pstrHoraAtual,
                                    startIndex, pageSize, sortBy, ref colaboradoresDentroCount);

                if (colListaAcessoColaborador.Count == 0) // Volta uma página para verificar se a ultima página está sendo consultada, mas foi removida. 
                {
                    startIndex = startIndex - pageSize;
                    colListaAcessoColaborador = objBLAcessoColaborador.Listar(intFilial, strNumeroDocumento, strNomeColaborador,
                                    intNumeroEscala, intHoraInicio, intHoraFim, intMinutoInicio, intMinutoFim, intEscalaDepto, 1, 
                                    pstrHoraAtual,
                                    startIndex, pageSize, sortBy, ref colaboradoresDentroCount);
                }
            }
            catch (Exception ex)
            {

            }
            return new List<ListaAcessoColaborador>(colListaAcessoColaborador);
        }

        public int GetColaboradoresDentroCount(string pNumeroDocumento, string pNomeColaborador, string pHoraInicio, string pHoraFim, string pEscalaColaborador, string pEscalaDep)
        {
            return colaboradoresDentroCount;
        }

    }
}