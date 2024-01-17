using Domain.Base;

namespace Public.DTO.v1;

public class PublicTagType : DomainEntityId
{
    public string Name { get; set; } = default!;
}