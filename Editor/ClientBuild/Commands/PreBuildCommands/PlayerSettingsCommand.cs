using System;
using UniGame.UniBuild.Editor.ClientBuild.Interfaces;
using UniModules.UniGame.UniBuild.Editor.ClientBuild.Commands.PreBuildCommands;
using UniModules.UniGame.UniBuild.Editor.ClientBuild.Interfaces;
using UnityEditor;

[Serializable]
public class PlayerSettingsCommand : SerializableBuildCommand
{
    public bool setIncrementalIl2CppBuild = true;
    
    public override void Execute(IUniBuilderConfiguration buildParameters)
    {
        Execute(buildParameters.BuildParameters.buildTargetGroup);
    }

#if ODIN_INSPECTOR
    [Sirenix.OdinInspector.Button]
#endif
    public void Execute(BuildTargetGroup targetGroup)
    {
        PlayerSettings.SetIncrementalIl2CppBuild(targetGroup,setIncrementalIl2CppBuild);
    }
}
