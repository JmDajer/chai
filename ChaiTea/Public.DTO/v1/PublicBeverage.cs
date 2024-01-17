using System.Text.Json.Serialization;
using Domain.Base;

namespace Public.DTO.v1;

public class PublicBeverage : DomainEntityId
{
    public string Name { get; set; } = default!;

    public string? Upc { get; set; }

    public string? Description { get; set; }

    public Guid AppUserId { get; set; } = default!;
    
    public ICollection<PublicBeverage>? ParentBeverages { get; set; }
    
    public ICollection<PublicBeverage>? SubBeverages { get; set; }
    
    public IEnumerable<PublicPicture>? Pictures { get; set; }
    
    public IEnumerable<PublicIngredient>? Ingredients { get; set; }
    
    public IEnumerable<PublicReview>? Reviews { get; set; }
    
    public IEnumerable<PublicTag>? Tags { get; set; }
}