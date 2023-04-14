using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace SafWeb.Model.Escala
{
    /// ----------------------------------------------------------------------------- 
    /// Project : 
    /// Class : Jornada
    /// ----------------------------------------------------------------------------- 
    /// <summary>  
    /// Implementação da classe Jornada
    /// </summary> 
    /// <history> 
    /// [cmarchi] created 13/1/2009 
    /// </history> 
    /// ----------------------------------------------------------------------------- 
    [Serializable]
    public class Jornada
    {
        #region Variáveis

        private int intCodigo;
        private string strDescricao;

        #endregion

        #region Propriedades

        /// <summary> Armazena o conteúdo do campo Id_Jornada  </summary> 
        public int IdJornada
        {
            get { return this.intCodigo; }
            set { this.intCodigo = value; }
        }

        /// <summary> Armazena o conteúdo do campo Des_Jornada  </summary> 
        public string DescricaoJornada
        {
            get { return this.strDescricao; }
            set { this.strDescricao = value; }
        }

        #endregion

        #region FromIDataReader

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// Metodo que popula a Model com base em uma interface datareader
        /// </summary>
        /// <param name="pobjIDataReader">interface datareader</param>
        /// <value>IDataReader</value> 
        /// <history> 
        /// [cmarchi] created 30/12/2009 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public void FromIDataReader(IDataReader pobjIDataReader)
        {
            if (pobjIDataReader == null) return;

            if (pobjIDataReader["Id_Jornada"] != System.DBNull.Value)
                this.IdJornada = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_Jornada"));

            if (pobjIDataReader["Des_Jornada"] != System.DBNull.Value)
                this.DescricaoJornada = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Des_Jornada"));
        }

        #endregion
    }
}
