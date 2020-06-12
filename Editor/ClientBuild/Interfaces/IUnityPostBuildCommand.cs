namespace UniGreenModules.UniGame.UnityBuild.Editor.ClientBuild.Interfaces {
    using UnityEditor.Build.Reporting;

    public interface IUnityPostBuildCommand  : IUnityBuildCommand,IUnityBuildCommandValidator
    {

        void Execute(IUniBuilderConfiguration configuration,BuildReport buildReport = null);
        
    }
}