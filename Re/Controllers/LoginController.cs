using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL;
using Models;
using Common;

namespace Re.Controllers
{
     [AllowAnonymous]
    public class LoginController : Controller
    {
        //
        // GET: /Login/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Logins(string account, string password)
        {
            ErrorResult result = new ErrorResult();
            Sys_User entity = new Sys_UserDAL().Login(account, password);
            if (entity != null)
            {
                var count = 0;
                result.Flag = true;
                List<SysTemplateViewModel> template = new List<SysTemplateViewModel>();
                LoginUser model = new LoginUser();
                model.DeptId = entity.DeptId;
                model.UserId = entity.Id;
                model.UserName = entity.Account;
                List<Sys_UserRole> userRoleList = new Sys_UserRoleDAL().GetList(entity.Id);
                if (userRoleList != null && userRoleList.Count > 0)
                {
                    Session["RoleName"] = new Sys_RoleDAL().GetModel(userRoleList[0].RoleId).RoleName;
                    foreach (var item in userRoleList)
                    {
                        List<SysTemplateViewModel> templateItemList = new Sys_TemplateDAL().GetViewListByRoleId(item.RoleId);
                        template.AddRange(templateItemList);
                        template.Union(templateItemList).ToList();
                    }
                }
                Session["UserName"] = model.UserName;
                model.UserRoleList = userRoleList;
                model.ActionList = template;
                Session["data"] = model;
                Session["menu"] = template;
                SessionHelper.Add("Re_USER_OBJ", model.UserName, 60);
            }
            else
            {
                result.Flag = false;
                result.Message = "用户名或者密码";
            }
            return Json(result);
        }

        public ActionResult LogOut()
        {
            Session.Clear();
            return Redirect("/Home/Login");
        }

    }
}
