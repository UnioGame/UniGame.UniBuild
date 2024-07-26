using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UniModules.UniGame.UniBuild.Editor.ClientBuild
{
    using System;
    using Abstract;
    using BuildConfiguration;
    using global::UniGame.UniBuild.Editor.ClientBuild;
    using global::UniGame.UniBuild.Editor.ClientBuild.BuildConfiguration;
    using global::UniGame.UniBuild.Editor.ClientBuild.Interfaces;
    using Interfaces;
    using UnityEditor;
    using UnityEditor.Build.Reporting;
    using Object = UnityEngine.Object;

    public static class UniBuildTool
    {

        public const string BuildFolder = "Build";

        private static UnityPlayerBuilder builder = new UnityPlayerBuilder();
    
        public static EditorBuildConfiguration CreateConfiguration(UniBuildConfigurationData buildData)
        {
            var commandLineParameters = Environment.GetCommandLineArgs().ToList();
            commandLineParameters.Add( $"{BuildArguments.BuildOutputFolderKey}:Builds");
            commandLineParameters.Add( $"{BuildArguments.BuildOutputNameKey}:{buildData.artifactName}");
            
            var argumentsProvider = new ArgumentsProvider(commandLineParameters.ToArray());
            var buildParameters = new BuildParameters(buildData, argumentsProvider);
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
        
        public static void BuildAndRunByConfigurationId(string guid)
        {
            var assetPath = AssetDatabase.GUIDToAssetPath(guid);
            var asset     = AssetDatabase.LoadAssetAtPath<UniBuildCommandsMap>(assetPath);
            var instance = Object.Instantiate(asset);
            instance.BuildData.buildOptions |= BuildOptions.AutoRunPlayer;
            UniBuildTool.ExecuteBuild(instance);
        }
        
        public static BuildReport ExecuteBuild(IUniBuildCommandsMap commandsMap)
        {
            var buildData     = commandsMap.BuildData;
            var configuration = CreateConfiguration(buildData);
            return BuildPlayer(configuration,commandsMap);
        }

        public static void ExecuteCommands(this IEnumerable<IUnityBuildCommand> commands, UniBuildConfigurationData buildData = null)
        {
            buildData ??= new UniBuildConfigurationData()
            {
                buildTarget = EditorUserBuildSettings.activeBuildTarget,
                buildTargetGroup = EditorUserBuildSettings.selectedBuildTargetGroup,
                artifactName = "Empty"
            }; 
            var configuration = CreateConfiguration(buildData);
            builder.ExecuteCommands(commands,configuration);
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
