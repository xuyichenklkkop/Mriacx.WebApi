using System;
using System.Collections.Generic;
using System.Text;

namespace Mriacx.Entity
{
    /// <summary>
    /// 配置类
    /// </summary>
    public class ConfigInfo
    {
        public string Type { get; set; }

        public string Name { get; set; }

        public string Text { get; set; }
    }

    public class AllConfigInfo
    {
        public List<ConfigInfo> ConfigList { get; set; }

        public OrderStock StockInfo { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class OrderStock
    {
        public long Id { get; set; }
        public int Remain { get; set; }
        public int Total { get; set; }
        public string StockCode { get; set; }
        public string StockName { get; set; }
    }
}
