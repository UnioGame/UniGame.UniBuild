using System;
using UniGame.UniBuild.Editor.ClientBuild.Interfaces;
using UniGame.UniBuild.Editor.Commands.PreBuildCommands;
using UniGame.UniBuild.Editor.Interfaces;
using UnityEditor;

#if ODIN_INSPECTOR
     using Sirenix.OdinInspector;
#endif

#if TRI_INSPECTOR
using TriInspector;
#endif

[Serializable]
public class PlayerSettingsCommand : SerializableBuildCommand
{
    public bool setIncrementalIl2CppBuild = true;
    
    public override void Execute(IUniBuilderConfiguration buildParameters)
    {
        Execute(buildParameters.BuildParameters.buildTargetGroup);
    }

#if  ODIN_INSPECTOR || TRI_INSPECTOR
    [Button]
#endif
    public void Execute(BuildTargetGroup targetGroup)
    {
        PlayerSettings.SetIncrementalIl2CppBuild(targetGroup,setIncrementalIl2CppBuild);
    }
}
