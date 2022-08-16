namespace Web.Core.Base.Store.Actions
{
    internal record BaseSuccessAction
    {
        public bool ShowMessage { get; init; } = false;
        public string SuccessMessage { get; init; } = "Успех.";
    }
}
