namespace UniGreenModules.UniGame.UnityBuild.Editor.ClientBuild.Interfaces {
    public interface IUnityPreBuildCommand : IUnityBuildCommand, IUnityBuildCommandValidator
    {
        void Execute(IUniBuilderConfiguration buildParameters);
        
    }
}