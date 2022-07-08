using System.Reflection;

namespace Hybrid.Maui
{
    public partial class MainPage : ContentPage
    {
        static IDictionary<string, object?> parameters = new Dictionary<string, object?>
        {
            { nameof(Web.Core.App.IsMobileApp), true },
        };

        public MainPage()
        {
            InitializeComponent();
            this.BlazorWebViewRoot.Parameters = parameters;
        }
    }
}