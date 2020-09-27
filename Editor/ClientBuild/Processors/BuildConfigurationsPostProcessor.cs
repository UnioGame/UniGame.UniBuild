namespace UniModules.UniGame.UniBuild.Editor.ClientBuild.Processors
{
    using Core.Editor.Tools;
    using Generator;
    using UnityEditor;

    public class BuildConfigurationsPostProcessor : AssetPostprocessor
    {
        static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
        {
            UniEditorProfiler.LogTime(nameof(BuildConfigurationsPostProcessor), BuildConfigurationBuilder.Rebuild);
        }
    }
}
