using AutoMapper;
using BLL.DTO;
using DAL.Base;
using Public.DTO.v1;

namespace Public.DTO.Mappers;

public class PublicReviewMapper : BaseMapper<BllReview, PublicReview>
{
    public PublicReviewMapper(IMapper mapper) : base(mapper)
    {
    }
}