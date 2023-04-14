using System;
using System.Collections.Generic;
using System.Text;

namespace SafWeb.Model.Filial
{
    public class Cidade
    {
        /// ----------------------------------------------------------------------------- 
        /// Project : SafWeb.Model.Filial
        /// Class : Model.Filial.Cidade
        /// ----------------------------------------------------------------------------- 
        /// <summary>  
        ///     Model da tabela de Cidade
        /// </summary> 
        /// <history> 
        ///     [haguiar_4] 14/01/2011 Created 
        /// </history> 
        /// -----------------------------------------------------------------------------
        #region Variáveis

        private int intId_Estado;
        private string srtDes_Cidade;
        private int intId_Cidade;

        #endregion

        #region Propriedades

        /// <summary> Armazena o conteúdo do campo Id_Estado  </summary> 
        public int Id_Estado
        {
            get { return intId_Estado; }
            set { intId_Estado = value; }
        }

        /// <summary> Armazena o conteúdo do campo Des_Cidade  </summary> 
        public string DescricaoCidade
        {
            get { return srtDes_Cidade; }
            set { srtDes_Cidade = value; }
        }

        /// <summary> Armazena o conteúdo do campo Id_Cidade  </summary> 
        public int Id_Cidade
        {
            get { return intId_Cidade; }
            set { intId_Cidade = value; }
        }

        #endregion

        public void FromIDataReader(System.Data.IDataReader pobjIDataReader)
        {
            if (pobjIDataReader == null) return;

            if (pobjIDataReader["Id_Estado"] != System.DBNull.Value)
                this.Id_Estado = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_Estado"));

            if (pobjIDataReader["Id_Cidade"] != System.DBNull.Value)
                this.Id_Cidade = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_Cidade"));

            if (pobjIDataReader["Des_Cidade"] != System.DBNull.Value)
                this.DescricaoCidade = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Des_Cidade"));
        }
    }
}
