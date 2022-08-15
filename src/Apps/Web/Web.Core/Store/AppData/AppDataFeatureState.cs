namespace Web.Core.Store.AppData
{
    internal class AppDataFeatureState : Feature<AppDataState>
    {
        public override string GetName() => nameof(AppDataState);

        protected override AppDataState GetInitialState() => new AppDataState { };
    }
}
