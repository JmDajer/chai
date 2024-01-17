using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Domain.Base;

namespace Domain.App;

public class Tag : DomainEntityId
{
    [MaxLength(128)]
    public string Name { get; set; } = default!;

    public Guid TagTypeId { get; set; }
    public TagType TagType { get; set; } = default!;

    [JsonIgnore]
    public ICollection<Beverage>? Beverages { get; set; }
}