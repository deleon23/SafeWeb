using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace SafWeb.Model.Escala
{
    /// ----------------------------------------------------------------------------- 
    /// Project : 
    /// Class : Escalacao
    /// ----------------------------------------------------------------------------- 
    /// <summary>  
    /// Implementação da classe Escalacao
    /// </summary> 
    /// <history> 
    /// [cmarchi] created 7/1/2010 
    /// </history> 
    /// ----------------------------------------------------------------------------- 
    [Serializable]
    public class Escalacao
    {
        #region Variáveis

        private int intCodigo;
        private int intIdFilial;
        private string strAliasFilial;
        private DateTime datDataInicioPeriodo;
        private DateTime datDataFinalPeriodo;
        private DateTime datDataInclusao;
        private DateTime datDataAlteracao;
        private int intCodigoTipoSolicitacao;
        private string strDesTipoSolicitacao;
        private string strPeriodo;
        private int intIdStatusSolicitacao;
        private string strStatusAprovacao;
        private int intCodigoUsuarioSolicitante;
        private string strUsuarioSolicitante;
        private int intIdUsuarioAprovador;
        private string strUsuarioAprovador;
        private bool blnFlgSituacao;
        private int intCodigoEscalaDepartamental;
        private string strDesEscalaDepartamental;
        private string strCorHTML;
        private string strPendenteCom;
        private bool blnFlgEditando;

        private EscalacaoColaboradoresData objEscalacaoColaboradoresData;
        private string strJornada;

        #endregion

        #region Propriedades

        /// <summary> Armazena o conteúdo do campo Id_Escalacao  </summary> 
        public int IdEscalacao
        {
            get { return this.intCodigo; }
            set { this.intCodigo = value; }
        }

        /// <summary> Armazena o conteúdo do campo Id_Filial  </summary> 
        public int IdFilial
        {
            get { return this.intIdFilial; }
            set { this.intIdFilial = value; }
        }

        /// <summary> Armazena o conteúdo do campo Alias_Filial  </summary> 
        public string AliasFilial
        {
            get { return this.strAliasFilial; }
            set { this.strAliasFilial = value; }
        }

        /// <summary> Armazena o conteúdo do campo Dt_InicioPeriodo  </summary> 
        public DateTime DataInicioPeriodo
        {
            get { return this.datDataInicioPeriodo; }
            set { this.datDataInicioPeriodo = value; }
        }

        /// <summary> Armazena o conteúdo do campo Dt_FinalPeriodo  </summary> 
        public DateTime DataFinalPeriodo
        {
            get { return this.datDataFinalPeriodo; }
            set { this.datDataFinalPeriodo = value; }
        }

        /// <summary> Armazena o conteúdo do campo Id_TipoSolicitacao  </summary> 
        public int IdTipoSolicitacao
        {
            get { return this.intCodigoTipoSolicitacao; }
            set { this.intCodigoTipoSolicitacao = value; }
        }

        /// <summary> Armazena o conteúdo do campo Des_TipoSolicitacao  </summary> 
        public string DescricaoTipoSolicitacao
        {
            get { return this.strDesTipoSolicitacao; }
            set { this.strDesTipoSolicitacao = value; }
        }

        /// <summary> Armazena o conteúdo do campo Periodo  </summary> 
        public string Periodo
        {
            get { return this.strPeriodo; }
            set { this.strPeriodo = value; }
        }

        /// <summary> Armazena o conteúdo do campo Id_StatusSolicitacao  </summary> 
        public int IdStatusSolicitacao
        {
            get { return this.intIdStatusSolicitacao; }
            set { this.intIdStatusSolicitacao = value; }
        }

        /// <summary> Armazena o conteúdo do campo Des_StatusAprovacao  </summary> 
        public string DescricaoStatusAprovacao
        {
            get { return this.strStatusAprovacao; }
            set { this.strStatusAprovacao = value; }
        }

        /// <summary> Armazena o conteúdo dos campos Id_UsuarioSolicitante  </summary> 
        public int IdUsuarioSolicitante
        {
            get { return this.intCodigoUsuarioSolicitante; }
            set { this.intCodigoUsuarioSolicitante = value; }
        }

        /// <summary> Armazena o conteúdo do campo Nom_UsuarioSolicitante  </summary> 
        public string NomeUsuarioSolicitante
        {
            get { return this.strUsuarioSolicitante; }
            set { this.strUsuarioSolicitante = value; }
        }

        /// <summary> Armazena o conteúdo dos campos Id_UsuarioAprovador  </summary> 
        public int IdUsuarioAprovador
        {
            get { return this.intIdUsuarioAprovador; }
            set { this.intIdUsuarioAprovador = value; }
        }

        /// <summary> Armazena o conteúdo do campo Nom_UsuarioAprovador  </summary> 
        public string NomeUsuarioAprovador
        {
            get { return this.strUsuarioAprovador; }
            set { this.strUsuarioAprovador = value; }
        }

        /// <summary> Armazena o conteúdo do campo Flg_Situacao </summary> 
        public bool Situacao
        {
            get { return this.blnFlgSituacao; }
            set { this.blnFlgSituacao = value; }
        }

        /// <summary> Armazena o conteúdo do campo Id_EscalaDpto  </summary> 
        public int IdEscalaDepartamental
        {
            get { return this.intCodigoEscalaDepartamental; }
            set { this.intCodigoEscalaDepartamental = value; }
        }

        /// <summary> Armazena o conteúdo do campo Des_EscalaDpto  </summary> 
        public string DescricaoEscalaDepartamental
        {
            get { return this.strDesEscalaDepartamental; }
            set { this.strDesEscalaDepartamental = value; }
        }

        /// <summary> Armazena o conteúdo do campo Des_Jornada  </summary> 
        public string DescricaoJornada
        {
            get { return this.strJornada; }
            set { this.strJornada = value; }
        }

        /// <summary> Armazena o conteúdo do campo EscalacaoColaboradoresData  </summary> 
        public EscalacaoColaboradoresData ObjEscalacaoColDatas
        {
            get { return this.objEscalacaoColaboradoresData; }
            set { this.objEscalacaoColaboradoresData = value; }
        }

        /// <summary> Armazena o conteúdo do campo PendenteCom  </summary> 
        public string PendenteCom
        {
            get { return this.strPendenteCom; }
            set { this.strPendenteCom = value; }
        }

        /// <summary> Armazena o conteúdo do campo Html_CorFonte  </summary> 
        public string CorFonteHtml
        {
            get { return this.strCorHTML; }
            set { this.strCorHTML = value; }
        }

        /// <summary> Armazena o conteúdo do campo Flg_Editando </summary> 
        public bool Editando
        {
            get { return this.blnFlgEditando; }
            set { this.blnFlgEditando = value; }
        }
        /// <summary> Armazena o conteúdo do campo Dt_FinalPeriodo  </summary> 
        public DateTime DataInclusao
        {
            get { return this.datDataInclusao; }
            set { this.datDataInclusao = value; }
        }
        /// <summary> Armazena o conteúdo do campo Dt_FinalPeriodo  </summary> 
        public DateTime DataAlteracao
        {
            get { return this.datDataAlteracao; }
            set { this.datDataAlteracao = value; }
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
        /// [haguiar_2] modify 07/12/2010
        /// propriedade Id_TipoSolicitacao estava sendo preenchida duas vezes, com o valor
        /// final 
        /// [haguiar_4] modify 19/01/2011
        /// acrescentar campo flg_flex para exportar excel
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public void FromIDataReader(IDataReader pobjIDataReader)
        {
            DataTable dttEscalacao = pobjIDataReader.GetSchemaTable();

            if (pobjIDataReader == null) return;

            if (pobjIDataReader["Id_Escalacao"] != System.DBNull.Value)
                this.IdEscalacao = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_Escalacao"));

            if (pobjIDataReader["Dt_InicioPeriodo"] != System.DBNull.Value)
                this.DataInicioPeriodo = pobjIDataReader.GetDateTime(pobjIDataReader.GetOrdinal("Dt_InicioPeriodo"));

            if (pobjIDataReader["Dt_FinalPeriodo"] != System.DBNull.Value)
                this.DataFinalPeriodo = pobjIDataReader.GetDateTime(pobjIDataReader.GetOrdinal("Dt_FinalPeriodo"));

            if (pobjIDataReader["Id_TipoSolicitacao"] != System.DBNull.Value)
                this.IdTipoSolicitacao = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_TipoSolicitacao"));

            if (pobjIDataReader["Id_UsuarioSolicitante"] != System.DBNull.Value)
                this.IdUsuarioSolicitante = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_UsuarioSolicitante"));


            if (pobjIDataReader["Flg_Situacao"] != System.DBNull.Value)
                this.Situacao = pobjIDataReader.GetBoolean(pobjIDataReader.GetOrdinal("Flg_Situacao"));

                //this.Situacao = Convert.ToBoolean(pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Flg_Situacao")));


            if (pobjIDataReader["Id_EscalaDpto"] != System.DBNull.Value)
                this.IdEscalaDepartamental = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_EscalaDpto"));

            if (pobjIDataReader["Flg_Editando"] != System.DBNull.Value)
                this.Editando = pobjIDataReader.GetBoolean(pobjIDataReader.GetOrdinal("Flg_Editando"));

                //this.Editando = Convert.ToBoolean(pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Flg_Editando")));

                //this.Editando = pobjIDataReader.GetBoolean(pobjIDataReader.GetOrdinal("Flg_Editando"));

            if (pobjIDataReader["Dt_Inclusao"] != System.DBNull.Value)
                this.DataInclusao = pobjIDataReader.GetDateTime(pobjIDataReader.GetOrdinal("Dt_Inclusao"));

            if (pobjIDataReader["Dt_Alteracao"] != System.DBNull.Value)
                this.DataAlteracao = pobjIDataReader.GetDateTime(pobjIDataReader.GetOrdinal("Dt_Alteracao"));

            //verifica se há coluna  descrição Jornada
            if (dttEscalacao.Rows[7]["ColumnName"].ToString() == "DES_JORNADA")
            {
                if (pobjIDataReader["Des_Jornada"] != System.DBNull.Value)
                    this.DescricaoJornada = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Des_Jornada"));
            }

            //verifica as colunas de horários e datas dos colocaboradores
            if (dttEscalacao.Rows[8]["ColumnName"].ToString() == "ID_COLABORADOR" && dttEscalacao.Rows[9]["ColumnName"].ToString() == "NOM_COLABORADOR"
                && dttEscalacao.Rows[10]["ColumnName"].ToString() == "DT_ESCALACAO" && dttEscalacao.Rows[11]["ColumnName"].ToString() == "FLG_COMPENSADO"
                && dttEscalacao.Rows[12]["ColumnName"].ToString() == "FLG_FOLGA" && dttEscalacao.Rows[13]["ColumnName"].ToString() == "FLG_LICENCA" && dttEscalacao.Rows[17]["ColumnName"].ToString() == "FLG_FLEX")
            {
                this.ObjEscalacaoColDatas = new EscalacaoColaboradoresData();
                this.ObjEscalacaoColDatas.FromIDataReader(pobjIDataReader);
            }


            //verifica se há coluna no Id da Filial
            if (dttEscalacao.Rows[1]["ColumnName"].ToString() == "ID_FILIAL")
            {
            if (pobjIDataReader["Id_Filial"] != System.DBNull.Value)
                this.IdFilial = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_Filial"));
            }

            //verifica se há coluna no Alias Filial
            if (dttEscalacao.Rows[2]["ColumnName"].ToString() == "ALIAS_FILIAL")
            {
            if (pobjIDataReader["Alias_Filial"] != System.DBNull.Value)
                this.AliasFilial = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Alias_Filial"));
            }

            //verifica se há coluna no Descrição de Escala Departamental
            if (dttEscalacao.Rows[4]["ColumnName"].ToString() == "DES_ESCALADPTO")
            {
            if (pobjIDataReader["Des_EscalaDpto"] != System.DBNull.Value)
                this.DescricaoEscalaDepartamental = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Des_EscalaDpto"));
            }

            //verifica se há coluna no Id do Tipo da Solicitação
            if (dttEscalacao.Rows[5]["ColumnName"].ToString() == "ID_TIPOSOLICITACAO")
            {
            if (pobjIDataReader["Id_TipoSolicitacao"] != System.DBNull.Value)
                this.IdTipoSolicitacao = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_TipoSolicitacao"));
            }

            //verifica se há coluna no Descrição do Tipo de Solicitação
            if (dttEscalacao.Rows[6]["ColumnName"].ToString() == "DES_TIPOSOLICITACAO")
            {
            if (pobjIDataReader["Des_TipoSolicitacao"] != System.DBNull.Value)
                this.DescricaoTipoSolicitacao = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Des_TipoSolicitacao"));
            }

            //verifica se há coluna no Período
            if (dttEscalacao.Rows[9]["ColumnName"].ToString() == "PERIODO")
            {
            if (pobjIDataReader["Periodo"] != System.DBNull.Value)
                this.Periodo = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Periodo"));
            }

            //verifica se há coluna no Id do Status da Solicitação
            if (dttEscalacao.Rows[10]["ColumnName"].ToString() == "ID_STATUSSOLICITACAO")
            {
            if (pobjIDataReader["Id_StatusSolicitacao"] != System.DBNull.Value)
                this.IdStatusSolicitacao = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_StatusSolicitacao"));
            }

            //verifica se há coluna no Descrição do Status de Aprovação
            if (dttEscalacao.Rows[11]["ColumnName"].ToString() == "DES_STATUSAPROVACAO")
            {
            if (pobjIDataReader["Des_StatusAprovacao"] != System.DBNull.Value)
                this.DescricaoStatusAprovacao = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Des_StatusAprovacao"));
            }

            //verifica se há coluna no Id do Usuário Solicitante
            if (dttEscalacao.Rows[12]["ColumnName"].ToString() == "ID_USUARIOSOLICITANTE")
            {
            if (pobjIDataReader["Id_UsuarioSolicitante"] != System.DBNull.Value)
                this.IdUsuarioSolicitante = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_UsuarioSolicitante"));
            }

            //verifica se há coluna do Nome do Usuário Solicitante
            if (dttEscalacao.Rows[13]["ColumnName"].ToString() == "NOM_USUARIOSOLICITANTE")
            {
            if (pobjIDataReader["Nom_UsuarioSolicitante"] != System.DBNull.Value)
                this.NomeUsuarioSolicitante = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Nom_UsuarioSolicitante"));
            }

            //verifica se há coluna no Id do Usuário Aprovador
            if (dttEscalacao.Rows[14]["ColumnName"].ToString() == "ID_USUARIOAPROVADOR")
            {
            if (pobjIDataReader["Id_UsuarioAprovador"] != System.DBNull.Value)
                this.IdUsuarioAprovador = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_UsuarioAprovador"));
            }

            //verifica se há coluna do Nome do Usuário Aprovador
            if (dttEscalacao.Rows.Count > 15 && dttEscalacao.Rows[15]["ColumnName"].ToString() == "NOM_USUARIOAPROVADOR")
            {
            if (pobjIDataReader["Nom_UsuarioAprovador"] != System.DBNull.Value)
                this.NomeUsuarioAprovador = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Nom_UsuarioAprovador"));
            }

            //verifica se há coluna de Pendente Com
            if (dttEscalacao.Rows.Count > 16 && dttEscalacao.Rows[16]["ColumnName"].ToString() == "PENDENTECOM")
            {
            if (pobjIDataReader["PendenteCom"] != System.DBNull.Value)
                this.PendenteCom = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("PendenteCom"));
            }

            //verifica se há coluna de Cor da Fonto do HTML
            if (dttEscalacao.Rows.Count > 17 && dttEscalacao.Rows[17]["ColumnName"].ToString() == "HTML_CORFONTE")
            {
            if (pobjIDataReader["Html_CorFonte"] != System.DBNull.Value)
                this.CorFonteHtml = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Html_CorFonte"));
            }

        }

        #endregion
    }
}
