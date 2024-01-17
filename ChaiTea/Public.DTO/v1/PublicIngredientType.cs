using Domain.Base;

namespace Public.DTO.v1;

public class PublicIngredientType : DomainEntityId
{
    public string Name { get; set; } = default!;
}