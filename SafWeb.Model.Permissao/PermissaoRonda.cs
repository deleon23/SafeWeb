using System;
using System.Collections.Generic;
using System.Text;
using SafWeb.Util.Extension;


namespace SafWeb.Model.Permissao
{
    [Serializable]
    public class PermissaoRonda
    {
        public int idPermissao { get; set; }

        public string desPermissao { get; set; }


        public void FromIDataReader(System.Data.IDataReader pobjIDataReader)
        {
            if (pobjIDataReader == null) return;

            if (pobjIDataReader.ColumnExists("id_Permissao"))
                if (pobjIDataReader["id_Permissao"] != System.DBNull.Value)
                    this.idPermissao = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("id_Permissao"));

            if (pobjIDataReader.ColumnExists("des_Permissao"))
                if (pobjIDataReader["des_Permissao"] != System.DBNull.Value)
                    this.desPermissao = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("des_Permissao"));

        }
    }
}
