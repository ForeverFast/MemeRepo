namespace Web.Core.Store.App
{
    internal class AppFeatureState : Feature<AppState>
    {
        public override string GetName() => nameof(AppState);

        protected override AppState GetInitialState() => new AppState { };
    }
}
