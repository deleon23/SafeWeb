using System;
using System.Collections.Generic;
using System.Text;

namespace SafWeb.Util.Extension
{
    public static class eIDataReader
    {
        public static bool ColumnExists(this System.Data.IDataReader reader, string columnName)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (reader.GetName(i).ToUpper().Equals(columnName.ToUpper()))
                {
                    return true;
                }
            }

            return false;
        }
    }
}

