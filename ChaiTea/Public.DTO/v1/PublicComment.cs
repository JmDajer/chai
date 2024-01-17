using Domain.Base;

namespace Public.DTO.v1;

public class PublicComment : DomainEntityId
{
    public string Text { get; set; } = default!;
    
    public string Name { get; set; } = default!;
    
    public Guid ReviewId { get; set; } = default!;
    public Guid AppUserId { get; set; } = default!;

}