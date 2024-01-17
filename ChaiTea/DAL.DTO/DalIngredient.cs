using Domain.Base;

namespace DAL.DTO;

public class DalIngredient : DomainEntityId
{
    public string Name { get; set; } = default!;

    public Guid IngredientTypeId { get; set; }
    public DalIngredientType IngredientType { get; set; } = default!;

    public ICollection<DalBeverage>? Beverages { get; set; }
}