using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    public class DBEntity : Attribute
    {
        public string _colName { get; set; }
        public bool _isPrimary { get; set; }

        public string _type { get; set; }

        public bool _isIncrement { get; set; }

        public string _defaultValue { get; set; }

        public bool _isNullOrEmpty { get; set; }

        public bool _isRepeter { get; set; }
    }

    public class DbCondition
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
