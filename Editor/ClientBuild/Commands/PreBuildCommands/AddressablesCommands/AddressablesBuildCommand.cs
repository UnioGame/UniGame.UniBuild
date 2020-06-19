using UniGreenModules.UniGame.UnityBuild.Editor.ClientBuild.Interfaces;

namespace UniGreenModules.UniGame.UnityBuild.Editor.ClientBuild.Commands.PreBuildCommands.AddressablesCommands
{
    using UnityEditor.AddressableAssets.Settings;
    using UnityEngine;

    [CreateAssetMenu(menuName = "UniGame/UniBuild/Commands/Build Addressables", fileName = nameof(AddressablesBuildCommand))]
    public class AddressablesBuildCommand : UnityPreBuildCommand
    {
        public override void Execute(IUniBuilderConfiguration buildParameters)
        {
            AddressableAssetSettings.BuildPlayerContent();
        }
    }
}
