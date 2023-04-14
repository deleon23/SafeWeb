using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace SafWeb.Model.Escala
{
    /// ----------------------------------------------------------------------------- 
    /// Project : 
    /// Class : EscalaLegadoRonda
    /// ----------------------------------------------------------------------------- 
    /// <summary>  
    /// Implementação da classe EscalaLegadoRonda
    /// </summary> 
    /// <history> 
    /// [haguiar] create 11/01/2012 14:16
    /// </history> 
    /// ----------------------------------------------------------------------------- 
    public class EscalaLegadoRonda
    {
        #region Variáveis

        private Int16 intCodEsc;
        private string strNomEsc;
        
        private Int64 intHorDSR;
        private Int64 intHorSem;
        private Int64 intHorMes;
        #endregion

        #region Propriedades

        /// <summary> Armazena o conteúdo do campo CodEsc</summary> 
        public Int16 CodEsc
        {
            get { return this.intCodEsc; }
            set { this.intCodEsc = value; }
        }

        /// <summary> Armazena o conteúdo do campo NomEsc</summary> 
        public string NomEsc
        {
            get { return this.strNomEsc; }
            set { this.strNomEsc = value; }
        }

        /// <summary> Armazena o conteúdo do campo HorDSR</summary> 
        public Int64 HorDSR
        {
            get { return this.intHorDSR; }
            set { this.intHorDSR = value; }
        }

        /// <summary> Armazena o conteúdo do campo HorSem</summary> 
        public Int64 HorSem
        {
            get { return this.intHorSem; }
            set { this.intHorSem = value; }
        }

        /// <summary> Armazena o conteúdo do campo HorMes</summary> 
        public Int64 HorMes
        {
            get { return this.intHorMes; }
            set { this.intHorMes = value; }
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
        /// [haguiar] created 11/01/2012 14:18
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public void FromIDataReader(IDataReader pobjIDataReader)
        {
            if (pobjIDataReader == null) return;

            if (pobjIDataReader["CodEsc"] != System.DBNull.Value)
                this.CodEsc = pobjIDataReader.GetInt16(pobjIDataReader.GetOrdinal("CodEsc"));

            if (pobjIDataReader["NomEsc"] != System.DBNull.Value)
                this.NomEsc = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("NomEsc"));

            if (pobjIDataReader["HorDSR"] != System.DBNull.Value)
                this.HorDSR = pobjIDataReader.GetInt64(pobjIDataReader.GetOrdinal("HorDSR"));

            if (pobjIDataReader["HorSem"] != System.DBNull.Value)
                this.HorSem = pobjIDataReader.GetInt64(pobjIDataReader.GetOrdinal("HorSem"));

            if (pobjIDataReader["HorMes"] != System.DBNull.Value)
                this.HorMes = pobjIDataReader.GetInt64(pobjIDataReader.GetOrdinal("HorMes"));
        }

        #endregion
    }
}