namespace UniModules.UniGame.UniBuild.Editor.ClientBuild.Generator
{
    using BuildConfiguration;
    using Core.EditorTools.Editor.AssetOperations;
    using Core.Runtime.Extension;
    using UnityEngine;

    public class CloudBuildMethodsGenerator
    {

        public const string ClassTemplatePath = "CloudBuildTemplateAsset";
        public const string MethodsTemplatePath   = "CloudBuildMethodsTemplateAsset";
        public const string MethodsKey   = "%CLOUD-METHODS%";
        public const string BuildConfigKey = "%CONFIG_NAME%";
        public const string ConfigGUIDKey   = "%BUILDMAP-GUID%";

        private static TextAsset _classTemplate;
        private static TextAsset _methodsTemplate;
        
        public static  TextAsset ClassTemplate   => (_classTemplate = _classTemplate ? Resources.Load<TextAsset>(ClassTemplatePath) : _classTemplate);
        public static  TextAsset MethodsTemplate => (_methodsTemplate = _methodsTemplate ? Resources.Load<TextAsset>(MethodsTemplatePath) : _methodsTemplate);

        public string CreateCloudBuildMethods()
        {
            var classTextAsset   = ClassTemplate?.text;
            var methodsTextAsset = MethodsTemplate?.text;

            if (string.IsNullOrEmpty(classTextAsset)) {
                Debug.LogWarning($"CreateCloudBuildClass: ERROR CLASS {ClassTemplatePath} NULL value");
                return string.Empty;
            }
            
            if (string.IsNullOrEmpty(methodsTextAsset)) {
                Debug.LogWarning($"CreateCloudBuildMethods: ERROR METHODS {MethodsTemplatePath} NULL value");
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
