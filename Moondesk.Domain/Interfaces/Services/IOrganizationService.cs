using Moondesk.Domain.Models;
using Moondesk.Domain.Enums;

namespace Moondesk.Domain.Interfaces.Services;

public interface IOrganizationService
{
    Task<Organization?> GetOrganizationAsync(string id);
    Task<Organization> CreateOrganizationAsync(string name, string ownerId);
    Task<Organization> UpdateOrganizationAsync(Organization organization);
    Task DeleteOrganizationAsync(string id);
    Task<OrganizationMembership> AddMemberAsync(string organizationId, string userId, UserRole role = UserRole.User);
    Task RemoveMemberAsync(string organizationId, string userId);
    Task<OrganizationMembership> UpdateMemberRoleAsync(string organizationId, string userId, UserRole role);
    Task<IEnumerable<OrganizationMembership>> GetMembersAsync(string organizationId);
    Task<bool> IsUserMemberAsync(string organizationId, string userId);
    Task<bool> IsUserOwnerAsync(string organizationId, string userId);
}
