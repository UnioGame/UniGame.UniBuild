using System.Linq;
using UnityEngine;

namespace UniModules.UniGame.UniBuild.Editor.ClientBuild
{
    using System;
    using Abstract;
    using BuildConfiguration;
    using Interfaces;
    using UnityEditor;
    using UnityEditor.Build.Reporting;

    public static class UniBuildTool
    {

        public const string BuildFolder = "Build";

        private static UnityPlayerBuilder builder = new UnityPlayerBuilder();
    
        public static EditorBuildConfiguration CreateConfiguration(string outputFileName,BuildTarget buildTarget,BuildTargetGroup targetGroup)
        {
            var commandLineParameters = Environment.GetCommandLineArgs().ToList();
            commandLineParameters.Add( $"{BuildArguments.BuildOutputFolderKey}:Builds");
            commandLineParameters.Add( $"{BuildArguments.BuildOutputNameKey}:{outputFileName}");
            
            var argumentsProvider = new ArgumentsProvider(commandLineParameters.ToArray());
            var buildParameters = new BuildParameters(buildTarget, targetGroup, argumentsProvider);
            var buildConfiguration = new EditorBuildConfiguration(argumentsProvider, buildParameters);
            
            Debug.LogFormat("\nUNIBUILD [CreateConfiguration] {0} \n", argumentsProvider);
            
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
            return BuildPlayer(configuration,commandsMap);
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
