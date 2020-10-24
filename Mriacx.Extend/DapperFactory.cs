using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mriacx.Utility
{
    public class DapperFactory
    {
        private static DapperFactory _instance;
       
        private IConfiguration _configuration;

        private readonly string CONNECTION_STRING = "ConnectionStrings";
        private DapperFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public static DapperFactory GetInstance(IConfiguration configuration)
        {
            if (null == _instance)
            {
                _instance = new DapperFactory(configuration);
            }

            return _instance;
        }

      


    }
}
