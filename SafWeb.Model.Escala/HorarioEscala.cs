using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using SafWeb.Util.Extension;

namespace SafWeb.Model.Escala
{
    /// ----------------------------------------------------------------------------- 
    /// Project : 
    /// Class : HorarioEscala
    /// ----------------------------------------------------------------------------- 
    /// <summary>  
    /// Implementação da classe HorarioEscala
    /// </summary> 
    /// <history> 
    /// [cmarchi] created 30/12/2009 
    /// [haguiar] modify 02/01/2012 14:14
    /// incluir propriedade cod_legado
    /// </history> 
    /// ----------------------------------------------------------------------------- 
    [Serializable]
    public class HorarioEscala
    {
        #region Variáveis

        private int intCodigoEscalaDepartamento;
        private string strCodigo;
        private bool blnSituacao;
        private int intCodLegado;

        #endregion

        #region Propriedades

        /// <summary> Armazena o conteúdo do campo Cod_Legado  </summary> 
        public int CodLegado
        {
            get { return this.intCodLegado; }
            set { this.intCodLegado = value; }
        }

        /// <summary> Armazena o conteúdo do campo Id_EscalaDpto  </summary> 
        public int IdEscalaDepartamento
        {
            get { return this.intCodigoEscalaDepartamento; }
            set { this.intCodigoEscalaDepartamento = value; }
        }

        /// <summary> Armazena o conteúdo do campo Hr_EscalaEntrada  </summary> 
        public string IdHorario
        {
            get { return this.strCodigo; }
            set { this.strCodigo = value; }
        }

        /// <summary> Armazena o conteúdo do campo Flg_Situacao </summary> 
        public bool Situacao
        {
            get { return this.blnSituacao; }
            set { this.blnSituacao = value; }
        }

        public int IdJornada { get; set; }

        public int IdEscala { get; set; }

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
        /// [haguiar] modify 02/01/2012 14:14
        /// incluir propriedade cod_legado
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public void FromIDataReader(IDataReader pobjIDataReader)
        {
            if (pobjIDataReader == null) return;

            if (pobjIDataReader["Id_EscalaDpto"] != System.DBNull.Value)
                this.IdEscalaDepartamento = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_EscalaDpto"));

            if (pobjIDataReader["Hr_EscalaEntrada"] != System.DBNull.Value)
                this.IdHorario = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Hr_EscalaEntrada"));

            if (pobjIDataReader["Flg_Situacao"] != System.DBNull.Value)
                this.Situacao = pobjIDataReader.GetBoolean(pobjIDataReader.GetOrdinal("Flg_Situacao"));

            if (pobjIDataReader["Cod_Legado"] != System.DBNull.Value)
                this.CodLegado = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Cod_Legado"));

            if (pobjIDataReader.ColumnExists("Id_Jornada"))
                if (pobjIDataReader["Id_Jornada"] != System.DBNull.Value)
                    this.IdJornada = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_Jornada"));

            if (pobjIDataReader.ColumnExists("Id_Escala"))
                if (pobjIDataReader["Id_Escala"] != System.DBNull.Value)
                    this.IdEscala = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_Escala"));

        }

        #endregion

    }
}
