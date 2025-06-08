using System;
using UniGame.UniBuild.Editor;

#if ODIN_INSPECTOR
     using Sirenix.OdinInspector;
#endif

#if TRI_INSPECTOR
using TriInspector;
#endif

[Serializable]
#if ODIN_INSPECTOR
[HideLabel]
#endif
#if  ODIN_INSPECTOR || TRI_INSPECTOR
[InlineProperty]
#endif
public class UniBuildItem
{
#if  ODIN_INSPECTOR || TRI_INSPECTOR
    [InlineEditor()]
#endif
#if ODIN_INSPECTOR
    [HorizontalGroup(nameof(UniBuildItem))]
#endif
    public UniBuildCommandsMap buildCommands;

#if  ODIN_INSPECTOR || TRI_INSPECTOR
    [Button]
#endif
    public void Build()
    {
        buildCommands.ExecuteBuild();
    }
}