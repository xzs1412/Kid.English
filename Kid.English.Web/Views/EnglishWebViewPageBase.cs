using Abp.Web.Mvc.Views;

namespace Kid.English.Web.Views
{
    public abstract class EnglishWebViewPageBase : EnglishWebViewPageBase<dynamic>
    {

    }

    public abstract class EnglishWebViewPageBase<TModel> : AbpWebViewPage<TModel>
    {
        protected EnglishWebViewPageBase()
        {
            LocalizationSourceName = EnglishConsts.LocalizationSourceName;
        }
    }
}