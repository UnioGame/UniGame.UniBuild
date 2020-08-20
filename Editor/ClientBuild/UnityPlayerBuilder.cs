namespace UniModules.UniGame.UniBuild.Editor.ClientBuild
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Abstract;
    using BuildConfiguration;
    using Commands.PostBuildCommands;
    using Commands.PreBuildCommands;
    using Core.EditorTools.Editor.AssetOperations;
    using Extensions;
    using Interfaces;
    using UniCore.Runtime.ProfilerTools;
    using UnityEditor;
    using UnityEditor.Build.Reporting;
    using Object = UnityEngine.Object;

    public class UnityPlayerBuilder : IUnityPlayerBuilder
    {
        private const string BuildFolder = "Build";

        public BuildReport Build(IUniBuilderConfiguration configuration)
        {
            var commandMap = SelectActualBuildMap(configuration);
            
            return Build(configuration,commandMap);
        }
        
        public BuildReport Build(IUniBuilderConfiguration configuration,IUniBuildCommandsMap commandsMap)
        {
            ExecuteCommands<IUnityPreBuildCommand>(configuration,commandsMap,x => x.Execute(configuration));

            var result = ExecuteBuild(configuration);
    
            ExecuteCommands<UnityPostBuildCommand>(configuration,commandsMap,x => x.Execute(configuration,result));

            return result;
        }

        private BuildReport ExecuteBuild(IUniBuilderConfiguration configuration)
        {
            var scenes = GetBuildInScenes(configuration);

            var buildParameters = configuration.BuildParameters;
            var outputLocation = GetTargetBuildLocation(configuration.BuildParameters);
            var buildOptions   = buildParameters.BuildOptions;
    
            LogBuildStep($"OUTPUT LOCATION : {outputLocation}");

            var report = BuildPipeline.BuildPlayer(scenes, outputLocation,
                buildParameters.BuildTarget, buildOptions);

            LogBuildStep(report.ReportMessage());

            return report;

        }

        private EditorBuildSettingsScene[] GetBuildInScenes(IUniBuilderConfiguration configuration)
        {
            var parameters = configuration.BuildParameters;
            var scenes = parameters.Scenes.Count > 0 ? parameters.Scenes :
                EditorBuildSettings.scenes;
            return scenes.Where(x => x.enabled).ToArray();
        }

        private string GetTargetBuildLocation(IBuildParameters buildParameters)
        {
            var file   = buildParameters.OutputFile;
            var folder = buildParameters.OutputFolder;
            return $"{folder}/{buildParameters.BuildTarget.ToString()}/{file}";
        }

        public void ExecuteCommands<TTarget>(
            IUniBuilderConfiguration configuration,
            Action<TTarget> action) 
            where  TTarget : Object,IUnityBuildCommand
        {
            var commandMap = SelectActualBuildMap(configuration);

            ExecuteCommands(configuration, commandMap, action);
        }
        
        public void ExecuteCommands<TTarget>(
            IUniBuilderConfiguration configuration,
            IUniBuildCommandsMap commandsMap,
            Action<TTarget> action) 
            where  TTarget : IUnityBuildCommand
        {
            LogBuildStep($"ExecuteCommands: {nameof(ExecuteCommands)} : \n {configuration}");

            var assetResources = commandsMap.
                LoadCommands<TTarget>(x => ValidateCommand(configuration,x)).
                ToList();

            ExecuteCommands(assetResources, action);
        }
        

        public IUniBuildCommandsMap SelectActualBuildMap(IUniBuilderConfiguration configuration)
        {
            //load build command maps
            var commandsMapsResources = AssetEditorTools.
                GetEditorResources<UniBuildCommandsMap>();
            
            //filter all valid commands map
            foreach (var mapResource in commandsMapsResources) {

                var commandMap = mapResource.Load<IUniBuildCommandsMap>();
                if(!commandMap.Validate(configuration) ) 
                    continue;
                
                LogBuildStep($"SELECT BUILD MAP {commandMap.ItemName}");
                return commandMap;
            }

            return null;
        }

        public bool ValidateCommand( IUniBuilderConfiguration configuration,IUnityBuildCommand command)
        {
            var asset = command;
            var isValid = asset != null && asset.IsActive;
            if (asset is IUnityBuildCommandValidator validator) {
                isValid = isValid && validator.Validate(configuration);
            }
            return isValid;
        }

        public void ExecuteCommands<TTarget>(
            IEnumerable<TTarget> commands, 
            Action<TTarget> action)
            where TTarget : IUnityBuildCommand
        {
            var executingCommands = commands;
            var stepCounter       = 1;
            foreach (var command in executingCommands) {

                if (command == null || !command.IsActive) 
                {
                    LogBuildStep($"SKIP COMMAND {command}");
                    continue;
                }
        
                var commandName    = command.Name;
                
                LogBuildStep($"EXECUTE COMMAND {commandName}");
                
                var startTime = DateTime.Now;
        
                action?.Invoke(command);

                var endTime       = DateTime.Now;
                var executionTime = endTime - startTime;
                
                LogBuildStep($"EXECUTE COMMAND [{stepCounter++}] FINISHED {commandName} DURATION: {executionTime.TotalSeconds}");
            }
            
            LogBuildStep($"COMMANDS FINISHED");
        }

        public void LogBuildStep(string message)
        {
            GameLog.LogRuntime($"UNIBUILD : {message}\n");
        }
        
    }
}
