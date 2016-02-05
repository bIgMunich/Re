using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using DAL;
using Common;
using Re.Controllers;
using Re.Models;


namespace Re.Areas.Admin.Controllers
{
    public class SysDeptController : Controller
    {
        //
        // GET: /Admin/SysDept/

        public ActionResult Index()
        {
          
            var obj = Session["UserName"];
            return View();
        }

        [RoleAction(FunctionNo = CompetenceModel.DeptAdd)]
        public ActionResult Add(Sys_Dept entity)
        {
            entity.DeptLever = 0;
            ErrorResult result = new Sys_DeptDAL().Insert(entity);
            return Json(result);
        }

        [RoleAction(FunctionNo = CompetenceModel.DeptEdit)]
        public ActionResult Update(Sys_Dept entity)
        {
            ErrorResult result = new Sys_DeptDAL().Update(entity);
            return Json(result);
        }

        [RoleAction(FunctionNo = CompetenceModel.DeptDelete)]
        public ActionResult Delete(int Id)
        {
            ErrorResult result = new ErrorResult();
            List<Sys_Dept> listDept = new Sys_DeptDAL().GetList(Id);
            if (listDept != null && listDept.Count > 0)
            {
                result.Flag = false;
                result.Message = "此部门下面有子部门或者人员";
                return Json(result);
            }
            List<Sys_User> listUser = new Sys_UserDAL().GetList(Id);
            if (listUser != null && listUser.Count > 0)
            {
                result.Flag = false;
                result.Message = "此部门下面有子部门或者人员";
                return Json(result);
            }
            result = new Sys_DeptDAL().Delete(Id);
            return Json(result);
        }

        public ActionResult GetModel(int Id)
        {
            Sys_Dept model = new Sys_DeptDAL().GetModel(Id);
            return Json(model, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetTreeTest()
        {
            List<Sys_Dept> list = new Sys_DeptDAL().GetList();
            List<TreeObj> listObj = new List<TreeObj>();
            TreeObj modelRoot = new TreeObj();
            modelRoot.id = 0;
            modelRoot.pId = -1;
            modelRoot.name = "Root";
            modelRoot.open = true;
            listObj.Add(modelRoot);
            if (list != null && list.Count > 0)
            {
                foreach (var item in list)
                {
                    TreeObj model = new TreeObj();
                    model.id = item.Id;
                    model.pId = item.ParentId;
                    model.name = item.DeptName;
                    model.open = true;
                    listObj.Add(model);
                }
            }
            return Json(listObj, JsonRequestBehavior.AllowGet);
        }

        public string GetTree()
        {
            string json = "[";
            List<Sys_Dept> list = new Sys_DeptDAL().GetList(0);
            //if (list != null && list.Count > 0)
            //{
            foreach (var item in list)
            {
                json += "{";
                json += "\"id\":" + item.Id + ",";
                json += "\"pid\":\"" + item.DeptName + "\",";
                json += "\"name\":\"" + item.ParentId + "\",";
                if (GetTreeByParentId(item.Id) != "")
                {
                    json = json + GetTreeByParentId(item.Id);
                }
                json += "},";

            }
            json = json.Substring(0, json.Length - 1);
            json += "]";
            //}
            return json;
        }

        public static string GetTreeByParentId(int ParentId)
        {
            string json = "";
            List<Sys_Dept> list = new Sys_DeptDAL().GetList(ParentId);
            if (list != null && list.Count > 0)
            {
                json = "[";
                foreach (var item in list)
                {
                    json += "{";
                    json += "\"id\":" + item.Id + ",";
                    json += "\"pid\":\"" + item.DeptName + "\",";
                    json += "\"name\":\"" + item.ParentId + "\",";
                    if (GetTreeByParentId(item.Id) != "")
                    {
                        json = json + GetTreeByParentId(item.Id);
                    }
                    json += "},";
                }
                json = json.Substring(0, json.Length - 1);
                json += "]";
            }
            return json;
        }

    }

    public class TreeObj
    {
        public int id { get; set; }
        public int pId { get; set; }
        public string name { get; set; }
        public bool open { get; set; }
        public string obj1 { get; set; }
        public string obj2 { get; set; }
        public string obj3 { get; set; }
    }
}
