namespace UniModules.UniGame.UniBuild.Editor.ClientBuild.Interfaces
{
    public interface IUniBuilderConfiguration
    {
        
        IArgumentsProvider Arguments { get; }
    
        IBuildParameters BuildParameters { get; }
        
    }
}