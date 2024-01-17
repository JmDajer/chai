using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace BLL.DTO;

public class BllIngredient : DomainEntityId
{
    public string Name { get; set; } = default!;

    public Guid IngredientTypeId { get; set; }
    public BllIngredientType IngredientType { get; set; } = default!;
    
    public ICollection<BllBeverage>? Beverages { get; set; }
}