using System;
using System.Collections.Generic;
using System.Text;

namespace SafWeb.Model.Colaborador
{
    public class TipoColaborador
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

        private int intIdTipo;
        private string strTipo;
        
        #endregion

        #region Propriedades

        /// <summary> Armazena o conteúdo do campo Id_TipoColaborador  </summary> 
        public int IdTipoColaborador
        {
            get { return intIdTipo; }
            set { intIdTipo = value; }
        }

        /// <summary> Armazena o conteúdo do campo Des_TipoColaborador  </summary> 
        public string DescricaoColaborador
        {
            get { return strTipo; }
            set { strTipo = value; }
        }

        #endregion

        public void FromIDataReader(System.Data.IDataReader pobjIDataReader)
        {
            if (pobjIDataReader == null) return;

            if (pobjIDataReader["Id_TipoColaborador"] != System.DBNull.Value)
                this.IdTipoColaborador = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_TipoColaborador"));

            if (pobjIDataReader["Des_TipoColaborador"] != System.DBNull.Value)
                this.DescricaoColaborador = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Des_TipoColaborador"));

        }
    }
}
