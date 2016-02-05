using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Re.Models
{
    public class UserAuthAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true)
                || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true))
            {
                return;
            }

            var obj = SessionHelper.Get("Re_USER_OBJ");
            if (obj == null || string.IsNullOrEmpty(obj.ToString()))
            {
                //string fromUrl = filterContext.HttpContext.Request.Url.AbsolutePath;
                //string redirectUrl = string.Format("?FromUrl={0}", fromUrl);
                string loginUrl = new UrlHelper(filterContext.RequestContext).Action("Login", "Home");
                //filterContext.Result = new RedirectToRouteResult("Re", new RouteValueDictionary(new { controller = "Home", action = "Login" }));
                filterContext.Result = new RedirectResult("/Home/Login");

                //filterContext.HttpContext.Response.Redirect(loginUrl.ToLower(), true);
            }
        }
    }
}