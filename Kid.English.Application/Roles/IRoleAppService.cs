using System.Threading.Tasks;
using Abp.Application.Services;
using Kid.English.Roles.Dto;

namespace Kid.English.Roles
{
    public interface IRoleAppService : IApplicationService
    {
        Task UpdateRolePermissions(UpdateRolePermissionsInput input);
    }
}
