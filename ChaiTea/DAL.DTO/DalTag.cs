using System.Text.Json.Serialization;
using Domain.Base;

namespace DAL.DTO;

public class DalTag : DomainEntityId
{
    public string Name { get; set; } = default!;

    public Guid TagTypeId { get; set; }
    public DalTagType TagType { get; set; } = default!;

    [JsonIgnore]
    public ICollection<DalBeverage>? Beverages { get; set; }
}