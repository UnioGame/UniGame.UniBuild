namespace UniModules.UniGame.UniBuild.Editor.ClientBuild
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Abstract;
    using BuildConfiguration;
    using UniModules.Editor;
    using Extensions;
    using Interfaces;
    using UnityEditor;
    using UnityEditor.Build.Reporting;
    using UnityEngine;

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
            BuildLogger.Initialize();
            
            var id = BuildLogger.LogWithTimeTrack($"Build Start At {DateTime.Now.ToLongDateString()}");
            
            ExecuteCommands(commandsMap.PreBuildCommands,configuration);

            BuildReport report = null;
            
            if (commandsMap.PlayerBuildEnabled)
            {
                report = ExecuteBuild(configuration);
                configuration.BuildReport = report;
            }
    
            ExecuteCommands(commandsMap.PostBuildCommands,configuration);

            BuildLogger.Log($"Build Finish At At {DateTime.Now.ToLongDateString()}",id,false);
            
            PrintBuildLog();
            
            return report;
        }

        public void ExecuteCommands(IEnumerable<IUnityBuildCommand> commands, IUniBuilderConfiguration configuration)
        {
            ExecuteCommands(commands,x => x.Execute(configuration));
        }

        public string GetTargetBuildLocation(IBuildParameters buildParameters)
        {
            var file   = buildParameters.OutputFile;
            var folder = buildParameters.OutputFolder;
            var artifactPath = folder.
//                CombinePath(buildParameters.BuildTarget.ToString()).
                CombinePath(file);
            
            buildParameters.ArtifactPath = artifactPath;
            
            return artifactPath;
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
                
                BuildLogger.Log($"SELECT BUILD MAP {commandMap.ItemName}");
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
            BuildLogger.Log($"ExecuteCommands Of Type {typeof(TTarget).Name}");

            var executingCommands = commands.ToList();
            var commandsNames     = executingCommands.Select(x => x.Name);
            var commandsNamesTest = string.Join("\n\t", commandsNames);
            
            BuildLogger.Log(commandsNamesTest);
            
            var stepCounter       = 1;
            foreach (var command in executingCommands) {

                if (command == null || !command.IsActive) 
                {
                    BuildLogger.Log($"SKIP COMMAND {command}");
                    continue;
                }
        
                var commandName    = command.Name;
                
                var id = BuildLogger.LogWithTimeTrack($"EXECUTE COMMAND {commandName}");
                
                action?.Invoke(command);
                
                BuildLogger.Log($"EXECUTE COMMAND [{commandName} [{stepCounter++}]] FINISHED",id);
            }
            
            BuildLogger.Log("COMMANDS FINISHED");
        }

        private BuildReport ExecuteBuild(IUniBuilderConfiguration configuration)
        {
            var scenes = GetBuildInScenes(configuration);

            var buildParameters = configuration.BuildParameters;
            var outputLocation = GetTargetBuildLocation(configuration.BuildParameters);
            var buildOptions   = buildParameters.BuildOptions;
    
            BuildLogger.Log($"OUTPUT LOCATION : {outputLocation}");

            var report = BuildPipeline.BuildPlayer(scenes, outputLocation,
                buildParameters.BuildTarget, buildOptions);

            BuildLogger.Log(report.ReportMessage());

            return report;

        }

        private void PrintBuildLog()
        {
            BuildLogger.Print();
            BuildLogger.Finish();
        }
        
        private EditorBuildSettingsScene[] GetBuildInScenes(IUniBuilderConfiguration configuration)
        {
            var parameters = configuration.BuildParameters;
            var scenes = parameters.Scenes.Count > 0 ? parameters.Scenes :
                EditorBuildSettings.scenes;
            return scenes.Where(x => x.enabled).ToArray();
        }

        
    }
}
