using System;
using System.Collections.Generic;
using System.Text;

namespace SafWeb.Model.Util
{
    [Serializable]
    public class RetornoAssync
    {
        public RetornoAssync()
        {
            this.mensagem = "";
        }

        public int codigo { get; set; }

        public bool erro { get; set; }

        public string mensagem { get; set; }

        public object lista { get; set; }
    }
}
