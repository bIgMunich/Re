using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace System.Web.Mvc
{
    public static class HtmlHelpers
    {
        public static HtmlString ShowPageNavigate(this HtmlHelper HtmlHelper, PagerInfo paging, int btnNumber = 5, string paramName = "page")
        {
            if (HtmlHelper.ViewContext.RequestContext.HttpContext.Request.Url == null) return null;

            var urlStr = HtmlHelper.ViewContext.RequestContext.HttpContext.Request.Url.ToString();

            var pageIndex = paging.CurrenetPageIndex; //当前页数
            var pageCount = paging.PageCount; //总页数

            if (pageCount <= 1) return null;

            var outPut = new StringBuilder();
            outPut.AppendFormat("<ul class='pagination'>");

            if (pageIndex > 1)
            {
                var urlTo = UrlComHelper.UpdateParam(urlStr, paramName, (pageIndex - 1).ToString());
                var firstPage = UrlComHelper.UpdateParam(urlStr, paramName, "1");
                outPut.AppendFormat("<li class='firstPage'><a href='{0}'><span>首页</span></a></li>", firstPage);
                outPut.AppendFormat("<li class='prev'><a href='{0}'>← <span>上一页</span></a></li>", urlTo);
            }

            const int index = 5;
            for (var i = 0; i < btnNumber; i++)
            {
                if ((pageIndex + i - index) < 1 || (pageIndex + i - index) > pageCount) continue;

                var urlTo = UrlComHelper.UpdateParam(urlStr, paramName, (pageIndex + i - index).ToString());

                outPut.AppendFormat(
                    pageIndex + i - index == pageIndex
                        ? "<li class='active'><a href='{0}'>{1}</a></li>"
                        : "<li><a href='{0}'>{1}</a></li>", urlTo, pageIndex + i - index);
            }

            if (pageIndex < pageCount)
            {
                var urlTo = UrlComHelper.UpdateParam(urlStr, paramName, (pageIndex + 1).ToString());
                var trailerPage = UrlComHelper.UpdateParam(urlStr, paramName, pageCount.ToString());

                outPut.AppendFormat("<li class='next'><a href='{0}'><span>下一页</span>→</a></li>", urlTo);
                outPut.AppendFormat("<li class='trailerPage'><a href='{0}'> <span>尾页</span></a></li>", trailerPage);
            }
            outPut.AppendFormat("</ul>");

            return new HtmlString(outPut.ToString());
        }
    }
}
