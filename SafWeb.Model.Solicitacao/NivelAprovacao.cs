using System;
using System.Collections.Generic;
using System.Text;

namespace SafWeb.Model.Solicitacao
{
    public class NivelAprovacao
    {
        #region Variaveis

        private int intCodigo;
        private string strDescricao;

        #endregion

        #region Propriedades

        public int Codigo
        {
            get { return intCodigo; }
            set { intCodigo = value; }
        }

        public string Descricao
        {
            get { return strDescricao; }
            set { strDescricao = value; }
        }

        #endregion

        public void FromIDataReader(System.Data.IDataReader pobjIDataReader)
        {
            if (pobjIDataReader == null) return;

            if (pobjIDataReader["Id_NivelAprovacao"] != System.DBNull.Value)
                this.Codigo = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_NivelAprovacao"));

            if (pobjIDataReader["Des_NivelAprovacao"] != System.DBNull.Value)
                this.Descricao = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Des_NivelAprovacao"));
        }
    }
}
