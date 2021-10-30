using UniModules.UniGame.Core.Runtime.Interfaces;

namespace UniModules.UniGame.UniBuild.Editor.ClientBuild.Commands.PreBuildCommands {
    using System;
    using Interfaces;
    using UnityEditor;

    [Serializable]
    public class DisableUnityLogoCommand : UnitySerializablePreBuildCommand, ICommand
    {
        public override void Execute(IUniBuilderConfiguration buildParameters) => Execute();

#if ODIN_INSPECTOR
        [Sirenix.OdinInspector.Button]
#endif
        public void Execute()
        {
            PlayerSettings.SplashScreen.showUnityLogo = false;
            AssetDatabase.SaveAssets();
        }
        
    }
}
