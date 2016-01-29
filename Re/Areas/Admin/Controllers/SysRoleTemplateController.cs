using System;
using System.Collections.Generic;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;
using Models;
using DAL;

namespace Re.Areas.Admin.Controllers
{
    public class SysRoleTemplateController : Controller
    {
        //
        // GET: /Admin/SysRoleTemplate/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add(Sys_RoleTemplate entity)
        {
            ErrorResult result = new Sys_RoleTemplateDAL().Insert((entity));
            return Json(result);
        }

        public ActionResult Edit(Sys_RoleTemplate entity)
        {
            ErrorResult result = new Sys_RoleTemplateDAL().Update(entity);
            return Json(result);
        }

        public ActionResult Delete(int roleId, int actionId)
        {
            return Json(new Sys_RoleTemplateDAL().Delete(roleId, actionId), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetListByRoleId(int RoleId)
        {
            return Json((new Sys_RoleTemplateDAL().GetList(RoleId)), JsonRequestBehavior.AllowGet);
        }

    }
}
