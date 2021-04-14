using System.Text.RegularExpressions;

namespace UniModules.UniGame.UniBuild.Editor.ClientBuild.Generator
{
    using CodeWriter.Editor.UnityTools;
    using Core.EditorTools.Editor;
    using Core.EditorTools.Editor.Tools;
    using UnityEditor;

    public class BuildConfigurationBuilder
    {
        private static string _cloudLocalPath = "UniBuild/Editor/" + CloudBuildMethodsGenerator.ClassFileName;
        private static string _path = EditorFileUtils.Combine(EditorPathConstants.GeneratedContentPath,"UniBuild/Editor/BuildMethods.cs");
        private static string _cloudPath = EditorFileUtils.Combine(EditorPathConstants.GeneratedContentPath,_cloudLocalPath);

        private static string _menuScript  = string.Empty;
        private static string _cloudScript = string.Empty;

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
            var result = script.CreateScript(_path,force);
            
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