using System;
using System.Collections.Generic;
using System.Text;

namespace SafWeb.Model.ListaVeiculos
{
    public class ListaVeiculos
    {
        #region Propriedades

        /// <summary> Armazena o conteúdo do campo Id_VeiculoLista  </summary> 
        public int IdLista { get; set; }

        /// <summary> Armazena o conteúdo do campo Des_VeiculoLista  </summary> 
        public string DescricaoLista { get; set; }

        /// <summary> Armazena o conteúdo do campo Id_Regional  </summary> 
        public int IdRegional { get; set; }

        /// <summary> Armazena o conteúdo do campo Id_Filial  </summary> 
        public int IdFilial { get; set; }

        /// <summary> Armazena o conteúdo do campo Des_Filial  </summary> 
        public string DescricaoFilial { get; set; }

        /// <summary> Armazena o conteúdo do campo Flg_Situacao  </summary> 
        public byte Situacao { get; set; }

        /// <summary> Armazena o conteúdo do campo Id_UsuarioInclusao </summary> 
        public int IdUsuario { get; set; }

        #endregion

        public void FromIDataReader(System.Data.IDataReader pobjIDataReader)
        {
            if (pobjIDataReader == null) return;

            if (pobjIDataReader["Id_VeiculoLista"] != System.DBNull.Value)
                this.IdLista = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_VeiculoLista"));

            if (pobjIDataReader["Des_VeiculoLista"] != System.DBNull.Value)
                this.DescricaoLista = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Des_VeiculoLista"));

            if (pobjIDataReader["Id_Regional"] != System.DBNull.Value)
                this.IdRegional = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_Regional"));

            if (pobjIDataReader["Id_Filial"] != System.DBNull.Value)
                this.IdFilial = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_Filial"));

            if (pobjIDataReader["Des_Filial"] != System.DBNull.Value)
                this.DescricaoFilial = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Des_Filial"));

            if (pobjIDataReader["Flg_Situacao"] != System.DBNull.Value)
                this.Situacao = pobjIDataReader.GetByte(pobjIDataReader.GetOrdinal("Flg_Situacao"));

            if (pobjIDataReader["Id_Usuario"] != System.DBNull.Value)
                this.IdUsuario = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_Usuario"));

        }
    }


}
