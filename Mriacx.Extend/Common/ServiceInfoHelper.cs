using DoCare.Utility.Common;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Controllers;
using Mriacx.Utility.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mriacx.Utility.Common
{
    /// <summary>
    /// 
    /// </summary>
    public static class ServiceInfoHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="apiExplorer"></param>
        /// <param name="filePath"></param>
        public static void WriteJs(IApiDescriptionGroupCollectionProvider apiExplorer, string filePath)
        {
            var s = apiExplorer.ApiDescriptionGroups;
            FileHandler.WriteAllText(GenWebAPI(s), filePath, "APIs.js");
        }

        /// <summary>
        /// 生成前端API文件
        /// </summary>
        private static string GenWebAPI(ApiDescriptionGroupCollection groups)
        {
            var remoteServiceList = new List<RemoteServiceEntity>();
            foreach (var group in groups.Items)
            {
                foreach (var item in group.Items)
                {
                    if (item.ActionDescriptor is ControllerActionDescriptor controllerActionDescriptor)
                    {
                        remoteServiceList.Add(new RemoteServiceEntity
                        {
                            Area = controllerActionDescriptor.RouteValues.NotNullGetValue("area") ?? "",
                            HttpMethod = string.IsNullOrEmpty(item.HttpMethod) ? GetHttpMethodFromStartWith(controllerActionDescriptor.MethodInfo.Name) : item.HttpMethod,
                            ResModule = controllerActionDescriptor.ControllerName,
                            MethodBehavior = controllerActionDescriptor.ActionName,
                            RelativePath = item.RelativePath.Substring(4),
                        });
                    }
                }
            }

            //把Area有值的放上面
            remoteServiceList = remoteServiceList.OrderBy(e => e.ResModule).ThenBy(e => e.MethodBehavior).ThenByDescending(e => e.Area).ToList();
            var sb = new StringBuilder();
            //开始写入
            sb.AppendLine("/*****************");
            sb.AppendLine("*	");
            sb.AppendLine("*	平台数据API列表");
            sb.AppendLine("*	");
            sb.AppendLine("*	@后台自动生成");
            sb.AppendLine("*	" + DateTimeHandler.CurrentTime.ToShortDateString());
            sb.AppendLine("* ");
            sb.AppendLine("*****************/");
            sb.AppendLine("window._Api = {};");
            sb.AppendLine();

            var sOldModule = "";
            var moduleList = new List<string>();
            var nIndex = 0;
            foreach (var item in remoteServiceList)
            {
                nIndex++;
                if (moduleList.Contains(item.ResModule + item.MethodBehavior))
                {
                    continue;
                }
                moduleList.Add(item.ResModule + item.MethodBehavior);
                if (sOldModule != item.ResModule)
                {
                    if (sOldModule != "")
                    {
                        sb.AppendLine("};");
                    }

                    sb.AppendLine("window._Api." + item.ResModule + " = {");
                }

                sb.AppendLine("    " + item.MethodBehavior + " : function (data) {");
                sb.AppendLine($"        return Req.{item.HttpMethod.Substring(0, 1).ToUpper() + item.HttpMethod.Substring(1).ToLower()}('/{item.RelativePath}', data);");
                sb.Append("	}");

                if (nIndex < remoteServiceList.Count)
                {
                    if (remoteServiceList[nIndex].ResModule == item.ResModule)
                    {
                        sb.Append(",");
                    }
                }
                sb.AppendLine();
                sOldModule = item.ResModule;
            }
            sb.AppendLine("};");
            return sb.ToString();
        }

        private static List<string> HttpMethods = new List<string> { "POST", "GET", "DELETE", "PUT" };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="actionName"></param>
        /// <returns></returns>
        public static string GetHttpMethodFromStartWith(string actionName)
        {
            var httpMethod = HttpMethods.FirstOrDefault(x => actionName.ToUpper().StartsWith(x));
            if (httpMethod == null)
                return "POST";
            return httpMethod;
        }



    }

  
}
