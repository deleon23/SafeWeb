using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace SafWeb.Model.Escala
{
    /// ----------------------------------------------------------------------------- 
    /// Project : 
    /// Class : HorarioLegado
    /// ----------------------------------------------------------------------------- 
    /// <summary>  
    /// Implementação da classe HorarioLegado
    /// </summary> 
    /// <history> 
    /// [haguiar] create 06/01/2012 11:07
    /// </history> 
    /// ----------------------------------------------------------------------------- 
    public class HorarioLegado
    {
        #region Variáveis

        private int intIdEscala;
        private int intIdJornada;
        private string strDesEscala;
        private string strDesJornada;
        private string strHrEntrada;
        private string strDurRefeicao;
        
        private int intCodLegado;
        private bool blnFlgSituacao;

        #endregion

        #region Propriedades

        /// <summary> Armazena o conteúdo do campo Id_Escala</summary> 
        public int IdEscala
        {
            get { return this.intIdEscala; }
            set { this.intIdEscala = value; }
        }

        /// <summary> Armazena o conteúdo do campo Id_Jornada</summary> 
        public int IdJornada
        {
            get { return this.intIdJornada; }
            set { this.intIdJornada = value; }
        }

        /// <summary> Armazena o conteúdo do campo Des_Escala</summary> 
        public string DesEscala
        {
            get { return this.strDesEscala; }
            set { this.strDesEscala = value; }
        }

        /// <summary> Armazena o conteúdo do campo Des_Jornada</summary> 
        public string DesJornada
        {
            get { return this.strDesJornada; }
            set { this.strDesJornada = value; }
        }

        /// <summary> Armazena o conteúdo do campo Hr_Entrada</summary> 
        public string HrEntrada
        {
            get { return this.strHrEntrada; }
            set { this.strHrEntrada = value; }
        }

        /// <summary> Armazena o conteúdo do campo Dur_Refeicao</summary> 
        public string DurRefeicao
        {
            get { return this.strDurRefeicao; }
            set { this.strDurRefeicao = value; }
        }
        /// <summary> Armazena o conteúdo do campo Cod_Legado  </summary> 
        public int CodLegado
        {
            get { return this.intCodLegado; }
            set { this.intCodLegado = value; }
        }
        
        /// <summary> Armazena o conteúdo do campo Flg_Situacao </summary> 
        public bool FlgSituacao
        {
            get { return this.blnFlgSituacao; }
            set { this.blnFlgSituacao = value; }
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
        /// [haguiar] created 06/01/2012 11:13
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public void FromIDataReader(IDataReader pobjIDataReader)
        {
            if (pobjIDataReader == null) return;

            if (pobjIDataReader["Id_Escala"] != System.DBNull.Value)
                this.IdEscala = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_Escala"));

            if (pobjIDataReader["Id_Jornada"] != System.DBNull.Value)
                this.IdJornada = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_Jornada"));

            if (pobjIDataReader["Des_Escala"] != System.DBNull.Value)
                this.DesEscala = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Des_Escala"));

            if (pobjIDataReader["Des_Jornada"] != System.DBNull.Value)
                this.DesJornada = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Des_Jornada"));

            if (pobjIDataReader["Hr_Entrada"] != System.DBNull.Value)
                this.HrEntrada = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Hr_Entrada"));

            if (pobjIDataReader["Dur_Refeicao"] != System.DBNull.Value)
                this.DurRefeicao = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Dur_Refeicao"));
            
            if (pobjIDataReader["Cod_Legado"] != System.DBNull.Value)
                this.CodLegado = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Cod_Legado"));

            if (pobjIDataReader["Flg_Situacao"] != System.DBNull.Value)
                this.FlgSituacao = pobjIDataReader.GetBoolean(pobjIDataReader.GetOrdinal("Flg_Situacao"));
        }

        #endregion
    }
}
