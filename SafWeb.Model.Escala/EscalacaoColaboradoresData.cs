using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using SafWeb.Util.Extension;

namespace SafWeb.Model.Escala
{
    /// ----------------------------------------------------------------------------- 
    /// Project : 
    /// Class : EscalacaoColaboradoresData
    /// ----------------------------------------------------------------------------- 
    /// <summary>  
    /// Implementação da classe EscalacaoColaboradoresData
    /// </summary> 
    /// <history> 
    /// [cmarchi] created 13/1/2009 
    /// </history> 
    /// ----------------------------------------------------------------------------- 
    [Serializable]
    public class EscalacaoColaboradoresData
    {
        #region Variáveis

        private int intCodigoEscalacao;
        private int intCodigoColaborador;
        private string strCodigosColaboradores;
        private string strDesColaborador;
        private DateTime datDataEscalacao;

        private int intCodLegado;

        private string strDesJornada;
        private string strHorarioColaboradores;

        private bool blnCompensado;
        private bool blnFolga;
        private bool blnLicenca;

        private bool blnExcluirTrocaHorario;
        private bool blnHorarioFlex;
        private bool blnHoraExtra;

        #endregion

        #region Propriedades

        /// <history>
        ///     [haguiar] created 03/01/2012 09:03
        ///</history>
        /// <summary> Armazena o conteúdo do campo horário do colaboradores  </summary> 
        public string HorarioColaborador
        {
            get { return this.strHorarioColaboradores; }
            set { this.strHorarioColaboradores = value; }
        }

        /// <history>
        ///     [haguiar] created 03/01/2012 09:03
        ///</history>
        /// <summary> Armazena o conteúdo do campo Cod_Legado  </summary> 
        public int CodLegado
        {
            get { return this.intCodLegado; }
            set { this.intCodLegado = value; }
        }


        /// <summary> Armazena o conteúdo do campo Id_Escalacao  </summary> 
        public int IdEscalacao
        {
            get { return this.intCodigoEscalacao; }
            set { this.intCodigoEscalacao = value; }
        }

        /// <summary> Armazena o conteúdo do campo Id_Colaborador  </summary> 
        public int IdColaborador
        {
            get { return this.intCodigoColaborador; }
            set { this.intCodigoColaborador = value; }
        }

        /// <summary> Armazena o conteúdo dos Ids dos colaboradores  </summary> 
        public string IdColaboradores
        {
            get { return this.strCodigosColaboradores; }
            set { this.strCodigosColaboradores = value; }
        }

        /// <summary> Armazena o conteúdo do campo Nom_Colaborador  </summary> 
        public string NomeColaborador
        {
            get { return this.strDesColaborador; }
            set { this.strDesColaborador = value; }
        }

        /// <summary> Armazena o conteúdo do campo Dt_Escalacao  </summary> 
        public DateTime DataEscalacao
        {
            get { return this.datDataEscalacao; }
            set { this.datDataEscalacao = value; }
        }

        /// <summary> Armazena o conteúdo do campo Flg_Compensado  </summary> 
        public bool Compensado
        {
            get { return this.blnCompensado; }
            set { this.blnCompensado = value; }
        }

        /// <summary> Armazena o conteúdo do campo Flg_Folga  </summary> 
        public bool Folga
        {
            get { return this.blnFolga; }
            set { this.blnFolga = value; }
        }

        /// <summary> Armazena o conteúdo do campo Flg_Licenca  </summary> 
        public bool Licenca
        {
            get { return this.blnLicenca; }
            set { this.blnLicenca = value; }
        }
        #endregion

        /// <summary> Armazena o conteúdo do campo Flg_HoraExtra</summary> 
        /// <history>
        ///     [haguiar_8829] created 06/07/2011 14:49
        /// </history>
        public bool HoraExtra
        {
            get { return this.blnHoraExtra; }
            set { this.blnHoraExtra = value; }
        }

        /// <summary> Armazena o conteúdo do campo Flg_HorarioFlex</summary> 
        /// <history>
        ///     [haguiar] created 04/12/2010
        /// </history>
        public bool HorarioFlex
        {
            get { return this.blnHorarioFlex; }
            set { this.blnHorarioFlex = value; }
        }

        /// <summary> Excluir a troca de horário </summary> 
        /// <history>
        ///     [haguiar] created 02/12/2010
        /// </history>
        public bool ExcluirTrocaHorario
        {
            get { return this.blnExcluirTrocaHorario; }
            set { this.blnExcluirTrocaHorario = value; }
        }

        /// <summary> Armazena o conteúdo do campo DesJornada  </summary> 
        public string DesJornada
        {
            get { return this.strDesJornada; }
            set { this.strDesJornada = value; }
        }

        public int IdJornada { get; set; }

        public bool flgIniciaFolgando { get; set; }

        public bool flgSituacao { get; set; }

        #region FromIDataReader

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// Metodo que popula a Model com base em uma interface datareader
        /// </summary>
        /// <param name="pobjIDataReader">interface datareader</param>
        /// <value>IDataReader</value> 
        /// <history> 
        /// [cmarchi] created 13/1/2010 
        /// [haguiar_2] modify 04/12/2010
        /// incluir horario flex
        /// [haguiar_8829] modify 06/07/2011 14:50
        /// incluir hora extra
        /// [haguiar_9004] modify 12/09/2011 11:48
        /// incluir des_jornada
        /// [haguiar] modify 02/01/2012 16:25
        /// incluir cod_legado
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public void FromIDataReader(IDataReader pobjIDataReader)
        {
            DataTable dttEscalacaoColaboradoresData = pobjIDataReader.GetSchemaTable();

            if (pobjIDataReader == null) return;

            if (pobjIDataReader["Id_Escalacao"] != System.DBNull.Value)
                this.IdEscalacao = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_Escalacao"));

            if (pobjIDataReader["Id_Colaborador"] != System.DBNull.Value)
                this.IdColaborador = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_Colaborador"));

            if (pobjIDataReader["Nom_Colaborador"] != System.DBNull.Value)
                this.NomeColaborador = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Nom_Colaborador"));  

            if (pobjIDataReader["Dt_Escalacao"] != System.DBNull.Value)
                this.DataEscalacao = pobjIDataReader.GetDateTime(pobjIDataReader.GetOrdinal("Dt_Escalacao"));

            if (pobjIDataReader["Flg_Compensado"] != System.DBNull.Value)
                this.Compensado = pobjIDataReader.GetBoolean(pobjIDataReader.GetOrdinal("Flg_Compensado"));

            if (pobjIDataReader["Flg_Folga"] != System.DBNull.Value)
                this.Folga = pobjIDataReader.GetBoolean(pobjIDataReader.GetOrdinal("Flg_Folga"));

            if (pobjIDataReader["Flg_Licenca"] != System.DBNull.Value)
                this.Licenca = pobjIDataReader.GetBoolean(pobjIDataReader.GetOrdinal("Flg_Licenca"));

            if (pobjIDataReader["Flg_Flex"] != System.DBNull.Value)
                this.HorarioFlex = pobjIDataReader.GetBoolean(pobjIDataReader.GetOrdinal("Flg_Flex"));

            if (pobjIDataReader["Flg_HoraExtra"] != System.DBNull.Value)
                this.HoraExtra = pobjIDataReader.GetBoolean(pobjIDataReader.GetOrdinal("Flg_HoraExtra"));


            for (int i = 0; i <= dttEscalacaoColaboradoresData.Rows.Count - 1; i++)
            {
                if (dttEscalacaoColaboradoresData.Rows[i]["ColumnName"].ToString().ToUpper() == "DES_JORNADA")
                {
                    if (pobjIDataReader["Des_Jornada"] != System.DBNull.Value)
                        this.DesJornada = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("DES_JORNADA"));
                }

                if (dttEscalacaoColaboradoresData.Rows[i]["ColumnName"].ToString().ToUpper() == "COD_LEGADO")
                {
                    if (pobjIDataReader["COD_LEGADO"] != System.DBNull.Value)
                        this.CodLegado = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("COD_LEGADO"));
                }

                if (dttEscalacaoColaboradoresData.Rows[i]["ColumnName"].ToString().ToUpper() == "DES_HORARIO")
                {
                    if (pobjIDataReader["DES_HORARIO"] != System.DBNull.Value)
                        this.HorarioColaborador = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("DES_HORARIO"));
                }

            }

            if (pobjIDataReader.ColumnExists("Flg_InicioFolga"))
                if (pobjIDataReader["Flg_InicioFolga"] != System.DBNull.Value)
                    this.flgIniciaFolgando = pobjIDataReader.GetBoolean(pobjIDataReader.GetOrdinal("Flg_InicioFolga"));

            if (pobjIDataReader.ColumnExists("Id_Jornada"))
                if (pobjIDataReader["Id_Jornada"] != System.DBNull.Value)
                    this.IdJornada = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_Jornada"));

            if (pobjIDataReader.ColumnExists("Flg_Situacao"))
                if (pobjIDataReader["Flg_Situacao"] != System.DBNull.Value)
                    this.flgSituacao = pobjIDataReader.GetBoolean(pobjIDataReader.GetOrdinal("Flg_Situacao"));



        }

        #endregion
    }
}