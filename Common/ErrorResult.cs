using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    public class ErrorResult
    {
        public bool Flag { get; set; }
        public string Message { get; set; }
    }

    public class ObjEntity
    {
        public object ResultData { get; set; }
        public object ResultPager { get; set; }
    }
}
