namespace UniGame.UniBuild.Editor.Commands.PreBuildCommands {
    using System;
    using Interfaces;

    [Serializable]
    public abstract class UnityPreBuildCommand : 
        UnityBuildCommand,
        IUnityPreBuildCommand
    {
    }
}
