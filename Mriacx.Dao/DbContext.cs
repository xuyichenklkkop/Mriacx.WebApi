using Dapper;
using Microsoft.Extensions.Configuration;
using Mriacx.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Mriacx.Dao
{
    public  class DbContext
    {
        public IDbConnection SqlClient { get; }



        public DbContext()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder();
            IConfiguration config = builder.Build();
            var dapper = DapperHelper.GetInstance(config);
            SqlClient = dapper.GetConnection();
        }
    }
}
