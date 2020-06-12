using System;
using UnityEditor;
using UnityEditor.Build.Reporting;

namespace UniGreenModules.UniGame.UnityBuild.Editor.ClientBuild
{
    using BuildConfiguration;
    using Interfaces;
    using UniModules.UniGame.UnityBuild.Editor.ClientBuild.BuildConfiguration;

    public static class UniBuildTool
    {

        public const string BuildFolder = "Build";

        private static UnityPlayerBuilder builder = new UnityPlayerBuilder();
    
        
        public static EditorBuildConfiguration CreateConfiguration(string outputFileName,BuildTarget buildTarget,BuildTargetGroup targetGroup)
        {
            var argumentsProvider = new ArgumentsProvider(new[] {
                $"{BuildArguments.BuildOutputFolderKey}:Builds",
                $"{BuildArguments.BuildOutputNameKey}:{outputFileName}",
            });

            var buildConfiguration = new EditorBuildConfiguration(
                argumentsProvider, 
                new BuildParameters(buildTarget, targetGroup, argumentsProvider));
            return buildConfiguration;
        }
        
                
        public static void BuildByConfigurationId(string guid)
        {
            var assetPath = AssetDatabase.GUIDToAssetPath(guid);
            var asset     = AssetDatabase.LoadAssetAtPath<UniBuildCommandsMap>(assetPath);
            UniBuildTool.ExecuteBuild(asset);
        }

        public static BuildReport ExecuteBuild(string outputFileName,BuildTarget buildTarget,BuildTargetGroup targetGroup)
        {
            var buildConfiguration = CreateConfiguration(outputFileName, buildTarget, targetGroup);
            return UniBuildTool.BuildPlayer(buildConfiguration);
        }
        
        public static BuildReport ExecuteBuild(IUniBuildCommandsMap commandsMap)
        {
            var buildData     = commandsMap.BuildData;
            var configuration = CreateConfiguration(buildData.ArtifactName, buildData.BuildTarget, buildData.BuildTargetGroup);
            return UniBuildTool.BuildPlayer(configuration,commandsMap);
        }
        
        /// <summary>
        /// Console build call. Close editor after end of build process
        /// </summary>
        public static void BuildUnityPlayer()
        {
            var configuration = new UniBuilderConsoleConfiguration(Environment.GetCommandLineArgs());
        
            var report = BuildPlayer(configuration);

            EditorApplication.Exit(report.summary.result == BuildResult.Succeeded ? 0 : 1);
        }

        public static BuildReport BuildPlayer(IUniBuilderConfiguration configuration)
        {
            var report = builder.Build(configuration);
            return report;
        }

        public static BuildReport BuildPlayer(IUniBuilderConfiguration configuration, IUniBuildCommandsMap commandsMap)
        {
            var report = builder.Build(configuration,commandsMap);
            return report;
        }
        
    }
}
