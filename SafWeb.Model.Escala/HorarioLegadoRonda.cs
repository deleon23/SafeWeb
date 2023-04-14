using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace SafWeb.Model.Escala
{
    /// ----------------------------------------------------------------------------- 
    /// Project : 
    /// Class : HorarioLegadoRonda
    /// ----------------------------------------------------------------------------- 
    /// <summary>  
    /// Implementação da classe HorarioLegadoRonda
    /// </summary> 
    /// <history> 
    /// [haguiar] create 12/01/2012 09:06
    /// </history> 
    /// ----------------------------------------------------------------------------- 
    public class HorarioLegadoRonda
    {
        #region Variáveis

        private Int16 intCodHor;
        private string strDesHor;

        #endregion

        #region Propriedades

        /// <summary> Armazena o conteúdo do campo CodHor</summary> 
        public Int16 CodHor
        {
            get { return this.intCodHor; }
            set { this.intCodHor = value; }
        }

        /// <summary> Armazena o conteúdo do campo DesHor</summary> 
        public string DesHor
        {
            get { return this.strDesHor; }
            set { this.strDesHor = value; }
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
        /// [haguiar] created 12/01/2012 09:09
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public void FromIDataReader(IDataReader pobjIDataReader)
        {
            if (pobjIDataReader == null) return;

            if (pobjIDataReader["CodHor"] != System.DBNull.Value)
                this.CodHor = pobjIDataReader.GetInt16(pobjIDataReader.GetOrdinal("CodHor"));

            if (pobjIDataReader["DesHor"] != System.DBNull.Value)
                this.DesHor = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("DesHor"));
        }

        #endregion
    }
}