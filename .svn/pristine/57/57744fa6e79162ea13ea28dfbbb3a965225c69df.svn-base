using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YoYoStudio.ManagementPortal.Models;

namespace YoYoStudio.ManagementPortal
{
    public static class Helper
    {
        public static HtmlString Image(this HtmlHelper html, int id)
        {
            var urlHelper = ((Controller)html.ViewContext.Controller).Url;
            var imageUrl = urlHelper.Action("GetImage", "Home", new { id = id });
            var img = new TagBuilder("img");
            img.MergeAttribute("src", imageUrl);
            return new HtmlString(img.ToString(TagRenderMode.SelfClosing));
        }
    }
}