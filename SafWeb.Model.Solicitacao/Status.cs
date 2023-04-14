using System;
using System.Collections.Generic;
using System.Text;

namespace SafWeb.Model.Solicitacao
{
    public class Status
    {
        #region Variáveis

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

            if (pobjIDataReader["Id_StatusSolicitacao"] != System.DBNull.Value)
                this.Codigo = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_StatusSolicitacao"));

            if (pobjIDataReader["Des_StatusAprovacao"] != System.DBNull.Value)
                this.Descricao = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Des_StatusAprovacao"));
        }
    }
}
