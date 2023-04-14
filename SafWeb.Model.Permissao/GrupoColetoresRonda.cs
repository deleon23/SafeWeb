using System;
using System.Collections.Generic;
using System.Text;
using SafWeb.Util.Extension;

namespace SafWeb.Model.Permissao
{
    public class GrupoColetoresRonda
    {
        public int idPermissao { get; set; }

        public int idGrupoColetores { get; set; }

        public string desGrupoColetores { get; set; }

        public bool Selecionado { get; set; }

        public void FromIDataReader(System.Data.IDataReader pobjIDataReader)
        {
            if (pobjIDataReader == null) return;

            if (pobjIDataReader.ColumnExists("id_Permissao"))
                if (pobjIDataReader["id_Permissao"] != System.DBNull.Value)
                    this.idPermissao = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("id_Permissao"));

            if (pobjIDataReader.ColumnExists("id_GrupoColetores"))
                if (pobjIDataReader["id_GrupoColetores"] != System.DBNull.Value)
                    this.idGrupoColetores = pobjIDataReader.GetInt16(pobjIDataReader.GetOrdinal("id_GrupoColetores"));

            if (pobjIDataReader.ColumnExists("des_GrupoColetores"))
                if (pobjIDataReader["des_GrupoColetores"] != System.DBNull.Value)
                    this.desGrupoColetores = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("des_GrupoColetores"));
        }

    }
}
