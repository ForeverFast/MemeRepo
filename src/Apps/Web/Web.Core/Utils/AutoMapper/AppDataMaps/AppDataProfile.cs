using Web.Core.Models;
using Web.Core.Models.Components;
using Web.Core.Store.AppData.Actions.FolderActions.CreateFolderActions;

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


            CreateMap<Meme, MemeViewModel>()
                .ForMember(dst => dst.ParentFolder, opt => opt.Ignore())
                ;
            CreateMap<Tag, TagViewModel>()
                ;


            CreateMap<FolderDialogViewModel, CreateFolderAction>()
                ;
            CreateMap<CreateFolderAction, Folder>()
                .ForMember(dst => dst.Memes, opt => opt.Ignore())
                .ForMember(dst => dst.Tags, opt => opt.Ignore())
                ;
        }
    }
}
