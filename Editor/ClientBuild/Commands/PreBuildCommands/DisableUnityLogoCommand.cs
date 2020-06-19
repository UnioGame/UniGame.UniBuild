namespace UniGreenModules.UniGame.UnityBuild.Editor.ClientBuild.Commands.PreBuildCommands {
    using Interfaces;
    using UnityEditor;
    using UnityEngine;

    [CreateAssetMenu(menuName = "UniGame/UniBuild/Commands/Disable Unity Logo", fileName = "DisableUnityLogo")]

    public class DisableUnityLogoCommand : UnityPreBuildCommand
    {
        public override void Execute(IUniBuilderConfiguration buildParameters) {
            PlayerSettings.SplashScreen.showUnityLogo = false;
        }
    }
}
