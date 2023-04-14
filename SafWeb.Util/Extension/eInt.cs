using System;
using System.Collections.Generic;
using System.Text;

namespace SafWeb.Util.Extension
{
    public static class eInt
    {
        public static bool IsBetween(this int valor, int vInicial, int vFinal)
        {
            return valor >= vInicial && valor <= vFinal;
        }
    }
}
