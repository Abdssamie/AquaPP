using Moondesk.Domain.Models.Network;
using Moondesk.Domain.Enums;

namespace Moondesk.Domain.Interfaces;

public interface IConnectionCredentialService
{
    Task<ConnectionCredential?> GetCredentialAsync(long id, string organizationId, string requestingUserId);
    Task<ConnectionCredential> CreateCredentialAsync(string name, string endpointUri, string organizationId, 
        Protocol protocol, string requestingUserId, string username = "", string password = "");
    Task<ConnectionCredential> UpdateCredentialAsync(ConnectionCredential credential, string organizationId, string requestingUserId);
    Task DeleteCredentialAsync(long id, string organizationId, string requestingUserId);
    Task<IEnumerable<ConnectionCredential>> GetOrganizationCredentialsAsync(string organizationId, string requestingUserId);
    Task<bool> TestConnectionAsync(long credentialId, string organizationId, string requestingUserId);
    Task<string> DecryptPasswordAsync(long credentialId, string organizationId, string requestingUserId);
    Task ActivateCredentialAsync(long id, string organizationId, string requestingUserId);
    Task DeactivateCredentialAsync(long id, string organizationId, string requestingUserId);
}
