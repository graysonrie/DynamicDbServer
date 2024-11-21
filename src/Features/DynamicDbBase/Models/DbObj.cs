using System.ComponentModel.DataAnnotations;
namespace DynamicDbServer.src.Features.DynamicDbBase.Models;

/// <summary>
/// Where <c>Ownerid<c>
/// </summary>
public class DbObj
{
    [Key]
    public Guid Guid { get; set; }
    public Guid ParentId { get; set; }
    public required Guid OwnerId { get; set; }
    public required Dictionary<string, object> Data { get; set; }
    public required string Group { get; set; }
}