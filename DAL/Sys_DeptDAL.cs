using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Models;
using Common;
using Base.BaseSql;

namespace DAL
{
    public class Sys_DeptDAL
    {

        public ErrorResult Insert(Sys_Dept entity)
        {
            ErrorResult result = new Helper<Sys_Dept>().Insert(entity);
            return result;
        }

        public ErrorResult Update(Sys_Dept entity)
        {
            ErrorResult result = new Helper<Sys_Dept>().Update(entity);
            return result;
        }

        public Sys_Dept GetModel(int Id)
        {
            Sys_Dept entity = new Sys_Dept();
            entity.Id = Id;
            Sys_Dept model = new Helper<Sys_Dept>().Find(entity);
            return model;
        }

        public List<Sys_Dept> GetList(int ParentId)
        {
            string sql = "select * from Sys_Dept where ParentId=@ParentId";
            List<SqlParam> list = new List<SqlParam>();
            list.Add(new SqlParam("ParentId", ParentId));
            List<Sys_Dept> lists = new Helper<Sys_Dept>().ExecuteList(sql, list);
            return lists;
        }

        public List<Sys_Dept> GetList()
        {
            string sql = "select * from Sys_Dept";
            List<Sys_Dept> lists = new Helper<Sys_Dept>().ExecuteList(sql, null);
            return lists;
        }

        public int GetCountByParentId(int ParentId)
        {
            string sql = "select count(Id) from Sys_Dept where ParentId=@ParentId";
            List<SqlParam> list = new List<SqlParam>();
            list.Add(new SqlParam("ParentId", ParentId));
            return new Helper<Sys_Dept>().ExecuteScalar(sql, list);
        }
    }
}
