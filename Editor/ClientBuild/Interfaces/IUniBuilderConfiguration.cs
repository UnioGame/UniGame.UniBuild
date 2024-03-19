namespace UniGame.UniBuild.Editor.ClientBuild.Interfaces
{
    using UniModules.UniGame.UniBuild.Editor.ClientBuild;
    using UnityEditor.Build.Reporting;

    public interface IUniBuilderConfiguration
    {
        /// <summary>
        /// Allow to use local argument data between build steps
        /// </summary>
        IArgumentsProvider Arguments { get; }
    
        /// <summary>
        /// Current Unity build parameters
        /// </summary>
        BuildParameters BuildParameters { get; }
        
        BuildReport BuildReport { get; set; }
        
    }
}