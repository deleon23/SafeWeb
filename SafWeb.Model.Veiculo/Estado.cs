using System;
using System.Collections.Generic;
using System.Text;

namespace SafWeb.Model.Veiculo
{
    public class Estado
    {
        /// ----------------------------------------------------------------------------- 
        /// Project : SafWeb.Model.Veiculo
        /// Class : Model.Veiculo.Estado
        /// ----------------------------------------------------------------------------- 
        /// <summary>  
        ///     Model da tabela de Estado
        /// </summary> 
        /// <history> 
        ///     [mribeiro] 10/07/2009 Created 
        /// </history> 
        /// -----------------------------------------------------------------------------
        #region Variáveis

        private int intEstado;
        private string srtDescricao;
        private string strAlias;

        #endregion

        #region Propriedades

        /// <summary> Armazena o conteúdo do campo Id_Estado  </summary> 
        public int Codigo
        {
            get { return intEstado; }
            set { intEstado = value; }
        }

        /// <summary> Armazena o conteúdo do campo Des_Estado  </summary> 
        public string DescricaoEstado
        {
            get { return srtDescricao; }
            set { srtDescricao = value; }
        }

        /// <summary> Armazena o conteúdo do campo Alias_Estado  </summary> 
        public string AliasEstado
        {
            get { return strAlias; }
            set { strAlias = value; }
        }

        #endregion

        public void FromIDataReader(System.Data.IDataReader pobjIDataReader)
        {
            if (pobjIDataReader == null) return;

            if (pobjIDataReader["Id_Estado"] != System.DBNull.Value)
                this.Codigo = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_Estado"));

            if (pobjIDataReader["Des_Estado"] != System.DBNull.Value)
                this.DescricaoEstado = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Des_Estado"));

            if (pobjIDataReader["Alias_Estado"] != System.DBNull.Value)
                this.AliasEstado= pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Alias_Estado"));
        }
    }
}
