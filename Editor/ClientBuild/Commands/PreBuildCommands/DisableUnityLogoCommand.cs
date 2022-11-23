using UniGame.Core.Runtime;

namespace UniModules.UniGame.UniBuild.Editor.ClientBuild.Commands.PreBuildCommands {
    using System;
    using Interfaces;
    using UnityEditor;

    [Serializable]
    public class DisableUnityLogoCommand : UnitySerializablePreBuildCommand, ICommand
    {

        public bool enableSplashScreen = false;

        public bool showUnityLogo = false;
        
        public override void Execute(IUniBuilderConfiguration buildParameters) => Execute();

#if ODIN_INSPECTOR
        [Sirenix.OdinInspector.Button]
#endif
        public void Execute()
        {
            PlayerSettings.SplashScreen.show = enableSplashScreen;
            PlayerSettings.SplashScreen.showUnityLogo = showUnityLogo;
            AssetDatabase.SaveAssets();
        }
        
    }
}
