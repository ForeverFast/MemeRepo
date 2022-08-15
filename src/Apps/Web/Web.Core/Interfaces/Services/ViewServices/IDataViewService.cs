using Web.Core.Models;
using Web.Core.Models.Components;

namespace Web.Core.Interfaces.Services.ViewServices
{
    public interface IDataViewService
    {
        ValueTask<List<MemeRepoItemViewModel>> LoadFolderMemesByFolderId(Guid folderId);
        ValueTask<List<TagViewModel>> LoadFolderTagsByFolderId(Guid folderId);
        ValueTask<List<TagViewModel>> LoadMemeTagsByMemeId(Guid folderId);
    }
}
