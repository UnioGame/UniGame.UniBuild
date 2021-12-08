using System;
using UniModules.UniGame.UniBuild.Editor.ClientBuild;
using UniModules.UniGame.UniBuild.Editor.ClientBuild.Commands.PreBuildCommands;
using UniModules.UniGame.UniBuild.Editor.ClientBuild.Interfaces;
using UnityEngine;

namespace UniModules.UniGame.UniBuild
{
    [Serializable]
    public class BuildCommandsGroup : SerializableBuildCommand,IUnityPreBuildCommand,IUnityPostBuildCommand
    {
#if ODIN_INSPECTOR
        [Sirenix.OdinInspector.MultiLineProperty]
#endif 
        public string description;
    
#if ODIN_INSPECTOR
        [Sirenix.OdinInspector.InlineProperty]
        [Sirenix.OdinInspector.HideLabel]
#endif
        public BuildCommands commands = new BuildCommands();
    
        public override void Execute(IUniBuilderConfiguration configuration)
        {
            ExecuteCommands(configuration);
        }
    
        private void ExecuteCommands(IUniBuilderConfiguration configuration)
        {
            foreach (var buildCommand in commands.Commands)
            {
                var commandName = buildCommand.Name;
                
                LogBuildStep($"EXECUTE COMMAND {commandName}");
                
                var startTime = DateTime.Now;
        
                buildCommand.Execute(configuration);

                var endTime       = DateTime.Now;
                var executionTime = endTime - startTime;
                
                LogBuildStep($"EXECUTE COMMAND [{commandName}] FINISHED DURATION: {executionTime.TotalSeconds}");
            }
        }
        
        public void LogBuildStep(string message)
        {
            BuildLogger.Log($"GROUP [{Name}] : \n{message}");
        }

    }
}