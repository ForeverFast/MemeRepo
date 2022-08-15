using Web.Core.Enums.Components;
using Web.Core.Models;
using Web.Core.Models.Components;
using Web.Core.Models.Components.Dialogs;

namespace Web.Core.Utils.AutoMapper.AppDataMaps
{
    internal class AppDataProfile : Profile
    {
        public AppDataProfile()
        {
            CreateMap<Folder, FolderViewModel>();
            CreateMap<Folder, FolderTreeViewModel>()
                .ForMember(dst => dst.ParentFolder, opt => opt.Ignore())
                .ForMember(dst => dst.IsExpanded, opt => opt.Ignore())
                .ForMember(dst => dst.PathFlag, opt => opt.Ignore())
                .AfterMap((src, dst, ctx) =>
                {
                    foreach (var innerFolder in dst.Folders)
                        innerFolder.ParentFolder = dst;
                })
                ;

            CreateMap<Folder, FolderDialogViewModel>()
                .ForMember(dst => dst.Tags, opt => opt.MapFrom(src => src.FolderTags.Select(x => x.TagId).ToList()))
                .ReverseMap()
                .ForMember(dst => dst.Id, opt => opt.Ignore())
                .ForMember(dst => dst.ParentFolderId, opt => opt.Ignore())

                .ForMember(dst => dst.Path, opt => opt.Ignore())

                .ForMember(dst => dst.ParentFolder, opt => opt.Ignore())
                .ForMember(dst => dst.Folders, opt => opt.Ignore())
                .ForMember(dst => dst.FolderTags, opt => opt.Ignore())
                .ForMember(dst => dst.Memes, opt => opt.Ignore())
                ;

            CreateMap<Meme, MemeViewModel>();
            CreateMap<Meme, MemeRepoItemViewModel>()
                .ForMember(x => x.FolderObjectType, opt => opt.MapFrom(src => MemeRepoItemType.Img))
                .ReverseMap()
                ;

            CreateMap<Meme, MemeDialogViewModel>()
                .ForMember(dst => dst.Tags, opt => opt.MapFrom(src => src.MemeTags.Select(x => x.TagId).ToList()))
                ;
            CreateMap<MemeDialogViewModel, Meme>()
                .ForMember(dst => dst.Id, opt => opt.Ignore())
                .ForMember(dst => dst.ParentFolderId, opt => opt.Ignore())

                .ForMember(dst => dst.Path, opt => opt.Ignore())

                .ForMember(dst => dst.ParentFolder, opt => opt.Ignore())
                .ForMember(dst => dst.MemeTags, opt => opt.Ignore())
                ;
          
            CreateMap<Tag, TagViewModel>()
                ;
        }
    }
}
