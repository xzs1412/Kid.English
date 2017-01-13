using System.Threading.Tasks;
using Abp.BackgroundJobs;

namespace Kid.English.BackgroundJobs
{
    public class EmailAppService : EnglishAppServiceBase, IEmailAppService
    {
        private readonly IBackgroundJobManager _backgroundJobManager;

        public EmailAppService(IBackgroundJobManager backgroundJobManager)
        {
            _backgroundJobManager = backgroundJobManager;
        }

        public async Task SendEmailAsync(SendEmailInput input)
        {
            await _backgroundJobManager.EnqueueAsync<SimpleSendEmailJob, SimpleSendEmailJobArgs>(
                new SimpleSendEmailJobArgs
                {
                    Subject = input.Subject,
                    Body = input.Body,
                    SenderUserId = input.SenderUserId,
                    TargetUserId = input.TargetUserId
                });
        }
    }
}