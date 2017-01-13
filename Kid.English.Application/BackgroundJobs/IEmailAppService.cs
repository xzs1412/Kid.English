using System.Threading.Tasks;
using Abp.Application.Services;

namespace Kid.English.BackgroundJobs
{
    public interface IEmailAppService: IApplicationService
    {
            Task SendEmailAsync(SendEmailInput input);
    }
}