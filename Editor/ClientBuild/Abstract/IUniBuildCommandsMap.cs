namespace UniModules.UniGame.UniBuild.Editor.ClientBuild.Abstract
{
    using System;
    using System.Collections.Generic;
    using Core.EditorTools.Editor.AssetOperations;
    using Interfaces;
    using UniGreenModules.UniCore.Runtime.Interfaces;

    public interface IUniBuildCommandsMap : 
        IUnityBuildCommandValidator,
        INamedItem
    {
        
        IUniBuildConfigurationData BuildData { get; }

        IEnumerable<T> LoadCommands<T>(Func<T,bool> filter = null)
            where T : IUnityBuildCommand;
    }
}