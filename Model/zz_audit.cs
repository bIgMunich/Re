using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Models
{

    public class zz_audit
    {
        [DBEntity(_isPrimary = true)]
        public int ID { get; set; }
        public string 流水号 { get; set; }
        public string 类别 { get; set; }
        public string 材料名称 { get; set; }
        public string 预审意见 { get; set; }
    }
}
