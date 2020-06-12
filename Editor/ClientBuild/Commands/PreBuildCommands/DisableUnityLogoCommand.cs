namespace UniGreenModules.UniGame.UnityBuild.Editor.ClientBuild.Commands.PreBuildCommands {
    using Interfaces;
    using UnityEditor;
    using UnityEngine;

    [CreateAssetMenu(menuName = "UnityBuild/PreBuildCommands/Disable Unity Logo", fileName = "DisableUnityLogo")]

    public class DisableUnityLogoCommand : UnityPreBuildCommand
    {
        public override void Execute(IUniBuilderConfiguration buildParameters) {
            PlayerSettings.SplashScreen.showUnityLogo = false;
        }
    }
}
