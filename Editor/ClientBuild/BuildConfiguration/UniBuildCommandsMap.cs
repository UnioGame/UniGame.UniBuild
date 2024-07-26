using System.Linq;
using UnityEngine.Serialization;

namespace UniModules.UniGame.UniBuild.Editor.ClientBuild.BuildConfiguration
{
    using System;
    using System.Collections.Generic;
    using Abstract;
    using Interfaces;
    using global::UniGame.Runtime.ObjectPool;
    using global::UniGame.Runtime.ObjectPool.Extensions;
    using global::UniGame.UniBuild.Editor.ClientBuild.BuildConfiguration;
    using global::UniGame.UniBuild.Editor.ClientBuild.Interfaces;
    using UnityEditor;
    using UnityEngine;

#if ODIN_INSPECTOR
     using Sirenix.OdinInspector;
#endif

#if TRI_INSPECTOR
    using TriInspector;
#endif

    [CreateAssetMenu(menuName = "UniBuild/UniBuildConfiguration",fileName = "UniGame Builder")]
    public class UniBuildCommandsMap : ScriptableObject, IUniBuildCommandsMap
    {
        public const string SettingsTabKey = "settings";
        public const string CommandsTabKey = "commands";
        
        private static Color _oddColor = new Color(0.2f, 0.4f, 0.3f);
        
#if  ODIN_INSPECTOR
        [TabGroup(SettingsTabKey)]
#endif
        public bool playerBuildEnabled = true;
        
#if  ODIN_INSPECTOR || TRI_INSPECTOR
        [InlineProperty()]
        [HideLabel()]
#endif
#if  ODIN_INSPECTOR
        [TabGroup(SettingsTabKey)]
#endif
        [FormerlySerializedAs("_buildData")]
        [SerializeField]
        private UniBuildConfigurationData buildData = new UniBuildConfigurationData();
        
#if  ODIN_INSPECTOR || TRI_INSPECTOR
        [Space]
#endif
#if ODIN_INSPECTOR
        [Searchable]
        [TabGroup(CommandsTabKey)]
        [ListDrawerSettings(AddCopiesLastElement = false,ElementColor = nameof(GetElementColor))]
#endif
        [Space]
        public List<BuildCommandStep> preBuildCommands = new List<BuildCommandStep>();

#if  ODIN_INSPECTOR || TRI_INSPECTOR
        [Space(6f)]
#endif
#if ODIN_INSPECTOR
        [Searchable]
        [TabGroup(CommandsTabKey)]
        [ListDrawerSettings(AddCopiesLastElement = false, ElementColor = nameof(GetElementColor))]
#endif
        [Space]
        public List<BuildCommandStep> postBuildCommands = new List<BuildCommandStep>();

        #region public properties

        public bool PlayerBuildEnabled => playerBuildEnabled;
        
        public UniBuildConfigurationData BuildData => buildData;
        
        public IEnumerable<IUnityBuildCommand> PreBuildCommands => FilterActiveCommands(preBuildCommands);

        public IEnumerable<IUnityBuildCommand> PostBuildCommands =>  FilterActiveCommands(postBuildCommands);
        
        public string ItemName => name;
        
        #endregion

        public IEnumerable<BuildCommandStep> Filter(string filter)
        {
            return Filter(filter, preBuildCommands)
                .Concat(Filter(filter, postBuildCommands));
        }

        public IEnumerable<BuildCommandStep> Filter(string filter, IEnumerable<BuildCommandStep> commandSteps)
        {
            return commandSteps.Where(x => ValidateCommandFilter(x, filter));
        }
        
        public bool ValidateCommandFilter(BuildCommandStep commandStep, string filterValue)
        {
            if (string.IsNullOrEmpty(filterValue))
                return true;
            
            var isValid = false;
            var commands = commandStep.GetCommands();
            foreach (var buildCommand in commands)
            {
                isValid |= buildCommand.Name.IndexOf(filterValue,StringComparison.OrdinalIgnoreCase) >= 0;
                isValid |= buildCommand
                    .GetType()
                    .Name
                    .IndexOf(filterValue,StringComparison.OrdinalIgnoreCase) >= 0;
            }

            return isValid;
        }

        public IEnumerable<T> LoadCommands<T>(Func<T,bool> filter = null)
            where T : IUnityBuildCommand 
        {
            var commandsBuffer = ClassPool.Spawn<List<IUnityBuildCommand>>();
            commandsBuffer.AddRange(PreBuildCommands);
            commandsBuffer.AddRange(PostBuildCommands);

            foreach (var command in commandsBuffer)
            {
                if(command.IsActive == false)
                    continue;
                
                if (!(command is T targetCommand)) continue;
                
                if(filter!=null && !filter(targetCommand))
                    continue;

                yield return targetCommand;
            }

            commandsBuffer.Despawn();
        }
        

        public bool Validate(IUniBuilderConfiguration config)
        {
            var buildParameters = config.BuildParameters;

            if (BuildData.buildTarget != buildParameters.buildTarget)
                return false;

            if (BuildData.buildTargetGroup!=buildParameters.buildTargetGroup)
                return false;
            
            return ValidatePlatform(config);
        }

#if ODIN_INSPECTOR
        [TabGroup(CommandsTabKey)]
#endif
#if  ODIN_INSPECTOR || TRI_INSPECTOR
        [GUIColor(0.5f, 0.4f, 0.1f)]
        [Button(nameof(ExecutePreBuildCommands))]
#endif
        public void ExecutePreBuildCommands() => PreBuildCommands.ExecuteCommands(buildData);
      
#if ODIN_INSPECTOR
        [TabGroup(CommandsTabKey)]
#endif
#if  ODIN_INSPECTOR || TRI_INSPECTOR
        [GUIColor(0.5f, 0.4f, 0.1f)]
        [Button(nameof(ExecutePostBuildCommands))]
#endif
        public void ExecutePostBuildCommands() => PostBuildCommands.ExecuteCommands(buildData);
        
#if  ODIN_INSPECTOR || TRI_INSPECTOR
        [GUIColor(0.2f, 0.8f, 0.1f)]
#endif
#if TRI_INSPECTOR
        [Button(ButtonSizes.Large)]
#endif
#if ODIN_INSPECTOR
        [PropertyOrder(-1)]
        [Button("Build",ButtonSizes.Large)]
        [TabGroup(CommandsTabKey)]
#endif
        public void ExecuteBuild()
        {
            UniBuildTool.ExecuteBuild(this);
        }
        
#if  ODIN_INSPECTOR || TRI_INSPECTOR
        [GUIColor(0.2f, 0.8f, 0.1f)]
#endif
#if TRI_INSPECTOR
        [Button(ButtonSizes.Large)]
#endif
#if ODIN_INSPECTOR
        [PropertyOrder(-1)]
        [Button("Build And Run",ButtonSizes.Large)]
        [TabGroup(CommandsTabKey)]
#endif
        public void ExecuteAndRunBuild()
        {
            var commandsMap = Instantiate(this);
            commandsMap.buildData.buildOptions |= BuildOptions.AutoRunPlayer;
            UniBuildTool.ExecuteBuild(commandsMap);
        }

        private IEnumerable<IUnityBuildCommand> FilterActiveCommands(IEnumerable<BuildCommandStep> commands)
        {
            var commandsBuffer = ClassPool.Spawn<List<IUnityBuildCommand>>();

            foreach (var command in commands) {
                commandsBuffer.AddRange(command.GetCommands());
            }

            return commandsBuffer;
        }
        
        protected virtual bool ValidatePlatform(IUniBuilderConfiguration config)
        {
            return true;
        }

        private Color GetElementColor(int index, Color defaultColor)
        {
            var result = index % 2 == 0 
                ? _oddColor : defaultColor;
            return result;
        }
    }
}
