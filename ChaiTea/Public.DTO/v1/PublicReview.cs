using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace Public.DTO.v1;

public class PublicReview : DomainEntityId
{
    public decimal Rating { get; set; } = default!;

    public string? ReviewText { get; set; }
    
    public string Name { get; set; } = default!;

    public Guid AppUserId { get; set; } = default!;

    public Guid BeverageId { get; set; } = default!;
    
    public ICollection<PublicComment>? Comments { get; set; }
}