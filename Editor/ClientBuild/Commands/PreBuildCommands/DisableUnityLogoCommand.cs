namespace UniModules.UniGame.UniBuild.Editor.ClientBuild.Commands.PreBuildCommands {
    using System;
    using Interfaces;
    using UnityEditor;
    using UnityEngine;

    [Serializable]
    public class DisableUnityLogoCommand : UnitySerializablePreBuildCommand
    {
        public override void Execute(IUniBuilderConfiguration buildParameters) {
            PlayerSettings.SplashScreen.showUnityLogo = false;
        }
    }
}
