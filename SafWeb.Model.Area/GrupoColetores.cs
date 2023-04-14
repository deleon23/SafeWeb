using System;
using System.Collections.Generic;
using System.Text;

namespace SafWeb.Model.Area
{
    public class GrupoColetores
    {
        /// <summary>  
        ///     Model da tabela de Grupo Coletores
        /// </summary> 
        /// <history> 
        ///     [haguiar_4] 26/01/2011
        /// </history> 
        /// -----------------------------------------------------------------------------
        
        #region Variáveis

        private string strDes_GrupoColetores;
        private int intId_GrupoColetores;

        #endregion

        #region Propriedades

        /// <summary> Armazena o conteúdo do campo strDes_GrupoColetores  </summary>
        public string Des_GrupoColetores
        {
            get { return strDes_GrupoColetores; }
            set { strDes_GrupoColetores = value; }
        }

        /// <summary> Armazena o conteúdo do campo Id_GrupoColetores  </summary>
        public int Id_GrupoColetores
        {
            get { return intId_GrupoColetores; }
            set { intId_GrupoColetores = value; }
        }

        #endregion

        public void FromIDataReader(System.Data.IDataReader pobjIDataReader)
        {
            if (pobjIDataReader == null) return;

            if (pobjIDataReader["Des_GrupoColetores"] != System.DBNull.Value)
                this.Des_GrupoColetores = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Des_GrupoColetores"));

            if (pobjIDataReader["Id_GrupoColetores"] != System.DBNull.Value)
                this.Id_GrupoColetores = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_GrupoColetores"));
        }
    }
}