using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using Models;
using Base.BaseSql;

namespace DAL
{
    public class Sys_TemplateDAL
    {
        public ErrorResult Insert(Sys_Template entity)
        {
            ErrorResult result = new ErrorResult();
            if (entity.Type == 0 && entity.Lever > 2)
            {
                result.Flag = false;
                result.Message = "栏目编码最多有两层";
                return result;
            }
            int res = GetCountByCode(entity.TemplateCode);
            if (res > 0)
            {
                result.Flag = false;
                result.Message = "栏目编码不能重复";
                return result;
            }
            result = new Helper<Sys_Template>().Insert(entity);
            return result;
        }

        public ErrorResult Update(Sys_Template entity)
        {
            ErrorResult result = new Helper<Sys_Template>().Update(entity);
            return result;
        }

        public Sys_Template GetModel(int Id)
        {
            Sys_Template entity = new Sys_Template();
            entity.Id = Id;
            Sys_Template model = new Helper<Sys_Template>().Find(entity);
            return model;
        }

        public List<Sys_Template> GetList(int ParentId = 0)
        {
            string sql = "select * from Sys_Template where ParentId=@ParentId";
            List<SqlParam> pars = new List<SqlParam>();
            pars.Add(new SqlParam("ParentId", ParentId));
            return new Helper<Sys_Template>().ExecuteList(sql, pars);
        }

        public int GetCountByCode(string TemplateCode)
        {
            string sql = "select count(Id) from Sys_Template where TemplateCode=@TemplateCode";
            List<SqlParam> pars = new List<SqlParam>();
            pars.Add(new SqlParam("TemplateCode", TemplateCode));
            return new Helper<Sys_Template>().ExecuteScalar(sql, pars);
        }

        public List<SysTemplateViewModel> GetViewlListByType(int Type = 0)
        {
            string sql = "select a.*,(select Template from Sys_Template where Id=a.ParentId) as ParentName  from Sys_Template a where Type=@Type";
            List<SqlParam> pars = new List<SqlParam>();
            pars.Add(new SqlParam("Type", Type));
            return new Helper<SysTemplateViewModel>().ExecuteList(sql, pars);
        }

        public List<SysTemplateViewModel> GetViewlListByType(int Type, int ParentId)
        {
            string sql = "select a.*,(select Template from Sys_Template where Id=a.ParentId) as ParentName  from Sys_Template a where Type=@Type and ParentId=@ParentId";
            List<SqlParam> pars = new List<SqlParam>();
            pars.Add(new SqlParam("Type", Type));
            pars.Add(new SqlParam("ParentId", ParentId));
            return new Helper<SysTemplateViewModel>().ExecuteList(sql, pars);
        }

        public List<SysTemplateViewModel> GetViewListByRoleId(int roleId)
        {
            string sql = "select a.* from sys_template a INNER JOIN sys_roletemplate b on b.ActionId=a.Id where b.RoleId=@RoleId";
            List<SqlParam> pars = new List<SqlParam>();
            pars.Add(new SqlParam("roleId", roleId));
            return new Helper<SysTemplateViewModel>().ExecuteList(sql, pars);
        }
    }
}
