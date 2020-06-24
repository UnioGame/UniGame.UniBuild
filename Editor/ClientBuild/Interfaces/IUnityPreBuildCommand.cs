namespace UniModules.UniGame.UniBuild.Editor.ClientBuild.Interfaces {
    public interface IUnityPreBuildCommand : IUnityBuildCommand, IUnityBuildCommandValidator
    {
        void Execute(IUniBuilderConfiguration buildParameters);
        
    }
}