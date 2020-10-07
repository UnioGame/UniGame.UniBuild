namespace UniModules.UniGame.UniBuild.Editor.ClientBuild.Commands.PreBuildCommands {
    using System;
    using Interfaces;

    [Serializable]
    public abstract class UnityPreBuildCommand : 
        UnityBuildCommand,
        IUnityPreBuildCommand
    {
    }
}
