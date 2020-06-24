namespace UniModules.UniGame.UniBuild.Editor.ClientBuild.Commands.PreBuildCommands {
    using Interfaces;

    public abstract class UnityPreBuildCommand : 
        UnityBuildCommand,
        IUnityPreBuildCommand,
        IUnityBuildCommandInfo
    {
        public abstract void Execute(IUniBuilderConfiguration buildParameters);
    }
}
