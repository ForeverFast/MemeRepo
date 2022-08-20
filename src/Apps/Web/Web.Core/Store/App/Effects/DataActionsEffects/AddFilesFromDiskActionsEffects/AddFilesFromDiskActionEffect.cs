﻿using DALQueryChain.Interfaces;
using Domain.Core.Interfaces;
using Web.Core.Components.DialogComponents;
using Web.Core.Models;
using Web.Core.Models.Components;
using Web.Core.Models.Components.Dialogs;
using Web.Core.Store.App.Actions.DataActions.AddFilesFromDiskActions;

namespace Web.Core.Store.App.Actions.DataActionsEffects.AddFilesFromDiskActionsEffects
{
    internal class AddFilesFromDiskActionEffect : BaseDataEffect<AddFilesFromDiskAction>
    {
        #region Injects

        private readonly IState<AppState> _appDataState;
        private readonly IDialogService _dialogService;
        private readonly IFileStorageProvider _fileStorageProvider;

        #endregion

        #region Ctors

        public AddFilesFromDiskActionEffect(IDALQueryChain<MemeRepoDbContext> dal,
                                            IMapper mapper,
                                            IFileStorageProvider fileStorageProvider,
                                            IState<AppState> appDataState,
                                            IDialogService dialogService) : base(dal, mapper)
        {
            _fileStorageProvider = fileStorageProvider;
            _appDataState = appDataState;
            _dialogService = dialogService;
        }

        #endregion

        public override async Task HandleAsync(AddFilesFromDiskAction action, IDispatcher dispatcher)
        {
            try
            {
                var paths = new List<string>();

                if (action.DialogFlag)
                {
                    var dialog = _dialogService!.Show<AddFilesFromDiskDialog>("Добавление файлов", AddFilesFromDiskDialog.DialogOptions);
                    var dialogResult = await dialog.Result;

                    if (dialogResult.Cancelled)
                        return;

                    var folderDialogResult = (FolderDialogViewModel)dialogResult.Data;
                }
                else
                {
                    if (!(action.FilePaths?.Any() ?? false))
                        return;

                    paths = action.FilePaths;
                }



                var topLevelMemes = new List<string>();
                var topLevelFolders = new List<string>();

                foreach (var item in paths)
                {
                    FileAttributes attr = File.GetAttributes(item);
                    if (attr.HasFlag(FileAttributes.Directory))
                        topLevelFolders.Add(item);
                    else
                        topLevelMemes.Add(item);
                }

                var absoluteParentFolderPath = _fileStorageProvider.GetAbsolutePath(_appDataState.Value.GetFolderRelativePath(action.ParentFolderId));

                var topLevelFolderResult = await CreateFoldersAsync(topLevelFolders.ToArray(), action.ParentFolderId, absoluteParentFolderPath);
                var topLevelMemeResult = await CreateMemesAsync(topLevelMemes.ToArray(), action.ParentFolderId, absoluteParentFolderPath);

                dispatcher.Dispatch(new AddFilesFromDiskSuccessAction(action.ParentFolderId, topLevelFolderResult, topLevelMemeResult)
                {

                });
            }
            catch (Exception ex)
            {
                dispatcher.Dispatch(new AddFilesFromDiskFailureAction
                {
                    ErrorMessage = ex.Message,
                    Exception = ex,
                });
            }
        }

        private async Task<HashSet<FolderTreeViewModel>> CreateFoldersAsync(string[] folderPaths, Guid? parentFolderId, string absoluteParentFolderPath)
        {
            var resultFolders = new HashSet<FolderTreeViewModel>();
            foreach (var folderPath in folderPaths)
            {
                var absoluteNewFolderPath = _fileStorageProvider.CreateFolderPath(absoluteParentFolderPath, new DirectoryInfo(folderPath).Name);
                var newDirectoryInfo = new DirectoryInfo(absoluteNewFolderPath);
                var folder = new Folder
                {
                    Title = newDirectoryInfo.Name,
                    Path = newDirectoryInfo.Name,
                    ParentFolderId = parentFolderId,
                };

                var createdFolder = await _dal.For<Folder>().Insert.InsertWithObjectAsync(folder);
                _fileStorageProvider.CreateFolder(absoluteNewFolderPath);
                var resultFolder = _mapper.Map<FolderTreeViewModel>(createdFolder);

                var innersFiles = Directory.EnumerateFiles(folderPath, "*.*", SearchOption.AllDirectories)
                    .Where(s => s.EndsWith(".jpg") || s.EndsWith(".jpeg") || s.EndsWith(".png") || s.EndsWith(".gif")).ToArray();
                _ = await CreateMemesAsync(innersFiles, resultFolder.Id, absoluteNewFolderPath);

                var innerFoldersPaths = Directory.GetDirectories(folderPath);
                var innerFolders = await CreateFoldersAsync(innerFoldersPaths, resultFolder.Id, absoluteNewFolderPath);
                foreach (var innerFolder in innerFolders)
                {
                    innerFolder.ParentFolder = resultFolder;
                    resultFolder.Folders.Add(innerFolder);
                }

                resultFolders.Add(resultFolder);
            }
            return resultFolders;
        }

        private async Task<List<MemeViewModel>> CreateMemesAsync(string[] filesPaths, Guid? parentFolderId, string absoluteParentFolderPath)
        {
            var resultMemes = new List<MemeViewModel>();
            foreach (var filePath in filesPaths)
            {
                var absoluteNewFilePath = _fileStorageProvider.CreateFilePath(
                    absoluteParentFolderPath, new FileInfo(filePath).Extension, Path.GetFileNameWithoutExtension(filePath));

                var newFileInfo = new FileInfo(absoluteNewFilePath);
                var meme = new Meme
                {
                    Title = Path.GetFileNameWithoutExtension(absoluteNewFilePath),
                    Path = newFileInfo.Name,
                    ParentFolderId = parentFolderId,
                };

                var createdMeme = await _dal.For<Meme>().Insert.InsertWithObjectAsync(meme);

                _fileStorageProvider.CopyFileToNewFile(filePath, absoluteNewFilePath);

                var resultMeme = _mapper.Map<MemeViewModel>(createdMeme);
                resultMemes.Add(resultMeme);
            }
            return resultMemes;
        }
    }
}
