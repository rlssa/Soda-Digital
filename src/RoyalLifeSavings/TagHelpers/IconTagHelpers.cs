using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace RoyalLifeSavings.TagHelpers
{
    [HtmlTargetElement("icon")]
    public partial class IconTagHelper : TagHelper
    {
        static readonly char[] _capsAndNumbers = "1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
        private readonly IHtmlHelper _html;

        public IconTagHelper(IHtmlHelper html)
        {
            _html = html;
        }

        [HtmlAttributeName("icon")]
        public Icon Icon { get; set; }

        [HtmlAttributeName("class")]
        public string Class { get; set; }

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            (_html as IViewContextAware).Contextualize(ViewContext);
            var icon = Icon.ToString();
            var iconDirectory = "Icons/";

            // One does not simply write regex.

            var partialNameBuilder = new StringBuilder();

            for (var i = 0; i < icon.Length; i++)
            {
                var c = icon[i];
                if (_capsAndNumbers.Contains(c))
                {
                    partialNameBuilder.Append($"{c}".ToLowerInvariant());
                }
                else
                {
                    partialNameBuilder.Append($"{c}");
                }
                if (i < icon.Length - 1 && _capsAndNumbers.Contains(icon[i + 1]))
                {
                    partialNameBuilder.Append("-");
                }
            }
            var partialName = partialNameBuilder.ToString();
            var iconMarkup = await _html.PartialAsync(iconDirectory + partialName, Class, ViewContext.ViewData);
            output.PostContent.SetHtmlContent(iconMarkup);
            output.TagName = null;
            output.TagMode = TagMode.SelfClosing;
        }
    }
}
