using AutoMapper;
using Business.Core.Dtos;

namespace Business.Core.Utils.AutoMapper.FolderMaps
{
    internal class FolderProfile : Profile
    {
        public FolderProfile()
        {
            CreateMap<Folder, FolderDto>();
        }
    }
}
