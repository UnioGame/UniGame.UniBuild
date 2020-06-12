namespace UniGreenModules.UniGame.UnityBuild.Editor.ClientBuild.Commands.PreBuildCommands {
    using Interfaces;
    using UnityEngine;

    public abstract class UnityPreBuildCommand : 
        UnityBuildCommand,
        IUnityPreBuildCommand,
        IUnityBuildCommandInfo
    {
        public abstract void Execute(IUniBuilderConfiguration buildParameters);
    }
}
