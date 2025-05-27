namespace UniModules.UniGame.UniBuild
{
    using System.Collections.Generic;
    using Editor.ClientBuild;
    using Editor.ClientBuild.Commands.PreBuildCommands;
    using Editor.ClientBuild.Interfaces;
    using global::UniGame.UniBuild.Editor.ClientBuild.Interfaces;
    using UnityEngine;
    
#if ODIN_INSPECTOR
     using Sirenix.OdinInspector;
#endif

#if TRI_INSPECTOR
    using TriInspector;
#endif
    
    [CreateAssetMenu(menuName = "UniBuild/ScriptableCommandsGroup",fileName = nameof(ScriptableCommandsGroup))]
    public class ScriptableCommandsGroup : UnityBuildCommand, IUnityPreBuildCommand,IUnityPostBuildCommand
    {
        /// <summary>
        /// you can set build arguments with inspector
        /// </summary>
#if  ODIN_INSPECTOR || TRI_INSPECTOR
        [InlineProperty]
        [HideLabel]
        [Title(nameof(arguments))]
#endif
#if  ODIN_INSPECTOR
        [FoldoutGroup(nameof(arguments),expanded:false)]
#endif
        [Space]
        public ApplyBuildArgumentsCommand arguments = new ApplyBuildArgumentsCommand();
        
#if  ODIN_INSPECTOR || TRI_INSPECTOR
        [InlineProperty]
        [HideLabel]
#endif
        [Space]
        public BuildCommands commands = new BuildCommands();

#if  ODIN_INSPECTOR || TRI_INSPECTOR
        [Button]
#endif
        public void ExecuteGroup()
        {
            commands.Commands.ExecuteCommands();
        }
        
        public IEnumerable<TValue> GetCommands<TValue>() where TValue : IUnityBuildCommand
        {
            foreach (var command in commands.Commands)
            {
                if (command is TValue value)
                    yield return value;
            }
        }

        public override void Execute(IUniBuilderConfiguration configuration)
        {
            var groupName = Name;
            
            arguments.Execute(configuration);
            
            var message = $"Execute Group {groupName}";
            var id = BuildLogger.LogWithTimeTrack(message);
            ExecuteCommands(configuration);
            BuildLogger.Log($"Execute Group {groupName} FINISHED",id);
        }

        private void ExecuteCommands(IUniBuilderConfiguration configuration)
        {
            foreach (var buildCommand in commands.Commands)
            {
                if(!buildCommand.IsActive) continue;
                
                var id = buildCommand.Name;
                
                var message = $"\tExecute COMMAND {id}";
                
                var logId = BuildLogger.LogWithTimeTrack(message);
                buildCommand.Execute(configuration);
                
                BuildLogger.Log($"\tExecute COMMAND {buildCommand.Name} FINISHED",logId);
            }
        }
    }


}