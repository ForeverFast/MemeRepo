using Microsoft.AspNetCore.WebUtilities;
using Web.Core.Models.Components.Search;

namespace Web.Core.Extensions
{
    internal static class NavigationManagerExtensions
    {
        public static void NavigateToFolder(this NavigationManager navigationManager, Guid? folderId)
            => navigationManager.NavigateTo($"folder/{folderId}");
        public static void NavigateToTag(this NavigationManager navigationManager, Guid tagId)
            => navigationManager.NavigateTo($"tag/{tagId}");
        public static void NavigateToSearch(this NavigationManager navigationManager, FilterModel filter)
            => navigationManager.NavigateTo($"search-page/{Guid.NewGuid()}?SearchTitle={filter.SearchTitle}");


        public static bool TryGetQueryString<T>(this NavigationManager navManager, string key, out T value)
        {
            var uri = navManager.ToAbsoluteUri(navManager.Uri);

            if (QueryHelpers.ParseQuery(uri.Query).TryGetValue(key, out var valueFromQueryString))
            {
                if (typeof(T) == typeof(int) && int.TryParse(valueFromQueryString, out var valueAsInt))
                {
                    value = (T)(object)valueAsInt;
                    return true;
                }

                if (typeof(T) == typeof(string))
                {
                    value = (T)(object)valueFromQueryString.ToString();
                    return true;
                }

                if (typeof(T) == typeof(decimal) && decimal.TryParse(valueFromQueryString, out var valueAsDecimal))
                {
                    value = (T)(object)valueAsDecimal;
                    return true;
                }
            }

            value = default!;
            return false;
        }
    }
}
