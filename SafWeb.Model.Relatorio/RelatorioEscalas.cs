using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace SafWeb.Model.Relatorio
{
    /// ----------------------------------------------------------------------------- 
    /// Project : SafWeb.Model.Relatorio
    /// Class : Model.Relatorio.RelatorioEscalas
    /// ----------------------------------------------------------------------------- 
    /// <summary>  
    ///     Model da tela de Relatorio de Escalas Agendadas
    /// </summary> 
    /// <history> 
    ///     [tgerevini] 15/06/2010 Created 
    /// </history> 
    /// -----------------------------------------------------------------------------
    public class RelatorioEscalas
    {
        #region Variáveis

        private int intIdEscalacao;
        private int intIdFilial;
        private string strCodColaborador;
        private string strALiasFilial;
        private string strHorario;
        private int intIdEscalaDepto;
        private string strDesEscalaDepto;
        private int intIdTipoSolicitacao;
        private string strDesTipoSolicitacao;
        private DateTime datInicioPeriodo;
        private DateTime datFimPeriodo;
        private DateTime datDataEscalacao;
        private string strPeriodo;
        private int intIdStatusEscala;
        private string strDesStatusAprovacao;
        private int intIdUsuarioSolicitante;
        private string strNomeSolicitante;
        private int intIdUsuarioAprovador;
        private string strNomeAprovador;
        private string strNomeColaborador;
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

        /// <summary> Armazena o conteúdo do campo NOM_COLABORADOR </summary> 
        public string NomeColaborador
        {
            get { return this.strNomeColaborador; }
            set { this.strNomeColaborador = value; }
        }

        /// <summary> Armazena o conteúdo do campo COD_COLABORADOR </summary> 
        public string CodigoColaborador
        {
            get { return this.strCodColaborador; }
            set { this.strCodColaborador = value; }
        }

        /// <summary> Armazena o conteúdo do campo DT_ESCALACAO </summary> 
        public DateTime DataEscalacao
        {
            get { return this.datDataEscalacao; }
            set { this.datDataEscalacao = value; }
        }

        /// <summary> Armazena o conteúdo do campo HORARIO </summary> 
        public string Horario
        {
            get { return this.strHorario; }
            set { this.strHorario = value; }
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
        /// [tgerevini] created 15/06/2010 
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

            if (pobjIDataReader["ID_USUARIOSOLICITANTE"] != System.DBNull.Value)
                this.IdUsuarioSolicitante = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("ID_USUARIOSOLICITANTE"));

            if (pobjIDataReader["NOM_USUARIOSOLICITANTE"] != System.DBNull.Value)
                this.NomeUsuarioSolicitante = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("NOM_USUARIOSOLICITANTE"));

            if (pobjIDataReader["ID_USUARIOAPROVADOR"] != System.DBNull.Value)
                this.IdUsuarioAprovador = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("ID_USUARIOAPROVADOR"));

            if (pobjIDataReader["NOM_USUARIOAPROVADOR"] != System.DBNull.Value)
                this.NomeUsuarioAprovador = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("NOM_USUARIOAPROVADOR"));

            if (pobjIDataReader["NOM_COLABORADOR"] != System.DBNull.Value)
                this.NomeColaborador = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("NOM_COLABORADOR"));

            if (pobjIDataReader["COD_COLABORADOR"] != System.DBNull.Value)
                this.CodigoColaborador = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("COD_COLABORADOR"));

            if (pobjIDataReader["DT_ESCALACAO"] != System.DBNull.Value)
                this.DataEscalacao = pobjIDataReader.GetDateTime(pobjIDataReader.GetOrdinal("DT_ESCALACAO"));

            if (pobjIDataReader["HORARIO"] != System.DBNull.Value)
                this.Horario = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("HORARIO"));
        }

        #endregion
    }
}