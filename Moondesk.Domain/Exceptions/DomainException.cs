namespace Moondesk.Domain.Exceptions;

public class DomainException : Exception
{
    public DomainException(string message) : base(message) { }
    public DomainException(string message, Exception innerException) : base(message, innerException) { }
}

public class UserNotFoundException : DomainException
{
    public UserNotFoundException(string userId) : base($"User with ID '{userId}' not found.") { }
}

public class OrganizationNotFoundException : DomainException
{
    public OrganizationNotFoundException(string organizationId) : base($"Organization with ID '{organizationId}' not found.") { }
}

public class ConnectionCredentialNotFoundException : DomainException
{
    public ConnectionCredentialNotFoundException(long credentialId) : base($"Connection credential with ID '{credentialId}' not found.") { }
}

public class DuplicateUsernameException : DomainException
{
    public DuplicateUsernameException(string username) : base($"Username '{username}' is already taken.") { }
}

public class DuplicateEmailException : DomainException
{
    public DuplicateEmailException(string email) : base($"Email '{email}' is already registered.") { }
}

public class DuplicateConnectionNameException : DomainException
{
    public DuplicateConnectionNameException(string name) : base($"Connection name '{name}' already exists in this organization.") { }
}

public class UnauthorizedOperationException : DomainException
{
    public UnauthorizedOperationException(string operation) : base($"Unauthorized to perform operation: {operation}") { }
}

public class NotOrganizationOwnerException : DomainException
{
    public NotOrganizationOwnerException(string userId, string organizationId) : base($"User '{userId}' is not the owner of organization '{organizationId}'.") { }
}

public class ConnectionTestFailedException : DomainException
{
    public ConnectionTestFailedException(string endpoint) : base($"Failed to connect to endpoint: {endpoint}") { }
}
