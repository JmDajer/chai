using Domain.Base;

namespace Public.DTO.v1;

public class PublicPicture : DomainEntityId
{
    public string Url { get; set; } = default!;
    public Guid BeverageId { get; set; } = default!;
}
