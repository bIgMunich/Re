using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Base.BaseSql;
using Common;
using Models;

namespace DAL
{
    public class Sys_RoleTemplateDAL
    {
        public ErrorResult Insert(Sys_RoleTemplate entity)
        {
            ErrorResult result = new Helper().Insert(entity);
            return result;
        }

        public ErrorResult Update(Sys_RoleTemplate entity)
        {
            ErrorResult result = new Helper().Update(entity);
            return result;
        }

        public ErrorResult Delete(int RoleId, int ActionId)
        {
            string sql = "delete from Sys_RoleTemplate where RoleId=@RoleId and ActionId=@ActionId";
            List<SqlParam> pars = new List<SqlParam>();
            pars.Add(new SqlParam("RoleId", RoleId));
            pars.Add(new SqlParam("ActionId", ActionId));
            return new Helper().Delete(sql, pars);
        }

        public List<Sys_RoleTemplate> GetList(int RoleId)
        {
            string sql = "select * from Sys_RoleTemplate where RoleId=@RoleId";
            List<SqlParam> pars = new List<SqlParam>();
            pars.Add(new SqlParam("RoleId", RoleId));
            return new Helper().ExecuteList<Sys_RoleTemplate>(sql, pars);
        }

    }
}
