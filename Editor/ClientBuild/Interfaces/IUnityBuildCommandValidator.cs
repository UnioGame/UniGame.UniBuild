namespace UniGame.UniBuild.Editor.Interfaces
{
    using global::UniGame.UniBuild.Editor.ClientBuild.Interfaces;

    public interface IUnityBuildCommandValidator
    {
        bool Validate(IUniBuilderConfiguration config);
    }
}