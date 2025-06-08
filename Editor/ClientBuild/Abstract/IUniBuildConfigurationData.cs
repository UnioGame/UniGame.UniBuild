namespace UniGame.UniBuild.Editor.Abstract
{
    using UnityEditor;

    public interface IUniBuildConfigurationData
    {
        BuildTargetGroup BuildTargetGroup { get; }
        BuildTarget BuildTarget { get; }
        string ArtifactName { get; }
    }
}