using System.Linq;
using UnityEngine.Serialization;

namespace UniModules.UniGame.UniBuild.Editor.ClientBuild.BuildConfiguration
{
    using System;
    using System.Collections.Generic;
    using Abstract;
    using Interfaces;
    using UniModules.UniCore.Runtime.ObjectPool.Runtime;
    using UniModules.UniCore.Runtime.ObjectPool.Runtime.Extensions;
    using UnityEngine;

#if ODIN_INSPECTOR
    using Sirenix.OdinInspector;
#endif

    [CreateAssetMenu(menuName = "UniGame/UniBuild/UniBuildConfiguration", fileName = nameof(UniBuildCommandsMap))]
    public class UniBuildCommandsMap : ScriptableObject, IUniBuildCommandsMap
    {

        public bool playerBuildEnabled = true;
        
#if  ODIN_INSPECTOR
        [InlineProperty()]
        [HideLabel()]
#endif
        [FormerlySerializedAs("_buildData")]
        [SerializeField]
        private UniBuildConfigurationData buildData = new UniBuildConfigurationData();

#if ODIN_INSPECTOR
        [Searchable]
        [BoxGroup(nameof(PreBuildCommands),false)]
#endif
        [Space]
        public List<BuildCommandStep> preBuildCommands = new List<BuildCommandStep>();

#if ODIN_INSPECTOR
        [Searchable]
        [BoxGroup(nameof(PostBuildCommands),false)]
#endif
        [Space]
        public List<BuildCommandStep> postBuildCommands = new List<BuildCommandStep>();

        #region public properties

        public bool PlayerBuildEnabled => playerBuildEnabled;
        
        public IUniBuildConfigurationData BuildData => buildData;
        
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

            if (BuildData.BuildTarget != buildParameters.BuildTarget)
                return false;

            if (BuildData.BuildTargetGroup!=buildParameters.BuildTargetGroup)
                return false;
            
            return ValidatePlatform(config);
        }

#if ODIN_INSPECTOR
        [BoxGroup(nameof(PreBuildCommands))]
        [Button(nameof(ExecutePreBuildCommands))]
#endif
        public void ExecutePreBuildCommands() => PreBuildCommands.ExecuteCommands(buildData);
        
#if ODIN_INSPECTOR
        [BoxGroup(nameof(PostBuildCommands))]
        [Button(nameof(ExecutePostBuildCommands))]
#endif
        public void ExecutePostBuildCommands() => PostBuildCommands.ExecuteCommands(buildData);
        
#if  ODIN_INSPECTOR
        [Button("Execute")]
#endif
        public void ExecuteBuild()
        {
            UniBuildTool.ExecuteBuild(this);
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

    }
}
