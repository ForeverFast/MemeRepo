﻿using MudBlazor.Utilities;
using Web.Core.Models.Components;

namespace Web.Core.Components.ItemsContainer
{
    public partial class MemeRepoContainerItem
    {
        #region Params

        [Parameter] public MemeRepoItemViewModel Data { get; set; } = new MemeRepoItemViewModel();
        [Parameter] public string? Width { get; set; }
        [Parameter] public string? Height { get; set; }

        #endregion

        #region UI Fields

        private static int numC = 1;

        private int Num = numC++;

        #endregion

        #region Component Css/Style

        protected virtual string Classname => new CssBuilder()
            .AddClass(Class)
            .Build();

        protected virtual string Stylename => new StyleBuilder()
            .AddStyle("width", $"{Width}", !string.IsNullOrEmpty(Width))
            .AddStyle("height", $"{Height}", !string.IsNullOrEmpty(Height))
            .AddStyle(Style)
            .Build();

        #endregion

        protected override void OnInitialized()
        {
            //var abslPath = Path.Combine("wwwroot", state.GetFileRelativePath(x.ParentFolderId!.Value, x.Path));
            //x.Path = "data:image/png;base64, " + Convert.ToBase64String(File.ReadAllBytes(abslPath));

            Console.WriteLine(Num);
            base.OnInitialized();
        }
    }
}
