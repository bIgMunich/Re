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
           return new Helper<Sys_Role>().Insert(entity);
       }

       public ErrorResult Update(Sys_Role entity)
       {
           return  new Helper<Sys_Role>().Update(entity);
       }

       public List<Sys_Role> GetList()
       {
           return new Helper<Sys_Role>().ExecuteList("select * from Sys_Role",null);
       }
    }
}
