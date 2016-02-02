using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Re.Models
{
    public static class CompetenceMethod
    {
        public static bool ActionFlag(string FunctionNo)
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