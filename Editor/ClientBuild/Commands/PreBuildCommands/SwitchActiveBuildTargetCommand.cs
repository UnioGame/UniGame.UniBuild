using UniGame.UniBuild.Editor.Interfaces;

namespace UniGame.UniBuild.Editor.Commands.PreBuildCommands
{
    using System;
    using global::UniGame.UniBuild.Editor.ClientBuild.Interfaces;
    using UnityEditor;

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
