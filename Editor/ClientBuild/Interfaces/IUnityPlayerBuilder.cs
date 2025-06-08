namespace UniGame.UniBuild.Editor.Interfaces
{
    using global::UniGame.UniBuild.Editor.ClientBuild.Interfaces;
    using UnityEditor.Build.Reporting;

    public interface IUnityPlayerBuilder
    {
        BuildReport Build(IUniBuilderConfiguration configuration);
    }
}