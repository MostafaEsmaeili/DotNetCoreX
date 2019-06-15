//namespace Framework.TagHelper
//{
//    using Microsoft.AspNetCore.Razor.TagHelpers;
//    using System.Threading.Tasks;

//    public class PersianDateTagHelper : TagHelper
//    {
//        private const string EmailDomain = "contoso.com";

//         Can be passed via <email mail-to="..." />. 
//         PascalCase gets translated into kebab-case.
//        public string DateFormat { get; set; } = "yyyy-MM-dd";
//        public string Value { get; set; }
//        public bool DisableBeforeToday { get; set; } = false;
//        public string @Class { get; set; }
//        public string Placeholder { get; set; }

//        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
//        {
//            output.TagName = "input"; // Replaces <email> with <a> tag
//            var content = await output.GetChildContentAsync();

//            output.Attributes.SetAttribute("class", Class);
//            output.Attributes.SetAttribute("placeholder", Placeholder);
//            output.PostContent.con

//            output.Content.SetHtmlContent(
//                $@"<ul><li><strong>Version:</strong> {Info.Version}</li>
//<li><strong>Copyright Year:</strong> {Info.CopyrightYear}</li>
//<li><strong>Approved:</strong> {Info.Approved}</li>
//<li><strong>Number of tags to show:</strong> {Info.TagsToShow}</li></ul>");
//        }

//    }
//}