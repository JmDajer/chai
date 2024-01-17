using System.ComponentModel.DataAnnotations;
using Domain.App.Identity;
using Domain.Base;

namespace Domain.App;

public class Comment : DomainEntityId
{
    [MaxLength(512)]
    public string Text { get; set; } = default!;
    
    [MaxLength(512)]
    public string Name { get; set; } = default!;
    
    public Guid AppUserId { get; set; }
    public AppUser AppUser { get; set; } = default!;

    public Guid ReviewId { get; set; }
    public Review Review { get; set; } = default!;
}