using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Framework.TagHelper
{
    public class PersianDateTagHelper : InputTagHelper
    {
        public PersianDateTagHelper(IHtmlGenerator generator) : base(generator)
        {
            InputTypeName = "text";
        }

        //   Can be passed via<email mail-to= "..." />.

        //  PascalCase gets translated into kebab-case.
        public string DateFormat { get; set; } = "yyyy-MM-dd";
        public bool? IsGregorian { get; set; } = null;
        public bool DisableBeforeToday { get; set; } = false;
        public bool EnableTimePicker { get; set; } = false;


        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            IsGregorian = IsGregorian ?? (Thread.CurrentThread.CurrentCulture.Name.Contains("en"));


            output.TagName = "input";
            await base.ProcessAsync(context, output);
            output.PostElement.SetHtmlContent($@"

<script>
$('#{For.Name}').MdPersianDateTimePicker({{
    targetTextSelector: '#{For.Name}',
    dateFormat: '{DateFormat}',
    isGregorian: {IsGregorian.ToString().ToLower()},
    enableTimePicker: {EnableTimePicker.ToString().ToLower()},
    disableBeforeToday: {DisableBeforeToday.ToString().ToLower()},
  }});

</script>");
        }
    }
}