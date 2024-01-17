using Domain.Base;

namespace DAL.DTO;

public class DalIngredientType : DomainEntityId
{
    public string Name { get; set; } = default!;

    public ICollection<DalIngredient>? Ingredients { get; set; }
}