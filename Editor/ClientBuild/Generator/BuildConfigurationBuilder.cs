
namespace UniModules.UniGame.UniBuild.Editor.ClientBuild.Generator
{
    using UniModules.Editor;
    using CodeWriter.Editor.UnityTools;
    using Core.EditorTools.Editor;
    using UnityEditor;

    public class BuildConfigurationBuilder
    {
        private static string _cloudLocalPath = "UniBuild/Editor/" + CloudBuildMethodsGenerator.ClassFileName;
        private static string _cloudPath = FileUtils.Combine(EditorPathConstants.GeneratedContentPath,_cloudLocalPath);

        private static string _menuScript  = string.Empty;
        private static string _cloudScript = string.Empty;

        public static string BuildPath => FileUtils.Combine(EditorPathConstants.GeneratedContentPath,"UniBuild/Editor/BuildMethods.cs");
        
        [MenuItem("UniGame/Uni Build/Rebuild Menu")]
        public static void RebuildMenuAction()
        {
            Rebuild(true);
        }

        public static void Rebuild(bool forceUpdate = false)
        {
            RebuildMenu(forceUpdate);
            RebuildCloudMethods(forceUpdate);
        }

        public static bool RebuildMenu(bool force = false)
        {
#if UNITY_CLOUD_BUILD
            return false;
#endif
            var generator  = new BuildMenuGenerator();
            var script     = generator.CreateBuilderScriptBody();
            var result = script.CreateScript(BuildPath,force);
            
            return result;
        }
        
        public static bool RebuildCloudMethods(bool force = false)
        {
#if UNITY_CLOUD_BUILD
            return false;
#endif
            var cloudGenerator = new CloudBuildMethodsGenerator();
            var content        = cloudGenerator.CreateCloudBuildMethods();
            var result = content.WriteUnityFile(_cloudPath,force);
            return result;
        }
    }
}