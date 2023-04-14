using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections.ObjectModel;

namespace SafWeb.Model.Escala
{
    /// ----------------------------------------------------------------------------- 
    /// Project : 
    /// Class : EscalaDepartamental
    /// ----------------------------------------------------------------------------- 
    /// <summary>  
    /// Implementação da classe EscalaDepartamental
    /// </summary> 
    /// <history> 
    /// [cmarchi] created 30/12/2009 
    /// [haguiar] modify 28/03/2011
    /// adicionar propriedade Flg_ReplicaRH;
    /// [haguiar] modify 23/03/2012 15:55
    /// adicionar propriedade Flg_EscalaCREW;    /// </history> 
    /// ----------------------------------------------------------------------------- 
    public class EscalaDepartamental
    {
        #region Variáveis

        private int intCodigo;
        private string strDescricao;
        private Filial.Filial objFilial;
        private Regional.Regional objRegional;
        private Periodicidade objPeriodicidade;
        private int intCodigoUsuarioAlteracao;
        private DateTime datDataAlteracao;
        private bool blnSituacao;
        private bool blnFlg_ReplicaRH;
        private bool blnFlg_EscalaCrew;

        private Collection<HorarioEscala> colHorarios;
        private Collection<SafWeb.Model.Colaborador.Colaborador> colColaboradores;
        private Collection<string> colPeriodos;

        #endregion

        #region Propriedades

        /// <summary> Armazena o conteúdo do campo Id_EscalaDpto  </summary> 
        public int IdEscalaDpto
        {
            get { return this.intCodigo; }
            set { this.intCodigo = value; }
        }

        /// <summary> Armazena o conteúdo do campo Des_EscalaDpto  </summary> 
        public string DescricaoEscalaDpto
        {
            get { return this.strDescricao; }
            set { this.strDescricao = value; }
        }

        /// <summary> Armazena o conteúdo dos campos da Regional  </summary> 
        public Regional.Regional ObjRegional
        {
            get { return this.objRegional; }
            set { this.objRegional = value; }
        }

        /// <summary> Armazena o conteúdo dos campos da Filial  </summary> 
        public Filial.Filial ObjFilial
        {
            get { return this.objFilial; }
            set { this.objFilial = value; }
        }

        /// <summary> Armazena o conteúdo dos campos de Periodicidade  </summary> 
        public Periodicidade ObjPeriodicidade
        {
            get { return this.objPeriodicidade; }
            set { this.objPeriodicidade = value; }
        }

        /// <summary> Armazena o conteúdo do campo Id_UsuarioAlteracao  </summary> 
        public int IdUsuarioAlteracao
        {
            get { return this.intCodigoUsuarioAlteracao; }
            set { this.intCodigoUsuarioAlteracao = value; }
        }

        /// <summary> Armazena o conteúdo do campo Dt_UltAlt_EscalaDpto  </summary> 
        public DateTime DataAlteracao
        {
            get { return this.datDataAlteracao; }
            set { this.datDataAlteracao = value; }
        }

        /// <summary> Armazena o conteúdo do campo Flg_EscalaCrew  </summary> 
        public bool Flg_EscalaCrew
        {
            get { return this.blnFlg_EscalaCrew; }
            set { this.blnFlg_EscalaCrew = value; }
        }

        /// <summary> Armazena o conteúdo do campo Flg_ReplicaRH  </summary> 
        public bool Flg_ReplicaRH
        {
            get { return this.blnFlg_ReplicaRH; }
            set { this.blnFlg_ReplicaRH = value; }
        }

        /// <summary> Armazena o conteúdo do campo Flg_Situacao  </summary> 
        public bool Situacao
        {
            get { return this.blnSituacao; }
            set { this.blnSituacao = value; }
        }

        /// <summary> Armazena os horários da Escala </summary> 
        public Collection<HorarioEscala> HorariosEscala
        {
            get { return this.colHorarios; }
            set { this.colHorarios = value; }
        }

        /// <summary> Armazena os horários da Escala </summary> 
        public Collection<SafWeb.Model.Colaborador.Colaborador> ColaboradoresEscala
        {
            get { return this.colColaboradores; }
            set { this.colColaboradores = value; }
        }

        /// <summary> Armazena o periodo da periodicidade </summary> 
        public Collection<string> Periodos
        {
            get { return this.colPeriodos; }
            set { this.colPeriodos = value; }
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
        /// [cmarchi] created 30/12/2009 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public void FromIDataReader(IDataReader pobjIDataReader)
        {
            if (pobjIDataReader == null) return;

            if (pobjIDataReader["Id_EscalaDpto"] != System.DBNull.Value)
                this.IdEscalaDpto = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_EscalaDpto"));

            if (pobjIDataReader["Des_EscalaDpto"] != System.DBNull.Value)
                this.DescricaoEscalaDpto = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Des_EscalaDpto"));

            this.ObjRegional = new SafWeb.Model.Regional.Regional();
            this.ObjRegional.FromIDataReader(pobjIDataReader);

            this.ObjFilial = new SafWeb.Model.Filial.Filial();
            this.ObjFilial.FromIDataReader(pobjIDataReader);

            this.objPeriodicidade = new Periodicidade();
            this.objPeriodicidade.FromIDataReader(pobjIDataReader);

            if (pobjIDataReader["Id_UsuarioAlteracao"] != System.DBNull.Value)
                this.IdUsuarioAlteracao = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_UsuarioAlteracao"));

            if (pobjIDataReader["Dt_Alteracao"] != System.DBNull.Value)
                this.DataAlteracao = pobjIDataReader.GetDateTime(pobjIDataReader.GetOrdinal("Dt_Alteracao"));

            if (pobjIDataReader["Flg_Situacao"] != System.DBNull.Value)
                this.Situacao = pobjIDataReader.GetBoolean(pobjIDataReader.GetOrdinal("Flg_Situacao"));

            if (pobjIDataReader["Flg_ReplicaRH"] != System.DBNull.Value)
                this.blnFlg_ReplicaRH = pobjIDataReader.GetBoolean(pobjIDataReader.GetOrdinal("Flg_ReplicaRH"));
        }
        #endregion
    }
}