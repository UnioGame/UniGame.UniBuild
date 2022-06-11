using UnityEditor.Build.Reporting;

namespace UniModules.UniGame.UniBuild.Editor.ClientBuild.Interfaces
{
    public interface IUniBuilderConfiguration
    {
        /// <summary>
        /// Allow to use local argument data between build steps
        /// </summary>
        IArgumentsProvider Arguments { get; }
    
        /// <summary>
        /// Current Unity build parameters
        /// </summary>
        IBuildParameters BuildParameters { get; }
        
        BuildReport BuildReport { get; set; }
        
    }
}