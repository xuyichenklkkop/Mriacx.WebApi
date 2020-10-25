using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Mriacx.Utility
{
    public class DapperHelper
    {
        /// 数据库连接名
        private static string _connection;

        /// 获取连接名        
        private static string Connection
        {
            get { return _connection; }
        }
        /// 返回连接实例        
        private static IDbConnection dbConnection = null;

        /// 定义一个标识确保线程同步        
        private static readonly object locker = new object();

        /// 静态变量保存类的实例        
        private static DapperHelper uniqueInstance;

        /// <summary>
        /// 私有构造方法，使外界不能创建该类的实例，以便实现单例模式
        /// </summary>
        private DapperHelper(IConfiguration configuration)
        {
             _connection = configuration.GetConnectionString("DefaultConnection");
            //_connection = @"server=.;uid=sa;pwd=sasasa;database=Dapper";
        }

        /// <summary>
        /// 获取实例，这里为单例模式，保证只存在一个实例
        /// </summary>
        /// <returns></returns>
        public static DapperHelper GetInstance(IConfiguration configuration)
        {
            if (uniqueInstance == null)
            {
                lock (locker)
                {
                    if (uniqueInstance == null)
                    {
                        uniqueInstance = new DapperHelper(configuration);
                    }
                }
            }
            return uniqueInstance;
        }

        public IDbConnection GetConnection()
        {
            if (dbConnection == null)
            {
                dbConnection = new SqlConnection(Connection);
            }
            //判断连接状态
            if (dbConnection.State == ConnectionState.Closed)
            {
                dbConnection.Open();
            }
            return dbConnection;
        }

    }
}
