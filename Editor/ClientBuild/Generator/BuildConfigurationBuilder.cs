namespace UniModules.UniGame.UniBuild.Editor.ClientBuild.Generator
{
    using CodeWriter.Editor.UnityTools;
    using Core.EditorTools.Editor;
    using Core.EditorTools.Editor.Tools;
    using UnityEditor;

    public class BuildConfigurationBuilder
    {
        private static string _path =  
                EditorFileUtils.Combine(EditorPathConstants.GeneratedContentPath,"UniBuild/Editor/BuildMethods.cs");

        private static string _cloudPath = 
                EditorFileUtils.Combine(EditorPathConstants.GeneratedContentPath,"UniBuild/Editor/CloudBuildMethods.cs");

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
            return;
#endif
            var generator  = new BuildMenuGenerator();
            var script     = generator.CreateBuilderScriptBody();
            var scriptData = script.Convert();
            
            if (string.IsNullOrEmpty(scriptData))
                return false;
            if (!force && _menuScript.Equals(scriptData))
                return false;
            
            var result =script.CreateScript(_path);
            _menuScript = scriptData;
            return result;
        }
        
        public static bool RebuildCloudMethods(bool force = false)
        {
#if UNITY_CLOUD_BUILD
            return;
#endif
            var cloudGenerator = new CloudBuildMethodsGenerator();
            var content        = cloudGenerator.CreateCloudBuildMethods();
            
            if (string.IsNullOrEmpty(content))
                return false;
            if (!force && _menuScript.Equals(content))
                return false;
            
            var result = content.WriteUnityFile(_cloudPath);
            _cloudScript = content;
            return result;
        }
    }
}