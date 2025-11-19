using Moondesk.Domain.Enums;
using Moondesk.Domain.Models.IoT;

namespace Moondesk.Domain.Interfaces.Repositories;

public interface ICommandRepository
{
    Task<Command?> GetByIdAsync(long id);
    Task<IEnumerable<Command>> GetBySensorIdAsync(long sensorId);
    Task<IEnumerable<Command>> GetByStatusAsync(CommandStatus status);
    Task<IEnumerable<Command>> GetPendingCommandsAsync(string organizationId);
    Task<Command> AddAsync(Command command);
    Task UpdateAsync(Command command);
    Task DeleteAsync(long id);
}
