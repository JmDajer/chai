using Domain.Base;

namespace Public.DTO.v1;

public class PublicIngredient : DomainEntityId
{
    public string Name { get; set; } = default!;
    
    public Guid IngredientTypeId { get; set; } = default!;
    public PublicIngredientType IngredientType { get; set; } = default!;
}