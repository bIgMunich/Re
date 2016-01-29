using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using DAL;

namespace Re.Areas.Admin.Controllers
{
    public class SysUserRoleController : Controller
    {
        //
        // GET: /Admin/SysUserRole/

        //public ActionResult Index()
        //{
        //    return View();
        //}

        public ActionResult Add(Sys_UserRole entity)
        {
            return Json(new Sys_UserRoleDAL().Insert(entity));
        }

        public ActionResult Delete(int userId, int roleId)
        {
            return Json(new Sys_UserRoleDAL().Delete(userId, roleId));
        }

        public ActionResult GetList(int userId)
        {
            List<Sys_UserRole> list = new Sys_UserRoleDAL().GetList(userId);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}
