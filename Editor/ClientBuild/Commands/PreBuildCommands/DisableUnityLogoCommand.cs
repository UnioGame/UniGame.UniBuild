using UniGame.Core.Runtime;

namespace UniGame.UniBuild.Editor.Commands.PreBuildCommands {
    using System;
    using global::UniGame.UniBuild.Editor.ClientBuild.Interfaces;
    using Interfaces;
    using UnityEditor;

#if ODIN_INSPECTOR
     using Sirenix.OdinInspector;
#endif

#if TRI_INSPECTOR
    using TriInspector;
#endif
    
    [Serializable]
    public class DisableUnityLogoCommand : UnitySerializablePreBuildCommand, ICommand
    {

        public bool enableSplashScreen = false;

        public bool showUnityLogo = false;
        
        public override void Execute(IUniBuilderConfiguration buildParameters) => Execute();

#if  ODIN_INSPECTOR || TRI_INSPECTOR
        [Button]
#endif
        public void Execute()
        {
            PlayerSettings.SplashScreen.show = enableSplashScreen;
            PlayerSettings.SplashScreen.showUnityLogo = showUnityLogo;
            AssetDatabase.SaveAssets();
        }
        
    }
}
