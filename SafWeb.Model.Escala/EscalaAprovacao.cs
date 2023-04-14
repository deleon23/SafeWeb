using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace SafWeb.Model.Escala
{
    /// ----------------------------------------------------------------------------- 
    /// Project : 
    /// Class : EscalaAprovacao
    /// ----------------------------------------------------------------------------- 
    /// <summary>  
    /// Implementação da classe EscalaAprovacao
    /// </summary> 
    /// <history> 
    /// [cmarchi] created 26/1/2010 
    /// </history> 
    /// ----------------------------------------------------------------------------- 
    public class EscalaAprovacao
    {
        #region Variáveis

        private int intIdEscalacao;
        private int intIdStatusSolicitacao;
        private int intIdUsuarioAprovador;
        private DateTime datDataAprovacao;
        private string strDesCricaoMotivoReprovacao;

        #endregion

        #region Propriedades

        /// <summary> Armazena o conteúdo do campo Id_Escalacao  </summary> 
        public int IdEscalacao
        {
            get { return this.intIdEscalacao; }
            set { this.intIdEscalacao = value; }
        }

        /// <summary> Armazena o conteúdo do campo Id_StatusSolicitacao  </summary> 
        public int IdStatusSolicitacao
        {
            get { return this.intIdStatusSolicitacao; }
            set { this.intIdStatusSolicitacao = value; }
        }

        /// <summary> Armazena o conteúdo do campo IdUsuarioAprovador  </summary> 
        public int IdUsuarioAprovador
        {
            get { return this.intIdUsuarioAprovador; }
            set { this.intIdUsuarioAprovador = value; }
        }

        /// <summary> Armazena o conteúdo do campo Dt_Aprovacao  </summary> 
        public DateTime DataAprovacao
        {
            get { return this.datDataAprovacao; }
            set { this.datDataAprovacao = value; }
        }

        /// <summary> Armazena o conteúdo do campo Des_MotivoReprovacao  </summary> 
        public string MotivoReprovacao
        {
            get { return this.strDesCricaoMotivoReprovacao; }
            set { this.strDesCricaoMotivoReprovacao = value; }
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
        /// [cmarchi] created 26/1/2010 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public void FromIDataReader(IDataReader pobjIDataReader)
        {
            //DataTable dttEscalacao = pobjIDataReader.GetSchemaTable();

            if (pobjIDataReader == null) return;

            if (pobjIDataReader["Id_Escalacao"] != System.DBNull.Value)
                this.IdEscalacao = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_Escalacao"));

            if (pobjIDataReader["Id_StatusSolicitacao"] != System.DBNull.Value)
                this.IdStatusSolicitacao = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_StatusSolicitacao"));

            if (pobjIDataReader["IdUsuarioAprovador"] != System.DBNull.Value)
                this.IdUsuarioAprovador = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("IdUsuarioAprovador"));

            if (pobjIDataReader["Dt_Aprovacao"] != System.DBNull.Value)
                this.DataAprovacao = pobjIDataReader.GetDateTime(pobjIDataReader.GetOrdinal("Dt_Aprovacao"));

            if (pobjIDataReader["Des_MotivoReprovacao"] != System.DBNull.Value)
                this.MotivoReprovacao = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Des_MotivoReprovacao"));

            ////verifica se há coluna  descrição Jornada
            //if (dttEscalacao.Rows[7]["ColumnName"].ToString() == "Des_Jornada")
            //{
            //    if (pobjIDataReader["Des_Jornada"] != System.DBNull.Value)
            //        this.DescricaoJornada = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Des_Jornada"));
            //}

            ////verifica as colunas de horários e datas dos colocaboradores
            //if (dttEscalacao.Rows[8]["ColumnName"].ToString() == "Id_Colaborador" && dttEscalacao.Rows[9]["ColumnName"].ToString() == "Nom_Colaborador"
            //    && dttEscalacao.Rows[10]["ColumnName"].ToString() == "Dt_Escalacao" && dttEscalacao.Rows[11]["ColumnName"].ToString() == "Flg_Compensado"
            //    && dttEscalacao.Rows[12]["ColumnName"].ToString() == "Flg_Folga" && dttEscalacao.Rows[13]["ColumnName"].ToString() == "Flg_Licenca")
            //{
            //    this.ObjEscalacaoColDatas = new EscalacaoColaboradoresData();
            //    this.ObjEscalacaoColDatas.FromIDataReader(pobjIDataReader);
            //}

        }

        #endregion
    }
}
