namespace UniModules.UniGame.UniBuild.Editor.ClientBuild.Processors
{
    using Generator;
    using UnityEditor;

    public class BuildConfigurationsPostProcessor : AssetPostprocessor
    {
        static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
        {
            BuildConfigurationBuilder.Rebuild();
        }
    }
}
