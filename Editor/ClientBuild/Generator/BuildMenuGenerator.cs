namespace UniModules.UniGame.UniBuild.Editor.ClientBuild.Generator
{
    using System.Collections.Generic;
    using System.Linq;
    using BuildConfiguration;
    using Core.Runtime.Extension;
    using global::CodeWriter.Editor.UnityTools;
    using UniGreenModules.UniCore.EditorTools.Editor.AssetOperations;
    using UnityEditor;

    public class BuildMenuGenerator
    {
        private readonly BuildConfigurationBuilder buildConfigurationBuilder;
        private const string _menuTemplate = "[MenuItem(\"UniGame/Uni Build/UniBuild_{0}\")]\n";

        public ScriptData CreateBuilderScriptBody()
        {
            var scriptData = new ScriptData() {
                Namespace = "namespace UniGame.UniBuild",
                Name = "UniPlatformBuilder",
                Usings = new [] {
                    typeof(MenuItem).Namespace,
                    typeof(UniBuildTool).Namespace
                },
                Methods = GetBuildMethods().ToArray()
            };

            return scriptData;
        }

        public string[] GetBuildMethods()
        {
            var map = new List<string>();
            var commands = AssetEditorTools.GetAssets<UniBuildCommandsMap>();
            foreach (var command in commands) {
                map.Add(CreateBuildMethod(command));
            }
            return map.ToArray();
        }

        public string CreateBuildMethod(UniBuildCommandsMap config)
        {
            var name = config.ItemName.RemoveSpecialAndDotsCharacters();
            var id = AssetEditorTools.GetGUID(config);
            var method = $"{string.Format(_menuTemplate,name)} public static void Build_{name}() => UniBuildTool.BuildByConfigurationId(\"{id}\");";
            return method;
        }
    }
    
}



