using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Domain.Base;

namespace DAL.DTO;

public class DalBeverage : DomainEntityId
{
    public string Name { get; set; } = default!;

    public string? Upc { get; set; }

    public string? Description { get; set; }

    public Guid AppUserId { get; set; } = default!;

    public ICollection<DalPicture>? Pictures { get; set; }
    
    public ICollection<DalBeverage>? ParentBeverages { get; set; }
    
    public ICollection<DalBeverage>? SubBeverages { get; set; }

    public ICollection<DalIngredient>? Ingredients { get; set; }
    
    public ICollection<DalReview>? Reviews { get; set; }
    
    public ICollection<DalTag>? Tags { get; set; }
}