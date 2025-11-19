using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moondesk.DataAccess.Data;
using Moondesk.Domain.Enums;
using Moondesk.Domain.Exceptions;
using Moondesk.Domain.Interfaces.Repositories;
using Moondesk.Domain.Models;

namespace Moondesk.DataAccess.Repositories;

public class OrganizationMembershipRepository : IOrganizationMembershipRepository
{
    private readonly MoondeskDbContext _context;
    private readonly ILogger<OrganizationMembershipRepository> _logger;

    public OrganizationMembershipRepository(MoondeskDbContext context, ILogger<OrganizationMembershipRepository> logger)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<OrganizationMembership?> GetByIdAsync(string userId, string organizationId)
    {
        if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(organizationId))
            throw new ArgumentException("User ID and Organization ID cannot be null or empty");

        try
        {
            return await _context.OrganizationMemberships
                .AsNoTracking()
                .Include(m => m.User)
                .Include(m => m.Organization)
                .FirstOrDefaultAsync(m => m.UserId == userId && m.OrganizationId == organizationId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving membership for user {UserId} in org {OrganizationId}", userId, organizationId);
            throw;
        }
    }

    public async Task<IEnumerable<OrganizationMembership>> GetByUserIdAsync(string userId)
    {
        if (string.IsNullOrWhiteSpace(userId))
            throw new ArgumentException("User ID cannot be null or empty", nameof(userId));

        try
        {
            return await _context.OrganizationMemberships
                .AsNoTracking()
                .Include(m => m.Organization)
                .Where(m => m.UserId == userId)
                .OrderBy(m => m.JoinedAt)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving memberships for user {UserId}", userId);
            throw;
        }
    }

    public async Task<IEnumerable<OrganizationMembership>> GetByOrganizationIdAsync(string organizationId)
    {
        if (string.IsNullOrWhiteSpace(organizationId))
            throw new ArgumentException("Organization ID cannot be null or empty", nameof(organizationId));

        try
        {
            return await _context.OrganizationMemberships
                .AsNoTracking()
                .Include(m => m.User)
                .Where(m => m.OrganizationId == organizationId)
                .OrderBy(m => m.JoinedAt)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving memberships for organization {OrganizationId}", organizationId);
            throw;
        }
    }

    public async Task<IEnumerable<OrganizationMembership>> GetByRoleAsync(UserRole role)
    {
        try
        {
            return await _context.OrganizationMemberships
                .AsNoTracking()
                .Include(m => m.User)
                .Include(m => m.Organization)
                .Where(m => m.Role == role)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving memberships by role {Role}", role);
            throw;
        }
    }

    public async Task<OrganizationMembership> CreateAsync(OrganizationMembership membership)
    {
        if (membership == null)
            throw new ArgumentNullException(nameof(membership));

        try
        {
            if (await ExistsAsync(membership.UserId, membership.OrganizationId))
                throw new DomainException($"Membership already exists for user {membership.UserId} in organization {membership.OrganizationId}");

            _logger.LogInformation("Creating membership for user {UserId} in org {OrganizationId} with role {Role}", 
                membership.UserId, membership.OrganizationId, membership.Role);

            _context.OrganizationMemberships.Add(membership);
            await _context.SaveChangesAsync();

            return membership;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating membership for user {UserId}", membership?.UserId);
            throw;
        }
    }

    public async Task<OrganizationMembership> UpdateAsync(OrganizationMembership membership)
    {
        if (membership == null)
            throw new ArgumentNullException(nameof(membership));

        try
        {
            var existing = await _context.OrganizationMemberships
                .FindAsync(membership.UserId, membership.OrganizationId);
            
            if (existing == null)
                throw new DomainException($"Membership not found for user {membership.UserId} in organization {membership.OrganizationId}");

            _context.Entry(existing).CurrentValues.SetValues(membership);
            await _context.SaveChangesAsync();

            return membership;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating membership for user {UserId}", membership?.UserId);
            throw;
        }
    }

    public async Task DeleteAsync(string userId, string organizationId)
    {
        if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(organizationId))
            throw new ArgumentException("User ID and Organization ID cannot be null or empty");

        try
        {
            var membership = await _context.OrganizationMemberships
                .FindAsync(userId, organizationId);
            
            if (membership == null)
                throw new DomainException($"Membership not found for user {userId} in organization {organizationId}");

            _logger.LogWarning("Deleting membership for user {UserId} from org {OrganizationId}", userId, organizationId);

            _context.OrganizationMemberships.Remove(membership);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting membership for user {UserId}", userId);
            throw;
        }
    }

    public async Task<bool> ExistsAsync(string userId, string organizationId)
    {
        if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(organizationId))
            return false;

        try
        {
            return await _context.OrganizationMemberships
                .AsNoTracking()
                .AnyAsync(m => m.UserId == userId && m.OrganizationId == organizationId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error checking membership existence for user {UserId}", userId);
            throw;
        }
    }
}
