using System.Collections.Generic;
using System.Linq;
using UniModules.UniGame.Core.EditorTools.Editor.AssetOperations;
using UniModules.UniGame.UniBuild.Editor.ClientBuild.BuildConfiguration;

namespace UniModules.UniGame.UniBuild.Editor.ClientBuild.Processors
{
    using Core.Editor.Tools;
    using Generator;
    using UnityEditor;

    public class BuildConfigurationsPostProcessor : AssetPostprocessor
    {
        
        private static List<string> commandsMapPaths = new List<string>();

        static BuildConfigurationsPostProcessor()
        {
            AssemblyReloadEvents.afterAssemblyReload += () =>
            {
                var assets = AssetEditorTools.GetAssets<UniBuildCommandsMap>();
                commandsMapPaths.Clear();
                commandsMapPaths.AddRange(assets.Select(AssetDatabase.GetAssetPath));
            };
        }
        
        static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
        {
            var isChanged = importedAssets.Any(x => commandsMapPaths.Contains(x));
            if (isChanged)
            {
                UniEditorProfiler.LogTime(nameof(BuildConfigurationsPostProcessor), () => BuildConfigurationBuilder.Rebuild());
            }
        }
    }
}
