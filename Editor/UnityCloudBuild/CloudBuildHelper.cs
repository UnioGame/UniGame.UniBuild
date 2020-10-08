//Use UniGame.CloudBuildHelper.[PreExport || PostExport]

namespace UniGame
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Newtonsoft.Json;
    using UniModules.UniGame.UniBuild.Editor.ClientBuild;
    using UniModules.UniGame.UniBuild.Editor.ClientBuild.BuildConfiguration;
    using UniModules.UniGame.UniBuild.Editor.ClientBuild.Extensions;
    using UniModules.UniGame.UniBuild.Editor.ClientBuild.Interfaces;
    using UniModules.UniGame.UniBuild.Editor.UnityCloudBuild;
    using UnityEditor;
    using UnityEditor.Build;
    using UnityEditor.Build.Reporting;
    using UnityEngine;

    //https://docs.unity3d.com/Manual/UnityCloudBuildManifestAsScriptableObject.html
    //https://docs.unity3d.com/Manual/UnityCloudBuildManifest.html
    public static class CloudBuildHelper
    {
        private const string ManifestFileName = "UnityCloudBuildManifest.json";


        private static CloudBuildArgs args;

        public static CloudBuildArgs LoadCloudBuildArgs()
        {
            var manifestAsset = (TextAsset) Resources.Load(ManifestFileName);
            CloudBuildArgs cloudBuildArgs = null;
            Dictionary<string, object> manifest = null;

            if (manifestAsset)
            {
                manifest = JsonConvert.DeserializeObject<Dictionary<string, object>>(manifestAsset.text);
                cloudBuildArgs = new CloudBuildArgs(
                    int.Parse(manifest["buildNumber"].ToString()),
                    manifest["bundleId"].ToString(),
                    manifest["projectId"].ToString(),
                    manifest["scmBranch"].ToString(),
                    manifest["cloudBuildTargetName"].ToString(),
                    manifest["bundleId"].ToString()
                );
            }

            return cloudBuildArgs;
        }

        //%CLOUD-METHODS%"

        //=====ExportMethods=====
        public static void PreExportCONFIG_NAME()
        {
            Debug.Log("UNI BUILD: START PreExport COMMAND");

            args = LoadCloudBuildArgs();

            Debug.Log($"UNI BUILD: ARGS\n {args}");

            var parameters = CreateCommandParameters();
            var builder = new UnityPlayerBuilder();

            var guid = "%BUILDMAP-GUID%";
            var assetPath = AssetDatabase.GUIDToAssetPath(guid);
            var configuration = AssetDatabase.LoadAssetAtPath<UniBuildCommandsMap>(assetPath);
            builder.ExecuteCommands(configuration.PreBuildCommands, x => x.Execute(parameters));
            
            Debug.Log("UNI BUILD: START PreExport COMMAND");
        }

        public static void PostExportCONFIG_NAME(string exportPath)
        {
            Debug.Log($"UNI BUILD: START PostExport Path {exportPath} COMMAND");

            if (string.IsNullOrEmpty(exportPath))
            {
                Debug.LogError("ExportPath is EMPTY PreExport methods can be skipped");
            }

            if (args == null)
            {
                Debug.LogError("Error: PostExport skipped because args is NULL");
                return;
            }

            var parameters = CreateCommandParameters();
            var artifactPath = UnityCloudPostBuild.OutputFiles.FirstOrDefault();
            if (string.IsNullOrEmpty(artifactPath) == false)
            {
                parameters.BuildParameters.ArtifactPath = artifactPath;
            }

            var builder = new UnityPlayerBuilder();

            var guid = "%BUILDMAP-GUID%";
            var assetPath = AssetDatabase.GUIDToAssetPath(guid);
            var configuration = AssetDatabase.LoadAssetAtPath<UniBuildCommandsMap>(assetPath);
            builder.ExecuteCommands(configuration.PostBuildCommands, x => x.Execute(parameters));
        }
        
        //=====ExportMethodsEnd=====
        
        private static IUniBuilderConfiguration CreateCommandParameters()
        {
            var argumentsProvider = new ArgumentsProvider(Environment.GetCommandLineArgs());

            Debug.LogFormat("\n[CloudBuildHelper] {0} \n", argumentsProvider);
            Debug.Log(args.ToString());

            var buildTarget = argumentsProvider.GetBuildTarget();
            var buildTargetGroup = argumentsProvider.GetBuildTargetGroup();

            var buildParameters = new BuildParameters(buildTarget, buildTargetGroup, argumentsProvider)
            {
                buildNumber = args.BuildNumber,
                buildTarget = buildTarget,
                projectId = args.ProjectId,
                bundleId = args.BundleId,
                environmentType = BuildEnvironmentType.UnityCloudBuild,
                branch = args.ScmBranch,
            };

            var cloudBuildArgs = LoadCloudBuildArgs();
            if (cloudBuildArgs != null)
            {
                buildParameters.buildNumber = cloudBuildArgs.BuildNumber;
            }

            var result = new EditorBuildConfiguration(argumentsProvider, buildParameters);
            return result;
        }

    }

    public class UnityCloudPostBuild : IPostprocessBuildWithReport
    {
        public static string BuildFileKey = nameof(BuildFileKey);
        private static List<string> buildFiles = new List<string>();

        public static List<string> OutputFiles
        {
            get
            {
                if (buildFiles != null)
                    return buildFiles;
                var value = EditorPrefs.HasKey(BuildFileKey) ?
                    EditorPrefs.GetString(BuildFileKey) :
                    string.Empty;
                buildFiles = string.IsNullOrEmpty(value)
                    ? new List<string>()
                    : JsonConvert.DeserializeObject<List<string>>(value);

                return buildFiles;
            }
            set => buildFiles = value;
        }

        public int callbackOrder { get; } = 0;

        public void OnPostprocessBuild(BuildReport report)
        {
            Debug.Log($"===== UNIBUILD{nameof(UnityCloudPostBuild)} : {report}");

            var files = report.files.Select(x => x.path).ToList();

            var buildResults = JsonConvert.SerializeObject(files);
            
            EditorPrefs.SetString(BuildFileKey,buildResults);

            OutputFiles = files;
        }
    }
}