using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models
{
    public class LoginUser
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public List<Sys_UserRole> UserRoleList { get; set; }
        public List<SysTemplateViewModel> ActionList { get; set; }
        public int DeptId { get; set; }
    }
}
