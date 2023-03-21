using AutoMapper;
using InforceUrlShortener.Domain.Entities;
using InforceUrlShortener.Models;

namespace InforceUrlShortener.Services
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UserDto, User>();
            CreateMap<User, UserDto>();


            CreateMap<ShortUrl, ShortUrlDto>();
            CreateMap<ShortUrlDto, ShortUrl>().ForMember(url => url.Id, opt => opt.Ignore());
        }
    }
}
