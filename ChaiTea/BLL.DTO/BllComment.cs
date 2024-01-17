using System.ComponentModel.DataAnnotations;
using BLL.DTO.Identity;
using Domain.Base;

namespace BLL.DTO;

public class BllComment : DomainEntityId
{
    public string Text { get; set; } = default!;

    public string Name { get; set; } = default!;
    
    public Guid AppUserId { get; set; } = default!;

    public Guid ReviewId { get; set; }
    public BllReview Review { get; set; } = default!;
}