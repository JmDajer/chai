using Domain.Base;

namespace Public.DTO.v1;

public class PublicTag : DomainEntityId
{
    public string Name { get; set; } = default!;
    public Guid TagTypeId { get; set; } = default!;
}