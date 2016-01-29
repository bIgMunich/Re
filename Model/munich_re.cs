using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Models
{
    //[TableAttribute(ConName = DbConnection.ConnectionString)]
    public class munich_re
    {
        [DBEntity(_isPrimary = true)]
        public int Id { get; set; }

        [DBEntity(_isNullOrEmpty = false)]
        public string Name { get; set; }

        public int Type { get; set; }

        public string Introduce { get; set; }

        public DateTime StartTime { get; set; }

        public bool IsTrue { get; set; }

    }
}
