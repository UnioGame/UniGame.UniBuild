namespace UniModules.UniGame.UniBuild.Editor.ClientBuild.Commands.PreBuildCommands.AddressablesCommands
{
    using System;
    using Interfaces;
    using UnityEditor.AddressableAssets.Settings;

    [Serializable]
    public class AddressablesBuildCommand : UnitySerializablePreBuildCommand
    {
        public override void Execute(IUniBuilderConfiguration buildParameters)
        {
            Execute();
        }

#if ODIN_INSPECTOR
        [Sirenix.OdinInspector.Button]
#endif
        public void Execute()
        {
            AddressableAssetSettings.BuildPlayerContent();
        }
        
    }
}
