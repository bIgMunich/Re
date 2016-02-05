using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;

namespace Re.Controllers
{

    //
    // GET: /Base/


    public class BaseController : Controller
    {
        public BaseController()
        {
            var obj = SessionHelper.Get("Re_USER_OBJ");
            if (obj != null && !string.IsNullOrEmpty(obj.ToString()))
            {
            }
            else
            {
                //Redirect2();
               // Response.StatusCode = 301;
               // //Url.Action()
               //Response.AppendHeader("Location", Url.Action("Login","Home"));
               // JsScriptHelper.AlertAndRedirect("登录超时，请重新登录", "/User/Login");
            }
        }

        public ActionResult Redirect2()
        {
            //Response.StatusCode = 301;
            //Response.AppendHeader("Location", Url.Action("Login", "Home"));
            JsScriptHelper.AlertAndRedirect("登录超时，请重新登录", "/User/Login");
            return new EmptyResult();
        }
    }

    public class UserAuthorizeAttribute : FilterAttribute, IAuthorizationFilter
    {

        public void OnAuthorization(AuthorizationContext filterContext)
        {

            var obj = SessionHelper.Get("Re_USER_OBJ");
            if (obj != null && !string.IsNullOrEmpty(obj.ToString()))
            {
            }
            else
            {
                filterContext.Result = new RedirectResult("/User/Index", true);
            }

        }
    }

}
