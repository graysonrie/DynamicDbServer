using System.ComponentModel.DataAnnotations;

namespace DynamicDbServer.src.Features.DynamicDbBase.Models;

public class JsonData
{
    [Key]
    public Guid Guid { get; set; }
    public required string Key { get; set; }
    public required string Value { get; set; }
}