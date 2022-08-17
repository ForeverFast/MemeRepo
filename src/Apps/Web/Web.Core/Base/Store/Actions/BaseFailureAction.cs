namespace Web.Core.Base.Store.Actions
{
    public record BaseFailureAction
    {
        public bool ShowMessage { get; init; } = true;
        public string ErrorMessage { get; init; } = "Произошла ошибка.";
        public Exception? Exception { get; init; }
    }
}
