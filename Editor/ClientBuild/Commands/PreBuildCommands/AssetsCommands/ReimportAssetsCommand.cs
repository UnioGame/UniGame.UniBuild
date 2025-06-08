using UniGame.UniBuild.Editor.Interfaces;

namespace UniGame.UniBuild.Editor.Commands.PreBuildCommands.AssetsCommands
{
    using System.Collections.Generic;
    using global::UniGame.UniBuild.Editor.ClientBuild.Interfaces;
    using UnityEditor;
    using UnityEngine;

    /// <summary>
    /// reimport target assets locations
    /// or target assets
    /// </summary>
    public class ReimportAssetsCommand : UnitySerializablePreBuildCommand
    {
        public List<Object> assets = new List<Object>();

        public ImportAssetOptions ImportAssetOptions = ImportAssetOptions.ImportRecursive;
        
        public override void Execute(IUniBuilderConfiguration buildParameters)
        {
            foreach (var asset in assets)
            {
                if(!asset) continue;
                var assetPath = AssetDatabase.GetAssetPath(asset);
                AssetDatabase.ImportAsset(assetPath, ImportAssetOptions);
            }       
        }
    }
}
