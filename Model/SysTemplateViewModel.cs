using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class SysTemplateViewModel
    {
        public int Id { get; set; }
        public int Vaild { get; set; }

        public string VaildName
        {
            get
            {
                switch (Vaild)
                {
                    case 0:
                        return "停用";
                    case 1:
                        return "启用";
                    default:
                        return "未知";

                }
            }
        }

        public int Lever { get; set; }
        public string Template { get; set; }
        public string TemplateUrl { get; set; }
        public int ParentId { get; set; }
        public string ParentName { get; set; }
        public string TemplateCode { get; set; }
        public int Type { get; set; }//0位菜单项，1位action项

        public string TypeName
        {
            get
            {
                switch (Type)
                {
                    case 0:
                        return "菜单";
                    case 1:
                        return "方法";
                    default:
                        return "未知";

                }
            }

        }
        
    }
}