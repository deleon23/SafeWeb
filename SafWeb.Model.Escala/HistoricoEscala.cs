using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace SafWeb.Model.Escala
{
    /// ----------------------------------------------------------------------------- 
    /// Project : 
    /// Class : HistoricoEscala
    /// ----------------------------------------------------------------------------- 
    /// <summary>  
    /// Implementação da classe HorarioEscala
    /// </summary> 
    /// <history> 
    /// [tgerevini] created 21/05/2010 
    /// </history> 
    /// -----------------------------------------------------------------------------
    public class HistoricoEscala
    {
        #region Variáveis

        private int intIdEscalacao;
        private int intIdFilial;
        private string strALiasFilial;
        private int intIdEscalaDepto;
        private string strDesEscalaDepto;
        private int intIdTipoSolicitacao;
        private string strDesTipoSolicitacao;
        private DateTime datInicioPeriodo;
        private DateTime datFimPeriodo;
        private string strPeriodo;
        private int intIdStatusEscala;
        private string strDesStatusAprovacao;
        private int intIdUsuarioSolicitante;
        private string strNomeSolicitante;
        private int intIdUsuarioAprovador;
        private string strNomeAprovador;
        private string strPendenteCom;
        private string strHtmlCorFonte;
        private bool blnSituacao;
        private bool blnEditando;
        private DateTime datDtInclusao;
        private DateTime datDtAlteracao;
  
        #endregion

        #region Propriedades

        /// <summary> Armazena o conteúdo do campo ID_ESCALACAO  </summary> 
        public int IdEscalacao
        {
            get { return this.intIdEscalacao; }
            set { this.intIdEscalacao = value; }
        }

        /// <summary> Armazena o conteúdo do campo ID_FILIAL  </summary> 
        public int IdFilial
        {
            get { return this.intIdFilial; }
            set { this.intIdFilial = value; }
        }

        /// <summary> Armazena o conteúdo do campo ALIAS_FILIAL </summary> 
        public string AliasFilial
        {
            get { return this.strALiasFilial; }
            set { this.strALiasFilial = value; }
        }

        /// <summary> Armazena o conteúdo do campo ID_ESCALADPTO </summary> 
        public int IdEscalaDepto
        {
            get { return this.intIdEscalaDepto; }
            set { this.intIdEscalaDepto = value; }
        }

        /// <summary> Armazena o conteúdo do campo DES_ESCALADPTO </summary> 
        public string DesEscalaDepto
        {
            get { return this.strDesEscalaDepto; }
            set { this.strDesEscalaDepto = value; }
        }

        /// <summary> Armazena o conteúdo do campo ID_TIPOSOLICITACAO </summary> 
        public int IdTipoSolicitacao
        {
            get { return this.intIdTipoSolicitacao; }
            set { this.intIdTipoSolicitacao = value; }
        }

        /// <summary> Armazena o conteúdo do campo DES_TIPOSOLICITACAO </summary> 
        public string DesTipoSolicitacao
        {
            get { return this.strDesTipoSolicitacao; }
            set { this.strDesTipoSolicitacao = value; }
        }

        /// <summary> Armazena o conteúdo do campo DT_INICIOPERIODO </summary> 
        public DateTime DataInicioPeriodo
        {
            get { return this.datInicioPeriodo; }
            set { this.datInicioPeriodo = value; }
        }

        /// <summary> Armazena o conteúdo do campo DT_FINALPERIODO </summary> 
        public DateTime DataFinalPeriodo
        {
            get { return this.datFimPeriodo; }
            set { this.datFimPeriodo = value; }
        }

        /// <summary> Armazena o conteúdo do campo PERIODO </summary> 
        public string Periodo
        {
            get { return this.strPeriodo; }
            set { this.strPeriodo = value; }
        }

        /// <summary> Armazena o conteúdo do campo ID_STATUSSOLICITACAO </summary> 
        public int IdStatusSolicitacao
        {
            get { return this.intIdStatusEscala; }
            set { this.intIdStatusEscala = value; }
        }

        /// <summary> Armazena o conteúdo do campo DES_STATUSAPROVACAO </summary> 
        public string DescricaoStatusAprovacao
        {
            get { return this.strDesStatusAprovacao; }
            set { this.strDesStatusAprovacao = value; }
        }

        /// <summary> Armazena o conteúdo do campo ID_USUARIOSOLICITANTE </summary> 
        public int IdUsuarioSolicitante
        {
            get { return this.intIdUsuarioSolicitante; }
            set { this.intIdUsuarioSolicitante = value; }
        }

        /// <summary> Armazena o conteúdo do campo ID_USUARIOAPROVADOR </summary> 
        public int IdUsuarioAprovador
        {
            get { return this.intIdUsuarioAprovador; }
            set { this.intIdUsuarioAprovador = value; }
        }

        /// <summary> Armazena o conteúdo do campo NOM_USUARIOAPROVADOR </summary> 
        public string NomeUsuarioAprovador
        {
            get { return this.strNomeAprovador; }
            set { this.strNomeAprovador = value; }
        }

        /// <summary> Armazena o conteúdo do campo NOM_USUARIOSOLICITANTE </summary> 
        public string NomeUsuarioSolicitante
        {
            get { return this.strNomeSolicitante; }
            set { this.strNomeSolicitante = value; }
        }

        /// <summary> Armazena o conteúdo do campo PENDENTECOM </summary> 
        public string PendenteCom
        {
            get { return this.strPendenteCom; }
            set { this.strPendenteCom = value; }
        }

        /// <summary> Armazena o conteúdo do campo HTML_CORFONTE </summary> 
        public string HTMLCorFonte
        {
            get { return this.strHtmlCorFonte; }
            set { this.strHtmlCorFonte = value; }
        }

        /// <summary> Armazena o conteúdo do campo FLG_SITUACAO </summary> 
        public Boolean Situacao
        {
            get { return this.blnSituacao; }
            set { this.blnSituacao = value; }
        }

        /// <summary> Armazena o conteúdo do campo FLG_EDITANDO </summary> 
        public Boolean Editando
        {
            get { return this.blnEditando; }
            set { this.blnEditando = value; }
        }

        /// <summary> Armazena o conteúdo do campo DT_INCLUSAO </summary> 
        public DateTime DataInclusao
        {
            get { return this.datDtInclusao; }
            set { this.datDtInclusao = value; }
        }

        /// <summary> Armazena o conteúdo do campo DT_ALTERACAO </summary> 
        public DateTime DataAlteracao
        {
            get { return this.datDtAlteracao; }
            set { this.datDtAlteracao = value; }
        }

        #endregion

        #region FromIDataReader

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// Metodo que popula a Model com base em uma interface datareader
        /// </summary>
        /// <param name="pobjIDataReader">interface datareader</param>
        /// <value>IDataReader</value> 
        /// <history> 
        /// [tgerevini] created 21/05/2010 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public void FromIDataReader(IDataReader pobjIDataReader)
        {
            if (pobjIDataReader == null) return;

            if (pobjIDataReader["ID_ESCALACAO"] != System.DBNull.Value)
                this.IdEscalacao = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("ID_ESCALACAO"));

            if (pobjIDataReader["ID_FILIAL"] != System.DBNull.Value)
                this.IdFilial = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("ID_FILIAL"));

            if (pobjIDataReader["ALIAS_FILIAL"] != System.DBNull.Value)
                this.AliasFilial = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("ALIAS_FILIAL"));

            if (pobjIDataReader["ID_ESCALADPTO"] != System.DBNull.Value)
                this.IdEscalaDepto = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("ID_ESCALADPTO"));

            if (pobjIDataReader["DES_ESCALADPTO"] != System.DBNull.Value)
                this.DesEscalaDepto = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("DES_ESCALADPTO"));

            if (pobjIDataReader["ID_TIPOSOLICITACAO"] != System.DBNull.Value)
                this.IdTipoSolicitacao = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("ID_TIPOSOLICITACAO"));

            if (pobjIDataReader["DES_TIPOSOLICITACAO"] != System.DBNull.Value)
                this.DesTipoSolicitacao = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("DES_TIPOSOLICITACAO"));

            if (pobjIDataReader["DT_INICIOPERIODO"] != System.DBNull.Value)
                this.DataInicioPeriodo = pobjIDataReader.GetDateTime(pobjIDataReader.GetOrdinal("DT_INICIOPERIODO"));

            if (pobjIDataReader["DT_FINALPERIODO"] != System.DBNull.Value)
                this.DataFinalPeriodo = pobjIDataReader.GetDateTime(pobjIDataReader.GetOrdinal("DT_FINALPERIODO"));

            if (pobjIDataReader["PERIODO"] != System.DBNull.Value)
                this.Periodo = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("PERIODO"));

            if (pobjIDataReader["ID_STATUSSOLICITACAO"] != System.DBNull.Value)
                this.IdStatusSolicitacao = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("ID_STATUSSOLICITACAO"));

            if (pobjIDataReader["DES_STATUSAPROVACAO"] != System.DBNull.Value)
                this.DescricaoStatusAprovacao = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("DES_STATUSAPROVACAO"));

            if (pobjIDataReader["ID_USUARIOSOLICITANTE"] != System.DBNull.Value)
                this.IdUsuarioSolicitante = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("ID_USUARIOSOLICITANTE"));

            if (pobjIDataReader["NOM_USUARIOSOLICITANTE"] != System.DBNull.Value)
                this.NomeUsuarioSolicitante = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("NOM_USUARIOSOLICITANTE"));

            if (pobjIDataReader["ID_USUARIOAPROVADOR"] != System.DBNull.Value)
                this.IdUsuarioAprovador = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("ID_USUARIOAPROVADOR"));

            if (pobjIDataReader["NOM_USUARIOAPROVADOR"] != System.DBNull.Value)
                this.NomeUsuarioAprovador = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("NOM_USUARIOAPROVADOR"));

            if (pobjIDataReader["PENDENTECOM"] != System.DBNull.Value)
                this.PendenteCom = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("PENDENTECOM"));

            if (pobjIDataReader["HTML_CORFONTE"] != System.DBNull.Value)
                this.HTMLCorFonte = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("HTML_CORFONTE"));

            if (pobjIDataReader["FLG_SITUACAO"] != System.DBNull.Value)
                this.Situacao = pobjIDataReader.GetBoolean(pobjIDataReader.GetOrdinal("FLG_SITUACAO"));

            if (pobjIDataReader["FLG_EDITANDO"] != System.DBNull.Value)
                this.Editando = pobjIDataReader.GetBoolean(pobjIDataReader.GetOrdinal("FLG_EDITANDO"));

            if (pobjIDataReader["DT_INCLUSAO"] != System.DBNull.Value)
                this.DataInclusao = pobjIDataReader.GetDateTime(pobjIDataReader.GetOrdinal("DT_INCLUSAO"));

            if (pobjIDataReader["DT_ALTERACAO"] != System.DBNull.Value)
                this.DataAlteracao = pobjIDataReader.GetDateTime(pobjIDataReader.GetOrdinal("DT_ALTERACAO"));
        }

        #endregion

    }
}
