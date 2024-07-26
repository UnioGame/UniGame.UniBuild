namespace UniModules.UniGame.UniBuild.Editor.ClientBuild.Generator
{
    using System.Collections.Generic;
    using System.Linq;
    using BuildConfiguration;
    using UniModules.Editor;
    using global::UniGame.Core.Runtime.Extension;
    using global::CodeWriter.Editor.UnityTools;
    using UnityEditor;

    public class BuildMenuGenerator
    {
        private readonly BuildConfigurationBuilder buildConfigurationBuilder;
        private const string _menuTemplate = "[MenuItem(\"UniGame/Uni Build/{0}_Build\")]\n";
        private const string _menuAndRunTemplate = "[MenuItem(\"UniGame/Uni Build/{0}_Build_And_Run\")]\n";
        private const string _buildTemplate = " Build_{0}";
        private const string _buildAndRunTemplate = " BuildAndRun_{0}";

        public ScriptData CreateBuilderScriptBody()
        {
            var scriptData = new ScriptData() {
                Namespace = "namespace UniGame.UniBuild",
                Name = "UniPlatformBuilder",
                Usings = new List<string>() {
                    typeof(MenuItem).Namespace,
                    typeof(UniBuildTool).Namespace
                },
                Methods = GetBuildMethods().ToList()
            };

            return scriptData;
        }

        public string[] GetBuildMethods()
        {
            var map = new List<string>();
            var commands = AssetEditorTools.GetAssets<UniBuildCommandsMap>();
            foreach (var command in commands) {
                map.Add(CreateBuildMethod(_menuTemplate,_buildTemplate,nameof(UniBuildTool.BuildByConfigurationId),command));
            }
            foreach (var command in commands) {
                map.Add(CreateBuildMethod(_menuAndRunTemplate,_buildAndRunTemplate,nameof(UniBuildTool.BuildAndRunByConfigurationId),command));
            }
            return map.ToArray();
        }

        public string CreateBuildMethod(string template,string menuTemplate,string buildMethod,UniBuildCommandsMap config)
        {
            var name = config.ItemName.RemoveSpecialAndDotsCharacters();
            var id = config.GetGUID();
            var menuMethodName = string.Format(menuTemplate,name);
            var method = $"{string.Format(template,name)} public static void {menuMethodName}() => UniBuildTool.{buildMethod}(\"{id}\");";
             return method;
        }
    }
    
}



