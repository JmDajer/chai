using Contracts.Base;
using DAL.Contracts.App;
using DAL.DTO;
using DAL.EF.Base;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.App.Repositories;

public class CommentRepository : EfBaseRepository<Comment, DalComment, ApplicationDbContext>, ICommentRepository
{
    public CommentRepository(
        ApplicationDbContext dataContext, IMapper<Comment, DalComment> mapper) :
        base(dataContext, mapper)
    {
    }

    public async override Task<IEnumerable<DalComment>> AllAsync()
    {
        return await RepositoryDbSet
            .Include(c => c.AppUser)
            .Select(c => Mapper.Map(c)!)
            .ToListAsync();
    }

    public async Task<IEnumerable<DalComment>> GetReviewsComments(Guid reviewId)
    {
        return await RepositoryDbSet
            .Where(c => c.ReviewId == reviewId)
            .Select(c => Mapper.Map(c)!)
            .ToListAsync();
    }
    
    public async override Task RemoveAsync(Guid id)
    {
        var entity = await RepositoryDbSet
            .FirstOrDefaultAsync(t => t.Id.Equals(id));
        if (entity != null)
        {
            RepositoryDbSet.Remove(entity);
        }

        await RepositoryDbContext.SaveChangesAsync();
    }
}