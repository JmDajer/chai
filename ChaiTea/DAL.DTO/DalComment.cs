using Domain.Base;

namespace DAL.DTO;

public class DalComment : DomainEntityId
{
    public string Text { get; set; } = default!;

    public string Name { get; set; } = default!;
    
    public Guid AppUserId { get; set; } = default!;

    public Guid ReviewId { get; set; }
    public DalReview Review { get; set; } = default!;
}