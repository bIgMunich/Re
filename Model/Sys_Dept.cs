using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models
{
    public class Sys_Dept
    {
        [DBEntity(_isPrimary=true)]
        public int Id { get; set; }

        [DBEntity(_isRepeter=true,_isNullOrEmpty=true)]
        public string DeptCode { get; set; }

        [DBEntity(_isNullOrEmpty=true)]
        public string DeptName { get; set; }

        public int ParentId { get; set; }

        public int DeptLever { get; set; }

    }
}
