using System;
using System.Collections.Generic;
using System.Text;

namespace SafWeb.Util.Extension
{
    public static class eString
    {
        public static int ToInt32(this string valor)
        {
            int varInt = 0;

            if (string.IsNullOrEmpty(valor))
                valor = "0";

            if (int.TryParse(valor, out varInt))
            {
                return varInt;
            }
            else
            {
                throw new Exception(string.Format("Erro ao converter \"{0}\" para int", valor));
            }
        }

        public static int ToInt32(this object valor)
        {
            try
            {
                return valor.ToString().ToInt32();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static string UppercaseFirst(this string s)
        {
            System.Globalization.CultureInfo cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;
            return cultureInfo.TextInfo.ToTitleCase(s);
        }

        public static bool ContainsRegex(this string valor, string patternER, System.Text.RegularExpressions.RegexOptions Options)
        {
            var ER = new System.Text.RegularExpressions.Regex(patternER, Options);

            return ER.IsMatch(valor);
    }

        public static bool ContainsRegex(this string valor, string patternER)
        {
            return valor.ContainsRegex(patternER, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
}
    }
}
