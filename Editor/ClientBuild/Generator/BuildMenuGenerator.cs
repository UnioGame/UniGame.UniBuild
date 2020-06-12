namespace UniModules.UniGame.UnityBuild.Editor.ClientBuild.Generator
{
    using System.Collections.Generic;
    using BuildConfiguration;
    using CodeWriter;
    using UniGreenModules.UniCore.EditorTools.Editor.AssetOperations;
    using UniGreenModules.UniGame.UnityBuild.Editor.ClientBuild;

    public struct MethodData
    {
        public string Name;
        public string Content;
    }
    
    public class BuildMenuGenerator
    {
        private readonly BuildConfigurationBuilder buildConfigurationBuilder;

        public BuildMenuGenerator()
        {
            buildConfigurationBuilder = new BuildConfigurationBuilder(this);
        }

        public BuildConfigurationBuilder BuildConfigurationBuilder {
            get { return buildConfigurationBuilder; }
        }

        public string CreateBuilderScriptBody()
        {
            var methods = GetBuildMethods();
            var writer = new CodeWriter(CodeWriterSettings.CSharpDefault);
            var builderNamespace = typeof(UniBuildTool).Namespace;
            using (writer.B("namespace UniGame.UnityBuild")) {
                writer._("using UnityEditor;");
                writer._($"using {builderNamespace};\n\n");

                using (writer.B("public static class UniPlatformBuilder")) {
                    foreach (var buildMethod in methods) {
                        var method = buildMethod.Value;
                        writer._($"[MenuItem(\"UniGame/UniBuild/UniBuild_{method.Name}\")]");
                        writer._($"{method.Content}\n");
                    }
                }

            }

            return writer.ToString();
        }

        public Dictionary<UniBuildCommandsMap, MethodData> GetBuildMethods()
        {
            var map = new Dictionary<UniBuildCommandsMap,MethodData>();
            var commands = AssetEditorTools.GetAssets<UniBuildCommandsMap>();
            foreach (var command in commands) {
                map[command] = CreateBuildMethod(command);
            }
            return map;
        }

        public MethodData CreateBuildMethod(UniBuildCommandsMap config)
        {
            var name = config.ItemName.Replace(" ", string.Empty);
            var id = AssetEditorTools.GetGUID(config);
            var method = $"public static void Build_{name}() => UniBuildTool.BuildByConfigurationId(\"{id}\");";
            return new MethodData() {
                Content = method,
                Name = name,
            };
        }
    }
    
}



