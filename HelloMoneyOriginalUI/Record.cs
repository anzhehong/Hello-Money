using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavigationMenuSample
{
    /// <summary>
    /// Data definition of Record
    /// </summary>
    public class Record
    {
        public double Amount { get; set; } // amount of this record
        public int Type { get; set; } // 0 denotes income & 1 denotes outcome
        public DateTime RecordTime { get; set; } // time of this record
        public Guid ID { get; set; } // id
        public string Category { get; set; } // category of this record

    }
}
