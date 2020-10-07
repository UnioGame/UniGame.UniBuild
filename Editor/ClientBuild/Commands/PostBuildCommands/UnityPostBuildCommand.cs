namespace UniModules.UniGame.UniBuild.Editor.ClientBuild.Commands.PostBuildCommands {
    using Interfaces;
    using PreBuildCommands;
    using UnityEditor.Build.Reporting;

    public abstract class UnityPostBuildCommand : 
        UnityBuildCommand,
        IUnityPostBuildCommand
    {

    }
}
