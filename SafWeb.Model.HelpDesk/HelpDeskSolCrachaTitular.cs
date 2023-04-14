using System;
using System.Collections.Generic;
using System.Text;
using SafWeb.Util.Extension;

namespace SafWeb.Model.HelpDesk
{
    public class HelpDeskSolCrachaTitular
    {
        public int idSolicitacaoCrachaTitular { get; set; }

        public int idFilial { get; set; }

        public string Filial { get; set; }

        public int idSolicitante { get; set; }

        public string NomeSolicitante { get; set; }

        public int idColaborador { get; set; }

        public string NomeColaborador { get; set; }

        public string Areas { get; set; }

        public DateTime DataSolicitacao { get; set; }

        public int idTipoSolicitacao { get; set; }

        public string TipoSolicitacao { get; set; }

        public bool Editavel { get; set; }

        public void FromIDataReader(System.Data.IDataReader pobjIDataReader)
        {
            if (pobjIDataReader == null) return;

            if (pobjIDataReader.ColumnExists("Id_SolicitacaoCrachaTitular"))
                if (pobjIDataReader["Id_SolicitacaoCrachaTitular"] != System.DBNull.Value)
                    this.idSolicitacaoCrachaTitular = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_SolicitacaoCrachaTitular"));

            if (pobjIDataReader.ColumnExists("Id_Filial"))
                if (pobjIDataReader["Id_Filial"] != System.DBNull.Value)
                    this.idFilial = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_Filial"));

            if (pobjIDataReader.ColumnExists("Filial"))
                if (pobjIDataReader["Filial"] != System.DBNull.Value)
                    this.Filial = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Filial"));

            if (pobjIDataReader.ColumnExists("id_Solicitante"))
                if (pobjIDataReader["id_Solicitante"] != System.DBNull.Value)
                    this.idSolicitante = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("id_Solicitante"));

            if (pobjIDataReader.ColumnExists("Solicitante"))
                if (pobjIDataReader["Solicitante"] != System.DBNull.Value)
                    this.NomeSolicitante = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Solicitante"));

            if (pobjIDataReader.ColumnExists("Id_Colaborador"))
                if (pobjIDataReader["Id_Colaborador"] != System.DBNull.Value)
                    this.idColaborador = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_Colaborador"));

            if (pobjIDataReader.ColumnExists("Nom_Colaborador"))
                if (pobjIDataReader["Nom_Colaborador"] != System.DBNull.Value)
                    this.NomeColaborador = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Nom_Colaborador"));

            if (pobjIDataReader.ColumnExists("Areas"))
                if (pobjIDataReader["Areas"] != System.DBNull.Value)
                    this.Areas = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Areas"));

            if (pobjIDataReader.ColumnExists("Dt_Solicitacao"))
                if (pobjIDataReader["Dt_Solicitacao"] != System.DBNull.Value)
                    this.DataSolicitacao = pobjIDataReader.GetDateTime(pobjIDataReader.GetOrdinal("Dt_Solicitacao"));

            if (pobjIDataReader.ColumnExists("Id_TipoSolicitacao"))
                if (pobjIDataReader["Id_TipoSolicitacao"] != System.DBNull.Value)
                    this.idTipoSolicitacao = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_TipoSolicitacao"));

            if (pobjIDataReader.ColumnExists("Des_TipoSolicitacao"))
                if (pobjIDataReader["Des_TipoSolicitacao"] != System.DBNull.Value)
                    this.TipoSolicitacao = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Des_TipoSolicitacao"));

            if (pobjIDataReader.ColumnExists("Editavel"))
                if (pobjIDataReader["Editavel"] != System.DBNull.Value)
                    this.Editavel = pobjIDataReader.GetBoolean(pobjIDataReader.GetOrdinal("Editavel"));

        }
    }
}
