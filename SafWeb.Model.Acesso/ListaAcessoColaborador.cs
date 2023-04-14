using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace SafWeb.Model.Acesso
{
    /// ----------------------------------------------------------------------------- 
    /// Project : 
    /// Class : ListaAcessoColaborador
    /// ----------------------------------------------------------------------------- 
    /// <summary>  
    ///     Implementação da classe ListaAcessoColaborador 
    /// </summary> 
    /// <history> 
    ///     tgerevini   5/04/2010 Created 
    /// </history> 
    /// -----------------------------------------------------------------------------
    public class ListaAcessoColaborador
    {

        #region Variáveis

        private int intCodigoFilial;
        private string strHoraEscalacao;
        private string strNomeColaborador;
        private string strCodigoColaborador;
        private int intCodigoEscalacao;
        private int intCodigoEscalaDpto;
        private string strDescricaoEscalaDpto;
        private bool blnFlgStatusAcesso;
        private int intIdColaborador;
        private DateTime datDt_Escalacao;

        private DateTime datDataEntrada;
        private DateTime datDataSaida;

        #endregion

        #region Propriedades
        
        /// <summary> Armazena o conteúdo do campo Dt_Entrada  </summary> 
        public DateTime DataEntrada
        {
            get { return datDataEntrada; }
            set { datDataEntrada = value; }
        }

        /// <summary> Armazena o conteúdo do campo Dt_Saida  </summary> 
        public DateTime DataSaida
        {
            get { return datDataSaida; }
            set { datDataSaida = value; }
        }

        /// <summary> Armazena o conteúdo do campo Id_Filial  </summary> 
        public int CodigoFilial
        {
            get { return intCodigoFilial; }
            set { intCodigoFilial = value; }
        }
        
        /// <summary> Armazena o conteúdo do campo Dt_Escalacao  </summary> 
        public string HoraEscalacao
        {
            get { return strHoraEscalacao; }
            set { strHoraEscalacao = value; }
        }

        /// <summary> Armazena o conteúdo do campo Nom_Colaborador  </summary> 
        public string NomeColaborador
        {
            get { return strNomeColaborador; }
            set { strNomeColaborador = value; }
        }

        /// <summary> Armazena o conteúdo do campo Cod_Colaborador  </summary> 
        public string CodigoColaborador
        {
            get { return strCodigoColaborador; }
            set { strCodigoColaborador = value; }
        }

        /// <summary> Armazena o conteúdo do campo Id_Colaborador  </summary> 
        public int IdColaborador
        {
            get { return intIdColaborador; }
            set { intIdColaborador = value; }
        }

        /// <summary> Armazena o conteúdo do campo Dt_Escalacao  </summary> 
        public DateTime DtEscalacao
        {
            get { return datDt_Escalacao; }
            set { datDt_Escalacao = value; }
        }

        /// <summary> Armazena o conteúdo do campo Id_Escalacao  </summary> 
        public int CodigoEscalacao
        {
            get { return intCodigoEscalacao; }
            set { intCodigoEscalacao = value; }
        }

        /// <summary> Armazena o conteúdo do campo Des_EscalaDepto  </summary> 
        public string DescricaoEscalaDepto
        {
            get { return strDescricaoEscalaDpto; }
            set { strDescricaoEscalaDpto = value; }
        }

        /// <summary> Armazena o conteúdo do campo Id_EscalaDpto  </summary> 
        public int CodigoEscalaDpto
        {
            get { return intCodigoEscalaDpto; }
            set { intCodigoEscalaDpto = value; }
        }

        /// <summary> Armazena o conteúdo do campo Flg_StatusAcesso  </summary> 
        public bool FlgStatusAcesso
        {
            get { return blnFlgStatusAcesso; }
            set { blnFlgStatusAcesso = value; }
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
        /// tgerevini 5/04/2010 Created 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public void FromIDataReader(IDataReader pobjIDataReader)
        {
            if (pobjIDataReader == null) return;

            DataTable dttListaAcessoColaborador = pobjIDataReader.GetSchemaTable();

            if (pobjIDataReader["Id_Filial"] != System.DBNull.Value)
                this.CodigoFilial  = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_Filial"));

            if (pobjIDataReader["Hora_Escalacao"] != System.DBNull.Value)
                this.HoraEscalacao = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Hora_Escalacao"));

            if (pobjIDataReader["Id_Colaborador"] != System.DBNull.Value)
                this.IdColaborador = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_Colaborador"));

            if (pobjIDataReader["Dt_Escalacao"] != System.DBNull.Value)
                this.DtEscalacao = pobjIDataReader.GetDateTime(pobjIDataReader.GetOrdinal("Dt_Escalacao"));

            if (pobjIDataReader["Nom_Colaborador"] != System.DBNull.Value)
                this.NomeColaborador = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Nom_Colaborador"));

            if (pobjIDataReader["Cod_Colaborador"] != System.DBNull.Value)
                this.CodigoColaborador = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Cod_Colaborador"));

            if (pobjIDataReader["Id_Escalacao"] != System.DBNull.Value)
                this.CodigoEscalacao = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_Escalacao"));

            if (pobjIDataReader["Id_EscalaDpto"] != System.DBNull.Value)
                this.CodigoEscalaDpto = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_EscalaDpto"));

            if (pobjIDataReader["Des_EscalaDpto"] != System.DBNull.Value)
                this.DescricaoEscalaDepto = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Des_EscalaDpto"));

            if (pobjIDataReader["Flg_StatusAcesso"] != System.DBNull.Value)
                this.FlgStatusAcesso = pobjIDataReader.GetBoolean(pobjIDataReader.GetOrdinal("Flg_StatusAcesso"));

            for (int i = 0; i <= dttListaAcessoColaborador.Rows.Count-1; i++)
            {
                if (dttListaAcessoColaborador.Rows[i]["ColumnName"].ToString() == "DT_ENTRADA")
                {
                    if (pobjIDataReader["DT_ENTRADA"] != System.DBNull.Value)
                        this.DataEntrada = pobjIDataReader.GetDateTime(pobjIDataReader.GetOrdinal("DT_ENTRADA"));
                }

                if (dttListaAcessoColaborador.Rows[i]["ColumnName"].ToString() == "DT_SAIDA")
                {
                    if (pobjIDataReader["DT_SAIDA"] != System.DBNull.Value)
                        this.datDataSaida = pobjIDataReader.GetDateTime(pobjIDataReader.GetOrdinal("DT_SAIDA"));
                }
            }
        }

        #endregion
    }
}
