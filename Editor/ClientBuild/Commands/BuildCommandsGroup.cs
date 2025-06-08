using System;
using UniGame.UniBuild.Editor;
using UniGame.UniBuild.Editor.Commands.PreBuildCommands;
using UniGame.UniBuild.Editor.Interfaces;
using UnityEngine;

namespace UniModules.UniGame.UniBuild
{
    using global::UniGame.UniBuild.Editor.ClientBuild.Interfaces;

#if ODIN_INSPECTOR
     using Sirenix.OdinInspector;
#endif

#if TRI_INSPECTOR
    using TriInspector;
#endif

    [Serializable]
    public class BuildCommandsGroup : SerializableBuildCommand,IUnityPreBuildCommand,IUnityPostBuildCommand
    {
        private const string LogMessageFormat = "GROUP [{0}] : \n{1}";
        
#if ODIN_INSPECTOR
        [MultiLineProperty]
#endif 
        public string description;
    
#if  ODIN_INSPECTOR || TRI_INSPECTOR
        [InlineProperty]
        [HideLabel]
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