using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace SafWeb.Model.Escala
{
    /// ----------------------------------------------------------------------------- 
    /// Project : 
    /// Class : Periodicidade
    /// ----------------------------------------------------------------------------- 
    /// <summary>  
    /// Implementação da classe Periodicidade
    /// </summary> 
    /// <history> 
    /// [cmarchi] created 30/12/2009 
    /// </history> 
    /// ----------------------------------------------------------------------------- 
    public class Periodicidade
    {
        #region Variáveis

        private int intCodigo;
        private string strDescricao;

        #endregion

        #region Propriedades

        /// <summary> Armazena o conteúdo do campo Id_Periodicidade  </summary> 
        public int IdPeriodicidade
        {
            get { return this.intCodigo; }
            set { this.intCodigo = value; }
        }
                
        /// <summary> Armazena o conteúdo do campo Des_Periodicidade  </summary> 
        public string DescricaoPeriodicidade 
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

            if (pobjIDataReader["Id_Periodicidade"] != System.DBNull.Value)
                this.IdPeriodicidade = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_Periodicidade"));

            if (pobjIDataReader["Des_Periodicidade"] != System.DBNull.Value)
                this.DescricaoPeriodicidade = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Des_Periodicidade"));
        }

        #endregion
    }
}
