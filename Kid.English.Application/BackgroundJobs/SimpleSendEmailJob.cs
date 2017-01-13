using Abp.BackgroundJobs;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Net.Mail;
using Kid.English.Users;

namespace Kid.English.BackgroundJobs
{
    public class SimpleSendEmailJob:BackgroundJob<SimpleSendEmailJobArgs>,ITransientDependency
    {
        private readonly IRepository<User, long> _useRepository;
        private readonly IEmailSender _emailSender;

        public SimpleSendEmailJob(IRepository<User,long> userRepository,IEmailSender emailSender)
        {
            _useRepository = userRepository;
            _emailSender = emailSender;
        }

        public override void Execute(SimpleSendEmailJobArgs args)
        {
            var senderUser = "66970551@qq.com";// _useRepository.Get(args.SenderUserId2);
            var targetUser ="66970551@qq.com";// _useRepository.Get(args.TargetUserId);

            _emailSender.Send(senderUser,targetUser,args.Subject,args.Body);
        }
    }
}