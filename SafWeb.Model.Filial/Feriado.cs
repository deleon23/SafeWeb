using System;
using System.Collections.Generic;
using System.Text;

namespace SafWeb.Model.Filial
{
    public class Feriado
    {
        //Cod_Feriado
        public int codigo { get; set; }

        //Dat_Feriado
        public DateTime data { get; set; }

        //Des_Feriado
        public string descricao { get; set; }

        public void FromIDataReader(System.Data.IDataReader pobjIDataReader)
        {
            if (pobjIDataReader == null) return;

            if (pobjIDataReader["Cod_Feriado"] != System.DBNull.Value)
                this.codigo = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Cod_Feriado"));

            if (pobjIDataReader["Dat_Feriado"] != System.DBNull.Value)
                this.data = pobjIDataReader.GetDateTime(pobjIDataReader.GetOrdinal("Dat_Feriado"));

            if (pobjIDataReader["Des_Feriado"] != System.DBNull.Value)
                this.descricao = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Des_Feriado"));
        }
    }
}
