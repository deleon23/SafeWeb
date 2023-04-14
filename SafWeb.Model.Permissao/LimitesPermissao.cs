using System;
using System.Collections.Generic;
using System.Text;
using SafWeb.Util.Extension;

namespace SafWeb.Model.Permissao
{
    public class LimitePermissao
    {
        public int idLimite { get; set; }

        public double Limite { get; set; }

        public void FromIDataReader(System.Data.IDataReader pobjIDataReader)
        {
            if (pobjIDataReader == null) return;

            if (pobjIDataReader.ColumnExists("Id_Limite"))
                if (pobjIDataReader["Id_Limite"] != System.DBNull.Value)
                    this.idLimite = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_Limite"));

            if (pobjIDataReader.ColumnExists("Limite"))
                if (pobjIDataReader["Limite"] != System.DBNull.Value)
                    this.Limite = Convert.ToDouble(pobjIDataReader.GetDecimal(pobjIDataReader.GetOrdinal("Limite")));


        }
    }
}
