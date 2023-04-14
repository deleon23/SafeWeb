using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace SafWeb.Model.Escala
{
    /// ----------------------------------------------------------------------------- 
    /// Project : 
    /// Class : EscalaColaboradoresCrew
    /// ----------------------------------------------------------------------------- 
    /// <summary>  
    /// Implementação da classe EscalaColaboradoresCrew
    /// </summary> 
    /// <history> 
    /// [haguiar] created 22/03/2012 09:30
    /// </history> 
    /// ----------------------------------------------------------------------------- 
    public class EscalaColaboradoresCrew
    {
        #region Variáveis

        private int intCOD_ESCALA;
        private int intCOD_FUNC;
        private int intCOD_JOR;
        private string strDES_HOR;

        #endregion

        #region Propriedades

        /// <summary> Armazena o conteúdo do campo COD_ESCALA</summary> 
        public int COD_ESCALA
        {
            get { return this.intCOD_ESCALA; }
            set { this.intCOD_ESCALA = value; }
        }
        
        /// <summary> Armazena o conteúdo do campo CODCHEFE  </summary> 
        public int COD_FUNC
        {
            get { return this.intCOD_FUNC; }
            set { this.intCOD_FUNC = value; }
        }

        /// <summary> Armazena o conteúdo do campo COD_JOR</summary> 
        public int COD_JOR
        {
            get { return this.intCOD_JOR; }
            set { this.intCOD_JOR = value; }
        }

        /// <summary> Armazena o conteúdo do campo DES_HOR</summary> 
        public string DES_HOR
        {
            get { return this.strDES_HOR; }
            set { this.strDES_HOR = value; }
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
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public void FromIDataReader(IDataReader pobjIDataReader)
        {
            DataTable dttEscalacao = pobjIDataReader.GetSchemaTable();

            if (pobjIDataReader == null) return;

            if (pobjIDataReader["COD_ESCALA"] != System.DBNull.Value)
                this.COD_ESCALA = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("COD_ESCALA"));

            if (pobjIDataReader["COD_FUNC"] != System.DBNull.Value)
                this.COD_FUNC = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("COD_FUNC"));

            if (pobjIDataReader["COD_JOR"] != System.DBNull.Value)
                this.COD_JOR = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("COD_JOR"));

            if (pobjIDataReader["DES_HOR"] != System.DBNull.Value)
                this.DES_HOR = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("DES_HOR"));
        }

        #endregion
    }
}