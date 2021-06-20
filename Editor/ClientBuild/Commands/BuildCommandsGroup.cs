using System;
using UniModules.UniGame.Core.Editor.Tools;
using UniModules.UniGame.UniBuild.Editor.ClientBuild.Commands.PreBuildCommands;
using UniModules.UniGame.UniBuild.Editor.ClientBuild.Interfaces;

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
            UniEditorProfiler.LogTime($"===BUILD COMMAND {Name} ===",() => ExecuteCommands(configuration));
        }
    
        private void ExecuteCommands(IUniBuilderConfiguration configuration)
        {
            foreach (var buildCommand in commands.Commands)
            {
                buildCommand.Execute(configuration);
            }
        }
    }
}