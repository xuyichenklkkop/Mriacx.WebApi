using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Mriacx.Utility.Common
{
    public  class FileHandler
    {
        private static string FolderName = String.Empty;
        private static readonly Object locko = new object();

        public static void CheckFolder(string folder)
        {
            FolderName = folder;
            if (!Directory.Exists(FolderName))
            {
                Directory.CreateDirectory(FolderName);
            }
        }

        public static void WriteAllText(string msg, string filePath, Encoding encoding = null)
        {
            if (encoding == null)
                File.WriteAllText(filePath, msg);
            else
                File.WriteAllText(filePath, msg, encoding);
        }


        public static void WriteAllText(string msg, string filePath, string fileName, Encoding encoding = null)
        {
            CheckFolder(filePath);
            WriteAllText(msg, $"{filePath}\\{fileName}", encoding);
        }

        /// <summary>
        /// 获得当前绝对路径(最后不会包含斜杠)
        /// </summary>
        /// <param name="strPath">指定的路径</param>
        /// <returns>绝对路径</returns>
        public static string GetMapPath(params string[] strPath)
        {
            var pathSb = "";
            foreach (var str in strPath)
            {
                if (string.IsNullOrWhiteSpace(str))
                    continue;
                var path = str.Replace("/", "\\");
                if (path.StartsWith("\\"))
                {
                    path = path.Substring(path.IndexOf('\\', 1)).TrimStart('\\');
                }
                pathSb = Path.Combine(pathSb, path);
            }
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, pathSb);
        }
    }
}
