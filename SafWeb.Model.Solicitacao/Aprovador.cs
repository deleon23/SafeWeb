using System;
using System.Collections.Generic;
using System.Text;

namespace SafWeb.Model.Solicitacao
{
    public class Aprovador
    {
        /// ----------------------------------------------------------------------------- 
        /// Project : SafWeb.Model.Solicitacao
        /// Class : Model.Solicitacao.Aprovador
        /// ----------------------------------------------------------------------------- 
        /// <summary>  
        ///     Model da tabela de AprovadorFilial
        /// </summary> 
        /// <history> 
        ///     [mribeiro] 15/07/2009 Created 
        /// </history> 
        /// -----------------------------------------------------------------------------
        #region Variáveis

        private int intIdUsuario;
        private string strNomeUsuario;

        #endregion

        #region Propriedades

        public int IdUsuario
        {
            get { return intIdUsuario; }
            set { intIdUsuario = value; }
        }

        public string NomeUsuario
        {
            get { return strNomeUsuario; }
            set { strNomeUsuario = value; }
        }

        #endregion

        public void FromIDataReader(System.Data.IDataReader pobjIDataReader)
        {
            if (pobjIDataReader == null) return;

            if (pobjIDataReader["Id_usuario"] != System.DBNull.Value)
                this.IdUsuario = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_usuario"));

            if (pobjIDataReader["USU_C_NOME"] != System.DBNull.Value)
                this.NomeUsuario = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("USU_C_NOME"));
        }
    }
}
