using System;
using UniModules.UniGame.UniBuild.Editor.ClientBuild.BuildConfiguration;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

[Serializable]
#if ODIN_INSPECTOR
[InlineProperty]
[HideLabel]
#endif
public class UniBuildItem
{
#if ODIN_INSPECTOR
    [HorizontalGroup(nameof(UniBuildItem))]
    [InlineEditor()]
#endif
    public UniBuildCommandsMap buildCommands;

#if ODIN_INSPECTOR
    [Button]
#endif
    public void Build()
    {
        buildCommands.ExecuteBuild();
    }
}