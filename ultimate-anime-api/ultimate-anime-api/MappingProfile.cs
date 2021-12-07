using AutoMapper;
using Entities.DTOs;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ultimate_anime_api
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            // Get
            CreateMap<Studio, StudioDto>().ForMember(s => s.FullAddress, opt => opt.MapFrom(x => string.Join(' ', x.Address, x.Country)));
            CreateMap<Anime, AnimeDto>();

            // Post
            CreateMap<StudioForCreationDto, Studio>();
        }
    }
}
