using System;
using System.Collections.Generic;
using System.Text;

namespace Mriacx.Utility.Entity
{
    /// <summary>
    /// 
    /// </summary>
    public class RemoteServiceEntity
    {
        /// <summary>
        /// 接口区域名字
        /// </summary>
        public string Area { get; set; }

        /// <summary>
        /// 接口编号
        /// </summary>
        public string InterfaceNo { get; set; }

        /// <summary>
        /// 接口模块
        /// </summary>
        public string ResModule { get; set; }

        /// <summary>
        /// 接口签名
        /// </summary>
        public string MethodBehavior { get; set; }

        /// <summary>
        /// 接口说明
        /// </summary>
        public string InterfaceRemark { get; set; }

        /// <summary>
        /// 传入参数注释
        /// </summary>
        public string ParmsRemark { get; set; }

        /// <summary>
        /// 返回参数注释
        /// </summary>
        public string ReturnRemark { get; set; }

        /// <summary>
        /// 超时时间 （毫秒） （0是不限制）
        /// </summary>
        public int TimeOut { get; set; } = 60000;

        public EnumMethodType MethodType { get; set; }

        /// <summary>
        /// HttpMethod
        /// </summary>
        public string HttpMethod { get; set; }

        /// <summary>
        /// 路由
        /// </summary>
        public string RelativePath { get; set; }
    }

    public enum EnumMethodType
    {
        /// <summary>
        /// 未定义，不定义时用方法名去定义
        /// </summary>
        None = 0,

        /// <summary>
        /// 查询
        /// </summary>
        Get = 1,

        /// <summary>
        /// 新增
        /// </summary>
        Post = 2,

        /// <summary>
        /// 修改
        /// </summary>
        Put = 3,

        /// <summary>
        /// 删除
        /// </summary>
        Delete = 4
    }
}
