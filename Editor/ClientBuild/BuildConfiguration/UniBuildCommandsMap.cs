using UnityEngine;

namespace UniModules.UniGame.UnityBuild.Editor.ClientBuild.BuildConfiguration
{
    using System;
    using System.Collections.Generic;
    using UniGreenModules.UniCore.EditorTools.Editor.AssetOperations;
    using UniGreenModules.UniCore.Runtime.ObjectPool.Runtime;
    using UniGreenModules.UniCore.Runtime.ObjectPool.Runtime.Extensions;
    using UniGreenModules.UniGame.UnityBuild.Editor.ClientBuild;
    using UniGreenModules.UniGame.UnityBuild.Editor.ClientBuild.Commands;
    using UniGreenModules.UniGame.UnityBuild.Editor.ClientBuild.Commands.PostBuildCommands;
    using UniGreenModules.UniGame.UnityBuild.Editor.ClientBuild.Commands.PreBuildCommands;
    using UniGreenModules.UniGame.UnityBuild.Editor.ClientBuild.Interfaces;

    [CreateAssetMenu(menuName = "UnityBuild/UniBuildConfiguration", fileName = nameof(UniBuildCommandsMap))]
    public class UniBuildCommandsMap : ScriptableObject, IUniBuildCommandsMap
    {

#if  ODIN_INSPECTOR
        [Sirenix.OdinInspector.InlineProperty()]
#endif
        [SerializeField]
        private UniBuildConfigurationData _buildData = new UniBuildConfigurationData();

        [Space]
#if  ODIN_INSPECTOR
        [Sirenix.OdinInspector.InlineEditor()]
#endif
        [SerializeField]
        private List<UnityPreBuildCommand> _preBuildCommands = new List<UnityPreBuildCommand>();
        
#if  ODIN_INSPECTOR
        [Sirenix.OdinInspector.InlineEditor()]
#endif
        [SerializeField]
        private List<UnityPostBuildCommand> _postBuildCommands = new List<UnityPostBuildCommand>();

        #region public properties

        public IUniBuildConfigurationData BuildData => _buildData;
        
        public IReadOnlyList<IUnityPreBuildCommand> PreBuildCommands => _preBuildCommands;

        public IReadOnlyList<IUnityPostBuildCommand> PostBuildCommands => _postBuildCommands;
        
        public string ItemName => name;
        
        #endregion

        public List<IEditorAssetResource> LoadCommands<T>(Func<T,bool> filter = null)
            where T : IUnityBuildCommand 
        {
            var result = new List<IEditorAssetResource>();
            
            var commandsBuffer = ClassPool.Spawn<List<UnityBuildCommand>>();
            commandsBuffer.AddRange(_preBuildCommands);
            commandsBuffer.AddRange(_postBuildCommands);
            
            foreach (var command in commandsBuffer) {
                if (command is T targetCommand) {
                    
                    if(filter!=null && !filter(targetCommand))
                        continue;
                    
                    result.Add(new EditorAssetResource().Initialize(command));
                }
            }

            commandsBuffer.Despawn();

            return result;
        }

        public bool Validate(IUniBuilderConfiguration config)
        {
            var buildParameters = config.BuildParameters;

            if (BuildData.BuildTarget != buildParameters.BuildTarget)
                return false;

            if (BuildData.BuildTargetGroup!=buildParameters.BuildTargetGroup)
                return false;

            var isUnderCloud = false;
            
#if UNITY_CLOUD_BUILD
            isUnderCloud = true;
#endif
            
            if (isUnderCloud != BuildData.CloudBuild)
                return false;
            
            return ValidatePlatform(config);
        }

#if  ODIN_INSPECTOR
        [Sirenix.OdinInspector.Button("Execute")]
#endif
        public void ExecuteBuild()
        {
            UniBuildTool.ExecuteBuild(BuildData.ArtifactName,BuildData.BuildTarget,BuildData.BuildTargetGroup);
        }
        
        protected virtual bool ValidatePlatform(IUniBuilderConfiguration config)
        {
            return true;
        }

    }
}
