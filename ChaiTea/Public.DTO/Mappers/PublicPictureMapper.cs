﻿using AutoMapper;
using BLL.DTO;
using DAL.Base;
using Public.DTO.v1;

namespace Public.DTO.Mappers;

public class PublicPictureMapper : BaseMapper<BllPicture, PublicPicture>
{
    public PublicPictureMapper(IMapper mapper) : base(mapper)
    {
    }
}