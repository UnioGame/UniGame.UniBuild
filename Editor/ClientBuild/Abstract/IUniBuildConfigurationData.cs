namespace UniModules.UniGame.UnityBuild.Editor.ClientBuild.BuildConfiguration
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