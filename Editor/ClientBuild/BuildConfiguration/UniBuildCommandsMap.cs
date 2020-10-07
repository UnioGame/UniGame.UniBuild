namespace UniModules.UniGame.UniBuild.Editor.ClientBuild.BuildConfiguration
{
    using System;
    using System.Collections.Generic;
    using Abstract;
    using Interfaces;
    using UniModules.UniCore.Runtime.ObjectPool.Runtime;
    using UniModules.UniCore.Runtime.ObjectPool.Runtime.Extensions;
    using UnityEngine;


    [CreateAssetMenu(menuName = "UniGame/UniBuild/UniBuildConfiguration", fileName = nameof(UniBuildCommandsMap))]
    public class UniBuildCommandsMap : ScriptableObject, IUniBuildCommandsMap
    {
#if  ODIN_INSPECTOR
        [Sirenix.OdinInspector.InlineProperty()]
        [Sirenix.OdinInspector.HideLabel()]
#endif
        [SerializeField]
        private UniBuildConfigurationData _buildData = new UniBuildConfigurationData();

        [Space]
        public List<BuildCommandStep> preBuildCommands = new List<BuildCommandStep>();

        [Space]
        public List<BuildCommandStep> postBuildCommands = new List<BuildCommandStep>();

        
        #region public properties

        public IUniBuildConfigurationData BuildData => _buildData;
        
        public IEnumerable<IUnityBuildCommand> PreBuildCommands => FilterActiveCommands(preBuildCommands);

        public IEnumerable<IUnityBuildCommand> PostBuildCommands =>  FilterActiveCommands(postBuildCommands);
        
        public string ItemName => name;
        
        #endregion

        public IEnumerable<T> LoadCommands<T>(Func<T,bool> filter = null)
            where T : IUnityBuildCommand 
        {
            var commandsBuffer = ClassPool.Spawn<List<IUnityBuildCommand>>();
            commandsBuffer.AddRange(PreBuildCommands);
            commandsBuffer.AddRange(PostBuildCommands);

            foreach (var command in commandsBuffer)
            {
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

#if  ODIN_INSPECTOR
        [Sirenix.OdinInspector.Button("Execute")]
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
