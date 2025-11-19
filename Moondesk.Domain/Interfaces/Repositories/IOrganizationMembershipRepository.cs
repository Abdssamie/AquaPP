using Moondesk.Domain.Models;
using Moondesk.Domain.Enums;

namespace Moondesk.Domain.Interfaces.Repositories;

public interface IOrganizationMembershipRepository
{
    Task<OrganizationMembership?> GetByIdAsync(string userId, string organizationId);
    Task<IEnumerable<OrganizationMembership>> GetByUserIdAsync(string userId);
    Task<IEnumerable<OrganizationMembership>> GetByOrganizationIdAsync(string organizationId);
    Task<IEnumerable<OrganizationMembership>> GetByRoleAsync(UserRole role);
    Task<OrganizationMembership> CreateAsync(OrganizationMembership membership);
    Task<OrganizationMembership> UpdateAsync(OrganizationMembership membership);
    Task DeleteAsync(string userId, string organizationId);
    Task<bool> ExistsAsync(string userId, string organizationId);
}
