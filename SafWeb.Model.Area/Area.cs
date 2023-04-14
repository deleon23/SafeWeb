using System;
using SafWeb.Util.Extension;

namespace SafWeb.Model.Area
{
    [Serializable]
    public class Area
    {
        /// ----------------------------------------------------------------------------- 
        /// Project : SafWeb.Model.Solicitacao
        /// Class : Model.Solicitacao.Area
        /// ----------------------------------------------------------------------------- 
        /// <summary>  
        ///     Model da tabela de Area
        /// </summary> 
        /// <history> 
        ///     [mribeiro] 07/07/2009 Created 
        ///     [haguiar_4] 02/02/2011 Modify 
        ///     [haguiar] 26/04/2011 modify
        ///     incluir flg_areati
        /// </history> 
        /// -----------------------------------------------------------------------------
        #region Variáveis

        private string strCodigo;
        private string strDescricao;
        private int intFilial;
        private int intGrupoColetores;
        private string strDes_GrupoColetores;

        private string strFlg_AreaSeg;

        private bool blnFlg_Situacao;
        private bool blnFlg_AreaTI;

        //private GrupoColetores objGrupoColetores;

        #endregion

        #region Propriedades

        /// <summary> Armazena o conteúdo do campo blnFlg_AreaSeg  </summary> 
        public string Flg_AreaSeg
        {
            get { return strFlg_AreaSeg; }
            set { strFlg_AreaSeg = value; }
        }

        /// <summary> Armazena o conteúdo do campo blnFlg_AreaTI  </summary> 
        public bool Flg_AreaTI
        {
            get { return blnFlg_AreaTI; }
            set { blnFlg_AreaTI = value; }
        }

        /// <summary> Armazena o conteúdo do campo blnFlg_Situacao  </summary> 
        public bool Flg_Situacao
        {
            get { return blnFlg_Situacao; }
            set { blnFlg_Situacao = value; }
        }

        /*
        /// <summary> Armazena o conteúdo dos campos do Grupo de Coletores  </summary> 
        public GrupoColetores ObjGrupoColetores
        {
            get { return this.objGrupoColetores; }
            set { this.objGrupoColetores = value; }
        }
        */

        /// <summary> Armazena o conteúdo do campo Id_Area + Flg_AreaSeg</summary>
        public string Codigo
        {
            get { return strCodigo; }
            set { strCodigo = value; }
        }

        /// <summary> Armazena o conteúdo do campo Des_Area  </summary>
        public string Descricao
        {
            get { return strDescricao; }
            set { strDescricao = value; }
        }

        /// <summary> Armazena o conteúdo do campo Id_Filial  </summary>
        public int IdFilial
        {
            get { return intFilial; }
            set { intFilial = value; }
        }

        /// <summary> Armazena o conteúdo do campo Id_GrupoColetores  </summary>
        public int IdGrupoColetores
        {
            get { return intGrupoColetores; }
            set { intGrupoColetores = value; }
        }

        /// <summary> Armazena o conteúdo do campo strDes_GrupoColetores  </summary>
        public string Des_GrupoColetores
        {
            get { return strDes_GrupoColetores; }
            set { strDes_GrupoColetores = value; }
        }

        public bool flg_ColetoresPonto { get; set; }
        #endregion

        /// <history> 
        ///     [no history]
        ///     [haguiar] 07/06/2011 modify
        ///     converter Flg_AreaTI BIT para boolean
        /// </history> 
        public void FromIDataReader(System.Data.IDataReader pobjIDataReader)
        {
            if (pobjIDataReader == null) return;

            if (pobjIDataReader["Codigo"] != System.DBNull.Value)
                this.Codigo = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Codigo"));

            if (pobjIDataReader["Des_Area"] != System.DBNull.Value)
                this.Descricao = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Des_Area"));

            if (pobjIDataReader["Id_Filial"] != System.DBNull.Value)
                this.IdFilial = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_Filial"));

            if (pobjIDataReader["Id_GrupoColetores"] != System.DBNull.Value)
                this.IdGrupoColetores = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_GrupoColetores"));

            if (pobjIDataReader["Flg_Situacao"] != System.DBNull.Value)
                this.Flg_Situacao = Convert.ToBoolean(pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Flg_Situacao")));

            try
            {
                if (pobjIDataReader["Flg_AreaSeg"] != System.DBNull.Value)
                {
                    if (pobjIDataReader.GetByte(pobjIDataReader.GetOrdinal("Flg_AreaSeg")) == 1)
                    {
                        this.Flg_AreaSeg = "SIM";
                    }
                    else
                    {
                        this.Flg_AreaSeg = "NAO";
                    }
                }
            }

            catch (Exception ex)
            {
                ex = null;

                this.Flg_AreaSeg = "NAO";
            }

            try
            {
                if (pobjIDataReader["Des_GrupoColetores"] != System.DBNull.Value)
                    this.Des_GrupoColetores = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Des_GrupoColetores"));
            }
            catch (Exception ex)
            {
                ex = null;

                this.Des_GrupoColetores = "";
            }

            if (pobjIDataReader["Flg_AreaTI"] != System.DBNull.Value)
                this.Flg_AreaTI = Convert.ToBoolean(pobjIDataReader.GetValue(pobjIDataReader.GetOrdinal("Flg_AreaTI")));


            if (pobjIDataReader.ColumnExists("flg_ColetoresPonto"))
                if (pobjIDataReader["flg_ColetoresPonto"] != System.DBNull.Value)
                    this.flg_ColetoresPonto = Convert.ToBoolean(pobjIDataReader.GetBoolean(pobjIDataReader.GetOrdinal("flg_ColetoresPonto")));
            /*
            this.objGrupoColetores = new GrupoColetores();
            this.objGrupoColetores.FromIDataReader(pobjIDataReader);
            */

        }
    }
}