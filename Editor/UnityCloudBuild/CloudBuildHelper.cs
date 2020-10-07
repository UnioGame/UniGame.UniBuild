    

//Use UniGame.CloudBuildHelper.[PreExport || PostExport]
namespace UniGame
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Newtonsoft.Json;
        
    using UniCore.Runtime.ProfilerTools;
    using UniModules.UniGame.UniBuild.Editor.ClientBuild;
    using UniModules.UniGame.UniBuild.Editor.ClientBuild.BuildConfiguration;
    using UniModules.UniGame.UniBuild.Editor.ClientBuild.Commands.PostBuildCommands;
    using UniModules.UniGame.UniBuild.Editor.ClientBuild.Commands.PreBuildCommands;
    using UniModules.UniGame.UniBuild.Editor.ClientBuild.Extensions;
    using UniModules.UniGame.UniBuild.Editor.ClientBuild.Interfaces;
    using UniModules.UniGame.UniBuild.Editor.UnityCloudBuild;
    using UnityEditor;
    using UnityEditor.Build;
    using UnityEditor.Build.Reporting;
    using UnityEngine;

    public class DummyManifest
    {
        public T GetValue<T>()           => default(T);
        public T GetValue<T>(string key) => default(T);
    }
    
    //https://docs.unity3d.com/Manual/UnityCloudBuildManifestAsScriptableObject.html
    //https://docs.unity3d.com/Manual/UnityCloudBuildManifest.html
    public static class CloudBuildHelper
    {
        private static CloudBuildArgs args;

#if UNITY_CLOUD_BUILD
        public static void PreExport(UnityEngine.CloudBuild.BuildManifestObject manifest) {
#else
        public static void PreExport(DummyManifest manifest)
        {
#endif
            GameLog.Log("UNI BUILD: START PreExport COMMAND");
            
            args = new CloudBuildArgs(
                manifest.GetValue<int>("buildNumber"),
                manifest.GetValue<string>("bundleId"),
                manifest.GetValue<string>("projectId"),
                manifest.GetValue<string>("scmCommitId"),
                manifest.GetValue<string>("scmBranch"),
                manifest.GetValue<string>("cloudBuildTargetName")
            );

            GameLog.Log($"UNI BUILD: ARGS\n {args}");
            
            var parameters = CreateCommandParameters();
            var builder    = new UnityPlayerBuilder();

            var guid = "%BUILDMAP-GUID%";
            var assetPath = AssetDatabase.GUIDToAssetPath(guid);
            var configuration = AssetDatabase.LoadAssetAtPath<UniBuildCommandsMap>(assetPath);
            builder.ExecuteCommands(configuration.PreBuildCommands,x => x.Execute(parameters));
        }

        public static void PostExport(string exportPath)
        {
            GameLog.Log($"UNI BUILD: START PostExport Path {exportPath} COMMAND");
            
            if (string.IsNullOrEmpty(exportPath)) {
                Debug.LogError("ExportPath is EMPTY PreExport methods can be skipped");
            }

            if (args == null) {
                Debug.LogError("Error: PostExport skipped because args is NULL");
                return;
            }

            var parameters   = CreateCommandParameters();
            var artifactPath = UnityCloudPostBuild.OutputFiles.FirstOrDefault();
            if (string.IsNullOrEmpty(artifactPath) == false)
            {
                parameters.BuildParameters.ArtifactPath = artifactPath;
            }
            
            
            var builder    = new UnityPlayerBuilder();

            var guid          = "%BUILDMAP-GUID%";
            var assetPath     = AssetDatabase.GUIDToAssetPath(guid);
            var configuration = AssetDatabase.LoadAssetAtPath<UniBuildCommandsMap>(assetPath);
            builder.ExecuteCommands(configuration.PostBuildCommands,x => x.Execute(parameters));
        }

        private static IUniBuilderConfiguration CreateCommandParameters()
        {
            var argumentsProvider = new ArgumentsProvider(Environment.GetCommandLineArgs());
            
            Debug.LogFormat("\n[CloudBuildHelper] {0} \n", argumentsProvider);
            Debug.Log(args.ToString());

            var buildTarget      = argumentsProvider.GetBuildTarget();
            var buildTargetGroup = argumentsProvider.GetBuildTargetGroup();

            var buildParameters = new BuildParameters(buildTarget, buildTargetGroup, argumentsProvider) {
                buildNumber     = args.BuildNumber,
                buildTarget     = buildTarget,
                projectId       = args.ProjectId,
                bundleId        = args.BundleId,
                environmentType = BuildEnvironmentType.UnityCloudBuild,
                branch          = args.ScmBranch,
            };
            
            var manifest = LoadBuildManifest();
            if (manifest != null)
            {
                if (manifest.TryGetValue("buildNumber", out var buildNumberValue) &&
                    Int32.TryParse(buildNumberValue.ToString(), out var buildNumber))
                {
                    buildParameters.buildNumber = buildNumber;
                }
            }

            var result = new EditorBuildConfiguration(argumentsProvider, buildParameters);
            return result;
        }

        private const string ManifestResource = "UnityCloudBuildManifest.json";

        public static Dictionary<string,object> LoadBuildManifest()
        {
            var manifest = (TextAsset) Resources.Load(ManifestResource);
            if (manifest != null)
            {
                var manifestText = manifest.text;
                
                Debug.Log($"{nameof(LoadBuildManifest)}: MANIFEST \n {manifestText}");
                
                var manifestDict = JsonConvert.DeserializeObject<Dictionary<string,object>>(manifestText) as Dictionary<string,object>;
                foreach (var kvp in manifestDict)
                {
                    // Be sure to check for null values!
                    var value = (kvp.Value != null) ? kvp.Value.ToString() : "";
                    Debug.Log(string.Format("Key: {0}, Value: {1}", kvp.Key, value));
                }

                return manifestDict;
            }
            else
            {
                Debug.Log($"{nameof(LoadBuildManifest)}: MANIFEST NOT FOUND");
            }

            return null;
        }
    }

    public class UnityCloudPostBuild : IPostprocessBuildWithReport
    {
        public static List<string> OutputFiles = new List<string>();    
        
    
        public int callbackOrder { get; } = 0;

        public void OnPostprocessBuild(BuildReport report)
        {
            Debug.Log($"===== UNIBUILD{nameof(UnityCloudPostBuild)} : {report}");
            
            var files = report.files.Select(x => x.path).ToList();
            OutputFiles.AddRange(files);
        }
    }
}