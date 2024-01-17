using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Domain.App.Identity;
using Domain.Base;

namespace Domain.App;

public class Review : DomainEntityId
{
    public decimal Rating { get; set; } = default!;

    [MaxLength(512)] public string? ReviewText { get; set; }
    
    [MaxLength(512)]
    public string Name { get; set; } = default!;
    
    public Guid AppUserId { get; set; }
    [JsonIgnore]
    public AppUser AppUser { get; set; } = default!;

    public Guid BeverageId { get; set; }
    [JsonIgnore]
    public Beverage Beverage { get; set; } = default!;

    public ICollection<Comment>? Comments { get; set; }
}