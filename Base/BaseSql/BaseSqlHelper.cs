using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Reflection;
using Common;

namespace Base.BaseSql
{
    public class BaseSqlHelper : IHelperInterfacce
    {

        /// <summary>
        /// 执行mysql语句返回查询的第一行第一列单元格数据
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdType"></param>
        /// <param name="sql"></param>
        /// <param name="pars"></param>
        /// <returns></returns>ExecuteScalar
        public object ExecuteScalar(string conn, CommandType cmdType, string sql, List<SqlParam> pars)
        {
            using (var connection = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.CommandType = cmdType;
                this.PrepareParamter(cmd, pars);
                try
                {
                    return cmd.ExecuteScalar();
                }
                catch (Exception e)
                {
                    connection.Close();
                    throw e;
                }
            }
        }

        public int ExecuteNonQuery(string conn, CommandType cmdType, string sql, List<SqlParam> pars)
        {
            using (var connection = new SqlConnection(conn))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.CommandType = cmdType;
                this.PrepareParamter(cmd, pars);
                try
                {
                    return cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    connection.Close();
                    throw e;
                }
            }
        }

        /// <summary>
        /// 准备数据查询参数
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="para"></param>
        internal void PrepareParamter(SqlCommand cmd, List<SqlParam> para)
        {
            if (para != null && para.Count > 0)
            {
                foreach (var item in para)
                {
                    cmd.Parameters.Add(new SqlParameter(item.key, item.value));
                }
            }
        }

        public T ExecuteEntity<T>(string conn, CommandType cmdType, string sql, List<SqlParam> sqlParm)
        {
            T obj = default(T);
            using (var connection = new SqlConnection(conn))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.CommandType = cmdType;
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    obj = ExecuteDataReader<T>(dr);
                    break;
                }
            }
            return obj;
        }

        public List<T> ExecuteList<T>(string conn, CommandType cmdType, string sql, List<SqlParam> sqlParm)
        {
            List<T> list = new List<T>();
            using (var connection = new SqlConnection(conn))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.CommandType = cmdType;
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    T obj = ExecuteDataReader<T>(dr);
                    list.Add(obj);
                }
            }
            return list;
        }

        internal T ExecuteEntity<T>(string commandText, CommandType commandType, SqlParameter[] paras, SqlConnection connection)
        {
            T obj = default(T);
            using (SqlCommand cmd = new SqlCommand(commandText, connection))
            {
                cmd.CommandType = commandType;
                cmd.Parameters.AddRange(paras);
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (dr.Read())
                    {
                        obj = ExecuteDataReader<T>(dr);
                        break;
                    }
                }
            }
            return obj;
        }

        private static T ExecuteDataReader<T>(SqlDataReader dr)
        {
            T obj = default(T);
            Type type = typeof(T);
            PropertyInfo[] propertyInfos = type.GetProperties();
            int columnCount = dr.FieldCount;
            obj = Activator.CreateInstance<T>();
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                string propertyName = propertyInfo.Name;
                for (int i = 0; i < columnCount; i++)
                {
                    string columnName = dr.GetName(i);
                    if (string.Compare(propertyName, columnName, true) == 0)
                    {
                        object value = dr.GetValue(i);
                        if (value != null && value != DBNull.Value)
                        {

                            propertyInfo.SetValue(obj, Convert.ChangeType(value, propertyInfo.PropertyType), null);
                        }
                        break;
                    }
                }
            }
            return obj;
        }

        public DataTable ExecuteDatatable(string conn, string sql, CommandType commandType, List<SqlParam> pars)
        {
            return new DataTable();
        }
    }
}
