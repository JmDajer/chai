using System.ComponentModel.DataAnnotations;
using Domain.Contracts.Base;
using Microsoft.AspNetCore.Identity;

namespace Domain.App.Identity;

public class AppUser : IdentityUser<Guid>, IDomainEntityId
{
    [MaxLength(128)]
    public string FirstName { get; set; } = default!;

    [MaxLength(128)]
    public string LastName { get; set; } = default!;
    
    public ICollection<Review>? Reviews { get; set; }
    public ICollection<Comment>? Comments { get; set; }
    public ICollection<Beverage>? Beverages { get; set; }

    public ICollection<AppRefreshToken>? AppRefreshTokens { get; set; }
}