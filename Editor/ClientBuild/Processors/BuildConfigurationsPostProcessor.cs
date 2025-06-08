using System.Collections.Generic;
using System.Linq;
using UniModules.Editor;

namespace UniGame.UniBuild.Editor
{
    using UniModules.UniGame.Core.Editor.Tools;
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
