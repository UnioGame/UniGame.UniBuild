using UniCore.Runtime.ProfilerTools;
using UniModules.UniGame.Core.Editor.Tools;
using UniModules.UniGame.UniBuild.Editor.ClientBuild;
using UniModules.UniGame.UniBuild.Editor.ClientBuild.Commands.PreBuildCommands;
using UniModules.UniGame.UniBuild.Editor.ClientBuild.Interfaces;
using UnityEngine;

namespace UniModules.UniGame.UniBuild
{
    [CreateAssetMenu(menuName = "UniGame/UniBuild/ScriptableBuildGroup",fileName = nameof(ScriptableBuildGroup))]
    public class ScriptableBuildGroup : UnityBuildCommand, IUnityPreBuildCommand,IUnityPostBuildCommand
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

#if ODIN_INSPECTOR
        [Sirenix.OdinInspector.Button]
#endif
        public void ExecuteGroup() => commands.Commands.ExecuteCommands();
        
        public override void Execute(IUniBuilderConfiguration configuration)
        {
            var message = $"===UNIBUILD: Execute Group {Name} ===";
            GameLog.Log(message);
            UniEditorProfiler.LogTime(message,() => ExecuteCommands(configuration));
        }

        private void ExecuteCommands(IUniBuilderConfiguration configuration)
        {
            foreach (var buildCommand in commands.Commands)
            {
                var message = $"===Execute COMMAND {buildCommand.Name} ===";
                GameLog.Log(message);
                UniEditorProfiler.LogTime(message,() => buildCommand.Execute(configuration));
            }
        }
    }
}