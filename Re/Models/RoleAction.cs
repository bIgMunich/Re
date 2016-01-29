using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Re.Models
{
    public class RoleActionAttribute : ActionFilterAttribute
    {
        public string FunctionNo { get; set; }

        public string[] FunctionNoList { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (ActionFlag(FunctionNo) == true)
            {
                base.OnActionExecuting(filterContext);
            }
            else
            {
                var json = new JsonResult();
                json.Data = new { Flag = false, Message = "权限不足！！！" };
                filterContext.Result = json;
            }
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
        }

        private bool ActionFlag(string FunctionNo)
        {
            bool falg = false;
            List<SysTemplateViewModel> list = (List<SysTemplateViewModel>)HttpContext.Current.Session["menu"];
            if (list != null && list.Count > 0)
            {
                if (FunctionNo != null && !string.IsNullOrEmpty(FunctionNo))
                {
                    foreach (var item in list)
                    {
                        if (item.TemplateCode == FunctionNo)
                        {
                            return true;
                        }
                    }
                }
            }
            return falg;
        }
    }
}