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
            ErrorResult result = new Helper().Insert(entity);
            return result;
        }

        public ErrorResult Update(Sys_Dept entity)
        {
            ErrorResult result = new Helper().Update(entity);
            return result;
        }

        public Sys_Dept GetModel(int Id)
        {
            Sys_Dept entity = new Sys_Dept();
            entity.Id = Id;
            Sys_Dept model = new Helper().Find(entity);
            return model;
        }

        public List<Sys_Dept> GetList(int ParentId)
        {
            string sql = "select * from Sys_Dept where ParentId=@ParentId";
            List<SqlParam> list = new List<SqlParam>();
            list.Add(new SqlParam("ParentId", ParentId));
            List<Sys_Dept> lists = new Helper().ExecuteList<Sys_Dept>(sql, list);
            return lists;
        }

        public List<Sys_Dept> GetList()
        {
            string sql = "select * from Sys_Dept";
            List<Sys_Dept> lists = new Helper().ExecuteList<Sys_Dept>(sql, null);
            return lists;
        }

        public int GetCountByParentId(int ParentId)
        {
            string sql = "select count(Id) from Sys_Dept where ParentId=@ParentId";
            List<SqlParam> list = new List<SqlParam>();
            list.Add(new SqlParam("ParentId", ParentId));
            return new Helper().ExecuteScalar(sql, list);
        }

        public ErrorResult Delete(int Id)
        {
            string sql = "delete from Sys_Dept where Id=@Id";
            List<SqlParam> pars = new List<SqlParam>();
            pars.Add(new SqlParam("Id", Id));
            return new Helper().Delete(sql, pars);
        }
    }
}
