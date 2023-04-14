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
using SafWeb.BusinessLayer.Colaborador;

namespace SafWeb.UI.Modulos.Solicitacao
{
    public class ColaboradoresData
    {

        private int colaboradoresCount;

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        ///     Método GetColaboradores do radgrid. Criado para melhorar o 
        ///     desempenho no carregamento da lista
        /// </summary> 
        /// <history> 
        ///     [aoliveira] 01/02/2013 Created 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        [DataObjectMethod(DataObjectMethodType.Select)]
        public Collection<SafWeb.Model.Colaborador.Colaborador> GetColaboradores(int startIndex, int pageSize, string sortBy, string pNome, string pDocumento, string pTipo, string pIgnorarEmFerias, ref string pNovaBusca)
        {
            string strNome = string.Empty;
            string strDocumento = string.Empty;
            string strTipo = "0";
            string strIgnorarEmFerias = "0";

            BLColaborador objBlColaborador = new BLColaborador();
            Collection<SafWeb.Model.Colaborador.Colaborador> colColaborador = new Collection<SafWeb.Model.Colaborador.Colaborador>();

            try
            {

                if ((pNovaBusca != null) && (pNovaBusca != string.Empty))
                {
                    startIndex = 0;
                }
                if ((pNome != null) && (pNome != string.Empty))
                {
                    strNome = pNome;
                }
                if ((pDocumento != null) && (pDocumento != string.Empty))
                {
                    strDocumento = pDocumento;
                }
                if ((pTipo != null) && (pTipo != string.Empty))
                {
                    strTipo = pTipo;
                }
                
                if ((pIgnorarEmFerias != null) && (pIgnorarEmFerias != string.Empty))
                {
                    strIgnorarEmFerias = pIgnorarEmFerias;
                }


                if ((strTipo != string.Empty) && (strTipo != "0"))
                {
                    if (strTipo == "13")
                    {
                        colColaborador = objBlColaborador.ListarColaboradorFuncionarioTerceiro(strNome, strDocumento, 0, startIndex, pageSize, sortBy, ref colaboradoresCount);
                    }
                    else
                    {
                        colColaborador = objBlColaborador.ListarColaborador(strNome, strDocumento, Convert.ToInt32(strTipo), Convert.ToInt32(strIgnorarEmFerias), startIndex, pageSize, sortBy, ref colaboradoresCount);
                    }
                }
                else
                {
                    colColaborador = objBlColaborador.ListarColaboradorVisitado(strNome, strDocumento, startIndex, pageSize, sortBy, ref colaboradoresCount);
                }
                
                return colColaborador;   
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int GetColaboradoresCount(string pNome, string pDocumento, string pTipo, string pIgnorarEmFerias, ref string pNovaBusca)
        {
            return colaboradoresCount;
        }       

    }
}