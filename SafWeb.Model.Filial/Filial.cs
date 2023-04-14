using System;
using System.Collections.Generic;
using System.Text;
using SafWeb.Model.Area;
using System.Collections;
using System.Collections.ObjectModel;

namespace SafWeb.Model.Filial
{
    [Serializable]
    public class Filial
    {
        /// ----------------------------------------------------------------------------- 
        /// Project : SafWeb.Model.Filial
        /// Class : Model.Filial.Filial
        /// ----------------------------------------------------------------------------- 
        /// <summary>  
        ///     Model da tabela de Filial
        /// </summary> 
        /// <history> 
        ///     [mribeiro] 23/06/2009 Created
        ///     [tgerevini] 6/4/2010    Modified
        ///     [tgerevini] 6/4/2010    Modified
        ///     [haguiar_4] 12/01/2011  Modified
        /// </history> 
        /// -----------------------------------------------------------------------------
        #region Variáveis

        private int intCodigo;
        private string srtDescricao;
        private string strAlias;
        private int intRegional;
        private int intCidade;

        private int intEstado;
        
        private int intCodFilial;
        private bool blnFlgPortValAcesso;
        private bool blnFlgContrAcessoOnline;
        private int intQtdToleranciaAcesso;

        private DateTime datDataInclusao;
        private DateTime datDataAlteracao;
        private bool blnFlgSituacao;

        private string strDes_Regional;

        private int intId_FusoHorario;
        private double dblVlr_FusoHorario;

        private Collection<SafWeb.Model.Area.Area> objColArea;

        #endregion

        #region Propriedades

        /// <summary> Armazena coleção de áreas da filial
        public Collection<SafWeb.Model.Area.Area> ColArea
        {
            get
            {
                return objColArea;
            }
            set
            {
                objColArea = value;
            }
        }


        /// <summary> Armazena o conteúdo do campo Id_Filial  </summary> 
        public int IdFilial
        {
            get { return intCodigo; }
            set { intCodigo = value; }
        }

        /// <summary> Armazena o conteúdo do campo Id_FusoHorario  </summary> 
        public int IdFusoHorario
        {
            get { return intId_FusoHorario; }
            set { intId_FusoHorario = value; }
        }

        /// <summary> Armazena o conteúdo do campo Vlr_FusoHorario  </summary> 
        public double Vlr_FusoHorario
        {
            get { return dblVlr_FusoHorario; }
            set { dblVlr_FusoHorario = value; }
        }

        /// <summary> Armazena o conteúdo do campo Des_Filial  </summary> 
        public string DescricaoFilial
        {
            get { return srtDescricao; }
            set { srtDescricao = value; }
        }

        /// <summary> Armazena o conteúdo do campo Alias_Filial  </summary> 
        public string AliasFilial
        {
            get { return strAlias; }
            set { strAlias = value; }
        }

        /// <summary> Armazena o conteúdo do campo Id_Regional  </summary> 
        public int IdRegional
        {
            get { return intRegional; }
            set { intRegional = value; }
        }

        /// <summary> Armazena o conteúdo do campo Id_Cidade  </summary> 
        public int IdCidade
        {
            get { return intCidade; }
            set { intCidade = value; }
        }

        /// <summary> Armazena o conteúdo do campo Id_Estado  </summary> 
        public int IdEstado
        {
            get { return intEstado; }
            set { intEstado = value; }
        }

        /// <summary> Armazena o conteúdo do campo Cod_Filial  </summary> 
        public int CodFilial
        {
            get { return intCodFilial; }
            set { intCodFilial = value; }
        }

        /// <summary> Armazena o conteúdo do campo Flg_PortValAcesso  </summary> 
        public bool FlgPortValAcesso
        {
            get { return blnFlgPortValAcesso; }
            set { blnFlgPortValAcesso = value; }
        }

        /// <summary> Armazena o conteúdo do campo Flg_ContrAcessoOnline  </summary> 
        public bool FlgContrAcessoOnline
        {
            get { return blnFlgContrAcessoOnline; }
            set { blnFlgContrAcessoOnline = value; }
        }

        /// <summary> Armazena o conteúdo do campo Qtd_ToleranciaAcesso  </summary> 
        public int QtdToleranciaAcesso
        {
            get { return intQtdToleranciaAcesso; }
            set { intQtdToleranciaAcesso = value; }
        }

        /// <summary> Armazena o conteúdo do campo Dt_Inclusao  </summary> 
        public DateTime DataInclusao
        {
            get { return datDataInclusao; }
            set { datDataInclusao = value; }
        }

        /// <summary> Armazena o conteúdo do campo Dt_Alteracao  </summary> 
        public DateTime DataAlteracao
        {
            get { return datDataAlteracao; }
            set { datDataAlteracao = value; }
        }

        /// <summary> Armazena o conteúdo do campo Flg_Situacao  </summary> 
        public bool Situacao
        {
            get { return blnFlgSituacao; }
            set { blnFlgSituacao = value; }
        }

        //[haguiar_4] created 12/01/2011
        /// <summary> Armazena o conteúdo do campo Des_Regional  </summary> 
        public string DescricaoRegional
        {
            get { return strDes_Regional; }
            set { strDes_Regional = value; }
        }


        #endregion

        public void FromIDataReader(System.Data.IDataReader pobjIDataReader)
        {
            if (pobjIDataReader == null) return;
            
            if (pobjIDataReader["Id_Filial"] != System.DBNull.Value)
                this.IdFilial = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_Filial"));
            
            if (pobjIDataReader["Des_Filial"] != System.DBNull.Value)
                this.DescricaoFilial = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Des_Filial"));
        
            if (pobjIDataReader["Alias_Filial"] != System.DBNull.Value)
                this.AliasFilial = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Alias_Filial"));
        
            if (pobjIDataReader["Id_Regional"] != System.DBNull.Value)
                this.IdRegional = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_Regional"));

            if (pobjIDataReader["Des_Regional"] != System.DBNull.Value)
                this.DescricaoRegional = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Des_Regional"));
        
            if (pobjIDataReader["Id_Cidade"] != System.DBNull.Value)
                this.IdCidade = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_Cidade"));

            /*
            if (pobjIDataReader["Id_Estado"] != System.DBNull.Value)
                this.IdEstado = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_Estado"));
            */

            if (pobjIDataReader["Cod_Filial"] != System.DBNull.Value)
                this.CodFilial = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Cod_Filial"));

            if (pobjIDataReader["Flg_PortValAcesso"] != System.DBNull.Value)
                this.FlgPortValAcesso = Convert.ToBoolean(pobjIDataReader.GetValue(pobjIDataReader.GetOrdinal("Flg_PortValAcesso")));

            if (pobjIDataReader["Flg_ContrAcessoOnline"] != System.DBNull.Value)
                this.FlgContrAcessoOnline = Convert.ToBoolean(pobjIDataReader.GetValue(pobjIDataReader.GetOrdinal("Flg_ContrAcessoOnline")));

            if (pobjIDataReader["Qtd_ToleranciaAcesso"] != System.DBNull.Value)
                this.QtdToleranciaAcesso = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Qtd_ToleranciaAcesso"));

            if (pobjIDataReader["Dt_Inclusao"] != System.DBNull.Value)
                this.DataInclusao = pobjIDataReader.GetDateTime(pobjIDataReader.GetOrdinal("Dt_Inclusao"));

            if (pobjIDataReader["Dt_Alteracao"] != System.DBNull.Value)
                this.DataAlteracao = pobjIDataReader.GetDateTime(pobjIDataReader.GetOrdinal("Dt_Alteracao"));

            /*
            if (pobjIDataReader["Flg_Situacao"] != System.DBNull.Value)
                this.Situacao = Convert.ToBoolean(pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Flg_Situacao")));
            */

            if (pobjIDataReader["Id_FusoHorario"] != System.DBNull.Value)
                this.IdFusoHorario = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_FusoHorario"));
        }
    }
}