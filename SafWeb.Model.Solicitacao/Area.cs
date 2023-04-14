using System;
using System.Collections.Generic;
using System.Text;

namespace SafWeb.Model.Solicitacao
{
    public class Area
    {
        /// ----------------------------------------------------------------------------- 
        /// Project : SafWeb.Model.Solicitacao
        /// Class : Model.Solicitacao.Area
        /// ----------------------------------------------------------------------------- 
        /// <summary>  
        ///     Model da tabela de Area
        /// </summary> 
        /// <history> 
        ///     [mribeiro] 07/07/2009 Created 
        /// </history> 
        /// -----------------------------------------------------------------------------
        #region Variáveis

        private int intCodigo;
        private string strDescricao;
        private int intFilial;
        private byte bytAreaSeg;
        private int intGrupoColetores;

        #endregion

        #region Propriedades

        /// <summary> Armazena o conteúdo do campo Id_Area  </summary>
        public int Codigo
        {
            get { return intCodigo; }
            set { intCodigo = value; }
        }

        /// <summary> Armazena o conteúdo do campo Des_Area  </summary>
        public string Descricao
        {
            get { return strDescricao; }
            set { strDescricao = value; }
        }

        /// <summary> Armazena o conteúdo do campo Id_Filial  </summary>
        public int IdFilial
        {
            get { return intFilial; }
            set { intFilial = value; }
        }

        /// <summary> Armazena o conteúdo do campo Flg_AreaSeg  </summary>
        public byte AreaSeguranca
        {
            get { return bytAreaSeg; }
            set { bytAreaSeg = value; }
        }

        /// <summary> Armazena o conteúdo do campo Id_GrupoColetores  </summary>
        public int IdGrupoColetores
        {
            get { return intGrupoColetores; }
            set { intGrupoColetores = value; }
        }

        #endregion

        public void FromIDataReader(System.Data.IDataReader pobjIDataReader)
        {
            if (pobjIDataReader == null) return;

            if (pobjIDataReader["Id_Area"] != System.DBNull.Value)
                this.Codigo = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_Area"));

            if (pobjIDataReader["Des_Area"] != System.DBNull.Value)
                this.Descricao = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Des_Area"));

            if (pobjIDataReader["Id_Filial"] != System.DBNull.Value)
                this.IdFilial = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_Filial"));

            if (pobjIDataReader["Flg_AreaSeg"] != System.DBNull.Value)
                this.AreaSeguranca = pobjIDataReader.GetByte(pobjIDataReader.GetOrdinal("Flg_AreaSeg"));

            if (pobjIDataReader["Id_GrupoColetores"] != System.DBNull.Value)
                this.IdGrupoColetores = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_GrupoColetores"));
        }
    }
}
