namespace Web.Core.Base.Store.Actions
{
    internal record BaseSuccessAction
    {
        public string SuccessMessage { get; init; } = "Успех.";
    }
}
