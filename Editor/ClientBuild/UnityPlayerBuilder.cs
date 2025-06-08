namespace UniGame.UniBuild.Editor
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Abstract;
    using global::UniGame.UniBuild.Editor;
    using UniModules.Editor;
    using Extensions;
    using global::UniGame.UniBuild.Editor.ClientBuild.Interfaces;
    using Interfaces;
    using UniModules;
    using UnityEditor;
    using UnityEditor.Build.Profile;
    using UnityEditor.Build.Reporting;

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

            //apply build settings
            var buildParameters = configuration.BuildParameters;
            buildParameters.Execute();
            
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

            var buildReportData = new BuildReportData()
            {
                report = report,
                writeLog = true,
                writeToFile = true
            };
            
            PrintBuilderReport(buildReportData);
            PrintBuildLog();
            
            return report;
        }
        
        public void PrintBuilderReport(BuildReportData reportData)
        {
            var reportBuilder = new UniBuildReportBuilder();
            reportBuilder.ApplyReport(reportData);
        }

        public void ExecuteCommands(IEnumerable<IUnityBuildCommand> commands, IUniBuilderConfiguration configuration)
        {
            ExecuteCommands(commands,x => x.Execute(configuration));
        }

        public string GetTargetBuildLocation(BuildParameters buildParameters)
        {
            var file   = buildParameters.outputFile;
            var folder = buildParameters.outputFolder;
            var artifactPath = folder.
//                CombinePath(buildParameters.BuildTarget.ToString()).
                CombinePath(file);
            
            buildParameters.artifactPath = artifactPath;
            
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
            var buildOptions   = buildParameters.buildOptions;
    
            BuildLogger.Log($"OUTPUT LOCATION : {outputLocation}");

            var buildConfig = new BuildPlayerOptions
            {
                locationPathName = outputLocation,
                target = buildParameters.buildTarget,
                #if UNITY_STANDALONE || UNITY_SERVER
                subtarget = (int)buildParameters.standaloneBuildSubtarget,
                #endif
                scenes = scenes.Select(x => x.path).ToArray(),
                options = buildOptions,
                targetGroup = buildParameters.buildTargetGroup,
            };
            
            var report = BuildPipeline.BuildPlayer(buildConfig);
            BuildLogger.Log(report.ReportMessage());
            return report;

            return default;
        }

        private void PrintBuildLog()
        {
            BuildLogger.Print();
            BuildLogger.Finish();
        }
        
        private EditorBuildSettingsScene[] GetBuildInScenes(IUniBuilderConfiguration configuration)
        {
            var parameters = configuration.BuildParameters;
            var scenes = parameters.scenes.Count > 0 ? parameters.scenes.ToArray() :
                EditorBuildSettings.scenes;
            return scenes.Where(x => x.enabled).ToArray();
        }

        
    }
}
