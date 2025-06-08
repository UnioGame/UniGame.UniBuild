namespace UniGame.UniBuild.Editor
{
    using System.Collections.Generic;
    using System.Linq;
    using UniModules.Editor;
    using global::UniGame.Core.Runtime.Extension;
    using UnityEditor;

    public class BuildMenuGenerator
    {
        private readonly BuildConfigurationBuilder buildConfigurationBuilder;

        public const string ClassTemplate = "namespace UniGame.UniBuild\n{{ \n{0} \npublic static class {1} \n{{ \n{2}\n }} }}";
        
        private const string MenuTemplate = "[MenuItem(\"UniGame/Uni Build/{0}_Build\")]\n";
        private const string MenuAndRunTemplate = "[MenuItem(\"UniGame/Uni Build/{0}_Build_And_Run\")]\n";
        private const string BuildTemplate = " Build_{0}";
        private const string BuildAndRunTemplate = " BuildAndRun_{0}";
        private const string ClassName = "UniPlatformBuilder";

        public string CreateBuilderScriptBody()
        {
            var methods = GetBuildMethods().ToList();
            var usings = new List<string>() {
                typeof(MenuItem).Namespace,
                typeof(UniBuildTool).Namespace
            };

            var usingDirectives = usings
                .Select(u => $"using {u};")
                .Aggregate((current, next) => $"{current}\n{next}");
            
            var methodsBody = methods
                .Select(m => $"{m}\n")
                .Aggregate((current, next) => $"{current}{next}");
            
            var script = string.Format(ClassTemplate,usingDirectives,ClassName,methodsBody);
            var result = script;
            return result;
        }

        public string[] GetBuildMethods()
        {
            var map = new List<string>();
            var commands = AssetEditorTools.GetAssets<UniBuildCommandsMap>();
            foreach (var command in commands) {
                map.Add(CreateBuildMethod(MenuTemplate,BuildTemplate,nameof(UniBuildTool.BuildByConfigurationId),command));
            }
            foreach (var command in commands) {
                map.Add(CreateBuildMethod(MenuAndRunTemplate,BuildAndRunTemplate,nameof(UniBuildTool.BuildAndRunByConfigurationId),command));
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



