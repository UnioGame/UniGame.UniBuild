namespace UniModules.UniGame.UniBuild.Editor.ClientBuild.Generator
{
    using BuildConfiguration;
    using Core.EditorTools.Editor.AssetOperations;
    using Core.Runtime.Extension;
    using UnityEngine;

    public class CloudBuildMethodsGenerator
    {

        public const string ClassTemplatePath = "CloudBuildTemplateAsset";
        public const string MethodsTemplate   = "CloudBuildMethodsTemplateAsset";
        public const string MethodsKey   = "%CLOUD-METHODS%";
        public const string BuildConfigKey = "%CONFIG_NAME%";
        public const string ConfigGUIDKey   = "%BUILDMAP-GUID%";


        public string CreateCloudBuildMethods()
        {
            var classAsset = Resources.Load<TextAsset>(ClassTemplatePath);
            var methodsAsset = Resources.Load<TextAsset>(MethodsTemplate);
            var classTextAsset = classAsset?.text;
            var methodsTextAsset = methodsAsset?.text;

            if (string.IsNullOrEmpty(classTextAsset)) {
                Debug.LogWarning($"CreateCloudBuildClass: ERROR CLASS {ClassTemplatePath} NULL value");
                return string.Empty;
            }
            
            if (string.IsNullOrEmpty(methodsTextAsset)) {
                Debug.LogWarning($"CreateCloudBuildMethods: ERROR METHODS {MethodsTemplate} NULL value");
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
