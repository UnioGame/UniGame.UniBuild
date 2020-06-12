namespace UniGreenModules.UniGame.UnityBuild.Editor.ClientBuild.Commands.PostBuildCommands {
    using Interfaces;
    using PreBuildCommands;
    using UnityEditor.Build.Reporting;
    using UnityEngine;

    public abstract class UnityPostBuildCommand : 
        UnityBuildCommand,
        IUnityPostBuildCommand
    {

        public abstract void Execute(IUniBuilderConfiguration configuration,BuildReport buildReport);

    }
}
