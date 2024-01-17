using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace BLL.DTO;

public class BllIngredientType : DomainEntityId
{
    public string Name { get; set; } = default!;

    public ICollection<BllIngredient>? Ingredients { get; set; }
}