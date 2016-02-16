using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using Base.BaseSql;
using Models;

namespace DAL
{
    public class Sys_UserDAL
    {
        public ErrorResult Insert(Sys_User entity)
        {
            ErrorResult result = new Helper().Insert(entity);
            return result;
        }

        public ErrorResult Update(Sys_User entity)
        {
            ErrorResult result = new Helper().Update(entity);
            return result;
        }

        public List<Sys_User> GetList(int DeptId)
        {
            string sql = "select * from Sys_User where DeptId=@DeptId";
            List<SqlParam> list = new List<SqlParam>();
            list.Add(new SqlParam("DeptId", DeptId));
            return new Helper().ExecuteList<Sys_User>(sql, list);
        }

        public Sys_User GetModel(int Id)
        {
            Sys_User entity = new Sys_User();
            entity.Id = Id;
            return new Helper().Find(entity);
        }

        public Sys_User Login(string account, string password)
        {
            string sql = "select * from Sys_User where Account=@Account and Password=@Password";
            List<SqlParam> pars = new List<SqlParam>();
            pars.Add(new SqlParam("Account", account));
            pars.Add(new SqlParam("Password", password));
            List<Sys_User> list = new Helper().ExecuteList<Sys_User>(sql, pars);
            if (list != null && list.Count > 0)
            {
                return list[0];
            }
            else
            {
                return null;
            }
        }

        public ObjEntity GetList(int DeptId, PagerInfo pageInfo)
        {
            string sql = "select * from Sys_User where DeptId=@DeptId";
            List<SqlParam> list = new List<SqlParam>();
            list.Add(new SqlParam("DeptId", DeptId));
            return new Helper().ExecutePages<Sys_User>(sql, pageInfo, list);
        }



        //public List<LoginUser> GetLoginUser(string userId)
        //{
        //    StringBuilder sb=new StringBuilder
        //}
    }
}
