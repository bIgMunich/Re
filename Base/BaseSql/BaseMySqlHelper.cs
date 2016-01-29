using Common;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Base.BaseSql
{
    public class BaseMySqlHelper : IHelperInterfacce
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
            using (var connection = new MySqlConnection(conn))
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand(sql, connection);
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
            using (var connection = new MySqlConnection(conn))
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand(sql, connection);
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
        internal void PrepareParamter(MySqlCommand cmd, List<SqlParam> para)
        {
            if (para != null && para.Count > 0)
            {
                foreach (var item in para)
                {
                    cmd.Parameters.Add(new MySqlParameter(item.key, item.value));
                }
            }
        }

        public T ExecuteEntity<T>(string conn, CommandType cmdType, string sql, List<SqlParam> sqlParm)
        {
            T obj = default(T);
            using (var connection = new MySqlConnection(conn))
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand(sql, connection);
                cmd.CommandType = cmdType;
                this.PrepareParamter(cmd, sqlParm);
                MySqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    obj = ExecuteDataReader<T>(dr);
                    break;
                }
            }
            return obj;
        }

        public List<T> ExecuteList<T>(string conn, CommandType type, string sql, List<SqlParam> sqlParm)
        {
            List<T> list = new List<T>();
            using (var connection = new MySqlConnection(conn))
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand(sql, connection);
                cmd.CommandType = type;
                this.PrepareParamter(cmd, sqlParm);
                MySqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    T obj = ExecuteDataReader<T>(dr);
                    list.Add(obj);
                }
            }
            return list;
        }

        internal T ExecuteEntity<T>(string commandText, CommandType commandType, MySqlParameter[] paras, MySqlConnection connection)
        {
            T obj = default(T);
            using (MySqlCommand cmd = new MySqlCommand(commandText, connection))
            {
                cmd.CommandType = commandType;
                cmd.Parameters.AddRange(paras);
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                using (MySqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
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

        private static T ExecuteDataReader<T>(MySqlDataReader dr)
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
            using (var connection = new MySqlConnection(conn))
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand(sql, connection);
                cmd.CommandType = commandType;
                this.PrepareParamter(cmd, pars);
                MySqlDataAdapter ad = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                ad.Fill(dt);
                try
                {
                    return dt;
                }
                catch (Exception e)
                {
                    connection.Close();
                    throw e;
                }
            }
        }
    }
}
