using System;
using System.Collections.Generic;
using System.Text;

namespace SafWeb.Model.Acesso
{
    public class AcessoColaborador
    {
        #region Variáveis

        private int intIdAcessoColEscalado;
        private int intIdFilial;
        private int intIdEscalacao;
        private DateTime datDataEntrada;
        private DateTime datDataSaida;
        private int intUsuarioLiberaEntrada;
        private int intUsuarioLiberaSaida;
        private int intIdColaborador;
        private DateTime datDataEscalacao;

        #endregion

        #region Propriedades

        public int CodAcessoColEscalado
        {
            get { return intIdAcessoColEscalado; }
            set { intIdAcessoColEscalado = value; }
        }

        public int CodFilial
        {
            get { return intIdFilial; }
            set { intIdFilial = value; }
        }

        public int CodEscalacao
        {
            get { return intIdEscalacao; }
            set { intIdEscalacao = value; }
        }

        public DateTime DataEntrada
        {
            get { return datDataEntrada; }
            set { datDataEntrada = value; }
        }

        public DateTime DataSaida
        {
            get { return datDataSaida; }
            set { datDataSaida = value; }
        }

        public int CodUsuarioLiberaEntrada
        {
            get { return intUsuarioLiberaEntrada; }
            set { intUsuarioLiberaEntrada = value; }
        }

        public int CodUsuarioLiberaSaida
        {
            get { return intUsuarioLiberaSaida; }
            set { intUsuarioLiberaSaida = value; }
        }

        public int CodColaborador
        {
            get { return intIdColaborador; }
            set { intIdColaborador = value; }
        }
        
        public DateTime DataEscalacao
        {
            get { return datDataEscalacao; }
            set { datDataEscalacao = value; }
        }


        #endregion

        #region FromIDataReader
        public void FromIDataReader(System.Data.IDataReader pobjIDataReader)
        {
            if (pobjIDataReader == null) return;

            if (pobjIDataReader["Id_AcessoColEscalado"] != System.DBNull.Value)
                this.CodAcessoColEscalado = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_AcessoColEscalado"));

            if (pobjIDataReader["Id_Filial"] != System.DBNull.Value)
                this.CodFilial = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_Filial"));

            if (pobjIDataReader["Id_Escalacao"] != System.DBNull.Value)
                this.CodEscalacao = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_Escalacao"));

            if (pobjIDataReader["Dt_Entrada"] != System.DBNull.Value)
                this.DataEntrada = pobjIDataReader.GetDateTime(pobjIDataReader.GetOrdinal("Dt_Entrada"));

            if (pobjIDataReader["Dt_Saida"] != System.DBNull.Value)
                this.DataSaida = pobjIDataReader.GetDateTime(pobjIDataReader.GetOrdinal("Dt_Saida"));

            if (pobjIDataReader["Id_UsrLiberaEntrada"] != System.DBNull.Value)
                this.CodUsuarioLiberaEntrada = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_UsrLiberaEntrada"));

            if (pobjIDataReader["Id_UsrLiberaSaida"] != System.DBNull.Value)
                this.CodUsuarioLiberaSaida = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_UsrLiberaSaida"));

            if (pobjIDataReader["Id_Colaborador"] != System.DBNull.Value)
                this.CodColaborador = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_Colaborador"));

            if (pobjIDataReader["Dt_Escalacao"] != System.DBNull.Value)
                this.DataEscalacao = pobjIDataReader.GetDateTime(pobjIDataReader.GetOrdinal("Dt_Escalacao"));
        }
        #endregion
    }
}
