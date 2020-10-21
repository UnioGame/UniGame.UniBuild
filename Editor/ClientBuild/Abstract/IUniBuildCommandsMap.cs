namespace UniModules.UniGame.UniBuild.Editor.ClientBuild.Abstract
{
    using System;
    using System.Collections.Generic;
    using Core.EditorTools.Editor.AssetOperations;
    using Core.Runtime.Interfaces;
    using Interfaces;

    public interface IUniBuildCommandsMap : 
        IUnityBuildCommandValidator,
        INamedItem
    {
        
        IUniBuildConfigurationData BuildData { get; }
        
                
        IEnumerable<IUnityBuildCommand> PreBuildCommands { get; }

        IEnumerable<IUnityBuildCommand> PostBuildCommands  { get; }


        IEnumerable<T> LoadCommands<T>(Func<T,bool> filter = null)
            where T : IUnityBuildCommand;
    }
}