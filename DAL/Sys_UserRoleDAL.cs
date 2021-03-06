﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using Base.BaseSql;
using Models;
using System.Web.Mvc;

namespace DAL
{
    public class Sys_UserRoleDAL
    {
        public ErrorResult Insert(Sys_UserRole entity)
        {
            ErrorResult result = new Helper().Insert(entity);
            return result;
        }

        public ErrorResult Delete(int userId, int roleId)
        {
            string sql = "delete from Sys_UserRole where UserId=@UserId and RoleId=@RoleId";
            List<SqlParam> pars = new List<SqlParam>();
            pars.Add(new SqlParam("UserId", userId));
            pars.Add(new SqlParam("RoleId", roleId));
            return new Helper().Delete(sql, pars);
        }

        public List<Sys_UserRole> GetList(int userId)
        {
            string sql = "select * from Sys_UserRole where UserId=@UserId";
            List<SqlParam> pars = new List<SqlParam>();
            pars.Add(new SqlParam("UserId", userId));
            return new Helper().ExecuteList<Sys_UserRole>(sql, pars);
        }

       
    }
}
