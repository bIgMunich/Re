using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace Models
{
    public class Sys_User
    {
        [DBEntity(_isPrimary = true)]
        public int Id { get; set; }

        [DBEntity(_isNullOrEmpty = true)]
        public string RealName { get; set; }

        [DBEntity(_isNullOrEmpty = true)]
        public string Account { get; set; }

        [DBEntity(_isNullOrEmpty = true)]
        public string Password { get; set; }

        [DBEntity(_isNullOrEmpty = true)]
        public int Type { get; set; }

        public byte[] Image { get; set; }

        public string Url { get; set; }

        [DBEntity(_isNullOrEmpty = true)]
        public int DeptId { get; set; }
    }
}
