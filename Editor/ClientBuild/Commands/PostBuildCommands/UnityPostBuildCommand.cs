namespace UniGame.UniBuild.Editor.Commands.PostBuildCommands {
    using Interfaces;
    using PreBuildCommands;

    public abstract class UnityPostBuildCommand : 
        UnityBuildCommand,
        IUnityPostBuildCommand
    {

    }
}
