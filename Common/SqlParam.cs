using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    public class SqlParam
    {
        public SqlParam()
        {
        }
        public SqlParam(string k, object v)
        {
            this.key = k;
            this.value = v;
        }
        public string key { get; set; }
        public object value { get; set; }
    }
}
