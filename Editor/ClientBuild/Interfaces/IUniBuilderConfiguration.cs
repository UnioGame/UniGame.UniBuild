namespace UniGreenModules.UniGame.UnityBuild.Editor.ClientBuild.Interfaces
{
    public interface IUniBuilderConfiguration
    {
        
        IArgumentsProvider Arguments { get; }
    
        IBuildParameters BuildParameters { get; }
        
    }
}