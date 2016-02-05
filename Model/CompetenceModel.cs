using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Models
{
    public static class CompetenceModel
    {

        //1为新增，2为修改，3为删除

        //基础管理
        public const string BaseCompetence = "A";

        #region 部门
        //部门管理
        public const string DeptManager = "A1-1";
        public const string DeptAdd = "A1-1-1";
        public const string DeptEdit = "A1-1-2";
        public const string DeptDelete = "A1-1-3";
        #endregion

        #region 用户
        //用户管理
        public const string UserManager = "A1-2";
        public const string UserAdd = "A1-2-1";
        public const string UserEdit = "A1-2-2";
        public const string UserDelete = "A1-2-3";
        #endregion

        #region 角色
        //角色管理
        public const string RoleManager = "A1-3";
        public const string RoleAdd = "A1-3-1";
        public const string RoleEdit = "A1-3-2";
        public const string RoleDelete = "A1-3-3";
        #endregion

        //模板管理
        public const string TemplateManager = "A1-4";

        public const string TemplateActionAdd = "A1-4-1";
        public const string TemplateActionEdit = "A1-4-2";
        public const string TemplateActionDelete = "A1-4-3";

    }
}
