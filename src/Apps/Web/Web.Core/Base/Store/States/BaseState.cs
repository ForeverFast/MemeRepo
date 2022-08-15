namespace Web.Core.Base.Store.States
{
    internal abstract record BaseState
    {
        public bool IsLoading { get; init; }

        public string? CurrentErrorMessage { get; init; }

        public bool HasCurrentErrors => !string.IsNullOrWhiteSpace(CurrentErrorMessage);
    }
}
