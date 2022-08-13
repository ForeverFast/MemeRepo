using AutoMapper;
using Business.Core.Dtos;

namespace Business.Core.Utils.AutoMapper.MemeMaps
{
    internal class MemeProfile : Profile
    {
        public MemeProfile()
        {
            CreateMap<Meme, MemeDto>()
              .ReverseMap()
              .ForMember(dst => dst.Tags, opt => opt.Ignore())
              ;
        }
    }
}
