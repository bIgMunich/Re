using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models
{
    public class Sys_UserRoleViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        public string UserName { get; set; }
        public int RoleId { get; set; }

        public string RoleName { get; set; }
    }
}
