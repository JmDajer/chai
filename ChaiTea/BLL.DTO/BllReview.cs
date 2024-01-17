using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using BLL.DTO.Identity;
using Domain.Base;

namespace BLL.DTO;

public class BllReview : DomainEntityId
{
    public decimal Rating { get; set; } = default!;

    public string? ReviewText { get; set; }

    public string Name { get; set; } = default!;

    public Guid AppUserId { get; set; } = default!;

    public Guid BeverageId { get; set; }
    [JsonIgnore]
    public BllBeverage Beverage { get; set; } = default!;

    public ICollection<BllComment>? Comments { get; set; }
}