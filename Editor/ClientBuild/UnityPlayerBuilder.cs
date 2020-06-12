namespace UniGreenModules.UniGame.UnityBuild.Editor.ClientBuild
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Commands.PostBuildCommands;
    using Commands.PreBuildCommands;
    using global::UniCore.Runtime.ProfilerTools;
    using Interfaces;
    using UniCore.EditorTools.Editor.AssetOperations;
    using UniModules.UniGame.UnityBuild.Editor.ClientBuild.BuildConfiguration;
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
            ExecuteCommands<UnityPreBuildCommand>(configuration,commandsMap,x => x.Execute(configuration));

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
            where  TTarget : Object,IUnityBuildCommand
        {
            LogBuildStep($"ExecuteCommands: {nameof(ExecuteCommands)} : \n {configuration}");

            var assetResources = commandsMap.
                LoadCommands<TTarget>(x => ValidateCommand(configuration,x));

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
            List<IEditorAssetResource> targetCommands, 
            Action<TTarget> action)
            where TTarget : Object, IUnityBuildCommand
        {
            var executingCommands = targetCommands;

            foreach (var command in executingCommands) {

                var commandAsset = command.Load<TTarget>();
                if (commandAsset == null || !commandAsset.IsActive) 
                {
                    LogBuildStep($"SKIP COMMAND {command}");
                    continue;
                }
        
                var commandName    = commandAsset.name;
                
                LogBuildStep($"EXECUTE COMMAND {commandName}");
                
                var startTime = DateTime.Now;
        
                action?.Invoke(commandAsset);

                var endTime       = DateTime.Now;
                var executionTime = endTime - startTime;
                
                LogBuildStep($"EXECUTE COMMAND FINISHED {commandName} DURATION: {executionTime.TotalSeconds}");
            }
        }

        public void LogBuildStep(string message)
        {
            GameLog.LogRuntime($"UNIBUILD : {message}\n");
        }
        
    }
}
