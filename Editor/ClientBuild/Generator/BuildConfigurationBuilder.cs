namespace UniGame.UniBuild.Editor
{
    using global::UniGame.Runtime.Utils;
    using UniModules;
    using UniModules.Editor;
    using UnityEditor;

    public class BuildConfigurationBuilder
    {
        private static MemorizeItem<string, string> _fileContentCache =
            MemorizeTool.Memorize<string, string>(targetPath =>
            {
                if (string.IsNullOrEmpty(targetPath))
                    return string.Empty;

                var result = FileUtils.ReadContent(targetPath, false);
                return result.content;
            });

        private static string _cloudLocalPath = "UniBuild/Editor/" + CloudBuildMethodsGenerator.ClassFileName;
        private static string _cloudPath = FileUtils.Combine(EditorPathConstants.GeneratedContentPath, _cloudLocalPath);

        private static string _menuScript = string.Empty;
        private static string _cloudScript = string.Empty;

        public static string BuildPath => FileUtils
            .Combine(EditorPathConstants.GeneratedContentPath, "UniBuild/Editor/BuildMethods.cs");

        [MenuItem("UniGame/Uni Build/Rebuild Menu")]
        public static void RebuildMenuAction()
        {
            Rebuild(true);
        }

        public static void Rebuild(bool forceUpdate = false)
        {
            RebuildMenu(forceUpdate);
            RebuildCloudMethods(forceUpdate);
        }

        public static bool RebuildMenu(bool force = false)
        {
#if UNITY_CLOUD_BUILD
            return false;
#endif
            var generator = new BuildMenuGenerator();
            var script = generator.CreateBuilderScriptBody();
            var result = WriteUnityFile(script, BuildPath, force);

            return result;
        }

        public static bool RebuildCloudMethods(bool force = false)
        {
#if UNITY_CLOUD_BUILD
            return false;
#endif
            var cloudGenerator = new CloudBuildMethodsGenerator();
            var content = cloudGenerator.CreateCloudBuildMethods();
            var result = WriteUnityFile(content, _cloudPath, force);
            return result;
        }

        public static bool WriteUnityFile(string scriptValue, string path, bool force = false)
        {
            var content = _fileContentCache[path];

            if (string.IsNullOrEmpty(scriptValue))
                return false;

            if (!force && scriptValue.Equals(content))
                return false;

            var result = FileUtils.WriteAssetsContent(path, scriptValue);
            if (result)
                _fileContentCache[path] = scriptValue;

            return result;
        }

    }
}