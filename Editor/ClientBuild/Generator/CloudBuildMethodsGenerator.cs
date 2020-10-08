using System.Text.RegularExpressions;
using UniGame;

namespace UniModules.UniGame.UniBuild.Editor.ClientBuild.Generator
{
    using BuildConfiguration;
    using Core.EditorTools.Editor.AssetOperations;
    using Core.Runtime.Extension;
    using UnityEngine;

    public class CloudBuildMethodsGenerator
    {
        private const string PreExportRegExpText = @"\/\/=====ExportMethods=====(?<category>[\w|\W|\s|\S]*)\/\/=====ExportMethodsEnd=====";
        
        private static Regex exportMethodsRegEx = new Regex(PreExportRegExpText,RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public const string MethodsKey   = "//%CLOUD-METHODS%";
        public const string BuildConfigKey = "CONFIG_NAME";
        public const string ConfigGUIDKey   = "%BUILDMAP-GUID%";

        private static string _classTemplate;
        private static TextAsset _methodsTemplate;

        public static  string ClassTemplate  => (_classTemplate = string.IsNullOrEmpty(_classTemplate) ?
            typeof(CloudBuildHelper).GetScriptAsset()?.text : 
            _classTemplate);

        public string LoadMethodsTemplate()
        {            
            var classTextAsset   = ClassTemplate;

            if (string.IsNullOrEmpty(classTextAsset)) {
                Debug.LogWarning($"CreateCloudBuildClass: ERROR CLASS {ClassTemplate} NULL value");
                return string.Empty;
            }
            
            if(!exportMethodsRegEx.IsMatch(classTextAsset))
            {
                Debug.LogWarning($"exportMethodsRegEx: ERROR Methods {ClassTemplate} Missings");
                return string.Empty;
            }

            var exportMethodsMatch = exportMethodsRegEx.Match(classTextAsset);
            var exportMethods = exportMethodsMatch.Value;
            
            if (string.IsNullOrEmpty(exportMethods)) {
                Debug.LogWarning($"CreateCloudBuildMethods: ERROR METHODS EMPTY NULL value");
                return string.Empty;
            }

            return exportMethods;
        }
        
        public string CreateCloudBuildMethods()
        {
            var methodsTextAsset = LoadMethodsTemplate();
            
            var commands = AssetEditorTools.GetAssets<UniBuildCommandsMap>();

            var methodsValue = string.Empty;
            
            foreach (var command in commands) {
                var guid = AssetEditorTools.GetGUID(command);
                var methodValue = methodsTextAsset.Replace(ConfigGUIDKey, guid);
                var methodName = command.name.RemoveSpecialAndDotsCharacters();
                methodsValue += methodValue.Replace(BuildConfigKey, methodName);
            }
            
            return ClassTemplate.Replace(MethodsKey,methodsValue);
        }

    }
}
