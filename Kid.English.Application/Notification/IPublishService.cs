using System.Threading.Tasks;
using Abp.Application.Services;

namespace Kid.English.Notification
{
    public interface IPublishService : IApplicationService
    {
        Task Publish_LowDisk(int remainingDiskInMb);

        Task KidPublishMethod(string targetUserName);
    }
}