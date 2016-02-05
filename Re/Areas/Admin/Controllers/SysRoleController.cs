﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using DAL;
using Re.Models;

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

        public ActionResult GetList()
        {
            return Json(new Sys_RoleDAL().GetList(), JsonRequestBehavior.AllowGet);
        }
    }
}
