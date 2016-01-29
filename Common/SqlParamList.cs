using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    public class SqlParamList
    {
        public SqlParamList()
        {
        }
        public SqlParamList(string k, string s, object v)
        {
            this.Key = k;
            this.Sign = s;
            this.Value = v;
        }
        public string Key { get; set; }
        public string Sign { get; set; }
        public object Value { get; set; }
    }
}
