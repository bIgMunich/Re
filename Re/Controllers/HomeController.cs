using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using DAL;
using Common;
using System.Data;

namespace Re.Controllers
{
    [AllowAnonymous]
    public class HomeController :Controller
    {
        //
        // GET: /Home/

        public ActionResult Index(int page = 1)
        {
            List<SqlParamList> list = new List<SqlParamList>();
            list.Add(new SqlParamList("IsTrue", "=", 0));
            PagerInfo pi = new PagerInfo();
            pi.CurrenetPageIndex = page;
            ObjEntity data = new munich_reDAL().GetPagerList(pi, list);
            return View(data);
        }

        public ActionResult Ulr()
        {
            return RedirectToAction("Index", "Admin/Home");
        }


        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(munich_re model)
        {
            ErrorResult result = new munich_reDAL().Insert(model);
            if (result.Flag == true)
            {
                return Json(result.Message);
            }
            else
            {
                return Json(result.Message);
            }
        }

        public ActionResult Edit(int Id)
        {
            munich_re entity = new munich_reDAL().GetModel(Id);
            return View(entity);
        }

        [HttpPost]
        public ActionResult Edit(munich_re entity)
        {
            ErrorResult result = new munich_reDAL().Update(entity);
            if (result.Flag == true)
            {
                return Json(result.Message);
            }
            else
            {
                return Json(result.Message);
            }
        }

        public ActionResult Login()
        {
            return View();
        }

    }
}
