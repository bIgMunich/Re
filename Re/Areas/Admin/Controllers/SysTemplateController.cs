using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using Common;
using DAL;

namespace Re.Areas.Admin.Controllers
{
    public class SysTemplateController : Controller
    {
        //
        // GET: /Admin/SysTemplate/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Test(int Id)
        {
            ViewBag.ParentId = Id;
            return View();
        }

        public ActionResult Add(Sys_Template entity)
        {
            entity.Vaild = 1;
            return Json(new Sys_TemplateDAL().Insert(entity));
        }

        public ActionResult Edit(Sys_Template entity)
        {
            return Json(new Sys_TemplateDAL().Update(entity));
        }

        public ActionResult GetModel(int Id)
        {
            return Json(new Sys_TemplateDAL().GetModel(Id));
        }

        public ActionResult GetList()
        {
            return Json(new Sys_TemplateDAL().GetList());
        }

        public ActionResult GetListByParentId()
        {
            return Json(new Sys_TemplateDAL().GetList(0), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetViewListByType(int Type)
        {
            return Json(new Sys_TemplateDAL().GetViewlListByType(Type), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetViewListByTypeAndParentId(int type, int parentId)
        {
            return Json(new Sys_TemplateDAL().GetViewlListByType(type, parentId), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetTreeTest()
        {
            List<Sys_Template> list = new Sys_TemplateDAL().GetList();
            List<TreeObj> listObj = new List<TreeObj>();
            TreeObj rootObj = new TreeObj();
            rootObj.id = 0;
            rootObj.name = "Root";
            rootObj.pId = 100000;
            listObj.Add(rootObj);
            if (list != null && list.Count > 0)
            {
                foreach (var item in list)
                {
                    TreeObj model = new TreeObj();
                    model.id = item.Id;
                    model.pId = item.ParentId;
                    model.name = item.Template;
                    model.obj1 = item.TemplateUrl;
                    model.obj2 = item.Lever.ToString();
                    listObj.Add(model);
                }
            }
            return Json(listObj, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ActionTree()
        {
            return View();
        }

        public ActionResult GetRoot()
        {
            return Json(new Sys_TemplateDAL().GetViewlListByType(0, 0), JsonRequestBehavior.AllowGet);
        }
    }
}
