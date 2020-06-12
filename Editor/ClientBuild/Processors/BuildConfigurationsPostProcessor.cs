using UnityEditor;

namespace UniModules.UniGame.UnityBuild.Editor.ClientBuild.Processors
{
    using Generator;

    public class BuildConfigurationsPostProcessor : AssetPostprocessor
    {
        static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
        {
            BuildConfigurationBuilder.Rebuild();
        }
    }
}
