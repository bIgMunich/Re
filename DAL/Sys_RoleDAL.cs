using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using Models;
using Base.BaseSql;
using Models;

namespace DAL
{
    public class Sys_RoleDAL
    {
        public ErrorResult Insert(Sys_Role entity)
        {
            return new Helper().Insert(entity);
        }

        public ErrorResult Update(Sys_Role entity)
        {
            return new Helper().Update(entity);
        }

        public List<Sys_Role> GetList()
        {
            return new Helper().ExecuteList<Sys_Role>("select * from Sys_Role", null);
        }

        public Sys_Role GetModel(int Id)
        {
            Sys_Role entity = new Sys_Role();
            //List<SqlParam> pars = new List<SqlParam>();
            //pars.Add(new SqlParam("Id", Id));
            entity.Id = Id;
            return new Helper().Find(entity);
        }
    }
}
