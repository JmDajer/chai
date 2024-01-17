using System.Text.Json.Serialization;
using Domain.Base;

namespace DAL.DTO;

public class DalReview : DomainEntityId
{
    public decimal Rating { get; set; } = default!;

    public string? ReviewText { get; set; }
    
    public string Name { get; set; } = default!;
    
    public Guid AppUserId { get; set; } = default!;

    public Guid BeverageId { get; set; }
    [JsonIgnore]
    public DalBeverage Beverage { get; set; } = default!;

    public ICollection<DalComment>? Comments { get; set; }
}