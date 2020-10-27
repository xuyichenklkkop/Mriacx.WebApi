using Mriacx.Dao;
using Mriacx.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mriacx.Service
{
    public  class ConfigInfoService
    {
        ConfigInfoDao configDao = new ConfigInfoDao();

        /// <summary>
        /// 获取所有配置
        /// </summary>
        /// <returns></returns>
        public AllConfigInfo GetAllList() 
        {
            var configList = configDao.GetAllList();
            AllConfigInfo all = new AllConfigInfo();
            var stock = configDao.GetOrderStock();
            all.ConfigList = configList;
            all.StockInfo = stock;
            return all;
        }

        
        /// <summary>
        /// 根绝类型获取配置
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public List<ConfigInfo> GetListByType(string type)
        {
           return configDao.GetAllList().Where(x => x.Type == type).ToList();
        }
    }
}
