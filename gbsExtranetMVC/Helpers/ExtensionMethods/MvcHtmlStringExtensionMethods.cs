using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace gbsExtranetMVC.Helpers.ExtensionMethods
{
    public static class MvcHtmlStringExtensionMethods
    {
        /// <summary>
        /// If you want to bind a field for display only, but need to give it an ID so its value can be passed back to the controller,
        /// use DisplayWithIDFor instead of DisplayFor
        /// </summary>
        public static MvcHtmlString DisplayWithIdFor<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression, string wrapperTag = "span")
        {
            var id = helper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(ExpressionHelper.GetExpressionText(expression));
            return MvcHtmlString.Create(string.Format("<{0} id=\"{1}\">{2}</{0}>", wrapperTag, id, helper.DisplayFor(expression)));
        }

    }
}