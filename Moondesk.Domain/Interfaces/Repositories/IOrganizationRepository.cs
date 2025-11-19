using Moondesk.Domain.Models;

namespace Moondesk.Domain.Interfaces.Repositories;

public interface IOrganizationRepository
{
    Task<Organization?> GetByIdAsync(string id);
    Task<Organization?> GetByNameAsync(string name);
    Task<IEnumerable<Organization>> GetByOwnerIdAsync(string ownerId);
    Task<IEnumerable<Organization>> GetAllAsync();
    Task<Organization> CreateAsync(Organization organization);
    Task<Organization> UpdateAsync(Organization organization);
    Task DeleteAsync(string id);
    Task<bool> ExistsAsync(string id);
}
