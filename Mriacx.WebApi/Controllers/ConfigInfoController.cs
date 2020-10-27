using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mriacx.Entity;
using Mriacx.Service;

namespace Mriacx.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ConfigInfoController : Controller
    {

        ConfigInfoService service = new ConfigInfoService();

        /// <summary>
        /// 获取所有配置
        /// </summary>
        /// <returns></returns>
        public AllConfigInfo GetAllList()
        {
            return service.GetAllList();
        }

        
    }
}
