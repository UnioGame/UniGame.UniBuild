namespace UniModules.UniGame.UniBuild.Editor.ClientBuild.BuildConfiguration
{
    using System;
    using System.Collections.Generic;
    using Abstract;
    using Interfaces;
    using UniGreenModules.UniCore.Runtime.ObjectPool.Runtime;
    using UniGreenModules.UniCore.Runtime.ObjectPool.Runtime.Extensions;
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
        public List<PreBuildCommandStep> preBuildCommands = new List<PreBuildCommandStep>();

        [Space]
        public List<PostBuildCommandStep> postBuildCommands = new List<PostBuildCommandStep>();

        
        #region public properties

        public IUniBuildConfigurationData BuildData => _buildData;
        
        public IEnumerable<IUnityPreBuildCommand> PreBuildCommands => LoadCommands<IUnityPreBuildCommand>();

        public IEnumerable<IUnityPostBuildCommand> PostBuildCommands =>  LoadCommands<IUnityPostBuildCommand>();
        
        public string ItemName => name;
        
        #endregion

        public IEnumerable<T> LoadCommands<T>(Func<T,bool> filter = null)
            where T : IUnityBuildCommand 
        {
            var commandsBuffer = ClassPool.Spawn<List<IUnityBuildCommand>>();

            foreach (var command in preBuildCommands) {
                commandsBuffer.AddRange(command.GetCommands());
            }
            foreach (var command in postBuildCommands) {
                commandsBuffer.AddRange(command.GetCommands());
            }

            foreach (var command in commandsBuffer) {
                if (command is T targetCommand) {
                    if(filter!=null && !filter(targetCommand))
                        continue;

                    yield return targetCommand;
                }
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
        
        protected virtual bool ValidatePlatform(IUniBuilderConfiguration config)
        {
            return true;
        }


    }
}
