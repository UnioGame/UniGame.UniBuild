namespace UniModules.UniGame.UnityBuild.Editor.ClientBuild.Generator
{
    using System.IO;
    using UnityEditor;
    using UnityEngine;

    public class BuildConfigurationBuilder
    {
        private BuildMenuGenerator buildMenuGenerator;
        private static string[] _folders = {"UniGame.Generated","UniBuild","Editor"};
        private static string _scriptFileName = "BuildMethods.cs";

        public BuildConfigurationBuilder(BuildMenuGenerator buildMenuGenerator)
        {
            this.buildMenuGenerator = buildMenuGenerator;
        }

        [MenuItem("UniGame/UniBuild/Rebuild Menu")]
        public static void Rebuild()
        {
            var generator = new BuildMenuGenerator();
            var path      = Application.dataPath;

            foreach (var folder in _folders) {
                path = Path.Combine(path,folder);
                if (Directory.Exists(path)) {
                    continue;
                }

                Debug.Log(path);
                Directory.CreateDirectory(path);
            }

            path = Path.Combine(path,_scriptFileName);
            var activeContent = File.Exists(path) ? File.ReadAllText(path) :  string.Empty;
            var generatedData = generator.CreateBuilderScriptBody();
            
            if (string.Equals(activeContent, generatedData)) {
                return;
            }
            
            File.WriteAllText(path,generatedData);
            
            Debug.Log("Rebuild UniBuild Configuration");
            
            AssetDatabase.Refresh();
        }
    }
}