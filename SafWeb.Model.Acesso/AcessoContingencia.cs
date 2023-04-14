using System;
using System.Collections.Generic;
using System.Text;

namespace SafWeb.Model.Acesso
{
    public class AcessoContingencia
    {
        #region Variáveis

        private int intId_AcessoConting;
        private int intId_Colaborador;
        private string strCod_Colaborador;

        private int intId_UsuarioLibAcesso;
        private int intId_Aprovador;

        private DateTime datDt_Inclusao;
        private DateTime datDt_Alteracao;
        
        private string strDes_Motivo;

        #endregion

        #region Propriedades

        public int Id_AcessoConting
        {
            get { return intId_AcessoConting; }
            set { intId_AcessoConting = value; }
        }

        public int Id_Colaborador
        {
            get { return intId_Colaborador; }
            set { intId_Colaborador = value; }
        }

        public string Cod_Colaborador
        {
            get { return strCod_Colaborador; }
            set { strCod_Colaborador = value; }
        }

        public int Id_UsuarioLibAcesso
        {
            get { return intId_UsuarioLibAcesso; }
            set { intId_UsuarioLibAcesso = value; }
        }

        public int Id_Aprovador
        {
            get { return intId_Aprovador; }
            set { intId_Aprovador = value; }
        }

        public DateTime Dt_Inclusao
        {
            get { return datDt_Inclusao; }
            set { datDt_Inclusao = value; }
        }

        public DateTime Dt_Alteracao
        {
            get { return datDt_Alteracao; }
            set { datDt_Alteracao = value; }
        }

        public string Des_Motivo
        {
            get { return strDes_Motivo; }
            set { strDes_Motivo = value; }
        }

        #endregion

        #region FromIDataReader
        public void FromIDataReader(System.Data.IDataReader pobjIDataReader)
        {
            if (pobjIDataReader == null) return;

            if (pobjIDataReader["Id_AcessoConting"] != System.DBNull.Value)
                this.Id_AcessoConting = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_AcessoConting"));

            if (pobjIDataReader["Id_Colaborador"] != System.DBNull.Value)
                this.Id_Colaborador = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_Colaborador"));

            if (pobjIDataReader["Cod_Colaborador"] != System.DBNull.Value)
                this.Cod_Colaborador = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Cod_Colaborador"));

            if (pobjIDataReader["Id_UsuarioLibAcesso"] != System.DBNull.Value)
                this.Id_UsuarioLibAcesso = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_UsuarioLibAcesso"));

            if (pobjIDataReader["Id_Aprovador"] != System.DBNull.Value)
                this.Id_Aprovador = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_Aprovador"));

            if (pobjIDataReader["Dt_Inclusao"] != System.DBNull.Value)
                this.Dt_Inclusao = pobjIDataReader.GetDateTime(pobjIDataReader.GetOrdinal("Dt_Inclusao"));

            if (pobjIDataReader["Dt_Alteracao"] != System.DBNull.Value)
                this.Dt_Alteracao = pobjIDataReader.GetDateTime(pobjIDataReader.GetOrdinal("Dt_Alteracao"));

            if (pobjIDataReader["Des_Motivo"] != System.DBNull.Value)
                this.Des_Motivo = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Des_Motivo"));
        }
        #endregion
    }
}