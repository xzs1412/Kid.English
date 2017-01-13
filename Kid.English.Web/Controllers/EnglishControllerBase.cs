using Abp.IdentityFramework;
using Abp.UI;
using Abp.Web.Mvc.Controllers;
using Microsoft.AspNet.Identity;

namespace Kid.English.Web.Controllers
{
    /// <summary>
    /// Derive all Controllers from this class.
    /// </summary>
    public abstract class EnglishControllerBase : AbpController
    {
        protected EnglishControllerBase()
        {
            LocalizationSourceName = EnglishConsts.LocalizationSourceName;
        }

        //IAbpSession Extensions
        public new IAbpSessionExtensions AbpSession { get; set; }

        protected virtual void CheckModelState()
        {
            if (!ModelState.IsValid)
            {
                throw new UserFriendlyException(L("FormIsNotValidMessage"));
            }
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}