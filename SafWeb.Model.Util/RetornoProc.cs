using System;
using System.Collections.Generic;
using System.Text;
using SafWeb.Util.Extension;


namespace SafWeb.Model.Util
{
    [Serializable]
    public class RetornoProc
    {
        public RetornoProc()
        {
            
        }

        public RetornoProc(bool _erro)
        {
            this.Erro = _erro;
        }

        public int Codigo { get; private set; }

        public bool Erro { get; private set; }

        public string Mensagem { get; private set; }

        public string ProcName { get; private set; }

        public void FromIDataReader(System.Data.IDataReader pobjIDataReader)
        {
            if (pobjIDataReader == null) return;

            if (pobjIDataReader.ColumnExists("CODIGO"))
                if (pobjIDataReader["CODIGO"] != System.DBNull.Value)
                    this.Codigo = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("CODIGO"));

            if (pobjIDataReader.ColumnExists("ERRO"))
                if (pobjIDataReader["ERRO"] != System.DBNull.Value)
                    this.Erro = pobjIDataReader.GetBoolean(pobjIDataReader.GetOrdinal("ERRO"));

            if (pobjIDataReader.ColumnExists("DES_MENSAGEM"))
                if (pobjIDataReader["DES_MENSAGEM"] != System.DBNull.Value)
                    this.Mensagem = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("DES_MENSAGEM"));

            if (pobjIDataReader.ColumnExists("PROC_NAME"))
                if (pobjIDataReader["PROC_NAME"] != System.DBNull.Value)
                    this.ProcName = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("PROC_NAME"));
        }
    }
}
