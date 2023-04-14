using System;
using System.Collections.Generic;
using System.Text;
using SafWeb.Util.Extension;

namespace SafWeb.Model.Veiculo
{
    [Serializable]
    public class Veiculo
    {
        /// ----------------------------------------------------------------------------- 
        /// Project : SafWeb.Model.Veiculo
        /// Class : Model.Veiculo.Veiculo
        /// ----------------------------------------------------------------------------- 
        /// <summary>  
        ///     Model da tabela de Veiculo
        /// </summary> 
        /// <history> 
        ///     [mribeiro] 10/07/2009 Created 
        /// </history> 
        /// -----------------------------------------------------------------------------
        #region Variáveis

        private int intVeiculo;
        private int intIdEmpresa;
        private string strPlaca;
        private string strPrefixo;
        private int intIdModelo;
        private int intIdUsuario;
        private byte bytSituacao;
        private int intIdEstado;
        private string strDescricaoEmpresa;
        private string strDescricaoEstado;

        #endregion

        #region Propriedades

        /// <summary> Armazena o conteúdo do campo Id_Veiculo</summary>
        public int Codigo
        {
            get { return intVeiculo; }
            set { intVeiculo = value; }
        }

        /// <summary> Armazena o conteúdo do campo Id_Empresa  </summary>
        public int IdEmpresa
        {
            get { return intIdEmpresa; }
            set { intIdEmpresa = value; }
        }

        /// <summary> Armazena o conteúdo do campo Des_Placa  </summary>
        public string DescricaoPlaca
        {
            get { return strPlaca; }
            set { strPlaca = value; }
        }

        /// <summary> Armazena o conteúdo do campo Des_Prefixo  </summary>
        public string Prefixo
        {
            get { return strPrefixo; }
            set { strPrefixo = value; }
        }

        /// <summary> Armazena o conteúdo do campo Id_VeiculoModelo  </summary>
        public int IdModelo
        {
            get { return intIdModelo; }
            set { intIdModelo = value; }
        }

        /// <summary> Armazena o conteúdo do campo Id_UsuarioInclusao/Id_UsuarioAlteracao  </summary>
        public int IdUsuario
        {
            get { return intIdUsuario; }
            set { intIdUsuario = value; }
        }

        /// <summary> Armazena o conteúdo do campo Flg_Situacao  </summary>
        public byte Situacao
        {
            get { return bytSituacao; }
            set { bytSituacao = value; }
        }

        /// <summary> Armazena o conteúdo do campo Id_Estado  </summary>
        public int IdEstado
        {
            get { return intIdEstado; }
            set { intIdEstado = value; }
        }

        /// <summary> Armazena o conteúdo do campo Des_Empresa  </summary>
        public string DescricaoEmpresa
        {
            get { return strDescricaoEmpresa; }
            set { strDescricaoEmpresa = value; }
        }

        /// <summary> Armazena o conteúdo do campo Des_Estado  </summary>
        public string DescricaoEstado
        {
            get { return strDescricaoEstado; }
            set { strDescricaoEstado = value; }
        }

        #endregion

        public void FromIDataReader(System.Data.IDataReader pobjIDataReader)
        {
            if (pobjIDataReader == null) return;

            if (pobjIDataReader["Id_Veiculo"] != System.DBNull.Value)
                this.Codigo = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_Veiculo"));

            if (pobjIDataReader["Id_Empresa"] != System.DBNull.Value)
                this.IdEmpresa = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_Empresa"));

            if (pobjIDataReader["Des_Placa"] != System.DBNull.Value)
                this.DescricaoPlaca = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Des_Placa"));

            if (pobjIDataReader["Id_VeiculoModelo"] != System.DBNull.Value)
                this.IdModelo = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_VeiculoModelo"));

            if (pobjIDataReader["Id_Usuario"] != System.DBNull.Value)
                this.IdUsuario = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_Usuario"));

            if (pobjIDataReader["Flg_Situacao"] != System.DBNull.Value)
                this.Situacao = pobjIDataReader.GetByte(pobjIDataReader.GetOrdinal("Flg_Situacao"));

            if (pobjIDataReader["Id_Estado"] != System.DBNull.Value)
                this.IdEstado = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_Estado"));



            if (pobjIDataReader.ColumnExists("Des_Empresa"))
                if (pobjIDataReader["Des_Empresa"] != System.DBNull.Value)
                    this.DescricaoEmpresa = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Des_Empresa"));


            if (pobjIDataReader.ColumnExists("Des_Estado"))
                if (pobjIDataReader["Des_Estado"] != System.DBNull.Value)
                    this.DescricaoEstado = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Des_Estado"));

        }
    }
}
