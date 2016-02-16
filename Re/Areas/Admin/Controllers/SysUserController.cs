using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;
using Models;
using DAL;
using Re.Models;

namespace Re.Areas.Admin.Controllers
{
    public class SysUserController : Controller
    {
        //
        // GET: /Admin/SysUser/

        public ActionResult Index()
        {
            return View();
        }

        [RoleAction(FunctionNo = CompetenceModel.UserAdd)]
        public ActionResult Add(Sys_User entity)
        {
            entity.Password = "123456";
            ErrorResult result = new Sys_UserDAL().Insert(entity);
            return Json(result);
        }

        [RoleAction(FunctionNo = CompetenceModel.UserEdit)]
        public ActionResult Update(Sys_User entity)
        {
            ErrorResult result = new Sys_UserDAL().Update(entity);
            return Json(result);
        }

        //public ActionResult GetList(int DeptId)
        //{
        //    List<Sys_User> list = new Sys_UserDAL().GetList(DeptId);
        //    return Json(list);
        //}

        public ActionResult GetList(int DeptId, int page = 1)
        {
            PagerInfo pi = new PagerInfo();
            pi.CurrenetPageIndex = page;
            ObjEntity list = new Sys_UserDAL().GetList(DeptId, pi);
            return Json(new { data = list.ResultData, pager = list.ResultPager });
        }

    }
}
