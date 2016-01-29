using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    public class PagerInfo
    {
        public PagerInfo()
        {
            this.orderBy = "desc";
            this.orderByKey = "Id";
            this.PageSize = 3;
        }
        public int ItemCount { get; set; }

        public int CurrenetPageIndex { get; set; }

        public int PageCount { get; set; }

        public int PageSize { get; set; }

        public string orderBy { get; set; }

        public string orderByKey { get; set; }
    }
}
