using System;
using System.Collections.Generic;
using System.Text;

namespace SafWeb.Model.Solicitacao
{
    [Serializable]
    public class Solicitacao
    {
        #region Variáveis
        
        private int intCodigo;
        private int intCodUsuSolic;
        private int intCodVisitado;
        private int intCodVisitante;
        private int intCodVeiculo;
        private int intCodVeiculoLista;
        private DateTime datInicioVisita;
        private DateTime datFimVisita;
        private bool blnAcSabado;
        private bool blnAcDomingo;
        private bool blnAcFeriado;
        private string strCodArea;
        private int intCodTipoSolicitacao;
        private int intCodMotivoVisita;
        private string strObservacao;
        private bool blnSituacao;
        private int intCodFilial;
        private string strAliasFilial;
        private string strVisitante;
        private int intCodEmpresa;
        private string strEmpresa;
        private string strTipoSolicitacao;
        private int intCodStatus;
        private string strStatus;
        private string strUsuarioSolic;
        private int intCodUsuarioAprov;
        private string strUsuarioAprov;
        private string strPendenteCom;
        private int intCodColaboradorLista;
        private string strCorFonte;

        #endregion

        #region Propriedades

        public int Codigo
        {
            get { return intCodigo; }
            set { intCodigo = value; }
        }

        public int CodUsuSolic
        {
            get { return intCodUsuSolic;  }
            set { intCodUsuSolic = value; }
        }

        public int CodVisitado
        {
            get { return intCodVisitado; }
            set { intCodVisitado = value; }
        }

        public int CodVisitante
        {
            get { return intCodVisitante; }
            set { intCodVisitante = value; }
        }

        public int CodVeiculo
        {
            get { return intCodVeiculo; }
            set { intCodVeiculo = value; }
        }

        public int CodVeiculoLista
        {
            get { return intCodVeiculoLista; }
            set { intCodVeiculoLista = value; }
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

        public int CodTipoSolicitacao
        {
            get { return intCodTipoSolicitacao; }
            set { intCodTipoSolicitacao = value; }
        }

        public int CodMotivoVisita
        {
            get { return intCodMotivoVisita; }
            set { intCodMotivoVisita = value; }
        }

        public string Observacao
        {
            get { return strObservacao; }
            set { strObservacao = value; }
        }

        public bool Situacao
        {
            get { return blnSituacao; }
            set { blnSituacao = value; }
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

        public string Visitante
        {
            get { return strVisitante; }
            set { strVisitante = value; }
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

        public string TipoSolicitacao
        {
            get { return strTipoSolicitacao; }
            set { strTipoSolicitacao = value; }
        }

        public int CodStatus
        {
            get { return intCodStatus; }
            set { intCodStatus = value; }
        }

        public string Status
        {
            get { return strStatus; }
            set { strStatus = value; }
        }

        public string UsuarioSolic
        {
            get { return strUsuarioSolic; }
            set { strUsuarioSolic = value; }
        }

        public int CodUsuarioAprov
        {
            get { return intCodUsuarioAprov; }
            set { intCodUsuarioAprov = value; }
        }

        public string UsuarioAprov
        {
            get { return strUsuarioAprov; }
            set { strUsuarioAprov = value; }
        }

        public string PendenteCom
        {
            get { return strPendenteCom; }
            set { strPendenteCom = value; }
        }

        public int CodColaboradorLista
        {
            get { return intCodColaboradorLista; }
            set { intCodColaboradorLista = value; }
        }

        public string CorFonte
        {
            get { return strCorFonte; }
            set { strCorFonte = value; }
        }

        #endregion

        public void FromIDataReader(System.Data.IDataReader pobjIDataReader)
        {
            if (pobjIDataReader == null) return;

            if (pobjIDataReader["Id_Solicitacao"] != System.DBNull.Value)
                this.Codigo = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_Solicitacao"));

            if (pobjIDataReader["Id_UsuarioSolicitante"] != System.DBNull.Value)
                this.CodUsuSolic = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_UsuarioSolicitante"));
        
            if (pobjIDataReader["Id_Visitante"] != System.DBNull.Value)
                this.CodVisitante = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_Visitante"));

            if (pobjIDataReader["Dt_InicioVisita"] != System.DBNull.Value)
                this.InicioVisita = pobjIDataReader.GetDateTime(pobjIDataReader.GetOrdinal("Dt_InicioVisita"));

            if (pobjIDataReader["Dt_FimVisita"] != System.DBNull.Value)
                this.FimVisita = pobjIDataReader.GetDateTime(pobjIDataReader.GetOrdinal("Dt_FimVisita"));

            if (pobjIDataReader["Flg_AcSabado"] != System.DBNull.Value)
                this.AcSabado = (Convert.ToInt16(pobjIDataReader.GetValue(pobjIDataReader.GetOrdinal("Flg_AcSabado"))) == 1 ? true : false);

            if (pobjIDataReader["Flg_AcDomingo"] != System.DBNull.Value)
                this.AcDomingo = (Convert.ToInt16(pobjIDataReader.GetValue(pobjIDataReader.GetOrdinal("Flg_AcDomingo"))) == 1 ? true : false);

            if (pobjIDataReader["Flg_AcFeriado"] != System.DBNull.Value)
                this.AcFeriado = (Convert.ToInt16(pobjIDataReader.GetValue(pobjIDataReader.GetOrdinal("Flg_AcFeriado"))) == 1 ? true : false);

            if (pobjIDataReader["Flg_Situacao"] != System.DBNull.Value)
                this.Situacao = (Convert.ToInt16(pobjIDataReader.GetValue(pobjIDataReader.GetOrdinal("Flg_Situacao"))) == 1 ? true : false);

            if (pobjIDataReader["Id_TipoSolicitacao"] != System.DBNull.Value)
                this.CodTipoSolicitacao = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_TipoSolicitacao"));

            if (pobjIDataReader["Id_Filial"] != System.DBNull.Value)
                this.CodFilial = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_Filial"));

            if (pobjIDataReader["Alias_Filial"] != System.DBNull.Value)
                this.AliasFilial = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Alias_Filial"));

            if (pobjIDataReader["Nom_Visitante"] != System.DBNull.Value)
                this.Visitante = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Nom_Visitante"));

            if (pobjIDataReader["Id_Empresa"] != System.DBNull.Value)
                this.CodEmpresa = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_Empresa"));

            if (pobjIDataReader["Des_Empresa"] != System.DBNull.Value)
                this.Empresa = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Des_Empresa"));

            if (pobjIDataReader["Des_TipoSolicitacao"] != System.DBNull.Value)
                this.TipoSolicitacao = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Des_TipoSolicitacao"));

            if (pobjIDataReader["Id_StatusSolicitacao"] != System.DBNull.Value)
                this.CodStatus = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_StatusSolicitacao"));

            if (pobjIDataReader["Des_StatusAprovacao"] != System.DBNull.Value)
                this.Status = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Des_StatusAprovacao"));

            if (pobjIDataReader["Nom_UsuarioSolicitante"] != System.DBNull.Value)
                this.UsuarioSolic = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Nom_UsuarioSolicitante"));

            if (pobjIDataReader["Id_UsuarioAprovador"] != System.DBNull.Value)
                this.CodUsuarioAprov = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_UsuarioAprovador"));

            if (pobjIDataReader["Nom_UsuarioAprovador"] != System.DBNull.Value)
                this.UsuarioAprov = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Nom_UsuarioAprovador"));

            if (pobjIDataReader["PendenteCom"] != System.DBNull.Value)
                this.PendenteCom = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("PendenteCom"));

            if (pobjIDataReader["Id_ColaboradorLista"] != System.DBNull.Value && pobjIDataReader["Id_ColaboradorLista"] != "")
                this.CodColaboradorLista = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_ColaboradorLista"));

            if (pobjIDataReader["Html_CorFonte"] != System.DBNull.Value)
                this.CorFonte = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Html_CorFonte"));
        }
    }
}
