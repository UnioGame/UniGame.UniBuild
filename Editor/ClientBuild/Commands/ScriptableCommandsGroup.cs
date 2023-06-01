namespace UniModules.UniGame.UniBuild
{
    using Editor.ClientBuild;
    using Editor.ClientBuild.Commands.PreBuildCommands;
    using Editor.ClientBuild.Interfaces;
    using UnityEngine;
    
    [CreateAssetMenu(menuName = "UniGame/UniBuild/ScriptableCommandsGroup",fileName = nameof(ScriptableCommandsGroup))]
    public class ScriptableCommandsGroup : UnityBuildCommand, IUnityPreBuildCommand,IUnityPostBuildCommand
    {
#if ODIN_INSPECTOR
        [Sirenix.OdinInspector.MultiLineProperty]
        [Sirenix.OdinInspector.PropertyOrder(0)]
#endif 
        public string description;
        
        /// <summary>
        /// you can set build arguments with inspector
        /// </summary>
#if ODIN_INSPECTOR
        [Sirenix.OdinInspector.InlineProperty]
        [Sirenix.OdinInspector.HideLabel]
        [Sirenix.OdinInspector.BoxGroup()]
        [Sirenix.OdinInspector.Title(nameof(arguments))]
#endif
        [Space]
        public ApplyBuildArgumentsCommand arguments = new ApplyBuildArgumentsCommand();
        
#if ODIN_INSPECTOR
        [Sirenix.OdinInspector.InlineProperty]
        [Sirenix.OdinInspector.HideLabel]
#endif
        [Space]
        public BuildCommands commands = new BuildCommands();

#if ODIN_INSPECTOR
        [Sirenix.OdinInspector.Button]
#endif
        public void ExecuteGroup()
        {
            commands.Commands.ExecuteCommands();
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
                var id = buildCommand.Name;
                
                var message = $"\tExecute COMMAND {id}";
                
                var logId = BuildLogger.LogWithTimeTrack(message);

                buildCommand.Execute(configuration);
                
                BuildLogger.Log($"\tExecute COMMAND {buildCommand.Name} FINISHED",logId);
            }
        }
    }


}