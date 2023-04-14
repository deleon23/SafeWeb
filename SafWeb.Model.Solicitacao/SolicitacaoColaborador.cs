using System;
using System.Collections.Generic;
using System.Text;

namespace SafWeb.Model.Solicitacao
{
    [Serializable]
    public class SolicitacaoColaborador
    {
        /// ----------------------------------------------------------------------------- 
        /// Project : SafWeb.Model.Solicitacao
        /// Class : Model.Solicitacao.SolicitacaoColaborador
        /// ----------------------------------------------------------------------------- 
        /// <summary>  
        ///     Model das tabelas de Solicitacao e Colaborador
        /// </summary> 
        /// <history> 
        ///     [mribeiro] 08/07/2009 Created 
        /// </history> 
        /// -----------------------------------------------------------------------------
        #region Variáveis

        private int intCodSolicitacao;
        private int intCodVisitante;
        private string strNomeVisitante;
        private int intCodLista;
        private int intCodVeiculo;
        private string strVeiculo;
        private int intEstadoVeiculo;
        private DateTime datInicioVisita;
        private DateTime datFimVisita;
        private bool blnAcSabado;
        private bool blnAcDomingo;
        private bool blnAcFeriado;
        private string strCodArea;
        private string strNomeArea;
        private int intCodMotivoVisita;
        private string strMotivoVisita;
        private string strObservacao;
        private int intCodRegional;
        private int intCodFilial;
        private string strAliasFilial;
        private int intCodEmpresa;
        private string strEmpresa;
        private int intCodTipoDocumento;
        private string strNumDocumento;
        private int intCodTipoSolicitacao;
        private int intCodTipoColaborador;
        private int intCodVisitado;
        private int intCodAprovador;
        private string strColInclusos;

        #endregion

        #region Propriedades

        public int CodSolicitacao
        {
            get { return intCodSolicitacao; }
            set { intCodSolicitacao = value; }
        }

        public int CodVisitante
        {
            get { return intCodVisitante; }
            set { intCodVisitante = value; }
        }

        public string NomeVisitante
        {
            get { return strNomeVisitante; }
            set { strNomeVisitante = value; }
        }

        public int CodLista
        {
            get { return intCodLista; }
            set { intCodLista = value; }
        }

        public int CodVeiculo
        {
            get { return intCodVeiculo; }
            set { intCodVeiculo = value; }
        }

        public string PlacaVeiculo
        {
            get { return strVeiculo; }
            set { strVeiculo = value; }
        }

        public int CodEstadoVeiculo
        {
            get { return intEstadoVeiculo; }
            set { intEstadoVeiculo = value; }
        }

        public DateTime InicioVisita
        {
            get { return datInicioVisita; }
            set { datInicioVisita = value; }
        }

        public DateTime FimVisita
        {
            get { return datFimVisita; }
            set { datFimVisita = value; }
        }

        public bool AcSabado
        {
            get { return blnAcSabado; }
            set { blnAcSabado = value; }
        }

        public bool AcDomingo
        {
            get { return blnAcDomingo; }
            set { blnAcDomingo = value; }
        }

        public bool AcFeriado
        {
            get { return blnAcFeriado; }
            set { blnAcFeriado = value; }
        }

        public string CodArea
        {
            get { return strCodArea; }
            set { strCodArea = value; }
        }

        public string NomeArea
        {
            get { return strNomeArea; }
            set { strNomeArea = value; }
        }

        public int CodMotivoVisita
        {
            get { return intCodMotivoVisita; }
            set { intCodMotivoVisita = value; }
        }

        public string MotivoVisita
        {
            get { return strMotivoVisita; }
            set { strMotivoVisita = value; }
        }

        public string Observacao
        {
            get { return strObservacao; }
            set { strObservacao = value; }
        }

        public int CodRegional
        {
            get { return intCodRegional; }
            set { intCodRegional = value; }
        }

        public int CodFilial
        {
            get { return intCodFilial; }
            set { intCodFilial = value; }
        }

        public string AliasFilial
        {
            get { return strAliasFilial; }
            set { strAliasFilial = value; }
        }

        public int CodEmpresa
        {
            get { return intCodEmpresa; }
            set { intCodEmpresa = value; }
        }

        public string Empresa
        {
            get { return strEmpresa; }
            set { strEmpresa = value; }
        }

        public int CodTipoDocumento
        {
            get { return intCodTipoDocumento; }
            set { intCodTipoDocumento = value; }
        }

        public string NumeroDocumento
        {
            get { return strNumDocumento; }
            set { strNumDocumento = value; }
        }

        public int CodTipoSolicitacao
        {
            get { return intCodTipoSolicitacao; }
            set { intCodTipoSolicitacao = value; }
        }

        public int CodTipoColaborador
        {
            get { return intCodTipoColaborador; }
            set { intCodTipoColaborador = value; }
        }

        public int CodVisitado
        {
            get { return intCodVisitado; }
            set { intCodVisitado = value; }
        }

        public int CodAprovador
        {
            get { return intCodAprovador; }
            set { intCodAprovador = value; }
        }

        public string CodInclusos
        {
            get { return strColInclusos; }
            set { strColInclusos = value; }
        }

        public int CodListaVeiculos { get; set; }

        #endregion
    }
}
