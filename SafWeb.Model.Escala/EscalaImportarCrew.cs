using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace SafWeb.Model.Escala
{
    /// ----------------------------------------------------------------------------- 
    /// Project : 
    /// Class : EscalaImportarCrew
    /// ----------------------------------------------------------------------------- 
    /// <summary>  
    /// Implementação da classe EscalaImportarCrew
    /// </summary> 
    /// <history> 
    /// [haguiar SDM 9004] created 10/08/2011 15:56
    /// </history> 
    /// ----------------------------------------------------------------------------- 
    public class EscalaImportarCrew
    {
        #region Variáveis

        private int intIdEscalacao;

        private DateTime datDataInicioPeriodo;
        private DateTime datDataFinalPeriodo;
        private string strPeriodo;

        private int intCodigoTipoSolicitacao;
        private string strDesTipoSolicitacao;

        private int intIdStatusSolicitacao;
        private string strStatusAprovacao;

        private int intCodigoUsuarioSolicitante;
        private string strUsuarioSolicitante;
        
        private EscalacaoColaboradoresData objEscalacaoColaboradoresData;

        #endregion

        #region Propriedades
        /// <summary> Armazena o conteúdo do campo IdEscalacao  </summary> 
        public int IdEscalacao
        {
            get { return this.intIdEscalacao; }
            set { this.intIdEscalacao = value; }
        }

        /// <summary> Armazena o conteúdo do campo Periodo  </summary> 
        public string Periodo
        {
            get { return this.strPeriodo; }
            set { this.strPeriodo = value; }
        }

        /// <summary> Armazena o conteúdo do campo Dt_InicioPeriodo  </summary> 
        public DateTime DataInicioPeriodo
        {
            get { return this.datDataInicioPeriodo; }
            set { this.datDataInicioPeriodo = value; }
        }

        /// <summary> Armazena o conteúdo do campo Dt_FinalPeriodo  </summary> 
        public DateTime DataFinalPeriodo
        {
            get { return this.datDataFinalPeriodo; }
            set { this.datDataFinalPeriodo = value; }
        }

        /// <summary> Armazena o conteúdo do campo Id_TipoSolicitacao  </summary> 
        public int IdTipoSolicitacao
        {
            get { return this.intCodigoTipoSolicitacao; }
            set { this.intCodigoTipoSolicitacao = value; }
        }

        /// <summary> Armazena o conteúdo do campo Des_TipoSolicitacao  </summary> 
        public string DescricaoTipoSolicitacao
        {
            get { return this.strDesTipoSolicitacao; }
            set { this.strDesTipoSolicitacao = value; }
        }
        
        /// <summary> Armazena o conteúdo dos campos Id_UsuarioSolicitante  </summary> 
        public int IdUsuarioSolicitante
        {
            get { return this.intCodigoUsuarioSolicitante; }
            set { this.intCodigoUsuarioSolicitante = value; }
        }

        /// <summary> Armazena o conteúdo do campo Nom_UsuarioSolicitante  </summary> 
        public string NomeUsuarioSolicitante
        {
            get { return this.strUsuarioSolicitante; }
            set { this.strUsuarioSolicitante = value; }
        }

        /// <summary> Armazena o conteúdo do campo EscalacaoColaboradoresData  </summary> 
        public EscalacaoColaboradoresData ObjEscalacaoColDatas
        {
            get { return this.objEscalacaoColaboradoresData; }
            set { this.objEscalacaoColaboradoresData = value; }
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

            if (pobjIDataReader["Id_TipoSolicitacao"] != System.DBNull.Value)
                this.IdTipoSolicitacao = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_TipoSolicitacao"));

            if (pobjIDataReader["Id_UsuarioSolicitante"] != System.DBNull.Value)
                this.IdUsuarioSolicitante = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_UsuarioSolicitante"));

            if (pobjIDataReader["Periodo"] != System.DBNull.Value)
                this.Periodo = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Periodo"));

            if (pobjIDataReader["Id_Escalacao"] != System.DBNull.Value)
                this.IdEscalacao = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_Escalacao"));

            if (pobjIDataReader["Id_TipoSolicitacao"] != System.DBNull.Value)
                this.IdTipoSolicitacao = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_TipoSolicitacao"));

            if (pobjIDataReader["Des_TipoSolicitacao"] != System.DBNull.Value)
                this.DescricaoTipoSolicitacao = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Des_TipoSolicitacao"));

            if (pobjIDataReader["Id_UsuarioSolicitante"] != System.DBNull.Value)
                this.IdUsuarioSolicitante = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_UsuarioSolicitante"));

            if (pobjIDataReader["Nom_UsuarioSolicitante"] != System.DBNull.Value)
                this.NomeUsuarioSolicitante = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Nom_UsuarioSolicitante"));
        }

        #endregion
    }
}