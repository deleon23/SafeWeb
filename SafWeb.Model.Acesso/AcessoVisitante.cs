using System;
using System.Collections.Generic;
using System.Text;

namespace SafWeb.Model.Acesso
{
    public class AcessoVisitante
    {
        #region Variáveis

        private int intCodAcessoVisitante;
        private int intCodSolicitacao;
        private int intCodVisitante;
        private string strNomVisitante;
        private string strDesTipoDocumento;
        private int intCodTipoDocumento;
        private string strNumDocumento;
        private DateTime datEntrada;
        private DateTime datValidadeVisita;
        private DateTime datSaida;
        private int intCodAcompanhante;
        private int intCodCracha;
        private string strNumCracha;
        private int intCodVeiculo;
        private int intCodEstado;
        private string strPlaca;
        private decimal decCodUsuLibEnt;
        private decimal decCodUsuLibSai;
        private string strObsVisita;
        private int intUsuario;
        private string strDataEntrada;

        #endregion

        #region Propriedades

        public int CodAcessoVisitante
        {
            get { return intCodAcessoVisitante; }
            set { intCodAcessoVisitante = value; }
        }
        
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

        public string NomVisitante
        {
            get { return strNomVisitante; }
            set { strNomVisitante = value; }
        }

        public string DesTipoDocumento
        {
            get { return strDesTipoDocumento; }
            set { strDesTipoDocumento = value; }
        }

        public int CodTipoDocumento
        {
            get { return intCodTipoDocumento; }
            set { intCodTipoDocumento = value; }
        }

        public string NumDocumento
        {
            get { return strNumDocumento; }
            set { strNumDocumento = value; }
        }

        public DateTime Entrada
        {
            get { return datEntrada; }
            set { datEntrada = value; }
        }

        public DateTime ValidadeVisita
        {
            get { return datValidadeVisita; }
            set { datValidadeVisita = value; }
        }

        public DateTime Saida
        {
            get { return datSaida; }
            set { datSaida = value; }
        }

        public int CodAcompanhante
        {
            get { return intCodAcompanhante; }
            set { intCodAcompanhante = value; }
        }

        public int CodCracha
        {
            get { return intCodCracha; }
            set { intCodCracha = value; }
        }

        public string NumCracha
        {
            get { return strNumCracha; }
            set { strNumCracha = value; }
        }

        public int CodVeiculo
        {
            get { return intCodVeiculo; }
            set { intCodVeiculo = value; }
        }

        public int CodEstado
        {
            get { return intCodEstado; }
            set { intCodEstado = value; }
        }

        public string Placa
        {
            get { return strPlaca; }
            set { strPlaca = value; }
        }

        public decimal CodUsuLibEnt
        {
            get { return decCodUsuLibEnt; }
            set { decCodUsuLibEnt = value; }
        }

        public decimal CodUsuLibSai
        {
            get { return decCodUsuLibSai; }
            set { decCodUsuLibSai = value; }
        }

        public string ObsVisita
        {
            get { return strObsVisita; }
            set { strObsVisita = value; }
        }

        public int CodUsuario
        {
            get { return intUsuario; }
            set { intUsuario = value; }
        }

        public string DataEntrada
        {
            get { return strDataEntrada; }
            set { strDataEntrada = value; }
        }

        #endregion

        public void FromIDataReader(System.Data.IDataReader pobjIDataReader)
        {
            if (pobjIDataReader == null) return;

            if (pobjIDataReader["Id_AcessoVisitante"] != System.DBNull.Value)
                this.CodAcessoVisitante = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_AcessoVisitante"));

            if (pobjIDataReader["Id_Cracha"] != System.DBNull.Value)
                this.CodCracha = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_Cracha"));

            if (pobjIDataReader["Num_Cracha"] != System.DBNull.Value)
                this.NumCracha = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Num_Cracha"));

            if (pobjIDataReader["Id_Veiculo"] != System.DBNull.Value)
                this.CodVeiculo = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_Veiculo"));

            if (pobjIDataReader["Id_Estado"] != System.DBNull.Value)
                this.CodEstado = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_Estado"));

            if (pobjIDataReader["Des_Placa"] != System.DBNull.Value)
                this.Placa = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Des_Placa"));

            if (pobjIDataReader["Id_Visitante"] != System.DBNull.Value)
                this.CodVisitante = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_Visitante"));

            if (pobjIDataReader["Nom_Visitante"] != System.DBNull.Value)
                this.NomVisitante = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Nom_Visitante"));

            if (pobjIDataReader["Des_DocumentoTipo"] != System.DBNull.Value)
                this.DesTipoDocumento = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Des_DocumentoTipo"));

            if (pobjIDataReader["Num_Documento"] != System.DBNull.Value)
                this.NumDocumento = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Num_Documento"));

            if (pobjIDataReader["Dt_Entrada"] != System.DBNull.Value)
                this.DataEntrada = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Dt_Entrada"));
        }
    }
}
