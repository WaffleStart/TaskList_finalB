using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TaskList.Views.Helpers
{
    public static class HtmlHelper
    {
        public static IHtmlContent MaskPassword(this IHtmlHelper htmlHelper, string password) => new HtmlString(new string('*', password.Length));
    }
}
