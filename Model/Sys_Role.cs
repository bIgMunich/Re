using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace Models
{
    public class Sys_Role
    {
        [DBEntity(_isPrimary = true)]
        public  int Id { get; set; }

        [DBEntity(_isNullOrEmpty = true)]
        public  string RoleName { get; set; }
    }
}
