using System;
using System.Collections.Generic;
using System.Text;

namespace SafWeb.Model.Empresa
{
    public class Empresa
    {
        /// ----------------------------------------------------------------------------- 
        /// Project : SafWeb.Model.Empresa
        /// Class : Model.Empresa.Empresa
        /// ----------------------------------------------------------------------------- 
        /// <summary>  
        ///     Model da tabela de Empresas
        /// </summary> 
        /// <history> 
        ///     [mribeiro] 24/06/2009 Created 
        /// </history> 
        /// -----------------------------------------------------------------------------
        #region Variáveis

        private int intCodigo;
        private string strNome;
        private string strCodigo;

        #endregion

        #region Propriedades

        /// <summary> Armazena o conteúdo do campo Id_Empresa  </summary> 
        public int IdEmpresa
        {
            get { return intCodigo; }
            set { intCodigo = value; }
        }

        /// <summary> Armazena o conteúdo do campo Des_Empresa  </summary> 
        public string DescricaoEmpresa
        {
            get { return strNome; }
            set { strNome = value; }
        }

        /// <summary> Armazena o conteúdo do campo Cod_Empresa  </summary> 
        public string CodigoEmpresa
        {
            get { return strCodigo; }
            set { strCodigo = value; }
        }

        #endregion

        public void FromIDataReader(System.Data.IDataReader pobjIDataReader)
        {
            if (pobjIDataReader == null) return;

            if (pobjIDataReader["Id_Empresa"] != System.DBNull.Value)
                this.IdEmpresa = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_Empresa"));

            if (pobjIDataReader["Des_Empresa"] != System.DBNull.Value)
                this.DescricaoEmpresa = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Des_Empresa"));

            if (pobjIDataReader["Cod_Empresa"] != System.DBNull.Value)
                this.CodigoEmpresa = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Cod_Empresa"));

        }
    }
}
