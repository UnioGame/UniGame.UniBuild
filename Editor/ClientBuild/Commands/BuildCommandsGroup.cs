using System;
using UniModules.UniGame.UniBuild.Editor.ClientBuild;
using UniModules.UniGame.UniBuild.Editor.ClientBuild.Commands.PreBuildCommands;
using UniModules.UniGame.UniBuild.Editor.ClientBuild.Interfaces;
using UnityEngine;

namespace UniModules.UniGame.UniBuild
{
    using global::UniGame.UniBuild.Editor.ClientBuild.Interfaces;

    [Serializable]
    public class BuildCommandsGroup : SerializableBuildCommand,IUnityPreBuildCommand,IUnityPostBuildCommand
    {
        private const string LogMessageFormat = "GROUP [{0}] : \n{1}";
        
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
                var message = $"\tEXECUTE COMMAND {commandName}";
                var logMessage = string.Format(LogMessageFormat, commandName, message);
                
                var id = BuildLogger.LogWithTimeTrack(logMessage);
        
                buildCommand.Execute(configuration);
                
                message = $"\tEXECUTE COMMAND [{commandName}] FINISHED";
                logMessage = string.Format(LogMessageFormat, commandName, message);
                
                BuildLogger.Log(logMessage,id);
            }
        }


    }
}