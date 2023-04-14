using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

namespace SafWeb.Model.Regional
{
    public class Regional 
    {
        /// ----------------------------------------------------------------------------- 
        /// Project : SafWeb.Model.Regional
        /// Class : Model.Regional.Regional
        /// ----------------------------------------------------------------------------- 
        /// <summary>  
        ///     Model da tabela de Regionais
        /// </summary> 
        /// <history> 
        ///     [mribeiro] 22/06/2009 Created 
        /// </history> 
        /// -----------------------------------------------------------------------------
        #region Variáveis

        private int intCodigo;
        private string strNome;

        #endregion

        #region Propriedades

        /// <summary> Armazena o conteúdo do campo Id_Regional  </summary> 
        public int IdRegional
        {
            get { return intCodigo; }
            set { intCodigo = value; }
        }

        /// <summary> Armazena o conteúdo do campo Des_Regional  </summary> 
        public string DescricaoRegional
        {
            get { return strNome; }
            set { strNome = value; }
        }

        #endregion

        public void FromIDataReader(System.Data.IDataReader pobjIDataReader)
        {
            if (pobjIDataReader == null) return;

            if (pobjIDataReader["Id_Regional"] != System.DBNull.Value)
                this.IdRegional = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_Regional"));

            if (pobjIDataReader["Des_Regional"] != System.DBNull.Value)
                this.DescricaoRegional = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Des_Regional"));

        }

    }
}
