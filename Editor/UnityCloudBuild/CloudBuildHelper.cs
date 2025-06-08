//Use UniGame.CloudBuildHelper.[PreExport || PostExport]

namespace UniGame
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Newtonsoft.Json;
    using UniBuild.Editor.ClientBuild.BuildConfiguration;
    using UniBuild.Editor.ClientBuild.Interfaces;
    using UniBuild.Editor;
    using UniBuild.Editor.Extensions;
    using UniBuild.Editor.Interfaces;
    using UniModules.UniGame.UniBuild.Editor.UnityCloudBuild;
    using UnityEditor;
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

            var guid = "%BUILDMAP-GUID%";
            var assetPath = AssetDatabase.GUIDToAssetPath(guid);
            var configuration = AssetDatabase.LoadAssetAtPath<UniBuildCommandsMap>(assetPath);

            var isCloudBuild = false;
#if UNITY_CLOUD_BUILD
            isCloudBuild = true;
#endif
            if (isCloudBuild)
            {
                var parameters = CreateCommandParameters();
                var builder = new UnityPlayerBuilder();
                builder.ExecuteCommands(configuration.PreBuildCommands, x => x.Execute(parameters));
            }
            else
            {
                UniBuildTool.ExecuteBuild(configuration);
            }
            
            Debug.Log("UNI BUILD: START PreExport COMMAND");
        }

        public static void PostExportCONFIG_NAME(string exportPath)
        {
            Debug.Log($"UNI BUILD: START PostExport Path {exportPath} COMMAND");

            if (string.IsNullOrEmpty(exportPath))
            {
                Debug.LogError("ExportPath is EMPTY PreExport methods can be skipped");
            }

            var parameters = CreateCommandParameters();
            var artifactPath = UnityCloudPostBuild.OutputFiles.FirstOrDefault();
            if (string.IsNullOrEmpty(artifactPath) == false)
            {
                parameters.BuildParameters.artifactPath = artifactPath;
            }

            var builder = new UnityPlayerBuilder();

            var guid = "%BUILDMAP-GUID%";
            var assetPath = AssetDatabase.GUIDToAssetPath(guid);
            var configuration = AssetDatabase.LoadAssetAtPath<UniBuildCommandsMap>(assetPath);
            builder.ExecuteCommands(configuration.PostBuildCommands, x => x.Execute(parameters));
        }
        
        //=====ExportMethodsEnd=====
        
        public static IUniBuilderConfiguration CreateCommandParameters()
        {
            var argumentsProvider = new ArgumentsProvider(Environment.GetCommandLineArgs());

            Debug.LogFormat("\n[CloudBuildHelper] {0} \n", argumentsProvider);

            var buildTarget = argumentsProvider.GetBuildTarget();
            var buildTargetGroup = argumentsProvider.GetBuildTargetGroup();

            var buildData = new UniBuildConfigurationData()
            {
                buildTarget = buildTarget,
                buildTargetGroup = buildTargetGroup,
            };
            
            var buildParameters = new BuildParameters(buildData, argumentsProvider)
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
            
            if (!manifestAsset) return null;
            
            var cloudBuildArgs = new CloudBuildArgs();
            var manifest = JsonConvert.DeserializeObject<Dictionary<string, object>>(manifestAsset.text);
            cloudBuildArgs.BuildNumber = int.Parse(manifest["buildNumber"].ToString());
            cloudBuildArgs.BundleId = manifest["bundleId"].ToString();
            cloudBuildArgs.ProjectId = manifest["projectId"].ToString();
            cloudBuildArgs.ScmBranch = manifest["scmBranch"].ToString();
            cloudBuildArgs.CloudBuildTargetName = manifest["cloudBuildTargetName"].ToString();
            cloudBuildArgs.ScmCommitId = manifest["scmCommitId"].ToString();

            return cloudBuildArgs;
        }

    }
}