using System;
using System.Collections.Generic;
using System.Text;
using SafWeb.Util.Extension;

namespace SafWeb.Model.Permissao
{
    [Serializable]
    public class LimitePrmColaborador
    {
        public int idLimiteColaborador { get; set; }

        public int idColaborador { get; set; }

        public string NomeColaborador { get; set; }

        public int idLimite { get; set; }

        public decimal Limite { get; set; }

        public void FromIDataReader(System.Data.IDataReader pobjIDataReader)
        {
            if (pobjIDataReader == null) return;

            if (pobjIDataReader.ColumnExists("Id_LimiteColaborador"))
                if (pobjIDataReader["Id_LimiteColaborador"] != System.DBNull.Value)
                    this.idLimiteColaborador = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_LimiteColaborador"));

            if (pobjIDataReader.ColumnExists("Usu_N_Codigo"))
                if (pobjIDataReader["Usu_N_Codigo"] != System.DBNull.Value)
                    this.idColaborador = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Usu_N_Codigo"));

            if (pobjIDataReader.ColumnExists("USU_C_NOME"))
                if (pobjIDataReader["USU_C_NOME"] != System.DBNull.Value)
                    this.NomeColaborador = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("USU_C_NOME"));

            if (pobjIDataReader.ColumnExists("Id_Limite"))
                if (pobjIDataReader["Id_Limite"] != System.DBNull.Value)
                    this.idLimite = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_Limite"));

            if (pobjIDataReader.ColumnExists("Limite"))
                if (pobjIDataReader["Limite"] != System.DBNull.Value)
                    this.Limite = pobjIDataReader.GetDecimal(pobjIDataReader.GetOrdinal("Limite"));

        }
    }
}
