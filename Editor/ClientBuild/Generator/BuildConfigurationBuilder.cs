namespace UniModules.UniGame.UniBuild.Editor.ClientBuild.Generator
{
    using CodeWriter.Editor.UnityTools;
    using UnityEditor;
    using UnityEngine;

    public class BuildConfigurationBuilder
    {
        private static string _path = "UniGame.Generated/UniBuild/Editor/BuildMethods.cs";
        private static string _cloudPath = "UniGame.Generated/UniBuild/Editor/CloudBuildMethods.cs";

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
            Debug.Log("Rebuild UniBuild Menu");
        }
        
        public static void RebuildCloudMethods()
        {
            var cloudGenerator = new CloudBuildMethodsGenerator();
            var content        = cloudGenerator.CreateCloudBuildMethods();
            content.WriteUnityFile(_cloudPath);
            
            Debug.Log("Rebuild UniCloudBuild Methods");
        }
    }
}