using System;
using System.Collections.Generic;
using System.Text;

namespace Mriacx.Entity.Model
{
    public class OrderModel
    {
        public string HQID { get; set; }
        public string FontType { get; set; }
        public string Content { get; set; }
        public string SignName { get; set; }
        public string CoName { get; set; }
        public string CoAddr { get; set; }
        public string CoTelPhone { get; set; }

        public string OrderNum { get; set; }

        public DateTime CreateTime { get; set; }

        public int Status { get; set; }
        public int Num { get; set; }

    }
}
