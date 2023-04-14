using System;
using System.Collections.Generic;
using System.Text;

namespace SafWeb.Model.Solicitacao
{
    public class MotivoSolicitacao
    {
        /// ----------------------------------------------------------------------------- 
        /// Project : SafWeb.Model.Solicitacao
        /// Class : Model.Solicitacao.MotivoSolicitacao
        /// ----------------------------------------------------------------------------- 
        /// <summary>  
        ///     Model da tabela de MotivoSolicitacao
        /// </summary> 
        /// <history> 
        ///     [mribeiro] 07/07/2009 Created 
        /// </history> 
        /// -----------------------------------------------------------------------------
        #region Variáveis

        private int intCodigo;
        private string strDescricao;

        #endregion

        #region Propriedades

        /// <summary> Armazena o conteúdo do campo Id_MotivoVisita  </summary>
        public int Codigo
        {
            get { return intCodigo; }
            set { intCodigo = value; }
        }

        /// <summary> Armazena o conteúdo do campo Des_MotivoVisita  </summary>
        public string Descricao
        {
            get { return strDescricao; }
            set { strDescricao = value; }
        }

        #endregion

        public void FromIDataReader(System.Data.IDataReader pobjIDataReader)
        {
            if (pobjIDataReader == null) return;

            if (pobjIDataReader["Id_MotivoVisita"] != System.DBNull.Value)
                this.Codigo = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_MotivoVisita"));

            if (pobjIDataReader["Des_MotivoVisita"] != System.DBNull.Value)
                this.Descricao = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Des_MotivoVisita"));
        }
    }
}
