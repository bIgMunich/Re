using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Base.BaseSql;

namespace Base.BaseSql
{
    internal static class Factory
    {
        internal static IHelperInterfacce SqlHelpers(string provider)
        {
            switch (provider)
            {
                case "MySql.Data":
                    return new BaseMySqlHelper();
                case "Oracle":
                    return null;
                case "System.Data.SqlClient":
                    return null;
                case "System.Data.OracleClient":
                    return null;
                case "Microsoft.ACE.OLEDB.12.0":
                    return null;
                default:
                    return null;
            }
        }
    }
}
