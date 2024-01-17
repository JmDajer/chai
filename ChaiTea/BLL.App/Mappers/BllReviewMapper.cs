using AutoMapper;
using BLL.DTO;
using DAL.Base;
using DAL.DTO;

namespace BLL.App.Mappers;

public class BllReviewMapper : BaseMapper<DalReview, BllReview>
{
    public BllReviewMapper(IMapper mapper) : base(mapper)
    {
    }
}