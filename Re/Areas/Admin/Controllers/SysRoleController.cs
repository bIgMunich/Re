using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using DAL;
using Re.Models;
using Common;

namespace Re.Areas.Admin.Controllers
{
    public class SysRoleController : Controller
    {
        //
        // GET: /Admin/SysRole/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Test()
        {
            return View();
        }

        [RoleAction(FunctionNo = CompetenceModel.RoleAdd)]
        public ActionResult Add(Sys_Role entity)
        {
            return Json(new Sys_RoleDAL().Insert(entity));
        }

        public ActionResult GetLists()
        {
            List<Sys_Role> list = new Sys_RoleDAL().GetList();
            return Json(list,JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetList(int page = 1)
        {
            PagerInfo pi = new PagerInfo();
            pi.CurrenetPageIndex = page;
            ObjEntity list = new Sys_RoleDAL().GetList(pi);
            return Json(new { data = list.ResultData, pager = list.ResultPager });
        }
    }
}
