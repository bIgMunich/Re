using Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Base.BaseSql
{
    public interface IHelperInterfacce
    {
        object ExecuteScalar(string conn, CommandType cmdType, string sql, List<SqlParam> pars);
        int ExecuteNonQuery(string conn, CommandType cmdType, string sql, List<SqlParam> pars);
        T ExecuteEntity<T>(string conn, CommandType cmdType, string sql, List<SqlParam> sqlParm);
        List<T> ExecuteList<T>(string conn, CommandType type, string sql, List<SqlParam> sqlParm);
        DataTable ExecuteDatatable(string conn, string sql, CommandType commandType, List<SqlParam> pars);
    }
}
