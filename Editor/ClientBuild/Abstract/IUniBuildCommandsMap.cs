namespace UniModules.UniGame.UniBuild.Editor.ClientBuild.Abstract
{
    using System;
    using System.Collections.Generic;
    using Interfaces;
    using UniGreenModules.UniCore.EditorTools.Editor.AssetOperations;
    using UniGreenModules.UniCore.Runtime.Interfaces;

    public interface IUniBuildCommandsMap : 
        IUnityBuildCommandValidator,
        INamedItem
    {
        
        IUniBuildConfigurationData BuildData { get; }

        List<IEditorAssetResource> LoadCommands<T>(Func<T,bool> filter = null)
            where T : IUnityBuildCommand;
    }
}