using System;
using System.Collections.Generic;
using System.Text;

namespace SafWeb.Model.Colaborador
{
    public class TipoDocumento
    {
        /// ----------------------------------------------------------------------------- 
        /// Project : SafWeb.Model.Colaborador
        /// Class : Model.Colaborador.TipoColaborador
        /// ----------------------------------------------------------------------------- 
        /// <summary>  
        ///     Model da tabela de ColaboradorTipo
        /// </summary> 
        /// <history> 
        ///     [mribeiro] 30/06/2009 Created 
        /// </history> 
        /// -----------------------------------------------------------------------------
        #region Variáveis

        private int intIdDocumentoTipo;
        private string strDocumentoTipo;

        #endregion

        #region Propriedades

        /// <summary> Armazena o conteúdo do campo Id_DocumentoTipo  </summary> 
        public int IdTipoDocumento
        {
            get { return intIdDocumentoTipo; }
            set { intIdDocumentoTipo = value; }
        }

        /// <summary> Armazena o conteúdo do campo Des_DocumentoTipo  </summary> 
        public string DescricaoDocumento
        {
            get { return strDocumentoTipo; }
            set { strDocumentoTipo = value; }
        }

        #endregion

        public void FromIDataReader(System.Data.IDataReader pobjIDataReader)
        {
            if (pobjIDataReader == null) return;

            if (pobjIDataReader["Id_DocumentoTipo"] != System.DBNull.Value)
                this.IdTipoDocumento = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_DocumentoTipo"));

            if (pobjIDataReader["Des_DocumentoTipo"] != System.DBNull.Value)
                this.DescricaoDocumento = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Des_DocumentoTipo"));

        }
    }
}
