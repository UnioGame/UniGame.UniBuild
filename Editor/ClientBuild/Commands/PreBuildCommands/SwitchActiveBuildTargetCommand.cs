using UniModules.UniGame.UniBuild.Editor.ClientBuild.Interfaces;

namespace UniModules.UniGame.UniBuild.Editor.ClientBuild.Commands.PreBuildCommands
{
    using UnityEditor;
    using UnityEngine;

    [CreateAssetMenu(menuName = "UniGame/UniBuild/Commands/Switch Active Build Target", fileName = nameof(SwitchActiveBuildTargetCommand))]
    public class SwitchActiveBuildTargetCommand : UnityPreBuildCommand
    {
        
        public BuildTargetGroup BuildTargetGroup = BuildTargetGroup.Android;
        public BuildTarget BuildTarget = BuildTarget.Android;
        
        public override void Execute(IUniBuilderConfiguration buildParameters)
        {
            EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup,BuildTarget);
        }
    }
}
