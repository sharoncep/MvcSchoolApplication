using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SchoolApplication.UtilityClass
{
    public static class MyHtmlHelpers
    {

        public static MvcHtmlString Image(this HtmlHelper helper, string name, string url)
        {
            return Image(helper, name, url, null);
        }

        public static MvcHtmlString Image(this HtmlHelper helper, string name, string url, object htmlAttributes)
        {
            var tagBuilder = new TagBuilder("img");
            tagBuilder.GenerateId(name);

            tagBuilder.Attributes["src"] = new UrlHelper(helper.ViewContext.RequestContext).Content(url);
            tagBuilder.MergeAttributes(new RouteValueDictionary(htmlAttributes));

            return MvcHtmlString.Create(tagBuilder.ToString(TagRenderMode.SelfClosing));
        }
    }
}