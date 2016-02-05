using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Common;
namespace Base.BaseSql
{
    public class Helper
    {
        private string providerName { get; set; }
        private string connectionString { get; set; }
        private IHelperInterfacce helper { get; set; }
        public Helper()
        {
            //Type t = typeof(T);
            //var collection = t.GetCustomAttributes(typeof(TableAttribute), false);
            //if (collection == null || collection.Length < 1)
            //{
            //    throw new Exception();
            //}
            //TableAttribute ta = (TableAttribute)collection[0];
            //var configure = ConfigurationManager.ConnectionStrings[Enum.GetName(typeof(DbConnection), ta.ConName)];
            //if (configure != null && !string.IsNullOrEmpty(configure.ToString()))
            //{
            //    this.connectionString = configure.ConnectionString;
            //    this.providerName = configure.ProviderName;
            //}
            var configure = ConfigurationManager.ConnectionStrings[DbConnection.ConnectionString.ToString()];
            if (configure != null && !string.IsNullOrEmpty(configure.ToString()))
            {
                this.connectionString = configure.ConnectionString;
                this.providerName = configure.ProviderName;
            }
            helper = Factory.SqlHelpers(this.providerName);
        }

        public ErrorResult Insert<T>(T entity) where T : new()
        {
            ErrorResult result = new ErrorResult();
            Type type = entity.GetType();
            string tableName = type.Name;
            PropertyInfo[] props = type.GetProperties();
            List<string> propNames = new List<string>();
            List<string> paraNames = new List<string>();
            List<SqlParam> paramters = new List<SqlParam>();
            foreach (PropertyInfo prop in props)
            {
                string propName = prop.Name;
                var propValue = prop.GetValue(entity, null);
                ErrorResult model = new Validator().ValidatorData(prop, entity);
                if (model != null && model.Flag == false)
                {
                    result.Flag = false;
                    result.Message = model.Message;
                    return result;
                }
                propNames.Add(propName);
                paraNames.Add("@" + propName);
                paramters.Add(new SqlParam(propName, propValue));
            }
            StringBuilder sb = new StringBuilder();
            sb.Append("insert into ").Append(tableName).Append("(").Append(string.Join(",", propNames)).Append(")values(").Append(string.Join(",", paraNames)).Append(")");
            int res = helper.ExecuteNonQuery(connectionString, CommandType.Text, sb.ToString(), paramters);
            if (res > 0)
            {
                result.Flag = true;
                result.Message = "新增成功";
            }
            else
            {
                result.Flag = false;
                result.Message = "新增失败";
            }
            return result;
        }

        public ErrorResult Delete(string sql, List<SqlParam> pars)
        {
            ErrorResult result = new ErrorResult();
            int res= helper.ExecuteNonQuery(connectionString, CommandType.Text, sql, pars);
            if(res>0)
            {
                result.Flag = true;
            }
            else
            {
                result.Flag = false;
            }
            return result;
        }

        public ErrorResult Update<T>(T entity) where T : new()
        {
            ErrorResult result = new ErrorResult();
            Type t = entity.GetType();
            string tableName = t.Name;
            PropertyInfo[] pi = t.GetProperties();
            List<string> propNames = new List<string>();
            List<string> propContions = new List<string>();
            List<SqlParam> paramters = new List<SqlParam>();
            foreach (PropertyInfo p in pi)
            {
                string name = p.Name;
                var value = p.GetValue(entity, null);
                var attrArrar = p.GetCustomAttributes(typeof(DBEntity), false);
                if (attrArrar != null && attrArrar.Length > 0)
                {
                    DBEntity model = (DBEntity)attrArrar[0];
                    if (model._isPrimary)
                    {
                        propContions.Add(name + "=@" + name);
                        paramters.Add(new SqlParam(name, value));
                    }
                    else
                    {
                        propNames.Add(name + "=@" + name);
                        paramters.Add(new SqlParam(name, value));
                    }
                }
                else
                {
                    propNames.Add(name + "=@" + name);
                    paramters.Add(new SqlParam(name, value));
                }
            }
            StringBuilder sb = new StringBuilder();
            sb.Append("update ").Append(tableName).Append(" set ").Append(string.Join(",", propNames)).Append(" where ").Append(string.Join(" and ", propContions));
            int res = helper.ExecuteNonQuery(connectionString, CommandType.Text, sb.ToString(), paramters);
            if (res > 0)
            {
                result.Flag = true;
                result.Message = "修改成功";
            }
            return result;
        }

        /// <summary>
        /// 根据主键
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public T Find<T>(T entity) where T : new()
        {
            ErrorResult result = new ErrorResult();
            Type t = entity.GetType();
            string tableName = t.Name;
            List<string> propWheres = new List<string>();
            List<SqlParam> paramters = new List<SqlParam>();
            PropertyInfo[] pi = t.GetProperties();
            foreach (PropertyInfo item in pi)
            {
                string name = item.Name;
                var value = item.GetValue(entity, null);
                var array = item.GetCustomAttributes(typeof(DBEntity), false);
                var type = item.GetType().ToString();
                if (array != null && array.Length > 0)
                {
                    DBEntity dbEntity = (DBEntity)array[0];
                    if (dbEntity._isPrimary)
                    {
                        if (value != null && !string.IsNullOrEmpty(value.ToString()))
                        {
                            if (type == "int" && int.Parse(value.ToString()) == 0)
                            {
                                throw new Exception("主键" + name + "的值不能为空");
                            }
                            propWheres.Add(name + "=@" + name);
                            paramters.Add(new SqlParam(name, value));
                        }
                        else
                        {
                            throw new Exception("主键" + name + "的值不能为空");
                        }
                    }
                }
            }
            StringBuilder sb = new StringBuilder();
            sb.Append("select  * from ").Append(tableName).Append(" where ").Append(string.Join(" and ", propWheres));
            T obj = helper.ExecuteEntity<T>(connectionString, CommandType.Text, sb.ToString(), paramters);
            return obj;
        }

        /// <summary>
        /// 根据多个条件不限于主键
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public T Find<T>(T entity, List<string> paramFileds, List<object> paramValues) where T : new()
        {
            ErrorResult result = new ErrorResult();
            Type t = entity.GetType();
            string tableName = t.Name;
            List<SqlParam> paramters = new List<SqlParam>();
            List<string> propWheres = new List<string>();
            if (paramFileds != null && paramFileds.Count > 0)
            {
                for (var i = 0; i < paramFileds.Count; i++)
                {
                    propWheres.Add(paramFileds[i] + "=@" + paramFileds[i]);
                    paramters.Add(new SqlParam(paramFileds[i], paramValues[i]));
                }
            }
            StringBuilder sb = new StringBuilder();
            sb.Append("select  * from ").Append(tableName).Append(" where ").Append(string.Join(" and ", propWheres));
            T obj = helper.ExecuteEntity<T>(connectionString, CommandType.Text, sb.ToString(), paramters);
            return obj;
        }

        public T ExecuteEntity<T>(string sql) where T : new()
        {
            return helper.ExecuteEntity<T>(connectionString, CommandType.Text, sql, null);
        }

        public List<T> ExecuteList<T>(string sql, List<SqlParam> list) where T : new()
        {
            return helper.ExecuteList<T>(connectionString, CommandType.Text, sql, list);
        }



        public ObjEntity ExecutePage<T>(T entity, PagerInfo pageInfo, List<SqlParamList> list) where T : new()
        {
            List<T> lists = new List<T>();
            ObjEntity obj = new ObjEntity();
            PagerInfo result = new PagerInfo();
            Type t = entity.GetType();
            string tableName = t.Name;
            List<string> listWhere = new List<string>();
            List<SqlParam> listParam = new List<SqlParam>();
            if (list != null && list.Count > 0)
            {
                foreach (var item in list)
                {
                    if (item.Sign == "=")
                    {
                        listWhere.Add(item.Key + "=@" + item.Key);
                        listParam.Add(new SqlParam(item.Key, item.Value));
                    }
                }
            }
            StringBuilder sb1 = new StringBuilder();
            if (list != null && list.Count > 0)
            {
                sb1.Append(" select count(*) from ").Append(tableName).Append(" where ").Append(String.Join(" and ", listWhere));
            }
            else
            {
                sb1.Append(" select count(*) from ").Append(tableName);
            }
            object o = helper.ExecuteScalar(connectionString, CommandType.Text, sb1.ToString(), listParam);
            result.ItemCount = Convert.ToInt32(o);
            if (result.ItemCount != 0)
            {
                result.PageCount = (int)Math.Ceiling((float)result.ItemCount / pageInfo.PageSize);
                if (result.CurrenetPageIndex > result.PageCount)
                {
                    pageInfo.CurrenetPageIndex = result.PageCount;
                }
                string wheres = " limit " + (pageInfo.CurrenetPageIndex - 1) * pageInfo.PageSize + "," + pageInfo.PageSize;
                StringBuilder sb2 = new StringBuilder();
                if (list != null && list.Count > 0)
                {
                    sb2.Append(" select * from ").Append(tableName).Append(" where ").Append(string.Join(" and ", listWhere)).Append(" order by ").Append(pageInfo.orderByKey).Append(" ").Append(pageInfo.orderBy).Append(wheres);
                }
                else
                {
                    sb2.Append(" select * from ").Append(tableName).Append(wheres);
                }
                lists = helper.ExecuteList<T>(connectionString, CommandType.Text, sb2.ToString(), listParam);
                result.CurrenetPageIndex = pageInfo.CurrenetPageIndex;
                result.PageSize = pageInfo.PageSize;
                result.PageCount = result.PageCount;
            }
            obj.ResultData = lists;
            obj.ResultPager = result;
            return obj;
        }

        public DataTable ExecuteDataTable(string sql, List<SqlParam> list)
        {
            return helper.ExecuteDatatable(connectionString, sql, CommandType.Text, list);
        }

        public ObjEntity ExecuteDataTablePagerInfo(string sql, List<SqlParam> list, PagerInfo pageInfo)
        {
            ObjEntity result = new ObjEntity();
            object o = helper.ExecuteScalar(connectionString, CommandType.Text, sql, list);
            pageInfo.ItemCount = int.Parse(o.ToString());
            if (pageInfo.ItemCount == 0)
            {
                pageInfo.PageCount = 0;
            }
            else
            {
                pageInfo.PageCount = (int)Math.Ceiling((float)pageInfo.ItemCount / pageInfo.PageSize);
            }
            if (pageInfo.ItemCount == 0)
            {
                pageInfo.CurrenetPageIndex = 0;
            }
            else if (pageInfo.CurrenetPageIndex > pageInfo.PageCount)
            {
                pageInfo.CurrenetPageIndex = pageInfo.PageCount;
            }
            StringBuilder sb = new StringBuilder();
            sb.Append(sql).Append(" limit ").Append("@pageBegin").Append(" to ").Append(" @pageEnd");
            list.Add(new SqlParam("pageBegin", (pageInfo.CurrenetPageIndex - 1) * pageInfo.PageSize));
            list.Add(new SqlParam("pageEnd", (pageInfo.CurrenetPageIndex) * pageInfo.PageSize));
            DataTable dt = helper.ExecuteDatatable(connectionString, sql, CommandType.Text, list);
            result.ResultData = dt;
            result.ResultPager = pageInfo;
            return result;
        }

        public int ExecuteScalar(string sql, List<SqlParam> list)
        {
            object obj = helper.ExecuteScalar(connectionString, CommandType.Text, sql, list);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
    }
}
