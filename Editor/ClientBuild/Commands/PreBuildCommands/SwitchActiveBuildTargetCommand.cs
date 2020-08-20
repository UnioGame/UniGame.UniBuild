using UniModules.UniGame.UniBuild.Editor.ClientBuild.Interfaces;

namespace UniModules.UniGame.UniBuild.Editor.ClientBuild.Commands.PreBuildCommands
{
    using System;
    using UnityEditor;
    using UnityEngine;

    [Serializable]
    public class SwitchActiveBuildTargetCommand : UnitySerializablePreBuildCommand
    {
        
        public BuildTargetGroup BuildTargetGroup = BuildTargetGroup.Android;
        public BuildTarget BuildTarget = BuildTarget.Android;
        
        public override void Execute(IUniBuilderConfiguration buildParameters)
        {
            EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup,BuildTarget);
        }
    }
}
