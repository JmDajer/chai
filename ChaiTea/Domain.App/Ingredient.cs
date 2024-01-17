using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Domain.Base;

namespace Domain.App;

public class Ingredient : DomainEntityId
{
    [MaxLength(128)] public string Name { get; set; } = default!;

    public Guid IngredientTypeId { get; set; }
    public IngredientType IngredientType { get; set; } = default!;
    
    public ICollection<Beverage>? Beverages { get; set; }
}