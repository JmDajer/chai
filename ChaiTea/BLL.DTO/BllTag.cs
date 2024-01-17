using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Domain.Base;

namespace BLL.DTO;

public class BllTag : DomainEntityId
{
    public string Name { get; set; } = default!;

    public Guid TagTypeId { get; set; }
    public BllTagType TagType { get; set; } = default!;
    
    [JsonIgnore]
    public ICollection<BllBeverage>? Beverages { get; set; }
}