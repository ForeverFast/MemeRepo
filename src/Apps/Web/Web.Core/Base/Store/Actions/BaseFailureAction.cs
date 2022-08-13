namespace Web.Core.Base.Store.Actions
{
    public abstract record BaseFailureAction
    {
        public string FailureMessage { get; init; } = "Произошла ошибка.";
        public string ErrorMessage { get; init; } = string.Empty;
    }
}
