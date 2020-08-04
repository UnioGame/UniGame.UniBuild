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
            var generator = new BuildMenuGenerator();
            var script    = generator.CreateBuilderScriptBody();
            script.CreateScript(_path);
        }
        
        public static void RebuildCloudMethods()
        {
            var cloudGenerator = new CloudBuildMethodsGenerator();
            var content        = cloudGenerator.CreateCloudBuildMethods();
            content.WriteUnityFile(_cloudPath);
        }
    }
}