using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Common
{
    public class JsScriptHelper
    {
        /// <summary>
        /// 弹出JavaScript小窗口
        /// </summary>
        public static void Alert(string message)
        {
            #region
            string js = @"<Script language='JavaScript'>
                    alert('" + message + "');</Script>";
            HttpContext.Current.Response.Write(js);
            //HttpContext.Current.Response.End();
            #endregion
        }

        public static void Alert(string message, bool isEnd)
        {
            Alert(message);
            if (isEnd)
            {
                HttpContext.Current.Response.End();
            }
        }

        /// <summary>
        /// 弹出JavaScript小窗口
        /// </summary>
        /// <param name="message"></param>
        public static void AlertAndGoBack(string message)
        {
            #region
            var js = @"<Script language='JavaScript'>
                    alert('" + message + "'); history.go(-1);</Script>";
            HttpContext.Current.Response.Write(js);
            HttpContext.Current.Response.End();

            #endregion
        }

        /// <summary>
        /// 用RegisterStartupScript方式弹出提示框
        /// </summary>
        /// <param name="page"></param>
        /// <param name="message">信息</param>
        //public static void AlertWithRegister(Page page, string message)
        //{
        //    page.RegisterStartupScript("", "<script>alert('" + message + "');</script>");
        //}

        /// <summary>
        /// 弹出消息框并且转向到新的URL
        /// </summary>
        /// <param name="message">消息内容</param>
        /// <param name="toUrl">连接地址</param>
        public static void AlertAndRedirect(string message, string toUrl)
        {
            #region
            string js = "<script language=javascript>alert('{0}');window.location.replace('{1}')</script>";
            //string js = "<script language=javascript>alert('{0}');top.location.href='{1}'</script>";
            HttpContext.Current.Response.Write(string.Format(js, message, toUrl));
            HttpContext.Current.Response.End();
            #endregion
        }

        /// <summary>
        /// 弹出消息框并且转向到新的URL(使用window.open，防止后退按钮)
        /// </summary>
        /// <param name="message"></param>
        /// <param name="toUrl"></param>
        public static void AlertAndRedirectByOpen(string message, string toUrl)
        {
            #region
            string js = "<script language=javascript>alert('{0}');window.open('{1}','_self')</script>";
            HttpContext.Current.Response.Write(string.Format(js, message, toUrl));
            HttpContext.Current.Response.End();
            #endregion

        }

        /// <summary>
        /// 弹出消息框并且转向到新的URL
        /// </summary>
        /// <param name="message">消息内容</param>
        /// <param name="toUrl">连接地址</param>
        public static void AlertAndRedirectTop(string message, string toUrl)
        {
            #region
            string js = "<script language=javascript>alert('{0}');top.location.replace('{1}')</script>";
            //string js = "<script language=javascript>alert('{0}');top.location.href='{1}'</script>";
            HttpContext.Current.Response.Write(string.Format(js, message, toUrl));
            HttpContext.Current.Response.End();
            #endregion
        }

        /// <summary>
        /// 弹出消息框并且转向到新的URL
        /// </summary>
        /// <param name="message">消息内容</param>
        /// <param name="toUrl">连接地址</param>
        public static void AlertAndRedirectTop(string toUrl)
        {
            #region
            string js = "<script language=javascript>top.location.replace('{0}')</script>";
            //string js = "<script language=javascript>alert('{0}');top.location.href='{1}'</script>";
            HttpContext.Current.Response.Write(string.Format(js, toUrl));
            HttpContext.Current.Response.End();
            #endregion
        }

        /// <summary>
        /// 回到历史页面
        /// </summary>
        /// <param name="value">-1/1</param>
        public static void GoHistory(int value)
        {
            #region
            string js = @"<Script language='JavaScript'>
                    history.go({0});  
                  </Script>";
            HttpContext.Current.Response.Write(string.Format(js, value));
            #endregion
        }

        /// <summary>
        /// 关闭当前窗口
        /// </summary>
        public static void CloseWindow()
        {
            #region
            string js = @"<Script language='JavaScript'>
                    parent.opener=null;window.close();  
                  </Script>";
            HttpContext.Current.Response.Write(js);
            HttpContext.Current.Response.End();
            #endregion
        }

        /// <summary>
        /// 刷新父窗口

        /// </summary>
        public static void RefreshParent(string url)
        {
            #region
            string js = @"<Script language='JavaScript'>
                    window.opener.location.href='" + url + "';window.close();</Script>";
            HttpContext.Current.Response.Write(js);
            #endregion
        }


        /// <summary>
        /// 刷新打开窗口
        /// </summary>
        public static void RefreshOpener()
        {
            #region
            string js = @"<Script language='JavaScript'>
                    opener.location.reload();
                  </Script>";
            HttpContext.Current.Response.Write(js);
            #endregion
        }


        /// <summary>
        /// 打开指定大小的新窗体
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="width">宽</param>
        /// <param name="heigth">高</param>
        /// <param name="top">头位置</param>
        /// <param name="left">左位置</param>
        public static void OpenWebFormSize(string url, int width, int heigth, int top, int left)
        {
            #region
            string js = @"<Script language='JavaScript'>window.open('" + url + @"','','height=" + heigth + ",width=" + width + ",top=" + top + ",left=" + left + ",location=no,menubar=no,resizable=yes,scrollbars=yes,status=yes,titlebar=no,toolbar=no,directories=no');</Script>";

            HttpContext.Current.Response.Write(js);
            #endregion
        }




        /// <summary>
        /// 转向Url制定的页面

        /// </summary>
        /// <param name="url">连接地址</param>
        public static void JavaScriptLocationHref(string url)
        {
            #region
            string js = @"<Script language='JavaScript'>
                    window.location.replace('{0}');
                  </Script>";
            js = string.Format(js, url);
            HttpContext.Current.Response.Write(js);
            #endregion
        }

        /// <summary>
        /// 打开指定大小位置的模式对话框
        /// </summary>
        /// <param name="webFormUrl">连接地址</param>
        /// <param name="width">宽</param>
        /// <param name="height">高</param>
        /// <param name="top">距离上位置</param>
        /// <param name="left">距离左位置</param>
        public static void ShowModalDialogWindow(string webFormUrl, int width, int height, int top, int left)
        {
            #region
            string features = "dialogWidth:" + width.ToString() + "px"
                + ";dialogHeight:" + height.ToString() + "px"
                + ";dialogLeft:" + left.ToString() + "px"
                + ";dialogTop:" + top.ToString() + "px"
                + ";center:yes;help=no;resizable:no;status:no;scroll=yes";
            ShowModalDialogWindow(webFormUrl, features);
            #endregion
        }

        public static void ShowModalDialogWindow(string webFormUrl, string features)
        {
            string js = ShowModalDialogJavascript(webFormUrl, features);
            HttpContext.Current.Response.Write(js);
        }

        public static string ShowModalDialogJavascript(string webFormUrl, string features)
        {
            #region
            string js = @"<script language=javascript>							
							showModalDialog('" + webFormUrl + "','','" + features + "');</script>";
            return js;
            #endregion
        }

    }
}
