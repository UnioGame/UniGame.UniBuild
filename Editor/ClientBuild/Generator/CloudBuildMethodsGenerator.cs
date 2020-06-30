namespace UniModules.UniGame.UniBuild.Editor.ClientBuild.Generator
{
    using BuildConfiguration;
    using Core.Runtime.Extension;
    using UniGreenModules.UniCore.EditorTools.Editor.AssetOperations;
    using UnityEditor;
    using UnityEngine;

    public class CloudBuildMethodsGenerator
    {

        public static string ClassTemplatePath = "CloudBuildTemplateAsset";
        public static string MethodsTemplate   = "CloudBuildMethodsTemplateAsset";
        public static string MethodsKey   = "%CLOUD-METHODS%";
        public static string BuildConfigKey = "%CONFIG_NAME%";
        public static string ConfigGUIDKey   = "%BUILDMAP-GUID%";


        public string CreateCloudBuildMethods()
        {
            var classTextAsset = Resources.Load<TextAsset>(ClassTemplatePath)?.text;
            var methodsTextAsset = Resources.Load<TextAsset>(MethodsTemplate)?.text;

            if (string.IsNullOrEmpty(classTextAsset)) {
                Debug.LogError($"CreateCloudBuildMethods: ERROR CLASS {ClassTemplatePath} NULL value");
                return string.Empty;
            }
            
            if (string.IsNullOrEmpty(methodsTextAsset)) {
                Debug.LogError($"CreateCloudBuildMethods: ERROR METHODS {MethodsTemplate} NULL value");
                return string.Empty;
            }
            
            var commands = AssetEditorTools.GetAssets<UniBuildCommandsMap>();

            var methodsValue = string.Empty;
            
            foreach (var command in commands) {
                var guid = AssetEditorTools.GetGUID(command);
                var methodValue = methodsTextAsset.Replace(ConfigGUIDKey, guid);
                var methodName = command.name.RemoveSpecialAndDotsCharacters();
                methodsValue += methodValue.Replace(BuildConfigKey, methodName);
            }
            
            return classTextAsset.Replace(MethodsKey,methodsValue);
        }

    }
}
