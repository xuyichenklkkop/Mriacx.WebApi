using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Mriacx.Utility.Common
{
    /// <summary>
    /// 生产随机Id
    /// </summary>
    public static class GuidCreator
    {
        /// <summary>
        /// 生成前缀+8位随机数
        /// </summary>
        /// <param name="profix"></param>
        /// <returns></returns>
        public static string GetUniqueKey(string profix)
        {
            int maxSize = 8;
            char[] chars = new char[62];
            string a;
            a = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            chars = a.ToCharArray();
            int size = maxSize;
            byte[] data = new byte[1];
            RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
            crypto.GetNonZeroBytes(data);
            size = maxSize;
            data = new byte[size];
            crypto.GetNonZeroBytes(data);
            StringBuilder result = new StringBuilder(size);
            foreach (byte b in data)
            { result.Append(chars[b % (chars.Length - 1)]); }
             return profix +result.ToString();
        }
    }
}
