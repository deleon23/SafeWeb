using System;
using System.Collections.Generic;
using System.Text;
using SafWeb.Util.Extension;

namespace SafWeb.Model.Colaborador
{
    [Serializable]
    public class Colaborador
    {
        /// ----------------------------------------------------------------------------- 
        /// Project : SafWeb.Model.Colaborador
        /// Class : Model.Colaborador.Colaborador
        /// ----------------------------------------------------------------------------- 
        /// <summary>  
        ///     Model da tabela de Colaboradores
        /// </summary> 
        /// <history> 
        ///     [mribeiro] 29/06/2009 Created 
        /// </history> 
        /// -----------------------------------------------------------------------------
        #region Variáveis

        private int intIdColaborador;
        private int intTipo;
        private string strCodigo;
        private string strNome;
        private byte bytSituacao;
        private int intIdEmpresa;
        private string strEmpresa;
        private DateTime datUltimaAtualizacao;
        private string strNumDocumento;
        private string strDescricaoDocumento;
        private int intTipoDocumento;
        private byte[] imagem;
        private DateTime dataFoto;
        private string des_Funcao;

        #endregion

        #region Propriedades

        /// <summary> Armazena o conteúdo do campo Id_Colaborador  </summary> 
        public int IdColaborador
        {
            get { return intIdColaborador; }
            set { intIdColaborador = value; }
        }

        /// <summary> Armazena o conteúdo do campo Id_TipoColaborador  </summary> 
        public int IdTipoColaborador
        {
            get { return intTipo; }
            set { intTipo = value; }
        }

        /// <summary> Armazena o conteúdo do campo Cod_Colaborador  </summary> 
        public string CodigoColaborador
        {
            get { return strCodigo; }
            set { strCodigo = value; }
        }

        /// <summary> Armazena o conteúdo do campo Nom_Colaborador  </summary> 
        public string NomeColaborador
        {
            get { return strNome; }
            set { strNome = value; }
        }

        /// <summary> Armazena o conteúdo do campo Flg_SitColaborador  </summary> 
        public byte SituacaoColaborador
        {
            get { return bytSituacao; }
            set { bytSituacao = value; }
        }

        /// <summary> Armazena o conteúdo do campo Id_Empresa  </summary> 
        public int IdEmpresa
        {
            get { return intIdEmpresa; }
            set { intIdEmpresa = value; }
        }

        /// <summary> Armazena o conteúdo do campo Des_Empresa  </summary> 
        public string DescricaoEmpresa
        {
            get { return strEmpresa; }
            set { strEmpresa = value; }
        }

        /// <summary> Armazena o conteúdo do campo Dt_UltimaAtualizacao  </summary> 
        public DateTime UltimaAtualizacao
        {
            get { return datUltimaAtualizacao; }
            set { datUltimaAtualizacao = value; }
        }

        /// <summary> Armazena o conteúdo do campo Id_DocumentoTipo  </summary> 
        public int IdTipoDocumento
        {
            get { return intTipoDocumento; }
            set { intTipoDocumento = value; }
        }

        /// <summary> Armazena o conteúdo do campo Des_DocumentoTipo  </summary> 
        public string TipoDocumento
        {
            get { return strDescricaoDocumento; }
            set { strDescricaoDocumento = value; }
        }

        /// <summary> Armazena o conteúdo do campo Num_Documento  </summary> 
        public string NumeroDocumento
        {
            get { return strNumDocumento; }
            set { strNumDocumento = value; }
        }

        /// <summary> Armazena o conteúdo do campo Flg_Situacao  </summary> 
        public bool Situacao { get; set; }

        public byte[] Imagem
        {
            get { return imagem; }
            set { imagem = value; }
        }

        public DateTime DataFoto 
        {
            get { return dataFoto; }
            set { dataFoto = value; }
        }

        public string Des_Funcao
        {
            get { return des_Funcao; }
            set { des_Funcao = value; }
        }
        

        #endregion

        public void FromIDataReader(System.Data.IDataReader pobjIDataReader)
        {
            if (pobjIDataReader == null) return;

            if (pobjIDataReader["Id_Colaborador"] != System.DBNull.Value)
                this.IdColaborador = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_Colaborador"));

            if (pobjIDataReader["Id_TipoColaborador"] != System.DBNull.Value)
                this.IdTipoColaborador = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_TipoColaborador"));

            if (pobjIDataReader["Cod_Colaborador"] != System.DBNull.Value)
                this.CodigoColaborador = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Cod_Colaborador"));

            if (pobjIDataReader["Nom_Colaborador"] != System.DBNull.Value)
                this.NomeColaborador = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Nom_Colaborador"));

            if (pobjIDataReader["Flg_SitColaborador"] != System.DBNull.Value)
                this.SituacaoColaborador = pobjIDataReader.GetByte(pobjIDataReader.GetOrdinal("Flg_SitColaborador"));

            if (pobjIDataReader["Id_Empresa"] != System.DBNull.Value)
                this.intIdEmpresa = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_Empresa"));

            if (pobjIDataReader["Des_Empresa"] != System.DBNull.Value)
                this.DescricaoEmpresa = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Des_Empresa"));

            if (pobjIDataReader["Dt_UltimaAtualizacao"] != System.DBNull.Value)
                this.UltimaAtualizacao = pobjIDataReader.GetDateTime(pobjIDataReader.GetOrdinal("Dt_UltimaAtualizacao"));

            if (pobjIDataReader["Id_DocumentoTipo"] != System.DBNull.Value)
                this.IdTipoDocumento = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_DocumentoTipo"));

            if (pobjIDataReader["Des_DocumentoTipo"] != System.DBNull.Value)
                this.TipoDocumento = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Des_DocumentoTipo"));

            if (pobjIDataReader["Num_Documento"] != System.DBNull.Value)
                this.NumeroDocumento = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Num_Documento"));

            if (pobjIDataReader.ColumnExists("Flg_Situacao"))
                if (pobjIDataReader["Flg_Situacao"] != System.DBNull.Value)
                    this.Situacao = pobjIDataReader.GetBoolean(pobjIDataReader.GetOrdinal("Flg_Situacao"));
        }
    }
}
