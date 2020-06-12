namespace UniModules.UniGame.UnityBuild.Editor.ClientBuild.BuildConfiguration
{
    using System;
    using System.Collections.Generic;
    using UniGreenModules.UniCore.EditorTools.Editor.AssetOperations;
    using UniGreenModules.UniCore.Runtime.Interfaces;
    using UniGreenModules.UniGame.UnityBuild.Editor.ClientBuild.Interfaces;

    public interface IUniBuildCommandsMap : 
        IUnityBuildCommandValidator,
        INamedItem
    {
        
        IUniBuildConfigurationData BuildData { get; }

        List<IEditorAssetResource> LoadCommands<T>(Func<T,bool> filter = null)
            where T : IUnityBuildCommand;
    }
}