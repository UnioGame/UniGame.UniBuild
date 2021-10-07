using System;
using Sirenix.OdinInspector;
using UniModules.UniGame.UniBuild.Editor.ClientBuild.BuildConfiguration;

[Serializable]
[InlineProperty]
[HideLabel]
public class UniBuildItem
{
    [HorizontalGroup(nameof(UniBuildItem))]
    [InlineEditor()]
    public UniBuildCommandsMap buildCommands;

    [Button]
    public void Build()
    {
        buildCommands.ExecuteBuild();
    }
}