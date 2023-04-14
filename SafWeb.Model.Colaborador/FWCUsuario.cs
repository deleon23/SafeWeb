using System;
using System.Collections.Generic;
using System.Text;
using SafWeb.Util.Extension;

namespace SafWeb.Model.Colaborador
{
    [Serializable]
    public class FWCUsuario
    {
        #region Variáveis
        private int intusuNCodigo;
        private string strusuCNome;
        #endregion

        #region Propriedades


        public int UsuNCodigo
        {
            get { return intusuNCodigo; }
            set { intusuNCodigo = value; }
        }

        public string UsuCNome
        {
            get { return strusuCNome; }
            set { strusuCNome = value; }
        }
        #endregion

        public void FromIDataReader(System.Data.IDataReader pobjIDataReader)
        {

            if (pobjIDataReader == null) return;

            if (pobjIDataReader.ColumnExists("USU_N_CODIGO"))
                if (pobjIDataReader["USU_N_CODIGO"] != System.DBNull.Value)
                    this.UsuNCodigo = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("USU_N_CODIGO"));

            if (pobjIDataReader.ColumnExists("USU_C_NOME"))
                if (pobjIDataReader["USU_C_NOME"] != System.DBNull.Value)
                    this.UsuCNome = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("USU_C_NOME"));
        }
    }
}
