using System.Net;
using System.Security.Claims;
using BLL.Base;
using BLL.Contracts.App;
using BLL.DTO;
using Contracts.Base;
using DAL.Contracts.App;
using DAL.DTO;
using Domain.App.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis;

namespace BLL.App.Services;

public class CommentService : BaseEntityService<BllComment, DalComment, ICommentRepository>,
    ICommentService
{
    protected readonly IAppUow Uow;

    public CommentService(IAppUow uow, IMapper<DalComment, BllComment> mapper) :
        base(uow.CommentRepository, mapper)
    {
        Uow = uow;
    }
    
    public async Task<IEnumerable<BllComment>> GetReviewsComments(Guid reviewId)
    {
        var comments = await Repository.GetReviewsComments(reviewId);
        return comments.Select(c => Mapper.Map(c)!);
    }
}