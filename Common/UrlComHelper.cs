﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Common
{
    public class UrlComHelper
    {
        private static readonly Encoding Encoding = Encoding.UTF8;

        #region URL的64位编码

        public static string Base64Encrypt(string sourthUrl)
        {
            var eurl = HttpUtility.UrlEncode(sourthUrl);
            eurl = Convert.ToBase64String(Encoding.GetBytes(eurl));
            return eurl;
        }

        #endregion

        #region URL的64位解码

        public static string Base64Decrypt(string eStr)
        {
            if (!IsBase64(eStr))
            {
                return eStr;
            }
            var buffer = Convert.FromBase64String(eStr);
            var sourthUrl = Encoding.GetString(buffer);
            sourthUrl = HttpUtility.UrlDecode(sourthUrl);
            return sourthUrl;
        }

        /// <summary>
        /// 是否是Base64字符串
        /// </summary>
        /// <param name="eStr"></param>
        /// <returns></returns>
        public static bool IsBase64(string eStr)
        {
            if ((eStr.Length % 4) != 0)
            {
                return false;
            }
            if (!Regex.IsMatch(eStr, "^[A-Z0-9/+=]*$", RegexOptions.IgnoreCase))
            {
                return false;
            }
            return true;
        }

        #endregion

        /// <summary>
        /// 添加URL参数
        /// </summary>
        public static string AddParam(string url, string paramName, string value)
        {
            var uri = new Uri(url);
            if (string.IsNullOrEmpty(uri.Query))
            {
                var eval = HttpContext.Current.Server.UrlEncode(value);
                return string.Concat(url, "?" + paramName + "=" + eval);
            }
            else
            {
                var eval = HttpContext.Current.Server.UrlEncode(value);
                return string.Concat(url, "&" + paramName + "=" + eval);
            }
        }

        /// <summary>
        /// 更新URL参数
        /// </summary>
        public static string UpdateParam(string url, string paramName, string value)
        {
            if (url.IndexOf(paramName, StringComparison.Ordinal) <= 0) return AddParam(url, paramName, value);

            var keyWord = paramName + "=";
            var index = url.IndexOf(keyWord, StringComparison.Ordinal) + keyWord.Length;

            var index1 = url.IndexOf("&", index, StringComparison.Ordinal);
            if (index1 == -1)
            {
                url = url.Remove(index, url.Length - index);
                url = string.Concat(url, value);
                return url;
            }
            url = url.Remove(index, index1 - index);
            url = url.Insert(index, value);
            return url;
        }

        #region 分析URL所属的域

        public static void GetDomain(string fromUrl, out string domain, out string subDomain)
        {
            try
            {
                if (fromUrl.IndexOf("的名片", StringComparison.Ordinal) > -1)
                {
                    subDomain = fromUrl;
                    domain = "名片";
                    return;
                }

                var builder = new UriBuilder(fromUrl);
                fromUrl = builder.ToString();

                var u = new Uri(fromUrl);

                if (u.IsWellFormedOriginalString())
                {
                    if (u.IsFile)
                    {
                        subDomain = domain = "客户端本地文件路径";

                    }
                    else
                    {
                        var authority = u.Authority;
                        var ss = u.Authority.Split('.');
                        if (ss.Length == 2)
                        {
                            authority = "www." + authority;
                        }
                        int index = authority.IndexOf('.', 0);
                        domain = authority.Substring(index + 1, authority.Length - index - 1).Replace("comhttp", "com");
                        subDomain = authority.Replace("comhttp", "com");
                        if (ss.Length < 2)
                        {
                            domain = "不明路径";
                            subDomain = "不明路径";
                        }
                    }
                }
                else
                {
                    if (u.IsFile)
                    {
                        subDomain = domain = "客户端本地文件路径";
                    }
                    else
                    {
                        subDomain = domain = "不明路径";
                    }
                }
            }
            catch
            {
                subDomain = domain = "不明路径";
            }
        }

        /// <summary>
        /// 分析 url 字符串中的参数信息
        /// </summary>
        /// <param name="url">输入的 URL</param>
        /// <param name="baseUrl">输出 URL 的基础部分</param>
        /// <param name="nvc">输出分析后得到的 (参数名,参数值) 的集合</param>
        public static void ParseUrl(string url, out string baseUrl, out NameValueCollection nvc)
        {
            if (url == null)
                throw new ArgumentNullException("url");

            nvc = new NameValueCollection();
            baseUrl = "";

            if (url == "")
                return;

            int questionMarkIndex = url.IndexOf('?');

            if (questionMarkIndex == -1)
            {
                baseUrl = url;
                return;
            }
            baseUrl = url.Substring(0, questionMarkIndex);
            if (questionMarkIndex == url.Length - 1)
                return;
            string ps = url.Substring(questionMarkIndex + 1);

            // 开始分析参数对   
            Regex re = new Regex(@"(^|&)?(\w+)=([^&]+)(&|$)?", RegexOptions.Compiled);
            MatchCollection mc = re.Matches(ps);

            foreach (Match m in mc)
            {
                nvc.Add(m.Result("$2").ToLower(), m.Result("$3"));
            }
        }

        #endregion
    }
}
