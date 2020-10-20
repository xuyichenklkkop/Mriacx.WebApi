using Dapper;
using Microsoft.Extensions.Configuration;
using Mriacx.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;

namespace Mriacx.Dao
{
    public  class DbContextFactory
    {
        public  IDbConnection SqlClient { get; set; }

        public  DbContextFactory()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.Development.json")
           .Build();

            var dapper = DapperHelper.GetInstance(configuration);
            SqlClient = dapper.GetConnection();
        }
    }
}
