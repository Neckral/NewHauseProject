using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DataAccesLayer.Enteties;
using BuisnesLogicLayer.DTO;

namespace BuisnesLogicLayer.MappersConfigurations
{
    public class MapperProfile : Profile
    {
        public MapperProfile() 
        {
            //========> User Mappers
            CreateMap<User, UserProfileDTO>();
            CreateMap<UserProfileDTO, User>();
            CreateMap<UserRegisterDTO, User>();

            //========> Ad Mappers
            CreateMap<Ad, AdInfoDTO>().ForMember(dest => dest.images, act => act.Ignore()).ForMember(dest => dest.tags, act => act.Ignore());
            CreateMap<AdCreateDTO, Ad>().ForMember(dest => dest.images, act => act.Ignore()).ForMember(dest => dest.tags, act => act.Ignore());
            CreateMap<AdEditDTO, Ad>();
            CreateMap<Ad, AdEditDTO>();

            //========> Tag Mappers
            CreateMap<Tag, TagDTO>();
            CreateMap<TagDTO, Tag>();

            //========> Image Mappers
            CreateMap<Image, ImageEditInfoDTO>();
            CreateMap<ImageCreateDTO, Image>();
            //========> Favorites Mappers
            CreateMap<Ad, AdShortInfoDTO>().ForMember(dest => dest.images, act => act.Ignore());

            //========> Favorites Mappers
            CreateMap<Ad, ForCompareDTO>().ForMember(dest => dest.images, act => act.Ignore());

            //========> Comments Mappers
            CreateMap<CommentCreateDTO, Comment>();
            CreateMap<Comment, CommentInfoAndEditIDTO>();
        }
    }
}
