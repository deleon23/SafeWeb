using System;
using System.Collections.Generic;
using System.Text;

namespace SafWeb.Model.Cracha
{
    public class CrachaTipo
    {
        #region Variáveis

        private int intCodCrachaTipo;
        private string strDesCrachaTipo;

        #endregion

        #region Propriedades

        public int CodCrachaTipo
        {
            get { return intCodCrachaTipo; }
            set { intCodCrachaTipo = value; }
        }

        public string DesCrachaTipo
        {
            get { return strDesCrachaTipo; }
            set { strDesCrachaTipo = value; }
        }

        #endregion

        public void FromIDataReader(System.Data.IDataReader pobjIDataReader)
        {
            if (pobjIDataReader == null) return;

            if (pobjIDataReader["Id_CrachaTipo"] != System.DBNull.Value)
                this.CodCrachaTipo = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_CrachaTipo"));

            if (pobjIDataReader["Des_CrachaTipo"] != System.DBNull.Value)
                this.DesCrachaTipo = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Des_CrachaTipo"));
        }
    }
}
