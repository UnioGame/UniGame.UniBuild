namespace UniModules.UniGame.UnityBuild.Editor.ClientBuild.Generator
{
    using System.IO;
    using CodeWriter.Editor.UnityTools;
    using UnityEditor;
    using UnityEngine;

    
    public class BuildConfigurationBuilder
    {
        private static string _path = "UniGame.Generated/UniBuild/Editor/BuildMethods.cs";

        [MenuItem("UniGame/UniBuild/Rebuild Menu")]
        public static void Rebuild()
        {
            var generator = new BuildMenuGenerator();
            var script = generator.CreateBuilderScriptBody();
            script.CreateScript(_path);
    
            Debug.Log("Rebuild UniBuild Configuration");
        }
    }
}