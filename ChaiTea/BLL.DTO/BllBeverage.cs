using System.Text.Json.Serialization;
using Domain.Base;

namespace BLL.DTO;

public class BllBeverage : DomainEntityId
{
    public string Name { get; set; } = default!;

    public string? Upc { get; set; }

    public string? Description { get; set; }

    public Guid AppUserId { get; set; } = default!;

    public ICollection<BllPicture>? Pictures { get; set; }
    
    public ICollection<BllBeverage>? ParentBeverages { get; set; }
    
    public ICollection<BllBeverage>? SubBeverages { get; set; }

    public ICollection<BllIngredient>? Ingredients { get; set; }
    
    public ICollection<BllReview>? Reviews { get; set; }
    
    public ICollection<BllTag>? Tags { get; set; }
}