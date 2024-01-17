using AutoMapper;
using DAL.Base;
using DAL.DTO;
using Domain.App;

namespace DAL.EF.App.Mapper;

public class DalReviewMapper : BaseMapper<Review, DalReview>
{
    public DalReviewMapper(IMapper mapper) : base(mapper)
    {
    }
}