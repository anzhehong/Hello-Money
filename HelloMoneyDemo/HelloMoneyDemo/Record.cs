using System;

namespace HelloMoneyDemo
{
    public class Record
    {
        public double Amount { get; set; } // amount of this record
        public int Type { get; set; } // type of this record , take 0 as income & 1 as outcome
        public DateTime RecordTime { get; set; } // time of this record
        public Guid ID { get; set; } // id
        public string Category { get; set; } // category of this record

    }
}
