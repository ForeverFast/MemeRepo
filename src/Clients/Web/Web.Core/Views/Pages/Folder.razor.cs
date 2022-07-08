using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Web.Core.Models;

namespace Web.Core.Views.Pages
{
    [Route("folder/{Id:guid}")]
    public partial class Folder
    {
        #region Params

        [Parameter] public Guid Id { get; set; }

        #endregion

        #region Injects

        #endregion

        #region UI Fields

        protected Guid _currentFolderId { get; set; }
        protected List<FolderObjectViewModel> FolderObjects = new List<FolderObjectViewModel>();

        #endregion

        protected override void OnParametersSet()
        {
            if (_currentFolderId != Id)
            {
                _currentFolderId = Id;
                FolderObjects.Clear();
                var itemNum = RandomNumberGenerator.GetInt32(0, 50);
                for (int i = 0; i < itemNum; i++)
                {
                    FolderObjects.Add(new FolderObjectViewModel
                    {
                        Title = $"Object {i + 1}",
                        Path = $@"C:\Users\ivans\Downloads\MhKwfM2V05Y.jpg"
                    });
                }
            }
        }

        void Test()
        {
            try
            {
                //Directory.CreateDirectory("D:\\test");
                using var file = File.Create($"D:\\test.txt");
            }
            catch (Exception ex)
            {

            }
        }
    }
}
