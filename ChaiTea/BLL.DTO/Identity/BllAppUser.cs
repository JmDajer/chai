using Domain.App.Identity;
using Domain.Contracts.Base;
using Microsoft.AspNetCore.Identity;

namespace BLL.DTO.Identity;

public class BllAppUser : IdentityUser<Guid>, IDomainEntityId
{
    public ICollection<BllReview>? Reviews { get; set; }
    public ICollection<BllComment>? Comments { get; set; }
    public ICollection<BllBeverage>? Beverages { get; set; }

    public ICollection<AppRefreshToken>? AppRefreshTokens { get; set; }
}