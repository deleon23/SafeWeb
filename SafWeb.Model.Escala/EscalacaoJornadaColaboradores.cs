using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace SafWeb.Model.Escala
{
    /// ----------------------------------------------------------------------------- 
    /// Project : 
    /// Class : EscalacaoJornadaColaboradores
    /// ----------------------------------------------------------------------------- 
    /// <summary>  
    /// Implementação da classe EscalacaoJornadaColaboradores
    /// </summary> 
    /// <history> 
    /// [cmarchi] created 15/1/2009 
    /// </history> 
    /// ----------------------------------------------------------------------------- 
    public class EscalacaoJornadaColaboradores
    {
        #region Variáveis

        private int intCodigoEscalacao;
        private int intCodigoJornada;
        private string strDesJornada;
        private int intCodigoColaborador;
        private string strDesColaborador;

        #endregion

        #region Propriedades

        /// <summary> Armazena o conteúdo do campo Id_Escalacao  </summary> 
        public int IdEscalacao
        {
            get { return this.intCodigoEscalacao; }
            set { this.intCodigoEscalacao = value; }
        }

        /// <summary> Armazena o conteúdo do campo Id_Jornada  </summary> 
        public int IdJornada
        {
            get { return this.intCodigoJornada; }
            set { this.intCodigoJornada = value; }
        }

        /// <summary> Armazena o conteúdo do campo Des_Jornada  </summary> 
        public string NomeJornada
        {
            get { return this.strDesJornada; }
            set { this.strDesJornada = value; }
        }

        /// <summary> Armazena o conteúdo do campo Id_Colaborador  </summary> 
        public int CodigoColaborador
        {
            get { return this.intCodigoColaborador; }
            set { this.intCodigoColaborador = value; }
        }

        /// <summary> Armazena o conteúdo do campo Nom_Colaborador  </summary> 
        public string NomeColaborador
        {
            get { return this.strDesColaborador; }
            set { this.strDesColaborador = value; }
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
        /// [cmarchi] created 15/1/2010 
        /// [cmarchi] modify 8/2/2010 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public void FromIDataReader(IDataReader pobjIDataReader)
        {
            if (pobjIDataReader == null) return;

            if (pobjIDataReader["Id_Escalacao"] != System.DBNull.Value)
                this.IdEscalacao = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_Escalacao"));

            if (pobjIDataReader["Id_Jornada"] != System.DBNull.Value)
                this.IdJornada = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_Jornada"));

            if (pobjIDataReader["Des_Jornada"] != System.DBNull.Value)
                this.NomeJornada = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Des_Jornada")); 

            if (pobjIDataReader["Id_Colaborador"] != System.DBNull.Value)
                this.CodigoColaborador = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_Colaborador"));

            if (pobjIDataReader["Nom_Colaborador"] != System.DBNull.Value)
                this.NomeColaborador = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Nom_Colaborador"));            
        }

        #endregion
    }
}
