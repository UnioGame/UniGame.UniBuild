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

        [MenuItem("UniGame/Uni Build/Rebuild Menu")]
        public static void Rebuild()
        {
            RebuildMenu();
            RebuildCloudMethods();
        }

        public static void RebuildMenu()
        {
#if UNITY_CLOUD_BUILD
            return;
#endif
            var generator = new BuildMenuGenerator();
            var script    = generator.CreateBuilderScriptBody();
            script.CreateScript(_path);
        }
        
        public static void RebuildCloudMethods()
        {
#if UNITY_CLOUD_BUILD
            return;
#endif
            var cloudGenerator = new CloudBuildMethodsGenerator();
            var content        = cloudGenerator.CreateCloudBuildMethods();
            if (string.IsNullOrEmpty(content))
                return;
            content.WriteUnityFile(_cloudPath);
        }
    }
}