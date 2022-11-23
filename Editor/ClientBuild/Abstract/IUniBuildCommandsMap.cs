using System;
using System.Collections.Generic;

namespace UniModules.UniGame.UniBuild.Editor.ClientBuild.Abstract
{
    using global::UniGame.Core.Runtime;
    using Interfaces;

    public interface IUniBuildCommandsMap : 
        IUnityBuildCommandValidator,
        INamedItem
    {
        
        public bool PlayerBuildEnabled { get; }
        
        IUnityBuildCommand ArgumentsCommand { get; }
        
        IUniBuildConfigurationData BuildData { get; }
        
        IEnumerable<IUnityBuildCommand> PreBuildCommands { get; }

        IEnumerable<IUnityBuildCommand> PostBuildCommands  { get; }
        
        IEnumerable<T> LoadCommands<T>(Func<T,bool> filter = null)
            where T : IUnityBuildCommand;
    }
}