namespace UniGreenModules.UniGame.UnityBuild.Editor.ClientBuild.Interfaces
{
    using UnityEditor.Build.Reporting;

    public interface IUnityPlayerBuilder
    {
        BuildReport Build(IUniBuilderConfiguration configuration);
    }
}