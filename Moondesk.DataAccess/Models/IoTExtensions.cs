using System.ComponentModel.DataAnnotations;
using Moondesk.Domain.Models.IoT;

namespace Moondesk.DataAccess.Models;

public class AssetExtended : Asset
{
    [MaxLength(30)]
    public required string OrganizationId { get; set; }
}

public class SensorExtended : Sensor
{
    [MaxLength(30)]
    public required string OrganizationId { get; set; }
}

public class ReadingExtended : Reading
{
    [MaxLength(30)]
    public required string OrganizationId { get; set; }
}

public class AlertExtended : Alert
{
    [MaxLength(30)]
    public required string OrganizationId { get; set; }
}

public class CommandExtended : Command
{
    [MaxLength(30)]
    public required string OrganizationId { get; set; }
}
