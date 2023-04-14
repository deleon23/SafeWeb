using System;
using System.Collections.Generic;
using System.Text;

namespace SafWeb.Model.Relatorio
{
    public class RelatorioEntradas
    {
        /// ----------------------------------------------------------------------------- 
        /// Project : SafWeb.Model.Relatorio
        /// Class : Model.Relatorio.RelatorioEntradas
        /// ----------------------------------------------------------------------------- 
        /// <summary>  
        ///     Model da tela de Relatorio de Entradas
        /// </summary> 
        /// <history> 
        ///     [mribeiro] 27/07/2009 Created 
        /// </history> 
        /// -----------------------------------------------------------------------------
        #region Variáveis

        private string strEmpresa;
        private string strFilial;
        private string strArea;
        private string strTipoVisitante;
        private DateTime datInicio;
        private DateTime datFim;
        private string strVisitado;
        private string strVisitante;
        private string strCracha;
        private string strPlaca;
        private string strDataEntrada;
        private string strDataSaida;
        private string strDocumento;

        #endregion

        #region Propriedades

        /// <summary> Armazena o conteúdo do campo Des_Empresa</summary>
        public string Empresa
        {
            get { return strEmpresa; }
            set { strEmpresa = value; }
        }

        /// <summary> Armazena o conteúdo do campo Alias_Filial  </summary>
        public string Filial
        {
            get { return strFilial; }
            set { strFilial = value; }
        }

        /// <summary> Armazena o conteúdo do campo Des_Area  </summary>
        public string Area
        {
            get { return strArea; }
            set { strArea = value; }
        }

        /// <summary> Armazena o conteúdo do campo Des_TipoColaborador  </summary>
        public string TipoVisitante
        {
            get { return strTipoVisitante; }
            set { strTipoVisitante = value; }
        }

        /// <summary> Armazena o conteúdo do campo Dt_Entrada  </summary>
        public DateTime DataInicio
        {
            get { return datInicio; }
            set { datInicio = value; }
        }

        /// <summary> Armazena o conteúdo do campo Dt_Saida  </summary>
        public DateTime DataFim
        {
            get { return datFim; }
            set { datFim = value; }
        }

        /// <summary> Armazena o conteúdo do campo Nom_Visitante  </summary>
        public string Visitante
        {
            get { return strVisitante; }
            set { strVisitante = value; }
        }

        /// <summary> Armazena o conteúdo do campo Nom_Visitado  </summary>
        public string Visitado
        {
            get { return strVisitado; }
            set { strVisitado = value; }
        }

        /// <summary> Armazena o conteúdo do campo Num_Cracha  </summary>
        public string Cracha
        {
            get { return strCracha; }
            set { strCracha = value; }
        }

        /// <summary> Armazena o conteúdo do campo Des_Placa  </summary>
        public string Veiculo
        {
            get { return strPlaca; }
            set { strPlaca = value; }
        }

        /// <summary> Armazena o conteúdo do campo Dt_Entrada  </summary>
        public string DataEntrada
        {
            get { return strDataEntrada; }
            set { strDataEntrada = value; }
        }

        /// <summary> Armazena o conteúdo do campo Dt_Saida  </summary>
        public string DataSaida
        {
            get { return strDataSaida; }
            set { strDataSaida = value; }
        }

        /// <summary> Armazena o conteúdo do campo Num_Documento  </summary>
        public string Documento
        {
            get { return strDocumento; }
            set { strDocumento = value; }
        }

        #endregion

        public void FromIDataReader(System.Data.IDataReader pobjIDataReader)
        {
            if (pobjIDataReader == null) return;

            if (pobjIDataReader["Des_Empresa"] != System.DBNull.Value)
                this.Empresa = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Des_Empresa"));

            if (pobjIDataReader["Alias_Filial"] != System.DBNull.Value)
                this.Filial = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Alias_Filial"));

            if (pobjIDataReader["Des_Area"] != System.DBNull.Value)
                this.Area = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Des_Area"));

            if (pobjIDataReader["Des_TipoColaborador"] != System.DBNull.Value)
                this.TipoVisitante = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Des_TipoColaborador"));

            if (pobjIDataReader["Dt_Entrada"] != System.DBNull.Value)
                this.DataEntrada = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Dt_Entrada"));

            if (pobjIDataReader["Dt_Saida"] != System.DBNull.Value)
                this.DataSaida = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Dt_Saida"));

            if (pobjIDataReader["Nom_Visitante"] != System.DBNull.Value)
                this.Visitante = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Nom_Visitante"));

            if (pobjIDataReader["Nom_Visitado"] != System.DBNull.Value)
                this.Visitado = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Nom_Visitado"));

            if (pobjIDataReader["Num_Cracha"] != System.DBNull.Value)
                this.Cracha = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Num_Cracha"));

            if (pobjIDataReader["Des_Placa"] != System.DBNull.Value)
                this.Veiculo = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Des_Placa"));

            if (pobjIDataReader["Num_Documento"] != System.DBNull.Value)
                this.Documento = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Num_Documento"));
        }
    }
}
