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
        private static CloudBuildArgs CloudBuildArgs => args = args ?? LoadCloudBuildArgs();

        //%CLOUD-METHODS%"

        //=====ExportMethods=====
        public static void PreExportCONFIG_NAME()
        {
            Debug.Log("UNI BUILD: START PreExport COMMAND");

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

            if (CloudBuildArgs == null)
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

            var buildTarget = argumentsProvider.GetBuildTarget();
            var buildTargetGroup = argumentsProvider.GetBuildTargetGroup();

            var buildParameters = new BuildParameters(buildTarget, buildTargetGroup, argumentsProvider)
            {
                buildTarget = buildTarget,
                environmentType = BuildEnvironmentType.UnityCloudBuild,
            };

            if (CloudBuildArgs != null)
            {
                buildParameters.buildNumber = CloudBuildArgs.BuildNumber;
                buildParameters.projectId = CloudBuildArgs.ProjectId;
                buildParameters.bundleId = CloudBuildArgs.BundleId;
                buildParameters.buildNumber = CloudBuildArgs.BuildNumber;
                buildParameters.branch = CloudBuildArgs.ScmBranch;
            }

            var result = new EditorBuildConfiguration(argumentsProvider, buildParameters);
            return result;
        }

        /// <summary>
        /// https://docs.unity3d.com/Manual/UnityCloudBuildManifest.html
        /// </summary>
        private static CloudBuildArgs LoadCloudBuildArgs()
        {
            var manifestAsset = (TextAsset) Resources.Load(ManifestFileName);
            var cloudBuildArgs = new CloudBuildArgs();

            if (manifestAsset)
            {
                var manifest = JsonConvert.DeserializeObject<Dictionary<string, object>>(manifestAsset.text);
                cloudBuildArgs.BuildNumber = int.Parse(manifest["buildNumber"].ToString());
                cloudBuildArgs.BundleId = manifest["bundleId"].ToString();
                cloudBuildArgs.ProjectId = manifest["projectId"].ToString();
                cloudBuildArgs.ScmBranch = manifest["scmBranch"].ToString();
                cloudBuildArgs.CloudBuildTargetName = manifest["cloudBuildTargetName"].ToString();
                cloudBuildArgs.ScmCommitId = manifest["scmCommitId"].ToString();
            }

            return cloudBuildArgs;
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