using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Common
{
    public class SessionHelper
    {

        /// <summary>
        /// 添加Session
        /// </summary>
        /// <param name="strSessionName">Session对象名称</param>
        /// <param name="strValue">Session值</param>
        /// <param name="iExpires">调动有效期（分钟） 默认30分钟</param>
        public static void Add(string strSessionName, object strValue, int? iExpires)
        {
            HttpContext.Current.Session[strSessionName] = strValue;
            HttpContext.Current.Session.Timeout = iExpires ?? 30;
        }

        /// <summary>
        /// 读取某个Session对象值
        /// </summary>
        /// <param name="strSessionName">Session对象名称</param>
        /// <returns>Session对象值</returns>
        public static object Get(string strSessionName)
        {
            return HttpContext.Current.Session[strSessionName];
        }

        /// <summary>
        /// 读取某个字符串Session对象值
        /// </summary>
        /// <param name="strSessionName">Session对象名称</param>
        /// <param name="defaultValue">session不存在时的默认值</param>
        /// <returns>Session对象值</returns>
        public static string GetString(string strSessionName, string defaultValue = "")
        {
            return HttpContext.Current.Session[strSessionName] == null ? defaultValue : HttpContext.Current.Session[strSessionName].ToString();
        }

        /// <summary>
        /// 删除某个Session对象
        /// </summary>
        /// <param name="strSessionName">Session对象名称</param>
        public static void Del(string strSessionName)
        {
            HttpContext.Current.Session[strSessionName] = null;
        }
    }
}
