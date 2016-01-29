using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace Models
{
    public class Sys_RoleTemplate
    {
        [DBEntity(_isPrimary = true)]
        public int Id { get; set; }

        [DBEntity(_isNullOrEmpty = true)]
        public int RoleId { get; set; }

        [DBEntity(_isNullOrEmpty = true)]
        public int ActionId { get; set; }
    }
}
