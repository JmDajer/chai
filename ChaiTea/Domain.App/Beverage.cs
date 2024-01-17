using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Domain.App.Identity;
using Domain.Base;

namespace Domain.App;

public class Beverage : DomainEntityId
{
    [MaxLength(256)] public string Name { get; set; } = default!;

    [MaxLength(13)] public string? Upc { get; set; }

    [MaxLength(512)] public string? Description { get; set; }
    
    public Guid AppUserId { get; set; }
    public AppUser AppUser { get; set; } = default!;
    
    public ICollection<Picture>? Pictures { get; set; }
    
    [JsonIgnore]
    public ICollection<Beverage>? ParentBeverages { get; set; }
    
    [JsonIgnore]
    public ICollection<Beverage>? SubBeverages { get; set; }
    
    public ICollection<Ingredient>? Ingredients { get; set; }
    
    public ICollection<Review>? Reviews { get; set; }
    
    public ICollection<Tag>? Tags { get; set; }
}