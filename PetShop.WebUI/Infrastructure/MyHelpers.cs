using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PetShop.WebUI.Infrastructure
{
    /// <summary>
    /// Extention for HtmlHelper
    /// </summary>
    public static class MyHelpers
    {
        /// <summary>
        /// Extends class HtmlHelper by works with images.
        /// </summary>
        /// <param name="helper">Exteded class.</param>
        /// <param name="url">Path to image.</param>
        /// <param name="htmlAttributes">Html attributes for image.</param>
        /// <returns>Html code.</returns>
        public static MvcHtmlString Image(this HtmlHelper helper,
                            string url,
                            object htmlAttributes)
        {
            return Image(helper, url, null, htmlAttributes);
        }
        /// <summary>
        /// Extends class HtmlHelper by works with images.
        /// </summary>
        /// <param name="helper">Exteded class.</param>
        /// <param name="url">Path to image.</param>
        /// <param name="altText">Text for images.</param>
        /// <param name="htmlAttributes">Html attributes for image.</param>
        /// <returns>Html code.</returns>
        public static MvcHtmlString Image(this HtmlHelper helper,
                                        string url,
                                        string altText,
                                        object htmlAttributes)
        {
            TagBuilder builder = new TagBuilder("image");

            var path = url.Split('?');
            string pathExtra = "";
            if (path.Length > 1)
            {
                pathExtra = "?" + path[1];
            }
            builder.Attributes.Add("src", VirtualPathUtility.ToAbsolute(path[0]) + pathExtra);
            builder.Attributes.Add("alt", altText);
            builder.MergeAttributes(new RouteValueDictionary(htmlAttributes));
            return MvcHtmlString.Create(builder.ToString(TagRenderMode.SelfClosing));
        }
    }
}