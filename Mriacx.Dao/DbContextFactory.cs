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
    public  static class DbContextFactory
    {
        public static IDbConnection GetDbContext()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
          .SetBasePath(Directory.GetCurrentDirectory())
          .AddJsonFile("appsettings.Development.json")
          .Build();

            var dapper = DapperHelper.GetInstance(configuration);
            var dbcontext = dapper.GetConnection();
            return dbcontext;
        }
    }
}
