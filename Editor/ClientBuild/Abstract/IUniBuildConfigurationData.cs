namespace UniModules.UniGame.UniBuild.Editor.ClientBuild.Abstract
{
    using UnityEditor;

    public interface IUniBuildConfigurationData
    {
        bool CloudBuild { get; }
        BuildTargetGroup BuildTargetGroup { get; }
        BuildTarget BuildTarget { get; }
        string ArtifactName { get; }
    }
}